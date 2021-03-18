type Cont<'T,'r> = (('T -> 'r) -> 'r)

module Continuation =
    let returnCont x : Cont<_,_> = (fun k -> k x)
    let bindCont (f : 'a -> Cont<'b,'r>) (m : Cont<'a,'r>) : Cont<'b,'r> = (fun k -> m (fun a -> f a k))
    let delayCont f = (fun k -> f () k)
    let runCont (c:Cont<_,_>) cont = c cont
    let callcc (f: ('T -> Cont<'b,'r>) -> Cont<'T,'r>) : Cont<'T,'r> =
        fun cont -> runCont (f (fun a -> (fun _ -> cont a))) cont

    type ContinuationBuilder() =
        member __.Return(x) = returnCont x
        member __.ReturnFrom(x) = x
        member __.Bind(m,f) = bindCont f m
        member __.Delay(f) = delayCont f
        member this.Zero () = this.Return ()

    let cont = new ContinuationBuilder()