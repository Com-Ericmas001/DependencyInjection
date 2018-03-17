@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

call %NuGet% restore DependencyInjection.sln -NonInteractive
msbuild DependencyInjection.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

mkdir Build
mkdir Build\lib
mkdir Build\lib\net40
mkdir Build\lib\portable-net45+wp80+win8+wpa81
mkdir Build\lib\portable-net40+sl5+wp80+win8+wpa81

%nuget% pack "DependencyInjection.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
