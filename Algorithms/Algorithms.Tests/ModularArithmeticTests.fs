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
    let ``euclid should fail if a < b`` a b =
        (fun () -> euclid a b |> ignore)
        |> should throw typeof<ArgumentException>


    [<Theory>]
    [<InlineData(2, 1, 1)>]
    [<InlineData(4, 2, 2)>]
    [<InlineData(10, 3, 1)>]
    [<InlineData(0, Int32.MaxValue, Int32.MaxValue)>]
    [<InlineData(Int32.MinValue, 0, Int32.MinValue)>]
    [<InlineData(Int32.MaxValue, 78, 1)>]
    let ``euclid finds the correct greates common divisor`` a b expected =
        euclid a b
        |> should equal expected
        
    open System.Collections.Generic
    open System.Collections
            
    type Data() =

        member __.Collection with get() = 
            let arr= ResizeArray<obj[]>()
            arr.Add([|2; 1; (0,1,1)|])
            arr.Add([|13; 4; (1,-3,1)|])
            arr.Add([|25; 11; (4, -9, 1)|])
            arr :> IEnumerable<obj[]>
        
        interface IEnumerable<obj[]> with
            member this.GetEnumerator() =
              this.Collection.GetEnumerator()
        
        interface IEnumerable with
            member this.GetEnumerator() =
              upcast this.Collection.GetEnumerator()

    [<Theory>]
    [<ClassData(typeof<Data>)>]
    let ``euclid-extended finds the correct greates common divisor and x and y factors so d= ax + by`` a b expected =
        
        ``euclid-extended`` a b
        |> should equal expected
