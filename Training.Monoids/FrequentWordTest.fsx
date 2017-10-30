#load "Monoids.fs"
open Training.Monoids.FrequentWordTest

document() 
|> List.reduce addText
|> mostFrequentWord
|> printfn "Using add first, the most frequent word is %s"

document() 
|> List.map mostFrequentWord
|> List.reduce (+)
|> printfn "Using map reduce, the most frequent word is %s"

