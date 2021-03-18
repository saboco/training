
type Cont<'T,'r> = (('T -> 'r) -> 'r)

let returnCont x = (fun k -> k x)
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
    //member this.Combine(comp1, comp2) = this.Bind(comp1, (fun () -> comp2))

let cont = ContinuationBuilder()

let c1 = fun k -> cont { return k "c1" }
let c2 = fun k -> cont { return k "c2" }

let c = fun k -> cont {
    let! a = k c1 "hola"
    let! b = k c2 "world"
    return a + b
}

let sum1K = fun x -> cont { return x + 1 }
let concatK l x = cont { return x::l }
let runKId comp1 comp2 = runCont comp1 (fun x -> comp2 x) ignore
runKId (sum1K 5) (fun x -> concatK [34] x)

(* Test callCC *) // From FSharpx tests
let sum l =
    let rec sum l = cont {
        let! result = callcc (fun exit1 -> cont {
          match l with
          | [] -> return 0
          | h::_ when h = 2 -> 
            printfn "passing on '2' branch"
            return! exit1 42
          | h::t -> let! r = sum t
                    printfn "returning from recursion r: %i, h:%i" r h
                    return h + r })
        printfn "returning from computation %i" result
        return result }
    runCont (sum l) id
    
sum [1;1;3;3] //|> should equal 8
sum [1;2;3] //|> should equal 43

/// The coroutine type from http://fssnip.net/7M
type Coroutine() =
    let tasks = new System.Collections.Generic.Queue<Cont<unit,unit>>()

    member this.Put(task) =
        let withYield = cont {
            do! callcc <| fun exit ->
                task <| (fun () ->  // can remove this part and the call after the exit())() <- this tow last parenthesis
                callcc <| 
                fun c ->
                tasks.Enqueue(c())
                exit ())() 
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

``When running a coroutine it should yield elements in turn``() 



let mutable escape : unit -> Cont<int, unit> = fun _ -> failwith "not implemented"

let sum1 i = cont { 
        printfn "this is called? %i" i
        return i + 1 }

let cont1 sum = cont {
    let! a = sum 2
    do! callcc (fun c -> 
        escape <- c 
        returnCont ())
    printfn "escaping with %i" a }

let a = cont1 sum1 
a  (fun ()  -> printfn "calling current continuation";)
escape () (fun i -> printfn "ending with %i" i)

