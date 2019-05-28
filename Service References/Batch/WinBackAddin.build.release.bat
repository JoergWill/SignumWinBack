@echo off
call msbuildpath.bat
"%MSBUILDDIR%msbuild.exe" WinbackAddin.vbproj /p:Configuration=Release