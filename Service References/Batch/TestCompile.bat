@echo off

:: ------------------------------------------
::	Compiler Winback-Büro Test-Suite
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
::	C:\Dokumente\Projekte\WinBackBüro\dunit
::	oder Hudson Workspace
::
cd "%ProjectPath%"

:: ------------------------------------------
:: Compiler aufrufen
:: Test-Suite wird als Konsolen-Projekt übersetzt
::
%Compiler% -DHudson -Q %Project%

:: ------------------------------------------
:: zurück zum aufrufenden Pfad
::
cd C:\Programme\Borland\Batch