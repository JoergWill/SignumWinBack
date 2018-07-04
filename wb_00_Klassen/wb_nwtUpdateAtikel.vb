Imports WinBack.wb_Sql_Selects

''' <summary>
''' Erzeugt eine Liste aller Rezepturen die eine Komponente X enthalten (Verwendung Komponente)
''' Alle Elemente dieser Liste werden nacheinander neu berechnet (Rezept.Rezeptschritte)
''' Mit der Verwendung der Rezepte in Artikeln werden alle verknüpften Artikel(Komponenten) aktualisiert
''' </summary>
Public Class wb_nwtUpdateAtikel

    Public ListeRezeptNr As New List(Of Integer)

    Public kt301 As wb_KomponParam301                   'Public für Unit-Test
    Public ztListe As String                            'Public für Unit-Test

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
    ''' Sucht die nächst folgende Komponente (Artikel/Rohstoff mit Rezeptur) sortiert nach KO_Nr 
    ''' aus der Datenbank. Dabei werden nur Datensätze berücksichtigt, die als Update markiert sind.
    ''' Einlesen der Komponenten-Daten in ein Komponenten-Objekt
    ''' Berechnen der Nährwerte der Rezeptur
    ''' 
    ''' Nachdem Nährwerte und Zutatenliste berechnet sind
    ''' </summary>
    ''' <returns>True wenn der Datensatz aktualisiert wurde</returns>
    Public Function UpdateNext(KompNr As Integer, Optional bUpdateOrgaBack As Boolean = False) As Boolean
        'Aktuelle Komponenten-Nummer setzen
        AktKO_Nr = KompNr
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Komponenten-Objekt nimmt die aktuellen Daten auf
        Dim nwtArtikelDaten As New wb_Komponente
        UpdateNext = False

        'nächsten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(setParams(sqlUpdateArtikelNWT, AktKO_Nr.ToString)) Then
            'Lesen KO-Nr
            If winback.Read Then
                'nächste KO_Nr aus DB in Object nwtDaten einlesen
                nwtArtikelDaten.MySQLdbRead(winback.MySqlRead)
                winback.CloseRead()
                AktKO_Nr = nwtArtikelDaten.Nr

                'Stammdaten und Nährwerte aus DB in (Object)nwtDaten einlesen
                If winback.sqlSelect(setParams(sqlgetNWT, AktKO_Nr.ToString, wb_Global.ArtikelMarker.nwtUpdate)) Then
                    'Lesen KO-Nr
                    If winback.Read Then
                        nwtArtikelDaten.MySQLdbRead(winback.MySqlRead)
                    End If
                End If

                'Änderungs-Log löschen
                nwtArtikelDaten.ClearReport()




                ''Änderungen der Nährwerte in Komponente(Rohstoff) sichern
                'nwtArtikelDaten.MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)

                ''Änderungen der Komponenten-Parameter(Rohstoff) in OrgaBack-DB schreiben
                ''Gibt true zurück, wenn der Artikel in OrgaBack existiert
                'If nwtArtikelDaten.MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301) Then
                '    'Zutaten-und Allergenliste in OrgaBack updaten
                '    nwtArtikelDaten.MsSqldbUpdate_Zutatenliste()
                'End If

                'Protokoll der Änderungen speichern in Hinweise
                nwtArtikelDaten.SaveReport()
                'Protokoll der Änderungen ausgeben
                Debug.Print(nwtArtikelDaten.GetReport)

                'Ausgabe-Text
                _InfoText = "(" & nwtArtikelDaten.Nr.ToString("00000") & ") " & nwtArtikelDaten.Bezeichnung
                UpdateNext = True
            Else
                'EOF() - ReStart bei KO_Nr = 0
                AktKO_Nr = 0
                _InfoText = ""
            End If
        End If

        winback.Close()
        Return UpdateNext
    End Function















    Public Function getRezepteZumRohstoff(KoNr As Integer) As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim RzNr As Integer
        ListeRezeptNr.Clear()

        'alle Datensätze aus Tabelle Rezeptschritte lesen
        If winback.sqlSelect(setParams(sqlKompInRZSchritte, KoNr.ToString)) Then
            While winback.MySqlRead.Read
                RzNr = wb_Functions.StrToInt(winback.MySqlRead.GetValue(0).ToString)
                If RzNr > 0 Then
                    ListeRezeptNr.Add(RzNr)
                End If
            End While
        End If
        'Anzahl der Datensätze
        Return ListeRezeptNr.Count
    End Function

    'TODO als Funktion schreiben und Nährwertinfo unt ZTListe in Struktur zurückgeben !!
    Public Sub reCalcRezeptListe()
        'Liste aller Artikel mit Verweis auf diese Artikelnummer
        Dim ListeArtikelNr As New List(Of Integer)

        For Each RzNr In ListeRezeptNr
            ListeArtikelNr = reCalcRezept(RzNr)
            'alle Artikel mit Verweis auf dieses Rezept erhalten diese gerade berechneten Nährwerte und Zutatenliste
            If ListeArtikelNr.Count > 0 Then
                For Each ArtNr In ListeArtikelNr
                    ReCalcArtikel(ArtNr)
                Next
            End If
        Next
    End Sub

    'TODO als Funktion schreiben und Nährwertinfo unt ZTListe in Struktur zurückgeben !!
    Public Function reCalcRezept(RzNr As Integer) As IList
        'Rezept mit allen Rezeptschritten lesen
        Dim Rzpt As New wb_Rezept(RzNr, Nothing)

        'Nährwert-Information berechnen
        kt301 = Rzpt.RootRezeptSchritt.ktTyp301
        Debug.Print("reCalcRezept (" & RzNr & ") " & Rzpt.RezeptNummer & " " & Rzpt.RezeptBezeichnung & " kt301(Kilokalorien) " & kt301.Naehrwert(wb_Global.T301_Kilokalorien))
        'Zutatenliste erzeugen
        ztListe = Rzpt.ZutatenListe(wb_Global.ZutatenListeMode.Show_ENummer)
        Debug.Print("Zutatenliste " & ztListe)

        'Liste aller Artikel, die dieses Rezept verwenden
        Return Rzpt.ArtikelVerwendung()
    End Function

    Public Sub ReCalcArtikel(KoNr As Integer)
        Debug.Print("ReCalc Artikel " & KoNr)
    End Sub
End Class
