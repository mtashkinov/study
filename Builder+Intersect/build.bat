@echo off

set BUILD_BAT=BUILD_BAT
call settings.bat
call clear_dir.bat
call clone_repo.bat

if ERRORLEVEL 1 (
type %GIT_LOG% >> %BUILD_LOG%
goto err_check
)

call make_sol.bat

if ERRORLEVEL 1 (
type %MAKE_SOLUTION_LOG% >> %BUILD_LOG%
goto err_check
)

:err_check
call is_succes.bat
:buildeof