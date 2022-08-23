module Builtins

open Types

let toNumber (form: Form) =
    match form with
    | Atom (Number n) -> Ok(n)
    | n -> Error $"{n} is not a number"

let foldResults f values =
    let rec foldResultsAcc f values acc =
        match values with
        | [] -> Ok acc
        | x :: xs ->
            match f x with
            | Ok v -> foldResultsAcc f xs (v :: acc)
            | Error e -> Error e

    foldResultsAcc f values []

let foldNumbers f values =
    match foldResults toNumber values with
    | Ok numbers -> Ok(Number(List.reduce f numbers))
    | Error e -> Error e

let add = foldNumbers (+)
