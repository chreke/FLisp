module Reader

open System.Text.RegularExpressions

open Types

let tokenize program : string list =
    let specialChars = @"[\[\]{}()'`~]"
    let stringLiteral = @"""(?:\\.|[^\\""])*""?"
    let comment = ";.*"
    let regularChars = @"[^\s\[\]{}()'`~""]+"

    let regex =
        [ specialChars
          stringLiteral
          comment
          regularChars ]
        |> String.concat "|"

    Regex.Matches(program, regex)
    |> Seq.toList
    |> List.map (fun m -> m.Value)


let (|Int|_|) (str: string) =
    match System.Int32.TryParse str with
    | (true, int) -> Some int
    | _ -> None

let (|Float|_|) (str: string) =
    match System.Double.TryParse str with
    | (true, f) -> Some f
    | _ -> None


let readString (str: string) =
    match str[0], str[str.Length - 1] with
    | '"', '"' when str.Length > 1 -> Ok str[1 .. (String.length str) - 2]
    | _ -> Error $"Unterminated string literal {str}"

let readAtom (str: string) =
    match str with
    | str when str.StartsWith "\"" -> readString str |> Result.map String
    | _ ->
        let atom =
            match str with
            | Float f -> Number f
            | "true" -> B true
            | "false" -> B false
            | "nil" -> Nil
            | str -> Symbol str

        Ok atom


let rec readList tokens =
    let rec readListAcc tokens elements =
        match tokens with
        | [] -> Error "Unbalanced parentheses"
        | ")" :: rest -> Ok(List.rev elements |> List, rest)
        | _ ->
            readForm tokens
            |> Result.bind (fun (elem, rest) -> readListAcc rest (elem :: elements))

    readListAcc tokens []

and readForm (tokens: string list) : Result<Form * string list, string> =
    match tokens with
    | [] -> Error "Nothing to do"
    | ")" :: _ -> Error "Unbalanced parentheses"
    | "(" :: rest -> readList rest
    | "'" :: rest ->
        readForm rest
        |> Result.map (fun (form, rest') -> Quote form, rest')
    | atom :: rest ->
        readAtom atom
        |> Result.map (fun a -> Atom a, rest)

let read program =
    let rec readAcc tokens acc =
        match tokens with
        | [] -> Ok(List.rev acc)
        | _ ->
            match readForm tokens with
            | Ok (form, remainingTokens) -> readAcc remainingTokens (form :: acc)
            | Error e -> Error e

    readAcc (tokenize program) []
