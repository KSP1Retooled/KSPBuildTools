# Locating your KSP Install

KSP1Retooled.BuildTools needs to know where you have KSP installed in order to reference the game dlls. These are all specific to your own computer, and should not be included in your git repo.

There are several options for this. KSP1Retooled.BuildTools will choose in the following order. Either [autodiscovery in the solution directory](#solution-directory) or [setting a reference path in a .user file](#kspbt-gameroot-msbuild-property) are the recommended methods for most users.

### Per-Game Properties

If {confval}`KSPBT_StockGameRoot` and {confval}`KSPBT_RetooledGameRoot` are both set, the mod is built for both games using those installs. See [](retooled.md).

### KSPBT_GameRoot MSBuild Property

If the {confval}`KSPBT_GameRoot` MSBuild property is already set, KSP1Retooled.BuildTools will use it as-is. This can be set in your .csproj.user file.

### Environment Variable

Set the {envvar}`KSP_ROOT` environment variable to the root of a KSP install. This is useful for CI workflows such as those using the {gha:action}`compile` action.

### Solution Directory

KSP1Retooled.BuildTools will look for a "KSP" directory in your solution directory and use it if it is a valid install. It identifies valid installs by looking for `assembly-csharp.dll` in the appropriate subdirectory for your operating system.

### Reference Path

KSP1Retooled.BuildTools will use the `ReferencePath` MSBuild property if it is a valid KSP install. This can be set in a user file located at `{csproj path}.user`. If you use Visual Studio, it can generate this file and property for you.

### From Steam

KSP1Retooled.BuildTools will use the default Steam install location if it is a valid install