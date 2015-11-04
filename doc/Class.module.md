# *module* Class

> [Documentation](?) / [API Reference](API.md) / **Class module**

* *function* WithContext
    * Signature: `val WithContext : Context -> Class<'T, #Component> -> Class<'T, #Component>`

    Sets the current context of the given class. Usage:

    ```fsharp
// a Class instance that uses Material UI components
|> Class.WithContext MaterialUI.Context
    ```
