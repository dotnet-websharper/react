namespace WebSharper.Elmist.React

open WebSharper.React.Bindings
open WebSharper.JavaScript
open Elmish
open WebSharper.Core.Resources

module Internal =

    let inline ofType<'T,'P,'S when 'T :> React.Component<'P,'S>> (props: 'P) (children: React.Element seq): React.Element =
        React.CreateElement(jsConstructor<'T>, props, children |> Array.ofSeq)

    let updateInputValue (value: string) (e: HTMLInputElement) =
        if not(isNull e) && e.Value <> value then
            e.Value <- value

type LazyProps<'model> = {
    model:'model
    render:unit->React.Element
    equal:'model->'model->bool
}

module Components =
    type LazyView<'model>(props) =
        inherit React.Component<LazyProps<'model>,obj>(props)

        override this.ShouldComponentUpdate(nextProps, _nextState) =
            not <| this.Props.equal this.Props.model nextProps.model

        override this.Render () =
            this.Props.render ()

[<AutoOpen>]
module Common =

    /// Avoid rendering the view unless the model has changed.
    /// equal: function to compare the previous and the new states
    /// view: function to render the model
    /// state: new state to render
    let lazyViewWith (equal:'model->'model->bool)
                     (view:'model->React.Element)
                     (state:'model) =
        Internal.ofType<Components.LazyView<_>,_,_>
            { render = fun () -> view state
              equal = equal
              model = state }
            []

    /// Avoid rendering the view unless the model has changed.
    /// equal: function to compare the previous and the new states
    /// view: function to render the model using the dispatch
    /// state: new state to render
    /// dispatch: dispatch function
    let lazyView2With (equal:'model->'model->bool)
                      (view:'model->'msg Dispatch->React.Element)
                      (state:'model)
                      (dispatch:'msg Dispatch) =
        Internal.ofType<Components.LazyView<_>,_,_>
            { render = fun () -> view state dispatch
              equal = equal
              model = state }
            []

    /// Avoid rendering the view unless the model has changed.
    /// equal: function to compare the previous and the new model (a tuple of two states)
    /// view: function to render the model using the dispatch
    /// state1: new state to render
    /// state2: new state to render
    /// dispatch: dispatch function
    let lazyView3With (equal:_->_->bool) (view:_->_->_->React.Element) state1 state2 (dispatch:'msg Dispatch) =
        Internal.ofType<Components.LazyView<_>,_,_>
            { render = fun () -> view state1 state2 dispatch
              equal = equal
              model = (state1,state2) }
            []

    /// Avoid rendering the view unless the model has changed.
    /// view: function of model to render the view
    let lazyView (view:'model->React.Element) =
        lazyViewWith (=) view

    /// Avoid rendering the view unless the model has changed.
    /// view: function of two arguments to render the model using the dispatch
    let lazyView2 (view:'model->'msg Dispatch->React.Element) =
        lazyView2With (=) view

    /// Avoid rendering the view unless the model has changed.
    /// view: function of three arguments to render the model using the dispatch
    let lazyView3 (view:_->_->_->React.Element) =
        lazyView3With (=) view


[<AutoOpen>]
module Helpers =
    open WebSharper.JavaScript.Pervasives
    /// `Ref` callback that sets the value of an input textbox after DOM element is created.
    /// Can be used instead of `DefaultValue` and `Value` props to override input box value.
    let inline valueOrDefault (value: string): IHTMLProp =
        As<IHTMLProp> ("ref", Internal.updateInputValue value)

[<RequireQualifiedAccess>]
module Program =

    module Internal =

        open Elmish
        open WebSharper.React.ReactDOM.Bindings
        open WebSharper.JavaScript

        // Use the new rendering API in React 18+
        let useRootApi = try int React.Version.[ .. 1 ] >= 18 with _ -> false

        let withReactBatchedUsing lazyView2With placeholderId (program:Program<_,_,_,_>) =
            let setState =
                let mutable lastRequest = None

                if useRootApi then
                    let root = ReactDomClient.CreateRoot (JS.Document.GetElementById placeholderId)

                    fun model dispatch ->
                        match lastRequest with
                        | Some r -> JS.CancelAnimationFrame r
                        | _ -> ()

                        lastRequest <- Some (JS.RequestAnimationFrame (fun _ ->
                            root.Render (lazyView2With (fun x y -> obj.ReferenceEquals(x,y)) (Program.view program) model dispatch)))
                else
                    fun model dispatch ->
                        match lastRequest with
                        | Some r -> JS.CancelAnimationFrame r
                        | _ -> ()

                        lastRequest <- Some (JS.RequestAnimationFrame (fun _ ->
                            ReactDOM.Render(
                                lazyView2With (fun x y -> obj.ReferenceEquals(x,y)) (Program.view program) model dispatch,
                                JS.Document.GetElementById placeholderId
                            )))

            program
            |> Program.withSetState setState

        let withReactSynchronousUsing lazyView2With placeholderId (program:Elmish.Program<_,_,_,_>) =
            let setState =
                if useRootApi then
                    let root = ReactDomClient.CreateRoot (JS.Document.GetElementById placeholderId)

                    fun model dispatch ->
                        root.Render (lazyView2With (fun x y -> obj.ReferenceEquals(x,y)) (Program.view program) model dispatch)
                else
                    fun model dispatch ->
                        ReactDOM.Render(
                            lazyView2With (fun x y -> obj.ReferenceEquals(x,y)) (Program.view program) model dispatch,
                            JS.Document.GetElementById placeholderId
                        )

            program
            |> Program.withSetState setState

        let withReactHydrateUsing lazyView2With placeholderId (program:Elmish.Program<_,_,_,_>) =
            let setState =
                if useRootApi then
                    let mutable root = None

                    fun model dispatch ->
                        match root with
                        | None ->
                            root <-
                                ReactDomClient.HydrateRoot (
                                    JS.Document.GetElementById placeholderId,
                                    lazyView2With (fun x y -> obj.ReferenceEquals(x,y)) (Program.view program) model dispatch
                                ) |> Some
                        | Some root ->
                            root.Render (lazyView2With (fun x y -> obj.ReferenceEquals(x,y)) (Program.view program) model dispatch)
                else
                    fun model dispatch ->
                        ReactDOM.Hydrate(
                            lazyView2With (fun x y -> obj.ReferenceEquals(x,y)) (Program.view program) model dispatch,
                            JS.Document.GetElementById placeholderId
                        )

            program
            |> Program.withSetState setState


    /// Renders React root component inside html element identified by placeholderId.
    /// Uses `requestAnimationFrame` to batch updates to prevent drops in frame rate.
    /// NOTE: This may have unexpected effects in React controlled inputs, see https://github.com/elmish/react/issues/12
    let withReactBatched placeholderId (program:Elmish.Program<_,_,_,_>) =
        Internal.withReactBatchedUsing lazyView2With placeholderId program

    /// Renders React root component inside html element identified by placeholderId.
    /// New renders are triggered immediately after an update.
    let withReactSynchronous placeholderId (program:Elmish.Program<_,_,_,_>) =
        Internal.withReactSynchronousUsing lazyView2With placeholderId program

    /// Renders React root component inside html element identified by placeholderId using `React.hydrate`.
    let withReactHydrate placeholderId (program:Elmish.Program<_,_,_,_>) =
        Internal.withReactHydrateUsing lazyView2With placeholderId program

module ReactNative =

    open Elmish
    open WebSharper

    module Components =
        type AppState = {
            render : unit -> React.Element
            setState : AppState -> unit
        }

        let mutable appState = None

        type App(props) as this =
            inherit React.Component<obj,AppState>(props)
            do
                match appState with
                | Some state ->
                    appState <- Some { state with AppState.setState = this.SetInitialState }
                    this.SetInitialState state
                | _ -> failwith "was Elmish.ReactNative.Program.withReactNative called?"

            override this.ComponentDidMount() =
                appState <- Some { appState.Value with setState = fun s -> this.SetState(fun _ _ -> s) }

            override this.ComponentWillUnmount() =
                appState <- Some { appState.Value with setState = ignore; render = this.State.render }

            override this.Render () =
                this.State.render()

    //FIXME I don't know what to do with this
    type ReactNative() =
        inherit BaseResource("what.js")

    //FIXME Require react-native resource
    type AppRegistry =
        [<Inline "$global.AppRegistry.registerComponent($appKey, $getComponentFunc)">]
        static member registerComponent(appKey:string, getComponentFunc:unit->React.ElementType<_>) : unit = X<unit>

    [<RequireQualifiedAccess>]
    module Program =
        open Components

        /// Setup rendering of root ReactNative component
        let withReactNative appKey (program:Program<_,_,_,_>) =
            AppRegistry.registerComponent(appKey, fun () -> unbox jsConstructor<App>)
            let setState m d =
                 match appState with
                 | Some state ->
                    state.setState { state with render = fun () -> (Program.view program) m d }
                 | _ ->
                    appState <- Some { render = fun () -> (Program.view program) m d
                                       setState = ignore }

            program
            |> Program.withSetState setState