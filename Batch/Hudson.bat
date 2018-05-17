@echo off

:: ------------------------------------------
::	Start Hudson
:: ------------------------------------------
::


:: ------------------------------------------
:: Hudson Programm-Verzeichnis einstellen
::	C:\Programme\Borland\Hudson
::
cd C:\Programme\Borland\Hudson

:: ------------------------------------------
:: Hudson starten
:: Aufruf im Explorer oder in Firefox
:: 	http://localhost:8080
::
java -jar Hudson-2.2.0.war --httpPort=8090
