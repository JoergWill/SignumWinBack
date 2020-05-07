Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Verwendung
    Inherits DockContent

    Private Sub wb_Rohstoffe_Verwendung_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Rohstoff-tauschen im Popup-Menu
        VerwDataGridView.PopupItemAdd("Rohstoff in Rezepten ersetzen", "", Nothing, AddressOf RohstoffeTauschen, True)
        'Beim ersten Aufruf wird der aktuelle Rohstoff angezeigt
        If RohStoff IsNot Nothing Then
            DetailInfo(sender)
        End If
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo
    End Sub

    Private Sub wb_Rohstoffe_Verwendung_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Rohstoffe_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo(sender)
        'Clear Popup-Menu
        '        VerwDataGridView.ContextMenu.MenuItems.Clear()

        'DataGrid füllen
        VerwDataGridView.LoadVerwendung(RohStoff.Nr)
    End Sub

    Private Sub RohstoffeTauschen()
        'Dialog-Fenster Rohstoff im Rezept tauschen/ersetzen
        Dim RohstoffeTauschen As New wb_Rohstoffe_Tauschen
        'wenn Rezepturen geändert worden sind, wird die Anzeige aktualisiert
        If RohstoffeTauschen.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'Liste aktualisieren
            Liste_Click(Nothing)
        End If
    End Sub
End Class