#load "tools/includes.fsx"

open IntelliFactory.Build

let bt =
    BuildTool().PackageId("Zafir.React")
        .VersionFrom("Zafir")
        .WithFSharpVersion(FSharpVersion.FSharp30)
        .WithFramework(fun x -> x.Net40)

let extension =
    bt.Zafir.Extension("WebSharper.React.Bindings")
        .SourcesFromProject()

let wrapper =
    bt.Zafir.Library("WebSharper.React")
        .SourcesFromProject()
        .References(fun ref ->
            [
                ref.Project extension
            ])

let tests =
    bt.Zafir.BundleWebsite("WebSharper.React.Tests")
        .SourcesFromProject()
        .References(fun ref ->
            [
                ref.Project extension
                ref.Project wrapper
            ])

bt.Solution [
    extension
    wrapper
    tests

    bt.NuGet.CreatePackage()
        .Configure(fun configuration ->
            { configuration with
                Title = Some "Zafir.React"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://bitbucket.org/intellifactory/websharper.react"
                Description = "WebSharper bindings and abstractions for React."
                Authors = [ "IntelliFactory" ]
                RequiresLicenseAcceptance = true })
        .Add(extension)
        .Add(wrapper)
]
|> bt.Dispatch
