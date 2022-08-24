module Types

type Atom =
    | B of bool
    | Number of float
    | Symbol of string
    | String of string
    | Nil

type Form =
    | Atom of Atom
    | Quote of Form
    | List of Form list
