# Configuration Reference

## Properties

```{confval} KSPBT_ModRoot
---
default: `$(MSBuildProjectDir)/../GameData/$(MSBuildProjectName)/`
---

specifies the root directory of your mod (the folder that gets placed into GameData).  Generally you'll want to set this to be relative to the csproj file using `$(MSBuildThisFileDirectory)`.
```

```{confval} KSPBT_ModPluginFolder
---
default: `./`
---

the directory where compiled binaries should be copied. This is relative to the {confval}`KSPBT_ModRoot`. The DLLs will be copied to this directory after each build.
```

```{confval} KSPBT_CKANCompatibleVersions
---
default: `1.12 1.11 1.10 1.9 1.8`
---

Used by the `CKANInstall` target to set additional KSP versions to treat as compatible when installing 
```

```{confval} KSPBT_ReferenceSystemAssemblies
---
default: `true`
---
If set to `true`, adds assembly references to Mono System DLLs.
```

```{confval} KSPBT_ReferenceUnityAssemblies
---
default: `true`
---
If set to `true`, adds references to the Unity assemblies listed in `KSPAssemblies.targets`.
```

```{confval} KSPBT_ReferenceGameAssemblies
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, adds references to the KSP assemblies listed in `KSPAssemblies.targets`, such as Assembly-CSharp and KSPAssets.
```

```{confval} KSPBT_ReferenceModAssemblies
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, adds references to the assemblies included in `ModReference` list.
```

```{confval} KSPBT_GenerateAssemblyAttribute
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, automatically generates the `KSPAssembly` for your assembly from the `Version` property.
```

```{confval} KSPBT_GenerateDependencyAttributes
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, automatically generates `KSPAssemblyDependency` attributes for each dependency. Dependencies should have either the `CKANIdentifier` metadata or `KSPAssemblyName` metadata. Versions can be supplied with `CKANVersion` or `KSPAssemblyVersion`. Building for Retooled also adds a dependency on that version of KSP. 
```

```{confval} KSPBT_CopyDLLsToPluginFolder
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, automatically copies the compiled DLL to the {confval}`KSPBT_ModPluginFolder`.
```

```{confval} KSPBT_GenerateVersionFile
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, automatically generates a version file using the information in any {confval}`KSPVersionFile` items. Without defining a {confval}`KSPVersionFile`, nothing will be generated. See [](generating-version-files.md/) for more details. 
```

````{confval} KSPBT_GameRoot
```{warning}
You should **not** set or use this property in your csproj file.
```
This property should be set to the root directory of your KSP install. see [Locating your KSP Install](ksp-install)
````

```{confval} KSPBT_TargetGame
---
default: detected from the KSP install
---
The game being built for, either `KSP1` or `Retooled`. Set this on the command line to build for only one game when {confval}`KSPBT_KSP1GameRoot` and {confval}`KSPBT_RetooledGameRoot` are set. The build fails if the assemblies it finds are from the other game.
```

```{confval} KSPBT_GameVersion
---
default: read from `readme.txt` in the KSP install, otherwise `1.12.5` for KSP 1 and `1.13.0` for Retooled
---
The version of the game being built for. For Retooled this decides the `KSPAssemblyDependency` on KSP, and the file name of the Retooled DLL when building for both games.
```

````{confval} KSPBT_KSP1GameRoot
```{warning}
You should **not** set or use this property in your csproj file.
```
The root directory of your KSP 1 install. Setting this and {confval}`KSPBT_RetooledGameRoot` builds your mod for both games. See [](retooled.md).
````

````{confval} KSPBT_RetooledGameRoot
```{warning}
You should **not** set or use this property in your csproj file.
```
The root directory of your KSP Retooled install. Setting this and {confval}`KSPBT_KSP1GameRoot` builds your mod for both games. See [](retooled.md).
````

## Items

````{confval} ModReference
A reference to another mod that is a dependency. This mod will be automatically referenced in the build process and installed using CKAN if an identifier is given. See [](dependencies.md) for examples.

```{rubric} Metadata
```

```{describe} Identity
The name of the mod you are referencing, as set in that mod's `KSPAssemblyAttribute`
```

```{describe} DLLPath
The path of the mod's assembly to reference when building, relative to {confval}`KSPBT_GameRoot`.
```

```{describe} CKANIdentifier
The name of the mod in CKAN to install before building.
```

```{describe} CKANVersion
The specific version to install from CKAN, if any.
```

Any additional metadata is copied to the resulting [`Reference`](https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-items?view=vs-2022#reference) item 
````

````{confval} KSPVersionFile
Defines a version file to generate. See [](generating-version-files.md) for examples.

```{rubric} Metadata
```

```{describe} Identity
To create a new version file from scratch, set to `.`. Otherwise set to a path to a json version file to use as a base
```
```{describe} Destination
Path to where the generated json version file should be placed
```

```{confval} Name
---
default: `$(ProjectName)`
---
The mod name. Corresponds to the `NAME` value in json.
```

```{confval} Version
---
default: `$(FileVersion)`
---
The mod version. Corresponds with the `VERSION` value in json.
```

```{describe} URL
The URL of the remote version file. Corresponds with the `URL` value in json.
```

```{describe} Download
Where to link players to update your mod. Corresponds with the `DOWNLOAD` value in json.
```

```{confval} KSP_Version
---
default: `1.12`, or the game's version in Retooled
---
The KSP version the mod is targeting. Corresponds with the `KSP_VERSION` value in json.
```

```{confval} KSP_Version_Min
---
default: `1.8`, or the game's version when only building for Retooled
---
The minimum supported KSP version. Corresponds with the `KSP_VERSION_MIN` value in json.
```

```{confval} KSP_Version_Max
---
default: `1.12`, or the game's version in Retooled
---
The maximum supported KSP version. Corresponds with the `KSP_VERSION_MAX` value in json.
```

````
