// ----------------------------------------------------------------------------
open System
open System.Threading
open System.Collections.Generic
// ----------------------------------------------------------------------------
type Continuation<'T> = 'T -> unit
type Coroutine<'T>    = Continuation<'T> -> unit
// ----------------------------------------------------------------------------
module Coroutine =
  let Bind (t : Coroutine<'T>) (fu : 'T -> Coroutine<'U>) : Coroutine<'U> = 
    fun continuation ->
      let inner tv = 
        let u = fu tv
        u continuation
      t inner

  let Combine (t : Coroutine<'T>) (u : Coroutine<'U>) : Coroutine<'U> = 
    fun continuation ->
      let inner _ = 
        u continuation
      t inner

  let Delay (ft : unit -> Coroutine<'T>) : Coroutine<'T> =
    fun continuation ->
      let t = ft ()
      t continuation

  let For (s : seq<'T>) (ft : 'T -> Coroutine<unit>) : Coroutine<unit> =
    fun continuation ->
      let e = s.GetEnumerator ()
      let rec loop () =
        if e.MoveNext () then
          let t = ft e.Current
          t loop
        else
          e.Dispose ()
          continuation ()
      loop ()

  let Return v : Coroutine<'T> = 
    fun continuation ->
      continuation v

  let ReturnFrom t : Coroutine<'T> = t

  let While (test : unit -> bool) (t : Coroutine<unit>) : Coroutine<unit> =
    fun continuation ->
      let rec loop () =
        if test () then
          t loop
        else
          continuation ()
      loop ()

  let Zero : Coroutine<unit> =
    fun continuation ->
      continuation ()

  type CoroutineBuilder() =
    member x.Bind (t, fu)   = Bind t fu
    member x.Combine (t,u)  = Combine t u
    member x.Delay (ft)     = Delay ft
    member x.For (s, ft)    = For s ft
    member x.Return (v)     = Return v
    member x.ReturnFrom (t) = ReturnFrom t
    member x.While (test, t)= While test t
    member x.Zero ()        = Zero

let coroutine = Coroutine.CoroutineBuilder()

let (Yield : Coroutine<unit>, RunYielded: unit -> unit) =
  let yielded = Queue<unit->unit> ()
  let y continuation =
    yielded.Enqueue continuation
  let run () =
    while yielded.Count > 0 do
      yielded.Dequeue () ()
  y, run

let ( >>= ) (t, fu) = Coroutine.Bind t fu

let Child name (t : Coroutine<'T>) : Coroutine<Coroutine<'T>> =
  fun continuation ->

    let rc = ref None
    let rv = ref None

    let nt : Coroutine<'T> = 
      fun nc ->
        match !rv with
        | Some v  -> 
          printfn "%s nt called with value %A" name v
          nc v
        | None    ->
          printfn "%s setting rc=nc=%A" name nc
          rc := Some nc

    let childc v = 
      printfn "%s childc called with %A" name v
      match !rc with
      | Some c  -> 
        printfn "%s childc can continue with c=%A an v=%A" name c v
        c v
      | None    ->
        printfn "%s setting rv with v=%A" name v
        rv := Some v
    
    t childc
    
    continuation nt

let Run (cr : Coroutine<'T>) : 'T =
  let value = ref None

  cr <| fun v -> value := Some v 

  RunYielded ()

  (!value).Value
// ----------------------------------------------------------------------------

let Trace (kind : string) (i : int) =
  let tid = Thread.CurrentThread.ManagedThreadId;
  Console.WriteLine ("{0} - {1}: {2}", tid, kind, i);

let Pop (q : Queue<'T>) =
  coroutine {
    while q.Count = 0 do
      do! Yield

    return q.Dequeue ()
  }

let Push (q : Queue<'T>) (v : 'T) : Coroutine<unit> =
  let waitfor = TimeSpan(0,0,0,0,200)
  let sw = new System.Diagnostics.Stopwatch()
  sw.Start()
  coroutine {

    while sw.Elapsed < waitfor do
      do! Yield

    sw.Stop()
    sw.Reset()

    return q.Enqueue v }

let queue = Queue<int> ()

let rec Popper =
 coroutine {
  let! first = Pop queue
  let mutable v = first

  while v > -1 do
    Trace "Popped" v
    let! rest = Pop queue
    v <- rest

  Trace "Pop done" -1
  return ()
 }

let Pusher =
 coroutine {
  for v = 0 to 10 do
    Trace "Pushed" v
    do! Push queue v
    //do! Yield

  Trace "Push done" -1
  do! Push queue -1
  //do! Yield
 }

let sample = 
  coroutine {
    Trace "Sample"  0
    printfn "Creating child popper"
    let! popper = Child "POPPER ---->" Popper
    printfn "Creating child pusher"
    let! pusher = Child "----> PUSHER" Pusher

    printfn "running poper"
    do! popper
    printf "running pusher"
    do! pusher

    return "hola"
  }

let sample2 = 
  coroutine {
    Trace "Sample"  0
    do! Popper
    do! Yield 
    do! Pusher
    do! Yield

    return "sample2"
  }

Run sample
Run sample2
