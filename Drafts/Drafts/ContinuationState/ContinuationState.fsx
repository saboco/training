type State<'State,'Value> = State of ('State -> 'Value * 'State)

module State =

    let returnS x =
        let run state =
            x, state
        State run
    
    let runS (State f) state = f state

    let bindS f xS =
        let run state =
            let x, newState = runS xS state
            runS (f x) newState
        State run

    let getS =
        let run state = state, state
        State run

    let putS newState =
        let run _ = (), newState
        State run

    type StateBuilder()=
        member __.Return(x) = returnS x
        member __.Bind(xS,f) = bindS f xS

type Cont<'T,'r> = (('T -> 'r) -> 'r)

module Continuation =
    let returnCont x : Cont<_,_> = (fun k -> k x)
    let bindCont (f : 'a -> Cont<'b,'r>) (m : Cont<'a,'r>) : Cont<'b,'r> = (fun k -> m (fun a -> f a k))
    let mapCont (f : 'a -> 'b) (xK : Cont<'a,'r>) : Cont<'b,'r> = (fun k -> xK (fun x -> f x |> k))
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

module ContinuousState =

    let retn x = x |> State.returnS |> Continuation.returnCont

    let bind f xSK : Cont<_,_> = Continuation.cont {
        let! xS = xSK
        let run state = 
            let x, newState = State.runS xS state
            let s = Continuation.runCont (f x) id //not good
            State.runS s newState
        return (State run)
    }

    let runSK (f : Cont<_,_>) state = f (fun (State s) -> s state)

    let getSK : Cont<_,_> = (fun k ->
        let run state = state, state
        State run |> k)

    let putSK newState = Continuation.cont {
        let run _ = (), newState
        return State run }

    let delaySK f = (fun k -> f () k)

    type ContinuousState() =
        member __.Return(x) = retn x
        member __.ReturnFrom(x) = x
        member __.Bind(m,f) = bind f m
        member this.Zero () = this.Return ()

    let stateK = new ContinuousState()

    type Stack<'a> = Stack of 'a list

    let popStack (Stack contents) = 
        match contents with
        | [] -> failwith "Stack underflow"
        | head::tail ->     
            head, (Stack tail)
            
    let pushStack newTop (Stack contents) = 
        Stack (newTop::contents)

    let emptyStack = Stack []

    let getValueSK stackM = 
        runSK stackM emptyStack |> fst

    let pop () = stateK {
        let! stack = getSK
        let top, remainingStack = popStack stack
        do! putSK remainingStack
        return top }

    let push newTop = stateK {
        let! stack = getSK
        let newStack = pushStack newTop stack
        do! putSK newStack 
        return () }


open ContinuousState

let helloWorldSK = (fun k -> stateK {
    do! push "world"
    do! push "hello"
    let! top1 = pop()
    let! top2 = pop()
    let combined = top1 + " " + top2 
    return combined
})

let helloWorld =  getValueSK (helloWorldSK id)
printfn "%s" helloWorld

open Continuation
open ContinuousState


type Coroutine() =

    let tasks = new System.Collections.Generic.Queue<Cont<unit,unit>>()

    member this.Put (task) =
    
        let withYield = cont {
            do! callcc (fun exit ->
                    task (
                        callcc (fun c ->
                            tasks.Enqueue(c())
                            exit () )))
            if tasks.Count <> 0 then
                do! tasks.Dequeue() }
        tasks.Enqueue(withYield)

    member this.Run() =
        runCont (tasks.Dequeue()) ignore

// from FSharpx tests
let ``When running a coroutine it should yield elements in turn``() =
      // This test comes from the sample on http://fssnip.net/7M
      let sb = System.Text.StringBuilder()
      let coroutine = Coroutine()
      coroutine.Put(fun yield' -> cont {
        sb.Append("A") |> ignore
        do! yield' 
        sb.Append("B") |> ignore
        do! yield' 
        sb.Append("C") |> ignore
        do! yield' 
      })
      coroutine.Put(fun yield' -> cont {
        sb.Append("1") |> ignore
        do! yield' 
        sb.Append("2") |> ignore
        do! yield' 
      })
      coroutine.Run()
      let actual = sb.ToString()
      if  actual = "A1B2C" 
      then printfn "Good implementation of coroutine: %s" actual
      else failwithf "Bad implementation of coroutines: %s" actual

let ``When running a coroutine it should yield elements in turn with state``() =
      // This test comes from the sample on http://fssnip.net/7M
      let sb = System.Text.StringBuilder()
      let coroutine = Coroutine()
      coroutine.Put(fun yield' -> cont {
        sb.Append("A") |> ignore
        do! yield' 
        sb.Append("B") |> ignore
        do! yield' 
        sb.Append("C") |> ignore
        do! yield' 
      })
      coroutine.Put(fun yield' -> cont {
        sb.Append("1") |> ignore
        do! yield' 
        sb.Append("2") |> ignore
        do! yield' 
      })
      coroutine.Run()
      let actual = sb.ToString()
      if  actual = "A1B2C" 
      then printfn "Good implementation of coroutine: %s" actual
      else failwithf "Bad implementation of coroutines: %s" actual


module StateK =

    type State<'State,'Value> = State of ('State -> 'Value * 'State)
    type StateK<'s,'T> = ((State<'s,'T> -> 'T * 's) -> 'T * 's)

    let returnCont x : StateK<'s,'a> = (fun k -> k x)

    let returnSK x =
        let run state =
            x, state
        State run |> returnCont

    let runSK (f : ((State<'s,'b> -> 'b * 's) -> 'b * 's)) state = f (fun (State xS) ->  xS state)

    let bindSK (f : 'a -> StateK<'s,'b>) (xS :StateK<'s,'a>) : StateK<'s,'b> =
        let run state =
            let x, newState = runSK xS state
            runSK (f x) newState
        returnCont (State run) // is this right? as far as I cant tell the previous (next?) continuation is encapsulated on run so this is only so the return type conforms with what is expected of a bind

    let getSK k =
        let run state = state, state
        State run |> k

    let putSK newState =
        let run _ = (), newState
        State run |> returnCont

    type StateKBuilder()=
        member __.Return(x) = returnSK x
        member __.Bind(xS,f) = bindSK f xS

    let stateK = new StateKBuilder()

    type Stack<'a> = Stack of 'a list

    let popStack (Stack contents) = 
        match contents with
        | [] -> failwith "Stack underflow"
        | head::tail ->
            head, (Stack tail)
            
    let pushStack newTop (Stack contents) = 
        Stack (newTop::contents)

    let emptyStack = Stack []

    let getValueS stackM = 
        runSK stackM emptyStack |> fst

    let pop () = stateK {
        let! stack = getSK
        let top, remainingStack = popStack stack
        do! putSK remainingStack
        return top }

    let push newTop = stateK {
        let! stack = getSK
        let newStack = pushStack newTop stack
        do! putSK newStack 
        return () }


    let helloWorldSK = (fun k -> stateK {
        do! push "world"
        do! push "hello"
        let! top1 = pop()
        let! top2 = pop()
        let combined = top1 + " " + top2 
        return combined
    })

    let helloWorld =  getValueS (helloWorldSK id)
    printfn "%s" helloWorld
