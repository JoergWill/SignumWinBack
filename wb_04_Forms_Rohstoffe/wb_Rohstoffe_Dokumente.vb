Imports System.IO

Public Class wb_Rohstoffe_Dokumente
    Private nwt As wb_nwtCl_WinBack
    Private id As String
    Private Page As Integer = 1


    Public Sub New(nwt As wb_nwtCl_WinBack, id As String)
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
    ''' <param name="Name"></param>
    Private Sub ShowDokument(Name As String)
        'Stream aus Cloud
        If nwt.GetProductSheet(id, Name) Then

            Select Case Path.GetExtension(Name)
                Case ".pdf"
                    wb_ShowPDF.ShowPdfDokument(wb_GlobalSettings.pRohstoffDatenPath & Name, VorschauPDF, Page)
                Case Else
                    VorschauPDF.Image = Nothing
            End Select
        Else
            VorschauPDF.Image = Nothing
        End If
    End Sub

End Class