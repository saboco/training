namespace Benchmark.Collections

module Performance = 
    let printHeader() =
        printfn "Label,ListSize,ReduceAndIterMs" 

    // time the reduce and iter steps for a given list size and print the results
    let time label reduce iter listSize = 
        System.GC.Collect() //clean up before starting
        let stopwatch = System.Diagnostics.Stopwatch()
        stopwatch.Start()
        reduce() |> iter
        stopwatch.Stop()
        printfn "%s,%iK,%i" label (listSize/1000) stopwatch.ElapsedMilliseconds 

    let testListPerformance listSize = 
        let lists = List.init listSize (fun i -> [i.ToString()])
        let reduce() = lists |> List.reduce (@) 
        let iter = List.iter ignore
        time "List.@" reduce iter listSize 

    let testSeqPerformance_Append listSize = 
        let seqs = List.init listSize (fun i -> seq {yield i.ToString()})
        let reduce() = seqs |> List.reduce Seq.append 
        let iter = Seq.iter ignore
        time "Seq.append" reduce iter listSize 

    let testSeqPerformance_Yield listSize = 
        let seqs = List.init listSize (fun i -> seq {yield i.ToString()})
        let reduce() = seqs |> List.reduce (fun x y -> seq {yield! x; yield! y})
        let iter = Seq.iter ignore
        time "seq(yield!)" reduce iter listSize 

    let testArrayPerformance listSize = 
        let arrays = List.init listSize (fun i -> [| i.ToString() |])
        let reduce() = arrays |> List.reduce Array.append 
        let iter = Array.iter ignore
        time "Array.append" reduce iter listSize 

    let testResizeArrayPerformance listSize  = 
        let resizeArrays = List.init listSize (fun i -> new ResizeArray<string>( [i.ToString()] ) ) 
        let append (x:ResizeArray<_>) y = x.AddRange(y); x
        let reduce() = resizeArrays |> List.reduce append 
        let iter = Seq.iter ignore
        time "ResizeArray.append" reduce iter listSize 
        