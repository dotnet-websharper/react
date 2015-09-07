namespace WebSharper.React.Tests

open WebSharper
open WebSharper.JavaScript

open WebSharper.React

[<Require(typeof<WebSharper.React.Bindings.Resources.React>)>]
[<JavaScript>]
module Client =
    
    type Action =
        | Home
        | Contact

    module Document =
        
        [<Inline "document.body">]
        let Body = X<Dom.Element>

    let Main =
        let routeMap =
            RouteMap.Create
            <| function
                | Home    -> "/"
                | Contact -> "/contact"
            <| function
                | "/"        -> Home
                | "/contact" -> Contact
                | _          -> Home

        let page (router : Router<_>) (title, action) : Element<Action> =
            Element.Wrap [
                Element.Wrap [
                    Element("strong", 
                        [ Text "Current URL: " ])
                    Text (routeMap.Serializer router.State)
                ]

                Element("button", 
                    [ Text title ])
                |>! OnClick (fun _ ->
                    router.SetState action
                )
            ]

        React.Router routeMap
        <| fun router ->
            match router.State with
            | Home ->
                page router ("Contact Me", Contact)
            | Contact ->
                page router ("Home", Home)
        |> React.Mount Document.Body
        