# *type* RouteMap

> [Documentation][1] / [API Reference](API.md) / **RouteMap type**

* *type* RouteMap<'Action>

    A record type which represents route maps with a serializer, deserializer pair that can map an action of type `'Action` to an URL string and vice versa.

    * *static member* Create
        * Signature: `static member RouteMap.Create : ('Action -> string list) -> (string list -> 'Action) -> RouteMap<'Action>`

        Creates a `RouteMap` instance from the given mappings.

## Example

```fsharp
type Action =
    | Home
    | Hello of name: string

// This will map action Home to /
// and Hello "John" to /hello/John and vice versa
let routeMap =
    RouteMap.Create
    <| function
        | Home -> []
        | Hello name -> [ "hello"; name ]
    <| function
        | [] -> Home
        | [ "hello"; name ] -> Hello name
        | _ -> Home
```

[1]: https://bitbucket.org/IntelliFactory/websharper.react/overview
