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

        let Element =
            Class "React.Element"

        let ComponentT =
            Generic -- fun props state ->
            AbstractClass "React.Component"
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
                |> WithComment "Get the component's props.children."
                "setInitialState" => state ^-> T<unit>
                |> WithInline "$0.state = $1"
                |> WithComment "Call this in the constructor of a component to set its initial state."
                "setState" => state?newState * !?(T<unit> ^-> T<unit>)?callback ^-> T<unit>
                "setState" => (state * props ^-> state)?update * !?(T<unit> ^-> T<unit>)?callback ^-> T<unit>
                "render" => T<unit> ^-> Element
                |> Abstract
                "componentDidMount" => T<unit> ^-> T<unit>
                |> Virtual
                "componentDidUpdate" => props * state * !?T<obj> ^-> T<unit>
                |> Virtual
                "componentWillUnmount" => T<unit> ^-> T<unit>
                |> Virtual
                "shouldComponentUpdate" => props * state ^-> T<bool>
                |> Virtual
                "getDerivedStateFromProps" => props * state ^-> props
                |> Virtual
                "getSnapshotBeforeUpdate" => props * state ^-> T<obj>
                |> Virtual
                "componentDidCatch" => T<Error> * ErrorInfo ^-> T<unit>
                |> Virtual
                "forceUpdate" => (T<unit> ^-> T<unit>) ^-> T<unit>
            ]

        let Children =
            Children_
            |+> Static [
                Generic - fun t -> "map" => TSelf * (Element ^-> t) ^-> Type.ArrayOf t
                "forEach" => TSelf * (Element ^-> T<unit>) ^-> T<unit>
                "count" => TSelf ^-> T<int>
                "only" => TSelf ^-> Element
                "toArray" => TSelf ^-> Type.ArrayOf Element
            ]

        let Fragment =
            Class "React.Fragment"
            |=> Inherits Element

        let Ref =
            Class "React.Ref"
            |+> Instance [
                "current" =? Element
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
            |> Requires [Res.CreateReactClass]
            |+> Static [
                Constructor (ComponentT.[props, state] ^-> Element)?Render
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
            "render" => React.Element * T<Dom.Element> * !?(T<unit> ^-> T<unit>) ^-> React.Ref
            "hydrate" => React.Element * T<Dom.Element> * !?(T<unit> ^-> T<unit>) ^-> React.Ref
            "unmountComponentAsNode" => T<Dom.Element> ^-> T<bool>
            "findDOMNode" => React.Element ^-> T<Dom.Element>
            "createPortal" => React.Element * T<Dom.Element> ^-> React.Element
        ]

    let React =
        Class "React"
        |> Requires [Res.React]
        |=> Nested [
            React.Children
            React.Class
            React.Element
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
                => (T<string> + React.Class + (T<obj> ^-> React.Element))?``type``
                * T<obj>?props
                *+T<obj>
                ^-> React.Element
            Generic - fun t ->
                "createElement"
                    => React.Consumer.[t]
                    * T<obj>?props
                    * (t ^-> React.Element)
                    ^-> React.Element
            "cloneElement"
                => React.Element?element
                * T<obj>?props
                *+T<obj>
                ^-> React.Element
            Generic -- fun props state ->
                "createClass" => React.CreateClassArgs.[props, state] ^-> React.Class
                |> WithInline "$global.createReactClass($0)"
            "isValidElement" => T<obj> ^-> T<bool>
            "createRef" => T<unit> ^-> React.Ref
            Generic - fun props ->
                "forwardRef" => (props * React.Ref ^-> React.Element) ^-> React.Element
            "Fragment" =? React.Class
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

[<Sealed>]
type Extension() =
    interface IExtension with
        member x.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
