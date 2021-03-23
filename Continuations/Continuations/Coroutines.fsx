#load "../State/.fake/State.fsx/intellisense.fsx"

let equalsOn f x (yobj : obj) =
    match yobj with
    | :? 'T as y -> (f x = f y)
    | _ -> false

let hashOn f x = hash (f x)

let compareOn f x (yobj : obj) =
    match yobj with
    | :? 'T as y -> compare (f x) (f y)
    | _ ->  invalidArg "yobj" "cannont compare values of different types"

type EventType =
    | Timeout of int
    | Resource of string

[<CustomEquality;CustomComparison>]
type Event<'next> = 
    | EventType of EventType * 'next
    member private x.selectSignificantValue (EventType (v,_)) = v
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

//*********** Continuation
type K<'T,'r> = (('T -> 'r) -> 'r)

let runK (c:K<_,_>) cont = c cont
let callcK (f: ('T -> K<'b,'r>) -> K<'T,'r>) : K<'T,'r> =
    fun cont -> runK (f (fun a -> (fun _ -> cont a))) cont    
let returnK x = (fun k -> k x)
let bindK m f = (fun k -> m (fun a -> f a k))

type ContinuationBuilder() =
    member __.Return(x) = returnK x
    member __.ReturnFrom(x) =  x
    member __.Bind(m,f) =  bindK m f
    member this.Zero () = this.Return ()

let K = new ContinuationBuilder()

let (>>=) c f = bindK c f

open FSharpx.Collections
type Coroutine() =
    let mutable tasks : IPriorityQueue<Event<K<unit,unit>>> = PriorityQueue.empty false
    let sb = new System.Text.StringBuilder()
    let mutable logicalClock = 0
    let updateLogicalClock t =
        if t > logicalClock then
            logicalClock <- t
    let updateState ev =
        match ev with
        | Timeout t ->
            updateLogicalClock t
            printfn "Timeout after (+) %i is %i" t logicalClock 
        | Resource s -> sb.Append(s) |> ignore

    member this.Put (task) =
    
        let withYield = K {
            do! callcK (fun exit ->
                    task (fun (ev:EventType) -> 
                        callcK (fun c ->
                            match ev with
                            | Timeout t ->
                                tasks <- PriorityQueue.insert (EventType(Timeout (logicalClock + t),c())) tasks
                            | Resource s -> 
                                tasks <- PriorityQueue.insert (EventType(Timeout logicalClock,c())) tasks
                                sb.Append(s) |> ignore
                            exit ())))
            if tasks.Length <> 0 then
               let (EventType (ev,k),tasks') = PriorityQueue.pop tasks
               tasks <- tasks'
               updateState ev
               do! k
            else printfn "%s" (sb.ToString()) }
        tasks <- PriorityQueue.insert (EventType (Timeout 0,withYield)) tasks

    member this.Run() =
        let (EventType (ev,k),tasks') = PriorityQueue.pop tasks
        tasks <- tasks'
        updateState ev
        printfn "Starting run on time %i" logicalClock
        runK k id

let timeout t = Timeout t
let resource s = Resource s

// from FSharpx tests
let ``When running a coroutine it should yield elements in turn``() =
      
      let coroutine = Coroutine()
      coroutine.Put(fun yield' -> K {
        do! yield' <| timeout 1
        do! yield' <| Resource "LO"
        printfn "LO :Yield at time 1"
        do! yield' <| timeout  5
        do! yield' <| Resource "R"
        printfn "R :Yield at time 6"
        do! yield' <| timeout 1
        do! yield' <| Resource "L"
        printfn "L :Yield at time 7"
        return ()
      })
      coroutine.Put(fun yield' -> K {
        do! yield' <| Resource "HE"
        printfn "HE :Yield At Time 0"
        do! yield' <| timeout 4
        do! yield' <| Resource "W"
        printfn "W :Yield at time 4"
        do! yield' <| timeout 1
        do! yield' <| Resource "O"
        printfn "O :Yield at time 5"
        do! yield' <| timeout 3
        do! yield' <| Resource "D"
        printfn "D :Yield at time 8"
        return ()
      })
      
      coroutine.Run()

``When running a coroutine it should yield elements in turn``()

let c : K<int, string> = returnK 5
runK c (fun i -> printfn "passed param %i" i; string i)
let cc : K<int,string> = 
    callcK (fun k -> 
        printfn "this will be executed"
        runK (k 9) (fun p -> printfn "continuation called with %A" p; p) 
        printfn "this won't be executed"
        returnK -1)

runK cc (fun _ -> c string)