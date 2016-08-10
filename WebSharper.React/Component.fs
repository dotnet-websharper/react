namespace WebSharper.React

open WebSharper
open WebSharper.React.Bindings

type Component =
    [<Name "Map">]
    abstract member Map : unit -> ReactElement
