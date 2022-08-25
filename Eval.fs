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

let specialForms = Map [ ("fn", (fun _ forms -> lambda forms)) ]

let toResult error opt =
    match opt with
    | Some a -> Ok a
    | None -> Error error

let join m1 m2 = Map.foldBack Map.add m2 m1

let (|SpecialForm|_|) (form) =
    match form with
    | Atom (Symbol sym) -> Map.tryFind sym specialForms
    | _ -> None

let rec eval (env: Environment) (form: Form) : Result<Value, string> =
    match form with
    | Atom (Symbol (sym)) ->
        env.TryFind sym
        |> toResult $"{sym} is not defined"
    | Atom a -> Ok(Form(Atom a))
    | Quote form -> Ok(Form form)
    | List [] -> Ok(Form(List []))
    | List (SpecialForm f :: rest) -> f env rest
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
