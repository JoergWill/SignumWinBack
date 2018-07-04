Imports WinBack.wb_Sql_Selects

''' <summary>
''' Erzeugt eine Liste aller Rezepturen die eine Komponente X enthalten (Verwendung Komponente)
''' Alle Elemente dieser Liste werden nacheinander neu berechnet (Rezept.Rezeptschritte)
''' Mit der Verwendung der Rezepte in Artikeln werden alle verknüpften Artikel(Komponenten) aktualisiert
''' </summary>
Public Class wb_nwtUpdateAtikel
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
    Public Function UpdateNext(KompNr As Integer, Optional bUpdateOrgaBack As Boolean = False) As Boolean
        'Aktuelle Komponenten-Nummer setzen
        AktKO_Nr = KompNr
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        UpdateNext = False

        'nächsten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(setParams(sqlUpdateArtikelNWT, AktKO_Nr.ToString, wb_Global.ArtikelMarker.nwtUpdate)) Then
            'Lesen KO-Nr
            If winback.Read Then
                'Komponenten-Objekt nimmt die aktuellen Daten auf
                Dim nwtArtikelDaten As New wb_Komponente

                'nächste KO_Nr aus DB in Object nwtDaten einlesen
                nwtArtikelDaten.MySQLdbRead(winback.MySqlRead)
                winback.CloseRead()
                'aktuelle Komponenten-Nummer
                _AktKO_Nr = nwtArtikelDaten.Nr

                'verknüpfte Rezeptnummer zum Artikel/Rohstoff
                _AktRZ_Nr = nwtArtikelDaten.RzNr
                'Rezept mit allen Rezeptschritten lesen
                Dim Rzpt As New wb_Rezept(_AktRZ_Nr, Nothing)

                'Änderungs-Log löschen
                nwtArtikelDaten.ClearReport()

                'Nährwert-Information berechnen
                nwtArtikelDaten.ktTyp301 = Rzpt.RootRezeptSchritt.ktTyp301
                Debug.Print("reCalcRezept (" & _AktRZ_Nr & ") " & Rzpt.RezeptNummer & " " & Rzpt.RezeptBezeichnung & " kt301(Kilokalorien) " & nwtArtikelDaten.ktTyp301.Naehrwert(wb_Global.T301_Kilokalorien))
                'Zutatenliste erzeugen
                nwtArtikelDaten.Deklaration = wb_sql_Functions.removeSonderZeichen(Rzpt.ZutatenListe(wb_Global.ZutatenListeMode.Show_ENummer))
                Debug.Print("Zutatenliste " & nwtArtikelDaten.Deklaration)

                'Änderungen der Nährwerte in Komponente(Rohstoff) sichern
                nwtArtikelDaten.MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
                'Änderungen der Zutatenliste in Komponente(Rohstoff) sichern
                nwtArtikelDaten.MySqldbUpdate_Zutatenliste()

                'Änderungen der Komponenten-Parameter(Rohstoff) in OrgaBack-DB schreiben
                'Gibt true zurück, wenn der Artikel in OrgaBack existiert
                If nwtArtikelDaten.MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301) Then
                    'Zutaten-und Allergenliste in OrgaBack updaten
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
                            nwtArtikelDaten.Deklaration = nwtArtikelDaten.Deklaration

                            'Daten sichern in Mysql
                            nwtArtikel.MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
                            'Änderungen der Zutatenliste in Komponente(Rohstoff) sichern
                            nwtArtikelDaten.MySqldbUpdate_Zutatenliste()

                            'Daten sichern in MsSQL
                            'Gibt true zurück, wenn der Artikel in OrgaBack existiert
                            If nwtArtikelDaten.MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301) Then
                                'Zutaten-und Allergenliste in OrgaBack updaten
                                nwtArtikelDaten.MsSqldbUpdate_Zutatenliste()
                            End If
                        End If
                    Next
                End If

                'Protokoll der Änderungen speichern in Hinweise
                nwtArtikelDaten.SaveReport()
                'Protokoll der Änderungen ausgeben
                Debug.Print(nwtArtikelDaten.GetReport)

                'Ausgabe-Text
                _InfoText = "<" & nwtArtikelDaten.Nr.ToString("00000") & "> " & nwtArtikelDaten.Bezeichnung
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