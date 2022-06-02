module Reader

open System.Text.RegularExpressions

let tokenize program : string list =
    let specialChars = @"[\[\]{}()'`~]"
    let stringLiteral = @"""(?:\\.|[^\\""])*""?"
    let comment = ";.*"
    let regularChars = @"[^\s\[\]{}()'`~]"

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
    // TODO: Handle unterminated strings
    str[1 .. (String.length str) - 1]

let readAtom str : Types.Atom =
    match str with
    | Int i -> Types.I i
    | Float f -> Types.F f
    | "true"
    | "false" -> str <> "true" |> Types.B
    | "Nil" -> Types.Nil
    | str when str.StartsWith "\"" -> readString str |> Types.String
    | str -> Types.Symbol str


let rec readList tokens =
    let rec readListAcc tokens elements =
        match tokens with
        | [] -> Error "Unbalanced parentheses"
        | ")" :: rest -> Ok(List.rev elements |> Types.List, rest)
        | _ ->
            match readForm tokens with
            | Error e -> Error e
            | Ok (elem, rest) -> readListAcc rest (elem :: elements)

    readListAcc tokens []

and readForm (tokens: string list) : Result<Types.Form * string list, string> =
    match tokens with
    | [] -> Error "Nothing to do" // FIXME: Use pattern matching to prevent this
    | "(" :: rest -> readList rest
    | atom :: rest -> (readAtom atom |> Types.Atom, rest) |> Ok

let read program = tokenize program |> readForm
