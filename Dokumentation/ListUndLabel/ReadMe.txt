ACHTUNG
=======

Folgende .dll-Files m�ssen im AddIn-Verzeichnis vorhanden sein:

	combit.ListLabel22.dll
	Infralution.Common.dll
	Infralution.Controls.dll
	Infralution.Controls.VirtualTree.dll
	WeifenLuo.WinFormsUIDocking.dll
	WeifenLuo.WinFormsUI.Docking.ThemeVS2015.dll

	EnhancedEdit.dll
	WinBackAddin.dll
	WinBackServerTask.exe


Die Dateien aus dem ListUndLabel-Verzeichnis m�ssen in ein separates Verzeichnis kopiert
werden, welches f�r alle Clients erreichbar ist.
Dieses Verzeichnis muss in die Pfad-Angabe mit aufgenommen werden.

Die Dateien aus Language m�ssen bei 64-Bit-Anwendungen zwingend in das Signum-Programm-Verzeichnis kopiert werden
Alle anderen dll-files kommen in den dll-Ordner



===================================================================================================
combit List & Label 22
===================================================================================================


===================================================================================================
WICHTIGE INFORMATIONEN ZUR REDISTRIBUTION
===================================================================================================
Vor der Redistribution m�ssen Sie unbedingt sicherstellen, �ber LL_OPTIONSTR_LICENSINGINFO Ihren pers�nlichen Lizensierungsschl�ssel zu setzen, um Fehlermeldungen beim Kunden zu vermeiden. VCL, OCX und .NET-Komponente bieten Ihnen ein Property "LicensingInfo" zu diesem Zweck.

Die ben�tigten Informationen finden Sie in der Datei PersonalLicense.txt im Hauptverzeichnis. Wenn mehrere Entwickler an einem Projekt arbeiten, kann jeder der Lizensierungsschl�ssel verwendet werden.

Mit Ausnahme von Installationen f�r Programmiersprachen, die OCX ActiveX-Controls nutzen (z.B. Visual Basic 6, dort m�ssen die OCX-Dateien registriert werden) unterst�tzt List & Label die xcopy-Installation. Die hier aufgef�hrten Dateien k�nnen so einfach in ein beliebiges Verzeichnis kopiert werden, es ist keine Registrierung notwendig. Beachten Sie bitte auch die Hinweise zur Redistribution der .NET-Assembly in der List & Label .NET-Hilfe unter dem Punkt "Deployment/Weitergabe".

Access-Datenbanken, die List & Label Programmcode enthalten, k�nnen nur im MDE-Format redistributiert werden, da ansonsten ein Source-Zugriff m�glich w�re und somit der Kunde eine Vollversion von List & Label ben�tigen w�rde.

Kopieren Sie die Webserverlizenzdatei in das gleiche Verzeichnis, in dem auch die cmLL22.dll liegt. Bei der Enterprise-Edition ist dieser Schritt nicht notwendig, f�r die anderen Editionen k�nnen Sie diese Datei mit Hilfe der Anwendung ll22web.exe erzeugen, die sich ebenfalls im Lieferumfang befindet. Sie ben�tigen hierf�r dann eine Seriennummer und den Product Key f�r eine Server-/Webserverlizenz.
===================================================================================================
Folgende Module d�rfen Sie weitergeben, sofern Sie sich an die Bedingungen des Lizenzvertrages halten. Die nachfolgenden Dateien finden Sie im "Redistributierbare Dateien"-Unterverzeichnis Ihrer Installation.


===================================================================================================
32 Bit (x86) Applikationen ben�tigen
===================================================================================================
cmBR22.dll
cmCT22.dll
cmDW22.dll
cmLL22.dll
cmLL22??.chm			(optional, Designer Hilfe; muss im gleichen Pfad wie cmLL22.dll liegen) *1
cmLL22??.lng *1
cmLL22??.ltpl			(optional, enth�lt Etikettenformate; muss im gleichen Pfad wie cmLL22.dll liegen) *1
cmLL22bc.llx			(optional, 2D Barcodes, nur f�r Professional/Enterprise Edition)
cmLL22ex.llx			(optional, Exportformate, ben�tigt f�r HTML5Viewer)
cmLL22fx.ocx			(optional, Designer Funktions-Erweiterungen, ben�tigt Haupt-DLLs; nur ben�tigt f�r OCX-Anwendungen, wie z.B. Visual Basic 6)
cmLL22ht.llx			(optional, HTML-Objekt)
cmLL22o.ocx			(optional, List & Label Druck/Design Funktionalit�t, ben�tigt Haupt-DLLs)
cmLL22oc.llx			(optional, OLE-Objekt)
cmLL22ox.ocx			(optional, Designer Objekt-Erweiterungen, ben�tigt Haupt-DLLs)
cmLL22pr.dll			(optional, PDF-Objekt)
cmLL22pw.llx			(optional, Projektassistent)
cmLL22r.ocx			(optional, RTF-Control, ben�tigt Haupt-DLLs)
cmLL22v.ocx			(optional, Vorschau-Control, ben�tigt "Standalone Viewer" DLLs)
cmLL22xl.dll
cmLS22.dll
cmLS22??.lng *1
cmMX22.dll
cmSC22.dll			(optional, ben�tigt f�r C# Scripting)
cmUT22.dll
combit.CSharpScript22.Engine.x86.dll	(optional, C# Scripting)
combit.CSharpScript22.Interface.x86.dll	(optional, C# Scripting, ben�tigt 32 Bit Visual C++ Redistributable f�r Visual Studio 2015)
combit.ListLabel22.Export.x86.dll	(optional, .NET Framework 4.0 Office Open XML Assembly) *2, *3
combit.ListLabel22.Internetmarke.dll	(optional, Internetmarke der Deutschen Post, ben�tigt .NET Framework 4.0)
cuLL22o.ocx			(optional, List & Label Druck/Design Funktionalit�t (Unicode), ben�tigt Haupt-DLLs)
cxMP22.exe			(optional, Proxy f�r Mailversand �ber einen 64 Bit Simple MAPI/XMAPI-Server. Erfordert Registrierung der EXE �ber /regserver mit Administrator-Rechten, ben�tigt cxCT22.dll, cxUT22.dll, cxMX22.dll; nur Professional/Enterprise Edition)
DocumentFormat.OpenXml.dll	(optional, ben�tigt von combit.ListLabel22.Export.x86.dll, ben�tigt .NET Framework 4.0)
ListLabel22JNI_x86.dll		(optional, Java Unterst�tzung)
LL22WebDesignerSetup.exe	(optional, Web Designer Setup)
ne_*.dbf/ne_*.shp		(optional, Landkartendaten in verschiedenen Aufl�sungen f�r Chart "Landkarte/Shapefile", nur f�r Enterprise Edition)


===================================================================================================
64 Bit (x64) Applikationen (nur Professional/Enterprise Edition) ben�tigen
===================================================================================================
cmLL22??.chm			(optional, Designer Hilfe; muss im gleichen Pfad wie cxLL22.dll liegen) *1
cmLL22??.ltpl			(optional, enth�lt Etikettenformate; muss im gleichen Pfad wie cxLL22.dll liegen) *1
cmMP22.exe			(optional, Proxy f�r Mailversand �ber einen 32 Bit Simple MAPI/XMAPI-Server. Erfordert Registrierung der EXE �ber /regserver mit Administrator-Rechten, ben�tigt cmCT22.dll, cmUT22.dll, cmMX22.dll)
combit.CSharpScript22.Engine.x64.dll	(optional, C# Scripting)
combit.CSharpScript22.Interface.x64.dll	(optional, C# Scripting, ben�tigt 64 Bit Visual C++ Redistributable f�r Visual Studio 2015)
combit.ListLabel22.Export.x64.dll	(optional, .NET Framework 4.0 Office Open XML Assembly) *2, *3
combit.ListLabel22.Internetmarke.dll	(optional, Internetmarke der Deutschen Post, ben�tigt .NET Framework 4.0)
cxBR22.dll
cxCT22.dll
cxDW22.dll
cxLL22.dll
cxLL22??.lng *1
cxLL22bc.llx			(optional, 2D Barcodes, nur f�r Professional/Enterprise Edition)
cxLL22ex.llx			(optional, Exportformate, ben�tigt f�r HTML5Viewer)
cxLL22ht.llx			(optional, HTML-Objekt)
cxLL22oc.llx			(optional, OLE-Objekt)
cxLL22pr.dll			(optional, PDF-Objekt)
cxLL22v.ocx			(optional, Vorschau-Control, ben�tigt 64 Bit Versionen der "Standalone Viewer" DLLs)
cxLL22xl.dll
cxLS22.dll
cxLS22??.lng *1
cxMX22.dll
cxSC22.dll			(optional, ben�tigt f�r C# Scripting)
cxUT22.dll
DocumentFormat.OpenXml.dll	(optional, ben�tigt von combit.ListLabel22.Export.x64.dll, ben�tigt .NET Framework 4.0)
ListLabel22JNI_x64.dll		(optional, Java Unterst�tzung)
ne_*.dbf/ne_*.shp		(optional, Landkartendaten in verschiedenen Aufl�sungen f�r Chart "Landkarte/Shapefile", nur f�r Enterprise Edition)


===================================================================================================
.NET Anwendungen ben�tigen zus�tzlich
===================================================================================================
Die meisten Assemblies werden in der .NET 2.0 und .NET 4.0 Variante mitgeliefert. Wir empfehlen dringend die .NET 4.0 Variante zu verwenden, da diese zus�tzliche Features (wie LINQ Unterst�tzung, fortgeschrittene Abfrageanalyse etc.) enth�lt.

combit.ListLabel22.ClientProfile.dll	(.NET Framework ClientProfile Assembly) *2, *3
combit.ListLabel22.dll			(.NET Framework Assembly) *2, *3
combit.ListLabel22.Web.dll		(.NET Framework Web Controls, Unterst�tzung f�r ASP.NET/MVC) *2, *3
combit.ListLabel22.Wpf.dll		(.NET Framework 3.5 WPF Controls, WPF Viewer Control) *2, *3
System.Data.SQLite.x64.dll		(.NET SQLite Assembly, ben�tigt f�r SQLite- und InMemory-Datenprovider, 64 Bit) *2, *3
System.Data.SQLite.x86.dll		(.NET SQLite Assembly, ben�tigt f�r SQLite- und InMemory-Datenprovider, AnyCPU) *2, *3

Einige zus�tzliche Datenprovider Assemblies sind ebenfalls redistributierbar, siehe goodies.txt f�r weitere Informationen.


===================================================================================================
Standalone Viewer ben�tigt
===================================================================================================
cmBR22.dll
cmCT22.dll
cmDW22.dll
cmLL22.dll
cmLL22??.lng *1			(mindestens eine Sprache wird ben�tigt)
cmLL22xl.dll			(optional, Exportformat PDF)
cmLS22.dll
cmLS22??.lng *1			(mindestens eine Sprache wird ben�tigt)
cmUT22.dll
llview22.exe


===================================================================================================
Signatur-Erweiterung (optional)
===================================================================================================
Die Koppel-DLL der jeweiligen Signaturanbieter muss sich im gleichen Verzeichnis wie die Datei cmLL22.dll befinden. Die Signatur-Erweiterung ist nicht als 64 Bit-Version verf�gbar!

Im Lieferumfang von digiSeal� office der secrypt GmbH findet sich die digiSealAPI.dll. F�r OpenLimit� CC Sign der OpenLimit� SignCubes GmbH wird ebenfalls eine zus�tzliche Koppel-DLL ben�tigt, welche aber gesondert von der OpenLimit� SignCubes GmbH zur Verf�gung gestellt wird. Im Lieferumfang von esiCAPI� der e.siqia technologies GmbH finden sich die Dateien esicAPI.dll und esicConfig.xml, welche zus�tzlich ausgeliefert werden m�ssen. Die DLL f�r den jeweiligen Konnektor wird direkt von e.siqia technologies GmbH bereitgestellt.


---------------------------------------------------------------------------------------------------
*1 "??" je nach erworbenem Sprachkit entsprechend der nachfolgenden Liste
	Chinesisch (Vereinfacht): 09
	Deutsch: 00
	Englisch: 01
	Franz�sisch: 12
	Italienisch: 18
	Japanisch: 19
	Niederl�ndisch: 0D
	Polnisch: 1E
	Portugiesisch: 1F
	Russisch: 21
	Slowakisch: 22
	Spanisch: 25
	Tschechisch: 0B
*2 Signierte Versionen der .NET Framework Assemblies befinden sich im jeweiligen "Signed"-Unterordner unter "..\combit\LL22\Beispiele\Microsoft .NET\Assemblies\"
*3 .NET 4.0 Versionen der .NET Framework Assemblies befinden sich im jeweiligen Unterordner unter "..\combit\LL22\Beispiele\Microsoft .NET\Assemblies\"