// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2026 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
namespace WebSharper.React.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.Sitelets
open WebSharper.Sitelets.InferRouter
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.React
open WebSharper.React.Html
open WebSharper.React.ReactDOM

[<JavaScript>]
module Client =

    // The template is the whole document, loaded from the DOM, so editing
    // index.html and refreshing the browser is enough.
    type IndexTemplate = Template<"index.html", ClientLoad.FromDocument>

    // ------------------------------------------------------------------
    // Page chrome (WebSharper.UI template)
    // ------------------------------------------------------------------

    let themeStorageKey = "websharper-react-theme"
    let theme = Var.Create "light"
    let themeLabel = Var.Create "🌙 Dark mode"
    let notice = Var.Create "The page chrome is a WebSharper.UI template; everything below the fold is React."
    let status = Var.Create ""
    let reactVersion = Var.Create "-"
    let route = Var.Create "-"

    let byId (id: string) =
        let el = JS.Document.GetElementById id
        if isNull el then None else Some el

    let applyTheme () =
        JS.Document.DocumentElement.SetAttribute("data-theme", theme.Value)
        themeLabel.Value <-
            if theme.Value = "dark" then "☀️ Light mode" else "🌙 Dark mode"

    let toggleTheme () =
        theme.Value <- (if theme.Value = "dark" then "light" else "dark")
        try JS.Window.LocalStorage.SetItem(themeStorageKey, theme.Value) with _ -> ()
        applyTheme ()

    // ------------------------------------------------------------------
    // React components
    // ------------------------------------------------------------------

    /// A context shared by the components below (Provider, Consumer, useContext).
    let accentContext = React.CreateContext "unprovided"

    type CounterProps = { step: int; label: string }

    /// useState · useCallback · useMemo · useRef · useId · useEffect
    let Counter (props: CounterProps) =
        let count, setCount = React.UseState 0
        let id = React.UseId()
        let renders = React.UseRef 0
        renders.Current <- renders.Current + 1
        let bump =
            React.UseCallback(
                (fun () -> setCount.Invoke (count + props.step)),
                [| box count; box props.step |]
            )
        let doubled = React.UseMemo((fun () -> count * 2), [| box count |])
        React.UseEffect((fun () -> Console.Log "Counter mounted"), [||]) |> ignore
        div [] [
            h3 [] [text props.label]
            div [attr.className "counter"] [
                button [attr.className "btn"; on.click (fun _ -> setCount.Invoke (count - props.step))] [text "−"]
                strong [] [text (string count)]
                button [attr.className "btn"; on.click (fun _ -> bump.Invoke ())] [text "+"]
            ]
            p [attr.className "hint"] [
                textf "step %d · doubled %d · renders %d · useId %s"
                    props.step doubled renders.Current id
            ]
        ]

    let private letters =
        [|
            "Alpha"; "Beta"; "Gamma"; "Delta"; "Epsilon"; "Zeta"; "Eta"; "Theta"
            "Iota"; "Kappa"; "Lambda"; "Mu"; "Nu"; "Xi"; "Omicron"; "Pi"; "Rho"
            "Sigma"; "Tau"; "Upsilon"; "Phi"; "Chi"; "Psi"; "Omega"
        |]

    let private inputValue (e: SyntheticEvent) =
        string ((As<Dom.Element> e.Target)?value)

    /// useState · useDeferredValue · useTransition
    let Filter (props: unit) =
        let query, setQuery = React.UseState ""
        let deferred = React.UseDeferredValue query
        let isPending, startTransition = React.UseTransition()
        let matches =
            letters
            |> Array.filter (fun w ->
                deferred = "" || w.ToLower().Contains(deferred.ToLower()))
        div [] [
            input [
                attr.className "input"
                attr.value query
                attr.placeholder "Filter the Greek letters…"
                on.input (fun e ->
                    let value = inputValue e
                    startTransition.Invoke(fun () -> setQuery.Invoke value))
            ] []
            p [attr.className "hint"] [
                textf "%d match(es)%s"
                    matches.Length
                    (if isPending then " · transition pending…" else "")
            ]
            ul [attr.className "tag-list"] [
                for w in matches ->
                    li [attr.key w] [text w]
            ]
        ]

    type TallyState = { value: int }
    type TallyAction = | Increment | Decrement | Reset

    let tallyReducer (state: TallyState) (action: TallyAction) =
        match action with
        | Increment -> { state with value = state.value + 1 }
        | Decrement -> { state with value = state.value - 1 }
        | Reset -> { value = 0 }

    /// useReducer
    let Tally (props: unit) =
        let state, dispatch = React.UseReducer((fun s a -> tallyReducer s a), { value = 0 })
        div [] [
            div [attr.className "counter"] [
                button [attr.className "btn"; on.click (fun _ -> dispatch.Invoke Decrement)] [text "−"]
                strong [] [text (string state.value)]
                button [attr.className "btn"; on.click (fun _ -> dispatch.Invoke Increment)] [text "+"]
                button [attr.className "btn"; on.click (fun _ -> dispatch.Invoke Reset)] [text "Reset"]
            ]
            p [attr.className "hint"] [textf "useReducer state: %d" state.value]
        ]

    type BadgeProps = { expected: string }

    /// useContext
    let Badge (props: BadgeProps) =
        let accent = React.UseContext accentContext
        span [attr.className "tag"] [
            textf "useContext → %s (expected %s)" accent props.expected
        ]

    /// createContext · Context.Provider · Context.Consumer
    let ContextPanel (props: unit) =
        div [] [
            accentContext.Provide "provided" [
                ReactHelpers.OfFunction Badge { expected = "provided" }
            ]
            accentContext.Consume (fun accent ->
                [ p [attr.className "hint"] [
                      textf "Context.Consumer → %s (expected unprovided)" accent
                  ] ])
        ]

    let portalRoot =
        let el = JS.Document.CreateElement "div"
        JS.Document.Body.AppendChild el |> ignore
        el

    type PortalProps = { onClose: unit -> unit }

    let PortalCard (props: PortalProps) =
        div [attr.className "modal-backdrop"] [
            div [attr.className "modal"] [
                h3 [] [text "ReactDOM.createPortal"]
                p [] [
                    text "This dialog is rendered into a separate node appended to "
                    code [] [text "<body>"]
                    text ", while staying part of the React tree."
                ]
                div [attr.className "toolbar"] [
                    button [
                        attr.className "btn btn-primary"
                        on.click (fun _ -> props.onClose ())
                    ] [text "Close"]
                ]
            ]
        ]

    /// useState · createPortal
    let PortalPanel (props: unit) =
        let isOpen, setOpen = React.UseState false
        div [] [
            div [attr.className "toolbar"] [
                button [
                    attr.className "btn btn-primary"
                    on.click (fun _ -> setOpen.Invoke true)
                ] [text "Open modal"]
            ]
            if isOpen then
                ReactDOM.CreatePortal(
                    ReactHelpers.OfFunction PortalCard { onClose = (fun () -> setOpen.Invoke false) },
                    portalRoot
                )
            else
                ReactHelpers.nothing
        ]

    /// renderToString / renderToStaticMarkup (react-dom/server)
    let ssrMarkup =
        try
            let el =
                ul [attr.className "tag-list"] [
                    li [attr.key "a"] [text "server-rendered"]
                    li [attr.key "b"] [text "no DOM"]
                ]
            ReactDOMServer.RenderToString el
        with _ ->
            "(renderToString is unavailable in this environment)"

    let ssrStaticMarkup =
        try
            ReactDOMServer.RenderToStaticMarkup(
                button [attr.className "btn"] [text "static markup"]
            )
        with _ ->
            "(renderToStaticMarkup is unavailable in this environment)"

    let SsrPanel (props: unit) =
        div [] [
            p [attr.className "hint"] [text "ReactDOMServer rendered these strings at startup:"]
            pre [attr.className "code"] [text ssrMarkup]
            pre [attr.className "code"] [text ssrStaticMarkup]
        ]

    // ------------------------------------------------------------------
    // Routing
    // ------------------------------------------------------------------

    type EndPoint =
        | [<EndPoint "/">] Overview
        | [<EndPoint "/game">] Game
        | [<EndPoint "/extras">] Extras

    let private endpointName = function
        | Overview -> "Overview"
        | Game -> "Tic-Tac-Toe"
        | Extras -> "Extras"

    let private card title subtitle (body: React.Element) =
        div [attr.className "card"] [
            header [attr.className "card-header"] [
                h2 [] [text title]
                p [] [text subtitle]
            ]
            body
        ]

    let private section title subtitle body =
        div [] [
            header [attr.className "card-header"] [
                h2 [] [text title]
                p [] [text subtitle]
            ]
            body
        ]

    let OverviewPage () =
        ReactHelpers.Fragment [
            card "Hooks" "useState, useCallback, useMemo, useRef, useId and useEffect in a function component."
                (ReactHelpers.OfFunction Counter { step = 1; label = "Counter" })
            card "Concurrent rendering" "useDeferredValue and useTransition keep typing responsive."
                (ReactHelpers.OfFunction Filter ())
            card "Reducer" "useReducer with a discriminated union of actions."
                (ReactHelpers.OfFunction Tally ())
            card "Context" "createContext, Context.Provider, Context.Consumer and useContext."
                (ReactHelpers.OfFunction ContextPanel ())
            card "Portals" "ReactDOM.createPortal renders outside the parent DOM node."
                (ReactHelpers.OfFunction PortalPanel ())
            card "Server rendering" "ReactDOMServer.renderToString and renderToStaticMarkup."
                (ReactHelpers.OfFunction SsrPanel ())
        ]

    let GamePage () =
        ReactHelpers.Fragment [
            card "Tic-tac-toe" "Class components: React.Component, SetInitialState, SetState, Props and State."
                (ReactHelpers.Make TicTacToe.Game ())
        ]

    let ExtrasPage () =
        ReactHelpers.Fragment [
            card "Higher-order and structural helpers"
                "ReactHelpers.Elt, ReactHelpers.Text, ReactHelpers.Fragment and React.ElementType."
                (ReactHelpers.Fragment [
                    ReactHelpers.Elt "p" [] [ReactHelpers.Text "Elt builds an element from a tag name, props and children."]
                    p [] [text "Elements can be nested with ReactHelpers.Fragment and keyed with attr.key."]
                    ul [attr.className "tag-list"] [
                        for w in letters do
                            li [attr.key w] [text w]
                    ]
                ])
        ]

    let private navLink (router: ReactHelpers.Router<EndPoint>) (endpoint: EndPoint) (label: string) =
        a [
            router.Href endpoint
            attr.className (if router.State = endpoint then "btn btn-primary" else "btn")
        ] [text label]

    let App () =
        ReactHelpers.HashRouter (Router.Infer<EndPoint>()) (fun router ->
            route.Value <- endpointName router.State
            ReactHelpers.Fragment [
                nav [attr.className "nav"] [
                    navLink router Overview "Overview"
                    navLink router Game "Tic-Tac-Toe"
                    navLink router Extras "Extras"
                ]
                match router.State with
                | Overview -> OverviewPage ()
                | Game -> GamePage ()
                | Extras -> ExtrasPage ()
            ])

    // ------------------------------------------------------------------
    // Entry point
    // ------------------------------------------------------------------

    [<SPAEntryPoint>]
    let Main () =
        // Restore a previously chosen theme (light is the default).
        let saved = try JS.Window.LocalStorage.GetItem themeStorageKey with _ -> null
        if not (isNull saved) && (saved = "dark" || saved = "light") then
            theme.Value <- saved
        applyTheme ()

        try
            reactVersion.Value <- React.Version
        with _ -> ()

        // Bind the page chrome first; `.Bind()` hydrates the whole document body.
        IndexTemplate()
            .themeLabel(themeLabel.V)
            .notice(notice.V)
            .status(status.V)
            .reactVersion(reactVersion.V)
            .route(route.V)
            .ToggleTheme(fun _ -> toggleTheme ())
            .Bind()

        // The DOM is ready now, so mount the React tree into its container.
        match byId "react-root" with
        | Some root ->
            try
                ReactHelpers.Mount root (App ())
                status.Value <- "React tree mounted with React " + React.Version
            with ex ->
                status.Value <- "Could not mount React: " + ex.Message
        | None ->
            status.Value <- "The #react-root container was not found."