Imports System.Reflection
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Fügt Sätze zu einer Inventur hinzu und führt eine Bestandskorrektur aus.")>
Public Class ob_Rohstoffe_SiloInventur
    Implements IExtension

    Public Property InfoContainer As IInfoContainer Implements IExtension.InfoContainer
    Public Property ServiceProvider As IOrgasoftServiceProvider Implements IExtension.ServiceProvider

    Private Shared oFactory As IFactoryService
    'Private Shared oSetting As ISettingService
    'Private Shared oViewProvider As IViewProvider

    Public Sub Initialize() Implements IExtension.Initialize
        'in wb_Main registrieren
        wb_Main_Shared.RegisterAddIn("ob_Rohstoffe_SiloInventur")

        'siehe Mail vom 13.Juli 2017 J.Erhardt - laden der dll schläg fehl 
        'AssemblyResolve wird definiert in WinBackAddIn.Erweiterte Kompilierungsoptionen
        If wb_Global.AssemblyResolve Then
            'Die eigenen dll-Files in sep. Verzeichnis verlagern
            AddHandler System.AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyAssemblyResolve
        End If
        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)
        'oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)
    End Sub

    Private Shared Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Return wb_Main_Shared.MyAssemblyResolve(sender, args, GetType(ob_Artikel_DockingExtension).Assembly)
    End Function

    ''' <summary>
    ''' Erzeugt eine Inventur für Artikel mit ArtikelNr, Einheit, Menge und führt anschließend für diesen Artikel eine Bestandskorrektur durch
    ''' </summary>
    Private Shared Function Bestandskorrektur(ArtikelNr As String, Einheit As Integer, EingabeMenge As String, ChargenNr As String, iFilialNr As Integer) As Boolean
        'Objekt vom Type Inventur erstellen lassen (via FactoryService)
        Dim oInventur = CType(oFactory.GetOrgasoftObject(ObjectEnum.StockTaking), INavigationClass)
        'Find(FilialNr) ausführen
        If oInventur.Find(iFilialNr) Then
            'Bei den Positionen eine Position mit dem Artikel hinzufügen
            Dim oInvPositionen = CType(oInventur.GetPropertyValue("Positionen"), ICollectionClass)
            Dim oPos = CType(DirectCast(oInvPositionen, ComponentModel.IBindingListView).AddNew(), IFrameWorkClass)
            oPos.SetPropertyValue("ArtikelNr", ArtikelNr)
            oPos.SetPropertyValue("Einheit", Einheit)
            oPos.SetPropertyValue("EingabeMengeString", EingabeMenge)
            'Leere Chargen-Nummern abfangen
            If ChargenNr = "" Then
                ChargenNr = wb_Global.FlagKeineChargenNummer
            End If
            oPos.SetPropertyValue("ChargenNr", ChargenNr)
            Dim oMethodInfo As Reflection.MethodInfo = oPos.GetType.GetMethod("EingabeBeenden")
            oMethodInfo.Invoke(oPos, {})
            'Update
            If oInventur.Update() Then
                'Objekt vom Typ Bestandkorrektur erstellen lassen
                Dim oBK = oFactory.GetOrgasoftObject(ObjectEnum.StockCorrection)
                'BestaendeAufNullSetzenJN auf True setzen
                oBK.SetPropertyValue("BestaendeAufNullSetzenJN", True)
                oBK.SetPropertyValue("FilialNr", iFilialNr)
                'Property TransferSelektion via Reflection (wichtig!) holen => Type=Object
                Dim oPropInfo As Reflection.PropertyInfo = oBK.GetType.GetProperty("TransferSelektion")
                Dim oSelektion = oPropInfo.GetValue(oBK)

                'TODO Folgende Zeile ist ein Workaround für ein internes Problem, ab 19.3.21 nicht mehr nötig
                'oBK.SetPropertyValue("TransferSelektion", oSelektion)

                Dim oPropInfo2 As Reflection.PropertyInfo = oSelektion.GetType.GetProperty("SelektionsName")
                oPropInfo2.SetValue(oSelektion, "temp. Selektion")
                'In diesem Objekt das Property SQLSelektion (wiederum via Reflection) auf einen passenden SQL-String setzen
                Dim oPropInfo3 As Reflection.PropertyInfo = oSelektion.GetType.GetProperty("SQLSelektion")
                oPropInfo3.SetValue(oSelektion, "SELECT HandelsArtikel.* FROM HandelsArtikel WHERE ArtikelNr='" & ArtikelNr & "'")
                'Execute() in der Klasse Bestandskorrektur aufrufen
                If DirectCast(oBK, IWorkerClassBase).Execute() Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Public Shared Function Bestandskorrektur(ArtikelNummer As String, NeueMenge As Integer, ChargenNr As String) As Boolean
        'Produktions-Filiale (Default)
        Dim Filiale As Integer = wb_Linien_Global.DefaultProdFiliale
        'Einheit ist immer kg
        Dim Einheit As Integer = wb_Global.obEinheitKilogramm
        'neue Menge(Inventur)
        Dim Menge As String = wb_Functions.FormatStr(NeueMenge.ToString, 3)
        'Bestandskorrektur in OrgaBack durchführen
        Return Bestandskorrektur(ArtikelNummer, Einheit, Menge, ChargenNr, Filiale)
    End Function

End Class
