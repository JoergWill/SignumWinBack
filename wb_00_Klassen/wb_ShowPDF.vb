Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms
Imports Ghostscript.NET.Rasterizer

Public Class wb_ShowPDF
    Private Shared Rasterizer As GhostscriptRasterizer
    Private Shared localDllInfo As Ghostscript.NET.GhostscriptVersionInfo
    Private Shared _MaxPages As Integer = 1
    Private Shared _ErrCode As ErrorCodes = ErrorCodes.NoError
    Private Shared _ErrText As String = ""

    Public Enum ErrorCodes
        NoError
        ErrLoadPdf
        ErrFTP
    End Enum

    Public Shared ReadOnly Property MaxPages As Integer
        Get
            Return _MaxPages
        End Get
    End Property

    Public Shared ReadOnly Property ErrCode As ErrorCodes
        Get
            Return _ErrCode
        End Get
    End Property

    Public Shared ReadOnly Property ErrText As String
        Get
            Return _ErrText
        End Get
    End Property

    ''' <summary>
    ''' gsdll32.dll extern einbinden (siehe auch https://github.com/jhabjan/Ghostscript.NET/blob/master/Ghostscript.NET.Samples/Samples/CustomGsdllLocationSample.cs)
    ''' Momentan funktioniert das nur mit der "alten" dll aus WinBack-Office
    ''' </summary>
    Shared Sub New()
        'gsdll32.dll extern einbinden
        Dim gsdll As String

        'Unterscheidung 32/64 Bit
        If Environment.Is64BitProcess Then
            gsdll = "gsdll64.dll"
        Else
            gsdll = "gsdll32.dll"
        End If

        'abhängig von der Programm-Variante
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
            localDllInfo = New Ghostscript.NET.GhostscriptVersionInfo(wb_GlobalSettings.pProgrammPath & wb_Global.SubDir_dll & gsdll)
        Else
            localDllInfo = New Ghostscript.NET.GhostscriptVersionInfo(wb_GlobalSettings.pAddInPath & wb_Global.SubDir_dll & gsdll)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pdfFile"></param>
    ''' <param name="VorschauPDF"></param>
    ''' <param name="dpi"></param>
    Shared Sub ShowPdfDokument(pdfFile As String, ByRef VorschauPDF As PictureBox, Page As Integer, Optional dpi As String = "")
        'Prüfen ob Datei vorhanden
        If IO.File.Exists(pdfFile) Then

            'Falls der Filename "problematische" Zeichen enthält wird vorsichtshalber die Datei auf einen ungefährlichen Namen in das 
            'User-Temp-Verzeichnis kopiert
            Dim pdfCopyPath As String = IO.Path.GetTempPath() & IO.Path.GetRandomFileName() & ".pdf"
            IO.File.Copy(pdfFile, pdfCopyPath)

            'Hinweis-Datei (pdf) laden
            Try
                Rasterizer = New GhostscriptRasterizer
                If dpi <> "" Then
                    Rasterizer.CustomSwitches.Add("-r" & dpi)
                End If
                'wirft Ghostscript not installed Exception !!!
                Rasterizer.Open(pdfCopyPath, localDllInfo, True)
                'Anzahl der Seiten
                _MaxPages = Rasterizer.PageCount
                'Vorschau anzeigen (Seite)
                VorschauPDF.Image = Rasterizer.GetPage(96, 96, Page)
            Catch ex As Exception
                If ex.Message.Contains("library") Then
                    'Library ist nicht installier
                    MsgBox("Bitte Ghostscript installieren !" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Artikel-Hinweis")
                Else
                    'pdf-Vorschau kann nicht erstellt werden - Anzeige im Windows-pdf-Reader
                    Trace.WriteLine("Fehler beim Erstellen des pdf " & ex.Message)
                    Process.Start(pdfCopyPath)
                    'MsgBox("Fehler beim Erstellen des pdf" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Artikel-Hinweis")
                End If
            End Try
            'Speicher wieder freigeben
            Rasterizer.Dispose()
        Else
            VorschauPDF.Image = Nothing
        End If
    End Sub

    Shared Function TransferPdfDokument(PdfName As String, pdfDirectory As String, ByRef VorschauPDF As PictureBox, Optional dpi As String = "") As Boolean
        'ftp-Files werden im Temp-Verzeichnis erzeugt
        Dim nmePage As String = "01"
        Dim nmeHTML As String = PdfName & ".html"
        Dim nmePNG As String = PdfName & "_" & nmePage & ".png"

        Dim ftpHTML As String = wb_GlobalSettings.pTempPath & nmeHTML
        Dim ftpPNG As String = wb_GlobalSettings.pTempPath & nmePNG
        Const C34 As Char = Chr(34)

        'alle Seiten übertragen
        For i = 1 To MaxPages
            'Dateiname pdf-File - Seite x
            wb_ShowPDF.ShowPdfDokument(pdfDirectory & "\" & PdfName & ".pdf", VorschauPDF, i, dpi)
            'Seite als String
            nmePage = i.ToString("0#")
            'Filename des umgewandelten Image als png
            nmePNG = PdfName & "_" & nmePage & ".png"
            ftpPNG = wb_GlobalSettings.pTempPath & nmePNG
            'pdf im Format png speichern
            If VorschauPDF.Image IsNot Nothing Then
                VorschauPDF.Image.Save(ftpPNG, ImageFormat.Png)
            Else
                _ErrText = "Fehler beim Laden/Anzeigen des pdf-Dokumentes " & ftpPNG
                _ErrCode = ErrorCodes.ErrLoadPdf
                Return False
            End If
        Next

        'HTML-Hülle erzeugen
        Dim HTMLFile As New StreamWriter(ftpHTML)
        HTMLFile.WriteLine("<!DOCTYPE HTML PUBLIC " & C34 & "-// W3C // DTD HTML 4.01 Transitional//EN" & C34 & ">")
        HTMLFile.WriteLine("<html>")
        HTMLFile.WriteLine("<head>")
        HTMLFile.WriteLine("<title>WinBack</title>")
        HTMLFile.WriteLine("</head>")
        HTMLFile.WriteLine("<body text=" & C34 & "#000000" & C34 & "bgcolor=" & C34 & "#FFFFFF" & C34 & "link=" & C34 & "#FF0000" & C34 & "alink=" & C34 & "#FF0000" & C34 & "vlink=" & C34 & "#FF0000" & C34 & ">")

        'mehrere Seiten untereinander
        For i = 1 To MaxPages
            nmePage = i.ToString("0#")
            nmePNG = PdfName & "_" & nmePage & ".png"
            HTMLFile.WriteLine("<img src=" & C34 & nmePNG & C34 & " width=" & C34 & "889" & C34 & " alt=" & C34 & C34 & " border=" & C34 & "0" & C34 & ">")
        Next

        HTMLFile.WriteLine("</body>")
        HTMLFile.WriteLine("</html>")
        HTMLFile.Close()

        'beide Files per FTP an den WinBack-Server übertragen
        Dim ftpErr As String
        ftpErr = wb_Functions.FTP_Upload_File(ftpHTML, wb_Global.WinBackServerHinweisDirectory & nmeHTML)
        If ftpErr Is Nothing Then
            'png-Files für jede Seite einzeln übertragen
            For i = 1 To MaxPages
                nmePage = i.ToString("0#")
                nmePNG = PdfName & "_" & nmePage & ".png"
                ftpPNG = wb_GlobalSettings.pTempPath & nmePNG
                ftpErr = wb_Functions.FTP_Upload_File(ftpPNG, wb_Global.WinBackServerHinweisDirectory & nmePNG)
            Next
        End If

        'Fehlermeldung ausgeben
        If ftpErr IsNot Nothing Then
            _ErrText = "Fehler bei der Datenübertragung zum WinBack-Server " & vbCrLf & ftpErr
            _ErrCode = ErrorCodes.ErrFTP
            Return False
        End If

        Return True
    End Function

End Class
