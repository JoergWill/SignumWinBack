Imports WinBack

Public Class wb_Chargen_Shared
    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eFilter_Click(sender As Object)
    Public Shared Event eDetail_DblClick(sender As Object, ChargenZeile As wb_ChargenSchritt)

    Private Shared _SortKriterium As wb_Global.ChargenListeSortKriterium
    Private Shared _Filter As Boolean
    Private Shared _FilterVon As Date
    Private Shared _FilterBis As Date
    Private Shared _AlleLinien As Boolean
    Private Shared _Liste_TagesWechselNummer As Integer
    Private Shared _Liste_KomponDoesNotCount As New Dictionary(Of Integer, Boolean)

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
        ReadKomponDoesNotCount()
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

    Public Shared Sub Liste_Click(sender As Object)
        RaiseEvent eListe_Click(sender)
    End Sub

    Public Shared Sub Filter_Click(sender As Object)
        RaiseEvent eFilter_Click(sender)
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
        Return _Liste_KomponDoesNotCount.ContainsKey(KomponNr)
    End Function

End Class
