# *module* Class

> [Documentation][1] / [API Reference](API.md) / **Class module**

* *function* WithContext
    * Signature: `val WithContext : Context -> Class<'T, #Component> -> Class<'T, #Component>`

    Sets the current context of the given class.

```fsharp
// a Class<_, _> instance which uses Material UI components for representation
|> Class.WithContext MaterialUI.Context
```

[1]: https://bitbucket.org/IntelliFactory/websharper.react/overview
