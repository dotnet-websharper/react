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
                text ("This should be \"" + props.shouldBe + "\": " + theme)
            ]
        ]

    type ContextTest() =
        inherit ComponentClass<unit, unit>(())

        override this.Render() =
            React.Fragment [
                theme.Provide "provided" [
                    Widget { shouldBe = "provided" }
                ]
                Widget { shouldBe = "default" }
            ]

    [<SPAEntryPoint>]
    let Main () =
        React.Make TicTacToe.Game ()
        |> React.Mount (JS.Document.GetElementById "tictactoe")

        React.Make ContextTest ()
        |> React.Mount (JS.Document.GetElementById "context")
