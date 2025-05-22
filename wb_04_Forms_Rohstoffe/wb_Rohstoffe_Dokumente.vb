Imports System.IO

Public Class wb_Rohstoffe_Dokumente
    Private nwt As wb_nwtCl_WinBack
    Private id As String
    Private Page As Integer = 1
    Private MaxPages As Integer = 1
    Private _PdfName As String
    Private WithEvents printDoc As New Drawing.Printing.PrintDocument()


    Public Sub New(nwt As wb_nwtCl_WinBack, id As String)
        Me.nwt = nwt
        Me.id = id
        'Alle Elemente der Oberfläche erzeugen
        Me.InitializeComponent()
        'Liste alle hinterlegten Dokumente
        ShowDetails()
    End Sub

    Public Property PdfName As String
        Get
            Return _PdfName
        End Get
        Set(value As String)
            _PdfName = value
        End Set
    End Property

    Private Sub ShowDetails()
        'Liste aller hinterlegten Dokumente
        lbDokumente.Items.Clear()

        For Each Dokument In nwt.getProductSheetList
            lbDokumente.Items.Add(Dokument)
        Next

        'Wenn nur ein Dokument vorhanden ist, gleich anzeigen
        If lbDokumente.Items.Count = 1 Then
            ShowDokument(lbDokumente.Items(0).ToString)
        End If
    End Sub

    Private Sub lbDokumente_DoubleClick(sender As Object, e As EventArgs) Handles lbDokumente.DoubleClick
        ShowDokument(lbDokumente.SelectedItem)
    End Sub

    ''' <summary>
    ''' Rohstoff-Produktdatenblatt aus der Cloud einlesen und im Verzeichnis ablegen.
    ''' Das Verzeichnis wird in wb_GlobalSettings als pRohstoffDatenPath bestimmt.
    ''' 
    ''' Ist die Datei ein pdf-File wird gleich ein Vorschau-Bild angezeigt.
    ''' </summary>
    ''' <param name="pdfName"></param>
    Private Sub ShowDokument(pdfName As String)
        'Start immer mit Seite 1
        Page = 1
        _PdfName = pdfName

        'Stream aus Cloud
        If nwt.GetProductSheet(id, pdfName) > 0 Then

            Select Case Path.GetExtension(pdfName)
                Case ".pdf"
                    ShowPDF(Page)
                    'Auswahl/Drehen...
                    PnlBearbeiten.Enabled = True
                Case Else
                    VorschauPDF.Image = Nothing
                    'Keine Auswahl/Drehen...
                    PnlBearbeiten.Enabled = False
            End Select
        Else
            'Prüfen ob das Ziel-Verzeichnis existiert...
            If IO.Directory.Exists(wb_GlobalSettings.pRohstoffDatenPath) Then
                'Fehler beim Laden aus der Cloud
                MsgBox("Fehler beim Laden des Dokumentes aus der Cloud", MsgBoxStyle.Exclamation, "Rohstoff-Datenblatt")
            Else
                'Fehler Verzeichnis exisitiert nicht oder keine Schreib-Rechte...
                MsgBox("Fehler beim Laden des Dokumentes" & vbCrLf & "Das Verzeichnis " & wb_GlobalSettings.pRohstoffDatenPath & " exisitiert nicht !", MsgBoxStyle.Exclamation, "Rohstoff-Datenblatt")
            End If
            VorschauPDF.Image = Nothing
                'Keine Auswahl/Drehen...
                PnlBearbeiten.Enabled = False
            End If

            'Anzahl der Seiten im pdf
            MaxPages = wb_ShowPDF.MaxPages

        'wenn mehrere Seiten im pdf vorhanden sind, werden die Buttons angezeigt
        If MaxPages > 1 Then
            BtnPageMinus.Visible = True
            BtnPagePlus.Visible = True
        Else
            BtnPageMinus.Visible = False
            BtnPagePlus.Visible = False
        End If

    End Sub

    Private Sub ShowPDF(Page)
        wb_ShowPDF.ShowPdfDokument(wb_GlobalSettings.pRohstoffDatenPath & PdfName, VorschauPDF, Page)
    End Sub

    Private Sub BtnPagePlus_Click(sender As Object, e As EventArgs) Handles BtnPagePlus.Click
        If Page < MaxPages Then
            Page = Page + 1
            'pdf-File neu einlesen
            ShowPDF(Page)
        End If
    End Sub

    Private Sub BtnPageMinus_Click(sender As Object, e As EventArgs) Handles BtnPageMinus.Click
        If Page > 1 Then
            Page = Page - 1
            'pdf-File anzeigen
            ShowPDF(Page)
        End If
    End Sub

    Private Sub BtnRotateL_Click(sender As Object, e As EventArgs) Handles BtnRotateL.Click
        VorschauPDF.Image.RotateFlip(Drawing.RotateFlipType.Rotate270FlipNone)
        VorschauPDF.Refresh()
    End Sub

    Private Sub BtnRotateR_Click(sender As Object, e As EventArgs) Handles BtnRotateR.Click
        VorschauPDF.Image.RotateFlip(Drawing.RotateFlipType.Rotate90FlipNone)
        VorschauPDF.Refresh()
    End Sub

    Private Sub BtnPrintPage_Click(sender As Object, e As EventArgs) Handles Button1.Click
        printDoc.Print()
    End Sub
    Private Sub PrintImage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDoc.PrintPage
        e.Graphics.DrawImage(VorschauPDF.Image, 0, 0)
    End Sub

    Private Sub BtnOpenPage_Click(sender As Object, e As EventArgs) Handles BtnOpenPage.Click
        Try
            Process.Start(wb_GlobalSettings.pRohstoffDatenPath & PdfName)
        Catch ex As Exception
            'Fehler beim Anzeigen des pdf
            MsgBox("Fehler beim Anzeigen des Dokumentes", MsgBoxStyle.Exclamation, "Rohstoff-Datenblatt")
        End Try
    End Sub
End Class