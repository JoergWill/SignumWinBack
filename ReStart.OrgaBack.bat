REM Dateien aus dem Workspace nach Orgasoft-Addin kopieren
mkdir "C:\ProgramData\OrgaSoft\AddIn"
xcopy "%WORKSPACE%%JOB_BASE_NAME%.*" "C:\ProgramData\OrgaSoft\AddIn" /E /C /R /Y
xcopy "%WORKSPACE%*.dll" "C:\ProgramData\OrgaSoft\AddIn" /E /C /R /Y

REM noch laufende Orgasoft-Instanzen beenden
Taskkill /IM Signum.Orgasoft.Main.exe

REM OrgaBack neu starten (aktiviert Addin)
"C:\Program Files (x86)\Signum\Orgasoft\Signum.Orgasoft.Main.exe" /M:01 /U:SYS /P:admin
