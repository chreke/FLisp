module Printer

open Types

let printBool =
    function
    | false -> "false"
    | true -> "true"

let printAtom =
    function
    | Number n -> string n
    | B b -> printBool b
    | Symbol str -> str
    | String str -> "\"" + str + "\""
    | Nil -> "nil"

// TODO: Rewrite this to be tail recursive
let rec print (form: Form) : string =
    match form with
    | Quote form' -> "'" + print form'
    | List forms ->
        "("
        + (forms |> List.map print |> String.concat " ")
        + ")"
    | Atom (atom) -> printAtom atom
