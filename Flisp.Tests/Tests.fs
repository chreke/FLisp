module Tests

open System
open Xunit

[<Fact>]
let ``My test`` () = Assert.True(true)


[<Theory>]
[<InlineData("true")>]
[<InlineData("foo")>]
[<InlineData("1")>]
[<InlineData("(+ 1 2 3)")>]
[<InlineData("'(+ 1 2 3)")>]
let ``read program and then print it back`` program =
    let result =
        match Reader.read program with
        | Ok tokens -> List.head tokens |> Printer.print
        | Error e -> e

    Assert.Equal(result, program)
