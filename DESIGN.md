# DESIGN

## Mutability

How should mutability work? We want to be able to redine / update both function
definitions and data (otherwise it's not possible to update the running
program, which is one of the design goals). However, what mutations should be
allowed and how should they work in practice?

### A.) Mutations only allowed at top level

In this version mutations are only allowed at the top level. 

#### Pros

- Should be relatively easy to implement. Since environments are immutable maps
  they're a bit annoying to update, but if we only have to handle mutations at
  the top level we can treat that as a special case.
- Makes the code easier to reason about; if mutations can only happen at the
  top level you can be sure that the rest of the code is pure.
- Makes it easier to identify pure commands, which is useful for
    * The developer; looking at the first symbol should tell 
- Transactions "for free"; since a single command can't update state in
  multiple places either the entire update will succeed or none of it will

#### Cons

- Might be hard to work with?

#### Implementation

Model the main process as a reduction over the environment + incoming commands
(like a `gen_server`)

## Concurrency

- A.) No concurrency
- B.) Run pure commands in parallell

# References

- Scheme special forms:
  https://groups.csail.mit.edu/mac/ftpdir/scheme-7.4/doc-html/scheme_3.html
