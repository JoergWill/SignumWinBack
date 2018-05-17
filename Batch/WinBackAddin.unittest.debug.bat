@echo off
REM call msbuildpath.bat
REM "%MSBUILDDIR%msbuild.exe" .\WinBackUnitTest\WinbackUnitTest.vbproj /p:Configuration=Release

:: ------------------------------------------
:: Pfad zu vstest.console.exe
::
call mstestpath.bat
cd %WORKSPACE%\WinBackUnitTest\bin\Debug"

:: ------------------------------------------
:: Test ausführen
::
"%MSTESTDIR%vstest.console.exe" WinBackUnitTest.dll /Logger:trx

:: ------------------------------------------
:: zurück zum Aufruf-Pfad
::
cd %WORKSPACE%\Batch
