// https://fsharpforfunandprofit.com/posts/monoids-part2/

#load "Monoids.fs"
open Training.Monoids.WordCountTest

time wordCountViaAddText "reduce then count"
time wordCountViaMap "map then reduce"
time wordCountViaParallelAddCounts "parallel map then reduce"
