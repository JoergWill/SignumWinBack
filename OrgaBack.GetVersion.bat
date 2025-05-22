@ECHO OFF
:: ------------------------------------------
:: Projekt-Version ermitteln
::

SET ProjectFile=AssemblyInfo.vb 
SET HelperFile=Product.Version.txt

:: ------------------------------------------
:: Projekt-Verzeichnis einstellen
::	.\my Project
::
CD "My Project"

@FINDSTR /c:AssemblyFileVersion %ProjectFile% > %HelperFile% 2>NUL
FOR /F "delims=. tokens=2,3" %%i in (%HelperFile%) do (
	SET "HV=%%i"
	SET "NV=%%j"
)
REM ECHO Versions-Nummer X.%HV%.%NV%.xx

:: ------------------------------------------
:: Prüfen ob Versions-Nummer gerade
:: ES WERDEN NUR EINSTELLIGE REVISIONSNUMMERN DEPLOYED
::
@FINDSTR /R "\.[0-9]\.[2,4,6,8,0]" %HelperFile%
SET Result=%ErrorLevel%

:: ------------------------------------------
:: Hilfsdatei wieder löschen
::
DEL /F/Q %HelperFile% >NUL 2>NUL

:: ------------------------------------------
:: zurück zum aufrufenden Pfad
::
CD ..

:: ------------------------------------------
:: Ergebnis zurückgeben
::	0 - gerade Versions-Nummer 
::	1 - ungerade Versions-Nummer
EXIT /B %Result%