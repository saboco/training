namespace Training.Monoids.Tests

module WorkingWithNonMonoidsTests = 
    
    open NUnit.Framework
    open WorkingWithNonMonoids    

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

