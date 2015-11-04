# *type* RouteMap

> [Documentation](?) / [API Reference](API.md) / **RouteMap type**

* *type* RouteMap<'Action>

    This is a record type which represents route maps holding a serializer, deserializer pair that can map an action of type `'Action` to an URL string and vice versa.

    * *static member* Create
        * Signature: `static member RouteMap.Create : ('Action -> string list) -> (string list -> 'Action) -> RouteMap<'Action>`

        Creates a `RouteMap` instance from the given mappings.

## Example

```fsharp
type Action =
    | Home
    | Hello of name: string

// This will map action Home to /
// or Hello "John" to /hello/John and vice versa
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
