Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Functions

Public Class wb_Schnittstelle_Import
    Inherits DockContent

    Private GesProgress As Integer = 0

    Private Sub wb_Schnittstelle_Import_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler wb_Schnittstelle_Shared.eFormatChanged, AddressOf FormatChange
        AddHandler wb_Schnittstelle_Shared.eImportProgressChange, AddressOf ProgressChange
        FormatChange(sender)
        ImportChange()
        ImportModeChange()
    End Sub

    Private Sub FormatChange(sender As Object)
        lblInterface.Text = "Import - " & wb_Schnittstelle_Shared.Bezeichnung

        pnlT1006.Enabled = wb_Schnittstelle_Shared.T1006.Import
        pnlT1001.Enabled = wb_Schnittstelle_Shared.T1001.Import
        pnlT1007.Enabled = wb_Schnittstelle_Shared.T1007.Import
        pnlT4107.Enabled = wb_Schnittstelle_Shared.T4107.Import

        cbAutoImport.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbRemoveImportFiles.Enabled = wb_Schnittstelle_Shared.ExpertMode

        cbImportT1006.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbImportT1001.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbImportT1007.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbImportT4107.Enabled = wb_Schnittstelle_Shared.ExpertMode

        cbTxtVrbT1006.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbRzpWrtT1007.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbWsrSveT1007.Enabled = wb_Schnittstelle_Shared.ExpertMode
        cbRzpWrtT1007.Enabled = wb_Schnittstelle_Shared.ExpertMode
    End Sub

    ''' <summary>
    ''' Aktualisiert die Fortschritts-Anzeige.
    ''' Die Anzeige der einzelnen Funktionen wird jeweils von 0..100 aktualisiert. Wenn die Einzelanzeige wieder neu startet,
    ''' wird die Gesamtanzeige um 100 erhöht.
    ''' Die Anzeige des Gesamt-Fortschritts ergibt sich aus dem Offset und der Fortschritts-Anzeige der Einzelfunktion.
    ''' </summary>
    ''' <param name="sender"></param>
    Private Sub ProgressChange(sender As Object)
        If wb_Schnittstelle_Shared.ImportProgress < pgEinzel.Value Then
            GesProgress += 100
        End If

        pgEinzel.Value = Math.Min(wb_Schnittstelle_Shared.ImportProgress, 100)
        pgGesamt.Value = Math.Min(GesProgress + pgEinzel.Value, pgGesamt.Maximum)
        Application.DoEvents()
    End Sub

    Private Sub ImportChange()
        cbImportT1001.Checked = wb_GlobalSettings.ImportArtikel
        cbImportT1006.Checked = wb_GlobalSettings.ImportRezeptKopf
        cbImportT1007.Checked = wb_GlobalSettings.ImportRezept
        cbImportT4107.Checked = wb_GlobalSettings.ImportLieferungen
        cbImportT1101.Checked = wb_GlobalSettings.ImportBackzettel

        cbTxtVrbT1006.Checked = wb_GlobalSettings.ImportRzpVerarbeitungsHinweise
        cbRzpWrtT1007.Checked = wb_GlobalSettings.ImportRzptUpdate
        cbWsrSveT1007.Checked = wb_GlobalSettings.ImportRzptWasserSpeichern
        cbArtRzpT1101.Checked = wb_GlobalSettings.ImportProdArtikelRezept
        cbFtpTrnT1101.Checked = wb_GlobalSettings.ImportProdFTP
    End Sub

    Private Sub ImportModeChange()
        BtnImport.Enabled = (pnlT1006.Enabled OrElse pnlT1001.Enabled OrElse pnlT1007.Enabled OrElse pnlT4107.Enabled) AndAlso
                            (cbImportT1001.Checked OrElse cbImportT1006.Checked OrElse cbImportT1007.Checked OrElse cbImportT4107.Checked Or cbImportT1101.Checked)
    End Sub

    Private Sub BtnImport_Click(sender As Object, e As EventArgs) Handles BtnImport.Click
        'Button Export deaktivieren
        BtnImport.Enabled = False
        GesProgress = 0
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        'Anzahl der aktiven Import-Aufträge
        pgGesamt.Maximum = wb_Schnittstelle_Shared.CountExportChecked(Controls)
        pgGesamt.Value = 0
        pgEinzel.Value = 0

        'Reihenfolge der Import-Aufträge
        For i = 1 To 6
            'Import Artikel/Rohstoffe
            If i = wb_Schnittstelle_Shared.T1001.ImportReihenfolge AndAlso cbImportT1001.Checked Then
                ImportT1001()
            End If

            'Import Rezeptköpfe
            If i = wb_Schnittstelle_Shared.T1006.ImportReihenfolge AndAlso cbImportT1006.Checked Then
                ImportT1006()
            End If
            'Import Rezeptschritte
            If i = wb_Schnittstelle_Shared.T1007.ImportReihenfolge AndAlso cbImportT1007.Checked Then
                ImportT1007()
            End If

            'Import Lieferungen
            If i = wb_Schnittstelle_Shared.T4107.ImportReihenfolge AndAlso cbImportT4107.Checked Then
                ImportT4107()
            End If

            'Import Produktions-Aufträge
            If i = wb_Schnittstelle_Shared.T1101.ImportReihenfolge AndAlso cbImportT1101.Checked Then
                ImportT1101()
            End If
        Next

        'Button wieder aktivieren
        BtnImport.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub BtnImportT1001_Click(sender As Object, e As EventArgs) Handles BtnImportT1001.Click
        'Button Import Rezeptkopf deaktivieren
        BtnImportT1001.Enabled = False
        'Import Artikel/Rohstoffe
        ImportT1001()
        'Button wieder aktivieren
        BtnImportT1001.Enabled = True
    End Sub

    Private Sub BtnImportT1006_Click(sender As Object, e As EventArgs) Handles BtnImportT1006.Click
        'Button Import Rezeptkopf deaktivieren
        BtnImportT1006.Enabled = False
        'Import Rezeptköpfe
        ImportT1006()
        'Button wieder aktivieren
        BtnImportT1006.Enabled = True
    End Sub

    Private Sub BtnImportT1007_Click(sender As Object, e As EventArgs) Handles BtnImportT1007.Click
        'Button Import Rezeptkopf deaktivieren
        BtnImportT1007.Enabled = False
        'Import Rezeptschritte
        ImportT1007()
        'Button wieder aktivieren
        BtnImportT1007.Enabled = True
    End Sub

    Private Sub BtnImportT4107_Click(sender As Object, e As EventArgs) Handles BtnImportT4107.Click
        'Button Import Rezeptkopf deaktivieren
        BtnImportT4107.Enabled = False
        'Import Lieferungen/Bilanzierung
        ImportT4107()
        'Button wieder aktivieren
        BtnImportT4107.Enabled = True
    End Sub

    Private Sub BtnImportT1101_Click(sender As Object, e As EventArgs) Handles BtnImportT1101.Click
        'Button Import Rezeptkopf deaktivieren
        BtnImportT1101.Enabled = False
        'Import Backliste
        ImportT1101()
        'Button wieder aktivieren
        BtnImportT1101.Enabled = True
    End Sub

    Private Sub ImportT1001()
        'Cursor umschalten
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'alle Import-Verzeichnisse in dieser Liste
        For Each ImportPath As String In getImportPfade(wb_Schnittstelle_Shared.T1001.ImportPath, -1, 1)
            'prüfen ob das Verzeichnis existiert
            If IO.Directory.Exists(ImportPath) Then
                'alle Dateien im Import-Verzeichnis nach diesem Muster
                For Each FileName In IO.Directory.GetFiles(ImportPath, wb_Schnittstelle_Shared.T1001.FileMask)
                    Trace.WriteLine("@I_Import T1001 - " & FileName)
                    wb_Schnittstelle_Shared.T1001.ImportOpenAndRead(FileName)
                Next
            End If
        Next
        'Cursor wieder aktivieren
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ImportT1006()
        'Cursor umschalten
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'alle Import-Verzeichnisse in dieser Liste
        For Each ImportPath As String In getImportPfade(wb_Schnittstelle_Shared.T1006.ImportPath, -1, 1)
            'prüfen ob das Verzeichnis existiert
            If IO.Directory.Exists(ImportPath) Then
                'alle Dateien im Import-Verzeichnis nach diesem Muster
                For Each FileName In IO.Directory.GetFiles(ImportPath, wb_Schnittstelle_Shared.T1006.FileMask)
                    Trace.WriteLine("@I_Import T1006 - " & FileName)
                    wb_Schnittstelle_Shared.T1006.ImportOpenAndRead(FileName)
                Next
            End If
        Next
        'Cursor wieder aktivieren
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ImportT1007()
        'Cursor umschalten
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'alle Import-Verzeichnisse in dieser Liste
        For Each ImportPath As String In getImportPfade(wb_Schnittstelle_Shared.T1007.ImportPath, -1, 1)
            'prüfen ob das Verzeichnis existiert
            If IO.Directory.Exists(ImportPath) Then
                'alle Dateien im Import-Verzeichnis nach diesem Muster
                For Each FileName In IO.Directory.GetFiles(ImportPath, wb_Schnittstelle_Shared.T1007.FileMask)
                    Trace.WriteLine("@I_Import T1007 - " & FileName)
                    wb_Schnittstelle_Shared.T1007.ImportOpenAndRead(FileName)
                Next
            End If
        Next
        'Cursor wieder aktivieren
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ImportT4107()
        'Cursor umschalten
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'alle Import-Verzeichnisse in dieser Liste
        For Each ImportPath As String In getImportPfade(wb_Schnittstelle_Shared.T4107.ImportPath, -1, 1)
            'prüfen ob das Verzeichnis existiert
            If IO.Directory.Exists(ImportPath) Then
                'alle Dateien im Import-Verzeichnis nach diesem Muster
                For Each FileName In IO.Directory.GetFiles(ImportPath, wb_Schnittstelle_Shared.T4107.FileMask)
                    Trace.WriteLine("@I_Import T4107 - " & FileName)
                    wb_Schnittstelle_Shared.T4107.ImportOpenAndRead(FileName)
                Next
            End If
        Next
        'Cursor wieder aktivieren
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ImportT1101()
        'Cursor umschalten
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'alle Import-Verzeichnisse in dieser Liste
        For Each ImportPath As String In getImportPfade(wb_Schnittstelle_Shared.T1101.ImportPath, -1, 1)
            'prüfen ob das Verzeichnis existiert
            If IO.Directory.Exists(ImportPath) Then
                'alle Dateien im Import-Verzeichnis nach diesem Muster
                For Each FileName In IO.Directory.GetFiles(ImportPath, wb_Schnittstelle_Shared.T1101.FileMask)
                    Trace.WriteLine("@I_Import T1101 - " & FileName)
                    wb_Schnittstelle_Shared.T1101.ImportOpenAndRead(FileName)
                Next
            End If
        Next
        'Cursor wieder aktivieren
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub


    ''' <summary>
    ''' Gibt eine Liste von Strings zurück, die entsprechend der Maske in der Pfad-Angabe
    ''' gebildet werden. Die Maske YYYYMMDD wird durch das aktuelle Datum -x Tage ersetzt.
    ''' </summary>
    ''' <param name="Pfad"></param>
    ''' <returns></returns>
    Private Function getImportPfade(Pfad As String, DaySsBegin As Integer, DaysEnd As Integer) As List(Of String)
        'Ergebnis-Array
        Dim Result As New List(Of String)

        'Pfad enthält eine Datums-Information(Optimo)
        If Pfad.Contains("YYYYMMDD") Then
            'Schleife über x Tage im Datums-Teil in der Pfad-Angabe
            For i = DaySsBegin To DaysEnd
                Result.Add(Replace(Pfad, "YYYYMMDD", DateAdd(DateInterval.Day, i, Date.Now).ToString("yyyyMMdd")))
            Next
        Else
            'wenn keine Maske in der Pfad-Angabe enthalten ist, wird der ursprüngliche String zurückgeben
            Result.Add(Pfad)
        End If

        Return Result
    End Function

    Private Sub wb_Schnittstelle_Import_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler wb_Schnittstelle_Shared.eFormatChanged, AddressOf FormatChange
        RemoveHandler wb_Schnittstelle_Shared.eExportProgressChange, AddressOf ProgressChange
    End Sub

    Private Sub cbTxtVrbT1006_Click(sender As Object, e As EventArgs) Handles cbTxtVrbT1006.Click
        wb_GlobalSettings.ImportRzpVerarbeitungsHinweise = cbTxtVrbT1006.Checked
        ImportModeChange()
    End Sub

    Private Sub cbRzpWrtT1007_Click(sender As Object, e As EventArgs) Handles cbRzpWrtT1007.Click
        wb_GlobalSettings.ImportRzptUpdate = cbRzpWrtT1007.Checked
        ImportModeChange()
    End Sub

    Private Sub cbWsrSveT1007_Click(sender As Object, e As EventArgs) Handles cbWsrSveT1007.Click
        wb_GlobalSettings.ImportRzptWasserSpeichern = cbWsrSveT1007.Checked
        ImportModeChange()
    End Sub

    Private Sub cbArtRzpT1101_Click(sender As Object, e As EventArgs) Handles cbArtRzpT1101.Click
        wb_GlobalSettings.ImportProdArtikelRezept = cbArtRzpT1101.Checked
        ImportModeChange()
    End Sub

    Private Sub cbFtpTrnT1101_Click(sender As Object, e As EventArgs) Handles cbFtpTrnT1101.Click
        wb_GlobalSettings.ImportProdFTP = cbFtpTrnT1101.Checked
        ImportModeChange()
    End Sub
End Class