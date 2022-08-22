module Tests

open System
open Xunit

[<Fact>]
let ``My test`` () = Assert.True(true)


[<Theory>]
[<InlineDataAttribute("true")>]
[<InlineDataAttribute("foo")>]
[<InlineDataAttribute("1")>]
[<InlineDataAttribute("(+ 1 2 3)")>]
[<InlineDataAttribute("'(+ 1 2 3)")>]
let ``read program and then print it back`` program = Reader.read program
