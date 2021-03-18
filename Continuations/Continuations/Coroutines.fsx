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
    let mutable logicalClock = 0
    let updateLogicalClock t =
        if t > logicalClock then
            logicalClock <- t

    member this.Put (task) =
    
        let withYield = K {
            do! callcK (fun exit ->
                    task (fun (t:int) -> 
                        callcK (fun c ->
                            tasks <- PriorityQueue.insert (Timeout(logicalClock + t,c())) tasks
                            exit ())))
            if tasks.Length <> 0 then
               let (Timeout (t,k),tasks') = PriorityQueue.pop tasks
               tasks <- tasks'
               updateLogicalClock t
               printfn "Timeout after (+) %i is %i" t logicalClock 
               do! k }
        tasks <- PriorityQueue.insert (Timeout (0,withYield)) tasks

    member this.Run() =
        let (Timeout (t,k),tasks') = PriorityQueue.pop tasks
        tasks <- tasks'
        printfn "Starting run on time %i" logicalClock
        runK k id

// from FSharpx tests
let ``When running a coroutine it should yield elements in turn``() =
      
      let coroutine = Coroutine()
      coroutine.Put(fun yield' -> K {
        do! yield' 1
        printfn "LO :Yield at time 1"
        do! yield' 5
        printfn "R :Yield at time"
        do! yield' 1
        printfn "L :Yield at time"
        return ()
      })
      coroutine.Put(fun yield' -> K {
        printfn "HE :Yield At Time 0"
        do! yield' 4
        printfn "W :Yield at time 4"
        do! yield' 1
        printfn "O :Yield at time 5"
        do! yield' 3
        printfn "D :Yield at time 8"
        return ()
      })
      
      coroutine.Run()


``When running a coroutine it should yield elements in turn``()