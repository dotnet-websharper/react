namespace WebSharper.React

open WebSharper.React.Bindings
open WebSharper.React.ReactDOM.Bindings

module rec Props =
    
    open System
    open WebSharper.JavaScript
    
    /// Helper to convert Fable StringEnum to string, compatible with .NET
    let stringEnum (case: obj): string =
        string case
    
    type FragmentProp =
        | Key of string
        interface IFragmentProp
    
    type Prop =
        | Key of string
        | Ref of (Dom.Element->unit)
        | [<CompiledName("ref")>] RefValue of Ref<Dom.Element option>
        interface IHTMLProp
    
    type DangerousHtml = {
        __html: string
    }

    type WSProp =
        abstract member Key : string
        abstract member Value : obj
        abstract member ToProp : string * obj


    type WSHTMLProp =
        inherit IHTMLProp
        inherit WSProp
    type WSRProp =
        inherit IProp
        inherit WSProp
    type WSFragmentProp =
        inherit IFragmentProp
        inherit WSProp
    
    type WSPropBase(k: string, v: obj) =
        interface WSProp with

            override this.Key = k
            override this.Value = v
            override this.ToProp = k, v

    type DOMAttr(k, v) =
        inherit WSPropBase(k, v)
        interface WSHTMLProp

    type SVGAttr(k, v) =
        inherit WSPropBase(k, v)
        interface WSRProp

    type HTMLAttr(k, v) =
        inherit WSPropBase(k, v)
        interface WSHTMLProp

    type CSSProp(k, v) =
        inherit WSPropBase(k, v)

    //type WSProp(key: string, v: obj) =
        
    
    [<AutoOpen>]
    module DOMAttr =
        let internal (=>) k (v: obj) = DOMAttr(k, v)
        let DangerouslySetInnerHTML (x: DangerousHtml) = "DangerouslySetInnerHTML" => x
        let OnCut (x: (ClipboardEvent -> unit)) = "OnCut" => x
        let OnPaste (x: (ClipboardEvent -> unit)) = "OnPaste" => x
        let OnCompositionEnd (x: (CompositionEvent -> unit)) = "OnCompositionEnd" => x
        let OnCompositionStart (x: (CompositionEvent -> unit)) = "OnCompositionStart" => x
        let OnCopy (x: (ClipboardEvent -> unit)) = "OnCopy" => x
        let OnCompositionUpdate (x: (CompositionEvent -> unit)) = "OnCompositionUpdate" => x
        let OnFocus (x: (FocusEvent -> unit)) = "OnFocus" => x
        let OnBlur (x: (FocusEvent -> unit)) = "OnBlur" => x
        let OnChange (x: (Dom.Event -> unit)) = "OnChange" => x
        let OnInput (x: (Dom.Event -> unit)) = "OnInput" => x
        let OnSubmit (x: (Dom.Event -> unit)) = "OnSubmit" => x
        let OnReset (x: (Dom.Event -> unit)) = "OnReset" => x
        let OnLoad (x: (Dom.Event -> unit)) = "OnLoad" => x
        let OnError (x: (Dom.Event -> unit)) = "OnError" => x
        let OnKeyDown (x: (KeyboardEvent -> unit)) = "OnKeyDown" => x
        let OnKeyPress (x: (KeyboardEvent -> unit)) = "OnKeyPress" => x
        let OnKeyUp (x: (KeyboardEvent -> unit)) = "OnKeyUp" => x
        let OnAbort (x: (Dom.Event -> unit)) = "OnAbort" => x
        let OnCanPlay (x: (Dom.Event -> unit)) = "OnCanPlay" => x
        let OnCanPlayThrough (x: (Dom.Event -> unit)) = "OnCanPlayThrough" => x
        let OnDurationChange (x: (Dom.Event -> unit)) = "OnDurationChange" => x
        let OnEmptied (x: (Dom.Event -> unit)) = "OnEmptied" => x
        let OnEncrypted (x: (Dom.Event -> unit)) = "OnEncrypted" => x
        let OnEnded (x: (Dom.Event -> unit)) = "OnEnded" => x
        let OnLoadedData (x: (Dom.Event -> unit)) = "OnLoadedData" => x
        let OnLoadedMetadata (x: (Dom.Event -> unit)) = "OnLoadedMetadata" => x
        let OnLoadStart (x: (Dom.Event -> unit)) = "OnLoadStart" => x
        let OnPause (x: (Dom.Event -> unit)) = "OnPause" => x
        let OnPlay (x: (Dom.Event -> unit)) = "OnPlay" => x
        let OnPlaying (x: (Dom.Event -> unit)) = "OnPlaying" => x
        let OnProgress (x: (Dom.Event -> unit)) = "OnProgress" => x
        let OnRateChange (x: (Dom.Event -> unit)) = "OnRateChange" => x
        let OnSeeked (x: (Dom.Event -> unit)) = "OnSeeked" => x
        let OnSeeking (x: (Dom.Event -> unit)) = "OnSeeking" => x
        let OnStalled (x: (Dom.Event -> unit)) = "OnStalled" => x
        let OnSuspend (x: (Dom.Event -> unit)) = "OnSuspend" => x
        let OnTimeUpdate (x: (Dom.Event -> unit)) = "OnTimeUpdate" => x
        let OnVolumeChange (x: (Dom.Event -> unit)) = "OnVolumeChange" => x
        let OnWaiting (x: (Dom.Event -> unit)) = "OnWaiting" => x
        let OnClick (x: (MouseEvent -> unit)) = "OnClick" => x
        let OnContextMenu (x: (MouseEvent -> unit)) = "OnContextMenu" => x
        let OnDoubleClick (x: (MouseEvent -> unit)) = "OnDoubleClick" => x
        //| OnDrag of (DragEvent -> unit)
        //| OnDragEnd of (DragEvent -> unit)
        //| OnDragEnter of (DragEvent -> unit)
        //| OnDragExit of (DragEvent -> unit)
        //| OnDragLeave of (DragEvent -> unit)
        //| OnDragOver of (DragEvent -> unit)
        //| OnDragStart of (DragEvent -> unit)
        //| OnDrop of (DragEvent -> unit)
        let OnMouseDown (x: (MouseEvent -> unit)) = "OnMouseDown" => x
        let OnMouseEnter (x: (MouseEvent -> unit)) = "OnMouseEnter" => x
        let OnMouseLeave (x: (MouseEvent -> unit)) = "OnMouseLeave" => x
        let OnMouseMove (x: (MouseEvent -> unit)) = "OnMouseMove" => x
        let OnMouseOut (x: (MouseEvent -> unit)) = "OnMouseOut" => x
        let OnMouseOver (x: (MouseEvent -> unit)) = "OnMouseOver" => x
        let OnMouseUp (x: (MouseEvent -> unit)) = "OnMouseUp" => x
        let OnSelect (x: (Dom.Event -> unit)) = "OnSelect" => x
        let OnTouchCancel (x: (TouchEvent -> unit)) = "OnTouchCancel" => x
        let OnTouchEnd (x: (TouchEvent -> unit)) = "OnTouchEnd" => x
        let OnTouchMove (x: (TouchEvent -> unit)) = "OnTouchMove" => x
        let OnTouchStart (x: (TouchEvent -> unit)) = "OnTouchStart" => x
        let OnScroll (x: (UIEvent -> unit)) = "OnScroll" => x
        let OnWheel (x: (WheelEvent -> unit)) = "OnWheel" => x
        let OnAnimationStart (x: (AnimationEvent -> unit)) = "OnAnimationStart" => x
        let OnAnimationEnd (x: (AnimationEvent -> unit)) = "OnAnimationEnd" => x
        let OnAnimationIteration (x: (AnimationEvent -> unit)) = "OnAnimationIteration" => x
        let OnTransitionEnd (x: (TransitionEvent -> unit)) = "OnTransitionEnd" => x
        let Custom (x: string * obj) = fst x => snd x
    
    [<AutoOpen>]
    module SVGAttr =
        let internal (=>) k (v: obj) = SVGAttr(k, v)
        let ClipPath (x: string) = "ClipPath" => x
        let Cx (x: obj) = "Cx" => x
        let Cy (x: obj) = "Cy" => x
        let D (x: string) = "D" => x
        let Dx (x: obj) = "Dx" => x
        let Dy (x: obj) = "Dy" => x
        let Fill (x: string) = "Fill" => x
        let FillOpacity (x: obj) = "FillOpacity" => x
        let FontFamily (x: string) = "FontFamily" => x
        let FontSize (x: obj) = "FontSize" => x
        let Fx (x: obj) = "Fx" => x
        let Fy (x: obj) = "Fy" => x
        let GradientTransform (x: string) = "GradientTransform" => x
        let GradientUnits (x: string) = "GradientUnits" => x
        let Height (x: obj) = "Height" => x
        let MarkerEnd (x: string) = "MarkerEnd" => x
        let MarkerMid (x: string) = "MarkerMid" => x
        let MarkerStart (x: string) = "MarkerStart" => x
        let Offset (x: obj) = "Offset" => x
        let Opacity (x: obj) = "Opacity" => x
        let PatternContentUnits (x: string) = "PatternContentUnits" => x
        let PatternUnits (x: string) = "PatternUnits" => x
        let Points (x: string) = "Points" => x
        let PreserveAspectRatio (x: string) = "PreserveAspectRatio" => x
        let R (x: obj) = "R" => x
        let Rx (x: obj) = "Rx" => x
        let Ry (x: obj) = "Ry" => x
        let SpreadMethod (x: string) = "SpreadMethod" => x
        let StopColor (x: string) = "StopColor" => x
        let StopOpacity (x: obj) = "StopOpacity" => x
        let Stroke (x: string) = "Stroke" => x
        let StrokeDasharray (x: string) = "StrokeDasharray" => x
        let StrokeDashoffset (x: string) = "StrokeDashoffset" => x
        let StrokeLinecap (x: string) = "StrokeLinecap" => x
        let StrokeMiterlimit (x: string) = "StrokeMiterlimit" => x
        let StrokeOpacity (x: obj) = "StrokeOpacity" => x
        let StrokeWidth (x: obj) = "StrokeWidth" => x
        let TextAnchor (x: string) = "TextAnchor" => x
        let Transform (x: string) = "Transform" => x
        let Version (x: string) = "Version" => x
        let ViewBox (x: string) = "ViewBox" => x
        let Width (x: obj) = "Width" => x
        let X1 (x: obj) = "X1" => x
        let X2 (x: obj) = "X2" => x
        let X (x: obj) = "X" => x
        let XlinkActuate (x: string) = "XlinkActuate" => x
        let XlinkArcrole (x: string) = "XlinkArcrole" => x
        let XlinkHref (x: string) = "XlinkHref" => x
        let XlinkRole (x: string) = "XlinkRole" => x
        let XlinkShow (x: string) = "XlinkShow" => x
        let XlinkTitle (x: string) = "XlinkTitle" => x
        let XlinkType (x: string) = "XlinkType" => x
        let XmlBase (x: string) = "XmlBase" => x
        let XmlLang (x: string) = "XmlLang" => x
        let XmlSpace (x: string) = "XmlSpace" => x
        let Y1 (x: obj) = "Y1" => x
        let Y2 (x: obj) = "Y2" => x
        let Y (x: obj) = "Y" => x
        /// If you are searching for a way to provide a value not supported by this DSL then use something like: CSSProp.Custom ("align-content", "center")
        let Custom (x: string * obj) = fst x => snd x
    
    [<AutoOpen>]
    module HTMLAttr =
        let internal (=>) k (v: obj) = HTMLAttr(k, v)
        let DefaultChecked (x: bool) = "DefaultChecked" => x
        let DefaultValue (x: obj) = "DefaultValue" => x
        let Accept (x: string) = "Accept" => x
        let AcceptCharset (x: string) = "AcceptCharset" => x
        let AccessKey (x: string) = "AccessKey" => x
        let Action (x: string) = "Action" => x
        let AllowFullScreen (x: bool) = "AllowFullScreen" => x
        let AllowTransparency (x: bool) = "AllowTransparency" => x
        let Alt (x: string) = "Alt" => x
        let AriaAtomic (x: bool) = "aria-atomic" => x
        let AriaBusy (x: bool) = "aria-busy" => x
        let AriaChecked (x: bool) = "aria-checked" => x
        let AriaColcount (x: int) = "aria-colcount" => x
        let AriaColindex (x: int) = "aria-colindex" => x
        let AriaColspan (x: int) = "aria-colspan" => x
        let AriaControls (x: string) = "aria-controls" => x
        let AriaCurrent (x: string) = "aria-current" => x
        let AriaDescribedBy (x: string) = "aria-describedby" => x
        let AriaDetails (x: string) = "aria-details" => x
        let AriaDisabled (x: bool) = "aria-disabled" => x
        let AriaErrormessage (x: string) = "aria-errormessage" => x
        let AriaExpanded (x: bool) = "aria-expanded" => x
        let AriaFlowto (x: string) = "aria-flowto" => x
        let AriaHasPopup (x: bool) = "aria-haspopup" => x
        let AriaHidden (x: bool) = "aria-hidden" => x
        let AriaInvalid (x: string) = "aria-invalid" => x
        let AriaKeyshortcuts (x: string) = "aria-keyshortcuts" => x
        let AriaLabel (x: string) = "aria-label" => x
        let AriaLabelledby (x: string) = "aria-labelledby" => x
        let AriaLevel (x: int) = "aria-level" => x
        let AriaLive (x: string) = "aria-live" => x
        let AriaModal (x: bool) = "aria-modal" => x
        let AriaMultiline (x: bool) = "aria-multiline" => x
        let AriaMultiselectable (x: bool) = "aria-multiselectable" => x
        let AriaOrientation (x: string) = "aria-orientation" => x
        let AriaOwns (x: string) = "aria-owns" => x
        let AriaPlaceholder (x: string) = "aria-placeholder" => x
        let AriaPosinset (x: int) = "aria-posinset" => x
        let AriaPressed (x: bool) = "aria-pressed" => x
        let AriaReadonly (x: bool) = "aria-readonly" => x
        let AriaRelevant (x: string) = "aria-relevant" => x
        let AriaRequired (x: bool) = "aria-required" => x
        let AriaRoledescription (x: string) = "aria-roledescription" => x
        let AriaRowcount (x: int) = "aria-rowcount" => x
        let AriaRowindex (x: int) = "aria-rowindex" => x
        let AriaRowspan (x: int) = "aria-rowspan" => x
        let AriaSelected (x: bool) = "aria-selected" => x
        let AriaSetsize (x: int) = "aria-setsize" => x
        let AriaSort (x: string) = "aria-sort" => x
        let AriaValuemax (x: float) = "aria-valuemax" => x
        let AriaValuemin (x: float) = "aria-valuemin" => x
        let AriaValuenow (x: float) = "aria-valuenow" => x
        let AriaValuetext (x: string) = "aria-valuetext" => x
        let ValueMultiple (x: string[]) = "value" => x
        let Class (x: string) = "className" => x
        let DataToggle (x: string) = "data-toggle" => x
        let Async (x: bool) = "Async" => x
        let AutoComplete (x: string) = "AutoComplete" => x
        let AutoFocus (x: bool) = "AutoFocus" => x
        let AutoPlay (x: bool) = "AutoPlay" => x
        let Capture (x: bool) = "Capture" => x
        let CellPadding (x: obj) = "CellPadding" => x
        let CellSpacing (x: obj) = "CellSpacing" => x
        let CharSet (x: string) = "CharSet" => x
        let Challenge (x: string) = "Challenge" => x
        let Checked (x: bool) = "Checked" => x
        let ClassID (x: string) = "ClassID" => x
        let ClassName (x: string) = "ClassName" => x
        let Cols (x: int) = "Cols" => x
        let ColSpan (x: int) = "ColSpan" => x
        let Content (x: string) = "Content" => x
        let ContentEditable (x: bool) = "ContentEditable" => x
        let ContextMenu (x: string) = "ContextMenu" => x
        let Controls (x: bool) = "Controls" => x
        let Coords (x: string) = "Coords" => x
        let CrossOrigin (x: string) = "CrossOrigin" => x
        let DateTime (x: string) = "DateTime" => x
        let Default (x: bool) = "Default" => x
        let Defer (x: bool) = "Defer" => x
        let Dir (x: string) = "Dir" => x
        let Disabled (x: bool) = "Disabled" => x
        let Download (x: obj) = "Download" => x
        let Draggable (x: bool) = "Draggable" => x
        let EncType (x: string) = "EncType" => x
        let Form (x: string) = "Form" => x
        let FormAction (x: string) = "FormAction" => x
        let FormEncType (x: string) = "FormEncType" => x
        let FormMethod (x: string) = "FormMethod" => x
        let FormNoValidate (x: bool) = "FormNoValidate" => x
        let FormTarget (x: string) = "FormTarget" => x
        let FrameBorder (x: obj) = "FrameBorder" => x
        let Headers (x: string) = "Headers" => x
        let Height (x: obj) = "Height" => x
        let Hidden (x: bool) = "Hidden" => x
        let High (x: float) = "High" => x
        let Href (x: string) = "Href" => x
        let HrefLang (x: string) = "HrefLang" => x
        let HtmlFor (x: string) = "HtmlFor" => x
        let HttpEquiv (x: string) = "HttpEquiv" => x
        let Icon (x: string) = "Icon" => x
        let Id (x: string) = "Id" => x
        let InputMode (x: string) = "InputMode" => x
        let Integrity (x: string) = "Integrity" => x
        let Is (x: string) = "Is" => x
        let KeyParams (x: string) = "KeyParams" => x
        let KeyType (x: string) = "KeyType" => x
        let Kind (x: string) = "Kind" => x
        let Label (x: string) = "Label" => x
        let Lang (x: string) = "Lang" => x
        let AttrList (x: string) = "List" => x
        let Loop (x: bool) = "Loop" => x
        let Low (x: float) = "Low" => x
        let Manifest (x: string) = "Manifest" => x
        let MarginHeight (x: float) = "MarginHeight" => x
        let MarginWidth (x: float) = "MarginWidth" => x
        let Max (x: obj) = "Max" => x
        let MaxLength (x: float) = "MaxLength" => x
        let Media (x: string) = "Media" => x
        let MediaGroup (x: string) = "MediaGroup" => x
        let Method (x: string) = "Method" => x
        let Min (x: obj) = "Min" => x
        let MinLength (x: float) = "MinLength" => x
        let Multiple (x: bool) = "Multiple" => x
        let Muted (x: bool) = "Muted" => x
        let Name (x: string) = "Name" => x
        let NoValidate (x: bool) = "NoValidate" => x
        let Open (x: bool) = "Open" => x
        let Optimum (x: float) = "Optimum" => x
        let Pattern (x: string) = "Pattern" => x
        let Placeholder (x: string) = "Placeholder" => x
        let Poster (x: string) = "Poster" => x
        let Preload (x: string) = "Preload" => x
        let RadioGroup (x: string) = "RadioGroup" => x
        let ReadOnly (x: bool) = "ReadOnly" => x
        let Rel (x: string) = "Rel" => x
        let Required (x: bool) = "Required" => x
        let Role (x: string) = "Role" => x
        let Rows (x: int) = "Rows" => x
        let RowSpan (x: int) = "RowSpan" => x
        let Sandbox (x: string) = "Sandbox" => x
        let Scope (x: string) = "Scope" => x
        let Scoped (x: bool) = "Scoped" => x
        let Scrolling (x: string) = "Scrolling" => x
        let Seamless (x: bool) = "Seamless" => x
        let Selected (x: bool) = "Selected" => x
        let Shape (x: string) = "Shape" => x
        let Size (x: float) = "Size" => x
        let Sizes (x: string) = "Sizes" => x
        let Span (x: float) = "Span" => x
        let SpellCheck (x: bool) = "SpellCheck" => x
        let Src (x: string) = "Src" => x
        let SrcDoc (x: string) = "SrcDoc" => x
        let SrcLang (x: string) = "SrcLang" => x
        let SrcSet (x: string) = "SrcSet" => x
        let Start (x: float) = "Start" => x
        let Step (x: obj) = "Step" => x
        let Summary (x: string) = "Summary" => x
        let TabIndex (x: int) = "TabIndex" => x
        let Target (x: string) = "Target" => x
        let Title (x: string) = "Title" => x
        let Type (x: string) = "Type" => x
        let UseMap (x: string) = "UseMap" => x
        let Value (x: obj) = "Value" => x
        let Width (x: obj) = "Width" => x
        let Wmode (x: string) = "Wmode" => x
        let Wrap (x: string) = "Wrap" => x
        let About (x: string) = "About" => x
        let Datatype (x: string) = "Datatype" => x
        let Inlist (x: obj) = "Inlist" => x
        let Prefix (x: string) = "Prefix" => x
        let Property (x: string) = "Property" => x
        let Resource (x: string) = "Resource" => x
        let Typeof (x: string) = "Typeof" => x
        let Vocab (x: string) = "Vocab" => x
        let AutoCapitalize (x: string) = "AutoCapitalize" => x
        let AutoCorrect (x: string) = "AutoCorrect" => x
        let AutoSave (x: string) = "AutoSave" => x
        let ItemProp (x: string) = "ItemProp" => x
        let ItemScope (x: bool) = "ItemScope" => x
        let ItemType (x: string) = "ItemType" => x
        let ItemID (x: string) = "ItemID" => x
        let ItemRef (x: string) = "ItemRef" => x
        let Results (x: float) = "Results" => x
        let Security (x: string) = "Security" => x
        let Unselectable (x: bool) = "Unselectable" => x
        let Custom (x: string * obj) = fst x => snd x

    let lowerCaseStr (str: string) =
        if String.IsNullOrEmpty(str) then
            str
        else
            let lC = str[0] |> string |> fun x -> x.ToLower()
            let rest = str[1..]
            sprintf "%s%s" lC rest

    let inline keyValueList (list: WSProp seq) =
        New (list |> Seq.map (fun x -> x.Key |> lowerCaseStr, x.Value))
    
    let [<WebSharper.Inline>] Style (css: CSSProp list): HTMLAttr =
        As<HTMLAttr>("style", keyValueList (css |> Seq.ofList |> Seq.map (fun x -> upcast x)))
    
    let inline Data (key: string, value: obj): HTMLAttr =
        As<HTMLAttr>("data-" + key, value)

    
    [<AutoOpen>]
    module CSSProp =
        let internal (=>) k (v: obj) = CSSProp(k, v)
        let AlignContent (x: string) = "AlignContent" => x
        let AlignItems (x: string) = "AlignItems" => x
        let AlignSelf (x: string) = "AlignSelf" => x
        let AlignmentAdjust (x: obj) = "AlignmentAdjust" => x
        let AlignmentBaseline (x: string) = "AlignmentBaseline" => x
        let All (x: string) = "All" => x
        let Animation (x: obj) = "Animation" => x
        let AnimationDelay (x: obj) = "AnimationDelay" => x
        let AnimationDirection (x: string) = "AnimationDirection" => x
        let AnimationDuration (x: obj) = "AnimationDuration" => x
        let AnimationFillMode (x: string) = "AnimationFillMode" => x
        let AnimationIterationCount (x: obj) = "AnimationIterationCount" => x
        let AnimationName (x: obj) = "AnimationName" => x
        let AnimationPlayState (x: obj) = "AnimationPlayState" => x
        let AnimationTimingFunction (x: obj) = "AnimationTimingFunction" => x
        let Appearance (x: string) = "Appearance" => x
        let BackfaceVisibility (x: string) = "BackfaceVisibility" => x
        let Background (x: obj) = "Background" => x
        let BackgroundAttachment (x: obj) = "BackgroundAttachment" => x
        let BackgroundBlendMode (x: obj) = "BackgroundBlendMode" => x
        let BackgroundClip (x: obj) = "BackgroundClip" => x
        let BackgroundColor (x: obj) = "BackgroundColor" => x
        let BackgroundComposite (x: obj) = "BackgroundComposite" => x
        let BackgroundImage (x: obj) = "BackgroundImage" => x
        let BackgroundOrigin (x: obj) = "BackgroundOrigin" => x
        let BackgroundPosition (x: obj) = "BackgroundPosition" => x
        let BackgroundPositionX (x: obj) = "BackgroundPositionX" => x
        let BackgroundPositionY (x: obj) = "BackgroundPositionY" => x
        let BackgroundRepeat (x: obj) = "BackgroundRepeat" => x
        let BackgroundSize (x: obj) = "BackgroundSize" => x
        let BaselineShift (x: obj) = "BaselineShift" => x
        let Behavior (x: obj) = "Behavior" => x
        let BlockSize (x: obj) = "BlockSize" => x
        let Border (x: obj) = "Border" => x
        let BorderBlockEnd (x: obj) = "BorderBlockEnd" => x
        let BorderBlockEndColor (x: obj) = "BorderBlockEndColor" => x
        let BorderBlockEndStyle (x: obj) = "BorderBlockEndStyle" => x
        let BorderBlockEndWidth (x: obj) = "BorderBlockEndWidth" => x
        let BorderBlockStart (x: obj) = "BorderBlockStart" => x
        let BorderBlockStartColor (x: obj) = "BorderBlockStartColor" => x
        let BorderBlockStartStyle (x: obj) = "BorderBlockStartStyle" => x
        let BorderBlockStartWidth (x: obj) = "BorderBlockStartWidth" => x
        let BorderBottom (x: obj) = "BorderBottom" => x
        let BorderBottomColor (x: obj) = "BorderBottomColor" => x
        let BorderBottomLeftRadius (x: obj) = "BorderBottomLeftRadius" => x
        let BorderBottomRightRadius (x: obj) = "BorderBottomRightRadius" => x
        let BorderBottomStyle (x: obj) = "BorderBottomStyle" => x
        let BorderBottomWidth (x: obj) = "BorderBottomWidth" => x
        let BorderCollapse (x: obj) = "BorderCollapse" => x
        let BorderColor (x: obj) = "BorderColor" => x
        let BorderCornerShape (x: obj) = "BorderCornerShape" => x
        let BorderImage (x: obj) = "BorderImage" => x
        let BorderImageOutset (x: obj) = "BorderImageOutset" => x
        let BorderImageRepeat (x: obj) = "BorderImageRepeat" => x
        let BorderImageSlice (x: obj) = "BorderImageSlice" => x
        let BorderImageSource (x: obj) = "BorderImageSource" => x
        let BorderImageWidth (x: obj) = "BorderImageWidth" => x
        let BorderInlineEnd (x: obj) = "BorderInlineEnd" => x
        let BorderInlineEndColor (x: obj) = "BorderInlineEndColor" => x
        let BorderInlineEndStyle (x: obj) = "BorderInlineEndStyle" => x
        let BorderInlineEndWidth (x: obj) = "BorderInlineEndWidth" => x
        let BorderInlineStart (x: obj) = "BorderInlineStart" => x
        let BorderInlineStartColor (x: obj) = "BorderInlineStartColor" => x
        let BorderInlineStartStyle (x: obj) = "BorderInlineStartStyle" => x
        let BorderInlineStartWidth (x: obj) = "BorderInlineStartWidth" => x
        let BorderLeft (x: obj) = "BorderLeft" => x
        let BorderLeftColor (x: obj) = "BorderLeftColor" => x
        let BorderLeftStyle (x: obj) = "BorderLeftStyle" => x
        let BorderLeftWidth (x: obj) = "BorderLeftWidth" => x
        let BorderRadius (x: obj) = "BorderRadius" => x
        let BorderRight (x: obj) = "BorderRight" => x
        let BorderRightColor (x: obj) = "BorderRightColor" => x
        let BorderRightStyle (x: obj) = "BorderRightStyle" => x
        let BorderRightWidth (x: obj) = "BorderRightWidth" => x
        let BorderSpacing (x: obj) = "BorderSpacing" => x
        let BorderStyle (x: obj) = "BorderStyle" => x
        let BorderTop (x: obj) = "BorderTop" => x
        let BorderTopColor (x: obj) = "BorderTopColor" => x
        let BorderTopLeftRadius (x: obj) = "BorderTopLeftRadius" => x
        let BorderTopRightRadius (x: obj) = "BorderTopRightRadius" => x
        let BorderTopStyle (x: obj) = "BorderTopStyle" => x
        let BorderTopWidth (x: obj) = "BorderTopWidth" => x
        let BorderWidth (x: obj) = "BorderWidth" => x
        let Bottom (x: obj) = "Bottom" => x
        let BoxAlign (x: string) = "BoxAlign" => x
        let BoxDecorationBreak (x: string) = "BoxDecorationBreak" => x
        let BoxDirection (x: string) = "BoxDirection" => x
        let BoxFlex (x: obj) = "BoxFlex" => x
        let BoxFlexGroup (x: obj) = "BoxFlexGroup" => x
        let BoxLineProgression (x: obj) = "BoxLineProgression" => x
        let BoxLines (x: obj) = "BoxLines" => x
        let BoxOrdinalGroup (x: obj) = "BoxOrdinalGroup" => x
        let BoxShadow (x: obj) = "BoxShadow" => x
        let BoxSizing (x: string) = "BoxSizing" => x
        let BreakAfter (x: string) = "BreakAfter" => x
        let BreakBefore (x: string) = "BreakBefore" => x
        let BreakInside (x: string) = "BreakInside" => x
        let CaptionSide (x: string) = "CaptionSide" => x
        let CaretColor (x: obj) = "CaretColor" => x
        let Clear (x: string) = "Clear" => x
        let Clip (x: obj) = "Clip" => x
        let ClipPath (x: obj) = "ClipPath" => x
        let ClipRule (x: obj) = "ClipRule" => x
        let Color (x: obj) = "Color" => x
        let ColorInterpolation (x: obj) = "ColorInterpolation" => x
        let ColorInterpolationFilters (x: obj) = "ColorInterpolationFilters" => x
        let ColorProfile (x: obj) = "ColorProfile" => x
        let ColorRendering (x: obj) = "ColorRendering" => x
        let ColumnCount (x: obj) = "ColumnCount" => x
        let ColumnFill (x: obj) = "ColumnFill" => x
        let ColumnGap (x: obj) = "ColumnGap" => x
        let ColumnRule (x: obj) = "ColumnRule" => x
        let ColumnRuleColor (x: obj) = "ColumnRuleColor" => x
        let ColumnRuleStyle (x: obj) = "ColumnRuleStyle" => x
        let ColumnRuleWidth (x: obj) = "ColumnRuleWidth" => x
        let ColumnSpan (x: obj) = "ColumnSpan" => x
        let ColumnWidth (x: obj) = "ColumnWidth" => x
        let Columns (x: obj) = "Columns" => x
        let Content (x: obj) = "Content" => x
        let CounterIncrement (x: obj) = "CounterIncrement" => x
        let CounterReset (x: obj) = "CounterReset" => x
        let Cue (x: obj) = "Cue" => x
        let CueAfter (x: obj) = "CueAfter" => x
        let Cursor (x: obj) = "Cursor" => x
        let Direction (x: string) = "Direction" => x
        let Display (x: string) = "Display" => x
        let DominantBaseline (x: obj) = "DominantBaseline" => x
        let EmptyCells (x: obj) = "EmptyCells" => x
        let EnableBackground (x: obj) = "EnableBackground" => x
        let Fill (x: obj) = "Fill" => x
        let FillOpacity (x: obj) = "FillOpacity" => x
        let FillRule (x: obj) = "FillRule" => x
        let Filter (x: obj) = "Filter" => x
        let Flex (x: obj) = "Flex" => x
        let FlexAlign (x: obj) = "FlexAlign" => x
        let FlexBasis (x: obj) = "FlexBasis" => x
        let FlexDirection (x: obj) = "FlexDirection" => x
        let FlexFlow (x: obj) = "FlexFlow" => x
        let FlexGrow (x: obj) = "FlexGrow" => x
        let FlexItemAlign (x: obj) = "FlexItemAlign" => x
        let FlexLinePack (x: obj) = "FlexLinePack" => x
        let FlexOrder (x: obj) = "FlexOrder" => x
        let FlexShrink (x: obj) = "FlexShrink" => x
        let FlexWrap (x: obj) = "FlexWrap" => x
        let Float (x: string) = "Float" => x
        let FloodColor (x: obj) = "FloodColor" => x
        let FloodOpacity (x: obj) = "FloodOpacity" => x
        let FlowFrom (x: obj) = "FlowFrom" => x
        let Font (x: obj) = "Font" => x
        let FontFamily (x: obj) = "FontFamily" => x
        let FontFeatureSettings (x: obj) = "FontFeatureSettings" => x
        let FontKerning (x: obj) = "FontKerning" => x
        let FontLanguageOverride (x: obj) = "FontLanguageOverride" => x
        let FontSize (x: obj) = "FontSize" => x
        let FontSizeAdjust (x: obj) = "FontSizeAdjust" => x
        let FontStretch (x: obj) = "FontStretch" => x
        let FontStyle (x: obj) = "FontStyle" => x
        let FontSynthesis (x: obj) = "FontSynthesis" => x
        let FontVariant (x: obj) = "FontVariant" => x
        let FontVariantAlternates (x: obj) = "FontVariantAlternates" => x
        let FontVariantCaps (x: obj) = "FontVariantCaps" => x
        let FontVariantEastAsian (x: obj) = "FontVariantEastAsian" => x
        let FontVariantLigatures (x: obj) = "FontVariantLigatures" => x
        let FontVariantNumeric (x: obj) = "FontVariantNumeric" => x
        let FontVariantPosition (x: obj) = "FontVariantPosition" => x
        let FontWeight (x: obj) = "FontWeight" => x
        let GlyphOrientationHorizontal (x: obj) = "GlyphOrientationHorizontal" => x
        let GlyphOrientationVertical (x: obj) = "GlyphOrientationVertical" => x
        let Grid (x: obj) = "Grid" => x
        let GridArea (x: obj) = "GridArea" => x
        let GridAutoColumns (x: obj) = "GridAutoColumns" => x
        let GridAutoFlow (x: obj) = "GridAutoFlow" => x
        let GridAutoRows (x: obj) = "GridAutoRows" => x
        let GridColumn (x: obj) = "GridColumn" => x
        let GridColumnEnd (x: obj) = "GridColumnEnd" => x
        let GridColumnGap (x: obj) = "GridColumnGap" => x
        let GridColumnStart (x: obj) = "GridColumnStart" => x
        let GridGap (x: obj) = "GridGap" => x
        let GridRow (x: obj) = "GridRow" => x
        let GridRowEnd (x: obj) = "GridRowEnd" => x
        let GridRowGap (x: obj) = "GridRowGap" => x
        let GridRowPosition (x: obj) = "GridRowPosition" => x
        let GridRowSpan (x: obj) = "GridRowSpan" => x
        let GridRowStart (x: obj) = "GridRowStart" => x
        let GridTemplate (x: obj) = "GridTemplate" => x
        let GridTemplateAreas (x: obj) = "GridTemplateAreas" => x
        let GridTemplateColumns (x: obj) = "GridTemplateColumns" => x
        let GridTemplateRows (x: obj) = "GridTemplateRows" => x
        let HangingPunctuation (x: obj) = "HangingPunctuation" => x
        let Height (x: obj) = "Height" => x
        let HyphenateLimitChars (x: obj) = "HyphenateLimitChars" => x
        let HyphenateLimitLines (x: obj) = "HyphenateLimitLines" => x
        let HyphenateLimitZone (x: obj) = "HyphenateLimitZone" => x
        let Hyphens (x: obj) = "Hyphens" => x
        let ImageOrientation (x: obj) = "ImageOrientation" => x
        let ImageRendering (x: string) = "ImageRendering" => x
        let ImageResolution (x: obj) = "ImageResolution" => x
        let ImeMode (x: obj) = "ImeMode" => x
        let InlineSize (x: obj) = "InlineSize" => x
        let Isolation (x: obj) = "Isolation" => x
        let JustifyContent (x: obj) = "JustifyContent" => x
        let JustifySelf (x: string) = "JustifySelf" => x
        let Kerning (x: obj) = "Kerning" => x
        let LayoutGrid (x: obj) = "LayoutGrid" => x
        let LayoutGridChar (x: obj) = "LayoutGridChar" => x
        let LayoutGridLine (x: obj) = "LayoutGridLine" => x
        let LayoutGridMode (x: obj) = "LayoutGridMode" => x
        let LayoutGridType (x: obj) = "LayoutGridType" => x
        let Left (x: obj) = "Left" => x
        let LetterSpacing (x: obj) = "LetterSpacing" => x
        let LightingColor (x: obj) = "LightingColor" => x
        let LineBreak (x: obj) = "LineBreak" => x
        let LineClamp (x: obj) = "LineClamp" => x
        let LineHeight (x: obj) = "LineHeight" => x
        let ListStyle (x: obj) = "ListStyle" => x
        let ListStyleImage (x: obj) = "ListStyleImage" => x
        let ListStylePosition (x: obj) = "ListStylePosition" => x
        let ListStyleType (x: obj) = "ListStyleType" => x
        let Margin (x: obj) = "Margin" => x
        let MarginBlockEnd (x: obj) = "MarginBlockEnd" => x
        let MarginBlockStart (x: obj) = "MarginBlockStart" => x
        let MarginBottom (x: obj) = "MarginBottom" => x
        let MarginInlineEnd (x: obj) = "MarginInlineEnd" => x
        let MarginInlineStart (x: obj) = "MarginInlineStart" => x
        let MarginLeft (x: obj) = "MarginLeft" => x
        let MarginRight (x: obj) = "MarginRight" => x
        let MarginTop (x: obj) = "MarginTop" => x
        let MarkerEnd (x: obj) = "MarkerEnd" => x
        let MarkerMid (x: obj) = "MarkerMid" => x
        let MarkerStart (x: obj) = "MarkerStart" => x
        let MarqueeDirection (x: obj) = "MarqueeDirection" => x
        let MarqueeStyle (x: obj) = "MarqueeStyle" => x
        let Mask (x: obj) = "Mask" => x
        let MaskBorder (x: obj) = "MaskBorder" => x
        let MaskBorderRepeat (x: obj) = "MaskBorderRepeat" => x
        let MaskBorderSlice (x: obj) = "MaskBorderSlice" => x
        let MaskBorderSource (x: obj) = "MaskBorderSource" => x
        let MaskBorderWidth (x: obj) = "MaskBorderWidth" => x
        let MaskClip (x: obj) = "MaskClip" => x
        let MaskComposite (x: obj) = "MaskComposite" => x
        let MaskImage (x: obj) = "MaskImage" => x
        let MaskMode (x: obj) = "MaskMode" => x
        let MaskOrigin (x: obj) = "MaskOrigin" => x
        let MaskPosition (x: obj) = "MaskPosition" => x
        let MaskRepeat (x: obj) = "MaskRepeat" => x
        let MaskSize (x: obj) = "MaskSize" => x
        let MaskType (x: obj) = "MaskType" => x
        let MaxFontSize (x: obj) = "MaxFontSize" => x
        let MaxHeight (x: obj) = "MaxHeight" => x
        let MaxWidth (x: obj) = "MaxWidth" => x
        let MinBlockSize (x: obj) = "MinBlockSize" => x
        let MinHeight (x: obj) = "MinHeight" => x
        let MinInlineSize (x: obj) = "MinInlineSize" => x
        let MinWidth (x: obj) = "MinWidth" => x
        let MixBlendMode (x: obj) = "MixBlendMode" => x
        let ObjectFit (x: obj) = "ObjectFit" => x
        let ObjectPosition (x: obj) = "ObjectPosition" => x
        let OffsetBlockEnd (x: obj) = "OffsetBlockEnd" => x
        let OffsetBlockStart (x: obj) = "OffsetBlockStart" => x
        let OffsetInlineEnd (x: obj) = "OffsetInlineEnd" => x
        let OffsetInlineStart (x: obj) = "OffsetInlineStart" => x
        let Opacity (x: obj) = "Opacity" => x
        let Order (x: obj) = "Order" => x
        let Orphans (x: obj) = "Orphans" => x
        let Outline (x: obj) = "Outline" => x
        let OutlineColor (x: obj) = "OutlineColor" => x
        let OutlineOffset (x: obj) = "OutlineOffset" => x
        let OutlineStyle (x: obj) = "OutlineStyle" => x
        let OutlineWidth (x: obj) = "OutlineWidth" => x
        let OverflowStyle (x: obj) = "OverflowStyle" => x
        let OverflowWrap (x: obj) = "OverflowWrap" => x
        let OverflowX (x: string) = "OverflowX" => x
        let OverflowY (x: string) = "OverflowY" => x
        let Padding (x: obj) = "Padding" => x
        let PaddingBlockEnd (x: obj) = "PaddingBlockEnd" => x
        let PaddingBlockStart (x: obj) = "PaddingBlockStart" => x
        let PaddingBottom (x: obj) = "PaddingBottom" => x
        let PaddingInlineEnd (x: obj) = "PaddingInlineEnd" => x
        let PaddingInlineStart (x: obj) = "PaddingInlineStart" => x
        let PaddingLeft (x: obj) = "PaddingLeft" => x
        let PaddingRight (x: obj) = "PaddingRight" => x
        let PaddingTop (x: obj) = "PaddingTop" => x
        let PageBreakAfter (x: obj) = "PageBreakAfter" => x
        let PageBreakBefore (x: obj) = "PageBreakBefore" => x
        let PageBreakInside (x: obj) = "PageBreakInside" => x
        let Pause (x: obj) = "Pause" => x
        let PauseAfter (x: obj) = "PauseAfter" => x
        let PauseBefore (x: obj) = "PauseBefore" => x
        let Perspective (x: obj) = "Perspective" => x
        let PerspectiveOrigin (x: obj) = "PerspectiveOrigin" => x
        let PointerEvents (x: obj) = "PointerEvents" => x
        let Position (x: string) = "Position" => x
        let PunctuationTrim (x: obj) = "PunctuationTrim" => x
        let Quotes (x: obj) = "Quotes" => x
        let RegionFragment (x: obj) = "RegionFragment" => x
        let Resize (x: obj) = "Resize" => x
        let RestAfter (x: obj) = "RestAfter" => x
        let RestBefore (x: obj) = "RestBefore" => x
        let Right (x: obj) = "Right" => x
        let RubyAlign (x: obj) = "RubyAlign" => x
        let RubyMerge (x: obj) = "RubyMerge" => x
        let RubyPosition (x: obj) = "RubyPosition" => x
        let ScrollBehavior (x: obj) = "ScrollBehavior" => x
        let ScrollSnapCoordinate (x: obj) = "ScrollSnapCoordinate" => x
        let ScrollSnapDestination (x: obj) = "ScrollSnapDestination" => x
        let ScrollSnapType (x: obj) = "ScrollSnapType" => x
        let ShapeImageThreshold (x: obj) = "ShapeImageThreshold" => x
        let ShapeInside (x: obj) = "ShapeInside" => x
        let ShapeMargin (x: obj) = "ShapeMargin" => x
        let ShapeOutside (x: obj) = "ShapeOutside" => x
        let ShapeRendering (x: obj) = "ShapeRendering" => x
        let Speak (x: obj) = "Speak" => x
        let SpeakAs (x: obj) = "SpeakAs" => x
        let StopColor (x: obj) = "StopColor" => x
        let StopOpacity (x: obj) = "StopOpacity" => x
        let Stroke (x: obj) = "Stroke" => x
        let StrokeDasharray (x: obj) = "StrokeDasharray" => x
        let StrokeDashoffset (x: obj) = "StrokeDashoffset" => x
        let StrokeLinecap (x: obj) = "StrokeLinecap" => x
        let StrokeLinejoin (x: obj) = "StrokeLinejoin" => x
        let StrokeMiterlimit (x: obj) = "StrokeMiterlimit" => x
        let StrokeOpacity (x: obj) = "StrokeOpacity" => x
        let StrokeWidth (x: obj) = "StrokeWidth" => x
        let TabSize (x: obj) = "TabSize" => x
        let TableLayout (x: obj) = "TableLayout" => x
        let TextAlign (x: string) = "TextAlign" => x
        let TextAlignLast (x: obj) = "TextAlignLast" => x
        let TextAnchor (x: obj) = "TextAnchor" => x
        let TextCombineUpright (x: obj) = "TextCombineUpright" => x
        let TextDecoration (x: obj) = "TextDecoration" => x
        let TextDecorationColor (x: obj) = "TextDecorationColor" => x
        let TextDecorationLine (x: obj) = "TextDecorationLine" => x
        let TextDecorationLineThrough (x: obj) = "TextDecorationLineThrough" => x
        let TextDecorationNone (x: obj) = "TextDecorationNone" => x
        let TextDecorationOverline (x: obj) = "TextDecorationOverline" => x
        let TextDecorationSkip (x: obj) = "TextDecorationSkip" => x
        let TextDecorationStyle (x: obj) = "TextDecorationStyle" => x
        let TextDecorationUnderline (x: obj) = "TextDecorationUnderline" => x
        let TextEmphasis (x: obj) = "TextEmphasis" => x
        let TextEmphasisColor (x: obj) = "TextEmphasisColor" => x
        let TextEmphasisPosition (x: obj) = "TextEmphasisPosition" => x
        let TextEmphasisStyle (x: obj) = "TextEmphasisStyle" => x
        let TextHeight (x: obj) = "TextHeight" => x
        let TextIndent (x: obj) = "TextIndent" => x
        let TextJustify (x: obj) = "TextJustify" => x
        let TextJustifyTrim (x: obj) = "TextJustifyTrim" => x
        let TextKashidaSpace (x: obj) = "TextKashidaSpace" => x
        let TextLineThrough (x: obj) = "TextLineThrough" => x
        let TextLineThroughColor (x: obj) = "TextLineThroughColor" => x
        let TextLineThroughMode (x: obj) = "TextLineThroughMode" => x
        let TextLineThroughStyle (x: obj) = "TextLineThroughStyle" => x
        let TextLineThroughWidth (x: obj) = "TextLineThroughWidth" => x
        let TextOrientation (x: obj) = "TextOrientation" => x
        let TextOverflow (x: obj) = "TextOverflow" => x
        let TextOverline (x: obj) = "TextOverline" => x
        let TextOverlineColor (x: obj) = "TextOverlineColor" => x
        let TextOverlineMode (x: obj) = "TextOverlineMode" => x
        let TextOverlineStyle (x: obj) = "TextOverlineStyle" => x
        let TextOverlineWidth (x: obj) = "TextOverlineWidth" => x
        let TextRendering (x: obj) = "TextRendering" => x
        let TextScript (x: obj) = "TextScript" => x
        let TextShadow (x: obj) = "TextShadow" => x
        let TextTransform (x: obj) = "TextTransform" => x
        let TextUnderlinePosition (x: obj) = "TextUnderlinePosition" => x
        let TextUnderlineStyle (x: obj) = "TextUnderlineStyle" => x
        let Top (x: obj) = "Top" => x
        let TouchAction (x: obj) = "TouchAction" => x
        let Transform (x: obj) = "Transform" => x
        let TransformBox (x: obj) = "TransformBox" => x
        let TransformOrigin (x: obj) = "TransformOrigin" => x
        let TransformOriginZ (x: obj) = "TransformOriginZ" => x
        let TransformStyle (x: obj) = "TransformStyle" => x
        let Transition (x: obj) = "Transition" => x
        let TransitionDelay (x: obj) = "TransitionDelay" => x
        let TransitionDuration (x: obj) = "TransitionDuration" => x
        let TransitionProperty (x: obj) = "TransitionProperty" => x
        let TransitionTimingFunction (x: obj) = "TransitionTimingFunction" => x
        let UnicodeBidi (x: obj) = "UnicodeBidi" => x
        let UnicodeRange (x: obj) = "UnicodeRange" => x
        let UserFocus (x: obj) = "UserFocus" => x
        let UserInput (x: obj) = "UserInput" => x
        let UserSelect (x: string) = "UserSelect" => x
        let VerticalAlign (x: obj) = "VerticalAlign" => x
        let Visibility (x: obj) = "Visibility" => x
        let VoiceBalance (x: obj) = "VoiceBalance" => x
        let VoiceDuration (x: obj) = "VoiceDuration" => x
        let VoiceFamily (x: obj) = "VoiceFamily" => x
        let VoicePitch (x: obj) = "VoicePitch" => x
        let VoiceRange (x: obj) = "VoiceRange" => x
        let VoiceRate (x: obj) = "VoiceRate" => x
        let VoiceStress (x: obj) = "VoiceStress" => x
        let VoiceVolume (x: obj) = "VoiceVolume" => x
        let WhiteSpace (x: string) = "WhiteSpace" => x
        let WhiteSpaceTreatment (x: obj) = "WhiteSpaceTreatment" => x
        let Widows (x: obj) = "Widows" => x
        let Width (x: obj) = "Width" => x
        let WillChange (x: obj) = "WillChange" => x
        let WordBreak (x: obj) = "WordBreak" => x
        let WordSpacing (x: obj) = "WordSpacing" => x
        let WordWrap (x: obj) = "WordWrap" => x
        let WrapFlow (x: obj) = "WrapFlow" => x
        let WrapMargin (x: obj) = "WrapMargin" => x
        let WrapOption (x: obj) = "WrapOption" => x
        let WritingMode (x: obj) = "WritingMode" => x
        let ZIndex (x: obj) = "ZIndex" => x
        let Zoom (x: obj) = "Zoom" => x
        /// If you are searching for a way to provide a value not supported by this DSL then use something like: CSSProp.Custom ("align-content", "center")
        let Custom (x: string * obj) = fst x => snd x
        let Overflow (overflow: string, overflowY: string option) =
            match overflowY with
            | Some value -> CSSProp.Custom ("overflow", stringEnum overflow + " " + stringEnum value)
            | None -> CSSProp.Custom ("overflow", stringEnum overflow)


module Helpers =
    open Props
    open WebSharper.JavaScript
    open WebSharper.Core.AST
    open WebSharper.Core
    open WebSharper
    
    [<Sealed; JavaScript false>]
    type JSConstMacro() =
        inherit WebSharper.Core.Macro()

        override this.TranslateCall(c) =
            let genericType = c.Method.Generics
            match genericType with
            | [t] ->
                match t with
                | ConcreteType d ->
                    match c.Compilation.GetClassInfo(d.Entity) with
                    | None -> MacroError "Could not get compilation information for type"
                    | Some cI ->
                        GlobalAccess cI.Address |> MacroOk
                | _ when t.IsParameter ->
                    MacroNeedsResolvedTypeArg t
                | _ ->
                    MacroError "Non supported type for jsConstructor"
            | _ ->
                MacroError "jsConstructor is called with more than 1 type argument"

    [<Macro(typeof<JSConstMacro>)>]
    let jsConstructor<'T> = X<'T>
    
    [<RequireQualifiedAccess>]
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module React =
        module ElementType =
            [<Inline>]
            let inline ofComponent<'comp, 'props, 'state when 'comp :> React.Component<'props, 'state>> : React.ElementType<'props> =
                jsConstructor<'comp> |> unbox
    
            let inline ofFunction<'props> (f: 'props -> React.Element): React.ElementType<'props> =
                f |> unbox
    
            let inline ofHtmlElement<'props> (name: string): React.ElementType<'props> =
                unbox name
    
            /// Create a React.Element to be rendered from an element type, props and children
            let inline create<'props> (comp: React.ElementType<'props>) (props: 'props) (children: React.Element seq): React.Element =
                React.CreateElement(comp, props, children |> Array.ofSeq)
    
            /// React.Memo is a higher order component. It’s similar to React.PureComponent but for function components instead of classes.
            /// If your function renders the same result given the "same" props (according to `areEqual`), you can wrap it in a call to React.Memo.
            /// React will skip rendering the component, and reuse the last rendered result.
            /// By default it will only shallowly compare complex objects in the props object. If you want control over the comparison, you can use `memoWith`.
            /// This version allow you to control the comparison used instead of the default shallow one by provide a custom comparison function.
            let memoWith<'props> (areEqual: 'props -> 'props -> bool) (render: 'props -> React.Element) =
                React.Memo(render, areEqual)
    
    
    [<AutoOpen>]
    module Helpers =
    
        /// Instantiate a component from a type inheriting React.Component
        /// Example: `ofType<MyComponent,_,_> { myProps = 5 } []`
        [<Inline>]
        let inline ofType<'T,'P,'S when 'T :> React.Component<'P,'S>> (props: 'P) (children: React.Element seq): React.Element =
            React.ElementType.create React.ElementType.ofComponent<'T,_,_> props children
    
        [<System.Obsolete("Use ofType"); Inline>]
        let inline com<'T,'P,'S when 'T :> React.Component<'P,'S>> (props: 'P) (children: React.Element seq): React.Element =
            ofType<'T, 'P, 'S> props children
    
        let inline ofFunction<'P> (f: 'P -> React.Element) (props: 'P) (children: React.Element seq): React.Element =
            React.ElementType.create (React.ElementType.ofFunction f) props children
    
        [<Inline("typeof $0 === 'function'")>]
        let private isFunction (x: obj): bool = X<bool>
    
        [<Inline("typeof $0 === 'object' && !$0[Symbol.iterator]")>]
        let private isNonEnumerableObject (x: obj): bool = X<bool>
    
        /// Normal structural F# comparison, but ignores top-level functions (e.g. Elmish dispatch).
        /// Can be used e.g. with the `FunctionComponent.Of` `memoizeWith` parameter.
        let equalsButFunctions (x: 'a) (y: 'a) =
            if obj.ReferenceEquals(x, y) then
                true
            elif isNonEnumerableObject x && not(isNull(box y)) then
                let keys = JavaScript.Object.Keys x
                let length = keys |> Array.length
                let mutable i = 0
                let mutable result = true
                while i < length && result do
                    let key = keys.[i]
                    i <- i + 1
                    let xValue = x?(key)
                    result <- isFunction xValue || xValue = y?(key)
                result
            else
                (box x) = (box y)
    
        /// Comparison similar to default React.Memo, but ignores functions (e.g. Elmish dispatch).
        /// Performs a memberwise comparison where value types and strings are compared by value,
        /// and other types by reference.
        /// Can be used e.g. with the `FunctionComponent.Of` `memoizeWith` parameter.
        let memoEqualsButFunctions (x: 'a) (y: 'a) =
            if obj.ReferenceEquals(x, y) then
                true
            elif isNonEnumerableObject x && not(isNull(box y)) then
                let keys = JavaScript.Object.Keys x
                let length = keys |> Array.length
                let mutable i = 0
                let mutable result = true
                while i < length && result do
                    let key = keys.[i]
                    i <- i + 1
                    let xValue = x?(key)
                    result <- isFunction xValue || obj.ReferenceEquals(xValue, y?(key))
                result
            else
                false
    
        /// Alias of `ofString`
        let inline str (s: string): React.Element =
            unbox s
    
        /// Cast a string to a React element (erased in runtime)
        let inline ofString (s: string): React.Element =
            str s
            
        /// The equivalent of `sprintf (...) |> str`
        let inline strf format =
            Printf.kprintf str format
    
        /// Cast an option value to a React element (erased in runtime)
        let inline ofOption (o: React.Element option): React.Element =
            match o with Some o -> o | None -> null // Option.toObj(o)
    
        [<System.Obsolete("Use ofOption")>]
        let opt (o: React.Element option): React.Element =
            ofOption o
    
        /// Cast an int to a React element (erased in runtime)
        let inline ofInt (i: int): React.Element =
            unbox i
    
        /// Cast a float to a React element (erased in runtime)
        let inline ofFloat (f: float): React.Element =
            unbox f
    
        /// Returns a list **from .render() method**
        let inline ofList (els: React.Element list): React.Element =
            unbox(List.toArray els)
    
        /// Returns an array **from .render() method**
        let inline ofArray (els: React.Element array): React.Element =
            unbox els
    
        /// A React.Element when you don't want to render anything (null in javascript)
        let nothing: React.Element =
            null
    
        /// Instantiate a DOM React element
        let [<Inline>] domEl (tag: string) (props: WSHTMLProp seq) (children: React.Element seq): React.Element =
            React.CreateElement(tag, keyValueList (props |> Seq.map (fun x -> upcast x)), children |> Array.ofSeq)
    
        /// Instantiate a DOM React element (void)
        let [<Inline>] voidEl (tag: string) (props: WSHTMLProp seq) : React.Element =
            React.CreateElement(tag, keyValueList (props |> Seq.map (fun x -> upcast x)), [||])
    
        /// Instantiate an SVG React element
        let [<Inline>] svgEl (tag: string) (props: WSRProp seq) (children: React.Element seq): React.Element =
            React.CreateElement(tag, keyValueList (props |> Seq.map (fun x -> upcast x)), children |> Array.ofSeq)
    
        /// Instantiate a React fragment
        let [<Inline>] fragment (props: WSFragmentProp seq) (children: React.Element seq): React.Element =
            React.CreateElement(React.Fragment, keyValueList (props |> Seq.map (fun x -> upcast x)), children |> Array.ofSeq)
    
        ///// Accepts a context value to be passed to consuming components that are descendants of this Provider.
        ///// One Provider can be connected to many consumers. Providers can be nested to override values deeper within the tree.
        ///// Important: In SSR, this is ignored, and the DEFAULT value is consumed!
        ///// More info at https://reactjs.org/docs/context.html#contextprovider
        //let inline contextProvider (ctx: IContext<'T>) (value: 'T) (children: React.Element seq): React.Element =
        //    React.CreateElement(ctx?Provider, New ["value", value], children |> Array.ofSeq)
    
        ///// Consumes a context value, either from the nearest parent in the tree, or from the default value.
        ///// Important: in SSR, this will always consume the context DEFAULT value!
        ///// More info at https://reactjs.org/docs/context.html#contextconsumer
        //let inline contextConsumer (ctx: IContext<'T>) (children: 'T -> React.Element): React.Element =
        //    React.CreateElement(ctx?Consumer, null, [|!!children|])
    
        ///// Creates a Context object. When React renders a component that subscribes to this Context
        ///// object it will read the current context value from the closest matching Provider above it
        ///// in the tree. More info at https://reactjs.org/docs/context.html#reactcreatecontext
        //let inline createContext (defaultValue: 'T): IContext<'T> =
        //    React.CreateContext(defaultValue)
    
        /// To be used in constructors of class components
        /// (for function components use `useRef` hook)
        let inline createRef (initialValue: 'T): React.Ref<'T> =
            React.CreateRef(initialValue)
    
        // Class list helpers
        let classBaseList baseClass classes =
            classes
            |> Seq.choose (fun (name, condition) ->
                if condition && not(System.String.IsNullOrEmpty(name)) then Some name
                else None)
            |> Seq.fold (fun state name -> state + " " + name) baseClass
            |> HTMLAttr.ClassName
    
        let classList classes = classBaseList "" classes
    
        /// Finds a DOM element by its ID and mounts the React element there
        /// Important: Not available in SSR
        let inline mountById (domElId: string) (reactEl: React.Element): unit =
            ReactDOM.Render(reactEl, JS.Document.GetElementById(domElId))

        /// Finds the first DOM element matching a CSS selector and mounts the React element there
        /// Important: Not available in SSR
        let inline mountBySelector (domElSelector: string) (reactEl: React.Element): unit =
            ReactDOM.Render(reactEl, JS.Document.QuerySelector(domElSelector))
    

module Standard =
    open Helpers

    let [<WebSharper.Inline>] a props children = domEl "a" props children
    let [<WebSharper.Inline>] abbr props children = domEl "abbr" props children
    let [<WebSharper.Inline>] address props children = domEl "address" props children
    let [<WebSharper.Inline>] article props children = domEl "article" props children
    let [<WebSharper.Inline>] aside props children = domEl "aside" props children
    let [<WebSharper.Inline>] audio props children = domEl "audio" props children
    let [<WebSharper.Inline>] b props children = domEl "b" props children
    let [<WebSharper.Inline>] bdi props children = domEl "bdi" props children
    let [<WebSharper.Inline>] bdo props children = domEl "bdo" props children
    let [<WebSharper.Inline>] big props children = domEl "big" props children
    let [<WebSharper.Inline>] blockquote props children = domEl "blockquote" props children
    let [<WebSharper.Inline>] body props children = domEl "body" props children
    let [<WebSharper.Inline>] button props children = domEl "button" props children
    let [<WebSharper.Inline>] canvas props children = domEl "canvas" props children
    let [<WebSharper.Inline>] caption props children = domEl "caption" props children
    let [<WebSharper.Inline>] cite props children = domEl "cite" props children
    let [<WebSharper.Inline>] code props children = domEl "code" props children
    let [<WebSharper.Inline>] colgroup props children = domEl "colgroup" props children
    let [<WebSharper.Inline>] data props children = domEl "data" props children
    let [<WebSharper.Inline>] datalist props children = domEl "datalist" props children
    let [<WebSharper.Inline>] dd props children = domEl "dd" props children
    let [<WebSharper.Inline>] del props children = domEl "del" props children
    let [<WebSharper.Inline>] details props children = domEl "details" props children
    let [<WebSharper.Inline>] dfn props children = domEl "dfn" props children
    let [<WebSharper.Inline>] dialog props children = domEl "dialog" props children
    let [<WebSharper.Inline>] div props children = domEl "div" props children
    let [<WebSharper.Inline>] dl props children = domEl "dl" props children
    let [<WebSharper.Inline>] dt props children = domEl "dt" props children
    let [<WebSharper.Inline>] em props children = domEl "em" props children
    let [<WebSharper.Inline>] fieldset props children = domEl "fieldset" props children
    let [<WebSharper.Inline>] figcaption props children = domEl "figcaption" props children
    let [<WebSharper.Inline>] figure props children = domEl "figure" props children
    let [<WebSharper.Inline>] footer props children = domEl "footer" props children
    let [<WebSharper.Inline>] form props children = domEl "form" props children
    let [<WebSharper.Inline>] h1 props children = domEl "h1" props children
    let [<WebSharper.Inline>] h2 props children = domEl "h2" props children
    let [<WebSharper.Inline>] h3 props children = domEl "h3" props children
    let [<WebSharper.Inline>] h4 props children = domEl "h4" props children
    let [<WebSharper.Inline>] h5 props children = domEl "h5" props children
    let [<WebSharper.Inline>] h6 props children = domEl "h6" props children
    let [<WebSharper.Inline>] head props children = domEl "head" props children
    let [<WebSharper.Inline>] header props children = domEl "header" props children
    let [<WebSharper.Inline>] hgroup props children = domEl "hgroup" props children
    let [<WebSharper.Inline>] html props children = domEl "html" props children
    let [<WebSharper.Inline>] i props children = domEl "i" props children
    let [<WebSharper.Inline>] iframe props children = domEl "iframe" props children
    let [<WebSharper.Inline>] ins props children = domEl "ins" props children
    let [<WebSharper.Inline>] kbd props children = domEl "kbd" props children
    let [<WebSharper.Inline>] label props children = domEl "label" props children
    let [<WebSharper.Inline>] legend props children = domEl "legend" props children
    let [<WebSharper.Inline>] li props children = domEl "li" props children
    let [<WebSharper.Inline>] main props children = domEl "main" props children
    let [<WebSharper.Inline>] map props children = domEl "map" props children
    let [<WebSharper.Inline>] mark props children = domEl "mark" props children
    let [<WebSharper.Inline>] menu props children = domEl "menu" props children
    let [<WebSharper.Inline>] meter props children = domEl "meter" props children
    let [<WebSharper.Inline>] nav props children = domEl "nav" props children
    let [<WebSharper.Inline>] noscript props children = domEl "noscript" props children
    let [<WebSharper.Inline>] object props children = domEl "object" props children
    let [<WebSharper.Inline>] ol props children = domEl "ol" props children
    let [<WebSharper.Inline>] optgroup props children = domEl "optgroup" props children
    let [<WebSharper.Inline>] option props children = domEl "option" props children
    let [<WebSharper.Inline>] output props children = domEl "output" props children
    let [<WebSharper.Inline>] p props children = domEl "p" props children
    let [<WebSharper.Inline>] picture props children = domEl "picture" props children
    let [<WebSharper.Inline>] pre props children = domEl "pre" props children
    let [<WebSharper.Inline>] progress props children = domEl "progress" props children
    let [<WebSharper.Inline>] q props children = domEl "q" props children
    let [<WebSharper.Inline>] rp props children = domEl "rp" props children
    let [<WebSharper.Inline>] rt props children = domEl "rt" props children
    let [<WebSharper.Inline>] ruby props children = domEl "ruby" props children
    let [<WebSharper.Inline>] s props children = domEl "s" props children
    let [<WebSharper.Inline>] samp props children = domEl "samp" props children
    let [<WebSharper.Inline>] script props children = domEl "script" props children
    let [<WebSharper.Inline>] section props children = domEl "section" props children
    let [<WebSharper.Inline>] select props children = domEl "select" props children
    let [<WebSharper.Inline>] small props children = domEl "small" props children
    let [<WebSharper.Inline>] span props children = domEl "span" props children
    let [<WebSharper.Inline>] strong props children = domEl "strong" props children
    let [<WebSharper.Inline>] style props children = domEl "style" props children
    let [<WebSharper.Inline>] sub props children = domEl "sub" props children
    let [<WebSharper.Inline>] summary props children = domEl "summary" props children
    let [<WebSharper.Inline>] sup props children = domEl "sup" props children
    let [<WebSharper.Inline>] table props children = domEl "table" props children
    let [<WebSharper.Inline>] tbody props children = domEl "tbody" props children
    let [<WebSharper.Inline>] td props children = domEl "td" props children
    let [<WebSharper.Inline>] textarea props children = domEl "textarea" props children
    let [<WebSharper.Inline>] tfoot props children = domEl "tfoot" props children
    let [<WebSharper.Inline>] th props children = domEl "th" props children
    let [<WebSharper.Inline>] thead props children = domEl "thead" props children
    let [<WebSharper.Inline>] time props children = domEl "time" props children
    let [<WebSharper.Inline>] title props children = domEl "title" props children
    let [<WebSharper.Inline>] tr props children = domEl "tr" props children
    let [<WebSharper.Inline>] u props children = domEl "u" props children
    let [<WebSharper.Inline>] ul props children = domEl "ul" props children
    let [<WebSharper.Inline>] var props children = domEl "var" props children
    let [<WebSharper.Inline>] video props children = domEl "video" props children
    
    // Void element
    let [<WebSharper.Inline>] area props = voidEl "area" props
    let [<WebSharper.Inline>] ``base`` props = voidEl "base" props
    let [<WebSharper.Inline>] br props = voidEl "br" props
    let [<WebSharper.Inline>] col props = voidEl "col" props
    let [<WebSharper.Inline>] embed props = voidEl "embed" props
    let [<WebSharper.Inline>] hr props = voidEl "hr" props
    let [<WebSharper.Inline>] img props = voidEl "img" props
    let [<WebSharper.Inline>] input props = voidEl "input" props
    let [<WebSharper.Inline>] keygen props = voidEl "keygen" props
    let [<WebSharper.Inline>] link props = voidEl "link" props
    let [<WebSharper.Inline>] menuitem props = voidEl "menuitem" props
    let [<WebSharper.Inline>] meta props = voidEl "meta" props
    let [<WebSharper.Inline>] param props = voidEl "param" props
    let [<WebSharper.Inline>] source props = voidEl "source" props
    let [<WebSharper.Inline>] track props = voidEl "track" props
    let [<WebSharper.Inline>] wbr props = voidEl "wbr" props
    
    // SVG elements
    let [<WebSharper.Inline>] svg props children = svgEl "svg" props children
    let [<WebSharper.Inline>] circle props children = svgEl "circle" props children
    let [<WebSharper.Inline>] clipPath props children = svgEl "clipPath" props children
    let [<WebSharper.Inline>] defs props children = svgEl "defs" props children
    let [<WebSharper.Inline>] ellipse props children = svgEl "ellipse" props children
    let [<WebSharper.Inline>] g props children = svgEl "g" props children
    let [<WebSharper.Inline>] image props children = svgEl "image" props children
    let [<WebSharper.Inline>] line props children = svgEl "line" props children
    let [<WebSharper.Inline>] linearGradient props children = svgEl "linearGradient" props children
    let [<WebSharper.Inline>] mask props children = svgEl "mask" props children
    let [<WebSharper.Inline>] path props children = svgEl "path" props children
    let [<WebSharper.Inline>] pattern props children = svgEl "pattern" props children
    let [<WebSharper.Inline>] polygon props children = svgEl "polygon" props children
    let [<WebSharper.Inline>] polyline props children = svgEl "polyline" props children
    let [<WebSharper.Inline>] radialGradient props children = svgEl "radialGradient" props children
    let [<WebSharper.Inline>] rect props children = svgEl "rect" props children
    let [<WebSharper.Inline>] stop props children = svgEl "stop" props children
    let [<WebSharper.Inline>] text props children = svgEl "text" props children
    let [<WebSharper.Inline>] tspan props children = svgEl "tspan" props children

[<assembly: WebSharper.JavaScript>]
do ()