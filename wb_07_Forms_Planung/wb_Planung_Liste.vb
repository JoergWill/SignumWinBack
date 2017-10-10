Imports System.Windows.Forms
Imports combit.ListLabel22.DataProviders
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_Liste
    Inherits DockContent
    Dim Produktion As New wb_Produktion

    Private Sub BtnVorlage_Click(sender As Object, e As EventArgs) Handles BtnVorlage.Click
        'Fenster Auswahl Vorlage anzeigen
        Dim Vorlage As New wb_Planung_Vorlage
        Vorlage.ShowDialog(Me)
        Me.Cursor = Cursors.WaitCursor

        If Vorlage.TWNr > 0 Then
            'Daten aus wbDaten einlesen
            If Not Produktion.MySQLdbSelect_ArbRzSchritte(Vorlage.TWNr) Then
                'Default-Cursor
                Me.Cursor = Cursors.Default
                'keine Datensätze in der Vorlage
                MsgBox("Keine Datensätze in dieser Vorlage", MsgBoxStyle.Exclamation, "Laden Produktionsdaten aus Vorlage")
                VirtualTree.Invalidate()
            Else
                'Virtual Tree anzeigen
                VirtualTree.DataSource = Produktion.RootRezeptSchritt
            End If
        End If
        'Default-Cursor
        Me.Cursor = Cursors.Default

    End Sub

    ''' <summary>
    ''' Neue Artikel-Zeile (mit Rezeptur anlegen)
    ''' TEST Artikel-Nummer 12
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnNeueCharge_Click(sender As Object, e As EventArgs) Handles btnNeueCharge.Click
        'TEST
        Produktion.AddArtikelCharge("2", "12", 0, 200)
        Produktion.AddArtikelCharge("1", "300", 0, 25, 25, "Filiale Seestrasse 5 Stk geschnitten anliefern")
        Produktion.AddArtikelCharge("1", "12", 0, 100, 90)
        Produktion.AddArtikelCharge("2", "300", 0, 50)
        'Virtual Tree anzeigen
        VirtualTree.DataSource = Produktion.RootRezeptSchritt
    End Sub

    Private Sub BtnBackZettelDrucken_Click(sender As Object, e As EventArgs) Handles BtnBackZettelDrucken.Click
        'Sortieren nach Teig(RezeptNummer), ArtikelNummer und Tour
        Produktion.RootRezeptSchritt.SortBackListe()

        Dim pDialog As New wb_PrinterDialog 'Drucker-Dialog
        'Druck-Daten
        pDialog.LL.DataSource = New ObjectDataProvider(Produktion.RootRezeptSchritt.ChildSteps)
        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Produktion"
        pDialog.ListFileName = "Backzettel.lst"
        pDialog.ShowDialog()

    End Sub

End Class
