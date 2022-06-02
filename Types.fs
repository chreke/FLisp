module Types

type Atom =
    | I of int
    | F of float
    | B of bool
    | Symbol of string
    | String of string
    | Nil

type Form =
    | Atom of Atom
    | Quote of Form
    | List of Form list
