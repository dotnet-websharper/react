namespace rec WebSharper.React

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
        R.CreateElement(name, New props, React.inlineArrayOfSeq children)

    let Text (s: string) =
        As<R.Component> s

    let Mount target ``component`` =
        ReactDOM.Render(``component``, target) |> ignore

    let Make<'T, 'Props, 'State when 'T :> ComponentClass<'Props, 'State>> (f: 'Props -> 'T) (props: 'Props) =
        R.CreateElement(As<string> f, props)

    let Fragment (children: seq<R.Component>) =
        R.CreateElement(R.Fragment, null, React.inlineArrayOfSeq children)

// Ideally we should be able to just write components with:
//
//      type MyComponent(props) =
//          inherit React.Component<Props, State>(props)
//          override this.Render() = ...
//
// But currently we need this intermediary hacky class because of these issues:
//  * core#1006: can't write an abstract class in WIG.
//  * core#1007: prototype chain not set correctly when inheriting from a stub class.
[<AbstractClass>]
type ComponentClass<'Props, 'State>(props: 'Props) =

    static do
        JS.Inline """
            for (var $p in React.Component.prototype) {
                WebSharper.React.ComponentClass.prototype[$p] = React.Component.prototype[$p];
            }
            """

    do JS.Inline("""React.Component.call(this, $1)""", props)

    [<Name "render">]
    abstract Render : unit -> R.Component

    [<Name "getDefaultProps">]
    abstract GetDefaultProps : unit -> 'Props
    default this.GetDefaultProps() = JS.Undefined

    [<Name "props"; Stub>]
    member this.Props = X<'Props>

    [<Name "state"; Stub>]
    member this.State : 'State = X<'State>

    [<Inline "$this.state = $state">]
    member this.SetInitialState(state: 'State) = X<unit>

    [<Name "setState"; Stub>]
    member this.SetState(s: 'State) = X<unit>

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
