@echo off

if not "%BUILD_BAT%"=="BUILD_BAT" goto settingsend
set SETTINGS_BAT=SETTINGS_BAT
set REPO_URL=d:\rep
set DOTNET_VERSION=v4.0.30319
set MSBUILD=%windir%\Microsoft.NET\Framework64\%DOTNET_VERSION%\MSbuild
set BUILD_FOLDER=d:\test\build
set PROJECT_NAME=Intersect.sln
set PATHS=paths.txt
set BLAT=blat323_64.full\blat323\full\blat.exe
set EMAIL=mikhailtash@gmail.com
set BUILD_LOG=build.log
set MAKE_SOLUTION_LOG=make_solution.log
set GIT_LOG=git.log
:settingsend