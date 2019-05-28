REM @echo off

:: ------------------------------------------
::	Compiler Winback-Büro
::	angepasst an Hudson Workspace
:: ------------------------------------------

:: ------------------------------------------
:: 27.07.2012/JW
:: 	Excptions mit MadExcept abfangen
:: ------------------------------------------
::
:: ------------------------------------------

:: ------------------------------------------
:: Projekt-Variablen
::
Set Compiler=C:\Programme\Borland\Delphi7\Bin\dcc32
Set Project=WinBack

Set ProjectPath=C:\Dokumente\Projekte\WinBac~1
if defined WORKSPACE Set ProjectPath=%WORKSPACE%

:: ------------------------------------------
:: Projekt-Verzeichnis einstellen
::	C:\Dokumente\Projekte\WinBackBüro
::	oder Hudson Workspace
::
echo Versions-Nummer=%CVS_BRANCH%
echo Build-Nummer=%BUILD_NUMBER%
echo Build-ID=%BUILD_ID%
echo Job-Name=%JOB_NAME%
echo Build-Tag=%BUILD_TAG%

cd "%ProjectPath%"

:: ------------------------------------------
:: Compiler aufrufen
:: Hier wird immer die Final-Version erzeugt !!
:: Excptions mit MadExcept abfangen (27.07.2012/JW)
::
%Compiler% -DFinalVersion -DMadExcept -Q %Project%

:: ------------------------------------------
:: zurück zum aufrufenden Pfad
::
cd C:\Programme\Borland\Batch