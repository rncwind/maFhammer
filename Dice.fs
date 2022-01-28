module Dice

open System
open System.Text.RegularExpressions

// A Die has a representation (eg, 1d6 is 1 d6, 8d20 is 8 d20s),
// a diceCount, and a value for each die.
type Die = {
    repr: string
    diceCount: int
    diceValue: int
}

let validateDieString (dieString: string) : bool =
    if Regex.IsMatch(dieString, "(\d)+d(\d)+") then
        true
    else
        false

let parseDie (dieString : string) =
    if validateDieString dieString then
        let dieParts = (dieString.Split "d") |> Array.filter(fun x -> x <> "d") |> Array.toList
        if List.length dieParts <> 2 then
            Error("Invalid Die String Provided. DieParts too long!")
        else
            let diceCount = dieParts.[0] |> int
            let diceValue = dieParts.[1] |> int
            let newDie = {repr = dieString; diceCount = diceCount; diceValue = diceValue}
            Ok(newDie)
    else
        Error("Invalid Die String provided")

let genRandom limit diceCount =
    let rnd = Random()
    let initial = Seq.initInfinite (fun _ -> rnd.Next(1,limit))
    initial |> Seq.take(diceCount) |> Seq.toList

let rollDie (die: Die) : int list =
    genRandom die.diceValue die.diceCount