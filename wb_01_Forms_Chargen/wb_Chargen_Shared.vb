Imports WinBack

Public Class wb_Chargen_Shared
    Public Shared Event eListe_Click(sender As Object, StatistikType As wb_Global.StatistikType)
    Public Shared Event eListe_Print(sender As Object, StatistikType As wb_Global.StatistikType)
    Public Shared Event eDetail_DblClick(sender As Object, ChargenZeile As wb_ChargenSchritt)
    Public Shared NrListe As New List(Of Integer)
    Public Shared NrLinien As New List(Of Integer)

    Private Shared _SortKriterium As wb_Global.ChargenListeSortKriterium
    Private Shared _Filter As Boolean
    Private Shared _FilterVon As Date
    Private Shared _FilterBis As Date
    Private Shared _UhrzeitVon As Date = wb_Global.wbNODATE
    Private Shared _UhrzeitBis As Date = wb_Global.wbNODATE
    Private Shared _UhrZeitVonChecked As Boolean = False
    Private Shared _UhrZeitBisChecked As Boolean = False
    Private Shared _AlleLinien As Boolean = True
    Private Shared _WasserTempAusblenden As Boolean = False
    Private Shared _IstwertNullAusblenden As Boolean = True
    Private Shared _FensterTitel As String

    Private Shared _Liste_TagesWechselNummer As Integer
    Private Shared _Liste_KomponDoesNotCount As New Dictionary(Of Integer, Boolean)
    Private Shared _Liste_KomponDoesNotCountInit As Boolean = False

    ''' <summary>
    ''' B_ARS_Status
    '''   0  -  Nicht bearbeitet
    '''   1  -  in Bearbeitung
    '''   2  -  Okay
    '''   3  -  Warnung
    '''   4  -  Fehler bei manueller Verwiegung
    '''   5  -  Fehler bei automatischer Verwiegung
    '''   6  -  Multistart markiert
    '''   7  -  Nachtstart
    '''   8  -  Start gespeichert (Multistart)
    ''' </summary>
    Shared Sub New()
        'Liste aller Komponenten, die nicht zum Rezeptgewicht zählen
        'ReadKomponDoesNotCount()
    End Sub

    Public Shared Property SortKriterium As wb_Global.ChargenListeSortKriterium
        Get
            Return _SortKriterium
        End Get
        Set(value As wb_Global.ChargenListeSortKriterium)
            _SortKriterium = value
        End Set
    End Property

    Public Shared Property Filter As Boolean
        Get
            Return _Filter
        End Get
        Set(value As Boolean)
            _Filter = value
        End Set
    End Property

    Public Shared Property FilterVon As Date
        Get
            Return _FilterVon
        End Get
        Set(value As Date)
            _FilterVon = value
        End Set
    End Property

    Public Shared Property FilterBis As Date
        Get
            Return _FilterBis
        End Get
        Set(value As Date)
            _FilterBis = value
        End Set
    End Property

    Public Shared Property AlleLinien As Boolean
        Get
            Return _AlleLinien
        End Get
        Set(value As Boolean)
            _AlleLinien = value
        End Set
    End Property

    Public Shared Property Liste_TagesWechselNummer As Integer
        Get
            Return _Liste_TagesWechselNummer
        End Get
        Set(value As Integer)
            _Liste_TagesWechselNummer = value
        End Set
    End Property

    Public Shared Property UhrZeitVonChecked As Boolean
        Get
            Return _UhrZeitVonChecked
        End Get
        Set(value As Boolean)
            _UhrZeitVonChecked = value
        End Set
    End Property

    Public Shared Property UhrZeitBisChecked As Boolean
        Get
            Return _UhrZeitBisChecked
        End Get
        Set(value As Boolean)
            _UhrZeitBisChecked = value
        End Set
    End Property

    Public Shared Property UhrzeitVon As Date
        Get
            Return _UhrzeitVon
        End Get
        Set(value As Date)
            _UhrzeitVon = value
        End Set
    End Property

    Public Shared Property UhrzeitBis As Date
        Get
            Return _UhrzeitBis
        End Get
        Set(value As Date)
            _UhrzeitBis = value
        End Set
    End Property

    Public Shared Property WasserTempAusblenden As Boolean
        Get
            Return _WasserTempAusblenden
        End Get
        Set(value As Boolean)
            _WasserTempAusblenden = value
        End Set
    End Property

    Public Shared Property IstwertNullAusblenden As Boolean
        Get
            Return _IstwertNullAusblenden
        End Get
        Set(value As Boolean)
            _IstwertNullAusblenden = value
        End Set
    End Property

    Public Shared Property FensterTitel As String
        Get
            Return _FensterTitel
        End Get
        Set(value As String)
            _FensterTitel = value
        End Set
    End Property

    Public Shared Sub SetFensterTitel(StatistikType As wb_Global.StatistikType, Optional StrtDate As String = "", Optional EndeDate As String = "")

        Select Case StatistikType
            Case wb_Global.StatistikType.StatistikRezepte
                _FensterTitel = "Statistik Rezepte vom "
                'Filter Uhrzeit übernehmen
                _FensterTitel &= FilterVonBis()

            Case wb_Global.StatistikType.StatistikRohstoffeVerbrauch
                _FensterTitel = "Statistik Rohstoff-Verbrauch vom "
                'Filter Uhrzeit übernehmen
                _FensterTitel &= FilterVonBis()

            Case wb_Global.StatistikType.StatistikRohstoffeDetails
                _FensterTitel = "Statistik Rohstoffe vom "
                'Filter Uhrzeit übernehmen
                _FensterTitel &= FilterVonBis()

            Case wb_Global.StatistikType.ChargenAuswertung, wb_Global.StatistikType.ChargenAuswertungSauerteig
                _FensterTitel = "Produktion "
                _FensterTitel &= FilterVonBis(StrtDate, EndeDate)

        End Select
    End Sub

    Private Shared Function FilterVonBis() As String
        Dim FilterText As String = ""

        If UhrZeitVonChecked Then
            FilterText &= FilterVon.ToString("dd.MM.yyyy") & UhrzeitVon.ToString(" hh:mm")
        Else
            FilterText &= FilterVon.ToString("dd.MM.yyyy")
        End If

        'Fenster-Titel
        _FensterTitel &= " bis "
        If UhrZeitBisChecked Then
            FilterText &= FilterBis.ToString("dd.MM.yyyy") & UhrzeitBis.ToString(" hh:mm ")
        Else
            FilterText &= FilterBis.ToString("dd.MM.yyyy")
        End If

        Return FilterText
    End Function

    Private Shared Function FilterVonBis(StrtDate As String, EndeDate As String) As String
        Dim FilterText As String = ""
        Try
            'Start-Datum
            If StrtDate <> "" Then
                FilterText &= " vom "
                FilterText &= Convert.ToDateTime(StrtDate).ToString("dd.MM.yyyy")
            End If
            'Ende-Datum
            If EndeDate <> "" Then
                FilterText &= " bis "
                FilterText &= Convert.ToDateTime(EndeDate).ToString("dd.MM.yyyy")
            End If
        Catch

        End Try
        Return FilterText
    End Function

    Public Shared Sub Liste_Click(sender As Object, StatistikType As wb_Global.StatistikType)
        RaiseEvent eListe_Click(sender, StatistikType)
    End Sub

    Public Shared Sub Liste_Print(sender As Object, StatistikType As wb_Global.StatistikType)
        RaiseEvent eListe_Print(sender, StatistikType)
    End Sub

    Public Shared Sub Detail_DblClick(sender As Object, ChargenZeile As wb_ChargenSchritt)
        RaiseEvent eDetail_DblClick(sender, ChargenZeile)
    End Sub

    ''' <summary>
    ''' Liste aller Komponenten mit Marker 'Zählt nicht zum Rezeptgewicht'
    ''' </summary>
    Private Shared Sub ReadKomponDoesNotCount()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim KomponentenNr As Integer

        winback.sqlSelect(wb_Sql_Selects.sqlKompDoNotCount)
        _Liste_KomponDoesNotCount.Clear()

        'Tabelle Komponenten
        While winback.Read
            'KomponentenNr
            KomponentenNr = winback.iField("KO_Nr")
            _Liste_KomponDoesNotCount.Add(KomponentenNr, True)
        End While

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Prüft ob die Komponente mit dieser Nummer NICHT zum Rezept-Gewicht zählt (True)
    ''' ''' </summary>
    ''' <param name="KomponNr"></param>
    ''' <returns></returns>
    Public Shared Function ZaehltNichtZumRezeptgewicht(KomponNr As Integer) As Boolean
        If Not _Liste_KomponDoesNotCountInit Then
            ReadKomponDoesNotCount()
            _Liste_KomponDoesNotCountInit = True
        End If
        Return _Liste_KomponDoesNotCount.ContainsKey(KomponNr)
    End Function


End Class
