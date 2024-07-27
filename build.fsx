#r "nuget: FAKE.Core"
#r "nuget: Fake.Core.Target"
#r "nuget: Fake.IO.FileSystem"
#r "nuget: Fake.Tools.Git"
#r "nuget: Fake.DotNet.Cli"
#r "nuget: Fake.DotNet.AssemblyInfoFile"
#r "nuget: Fake.DotNet.Paket"
#r "nuget: Paket.Core, 8.1.0-alpha004"
#r "nuget: MSBuild.StructuredLogger"

open Fake.Core

open Fake.Core
System.Environment.GetCommandLineArgs()
|> Array.skip 2 // skip fsi.exe; build.fsx
|> Array.toList
|> Fake.Core.Context.FakeExecutionContext.Create false __SOURCE_FILE__
|> Fake.Core.Context.RuntimeContext.Fake
|> Fake.Core.Context.setExecutionContext

#load "paket-files/wsbuild/github.com/dotnet-websharper/build-script/WebSharper.Fake.fsx"
#r "System.Xml.Linq"


open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open WebSharper.Fake
open Fake.DotNet

let targets =
    LazyVersionFrom "WebSharper" |> WSTargets.Default
    |> fun args ->
        { args with
            Attributes =
                    [
                        AssemblyInfo.Company "IntelliFactory"
                        AssemblyInfo.Copyright "(c) IntelliFactory 2023"
                        AssemblyInfo.Title "https://github.com/dotnet-websharper/react"
                        AssemblyInfo.Product "WebSharper React"
                    ]
        }
    |> MakeTargets

Target.create "AppVeyor" ignore
"WS-Update" ==> "AppVeyor"
"WS-BuildRelease" ==> "AppVeyor"
Target.runOrDefault "Build"
