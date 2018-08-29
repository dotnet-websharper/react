﻿namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript
open WebSharper.Sitelets
open WebSharper.Sitelets.InferRouter
open WebSharper.React.Bindings
type private R = WebSharper.React.Bindings.React
type private SRouter<'Endpoint when 'Endpoint : equality> = WebSharper.Sitelets.Router<'Endpoint>
module SRouter = WebSharper.Sitelets.Router
type React = R

[<JavaScript false>]
module Macros =
    open WebSharper.Core
    open WebSharper.Core.AST
    module M = WebSharper.Core.Metadata
    module I = WebSharper.Core.AST.IgnoreSourcePos

    type private Marker = class end

    [<Literal>]
    let private staticFlags =
        System.Reflection.BindingFlags.Static |||
        System.Reflection.BindingFlags.Public |||
        System.Reflection.BindingFlags.NonPublic

    let tPervasives =
        TypeDefinition { Assembly = "WebSharper.Main"
                         FullName = "WebSharper.JavaScript.Pervasives" }
    let tSeq =
        TypeDefinition { Assembly = "netstandard"
                         FullName = "System.Collections.Generic.IEnumerable`1" }
    let tReactModule =
        TypeDefinition { Assembly = "WebSharper.React"
                         FullName = "WebSharper.React.ReactModule" }
    let tReact =
        TypeDefinition { Assembly = "WebSharper.React.Bindings"
                         FullName = "WebSharper.React.Bindings.React" }
    let tElement =
        TypeDefinition { Assembly = "WebSharper.React.Bindings"
                         FullName = "WebSharper.React.Bindings.React+Element" }
    let tHtml =
        TypeDefinition { Assembly = "WebSharper.React"
                         FullName = "WebSharper.React.Html" }
    let tTags =
        TypeDefinition { Assembly = "WebSharper.React"
                         FullName = "WebSharper.React.Html+Tags" }
    let mAs =
        Method { MethodName = "As"
                 Generics = 1
                 Parameters = [NonGenericType Definitions.Object]
                 ReturnType = TypeParameter 0 }
    let mNew =
        Method { MethodName = "New"
                 Generics = 1
                 Parameters = [
                        GenericType tSeq [
                            TupleType ([
                                NonGenericType Definitions.String
                                NonGenericType Definitions.Object
                            ], false)
                        ]
                    ]
                 ReturnType = TypeParameter 0 }
    let mElt =
        Method { MethodName = "elt"
                 Generics = 0
                 Parameters = [
                        NonGenericType Definitions.String
                        NonGenericType Definitions.Object
                        GenericType tSeq [NonGenericType tElement]
                    ]
                 ReturnType = NonGenericType tElement }
    let mCreateElement =
        Method { MethodName = "CreateElement"
                 Generics = 0
                 Parameters = [
                        NonGenericType Definitions.String
                        NonGenericType Definitions.Object
                        ArrayType(NonGenericType Definitions.Object, 1)
                    ]
                 ReturnType = NonGenericType tElement }

    let callElt elt props children =
        match children with
        | I.NewArray _ ->
            Call(None, NonGeneric tReact, NonGeneric mCreateElement, [elt; props; children])
        | _ ->
            Call(None, NonGeneric tReactModule, NonGeneric mElt, [elt; props; children])
    let callNew arg = Call(None, NonGeneric tPervasives, NonGeneric mNew, [arg])
    let callNewOrNull = function
        | I.NewArray [] -> !~Null
        | arg -> callNew arg

    type Make() =
        inherit Macro()

        abstract Fallback : MacroCall -> MacroResult
        default this.Fallback(call) = MacroFallback

        override this.TranslateCall(call) =
            let ix =
                match call.Parameter with
                | Some (:? int as i) -> i
                | _ -> 0

            let callWithReplacedArg (arg: Expression) =
                let args = call.Arguments |> List.mapi (fun i e -> if i = ix then arg else e)
                Call(call.This, call.DefiningType, call.Method, args)

            let dotnetCtor (ty: Concrete<TypeDefinition>) (ctor: Constructor) =
                match call.Compilation.GetClassInfo(ty.Entity) with
                | Some x ->
                    match x.Constructors.[ctor] with
                    | M.Constructor addr ->
                        let call = callWithReplacedArg (GlobalAccess addr)
                        MacroDependencies ([M.ConstructorNode(ty.Entity, ctor)], MacroOk call)
                    | _ -> this.Fallback call
                | None -> this.Fallback call

            let jsCtor (ctor: Expression) =
                let call = callWithReplacedArg ctor
                MacroOk call

            match call.Arguments.[ix] with
            | I.Call(None, ty, meth, [comp]) when ty.Entity = tPervasives && meth.Entity = mAs ->
                callWithReplacedArg comp |> MacroOk
            | I.Function ([], I.Return (I.Ctor(ty, ctor, []))) ->
                dotnetCtor ty ctor
            | I.Function ([props], I.Return (I.Ctor(ty, ctor, [I.Var props']))) when props = props' ->
                dotnetCtor ty ctor
            | I.Function ([], I.Return (I.New (ctor, []))) ->
                jsCtor ctor
            | I.Function ([props], I.Return (I.New (ctor, [I.Var props']))) when props = props' ->
                jsCtor ctor
            | _ ->
                this.Fallback call

    type Element() =
        inherit Make()

        override this.Fallback(call) =
            callElt
                call.Arguments.[0]
                (callNewOrNull call.Arguments.[1])
                call.Arguments.[2]
            |> MacroOk

    type Html() =
        inherit Macro()

        override this.TranslateCall(call) =
            callElt
                !~(String call.Method.Entity.Value.MethodName)
                (callNewOrNull call.Arguments.[0])
                call.Arguments.[1]
            |> MacroOk

module React =

    let internal inlineArrayOfSeq (s: seq<'T>) : array<'T> =
        match s with
        | :? System.Array -> As s
        | s -> Array.ofSeq s

    let private elt (name: string) (props: obj) (children: seq<R.Element>) =
        R.CreateElement(name, props, inlineArrayOfSeq children)

    [<Macro(typeof<Macros.Element>); Inline>]
    let Element name props children =
        elt name (New props) children

    [<Inline>]
    let Text (s: string) =
        As<R.Element> s

    [<Inline>]
    let Mount target ``component`` =
        ReactDOM.Render(``component``, target) |> ignore

    [<Macro(typeof<Macros.Make>)>]
    [<Inline>]
    let Make<'T, 'Props, 'State when 'T :> R.Component<'Props, 'State>> (f: 'Props -> 'T) (props: 'Props) =
        R.CreateElement(As<R.Class> f, props)

    let Fragment (children: seq<R.Element>) =
        R.CreateElement(R.Fragment, null, inlineArrayOfSeq children)

    [<AbstractClass>]
    type Router<'Endpoint when 'Endpoint : equality>(props) =
        inherit R.Component<RouterProps<'Endpoint>, 'Endpoint>(props)

        override this.Render() =
            this.Props.render this

        [<Inline>]
        member this.Goto(endpoint: 'Endpoint) =
            this.SetState endpoint

        abstract Link : endpoint: 'Endpoint -> string

        [<Inline>]
        member this.Href(endpoint: 'Endpoint) =
            "href" => this.Link endpoint

    and RouterProps<'Endpoint when 'Endpoint : equality> =
        {
            router: SRouter<'Endpoint>
            render: Router<'Endpoint> -> R.Element
        }

    type HashRouter<'Endpoint when 'Endpoint : equality>(props) as this =
        inherit Router<'Endpoint>(props)

        let mutable listener : (Dom.Event -> unit) = JS.Undefined

        do this.ComputeRoute() |> Option.iter this.SetInitialState

        member this.ComputeRoute() =
            Route.FromHash(JS.Window.Location.Hash, true)
            |> SRouter.Parse this.Props.router

        override this.Link(endpoint: 'Endpoint) =
            this.Props.router.HashLink endpoint

        override this.ComponentDidUpdate(prevProps, prevState, _) =
            if prevState <> this.State then
                JS.Window.Location.Hash <- this.Link this.State

        override this.ComponentDidMount() =
            listener <- fun (e: Dom.Event) ->
                this.ComputeRoute() |> Option.iter this.SetState
            JS.Window.AddEventListener("hashchange", listener)

        override this.ComponentWillUnmount() =
            JS.Window.RemoveEventListener("hashchange", listener)

    [<Inline>]
    let HashRouter (router: SRouter<'Endpoint>) (render: Router<'Endpoint> -> R.Element) : R.Element =
        Make HashRouter { router = router; render = render }

    /// Create a WebSharper control that can be embedded in a Sitelet page.
    let Control (e: React.Element) =
        { new WebSharper.IControlBody with
            member this.ReplaceInDom(el) =
                let el' = el.OwnerDocument.CreateElement("div")
                el.ParentNode.InsertBefore(el', el) |> ignore
                el.ParentNode.RemoveChild(el) |> ignore
                Mount el' e
        }

[<AutoOpen>]
module Extensions =

    type R.Context<'T> with

        member this.Provide (value: 'T) (comp: seq<R.Element>) =
            R.CreateElement(this.Provider, New ["value" => value], React.inlineArrayOfSeq comp)

        member this.Consume (f: 'T -> #seq<R.Element>) =
            R.CreateElement(this.Consumer, null, fun v ->
                R.CreateElement(R.Fragment, null, React.inlineArrayOfSeq(f v)))

[<assembly: JavaScript>]
do ()
