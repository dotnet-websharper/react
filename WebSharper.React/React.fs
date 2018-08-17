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

    static member Class<'Props, 'State>
        (
            render: R.Component<'Props, 'State> -> R.Component,
            ?defaultProps: 'Props,
            ?initialState: 'Props -> 'State
        ) : 'Props -> R.Component =
        let args =
            R.CreateClassArgs(Render = (fun this -> render this))
        match defaultProps with
        | None -> ()
        | Some props -> args.GetDefaultProps <- fun () -> props
        match initialState with
        | None -> ()
        | Some getState -> args.GetInitialState <- (fun this -> getState this.Props)
        let cls = R.CreateClass args
        fun props -> R.CreateElement(cls, props)

    static member Element (name: string) (props: seq<string * obj>) (children: seq<R.Component>) =
        R.CreateElement(name, New props, inlineArrayOfSeq children)

    static member Text (s: string) =
        As<R.Component> s

    static member Mount target ``component`` =
        ReactDOM.Render(``component``, target) |> ignore

[<assembly: JavaScript>]
do ()
