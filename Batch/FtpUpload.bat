@echo on

:: ------------------------------------------
::	Ftp-Upload Winback-Büro
:: ------------------------------------------
::
:: ------------------------------------------
::	ToDo:	
::
:: ------------------------------------------
::
::	- ftp-Transfer nach www.winback.de/software


Set FtpPrg=C:\Programme\Curl\curl.exe
K:
cd "K:\Projekte\_WinBack_Buero\WinBack V2.9.2\Internet"
dir


%FtpPrg% -T winback.zip -u p145032f1:501awin# ftp://p145032.typo3server.info
%FtpPrg% -T test.txt -u p145032f1:501awin# ftp://p145032.typo3server.info


c:


