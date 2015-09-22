namespace WebSharper.React

open WebSharper

type RouteMap<'a> =
    {
        Serializer   : 'a -> string list
        Deserializer : string list -> 'a
    }

[<JavaScript>]
type RouteMap =
    static member Create serializer deserializer =
        {
            Serializer   = serializer
            Deserializer = deserializer
        }

type Router<'a> = Class<'a>
