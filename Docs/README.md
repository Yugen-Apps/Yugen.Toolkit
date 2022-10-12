msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Standard\Yugen.Toolkit.Standard.csproj /p:Configuration=Release /t:pack /p:PackageVersion=1.1.1
msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Standard.Core\Yugen.Toolkit.Standard.Core.csproj /p:Configuration=Release /t:pack /p:PackageVersion=1.1.1
msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Standard.Data\Yugen.Toolkit.Standard.Data.csproj /p:Configuration=Release /t:pack /p:PackageVersion=1.1.1

msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp\Yugen.Toolkit.Uwp.csproj /p:Configuration=Release /t:pack /p:PackageVersion=1.1.1
msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp.Controls\Yugen.Toolkit.Uwp.Controls.csproj /p:Configuration=Release /t:pack /p:PackageVersion=1.1.1
msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp.Mvvm\Yugen.Toolkit.Uwp.Mvvm.csproj /p:Configuration=Release /t:pack /p:PackageVersion=1.1.1

msbuild C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp.Audio.Services.Abstractions\Yugen.Toolkit.Uwp.Audio.Services.Abstractions.csproj /p:Configuration=Release
C:\Dev\.tools\nuget.exe pack C:\Dev\Yugen.Toolkit\Yugen.Toolkit.Uwp.Audio.Services.Abstractions\Yugen.Toolkit.Uwp.Audio.Services.Abstractions.csproj -properties Configuration=Release -Version 1.1.1