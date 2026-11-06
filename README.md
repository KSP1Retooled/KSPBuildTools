KSP1Retooled Build Tools
========================

[![Documentation Status](https://readthedocs.org/projects/kspbuildtools/badge/?version=latest)](https://kspbuildtools.readthedocs.io/en/latest/?badge=latest)
[![CI](https://github.com/KSP1Retooled/KSPBuildTools/actions/workflows/internal-ci.yml/badge.svg)](https://github.com/KSP1Retooled/KSPBuildTools/actions/workflows/internal-ci.yml)
[![NuGet Version](https://img.shields.io/nuget/vpre/KSP1Retooled.BuildTools)](https://www.nuget.org/packages/KSP1Retooled.BuildTools)

This repository provides a common set of tools for developing mods for Kerbal Space Program and KSP Retooled. It integrates with MSBuild to simplify setting up plugins, and integrates with CKAN to easily reference other mods. it also includes a set of CI actions for automating builds.

[Full Documentation](https://kspbuildtools.readthedocs.io/en/)

## Installation

Run `dotnet add package KSP1Retooled.BuildTools` on your project, or add

```xml
<ItemGroup>
  <PackageReference Include="KSP1Retooled.BuildTools"/> 
</ItemGroup>
```

to the .csproj file. Pinning the version is highly recommended. 

## Usage

Once you [have a KSP installation to link to](https://kspbuildtools.readthedocs.io/en/stable/msbuild/ksp-install.html), all the game DLLs will be automatically included in your project automatically. 

Configure your mod's location in GameData and where to put the output DLLs

```xml
<!-- DLLs will be written to ../GameData/ModName/Plugins/ -->
<KSPBT_ModRoot>$(MSBuildThisFileDirectory)/../GameData/$(MSBuildProjectName)</KSPBT_ModRoot>
<KSPBT_ModPluginFolder>plugins</KSPBT_ModPluginFolder>
```

Reference dependency mods in your DLL by adding `ModReference` items to the project. They will be automatically installed using CKAN.

```xml
<!-- Depends on Modulemanager and Harmony -->
<ItemGroup>
  <ModReference Include="Modulemanager">
    <DLLPath>GameData/Modulemanager*.dll</DLLPath>
    <CKANIdentifier>ModuleManager</CKANIdentifier>
  </ModReference>
  <ModReference Include="0Harmony">
    <DLLPath>GameData/000_Harmony/0Harmony.dll</DLLPath>
    <CKANIdentifier>Harmony2</CKANIdentifier>
  </ModReference>
</ItemGroup>
```

Auto-generate version files from your project's `FileVersion`. This works well with [minver](https://github.com/adamralph/minver).

```xml
<!-- Version Files -->
<ItemGroup>
  <KSPVersionFile Include=".">
    <Destination>$(KSPBT_ModRoot)/mymod.version</Destination>
    <URL>https://github.com/username/repo/releases/latest/download/mymod.version</URL>
    <Download>https://github.com/username.repo/releases/latest</Download>
  </KSPVersionFile>
</ItemGroup>
```

From there you should be able to build your mod with just `dotnet build`

## KSP Retooled

Whether you are building for KSP 1 or for KSP Retooled is detected from your KSP install. When building for Retooled the `KSP_RETOOLED` and `UNITY_6_7` symbols are defined.

To build for both, set the path to each install in your `.csproj.user` file. Your mod then gets a `MyMod.dll` for KSP 1 and a `MyMod.1.13.dll` for Retooled, and each game loads the right one.

```xml
<KSPBT_KSP1GameRoot>C:\Games\Kerbal Space Program</KSPBT_KSP1GameRoot>
<KSPBT_RetooledGameRoot>C:\Games\KSP Retooled</KSPBT_RetooledGameRoot>
```

For more details, see the [MSBuild section in the docs](https://kspbuildtools.readthedocs.io/en/stable/msbuild/index.html).
