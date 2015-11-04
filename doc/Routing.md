# *type* Router

> [Documentation](?) / [API Reference](API.md) / **Router type**

There is a special function inside the [React module](), called `React.Router` which instantiates a special `Class<_, _>` type, meant for client-side routing. Basically this is a reactive component, where the state is identical value (preferably a discriminated union case), which represents a given page in the application.

Since this is `Class<_, _>`, the currently viewed page can be altered type-safely, by calling the `SetState` member. It also listens for URL changes, so redirections using [Location API]() works as well.

* *function* Router
    * Signature: `val Router : RouteMap<'Action> -> (Router<'Action> -> #Component) -> Class<'Action, #Component>`

    Initializes a router with a route map and with a renderer function, that decides what components to show for a particular action.

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
