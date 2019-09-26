namespace Training.Monoids.Tests


open Xunit
open Training.Monoids.WorkingWithNonMonoids    

module WorkingWithNonMonoidsTests =

    module CharToRemoveTests = 
        open CharsToRemove
        
        [<Fact>]
        let ``removing abd from abcdef should be cef``() =
            let removalAction = (subtract "abd") 
            Assert.Equal(removalAction |> applyTo "abcdef",  "cef")
            
        [<Fact>]
        let ``removing abc multiple times from abcdef should be def``() =
            let removalAction = (subtract "abc") ++ (subtract "abc") ++ (subtract "abc")   
            Assert.Equal(removalAction |> applyTo "abcdef", "def")
        
        [<Fact>]
        let ``removing a the bc from abcdef should equals to removing ab then c``() =
            let removalAction = (subtract "a") ++ (subtract "bc")
            let removalAction' = (subtract "ab") ++ (subtract "c")
            let removalAction'' = (subtract "a") ++ (subtract "b") ++ (subtract "c")
            let value = "abcdef"
            Assert.Equal(removalAction |> applyTo value, removalAction' |> applyTo value)
            Assert.Equal(removalAction |> applyTo value, removalAction'' |> applyTo value)            
    
    module AverageTests =        
        open Average
         
        [<Fact>]
        let ``adding average sould return an average and contains the sum of the totals as well as the sum of the coutns``() =
            let avg = addAvg (avg 4) (avg 5)       
            Assert.IsType(typeof<Average.Avg>, avg)
            Assert.Equal(9, avg.total)
            Assert.Equal(2, avg.count)            
            
        [<Fact>]
        let ``adding averages shold be associative`` () =
                let avg1 = ((avg 4) ++ (avg 5)) ++ (avg 9)
                let avg2 = (avg 4) ++ ((avg 5) ++ (avg 9))
                Assert.Equal(avg1, avg2)    
           
        [<Fact>]
        let ``averaging int from 1 to 10 should be 5 dot 5`` () =
            let avg = 
                [1..10]
                |> List.map avg
                |> List.reduce addAvg
                |> calcAvg
            
            Assert.Equal(5.5, avg)
            
    
