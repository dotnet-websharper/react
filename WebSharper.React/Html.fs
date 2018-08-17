namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript
open WebSharper.React.Bindings

// This is an auto-generated module providing HTML5 vocabulary.
// Generated using tags.csv from WebSharper;
// See tools/UpdateElems.fsx for the code-generation logic.
/// HTML helper functions.
module Html =

    /// Create a text node.
    [<Inline>]
    let text (t: string) = React.Text t

    // {{ tag normal
    /// Create an HTML element <a> with props and children.
    [<Inline>]
    let a props children = React.Element "a" props children
    
    /// Create an HTML element <abbr> with props and children.
    [<Inline>]
    let abbr props children = React.Element "abbr" props children
    
    /// Create an HTML element <address> with props and children.
    [<Inline>]
    let address props children = React.Element "address" props children
    
    /// Create an HTML element <area> with props and children.
    [<Inline>]
    let area props children = React.Element "area" props children
    
    /// Create an HTML element <article> with props and children.
    [<Inline>]
    let article props children = React.Element "article" props children
    
    /// Create an HTML element <aside> with props and children.
    [<Inline>]
    let aside props children = React.Element "aside" props children
    
    /// Create an HTML element <audio> with props and children.
    [<Inline>]
    let audio props children = React.Element "audio" props children
    
    /// Create an HTML element <b> with props and children.
    [<Inline>]
    let b props children = React.Element "b" props children
    
    /// Create an HTML element <base> with props and children.
    [<Inline>]
    let ``base`` props children = React.Element "base" props children
    
    /// Create an HTML element <bdi> with props and children.
    [<Inline>]
    let bdi props children = React.Element "bdi" props children
    
    /// Create an HTML element <bdo> with props and children.
    [<Inline>]
    let bdo props children = React.Element "bdo" props children
    
    /// Create an HTML element <blockquote> with props and children.
    [<Inline>]
    let blockquote props children = React.Element "blockquote" props children
    
    /// Create an HTML element <body> with props and children.
    [<Inline>]
    let body props children = React.Element "body" props children
    
    /// Create an HTML element <br> with props and children.
    [<Inline>]
    let br props children = React.Element "br" props children
    
    /// Create an HTML element <button> with props and children.
    [<Inline>]
    let button props children = React.Element "button" props children
    
    /// Create an HTML element <canvas> with props and children.
    [<Inline>]
    let canvas props children = React.Element "canvas" props children
    
    /// Create an HTML element <caption> with props and children.
    [<Inline>]
    let caption props children = React.Element "caption" props children
    
    /// Create an HTML element <cite> with props and children.
    [<Inline>]
    let cite props children = React.Element "cite" props children
    
    /// Create an HTML element <code> with props and children.
    [<Inline>]
    let code props children = React.Element "code" props children
    
    /// Create an HTML element <col> with props and children.
    [<Inline>]
    let col props children = React.Element "col" props children
    
    /// Create an HTML element <colgroup> with props and children.
    [<Inline>]
    let colgroup props children = React.Element "colgroup" props children
    
    /// Create an HTML element <command> with props and children.
    [<Inline>]
    let command props children = React.Element "command" props children
    
    /// Create an HTML element <datalist> with props and children.
    [<Inline>]
    let datalist props children = React.Element "datalist" props children
    
    /// Create an HTML element <dd> with props and children.
    [<Inline>]
    let dd props children = React.Element "dd" props children
    
    /// Create an HTML element <del> with props and children.
    [<Inline>]
    let del props children = React.Element "del" props children
    
    /// Create an HTML element <details> with props and children.
    [<Inline>]
    let details props children = React.Element "details" props children
    
    /// Create an HTML element <dfn> with props and children.
    [<Inline>]
    let dfn props children = React.Element "dfn" props children
    
    /// Create an HTML element <div> with props and children.
    [<Inline>]
    let div props children = React.Element "div" props children
    
    /// Create an HTML element <dl> with props and children.
    [<Inline>]
    let dl props children = React.Element "dl" props children
    
    /// Create an HTML element <dt> with props and children.
    [<Inline>]
    let dt props children = React.Element "dt" props children
    
    /// Create an HTML element <em> with props and children.
    [<Inline>]
    let em props children = React.Element "em" props children
    
    /// Create an HTML element <embed> with props and children.
    [<Inline>]
    let embed props children = React.Element "embed" props children
    
    /// Create an HTML element <fieldset> with props and children.
    [<Inline>]
    let fieldset props children = React.Element "fieldset" props children
    
    /// Create an HTML element <figcaption> with props and children.
    [<Inline>]
    let figcaption props children = React.Element "figcaption" props children
    
    /// Create an HTML element <figure> with props and children.
    [<Inline>]
    let figure props children = React.Element "figure" props children
    
    /// Create an HTML element <footer> with props and children.
    [<Inline>]
    let footer props children = React.Element "footer" props children
    
    /// Create an HTML element <form> with props and children.
    [<Inline>]
    let form props children = React.Element "form" props children
    
    /// Create an HTML element <h1> with props and children.
    [<Inline>]
    let h1 props children = React.Element "h1" props children
    
    /// Create an HTML element <h2> with props and children.
    [<Inline>]
    let h2 props children = React.Element "h2" props children
    
    /// Create an HTML element <h3> with props and children.
    [<Inline>]
    let h3 props children = React.Element "h3" props children
    
    /// Create an HTML element <h4> with props and children.
    [<Inline>]
    let h4 props children = React.Element "h4" props children
    
    /// Create an HTML element <h5> with props and children.
    [<Inline>]
    let h5 props children = React.Element "h5" props children
    
    /// Create an HTML element <h6> with props and children.
    [<Inline>]
    let h6 props children = React.Element "h6" props children
    
    /// Create an HTML element <head> with props and children.
    [<Inline>]
    let head props children = React.Element "head" props children
    
    /// Create an HTML element <header> with props and children.
    [<Inline>]
    let header props children = React.Element "header" props children
    
    /// Create an HTML element <hgroup> with props and children.
    [<Inline>]
    let hgroup props children = React.Element "hgroup" props children
    
    /// Create an HTML element <hr> with props and children.
    [<Inline>]
    let hr props children = React.Element "hr" props children
    
    /// Create an HTML element <html> with props and children.
    [<Inline>]
    let html props children = React.Element "html" props children
    
    /// Create an HTML element <i> with props and children.
    [<Inline>]
    let i props children = React.Element "i" props children
    
    /// Create an HTML element <iframe> with props and children.
    [<Inline>]
    let iframe props children = React.Element "iframe" props children
    
    /// Create an HTML element <img> with props and children.
    [<Inline>]
    let img props children = React.Element "img" props children
    
    /// Create an HTML element <input> with props and children.
    [<Inline>]
    let input props children = React.Element "input" props children
    
    /// Create an HTML element <ins> with props and children.
    [<Inline>]
    let ins props children = React.Element "ins" props children
    
    /// Create an HTML element <kbd> with props and children.
    [<Inline>]
    let kbd props children = React.Element "kbd" props children
    
    /// Create an HTML element <keygen> with props and children.
    [<Inline>]
    let keygen props children = React.Element "keygen" props children
    
    /// Create an HTML element <label> with props and children.
    [<Inline>]
    let label props children = React.Element "label" props children
    
    /// Create an HTML element <legend> with props and children.
    [<Inline>]
    let legend props children = React.Element "legend" props children
    
    /// Create an HTML element <li> with props and children.
    [<Inline>]
    let li props children = React.Element "li" props children
    
    /// Create an HTML element <link> with props and children.
    [<Inline>]
    let link props children = React.Element "link" props children
    
    /// Create an HTML element <mark> with props and children.
    [<Inline>]
    let mark props children = React.Element "mark" props children
    
    /// Create an HTML element <meta> with props and children.
    [<Inline>]
    let meta props children = React.Element "meta" props children
    
    /// Create an HTML element <meter> with props and children.
    [<Inline>]
    let meter props children = React.Element "meter" props children
    
    /// Create an HTML element <nav> with props and children.
    [<Inline>]
    let nav props children = React.Element "nav" props children
    
    /// Create an HTML element <noframes> with props and children.
    [<Inline>]
    let noframes props children = React.Element "noframes" props children
    
    /// Create an HTML element <noscript> with props and children.
    [<Inline>]
    let noscript props children = React.Element "noscript" props children
    
    /// Create an HTML element <ol> with props and children.
    [<Inline>]
    let ol props children = React.Element "ol" props children
    
    /// Create an HTML element <optgroup> with props and children.
    [<Inline>]
    let optgroup props children = React.Element "optgroup" props children
    
    /// Create an HTML element <output> with props and children.
    [<Inline>]
    let output props children = React.Element "output" props children
    
    /// Create an HTML element <p> with props and children.
    [<Inline>]
    let p props children = React.Element "p" props children
    
    /// Create an HTML element <param> with props and children.
    [<Inline>]
    let param props children = React.Element "param" props children
    
    /// Create an HTML element <picture> with props and children.
    [<Inline>]
    let picture props children = React.Element "picture" props children
    
    /// Create an HTML element <pre> with props and children.
    [<Inline>]
    let pre props children = React.Element "pre" props children
    
    /// Create an HTML element <progress> with props and children.
    [<Inline>]
    let progress props children = React.Element "progress" props children
    
    /// Create an HTML element <q> with props and children.
    [<Inline>]
    let q props children = React.Element "q" props children
    
    /// Create an HTML element <rp> with props and children.
    [<Inline>]
    let rp props children = React.Element "rp" props children
    
    /// Create an HTML element <rt> with props and children.
    [<Inline>]
    let rt props children = React.Element "rt" props children
    
    /// Create an HTML element <rtc> with props and children.
    [<Inline>]
    let rtc props children = React.Element "rtc" props children
    
    /// Create an HTML element <ruby> with props and children.
    [<Inline>]
    let ruby props children = React.Element "ruby" props children
    
    /// Create an HTML element <samp> with props and children.
    [<Inline>]
    let samp props children = React.Element "samp" props children
    
    /// Create an HTML element <script> with props and children.
    [<Inline>]
    let script props children = React.Element "script" props children
    
    /// Create an HTML element <section> with props and children.
    [<Inline>]
    let section props children = React.Element "section" props children
    
    /// Create an HTML element <select> with props and children.
    [<Inline>]
    let select props children = React.Element "select" props children
    
    /// Create an HTML element <shadow> with props and children.
    [<Inline>]
    let shadow props children = React.Element "shadow" props children
    
    /// Create an HTML element <small> with props and children.
    [<Inline>]
    let small props children = React.Element "small" props children
    
    /// Create an HTML element <source> with props and children.
    [<Inline>]
    let source props children = React.Element "source" props children
    
    /// Create an HTML element <span> with props and children.
    [<Inline>]
    let span props children = React.Element "span" props children
    
    /// Create an HTML element <strong> with props and children.
    [<Inline>]
    let strong props children = React.Element "strong" props children
    
    /// Create an HTML element <sub> with props and children.
    [<Inline>]
    let sub props children = React.Element "sub" props children
    
    /// Create an HTML element <summary> with props and children.
    [<Inline>]
    let summary props children = React.Element "summary" props children
    
    /// Create an HTML element <sup> with props and children.
    [<Inline>]
    let sup props children = React.Element "sup" props children
    
    /// Create an HTML element <table> with props and children.
    [<Inline>]
    let table props children = React.Element "table" props children
    
    /// Create an HTML element <tbody> with props and children.
    [<Inline>]
    let tbody props children = React.Element "tbody" props children
    
    /// Create an HTML element <td> with props and children.
    [<Inline>]
    let td props children = React.Element "td" props children
    
    /// Create an HTML element <textarea> with props and children.
    [<Inline>]
    let textarea props children = React.Element "textarea" props children
    
    /// Create an HTML element <tfoot> with props and children.
    [<Inline>]
    let tfoot props children = React.Element "tfoot" props children
    
    /// Create an HTML element <th> with props and children.
    [<Inline>]
    let th props children = React.Element "th" props children
    
    /// Create an HTML element <thead> with props and children.
    [<Inline>]
    let thead props children = React.Element "thead" props children
    
    /// Create an HTML element <time> with props and children.
    [<Inline>]
    let time props children = React.Element "time" props children
    
    /// Create an HTML element <tr> with props and children.
    [<Inline>]
    let tr props children = React.Element "tr" props children
    
    /// Create an HTML element <track> with props and children.
    [<Inline>]
    let track props children = React.Element "track" props children
    
    /// Create an HTML element <ul> with props and children.
    [<Inline>]
    let ul props children = React.Element "ul" props children
    
    /// Create an HTML element <video> with props and children.
    [<Inline>]
    let video props children = React.Element "video" props children
    
    /// Create an HTML element <wbr> with props and children.
    [<Inline>]
    let wbr props children = React.Element "wbr" props children
    
    // }}

    /// Deprecated and colliding HTML tags.
    module Tags =
        // {{ tag colliding deprecated
        /// Create an HTML element <acronym> with props and children.
        [<Inline>]
        let acronym props children = React.Element "acronym" props children
        
        /// Create an HTML element <applet> with props and children.
        [<Inline>]
        let applet props children = React.Element "applet" props children
        
        /// Create an HTML element <basefont> with props and children.
        [<Inline>]
        let basefont props children = React.Element "basefont" props children
        
        /// Create an HTML element <big> with props and children.
        [<Inline>]
        let big props children = React.Element "big" props children
        
        /// Create an HTML element <center> with props and children.
        [<Inline>]
        let center props children = React.Element "center" props children
        
        /// Create an HTML element <content> with props and children.
        [<Inline>]
        let content props children = React.Element "content" props children
        
        /// Create an HTML element <data> with props and children.
        [<Inline>]
        let data props children = React.Element "data" props children
        
        /// Create an HTML element <dir> with props and children.
        [<Inline>]
        let dir props children = React.Element "dir" props children
        
        /// Create an HTML element <font> with props and children.
        [<Inline>]
        let font props children = React.Element "font" props children
        
        /// Create an HTML element <frame> with props and children.
        [<Inline>]
        let frame props children = React.Element "frame" props children
        
        /// Create an HTML element <frameset> with props and children.
        [<Inline>]
        let frameset props children = React.Element "frameset" props children
        
        /// Create an HTML element <isindex> with props and children.
        [<Inline>]
        let isindex props children = React.Element "isindex" props children
        
        /// Create an HTML element <main> with props and children.
        [<Inline>]
        let main props children = React.Element "main" props children
        
        /// Create an HTML element <map> with props and children.
        [<Inline>]
        let map props children = React.Element "map" props children
        
        /// Create an HTML element <menu> with props and children.
        [<Inline>]
        let menu props children = React.Element "menu" props children
        
        /// Create an HTML element <menuitem> with props and children.
        [<Inline>]
        let menuitem props children = React.Element "menuitem" props children
        
        /// Create an HTML element <object> with props and children.
        [<Inline>]
        let ``object`` props children = React.Element "object" props children
        
        /// Create an HTML element <option> with props and children.
        [<Inline>]
        let option props children = React.Element "option" props children
        
        /// Create an HTML element <s> with props and children.
        [<Inline>]
        let s props children = React.Element "s" props children
        
        /// Create an HTML element <strike> with props and children.
        [<Inline>]
        let strike props children = React.Element "strike" props children
        
        /// Create an HTML element <style> with props and children.
        [<Inline>]
        let style props children = React.Element "style" props children
        
        /// Create an HTML element <template> with props and children.
        [<Inline>]
        let template props children = React.Element "template" props children
        
        /// Create an HTML element <title> with props and children.
        [<Inline>]
        let title props children = React.Element "title" props children
        
        /// Create an HTML element <tt> with props and children.
        [<Inline>]
        let tt props children = React.Element "tt" props children
        
        /// Create an HTML element <u> with props and children.
        [<Inline>]
        let u props children = React.Element "u" props children
        
        /// Create an HTML element <var> with props and children.
        [<Inline>]
        let var props children = React.Element "var" props children
        
        // }}

    /// SVG elements.
    module SvgElements =
        // {{ svgtag normal
        /// Create an SVG element <a> with props and children.
        [<Inline>]
        let a props children = React.Element "a" props children
        
        /// Create an SVG element <altglyph> with props and children.
        [<Inline>]
        let altglyph props children = React.Element "altglyph" props children
        
        /// Create an SVG element <altglyphdef> with props and children.
        [<Inline>]
        let altglyphdef props children = React.Element "altglyphdef" props children
        
        /// Create an SVG element <altglyphitem> with props and children.
        [<Inline>]
        let altglyphitem props children = React.Element "altglyphitem" props children
        
        /// Create an SVG element <animate> with props and children.
        [<Inline>]
        let animate props children = React.Element "animate" props children
        
        /// Create an SVG element <animatecolor> with props and children.
        [<Inline>]
        let animatecolor props children = React.Element "animatecolor" props children
        
        /// Create an SVG element <animatemotion> with props and children.
        [<Inline>]
        let animatemotion props children = React.Element "animatemotion" props children
        
        /// Create an SVG element <animatetransform> with props and children.
        [<Inline>]
        let animatetransform props children = React.Element "animatetransform" props children
        
        /// Create an SVG element <circle> with props and children.
        [<Inline>]
        let circle props children = React.Element "circle" props children
        
        /// Create an SVG element <clippath> with props and children.
        [<Inline>]
        let clippath props children = React.Element "clippath" props children
        
        /// Create an SVG element <color-profile> with props and children.
        [<Inline>]
        let colorProfile props children = React.Element "color-profile" props children
        
        /// Create an SVG element <cursor> with props and children.
        [<Inline>]
        let cursor props children = React.Element "cursor" props children
        
        /// Create an SVG element <defs> with props and children.
        [<Inline>]
        let defs props children = React.Element "defs" props children
        
        /// Create an SVG element <desc> with props and children.
        [<Inline>]
        let desc props children = React.Element "desc" props children
        
        /// Create an SVG element <ellipse> with props and children.
        [<Inline>]
        let ellipse props children = React.Element "ellipse" props children
        
        /// Create an SVG element <feblend> with props and children.
        [<Inline>]
        let feblend props children = React.Element "feblend" props children
        
        /// Create an SVG element <fecolormatrix> with props and children.
        [<Inline>]
        let fecolormatrix props children = React.Element "fecolormatrix" props children
        
        /// Create an SVG element <fecomponenttransfer> with props and children.
        [<Inline>]
        let fecomponenttransfer props children = React.Element "fecomponenttransfer" props children
        
        /// Create an SVG element <fecomposite> with props and children.
        [<Inline>]
        let fecomposite props children = React.Element "fecomposite" props children
        
        /// Create an SVG element <feconvolvematrix> with props and children.
        [<Inline>]
        let feconvolvematrix props children = React.Element "feconvolvematrix" props children
        
        /// Create an SVG element <fediffuselighting> with props and children.
        [<Inline>]
        let fediffuselighting props children = React.Element "fediffuselighting" props children
        
        /// Create an SVG element <fedisplacementmap> with props and children.
        [<Inline>]
        let fedisplacementmap props children = React.Element "fedisplacementmap" props children
        
        /// Create an SVG element <fedistantlight> with props and children.
        [<Inline>]
        let fedistantlight props children = React.Element "fedistantlight" props children
        
        /// Create an SVG element <feflood> with props and children.
        [<Inline>]
        let feflood props children = React.Element "feflood" props children
        
        /// Create an SVG element <fefunca> with props and children.
        [<Inline>]
        let fefunca props children = React.Element "fefunca" props children
        
        /// Create an SVG element <fefuncb> with props and children.
        [<Inline>]
        let fefuncb props children = React.Element "fefuncb" props children
        
        /// Create an SVG element <fefuncg> with props and children.
        [<Inline>]
        let fefuncg props children = React.Element "fefuncg" props children
        
        /// Create an SVG element <fefuncr> with props and children.
        [<Inline>]
        let fefuncr props children = React.Element "fefuncr" props children
        
        /// Create an SVG element <fegaussianblur> with props and children.
        [<Inline>]
        let fegaussianblur props children = React.Element "fegaussianblur" props children
        
        /// Create an SVG element <feimage> with props and children.
        [<Inline>]
        let feimage props children = React.Element "feimage" props children
        
        /// Create an SVG element <femerge> with props and children.
        [<Inline>]
        let femerge props children = React.Element "femerge" props children
        
        /// Create an SVG element <femergenode> with props and children.
        [<Inline>]
        let femergenode props children = React.Element "femergenode" props children
        
        /// Create an SVG element <femorphology> with props and children.
        [<Inline>]
        let femorphology props children = React.Element "femorphology" props children
        
        /// Create an SVG element <feoffset> with props and children.
        [<Inline>]
        let feoffset props children = React.Element "feoffset" props children
        
        /// Create an SVG element <fepointlight> with props and children.
        [<Inline>]
        let fepointlight props children = React.Element "fepointlight" props children
        
        /// Create an SVG element <fespecularlighting> with props and children.
        [<Inline>]
        let fespecularlighting props children = React.Element "fespecularlighting" props children
        
        /// Create an SVG element <fespotlight> with props and children.
        [<Inline>]
        let fespotlight props children = React.Element "fespotlight" props children
        
        /// Create an SVG element <fetile> with props and children.
        [<Inline>]
        let fetile props children = React.Element "fetile" props children
        
        /// Create an SVG element <feturbulence> with props and children.
        [<Inline>]
        let feturbulence props children = React.Element "feturbulence" props children
        
        /// Create an SVG element <filter> with props and children.
        [<Inline>]
        let filter props children = React.Element "filter" props children
        
        /// Create an SVG element <font> with props and children.
        [<Inline>]
        let font props children = React.Element "font" props children
        
        /// Create an SVG element <font-face> with props and children.
        [<Inline>]
        let fontFace props children = React.Element "font-face" props children
        
        /// Create an SVG element <font-face-format> with props and children.
        [<Inline>]
        let fontFaceFormat props children = React.Element "font-face-format" props children
        
        /// Create an SVG element <font-face-name> with props and children.
        [<Inline>]
        let fontFaceName props children = React.Element "font-face-name" props children
        
        /// Create an SVG element <font-face-src> with props and children.
        [<Inline>]
        let fontFaceSrc props children = React.Element "font-face-src" props children
        
        /// Create an SVG element <font-face-uri> with props and children.
        [<Inline>]
        let fontFaceUri props children = React.Element "font-face-uri" props children
        
        /// Create an SVG element <foreignobject> with props and children.
        [<Inline>]
        let foreignobject props children = React.Element "foreignobject" props children
        
        /// Create an SVG element <g> with props and children.
        [<Inline>]
        let g props children = React.Element "g" props children
        
        /// Create an SVG element <glyph> with props and children.
        [<Inline>]
        let glyph props children = React.Element "glyph" props children
        
        /// Create an SVG element <glyphref> with props and children.
        [<Inline>]
        let glyphref props children = React.Element "glyphref" props children
        
        /// Create an SVG element <hkern> with props and children.
        [<Inline>]
        let hkern props children = React.Element "hkern" props children
        
        /// Create an SVG element <image> with props and children.
        [<Inline>]
        let image props children = React.Element "image" props children
        
        /// Create an SVG element <line> with props and children.
        [<Inline>]
        let line props children = React.Element "line" props children
        
        /// Create an SVG element <lineargradient> with props and children.
        [<Inline>]
        let lineargradient props children = React.Element "lineargradient" props children
        
        /// Create an SVG element <marker> with props and children.
        [<Inline>]
        let marker props children = React.Element "marker" props children
        
        /// Create an SVG element <mask> with props and children.
        [<Inline>]
        let mask props children = React.Element "mask" props children
        
        /// Create an SVG element <metadata> with props and children.
        [<Inline>]
        let metadata props children = React.Element "metadata" props children
        
        /// Create an SVG element <missing-glyph> with props and children.
        [<Inline>]
        let missingGlyph props children = React.Element "missing-glyph" props children
        
        /// Create an SVG element <mpath> with props and children.
        [<Inline>]
        let mpath props children = React.Element "mpath" props children
        
        /// Create an SVG element <path> with props and children.
        [<Inline>]
        let path props children = React.Element "path" props children
        
        /// Create an SVG element <pattern> with props and children.
        [<Inline>]
        let pattern props children = React.Element "pattern" props children
        
        /// Create an SVG element <polygon> with props and children.
        [<Inline>]
        let polygon props children = React.Element "polygon" props children
        
        /// Create an SVG element <polyline> with props and children.
        [<Inline>]
        let polyline props children = React.Element "polyline" props children
        
        /// Create an SVG element <radialgradient> with props and children.
        [<Inline>]
        let radialgradient props children = React.Element "radialgradient" props children
        
        /// Create an SVG element <rect> with props and children.
        [<Inline>]
        let rect props children = React.Element "rect" props children
        
        /// Create an SVG element <script> with props and children.
        [<Inline>]
        let script props children = React.Element "script" props children
        
        /// Create an SVG element <set> with props and children.
        [<Inline>]
        let set props children = React.Element "set" props children
        
        /// Create an SVG element <stop> with props and children.
        [<Inline>]
        let stop props children = React.Element "stop" props children
        
        /// Create an SVG element <style> with props and children.
        [<Inline>]
        let style props children = React.Element "style" props children
        
        /// Create an SVG element <svg> with props and children.
        [<Inline>]
        let svg props children = React.Element "svg" props children
        
        /// Create an SVG element <switch> with props and children.
        [<Inline>]
        let switch props children = React.Element "switch" props children
        
        /// Create an SVG element <symbol> with props and children.
        [<Inline>]
        let symbol props children = React.Element "symbol" props children
        
        /// Create an SVG element <text> with props and children.
        [<Inline>]
        let text props children = React.Element "text" props children
        
        /// Create an SVG element <textpath> with props and children.
        [<Inline>]
        let textpath props children = React.Element "textpath" props children
        
        /// Create an SVG element <title> with props and children.
        [<Inline>]
        let title props children = React.Element "title" props children
        
        /// Create an SVG element <tref> with props and children.
        [<Inline>]
        let tref props children = React.Element "tref" props children
        
        /// Create an SVG element <tspan> with props and children.
        [<Inline>]
        let tspan props children = React.Element "tspan" props children
        
        /// Create an SVG element <use> with props and children.
        [<Inline>]
        let ``use`` props children = React.Element "use" props children
        
        /// Create an SVG element <view> with props and children.
        [<Inline>]
        let view props children = React.Element "view" props children
        
        /// Create an SVG element <vkern> with props and children.
        [<Inline>]
        let vkern props children = React.Element "vkern" props children
        
        // }}

    module attr =
        /// Create an HTML class atttribute with the given value.
        [<Inline>]
        let className (value: string) = "className" => value

        /// Create a React key prop.
        [<Inline>]
        let key (value: obj) = "key" => value

        // {{ attr normal colliding deprecated
        /// Create an HTML attribute "accept" with the given value.
        [<Inline>]
        let accept (value: string) = "accept" => value
        
        /// Create an HTML attribute "accept-charset" with the given value.
        [<Inline>]
        let acceptCharset (value: string) = "acceptCharset" => value
        
        /// Create an HTML attribute "accesskey" with the given value.
        [<Inline>]
        let accesskey (value: string) = "accesskey" => value
        
        /// Create an HTML attribute "action" with the given value.
        [<Inline>]
        let action (value: string) = "action" => value
        
        /// Create an HTML attribute "align" with the given value.
        [<Inline>]
        let align (value: string) = "align" => value
        
        /// Create an HTML attribute "alink" with the given value.
        [<Inline>]
        let alink (value: string) = "alink" => value
        
        /// Create an HTML attribute "alt" with the given value.
        [<Inline>]
        let alt (value: string) = "alt" => value
        
        /// Create an HTML attribute "altcode" with the given value.
        [<Inline>]
        let altcode (value: string) = "altcode" => value
        
        /// Create an HTML attribute "archive" with the given value.
        [<Inline>]
        let archive (value: string) = "archive" => value
        
        /// Create an HTML attribute "async" with the given value.
        [<Inline>]
        let async (value: string) = "async" => value
        
        /// Create an HTML attribute "autocomplete" with the given value.
        [<Inline>]
        let autocomplete (value: string) = "autocomplete" => value
        
        /// Create an HTML attribute "autofocus" with the given value.
        [<Inline>]
        let autofocus (value: string) = "autofocus" => value
        
        /// Create an HTML attribute "autoplay" with the given value.
        [<Inline>]
        let autoplay (value: string) = "autoplay" => value
        
        /// Create an HTML attribute "autosave" with the given value.
        [<Inline>]
        let autosave (value: string) = "autosave" => value
        
        /// Create an HTML attribute "axis" with the given value.
        [<Inline>]
        let axis (value: string) = "axis" => value
        
        /// Create an HTML attribute "background" with the given value.
        [<Inline>]
        let background (value: string) = "background" => value
        
        /// Create an HTML attribute "bgcolor" with the given value.
        [<Inline>]
        let bgcolor (value: string) = "bgcolor" => value
        
        /// Create an HTML attribute "border" with the given value.
        [<Inline>]
        let border (value: string) = "border" => value
        
        /// Create an HTML attribute "bordercolor" with the given value.
        [<Inline>]
        let bordercolor (value: string) = "bordercolor" => value
        
        /// Create an HTML attribute "buffered" with the given value.
        [<Inline>]
        let buffered (value: string) = "buffered" => value
        
        /// Create an HTML attribute "cellpadding" with the given value.
        [<Inline>]
        let cellpadding (value: string) = "cellpadding" => value
        
        /// Create an HTML attribute "cellspacing" with the given value.
        [<Inline>]
        let cellspacing (value: string) = "cellspacing" => value
        
        /// Create an HTML attribute "challenge" with the given value.
        [<Inline>]
        let challenge (value: string) = "challenge" => value
        
        /// Create an HTML attribute "char" with the given value.
        [<Inline>]
        let char (value: string) = "char" => value
        
        /// Create an HTML attribute "charoff" with the given value.
        [<Inline>]
        let charoff (value: string) = "charoff" => value
        
        /// Create an HTML attribute "charset" with the given value.
        [<Inline>]
        let charset (value: string) = "charset" => value
        
        /// Create an HTML attribute "checked" with the given value.
        [<Inline>]
        let ``checked`` (value: string) = "checked" => value
        
        /// Create an HTML attribute "cite" with the given value.
        [<Inline>]
        let cite (value: string) = "cite" => value
        
        /// Create an HTML attribute "class" with the given value.
        [<Inline>]
        let ``class`` (value: string) = "class" => value
        
        /// Create an HTML attribute "classid" with the given value.
        [<Inline>]
        let classid (value: string) = "classid" => value
        
        /// Create an HTML attribute "clear" with the given value.
        [<Inline>]
        let clear (value: string) = "clear" => value
        
        /// Create an HTML attribute "code" with the given value.
        [<Inline>]
        let code (value: string) = "code" => value
        
        /// Create an HTML attribute "codebase" with the given value.
        [<Inline>]
        let codebase (value: string) = "codebase" => value
        
        /// Create an HTML attribute "codetype" with the given value.
        [<Inline>]
        let codetype (value: string) = "codetype" => value
        
        /// Create an HTML attribute "color" with the given value.
        [<Inline>]
        let color (value: string) = "color" => value
        
        /// Create an HTML attribute "cols" with the given value.
        [<Inline>]
        let cols (value: string) = "cols" => value
        
        /// Create an HTML attribute "colspan" with the given value.
        [<Inline>]
        let colspan (value: string) = "colspan" => value
        
        /// Create an HTML attribute "compact" with the given value.
        [<Inline>]
        let compact (value: string) = "compact" => value
        
        /// Create an HTML attribute "content" with the given value.
        [<Inline>]
        let content (value: string) = "content" => value
        
        /// Create an HTML attribute "contenteditable" with the given value.
        [<Inline>]
        let contenteditable (value: string) = "contenteditable" => value
        
        /// Create an HTML attribute "contextmenu" with the given value.
        [<Inline>]
        let contextmenu (value: string) = "contextmenu" => value
        
        /// Create an HTML attribute "controls" with the given value.
        [<Inline>]
        let controls (value: string) = "controls" => value
        
        /// Create an HTML attribute "coords" with the given value.
        [<Inline>]
        let coords (value: string) = "coords" => value
        
        /// Create an HTML attribute "data" with the given value.
        [<Inline>]
        let data (value: string) = "data" => value
        
        /// Create an HTML attribute "datetime" with the given value.
        [<Inline>]
        let datetime (value: string) = "datetime" => value
        
        /// Create an HTML attribute "declare" with the given value.
        [<Inline>]
        let declare (value: string) = "declare" => value
        
        /// Create an HTML attribute "default" with the given value.
        [<Inline>]
        let ``default`` (value: string) = "default" => value
        
        /// Create an HTML attribute "defer" with the given value.
        [<Inline>]
        let defer (value: string) = "defer" => value
        
        /// Create an HTML attribute "dir" with the given value.
        [<Inline>]
        let dir (value: string) = "dir" => value
        
        /// Create an HTML attribute "disabled" with the given value.
        [<Inline>]
        let disabled (value: string) = "disabled" => value
        
        /// Create an HTML attribute "download" with the given value.
        [<Inline>]
        let download (value: string) = "download" => value
        
        /// Create an HTML attribute "draggable" with the given value.
        [<Inline>]
        let draggable (value: string) = "draggable" => value
        
        /// Create an HTML attribute "dropzone" with the given value.
        [<Inline>]
        let dropzone (value: string) = "dropzone" => value
        
        /// Create an HTML attribute "enctype" with the given value.
        [<Inline>]
        let enctype (value: string) = "enctype" => value
        
        /// Create an HTML attribute "face" with the given value.
        [<Inline>]
        let face (value: string) = "face" => value
        
        /// Create an HTML attribute "for" with the given value.
        [<Inline>]
        let ``for`` (value: string) = "for" => value
        
        /// Create an HTML attribute "form" with the given value.
        [<Inline>]
        let form (value: string) = "form" => value
        
        /// Create an HTML attribute "formaction" with the given value.
        [<Inline>]
        let formaction (value: string) = "formaction" => value
        
        /// Create an HTML attribute "formenctype" with the given value.
        [<Inline>]
        let formenctype (value: string) = "formenctype" => value
        
        /// Create an HTML attribute "formmethod" with the given value.
        [<Inline>]
        let formmethod (value: string) = "formmethod" => value
        
        /// Create an HTML attribute "formnovalidate" with the given value.
        [<Inline>]
        let formnovalidate (value: string) = "formnovalidate" => value
        
        /// Create an HTML attribute "formtarget" with the given value.
        [<Inline>]
        let formtarget (value: string) = "formtarget" => value
        
        /// Create an HTML attribute "frame" with the given value.
        [<Inline>]
        let frame (value: string) = "frame" => value
        
        /// Create an HTML attribute "frameborder" with the given value.
        [<Inline>]
        let frameborder (value: string) = "frameborder" => value
        
        /// Create an HTML attribute "headers" with the given value.
        [<Inline>]
        let headers (value: string) = "headers" => value
        
        /// Create an HTML attribute "height" with the given value.
        [<Inline>]
        let height (value: string) = "height" => value
        
        /// Create an HTML attribute "hidden" with the given value.
        [<Inline>]
        let hidden (value: string) = "hidden" => value
        
        /// Create an HTML attribute "high" with the given value.
        [<Inline>]
        let high (value: string) = "high" => value
        
        /// Create an HTML attribute "href" with the given value.
        [<Inline>]
        let href (value: string) = "href" => value
        
        /// Create an HTML attribute "hreflang" with the given value.
        [<Inline>]
        let hreflang (value: string) = "hreflang" => value
        
        /// Create an HTML attribute "hspace" with the given value.
        [<Inline>]
        let hspace (value: string) = "hspace" => value
        
        /// Create an HTML attribute "http" with the given value.
        [<Inline>]
        let http (value: string) = "http" => value
        
        /// Create an HTML attribute "icon" with the given value.
        [<Inline>]
        let icon (value: string) = "icon" => value
        
        /// Create an HTML attribute "id" with the given value.
        [<Inline>]
        let id (value: string) = "id" => value
        
        /// Create an HTML attribute "ismap" with the given value.
        [<Inline>]
        let ismap (value: string) = "ismap" => value
        
        /// Create an HTML attribute "itemprop" with the given value.
        [<Inline>]
        let itemprop (value: string) = "itemprop" => value
        
        /// Create an HTML attribute "keytype" with the given value.
        [<Inline>]
        let keytype (value: string) = "keytype" => value
        
        /// Create an HTML attribute "kind" with the given value.
        [<Inline>]
        let kind (value: string) = "kind" => value
        
        /// Create an HTML attribute "label" with the given value.
        [<Inline>]
        let label (value: string) = "label" => value
        
        /// Create an HTML attribute "lang" with the given value.
        [<Inline>]
        let lang (value: string) = "lang" => value
        
        /// Create an HTML attribute "language" with the given value.
        [<Inline>]
        let language (value: string) = "language" => value
        
        /// Create an HTML attribute "link" with the given value.
        [<Inline>]
        let link (value: string) = "link" => value
        
        /// Create an HTML attribute "list" with the given value.
        [<Inline>]
        let list (value: string) = "list" => value
        
        /// Create an HTML attribute "longdesc" with the given value.
        [<Inline>]
        let longdesc (value: string) = "longdesc" => value
        
        /// Create an HTML attribute "loop" with the given value.
        [<Inline>]
        let loop (value: string) = "loop" => value
        
        /// Create an HTML attribute "low" with the given value.
        [<Inline>]
        let low (value: string) = "low" => value
        
        /// Create an HTML attribute "manifest" with the given value.
        [<Inline>]
        let manifest (value: string) = "manifest" => value
        
        /// Create an HTML attribute "marginheight" with the given value.
        [<Inline>]
        let marginheight (value: string) = "marginheight" => value
        
        /// Create an HTML attribute "marginwidth" with the given value.
        [<Inline>]
        let marginwidth (value: string) = "marginwidth" => value
        
        /// Create an HTML attribute "max" with the given value.
        [<Inline>]
        let max (value: string) = "max" => value
        
        /// Create an HTML attribute "maxlength" with the given value.
        [<Inline>]
        let maxlength (value: string) = "maxlength" => value
        
        /// Create an HTML attribute "media" with the given value.
        [<Inline>]
        let media (value: string) = "media" => value
        
        /// Create an HTML attribute "method" with the given value.
        [<Inline>]
        let ``method`` (value: string) = "method" => value
        
        /// Create an HTML attribute "min" with the given value.
        [<Inline>]
        let min (value: string) = "min" => value
        
        /// Create an HTML attribute "multiple" with the given value.
        [<Inline>]
        let multiple (value: string) = "multiple" => value
        
        /// Create an HTML attribute "name" with the given value.
        [<Inline>]
        let name (value: string) = "name" => value
        
        /// Create an HTML attribute "nohref" with the given value.
        [<Inline>]
        let nohref (value: string) = "nohref" => value
        
        /// Create an HTML attribute "noresize" with the given value.
        [<Inline>]
        let noresize (value: string) = "noresize" => value
        
        /// Create an HTML attribute "noshade" with the given value.
        [<Inline>]
        let noshade (value: string) = "noshade" => value
        
        /// Create an HTML attribute "novalidate" with the given value.
        [<Inline>]
        let novalidate (value: string) = "novalidate" => value
        
        /// Create an HTML attribute "nowrap" with the given value.
        [<Inline>]
        let nowrap (value: string) = "nowrap" => value
        
        /// Create an HTML attribute "object" with the given value.
        [<Inline>]
        let ``object`` (value: string) = "object" => value
        
        /// Create an HTML attribute "open" with the given value.
        [<Inline>]
        let ``open`` (value: string) = "open" => value
        
        /// Create an HTML attribute "optimum" with the given value.
        [<Inline>]
        let optimum (value: string) = "optimum" => value
        
        /// Create an HTML attribute "pattern" with the given value.
        [<Inline>]
        let pattern (value: string) = "pattern" => value
        
        /// Create an HTML attribute "ping" with the given value.
        [<Inline>]
        let ping (value: string) = "ping" => value
        
        /// Create an HTML attribute "placeholder" with the given value.
        [<Inline>]
        let placeholder (value: string) = "placeholder" => value
        
        /// Create an HTML attribute "poster" with the given value.
        [<Inline>]
        let poster (value: string) = "poster" => value
        
        /// Create an HTML attribute "preload" with the given value.
        [<Inline>]
        let preload (value: string) = "preload" => value
        
        /// Create an HTML attribute "profile" with the given value.
        [<Inline>]
        let profile (value: string) = "profile" => value
        
        /// Create an HTML attribute "prompt" with the given value.
        [<Inline>]
        let prompt (value: string) = "prompt" => value
        
        /// Create an HTML attribute "pubdate" with the given value.
        [<Inline>]
        let pubdate (value: string) = "pubdate" => value
        
        /// Create an HTML attribute "radiogroup" with the given value.
        [<Inline>]
        let radiogroup (value: string) = "radiogroup" => value
        
        /// Create an HTML attribute "readonly" with the given value.
        [<Inline>]
        let readonly (value: string) = "readonly" => value
        
        /// Create an HTML attribute "rel" with the given value.
        [<Inline>]
        let rel (value: string) = "rel" => value
        
        /// Create an HTML attribute "required" with the given value.
        [<Inline>]
        let required (value: string) = "required" => value
        
        /// Create an HTML attribute "rev" with the given value.
        [<Inline>]
        let rev (value: string) = "rev" => value
        
        /// Create an HTML attribute "reversed" with the given value.
        [<Inline>]
        let reversed (value: string) = "reversed" => value
        
        /// Create an HTML attribute "rows" with the given value.
        [<Inline>]
        let rows (value: string) = "rows" => value
        
        /// Create an HTML attribute "rowspan" with the given value.
        [<Inline>]
        let rowspan (value: string) = "rowspan" => value
        
        /// Create an HTML attribute "rules" with the given value.
        [<Inline>]
        let rules (value: string) = "rules" => value
        
        /// Create an HTML attribute "sandbox" with the given value.
        [<Inline>]
        let sandbox (value: string) = "sandbox" => value
        
        /// Create an HTML attribute "scheme" with the given value.
        [<Inline>]
        let scheme (value: string) = "scheme" => value
        
        /// Create an HTML attribute "scope" with the given value.
        [<Inline>]
        let scope (value: string) = "scope" => value
        
        /// Create an HTML attribute "scoped" with the given value.
        [<Inline>]
        let scoped (value: string) = "scoped" => value
        
        /// Create an HTML attribute "scrolling" with the given value.
        [<Inline>]
        let scrolling (value: string) = "scrolling" => value
        
        /// Create an HTML attribute "seamless" with the given value.
        [<Inline>]
        let seamless (value: string) = "seamless" => value
        
        /// Create an HTML attribute "selected" with the given value.
        [<Inline>]
        let selected (value: string) = "selected" => value
        
        /// Create an HTML attribute "shape" with the given value.
        [<Inline>]
        let shape (value: string) = "shape" => value
        
        /// Create an HTML attribute "size" with the given value.
        [<Inline>]
        let size (value: string) = "size" => value
        
        /// Create an HTML attribute "sizes" with the given value.
        [<Inline>]
        let sizes (value: string) = "sizes" => value
        
        /// Create an HTML attribute "span" with the given value.
        [<Inline>]
        let span (value: string) = "span" => value
        
        /// Create an HTML attribute "spellcheck" with the given value.
        [<Inline>]
        let spellcheck (value: string) = "spellcheck" => value
        
        /// Create an HTML attribute "src" with the given value.
        [<Inline>]
        let src (value: string) = "src" => value
        
        /// Create an HTML attribute "srcdoc" with the given value.
        [<Inline>]
        let srcdoc (value: string) = "srcdoc" => value
        
        /// Create an HTML attribute "srclang" with the given value.
        [<Inline>]
        let srclang (value: string) = "srclang" => value
        
        /// Create an HTML attribute "standby" with the given value.
        [<Inline>]
        let standby (value: string) = "standby" => value
        
        /// Create an HTML attribute "start" with the given value.
        [<Inline>]
        let start (value: string) = "start" => value
        
        /// Create an HTML attribute "step" with the given value.
        [<Inline>]
        let step (value: string) = "step" => value
        
        /// Create an HTML attribute "style" with the given value.
        [<Inline>]
        let style (value: string) = "style" => value
        
        /// Create an HTML attribute "subject" with the given value.
        [<Inline>]
        let subject (value: string) = "subject" => value
        
        /// Create an HTML attribute "summary" with the given value.
        [<Inline>]
        let summary (value: string) = "summary" => value
        
        /// Create an HTML attribute "tabindex" with the given value.
        [<Inline>]
        let tabindex (value: string) = "tabindex" => value
        
        /// Create an HTML attribute "target" with the given value.
        [<Inline>]
        let target (value: string) = "target" => value
        
        /// Create an HTML attribute "text" with the given value.
        [<Inline>]
        let text (value: string) = "text" => value
        
        /// Create an HTML attribute "title" with the given value.
        [<Inline>]
        let title (value: string) = "title" => value
        
        /// Create an HTML attribute "type" with the given value.
        [<Inline>]
        let ``type`` (value: string) = "type" => value
        
        /// Create an HTML attribute "usemap" with the given value.
        [<Inline>]
        let usemap (value: string) = "usemap" => value
        
        /// Create an HTML attribute "valign" with the given value.
        [<Inline>]
        let valign (value: string) = "valign" => value
        
        /// Create an HTML attribute "value" with the given value.
        [<Inline>]
        let value (value: string) = "value" => value
        
        /// Create an HTML attribute "valuetype" with the given value.
        [<Inline>]
        let valuetype (value: string) = "valuetype" => value
        
        /// Create an HTML attribute "version" with the given value.
        [<Inline>]
        let version (value: string) = "version" => value
        
        /// Create an HTML attribute "vlink" with the given value.
        [<Inline>]
        let vlink (value: string) = "vlink" => value
        
        /// Create an HTML attribute "vspace" with the given value.
        [<Inline>]
        let vspace (value: string) = "vspace" => value
        
        /// Create an HTML attribute "width" with the given value.
        [<Inline>]
        let width (value: string) = "width" => value
        
        /// Create an HTML attribute "wrap" with the given value.
        [<Inline>]
        let wrap (value: string) = "wrap" => value
        
        // }}

    /// Event handlers.
    module on =
        // Note: list of events supported by React taken from:
        // https://reactjs.org/docs/events.html#supported-events

        [<Inline>]
        let copy (f: ClipboardEvent -> unit) = "onCopy" => f

        [<Inline>]
        let cut (f: ClipboardEvent -> unit) = "onCut" => f

        [<Inline>]
        let paste (f: ClipboardEvent -> unit) = "onPaste" => f

        [<Inline>]
        let compositionEnd (f: CompositionEvent -> unit) = "onCompositionEnd" => f

        [<Inline>]
        let compositionStart (f: CompositionEvent -> unit) = "onCompositionStart" => f

        [<Inline>]
        let compositionUpdate (f: CompositionEvent -> unit) = "onCompositionUpdate" => f

        [<Inline>]
        let keyDown (f: KeyboardEvent -> unit) = "onKeyDown" => f

        [<Inline>]
        let keyPress (f: KeyboardEvent -> unit) = "onKeyPress" => f

        [<Inline>]
        let keyUp (f: KeyboardEvent -> unit) = "onKeyUp" => f

        [<Inline>]
        let focus (f: FocusEvent -> unit) = "onFocus" => f

        [<Inline>]
        let blur (f: FocusEvent -> unit) = "onBlur" => f

        [<Inline>]
        let change (f: SyntheticEvent -> unit) = "onChange" => f

        [<Inline>]
        let input (f: SyntheticEvent -> unit) = "onInput" => f

        [<Inline>]
        let invalid (f: SyntheticEvent -> unit) = "onInvalid" => f

        [<Inline>]
        let submit (f: SyntheticEvent -> unit) = "onSubmit" => f

        [<Inline>]
        let click (f: MouseEvent -> unit) = "onClick" => f

        [<Inline>]
        let contextMenu (f: MouseEvent -> unit) = "onContextMenu" => f

        [<Inline>]
        let doubleClick (f: MouseEvent -> unit) = "onDoubleClick" => f

        [<Inline>]
        let drag (f: MouseEvent -> unit) = "onDrag" => f

        [<Inline>]
        let dragEnd (f: MouseEvent -> unit) = "onDragEnd" => f

        [<Inline>]
        let dragEnter (f: MouseEvent -> unit) = "onDragEnter" => f

        [<Inline>]
        let dragExit (f: MouseEvent -> unit) = "onDragExit" => f

        [<Inline>]
        let dragLeave (f: MouseEvent -> unit) = "onDragLeave" => f

        [<Inline>]
        let dragOver (f: MouseEvent -> unit) = "onDragOver" => f

        [<Inline>]
        let dragStart (f: MouseEvent -> unit) = "onDragStart" => f

        [<Inline>]
        let drop (f: MouseEvent -> unit) = "onDrop" => f

        [<Inline>]
        let mouseDown (f: MouseEvent -> unit) = "onMouseDown" => f

        [<Inline>]
        let mouseEnter (f: MouseEvent -> unit) = "onMouseEnter" => f

        [<Inline>]
        let mouseLeave (f: MouseEvent -> unit) = "onMouseLeave" => f

        [<Inline>]
        let mouseMove (f: MouseEvent -> unit) = "onMouseMove" => f

        [<Inline>]
        let mouseOut (f: MouseEvent -> unit) = "onMouseOut" => f

        [<Inline>]
        let mouseOver (f: MouseEvent -> unit) = "onMouseOver" => f

        [<Inline>]
        let mouseUp (f: MouseEvent -> unit) = "onMouseUp" => f

        [<Inline>]
        let pointerDown (f: PointerEvent -> unit) = "onPointerDown" => f

        [<Inline>]
        let pointerMove (f: PointerEvent -> unit) = "onPointerMove" => f

        [<Inline>]
        let pointerUp (f: PointerEvent -> unit) = "onPointerUp" => f

        [<Inline>]
        let pointerCancel (f: PointerEvent -> unit) = "onPointerCancel" => f

        [<Inline>]
        let gotPointerCapture (f: PointerEvent -> unit) = "onGotPointerCapture" => f

        [<Inline>]
        let lostPointerCapture (f: PointerEvent -> unit) = "onLostPointerCapture" => f

        [<Inline>]
        let pointerEnter (f: PointerEvent -> unit) = "onPointerEnter" => f

        [<Inline>]
        let pointerLeave (f: PointerEvent -> unit) = "onPointerLeave" => f

        [<Inline>]
        let pointerOver (f: PointerEvent -> unit) = "onPointerOver" => f

        [<Inline>]
        let pointerOut (f: PointerEvent -> unit) = "onPointerOut" => f

        [<Inline>]
        let select (f: SyntheticEvent -> unit) = "onSelect" => f

        [<Inline>]
        let touchCancel (f: TouchEvent -> unit) = "onTouchCancel" => f

        [<Inline>]
        let touchEnd (f: TouchEvent -> unit) = "onTouchEnd" => f

        [<Inline>]
        let touchMove (f: TouchEvent -> unit) = "onTouchMove" => f

        [<Inline>]
        let touchStart (f: TouchEvent -> unit) = "onTouchStart" => f

        [<Inline>]
        let scroll (f: UIEvent -> unit) = "onScroll" => f

        [<Inline>]
        let wheel (f: WheelEvent -> unit) = "onWheel" => f

        [<Inline>]
        let abort (f: SyntheticEvent -> unit) = "onAbort" => f

        [<Inline>]
        let canPlay (f: SyntheticEvent -> unit) = "onCanPlay" => f

        [<Inline>]
        let canPlayThrough (f: SyntheticEvent -> unit) = "onCanPlayThrough" => f

        [<Inline>]
        let durationChange (f: SyntheticEvent -> unit) = "onDurationChange" => f

        [<Inline>]
        let emptied (f: SyntheticEvent -> unit) = "onEmptied" => f

        [<Inline>]
        let encrypted (f: SyntheticEvent -> unit) = "onEncrypted" => f

        [<Inline>]
        let ended (f: SyntheticEvent -> unit) = "onEnded" => f

        [<Inline>]
        let error (f: SyntheticEvent -> unit) = "onError" => f

        [<Inline>]
        let loadedData (f: SyntheticEvent -> unit) = "onLoadedData" => f

        [<Inline>]
        let loadStart (f: SyntheticEvent -> unit) = "onLoadStart" => f

        [<Inline>]
        let pause (f: SyntheticEvent -> unit) = "onPause" => f

        [<Inline>]
        let play (f: SyntheticEvent -> unit) = "onPlay" => f

        [<Inline>]
        let playing (f: SyntheticEvent -> unit) = "onPlaying" => f

        [<Inline>]
        let progress (f: SyntheticEvent -> unit) = "onProgress" => f

        [<Inline>]
        let rateChange (f: SyntheticEvent -> unit) = "onRateChange" => f

        [<Inline>]
        let seeked (f: SyntheticEvent -> unit) = "onSeeked" => f

        [<Inline>]
        let seeking (f: SyntheticEvent -> unit) = "onSeeking" => f

        [<Inline>]
        let stalled (f: SyntheticEvent -> unit) = "onStalled" => f

        [<Inline>]
        let suspend (f: SyntheticEvent -> unit) = "onSuspend" => f

        [<Inline>]
        let timeUpdate (f: SyntheticEvent -> unit) = "onTimeUpdate" => f

        [<Inline>]
        let volumeChange (f: SyntheticEvent -> unit) = "onVolumeChange" => f

        [<Inline>]
        let waiting (f: SyntheticEvent -> unit) = "onWaiting" => f

        [<Inline>]
        let load (f: SyntheticEvent -> unit) = "onLoad" => f

        [<Inline>]
        let animationStart (f: AnimationEvent -> unit) = "onAnimationStart" => f

        [<Inline>]
        let animationEnd (f: AnimationEvent -> unit) = "onAnimationEnd" => f

        [<Inline>]
        let animationIteration (f: AnimationEvent -> unit) = "onAnimationIteration" => f

        [<Inline>]
        let transitionEnd (f: TransitionEvent -> unit) = "onTransitionEnd" => f

        [<Inline>]
        let toggle (f: SyntheticEvent -> unit) = "onToggle" => f
