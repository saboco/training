type Coroutine<'a,'b> = unit -> CoroutineStep<'a,'b>
        and CoroutineStep<'a,'b> =
          | Ready of 'a
          | Yield of 'b * Coroutine<'a,'b>

let rec bind (f : 'a -> Coroutine<'b,'c>) (m : Coroutine<'a,'c>) : Coroutine<'b,'c> =
        fun s ->
          match m s with
          | Ready x -> f x s
          | Yield (b, m') -> Yield (b,(bind f m'))

let ret x  = fun () -> Ready x
let empty = fun () -> Ready ()

type CoroutineBuilder() =
    member __.Return(x:'a) : Coroutine<'a,'b> = ret x
    member __.ReturnFrom(s:Coroutine<'a,'b>) = s
    member __.Bind(p, k) : Coroutine<'b,'c> = bind k p
    member this.Zero() : Coroutine<unit,'a> = this.Return ()
    member __.Delay s =  s ()
    member __.Run s = s
    member this.Combine(p1:Coroutine<_,_>, p2:Coroutine<_,_>) : Coroutine<_,_> =
        this.Bind(p1, fun _ -> p2)

let co = CoroutineBuilder()

let yield' a : Coroutine<unit,'a> = fun () -> Yield (a,empty)

let co_step = function
    | Ready r -> co { return r }
    | Yield (x,f) -> fun () -> Yield (x,f)

let rec run c = 
    match c () with
    | Ready r -> r
    | Yield (x, k) -> run k

let co1 = co {
    do! yield' "hello"
    return ()
}

let co2 = co {
    do! yield' "world"
    return ()
}

run co2