@echo off

@REM Batch-Hilfs-File fuer GitLab-CI
@REM
@REM 	Da der Aufruf der Unit-Tests (eigenes Projekt) im GitLab nicht funktioniert
@REM 	werden alle Funktionen über ein Batch-Script realisiert

@REM Parameter
@REM		Stage		(%CI_JOB_STAGE%)
@REM		Buildpath	(%CI_PROJECT_DIR%)

@REM Variablen
SET NUGET_PATH="C:\Program Files (x86)\Microsoft Visual Studio\NuGet\nuget.exe"
SET MSBUILD_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe"
SET NUNIT_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\Extensions\TestPlatform\vstest.console.exe"
SET COVER_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe"
SET SONAR_PATH="C:\Users\Will.WINBACK\AppData\Roaming\SonarScanner\SonarScanner.MSBuild.exe"
SET CYCLONE_PATH=dotnet CycloneDX
SET DEPENDENCY_TRACK=curl -X "POST" ^"http://172.16.16.40:9040/api/v1/bom^" -H ^"Content-Type: multipart/form-data^" -H ^"X-Api-Key: rAkk5tMACcI5JKvcbgNgzyYzzihDOSSy^" -F ^"autoCreate=true^" -F ^"projectName=SignumWinBack^" -F ^"bom=@TestResults\bom.xml^"

@REM Das Ausgabeformat von vstest ist .trx und muss in .xml umgewandelt werden
@REM Die Umwandlung erfolgt über ein externes Tool: trx2unit.exe
@REM 	Die Installation erfolgt zunächst ins User-Verzeichnis mit 'dotnet tool install -g trx2junit'
@REM	Anschliessen muss die trx2junit.exe und der Ordner .store in ein anderes Verzeichnis kopiert werden. (User-Verzeichnis)
SET TRX2JUNIT_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\Extensions\TestPlatform\trx2junit.exe"
SET CHECK_VERSION="OrgaBack.GetVersion.bat"

SET SOURCE_DIR="C:\Orgasoft\AddIns"
SET TARGET_DIR="K:\Projekte\_OrgaBack_Office\AddIn"

@REM Check Command-Line-Parameter
IF %1.==. GOTO :FEHLER
IF %2.==. GOTO :FEHLER

IF "%1" == "build"  GOTO :BUILD
IF "%1" == "test"   GOTO :TEST
IF "%1" == "deploy" GOTO :DEPLOY
IF "%1" == "sonar" GOTO :SONAR
IF "%1" == "bom" GOTO :CYCLONE
GOTO :FAIL

@REM =============================
@REM === Build ===================
@REM =============================
:BUILD
ECHO Restoring NuGet packages...
%NUGET_PATH% restore >NUL
ECHO Build...
DEL Build.Result.txt /f/q/s 2>NUL >NUL
%MSBUILD_PATH% -verbosity:quiet -consoleloggerparameters:ErrorsOnly -property:Configuration=Release WinBackAddInn.sln >Build.Result.txt

@REM Pruefen ob in der Ausgabedatei der Text error aufgetreten ist (Fehler bei Build)
FIND /c "error" Build.Result.txt >NUL
IF errorlevel 1 goto NOERROR
ECHO Fehler bei Build
EXIT /B 1

@REM =============================
@REM === Test ====================
@REM =============================
:TEST
ECHO Restoring NuGet packages...
%NUGET_PATH% restore >NUL
ECHO Build...
DEL Build.Result.txt /f/q/s 2>NUL >NUL
%MSBUILD_PATH% -verbosity:quiet -consoleloggerparameters:ErrorsOnly -property:Configuration=Debug WinBackAddInn.sln >Build.Result.txt

ECHO UnitTest...
CD WinBackUnitTest\bin\Debug
%NUNIT_PATH% /Enablecodecoverage /Logger:Trx;LogFileName=TestResult.trx WinBackUnitTest.dll  2>.\..\..\..\UnitTest.Error.txt >.\..\..\..\UnitTest.Result.txt
%TRX2JUNIT_PATH% TestResults\TestResult.trx

@REM zurück ins Build-Verzeichnis
CD .\..\..\..\
@REM Test-Result einsammeln
copy WinBackUnitTest\bin\Debug\TestResults\TestResult.xml .

@REM Pruefen ob in der Ausgabedatei der Text error aufgetreten ist (Fehler bei Build)
FIND /c "Fehler" UnitTest.Error.txt >NUL
IF errorlevel 1 goto NOERROR
ECHO Fehler bei UnitTest
EXIT /B 1

@REM =============================
@REM === SonarQube/SonarScanner ==
@REM =============================
:SONAR
ECHO Start SonarScanner...
%SONAR_PATH% begin /k:"Joerg_SignumWinBack_AYFctgqzgJ0K9qIC8iY9" /d:sonar.cs.xunit.reportsPaths="TestResult.coverage" /d:sonar.login="sqp_6163d8df213e0cc472d05a4edcac4f4230047e9d" /d:sonar.host.url="http://172.16.16.40:9000"
ECHO Rebuild Project...
%MSBUILD_PATH% -t:Rebuild -verbosity:quiet -consoleloggerparameters:ErrorsOnly -property:Configuration=Debug WinBackAddInn.sln >Build.Result.txt
ECHO Test-Coverage...
%COVER_PATH% collect /output:"TestResult.coverage" "%NUNIT_PATH%" "WinBackUnitTest\bin\Debug\WinBackUnitTest.dll"
ECHO End SonarScanner...
%SONAR_PATH% end /d:sonar.login="sqp_6163d8df213e0cc472d05a4edcac4f4230047e9d"
IF errorlevel 0 goto NOERROR
ECHO Fehler bei Sonar
EXIT /B 1

@REM =============================
@REM === CycloneDX SBOM ==
@REM =============================
:CYCLONE
ECHO Start CycloneDX (SBOM)...
%CYCLONE_PATH% WinBackAddIn.vbproj -r -o TestResults
ECHO Upload to Dependency Track...
%DEPENDENCY_TRACK%
goto NOERROR

@REM =============================
@REM === Deploy ==================
@REM =============================
:DEPLOY
ECHO Check AssemblyFileVersion...
CALL %CHECK_VERSION%
IF errorlevel 1 GOTO NODEPLOY

ECHO Restoring NuGet packages...
%NUGET_PATH% restore >NUL
ECHO Build(Release)...
DEL Build.Result.txt /f/q/s 2>NUL >NUL
%MSBUILD_PATH% -verbosity:quiet -consoleloggerparameters:ErrorsOnly -property:Configuration=Release WinBackAddInn.sln >Build.Result.txt
ECHO Deploy(Release)...

@REM In diesem SONDERFALL befinden sich alle Files im Verzeichnis C:\OrgaSoft\AddIns
@REM Dieses Verzeichnis wird über die Build-Ereignisse in VisualStudio aktualisiert (Debug des OrgaBack-AddIn)
xcopy /R /Y %SOURCE_DIR% %TARGET_DIR%

@REM Die Files von OrgaBack-Office kopieren
cd WinBackStart\bin\Release
xcopy /R /Y WinBackOffice.* %TARGET_DIR%

EXIT /B 0



@REM =============================
@REM === Fail ====================
@REM =============================
:FAIL
ECHO BuildStage kann folgende Werte haben:
ECHO 	build
ECHO 	test
ECHO 	deploy 
EXIT /B 1

:FEHLER 
echo Das Script muss mit Parameter aufgerufen werden: 
echo 	%0 BuildStage ProjectDir
EXIT /B 1

:NOERROR
ECHO Kein Fehler aufgetreten
EXIT /B 0

:NODEPLOY
ECHO Versionsnummer ungerade - Kein Deploy
EXIT /B 0