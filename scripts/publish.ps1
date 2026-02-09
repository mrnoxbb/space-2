param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64",
    [string]$OutputRoot = "publish"
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$projectPath = Join-Path $repoRoot "SpaceVenueApp\SpaceVenueApp.csproj"
$outputPath = Join-Path $repoRoot $OutputRoot

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw "dotnet SDK is required to publish the app. Install .NET 8 SDK and re-run."
}

Write-Host "Publishing Space Venue Manager..."

& dotnet publish $projectPath `
    -c $Configuration `
    -r $Runtime `
    --self-contained true `
    /p:PublishSingleFile=true `
    /p:IncludeNativeLibrariesForSelfExtract=true `
    /p:PublishReadyToRun=true `
    /p:DebugType=None `
    /p:DebugSymbols=false `
    -o $outputPath

Write-Host "Publish output: $outputPath"
