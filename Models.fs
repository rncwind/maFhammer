module Models

open Dice
open Weapons

open FSharp.Data
open FSharp.Data.JsonExtensions

// Encode the crunch-line as a tuple.
// We don't care about base size or movement range.
type StatLine = {
    WS: int
    BS: int
    S: int
    T: int
    W: int
    A: int
    Ld: int
    Sv: int
}

let deserialiseStatline jsonpath =
    let parsed = System.IO.File.ReadAllText jsonpath |> JsonValue.Parse
    let sl = {WS = parsed?WS.AsInteger(); BS = parsed?BS.AsInteger(); S = parsed?S.AsInteger();
              T = parsed?T.AsInteger(); W = parsed?W.AsInteger(); A = parsed?A.AsInteger();
              Ld = parsed?Ld.AsInteger(); Sv = parsed?Sv.AsInteger()}
    (sl, parsed?name.AsString())

// Encode the weapon information too.
// A Model is it's statline, and it's weapon
type Model = {
    crunch: StatLine
    weapon: Weapon
    modelName: string
}

let createModel modelName weaponName =
    let (statline, name) = deserialiseStatline("Statlines/" + modelName + ".json")
    let weapon = deserialiseWeapon("Weapons/" + weaponName + ".json")
    {crunch = statline; weapon = weapon; modelName = name}

let createFromDescriptor descriptorPath =
    let parsed = System.IO.File.ReadAllText descriptorPath |> JsonValue.Parse
    let statline = "Statlines/" + parsed?statline.AsString()
    let weaponPath = "Weapons/" + parsed?weapon.AsString()
    let (sl, name) = deserialiseStatline(statline)
    let w = deserialiseWeapon(weaponPath)
    {crunch = sl; weapon = w; modelName = name}

// A squad is comprised of multiple Models.
type Squad = Model list

let createSquad model squadSize =
    let l: Squad = List.replicate squadSize model
    l