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
    
    type DOMAttr =
        | DangerouslySetInnerHTML of DangerousHtml
        | OnCut of (ClipboardEvent -> unit)
        | OnPaste of (ClipboardEvent -> unit)
        | OnCompositionEnd of (CompositionEvent -> unit)
        | OnCompositionStart of (CompositionEvent -> unit)
        | OnCopy of (ClipboardEvent -> unit)
        | OnCompositionUpdate of (CompositionEvent -> unit)
        | OnFocus of (FocusEvent -> unit)
        | OnBlur of (FocusEvent -> unit)
        | OnChange of (Dom.Event -> unit)
        | OnInput of (Dom.Event -> unit)
        | OnSubmit of (Dom.Event -> unit)
        | OnReset of (Dom.Event -> unit)
        | OnLoad of (Dom.Event -> unit)
        | OnError of (Dom.Event -> unit)
        | OnKeyDown of (KeyboardEvent -> unit)
        | OnKeyPress of (KeyboardEvent -> unit)
        | OnKeyUp of (KeyboardEvent -> unit)
        | OnAbort of (Dom.Event -> unit)
        | OnCanPlay of (Dom.Event -> unit)
        | OnCanPlayThrough of (Dom.Event -> unit)
        | OnDurationChange of (Dom.Event -> unit)
        | OnEmptied of (Dom.Event -> unit)
        | OnEncrypted of (Dom.Event -> unit)
        | OnEnded of (Dom.Event -> unit)
        | OnLoadedData of (Dom.Event -> unit)
        | OnLoadedMetadata of (Dom.Event -> unit)
        | OnLoadStart of (Dom.Event -> unit)
        | OnPause of (Dom.Event -> unit)
        | OnPlay of (Dom.Event -> unit)
        | OnPlaying of (Dom.Event -> unit)
        | OnProgress of (Dom.Event -> unit)
        | OnRateChange of (Dom.Event -> unit)
        | OnSeeked of (Dom.Event -> unit)
        | OnSeeking of (Dom.Event -> unit)
        | OnStalled of (Dom.Event -> unit)
        | OnSuspend of (Dom.Event -> unit)
        | OnTimeUpdate of (Dom.Event -> unit)
        | OnVolumeChange of (Dom.Event -> unit)
        | OnWaiting of (Dom.Event -> unit)
        | OnClick of (MouseEvent -> unit)
        | OnContextMenu of (MouseEvent -> unit)
        | OnDoubleClick of (MouseEvent -> unit)
        //| OnDrag of (DragEvent -> unit)
        //| OnDragEnd of (DragEvent -> unit)
        //| OnDragEnter of (DragEvent -> unit)
        //| OnDragExit of (DragEvent -> unit)
        //| OnDragLeave of (DragEvent -> unit)
        //| OnDragOver of (DragEvent -> unit)
        //| OnDragStart of (DragEvent -> unit)
        //| OnDrop of (DragEvent -> unit)
        | OnMouseDown of (MouseEvent -> unit)
        | OnMouseEnter of (MouseEvent -> unit)
        | OnMouseLeave of (MouseEvent -> unit)
        | OnMouseMove of (MouseEvent -> unit)
        | OnMouseOut of (MouseEvent -> unit)
        | OnMouseOver of (MouseEvent -> unit)
        | OnMouseUp of (MouseEvent -> unit)
        | OnSelect of (Dom.Event -> unit)
        | OnTouchCancel of (TouchEvent -> unit)
        | OnTouchEnd of (TouchEvent -> unit)
        | OnTouchMove of (TouchEvent -> unit)
        | OnTouchStart of (TouchEvent -> unit)
        | OnScroll of (UIEvent -> unit)
        | OnWheel of (WheelEvent -> unit)
        | OnAnimationStart of (AnimationEvent -> unit)
        | OnAnimationEnd of (AnimationEvent -> unit)
        | OnAnimationIteration of (AnimationEvent -> unit)
        | OnTransitionEnd of (TransitionEvent -> unit)
        | Custom of string * obj
        interface IHTMLProp
    
    type SVGAttr =
        | ClipPath of string
        | Cx of obj
        | Cy of obj
        | D of string
        | Dx of obj
        | Dy of obj
        | Fill of string
        | FillOpacity of obj
        | FontFamily of string
        | FontSize of obj
        | Fx of obj
        | Fy of obj
        | GradientTransform of string
        | GradientUnits of string
        | Height of obj
        | MarkerEnd of string
        | MarkerMid of string
        | MarkerStart of string
        | Offset of obj
        | Opacity of obj
        | PatternContentUnits of string
        | PatternUnits of string
        | Points of string
        | PreserveAspectRatio of string
        | R of obj
        | Rx of obj
        | Ry of obj
        | SpreadMethod of string
        | StopColor of string
        | StopOpacity of obj
        | Stroke of string
        | StrokeDasharray of string
        | StrokeDashoffset of string
        | StrokeLinecap of string
        | StrokeMiterlimit of string
        | StrokeOpacity of obj
        | StrokeWidth of obj
        | TextAnchor of string
        | Transform of string
        | Version of string
        | ViewBox of string
        | Width of obj
        | X1 of obj
        | X2 of obj
        | X of obj
        | XlinkActuate of string
        | XlinkArcrole of string
        | XlinkHref of string
        | XlinkRole of string
        | XlinkShow of string
        | XlinkTitle of string
        | XlinkType of string
        | XmlBase of string
        | XmlLang of string
        | XmlSpace of string
        | Y1 of obj
        | Y2 of obj
        | Y of obj
        /// If you are searching for a way to provide a value not supported by this DSL then use something like: CSSProp.Custom ("align-content", "center")
        | Custom of string * obj
        interface IProp
    
    type HTMLAttr =
        | DefaultChecked of bool
        | DefaultValue of obj
        | Accept of string
        | AcceptCharset of string
        | AccessKey of string
        | Action of string
        | AllowFullScreen of bool
        | AllowTransparency of bool
        | Alt of string
        | [<CompiledName("aria-atomic")>] AriaAtomic of bool
        | [<CompiledName("aria-busy")>] AriaBusy of bool
        | [<CompiledName("aria-checked")>] AriaChecked of bool
        | [<CompiledName("aria-colcount")>] AriaColcount of int
        | [<CompiledName("aria-colindex")>] AriaColindex of int
        | [<CompiledName("aria-colspan")>] AriaColspan of int
        | [<CompiledName("aria-controls")>] AriaControls of string
        | [<CompiledName("aria-current")>] AriaCurrent of string
        | [<CompiledName("aria-describedby")>] AriaDescribedBy of string
        | [<CompiledName("aria-details")>] AriaDetails of string
        | [<CompiledName("aria-disabled")>] AriaDisabled of bool
        | [<CompiledName("aria-errormessage")>] AriaErrormessage of string
        | [<CompiledName("aria-expanded")>] AriaExpanded of bool
        | [<CompiledName("aria-flowto")>] AriaFlowto of string
        | [<CompiledName("aria-haspopup")>] AriaHasPopup of bool
        | [<CompiledName("aria-hidden")>] AriaHidden of bool
        | [<CompiledName("aria-invalid")>] AriaInvalid of string
        | [<CompiledName("aria-keyshortcuts")>] AriaKeyshortcuts of string
        | [<CompiledName("aria-label")>] AriaLabel of string
        | [<CompiledName("aria-labelledby")>] AriaLabelledby of string
        | [<CompiledName("aria-level")>] AriaLevel of int
        | [<CompiledName("aria-live")>] AriaLive of string
        | [<CompiledName("aria-modal")>] AriaModal of bool
        | [<CompiledName("aria-multiline")>] AriaMultiline of bool
        | [<CompiledName("aria-multiselectable")>] AriaMultiselectable of bool
        | [<CompiledName("aria-orientation")>] AriaOrientation of string
        | [<CompiledName("aria-owns")>] AriaOwns of string
        | [<CompiledName("aria-placeholder")>] AriaPlaceholder of string
        | [<CompiledName("aria-posinset")>] AriaPosinset of int
        | [<CompiledName("aria-pressed")>] AriaPressed of bool
        | [<CompiledName("aria-readonly")>] AriaReadonly of bool
        | [<CompiledName("aria-relevant")>] AriaRelevant of string
        | [<CompiledName("aria-required")>] AriaRequired of bool
        | [<CompiledName("aria-roledescription")>] AriaRoledescription of string
        | [<CompiledName("aria-rowcount")>] AriaRowcount of int
        | [<CompiledName("aria-rowindex")>] AriaRowindex of int
        | [<CompiledName("aria-rowspan")>] AriaRowspan of int
        | [<CompiledName("aria-selected")>] AriaSelected of bool
        | [<CompiledName("aria-setsize")>] AriaSetsize of int
        | [<CompiledName("aria-sort")>] AriaSort of string
        | [<CompiledName("aria-valuemax")>] AriaValuemax of float
        | [<CompiledName("aria-valuemin")>] AriaValuemin of float
        | [<CompiledName("aria-valuenow")>] AriaValuenow of float
        | [<CompiledName("aria-valuetext")>] AriaValuetext of string
        | Async of bool
        | AutoComplete of string
        | AutoFocus of bool
        | AutoPlay of bool
        | Capture of bool
        | CellPadding of obj
        | CellSpacing of obj
        | CharSet of string
        | Challenge of string
        | Checked of bool
        | ClassID of string
        | ClassName of string
        /// Alias of ClassName
        | [<CompiledName("className")>] Class of string
        | Cols of int
        | ColSpan of int
        | Content of string
        | ContentEditable of bool
        | ContextMenu of string
        | Controls of bool
        | Coords of string
        | CrossOrigin of string
        // | Data of string
        | [<CompiledName("data-toggle")>] DataToggle of string
        | DateTime of string
        | Default of bool
        | Defer of bool
        | Dir of string
        | Disabled of bool
        | Download of obj
        | Draggable of bool
        | EncType of string
        | Form of string
        | FormAction of string
        | FormEncType of string
        | FormMethod of string
        | FormNoValidate of bool
        | FormTarget of string
        | FrameBorder of obj
        | Headers of string
        | Height of obj
        | Hidden of bool
        | High of float
        | Href of string
        | HrefLang of string
        | HtmlFor of string
        | HttpEquiv of string
        | Icon of string
        | Id of string
        | InputMode of string
        | Integrity of string
        | Is of string
        | KeyParams of string
        | KeyType of string
        | Kind of string
        | Label of string
        | Lang of string
        | List of string
        | Loop of bool
        | Low of float
        | Manifest of string
        | MarginHeight of float
        | MarginWidth of float
        | Max of obj
        | MaxLength of float
        | Media of string
        | MediaGroup of string
        | Method of string
        | Min of obj
        | MinLength of float
        | Multiple of bool
        | Muted of bool
        | Name of string
        | NoValidate of bool
        | Open of bool
        | Optimum of float
        | Pattern of string
        | Placeholder of string
        | Poster of string
        | Preload of string
        | RadioGroup of string
        | ReadOnly of bool
        | Rel of string
        | Required of bool
        | Role of string
        | Rows of int
        | RowSpan of int
        | Sandbox of string
        | Scope of string
        | Scoped of bool
        | Scrolling of string
        | Seamless of bool
        | Selected of bool
        | Shape of string
        | Size of float
        | Sizes of string
        | Span of float
        | SpellCheck of bool
        | Src of string
        | SrcDoc of string
        | SrcLang of string
        | SrcSet of string
        | Start of float
        | Step of obj
        | Summary of string
        | TabIndex of int
        | Target of string
        | Title of string
        | Type of string
        | UseMap of string
        | Value of obj
        /// Compiles to same prop as `Value`. Intended for `select` elements
        /// with `Multiple` prop set to `true`.
        | [<CompiledName("value")>] ValueMultiple of string[]
        | Width of obj
        | Wmode of string
        | Wrap of string
        | About of string
        | Datatype of string
        | Inlist of obj
        | Prefix of string
        | Property of string
        | Resource of string
        | Typeof of string
        | Vocab of string
        | AutoCapitalize of string
        | AutoCorrect of string
        | AutoSave of string
        // | Color of string // Conflicts with CSSProp, shouldn't be used in HTML5
        | ItemProp of string
        | ItemScope of bool
        | ItemType of string
        | ItemID of string
        | ItemRef of string
        | Results of float
        | Security of string
        | Unselectable of bool
        | Custom of string * obj
        interface IHTMLProp

    let lowerCaseStr (str: string) =
        if String.IsNullOrEmpty(str) then
            str
        else
            let lC = str[0] |> Char.ToLower |> string
            let rest = str[1..]
            sprintf "%s%s" lC rest

    let keyValueList (list: 'T seq) =
        New (list |> Seq.map (fun x -> (string x).Split([|' '|]).[0], box x))
    
    
    let inline Style (css: CSSProp list): HTMLAttr =
        As<HTMLAttr>("style", keyValueList css)
    
    let inline Data (key: string, value: obj): HTMLAttr =
        As<HTMLAttr>("data-" + key, value)

    
    type CSSProp =
        | AlignContent of string
        | AlignItems of string
        | AlignSelf of string
        | AlignmentAdjust of obj
        | AlignmentBaseline of string
        | All of string
        | Animation of obj
        | AnimationDelay of obj
        | AnimationDirection of string
        | AnimationDuration of obj
        | AnimationFillMode of string
        | AnimationIterationCount of obj
        | AnimationName of obj
        | AnimationPlayState of obj
        | AnimationTimingFunction of obj
        | Appearance of string
        | BackfaceVisibility of string
        | Background of obj
        | BackgroundAttachment of obj
        | BackgroundBlendMode of obj
        | BackgroundClip of obj
        | BackgroundColor of obj
        | BackgroundComposite of obj
        | BackgroundImage of obj
        | BackgroundOrigin of obj
        | BackgroundPosition of obj
        | BackgroundPositionX of obj
        | BackgroundPositionY of obj
        | BackgroundRepeat of obj
        | BackgroundSize of obj
        | BaselineShift of obj
        | Behavior of obj
        | BlockSize of obj
        | Border of obj
        | BorderBlockEnd of obj
        | BorderBlockEndColor of obj
        | BorderBlockEndStyle of obj
        | BorderBlockEndWidth of obj
        | BorderBlockStart of obj
        | BorderBlockStartColor of obj
        | BorderBlockStartStyle of obj
        | BorderBlockStartWidth of obj
        | BorderBottom of obj
        | BorderBottomColor of obj
        | BorderBottomLeftRadius of obj
        | BorderBottomRightRadius of obj
        | BorderBottomStyle of obj
        | BorderBottomWidth of obj
        | BorderCollapse of obj
        | BorderColor of obj
        | BorderCornerShape of obj
        | BorderImage of obj
        | BorderImageOutset of obj
        | BorderImageRepeat of obj
        | BorderImageSlice of obj
        | BorderImageSource of obj
        | BorderImageWidth of obj
        | BorderInlineEnd of obj
        | BorderInlineEndColor of obj
        | BorderInlineEndStyle of obj
        | BorderInlineEndWidth of obj
        | BorderInlineStart of obj
        | BorderInlineStartColor of obj
        | BorderInlineStartStyle of obj
        | BorderInlineStartWidth of obj
        | BorderLeft of obj
        | BorderLeftColor of obj
        | BorderLeftStyle of obj
        | BorderLeftWidth of obj
        | BorderRadius of obj
        | BorderRight of obj
        | BorderRightColor of obj
        | BorderRightStyle of obj
        | BorderRightWidth of obj
        | BorderSpacing of obj
        | BorderStyle of obj
        | BorderTop of obj
        | BorderTopColor of obj
        | BorderTopLeftRadius of obj
        | BorderTopRightRadius of obj
        | BorderTopStyle of obj
        | BorderTopWidth of obj
        | BorderWidth of obj
        | Bottom of obj
        | BoxAlign of string
        | BoxDecorationBreak of string
        | BoxDirection of string
        | BoxFlex of obj
        | BoxFlexGroup of obj
        | BoxLineProgression of obj
        | BoxLines of obj
        | BoxOrdinalGroup of obj
        | BoxShadow of obj
        | BoxSizing of string
        | BreakAfter of string
        | BreakBefore of string
        | BreakInside of string
        | CaptionSide of string
        | CaretColor of obj
        | Clear of string
        | Clip of obj
        | ClipPath of obj
        | ClipRule of obj
        | Color of obj
        | ColorInterpolation of obj
        | ColorInterpolationFilters of obj
        | ColorProfile of obj
        | ColorRendering of obj
        | ColumnCount of obj
        | ColumnFill of obj
        | ColumnGap of obj
        | ColumnRule of obj
        | ColumnRuleColor of obj
        | ColumnRuleStyle of obj
        | ColumnRuleWidth of obj
        | ColumnSpan of obj
        | ColumnWidth of obj
        | Columns of obj
        | Content of obj
        | CounterIncrement of obj
        | CounterReset of obj
        | Cue of obj
        | CueAfter of obj
        | Cursor of obj
        | Direction of string
        | Display of string
        | DominantBaseline of obj
        | EmptyCells of obj
        | EnableBackground of obj
        | Fill of obj
        | FillOpacity of obj
        | FillRule of obj
        | Filter of obj
        | Flex of obj
        | FlexAlign of obj
        | FlexBasis of obj
        | FlexDirection of obj
        | FlexFlow of obj
        | FlexGrow of obj
        | FlexItemAlign of obj
        | FlexLinePack of obj
        | FlexOrder of obj
        | FlexShrink of obj
        | FlexWrap of obj
        | Float of string
        | FloodColor of obj
        | FloodOpacity of obj
        | FlowFrom of obj
        | Font of obj
        | FontFamily of obj
        | FontFeatureSettings of obj
        | FontKerning of obj
        | FontLanguageOverride of obj
        | FontSize of obj
        | FontSizeAdjust of obj
        | FontStretch of obj
        | FontStyle of obj
        | FontSynthesis of obj
        | FontVariant of obj
        | FontVariantAlternates of obj
        | FontVariantCaps of obj
        | FontVariantEastAsian of obj
        | FontVariantLigatures of obj
        | FontVariantNumeric of obj
        | FontVariantPosition of obj
        | FontWeight of obj
        | GlyphOrientationHorizontal of obj
        | GlyphOrientationVertical of obj
        | Grid of obj
        | GridArea of obj
        | GridAutoColumns of obj
        | GridAutoFlow of obj
        | GridAutoRows of obj
        | GridColumn of obj
        | GridColumnEnd of obj
        | GridColumnGap of obj
        | GridColumnStart of obj
        | GridGap of obj
        | GridRow of obj
        | GridRowEnd of obj
        | GridRowGap of obj
        | GridRowPosition of obj
        | GridRowSpan of obj
        | GridRowStart of obj
        | GridTemplate of obj
        | GridTemplateAreas of obj
        | GridTemplateColumns of obj
        | GridTemplateRows of obj
        | HangingPunctuation of obj
        | Height of obj
        | HyphenateLimitChars of obj
        | HyphenateLimitLines of obj
        | HyphenateLimitZone of obj
        | Hyphens of obj
        | ImageOrientation of obj // Likely to be deprecated in the near future
        | ImageRendering of string
        | ImageResolution of obj
        | ImeMode of obj
        | InlineSize of obj
        | Isolation of obj
        | JustifyContent of obj
        | JustifySelf of string
        | Kerning of obj
        | LayoutGrid of obj
        | LayoutGridChar of obj
        | LayoutGridLine of obj
        | LayoutGridMode of obj
        | LayoutGridType of obj
        | Left of obj
        | LetterSpacing of obj
        | LightingColor of obj
        | LineBreak of obj
        | LineClamp of obj
        | LineHeight of obj
        | ListStyle of obj
        | ListStyleImage of obj
        | ListStylePosition of obj
        | ListStyleType of obj
        | Margin of obj
        | MarginBlockEnd of obj
        | MarginBlockStart of obj
        | MarginBottom of obj
        | MarginInlineEnd of obj
        | MarginInlineStart of obj
        | MarginLeft of obj
        | MarginRight of obj
        | MarginTop of obj
        | MarkerEnd of obj
        | MarkerMid of obj
        | MarkerStart of obj
        | MarqueeDirection of obj
        | MarqueeStyle of obj
        | Mask of obj
        | MaskBorder of obj
        | MaskBorderRepeat of obj
        | MaskBorderSlice of obj
        | MaskBorderSource of obj
        | MaskBorderWidth of obj
        | MaskClip of obj
        | MaskComposite of obj
        | MaskImage of obj
        | MaskMode of obj
        | MaskOrigin of obj
        | MaskPosition of obj
        | MaskRepeat of obj
        | MaskSize of obj
        | MaskType of obj
        | MaxFontSize of obj
        | MaxHeight of obj
        | MaxWidth of obj
        | MinBlockSize of obj
        | MinHeight of obj
        | MinInlineSize of obj
        | MinWidth of obj
        | MixBlendMode of obj
        | ObjectFit of obj
        | ObjectPosition of obj
        | OffsetBlockEnd of obj
        | OffsetBlockStart of obj
        | OffsetInlineEnd of obj
        | OffsetInlineStart of obj
        | Opacity of obj
        | Order of obj
        | Orphans of obj
        | Outline of obj
        | OutlineColor of obj
        | OutlineOffset of obj
        | OutlineStyle of obj
        | OutlineWidth of obj
        | OverflowStyle of obj
        | OverflowWrap of obj
        | OverflowX of string
        | OverflowY of string
        | Padding of obj
        | PaddingBlockEnd of obj
        | PaddingBlockStart of obj
        | PaddingBottom of obj
        | PaddingInlineEnd of obj
        | PaddingInlineStart of obj
        | PaddingLeft of obj
        | PaddingRight of obj
        | PaddingTop of obj
        | PageBreakAfter of obj
        | PageBreakBefore of obj
        | PageBreakInside of obj
        | Pause of obj
        | PauseAfter of obj
        | PauseBefore of obj
        | Perspective of obj
        | PerspectiveOrigin of obj
        | PointerEvents of obj
        | Position of string
        | PunctuationTrim of obj
        | Quotes of obj
        | RegionFragment of obj
        | Resize of obj
        | RestAfter of obj
        | RestBefore of obj
        | Right of obj
        | RubyAlign of obj
        | RubyMerge of obj
        | RubyPosition of obj
        | ScrollBehavior of obj
        | ScrollSnapCoordinate of obj
        | ScrollSnapDestination of obj
        | ScrollSnapType of obj
        | ShapeImageThreshold of obj
        | ShapeInside of obj
        | ShapeMargin of obj
        | ShapeOutside of obj
        | ShapeRendering of obj
        | Speak of obj
        | SpeakAs of obj
        | StopColor of obj
        | StopOpacity of obj
        | Stroke of obj
        | StrokeDasharray of obj
        | StrokeDashoffset of obj
        | StrokeLinecap of obj
        | StrokeLinejoin of obj
        | StrokeMiterlimit of obj
        | StrokeOpacity of obj
        | StrokeWidth of obj
        | TabSize of obj
        | TableLayout of obj
        | TextAlign of string
        | TextAlignLast of obj
        | TextAnchor of obj
        | TextCombineUpright of obj
        | TextDecoration of obj
        | TextDecorationColor of obj
        | TextDecorationLine of obj
        | TextDecorationLineThrough of obj
        | TextDecorationNone of obj
        | TextDecorationOverline of obj
        | TextDecorationSkip of obj
        | TextDecorationStyle of obj
        | TextDecorationUnderline of obj
        | TextEmphasis of obj
        | TextEmphasisColor of obj
        | TextEmphasisPosition of obj
        | TextEmphasisStyle of obj
        | TextHeight of obj
        | TextIndent of obj
        | TextJustify of obj
        | TextJustifyTrim of obj
        | TextKashidaSpace of obj
        | TextLineThrough of obj
        | TextLineThroughColor of obj
        | TextLineThroughMode of obj
        | TextLineThroughStyle of obj
        | TextLineThroughWidth of obj
        | TextOrientation of obj
        | TextOverflow of obj
        | TextOverline of obj
        | TextOverlineColor of obj
        | TextOverlineMode of obj
        | TextOverlineStyle of obj
        | TextOverlineWidth of obj
        | TextRendering of obj
        | TextScript of obj
        | TextShadow of obj
        | TextTransform of obj
        | TextUnderlinePosition of obj
        | TextUnderlineStyle of obj
        | Top of obj
        | TouchAction of obj
        | Transform of obj
        | TransformBox of obj
        | TransformOrigin of obj
        | TransformOriginZ of obj
        | TransformStyle of obj
        | Transition of obj
        | TransitionDelay of obj
        | TransitionDuration of obj
        | TransitionProperty of obj
        | TransitionTimingFunction of obj
        | UnicodeBidi of obj
        | UnicodeRange of obj
        | UserFocus of obj
        | UserInput of obj
        | UserSelect of string
        | VerticalAlign of obj
        | Visibility of obj
        | VoiceBalance of obj
        | VoiceDuration of obj
        | VoiceFamily of obj
        | VoicePitch of obj
        | VoiceRange of obj
        | VoiceRate of obj
        | VoiceStress of obj
        | VoiceVolume of obj
        | WhiteSpace of string
        | WhiteSpaceTreatment of obj
        | Widows of obj
        | Width of obj
        | WillChange of obj
        | WordBreak of obj
        | WordSpacing of obj
        | WordWrap of obj
        | WrapFlow of obj
        | WrapMargin of obj
        | WrapOption of obj
        | WritingMode of obj
        | ZIndex of obj
        | Zoom of obj
        /// If you are searching for a way to provide a value not supported by this DSL then use something like: CSSProp.Custom ("align-content", "center")
        | Custom of string * obj
        static member Overflow (overflow: string, ?overflowY: string) =
            match overflowY with
            | Some value -> CSSProp.Custom ("overflow", stringEnum overflow + " " + stringEnum value)
            | None -> CSSProp.Custom ("overflow", stringEnum overflow)


module Helpers =
    open Props
    open WebSharper.JavaScript
    open WebSharper
    
    
    [<RequireQualifiedAccess>]
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module React =
        module ElementType =
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
        let inline ofType<'T,'P,'S when 'T :> React.Component<'P,'S>> (props: 'P) (children: React.Element seq): React.Element =
            React.ElementType.create React.ElementType.ofComponent<'T,_,_> props children
    
        [<System.Obsolete("Use ofType")>]
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
        let inline domEl (tag: string) (props: IHTMLProp seq) (children: React.Element seq): React.Element =
            React.CreateElement(tag, keyValueList props, children |> Array.ofSeq)
    
        /// Instantiate a DOM React element (void)
        let inline voidEl (tag: string) (props: IHTMLProp seq) : React.Element =
            React.CreateElement(tag, keyValueList props, [||])
    
        /// Instantiate an SVG React element
        let inline svgEl (tag: string) (props: IProp seq) (children: React.Element seq): React.Element =
            React.CreateElement(tag, keyValueList props, children |> Array.ofSeq)
    
        /// Instantiate a React fragment
        let inline fragment (props: IFragmentProp seq) (children: React.Element seq): React.Element =
            React.CreateElement(React.Fragment, keyValueList props, children |> Array.ofSeq)
    
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
            |> ClassName
    
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

    let inline a props children = domEl "a" props children
    let inline abbr props children = domEl "abbr" props children
    let inline address props children = domEl "address" props children
    let inline article props children = domEl "article" props children
    let inline aside props children = domEl "aside" props children
    let inline audio props children = domEl "audio" props children
    let inline b props children = domEl "b" props children
    let inline bdi props children = domEl "bdi" props children
    let inline bdo props children = domEl "bdo" props children
    let inline big props children = domEl "big" props children
    let inline blockquote props children = domEl "blockquote" props children
    let inline body props children = domEl "body" props children
    let inline button props children = domEl "button" props children
    let inline canvas props children = domEl "canvas" props children
    let inline caption props children = domEl "caption" props children
    let inline cite props children = domEl "cite" props children
    let inline code props children = domEl "code" props children
    let inline colgroup props children = domEl "colgroup" props children
    let inline data props children = domEl "data" props children
    let inline datalist props children = domEl "datalist" props children
    let inline dd props children = domEl "dd" props children
    let inline del props children = domEl "del" props children
    let inline details props children = domEl "details" props children
    let inline dfn props children = domEl "dfn" props children
    let inline dialog props children = domEl "dialog" props children
    let inline div props children = domEl "div" props children
    let inline dl props children = domEl "dl" props children
    let inline dt props children = domEl "dt" props children
    let inline em props children = domEl "em" props children
    let inline fieldset props children = domEl "fieldset" props children
    let inline figcaption props children = domEl "figcaption" props children
    let inline figure props children = domEl "figure" props children
    let inline footer props children = domEl "footer" props children
    let inline form props children = domEl "form" props children
    let inline h1 props children = domEl "h1" props children
    let inline h2 props children = domEl "h2" props children
    let inline h3 props children = domEl "h3" props children
    let inline h4 props children = domEl "h4" props children
    let inline h5 props children = domEl "h5" props children
    let inline h6 props children = domEl "h6" props children
    let inline head props children = domEl "head" props children
    let inline header props children = domEl "header" props children
    let inline hgroup props children = domEl "hgroup" props children
    let inline html props children = domEl "html" props children
    let inline i props children = domEl "i" props children
    let inline iframe props children = domEl "iframe" props children
    let inline ins props children = domEl "ins" props children
    let inline kbd props children = domEl "kbd" props children
    let inline label props children = domEl "label" props children
    let inline legend props children = domEl "legend" props children
    let inline li props children = domEl "li" props children
    let inline main props children = domEl "main" props children
    let inline map props children = domEl "map" props children
    let inline mark props children = domEl "mark" props children
    let inline menu props children = domEl "menu" props children
    let inline meter props children = domEl "meter" props children
    let inline nav props children = domEl "nav" props children
    let inline noscript props children = domEl "noscript" props children
    let inline object props children = domEl "object" props children
    let inline ol props children = domEl "ol" props children
    let inline optgroup props children = domEl "optgroup" props children
    let inline option props children = domEl "option" props children
    let inline output props children = domEl "output" props children
    let inline p props children = domEl "p" props children
    let inline picture props children = domEl "picture" props children
    let inline pre props children = domEl "pre" props children
    let inline progress props children = domEl "progress" props children
    let inline q props children = domEl "q" props children
    let inline rp props children = domEl "rp" props children
    let inline rt props children = domEl "rt" props children
    let inline ruby props children = domEl "ruby" props children
    let inline s props children = domEl "s" props children
    let inline samp props children = domEl "samp" props children
    let inline script props children = domEl "script" props children
    let inline section props children = domEl "section" props children
    let inline select props children = domEl "select" props children
    let inline small props children = domEl "small" props children
    let inline span props children = domEl "span" props children
    let inline strong props children = domEl "strong" props children
    let inline style props children = domEl "style" props children
    let inline sub props children = domEl "sub" props children
    let inline summary props children = domEl "summary" props children
    let inline sup props children = domEl "sup" props children
    let inline table props children = domEl "table" props children
    let inline tbody props children = domEl "tbody" props children
    let inline td props children = domEl "td" props children
    let inline textarea props children = domEl "textarea" props children
    let inline tfoot props children = domEl "tfoot" props children
    let inline th props children = domEl "th" props children
    let inline thead props children = domEl "thead" props children
    let inline time props children = domEl "time" props children
    let inline title props children = domEl "title" props children
    let inline tr props children = domEl "tr" props children
    let inline u props children = domEl "u" props children
    let inline ul props children = domEl "ul" props children
    let inline var props children = domEl "var" props children
    let inline video props children = domEl "video" props children
    
    // Void element
    let inline area props = voidEl "area" props
    let inline ``base`` props = voidEl "base" props
    let inline br props = voidEl "br" props
    let inline col props = voidEl "col" props
    let inline embed props = voidEl "embed" props
    let inline hr props = voidEl "hr" props
    let inline img props = voidEl "img" props
    let inline input props = voidEl "input" props
    let inline keygen props = voidEl "keygen" props
    let inline link props = voidEl "link" props
    let inline menuitem props = voidEl "menuitem" props
    let inline meta props = voidEl "meta" props
    let inline param props = voidEl "param" props
    let inline source props = voidEl "source" props
    let inline track props = voidEl "track" props
    let inline wbr props = voidEl "wbr" props
    
    // SVG elements
    let inline svg props children = svgEl "svg" props children
    let inline circle props children = svgEl "circle" props children
    let inline clipPath props children = svgEl "clipPath" props children
    let inline defs props children = svgEl "defs" props children
    let inline ellipse props children = svgEl "ellipse" props children
    let inline g props children = svgEl "g" props children
    let inline image props children = svgEl "image" props children
    let inline line props children = svgEl "line" props children
    let inline linearGradient props children = svgEl "linearGradient" props children
    let inline mask props children = svgEl "mask" props children
    let inline path props children = svgEl "path" props children
    let inline pattern props children = svgEl "pattern" props children
    let inline polygon props children = svgEl "polygon" props children
    let inline polyline props children = svgEl "polyline" props children
    let inline radialGradient props children = svgEl "radialGradient" props children
    let inline rect props children = svgEl "rect" props children
    let inline stop props children = svgEl "stop" props children
    let inline text props children = svgEl "text" props children
    let inline tspan props children = svgEl "tspan" props children