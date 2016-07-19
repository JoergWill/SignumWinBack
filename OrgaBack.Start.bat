@echo off

SCHTASKS /Create /SC ONCE /TN OrgaBack /TR "C:\Program Files (x86)\Signum\Orgasoft\Signum.Orgasoft.Main.exe" /M:01 /U:SYS /P:admin
REM Start /B "OrgaBack" "C:\Program Files (x86)\Signum\Orgasoft\Signum.Orgasoft.Main.exe" /M:01 /U:SYS /P:admin
