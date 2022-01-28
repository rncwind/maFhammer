open Models
open Dice

let rec diceTests() =
    let dieString = "2d6"
    let die = parseDie dieString
    let (Ok unwrapped) = die // This is equivilent to unwrap() in rust.
    printfn "%A" unwrapped
    let rollResult = rollDie unwrapped
    printfn "%A" rollResult
    ()

let parseLasgunTest() =
    Weapons.deserialiseWeapon "./Weapons/lasgun.json"

[<EntryPoint>]
let main argv =
    diceTests() |> ignore
    parseLasgunTest() |> ignore
    0