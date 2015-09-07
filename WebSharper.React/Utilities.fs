namespace WebSharper.React.Obsolete

open WebSharper

[<AutoOpen>]
[<JavaScript>]
module Utilities =

    let inline default' option' value =
        match option' with
        | Some value ->
            value
        | _ ->
            value
