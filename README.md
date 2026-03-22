# SwapLogo

Swap the intro Team Cherry logo to others.

## Build

.NET 10 is required.

Create `SilksongPath.props`. Copy and paste the following text and edit as needed.

```xml
<Project>
  <PropertyGroup>
    <SilksongFolder>C:/Users/carri/AppData/Roaming/r2modmanPlus-local/HollowKnightSilksong/profiles/dev</SilksongFolder>
    <!-- If you use a mod manager rather than manually installing BepInEx, this should be a profile directory for that mod manager. -->
    <SilksongPluginsFolder>$(SilksongFolder)/BepInEx/plugins</SilksongPluginsFolder>
  </PropertyGroup>
</Project>
```

```
dotnet build -c Release
```