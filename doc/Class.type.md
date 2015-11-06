# *type* Class

> [Documentation][1] / [API Reference](API.md) / **Class type**

* *type* Class<'T, #Component>

    A record type which represents reactive components with a state, a renderer function and an event listener list. Should be constructed with [`React.Class`](React.module.md).

* *type* Class<'T>

    `Class<'T, #Component>` subset which gives access only to the state. An instance of this type is always made from `Class<_, _>` values being created and passed to the renderer function.

    * *member* State

        The current state of type `'T` of the class. This property has only a  getter.

    * *member* SetState

        * Signature: `member Class.SetState: 'T -> unit`

        Sets the current state.

[1]: https://bitbucket.org/IntelliFactory/websharper.react/overview
