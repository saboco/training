// https://fsharpforfunandprofit.com/posts/monoids-part2/

#load "Monoids.fs"
open Training.Monoids.Products

let orderLine1 = Product {ProductCode="AAA"; Qty=2; Price=9.99; LineTotal=19.98}
let orderLine2 = Product {ProductCode="BBB"; Qty=1; Price=1.99; LineTotal=1.99}
let orderLine3 = addLine orderLine1 orderLine2 

orderLine1 |> printLine 
orderLine2 |> printLine 
orderLine3 |> printLine 

let zero = EmptyOrder

zero |> printLine

// test identity
let productLine = Product {ProductCode="AAA"; Qty=2; Price=9.99; LineTotal=19.98}
assert (productLine = addLine productLine zero)
assert (productLine = addLine zero productLine)

let totalLine = Total {Qty=2; OrderTotal=19.98}
assert (totalLine = addLine totalLine zero)
assert (totalLine = addLine zero totalLine)

