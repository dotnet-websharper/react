namespace WebSharper.React

open System.Collections.Generic

open WebSharper
open WebSharper.JavaScript

open WebSharper.React.Bindings

[<JavaScript>]
type Class<'T>() =
    member this.State
        with [<Inline "$0.state">] get () =
            X<'T>

    [<Inline "$0.setState($1)">]
    member this.SetState (_ : 'T) = ()

[<JavaScript>]
type Event<'T> =
    | OnMount of (Class<'T> -> (SyntheticEvent -> unit))
    | OnClick of (SyntheticEvent -> unit)

    override this.ToString () =
        match this with
        | OnMount _ -> null
        | OnClick _ -> "onClick"

/// A component that can be mapped to a native React element.
[<AbstractClass>]
[<JavaScript>]
type Component<'T>() =
    abstract member Map      : unit -> ReactElement
    abstract member AddEvent : Event<'T> -> unit

[<JavaScript>]
type Class<'T, 'U when 'U :> Component<'T>> internal (initialState : 'T, renderer : Class<'T> -> 'U) =
    inherit Component<'T>()

    let events : List<Event<'T>> = List()

    override this.Map () =
        New [
            yield "getInitialState" => fun () ->
                initialState
            yield "render" => FuncWithOnlyThis(fun this' ->
                (renderer this').Map()
            )

            match Seq.tryFind (function | OnMount _ -> true | _ -> false) events with
            | Some event ->
                yield "componentDidMount" => FuncWithThis<_, _ -> _>(event?``$0``)
            | _ ->
                ()
        ]
        |> React.CreateClass
        |> React.CreateElement

    override this.AddEvent event =
        events.Add event

[<JavaScript>]
type RouteMap<'T> =
    {
        Serializer   : 'T -> string
        Deserializer : string -> 'T
    }

[<JavaScript>]
type RouteMap =

    static member Create<'T> serializer deserializer : RouteMap<'T> =
        { Serializer = serializer; Deserializer = deserializer }

type Router<'T> = Class<'T>

[<AutoOpen>]
[<JavaScript>]
module Operators =
    
    let (|>!) (class' : #Component<'T>) event =
        class'.AddEvent event
        class'

[<JavaScript>]
module React =

    [<Inline "window">]
    let private Window = X<Dom.EventTarget>

    let Class initialState renderer =
        Class<_, _>(initialState, renderer)

    let Mount target (component' : #Component<'T>) =
        React.Render(component'.Map(), target)

    let Router<'T, 'U when 'U :> Component<'T> and 'T : equality> (routeMap : RouteMap<'T>) (renderer : Router<_> -> 'U) =
        Class (routeMap.Deserializer "/") (fun this ->
            JS.Window.Location.Hash <- "#" + (routeMap.Serializer this.State)
            
            renderer this
        )
        |>! OnMount (fun class' _ ->
            JS.Window.Location.Hash <- "#/"
            
            Window.AddEventListener("hashchange", (fun () ->
                let newState =
                    routeMap.Deserializer (JS.Window.Location.Hash.Substring 1)
                
                if class'.State <> newState then
                    class'.SetState newState
            ), false)
        )

[<AutoOpen>]
[<JavaScript>]
module Element =

    type Element<'T>(tagName : string, ?children : Component<'T> list) =
        inherit Component<'T>()
        
        let events : List<Event<'T>> = List()

        override this.Map () =
            let children =
                match children with
                | Some children ->
                    children
                    |> List.map (fun child ->
                        child.Map()
                    )
                    |> List.toArray
                | None ->
                    Array.empty
                
            let properties =
                New [
                    yield! events.ToArray()
                        |> Array.map (fun event ->
                            (string event, event?``$0``)
                        )
                ]

            if tagName.StartsWith "!" then
                React.CreateElement("span", null, (tagName.Substring 1))
            else
                React.CreateElement(tagName, properties, children)

        override this.AddEvent event =
            events.Add event

    let Text x = Element("!" + x)

    let Wrap children =
        Element("div", children)
