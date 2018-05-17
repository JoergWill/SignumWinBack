@echo off

:: ------------------------------------------
::	Deployment Winback-Büro
:: ------------------------------------------
::
:: ------------------------------------------
::	ToDo:	- Fehler abfangen
::	ToDo:	- Fehler beim Kopieren der Vorlagen
::	ToDo:	- Adminfreie-Version automatisch erzeugen und uploaden
::	ToDo:	- Upload auf www.winback.de (1und1)
::
:: ------------------------------------------
::

::	- neues Verzeichnis in K:\Projekte\_WinBack_Buero\cdrom erstellen
::		K:\Projekte\_WinBack_Buero\archiv\WinBack V0.0.0
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x

::	- neue Versionen in das neue Verzeichnis kopieren
::		winback.exe
::		winback.sib
::		version.txt

::	- zip-files aktualisieren
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\winback.zip
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\Mail\winback V2.x.x.zip
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\Internet\winback.zip

::	- Version.txt aktualsieren
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\Internet\version.txt

::	- ftp-Transfer nach p145032.typo3server.info/software

::	- kopieren nach cdrom
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\*.* (ohne Verzeichnisse)
::		K:\Projekte\_WinBack_Buero\cdrom\WinBack V2.x.x

::	- kopieren nach archiv
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\*.*
::		K:\Projekte\_WinBack_Buero\archiv\WinBack V2.x.x

::	- aktuelle Programmversion ermitteln (aus winback.dof)
::		Bei ungeraden Version wird das Programm 
::		beendet !!
::
Call C:\Programme\Borland\Batch\GetVersion.bat
if ErrorLevel 1 Exit /B 1

::		Jenkins-Slave wird als Dienst ausgeführt
::		(Unter Verwaltung/Dienste/Jenkins-Slave muss User will eingetragten sein)
::		Netzlaufwerk verbinden
net use K: \\Tux\WinBack

:: ------------------------------------------
:: Allgemeine Einstellungen
::
Set RootDr=K:\Projekte\_WinBack_Buero

:: ------------------------------------------
:: Pfade einstellen
::	Projekt-Pfad
::
Set Source=C:\Dokumente\Projekte\WinBac~1\
Set Vorlage=C:\Dokumente\Projekte\WinBac~1\Vorlagen\
if defined WORKSPACE Set Source=%WORKSPACE%\
if defined WORKSPACE Set Vorlage=%WORKSPACE%\Vorlagen\

Set Quelle=%RootDr%\archiv\WinBack V0.0.0
Set Target=%RootDr%\WinBack V%Version%
Set Kunden=%RootDr%\cdrom\WinBack V%Version%
Set Archiv=%RootDr%\archiv\WinBack V%Version%

Set SrcInternetVer=%Source%InternetVersion.txt 
Set TargetInternet=%Target%\Internet\Version.txt

Set SrcExe=%Source%winback.exe
Set SrcSib=%Source%winback.sib
Set SrcVer=%Source%Version.txt
Set SrcVbs=%Source%Artikel_info.vbs

Set SrcA4H=%Vorlage%Vorlage_A4_hoch.doc 
Set SrcA4Q=%Vorlage%Vorlage_A4_quer.doc 
Set SrcT4H=%Vorlage%Tabelle_A4_hoch.doc 

Set ZipCmd=C:\Programme\7-zip\7z.exe
Set ZipSetup=%Target%\winback.zip
Set ZipInternet=%Target%\Internet\winback.zip
Set ZipMail=%Target%\Mail\winback.zip
Set VerMail=%Target%\Mail\winback%Version%.zip

Set FtpPrg=C:\Programme\Curl\curl.exe

echo ----------------
echo Quelle=%Quelle%
echo Source=%Source%
echo Vorlage=%Vorlage%
echo Target=%Target%
echo ----------------

::	- Quelle aktualisieren
::		Vorlage_A4_hoch.doc
::		Vorlage_A4_quer.doc
::		Tabelle_A4_hoch.doc
::
xcopy "%SrcA4H%" "%Quelle%" /R/Y
xcopy "%SrcA4Q%" "%Quelle%" /R/Y
xcopy "%SrcT4H%" "%Quelle%" /R/Y

::	- neues Verzeichnis in K:\Projekte\_WinBack_Buero\cdrom erstellen
::		K:\Projekte\_WinBack_Buero\archiv\WinBack V0.0.0
::		K:\Projekte\_WinBack_Buero\WinBack V3.x.x
::
echo ---------------- V0.0.0 nach V3.x.x 
xcopy "%Quelle%" "%Target%" /E/I/R/Y

::	- neue Versionen in das neue Verzeichnis kopieren
::		winback.exe
::		winback.sib
::		version.txt
::
echo ---------------- .exe/.sib/.txt/.vbs nach V3.x.x 
xcopy "%SrcExe%" "%Target%" /R/Y
xcopy "%SrcSib%" "%Target%" /R/Y
xcopy "%SrcVer%" "%Target%" /R/Y
xcopy "%SrcVbs%" "%Target%" /R/Y

::	- zip-files aktualisieren
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\winback.zip
::
echo ---------------- zipcmd V3.x.x 
%ZipCmd% a "%ZipSetup%" "%SrcExe%"
%ZipCmd% a "%ZipSetup%" "%SrcSib%"
%ZipCmd% a "%ZipSetup%" "%SrcVer%"
%ZipCmd% a "%ZipSetup%" "%SrcVbs%"

::	- zip-files aktualisieren
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\Internet\winback.zip
::
echo ---------------- zipcmd \Internet\V3.x.x 
%ZipCmd% a "%ZipInternet%" "%SrcExe%"
%ZipCmd% a "%ZipInternet%" "%SrcSib%"
%ZipCmd% a "%ZipInternet%" "%SrcVer%"
%ZipCmd% a "%ZipInternet%" "%SrcVbs%"

::	- zip-files aktualisieren
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\Mail\winback.zip
::		Rename winback.zip winback V2.x.x.zip
::
echo ---------------- zipcmd \Mail\V3.x.x 
%ZipCmd% a "%ZipMail%" "%SrcExe%"
%ZipCmd% a "%ZipMail%" "%SrcSib%"
%ZipCmd% a "%ZipMail%" "%SrcVer%"
%ZipCmd% a "%ZipMail%" "%SrcVbs%"

::Del /S/F "%VerMail%"
::xcopy /R/Y "%ZipMail%" "%VerMail%" 
::Del /S/F "%ZipMail%" 

::	- Version.txt erzeugen
::		Versions-Nummer aus GetVersion.bat schreiben nach
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\Internet\version.txt
echo %Version% 1> "%TargetInternet%" 
echo 2.30 1>> "%TargetInternet%" 

::	- Dateien für www.winback.de nach C:\Temp kopieren
::	- ftp-Transfer nach www.winback.de/software
::
%FtpPrg% -T "%ZipInternet%" -u p36038431-software:winback2011 ftp://home97076023.1and1-data.host
%FtpPrg% -T "%TargetInternet%" -u p36038431-software:winback2011 ftp://home97076023.1and1-data.host

::	- kopieren nach cdrom
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\*.* (ohne Verzeichnisse)
::		K:\Projekte\_WinBack_Buero\cdrom\WinBack V2.x.x
mkdir "%Kunden%"
xcopy "%Target%" "%Kunden%" /R/Y

::	- kopieren nach archiv
::		K:\Projekte\_WinBack_Buero\WinBack V2.x.x\*.*
::		K:\Projekte\_WinBack_Buero\archiv\WinBack V2.x.x
mkdir "%Archiv%"
xcopy "%Target%" "%Archiv%" /R/Y




