namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =
    
    let ReactClass =
        Interface "ReactClass"

    let ReactElement =
        Interface "ReactElement"

    let DOMNode     = T<Dom.Node>
    let DOMEventTarget = T<Dom.EventTarget>

    let ReactComponent =
        Class "ReactComponent"
        |+> Instance [
            "setState"    => ((T<obj> * !? T<obj> ^-> T<obj>) + T<obj>) * !? T<unit -> unit> ^-> T<unit>
            "forceUpdate" => !? T<unit -> unit> ^-> T<unit>
            "setProps"    => T<obj> * !? T<unit -> unit> ^-> T<unit>
        ]

    let PropTypes =
        Class "React.PropTypes"
            |+> Static [
                "array"   =? TSelf
                "bool"    =? TSelf
                "func"    =? TSelf
                "number"  =? TSelf
                "object"  =? TSelf
                "string"  =? TSelf
                "node"    =? TSelf
                "element" =? TSelf
                "any"     =? TSelf

                "isRequired" =? TSelf

                "instanceOf" => T<obj> ^-> TSelf
                
                Generic - fun U ->
                    "oneOf" => Type.ArrayOf U ^-> TSelf
                
                "oneOfType" => Type.ArrayOf TSelf ^-> TSelf
                "arrayOf"   => Type.ArrayOf TSelf ^-> TSelf
                "objOf"     => T<obj> ^-> TSelf
                "shape"     => T<obj> ^-> TSelf
            ]

    let Children =
        Class "React.Children"
            |+> Static [
                "map"     => T<obj> * T<obj -> obj> * !? T<obj> ^-> T<obj>
                "forEach" => T<obj> * T<obj -> unit> * !? T<obj> ^-> T<unit>
                "count"   => T<obj> ^-> T<int>
                "only"    => T<obj> ^-> T<obj>
            ]

    let SyntheticEvent =
        Class "SyntheticEvent"
        |+> Instance [
            "bubbles"          =? T<bool>
            "cancelable"       =? T<bool>
            "currentTarget"    =? DOMEventTarget
            "defaultPrevented" =? T<bool>
            "eventPhase"       =? T<JavaScript.Dom.PhaseType>
            "isTrusted"        =? T<bool>
            "nativeEvent"      =? T<JavaScript.Dom.Event>
            "target"           =? DOMEventTarget
            "timeStamp"        =? T<int>
            "type"             =? T<string>

            "preventDefault"  => T<unit> ^-> T<unit>
            "stopPropagation" => T<unit> ^-> T<unit>
        ]

    let React =
        Class "React"
        |+> Static [
            "createClass" => T<obj> ^-> ReactClass
            
            "createElement" => (T<string> + ReactClass) * !? T<obj> ^-> ReactElement
            "createElement" => (T<string> + ReactClass) * T<obj> *+ (T<string> + ReactClass + ReactElement) ^-> ReactElement
            
            "cloneElement" => ReactElement * !? T<obj> ^-> ReactElement
            "cloneElement" => ReactElement * T<obj> *+ (T<string> + ReactClass + ReactElement) ^-> ReactElement

            "createFactory" => (T<string> + ReactClass) ^-> (T<obj> * (T<string> + ReactClass + ReactElement) ^-> ReactElement)

            "render" => ReactElement * DOMNode * !? T<unit -> unit> ^-> ReactComponent

            "unmountComponentAtNode" => DOMNode ^-> T<bool>

            "renderToString" => ReactElement ^-> T<string>

            "renderToStaticMarkup" => ReactElement ^-> T<string>

            "isValidElement" => T<obj> ^-> T<bool>

            "findDOMNode" => ReactComponent ^-> DOMNode

            "initializeTouchEvents" => T<bool> ^-> T<unit>
        ]
        |=> Nested [
            PropTypes
            Children
        ]

    let Assembly =
        Assembly [
            Namespace "WebSharper.React.Bindings" [
                 ReactClass
                 ReactElement
                 ReactComponent
                 SyntheticEvent
                 React
            ]
            Namespace "WebSharper.React.Bindings.Resources" [
                Resource "React" "https://fb.me/react-0.13.3.min.js"
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member x.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
