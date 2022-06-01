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


let readList tokens =
    let rec readListAcc tokens elements =
        match tokens with
        | [] -> Error "Unbalanced parentheses"
        | ")" :: rest -> Ok(List.rev elements, rest)
        | form :: rest -> readListAcc rest (form :: elements)

    readListAcc tokens []

let readForm tokens =
    match tokens with
    | [] -> Ok([], [])
    | "(" :: rest -> readList rest
    | _ -> Error "Syntax error"

let read program = tokenize program |> readForm
