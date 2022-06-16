module Printer

open Types

let printAtom =
    function
    | I i -> string i
    | F f -> string f
    | B b -> string b
    | Symbol str -> str
    // TODO: Escape special characters
    | String str -> "\"" + str + "\""
    | Nil -> "nil"

// TODO: Rewrite this to be tail recursive
let rec print (form: Form) : string =
    match form with
    | Quote form' -> "'" + print form
    | List forms ->
        "("
        + (forms |> List.map print |> String.concat " ")
        + ")"
    | Atom (atom) -> printAtom atom
