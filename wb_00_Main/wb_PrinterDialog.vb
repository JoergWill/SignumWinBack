Imports System.IO
Imports System.Windows.Forms
Imports combit.ListLabel22

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

    Private _LL_KopfZeile_1 As String = ""
    Private _LL_KopfZeile_2 As String = ""

    Public WithEvents LL As New ListLabel()

    Public Sub New(ExcelExport As Boolean)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        _ExcelExport = ExcelExport
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

            'wenn die Datei existiert wird kein Auswahl-Dialog bei Start von List&Label angezeigt
            If File.Exists(LL.AutoProjectFile) Then
                LL.AutoShowSelectFile = False
            End If
        End Set
    End Property

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

    ''' <summary>
    ''' Start Druck
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'Try
        LL.AutoDestination = LlPrintMode.Normal
        LL.AutoShowPrintOptions = False

        'Drucker einstellen (im List&Label-Projekt-File)
        Dim Settings As New Drawing.Printing.PrinterSettings()
        Settings.PrinterName = cbPrinterAuswahl.SelectedItem.ToString
        LL.Core.LlSetPrinterInPrinterFile(LlProject.List, LL.AutoProjectFile, LlPrinterIndex.AllPages, Settings)

        'Druckauftrag starten
        Try
            LL.Print()
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
    Private Sub wb_PrinterDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Lizenz-Information eintragen
        LL.LicensingInfo = "PcyjD"

        Dim IdxProjectPrinter As Integer = wb_Global.UNDEFINED
        Dim llProjectPrinter As String = wb_Global.UNDEFINED

        'Letzter verwendeter Drucker aus der List&Label-Projektdatei
        Try
            llProjectPrinter = LL.Core.LlGetPrinterFromPrinterFile(LlProject.List, LL.AutoProjectFile, LlPrinterIndex.AllPages).dmDeviceName
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
        LL.Core.LlSetOptionString(LlOptionString.PreviewFileName, wb_GlobalSettings.pWindowsTempPath & "LLPreview.ll")
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
        If PrintDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
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
    End Sub

End Class
