Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.IO

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Erweitert das Ribbon um ein neues Tab mit mehreren Aufgaben")>
Public Class MenuSampleExtension
    Implements IExtension

    Public Property InfoContainer As Common.IInfoContainer Implements Extensibility.IExtension.InfoContainer
    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements Extensibility.IExtension.ServiceProvider

    Private oViewProvider As IViewProvider
    Private oMenuService As IMenuService
    Private oSetting As ISettingService
    Private oFactory As IFactoryService

    Public Sub Initialize() Implements IExtension.Initialize

        oViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)
        oMenuService = TryCast(ServiceProvider.GetService(GetType(IMenuService)), IMenuService)
        oSetting = TryCast(ServiceProvider.GetService(GetType(ISettingService)), ISettingService)
        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)

        ' Fügt dem Ribbon ein neues RibbonTab hinzu
        Dim oNewTab = oMenuService.AddTab("MenuExtension", "Kontrollaufgaben", "Wichtige Aufgaben zur täglichen Kontrolle")
        ' Das neue RibbonTab erhält eine Gruppe
        Dim oGrp = oNewTab.AddGroup("MenuExtensionVerbuchung", "Verbuchung")
        ' ... und dieser Gruppe wird ein Button hinzugefügt
        oGrp.AddButton("MenuExtensionButton1", "VerbuchungsStatus", "Zeige alle unverbuchten Datensätze", My.Resources.EditTask_16, My.Resources.EditTask_32, AddressOf CheckBookingTable)
        oGrp.AddButton("MenuExtensionButton5", "Protokoll anzeigen", "Zeigt das Verbuchungsprotokoll an", My.Resources.Search_16, My.Resources.Protocol_32, AddressOf ShowVbuPrt)
        Dim oBtn = oGrp.AddButton("MenuExtensionButton6", "Disabled Button", "Zeigt das Verbuchungsprotokoll an", My.Resources.Search_16, My.Resources.Protocol_32, AddressOf ShowVbuPrt)
        oBtn.Enabled = False

        oGrp = oNewTab.AddGroup("MenuExtensionScheduler", "Scheduler")
        oGrp.AddButton("MenuExtensionButton2", "Scheduler-Jobs", "Zeige alle fehlerhaften Scheduler-Jobs", Nothing, My.Resources.Scheduler_32, AddressOf CheckScheduler)

        oGrp = oNewTab.AddGroup("MenuExtensionUmsatz", "Umsatz")
        oGrp.AddButton("MenuExtensionButton3", "Umsatz-Kontrolle", "Umsatz analysieren", Nothing, My.Resources.DrillDown_32, AddressOf ShowDrillDown)

        ' Erweitert ein bestehendes RibbonTab um einen Button (Stammdaten/Adressen)
        Dim oTabs = oMenuService.GetTabs
        Dim oGrps = oTabs(0).GetGroups
        oGrps(1).AddButton("MenuExtensionButton4", "Suche in Historie", "Sucht Artikel-Informationen in der Artikel-Historie", My.Resources.Search_16, My.Resources.Search_32, AddressOf History)

        ' Fügt dem Stammdaten-Tab eine neue Group hinzu, in dem ein hier selbst definiertes Panel sitzt
        Dim oPanelGroup = oTabs(0).AddGroup("LabelPrinting", "Etikettendirekt-Druck")
        oPanelGroup.AddControl(CreateRibbonPanel)
    End Sub

    ''' <summary>
    ''' Erzeugt ein Panel mit einigen Controls zum Etikettendruck, das im Ribbon eingebettet wird
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateRibbonPanel() As Control

        Dim oPanel As New Panel

        Dim lblEtikettenNr As New Label
        lblEtikettenNr.AutoSize = True
        lblEtikettenNr.Location = New System.Drawing.Point(3, 4)
        lblEtikettenNr.Name = "lblEtikettenNr"
        lblEtikettenNr.Size = New System.Drawing.Size(52, 13)
        lblEtikettenNr.TabIndex = 1
        lblEtikettenNr.Text = "Etikett:"

        Dim lblAnzahl As New Label
        lblAnzahl.AutoSize = True
        lblAnzahl.Location = New System.Drawing.Point(3, 28)
        lblAnzahl.Name = "lblAnzahl"
        lblAnzahl.Size = New System.Drawing.Size(52, 13)
        lblAnzahl.TabIndex = 3
        lblAnzahl.Text = "Anzahl:"

        Dim lblArtikel As New Label
        lblArtikel.AutoSize = True
        lblArtikel.Location = New System.Drawing.Point(3, 54)
        lblArtikel.Name = "lblArtikel"
        lblArtikel.Size = New System.Drawing.Size(52, 13)
        lblArtikel.TabIndex = 5
        lblArtikel.Text = "Artikel:"

        Dim txtArtikel As New TextBox
        txtArtikel.Location = New System.Drawing.Point(50, 51)
        txtArtikel.MaxLength = 20
        txtArtikel.Name = "txtArtikel"
        txtArtikel.TabIndex = 6
        txtArtikel.Size = New System.Drawing.Size(103, 21)

        Dim dupdEtikettAnzahl As New NumericUpDown
        dupdEtikettAnzahl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        dupdEtikettAnzahl.Location = New System.Drawing.Point(50, 26)
        dupdEtikettAnzahl.Maximum = 100
        dupdEtikettAnzahl.Minimum = 0
        dupdEtikettAnzahl.Name = "dupdEtikettAnzahl"
        dupdEtikettAnzahl.Size = New System.Drawing.Size(70, 21)
        dupdEtikettAnzahl.TabIndex = 4
        dupdEtikettAnzahl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right

        Dim cmbEtikett As New ComboBox
        cmbEtikett.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
             Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        cmbEtikett.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        cmbEtikett.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        cmbEtikett.FormattingEnabled = True
        cmbEtikett.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEtikett.Location = New System.Drawing.Point(50, 0)
        cmbEtikett.Name = "dcmbEtikettNr"
        cmbEtikett.Size = New System.Drawing.Size(103, 21)
        cmbEtikett.TabIndex = 2

        ' Inhalt der Combobox ermitteln
        Dim oData As IData = oFactory.GetData
        Dim colLabel As New List(Of String)
        Using oTable = oData.OpenDataTable(Database.Main, "SELECT Etikett, Bezeichnung FROM Etikett WHERE Datenquelle='R' ORDER BY Etikett", LockType.ReadOnly)
            For Each oRow As DataRow In oTable.Rows
                colLabel.Add(oData.DB2String(oRow(0)) & " - " & oData.DB2String(oRow(1)))
            Next
        End Using
        cmbEtikett.DataSource = colLabel

        oPanel.BackColor = Drawing.Color.Transparent
        oPanel.Size = New System.Drawing.Size(160, 70)
        oPanel.Controls.Add(lblEtikettenNr)
        oPanel.Controls.Add(lblAnzahl)
        oPanel.Controls.Add(lblArtikel)
        oPanel.Controls.Add(dupdEtikettAnzahl)
        oPanel.Controls.Add(cmbEtikett)
        oPanel.Controls.Add(txtArtikel)

        Return oPanel

    End Function

    Private Sub CheckBookingTable(sender As Object, e As EventArgs)
        oViewProvider.ShowSqlView(Database.Main, "SELECT * FROM Verbuchung WHERE VerbuchungsStatus>=0")
    End Sub

    Private Sub CheckScheduler(sender As Object, e As EventArgs)
        oViewProvider.ShowSqlView(Database.Admin, "SELECT * FROM Scheduler WHERE Status='Fehler'")
    End Sub

    Private Sub History(sender As Object, e As EventArgs)
        Dim SearchString As String = InputBox("Teil des Textes, nach dem gesucht werden soll:", "Suche in der Historie", "")
        oViewProvider.ShowSqlView(Database.Main, "SELECT * FROM AdressArtikelHistorie WHERE Bezeichnung like '%" & SearchString & "%'")
    End Sub

    Private Sub ShowDrillDown(sender As Object, e As EventArgs)
        oViewProvider.OpenForm(ObjectEnum.DrillDownStatistics)
    End Sub

    Private Sub ShowVbuPrt(sender As Object, e As EventArgs)

        Dim sFileName As String = CType(oSetting.GetSetting("Verzeichnisse.ProtokollPfad"), String)
        sFileName &= "VBU.PRT"
        oViewProvider.OpenFile(sFileName)
    End Sub

End Class
