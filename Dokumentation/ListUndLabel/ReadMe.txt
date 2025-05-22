ACHTUNG
=======

Folgende .dll-Files müssen im AddIn-Verzeichnis vorhanden sein:

	combit.ListLabel22.dll
	Infralution.Common.dll
	Infralution.Controls.dll
	Infralution.Controls.VirtualTree.dll
	WeifenLuo.WinFormsUIDocking.dll
	WeifenLuo.WinFormsUI.Docking.ThemeVS2015.dll

	EnhancedEdit.dll
	WinBackAddin.dll
	WinBackServerTask.exe


Die Dateien aus dem ListUndLabel-Verzeichnis müssen in ein separates Verzeichnis kopiert
werden, welches für alle Clients erreichbar ist.
Dieses Verzeichnis muss in die Pfad-Angabe mit aufgenommen werden.

Die Dateien aus Language müssen bei 64-Bit-Anwendungen zwingend in das Signum-Programm-Verzeichnis kopiert werden
Alle anderen dll-files kommen in den dll-Ordner



===================================================================================================
combit List & Label 22
===================================================================================================


===================================================================================================
WICHTIGE INFORMATIONEN ZUR REDISTRIBUTION
===================================================================================================
Vor der Redistribution müssen Sie unbedingt sicherstellen, über LL_OPTIONSTR_LICENSINGINFO Ihren persönlichen Lizensierungsschlüssel zu setzen, um Fehlermeldungen beim Kunden zu vermeiden. VCL, OCX und .NET-Komponente bieten Ihnen ein Property "LicensingInfo" zu diesem Zweck.

Die benötigten Informationen finden Sie in der Datei PersonalLicense.txt im Hauptverzeichnis. Wenn mehrere Entwickler an einem Projekt arbeiten, kann jeder der Lizensierungsschlüssel verwendet werden.

Mit Ausnahme von Installationen für Programmiersprachen, die OCX ActiveX-Controls nutzen (z.B. Visual Basic 6, dort müssen die OCX-Dateien registriert werden) unterstützt List & Label die xcopy-Installation. Die hier aufgeführten Dateien können so einfach in ein beliebiges Verzeichnis kopiert werden, es ist keine Registrierung notwendig. Beachten Sie bitte auch die Hinweise zur Redistribution der .NET-Assembly in der List & Label .NET-Hilfe unter dem Punkt "Deployment/Weitergabe".

Access-Datenbanken, die List & Label Programmcode enthalten, können nur im MDE-Format redistributiert werden, da ansonsten ein Source-Zugriff möglich wäre und somit der Kunde eine Vollversion von List & Label benötigen würde.

Kopieren Sie die Webserverlizenzdatei in das gleiche Verzeichnis, in dem auch die cmLL22.dll liegt. Bei der Enterprise-Edition ist dieser Schritt nicht notwendig, für die anderen Editionen können Sie diese Datei mit Hilfe der Anwendung ll22web.exe erzeugen, die sich ebenfalls im Lieferumfang befindet. Sie benötigen hierfür dann eine Seriennummer und den Product Key für eine Server-/Webserverlizenz.
===================================================================================================
Folgende Module dürfen Sie weitergeben, sofern Sie sich an die Bedingungen des Lizenzvertrages halten. Die nachfolgenden Dateien finden Sie im "Redistributierbare Dateien"-Unterverzeichnis Ihrer Installation.


===================================================================================================
32 Bit (x86) Applikationen benötigen
===================================================================================================
cmBR22.dll
cmCT22.dll
cmDW22.dll
cmLL22.dll
cmLL22??.chm			(optional, Designer Hilfe; muss im gleichen Pfad wie cmLL22.dll liegen) *1
cmLL22??.lng *1
cmLL22??.ltpl			(optional, enthält Etikettenformate; muss im gleichen Pfad wie cmLL22.dll liegen) *1
cmLL22bc.llx			(optional, 2D Barcodes, nur für Professional/Enterprise Edition)
cmLL22ex.llx			(optional, Exportformate, benötigt für HTML5Viewer)
cmLL22fx.ocx			(optional, Designer Funktions-Erweiterungen, benötigt Haupt-DLLs; nur benötigt für OCX-Anwendungen, wie z.B. Visual Basic 6)
cmLL22ht.llx			(optional, HTML-Objekt)
cmLL22o.ocx			(optional, List & Label Druck/Design Funktionalität, benötigt Haupt-DLLs)
cmLL22oc.llx			(optional, OLE-Objekt)
cmLL22ox.ocx			(optional, Designer Objekt-Erweiterungen, benötigt Haupt-DLLs)
cmLL22pr.dll			(optional, PDF-Objekt)
cmLL22pw.llx			(optional, Projektassistent)
cmLL22r.ocx			(optional, RTF-Control, benötigt Haupt-DLLs)
cmLL22v.ocx			(optional, Vorschau-Control, benötigt "Standalone Viewer" DLLs)
cmLL22xl.dll
cmLS22.dll
cmLS22??.lng *1
cmMX22.dll
cmSC22.dll			(optional, benötigt für C# Scripting)
cmUT22.dll
combit.CSharpScript22.Engine.x86.dll	(optional, C# Scripting)
combit.CSharpScript22.Interface.x86.dll	(optional, C# Scripting, benötigt 32 Bit Visual C++ Redistributable für Visual Studio 2015)
combit.ListLabel22.Export.x86.dll	(optional, .NET Framework 4.0 Office Open XML Assembly) *2, *3
combit.ListLabel22.Internetmarke.dll	(optional, Internetmarke der Deutschen Post, benötigt .NET Framework 4.0)
cuLL22o.ocx			(optional, List & Label Druck/Design Funktionalität (Unicode), benötigt Haupt-DLLs)
cxMP22.exe			(optional, Proxy für Mailversand über einen 64 Bit Simple MAPI/XMAPI-Server. Erfordert Registrierung der EXE über /regserver mit Administrator-Rechten, benötigt cxCT22.dll, cxUT22.dll, cxMX22.dll; nur Professional/Enterprise Edition)
DocumentFormat.OpenXml.dll	(optional, benötigt von combit.ListLabel22.Export.x86.dll, benötigt .NET Framework 4.0)
ListLabel22JNI_x86.dll		(optional, Java Unterstützung)
LL22WebDesignerSetup.exe	(optional, Web Designer Setup)
ne_*.dbf/ne_*.shp		(optional, Landkartendaten in verschiedenen Auflösungen für Chart "Landkarte/Shapefile", nur für Enterprise Edition)


===================================================================================================
64 Bit (x64) Applikationen (nur Professional/Enterprise Edition) benötigen
===================================================================================================
cmLL22??.chm			(optional, Designer Hilfe; muss im gleichen Pfad wie cxLL22.dll liegen) *1
cmLL22??.ltpl			(optional, enthält Etikettenformate; muss im gleichen Pfad wie cxLL22.dll liegen) *1
cmMP22.exe			(optional, Proxy für Mailversand über einen 32 Bit Simple MAPI/XMAPI-Server. Erfordert Registrierung der EXE über /regserver mit Administrator-Rechten, benötigt cmCT22.dll, cmUT22.dll, cmMX22.dll)
combit.CSharpScript22.Engine.x64.dll	(optional, C# Scripting)
combit.CSharpScript22.Interface.x64.dll	(optional, C# Scripting, benötigt 64 Bit Visual C++ Redistributable für Visual Studio 2015)
combit.ListLabel22.Export.x64.dll	(optional, .NET Framework 4.0 Office Open XML Assembly) *2, *3
combit.ListLabel22.Internetmarke.dll	(optional, Internetmarke der Deutschen Post, benötigt .NET Framework 4.0)
cxBR22.dll
cxCT22.dll
cxDW22.dll
cxLL22.dll
cxLL22??.lng *1
cxLL22bc.llx			(optional, 2D Barcodes, nur für Professional/Enterprise Edition)
cxLL22ex.llx			(optional, Exportformate, benötigt für HTML5Viewer)
cxLL22ht.llx			(optional, HTML-Objekt)
cxLL22oc.llx			(optional, OLE-Objekt)
cxLL22pr.dll			(optional, PDF-Objekt)
cxLL22v.ocx			(optional, Vorschau-Control, benötigt 64 Bit Versionen der "Standalone Viewer" DLLs)
cxLL22xl.dll
cxLS22.dll
cxLS22??.lng *1
cxMX22.dll
cxSC22.dll			(optional, benötigt für C# Scripting)
cxUT22.dll
DocumentFormat.OpenXml.dll	(optional, benötigt von combit.ListLabel22.Export.x64.dll, benötigt .NET Framework 4.0)
ListLabel22JNI_x64.dll		(optional, Java Unterstützung)
ne_*.dbf/ne_*.shp		(optional, Landkartendaten in verschiedenen Auflösungen für Chart "Landkarte/Shapefile", nur für Enterprise Edition)


===================================================================================================
.NET Anwendungen benötigen zusätzlich
===================================================================================================
Die meisten Assemblies werden in der .NET 2.0 und .NET 4.0 Variante mitgeliefert. Wir empfehlen dringend die .NET 4.0 Variante zu verwenden, da diese zusätzliche Features (wie LINQ Unterstützung, fortgeschrittene Abfrageanalyse etc.) enthält.

combit.ListLabel22.ClientProfile.dll	(.NET Framework ClientProfile Assembly) *2, *3
combit.ListLabel22.dll			(.NET Framework Assembly) *2, *3
combit.ListLabel22.Web.dll		(.NET Framework Web Controls, Unterstützung für ASP.NET/MVC) *2, *3
combit.ListLabel22.Wpf.dll		(.NET Framework 3.5 WPF Controls, WPF Viewer Control) *2, *3
System.Data.SQLite.x64.dll		(.NET SQLite Assembly, benötigt für SQLite- und InMemory-Datenprovider, 64 Bit) *2, *3
System.Data.SQLite.x86.dll		(.NET SQLite Assembly, benötigt für SQLite- und InMemory-Datenprovider, AnyCPU) *2, *3

Einige zusätzliche Datenprovider Assemblies sind ebenfalls redistributierbar, siehe goodies.txt für weitere Informationen.


===================================================================================================
Standalone Viewer benötigt
===================================================================================================
cmBR22.dll
cmCT22.dll
cmDW22.dll
cmLL22.dll
cmLL22??.lng *1			(mindestens eine Sprache wird benötigt)
cmLL22xl.dll			(optional, Exportformat PDF)
cmLS22.dll
cmLS22??.lng *1			(mindestens eine Sprache wird benötigt)
cmUT22.dll
llview22.exe


===================================================================================================
Signatur-Erweiterung (optional)
===================================================================================================
Die Koppel-DLL der jeweiligen Signaturanbieter muss sich im gleichen Verzeichnis wie die Datei cmLL22.dll befinden. Die Signatur-Erweiterung ist nicht als 64 Bit-Version verfügbar!

Im Lieferumfang von digiSeal® office der secrypt GmbH findet sich die digiSealAPI.dll. Für OpenLimit® CC Sign der OpenLimit® SignCubes GmbH wird ebenfalls eine zusätzliche Koppel-DLL benötigt, welche aber gesondert von der OpenLimit® SignCubes GmbH zur Verfügung gestellt wird. Im Lieferumfang von esiCAPI® der e.siqia technologies GmbH finden sich die Dateien esicAPI.dll und esicConfig.xml, welche zusätzlich ausgeliefert werden müssen. Die DLL für den jeweiligen Konnektor wird direkt von e.siqia technologies GmbH bereitgestellt.


---------------------------------------------------------------------------------------------------
*1 "??" je nach erworbenem Sprachkit entsprechend der nachfolgenden Liste
	Chinesisch (Vereinfacht): 09
	Deutsch: 00
	Englisch: 01
	Französisch: 12
	Italienisch: 18
	Japanisch: 19
	Niederländisch: 0D
	Polnisch: 1E
	Portugiesisch: 1F
	Russisch: 21
	Slowakisch: 22
	Spanisch: 25
	Tschechisch: 0B
*2 Signierte Versionen der .NET Framework Assemblies befinden sich im jeweiligen "Signed"-Unterordner unter "..\combit\LL22\Beispiele\Microsoft .NET\Assemblies\"
*3 .NET 4.0 Versionen der .NET Framework Assemblies befinden sich im jeweiligen Unterordner unter "..\combit\LL22\Beispiele\Microsoft .NET\Assemblies\"