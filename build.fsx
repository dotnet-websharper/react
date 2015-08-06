#load "tools/includes.fsx"

open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.React")
        .VersionFrom("WebSharper")

let extension =
    bt.WebSharper.Extension("WebSharper.React.Bindings")
        .SourcesFromProject()

let wrapper =
    bt.WebSharper.Library("WebSharper.React")
        .SourcesFromProject()
        .References(fun ref -> 
            [
                ref.Project extension
            ])

let tests =
    bt.WebSharper.BundleWebsite("WebSharper.React.Tests")
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
                Title = Some "WebSharper.React"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://github.com/intellifactory/websharper.react"
                Description = "WebSharper bindings and abstractions for React."
                Authors = [ "IntelliFactory" ]
                RequiresLicenseAcceptance = true })
        .Add(extension)
        .Add(wrapper)
]
|> bt.Dispatch
