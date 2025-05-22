Imports MySql.Data.MySqlClient

Public Class wb_SchnittstelleT1001
    Inherits wb_SchnittstelleTabelle

    Private _OptionArtikel As Boolean
    Private _OptionRohstoff As Boolean
    Private _OptionSauerteig As Boolean

    Public T1001_Nummer As New wb_SchnittstelleFeld("T1001_KO_Nr_AlNum", 1)                     'Artikel/Rohstoff-Nummer
    Public T1001_Name As New wb_SchnittstelleFeld("T1001_KO_Bezeichnung", 3)                    'Artikel/Rohstoff-Bezeichnung
    Public T1001_Kommentar As New wb_SchnittstelleFeld("T1001_KO_Kommentar", 4)                 'Artikel/Rohstoff-Kommentar
    Public T1001_KurzName As New wb_SchnittstelleFeld("T1001_KA_Kurzname", 5)                   'Artikel/Rohstoff-Kurzname
    Public T1001_Type As New wb_SchnittstelleFeld("T1001_KO_Type", 6)                           'Artikel/Rohstoff-Type
    Public T1001_RezeptNummer As New wb_SchnittstelleFeld("T1001_KA_RZ_Nr_AlNum", 7)            'Artikel/Rohstoff-Rezeptur-Nummer
    Public T1001_Lagerort As New wb_SchnittstelleFeld("T1001_KA_Lagerort", 8)                   'Artikel/Rohstoff-Lagerort
    Public T1001_StkGewicht As New wb_SchnittstelleFeld("T1001_KA_Stueckgewicht", 9)            'Artikel/Rohstoff-Stückgewicht
    Public T1001_ChargeOpt As New wb_SchnittstelleFeld("T1001_KA_Charge_Opt", 10)               'Artikel/Rohstoff-OptimalCharge
    Public T1001_ChargeMin As New wb_SchnittstelleFeld("T1001_KA_Charge_Min", 11)               'Artikel/Rohstoff-MinimalCharge
    Public T1001_ChargeMax As New wb_SchnittstelleFeld("T1001_KA_Charge_Max", 12)               'Artikel/Rohstoff-MaximalChargen
    Public T1001_RSChange As New wb_SchnittstelleFeld("T1001_KA_RS_veraenderbar", 13)           'Artikel/Rohstoff Rezeptschritt veränderbar
    Public T1001_RSMenge As New wb_SchnittstelleFeld("T1001_KA_zaehlt_zu_RZ_Gesamtmenge", 14)   'Artikel/Rohstoff zählt zur Rezept-Gesamt-Menge
    Public T1001_WaermeKap As New wb_SchnittstelleFeld("T1001_KA_Spez_WKap", 15)                'Artikel/Rohstoff-Wärmekapazität
    Public T1001_Alternativ As New wb_SchnittstelleFeld("T1001_KA_alternativ_RS", 16)           'Artikel/Rohstoff Alternativ-Artikel/Rohstoff
    Public T1001_VHinweis As New wb_SchnittstelleFeld("T1001_KA_Verarbeitungshinweise", 17)     'Artikel/Rohstoff Verarbeitungshinweise
    Public T1001_Aktiv As New wb_SchnittstelleFeld("T1001_KA_aktiv", 18)                        'Artikel/Rohstoff aktiv
    Public T1001_Preis As New wb_SchnittstelleFeld("T1001_KA_Preis", 19)                        'Artikel/Rohstoff Preis
    Public T1001_Gruppe1 As New wb_SchnittstelleFeld("T1001_KA_Grp1", 20)                       'Artikel/Rohstoff Gruppe 1
    Public T1001_Gruppe2 As New wb_SchnittstelleFeld("T1001_KA_Grp2", 21)                       'Artikel/Rohstoff Gruppe 2
    Public T1001_GebGroesse As New wb_SchnittstelleFeld("T1001_KA_GebGroesse", 22)              'Artikel/Rohstoff Gebinde-Größe
    Public T1001_TolProzent As New wb_SchnittstelleFeld("T1001_KA_TolProzent", 23)              'Artikel/Rohstoff Toleranz in Prozent
    Public T1001_TolGramm As New wb_SchnittstelleFeld("T1001_KA_TolGramm", 24)                  'Artikel/Rohstoff Toleranz in Gramm
    Public T1001_KO_Nr As New wb_SchnittstelleFeld("T1001_KO_Nr", 25)                           'Artikel/Rohstoff interne Nummer
    Public T1001_MatchCode As New wb_SchnittstelleFeld("T1001_KA_Matchcode", 26)                'Artikel/Rohstoff Matchcode

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides ReadOnly Property TabName As String
        Get
            Return "T1001"
        End Get
    End Property

    Public Overrides ReadOnly Property sql As String
        Get
            If OptionArtikel Then
                Return wb_sql_Selects.sqlT1001A
            Else
                If OptionSauerteig Then
                    Return wb_sql_Selects.sqlT1001R_ST
                Else
                    Return wb_sql_Selects.sqlT1001R
                End If
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property sqlCount As String
        Get
            If OptionArtikel Then
                Return wb_sql_Selects.sqlT1001A_Count
            Else
                If OptionSauerteig Then
                    Return wb_sql_Selects.sqlT1001R_ST_Count
                Else
                    Return wb_sql_Selects.sqlT1001R_Count
                End If
            End If
        End Get
    End Property

    Public Property OptionArtikel As Boolean
        Get
            Return _OptionArtikel
        End Get
        Set(value As Boolean)
            _OptionArtikel = value
        End Set
    End Property

    Public Property OptionRohstoff As Boolean
        Get
            Return _OptionRohstoff
        End Get
        Set(value As Boolean)
            _OptionRohstoff = value
        End Set
    End Property

    Public Property OptionSauerteig As Boolean
        Get
            Return _OptionSauerteig
        End Get
        Set(value As Boolean)
            _OptionSauerteig = value
        End Set
    End Property

    Public Overrides Sub ImportWorker(winback As wb_Sql)
        'Log-Ausgabe
        If wb_Schnittstelle_Shared.DebugMode_Max Then
            Trace.WriteLine("Artikel/Rohstoff-Nummer                    " & T1001_Nummer.ToString)
            Trace.WriteLine("Artikel/Rohstoff-Bezeichnung               " & T1001_Name.ToString)
            Trace.WriteLine("Artikel/Rohstoff-Kommentar                 " & T1001_Kommentar.ToString)
            Trace.WriteLine("Artikel/Rohstoff-Kurzname                  " & T1001_KurzName.ToString)
            Trace.WriteLine("Artikel/Rohstoff-Type                      " & T1001_Type.ToString)
            Trace.WriteLine("Artikel/Rohstoff-Rezeptur-Nummer           " & T1001_RezeptNummer.ToString)
            Trace.WriteLine("Artikel/Rohstoff-Lagerort                  " & T1001_Lagerort.ToString)
            Trace.WriteLine("Artikel/Rohstoff-Stückgewicht              " & T1001_StkGewicht.ToString)
            Trace.WriteLine("Artikel/Rohstoff-OptimalCharge             " & T1001_ChargeOpt.ToString)
            Trace.WriteLine("Artikel/Rohstoff-MinimalCharge             " & T1001_ChargeMin.ToString)
            Trace.WriteLine("Artikel/Rohstoff-MaximalCharge             " & T1001_ChargeMax.ToString)
            Trace.WriteLine("Artikel/Rohstoff Rezeptschritt veränderbar " & T1001_RSChange.ToString)
            Trace.WriteLine("Artikel/Rohstoff zählt z.RezeptGesamtMenge " & T1001_RSMenge.ToString)
            Trace.WriteLine("Artikel/Rohstoff Wärmekapazität            " & T1001_WaermeKap.ToString)
            Trace.WriteLine("Artikel/Rohstoff Alternativ-Rohstoff       " & T1001_Alternativ.ToString)
            Trace.WriteLine("Artikel/Rohstoff Verarbeitungshinweise     " & T1001_VHinweis.ToString)
            Trace.WriteLine("Artikel/Rohstoff aktiv                     " & T1001_Aktiv.ToString)
            Trace.WriteLine("Artikel/Rohstoff Preis                     " & T1001_Preis.ToString)
            Trace.WriteLine("Artikel/Rohstoff Gruppe 1                  " & T1001_Gruppe1.ToString)
            Trace.WriteLine("Artikel/Rohstoff Gruppe 2                  " & T1001_Gruppe2.ToString)
            Trace.WriteLine("Artikel/Rohstoff Gebinde-Größe             " & T1001_GebGroesse.ToString)
            Trace.WriteLine("Artikel/Rohstoff Toleranz in Prozent       " & T1001_TolProzent.ToString)
            Trace.WriteLine("Artikel/Rohstoff Toleranz in Gramm         " & T1001_TolGramm.ToString)
            Trace.WriteLine("Artikel/Rohstoff interne Nummer            " & T1001_KO_Nr.ToString)
            Trace.WriteLine("Artikel/Rohstoff Matchcode                 " & T1001_MatchCode.ToString)
        End If

        'Artikel/Rohstoff-Type MUSS angegeben sein
        If T1001_Type.Value <> "" Then
            'Artikel/Rohstoff neu anlegen oder updaten
            If sqlExists(winback) Then
                'Artikel/Rohstoff existiert schon - Daten aus Schnittstelle eintragen
                sqlUpdate(winback)
            Else
                'Artikel/Rohstoff neu anlegen
                sqlInsert(winback)
                'Daten aus Schnittstelle eintragen
                sqlUpdate(winback)
            End If
        Else
            Trace.WriteLine("@E_Keine Rohstoff-Type in der Import-Datei angegeben !")
        End If

    End Sub

    ''' <summary>
    ''' Prüft ob ein Artikel/Rohstoff mit dieser Nummer/Lagerort schon vorhanden ist.
    ''' Gibt den Index im Datenfeld T1001_KO_Nr zurück
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <returns></returns>
    Private Function sqlExists(winback As wb_Sql) As Boolean
        'abhängig von der Komponenten-Type
        Select Case wb_Functions.IntToKomponType(T1001_Type.Value)

            Case wb_Global.KomponTypen.KO_TYPE_ARTIKEL, wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE
                'Suche nach Artikel/Rohstoff-Nummer
                Return sqlExists(winback, wb_sql_Selects.setParams(wb_sql_Selects.sqlT1001_ExistsType, T1001_Nummer.Value, T1001_Type.Value))

            Case wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE, wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE
                'Suche nach Lagerort
                If sqlExists(winback, wb_sql_Selects.setParams(wb_sql_Selects.sqlT1001_ExistsLger, T1001_Lagerort.Value)) Then
                    Return True
                Else
                    'wenn kein Lagerort vorhanden/gefunden - Suche nach Rohstoff-Nummer
                    Return sqlExists(winback, wb_sql_Selects.setParams(wb_sql_Selects.sqlT1001_ExistsNumr, T1001_Nummer.Value))
                End If

            Case Else
                Trace.WriteLine("@E_unbekannte Komponenten-Type in der Import-Datei !")
                Return False
        End Select
    End Function

    Private Function sqlExists(winback As wb_Sql, sql As String) As Boolean
        'Result vorbelegen
        Dim Result As Boolean = False
        'Datensätze aus Tabelle Rezepte lesen
        If winback.sqlSelect(sql) AndAlso winback.Read Then
            'Index aus Datenbank lesen
            T1001_KO_Nr.Value = winback.iField("KO_Nr")
            Result = True
        End If
        'Datenbank schliessen
        winback.CloseRead()
        Return Result
    End Function

    Private Function sqlInsert(winback As wb_Sql) As Boolean
        'NEUE interne Komponenten-Nummer ermitteln aus max(KO_NR)
        T1001_KO_Nr.Value = wb_sql_Functions.getNewKomponNummer()
        'Datensatz neu anlegen
        winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlAddNewKompon, T1001_KO_Nr.Value, T1001_Nummer.Value, T1001_Type.Value, T1001_Name.Value))

        'Rohstoff-Handkomponente - Lagerort neu anlegen
        If wb_Functions.IntToKomponType(T1001_Type.Value) = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE Then

            'Datensatz in winback.Lagerorte anlegen
            T1001_Lagerort.Value = "KT102_" & T1001_KO_Nr.Value.ToString
            winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlInsertLagerOrte, T1001_Lagerort.Value))

            'Datensatz in winback.KomponParams anlegen
            Dim ktTypXXX As New wb_KomponParamXXX
            ktTypXXX.Wert(wb_Global.T102_TolMinus) = "0,020"
            ktTypXXX.Wert(wb_Global.T102_TolPlus) = "0,020"
            ktTypXXX.MySQLdbUpdate(T1001_KO_Nr.Value, wb_Global.T102_SollMenge, winback)
            ktTypXXX.MySQLdbUpdate(T1001_KO_Nr.Value, wb_Global.T102_SollProzent, winback)
            ktTypXXX.MySQLdbUpdate(T1001_KO_Nr.Value, wb_Global.T102_TolMinus, winback)
            ktTypXXX.MySQLdbUpdate(T1001_KO_Nr.Value, wb_Global.T102_TolPlus, winback)
            ktTypXXX.MySQLdbUpdate(T1001_KO_Nr.Value, wb_Global.T102_TolProzent, winback)
        End If
        Return True
    End Function

    Private Function sqlUpdate(winback As wb_Sql) As Boolean
        'TODO Hier muss der Datensatz in der WinBack-Tabelle aktualisiert werden
    End Function

    ''' <summary>
    ''' In BuildExportString werden alle Datenfelder aus der Datenbank-Tabelle abhängig vom DBIndex (aus der Schnittstellen-Konfig)
    ''' in die jeweiligen Felder einsortiert. 
    ''' Anschliessend werden die Felder nacheinander (sortiert nach Idx) ausgegeben und in die Export-Datei geschrieben
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <param name="Writer"></param>
    Public Overrides Sub ExportWorker(ByRef sqlReader As MySqlDataReader, Writer As System.IO.StreamWriter)
        'Datensatz in Export-File schreiben
        BuildExportString("T1001", sqlReader, Writer)
    End Sub
End Class
