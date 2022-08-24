module Types

type Atom =
    | B of bool
    | Number of float
    | Symbol of string
    | String of string
    | Nil

type SpecialForm =
    | Def
    | Fun
    | Macro
    | If

type Form =
    | Atom of Atom
    | Quote of Form
    | List of Form list

type Function = { Args: string list; Body: Form }

// NB: Some values can only occur *after* evaluation, henche the need for
// different union types for Forms and Values. (However, it would be interesting
// to try to bridge this gap; e.g. allow for typing in function references and
// special forms)
type Value =
    | Form of Form
    | Function of Function
    | Builtin of (Value list -> Result<Value, string>)
    | ValueList of Value list

type Environment = Map<string, Value>
