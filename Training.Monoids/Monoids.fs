// https://fsharpforfunandprofit.com/posts/monoids-part2/

namespace Training.Monoids
    
    module Products = 

        type ProductLine = {
            ProductCode: string
            Qty: int
            Price: float
            LineTotal: float
            }

        type TotalLine = {
            Qty: int
            OrderTotal: float }

        type OrderLine = 
            | Product of ProductLine
            | Total of TotalLine
            | EmptyOrder

        let addLine orderLine1 orderLine2 =
            match orderLine1,orderLine2 with
            // is one of them zero? If so, return the other one
            | EmptyOrder, _ -> orderLine2
            | _, EmptyOrder -> orderLine1
            // otherwise as before
            | Product p1, Product p2 ->
                Total { Qty = p1.Qty + p2.Qty;
                OrderTotal = p1.LineTotal + p2.LineTotal}
            | Product p, Total t ->
                Total {Qty = p.Qty + t.Qty;
                OrderTotal = p.LineTotal + t.OrderTotal}
            | Total t, Product p ->
                Total {Qty = p.Qty + t.Qty;
                OrderTotal = p.LineTotal + t.OrderTotal}
            | Total t1, Total t2 ->
                Total {Qty = t1.Qty + t2.Qty;
                OrderTotal = t1.OrderTotal + t2.OrderTotal}

        let printLine =  function
            | Product {ProductCode=p; Qty=q; Price=pr; LineTotal=t} -> 
                printfn "%-10s %5i @%4g each %6g" p q pr t 
            | Total {Qty=q; OrderTotal=t} -> 
                printfn "%-10s %5i %6g" "TOTAL" q t 
            | EmptyOrder -> printfn "Empty Order"

    module Customers = 
        open System
        
        type Customer = {
            Name:string // and many more string fields!
            LastActive:DateTime 
            TotalSpend:float }

        type CustomerStats = {
            // number of customers contributing to these stats
            Count:int 
            // total number of days since last activity
            TotalInactiveDays:int 
            // total amount of money spent
            TotalSpend:float }
            
        let customerZeroStats = { Count=0; TotalInactiveDays=0; TotalSpend=0.0}

        let add stat1 stat2 = {
            Count = stat1.Count + stat2.Count;
            TotalInactiveDays = stat1.TotalInactiveDays + stat2.TotalInactiveDays
            TotalSpend = stat1.TotalSpend + stat2.TotalSpend }

        let (++) a b = add a b

    module WordCountTest = 
        open System

        type Text = Text of string
    
        let addText (Text s1) (Text s2) =
            Text (s1 + s2)
    
        let wordCount (Text s) =
            System.Text.RegularExpressions.Regex.Matches(s,@"\S+").Count
            
        let page() = 
            List.replicate 1000 "hello "
            |> List.reduce (+)
            |> Text

        let document() = 
            page() |> List.replicate 1000 


        let time f msg = 
            let stopwatch = Diagnostics.Stopwatch()
            stopwatch.Start()
            f() 
            stopwatch.Stop()
            printfn "Time taken for %s was %ims" msg stopwatch.ElapsedMilliseconds


        let wordCountViaAddText() = 
            document() 
            |> List.reduce addText
            |> wordCount
            |> printfn "The word count is %i"

            
        let wordCountViaParallelAddCounts() = 
            document() 
            |> List.toArray
            |> Array.Parallel.map wordCount
            |> Array.reduce (+)
            |> printfn "The word count is %i"

        
        let wordCountViaMap() = 
            document() 
            |> List.map wordCount
            |> List.reduce (+)
            |> printfn "The word count is %i"


    module FrequentWordTest = 
        
        open System.Text.RegularExpressions

        type Text = Text of string

        let addText (Text s1) (Text s2) =
            Text (s1 + s2)

        let mostFrequentWord (Text s) =
            Regex.Matches(s,@"\S+")
            |> Seq.cast<Match>
            |> Seq.map (fun m -> m.ToString())
            |> Seq.groupBy id
            |> Seq.map (fun (k,v) -> k,Seq.length v)
            |> Seq.sortBy (fun (_,v) -> -v)
            |> Seq.head
            |> fst

        let page1() = 
            List.replicate 1000 "hello world "
            |> List.reduce (+)
            |> Text

        let page2() = 
            List.replicate 1000 "goodbye world "
            |> List.reduce (+)
            |> Text

        let page3() = 
            List.replicate 1000 "foobar "
            |> List.reduce (+)
            |> Text

        let document() = 
            [page1(); page2(); page3()]
        