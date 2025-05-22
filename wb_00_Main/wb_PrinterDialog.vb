Imports System.IO
Imports System.Windows.Forms
Imports combit.Reporting

''' <summary>
''' KLasse zum Drucken von Reports über ListUndLabel
''' Erzeugt zusätzliche Variablen in LL
'''     Kopfzeile_1
'''     Kopfzeile_2
''' </summary>
Public Class wb_PrinterDialog
    Private _ListSubDirectory As String
    Private _ListFileName As String
    Private _ExcelExport As Boolean = False
    Private _Druckhistorie As Boolean = False

    Private _LL_KopfZeile_1 As String = ""
    Private _LL_KopfZeile_2 As String = ""
    Private _LL_Parameter_1 As String = ""
    Private _LL_Parameter_2 As String = ""
    Public WithEvents ll As combit.Reporting.ListLabel

    ''' <summary>
    ''' Erzeugt ein neues Druck-Fenster.
    ''' Die Druckhistorie wird aus dem ArrayList-Objekt erzeugt und vor dem Drucken gespeichert.
    ''' Der Dateiname wird erzeugt aus Druckdatum und Name der Vorlage. Gespeichert werden die Druckdaten im Verzeichnis TEMP
    ''' </summary>
    ''' <param name="ExcelExport"></param>
    ''' <param name="Druckhistorie"></param>
    Public Sub New(ExcelExport As Boolean, Optional Druckhistorie As Boolean = False)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        _ExcelExport = ExcelExport
        'Druckhistorie speichern vor dem Ausdruck
        _Druckhistorie = Druckhistorie
        BtnDruckHistorie.Enabled = Druckhistorie
        Me.ll = wb_Main_Shared.LL
    End Sub

    ''' <summary>
    ''' Name des Unter-Verzeichnis für die ListUndLabel-Report-Files
    ''' Das Stammverzeichnis wird in wb_globalSettings festgelegt
    '''     Programm-Verzeichnis/Listen
    ''' </summary>
    Public WriteOnly Property ListSubDirectory As String
        Set(value As String)
            _ListSubDirectory = wb_GlobalSettings.pListenPath & value & "\"
            LL.AutoProjectType = LlProject.List
        End Set
    End Property

    ''' <summary>
    ''' Datei-Name des ListUndLabel-Report-Files
    ''' </summary>
    Public WriteOnly Property ListFileName As String
        Set(value As String)
            _ListFileName = value
            'Den Standard-Projektnamen setzen
            LL.AutoProjectFile = _ListSubDirectory & _ListFileName
            'Drop-Down-Liste
            cbVorlageAuswahl.Text = _ListFileName

            'wenn die Datei existiert wird kein Auswahl-Dialog bei Start von List&Label angezeigt
            If File.Exists(LL.AutoProjectFile) Then
                LL.AutoShowSelectFile = False
            End If
        End Set
    End Property

#Disable Warning BC42304 ' Analysefehler in XML-Dokumentation
    ''' <summary>
    ''' Erzeugt eine Liste von List&Label-Projekt-File-Namen.
    ''' Im Drucker-Dialog kann dann die Vorlage über eine Drop-Down-Liste ausgewählt werden
    ''' </summary>
    ''' <param name="FileName"></param>
    Public Sub AddListFileNames(FileName As String)
#Enable Warning BC42304 ' Analysefehler in XML-Dokumentation
        'Vorlagen über Wildcard
        If FileName.Contains("*") Then
            For Each Item As String In IO.Directory.GetFiles(_ListSubDirectory, FileName)
                cbVorlageAuswahl.Items.Add(IO.Path.GetFileName(Item))
            Next
        Else
            'Prüfen ob die Datei existiert..
            If File.Exists(_ListSubDirectory & FileName) Then
                cbVorlageAuswahl.Items.Add(FileName)
            End If
        End If
    End Sub

    Private Sub cbVorlageAuswahl_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbVorlageAuswahl.SelectionChangeCommitted
        ListFileName = cbVorlageAuswahl.SelectedItem
        ShowPreview()
    End Sub

    ''' <summary>
    ''' Kopfzeile 1 ListUndLabel
    ''' </summary>
    Public WriteOnly Property LL_KopfZeile_1 As String
        Set(value As String)
            _LL_KopfZeile_1 = value
        End Set
    End Property

    ''' <summary>
    ''' Kopfzeile 2 ListUndLabel
    ''' </summary>
    Public WriteOnly Property LL_KopfZeile_2 As String
        Set(value As String)
            _LL_KopfZeile_2 = value
        End Set
    End Property

    Public WriteOnly Property LL_Parameter_1 As String
        Set(value As String)
            _LL_Parameter_1 = value
        End Set
    End Property

    Public WriteOnly Property LL_Parameter_2 As String
        Set(value As String)
            _LL_Parameter_2 = value
        End Set
    End Property

    ''' <summary>
    ''' Start Druck
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'Try
        LL.AutoDestination = LlPrintMode.Normal
        LL.AutoShowPrintOptions = False
        LL.Language = LlLanguage.German

        'Drucker einstellen (im List&Label-Projekt-File)
        Dim Settings As New Drawing.Printing.PrinterSettings With {.PrinterName = cbPrinterAuswahl.SelectedItem.ToString}
        'Funktioniert teilweise nicht, wenn keine RECHTE vorhanden sind !! (RemoteDesktop Windows-Server)
        Try
            LL.Core.LlSetPrinterInPrinterFile(LlProject.List, LL.AutoProjectFile, LlPrinterIndex.AllPages, Settings)
        Catch
        End Try

        'Druckauftrag speichern
        If _Druckhistorie Then
            'Alle pdf-Files die älter als 3 Tage sind löschen
            wb_Functions.DeleteOldFiles(wb_GlobalSettings.pDruckHistoriePath, IO.Path.GetFileNameWithoutExtension(_ListFileName) & "*.pdf", wb_Global.MaxHistDays)

            'Dateiname aus Vorlagen-Name und Datum/Uhrzeit
            Dim fName As String = wb_GlobalSettings.pDruckHistoriePath & wb_GlobalSettings.OrgaBackMandantNr.ToString("00") & "_" & IO.Path.GetFileNameWithoutExtension(_ListFileName) & "_" & Date.Now.ToString("yyyyMMdd_HHmmss") & ".pdf"

            'aktuelle Datei als pdf-File exportieren
            Try
                Dim pdfconfig As New ExportConfiguration(LlExportTarget.Pdf, fName, LL.AutoProjectFile)
                LL.Export(pdfconfig)
            Catch ex As Exception
            End Try
        End If

        'Druckauftrag starten
        Try
            LL.Print(Settings.PrinterName, LlProject.List, LL.AutoProjectFile)
        Catch LLException As Exception
            MessageBox.Show(LLException.Message & vbCrLf & "Fehler in List&Label", "Drucken/Vorschau", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        'Dialog-Fenster wird geschlossen
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' Abbruch Drucken
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        'Me.Close()
    End Sub

    ''' <summary>
    ''' Edit ListUndLabel-Vorlage-Datei
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnEditVorlage_Click(sender As Object, e As EventArgs) Handles btnEditVorlage.Click
        Try
            LL.Design()
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Export nach Excel über ListUndLabel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        LL.ExportOptions.Clear()
        LL.ExportOptions.Add(LlExportOption.ExportTarget, "XLS")
        'LL.ExportOptions.Add(LlExportOption.ExportFile, "C:\Temp\Excel.xls")
        'LL.ExportOptions.Add(LlExportOption.ExportPath, _ListSubDirectory)
        'LL.ExportOptions.Add(LlExportOption.ExportShowResult, "0")
        'LL.ExportOptions.Add(LlExportOption.XlsIgnoreGroupLines, "TRUE")
        'LL.ExportOptions.Add(LlExportOption.XlsIgnoreHeaderFooterLines, "TRUE")
        'LL.ExportOptions.Add(LlExportOption.XlsIgnoreLineWrapForDataOnlyExport, "TRUE")
        Try
            LL.Print()
        Catch LLException As Exception
            MessageBox.Show(LLException.Message & vbCrLf & "Fehler in List&Label", "Drucken/Vorschau", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        Me.Close()
    End Sub

    ''' <summary>
    ''' Initialisierung.
    ''' Laden aller Windows-Drucker. Ermittlung Windows-Standard-Drucker
    ''' Laden des letzten verwendeten Druckers aus dem ListundLabel-Report-File
    ''' 
    ''' Das Drucker-Auswahl-Feld wird mit allen verfügbaren Druckern initialisiert.
    ''' Der Default-Drucker ist entweder der Windows-Standard-Drucker oder der letzte
    ''' verwendete Drucker aus dem LL-Report-File
    ''' 
    ''' Anzeige der Miniatur-Vorschau
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    <CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnötige Zuweisung eines Werts.", Justification:="<Ausstehend>")>
    Private Sub wb_PrinterDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Lizenz-Information eintragen-ListUndLabel-Version 22
        'LL.LicensingInfo = "PcyjD"    
        'Lizenz-Information eintragen-ListUndLabel-Version 30
        ll.LicensingInfo = "IApxG"

        Dim IdxProjectPrinter As Integer = wb_Global.UNDEFINED
        Dim llProjectPrinter As String = wb_Global.UNDEFINED

        'Letzter verwendeter Drucker aus der List&Label-Projektdatei
        Try
            llProjectPrinter = ll.Core.LlGetPrinterFromPrinterFile(LlProject.List, ll.AutoProjectFile, LlPrinterIndex.AllPages).dmDeviceName
        Catch ex As Exception
        End Try

        'Windows-Standard-Drucker
        Dim pd As New Drawing.Printing.PrintDocument
        Dim DefaultPrinter As String = pd.PrinterSettings.PrinterName
        Dim IdxDefaultPrinter As Integer = wb_Global.UNDEFINED

        'Auswahlbox Drucker
        For Each Printername As String In Drawing.Printing.PrinterSettings.InstalledPrinters
            cbPrinterAuswahl.Items.Add(Printername)

            'Standard-Drucker merken
            If Printername = DefaultPrinter Then
                IdxDefaultPrinter = cbPrinterAuswahl.Items.Count - 1
            End If

            'Default-Drucker aus Projekt-File (letzter angewählter Drucker)
            If Printername = llProjectPrinter Then
                IdxProjectPrinter = cbPrinterAuswahl.Items.Count - 1
            End If
        Next

        'Auswahlbox List&Label-Vorlagen
        If cbVorlageAuswahl.Items.Count > 1 Then
            cbVorlageAuswahl.Visible = True
            lblVorlage.Visible = True
        End If

        'Default Drucker einstellen. Wenn im Projekt-File kein gültiger Drucker angegeben wurden, wird der Windows-Default-Drucker verwendet
        If IdxProjectPrinter <> wb_Global.UNDEFINED Then
            cbPrinterAuswahl.SelectedIndex = IdxProjectPrinter
        ElseIf IdxDefaultPrinter <> wb_Global.UNDEFINED Then
            cbPrinterAuswahl.SelectedIndex = IdxDefaultPrinter
        Else
            cbPrinterAuswahl.SelectedIndex = 0
        End If

        'Vorschau (Miniatur) anzeigen
        If Not _ExcelExport Then
            ShowPreview()
            btnExportExcel.Enabled = False
            gbVorschau.Visible = True
        Else
            btnExportExcel.Enabled = True
            gbVorschau.Visible = False
        End If

        'wieder aufräumen
        llProjectPrinter = Nothing

    End Sub

    ''' <summary>
    ''' Anzeige der Miniatur-Vorschau. Wenn keine Daten vorhanden sind, wird das Fenster 
    ''' geschlossen und eine Fehlermeldung ausgegeben.
    ''' </summary>
    Private Sub ShowPreview()
        'Preview anzeigen
        LL.PreviewControl = LLPreview
        LL.AutoDestination = LlPrintMode.PreviewControl
        LL.AutoShowPrintOptions = False
        LL.ExportOptions.Clear()
        'spezielles File für die Preview - sonst kolldiert der Print-Befehl mit der offenen Preview-Datei
        LL.Core.LlSetOptionString(LlOptionString.PreviewFileName, wb_GlobalSettings.pDruckHistoriePath & "LLPreview.ll")
        LL.ExportOptions.Add(LlExportOption.ExportShowResult, "0")
        If File.Exists(LL.AutoProjectFile) Then
            Try
                LL.Print()
            Catch LLException As LL_NoData_Exception
                'keine Datensätze vorhanden - Drucken wird beendet
                MessageBox.Show("Keine Daten zum Drucken oder Anzeigen", "Drucken/Vorschau", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Not Debugger.IsAttached Then
                    Me.Close()
                End If
            Catch LLException As Exception
                MessageBox.Show(LLException.Message & vbCrLf & "Fehler in List&Label", "Drucken/Vorschau", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End If
    End Sub

    Private Sub BtnPrinterDialog_Click(sender As Object, e As EventArgs) Handles BtnPrinterDialog.Click
        'ausgewählter Drucker
        PrintDialog.PrinterSettings.PrinterName = cbPrinterAuswahl.SelectedItem.ToString
        'Drucker-Einstellungen
        If PrintDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            'falls der Drucker geändert wurde
            cbPrinterAuswahl.SelectedItem = PrintDialog.PrinterSettings.PrinterName
            'Ausdruck starten
            OK_Button_Click(sender, e)
        End If
    End Sub

    ''' <summary>
    ''' Anzeige eines (großen) Vorschau-Fensters. Aus diesem Fenster kann auch gedruckt werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnVorschau_Click(sender As Object, e As EventArgs) Handles BtnVorschau.Click
        'Preview in eigenem Fenster anzeigen
        Dim Preview As New wb_PrinterPreview

        LL.PreviewControl = Preview.LLPreview
        LL.PreviewControl.Filename = String.Empty
        Preview.Show()
        LL.AutoDestination = LlPrintMode.PreviewControl
        LL.AutoShowPrintOptions = False
        LL.ExportOptions.Clear()
        LL.ExportOptions.Add(LlExportOption.ExportShowResult, "0")
        If File.Exists(LL.AutoProjectFile) Then
            Try
                LL.Print()
            Catch LLException As Exception
                MessageBox.Show(LLException.Message & vbCrLf & "Fehler in List&Label", "Drucken/Vorschau", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Zeigt eine Liste alle pdf-Dateien mit dem passenden Filename an. 
    ''' Ein Doppelklick auf den Dateinamen startet den Druck.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnDruckHistorie_Click(sender As Object, e As EventArgs) Handles BtnDruckHistorie.Click
        'File-Auswahl-Dialog Formular*.pdf
        Dim fMaske As String = wb_GlobalSettings.pDruckHistoriePath & IO.Path.GetFileNameWithoutExtension(_ListFileName) & "*.pdf"
        OpenFileDialog.Title = "Druck-Historie Datei auswählen"
        OpenFileDialog.InitialDirectory = wb_GlobalSettings.pDruckHistoriePath
        OpenFileDialog.Filter = "Druckhistorie|" & IO.Path.GetFileNameWithoutExtension(_ListFileName) & "*.pdf"
        OpenFileDialog.FileName = "*.pdf"

        'Filedialog aufrufen und pdf-File anzeigen
        If OpenFileDialog.ShowDialog = DialogResult.OK Then
            wb_Functions.ShowPdf(OpenFileDialog.FileName)
        End If
    End Sub

    ''' <summary>
    '''  Zusätzliche Felder anmelden:
    '''  Innerhalb des "AutoDefineNewLine" Events können In der Fields-Collection mit der Methode "Add" Felder hinzugefügt werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LL_AutoDefineNewLinie(sender As Object, e As AutoDefineNewLineEventArgs) Handles LL.AutoDefineNewLine
        'LL.Fields.Add("MyNewField", "Ein neues Feld")
    End Sub

    ''' <summary>
    ''' Zusätzliche Variablen anmelden:
    ''' Um zusätzliche Variablen zu definieren, kann man das Event "AutoDefineNewPage" verwenden. Auch hier funktioniert die Variables-Collection.
    ''' 
    ''' Fügt zwei zusätzliche Variablen in ListUundLabel ein:
    '''     -Kopfzeile1
    '''     -Kopfzeile2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LL_AutoDefineNewPage(sender As Object, e As AutoDefineNewPageEventArgs) Handles LL.AutoDefineNewPage
        LL.Variables.Add("KopfZeile1", _LL_KopfZeile_1)
        LL.Variables.Add("KopfZeile2", _LL_KopfZeile_2)
        LL.Variables.Add("Parameter1", _LL_Parameter_1)
        LL.Variables.Add("Parameter2", _LL_Parameter_2)
    End Sub

    Private Sub wb_PrinterDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'LL.Dispose()
    End Sub

End Class
