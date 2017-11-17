namespace Training.Monoids.Tests


open NUnit.Framework
open Training.Monoids.WorkingWithNonMonoids    

module WorkingWithNonMonoidsTests =

    module CharToRemoveTests = 
        open CharsToRemove
        
        [<Test>]
        let ``removing abd from abcdef should be cef``() =
            let removalAction = (subtract "abd") 
            Assert.AreEqual(removalAction |> applyTo "abcdef",  "cef")
            
        [<Test>]
        let ``removing abc multiple times from abcdef should be def``() =
            let removalAction = (subtract "abc") ++ (subtract "abc") ++ (subtract "abc")   
            Assert.AreEqual(removalAction |> applyTo "abcdef", "def")
        
        [<Test>]
        let ``removing a the bc from abcdef should equals to removing ab then c``() =
            let removalAction = (subtract "a") ++ (subtract "bc")
            let removalAction' = (subtract "ab") ++ (subtract "c")
            let removalAction'' = (subtract "a") ++ (subtract "b") ++ (subtract "c")
            let value = "abcdef"
            Assert.AreEqual(removalAction |> applyTo value, removalAction' |> applyTo value)
            Assert.AreEqual(removalAction |> applyTo value, removalAction'' |> applyTo value)            
    
    module AverageTests =        
        open Average
         
        [<Test>]
        let ``adding average sould return an average and contains the sum of the totals as well as the sum of the coutns``() =
            let avg = addAvg (avg 4) (avg 5)       
            Assert.IsInstanceOf(typeof<Average.Avg>, avg)
            Assert.AreEqual(9, avg.total)
            Assert.AreEqual(2, avg.count)            
            
        [<Test>]
        let ``adding averages shold be associative`` () =
                let avg1 = ((avg 4) ++ (avg 5)) ++ (avg 9)
                let avg2 = (avg 4) ++ ((avg 5) ++ (avg 9))
                Assert.AreEqual(avg1, avg2)    
           
        [<Test>]
        let ``averaging int from 1 to 10 should be 5 dot 5`` () =
            let avg = 
                [1..10]
                |> List.map avg
                |> List.reduce addAvg
                |> calcAvg
            
            Assert.AreEqual(5.5, avg)
            
    
