Imports WinBack.wb_Artikel_Shared
Imports WeifenLuo.WinFormsUI.Docking
Imports Ghostscript.NET.Rasterizer
Imports System.Drawing.Imaging
Imports System.IO

Public Class wb_Artikel_Hinweise
    Inherits DockContent

    Private Rasterizer As GhostscriptRasterizer
    Private localDllInfo As Ghostscript.NET.GhostscriptVersionInfo

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
            localDllInfo = New Ghostscript.NET.GhostscriptVersionInfo(wb_GlobalSettings.pProgrammPath & "\gsdll32.dll")
        Else
            localDllInfo = New Ghostscript.NET.GhostscriptVersionInfo(wb_GlobalSettings.pAddInPath & "\gsdll32.dll")
        End If

        'Beim ersten Aufruf wird der aktuelle Artikel angezeigt. Sonst wird beim Öffnen des Detail-Info-Fensters
        'der Inhalt der Textfelder gelöscht !!
        If Artikel IsNot Nothing Then
            DetailInfo(sender)
        End If
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
        'wenn ein Artikel-Verarbeitungshinweis vorhanden ist
        If Artikel.VerarbeitungsHinweisePfad <> "" Then
            'Dateiname pdf-File
            pdfFile = Artikel.VerarbeitungsHinweisePfad & "\" & Artikel.VerarbeitungsHinweise & ".pdf"
            'Prüfen ob Datei vorhanden
            If IO.File.Exists(pdfFile) Then
                'Hinweis-Datei (pdf) laden
                Rasterizer = New GhostscriptRasterizer
                'wirft Ghostscript not installed Exception !!!
                Rasterizer.Open(pdfFile, localDllInfo, True)
                VorschauPDF.Image = Rasterizer.GetPage(96, 96, 1)
            Else
                VorschauPDF.Image = Nothing
            End If
        Else
            VorschauPDF.Image = Nothing
        End If
    End Sub

    Private Sub BtnLoadPdf_Click(sender As Object, e As EventArgs) Handles BtnLoadPdf.Click
        If OpenPdfFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Artikel.VerarbeitungsHinweise = IO.Path.GetFileNameWithoutExtension(OpenPdfFile.SafeFileName)
            Artikel.VerarbeitungsHinweisePfad = IO.Path.GetDirectoryName(OpenPdfFile.FileName)
            DetailInfo(sender)
            Artikel.UpdateDB()
        End If
    End Sub

    Private Sub BtnRotatePdf_Click(sender As Object, e As EventArgs) Handles BtnRotatePdf.Click
        VorschauPDF.Image.RotateFlip(Drawing.RotateFlipType.Rotate90FlipNone)
        VorschauPDF.Refresh()
    End Sub

    Private Sub BtnTransferPdf_Click(sender As Object, e As EventArgs) Handles BtnTransferPdf.Click
        'ftp-Files werden im Temp-Verzeichnis erzeugt
        Dim ftpHTML As String = wb_GlobalSettings.pTempPath & Artikel.VerarbeitungsHinweise & ".html"
        Dim ftpPNG As String = wb_GlobalSettings.pTempPath & Artikel.VerarbeitungsHinweise & ".png"
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
        HTMLFile.WriteLine("<img src=" & C34 & ftpPNG & C34 & " width=" & C34 & "889" & C34 & " alt=" & C34 & C34 & " border=" & C34 & "0" & C34 & ">")
        HTMLFile.WriteLine("</body>")
        HTMLFile.WriteLine("</html>")
        HTMLFile.Close()

        'beide Files per FTP an den WinBack-Server übertragen
        wb_Functions.FTP_Upload_File(ftpHTML)
        wb_Functions.FTP_Upload_File(ftpPNG)

    End Sub
End Class