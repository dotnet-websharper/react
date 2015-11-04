# *module* React

> [Documentation](?) / [API Reference](API.md) / **React module**

* *function* Class
    * Signature: `val Class : 'T -> (Class<'T> -> #Component) -> Class<'T, #Component)`

    Creates a reactive component from an initial state of type `'T` and from a renderer function, which defines the representation of the class being created.

* *function* Mount
    * Signature: `val Mount : Dom.Node -> Component -> ReactComponent`

    Mounts a `Component` instance to the given DOM location.
