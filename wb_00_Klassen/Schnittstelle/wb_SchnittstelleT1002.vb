Imports MySql.Data.MySqlClient

Public Class wb_SchnittstelleT1002
    Inherits wb_SchnittstelleTabelle

    Private _OptionArtikel As Boolean
    Private _OptionRohstoff As Boolean
    Private _LastKompNr As Integer = wb_Global.UNDEFINED

    Public T1002_Nummer As New wb_SchnittstelleFeld("T1002_KO_Nr_AlNum", 1)             'Artikel/Rohstoff-Nummer
    Public T1002_Name As New wb_SchnittstelleFeld("T1002_KO_Bezeichnung", 3)            'Artikel/Rohstoff-Bezeichnung

    Public T1002_Zutaten As New wb_SchnittstelleFeld("T1002_Zutaten", 4)                'Artikel/Rohstoff-Zutatenliste

    Public T1002_Kcal As New wb_SchnittstelleFeld("T1002_KiloKalorien", 5)              'Artikel/Rohstoff-Nährwerte Kilokalorien
    Public T1002_KJoule As New wb_SchnittstelleFeld("T1002_KiloJoule", 6)               'Artikel/Rohstoff-Nährwerte KiloJoule
    Public T1002_Proteine As New wb_SchnittstelleFeld("T1002_Proteine", 7)              'Artikel/Rohstoff-Nährwerte Proteine
    Public T1002_Khydrate As New wb_SchnittstelleFeld("T1002_Kohlenhydrate", 8)         'Artikel/Rohstoff-Nährwerte Kohlenhydrate
    Public T1002_Fette As New wb_SchnittstelleFeld("T1002_Fette", 9)                    'Artikel/Rohstoff-Nährwerte Fette
    Public T1002_Zucker As New wb_SchnittstelleFeld("T1002_Zucker", 10)                 'Artikel/Rohstoff-Nährwerte Zucker
    Public T1002_GesFette As New wb_SchnittstelleFeld("T1002_GesFette", 11)             'Artikel/Rohstoff-Nährwerte gesättigte Fettsäuren
    Public T1002_Ballast As New wb_SchnittstelleFeld("T1002_BallastStoffe", 12)         'Artikel/Rohstoff-Nährwerte Ballaststoffe
    Public T1002_Natrium As New wb_SchnittstelleFeld("T1002_Natrium", 13)               'Artikel/Rohstoff-Nährwerte Natrium
    Public T1002_Alkohol As New wb_SchnittstelleFeld("T1002_Alkohol", 14)               'Artikel/Rohstoff-Nährwerte Alkohol

    Public T1002_Gluten As New wb_SchnittstelleFeld("T1002_Gluten", 15)                 'Artikel/Rohstoff-Allergene Gluten
    Public T1002_Krebstiere As New wb_SchnittstelleFeld("T1002_Krebstiere", 16)         'Artikel/Rohstoff-Allergene Krebstiere
    Public T1002_Eier As New wb_SchnittstelleFeld("T1002_Eier", 17)                     'Artikel/Rohstoff-Allergene Eier
    Public T1002_Fische As New wb_SchnittstelleFeld("T1002_Fische", 18)                 'Artikel/Rohstoff-Allergene Fische
    Public T1002_Erdnuesse As New wb_SchnittstelleFeld("T1002_Erdnuesse", 19)           'Artikel/Rohstoff-Allergene Erdnüsse
    Public T1002_Soja As New wb_SchnittstelleFeld("T1002_Soja", 20)                     'Artikel/Rohstoff-Allergene Soja
    Public T1002_Milch As New wb_SchnittstelleFeld("T1002_Milch", 21)                   'Artikel/Rohstoff-Allergene Milch
    Public T1002_Schalen As New wb_SchnittstelleFeld("T1002_Schalenfruechte", 22)       'Artikel/Rohstoff-Allergene Schalenfrüchte
    Public T1002_Sellerie As New wb_SchnittstelleFeld("T1002_Sellerie", 23)             'Artikel/Rohstoff-Allergene Sellerie
    Public T1002_Senf As New wb_SchnittstelleFeld("T1002_Senf", 24)                     'Artikel/Rohstoff-Allergene Senf
    Public T1002_Sesam As New wb_SchnittstelleFeld("T1002_Sesam", 25)                   'Artikel/Rohstoff-Allergene Sesam
    Public T1002_Sulfite As New wb_SchnittstelleFeld("T1002_Sulfite", 26)               'Artikel/Rohstoff-Allergene Sulfite
    Public T1002_Lupinen As New wb_SchnittstelleFeld("T1002_Lupinen", 27)               'Artikel/Rohstoff-Allergene Lupinen
    Public T1002_Weichtiere As New wb_SchnittstelleFeld("T1002_Weichtiere", 28)         'Artikel/Rohstoff-Allergene Weichtiere
    Public T1002_Weizen As New wb_SchnittstelleFeld("T1002_Weizen", 29)                 'Artikel/Rohstoff-Allergene Weizen
    Public T1002_Roggen As New wb_SchnittstelleFeld("T1002_Roggen", 30)                 'Artikel/Rohstoff-Allergene Roggen
    Public T1002_Gerste As New wb_SchnittstelleFeld("T1002_Gerste", 31)                 'Artikel/Rohstoff-Allergene Gerste
    Public T1002_Dinkel As New wb_SchnittstelleFeld("T1002_Dinkel", 32)                 'Artikel/Rohstoff-Allergene Dinkel
    Public T1002_Kamut As New wb_SchnittstelleFeld("T1002_Kamut", 33)                   'Artikel/Rohstoff-Allergene Kamut
    Public T1002_Hafer As New wb_SchnittstelleFeld("T1002_Hafer", 34)                   'Artikel/Rohstoff-Allergene Hafer
    Public T1002_Emmer As New wb_SchnittstelleFeld("T1002_Emmer", 35)                   'Artikel/Rohstoff-Allergene Emmer
    Public T1002_Einkorn As New wb_SchnittstelleFeld("T1002_Einkorn", 36)               'Artikel/Rohstoff-Allergene Einkorn
    Public T1002_Mandeln As New wb_SchnittstelleFeld("T1002_Mandeln", 37)               'Artikel/Rohstoff-Allergene Mandeln
    Public T1002_Haselnuesse As New wb_SchnittstelleFeld("T1002_Haselnuesse", 38)       'Artikel/Rohstoff-Allergene Haselnüsse
    Public T1002_Walnuesse As New wb_SchnittstelleFeld("T1002_Walnuesse", 39)           'Artikel/Rohstoff-Allergene Walnüsse
    Public T1002_Kaschunuesse As New wb_SchnittstelleFeld("T1002_Kaschunuesse", 40)     'Artikel/Rohstoff-Allergene Kaschunüsse
    Public T1002_Pekannuesse As New wb_SchnittstelleFeld("T1002_Pekanuesse", 41)        'Artikel/Rohstoff-Allergene Pekannüsse
    Public T1002_Paranuesse As New wb_SchnittstelleFeld("T1002_Paranuesse", 42)         'Artikel/Rohstoff-Allergene Paranüsse
    Public T1002_Pistazien As New wb_SchnittstelleFeld("T1002_Pistazien", 43)           'Artikel/Rohstoff-Allergene Pistazien
    Public T1002_Makadamia As New wb_SchnittstelleFeld("T1002_Makadamianuesse", 44)     'Artikel/Rohstoff-Allergene Makadamianüsse

    Private Const cKompNr As Integer = 0
    Private Const cRPTypNr As Integer = 36
    Private Const cRPParamNr As Integer = 37
    Private Const cRPParamWert As Integer = 38

    Private Const cZutaten As Integer = 1
    Private Const cMehl As Integer = 2

#Disable Warning IDE0140 ' Objekterstellung kann vereinfacht werden
    Private ktTyp100 As wb_KomponProduktInfo
    Private ktTyp200 As wb_KomponParam200 = New wb_KomponParam200
    Private ktTyp301 As wb_KomponParam301 = New wb_KomponParam301
#Enable Warning IDE0140 ' Objekterstellung kann vereinfacht werden

    Private xKT100(wb_Global.maxTyp200) As String                                       'Produkt-Info
    Private xKT200(wb_Global.maxTyp200) As String                                       'Produktions-Daten
    Private xKT301(wb_Global.maxTyp301) As String                                       'Nährwerte und Allergene

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides ReadOnly Property TabName As String
        Get
            Return "T1002"
        End Get
    End Property

    Public Overrides ReadOnly Property sql As String
        Get
            If OptionArtikel Then
                Return wb_sql_Selects.sqlT1002A
            Else
                Return wb_sql_Selects.sqlT1002R
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property sqlCount As String
        Get
            If OptionArtikel Then
                Return wb_sql_Selects.sqlT1002A_Count
            Else
                Return wb_sql_Selects.sqlT1002R_Count
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

    ''' <summary>
    ''' Liefert den Index auf die Zutatenliste (Deklarationsbezeichnung) in der Tabelle xKT100
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property OptionZutatenListe As wb_Global.Hinweise
        Get
            If OptionArtikel Then
                Return wb_Global.Hinweise.ZutatenListe

                'Liefert AUCH für Artikel den Index auf die Deklarationsbezeichnung
                If wb_GlobalSettings.NwtInterneDeklaration Then
                    'Zutatenliste aus der internen Deklarataion
                    Return wb_Global.Hinweise.DeklBezRohstoffIntern
                Else
                    'Zutatenliste aus der externen Deklarataion
                    Return wb_Global.Hinweise.DeklBezRohstoff
                End If
            Else
                'Export Rohstoffe
                If wb_GlobalSettings.NwtInterneDeklaration Then
                    'Zutatenliste aus der internen Deklarataion
                    Return wb_Global.Hinweise.DeklBezRohstoffIntern
                Else
                    'Zutatenliste aus der externen Deklarataion
                    Return wb_Global.Hinweise.DeklBezRohstoff
                End If
            End If
        End Get
    End Property

    Public Overrides Sub ImportWorker(winback As wb_Sql)
    End Sub

    Public Overrides Sub ExportWorker(ByRef sqlReader As MySqlDataReader, writer As System.IO.StreamWriter)
        'Komponenten-Nummer
        Dim KompNr As Integer = sqlReader.GetValue(cKompNr)

        'Komponenten-Nummer hat sich geändert - Datensatz schreiben
        If KompNr <> _LastKompNr AndAlso _LastKompNr <> wb_Global.UNDEFINED Then
            'Produkt-Info in Array schreiben
            ExportWorker_kt100()
            'Produktions-Daten in Array schreiben
            ExportWorker_kt200()
            'Nährwert-Daten in Array schreiben - anschliessend wird das Array in das Export-File geschrieben
            ExportWorker_kt301(writer)
        End If

        'Daten aus sql in Array eintragen (nur einmalig oder beim ersten Durchlauf)
        If KompNr <> _LastKompNr OrElse _LastKompNr = wb_Global.UNDEFINED Then
            'sql-Daten in Array schreiben
            ExportWorker_sql(sqlReader)
            'Produkt-Info
            ktTyp100 = New wb_KomponProduktInfo(KompNr)
        End If

        'Spezial-Felder für ProduktionsDaten - T1002 werden im Array ktTyp200.Wert() gespeichert
        If sqlReader.GetValue(cRPTypNr) = wb_Global.ktParam.kt200 Then
            ktTyp200.Wert(sqlReader.GetValue(cRPParamNr)) = sqlReader.GetValue(cRPParamWert)
        End If

        'Spezial-Felder für Nährwerte - T1002 werden im Array ktTyp301.Wert() gespeichert
        If sqlReader.GetValue(cRPTypNr) = wb_Global.ktParam.kt301 Then
            ktTyp301.Wert(sqlReader.GetValue(cRPParamNr)) = sqlReader.GetValue(cRPParamWert)
        End If

        'Komponenten-Nummer speichern
        _LastKompNr = KompNr
    End Sub

    Private Sub ExportWorker_sql(ByRef sqlReader As MySqlDataReader)
        'sql-Datensatz in Export-File schreiben
        BuildExportString("T1002", sqlReader)
    End Sub

    Private Sub ExportWorker_kt100()
        'Deklarationsbezeichnung/Zutatenliste in KomponParam100-Array eintragen
        Select Case OptionZutatenListe
            Case wb_Global.Hinweise.ZutatenListe
                xKT100(cZutaten) = wb_Functions.XRemoveSonderZeichen_ImportExport(ktTyp100.Zutatenliste)
            Case wb_Global.Hinweise.DeklBezRohstoff
                xKT100(cZutaten) = wb_Functions.XRemoveSonderZeichen_ImportExport(ktTyp100.DeklarationsBezeichung)
            Case wb_Global.Hinweise.DeklBezRohstoffIntern
                xKT100(cZutaten) = wb_Functions.XRemoveSonderZeichen_ImportExport(ktTyp100.DeklarationsBezeichungIntern)
            Case Else
                xKT100(cZutaten) = ""
        End Select

        'Export-Felder schreiben (Sonderfunktion T1002 - Deklarationsbezeichnungen im Array X)
        BuildExportString("T1002", xKT100, "KT100")
    End Sub

    Private Sub ExportWorker_kt200()
        'ProduktionsDaten aus KomponParam200 in Array übertragen
        For i = 0 To xKT200.Length - 1
            xKT200(i) = ktTyp200.Wert(i)
        Next
        'Export-Felder schreiben (Sonderfunktion T1002 - Nährwerte im Array X)
        BuildExportString("T1002", xKT200, "KT200")
        'Array Nährwerte wieder löschen
        ktTyp200.Clear()
    End Sub
    Private Sub ExportWorker_kt301(writer As System.IO.StreamWriter)
        'Nährwerte aus KomponParam301 in Array übertragen
        For i = 0 To xKT301.Length - 1
            If wb_KomponParam301_Global.IsAllergen(i) Then
                xKT301(i) = ktTyp301.oWert(i)
            Else
                xKT301(i) = ktTyp301.Wert(i)
            End If
        Next
        'Export-Felder schreiben (Sonderfunktion T1002 - Nährwerte im Array X) - Anschliessend wird das Array in das Export-File geschrieben
        BuildExportString("T1002", xKT301, "KT301", writer)
        'Array Nährwerte wieder löschen
        ktTyp301.Clear()
    End Sub

End Class
