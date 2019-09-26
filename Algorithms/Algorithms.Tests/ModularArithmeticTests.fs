namespace Algorithms.Tests

module ModularArithmeticTests =

    open System
    open Xunit
    open FsUnit.Xunit
    open Algorithms.``Euclid's algorithm for greates common divisor``

    [<Theory>]
    [<InlineData(1, 2)>]
    [<InlineData(4, 2)>]
    [<InlineData(10, 3)>]
    [<InlineData(0, Int32.MaxValue)>]
    [<InlineData(Int32.MinValue, 0)>]
    [<InlineData(Int32.MinValue, Int32.MaxValue)>]
    let ``gcd should fail if a < b`` a b =
        (fun () -> gcd a b |> ignore)
        |> should throw typeof<ArgumentException>


    [<Theory>]
    [<InlineData(1, 2, 0)>]
    [<InlineData(4, 2, 0)>]
    [<InlineData(10, 3, 1)>]
    [<InlineData(0, Int32.MaxValue)>]
    [<InlineData(Int32.MinValue, 0)>]
    [<InlineData(Int32.MinValue, Int32.MaxValue)>]
    let ``gcd founds the correct greates common divisor`` b a expected =
        gcd a b
        |> should equal expected
        
