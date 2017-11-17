Imports System.IO
Imports System.Windows.Forms
Imports combit.ListLabel22
Imports WinBack

Public Class wb_PrinterDialog
    Private _ListSubDirectory As String
    Private _ListFileName As String

    Public LL As New ListLabel()

    Public WriteOnly Property ListSubDirectory As String
        Set(value As String)
            _ListSubDirectory = wb_GlobalSettings.pListenPath & value & "\"
            LL.AutoProjectType = LlProject.List
        End Set
    End Property

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

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        LL.AutoDestination = LlPrintMode.Normal
        LL.AutoShowPrintOptions = False

        'Drucker einstellen (im List&Label-Projekt-File)
        Dim Settings As New Drawing.Printing.PrinterSettings()
        Settings.PrinterName = cbPrinterAuswahl.SelectedItem.ToString
        LL.Core.LlSetPrinterInPrinterFile(LlProject.List, LL.AutoProjectFile, LlPrinterIndex.AllPages, Settings)

        'Druckauftrag starten
        LL.Print()
        'Catch
        'End Try
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnEditVorlage_Click(sender As Object, e As EventArgs) Handles btnEditVorlage.Click
        Try
            LL.Design()
        Catch
        End Try
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        LL.ExportOptions.Clear()
        LL.ExportOptions.Add(LlExportOption.ExportTarget, "XLS")
        'LL.ExportOptions.Add(LlExportOption.ExportFile, "Excel.xls")
        'LL.ExportOptions.Add(LlExportOption.ExportPath, _ListSubDirectory)
        LL.ExportOptions.Add(LlExportOption.ExportShowResult, "1")
        LL.ExportOptions.Add(LlExportOption.XlsIgnoreGroupLines, "TRUE")
        LL.ExportOptions.Add(LlExportOption.XlsIgnoreHeaderFooterLines, "TRUE")
        LL.ExportOptions.Add(LlExportOption.XlsIgnoreLineWrapForDataOnlyExport, "TRUE")
        'Try
        LL.Print()
        'Catch
        'End Try
        Me.Close()
    End Sub

    Private Sub wb_PrinterDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Letzter verwendeter Drucker aus der List&Label-Projektdatei
        Dim LLProjectPrinter As String = LL.Core.LlGetPrinterFromPrinterFile(LlProject.List, LL.AutoProjectFile, LlPrinterIndex.AllPages).dmDeviceName
        Dim IdxProjectPrinter As Integer = wb_Global.UNDEFINED
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
            If Printername = LLProjectPrinter Then
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
        'ShowPreview()
    End Sub

    Private Sub ShowPreview()
        'Preview anzeigen
        LL.PreviewControl = LLPreview
        LL.AutoDestination = LlPrintMode.PreviewControl
        LL.AutoShowPrintOptions = False
        LL.ExportOptions.Clear()
        LL.ExportOptions.Add(LlExportOption.ExportShowResult, "0")
        If File.Exists(LL.AutoProjectFile) Then
            Try
                LL.Print()
            Catch
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

    Private Sub BtnVorschau_Click(sender As Object, e As EventArgs) Handles BtnVorschau.Click
        'Preview in eigenem Fenster anzeigen
        Dim Preview As New wb_PrinterPreview

        LL.PreviewControl = Preview.LLPreview
        Preview.Show()
        LL.AutoDestination = LlPrintMode.PreviewControl
        LL.AutoShowPrintOptions = False
        LL.ExportOptions.Clear()
        LL.ExportOptions.Add(LlExportOption.ExportShowResult, "0")
        If File.Exists(LL.AutoProjectFile) Then
            'Try
            LL.Print()
            'Catch
            'End Try
        End If
    End Sub
End Class
