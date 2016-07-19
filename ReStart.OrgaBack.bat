REM Dateien aus dem Workspace nach Orgasoft-Addin kopieren
mkdir "C:\ProgramData\OrgaSoft\AddIn" 2>NUL
xcopy "%WORKSPACE%\WinBackAddin.*" "C:\ProgramData\OrgaSoft\AddIn" /E /C /R /Y
xcopy "%WORKSPACE%*.dll" "C:\ProgramData\OrgaSoft\AddIn" /E /C /R /Y

REM noch laufende Orgasoft-Instanzen beenden
Taskkill /IM Signum.Orgasoft.Main.exe
REM 10 Sekunden warten
ping -n 10 localhost 2> NUL

REM OrgaBack neu starten (aktiviert Addin)
Start /B "OrgaBack" "C:\Program Files (x86)\Signum\Orgasoft\Signum.Orgasoft.Main.exe" /M:01 /U:SYS /P:admin
