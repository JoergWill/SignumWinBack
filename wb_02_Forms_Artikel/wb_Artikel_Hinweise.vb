Imports WinBack.wb_Artikel_Shared
Imports WeifenLuo.WinFormsUI.Docking
Imports Ghostscript.NET.Rasterizer
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms

Public Class wb_Artikel_Hinweise
    Inherits DockContent

    Private Rasterizer As GhostscriptRasterizer
    Private localDllInfo As Ghostscript.NET.GhostscriptVersionInfo
    Private dpi As String = ""

    ''' <summary>
    ''' gsdll32.dll extern einbinden (siehe auch https://github.com/jhabjan/Ghostscript.NET/blob/master/Ghostscript.NET.Samples/Samples/CustomGsdllLocationSample.cs)
    ''' Momentan funktioniert das nur mit der "alten" dll aus WinBack-Office
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Artikel_Hinweise_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Artikel-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo
        ' gsdll32.dll extern einbinden
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
            localDllInfo = New Ghostscript.NET.GhostscriptVersionInfo(wb_GlobalSettings.pProgrammPath & "\GhostScript\gsdll32.dll")
        Else
            localDllInfo = New Ghostscript.NET.GhostscriptVersionInfo(wb_GlobalSettings.pAddInPath & "\GhostScript\gsdll32.dll")
        End If

        'Beim ersten Aufruf wird der aktuelle Artikel angezeigt. Sonst wird beim Öffnen des Detail-Info-Fensters
        'der Inhalt der Textfelder gelöscht !!
        If Artikel IsNot Nothing Then
            DetailInfo(sender)
        End If
        'Focus neu setzen
        cbAufloesung.SelectionLength = 0
        BtnTransferPdf.Focus()
    End Sub

    Private Sub wb_Artikel_Hinweise_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Artikel_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    ''' <summary>
    ''' Anzeige der Artikel-Details.
    ''' Wird aufgerufen durch Event eListe_Click(). Aktualisiert die Anzeigefelder (Nummer/Text/Kommentar...)
    ''' </summary>
    Private Sub DetailInfo(sender)
        'Artikel-Hinweis pdf-Original-Datei
        Dim pdfFile As String = ""

        'Datei-Name Verarbeitungshinweis (pdf)
        tHinweisName.Text = Artikel.VerarbeitungsHinweise
        'Auflösung (Umwandlung pdf nach png)
        dpi = Artikel.VerarbeitungsHinweise_DPI
        If dpi <> "" And dpi <> "0" Then
            cbAufloesung.Text = dpi & " dpi"
        Else
            cbAufloesung.Text = "Default"
        End If

        'wenn ein Artikel-Verarbeitungshinweis vorhanden ist
        If Artikel.VerarbeitungsHinweisePfad <> "" Then
            'Mauszeiger anpassen
            Me.Cursor = Cursors.WaitCursor
            'Dateiname pdf-File
            pdfFile = Artikel.VerarbeitungsHinweisePfad & "\" & Artikel.VerarbeitungsHinweise & ".pdf"
            'Prüfen ob Datei vorhanden
            If IO.File.Exists(pdfFile) Then
                'Hinweis-Datei (pdf) laden
                Try
                    Rasterizer = New GhostscriptRasterizer
                    If dpi <> "" Then
                        Rasterizer.CustomSwitches.Add("-r" & dpi)
                    End If
                    'wirft Ghostscript not installed Exception !!!
                    Rasterizer.Open(pdfFile, localDllInfo, True)
                    VorschauPDF.Image = Rasterizer.GetPage(96, 96, 1)
                Catch ex As Exception
                    If ex.Message.Contains("library") Then
                        MsgBox("Bitte Ghostscript installieren !" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Artikel-Hinweis")
                    Else
                        MsgBox("Fehler beim Erstellen des pdf" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Artikel-Hinweis")
                    End If
                End Try
                'Speicher wieder freigeben
                Rasterizer.Dispose()
            Else
                VorschauPDF.Image = Nothing
            End If
        Else
            VorschauPDF.Image = Nothing
        End If
        'Mauszeiger anpassen
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BtnLoadPdf_Click(sender As Object, e As EventArgs) Handles BtnLoadPdf.Click
        If OpenPdfFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Artikel.VerarbeitungsHinweise = IO.Path.GetFileNameWithoutExtension(OpenPdfFile.SafeFileName)
            Artikel.VerarbeitungsHinweisePfad = IO.Path.GetDirectoryName(OpenPdfFile.FileName)
            DetailInfo(sender)
            Artikel.UpdateDB()
        End If
    End Sub

    Private Sub BtnRotateR_Click(sender As Object, e As EventArgs) Handles BtnRotateR.Click
        VorschauPDF.Image.RotateFlip(Drawing.RotateFlipType.Rotate90FlipNone)
        VorschauPDF.Refresh()
    End Sub

    Private Sub BtnRotateL_Click(sender As Object, e As EventArgs) Handles BtnRotateL.Click
        VorschauPDF.Image.RotateFlip(Drawing.RotateFlipType.Rotate270FlipNone)
        VorschauPDF.Refresh()
    End Sub

    Private Sub BtnTransferPdf_Click(sender As Object, e As EventArgs) Handles BtnTransferPdf.Click
        'Mauszeiger anpassen
        Me.Cursor = Cursors.WaitCursor

        'ftp-Files werden im Temp-Verzeichnis erzeugt
        Dim nmeHTML As String = Artikel.VerarbeitungsHinweise & ".html"
        Dim nmePNG As String = Artikel.VerarbeitungsHinweise & ".png"

        Dim ftpHTML As String = wb_GlobalSettings.pTempPath & nmeHTML
        Dim ftpPNG As String = wb_GlobalSettings.pTempPath & nmePNG
        Const C34 As Char = Chr(34)

        'pdf im Format png speichern
        VorschauPDF.Image.Save(ftpPNG, ImageFormat.Png)
        'HTML-Hülle erzeugen
        Dim HTMLFile As New StreamWriter(ftpHTML)
        HTMLFile.WriteLine("<!DOCTYPE HTML PUBLIC " & C34 & "-// W3C // DTD HTML 4.01 Transitional//EN" & C34 & ">")
        HTMLFile.WriteLine("<html>")
        HTMLFile.WriteLine("<head>")
        HTMLFile.WriteLine("<title>WinBack</title>")
        HTMLFile.WriteLine("</head>")
        HTMLFile.WriteLine("<body text=" & C34 & "#000000" & C34 & "bgcolor=" & C34 & "#FFFFFF" & C34 & "link=" & C34 & "#FF0000" & C34 & "alink=" & C34 & "#FF0000" & C34 & "vlink=" & C34 & "#FF0000" & C34 & ">")
        HTMLFile.WriteLine("<img src=" & C34 & nmePNG & C34 & " width=" & C34 & "889" & C34 & " alt=" & C34 & C34 & " border=" & C34 & "0" & C34 & ">")
        HTMLFile.WriteLine("</body>")
        HTMLFile.WriteLine("</html>")
        HTMLFile.Close()

        'beide Files per FTP an den WinBack-Server übertragen
        Dim ftpErr As String
        ftpErr = wb_Functions.FTP_Upload_File(ftpHTML, wb_Global.WinBackServerHinweisDirectory & nmeHTML)
        If ftpErr Is Nothing Then
            ftpErr = wb_Functions.FTP_Upload_File(ftpPNG, wb_Global.WinBackServerHinweisDirectory & nmePNG)
        End If

        'Mauszeiger anpassen
        Me.Cursor = Cursors.Default
        'Fehlermeldung ausgeben
        If ftpErr IsNot Nothing Then
            MsgBox("Fehler bei der Datenübertragung zum WinBack-Server " & vbCrLf & ftpErr, MsgBoxStyle.Exclamation, "Fehler Artikel-Hinweis")
        End If
    End Sub

    Private Sub cbAufloesung_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAufloesung.SelectedIndexChanged
        'Auflösung einstellen
        If cbAufloesung.SelectedIndex > 0 Then
            dpi = wb_Functions.StrToInt(cbAufloesung.Text).ToString
        End If
        'Auflösung speichern
        Artikel.VerarbeitungsHinweise_DPI = dpi
        'pdf-File neu einlesen
        DetailInfo(sender)
        'Focus neu setzen
        cbAufloesung.SelectionLength = 0
        BtnTransferPdf.Focus()
    End Sub

End Class