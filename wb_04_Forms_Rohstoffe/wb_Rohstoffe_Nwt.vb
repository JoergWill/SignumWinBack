Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Nwt
    Inherits DockContent
    Dim nwtGrid As wb_ArrayGridViewKomponParam301
    Dim NwtTabelle(wb_Global.maxTyp301) As wb_Global.Nwt

    Private Sub wb_Rohstoffe_Nwt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf NaehrwertInfo
        'Daten vom aktuellen Rohstoff anzeigen
        If RohStoff.Nr > 0 Then
            NaehrwertInfo()
        End If
    End Sub

    Public Sub NaehrwertInfo()
        RohStoff.MySQLdbRead(RohStoff.Nr)

        'Deklarationsfelder
        tbDeklarationExtern.Text = RohStoff.DeklBezeichungExtern
        tbDeklarationIntern.Text = RohStoff.DeklBezeichungIntern

        'Array aufbauen über alle Nährwerte - Grid aus KomponParam301_global, Werte aus Rezept.ktTyp301.Wert(_RootRezeptschritt)
        For i = 1 To wb_Global.maxTyp301
            NwtTabelle(i).Visible = wb_KomponParam301_Global.kt301Param(i).Used
            NwtTabelle(i).Nr = i
            NwtTabelle(i).Text = wb_KomponParam301_Global.kt301Param(i).Bezeichnung
            NwtTabelle(i).Wert = RohStoff.ktTyp301.Wert(i)
            NwtTabelle(i).Einheit = wb_KomponParam301_Global.kt301Param(i).Einheit
            NwtTabelle(i).Header = wb_Functions.kt301GruppeToString(wb_KomponParam301_Global.kt301Param(i).Gruppe)
            NwtTabelle(i).FehlerText = ""

            'Debug.Print("FEHLER :" & Rezept.KtTyp301.FehlerKompName(i))
            'If NwtTabelle(i).Visible Then
            '    Debug.Print(NwtTabelle(i).Header & " " & NwtTabelle(i).Text & " " & NwtTabelle(i).Wert & " " & NwtTabelle(i).Einheit)
            'End If
        Next

        'Daten im Grid anzeigen
        If nwtGrid IsNot Nothing Then
            nwtGrid.Dispose()
        End If
        nwtGrid = New wb_ArrayGridViewKomponParam301(NwtTabelle)
        nwtGrid.BackgroundColor = Me.BackColor
        nwtGrid.GridLocation(pnl_Nwt)
        nwtGrid.PerformLayout()

    End Sub


End Class