Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
'Imports WinBack.wb_Functions

Public Class wb_Schnittstelle_Export
    Inherits DockContent

    Private GesProgress As Integer = 0

    Private Sub wb_Schnittstelle_Export_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler wb_Schnittstelle_Shared.eFormatChanged, AddressOf FormatChange
        AddHandler wb_Schnittstelle_Shared.eExportProgressChange, AddressOf ProgressChange

        FormatChange(sender)
        ExportChange()
        ExportModeChange()
    End Sub

    Private Sub FormatChange(sender As Object)
        lblInterface.Text = "Export - " & wb_Schnittstelle_Shared.Bezeichnung

        pnlT1001A.Enabled = wb_Schnittstelle_Shared.T1001.Export Or wb_Schnittstelle_Shared.T1002.Export
        pnlT1001R.Enabled = wb_Schnittstelle_Shared.T1001.Export Or wb_Schnittstelle_Shared.T1002.Export
        pnlT1006R.Enabled = wb_Schnittstelle_Shared.T1006.Export Or wb_Schnittstelle_Shared.T1007.Export
        pnlT4105C.Enabled = wb_Schnittstelle_Shared.T4105.Export Or wb_Schnittstelle_Shared.T4106.Export
        tbTWNummr.Enabled = pnlT4105C.Enabled

        cbExportT1002A.Enabled = wb_Schnittstelle_Shared.ExpertMode And wb_Schnittstelle_Shared.T1002.Export
        cbExportT1002R.Enabled = wb_Schnittstelle_Shared.ExpertMode And wb_Schnittstelle_Shared.T1002.Export

        cbExportT1001R.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbExportT1001A.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbExportT1006R.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbExportT4105C.Enabled = wb_Schnittstelle_Shared.ExpertMode

        cbAendrgnT1006.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbSauertgT1006.Enabled = wb_Schnittstelle_Shared.ExpertMode
    End Sub

    ''' <summary>
    ''' Aktualisiert die Fortschritts-Anzeige.
    ''' Die Anzeige der einzelnen Funktionen wird jeweils von 0..100 aktualisiert. Wenn die Einzelanzeige wieder neu startet,
    ''' wird die Gesamtanzeige um 100 erhöht.
    ''' Die Anzeige des Gesamt-Fortschritts ergibt sich aus dem Offset und der Fortschritts-Anzeige der Einzelfunktion.
    ''' </summary>
    ''' <param name="sender"></param>
    Private Sub ProgressChange(sender As Object)
        If wb_Schnittstelle_Shared.ExportProgress < pgEinzel.Value Then
            GesProgress += 100
        End If

        pgEinzel.Value = Math.Min(wb_Schnittstelle_Shared.ExportProgress, 100)
        pgGesamt.Value = Math.Min(GesProgress + pgEinzel.Value, pgGesamt.Maximum)
        Application.DoEvents()
    End Sub

    Private Sub ResetProgressChange(GesMax As Integer)
        pgGesamt.Maximum = GesMax
        pgGesamt.Value = 0
        pgEinzel.Value = 0
        GesProgress = 0
    End Sub

    Private Sub ExportChange()
        cbExportT1001A.Checked = wb_GlobalSettings.ExportArtikel
        cbExportT1001R.Checked = wb_GlobalSettings.ExportRohstoffe
        cbExportT1006R.Checked = wb_GlobalSettings.ExportRezepte
        cbExportT1007R.Checked = wb_GlobalSettings.ExportRezepte        'versteckte CheckBox für die Tabelle T1007(CountExportChecked)
        cbExportT4105C.Checked = wb_GlobalSettings.ExportChargen
        cbExportT1002A.Checked = wb_GlobalSettings.ExportArtikel_Nwt
        cbExportT1002R.Checked = wb_GlobalSettings.ExportRohstoffe_Nwt

        cbAendrgnT1006.Checked = wb_GlobalSettings.ExportRezepte_Aendrgn
        cbSauertgT1006.Checked = wb_GlobalSettings.ExportRezepte_Sauertg

        tbTWNummr.Text = wb_Schnittstelle_Shared.T4105.Option_TW_Nummer
    End Sub

    Private Sub ExportModeChange()
        BtnExport.Enabled = (pnlT1001R.Enabled OrElse pnlT1001A.Enabled OrElse pnlT1006R.Enabled OrElse pnlT4105C.Enabled) AndAlso
                            (cbExportT1001A.Checked OrElse cbExportT1001R.Checked OrElse cbExportT1006R.Checked OrElse cbExportT4105C.Checked)
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
        'Button Export deaktivieren
        BtnExport.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        'Anzahl der aktiven Export-Aufträge
        ResetProgressChange(wb_Schnittstelle_Shared.CountExportChecked(Controls))

        'Reihenfolge der Export-Aufträge
        For i = 1 To 6
            'Export Artikel
            If i = wb_Schnittstelle_Shared.T1001.ExportReihenfolge AndAlso cbExportT1001A.Checked Then
                ExportT1001x(True)
            End If
            'Export Nährwerte(Artikel)
            If i = wb_Schnittstelle_Shared.T1002.ExportReihenfolge AndAlso cbExportT1002A.Checked Then
                ExportT1002x(True)
            End If

            'Export Rohstoffe
            If i = wb_Schnittstelle_Shared.T1001.ExportReihenfolge AndAlso cbExportT1001R.Checked Then
                ExportT1001x(False)
            End If
            'Export Nährwerte(Rohstoffe)
            If i = wb_Schnittstelle_Shared.T1002.ExportReihenfolge AndAlso cbExportT1002R.Checked Then
                ExportT1002x(False)
            End If

            'Export Rezepturen (Rezeptkopf und Rezeptschritte)
            If i = wb_Schnittstelle_Shared.T1006.ExportReihenfolge AndAlso cbExportT1006R.Checked Then
                ExportT1006()
                ExportT1007()
            End If

            'Export Chargen
            If i = wb_Schnittstelle_Shared.T4105.ExportReihenfolge AndAlso cbExportT4105C.Checked Then
                ExportT4105()
                ExportT4106()
                'Anzeige Tageswechsel-Nummer aktualisieren
                ExportChange()
            End If
        Next

        'Button wieder aktivieren
        BtnExport.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    ''' <summary>
    ''' Export Artikel/Artikel-Nährwerte - Tabelle T1001/T1002
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnExportT1001A_Click(sender As Object, e As EventArgs) Handles BtnExportT1001A.Click
        'Button Export Artikel deaktivieren
        BtnExportT1001A.Enabled = False
        'Export Artikel
        ResetProgressChange(100)
        GesProgress = 0
        ExportT1001x(True)
        'Button wieder aktivieren
        BtnExportT1001A.Enabled = True
    End Sub
    Private Sub BtnExportT1002A_Click(sender As Object, e As EventArgs) Handles BtnExportT1002A.Click
        'Button Export Artikel Nährwerte deaktivieren
        BtnExportT1002A.Enabled = False
        'Export Nährwerte(Artikel)
        ResetProgressChange(100)
        ExportT1002x(True)
        'Button Export Artikel Nährwerte deaktivieren
        BtnExportT1002A.Enabled = True
    End Sub

    ''' <summary>
    ''' Export Rohstoffe/Rohstoffe-Nährwerte - Tabelle T1001/T1002
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnExportT1001R_Click(sender As Object, e As EventArgs) Handles BtnExportT1001R.Click
        'Button Export Rohstoffe deaktivieren
        BtnExportT1001R.Enabled = False
        'Export Rohstoffe
        ResetProgressChange(100)
        ExportT1001x(False)
        'Button wieder aktivieren
        BtnExportT1001R.Enabled = True
    End Sub
    Private Sub BtnExportT1002R_Click(sender As Object, e As EventArgs) Handles BtnExportT1002R.Click
        'Button Export Rohstoffe Nährwerte deaktivieren
        BtnExportT1002R.Enabled = False
        'Export Nährwerte(Rohstoffe)
        ResetProgressChange(100)
        ExportT1002x(False)
        'Button Export Rohstoffe Nährwerte wieder aktivieren
        BtnExportT1002R.Enabled = True
    End Sub

    ''' <summary>
    ''' Export Rezepte/Rezeptschritte - Tabelle T1006/T1007
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnExportT1006R_Click(sender As Object, e As EventArgs) Handles BtnExportT1006R.Click
        'Button Export Rezepte deaktivieren
        BtnExportT1006R.Enabled = False
        'Export Rezeptköpfe und Rezeptschritte
        ResetProgressChange(200)
        ExportT1006()
        ExportT1007()
        'Button Export Rezepte wieder aktivieren
        BtnExportT1006R.Enabled = True
    End Sub

    ''' <summary>
    ''' Export Chargen/Chargendetails - Tabelle T4105/T4106
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnExportT4105C_Click(sender As Object, e As EventArgs) Handles BtnExportT4105C.Click
        'Button Export Chargen deaktivieren
        BtnExportT4105C.Enabled = False
        'Export Chargen/Chargen-Details
        ResetProgressChange(200)
        ExportT4105()
        ExportT4106()
        'Anzeige Tageswechsel-Nummer aktualisieren
        ExportChange()
        'Button Export Chargen wieder aktivieren
        BtnExportT4105C.Enabled = True
    End Sub

    ''' <summary>
    ''' Export Artikel/Rohstoffe - Tabelle T1001
    ''' </summary>
    Private Sub ExportT1001x(Artikel As Boolean)
        'Cursor umschalten
        Cursor = Cursors.WaitCursor
        'Optionen
        wb_Schnittstelle_Shared.T1001.OptionArtikel = Artikel
        wb_Schnittstelle_Shared.T1001.OptionRohstoff = Not Artikel
        wb_Schnittstelle_Shared.T1001.OptionSauerteig = cbSauertgT1006.Checked
        'Export-Verzeichnis
        Dim ExportPath As String = wb_Schnittstelle_Shared.T1001.ExportPath
        Dim FileName As String = wb_Schnittstelle_Shared.T1001.WinBackExportFileName
        'prüfen ob das Verzeichnis existiert
        If IO.Directory.Exists(ExportPath) Then
            Trace.WriteLine("@I_Export T1001 - " & FileName)
            wb_Schnittstelle_Shared.T1001.ExportOpenAndWrite(ExportPath & FileName)
        End If
        'Cursor wieder aktivieren
        Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Export Nährwerte Artikel/Rohstoffe - Tabelle T1002
    ''' </summary>
    Private Sub ExportT1002x(Artikel As Boolean)
        'Cursor umschalten
        Cursor = Cursors.WaitCursor
        'Options-Felder
        wb_Schnittstelle_Shared.T1002.OptionArtikel = Artikel
        wb_Schnittstelle_Shared.T1002.OptionRohstoff = Not Artikel
        'Export-Verzeichnis
        Dim ExportPath As String = wb_Schnittstelle_Shared.T1002.ExportPath
        Dim FileName As String = wb_Schnittstelle_Shared.T1002.WinBackExportFileName
        'prüfen ob das Verzeichnis existiert
        If IO.Directory.Exists(ExportPath) Then
            Trace.WriteLine("@I_Export T1002 - " & FileName)
            wb_Schnittstelle_Shared.T1002.ExportOpenAndWrite(ExportPath & FileName)
        End If
        'Cursor wieder aktivieren
        Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Export Rezeptköpfe - Tabelle T1006
    ''' </summary>
    Private Sub ExportT1006()
        'Cursor umschalten
        Cursor = Cursors.WaitCursor
        'Options-Felder
        wb_Schnittstelle_Shared.T1006.OptionSauerteig = cbSauertgT1006.Checked
        'Export-Verzeichnis
        Dim ExportPath As String = wb_Schnittstelle_Shared.T1006.ExportPath
        Dim FileName As String = wb_Schnittstelle_Shared.T1006.WinBackExportFileName
        'prüfen ob das Verzeichnis existiert
        If IO.Directory.Exists(ExportPath) Then
            Trace.WriteLine("@I_Export T1006 - " & FileName)
            wb_Schnittstelle_Shared.T1006.ExportOpenAndWrite(ExportPath & FileName)
        End If
        'Cursor wieder aktivieren
        Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Export Rezeptköpfe - Tabelle T1007
    ''' </summary>
    Private Sub ExportT1007()
        'Cursor umschalten
        Cursor = Cursors.WaitCursor
        'Options-Felder
        wb_Schnittstelle_Shared.T1007.OptionSauerteig = cbSauertgT1006.Checked
        'Export-Verzeichnis
        Dim ExportPath As String = wb_Schnittstelle_Shared.T1007.ExportPath
        Dim FileName As String = wb_Schnittstelle_Shared.T1007.WinBackExportFileName
        'prüfen ob das Verzeichnis existiert
        If IO.Directory.Exists(ExportPath) Then
            Trace.WriteLine("@I_Export T1007 - " & FileName)
            wb_Schnittstelle_Shared.T1007.ExportOpenAndWrite(ExportPath & FileName)
        End If
        'Cursor wieder aktivieren
        Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Export Chargen - Tabelle T4105
    ''' </summary>
    Private Sub ExportT4105()
        'Cursor umschalten
        Cursor = Cursors.WaitCursor
        'Export-Verzeichnis
        Dim ExportPath As String = wb_Schnittstelle_Shared.T4105.ExportPath
        Dim FileName As String = wb_Schnittstelle_Shared.T4105.WinBackExportFileName
        'prüfen ob das Verzeichnis existiert
        If IO.Directory.Exists(ExportPath) Then
            Trace.WriteLine("@I_Export T4105 - " & FileName)
            wb_Schnittstelle_Shared.T4105.ExportOpenAndWrite(ExportPath & FileName)
        End If
        'Cursor wieder aktivieren
        Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Export ChargenDetails - Tabelle T4106
    ''' </summary>
    Private Sub ExportT4106()
        'Cursor umschalten
        Cursor = Cursors.WaitCursor
        'Export-Verzeichnis
        Dim ExportPath As String = wb_Schnittstelle_Shared.T4106.ExportPath
        Dim FileName As String = wb_Schnittstelle_Shared.T4106.WinBackExportFileName
        'prüfen ob das Verzeichnis existiert
        If IO.Directory.Exists(ExportPath) Then
            Trace.WriteLine("@I_Export T4106 - " & FileName)
            wb_Schnittstelle_Shared.T4106.ExportOpenAndWrite(ExportPath & FileName)
        End If
        'Cursor wieder aktivieren
        Cursor = Cursors.Default
    End Sub


#Region "Options"
    Private Sub cbExportT1001A_Click(sender As Object, e As EventArgs) Handles cbExportT1001A.Click
        wb_GlobalSettings.ExportArtikel = cbExportT1001A.Checked
        ExportModeChange()
    End Sub
    Private Sub cbExportT1001R_Click(sender As Object, e As EventArgs) Handles cbExportT1001R.Click
        wb_GlobalSettings.ExportRohstoffe = cbExportT1001R.Checked
        ExportModeChange()
    End Sub
    Private Sub cbExportT1006R_Click(sender As Object, e As EventArgs) Handles cbExportT1006R.Click
        wb_GlobalSettings.ExportRezepte = cbExportT1006R.Checked
        cbExportT1007R.Checked = cbExportT1006R.Checked 'versteckte CheckBox für die Tabelle T1007(CountExportChecked)
        ExportModeChange()
    End Sub
    Private Sub cbExportT4105C_Click(sender As Object, e As EventArgs) Handles cbExportT4105C.Click
        wb_GlobalSettings.ExportChargen = cbExportT4105C.Checked
        ExportModeChange()
    End Sub
    Private Sub cbExportT1002A_Click(sender As Object, e As EventArgs) Handles cbExportT1002A.Click
        wb_GlobalSettings.ExportArtikel_Nwt = cbExportT1002A.Checked
        ExportModeChange()
    End Sub
    Private Sub cbExportT1002R_Click(sender As Object, e As EventArgs) Handles cbExportT1002R.Click
        wb_GlobalSettings.ExportRohstoffe_Nwt = cbExportT1002R.Checked
        ExportModeChange()
    End Sub
    Private Sub cbAendrgnT1006_Click(sender As Object, e As EventArgs) Handles cbAendrgnT1006.Click
        wb_GlobalSettings.ExportRezepte_Aendrgn = cbAendrgnT1006.Checked
        ExportModeChange()
    End Sub
    Private Sub cbSauertgT1006_Click(sender As Object, e As EventArgs) Handles cbSauertgT1006.Click
        wb_GlobalSettings.ExportRezepte_Sauertg = cbSauertgT1006.Checked
        ExportModeChange()
    End Sub


#End Region

End Class