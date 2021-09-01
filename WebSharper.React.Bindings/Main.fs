// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Res =

    let React =
        Resource "React" "https://unpkg.com/react@17/umd/react.production.min.js"
        
    let ReactDOM =
        Resource "ReactDOM" "https://unpkg.com/react-dom@17/umd/react-dom.production.min.js"
        |> Requires [React]

    let ReactDOMServer =
        Resource "ReactDOMServer" "https://unpkg.com/react-dom@17.0.2/umd/react-dom-server.browser.production.min.js"
        |> Requires [ReactDOM]

    let ReactTestUtils =
        Resource "ReactTestUtils" "https://unpkg.com/react-dom@17.0.2/umd/react-dom-test-utils.production.min.js"
        |> Requires [ReactDOM]

    let TestRenderer =
        Resource "TestRenderer" "https://unpkg.com/react-test-renderer@17.0.2/umd/react-test-renderer.development.js"
        |> Requires [React]

    let CreateReactClass =
        Resource "CreateReactClass" "https://unpkg.com/create-react-class@15.7.0/create-react-class.min.js"
        |> Requires [React]

module Definition =

    module React =

        type Key = string

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
                |> Virtual
                "getDerivedStateFromError" => T<Error> ^-> T<obj>
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
            "unmountComponentAtNode" => T<Dom.Element> ^-> T<bool>
            "findDOMNode" => React.Element ^-> T<Dom.Element> |> Obsolete
            "createPortal" => React.Element * T<Dom.Element> ^-> React.Element
        ]

    let ReactDOMServer =
        Class "ReactDOMServer"
        |> Requires [Res.ReactDOMServer]
        |+> Static [
            "renderToString" => React.Element ^-> T<string>
            "renderToStaticMarkup" => React.Element ^-> T<string>
            "renderToNodeStream" => React.Element ^-> (T<obj> ^-> T<string>)
            |> WithWarning "Server-only. This API is not available in the browser."
            "renderToStaticNodeStream" => React.Element ^-> (T<obj> ^-> T<string>)
            |> WithWarning "Server-only. This API is not available in the browser."
        ]

    let ReactTestUtils =
        Class "ReactTestUtils"
        |> Requires [Res.ReactTestUtils]
        |+> Static [
            "act"=> T<unit> ^-> React.ComponentT ^-> T<unit>
            "mockComponent" => React.ComponentT * !? T<string> ^-> T<unit>
            "isElement" => React.Element ^-> T<bool>
            "isElementOfType" => React.Element ^-> T<obj> ^-> T<bool>
            "isDOMComponent" => T<obj> ^-> T<bool>
            "isCompositeComponent" => T<obj> ^-> T<bool>
            "isCompositeComponentWithType" => T<obj> ^-> T<obj> ^-> T<bool>
            "findAllInRenderedTree" => T<obj> ^-> (React.ComponentT ^-> T<bool>) ^-> !| React.ComponentT
            "scryRenderedDOMComponentsWithClass" => T<obj> ^-> T<string> ^-> !| T<Dom.Element>
            "findRenderedDOMComponentsWithClass" => T<obj> ^-> T<string> ^-> T<Dom.Element> + T<Error>
            "scryRenderedDOMComponentsWithTag" => T<obj> ^-> T<string> ^-> !| T<Dom.Element>
            "findRenderedDOMComponentsWithTag" => T<obj> ^-> T<string> ^-> T<Dom.Element> + T<Error>
            "scryRenderedDOMComponentsWithType" => T<obj> ^-> T<obj> ^-> !| T<Dom.Element>
            "findRenderedDOMComponentsWithType" => T<obj> ^-> T<obj> ^-> T<Dom.Element> + T<Error>
            "renderIntoDocument" => T<Dom.Element> ^-> T<unit>
            "Simulate" => T<string> ^-> React.Element ^-> !? T<obj> ^-> T<unit>
            |> WithInline "Simulate.$0($1, $2)"
        ]

    let rec TestRenderer =
        Class "TestRenderer"
        |> Requires [Res.TestRenderer]
        |+> Static [
            "create" => React.Element  ^-> TSelf
            "act" => T<unit> ^-> React.ComponentT ^-> T<unit>
            "toJSON" => T<unit> ^-> T<obj>
            "toTree" => T<unit> ^-> T<obj>
            "update" => React.Element ^-> T<unit>
            "unmount" => T<unit> ^-> T<unit>
            "getInstance" => T<unit> ^-> React.Element
            "root" => T<obj>
            "find" => (React.Element ^-> T<bool>) ^-> T<bool> + T<Error>
            "findByType" => T<obj> ^-> React.Element + T<Error>
            "findByProps" => T<obj> ^-> React.Element + T<Error>
            "findAll" => (React.Element ^-> T<bool>) ^-> (!| React.Element)
            "findAllByType" => T<obj> ^-> (!| React.Element)
            "findAllByProps" => T<obj> ^-> (!| React.Element)
            "instance" => React.ComponentT
            "type" => T<obj>
            "props" => T<obj>
            "parent" => React.ComponentT
            "children" => !| React.ComponentT
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
            Generic - fun t ->
                "createFactory"
                    => React.Consumer.[t]
                        * T<obj>?props
                        * (t ^-> React.Element)
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
            "Lazy" => T<unit> ^-> React.Element

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
            "target" =? T<Dom.EventTarget>
            "timeStamp" =? T<int>
            "type" =? T<string>
        ]

    let ClipboardEvent =
        Class "ClipboardEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "clipboardData" =? T<obj> //DOMDataTransfer
        ]

    let CompositionEvent =
        Class "CompositionEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "data" =? T<string>
        ]

    let KeyboardEvent =
        Class "KeyboardEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "altKey" =? T<bool>
            "charCode" =? T<int>
            "ctrlKey" =? T<bool>
            "getModifierState" => T<React.Key> ^-> T<bool>
            "key" =? T<string>
            "keyCode" =? T<int>
            "locale" =? T<string>
            "location" =? T<int>
            "metaKey" =? T<bool>
            "repeat" =? T<bool>
            "shiftKey" =? T<bool>
            "which" =? T<int>
        ]

    let FocusEvent =
        Class "FocusEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "relatedTarget" =? T<Dom.EventTarget>
        ]

    let FormEvent =
        Class "FormEvent"
        |=> Inherits SyntheticEvent
        |+> Instance []

    let GenericEvent =
        Class "GenericEvent"
        |=> Inherits SyntheticEvent
        |+> Instance []

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
            "getModifierState" => T<React.Key> ^-> T<bool>
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
            "tangentialPressure" =? T<int>
            "tiltX" =? T<int>
            "tiltY" =? T<int>
            "twist" =? T<int>
            "pointerType" =? T<string>
            "isPrimary" =? T<bool>
        ]

    let SelectionEvent =
        Class "SelectionEvent"
        |=> Inherits SyntheticEvent
        |+> Instance []

    let TouchEvent =
        Class "TouchEvent"
        |=> Inherits SyntheticEvent
        |+> Instance [
            "altKey" =? T<bool>
            "changedTouches" =? T<obj> // DOMTouchList
            "ctrlKey" =? T<bool>
            "getModifierState" => T<React.Key> ^-> T<bool>
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

    let MediaEvent =
        Class "MediaEvent"
        |=> Inherits SyntheticEvent
        |+> Instance []

    let ImageEvent =
        Class "ImageEvent"
        |=> Inherits SyntheticEvent
        |+> Instance []

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
                 ReactDOMServer
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
                 FormEvent
                 GenericEvent
                 SelectionEvent
                 MediaEvent
                 ImageEvent
            ]
            Namespace "WebSharper.React.Bindings.Resources" [
                Res.React
                Res.ReactDOM
                Res.ReactDOMServer
                Res.ReactTestUtils
                Res.TestRenderer
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
