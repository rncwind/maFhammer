module Models

open Dice
open Weapons

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

// Encode the weapon information too.
// A Model is it's statline, and it's weapon
type Model = {
    crunch: StatLine
    weapon: Weapon
}

// A squad is comprised of multiple Models.
type Squad = Model list