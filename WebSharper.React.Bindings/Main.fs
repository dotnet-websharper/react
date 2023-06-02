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

    let ImportReactDomClient (ent: #CodeModel.Entity) =
        Import ent.Name "react-dom/client" ent

    let ImportReactDomServer (ent: #CodeModel.Entity) =
        Import ent.Name "react-dom/server" ent

    let ImportReactDom (ent: #CodeModel.Entity) =
        Import ent.Name "react-dom" ent

    let ImportReactSubType (ent: #CodeModel.Entity) =
        let name = ent.Name.Replace("React.", "")
        Import name "react" ent

    let ImportReact (ent: #CodeModel.Entity) =
        Import ent.Name "react" ent

    let ImportDefaultReact (ent: #CodeModel.Entity) =
        ImportDefault "react" ent

module Definition =

    module React =

        type Key = string

        let Children_ =
            Class "React.Children"

        let ErrorInfo =
            Class "React.ErrorInfo"
            |> Res.ImportReactSubType
            |+> Instance [
                "componentStack" =? T<string>
            ]

        // NEW

        let Element =
            Class "React.Element"

        let ElementType =
            Class "React.ElementType"

        let ElementTypeGen =
            Generic - fun props ->
                Class "React.ElementType"

        let ISSRContext =
            Generic - fun t ->
                Class "React.ISSRContext"
                |+> Instance [
                    "DefaultValue" =@ t
                ]

        // import React from "react"

        let ComponentT =
            Generic -- fun props state ->
            AbstractClass "React.Component"
            |> Res.ImportReactSubType
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
            |> Res.ImportReactSubType
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
            Generic - fun t ->
                Class "React.Ref"
                |+> Instance [
                    "current" =? t
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

        let Class = Class_

    let ReactRoot =
        Class "ReactRoot"
        |+> Instance [
            "render" => React.Element?render ^-> T<unit>
            "unmount" => T<unit> ^-> T<unit>
        ]

    let ReactDOM =
        Class "ReactDOM"
        |+> Static [
            "render" => React.Element * T<Dom.Element> * !?(T<unit> ^-> T<unit>) ^-> T<unit>
            |> ObsoleteWithMessage "In React 18, render was replaced with createRoot"
            |> Res.ImportReactDom
            "hydrate" => React.Element * T<Dom.Element> * !?(T<unit> ^-> T<unit>) ^-> T<unit>
            |> ObsoleteWithMessage "In React 18, hydrate was replaced with hydrateRoot"
            |> Res.ImportReactDom
            "unmountComponentAtNode" => T<Dom.Element> ^-> T<bool>
            |> ObsoleteWithMessage "In React 18, unmountComponentAtNode was replaced with root.unmount()"
            |> Res.ImportReactDom
            "createPortal" => React.Element * T<Dom.Element> * !?T<string> ^-> React.Element
            |> Res.ImportReactDom
            "flushSync" => (T<unit> ^-> T<unit>)?callback ^-> T<unit>
            |> Res.ImportReactDom
        ]

    let ReadableStreamOptions =
        Pattern.Config "ReadableStreamOptions"
            {
                Required = []
                Optional = [
                    "bootstrapScriptContent", T<string>
                    "bootstrapScripts", T<string []>
                    "bootstrapModules", T<string []>
                    "identifierPrefix", T<string>
                    "namespaceURI", T<string>
                    "nonce", T<string>
                    "onError", T<obj -> unit>
                    "progressiveChunkSize", T<int>
                    //"signal"
                ]
            }

    let ReactDOMServer =
        Class "ReactDOMServer"
        |+> Static [
            "renderToString" => React.Element ^-> T<string>
            |> Res.ImportReactDomServer
            "renderToStaticMarkup" => React.Element ^-> T<string>
            |> Res.ImportReactDomServer
            "renderToReadableStream" => React.Element * !?ReadableStreamOptions.Type ^-> T<WebSharper.JavaScript.ReadableStream>
        ]

    let RootOptions =
        Class "RootOptions"
        |+> Instance [
            "onRecoverableError" =@ T<obj -> unit>
            "identifierPrefix" =@ T<string>
        ]

    let CreateRootOptions =
        Class "CreateRootOptions"
        |=> Inherits RootOptions

    let HydrateRootOptions =
        Class "HydrateRootOptions"
        |=> Inherits RootOptions

    let ReactDOMClient =
        AbstractClass "ReactDomClient"
        |+> Static [
            "createRoot" => T<Dom.Element>?container * !?CreateRootOptions?options ^-> ReactRoot
            |> Res.ImportReactDomClient
            "hydrateRoot" => T<Dom.Element>?container * React.Element?element * !?HydrateRootOptions?options ^-> ReactRoot
            |> Res.ImportReactDomClient
        ]

    let React =
        Class "React"
        |> Res.ImportDefaultReact
        |=> Nested [
            React.Children
            React.Class
            React.Element
            React.ElementType
            React.ElementTypeGen
            React.ISSRContext
            React.ComponentT
            React.Consumer
            React.Context
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
                |> Res.ImportReact
            "createElement" => T<obj>?comp * T<obj>?props *+ React.Element ^-> React.Element |> Res.ImportReact
            "isValidElement" => T<obj> ^-> T<bool> |> Res.ImportReact
            Generic - fun t ->
                "createElement"
                    => React.Consumer.[t]
                    * T<obj>?props
                    * (t ^-> React.Element)
                    ^-> React.Element |> Res.ImportReact
            "version" =? T<string> |> Res.ImportReact
            Generic - fun t -> "createRef" => t ^-> React.Ref.[t] |> Res.ImportReact

            Generic - fun t -> "createContext" => t ^-> React.Context.[t] |> Res.ImportReact
            Generic -- fun props t -> "forwardRef" => (props * !?React.Ref.[t] ^-> React.Element)?render ^-> React.ElementTypeGen.[props] |> Res.ImportReact
            Generic - fun props -> "memo" => (props ^-> React.Element)?render * (props * props ^-> T<bool>)?areEqual ^-> React.ElementTypeGen.[props] |> Res.ImportReact
            "startTransition" => T<unit -> unit>?callback ^-> T<unit>
            |> Res.ImportReact

            //hooks
            Generic - fun state ->
                "useState" => state ^-> state * (state ^-> T<unit>)
                |> Res.ImportReact
            Generic - fun t ->
                "useContext" => React.Context.[t] ^-> t
                |> Res.ImportReact
            Generic - fun t ->
                "useCallback" => (t ^-> T<unit>) * !|T<obj> ^-> (t ^-> T<unit>)
                |> Res.ImportReact
            Generic - fun t ->
                "useDebugValue" => t * !? (t ^-> T<string>) ^-> T<unit>
                |> Res.ImportReact
            Generic - fun t ->
                "useDeferredValue" => t ^-> t
                |> Res.ImportReact
            "useEffect" => (T<unit> ^-> T<unit>) * !|T<obj> ^-> (T<unit> ^-> T<unit>)
            |> Res.ImportReact
            "useId" => T<unit> ^-> T<string>
            |> Res.ImportReact
            Generic -- fun t u ->
                "useImperativeHandle" => React.Ref.[t] * (T<unit> ^-> React.Ref.[u]) * !|T<obj> |> Res.ImportReact
            "useInsertionEffect" => (T<unit> ^-> T<unit>) * !|T<obj> ^-> (T<unit> ^-> T<unit>) |> Res.ImportReact
            "useLayoutEffect" => (T<unit> ^-> T<unit>) * !|T<obj> ^-> (T<unit> ^-> T<unit>) |> Res.ImportReact
            Generic - fun t ->
                "useMemo" => (T<unit> ^-> t) * !|T<obj> ^-> t |> Res.ImportReact
            Generic -- fun t u ->
                "useReducer" => (t * u ^-> t) * t * !?(t ^-> t) ^-> t * (u ^-> T<unit>) |> Res.ImportReact
            Generic - fun ref ->
                "useRef" => ref ^-> React.Ref.[ref]
                |> Res.ImportReact
            "useTransition" => T<unit> ^-> T<bool> * T<unit -> unit> |> Res.ImportReact
            Generic -- fun t u ->
                "useSyncExternalStore" => (u ^-> (T<unit> ^-> u)) * (T<unit> ^-> t) ^-> t
                |> WithComment "If your app is fully built with react, we recommend using React state instead"
                |> Res.ImportReact

            "Fragment" =? React.ElementTypeGen.[T<obj>]
            |> Res.ImportReact
            "Suspense" =? React.ElementTypeGen.[T<obj>]
            |> Res.ImportReact
            "Lazy" => T<unit> ^-> React.Element
            |> Res.ImportReact
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

    let IProp =
        Interface "IProp"

    let IHTMLProp =
        Interface "IHTMLProp"
        |=> Extends [IProp]

    let IFragmentProp =
        Interface "IFragmentProp"
        |=> Extends [IProp]

    let Assembly =
        Assembly [
            Namespace "WebSharper.React.ReactDOM.Bindings" [
                 ReactDOM
                 ReactDOMServer
                 ReactDOMClient
                 ReactRoot
                 RootOptions
                 CreateRootOptions
                 HydrateRootOptions
                 ReadableStreamOptions
            ]
            Namespace "WebSharper.React.Bindings" [
                 IProp
                 IHTMLProp
                 IFragmentProp
                 React
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
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member x.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
