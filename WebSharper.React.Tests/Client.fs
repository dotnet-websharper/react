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

    let HomePage (router: Router<EndPoint>) =
        div [] [
            h1 [] [text "React tests"]
            ul [] [
                li [] [
                    a [attr.href ("#" + Router.Link router TicTacToe)] [text "Tic-Tac-Toe"]
                ]
                li [] [
                    a [attr.href ("#" + Router.Link router Context)] [text "Context"]
                ]
            ]
        ]

    [<SPAEntryPoint>]
    let Main () =
        let router = Router.Infer<EndPoint>()
        React.HashRouter router (fun endpoint ->
            match endpoint with
            | Home -> HomePage router
            | Context ->
                React.Fragment [
                    React.Make ContextTest ()
                    a [attr.href ("#" + Router.Link router Home)] [text "Back to Home"]
                ]
            | TicTacToe ->
                React.Fragment [
                    h1 [] [text "Tic-tac-toe"]
                    React.Make TicTacToe.Game ()
                    a [attr.href ("#" + Router.Link router Home)] [text "Back to Home"]
                ]
        )
        |> React.Mount (JS.Document.GetElementById "main")
