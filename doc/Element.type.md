# *type* Element

> [Documentation](?) / [API Reference](API.md) / **Element type**

* *type* Element

    This is a record type which represents static HTML tags.

    * *static member* Create
        * Signature: `static member Element.Create : string -> Component list -> Element`

        Creates a static HTML tag with the given selector and children.

    * *static member* Wrap
        * Signature: `static member Element.Wrap : Component list -> Element`

        Wraps the given child components into a `<div />`. Useful for grouping other `Component` instances.

## Selectors

Selectors are strings in the following form, `tagName.x.y...` where `x` and `y` are CSS class names. `tagName` can be omitted if there is at least one class name specified.

Selector            | Element
------------------- | -------
button              | `<button />`
button.primary.blue | `<button class="primary blue" />`
.container          | `<div class="container" />`

## Helper functions

* *function* Text
    * Signature: `val Text : string -> Element`

    Shorthand function for creating `<span />` elements, containing the given text.

* *function* (-<)
    * Signature: `val (-<) : Element -> (string * 'T) list -> Element`

    An operator for assigning un-typed properties to `Element` instances.

    ```fsharp
Element.create "button" [ Text "Button1" ] -< [
        "myProperty", myPropertyValue
]
    ```
