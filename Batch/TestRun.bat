@echo off

:: ------------------------------------------
::	Run Winback-Büro Test-Suite
::	angepasst an Hudson Workspace
:: ------------------------------------------
::
:: ------------------------------------------
::	21.02.2012/JW Deployment entfernt
::	23.03.2012/JW winback.ini in C:\Temp
::

:: ------------------------------------------
:: Projekt-Variablen
::
Set Deploy=C:\Programme\Borland\Batch\CheckDeploy.bat
Set Runner=winbackTests -ini C:\temp\winback.ini

Set ProjectPath=C:\Dokumente\Projekte\WinBac~1\dunit
if defined WORKSPACE Set ProjectPath=%WORKSPACE%\dunit

Set SilPath=C:\Dokumente\Projekte\WinBac~1\
if defined WORKSPACE Set SilPath=%WORKSPACE%\


:: ------------------------------------------
:: Projekt-Verzeichnis einstellen
::	C:\Dokumente\Projekte\WinBackBüro\dunit
::
cd "%ProjectPath%"

:: ------------------------------------------
:: winback.sib kopieren aus Programm-Pfad
::
copy "%SilPath%"\winback.sib "%ProjectPath%"

:: ------------------------------------------
:: winback.ini kopieren aus Programm-Pfad nach
::	C:\Temp\winback.ini
::
:: 	damit die winback.ini im csv-Verzeichnis
:: 	bleiben und trotzdem vom Test-Programm
:: 	verändert werden kann.
::
copy "%ProjectPath%"\winback.ini C:\Temp\winback.ini

:: ------------------------------------------
:: Test aufrufen
::
%Runner%
echo %Errorlevel%

:: ------------------------------------------
:: Wenn der Test erfolgreich war
:: 	Delpoyment aufrufen
::
Set Test=%Errorlevel%
cd C:\Programme\Borland\Batch
if %Test% == 0 Call %Deploy%

:: ------------------------------------------
:: zurück zum aufrufenden Pfad
::
cd C:\Programme\Borland\Batch

echo /B %Test%