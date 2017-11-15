// https://fsharpforfunandprofit.com/posts/elevated-world/
module Result

type Result<'T> =
    | Success of 'T
    | Failure of string list
let map f xResult = 
    match xResult with
    | Success x ->
        Success (f x)
    | Failure errs ->
        Failure errs
// Signature: ('a -> 'b) -> Result<'a> -> Result<'b>

// "return" is a keyword in F#, so abbreviate it
let retn x = 
    Success x
// Signature: 'a -> Result<'a>

let apply fResult xResult = 
    match fResult,xResult with
    | Success f, Success x ->
        Success (f x)
    | Failure errs, Success x ->
        Failure errs
    | Success f, Failure errs ->
        Failure errs
    | Failure errs1, Failure errs2 ->
        // concat both lists of errors
        Failure (List.concat [errs1; errs2])
// Signature: Result<('a -> 'b)> -> Result<'a> -> Result<'b>

let bind f xResult = 
    match xResult with
    | Success x ->
        f x
    | Failure errs ->
        Failure errs
// Signature: ('a -> Result<'b>) -> Result<'a> -> Result<'b>
    
//type ResultBuilder() =
//        member this.Return x = retn x
//        member this.Bind(x,f) = bind f x

//let result = new ResultBuilder()