@echo off
REM call msbuildpath.bat
REM "%MSBUILDDIR%msbuild.exe" .\WinBackUnitTest\WinbackUnitTest.vbproj /p:Configuration=Release
call mstestpath.bat
cd ".\WinBackUnitTest\bin\Release"
"%MSTESTDIR%vstest.console.exe" WinBackUnitTest.dll
cd ..\..\..
