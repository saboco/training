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

type Coroutine() =
    let tasks = new System.Collections.Generic.Queue<K<unit, unit>>()

    member this.Put (task) =
    
        let withYield = K {
            do! callcK (fun exit ->
                    task (fun () -> 
                        callcK (fun c ->
                            tasks.Enqueue(c())
                            exit ())))
            if tasks.Count <> 0 then
               do! tasks.Dequeue() }
        tasks.Enqueue(withYield)

    member this.Run() =
         runK (tasks.Dequeue()) id

// from FSharpx tests
let ``When running a coroutine it should yield elements in turn``() =
      
      let coroutine = Coroutine()
      coroutine.Put(fun yield' -> K {
        do! yield' ()
        printfn "hola co1"
        do! yield' ()
        printfn "world co1"
        do! yield' ()
        return ()
      })
      coroutine.Put(fun yield' -> K {
        printfn "hola co2"
        do! yield' ()
        printfn "world co2"
        do! yield' ()
        return ()
      })
      
      let actual = coroutine.Run()
      if  actual = "holaworldlast" 
      then printfn "Good implementation of coroutine: %s" actual
      else failwithf "Bad implementation of coroutines: %s" actual


``When running a coroutine it should yield elements in turn``()

