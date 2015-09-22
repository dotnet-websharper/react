namespace WebSharper.React

open WebSharper
open WebSharper.React.Bindings

type Component =
    abstract member Map : unit -> ReactElement
