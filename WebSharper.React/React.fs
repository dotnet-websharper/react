namespace rec WebSharper.React

open WebSharper
open WebSharper.JavaScript
open WebSharper.React.Bindings
type private R = WebSharper.React.Bindings.React

type React private () =

    static let inlineArrayOfSeq (s: seq<'T>) : array<'T> =
        match s with
        | :? System.Array -> As s
        | s -> Array.ofSeq s

    static member Element (name: string) (props: seq<string * obj>) (children: seq<R.Component>) =
        R.CreateElement(name, New props, inlineArrayOfSeq children)

    static member Text (s: string) =
        As<R.Component> s

    static member Mount target ``component`` =
        ReactDOM.Render(``component``, target) |> ignore

    static member Make<'T, 'Props, 'State when 'T :> ComponentClass<'Props, 'State>>
            (f: 'Props -> 'T)
            (props: 'Props)
            : R.Component =
        R.CreateElement(As<string> f, props)

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

[<assembly: JavaScript>]
do ()
