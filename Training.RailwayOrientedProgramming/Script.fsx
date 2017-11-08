// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "Training.RailwayOrientedProgramming.fs"
open Training.RailwayOrientedProgramming.Rop

type Request = {name:string; email:string}

let validate1 input =
   if input.name = "" then Failure "Name must not be blank"
   else Success input

let validate2 input =
   if input.name.Length > 50 then Failure "Name must not be longer than 50 chars"
   else Success input

let validate3 input =
   if input.email = "" then Failure "Email must not be blank"
   else Success input

let log twoTrackInput = 
    let success x = printfn "DEBUG. Success so far: %A" x; x
    let failure x = printfn "ERROR. %A" x; x
    doubleMap success failure twoTrackInput 

let (&&&) v1 v2 = 
        let addSuccess r1 _r2 = r1 // return first
        let addFailure s1 s2 = s1 + "; " + s2  // concat
        plus addSuccess addFailure v1 v2 

let combinedValidation = 
    validate1 
    &&& validate2 
    &&& validate3 

let canonicalizeEmail input =
   { input with email = input.email.Trim().ToLower() }

let updateDatabase input =
   input |> ignore

let exHandler (input:exn) =
    input.Message
let usecase = 
    combinedValidation
    >=> switch canonicalizeEmail
    >=> tryCatch (tee updateDatabase) exHandler

let goodInput = {name="Alice"; email="good"}
let badInput = {name=""; email=""}

usecase badInput 
|> printfn "Bad Result = %A"

usecase goodInput 
|> printfn "Good Result = %A"
