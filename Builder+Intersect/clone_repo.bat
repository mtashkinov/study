@echo off

if not "%BUILD_BAT%"=="BUILD_BAT" goto clone_repoend
echo "Cloning project..." >> %BUILD_LOG%
git clone %REPO_URL% %BUILD_FOLDER% > %GIT_LOG% 2>&1
:clonerepoend