# MAYBE

- Pass around the environment explicitly instead of implicitly? E.g. `def`
  implicitly adds something to the top level, but what if this was made explicit?
- Make _everything_ `read`-able? E.g. function references and other things that
  the reader can't handle in Clojure
- Special forms are really _special_ in most Lisps; e.g. a `lambda` form _must_
  have a symbol list as its second argument. But this shouldn't be strictly
  necessary-what if, for example, you could use a list of symbols from the current
  environment? Same thing with the function body; what if you could provide a
  reference to a list of symbols? (I guess this would eliminate the need for
  macros as a separate construct?)
- Tail-call optimization
    - Possible to leverage F#'s built-in TCO?
    - Might not be worth it; maybe if it compiles to bytecode?
