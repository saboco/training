// https://fsharpforfunandprofit.com/posts/monoids-part3/

module WorkingWithNonMonoids
// Associativity exemple 
/// store a list of chars to remove
type CharsToRemove = CharsToRemove of Set<char>

/// construct a new CharsToRemove
let subtract (s:string) = 
    s.ToCharArray() |> Set.ofArray |>  CharsToRemove 

/// apply a CharsToRemove to a string
let applyTo (s:string) (CharsToRemove chs) = 
    let isIncluded ch = Set.exists ((=) ch) chs |> not
    let chars = s.ToCharArray() |> Array.filter isIncluded
    System.String(chars)

// combine two CharsToRemove to get a new one
let (++) (CharsToRemove c1) (CharsToRemove c2) = 
    CharsToRemove (Set.union c1 c2) 
    