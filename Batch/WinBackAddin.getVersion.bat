@echo off
:: ------------------------------------------
:: Projekt-Version ermitteln
::


:: ------------------------------------------
:: Pfade einstellen
::	Projekt-Pfad
::	Projekt-File mit Versions-Nummer
::
Set ProjectPath=C:\Users\will.WINBACK\Source\Repos\Signum_WinBack\My Project
if defined WORKSPACE Set ProjectPath=%WORKSPACE%\My Project

Set ProjectFile=AssemblyInfo.vb 
Set HelperFile=ProductVersion.txt

:: ------------------------------------------
:: Projekt-Verzeichnis einstellen
::	C:\Users\will.WINBACK\Source\Repos\Signum_WinBack\My Project
::	oder Jenkins Workspace
::
cd "%ProjectPath%"

findstr AssemblyFileVersion %ProjectFile% > %HelperFile%
For /F "delims=(. tokens=4" %%i in (%HelperFile%) do (
	set "Version=%%i"
)
echo Versions-Nummer %Version% > %HelperFile%

:: ------------------------------------------
:: Prüfen ob Versions-Nummer gerade
::
findstr /R "[2,4,6,8,0]" %HelperFile%
Set Result=%ErrorLevel%

:: ------------------------------------------
:: Hilfsdatei wieder löschen
::
del /F/Q %HelperFile%


:: ------------------------------------------
:: zurück zum aufrufenden Pfad
::
cd ./../Batch


:: ------------------------------------------
:: Ergebnis zurückgeben
::	0 - gerade Versions-Nummer 
::	1 - ungerade Versions-Nummer
exit /B %Result%