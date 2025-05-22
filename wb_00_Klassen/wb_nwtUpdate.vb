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
    Public Function UpdateNext(KompNr As Integer, Optional ByRef bUpdateOrgaBack As Boolean = False) As Boolean
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
                winback.CloseRead()

                'Datum der letzen Nährwert-Aktualisierung in der WinBack-Datenbank
                Dim WinBackChangeDate As Date = nwtDaten.ktTyp301.TimeStamp
                'Änderungs-Log löschen
                nwtDaten.ClearReport()
                'Nährwert-Info aus der Cloud lesen (Datum der letzten Änderung). Die Daten werden in nwtDaten eingetragen
                Dim CloudChangeDate As Date = GetNaehrwerte(nwtDaten.MatchCode, nwtDaten)

                'wenn die Daten in der Cloud aktueller sind - Änderungen ausgeben
                If (CloudChangeDate > WinBackChangeDate) Or bUpdateOrgaBack Then
                    'Änderungen in Datenbank schreiben (WinBack und OrgaBack)
                    DbUpdateNaehrwerte(nwtDaten, True)
                    'Protokoll der Änderungen speichern in Hinweise
                    nwtDaten.SaveReport()
                    'Protokoll der Änderungen ausgeben
                    'Debug.Print("Report " & nwtDaten.GetReport)

                    'Ausgabe-Text
                    _InfoText = "(" & nwtDaten.Nr.ToString("00000") & ") " & nwtDaten.Bezeichnung
                    UpdateNext = True
                End If
            Else
                'EOF() - ReStart bei KO_Nr = 0
                AktKO_Nr = 0
                _InfoText = ""
                'Reset Flag alle Artikel in OrgaBack updaten
                bUpdateOrgaBack = False
            End If
        End If
        winback.Close()
        Return UpdateNext
    End Function

    ''' <summary>
    ''' Schreibt die Nährwertinfo (nach Update) in die WinBack und OrgaBack-Datenbank
    ''' Wenn Artikel von der Nährwert-Änderung betroffen sind, wird der entsprechende 
    ''' Artikel-Datensatz markiert.
    ''' </summary>
    ''' <param name="UpdateOrgaBack"></param>
    Public Sub DbUpdateNaehrwerte(nwtDaten As wb_Komponente, UpdateOrgaBack As Boolean)
        'Änderungen der Nährwerte in Komponente(Rohstoff) sichern
        Debug.Print("Update (Komp)Nährwerte in WinBack " & nwtDaten.Nummer & " " & nwtDaten.Bezeichnung)
        nwtDaten.MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
        'Änderungen der Zutatenliste in Komponente(Rohstoff) sichern
        'Debug.Print("Update (Komp)Zutatenliste in WinBack " & nwtDaten.Nummer & " " & nwtDaten.Bezeichnung)
        'nwtDaten.MySqldbUpdate_Zutatenliste()
        'Alle Artikel, welche diese Komponente in Rezepturen verwenden markieren
        'die Nährwerte müssen neu berechnet werden. Farbige Markierung in der Artikel-Liste
        nwtDaten.MySQLdbSetMarkerRzptListe(wb_Global.ArtikelMarker.nwtUpdate)

        'Änderungen der Komponenten-Parameter(Rohstoff) in OrgaBack-DB schreiben
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OBServerTask Then
            'Gibt true zurück, wenn der Artikel in OrgaBack existiert
            Debug.Print("Update (Komp)Nährwerte in OrgaBack " & nwtDaten.Nummer & " " & nwtDaten.Bezeichnung)
            If nwtDaten.MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301) Then
                'Zutaten-und Allergenliste in OrgaBack updaten
                Debug.Print("Update (Komp)Zutatenliste in OrgaBack " & nwtDaten.Nummer & " " & nwtDaten.Bezeichnung)
                nwtDaten.MsSqldbUpdate_Zutatenliste()
            End If
        End If
    End Sub


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
        If Not ID = wb_Global.UNDEFINED.ToString Then
            If wb_Functions.IsDatenLinkID(ID) Then
                Return GetNaehrwerteDatenlink(ID, nwtdaten)
            Else
                Return GetNaehrwerteHetzner(ID, nwtdaten)
            End If
        Else
            Return #11/22/1964 00:00:00#
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
        Dim nwt As New wb_nwtCl_WinBack(wb_GlobalSettings.WinBackCloud_Pass, wb_GlobalSettings.WinBackCloud_Url)
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
        Dim dl As New wb_nwtCl_DatenLink(wb_GlobalSettings.Datenlink_PAT, wb_GlobalSettings.Datenlink_CAT, wb_GlobalSettings.Datenlink_Url)
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
