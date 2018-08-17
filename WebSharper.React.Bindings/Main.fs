namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Res =

    let React =
        Resource "React" "https://unpkg.com/react@16/umd/react.production.min.js"
        
    let ReactDOM =
        Resource "ReactDOM" "https://unpkg.com/react-dom@16/umd/react-dom.production.min.js"
        |> Requires [React]

    let CreateReactClass =
        Resource "CreateReactClass" "https://unpkg.com/create-react-class@15.6.3/create-react-class.min.js"
        |> Requires [React]

module Definition =

    module React =

        let Children_ =
            Class "React.Children"

        let ErrorInfo =
            Class "React.ErrorInfo"
            |+> Instance [
                "componentStack" =? T<string>
            ]

        let Component =
            Class "React.Component"

        let ComponentT =
            Generic -- fun props state ->
            Class "React.Component"
            |=> Inherits Component
            |+> Static [
                Constructor props
                "defaultProps" =? props
                "displayName" =? T<string>
            ]
            |+> Instance [
                "props" =? props
                "state" =? state
                "children" =? Children_
                |> WithGetterInline "$this.props.children"
                "setState" => state?newState * !?(T<unit> ^-> T<unit>)?callback ^-> T<unit>
                "setState" => (state * props ^-> state)?update * !?(T<unit> ^-> T<unit>)?callback ^-> T<unit>
                "render" => T<unit> ^-> Component
                "componentDidMount" => T<unit> ^-> T<unit>
                "componentDidUpdate" => props * state * !?T<obj> ^-> T<unit>
                "componentWillUnmount" => T<unit> ^-> T<unit>
                "shouldComponentUpdate" => props * state ^-> T<bool>
                "getDerivedStateFromProps" => props * state ^-> props
                "getSnapshotBeforeUpdate" => props * state ^-> T<obj>
                "componentDidCatch" => T<Error> * ErrorInfo ^-> T<unit>
                "forceUpdate" => (T<unit> ^-> T<unit>) ^-> T<unit>
            ]

        let Children =
            Children_
            |+> Static [
                Generic - fun t -> "map" => TSelf * (Component ^-> t) ^-> Type.ArrayOf t
                "forEach" => TSelf * (Component ^-> T<unit>) ^-> T<unit>
                "count" => TSelf ^-> T<int>
                "only" => TSelf ^-> Component
                "toArray" => TSelf ^-> Type.ArrayOf Component
            ]

        let Fragment =
            Class "React.Fragment"
            |=> Inherits Component

        let Ref =
            Class "React.Ref"
            |+> Instance [
                "current" =? Component
            ]

        let Class_ =
            Class "React.Class"

        let Consumer =
            Generic - fun t ->
            Class "React.Consumer"
            |=> Inherits Class_

        let Context =
            Generic - fun t ->
            Class "React.Context"
            |+> Instance [
                "Consumer" =? Consumer.[t]
                "Provider" =? Class_
            ]

        let CreateClassArgs =
            Generic -- fun props state ->
            Class "React.CreateClassArgs"
            |+> Static [
                Constructor (ComponentT.[props, state] ^-> Component)?Render
                |> WithInline "{render:$wsruntime.CreateFuncWithOnlyThis($Render)}"
            ]
            |+> Instance [
                "getDefaultProps" =! T<unit> ^-> props
                "getInitialState" =! ComponentT.[props, state]?this ^-> state
                |> WithSetterInline "$0.getInitialState = $wsruntime.CreateFuncWithOnlyThis($1)"
            ]

        let Class = Class_

    let ReactDOM =
        Class "ReactDOM"
        |> Requires [Res.ReactDOM]
        |+> Static [
            "render" => React.Component * T<Dom.Element> * !?(T<unit> ^-> T<unit>) ^-> React.Ref
            "hydrate" => React.Component * T<Dom.Element> * !?(T<unit> ^-> T<unit>) ^-> React.Ref
            "unmountComponentAsNode" => T<Dom.Element> ^-> T<bool>
            "findDOMNode" => React.Component ^-> T<Dom.Element>
            "createPortal" => React.Component * T<Dom.Element> ^-> React.Component
        ]

    let React =
        Class "React"
        |> Requires [Res.React; Res.CreateReactClass]
        |=> Nested [
            React.Children
            React.Class
            React.Component
            React.ComponentT
            React.Consumer
            React.Context
            React.CreateClassArgs
            React.ErrorInfo
            React.Fragment
            React.Ref
        ]
        |+> Static [
            "createElement"
                => (T<string> + React.Class + (T<obj> ^-> React.Component))?``type``
                * T<obj>?props
                *+T<obj>
                ^-> React.Component
            Generic - fun t ->
                "createElement"
                    => React.Consumer.[t]
                    * T<obj>?props
                    * (t ^-> React.Component)
                    ^-> React.Component
            "cloneElement"
                => React.Component?element
                * T<obj>?props
                *+T<obj>
                ^-> React.Component
            Generic -- fun props state ->
                "createClass" => React.CreateClassArgs.[props, state] ^-> React.Class
                |> WithInline "$global.createReactClass($0)"
            "isValidElement" => T<obj> ^-> T<bool>
            "createRef" => T<unit> ^-> React.Ref
            Generic - fun props ->
                "forwardRef" => (props * React.Ref ^-> React.Component) ^-> React.Component
            "fragment" =? React.Class
            Generic - fun t ->
                "createContext" => t ^-> React.Context.[t]
        ]

    let SyntheticEvent =
        Class "SyntheticEvent"
        |+> Instance [
            "bubbles" =? T<bool>
            "cancelable" =? T<bool>
            "currentTarget" =? T<Dom.EventTarget>
            "defaultPrevented" =? T<bool>
            "eventPhase" =? T<int>
            "isTrusted" =? T<bool>
            "nativeEvent" =? T<Dom.Event>
            "prevetDefault" => T<unit> ^-> T<unit>
            "isDefaultPrevented" => T<unit> ^-> T<bool>
            "stopPropagation" => T<unit> ^-> T<unit>
            "isPropagationStopped" => T<unit> ^-> T<bool>
            "target" => T<Dom.EventTarget>
            "timeStamp" => T<int>
            "type" => T<string>
        ]

    let ClipboardEvent =
        Class "ClipboardEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "clipboardData" => T<obj>
        ]

    let CompositionEvent =
        Class "CompositionEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "data" => T<string>
        ]

    let FocusEvent =
        Class "FocusEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "relatedTarget" =? T<Dom.EventTarget>
        ]

    let KeyboardEvent =
        Class "KeyboardEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "altKey" =? T<bool>
            "charCode" =? T<int>
            "ctrlKey" =? T<bool>
            "getModifierState" => T<int> ^-> T<bool>
            "key" =? T<string>
            "keyCode" =? T<int>
            "locale" =? T<string>
            "location" =? T<int>
            "metaKey" =? T<bool>
            "repeat" =? T<bool>
            "shiftKey" =? T<bool>
            "which" =? T<int>
        ]

    let MouseEvent =
        Class "MouseEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "altKey" =? T<bool>
            "button" =? T<int>
            "buttons" =? T<int>
            "clientX" =? T<int>
            "clientY" =? T<int>
            "ctrlKey" =? T<bool>
            "getModifierState" => T<int> ^-> T<bool>
            "metaKey" =? T<bool>
            "pageX" =? T<int>
            "pageY" =? T<int>
            "relatedTarget" =? T<Dom.EventTarget>
            "screenX" =? T<int>
            "screenY" =? T<int>
            "shiftKey" =? T<bool>
        ]

    let PointerEvent =
        Class "PointerEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "pointerId" =? T<int>
            "width" =? T<int>
            "height" =? T<int>
            "pressure" =? T<int>
            "tilltX" =? T<int>
            "tiltY" =? T<int>
            "pointerType" =? T<string>
            "isPrimary" =? T<bool>
        ]

    let TouchEvent =
        Class "TouchEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "altKey" =? T<bool>
            "changedTouches" =? T<obj> // DOMTouchList
            "ctrlKey" =? T<bool>
            "getModifierState" => T<int> ^-> T<bool>
            "metaKey" =? T<bool>
            "shiftKey" =? T<bool>
            "targetTouches" =? T<obj> // DOMTouchList
            "touches" =? T<obj> // DOMTouchList
        ]

    let UIEvent =
        Class "UIEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "detail" =? T<int>
            "view" =? T<Dom.AbstractView>
        ]

    let WheelEvent =
        Class "WheelEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "deltaMode" =? T<int>
            "deltaX" =? T<int>
            "deltaY" =? T<int>
            "deltaZ" =? T<int>
        ]

    let AnimationEvent =
        Class "AnimationEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "animationName" =? T<string>
            "pseudoElement" =? T<string>
            "elapsedTime" =? T<float>
        ]

    let TransitionEvent =
        Class "TransitionEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "propertyName" =? T<string>
            "pseudoElement" =? T<string>
            "elapsedTime" =? T<float>
        ]

    let Assembly =
        Assembly [
            Namespace "WebSharper.React.Bindings" [
                 React
                 ReactDOM
                 SyntheticEvent
                 ClipboardEvent
                 CompositionEvent
                 FocusEvent
                 KeyboardEvent
                 MouseEvent
                 PointerEvent
                 TouchEvent
                 UIEvent
                 WheelEvent
                 AnimationEvent
                 TransitionEvent
            ]
            Namespace "WebSharper.React.Bindings.Resources" [
                Res.React
                Res.ReactDOM
                Res.CreateReactClass
            ]
        ]

    // TODO: Context




// module OldDefinition =
    
//     let ReactClass =
//         Interface "ReactClass"

//     let ReactElement =
//         Interface "ReactElement"

//     let DOMNode     = T<Dom.Node>
//     let DOMEventTarget = T<Dom.EventTarget>

//     let ReactComponent =
//         Class "ReactComponent"
//         |+> Instance [
//             "setState"    => ((T<obj> * !? T<obj> ^-> T<obj>) + T<obj>) * !? T<unit -> unit> ^-> T<unit>
//             "forceUpdate" => !? T<unit -> unit> ^-> T<unit>
//             "setProps"    => T<obj> * !? T<unit -> unit> ^-> T<unit>
//             "props"       =? T<obj>
//             "state"       =? T<obj>
//         ]

//     let ReactComponentT =
//         Generic -- fun props state ->
//         Class "ReactComponent"
//         |=> Inherits ReactComponent
//         |+> Instance [
//             "setState" => state * !? T<unit -> unit> ^-> T<unit>
//             "props" =? props
//             "state" =? state
//         ]

//     let PropTypes =
//         Class "React.PropTypes"
//             |+> Static [
//                 "array"   =? TSelf
//                 "bool"    =? TSelf
//                 "func"    =? TSelf
//                 "number"  =? TSelf
//                 "object"  =? TSelf
//                 "string"  =? TSelf
//                 "node"    =? TSelf
//                 "element" =? TSelf
//                 "any"     =? TSelf

//                 "isRequired" =? TSelf

//                 "instanceOf" => T<obj> ^-> TSelf
                
//                 Generic - fun U ->
//                     "oneOf" => Type.ArrayOf U ^-> TSelf
                
//                 "oneOfType" => Type.ArrayOf TSelf ^-> TSelf
//                 "arrayOf"   => Type.ArrayOf TSelf ^-> TSelf
//                 "objOf"     => T<obj> ^-> TSelf
//                 "shape"     => T<obj> ^-> TSelf
//             ]

//     let Children =
//         Class "React.Children"
//             |+> Static [
//                 "map"     => T<obj> * T<obj -> obj> * !? T<obj> ^-> T<obj>
//                 "forEach" => T<obj> * T<obj -> unit> * !? T<obj> ^-> T<unit>
//                 "count"   => T<obj> ^-> T<int>
//                 "only"    => T<obj> ^-> T<obj>
//             ]

//     let SyntheticEvent =
//         Class "SyntheticEvent"
//         |+> Instance [
//             "bubbles"          =? T<bool>
//             "cancelable"       =? T<bool>
//             "currentTarget"    =? DOMEventTarget
//             "defaultPrevented" =? T<bool>
//             "eventPhase"       =? T<JavaScript.Dom.PhaseType>
//             "isTrusted"        =? T<bool>
//             "nativeEvent"      =? T<JavaScript.Dom.Event>
//             "target"           =? DOMEventTarget
//             "timeStamp"        =? T<int>
//             "type"             =? T<string>

//             "preventDefault"  => T<unit> ^-> T<unit>
//             "stopPropagation" => T<unit> ^-> T<unit>
//         ]

//     let React =
//         Class "React"
//         |+> Static [
//             "createClass" => T<obj> ^-> ReactClass
            
//             "createElement" => (T<string> + ReactClass) * !? T<obj> ^-> ReactElement
//             "createElement" => (T<string> + ReactClass) * T<obj> *+ (T<string> + ReactClass + ReactElement) ^-> ReactElement
            
//             "cloneElement" => ReactElement * !? T<obj> ^-> ReactElement
//             "cloneElement" => ReactElement * T<obj> *+ (T<string> + ReactClass + ReactElement) ^-> ReactElement

//             "createFactory" => (T<string> + ReactClass) ^-> (T<obj> * (T<string> + ReactClass + ReactElement) ^-> ReactElement)

//             "render" => ReactElement * DOMNode * !? T<unit -> unit> ^-> ReactComponent

//             "unmountComponentAtNode" => DOMNode ^-> T<bool>

//             "renderToString" => ReactElement ^-> T<string>

//             "renderToStaticMarkup" => ReactElement ^-> T<string>

//             "isValidElement" => T<obj> ^-> T<bool>

//             "findDOMNode" => ReactComponent ^-> DOMNode

//             "initializeTouchEvents" => T<bool> ^-> T<unit>
//         ]
//         |=> Nested [
//             PropTypes
//             Children
//         ]

//     let Assembly =
//         Assembly [
//             Namespace "WebSharper.React.Bindings" [
//                  ReactClass
//                  ReactElement
//                  ReactComponent
//                  ReactComponentT
//                  SyntheticEvent
//                  React
//             ]
//             Namespace "WebSharper.React.Bindings.Resources" [
//                 Resource "React" "https://fb.me/react-0.13.3.min.js"
//                 |> AssemblyWide
//             ]
//         ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member x.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
