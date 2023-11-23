// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
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
            ReactHelpers.Fragment [
                h1 [] [text "Context test"]
                theme.Provide "provided" [
                    Widget { shouldBe = "provided" }
                ]
                Widget { shouldBe = "default" }
            ]

    let HomePage (router: ReactHelpers.Router<EndPoint>) =
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
        ReactHelpers.HashRouter (Router.Infer<EndPoint>()) (fun router ->
            match router.State with
            | Home -> HomePage router
            | Context ->
                ReactHelpers.Fragment [
                    ReactHelpers.Make ContextTest ()
                    a [
                        // Test that setting the state sets the route
                        attr.href "javascript:void(0)"
                        on.click (fun _ -> router.Goto Home)
                    ] [text "Back to Home"]
                ]
            | TicTacToe ->
                ReactHelpers.Fragment [
                    h1 [] [text "Tic-tac-toe"]
                    ReactHelpers.Make TicTacToe.Game ()
                    a [router.Href Home] [text "Back to Home"]
                ]
        )
        |> ReactHelpers.Mount (JS.Document.GetElementById "main")
