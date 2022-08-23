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

let toInt =
    function
    | I i -> i
    | F f -> int f

let toFloat =
    function
    | I i -> float i
    | F f -> f

let highestType a b =
    match a with
    | F f -> f
    | I i -> i

let intFold f numbers =
    let ints = List.map toInt numbers
    I(List.reduce f ints)

let floatFold f numbers =
    let floats = List.map toFloat numbers
    F(List.reduce f floats)

let foldMathOperator f values =
    match foldResults toNumber values with
    | Ok numbers -> Ok numbers
    | Error e -> Error e

type NumberType =
    | Float = 1
    | Integer = 0

let numberToNumberType =
    function
    | F f -> NumberType.Float
    | I i -> NumberType.Integer

let foldMath f values =
    match foldResults toNumber values with
    | Ok numbers ->
        let number =
            match List.map numberToNumberType numbers |> List.max with
            | NumberType.Integer -> List.map toInt numbers |> List.reduce f |> I
            | NumberType.Float -> List.map toFloat numbers |> List.reduce f |> F

        Ok number
    | Error e -> Error e

let add numbers =
    match foldResults toNumber numbers with
    | Ok ns -> Ok ns
    | Error e -> Error e
