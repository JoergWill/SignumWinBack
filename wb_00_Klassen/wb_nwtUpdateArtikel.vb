Imports WinBack.wb_Sql_Selects

''' <summary>
''' Erzeugt eine Liste aller Rezepturen die eine Komponente X enthalten (Verwendung Komponente)
''' Alle Elemente dieser Liste werden nacheinander neu berechnet (Rezept.Rezeptschritte)
''' Mit der Verwendung der Rezepte in Artikeln werden alle verknüpften Artikel(Komponenten) aktualisiert
''' </summary>
Public Class wb_nwtUpdateArtikel
    Implements IDisposable
    Protected disposed As Boolean = False

    Private _InfoText As String = ""
    Private _AktKO_Nr As Integer = 0
    Private _AktRZ_Nr As Integer = 0

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
    Public Function UpdateNext(KompNr As Integer, Optional ByRef bUpdateOrgaBack As Boolean = False) As Boolean
        'Aktuelle Komponenten-Nummer setzen
        AktKO_Nr = KompNr
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        UpdateNext = False

        'Filter setzen (nur markierte Artikel oder alle)
        Dim Filter As String = wb_Global.ArtikelMarker.nwtUpdate
        If bUpdateOrgaBack Then
            Filter = "0"
        End If

        'nächsten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(setParams(sqlUpdateArtikelNWT, AktKO_Nr.ToString, Filter)) Then
            'Lesen KO-Nr
            If winback.Read Then
                'Komponenten-Objekt nimmt die aktuellen Daten auf
                Dim nwtArtikelDaten As New wb_Komponente

                'nächste KO_Nr aus DB in Object nwtDaten einlesen
                nwtArtikelDaten.MySQLdbRead(winback.MySqlRead)
                winback.CloseRead()
                'aktuelle Komponenten-Nummer
                _AktKO_Nr = nwtArtikelDaten.Nr

                'Artikel-Daten komplett (inkusive Parameter) einlesen
                nwtArtikelDaten.MySQLdbRead(_AktKO_Nr)
                'verknüpfte Rezeptnummer zum Artikel/Rohstoff
                _AktRZ_Nr = nwtArtikelDaten.RzNr
                'Rezept mit allen Rezeptschritten lesen (NoMessage=True unterdrückt die Meldung "Rezept verweist auf sich selbst")
                Dim Rzpt As New wb_Rezept(_AktRZ_Nr, Nothing, nwtArtikelDaten.Backverlust, 1, "", "", True)

                'Änderungs-Log löschen
                nwtArtikelDaten.ClearReport()

                'Nährwert-Information berechnen
                nwtArtikelDaten.ktTyp301 = Rzpt.KtTyp301
                Debug.Print("reCalcRezept (" & _AktRZ_Nr & ") " & Rzpt.RezeptNummer & " " & Rzpt.RezeptBezeichnung & " kt301(Kilokalorien) " & nwtArtikelDaten.ktTyp301.Naehrwert(wb_Global.T301_Kilokalorien))
                'Zutatenliste erzeugen
                nwtArtikelDaten.Deklaration = wb_Functions.XRemoveSonderZeichen(Rzpt.ZutatenListe(wb_Global.ZutatenListeMode.Show_ENummer), True)
                Debug.Print("Zutatenliste " & nwtArtikelDaten.Deklaration)
                'Mehlzusammensetzung berechnen
                nwtArtikelDaten.Mehlzusammensetzung = Rzpt.MehlZusammensetzung(wb_Global.TrennzMehlAnteil)

                'Änderungen der Nährwerte in Komponente(Rohstoff) sichern
                Debug.Print("Update (Artikel)Nährwerte in WinBack " & nwtArtikelDaten.Nummer & " " & nwtArtikelDaten.Bezeichnung)
                nwtArtikelDaten.MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
                'Änderungen der Zutatenliste in Komponente(Rohstoff) sichern
                Debug.Print("Update (Artikel)Zutaten in WinBack " & nwtArtikelDaten.Nummer & " " & nwtArtikelDaten.Bezeichnung)
                nwtArtikelDaten.MySqldbUpdate_Zutatenliste()
                'Markierung Artikel-Nährwerte müssen aktualisiert werden wieder entfernen
                nwtArtikelDaten.MySQLdbSetMarker(wb_Global.ArtikelMarker.nwtOK)

                'Änderungen der Komponenten-Parameter(Rohstoff) in OrgaBack-DB schreiben
                'Gibt true zurück, wenn der Artikel in OrgaBack existiert
                Debug.Print("Update (Artikel)Nährwerte in OrgaBack " & nwtArtikelDaten.Nummer & " " & nwtArtikelDaten.Bezeichnung)
                If nwtArtikelDaten.MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301) Then
                    'Zutaten-und Allergenliste in OrgaBack updaten
                    Debug.Print("Update (Artikel)Zutaten in OrgaBack " & nwtArtikelDaten.Nummer & " " & nwtArtikelDaten.Bezeichnung)
                    nwtArtikelDaten.MsSqldbUpdate_Zutatenliste()
                End If

                'Liste aller Artikel, die dieses Rezept verwenden
                Dim ArtikelListe As List(Of Integer) = Rzpt.ArtikelVerwendung()
                'wenn noch weitere Artikel dieses Rezept verwenden werden diese auch gleich verarbeitet
                If ArtikelListe.Count > 1 Then
                    For Each ArtikelNr As Integer In ArtikelListe
                        'der aktuelle Artikel ist schon bearbeitet
                        If ArtikelNr <> _AktKO_Nr Then
                            'Komponenten-Objekt
                            Dim nwtArtikel As New wb_Komponente
                            'Daten aus Winback-Db lesen
                            nwtArtikel.MySQLdbRead(ArtikelNr)

                            'Nährwerte und Allergene aktualisieren
                            nwtArtikel.ktTyp301 = nwtArtikelDaten.ktTyp301
                            'ZutatenListe aktualisieren
                            nwtArtikel.Deklaration = nwtArtikelDaten.Deklaration
                            'Mehlzusammensetzung aktualisieren
                            nwtArtikel.Mehlzusammensetzung = nwtArtikelDaten.Mehlzusammensetzung

                            'Daten sichern in Mysql
                            Debug.Print("Update (weitere Artikel)Nährwerte in WinBack " & nwtArtikel.Nummer & " " & nwtArtikel.Bezeichnung)
                            nwtArtikel.MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
                            'Änderungen der Zutatenliste in Komponente(Rohstoff) sichern
                            Debug.Print("Update (weitere Artikel)Zutaten in WinBack " & nwtArtikel.Nummer & " " & nwtArtikel.Bezeichnung)
                            nwtArtikel.MySqldbUpdate_Zutatenliste()
                            'Markierung Artikel-Nährwerte müssen aktualisiert werden wieder entfernen
                            nwtArtikel.MySQLdbSetMarker(wb_Global.ArtikelMarker.nwtOK)

                            'Daten sichern in MsSQL
                            'Gibt true zurück, wenn der Artikel in OrgaBack existiert
                            Debug.Print("Update (weitere Artikel)Nährwerte in OrgaBack " & nwtArtikel.Nummer & " " & nwtArtikel.Bezeichnung)
                            If nwtArtikel.MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301) Then
                                'Zutaten-und Allergenliste in OrgaBack updaten
                                Debug.Print("Update (weitere Artikel)Zutaten in OrgaBack " & nwtArtikel.Nummer & " " & nwtArtikel.Bezeichnung)
                                nwtArtikel.MsSqldbUpdate_Zutatenliste()
                            End If
                        End If
                    Next
                End If

                'alle Artikel markieren, die einen Verweis auf dieses Rezept (enthält die geänderte Komponente) enthalten (Rezept-im-Rezept)
                nwtArtikelDaten.MySQLdbSetMarkerRzptListe(wb_Global.ArtikelMarker.nwtUpdate)

                'Protokoll der Änderungen speichern in Hinweise
                nwtArtikelDaten.SaveReport()
                'Protokoll der Änderungen ausgeben
                'Debug.Print("Report " & nwtArtikelDaten.GetReport)

                'Ausgabe-Text
                _InfoText = "<" & nwtArtikelDaten.Nr.ToString("000000") & "> " & nwtArtikelDaten.Bezeichnung
                UpdateNext = True
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