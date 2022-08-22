module Types

type Symbol = Symbol of string

type Atom =
    | I of int
    | F of float
    | B of bool
    | Symbol of Symbol
    | String of string
    | Nil

type Form =
    | Atom of Atom
    | Quote of Form
    | List of Form list
