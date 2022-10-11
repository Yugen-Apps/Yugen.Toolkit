![logo](/Yugen.Toolkit.Uwp.Samples/Assets/StoreLogo.scale-400.png)

# Yugen Toolkit

## The Toolkit is a collection of helper functions, custom controls, and app services. It helps develop common task for UWP apps. 

### Contributing
Everyone is welcome to contribute, if you're looking to help out with the project, feel free to join and submit new helper functions, custom controls, and app services that you find useful. If you find an issue please could you raise it in the issues section or it would be fantastic if you could have a look at rectifying the issue and submitting a pull request. 

### Getting started
The project has been primarily built for the universal Windows platform (UWP), so you'll need the latest version of [Visual Studio 2019](https://www.visualstudio.com/) (including the community edition) and the latest Windows 10 SDK which you can install as part of the Visual Studio installer.

## Docs
[https://panda-sharp.github.io/Yugen.Toolkit.Docs/](https://panda-sharp.github.io/Yugen.Toolkit.Docs/)


msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Standard\Yugen.Toolkit.Standard.csproj /p:Configuration=Release /t:pack
msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp.Mvvm\Yugen.Toolkit.Uwp.Mvvm.csproj /p:Configuration=Release /t:pack

msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp\Yugen.Toolkit.Uwp.csproj /p:Configuration=Release
.\nuget.exe pack C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp\Yugen.Toolkit.Uwp.csproj -properties Configuration=Release