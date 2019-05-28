@echo off
REM call msbuildpath.bat
REM "%MSBUILDDIR%msbuild.exe" .\WinBackUnitTest\WinbackUnitTest.vbproj /p:Configuration=Release

:: ------------------------------------------
:: Pfad zu vstest.console.exe
::
cd %WORKSPACE%\Batch
call mstestpath.bat

:: ------------------------------------------
:: Test ausführen
::
cd %WORKSPACE%\WinBackUnitTest\bin\Debug"
"%MSTESTDIR%vstest.console.exe" WinBackUnitTest.dll /Logger:trx

:: ------------------------------------------
:: zurück zum Aufruf-Pfad
::
cd %WORKSPACE%\Batch
