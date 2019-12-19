Imports System.IO
Imports Ghostscript.NET.Rasterizer
Imports WinBack

Public Class wb_Rohstoff_Dokumente
    Private nwt As wb_nwtCl_WinBack
    Private id As String

    Private Rasterizer As GhostscriptRasterizer
    Private localDllInfo As Ghostscript.NET.GhostscriptVersionInfo

    Private Sub wb_Rohstoff_Dokumente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Public Sub New(nwt As wb_nwtCl_WinBack, id As String)
        ' gsdll32.dll extern einbinden
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
            localDllInfo = New Ghostscript.NET.GhostscriptVersionInfo(wb_GlobalSettings.pProgrammPath & "gsdll32.dll")
        Else
            localDllInfo = New Ghostscript.NET.GhostscriptVersionInfo(wb_GlobalSettings.pAddInPath & "gsdll32.dll")
        End If

        Me.nwt = nwt
        Me.id = id
        'Alle Elemente der Oberfläche erzeugen
        Me.InitializeComponent()
        'Liste alle hinterlegten Dokumente
        ShowDetails()
    End Sub

    Private Sub ShowDetails()
        'Liste aller hinterlegten Dokumente
        lbDokumente.Items.Clear()

        For Each Dokument In nwt.getProductSheetList
            lbDokumente.Items.Add(Dokument)
        Next

        'Wenn nur ein Dokument vorhanden ist, gleich anzeigen
        'If lbDokumente.Items.Count = 1 Then
        '    ShowDokument(lbDokumente.Items(0).ToString)
        'End If
    End Sub

    ''' <summary>
    ''' Rohstoff-Produktdatenblatt aus der Cloud einlesen und im Verzeichnis ablegen.
    ''' Das Verzeichnis wird in wb_GlobalSettings als pRohstoffDatenPath bestimmt.
    ''' 
    ''' Ist die Datei ein pdf-File wird gleich ein Vorschau-Bild angezeigt.
    ''' </summary>
    ''' <param name="Name"></param>
    Private Sub ShowDokument(Name As String)
        'Stream aus Cloud
        If nwt.GetProductSheet(id, Name) Then

            Select Case Path.GetExtension(Name)
                Case ".pdf"
                    ShowPdfDokument(wb_GlobalSettings.pRohstoffDatenPath & Name)
                Case Else
                    VorschauPDF.Image = Nothing
            End Select
        Else
            VorschauPDF.Image = Nothing
        End If
    End Sub

    Private Sub ShowPdfDokument(pdfFile As String, Optional dpi As String = "")
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
    End Sub

    Private Sub lbDokumente_DoubleClick(sender As Object, e As EventArgs) Handles lbDokumente.DoubleClick
        ShowDokument(lbDokumente.SelectedItem)
    End Sub
End Class