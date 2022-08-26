# TODO

- Use a better testing library; xUnit is not very ergonomic
- Get debugging working in VSCode
- def (have to return a new environment)
- fn
- if
- Macros
  - Macro expansion needs to be performed before `eval`
- Variadic function arguments
  - Add "rest" argument
- Throw on mismatch between provided args and args list
- Closures
  - To get closures working I'll have to change how environment handling works.
    If the closure captures the entire environment it effectively becomes
    immutable; this means we can't update any definitions. So, in order to be
    able to update definitions we we have to split it up into a lexical and
    global environment. (Which might not be too terrible--split it up into a
    record)

# DONE

- Unwrap Symbol - using "string" as environment keys is fine
- Get the CLI test runner working (I just needed to run it in the right project 😛)
- Print values
