module Tests

open System
open Xunit

let unlift =
    function
    | Ok v -> v
    | Error e -> e

let isOk =
    function
    | Ok _ -> true
    | Error _ -> false

[<Fact>]
let ``My test`` () = Assert.True(true)

[<Theory>]
[<InlineData("true")>]
[<InlineData("foo")>]
[<InlineData("1")>]
[<InlineData("\"foo\"")>]
[<InlineData("(+ 1 2 3)")>]
[<InlineData("'(+ 1 2 3)")>]
// [<InlineData("\"foo\\nbar\"")>]
// [<InlineData("\"foo\\\\\"")>]
// [<InlineData("\"foo\\\"\"")>]
let ``read program and then print it back`` program =
    let result =
        Reader.read program
        |> Result.map (List.head >> Printer.printForm)

    Assert.Equal(program, unlift result)

[<Theory>]
[<InlineData("\"foo")>]
[<InlineData("foo\"")>]
[<InlineData("(+ 1 2")>]
[<InlineData("+ 1 2)")>]
let ``read an invalid program and return an Error result`` program =
    let result = Reader.read program
    Assert.False(isOk result)

[<Theory>]
[<InlineData("\"foo\"", "\"foo\"")>]
[<InlineData("true", "true")>]
[<InlineData("'(1 2)", "(1 2)")>]
[<InlineData("'foo", "foo")>]
[<InlineData("(+ 1 2 3)", "6")>]
[<InlineData("(+ 1 (+ 2 3))", "6")>]
[<InlineData("((fn (x) (+ 1 2 x)) 3)", "6")>]
[<InlineData("(def x 6) x", "6")>]
let ``evaluate expressions`` program expected =
    let result = Repl.repl program
    Assert.Equal(expected, Repl.repl program |> Result.map (v, _) -> v |> unlift)
