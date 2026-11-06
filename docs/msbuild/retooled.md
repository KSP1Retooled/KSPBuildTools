# Building for KSP Retooled

KSP1Retooled.BuildTools builds mods for KSP 1 and for [KSP Retooled](https://github.com/KSP1Retooled). Many mods built for KSP 1.12 run on Retooled as they are, so you only need this if your mod has to be compiled against Retooled.

## Building for one game

There is nothing to set up. The game is detected from the install found by [the usual steps](ksp-install.md), so pointing {confval}`KSPBT_GameRoot` at a Retooled install is all it takes.

When building against Retooled:

- The `KSP_RETOOLED` and `UNITY_6_7` symbols are defined
- `netstandard.dll` is referenced as well
- The assembly gets a `[KSPAssemblyDependency("KSP", 1, 13)]` attribute, so stock KSP won't load it. This is part of {confval}`KSPBT_GenerateDependencyAttributes`
- `KSPVersionFile` items default to that version of KSP

The symbols let you share code between both games:

```csharp
#if KSP_RETOOLED
    // Only compiled for Retooled
#else
    // Only compiled for KSP 1
#endif
```

{confval}`KSPBT_TargetGame` and {confval}`KSPBT_GameVersion` hold what was detected, if you need them in your own targets. They are set once KSP1Retooled.BuildTools' targets have been evaluated, so they aren't available in the body of your csproj.

## Building for both games

To build for both games at once, set the path to each install. Since these are specific to your computer, they belong in a user file and not in your git repo:

```xml
<!-- MyMod.csproj.user -->
<Project>
  <PropertyGroup>
    <KSPBT_KSP1GameRoot>C:\Games\Kerbal Space Program</KSPBT_KSP1GameRoot>
    <KSPBT_RetooledGameRoot>C:\Games\KSP Retooled</KSPBT_RetooledGameRoot>
  </PropertyGroup>
</Project>
```

These can also be set as environment variables. You have to set both. With neither set, the project builds for whichever game is detected, so anyone without a second install can still work on your mod.

Each build then compiles the project twice and puts both DLLs in {confval}`KSPBT_ModPluginFolder`:

```
GameData/MyMod/Plugins/
  MyMod.dll       loaded by KSP 1
  MyMod.1.13.dll  loaded by Retooled
```

Both are named `MyMod` as far as the runtime is concerned, and only the file differs. Retooled loads the DLL requiring the newest version of KSP that it can satisfy, and stock KSP refuses `MyMod.1.13.dll` because of its dependency on KSP 1.13. Both use the same `PluginData` folder as long as {confval}`KSPBT_GenerateAssemblyAttribute` is on.

The Retooled build uses its own `bin/retooled` and `obj/retooled` folders. A few things are specific to building both:

- Only the project's own DLL is copied for Retooled. Other files in the output folder, such as NuGet dependencies set to copy locally, come from the KSP 1 build.
- Project references are built for the matching game, so `dotnet build` on one project is enough.
- `ModReference` items are resolved in each install, and installed into each by CKAN.
- Version files are written by the Retooled build, with `KSP_VERSION` and `KSP_VERSION_MAX` set to its version. `KSP_VERSION_MIN` keeps its default of 1.8.

To build for only one of them, set {confval}`KSPBT_TargetGame`:

```console
dotnet build -p:KSPBT_TargetGame=Retooled
```

## Building in CI

The {gha:action}`compile` action unpacks its `ksp-zip-url` to `KSP_ROOT`, by default `/tmp/ksp`. To build for Retooled only, set `ksp-zip-url` to a `retooled-refs.zip`. To build for both games, unpack it first and set both paths:

```yaml
- name: Download Retooled libs
  run: |
    wget --quiet <url of retooled-refs.zip> -O /tmp/retooled-refs.zip
    unzip -q /tmp/retooled-refs.zip -d /tmp/retooled

- uses: KSP1Retooled/KSPBuildTools/.github/actions/compile@main
  env:
    KSPBT_KSP1GameRoot: /tmp/ksp
    KSPBT_RetooledGameRoot: /tmp/retooled
```

A KSP libs zip has no `readme.txt` unless it includes one, in which case the version falls back to 1.12.5 or 1.13.0. Set {confval}`KSPBT_GameVersion` to override it.
