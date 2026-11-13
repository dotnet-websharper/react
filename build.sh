#!/bin/bash
set -e

# Encapsulated build logic (mirrors build.cmd): restore tools, refresh the
# wsbuild group (fetches the build-script), then run build.fsx with the target.
# The build stack calls `./build <target>` (e.g. CI-Release, WS-Package).
dotnet tool restore
dotnet paket update -g wsbuild --no-install

if [ "$#" -eq 0 ]; then
    dotnet fsi ./build.fsx
else
    dotnet fsi ./build.fsx -t "$@"
fi
