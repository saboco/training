namespace Algorithms.Tests

module ModularArithmeticTests =

    open System
    open Xunit
    open FsUnit.Xunit
    open Algorithms.``Euclid's algorithm for greates common divisor``

    [<Theory>]
    [<InlineData(1, 2)>]
    [<InlineData(2, 4)>]
    [<InlineData(3, 10)>]
    [<InlineData(1, Int32.MaxValue)>]
    [<InlineData(25, 7891)>]
    [<InlineData(48, Int32.MaxValue)>]
    let ``gcd should fail if a < b`` a b =
        (fun () -> gcd a b |> ignore)
        |> should throw typeof<ArgumentException>


    [<Theory>]
    [<InlineData(2, 1, 1)>]
    [<InlineData(4, 2, 2)>]
    [<InlineData(10, 3, 1)>]
    [<InlineData(0, Int32.MaxValue, Int32.MaxValue)>]
    [<InlineData(Int32.MinValue, 0, Int32.MinValue)>]
    [<InlineData(Int32.MaxValue, 78, 1)>]
    let ``gcd finds the correct greates common divisor`` a b expected =
        gcd a b
        |> should equal expected
        
