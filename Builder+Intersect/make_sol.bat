@echo off

if not "%BUILD_BAT%"=="BUILD_BAT" goto make_solend
echo "Building project..." >> %BUILD_LOG%
%MSBUILD% %BUILD_FOLDER%\%PROJECT_NAME% > %MAKE_SOLUTION_LOG%
:make_solend