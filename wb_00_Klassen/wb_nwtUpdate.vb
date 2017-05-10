Imports WinBack.wb_Sql_Selects

Public Class wb_nwtUpdate
    Implements IDisposable
    Protected disposed As Boolean = False

    Private _InfoText As String = ""
    Private AktKO_Nr As Integer = 0

    Public ReadOnly Property InfoText As String
        Get
            Return DateTime.Now.ToLongTimeString & " " & _InfoText
        End Get
    End Property

    ''' <summary>
    ''' Sucht die nächst folgende Komponente sortiert nach KO_Nr aus der Datenbank
    ''' Einlesen der Komponenten-Daten in ein Komponenten-Objekt
    ''' Abfrage der Nährwert-Daten aus der Cloud (WinBack/Datenlink)
    ''' wenn die Daten aus der Cloud aktueller sind als in der WinBack-Datenbank wird ein Report generiert und
    ''' die Daten werden aktualisiert.
    ''' </summary>
    ''' <returns>True wenn der Datensatz aktualisiert wurde</returns>
    Public Function UpdateNext() As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)
        'Komponenten-Objekt nimmt die aktuellen Daten auf
        Dim nwtDaten As New wb_Komponenten
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
                If CloudChangeDate > WinBackChangeDate Then
                    'Protokoll der Änderungen speichern in Hinweise
                    nwtDaten.SaveReport()
                    'Protokoll der Änderungen ausgeben
                    Debug.Print(nwtDaten.GetReport)

                    'Ausgabe-Text
                    _InfoText = "(" & nwtDaten.Nr.ToString("00000") & ") " & nwtDaten.Bezeichung
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
    Public Function GetNaehrwerte(ID As String, nwtdaten As wb_Komponenten) As Date
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
    Private Function GetNaehrwerteHetzner(iD As String, nwtdaten As wb_Komponenten) As Date
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
    Private Function GetNaehrwerteDatenlink(iD As String, nwtdaten As wb_Komponenten) As Date
        'Create new instance of nwtCloud
        Dim dl As New wb_nwtDatenLink(wb_Credentials.Datenlink_PAT, wb_Credentials.Datenlink_CAT, wb_Credentials.Datenlink_Url)
        If dl.GetProductData(iD, nwtdaten) > 0 Then
            'Datum/Uhrzeit der letzten Änderung
            Return nwtdaten.ktTyp301.TimeStamp
        Else
            Return #11/22/1964 00:00:00#
        End If
    End Function

    Public Sub New()
        'letzte aktualisierte Komponenten-ID aus der winback.ini lesen
        Dim IniFile As New wb_IniFile
        AktKO_Nr = IniFile.ReadInt("Cloud", "UpdateNaehrwerteKONr")
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                ' Insert Code to free managed resource
            End If
            'Beim Programm-Ende wird die aktuelle Komponenten-Nummer gesichert
            Dim IniFile As New wb_IniFile
            IniFile.WriteInt("Cloud", "UpdateNaehrwerteKONr", AktKO_Nr)
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
