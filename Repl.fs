module Repl

open Types

let initEnvironment: Environment = Map [ ("+", Builtins.add) ]

let evalDef env forms =
    match forms with
    | [ Atom (Symbol name); form ] ->
        Eval.eval env form
        |> Result.map (fun v -> Map.add name v env)
    | _ -> Error "def must be followed by a name and an expression"

let repl program environment =
    let rec replAcc forms env value =
        match forms with
        | [] -> Ok(value, env)
        | List (Atom (Symbol "def") :: rest) :: forms' ->
            evalDef env rest
            |> Result.bind (fun env' -> replAcc forms' env' (Form(Atom Nil)))
        | form :: forms' ->
            Eval.eval env form
            |> Result.bind (replAcc forms' env)

    Reader.read program
    |> Result.bind (fun forms -> replAcc forms environment (Form(Atom Nil)))
    |> Result.map (fun (v, env) -> (Printer.print v), env)
