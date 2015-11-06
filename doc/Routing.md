# Routing

> [Documentation][1] / [API Reference](API.md) / **Router type**

Structuring Single Page Application with **WebSharper.React** is fairly easy, because it supports client-side routing out of the box.

`React.Router` function inside the [React module](React.module.md) instantiates a special reactive component, where the state is an identical value (preferably a discriminated union case), which represents a page in your application.

Since this is `Class<_, _>` instance, it can be mounted into the DOM and the currently viewed page can be altered type-safely, by calling the `SetState` member. It also listens for URL changes, so redirections using [Location API][2] works as well.

* *function* Router
    * Signature: `val Router : RouteMap<'Action> -> (Router<'Action> -> #Component) -> Class<'Action, #Component>`

    Initializes a router with a route map and with a renderer function, that decides what group of components (page) show for a particular action.

## Example

```fsharp
type Action =
    | Home
    | Hello of name: string

let routeMap =
    RouteMap.Create
    <| function
        | Home -> []
        | Hello name -> [ "hello"; name ]
    <| function
        | [] -> Home
        | [ "hello"; name ] -> Hello name
        | _ -> Home

let router =
    React.Router routeMap
    <| fun router ->
        match router.State with
        | Home ->
            Element.Create "button" [ Text "Say Hello to John"]
            |> OnClick (fun _ ->
                router.SetState (Hello "John")
            )
        | Hello name ->
            Text ("Hello " + name + "!")
```

[1]: https://bitbucket.org/IntelliFactory/websharper.react/overview
[2]: https://developer.mozilla.org/en-US/docs/Web/API/Location
