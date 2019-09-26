
//******* normal version
let hello name =
    sprintf "hello %s" name

let world () =
    "world"

let helloworld () =
    let h = hello "santiago"
    let w = world ()
    h + " " + w

let r = helloworld ()

//********* CPS version
let helloK name k =
    sprintf "hello %s" name |> k
  
let worldK k =
    k "world"

let helloworldK k =
    helloK "santiago" (fun h -> worldK (fun w -> h + " " + w |> k))

let rK = helloworldK id


//******************
let runCont (c: (('T -> 'r) -> 'r)) cont = c cont

let callcc  (f: ('T -> (('b -> 'r) -> 'r)) -> (('T -> 'r) -> 'r)) : (('T -> 'r) -> 'r) =
    fun cont ->
        runCont (f (fun a -> printfn "k called with %A" a;(fun _ -> printfn "cont called with %A" a;cont a))) cont

let foo (x : int) =
        callcc (fun k ->
            k (x + 2)
            (fun a -> (x + 1)))
    
let a = runCont  (foo 3) id

type Cont<'T,'r> = (('T -> 'r) -> 'r)
let returnK x = (fun k -> k x)
let runK (c:Cont<_,_>) cont = c cont    
let callcK (f: ('T -> Cont<'b,'r>) -> Cont<'T,'r>) : Cont<'T,'r> =
    fun cont -> runCont (f (fun a -> printfn "k called with %A" a;(fun _ -> printfn "cont called with %A" a;cont a))) cont

let bar (x : int) =
    callcK (fun exit ->
        (exit 3 id) |> ignore
        returnK (x + 10))

let z = (bar 5) id

let bar' (x : int) =
    (fun cont ->
        runK ((cont 3) |> ignore; returnK (x + 10)) cont)

let b = (bar' 5) id

let factorial n =
    let rec loop n acc =
        match n with
        | 0 -> acc
        | i -> loop (n - 1) (acc * i)
    loop n 1

let a' = factorial 3

let factorialK n k =
    let rec loop n acc =
        match n with
        | 0 -> k acc
        | i -> loop (n - 1) (i * acc)
    loop n 1

factorialK 3 (fun x -> printfn "%A" x)

let rec factorial' n =
    if n = 0 
    then 1
    else n * factorial' (n - 1)

let a''' = factorial' 5

let rec factorial'K n k =
    if n = 0
    then k 1
    else factorial'K (n - 1) (fun x -> k n * x)

factorial'K 33 (fun x -> printfn "%A" x; x)

