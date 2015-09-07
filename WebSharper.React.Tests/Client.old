namespace WebSharper.React.Tests

open WebSharper
open WebSharper.JavaScript

open WebSharper.React.Bindings
open WebSharper.React

[<Require(typeof<Resources.React>)>]
[<JavaScript>]
module Client =
    
    type User =
        {
            Name : string
        }

    module Document =
        
        [<Inline "document.body">]
        let Body = X<Dom.Element>

    let Main =
        React.Class { Name = "" }
        <| fun this ->
            Element.Wrap [
                Element "input" -< [
                    "placeholder", "Your name"
                ]
                |>! Change (fun event ->
                    this.SetState {
                        Name = event.Target?value
                    }
                )

                Element("h3", 
                    [
                        Text (if this.State.Name = "" then "" else "Hello " + this.State.Name + "!")
                    ])
            ]
        |> React.Mount Document.Body
