Imports WinBack.wb_Sql_Selects

Public Class wb_OrgaBackProcess_WE
    Inherits wb_OrgaBackProcess

    Public Sub New(ProcessCode As String, ProcessNumber As String)
        'Daten aus [dbo].[GeschäftsvorfallPosition] einlesen
        MyBase.New(ProcessCode, ProcessNumber)
        'Debug-Ausgabe
        Debug.Print("WinBack.ob_Main.ProcessChanged Wareneingang verbucht Vorfall-Nummer " & ProcessNumber)
    End Sub

    ''' <summary>
    ''' Der OrgaBack-Vorfall WE wird in WinBack als Lieferung eingebucht.
    ''' Wenn die Rohstoff-Nummer eindeutig zugeordnet werden kann, wird die Lieferung in WinBack eingetragen und True zurückgegeben
    ''' Ist die Rohstoff-Nummer nicht eindeutig zuzuordnen, wird False zurückgegeben
    ''' </summary>
    ''' <param name="Action"></param>
    ''' <returns></returns>
    Public Overrides Function DoAction(PositionNummer As Integer, Action As Signum.OrgaSoft.ERP.ProcessChangedAction) As Boolean
        'Artikel-Nummer 
        Dim ArtikelNummer As String = ProcPositions(PositionNummer - 1).ArtikelNummer
        'Vorfall WE
        Debug.Print("Vorfall WE Action " & PositionNummer & "/" & ArtikelNummer & "/" & Action.ToString)

        'Prüfen ob für diese Rohstoff-Nummer mehrere Silos vorhanden sind
        If wb_Rohstoffe_Shared.HatSiloUmschaltung(ArtikelNummer) Then
            'es sind mehrere Silos vorhanden - Dialog-Fenster Verteilung der Lieferung auf mehrere Silos (öffnet in ob_Main_Menu)
            Return False
        Else
            'nur ein Rohstoff in WinBack vorhanden - eindeutige Zuordnung möglich
            Dim s As New wb_LagerSilo
            s.CopyFrom(ProcPositions(PositionNummer - 1))
            'Lieferung verbuchen
            LieferungVerbuchen(s)
            Return True
        End If
    End Function

    Public Overloads Function DoAction(PositionNummer As Integer, Action As Signum.OrgaSoft.ERP.ProcessChangedAction, Silos As List(Of wb_LagerSilo)) As Boolean
        'alle Lieferungen in der Liste
        For Each s As wb_LagerSilo In Silos
            'Silo wird/wurde befüllt oder auf Null gesetzt
            If (s.BefMenge > 0) Or (s.TaraWert > 0) Then
                'Lieferung verbuchen
                LieferungVerbuchen(s)
            End If
        Next
        Return True
    End Function

    Private Sub LieferungVerbuchen(s As wb_LagerSilo)
        'Lieferungen-Objekt nimmt die aktuellen Daten auf
        Dim Lieferungen As New wb_Lieferungen

        'Datensatz aus Komponenten/Lagerorte lesen - Lieferung verbuchen - Bilanzmenge aktualisieren
        Lieferungen.LieferungVerbuchen(s)
    End Sub

End Class
