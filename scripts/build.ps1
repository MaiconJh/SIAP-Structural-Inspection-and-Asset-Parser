$ErrorActionPreference = 'Stop'

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
  Write-Error "dotnet CLI not found. Install .NET 8 SDK to build."
  exit 1
}

$repoRoot = Split-Path -Parent $PSScriptRoot
Set-Location $repoRoot

dotnet build "src/SIAP.sln"
