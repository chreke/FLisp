module Tests

open System
open Xunit

let unlift =
    function
    | Ok v -> v
    | Error e -> e

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
        Reader.read program
        |> Result.map (List.head >> Printer.printForm)

    Assert.Equal(program, unlift result)

[<Theory>]
[<InlineData("true", "true")>]
[<InlineData("'(1 2)", "(1 2)")>]
[<InlineData("'foo", "foo")>]
[<InlineData("(+ 1 2 3)", "6")>]
[<InlineData("(+ 1 (+ 2 3))", "6")>]
let ``evaluate expressions`` program expected =
    let eval = Eval.eval Eval.initEnvironment

    let actual =
        Reader.read program
        |> Result.bind (List.head >> eval)
        |> Result.map Printer.print

    Assert.Equal(unlift actual, expected)
