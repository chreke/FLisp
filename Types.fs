module Types

type Symbol = Symbol of string

type Atom =
    | B of bool
    | Number of float
    | Symbol of Symbol
    | String of string
    | Nil

type Form =
    | Atom of Atom
    | Quote of Form
    | List of Form list
