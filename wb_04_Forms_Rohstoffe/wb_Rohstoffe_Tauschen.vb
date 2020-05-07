Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Rohstoffe_Shared

Public Class wb_Rohstoffe_Tauschen
    Inherits DockContent

    Private Nr As Integer = wb_Global.UNDEFINED

    Private Sub wb_Rohstoffe_Tauschen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Rohstoff Name
        tbRohNrOrg.Text = RohStoff.Nummer
        'Rohstoff Nummer
        tbRohNameOrg.Text = RohStoff.Bezeichnung
    End Sub

    Private Sub tbRohNeu_Click(sender As Object, e As EventArgs) Handles tbRohNrNeu.Click, tbRohNrNeu.DoubleClick, tbRohNameNeu.DoubleClick, tbRohNameNeu.Click
        'Rohstoff-Auswahl-Liste
        Dim RohstoffAuswahl As New wb_Rohstoff_AuswahlListe
        'Auswahl Sauerteig/Produktion
        If wb_Rohstoffe_Shared.RohStoff.Type < wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL Then
            RohstoffAuswahl.Anzeige = wb_Rohstoffe_Shared.AnzeigeFilter.RezeptKomp
        Else
            RohstoffAuswahl.Anzeige = wb_Rohstoffe_Shared.AnzeigeFilter.Sauerteig
        End If
        'Auswahldialog Rohstoff
        If RohstoffAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tbRohNameNeu.Text = RohstoffAuswahl.RohstoffName
            tbRohNrNeu.Text = RohstoffAuswahl.RohstoffNummer
            Nr = RohstoffAuswahl.RohstoffNr
        End If
    End Sub

    Private Sub cbTauschen_Click(sender As Object, e As EventArgs) Handles cbTauschen.Click
        If cbTauschen.Checked Then
            lbRohstoffNeu.Text = "tauschen mit"
            BtnOK.Text = "Rohstoffe tauschen"
            lblTauschen.Visible = True
        Else
            lbRohstoffNeu.Text = "ersetzen durch"
            BtnOK.Text = "Rohstoffe ersetzen"
            lblTauschen.Visible = False
        End If
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        'Prüfe ob ein Rohstoff ausgewählt wurde
        If Nr <> wb_Global.UNDEFINED Then

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'Gibt die Anzahl der geänderten Rezept-Zeilen zurück
            Dim AnzahlRezeptZeilen As Integer = 0

            'Tauschen oder Ersetzen
            If cbTauschen.Checked Then
                'nächste freie Rohstoff-Nummer suchen
                Dim DummyRohNr As Integer = wb_sql_Functions.getNewKomponNummer()

                'Rohstoff A zunächst durch Dummy ersetzen
                Dim AnzRez_A As Integer = TauscheRohstoffeImRezept(wb_Rohstoffe_Shared.RohStoff.Nr, DummyRohNr, winback)
                'Rohstoff B wird ersetzt durch Rohstoff A
                AnzahlRezeptZeilen = TauscheRohstoffeImRezept(Nr, wb_Rohstoffe_Shared.RohStoff.Nr, winback)
                'Dummy wird Rohstoff B
                Dim AnzRez_B As Integer = TauscheRohstoffeImRezept(DummyRohNr, Nr, winback)

                'alle zwei Datenbank-Operationen müssen die gleiche Anzahl an Datensätzen zurückliefern
                If AnzRez_A <> AnzRez_B Then
                    'Fehler
                    AnzahlRezeptZeilen = -1
                Else
                    'Gesamtzahl aller Änderungen
                    AnzahlRezeptZeilen += AnzRez_A
                End If
            Else
                'Rohstoff A wird ersetzt durch Rohstoff B
                AnzahlRezeptZeilen = TauscheRohstoffeImRezept(wb_Rohstoffe_Shared.RohStoff.Nr, Nr, winback)
            End If

            'Datenbank-Verbindung wieder schliessen
            winback.Close()

            'Auswertung Ergebnis
            Select Case AnzahlRezeptZeilen
                Case -1
                    MsgBox("Fehler beim Ändern der Rezepturen !", MsgBoxStyle.Exclamation)
                    DialogResult = Windows.Forms.DialogResult.Abort
                Case 0
                    MsgBox("Rohstoff wird in keinen Rezepturen verwendet", MsgBoxStyle.Information)
                    DialogResult = Windows.Forms.DialogResult.No
                Case Else
                    MsgBox("Es wurden " & AnzahlRezeptZeilen.ToString & " Rezept-Zeilen geändert", MsgBoxStyle.Information)
                    DialogResult = Windows.Forms.DialogResult.OK
            End Select
        Else
            MsgBox("Keine Änderungen durchgeführt", MsgBoxStyle.Information)
            DialogResult = Windows.Forms.DialogResult.No
        End If

        'Fenster wieder schliessen
        Close()
    End Sub

    ''' <summary>
    ''' Ersetzt in allen Rezepten die angegebene alte gegen die neue Rohstoff-Nummer.
    ''' Wird als neue Rohstoff-Nummer -1 übergeben (Tausch), wird zuerst geprüft ob diese Nummer schon in Rezepturen exisiert.
    ''' 
    ''' Gibt die Anzahl der ersetzen Rezept-Zeilen zurück.
    ''' </summary>
    ''' <param name="NrAlt"></param>
    ''' <param name="NrNeu"></param>
    Private Function TauscheRohstoffeImRezept(NrAlt As Integer, NrNeu As Integer, winback As wb_Sql) As Integer
        'sql-Kommando Rohstoffe im Rezept tauschen
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptRohst, NrAlt.ToString, NrNeu.ToString)

        'Rohstoffe im Rezept tauschen
        TauscheRohstoffeImRezept = winback.sqlCommand(sql)
    End Function

End Class