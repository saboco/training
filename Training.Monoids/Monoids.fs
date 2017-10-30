namespace Training.Monoids

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