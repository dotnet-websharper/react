# *type* Class

> [Documentation](?) / [API Reference](API.md) / **Class type**

* *type* Class<'T, #Component>

    This is a record type which represents reactive components holding a state, a renderer and an event list. Should be constructed with [`React.Class`](React.module.md).

* *type* Class<'T>

    This is a subset of the type `Class<'T, #Component>`, giving access only to the state. An instance of this type is always made from `Class<_, _>` values being created and passed to renderer functions.

    * *member* State

        The current state of type `'T` of the class. This property has only a  getter.

    * *member* SetState

        * Signature: `member SetState: 'T -> unit`

        Sets the current state of the class.
