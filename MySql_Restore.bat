echo off
echo Datenr�cksicherung WinBack

cd "%1"
bin\mysql -uherbst -pherbst < %2
exit /B %ERRORLEVEL%

