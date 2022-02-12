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
    printfn "Deserialising Lasgun"
    let w = Weapons.deserialiseWeapon "./Weapons/lasgun.json"
    ()

let parseGEQ() =
    printfn "Deserialising GEQ statline"
    let (geq, name) = Models.deserialiseStatline "./Statlines/GEQ.json"
    ()

let modelCreate() =
    printfn "Creating a GEQ with lasgun with createModel"
    let m = Models.createModel "GEQ" "lasgun"
    ()

let createSquad() =
    printfn "Creating Squad of GEQ with Lasgun x10"
    let m = Models.createModel "GEQ" "lasgun"
    let squad = Models.createSquad m 10
    ()

let fromDescriptor() =
    printfn "Loading GEQ with Lasgun from Descriptor"
    let m = Models.createFromDescriptor "./ModelDescriptors/GEQ_lasgun.json"
    ()

[<EntryPoint>]
let main argv =
    diceTests() |> ignore
    parseLasgunTest() |> ignore
    parseGEQ() |> ignore
    modelCreate() |> ignore
    createSquad() |> ignore
    fromDescriptor() |> ignore
    0