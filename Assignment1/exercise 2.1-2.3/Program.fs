module Program

open Intcomp1

[<EntryPoint>]
let main argv =
    printfn "eval e1 = %d" (eval e1 [])
    0