namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript
open WebSharper.Sitelets
open WebSharper.Sitelets.InferRouter
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

    type BaseRouter<'Endpoint when 'Endpoint : equality>(router: Router<'Endpoint>, render_: 'Endpoint -> R.Component) =
        inherit R.Component<Router<'Endpoint>, 'Endpoint>(router)

        override this.Render() =
            render_ this.State

    type HashRouter<'Endpoint when 'Endpoint : equality>(router: Router<'Endpoint>, render_: 'Endpoint -> R.Component) as this =
        inherit BaseRouter<'Endpoint>(router, render_)

        let mutable listener : (Dom.Event -> unit) = JS.Undefined

        let computeRoute() =
            Route.FromHash(JS.Window.Location.Hash, true)
            |> Router.Parse router

        do computeRoute() |> Option.iter this.SetInitialState

        override this.ComponentDidMount() =
            listener <- fun (e: Dom.Event) ->
                computeRoute() |> Option.iter this.SetState
            JS.Window.AddEventListener("hashchange", listener)

        override this.ComponentWillUnmount() =
            JS.Window.RemoveEventListener("hashchange", listener)

    // // Attempt to force WebSharper to include abstract methods in the output even with DCE.
    // let private forceMethods() =
    //     New [
    //         "a" => fun (x: R.Component<unit, unit>) -> x.Render()
    //         "b" => fun (x: R.Component<unit, unit>) -> x.ComponentDidMount()
    //         "c" => fun (x: R.Component<unit, unit>) a b -> x.ComponentDidUpdate(a, b)
    //         "d" => fun (x: R.Component<unit, unit>) -> x.ComponentWillUnmount()
    //         "e" => fun (x: R.Component<unit, unit>) a b -> x.ShouldComponentUpdate(a, b)
    //         "f" => fun (x: R.Component<unit, unit>) a b -> x.GetDerivedStateFromProps(a, b)
    //         "g" => fun (x: R.Component<unit, unit>) a b -> x.GetSnapshotBeforeUpdate(a, b)
    //         "h" => fun (x: R.Component<unit, unit>) a b -> x.ComponentDidCatch(a, b)
    //     ]

    [<Inline>]
    let HashRouter (router: Router<'Endpoint>) (render: 'Endpoint -> R.Component) : R.Component =
        // forceMethods()
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
