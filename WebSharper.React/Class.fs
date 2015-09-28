namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript

open WebSharper.React.Bindings

type Class<'a> =
    member this.State with [<Inline "$0.state">] get () = X<'a>

    [<Inline "$0.setState($1)">]
    member this.SetState (_ : 'a) = ()

type Context =
    interface end

type Class<'a, 'b when 'b :> Component> =
    {
        InitialState : 'a
        Renderer     : Class<'a> -> 'b
        Events       : (string * (Class<'a> -> SyntheticEvent -> unit)) list
        Context      : Context option
    }
    
    interface Component with
        [<JavaScript>]
        member this.Map () =
            New [
                yield "getInitialState" => fun () -> this.InitialState

                yield "render" => FuncWithOnlyThis(fun this' ->
                    (this.Renderer this').Map()
                )

                match this.Context with
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

                yield! this.Events
                    |> List.map (fun (event, callback) ->
                        (event, box (FuncWithThis callback))
                    )
            ]
            |> React.CreateClass
            |> React.CreateElement

[<AutoOpen>]
[<JavaScript>]
module ClassEvents =
    
    let private Event<'a, 'b when 'b :> Component> type' callback (class' : Class<'a, 'b>) =
        { class' with
            Events = (type', callback) :: class'.Events }

    let OnMount<'a, 'b when 'b :> Component> = Event<'a, 'b> "componentDidMount"

    let OnBeforeMount<'a, 'b when 'b :> Component> = Event<'a, 'b> "componentWillMount"

    let OnUpdate<'a, 'b when 'b :> Component> = Event<'a, 'b> "componentDidUpdate"

[<JavaScript>]
module Class =
    
    let WithContext context (class' : Class<_, _>) =
        { class' with
            Context = Some context }
