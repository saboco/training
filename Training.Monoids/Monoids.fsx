// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "Monoids.fs"
open Training.Monoids.WordCountTest


time wordCountViaAddText "reduce then count"
time wordCountViaMap "map then reduce"
time wordCountViaParallelAddCounts "parallel map then reduce"
