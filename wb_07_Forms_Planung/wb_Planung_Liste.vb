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

    Private Sub BtnBackZettelDrucken_Click(sender As Object, e As EventArgs) Handles BtnBackZettelDrucken.Click
        'wb_Rezept_Shared.LL.DataSource = New ObjectDataProvider(Produktion)
        'wb_Rezept_Shared.LL.Print()
    End Sub
End Class
