module Builtins

open Types

let toNumber (form: Value) =
    match form with
    | Form (Atom (Number n)) -> Ok(n)
    | n -> Error $"{n} is not a number"

let foldResults f values =
    let rec foldResultsAcc f values acc =
        match values with
        | [] -> Ok(List.rev acc)
        | x :: xs ->
            match f x with
            | Ok v -> foldResultsAcc f xs (v :: acc)
            | Error e -> Error e

    foldResultsAcc f values []

let foldNumbers f values =
    foldResults toNumber values
    |> Result.map ((List.reduce f) >> Number >> Atom >> Form)

let add = Builtin(foldNumbers (+))
