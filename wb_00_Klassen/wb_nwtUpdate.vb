Imports WinBack.wb_Sql_Selects

Public Class wb_nwtUpdate
    Implements IDisposable
    Protected disposed As Boolean = False

    Private _InfoText As String = ""
    Private _AktKO_Nr As Integer = 0

    Public ReadOnly Property InfoText As String
        Get
            Return DateTime.Now.ToLongTimeString & " " & _InfoText
        End Get
    End Property

    Public Property AktKO_Nr As Integer
        Get
            Return _AktKO_Nr
        End Get
        Set(value As Integer)
            _AktKO_Nr = value
        End Set
    End Property

    ''' <summary>
    ''' Sucht die nächst folgende Komponente sortiert nach KO_Nr aus der Datenbank
    ''' Einlesen der Komponenten-Daten in ein Komponenten-Objekt
    ''' Abfrage der Nährwert-Daten aus der Cloud (WinBack/Datenlink)
    ''' wenn die Daten aus der Cloud aktueller sind als in der WinBack-Datenbank wird ein Report generiert und
    ''' die Daten werden aktualisiert.
    ''' </summary>
    ''' <returns>True wenn der Datensatz aktualisiert wurde</returns>
    Public Function UpdateNext(KompNr As Integer, Optional bUpdateOrgaBack As Boolean = False) As Boolean
        'Aktuelle Komponenten-Nummer setzen
        AktKO_Nr = KompNr
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Komponenten-Objekt nimmt die aktuellen Daten auf
        Dim nwtDaten As New wb_Komponente
        UpdateNext = False

        'nächsten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(setParams(sqlUpdateNWT, AktKO_Nr.ToString)) Then
            'Lesen KO-Nr
            If winback.Read Then
                'nächste KO_Nr aus DB in Object nwtDaten einlesen
                nwtDaten.MySQLdbRead(winback.MySqlRead)
                winback.CloseRead()
                AktKO_Nr = nwtDaten.Nr

                'Stammdaten und Nährwerte aus DB in (Object)nwtDaten einlesen
                If winback.sqlSelect(setParams(sqlgetNWT, AktKO_Nr.ToString)) Then
                    'Lesen KO-Nr
                    If winback.Read Then
                        nwtDaten.MySQLdbRead(winback.MySqlRead)
                    End If
                End If

                'Datum der letzen Nährwert-Aktualisierung in der WinBack-Datenbank
                Dim WinBackChangeDate As Date = nwtDaten.ktTyp301.TimeStamp
                'Änderungs-Log löschen
                nwtDaten.ClearReport()
                'Nährwert-Info aus der Cloud lesen (Datum der letzten Änderung). Die Daten werden in nwtDaten eingetragen
                Dim CloudChangeDate As Date = GetNaehrwerte(nwtDaten.MatchCode, nwtDaten)

                'wenn die Daten in der Cloud aktueller sind - Änderungen ausgeben
                If (CloudChangeDate > WinBackChangeDate) Or bUpdateOrgaBack Then
                    'Änderungen der Nährwerte in Komponente(Rohstoff) sichern
                    nwtDaten.MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
                    'Alle Artikel, welche diese Komponente in Rezepturen verwenden markieren
                    'die Nährwerte müssen neu berechnet werden. Farbige Markierung in der Artikel-Liste
                    nwtDaten.MySQLdbSetMarker(wb_Global.ArtikelMarker.nwtUpdate)

                    'Änderungen der Komponenten-Parameter(Rohstoff) in OrgaBack-DB schreiben
                    'Gibt true zurück, wenn der Artikel in OrgaBack existiert
                    If nwtDaten.MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301) Then
                        'Zutaten-und Allergenliste in OrgaBack updaten
                        nwtDaten.MsSqldbUpdate_Zutatenliste()
                    End If

                    'Protokoll der Änderungen speichern in Hinweise
                    nwtDaten.SaveReport()
                    'Protokoll der Änderungen ausgeben
                    Debug.Print(nwtDaten.GetReport)

                    'Ausgabe-Text
                    _InfoText = "(" & nwtDaten.Nr.ToString("00000") & ") " & nwtDaten.Bezeichnung
                    UpdateNext = True
                End If
            Else
                'EOF() - ReStart bei KO_Nr = 0
                AktKO_Nr = 0
                _InfoText = ""
            End If
        End If
        winback.Close()
        Return UpdateNext
    End Function

    ''' <summary>
    ''' Abfrage der Nährwerte aus der Cloud
    ''' Abhängig von der ID wird die jeweilige Routine für Abfrage und Dekodierung
    ''' der Nährwert-Daten aufgerufen
    ''' Die Daten werden in nwtDaten eingtragen
    ''' 
    '''     DL-xxxx Datenlink
    '''     xxxx    WinBack-Cloud
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <returns>Gibt das Datum der letzten Änderung in der Cloud zurück</returns>
    Public Function GetNaehrwerte(ID As String, nwtdaten As wb_Komponente) As Date
        If Left(ID, 3) = "DL-" Then
            Return GetNaehrwerteDatenlink(ID, nwtdaten)
        Else
            Return GetNaehrwerteHetzner(ID, nwtdaten)
        End If
    End Function

    ''' <summary>
    ''' Abfrage der Daten aus der WinBack Cloud (Hetzner-Server)
    ''' Die Daten werden in nwtDaten eingtragen
    ''' 
    ''' </summary>
    ''' <param name="iD"></param>
    ''' <returns>TimeStamp (DateTime) - Änderungsdatum aus der Cloud</returns>
    Private Function GetNaehrwerteHetzner(iD As String, nwtdaten As wb_Komponente) As Date
        Dim nwt As New wb_nwtCloud(wb_Credentials.WinBackCloud_Pass, wb_Credentials.WinBackCloud_Url)
        If nwt.GetProductData(iD, nwtdaten) > 0 Then
            Return nwtdaten.ktTyp301.TimeStamp
        Else
            Return #11/22/1964 00:00:00#
        End If
    End Function

    ''' <summary>
    ''' Abfrage der Daten aus DatenLink
    ''' Das Ergebnis ist ein verschachteltes XML-Objekt
    ''' Die Daten werden in nwtDaten eingtragen
    ''' </summary>
    ''' <param name="iD"></param>
    ''' <returns>TimeStamp (DateTime) - Änderungsdatum aus der Cloud</returns>
    Private Function GetNaehrwerteDatenlink(iD As String, nwtdaten As wb_Komponente) As Date
        'Create new instance of nwtCloud
        Dim dl As New wb_nwtDatenLink(wb_Credentials.Datenlink_PAT, wb_Credentials.Datenlink_CAT, wb_Credentials.Datenlink_Url)
        If dl.GetProductData(iD, nwtdaten) > 0 Then
            'Datum/Uhrzeit der letzten Änderung
            Return nwtdaten.ktTyp301.TimeStamp
        Else
            Return #11/22/1964 00:00:00#
        End If
    End Function

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                ' Insert Code to free managed resource
            End If
        End If
        Me.disposed = True
    End Sub

#Region " IDisposable Support "
    ' Do not change or add Overridable to these methods.
    ' Put cleanup code in Dispose(ByVal disposing As Boolean).
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region
End Class
