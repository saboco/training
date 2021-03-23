#r "paket: 
source https://api.nuget.org/v3/index.json
nuget FSharpx.Extras"

#load ".fake/State.fsx/intellisense.fsx"

open FSharpx.Collections

let equalsOn f x (yobj : obj) =
    match yobj with
    | :? 'T as y -> (f x = f y)
    | _ -> false

let hashOn f x = hash (f x)

let compareOn f x (yobj : obj) =
    match yobj with
    | :? 'T as y -> compare (f x) (f y)
    | _ ->  invalidArg "yobj" "cannont compare values of different types"

[<NoEquality;NoComparison>]
type S<'State,'Value> = S of ('State -> 'Value * 'State)

module State =

    let runS (S f) state = f state

    let returnS x =
        let run state =
            x, state
        S run

    let mapS (f : 'a -> 'b) (m : S<'s,'a>) : S<'s,'b> = 
        let run state =
            let x, newState = runS m state
            f x, newState
        S run

    let bindS f xS =
        let run state =
            let x, newState = runS xS state
            runS (f x) newState
        S run

    let applyS (ff : S<'s, 'a -> 'b>) (xS : S<'s,'a>) : S<'s,'b> =
        let run state =
            let f, s1 = runS ff state
            let a, s2 = runS xS s1
            f a, s2
        S run        

    let getS =
        let run state = state, state
        S run

    let putS newState =
        let run _ = (), newState
        S run

    type StateBuilder()=
        member __.Return(x) = returnS x
        member __.Bind(xS,f) = bindS f xS

open State

let state = new StateBuilder()


[<CustomEquality;CustomComparison>]
type Event<'next> =
    | Timeout of int * 'next
    member private x.selectSignificantValue (Timeout (v,_)) = v
    override x.Equals(yobj) =
        match yobj with
        | :? Event<'next> as y -> equalsOn x.selectSignificantValue x y
        | _ -> false
    override x.GetHashCode() = hashOn x.selectSignificantValue x

    interface System.IComparable with
        member x.CompareTo yobj =
            match yobj with
            | :? Event<'next> as y -> compareOn x.selectSignificantValue x y
            | _ -> invalidArg "yobj" "cannot compare value of different types"

// define the type to use as the state
type Stack<'a> = Stack of Event<'a> IPriorityQueue

// define pop outside of state expressions
let popStack (Stack contents) = 
    match contents.IsEmpty with
    | true -> failwith "Stack underflow"
    | _ -> 
        let (v, newStack) = contents.Pop()
        v, (Stack newStack)

// define push outside of state expressions
let pushStack newTop (Stack contents) = Stack (contents.Insert newTop)

// define an empty stack
let emptyStack<'a> = PriorityQueue.empty false |> Stack

// get the value of the stack when run
// starting with the empty stack
let getValue stackM = 
    runS stackM emptyStack |> fst

let getState stackM = 
    runS stackM emptyStack |> snd

let pop () = state {
    let! stack = getS
    let top, remainingStack = popStack stack
    do! putS remainingStack 
    return top }

let push newTop = state {
    let! stack = getS
    let newStack = pushStack newTop stack
    do! putS newStack 
    return () }

let timeout t f = state {
    do! push (Timeout (t,f)) }

let helloWorldS = state {
    do! timeout 3 (fun () -> "world ")
    do! timeout 2 (fun ()-> "hello ") 
    do! timeout 5 (fun ()-> "program") 
    do! timeout 0 (fun ()-> "This is a ") 
    do! timeout 20 (fun ()-> "!") }

let (Stack q) = helloWorldS |> getState
Seq.iter (fun (Timeout (_,f)) -> f() |> printfn "%s") q