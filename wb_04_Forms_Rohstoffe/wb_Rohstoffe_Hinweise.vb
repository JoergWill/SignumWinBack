Imports WeifenLuo.WinFormsUI.Docking
Imports System.Windows.Forms

Public Class wb_Rohstoffe_Hinweise
    Inherits DockContent
    Private dpi As String = ""
    Private Page As Integer = 1
    Private MaxPages As Integer = 1

    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Artikel_Hinweise_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Artikel-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Rohstoffe_Shared.eListe_Click, AddressOf DetailInfo
        'Beim ersten Aufruf wird der aktuelle Artikel angezeigt. Sonst wird beim Öffnen des Detail-Info-Fensters
        'der Inhalt der Textfelder gelöscht !!
        If wb_Rohstoffe_Shared.RohStoff IsNot Nothing Then
            DetailInfo(sender)
        End If
        'Focus neu setzen
        cbAufloesung.SelectionLength = 0
        BtnTransferPdf.Focus()
    End Sub

    Private Sub wb_Artikel_Hinweise_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Rohstoffe_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    ''' <summary>
    ''' Anzeige der Artikel-Details.
    ''' Wird aufgerufen durch Event eListe_Click(). Aktualisiert die Anzeigefelder (Nummer/Text/Kommentar...)
    ''' </summary>
    Private Sub DetailInfo(sender As Object, Optional Reload As Boolean = False)
        'Rohstoff mit gültiger Rezeptnummer
        If wb_Rohstoffe_Shared.RohStoff.RzNr > 0 Then

            'Auswahl pdf und/oder Drehen möglich
            Panel1.Enabled = True
            Panel2.Enabled = True

            'Datei-Name Verarbeitungshinweis (pdf)
            tHinweisName.Text = wb_Rohstoffe_Shared.RohStoff.VerarbeitungsHinweise
            'Auflösung (Umwandlung pdf nach png)
            dpi = wb_Rohstoffe_Shared.RohStoff.VerarbeitungsHinweise_DPI
            If dpi <> "" And dpi <> "0" Then
                cbAufloesung.Text = dpi & " dpi"
            Else
                cbAufloesung.Text = "Default"
            End If

            'PDF-Seite anzeigen
            Page = 1
            ShowPDF(Page)
        Else
            'Keine Auswahl/Drehen...
            Panel1.Enabled = False
            Panel2.Enabled = False
            'keine Hinweise anzeigen
            VorschauPDF.Image = Nothing
        End If
    End Sub

    Private Sub ShowPDF(Seite As Integer)
        'wenn ein Artikel-Verarbeitungshinweis vorhanden ist
        If wb_Rohstoffe_Shared.RohStoff.VerarbeitungsHinweisePfad <> "" Then
            'Mauszeiger anpassen
            Me.Cursor = Cursors.WaitCursor
            'Dateiname pdf-File
            wb_ShowPDF.ShowPdfDokument(wb_Rohstoffe_Shared.RohStoff.VerarbeitungsHinweisePfad & "\" & wb_Rohstoffe_Shared.RohStoff.VerarbeitungsHinweise & ".pdf", VorschauPDF, Seite, dpi)
            MaxPages = wb_ShowPDF.MaxPages
        Else
            VorschauPDF.Image = Nothing
            MaxPages = 1
        End If
        'Mauszeiger anpassen
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BtnLoadPdf_Click(sender As Object, e As EventArgs) Handles BtnLoadPdf.Click
        If OpenPdfFile.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            wb_Rohstoffe_Shared.RohStoff.VerarbeitungsHinweise = IO.Path.GetFileNameWithoutExtension(OpenPdfFile.SafeFileName)
            wb_Rohstoffe_Shared.RohStoff.VerarbeitungsHinweisePfad = IO.Path.GetDirectoryName(OpenPdfFile.FileName)
            DetailInfo(sender)

            'Anzahl der Seiten im pdf
            Page = 1
            MaxPages = wb_ShowPDF.MaxPages
            'wenn mehrere Seiten im pdf vorhanden sind, werden die Buttons angezeigt
            If MaxPages > 1 Then
                BtnPageMinus.Visible = True
                BtnPagePlus.Visible = True
            Else
                BtnPageMinus.Visible = False
                BtnPagePlus.Visible = False
            End If

            'Verarbeitungshinweis-Filename in DB schreiben
            wb_Rohstoffe_Shared.RohStoff.UpdateDB()
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

        'Artikelhinweise per FTP an WinBack-Server übertragen
        If Not wb_ShowPDF.TransferPdfDokument(wb_Rohstoffe_Shared.RohStoff.VerarbeitungsHinweise, wb_Rohstoffe_Shared.RohStoff.VerarbeitungsHinweisePfad, VorschauPDF, dpi) Then
            Select Case wb_ShowPDF.ErrCode
                Case wb_ShowPDF.ErrorCodes.ErrLoadPdf
                    MsgBox(wb_ShowPDF.ErrText, MsgBoxStyle.Critical, "Verarbeitungshinweise")
                Case wb_ShowPDF.ErrorCodes.ErrFTP
                    MsgBox(wb_ShowPDF.ErrText, MsgBoxStyle.Exclamation, "Fehler Artikel-Hinweis")
            End Select
        End If

        'Mauszeiger anpassen
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cbAufloesung_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAufloesung.SelectedIndexChanged
        'Auflösung einstellen
        If cbAufloesung.SelectedIndex > 0 Then
            dpi = wb_Functions.StrToInt(cbAufloesung.Text).ToString
        End If
        'Auflösung speichern
        wb_Rohstoffe_Shared.RohStoff.VerarbeitungsHinweise_DPI = dpi
        'pdf-File neu einlesen
        DetailInfo(sender)
        'Focus neu setzen
        cbAufloesung.SelectionLength = 0
        BtnTransferPdf.Focus()
    End Sub

    Private Sub BtnPageMinus_Click(sender As Object, e As EventArgs) Handles BtnPageMinus.Click
        If Page > 1 Then
            Page = Page - 1
            'pdf-File anzeigen
            ShowPDF(Page)
        End If
    End Sub

    Private Sub BtnPagePlus_Click(sender As Object, e As EventArgs) Handles BtnPagePlus.Click
        If Page < MaxPages Then
            Page = Page + 1
            'pdf-File neu einlesen
            ShowPDF(Page)
        End If
    End Sub
End Class