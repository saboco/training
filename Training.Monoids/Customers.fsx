// https://fsharpforfunandprofit.com/posts/monoids-part2/

#load "Monoids.fs"
open Training.Monoids.Customers
open System

let toStats cust =
    let inactiveDays= DateTime.Now.Subtract(cust.LastActive).Days;
    {Count=1; TotalInactiveDays=inactiveDays; TotalSpend=cust.TotalSpend}

// create a list of customers
let c1 = {Name="Alice"; LastActive=DateTime(2005,1,1); TotalSpend=100.0}
let c2 = {Name="Bob"; LastActive=DateTime(2010,2,2); TotalSpend=45.0}
let c3 = {Name="Charlie"; LastActive=DateTime(2011,3,3); TotalSpend=42.0}
let customers = [c1;c2;c3]

// aggregate the stats

let customerToStatsAndPrint customers =
    customers 
    |> List.map toStats
    |> List.reduce add
    |> printfn "result = %A"

customerToStatsAndPrint customers

customerToStatsAndPrint [] // bug

let customerToStatsAndPrintWithFold customers =
    customers 
    |> List.map toStats
    |> List.fold add customerZeroStats
    |> printfn "result = %A"

customerToStatsAndPrintWithFold customers

customerToStatsAndPrintWithFold []
