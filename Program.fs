// For more information see https://aka.ms/fsharp-console-apps
// printfn "Hello from F#"
open System


let rec readlines () =
    seq {
        Console.Write "=> "
        let line = Console.ReadLine()

        if line <> null then
            yield line
            yield! readlines ()
    }

[<EntryPoint>]
let main args =
    let folder =
        (fun acc x ->
            match Repl.repl x acc with
            | Ok (value, env') ->
                Console.WriteLine value
                env'
            | Error e ->
                Console.WriteLine e
                acc)

    let lines = readlines ()
    let state = Seq.fold folder Repl.initEnvironment lines
    0
