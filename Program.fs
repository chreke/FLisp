// For more information see https://aka.ms/fsharp-console-apps
// printfn "Hello from F#"



[<EntryPoint>]
let main args =
    for arg in args do
        printfn "%s" arg

    0
