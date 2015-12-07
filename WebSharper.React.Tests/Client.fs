namespace WebSharper.React.Tests

open WebSharper
open WebSharper.JavaScript

open WebSharper.React

[<Require(typeof<Bindings.Resources.React>)>]
[<JavaScript>]
module Client =
    
    type Action =
        | Home
        | Hello of string
    
    type Person = { Name : string }

#if ZAFIR
    [<SPAEntryPoint>]
    let Main() =
#else
    let Main =
#endif
        let routeMap =
            RouteMap.Create
            <| function
                | Home -> []
                | Hello name -> [ "hello"; name ]
            <| function
                | [] -> Home
                | [ "hello"; name ] -> Hello name
                | _ -> Home
            
        React.Router routeMap
        <| fun router ->
            match router.State with
            | Home ->
                React.Class { Name = "" }
                <| fun this ->
                    Element.Wrap [
                        Element.Create "input" []
                        |> OnChange (fun event ->
                            this.SetState { Name = event?target?value }
                        )

                        Element.Create "h3" [ Text ("Hello " + this.State.Name + "!") ]

                        Element.Create "button" [ Text "Greet" ]
                        |> OnClick (fun _ ->
                            router.SetState (Action.Hello this.State.Name)
                        )
                    ]
                :> Component
            | Hello name ->
                Element.Create "button" [ Text "Say Hello" ]
                |> OnClick (fun _ ->
                    JS.Alert ("Hello " + name + "!")
                )
                :> Component
        |> React.Mount Document.Body
        