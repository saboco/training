module Continuum =
  open System
  open System.Collections.Generic
  open System.Diagnostics
  open System.Net
  open System.Threading
  open System.Threading.Tasks

  module Common =
    let inline tid ()     = Thread.CurrentThread.ManagedThreadId
    let inline dbreak ()  = Debugger.Break ()
    let inline invoke c ctx continuation = c (struct (ctx, continuation))
  open Common

  // TODO: Implement Fork/Join

  type CoStackFrame =
  | CoFinally   of (unit -> unit)
  | CoUsing     of IDisposable
  | CoWith      of (exn -> unit -> unit)

  [<RequireQualifiedAccess>]
  type Coresult<'T> =
  | Success     of 'T
  | Exception   of exn
//    | Cancelled

  [<RequireQualifiedAccess>]
  type CounrollAction =
  | Execute   of (unit -> unit)
  | Exception of exn
  | Continue

  type [<AbstractClass>] CoContext () =
    class
      let costack = Stack<CoStackFrame> 16

      let rec run raiser a =
        let activeException =
          try
            a ()
            null
          with
          | e -> e
        if activeException <> null then 
          unroll raiser activeException
      and unroll raiser activeException =
        if costack.Count > 0 then
          let action =
            // TODO: Avoid try/with per pop
            try
              match costack.Pop () with
              | CoFinally f -> 
                f ()
                CounrollAction.Continue
              | CoUsing   d -> 
                if d <> null then d.Dispose ()
                CounrollAction.Continue
              | CoWith    e -> 
                if activeException <> null then 
                  e activeException |> CounrollAction.Execute 
                else 
                  CounrollAction.Continue
            with 
            | e -> CounrollAction.Exception e
          match action with
          | CounrollAction.Execute    a   -> run raiser a
          | CounrollAction.Exception  e   -> unroll raiser e  // How to make activeException the inner exception?
          | CounrollAction.Continue       -> unroll raiser activeException
        else              
          if activeException <> null then
            raiser activeException

      member x.Continue (a : unit -> unit) =
        run (fun e -> x.Raise e) a

      member x.PushFrame csf =
        costack.Push csf

      member x.PopFrame () =
        match costack.Pop () with
        | CoUsing   d -> if d <> null then d.Dispose ()
        | CoFinally f -> f ()
        | CoWith    _ -> ()

      abstract Raise: exn -> unit
    end
  type [<Sealed>] CoContext<'T> (continuation : Coresult<'T> -> unit) =
    class
      inherit CoContext ()

      member x.Run (t : Coroutine<'T>) =
        let a ()  = invoke t (x :> CoContext) (fun v -> continuation (Coresult.Success v))
        x.Continue a

      override x.Raise e =
        continuation (Coresult.Exception e)

    end
  and Coroutine<'T> = (struct (CoContext*('T -> unit))) -> unit

  // Monad

  let inline coreturn (v : 'T) : Coroutine<'T> =
    fun struct (ctx, continuation) -> 
      continuation v

  let inline cobind (t : Coroutine<'T>) (uf : 'T -> Coroutine<'U>) : Coroutine<'U> =
    fun struct (ctx, continuation) -> 
      invoke t ctx (fun tv ->
          let u = uf tv
          invoke u ctx continuation
        )

  let inline cocombine (t : Coroutine<'T>) (u : Coroutine<'U>) : Coroutine<'U> =
    fun struct (ctx, continuation) -> 
      invoke t ctx (fun _ -> invoke u ctx continuation)

  // Arrow

  let inline coarr f v = coreturn (f v)

  let inline cokleisli tf uf = fun tv -> cobind (tf tv) uf

  // Applicative

  let inline copure f = coreturn f

  let inline coap (f : Coroutine<'T -> 'U>) (t : Coroutine<'T>) : Coroutine<'U> =
    fun struct (ctx, continuation) -> 
      invoke f ctx (fun fv -> invoke t ctx (fun tv -> continuation (fv tv)))

  // Functor

  let inline comap (m : 'T -> 'U) (t : Coroutine<'T>) : Coroutine<'U> =
    fun struct (ctx, continuation) -> 
      invoke t ctx (fun tv -> continuation (m tv))

  let inline cotryFinally (t : Coroutine<'T>) (f : unit -> unit) : Coroutine<'T> =
    fun struct (ctx, continuation) -> 
      ctx.PushFrame (CoFinally f)
      invoke t ctx (fun tv -> ctx.PopFrame (); continuation tv)

  let inline cotryWith (t : Coroutine<'T>) (ef : exn -> Coroutine<'T>) : Coroutine<'T> =
    fun struct (ctx, continuation) -> 
      ctx.PushFrame (CoWith (fun e () -> invoke (ef e) ctx continuation))
      invoke t ctx (fun tv -> ctx.PopFrame (); continuation tv)

  let inline cousing (v : #IDisposable) (uf : 'T -> Coroutine<'U>) : Coroutine<'U> =
    fun struct (ctx, continuation) -> 
      ctx.PushFrame (CoUsing v)
      let u = uf v
      invoke u ctx (fun uv -> ctx.PopFrame (); continuation uv)

  type CoroutineBuilder () =
    member inline x.Bind        (t, uf) = cobind        t uf
    member inline x.Combine     (t, u)  = cocombine     t u
    member inline x.Return      v       = coreturn      v
    member inline x.ReturnFrom  t       = t             : Coroutine<_>
    member inline x.TryFinally  t a     = cotryFinally  t a
    member inline x.TryWith     t ef    = cotryWith     t ef
    member inline x.Using       (v, uf) = cousing       v uf
    member inline x.Zero        ()      = coreturn      LanguagePrimitives.GenericZero<_>
  let coroutine = CoroutineBuilder ()
  
  module Infixes =
    let inline (<*>)  f t  = coap       f t
    let inline (>>=)  t uf = cobind     t uf
    let inline (>>.)  t u  = cocombine  t u
    let inline (>=>) tf uf = cokleisli tf uf
    let inline (|>>)  t m  = comap      m t

  let inline codebug name (t : Coroutine<'T>) : Coroutine<'T> =
    fun struct (ctx, continuation) -> 
      printfn "BEFORE - %s - tid: %d" name (tid ())
      invoke t ctx  (fun tv -> 
                      printfn "AFTER  - %s - tid: %d" name (tid ())
                      continuation tv
                    )

  let inline cobreak (t : Coroutine<'T>) : Coroutine<'T> =
    fun struct (ctx, continuation) -> 
      dbreak ()
      invoke t ctx  (fun tv -> 
                      dbreak ()
                      continuation tv
                    )

  let inline codelay (tf : unit -> Coroutine<'T>) : Coroutine<'T> =
    fun struct (ctx, continuation) -> 
      let t = tf ()
      invoke t ctx continuation

  let inline corun (t : Coroutine<'T>) (continuation : Coresult<'T> -> unit) : unit =
    let ctx = CoContext<_> continuation
    ctx.Run t

  let inline coadaptTask (t : Task<'T>) : Coroutine<'T> =
    fun struct (ctx, continuation) -> 
      t.ContinueWith (fun (t : Task<'T>) -> ctx.Continue (fun () -> continuation t.Result)) |> ignore

  let codownloadStringFrom (uri : Uri) : Coroutine<string> =
    fun () -> cousing (new WebClient ()) (fun wc -> coadaptTask (wc.DownloadStringTaskAsync uri))
    |> codelay

module Samples =
  open Continuum
  open Continuum.Infixes
  open System

  let inline len (s : string) = s.Length

  let test =
    coroutine {
      let! google = codownloadStringFrom (Uri "http://google.com")  |> codebug "google"
      let! bing   = codownloadStringFrom (Uri "http://bing.com")    |> codebug "bing"
      return google.Length, bing.Length
    } |> codebug "test"

  let test2 =
    copure (fun google bing -> len google, len bing)
    <*> codownloadStringFrom (Uri "http://google.com")  |> codebug "google"
    <*> codownloadStringFrom (Uri "http://bing.com")    |> codebug "bing"

  let dtest =
    codownloadStringFrom (Uri "http://google.com") |>> len |> cobreak

  let run () =
    printfn "Starting..."
    corun test (printfn "Result: %A")
    corun test2 (printfn "Result: %A")
    printfn "Executing"

[<EntryPoint>]
let main argv = 
  try
    Samples.run ()
    
    printfn "Press any key..."
    System.Console.ReadKey () |> ignore
    0
  with
  | e -> 
    printfn "Caught: %s" e.Message
    999
