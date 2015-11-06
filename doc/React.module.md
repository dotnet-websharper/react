# *module* React

> [Documentation][1] / [API Reference](API.md) / **React module**

* *function* Class
    * Signature: `val Class : 'T -> (Class<'T> -> #Component) -> Class<'T, #Component)`

    Creates a reactive component from an initial state of type `'T` and from a renderer function, which maps the current state to a representation of the class being created.

```fsharp
React.Class { Name = "John" }
<| (fun this ->
    Text ("Hello " + this.State.Name + "!")
)
```

* *function* Mount
    * Signature: `val Mount : Dom.Node -> Component -> ReactComponent`

    Mounts a `Component` instance into the given DOM node.

```fsharp
// a Class<_, _> instance
|> React.Mount Document.Body
```

[1]: https://bitbucket.org/IntelliFactory/websharper.react/overview
