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
- Tail-call optimization (possible to leverage F#'s built-in TCO?)

# MAYBE

- Pass around the environment explicitly instead of implicitly? E.g. `def`
  implicitly adds something to the top level, but what if this was made explicit?
- Make _everything_ `read`-able? E.g. function references and other things that
  the reader can't handle in Clojure

# DONE

- Unwrap Symbol - using "string" as environment keys is fine
- Get the CLI test runner working (I just needed to run it in the right project 😛)
- Print values
