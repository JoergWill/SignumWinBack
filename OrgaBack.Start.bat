@echo off

REM Orgasoft muss von Jenkins über eine Task gestartet werden, weil ein Dienst
REM keine Programme ausführen kann

REM Diese Zeile wird nur einmal zum Erzeugen der Task benötigt
REM SCHTASKS /Create /TN OrgaBack /SC BEIEREIGNIS /EC System /MO *[System/EventID=9999] /TR "C:\Program Files (x86)\Signum\Orgasoft\Signum.Orgasoft.Main.exe /M:01 /U:SYS /P:admin"

REM
REM Workspace nach Orgasoft\Addin kopieren
REM
mkdir "C:\ProgramData\OrgaSoft\AddIn" 2>NUL
xcopy "%WORKSPACE%\WinBackAddin.*" "C:\ProgramData\OrgaSoft\AddIn" /E /C /R /Y
xcopy "%WORKSPACE%*.dll" "C:\ProgramData\OrgaSoft\AddIn" /E /C /R /Y

REM
REM noch laufende Orgasoft-Instanzen beenden
REM
SCHTASKS /end /TN OrgaBack
Taskkill /IM Signum.Orgasoft.Main.exe

REM
REM 10 Sekunden warten
REM
ping -n 10 localhost 2> NUL

REM
REM OrgaBack neu starten (aktiviert Addin)
REM
SCHTASKS /run /TN OrgaBack
