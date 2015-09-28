namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript

open WebSharper.React.Bindings

type Element =
    {
        TagName    : string
        Children   : Component list
        Events     : (string * (SyntheticEvent -> unit)) list
        Attributes : (string * obj) list
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
                    yield! this.Attributes
                ]
                
            if this.TagName.StartsWith "!" then
                React.CreateElement("span", properties, this.TagName.Substring 1)
            else
                React.CreateElement(this.TagName, properties, children)
    
    [<JavaScript>]
    static member Create (tagName : string) children =
        let (tagName, attributes) =
            if not (tagName.StartsWith "!") && tagName.Contains "." then
                if tagName.StartsWith "." then
                    ("div", [ "className", box (tagName.Substring(1).Replace('.', ' ')) ])
                else
                    let parts = tagName.Split '.'
                    
                    (parts.[0], [ "className", box (String.concat " " parts.[1 ..]) ])
            else
                (tagName, [])
        
        {
            TagName    = tagName
            Children   = children
            Events     = []
            Attributes = attributes
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

    let (-<) element attributes =
        { element with
            Attributes = List.map (fun (name, value) -> (name, box value)) attributes }

[<AutoOpen>]
[<JavaScript>]
module Tags =
    
    let Input = Element.Create "input" []

    let Ul = Element.Create "ul"
    let Li = Element.Create "li"
