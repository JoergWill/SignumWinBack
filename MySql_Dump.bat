@echo off
echo Datensicherung WinBack

cd "%1"
bin\mysqldump -uherbst -pherbst --extended-insert=FALSE --add-drop-database=TRUE --databases=TRUE %3 > %2
exit /B %ERRORLEVEL%
