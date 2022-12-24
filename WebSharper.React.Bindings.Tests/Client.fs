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
namespace WebSharper.React.Bindings.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.React.Bindings
open WebSharper.React.ReactDOM.Bindings

[<JavaScript>]
module Client =

    type SquareProps = { value: option<string>; onClick: unit -> unit }

    let Square props =
        React.CreateElement("button",
            New ["className" => "square"; "onClick" => props.onClick],
            As<React.Element> (defaultArg props.value "")
        )

    type BoardProps = { squares: option<string>[]; onClick: int -> unit }

    let Board =
        React.CreateClassArgs<BoardProps, unit>(
            Render = (fun this ->
                let renderSquare i =
                    Square {
                        value = this.Props.squares.[i]
                        onClick = fun () -> this.Props.onClick i
                    }
                React.CreateElement("div", null,
                    React.CreateElement("div", New ["className" => "board-row"],
                        renderSquare 0,
                        renderSquare 1,
                        renderSquare 2
                    ),
                    React.CreateElement("div", New ["className" => "board-row"],
                        renderSquare 3,
                        renderSquare 4,
                        renderSquare 5
                    ),
                    React.CreateElement("div", New ["className" => "board-row"],
                        renderSquare 6,
                        renderSquare 7,
                        renderSquare 8
                    )
                )
            )
        )
        |> React.CreateClass

    type HistoryItem = { squares: option<string>[] }

    type GameState =
        {
            history: HistoryItem[]
            stepNumber: int
            xIsNext: bool
        }

    let calculateWinner (squares: option<string>[]) =
        [|
            (0, 1, 2)
            (3, 4, 5)
            (6, 7, 8)
            (0, 3, 6)
            (1, 4, 7)
            (2, 5, 8)
            (0, 4, 8)
            (2, 4, 6)
        |]
        |> Array.tryPick (fun (a, b, c) ->
            if squares.[a].IsSome && squares.[a] = squares.[b] && squares.[a] = squares.[c] then
                squares.[a]
            else
                None
        )

    let Game =
        React.CreateClassArgs<unit, GameState>(
            GetInitialState = (fun this ->
                {
                    history = [| { squares = Array.create 9 None } |]
                    stepNumber = 0
                    xIsNext = true
                }
            ),
            Render = (fun this ->
                let handleClick i =
                    let history = this.State.history.[0..this.State.stepNumber]
                    let current = Array.last history
                    let squares = Array.copy current.squares
                    if (calculateWinner squares).IsSome || squares.[i].IsSome then
                        ()
                    else
                        squares.[i] <- Some (if this.State.xIsNext then "X" else "O")
                        this.SetState {
                            history = Array.append history [| { squares = squares } |]
                            stepNumber = history.Length
                            xIsNext = not this.State.xIsNext
                        }

                let jumpTo step =
                    this.SetState {
                        this.State with
                            stepNumber = step
                            xIsNext = (step % 2) = 0
                    }

                let history = this.State.history
                let current = history.[this.State.stepNumber]
                let winner = calculateWinner current.squares
                let moves =
                    history |> Array.mapi (fun move step ->
                        let desc =
                            if move = 0 then
                                "Go to game start"
                            else
                                "Go to move #" + string move
                        React.CreateElement("li",
                            New ["key" => move; "onClick" => fun () -> jumpTo move],
                            React.CreateElement("button", null, As<React.Element> desc)
                        )
                    )
                let status =
                    match winner with
                    | Some winner -> "Winner: " + winner
                    | None -> "Next player: " + (if this.State.xIsNext then "X" else "O")

                React.CreateElement("div", New ["className" => "game"],
                    React.CreateElement("div", New ["className" => "game-board"],
                        React.CreateElement(Board, { squares = current.squares; onClick = handleClick })
                    ),
                    React.CreateElement("div", New ["className" => "game-info"],
                        React.CreateElement("div", null, As<React.Element> status),
                        React.CreateElement("ol", null, moves)
                    )
                )
            )
        )
        |> React.CreateClass

    [<SPAEntryPoint>]
    let Main () =
        let e = React.CreateElement(Game, ())
        ReactDOM.Render(e, JS.Document.GetElementById "main") |> ignore
