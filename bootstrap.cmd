@echo off
Nuget.exe restore "Source\FluentHtml.sln"

NuGet.exe install MSBuildTasks -OutputDirectory .\Tools\ -ExcludeVersion -NonInteractive
NuGet.exe install xunit.runners -OutputDirectory .\Tools\ -ExcludeVersion -NonInteractive
