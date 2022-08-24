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

type Value =
    | Form of Form
    | Function of Function
    | Builtin of (Value list -> Result<Value, string>)

type Environment = Map<string, Value>
