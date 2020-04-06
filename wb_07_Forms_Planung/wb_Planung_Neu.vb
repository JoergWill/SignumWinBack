Public Class wb_Planung_Neu

    Private Sub wb_Planung_Neu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Drop-Down-Liste Liniengruppe initialisieren

    End Sub


    ''' <summary>
    ''' Auswahlfenster Artikel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tArtikelNummer_DoubleClick(sender As Object, e As EventArgs) Handles tArtikelNummer.DoubleClick, tArtikelName.DoubleClick

    End Sub

    ''' <summary>
    ''' Auswahlfenster Rezeptur
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tRezeptNummer_DoubleClick(sender As Object, e As EventArgs) Handles tRezeptNummer.DoubleClick, tRezeptName.DoubleClick
        Dim RezeptAuswahl As New wb_Rezept_AuswahlListe
        If RezeptAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tRezeptNummer.Text = RezeptAuswahl.RezeptNr
            tRezeptName.Text = RezeptAuswahl.RezeptNummer
            tRezeptName.Text = RezeptAuswahl.RezeptName
        End If
        tGesMengeKg.Focus()
        RezeptAuswahl = Nothing
    End Sub

    ''' <summary>
    ''' Rezeptnummer suchen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tRezeptNummer_Leave(sender As Object, e As EventArgs) Handles tRezeptNummer.Leave
        'Rezept mit dieser Nummer suchen
        Dim RezeptAuswahl As New wb_Rezept_AuswahlListe(tRezeptNummer.Text, "")
        'wenn das Ergebnis nicht eindeutig ist
        If RezeptAuswahl.RowCount <> 1 Then
            'Auswahl-Dialog anzeigen
            If RezeptAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
                tRezeptNummer.Text = RezeptAuswahl.RezeptNr
                tRezeptName.Text = RezeptAuswahl.RezeptNummer
                tRezeptName.Text = RezeptAuswahl.RezeptName
            End If
        End If
        'tGesMengeKg.Focus()
        RezeptAuswahl = Nothing
    End Sub

    Private Sub tRezeptName_Leave(sender As Object, e As EventArgs) Handles tRezeptName.Leave
        'Rezept mit dieser Nummer suchen
        Dim RezeptAuswahl As New wb_Rezept_AuswahlListe("", tRezeptName.Text)
        'wenn das Ergebnis nicht eindeutig ist
        If RezeptAuswahl.RowCount <> 1 Then
            'Auswahl-Dialog anzeigen
            If RezeptAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
                tRezeptNummer.Text = RezeptAuswahl.RezeptNr
                tRezeptName.Text = RezeptAuswahl.RezeptNummer
                tRezeptName.Text = RezeptAuswahl.RezeptName
            End If
        End If
        'tGesMengeKg.Focus()
        RezeptAuswahl = Nothing
    End Sub

End Class