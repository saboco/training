
#load "CollectionPerformance.fs"

open Benchmark.Collections
open Performance

printHeader() 

[2000..4000..50000]
|> List.iter testArrayPerformance 

[2000..4000..50000]
|> List.iter testResizeArrayPerformance 

[2000..4000..50000]
|> List.iter testListPerformance

[2000..4000..50000]
|> List.iter testSeqPerformance_Append 

[2000..4000..50000]
|> List.iter testSeqPerformance_Yield 