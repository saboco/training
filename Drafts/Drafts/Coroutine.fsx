open System
open System.Diagnostics
open System.Net
open System.Threading
open System.Threading.Tasks

module Common =
  let clock =
    let d   = float Stopwatch.Frequency / 1000000.
    let sw  = Stopwatch ()
    sw.Start ()
    fun () ->
      float sw.ElapsedTicks / d |> int64

module Coroutine =
  open Common

  type Coroutine<'T> = ('T -> unit) -> unit

  // Monad
  let inline cobind (t : Coroutine<'T>) (uf : 'T -> Coroutine<'U>) : Coroutine<'U> =
    fun c -> t (fun v -> uf v c)

  let inline coreturn v : Coroutine<'T> = fun c -> c v

  // Arrow
  let inline coarr f = fun v -> coreturn (f v)

  let inline cokleisli tf uf = fun v -> cobind (tf v) uf

  // Applicative
  let inline copure v = coreturn v

  let inline coapply (f : Coroutine<'T -> 'U>) (t : Coroutine<'T>) : Coroutine<'U> =
    fun c -> f (fun fv -> t (fun tv -> c (fv tv)))

  // Functor
  let inline comap (m : 'T -> 'U) (t : Coroutine<'T>) : Coroutine<'U> =
    fun c -> t (fun tv -> c (m tv))

  let inline cokeepRight (t : Coroutine<'T>) (u : Coroutine<'U>) : Coroutine<'U> =
    fun c -> t (fun _ -> u c)

  module Infixes =
    let inline (>>=)  t uf = cobind       t uf
    let inline (>=>) tf uf = cokleisli   tf uf 
    let inline (>>.)  t u  = cokeepRight  t u
    let inline (<*>)  f t  = coapply      f t
    let inline (|>>)  t m  = comap        m t

  type CoroutineBuilder () =
    member inline x.Bind        (t, uf) = cobind      t uf
    member inline x.Combine     (t, u)  = cokeepRight t u
    member inline x.Return      v       = coreturn    v
    member inline x.ReturnFrom  t       = t : Coroutine<'T>
    member inline x.Zero        ()      = coreturn LanguagePrimitives.GenericZero<'T>

  let coroutine = CoroutineBuilder ()

  let codebug nm (t : Coroutine<'T>) : Coroutine<'T> =
    fun c ->
      printfn "BEFORE - %s - tid: %d" nm Thread.CurrentThread.ManagedThreadId
      let before = clock ()
      t (fun v -> 
          let after = clock ()
          let diff  = after - before
          printfn "AFTER  - %s - tid: %d - took: %d us" nm Thread.CurrentThread.ManagedThreadId diff
          c v
        )
                                                            
  let corun (t : Coroutine<'T>) (c: 'T -> unit) = t c

  module Details =
    type ChildStatus<'T> =
      | NoResult
      | HasValue          of 'T
      | HasContinuation   of ('T -> unit)
      | Done

  open Details

  let inline cochild (t : Coroutine<'T>) : Coroutine<Coroutine<'T>> =
    fun c ->
      let l   = obj ()
      let cs  = ref NoResult

      let childc : Coroutine<'T> =
        fun c ->
          lock l <| fun () ->
            match !cs with
            | NoResult            -> cs := HasContinuation c
            | HasValue        vv  -> cs := Done; c vv
            | HasContinuation __  -> failwith  "Received continuation twice"
            | Done                -> failwithf "Received continuation after"

      let tc (v : 'T) =
        lock l <| fun () ->
          match !cs with
          | NoResult            -> cs := HasValue v
          | HasValue        vv  -> failwithf "Received value twice, existing: %A, new: %A" vv v
          | HasContinuation cc  -> cs := Done; cc v
          | Done                -> failwithf "Received value after finished, new: %A" v

      t tc
      c childc

  let inline coawait (t : Task<'T>) : Coroutine<'T> = 
    fun c ->
      let cw (t : Task<'T>) = c t.Result
      t.ContinueWith cw |> ignore

  let await (c : Coroutine<'T>) : Coroutine<'T> =
    fun k ->
      let cw (c : Coroutine<'T>) = c (fun t -> k t)
      cw c

  let cojoinThreadPool : Coroutine<unit> = 
    fun c ->
      let cb _ = c ()
      ThreadPool.QueueUserWorkItem (WaitCallback cb) |> ignore

  let codownloadStringFrom (uri : Uri) : Coroutine<string> =
    let wc    = new WebClient ()
    let task  = wc.DownloadStringTaskAsync uri
    coawait task

module Examples =
  open Coroutine

  let testSimple =
    coroutine {
      let! google  = Uri "http://www.google.com"  |> codownloadStringFrom |> codebug "google"
      let! bing    = Uri "http://www.bing.com"    |> codownloadStringFrom |> codebug "bing"
      return google.Length, bing.Length
    } |> codebug "testSimple"

  let testComplex =
    coroutine {
      // do! cojoinThreadPool |> codebug "ThreadPool"
      let! cgoogle  = Uri "http://www.google.com" |> codownloadStringFrom |> codebug "google" |> cochild
      let! cbing    = Uri "http://www.bing.com"   |> codownloadStringFrom |> codebug "bing"   |> cochild
      let! google   = cgoogle
      let! bing     = cbing
      return google.Length, bing.Length
    } |> codebug "testComplex"

  let exampleAwait = 
      let c1 = coroutine { 
          printfn "hello"
          let! a = coreturn "hello"
          printfn "hola"
          let! b = coreturn "hola"
          printfn "hello(hola)"
          return a + "(" + b + ")" } |> cochild
      let c2 = coroutine {
        printfn "world"
        return "world" } |> cochild
      coroutine {
        let! c1 = await c1
        let! c2 = c2
        let! a = c1
        let! b = c2
        return a + " " + b 
      } |> codebug "testAwait"

open Coroutine
open Examples

corun exampleAwait <| printfn "%A"

[<EntryPoint>]
let main argv = 
  corun testSimple <| printfn "%A"

  printfn "Press any key"  
  Console.ReadKey () |> ignore
  0