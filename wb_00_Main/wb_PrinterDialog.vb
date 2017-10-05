Imports System.IO
Imports System.Windows.Forms
Imports combit.ListLabel22

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
            LL.AutoProjectFile = _ListSubDirectory & value
            'wenn die Datei existiert wird kein Auswahl-Dialog bei Start von List&Label angezeigt
            If File.Exists(LL.AutoProjectFile) Then
                LL.AutoShowSelectFile = False
            End If
        End Set
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            LL.Print()
        Catch
        End Try
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
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
        LL.ExportOptions.Add(LlExportOption.XlsIgnoreGroupLines, "TRUE")
        LL.ExportOptions.Add(LlExportOption.XlsIgnoreHeaderFooterLines, "TRUE")
        LL.ExportOptions.Add(LlExportOption.XlsIgnoreLineWrapForDataOnlyExport, "TRUE")
        Try
            LL.Print()
        Catch
        End Try
        Me.Close()
    End Sub
End Class
