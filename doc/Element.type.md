# *type* Element

> [Documentation][1] / [API Reference](API.md) / **Element type**

* *type* Element

    A record type which represents static HTML tags.

    * *static member* Create
        * Signature: `static member Element.Create : string -> Component list -> Element`

        Creates an element with the given selector and children.

    * *static member* Wrap
        * Signature: `static member Element.Wrap : Component list -> Element`

        Wraps the given child components. Useful for grouping `Component` instances.

## Selectors

Selectors are strings in `tagName.x.y...` form where `x` and `y` are CSS class names.

> Note: `tagName` can be omitted if there is at least one class name specified.

### Examples

Selector            | Element
------------------- | -------
button              | `<button />`
button.primary.blue | `<button class="primary blue" />`
.container          | `<div class="container" />`

## Shorthand functions

* *function* Text
    * Signature: `val Text : string -> Element`

    Creates a `<span />` element, containing the given text.

* *function* (-<)
    * Signature: `val (-<) : Element -> (string * 'T) list -> Element`

    Assigns un-typed properties to `Element` instances.

### Example

```fsharp
Element.create "button" [ Text "Button1" ] -< [
        "myProperty", myPropertyValue
]
```

[1]: https://bitbucket.org/IntelliFactory/websharper.react/overview
