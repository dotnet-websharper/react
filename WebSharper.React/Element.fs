namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript

open WebSharper.React.Bindings

type Element =
    {
        TagName  : string
        Children : Component list
        Events   : (string * (SyntheticEvent -> unit)) list
    }

    interface Component with
        [<JavaScript>]
        member this.Map () =
            let children =
                this.Children
                |> List.map (fun child -> child.Map())
                |> List.toArray

            let properties =
                New [
                    yield! this.Events
                        |> List.map (fun (event, callback) ->
                            (event, box callback)
                        )
                ]
                
            if this.TagName.StartsWith "!" then
                React.CreateElement("span", null, this.TagName.Substring 1)
            else
                React.CreateElement(this.TagName, properties, children)
    
    [<JavaScript>]
    static member Create tagName children =
        {
            TagName  = tagName
            Children = children
            Events   = []
        }

    [<JavaScript>]
    static member inline Wrap = Element.Create "div"

[<AutoOpen>]
[<JavaScript>]
module ElementEvents =
    
    let private Event type' callback element : Element =
        { element with
            Events = (type', callback) :: element.Events }

    let OnClick  = Event "onClick"
    let OnChange = Event "onChange"

[<AutoOpen>]
[<JavaScript>]
module Operators =
    
    let Text payload = Element.Create ("!" + payload) []

    module Document =
        
        [<Inline "document.body">]
        let Body = X<Dom.Document>
