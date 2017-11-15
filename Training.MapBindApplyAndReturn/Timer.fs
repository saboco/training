// https://fsharpforfunandprofit.com/posts/elevated-world/

module Timer

/// Do countN repetitions of the function f and print the time per run
let time countN label f  = 

    let stopwatch = System.Diagnostics.Stopwatch()
    
    // do a full GC at the start but not thereafter
    // allow garbage to collect for each iteration
    System.GC.Collect()  

    printfn "======================="         
    printfn "%s" label 
    printfn "======================="         
    
    let mutable totalMs = 0L

    for iteration in [1..countN] do
        stopwatch.Restart() 
        f()
        stopwatch.Stop() 
        printfn "#%2i elapsed:%6ims " iteration stopwatch.ElapsedMilliseconds 
        totalMs <- totalMs + stopwatch.ElapsedMilliseconds

    let avgTimePerRun = totalMs / int64 countN
    printfn "%s: Average time per run:%6ims " label avgTimePerRun 