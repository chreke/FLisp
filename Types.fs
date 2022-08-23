module Types

type Symbol = Symbol of string

type Number =
    | I of int
    | F of float

type Atom =
    | B of bool
    | Number of Number
    | Symbol of Symbol
    | String of string
    | Nil

type Form =
    | Atom of Atom
    | Quote of Form
    | List of Form list
