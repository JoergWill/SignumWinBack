Imports System.Windows.Forms
Imports Ghostscript.NET.Rasterizer

Public Class wb_ShowPDF
    Private Shared Rasterizer As GhostscriptRasterizer
    Private Shared localDllInfo As Ghostscript.NET.GhostscriptVersionInfo
    Private Shared _MaxPages As Integer = 1

    Public Shared ReadOnly Property MaxPages As Integer
        Get
            Return _MaxPages
        End Get
    End Property

    ''' <summary>
    ''' gsdll32.dll extern einbinden (siehe auch https://github.com/jhabjan/Ghostscript.NET/blob/master/Ghostscript.NET.Samples/Samples/CustomGsdllLocationSample.cs)
    ''' Momentan funktioniert das nur mit der "alten" dll aus WinBack-Office
    ''' </summary>
    Shared Sub New()
        ' gsdll32.dll extern einbinden
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
            localDllInfo = New Ghostscript.NET.GhostscriptVersionInfo(wb_GlobalSettings.pProgrammPath & "gsdll32.dll")
        Else
            localDllInfo = New Ghostscript.NET.GhostscriptVersionInfo(wb_GlobalSettings.pAddInPath & "dll/gsdll32.dll")
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
            'Hinweis-Datei (pdf) laden
            Try
                Rasterizer = New GhostscriptRasterizer
                If dpi <> "" Then
                    Rasterizer.CustomSwitches.Add("-r" & dpi)
                End If
                'wirft Ghostscript not installed Exception !!!
                Rasterizer.Open(pdfFile, localDllInfo, True)
                'Anzahl der Seiten
                _MaxPages = Rasterizer.PageCount
                'Vorschau anzeigen (Seite)
                VorschauPDF.Image = Rasterizer.GetPage(96, 96, Page)
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
    End Sub
End Class
