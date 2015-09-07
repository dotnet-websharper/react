namespace WebSharper.React.Obsolete

open System.Collections.Generic

open WebSharper
open WebSharper.JavaScript

open WebSharper.React.Bindings

[<AutoOpen>]
[<JavaScript>]
module Element =
    
    type GenericEvent =
        abstract member ToString : unit -> string

    type Event =
        | Click  of (SyntheticEvent -> unit)
        | Change of (SyntheticEvent -> unit)
        
        interface GenericEvent with
            override this.ToString () =
                match this with
                | Click  _ -> "onClick"
                | Change _ -> "onChange"

    [<Inline "$0.$0">]
    let private extractCallback _ = X<SyntheticEvent -> unit>

    [<AbstractClass>]
    type GenericElement(?children) =
        abstract member Type : Choice<string, ReactClass>

        member val Children   = default' children []
        member val Properties = List()

        member this.AddEvent (event : GenericEvent) =
            this.Properties.Add(string event, box (extractCallback event))

        member this.AddProperty (name, value : 'T) =
            this.Properties.Add (name, box value)

        static member Transpile (element : GenericElement) =
            let children =
                element.Children
                |> List.map GenericElement.Transpile
                |> List.toArray
        
            let properties =
                New element.Properties

            match element.Type with
            | Choice1Of2 tagName ->
                if tagName.StartsWith "!" then
                    As<ReactElement> (tagName.Substring 1)
                else
                    React.CreateElement(tagName, properties, children)
            | Choice2Of2 class' ->
                React.CreateElement(class', properties, children)
                
    type Element(tagName, ?children) =
        inherit GenericElement(?children = children) with
            override this.Type = Choice1Of2 tagName

    let inline Text x = Element ("!" + x)

    let (|>!) (element : #GenericElement) event =
        element.AddEvent event
        element

    let inline Wrap children =
        Element("div", children)

    let (-<) (element : #GenericElement) (properties : (string * 'T) list) =
        properties
        |> List.iter (fun prop ->
            element.AddProperty prop
        )

        element
