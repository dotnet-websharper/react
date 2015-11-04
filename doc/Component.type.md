# *abstract type* Component

> [Documentation](?) / [API Reference](API.md) / **Component type**

This is the base class for everything that can be mounted to the DOM.

* *abstract member* Map

    * Signature: `abstract member Map : unit -> ReactElement`

    Defines how the given `Component` instance is represented with [React](?), for example, `Element` instances just transpiled to `React.createElement` calls.
