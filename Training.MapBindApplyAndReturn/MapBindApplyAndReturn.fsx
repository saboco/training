// https://fsharpforfunandprofit.com/posts/elevated-world/

#load "MapBindApplyAndReturn.fs"
open Training.MapBindApplyAndReturn.MapBindApplyAndReturn
open System
open Training.MapBindApplyAndReturn
      
type ParseE = E<string> -> E<int>
type AddE1 = E<int> -> E<int>    

// map        
let parse s = Int32.Parse(s)
let add1 a = a + 1
let parseAndAdd1 s = s |> (parse >> add1)    
let parseE : ParseE = fun x -> mapE parse x
let addE1 : AddE1 = fun x -> mapE add1 x
let parseAndAdd1E s = s |> (parseE >> addE1)
let parseAndAdd1E' s = mapE parseAndAdd1 s

let a = parseE (Data "2")
let b = parseAndAdd1E (Data "4")
let c = parseAndAdd1E' (Data "3")

// return
let d = returnE "a"
let e = returnE 1

// apply
let add a b = a + b
let f = returnE add
let g = applyE2 f (Data 6) (Data 4)
let i = applyE f (Data 5)
let j = applyE i (Data 6)

let k = returnE add1
let l = returnE 5
let m = applyE k l

let n = returnE (add1 5)

let tuple x y = x,y

// create a generic combiner of options
// with the tuple constructor baked in
let combineOpt x y = Option.lift2 tuple x y 

// create a generic combiner of lists
// with the tuple constructor baked in
let combineList x y = List.lift2 tuple x y 

combineOpt (Some 1) (Some 2) 
combineList [1;2] [100;200]     

combineOpt (Some 2) (Some 3)        
|> Option.map (fun (x,y) -> x + y)
combineList [1;2] [100;200]         
|> List.map (fun (x,y) -> x + y)    


// bind
let add1EFromInt x = Data (x + 1)
let add2EFromInt x = Data (x + 2)

let add1e2FromInt x = (add1EFromInt >> bindE add2EFromInt) x
let add1e2FromInt' x = add1EFromInt x|> bindE add2EFromInt

let o = add1e2FromInt 1
let p = add1e2FromInt' 1

let parseInt str = 
    match str with
    | "-1" -> Some -1
    | "0" -> Some 0
    | "1" -> Some 1
    | "2" -> Some 2
    // etc
    | _ -> None

type OrderQty = OrderQty of int

let toOrderQty qty = 
    if qty >= 1 then 
        Some (OrderQty qty)
    else
        // only positive numbers allowed
        None


let parseOrderQty str =
    parseInt str
    |> Option.bind toOrderQty

let q = parseOrderQty "1"
let r = parseOrderQty ""

// first law (identity)
let s = (Data 5)
(id s) = s

// second monad law    

let parserEfromStr s =
    parseE (Data s)

let fEbounded aE = bindE parserEfromStr aE
let aE = returnE "-3"
let applyedE = fEbounded aE 
let applyedF = parserEfromStr "-3"
applyedE = applyedF

let v = 1;
let double x = x * 2;
let doubleE x = Data (double x)
let add1E x = Data (add1 x)
let groupLeft x = (x |> add1 ) |> double
let groupRight x = x |> (add1 >> double)

(groupLeft v) = (groupRight v)

let groupLeftE x = (x >>= add1E) >>= doubleE
let groupRightE x = x >>= (fun a -> add1E a >>= doubleE)

let vE = returnE 5
(groupLeftE vE) = (groupRightE vE)


let tuples = [Some (1,2); Some (3,4); None; Some (7,8);]
