REM Dateien aus dem Build-Verzeichnis in OrgaBack kopieren
mkdir "C:\ProgramData\OrgaSoft\AddIn"
xcopy "$(WORKSPACE)$(JOB_BASE_NAME).*" "C:\ProgramData\OrgaSoft\AddIn" /E /C /R /Y
xcopy "$(WORKSPACE)*.dll" "C:\ProgramData\OrgaSoft\AddIn" /E /C /R /Y

REM noch laufende Orgasoft-Instanzen beenden
Taskkill /IM Signum.Orgasoft.Main.exe

REM OrgaBack neu starten (aktiviert Addin)
Start /B "C:\Program Files (x86)\Signum\Orgasoft\Signum.Orgasoft.Main.exe"
