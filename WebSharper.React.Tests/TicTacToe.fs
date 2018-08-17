namespace WebSharper.React.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.React.Html

[<JavaScript>]
module TicTacToe =

    type SquareProps = { value: option<string>; onClick: unit -> unit }

    let Square props =
        button [
            attr.className "square"
            on.click (fun _ -> props.onClick())
        ] [text (defaultArg props.value "")]

    type BoardProps = { squares: option<string>[]; onClick: int -> unit }

    type Board(props) =
        inherit ComponentClass<BoardProps, unit>(props)

        override this.Render() =
            let renderSquare i =
                Square {
                    value = this.Props.squares.[i]
                    onClick = fun () -> this.Props.onClick i
                }
            div [] [
                div [attr.className "board-row"] [
                    renderSquare 0
                    renderSquare 1
                    renderSquare 2
                ]
                div [attr.className "board-row"] [
                    renderSquare 3
                    renderSquare 4
                    renderSquare 5
                ]
                div [attr.className "board-row"] [
                    renderSquare 6
                    renderSquare 7
                    renderSquare 8
                ]
            ]

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

    type Game(props) as this =
        inherit ComponentClass<unit, GameState>(props)

        do this.SetInitialState {
                history = [| { squares = Array.create 9 None } |]
                stepNumber = 0
                xIsNext = true
            }

        member this.handleClick i =
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

        member this.jumpTo step =
            this.SetState {
                this.State with
                    stepNumber = step
                    xIsNext = (step % 2) = 0
            }

        [<JavaScriptExport>]
        override this.Render() =
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
                    li [
                        attr.key move
                        on.click (fun _ -> this.jumpTo move)
                    ] [button [] [text desc]]
                )
            let status =
                match winner with
                | Some winner -> "Winner: " + winner
                | None -> "Next player: " + (if this.State.xIsNext then "X" else "O")

            div [attr.className "game"] [
                div [attr.className "game-board"] [
                    React.Make Board { squares = current.squares; onClick = this.handleClick }
                ]
                div [attr.className "game-info"] [
                    div [] [text status]
                    ol [] moves
                ]
            ]
