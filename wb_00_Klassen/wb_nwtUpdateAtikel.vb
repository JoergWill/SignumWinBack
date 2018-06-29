Imports WinBack.wb_Sql_Selects

''' <summary>
''' Erzeugt eine Liste aller Rezepturen die eine Komponente X enthalten (Verwendung Komponente)
''' Alle Elemente dieser Liste werden nacheinander neu berechnet (Rezept.Rezeptschritte)
''' Mit der Verwendung der Rezepte in Artikeln werden alle verknüpften Artikel(Komponenten) aktualisiert
''' </summary>
Public Class wb_nwtUpdateAtikel

    Public ListeRezeptNr As New List(Of Integer)
    Public ListeArtikelNr As New List(Of Integer)

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
        For Each RzNr In ListeRezeptNr
            reCalcRezept(RzNr)
            'alle Artikel mit Verweis auf dieses Rezept erhalten dise gerade berechneten Nährwerte und Zutatenliste
            If ListeArtikelNr.Count > 0 Then
                For Each ArtNr In ListeArtikelNr
                    ReCalcArtikel(ArtNr)
                Next
            End If
        Next
    End Sub

    'TODO als Funktion schreiben und Nährwertinfo unt ZTListe in Struktur zurückgeben !!
    Public Sub reCalcRezept(RzNr As Integer)
        'Rezept mit allen Rezeptschritten lesen
        Dim Rzpt As New wb_Rezept(RzNr, Nothing)

        'Nährwert-Information berechnen
        kt301 = Rzpt.RootRezeptSchritt.ktTyp301
        Debug.Print("reCalcRezept (" & RzNr & ") " & Rzpt.RezeptNummer & " " & Rzpt.RezeptBezeichnung & " kt301(Kilokalorien) " & kt301.Naehrwert(wb_Global.T301_Kilokalorien))
        'Zutatenliste erzeugen
        ztListe = Rzpt.ZutatenListe(wb_Global.ZutatenListeMode.Show_ENummer)
        Debug.Print("Zutatenliste " & ztListe)

        'Liste aller Artikel/Komponenten mit Verweis auf dieses Rezept
        ListeArtikelNr.Clear()
        'TODO Liste aller Artikel in Klasse Rezept schreiben
        ListeArtikelNr = Rzpt.ListeArtikelVerwendung()
    End Sub

    Public Sub ReCalcArtikel(KoNr As Integer)

    End Sub
End Class
