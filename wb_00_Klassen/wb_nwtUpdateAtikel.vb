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
