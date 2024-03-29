module Eval

open Types

let foldResult f values =
    let rec foldResultAcc f xs acc =
        match xs with
        | [] -> Ok(List.rev acc)
        | h :: t ->
            match f h with
            | Ok v -> foldResultAcc f t (v :: acc)
            | Error e -> Error e

    foldResultAcc f values []

let toSymbol (form: Form) =
    match form with
    | Atom (Symbol s) -> Ok(s)
    | f -> Error $"{f} is not a symbol"

let lambda (forms: Form list) =
    match forms with
    | [ List args; body ] ->
        foldResult toSymbol args
        |> Result.map ((fun x -> { Args = x; Body = body }) >> Function)
    | _ -> Error $"Invalid function expression: {forms}"

let lookup key env =
    match Map.tryFind key env with
    | Some v -> Ok v
    | None -> Error $"Undefined symbol: {key}"

let join m1 m2 = Map.foldBack Map.add m2 m1

let rec eval (env: Environment) (form: Form) : Result<Value, string> =
    match form with
    | Atom (Symbol (sym)) -> lookup sym env
    | Atom a -> Ok(Form(Atom a))
    | Quote form -> Ok(Form form)
    | List [] -> Ok(Form(List []))
    | List (Atom (Symbol "fn") :: rest) -> lambda rest
    | List funList -> evalFun env funList

and evalFun env funList =
    match funList |> foldResult (eval env) with
    | Ok (Function f :: args) -> evalBasicFun env f args
    | Ok (Builtin f :: args) -> f args
    | Ok (h :: _) -> Error $"{h} is not a function"
    | Ok ([]) -> Error $"Empty result (this should never happen)"
    | Error e -> Error e

and evalBasicFun env f inputs =
    let { Args = args; Body = body } = f
    let stackFrame = List.zip args inputs |> Map
    eval (join env stackFrame) body

let initEnvironment: Environment = Map [ ("+", Builtins.add) ]
