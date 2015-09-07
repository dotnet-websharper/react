namespace WebSharper.React.Obsolete

open WebSharper
open WebSharper.JavaScript

open WebSharper.React.Bindings

[<JavaScript>]
type Component<'T> private () =

    member this.State with [<Inline "$0.state">] get () = X<'T>

    [<Inline "$0.setState($1)">]
    member this.SetState (_ : 'T) =
        ()

type GenericContext =
    interface end

[<JavaScript>]
type Class<'T, 'U when 'U :> GenericElement> =
    {
        InitialState : 'T
        Renderer     : Component<'T> -> 'U
        Context      : GenericContext option
    }

[<JavaScript>]
module React =
    
    let Class initialState renderer =
        {
            InitialState = initialState
            Renderer     = renderer
            Context      = None
        }

    let Mount target (class' : Class<'T, _>) =
        New [
            yield "getInitialState" => fun _ ->
                class'.InitialState
            yield "render" => FuncWithOnlyThis(fun this ->
                GenericElement.Transpile (class'.Renderer this)
            )

            match class'.Context with
            | Some context ->
                let types =
                    New [
                        yield! context
                            |> Object.Keys
                            |> Array.map (fun key ->
                                key => React.PropTypes.Any
                            )
                    ]
                
                yield "childContextTypes" => types
                yield "getChildContext"   => fun _ ->
                    context
            | _ ->
                ()
        ]
        |> React.CreateClass
        |> React.CreateElement
        |> (fun node ->
            React.Render(node, target)
        )

[<JavaScript>]
module Class =
    
    let WithContext context (class' : Class<'T, 'U>) =
        { class' with
            Context = Some context }
