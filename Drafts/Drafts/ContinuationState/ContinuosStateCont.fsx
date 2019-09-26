type Cont<'T,'r> = (('T -> 'r) -> 'r)

let returnCont x = (fun k -> k x)
let mapCont (f : 'a -> 'b) (m : Cont<'a,'r>) : Cont<'b,'r> = (fun k -> m (fun a -> k (f a)))
let bindCont f m = (fun k -> m (fun a -> f a k))
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

type StateK<'State,'Value,'r> = StateK of ('State -> Cont<'Value * 'State, 'r>)

module StateK =

    let returnSK x =
        let run state = cont {
            return x, state
        }
        StateK run

    let runSK (StateK fSK : StateK<'s,'a,'r>) (state : 's) : Cont<'a * 's, _> = cont {
        return! fSK state }

    let mapSK (f : 'a -> 'b) (m : StateK<'s,'a,'r>) : StateK<'s,'b,'r> =
            let run state = cont {
                let! x, newState = runSK m state
                return f x, newState  }
            StateK run

    let bindSK (f : 'a -> StateK<'s,'b,'r>) (xSK : StateK<'s,'a,'r>) : (StateK<'s,'b,'r>) =
        let run state = cont {
            let! x, newState = runSK xSK state
            return! runSK (f x) newState }
        StateK run

    let applySK (fS : StateK<'s, 'a -> 'b, 'r>) (xSK : StateK<'s,'a,'r>) : StateK<'s,'b,'r> =
        let run state = cont {
            let! f, s1 = runSK fS state
            let! x, s2 = runSK xSK s1
            return f x, s2 }
        StateK run

    let getSK =
        let run state = cont { return state, state }
        StateK run

    let putSK newState =
        let run _ = cont { return (), newState }
        StateK run

    type StateKBuilder() =
        member __.Return(x) = returnSK x
        member __.ReturnFrom (x) = x
        member __.Bind(xS,f) = bindSK f xS
        member this.Zero() = this.Return ()

    let stateK = new StateKBuilder()

module StackCont =
    open StateK

    type Stack<'a> = Stack of 'a list

    let popStack (Stack contents) =
        match contents with
        | [] -> failwith "Stack underflow"
        | head::tail ->
            head, (Stack tail)

    let pushStack newTop (Stack contents) =
        Stack (newTop::contents)

    let emptyStack = Stack []

    let getValueSK stackM = cont {
        let! f = runSK stackM emptyStack
        return f |> fst }

    let pop() = stateK {
        let! stack = getSK
        let top, remainingStack = popStack stack
        do! putSK remainingStack
        return top }

    let push newTop = stateK {
        let! stack = getSK
        let newStack = pushStack newTop stack
        do! putSK newStack
        return () }

open StateK
open StackCont

let helloWorldSK = (fun () -> stateK {
    do! push "world"
    do! push "hello"
    let! top1 = pop()
    let! top2 = pop()
    let combined = top1 + " " + top2
    return combined
})

let helloWorld = getValueSK (helloWorldSK ()) id
printfn "%s" helloWorld

type ContSK<'s,'T,'r> = (Cont<StateK<'s,'T,'r>, 'r>)

module ContSK =

    let runKS (f : ContSK<'s,'a,'r>) (state : 's) : Cont<'a * 's,'r> = cont {
        let! fSK = f
        return! runSK fSK state
    }

    let returnKS x : ContSK<'s,'a,'r> = x |> returnSK |> returnCont

    let mapKS (f : 'a -> 'b) (m : ContSK<'s,'a,'r>) : ContSK<'s,'b,'r> =  m |>  mapCont (mapSK f)

    let bindKS (f : 'a -> ContSK<'s,'b,'r>) (m : ContSK<'s,'a,'r>) : ContSK<'s,'b,'r> =
        let run state = cont{
            let! xSK = m
            let! x, newState = runSK xSK state
            let! fSK = f x
            return!  runSK fSK newState
        }
        StateK run |> returnCont

    let applyKS (fK : ContSK<'s, 'a -> 'b, 'r>) (xKS : ContSK<'s,'a,'r>) : ContSK<'s,'b,'r> =
        let run state = cont {
            let! fS = fK
            let! xSK = xKS
            let! f, s1 = runSK fS state
            let! x, s2 = runSK xSK s1
            return f x, s2 }
        StateK run |> returnCont

    let getKS : ContSK<'a, 'a,'r> = (fun k ->
        let run state = cont { return state, state }
        StateK run |> k)

    let putKS newState =
        let run _ = cont { return (), newState }
        StateK run |> returnCont

    type ContSKBuilder () =
        member __.Return(x) = returnKS x
        member __.ReturnFrom (x) = x
        member __.Bind(xKS, f) = bindKS f xKS
        member this.Zero() = this.Return ()

    let contSK = new ContSKBuilder()

module StackContSK =
    open ContSK

    type Stack<'a> = Stack of 'a list

    let popStack (Stack contents) =
        match contents with
        | [] -> failwith "Stack underflow"
        | head::tail ->
            head, (Stack tail)

    let pushStack newTop (Stack contents) =
        Stack (newTop::contents)

    let emptyStack = Stack []

    let getValueKS stackM = cont {
        let! f = runKS stackM emptyStack
        return f |> fst }

    let pop() = contSK {
        let! stack = getKS
        let top, remainingStack = popStack stack
        do! putKS remainingStack
        return top }

    let push newTop = contSK {
        let! stack = getKS
        let newStack = pushStack newTop stack
        do! putKS newStack
        return () }

open StackContSK
open ContSK

let helloWorldKS = (fun () -> contSK {
    do! push "world"
    do! push "hello"
    let! top1 = pop()
    let! top2 = pop()
    let combined = top1 + " " + top2
    return combined
})

let helloWorldk = getValueKS (helloWorldKS ()) id
printfn "%s" helloWorldk
