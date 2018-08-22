namespace WebSharper.React.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.React.Html

[<JavaScript>]
module Client =

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

    [<SPAEntryPoint>]
    let Main () =
        React.Fragment [
            h1 [] [text "Tic-tac-toe"]
            React.Make TicTacToe.Game ()
        ]
        |> React.Mount (JS.Document.GetElementById "tictactoe")

        React.Make ContextTest ()
        |> React.Mount (JS.Document.GetElementById "context")
