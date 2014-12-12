@echo off

if not "%BUILD_BAT%"=="BUILD_BAT" goto clear_dirend

del %BUILD_LOG% /S /Q >nul 2>nul
del %GIT_LOG% /S /Q >nul 2>nul
del %MAKE_SOLUTION_LOG% /S /Q >nul 2>nul

if exist %BUILD_FOLDER% (
echo "Deleting build folder..." >> %BUILD_LOG%
rd %BUILD_FOLDER% /S /Q
)
:clear_dirend