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

    [<AbstractClass>]
    type Router<'Endpoint when 'Endpoint : equality>
        (
            router: SRouter<'Endpoint>,
            render_: Router<'Endpoint> -> R.Component
        ) =
        inherit R.Component<SRouter<'Endpoint>, 'Endpoint>(router)

        override this.Render() =
            render_ this

        [<Inline>]
        member this.Goto(endpoint: 'Endpoint) =
            this.SetState endpoint

        abstract Link : endpoint: 'Endpoint -> string

        [<Inline>]
        member this.Href(endpoint: 'Endpoint) =
            "href" => this.Link endpoint

    type HashRouter<'Endpoint when 'Endpoint : equality>
        (
            router: SRouter<'Endpoint>,
            render: Router<'Endpoint> -> R.Component
        ) as this =
        inherit Router<'Endpoint>(router, render)

        let mutable listener : (Dom.Event -> unit) = JS.Undefined

        let computeRoute() =
            Route.FromHash(JS.Window.Location.Hash, true)
            |> SRouter.Parse router

        do computeRoute() |> Option.iter this.SetInitialState

        override this.Link(endpoint: 'Endpoint) =
            router.HashLink endpoint

        override this.ComponentDidUpdate(prevProps, prevState, _) =
            if prevState <> this.State then
                JS.Window.Location.Hash <- this.Link this.State

        override this.ComponentDidMount() =
            listener <- fun (e: Dom.Event) ->
                computeRoute() |> Option.iter this.SetState
            JS.Window.AddEventListener("hashchange", listener)

        override this.ComponentWillUnmount() =
            JS.Window.RemoveEventListener("hashchange", listener)

    [<Inline>]
    let HashRouter (router: SRouter<'Endpoint>) (render: Router<'Endpoint> -> R.Component) : R.Component =
        Make (fun router -> HashRouter(router, render) :> R.Component<_, _>) router

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
