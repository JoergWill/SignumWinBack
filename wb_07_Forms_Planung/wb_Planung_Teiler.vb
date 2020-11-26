Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_Teiler
    Inherits DockContent

    Private Sub wb_Planung_Teiler_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'CheckedListBox Hintergrund an Formular anpassen
        cbChargenTeiler.BackColor = BackColor
        'CheckedListBox füllen 
        cbChargenTeiler.Fill(System.Enum.GetValues(GetType(wb_Global.ModusChargenTeiler)), AddressOf wb_Functions.ModusChargenTeilerToString)
        'CheckedListBox Voreinstellungen aus WinBack.ini
        cbChargenTeiler.SelIndex = wb_GlobalSettings.ChargenTeiler

        'CheckedListBox Hintergrund an Formular anpassen
        cbTeigOptimierung.BackColor = BackColor
        'CheckedListBox füllen 
        cbTeigOptimierung.Fill(System.Enum.GetValues(GetType(wb_Global.ModusTeigOptimierung)), AddressOf wb_Functions.ModusTeigOptimierungToString)
        'CheckedListBox Voreinstellungen aus WinBack.ini
        cbTeigOptimierung.SelIndex = wb_GlobalSettings.TeigOptimierung

    End Sub

    Private Sub cbChargenTeiler_SelectedIndexChanged(sender As Object, e As EventArgs)
        wb_GlobalSettings.ChargenTeiler = cbChargenTeiler.SelIndex
    End Sub

    Private Sub cbTeigOptimierung_SelectedIndexChanged(sender As Object, e As EventArgs)
        wb_GlobalSettings.TeigOptimierung = cbTeigOptimierung.SelIndex
    End Sub

End Class