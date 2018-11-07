''' <summary>
''' Statische Daten zur Verwaltung der Kocher/Rezepte
'''  - Anzahl der Kocher/IP-Adressen
'''  - Verweise auf die Master-Rezepte
''' </summary>
Public Class wb_Kocher_Global
    Private Shared _AnzahlKocher As Integer
    Private Shared _KocherListe As New Hashtable


    Shared Sub New()
        Init_BC9000_Kocher()

    End Sub

    Public Shared ReadOnly Property AnzahlKocher As Integer
        Get
            Return _AnzahlKocher
        End Get
    End Property

    Public Shared Property KocherListe As Hashtable
        Get
            Return _KocherListe
        End Get
        Set(value As Hashtable)
            _KocherListe = value
        End Set
    End Property

    ''' <summary>
    ''' Berechnet den Rezeptschritt aus dem Index in der Rezept-Text-Datei
    ''' </summary>
    ''' <param name="Idx"></param>
    ''' <returns></returns>
    Public Shared Function IdxToSchritt(Idx As Integer) As Integer
        Return Fix((Idx + wb_Global.Kocher_IdxOffset) / wb_Global.Kocher_IdxTeiler)
    End Function

    ''' <summary>
    ''' Berechnet die Parameter-Nummer aus Index und Rezeptschritt in der Rezept-Text-Datei
    ''' </summary>
    ''' <param name="Idx"></param>
    ''' <param name="Schritt"></param>
    ''' <returns></returns>
    Public Shared Function IdxToParam(Idx As Integer, Schritt As Integer) As Integer
        Return (Idx - (Schritt * wb_Global.Kocher_IdxTeiler)) + wb_Global.Kocher_IdxOffset + 1
    End Function

    ''' <summary>
    ''' Berechnet den Index aus Parameter-Nummer Rezeptschritt in der Rezept-Text-Datei
    ''' </summary>
    ''' <param name="Prm"></param>
    ''' <param name="Schritt"></param>
    ''' <returns></returns>
    Public Shared Function ParamToIdx(Prm As Integer, Schritt As Integer) As Integer
        Return Schritt * wb_Global.Kocher_IdxTeiler + Prm - wb_Global.Kocher_IdxTeiler + 2
    End Function

    ''' <summary>
    ''' Liest die Anzahl der Kocher/Röster aus der BC9000-Liste ein.
    '''  - BC9_BC_Typ=3
    ''' </summary>
    Shared Sub Init_BC9000_Kocher()
        'Lesen aller Kocher aus winback.BC9000Liste(BC9_BC_Typ=3)
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlKocherBC9)
        'Liste aller Kocher/Röster
        _KocherListe.Clear()
        _AnzahlKocher = 0

        'alle Datensätze sortiert nach Kocher/Röster-Nummer
        While winback.Read
            Try
                'Kocher-Nr(laufende Nummer)
                _AnzahlKocher += 1
                Dim Kocher As New wb_Kocher

                Kocher.Nummer = _AnzahlKocher
                Kocher.IP_Adresse = winback.sField("BC9_IpAdresse")

                'zum Dictonary hinzufügen
                _KocherListe.Add(_AnzahlKocher, Kocher)
            Catch
            End Try
        End While

        'Master(Server) erstellen und in Liste eintragen
        Dim Master As New wb_Kocher
        Master.Nummer = wb_Global.KocherMaster
        KocherListe.Add(wb_Global.KocherMaster, Master)

        'Verbindung wieder schliessen
        winback.Close()
    End Sub


    'Tabelle winback.BC9000Liste
    '   BC9_Nr
    '   BC9_BC_Typ
    '   BC9_Typ
    '   BC9_SubTyp
    '   BC9_aktiv
    '   BC9_Beh_Nr
    '   BC9_Param
    '   BC9_IpAdresse
    '   BC9_Bezeichnung
    '   BC9_BezeichNr
    '   BC9_Kommentar
    '   BC9_AnzFBits
    '   BC9_Timestamp

End Class
