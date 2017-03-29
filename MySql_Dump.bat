@echo off
echo Datensicherung WinBack

cd "%1"
bin\mysqldump -uherbst -pherbst --add-drop-database=TRUE --databases=TRUE winback > %2

