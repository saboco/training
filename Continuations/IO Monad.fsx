type Cont<'t> =
    | Finish of 't
    | Cont of Action<Cont<'t>>
and Action<'next> =
    | GetLine of (string -> 'next)
    | GetTime of  (System.DateTime -> 'next)
    | Print of string * (unit -> 'next)

let mapAction f = function
    | GetLine c -> GetLine(c >> f)
    | GetTime c -> GetTime(c >> f)
    | Print(s, c) -> Print(s, c >> f)

let rec map (f: 'a -> 'b) (x: Cont<'a>) : Cont<'b>  = 
     match x with
     | Finish v -> Finish (f v)
     | Cont a -> Cont(mapAction (map f) a)

let ret x = Finish x

let rec bind f x =
    match x with
    | Finish v -> f v
    | Cont a -> Cont(mapAction (bind f) a)

let (>>=) x f = bind f x
let (>=>) f g = fun x -> f x >>= g

type ContBuilder() =
    member __.Bind(x,f) = bind f x
    member __.Return x = ret x
    member __.ReturnFrom x = x
    member __.Zero() = ret ()

let cont = ContBuilder()


let getTime() = GetTime ret |> Cont
let getLine() = GetLine ret |> Cont
let print s = Print(s, ret) |> Cont

open Printf

let printf fmt =
    kprintf print fmt

let prog =
    cont {
        let! time = getTime()
        do! printf "CurrentTime: %O" time

        let! line = getLine()
        do! printf "You typed: %s" line
    }

let rec run prog =
    match prog with
    | Finish _ -> ()
    | Cont (GetTime f) -> f System.DateTime.UtcNow |> run
    | Cont (GetLine f) -> f (System.Console.ReadLine()) |> run
    | Cont (Print(s, f)) -> printfn "[print] %s" s; f () |> run

type Action =
    | Time of System.DateTime
    | Line of string
    

let rec test prog actions output =
    match prog, actions with
    | Finish _, _ -> output |> List.rev |> Ok
    | Cont (GetTime f), Time t :: tail -> test (f t) tail output
    | Cont (GetLine f), Line l :: tail -> test (f l) tail output
    | Cont (Print(s,f)), a -> test (f ()) a (s :: output)
    | _ -> Error "Invalid test"

test prog [Time (System.DateTime(2018,01,01)); Line "Hello santiago" ] []
test prog [Time (System.DateTime(2018,01,01)); Line "Hello jeremie" ] []


run prog
