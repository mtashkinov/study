@echo off

if not "%BUILD_BAT%"=="BUILD_BAT" goto is_succesend
for /f "tokens=*" %%a in (%PATHS%) do (
    if not exist %BUILD_FOLDER%\%%a (
       .\%BLAT% %BUILD_LOG% -subject "Build" -to %EMAIL% 
       goto is_succesend
    )
)
echo "Done!" >> %BUILD_LOG%
.\%BLAT% -subject "Build" -body "Ok" -to %EMAIL% 
:is_succesend