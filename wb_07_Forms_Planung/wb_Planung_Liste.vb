Imports combit.ListLabel22.DataProviders
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_Liste
    Inherits DockContent
    Dim Produktion As New wb_Produktion

    Private Sub BtnVorlage_Click(sender As Object, e As EventArgs) Handles BtnVorlage.Click
        'Fenster Auswahl Vorlage anzeigen

        'Daten aus wbDaten einlesen
        If Not Produktion.MySQLdbSelect_ArbRzSchritte(24) Then
            'keine Datensätze in der Vorlage
            MsgBox("Keine Datensätze in dieser Vorlage", MsgBoxStyle.Exclamation, "Laden Produktionsdaten aus Vorlage")

            VirtualTree.Invalidate()
        Else
            'Virtual Tree anzeigen
            VirtualTree.DataSource = Produktion.RootRezeptSchritt
        End If


    End Sub

    Private Sub BtnBackZettelDrucken_Click(sender As Object, e As EventArgs) Handles BtnBackZettelDrucken.Click
        wb_Rezept_Shared.LL.DataSource = New ObjectDataProvider(Produktion)
        wb_Rezept_Shared.LL.Print()
    End Sub
End Class
