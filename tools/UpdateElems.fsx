#nowarn "25"

open System.IO
open System.Text.RegularExpressions

module Tags =

    let NeedsBuilding input output =
//        let i = FileInfo(input)
//        let o = FileInfo(output)
//        not o.Exists || o.LastWriteTimeUtc < i.LastWriteTimeUtc
        true

    let tagsFilePath =
        Path.Combine(__SOURCE_DIRECTORY__, "..", "paket-files", "includes", "dotnet-websharper", "websharper", "src", "htmllib", "tags.csv")

    let groupByFst (s: seq<'a * 'b>) : seq<'a * seq<'b>> =
        s
        |> Seq.groupBy fst
        |> Seq.map (fun (k, s) -> k, Seq.map snd s)

    let Parse() =
        File.ReadAllLines(tagsFilePath)
        |> Array.map (fun line ->
            let [|``type``; status; isKeyword; name; srcname|] = line.Split ','
            let isKeyword = if isKeyword = "keyword" then true else false
            (``type``, (status, (isKeyword, name, srcname))))
        |> groupByFst
        |> Seq.map (fun (k, s) -> (k, groupByFst s |> Map.ofSeq))
        |> Map.ofSeq

    let start =
        Regex("""
            # indentation
            ^(\s*)
            # comment and {{ marker
            //\s* {{ \s*
            # type (tag, attr, event, etc.)
            ([a-z]+) \s*
            # categories (normal, deprecated, colliding, event arg type)
            (?: ([a-z]+) \s* )*
            # identifier to distinguish between blocks in the same file
            (?: \[ ([a-z]+) \] )?
            """,
            RegexOptions.IgnorePatternWhitespace)
    let finish = Regex("// *}}")
    let dash = Regex("-([a-z])")

    type Elt =
        {
            /// tag, attr, event, etc.
            Type: string
            /// for tag, attr: normal / deprecated / colliding
            Category: string
            /// for event: the type of event arg
            /// javascript name
            Name: string
            /// lowercase name for F# source
            LowName: string
            /// lowercase name for F# source, with ``escapes`` if necessary
            LowNameEsc: string
            /// PascalCase name for F# source
            PascalName: string
            /// Unique identifier for a group in a given F# file
            Uid: string
        }
        /// camelCase name for F# source
        member this.CamelName =
            if System.Char.IsLower this.LowName.[0] then
                string(this.PascalName.[0]).ToLowerInvariant() + this.PascalName.[1..]
            else
                this.PascalName

        member this.CamelNameEsc =
            let s = this.CamelName
            if s = this.LowName then this.LowNameEsc else s

    let RunOn (path: string) (all: Map<string, Map<string, seq<bool * string * string>>>) (f: Elt -> string[]) =
        if NeedsBuilding tagsFilePath path then
            let e = (File.ReadAllLines(path) :> seq<_>).GetEnumerator()
            let newLines =
                [|
                    while e.MoveNext() do
                        yield e.Current
                        let m = start.Match(e.Current)
                        if m.Success then
                            while e.MoveNext() && not (finish.Match(e.Current).Success) do ()
                            let indent = m.Groups.[1].Value
                            let ``type`` = m.Groups.[2].Value
                            let allType =
                                match m.Groups.[3].Captures |> Seq.cast<Capture> |> Array.ofSeq with
                                | [||] ->
                                    seq {
                                        for KeyValue(category, elts) in all.[``type``] do
                                            for elt in elts do
                                                yield category, elt
                                    }
                                | categories ->
                                    seq {
                                        for s in categories do
                                            match Map.tryFind s.Value all.[``type``] with
                                            | None -> ()
                                            | Some elts ->
                                                for elt in elts do yield s.Value, elt
                                    }
                                |> Seq.sortBy (fun (_, (_, _, s)) -> s.ToLower())
                            let uid =
                                match m.Groups.[4].Captures |> Seq.cast<Capture> |> Seq.tryHead with
                                | Some c -> c.Value
                                | None -> ""
                            for category, (isKeyword, name, upname) in allType do
                                let lowname = dash.Replace(name, fun m ->
                                    m.Groups.[1].Value.ToUpperInvariant())
                                let lownameEsc = if isKeyword then sprintf "``%s``" lowname else lowname
                                let x =
                                    {
                                        Type = ``type``
                                        Category = category
                                        Name = name
                                        LowName = lowname
                                        LowNameEsc = lownameEsc
                                        PascalName = upname
                                        Uid = uid
                                    }
                                for l in f x do
                                    yield indent + l
                            yield e.Current
                |]
            File.WriteAllLines(path, newLines)

    let Run() =
        let all = Parse()
        RunOn (Path.Combine(__SOURCE_DIRECTORY__, "..", "WebSharper.React", "Html.fs")) all <| fun e ->
            match e.Type with
            | "tag" ->
                [|
                    sprintf "/// Create an HTML element <%s> with props and children." e.Name
                    "[<Inline; Macro(typeof<Macros.Html>)>]"
                    sprintf "let %s props children = React.Element \"%s\" props children" e.LowNameEsc e.Name
                    ""
                |]
            | "svgtag" ->
                [|
                    sprintf "/// Create an SVG element <%s> with props and children." e.Name
                    "[<Inline>]"
                    sprintf "let %s props children = React.Element \"%s\" props children" e.LowNameEsc e.Name
                    ""
                |]
            | "attr" ->
                [|
                    sprintf "/// Create an HTML attribute \"%s\" with the given value." e.Name
                    "[<Inline>]"
                    sprintf "let %s (value: string) = \"%s\" => value" e.LowNameEsc e.LowName
                    ""
                |]

Tags.Run()
