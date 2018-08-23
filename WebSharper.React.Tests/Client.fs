namespace WebSharper.React.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.Sitelets
open WebSharper.Sitelets.InferRouter
open WebSharper.React
open WebSharper.React.Html

[<JavaScript>]
module Client =

    type EndPoint =
        | [<EndPoint "/">] Home
        | [<EndPoint "/context">] Context
        | [<EndPoint "/tic-tac-toe">] TicTacToe

    let theme = React.CreateContext("default")

    type WidgetProps = { shouldBe: string }

    let Widget props =
        theme.Consume <| fun theme -> [
            div [] [
                textf "This should be \"%s\": %s" props.shouldBe theme
            ]
        ]

    type ContextTest() =
        inherit React.Component<unit, unit>(())

        override this.Render() =
            React.Fragment [
                h1 [] [text "Context test"]
                theme.Provide "provided" [
                    Widget { shouldBe = "provided" }
                ]
                Widget { shouldBe = "default" }
            ]

    let HomePage (router: React.Router<EndPoint>) =
        div [] [
            h1 [] [text "React tests"]
            ul [] [
                li [] [
                    a [router.Href TicTacToe] [text "Tic-Tac-Toe"]
                ]
                li [] [
                    a [router.Href Context] [text "Context"]
                ]
            ]
        ]

    [<SPAEntryPoint>]
    let Main () =
        React.HashRouter (Router.Infer<EndPoint>()) (fun router ->
            match router.State with
            | Home -> HomePage router
            | Context ->
                React.Fragment [
                    React.Make ContextTest ()
                    a [
                        // Test that setting the state sets the route
                        attr.href "javascript:void(0)"
                        on.click (fun _ -> router.Goto Home)
                    ] [text "Back to Home"]
                ]
            | TicTacToe ->
                React.Fragment [
                    h1 [] [text "Tic-tac-toe"]
                    React.Make TicTacToe.Game ()
                    a [router.Href Home] [text "Back to Home"]
                ]
        )
        |> React.Mount (JS.Document.GetElementById "main")
