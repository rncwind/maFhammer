module Weapons

open Dice

open FSharp.Data
open FSharp.Data.JsonExtensions

type Weapon = {
    wName: string
    wType: WeaponType
    S: int
    AP: int
    D: DamageType
} and DamageType =
    | Literal of int
    | Dice of Die
and WeaponType = {
    dt: DamageType
    category: string
}

let extractDamageType dt =
    let (dt: int, d: string) = dt
    match dt with
    | 1 -> Ok(DamageType.Literal( (d |> int) ))
    | 2 ->
        let die = Dice.parseDie(d)
        match die with
        | Ok validDie -> Ok(DamageType.Dice(validDie))
        | Error e ->
            printfn "%s" e
            Error("Invalid Dice type Weapon found.")
    | _ ->
        let emsg = sprintf "A weapon with the damage type %s was found" d
        Error(emsg)

let handleComplexTypes parsed =
    // Get the Weapon Type
    let wt = (parsed?wType?DamageType.AsInteger(), parsed?wType?Dice.AsString())
    let wtcat = parsed?wType?Category.AsString()
    let realWtDt = extractDamageType wt
    let realWtDt = 
        match realWtDt with
        | Ok valid -> valid
        | Error e -> failwith e
    // This is our reified WeaponType
    let realWt = {dt = realWtDt; category =wtcat}

    // Extract the damage type
    let dt = (parsed?D?DamageType.AsInteger(), parsed?D?Damage.AsString())
    let realDt = extractDamageType dt
    let realDt =
        match realDt with
        | Ok valid -> valid
        | Error e -> failwith e
    
    (realWt, realDt)

let deserialiseWeapon jsonpath =
    let rawjson = System.IO.File.ReadAllText jsonpath
    let parsed = JsonValue.Parse(rawjson)
    let (wt, dt) = handleComplexTypes parsed
    let name = parsed?name.AsString()
    let S = parsed?S.AsInteger()
    let AP = parsed?AP.AsInteger()
    {wName = name; wType = wt; S = S; AP = AP; D = dt;}