param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64",
    [string]$OutputRoot = "publish"
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot

$publishScript = Join-Path $PSScriptRoot "publish.ps1"
& $publishScript -Configuration $Configuration -Runtime $Runtime -OutputRoot $OutputRoot

if (-not (Get-Command iscc -ErrorAction SilentlyContinue)) {
    throw "Inno Setup (iscc) is required to build the installer. Install Inno Setup and ensure iscc is on PATH."
}

$installerScript = Join-Path $repoRoot "installer\SpaceVenueInstaller.iss"
& iscc $installerScript

Write-Host "Installer generated in the installer\Output folder."
