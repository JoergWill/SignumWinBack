Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Silo
    Inherits DockContent

    Dim s As New List(Of wb_SiloRohstoff)
    Dim sType As wb_Global.RohSiloTypen = wb_Global.RohSiloTypen.UNDEF
    Dim aType As wb_Global.RohSiloTypen = wb_Global.RohSiloTypen.UNDEF
    Dim sFuellStand As New Hashtable

    Private Sub wb_Rohstoffe_Silo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo

        'Beim ersten Aufruf wird der aktuelle Rohstoff angezeigt. Sonst wird beim Öffnen des Detail-Info-Fensters
        'der Inhalt der Textfelder gelöscht !!
        If RohStoff IsNot Nothing Then
            DetailInfo(sender)
        End If
    End Sub

    Private Sub wb_Rohstoffe_Silo_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Rohstoffe_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    ''' <summary>
    ''' Anzeige der Rohstoff-Details.
    ''' Wird aufgerufen durch Event eListe_Click(). Aktualisiert die Anzeigefelder (Nummer/Text/Kommentar...)
    ''' </summary>
    Private Sub DetailInfo(sender)
        'Prüfen ob zu dieser Rohstoff-Nummer ein/mehrere Silo's exisitieren
        sType = wb_Rohstoffe_Shared.GetRohSiloType(RohStoff.Lagerort)
        If (sType <> wb_Global.RohSiloTypen.UNDEF) Then

            'Silo-Type hat sich geändert
            If (sType <> aType) Then
                'Anzeige ausblenden
                TableLayoutPanel.Visible = False
                'Panels für die Silo-Füllstände anzeigen
                DrawSiloReihe()
                'Anzeige wieder einblenden
                TableLayoutPanel.Visible = True
            End If

            'Silo-Type merken
            aType = sType
            'Füllstände einlesen
            GetSiloFuellstand_DB(sType)
            'Füllstände in Grafik einblenden
            SetSiloFuellStand()
        Else
            TableLayoutPanel.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Zeichnet die einzelnen Silo-User-Controls in das Panel ein
    ''' </summary>
    Private Sub DrawSiloReihe()
        s.Clear()
        'Alle Silos einzeln anzeigen
        s = wb_Rohstoffe_Shared.GetAllSilos(RohStoff.Lagerort)

        'Array für die Controls (Silo-Füllstand)
        TableLayoutPanel.Controls.Clear()
        TableLayoutPanel.ColumnCount = s.Count
        'alle Silos anzeigen
        For i = 0 To s.Count - 1
            TableLayoutPanel.Controls.Add(s(i).Silo)
            s(i).SiloReiheMaxMenge = SiloReiheMaxMenge
        Next
    End Sub

    ''' <summary>
    ''' Ermittelt die Füllstände aus der Lagerorte-Tabelle und zeigt diese im User-Control an
    ''' </summary>
    Private Sub GetSiloFuellstand_DB(sType)
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim SiloNummer As Integer = 0
        Dim Bilanzmenge As Integer = 0
        Dim sql As String = ""

        'alle Datensätze lesen
        sFuellStand.Clear()
        'Silo-Füllstände aus winback.Lagerorte
        Select Case sType
            Case wb_Global.RohSiloTypen.BW
                sql = "BW*"
            Case wb_Global.RohSiloTypen.M
                sql = "M__"
            Case wb_Global.RohSiloTypen.MK
                sql = "MK*"
            Case wb_Global.RohSiloTypen.KKA
                sql = "KK*"
        End Select
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSiloGrpLager, sql))
        While winback.Read
            'Silo-Nummer
            SiloNummer = winback.iField("LG_Silo_Nr")
            'Bilanzmenge
            Bilanzmenge = wb_Functions.StrToInt(winback.sField("LG_Bilanzmenge"))
            'Daten in HashTable schreiben
            sFuellStand.Add(SiloNummer, Bilanzmenge)
        End While

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Füllstand aus HashTable in die Silo-Grafik übertragen
    ''' </summary>
    Private Sub SetSiloFuellStand()
        'alle Silos im Panel
        For i = 0 To s.Count - 1
            If sFuellStand.ContainsKey(s(i).SiloNr) Then
                s(i).IstMenge = sFuellStand(s(i).SiloNr)
            Else
                s(i).IstMenge = 0
            End If
        Next
    End Sub

End Class


''Prüfen ob zu dieser Rohstoff-Nummer ein/mehrere Silo's exisitieren
'Dim AnzSilos As Integer = wb_Rohstoffe_Shared.AnzahlSilos(RohStoff.Nummer)
''wenn Ein oder mehrere Silo's vorhanden sind
'If AnzSilos > 0 Then
'    'Array für die Controls (Silo-Füllstand)
'    TableLayoutPanel.Controls.Clear()
'    TableLayoutPanel.ColumnCount = AnzSilos

'    'Alle Silos einzeln anzeigen
'    Dim s As List(Of wb_SiloRohstoff) = wb_Rohstoffe_Shared.GetAllSilos(RohStoff.Lagerort)
'    For i = 1 To AnzSilos
'        TableLayoutPanel.Controls.Add(s(i - 1).Silo)
'    Next

'    TableLayoutPanel.Visible = True
'Else
'    TableLayoutPanel.Visible = False
'End If
