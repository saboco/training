//*********** Continuation
type K<'T,'r> = (('T -> 'r) -> 'r)

let runK (c:K<_,_>) cont = c cont
let callcK (f: ('T -> K<'b,'r>) -> K<'T,'r>) : K<'T,'r> =
    fun cont -> runK (f (fun a -> (fun _ -> cont a))) cont

let sumlist l =
    let rec sum l k =
        match l with
        | [] -> k 0
        | h::t -> sum t (fun r -> r + h |> k)
    sum l id

sumlist [1;2;3]
sumlist [1;2;3;4]
sumlist [1;1;3;3]

(* Test callCC *) // From FSharpx tests
let testcallcK () =
    let sum l =
        let rec sum l =
            callcK (fun exit1 ->
                (fun cont ->
                    match l with
                    | [] -> cont 0
                    | h::_ when h = 2 -> (exit1 42) cont
                    | h::t -> sum t (fun r -> r + h |> cont)))
        runK (sum l) id 

    let c1 = sum [1;1;3;3]
    let c2 = sum [1;2;3;5;10]
    
    if c1 = 8 then printfn "c1: %i" c1 else failwithf "not good callcc implementation %i" c1
    if c2 = 43 then printfn "c2: %i" c2 else failwithf "not good callcc 2 implementation %i" c2

testcallcK ()

let returnK x = (fun k -> k x)
let bindK m f = (fun k -> m (fun a -> f a k))

type ContinuationBuilder() =
    member __.Return(x) = returnK x
    member __.ReturnFrom(x) =  x
    member __.Bind(m,f) =  bindK m f
    member this.Zero () = this.Return ()

let K = new ContinuationBuilder()

let (>>=) c f = bindK c f

/// The coroutine type from http://fssnip.net/7M
type Coroutine() =
    let tasks = new System.Collections.Generic.Queue<K<unit,unit>>()

    member this.Put (task) =
        printfn "putting task on coroutines chain"
        let withYield = K {
            printfn "evaluating K{}"
            do! callcK (fun exit ->
                    printfn "passing to task the continuation yield function"
                    task (
                        callcK (fun c ->
                            printfn "enqueuing continuation"
                            tasks.Enqueue(c())
                            exit ())))
            printfn "exit was called so another continuation is dequeued and executed"
            if tasks.Count <> 0 then
                do! tasks.Dequeue() }
        tasks.Enqueue(withYield)

    member this.Put1(task) =
        let withYield =
            bindK
                (callcK (fun exit ->
                    task (
                        callcK (fun c ->
                            tasks.Enqueue(c())
                            exit ()))))
                (fun () ->
                    if tasks.Count <> 0 
                    then tasks.Dequeue()
                    else returnK ())
        tasks.Enqueue(withYield)

    member this.Put2(task) =
        let withYield =
            callcK( fun exit ->
                    task( callcK (fun c ->  
                        tasks.Enqueue(c())
                        exit())
                    )
                ) >>= fun () -> 
            if tasks.Count <> 0 then
                tasks.Dequeue() 
            else returnK ()
        tasks.Enqueue withYield

    member this.Put3(task) =
        let withYield =
            fun k ->
                let m =
                    (callcK (fun exit ->
                        task (
                            callcK (fun c ->
                                tasks.Enqueue(c())
                                exit ()))))
                let f =
                    fun () ->
                        if tasks.Count <> 0 
                        then tasks.Dequeue()
                        else returnK ()
                m (fun a -> f a k)
        tasks.Enqueue(withYield)
        
    member this.Run() =
        runK (tasks.Dequeue()) ignore

// from FSharpx tests
let ``When running a coroutine it should yield elements in turn``() =
      // This test comes from the sample on http://fssnip.net/7M
      let sb = System.Text.StringBuilder()
      let coroutine = Coroutine()
      coroutine.Put(fun yield' -> K {
        printfn "appending A"
        sb.Append("A") |> ignore
        printfn "yielding from coroutine 1"
        do! yield' 
        printfn "appending B"
        sb.Append("B") |> ignore
        printfn "yielding from coroutine 1"
        do! yield' 
        printfn "appending C"
        sb.Append("C") |> ignore
        printfn "yielding from coroutine 1"
        do! yield'
      })
      coroutine.Put(fun yield' -> K {
        printfn "appending 1"
        sb.Append("1") |> ignore
        printfn "yielding from coroutine 2"
        do! yield' 
        printfn "appending 2"
        sb.Append("2") |> ignore
        printfn "yielding from coroutine 2"
        do! yield' 
      })
      printfn "start running coroutines"
      coroutine.Run()
      printfn "end running coroutines"
      let actual = sb.ToString()
      if  actual = "A1B2C" 
      then printfn "Good implementation of coroutine: %s" actual
      else failwithf "Bad implementation of coroutines: %s" actual

let ``When running a coroutine it should yield elements in turn 2``() =
      // This test comes from the sample on http://fssnip.net/7M
      let sb = System.Text.StringBuilder()
      let coroutine = Coroutine()
      coroutine.Put(fun yield' ->
        sb.Append("A") |> ignore
        yield' >>= (fun () ->
        sb.Append("B") |> ignore
        yield' >>= (fun () -> 
        sb.Append("C") |> ignore
        yield' >>= fun() -> ignore))
      )
      coroutine.Put(fun yield' ->
        sb.Append("1") |> ignore
        yield' >>= (fun() -> 
        sb.Append("2") |> ignore
        yield' >>= fun() -> ignore)
      )
      printfn "start running coroutines"
      coroutine.Run()
      printfn "end running coroutines"
      let actual = sb.ToString()
      if  actual = "A1B2C" 
      then printfn "Good implementation of coroutine: %s" actual
      else failwithf "Bad implementation of coroutines: %s" actual

let ``When running a coroutine it should yield elements in turn Put1``() =
      // This test comes from the sample on http://fssnip.net/7M
      let sb = System.Text.StringBuilder()
      let coroutine = Coroutine()
      coroutine.Put1(fun yield' -> K {
        sb.Append("A") |> ignore
        do! yield' 
        sb.Append("B") |> ignore
        do! yield' 
        sb.Append("C") |> ignore
        do! yield' 
      })
      coroutine.Put1(fun yield' -> K {
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

let ``When running a coroutine it should yield elements in turn Put2``() =
      // This test comes from the sample on http://fssnip.net/7M
      let sb = System.Text.StringBuilder()
      let coroutine = Coroutine()
      coroutine.Put2(fun yield' -> K {
        sb.Append("A") |> ignore
        do! yield' 
        sb.Append("B") |> ignore
        do! yield' 
        sb.Append("C") |> ignore
        do! yield' 
      })
      coroutine.Put2(fun yield' -> K {
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

``When running a coroutine it should yield elements in turn``()
``When running a coroutine it should yield elements in turn 2``() 
``When running a coroutine it should yield elements in turn Put1``()
``When running a coroutine it should yield elements in turn Put2``()
