@echo off

:: ------------------------------------------
::	Deployment Winback-Büro
:: ------------------------------------------
::
::	- aktuelle Programmversion ermitteln (GetVersion)
::		aus winback.dof. Die Versions-Nummer steht dann in 
::		%Version%

::	- Deployment in Hudson aufrufen 
::		Start http://localhost:8090/job/WinBack-Office-Deployment/build
::		Leerzeichen werden durch %20 ersetzt

Set URL=http://172.16.17.154:8080/job/WinBack-Office-Deployment/build

::	- aktuelle Programmversion ermitteln (aus winback.dof)
::		Bei ungeraden Version wird das Programm 
::		beendet !!
::
Call GetVersion
if ErrorLevel 1 Exit /B 1

Start %URL%