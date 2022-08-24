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

// TODO: Write a tail-recursive list printer
let rec print =
    function
    | Form f -> printForm f
    | Function f -> "function"
    | Builtin f -> "builtin"
    | ValueList values ->
        "("
        + (values |> List.map print |> String.concat " ")
        + ")"

and printForm (form: Form) : string =
    match form with
    | Quote form' -> "'" + printForm form'
    | Atom (atom) -> printAtom atom
    | List forms ->
        "("
        + (forms |> List.map printForm |> String.concat " ")
        + ")"
