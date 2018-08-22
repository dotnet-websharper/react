namespace WebSharper.React

open System.Runtime.CompilerServices
open WebSharper
open WebSharper.JavaScript
open WebSharper.React.Bindings
type private R = WebSharper.React.Bindings.React
type React = R

module React =

    let internal inlineArrayOfSeq (s: seq<'T>) : array<'T> =
        match s with
        | :? System.Array -> As s
        | s -> Array.ofSeq s

    let Element (name: string) (props: seq<string * obj>) (children: seq<R.Component>) =
        R.CreateElement(name, New props, inlineArrayOfSeq children)

    [<Inline>]
    let Text (s: string) =
        As<R.Component> s

    [<Inline>]
    let Mount target ``component`` =
        ReactDOM.Render(``component``, target) |> ignore

    [<Inline>]
    let Make<'T, 'Props, 'State when 'T :> R.Component<'Props, 'State>> (f: 'Props -> 'T) (props: 'Props) =
        R.CreateElement(As<R.Class> f, props)

    let Fragment (children: seq<R.Component>) =
        R.CreateElement(R.Fragment, null, inlineArrayOfSeq children)

[<AutoOpen>]
module Extensions =

    type R.Context<'T> with

        member this.Provide (value: 'T) (comp: seq<R.Component>) =
            R.CreateElement(this.Provider, New ["value" => value], React.inlineArrayOfSeq comp)

        member this.Consume (f: 'T -> #seq<R.Component>) =
            R.CreateElement(this.Consumer, null, fun v ->
                R.CreateElement(R.Fragment, null, React.inlineArrayOfSeq(f v)))

[<assembly: JavaScript>]
do ()
