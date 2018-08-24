namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript
open WebSharper.Sitelets
open WebSharper.Sitelets.InferRouter
open WebSharper.React.Bindings
type private R = WebSharper.React.Bindings.React
type private SRouter<'Endpoint when 'Endpoint : equality> = WebSharper.Sitelets.Router<'Endpoint>
module SRouter = WebSharper.Sitelets.Router
type React = R

[<JavaScript false>]
module Macros =
    open WebSharper.Core
    open WebSharper.Core.AST
    module M = WebSharper.Core.Metadata
    module I = WebSharper.Core.AST.IgnoreSourcePos

    type Make() =
        inherit Macro()

        override this.TranslateCall(call) =
            match call.Arguments.[0] with
            | I.Function ([props], I.Return (I.Ctor(ty, ctor, [I.Var props']))) when props = props' ->
                // Constructor of a .NET class
                match call.Compilation.GetClassInfo(ty.Entity) with
                | Some x ->
                    match x.Constructors.[ctor] with
                    | M.Constructor addr ->
                        let args = GlobalAccess addr :: List.tail call.Arguments
                        let call = Call(call.This, call.DefiningType, call.Method, args)
                        MacroDependencies ([M.ConstructorNode(ty.Entity, ctor)], MacroOk call)
                    | _ -> MacroFallback
                | None -> MacroFallback
            | I.Function ([props], I.Return (I.New (ctor, [I.Var props']))) when props = props' ->
                // Inlined js constructor
                let args = ctor :: List.tail call.Arguments
                let call = Call(call.This, call.DefiningType, call.Method, args)
                MacroOk call
            | _ -> MacroFallback

module React =

    let internal inlineArrayOfSeq (s: seq<'T>) : array<'T> =
        match s with
        | :? System.Array -> As s
        | s -> Array.ofSeq s

    let Element (name: string) (props: seq<string * obj>) (children: seq<R.Element>) =
        R.CreateElement(name, New props, inlineArrayOfSeq children)

    [<Inline>]
    let Text (s: string) =
        As<R.Element> s

    [<Inline>]
    let Mount target ``component`` =
        ReactDOM.Render(``component``, target) |> ignore

    [<Macro(typeof<Macros.Make>)>]
    [<Inline>]
    let Make<'T, 'Props, 'State when 'T :> R.Component<'Props, 'State>> (f: 'Props -> 'T) (props: 'Props) =
        R.CreateElement(As<R.Class> f, props)

    let Fragment (children: seq<R.Element>) =
        R.CreateElement(R.Fragment, null, inlineArrayOfSeq children)

    [<AbstractClass>]
    type Router<'Endpoint when 'Endpoint : equality>(props) =
        inherit R.Component<RouterProps<'Endpoint>, 'Endpoint>(props)

        override this.Render() =
            this.Props.render this

        [<Inline>]
        member this.Goto(endpoint: 'Endpoint) =
            this.SetState endpoint

        abstract Link : endpoint: 'Endpoint -> string

        [<Inline>]
        member this.Href(endpoint: 'Endpoint) =
            "href" => this.Link endpoint

    and RouterProps<'Endpoint when 'Endpoint : equality> =
        {
            router: SRouter<'Endpoint>
            render: Router<'Endpoint> -> R.Element
        }

    type HashRouter<'Endpoint when 'Endpoint : equality>(props) as this =
        inherit Router<'Endpoint>(props)

        let mutable listener : (Dom.Event -> unit) = JS.Undefined

        do this.ComputeRoute() |> Option.iter this.SetInitialState

        member this.ComputeRoute() =
            Route.FromHash(JS.Window.Location.Hash, true)
            |> SRouter.Parse this.Props.router

        override this.Link(endpoint: 'Endpoint) =
            this.Props.router.HashLink endpoint

        override this.ComponentDidUpdate(prevProps, prevState, _) =
            if prevState <> this.State then
                JS.Window.Location.Hash <- this.Link this.State

        override this.ComponentDidMount() =
            listener <- fun (e: Dom.Event) ->
                this.ComputeRoute() |> Option.iter this.SetState
            JS.Window.AddEventListener("hashchange", listener)

        override this.ComponentWillUnmount() =
            JS.Window.RemoveEventListener("hashchange", listener)

    [<Inline>]
    let HashRouter (router: SRouter<'Endpoint>) (render: Router<'Endpoint> -> R.Element) : R.Element =
        Make HashRouter { router = router; render = render }

[<AutoOpen>]
module Extensions =

    type R.Context<'T> with

        member this.Provide (value: 'T) (comp: seq<R.Element>) =
            R.CreateElement(this.Provider, New ["value" => value], React.inlineArrayOfSeq comp)

        member this.Consume (f: 'T -> #seq<R.Element>) =
            R.CreateElement(this.Consumer, null, fun v ->
                R.CreateElement(R.Fragment, null, React.inlineArrayOfSeq(f v)))

[<assembly: JavaScript>]
do ()
