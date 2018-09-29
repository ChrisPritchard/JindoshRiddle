open Solver

[<EntryPoint>]
let main _ =
    let timer = System.Diagnostics.Stopwatch.StartNew ()

    solve ()
    |> Seq.iter (fun p ->
        printfn "%A owns the %A" p.woman p.owns)

    timer.Stop ()
    printfn ""
    printfn "Time taken: %f seconds" <| float timer.ElapsedMilliseconds / 1000.

    0