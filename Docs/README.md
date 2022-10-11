msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp.Mvvm\Yugen.Toolkit.Uwp.Mvvm.csproj /p:Configuration=Release /t:pack

msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp\Yugen.Toolkit.Uwp.csproj /p:Configuration=Release
C:\Dev\.tools\nuget.exe pack C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp\Yugen.Toolkit.Uwp.csproj -properties Configuration=Release