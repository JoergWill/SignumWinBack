@echo off

:: ------------------------------------------
::	Compiler Winback-B�ro Test-Suite
::	angepasst and Hudson Workspace
:: ------------------------------------------
::

:: ------------------------------------------
:: Projekt-Variablen
::
Set Compiler=C:\Programme\Borland\Delphi7\Bin\dcc32
Set Project=WinBackTests

Set ProjectPath=C:\Dokumente\Projekte\WinBac~1\dunit
if defined WORKSPACE Set ProjectPath=%WORKSPACE%\dunit

:: ------------------------------------------
:: Projekt-Verzeichnis einstellen
::	C:\Dokumente\Projekte\WinBackB�ro\dunit
::	oder Hudson Workspace
::
cd "%ProjectPath%"

:: ------------------------------------------
:: Compiler aufrufen
:: Test-Suite wird als Konsolen-Projekt �bersetzt
::
%Compiler% -DHudson -Q %Project%

:: ------------------------------------------
:: zur�ck zum aufrufenden Pfad
::
cd C:\Programme\Borland\Batch