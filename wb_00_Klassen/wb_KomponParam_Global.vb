Imports WinBack.wb_Global
''' <summary>
''' Ermittlung der Parameter für die Produktion
''' Gibt zu einem spezifischen Parameter p die entsprechenden statischen Werte zurück.
''' Vor dem ersten Aufruf wird die Hash-Table aus der Datenbank gelesen und aufgebaut.
''' </summary>
Public Class wb_KomponParam_Global

    Private Shared _ktTypMaxParams As New Hashtable

    Public Shared ktTypXXXParams As New Hashtable

    ''' <summary>
    ''' Vor dem ersten Aufruf der Funktionen in dieser Klasse wird der Shared-Konstruktor aufgerufen
    ''' Initialisierung und Aufbau der Hash-Table aus der Datenbank (Tabelle KomponTypen und Einheiten)
    ''' </summary>
    Shared Sub New()
        Dim k As ktTypXXXParam
        Dim Type As Integer = wb_Global.UNDEFINED
        _ktTypMaxParams.Clear()

        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompTypXXX, "<>200"))
        ktTypXXXParams.Clear()
        While winback.Read

            'Parameter-Datensatz
            k.Type = winback.iField("KT_Typ_Nr")

            'Die Komponenten(Parameter-)Type hat sich geändert
            If k.Type <> Type Then
                'Sonder-Parameter einfügen
                ExtendedParameter(Type, k.ParamNr)
                'Maximalwert der Parameter pro Type
                _ktTypMaxParams.Add(Type, k.ParamNr)
                'letzte Type merken
                Type = k.Type
            End If

            'Parameter-Nummer
            k.ParamNr = winback.iField("KT_ParamNr")
            'Parameter-Bezeichnung
            k.Bezeichnung = wb_Language.TextFilter(winback.sField("KT_Bezeichnung"))
            'Parameter-Einheit
            k.Einheit = winback.sField("E_Einheit")
            'Parameter-Format
            k.Format = winback.iField("KT_Format")
            'Parameter-Eingabe unterer Grenzwert
            k.GwUnten = winback.sField("KT_UnterGW")
            'Parameter-Eingabe oberer Grenzwert
            k.GwOben = winback.sField("KT_OberGW")
            'Parameter aktiv (Nährwerte und Allergene
            k.Used = (winback.sField("KT_Rezept") = "")

            'Parameter-Datensatz speichern
            Try
                ktTypXXXParams.Add(BuildKey(k.Type, k.ParamNr), k)
            Catch
            End Try

        End While
        winback.Close()
    End Sub

    ''' <summary>
    ''' Fügt, abhängig vom Konfiguration und Komponenten(Parameter)-Type zusätzliche
    ''' Parameter ein.
    ''' 
    '''     Type 101 - Lagerort/Silo
    '''     
    ''' </summary>
    ''' <param name="t"></param>
    Private Shared Sub ExtendedParameter(t As Integer, ByRef p As Integer)
        'neuer Parameter
        Dim k As ktTypXXXParam

        'abhängig von der Komponenten-Type
        Select Case wb_Functions.IntToKomponType(t)

            'Automatik-Rohstoffe
            Case wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE
                'zusätzlicher Parameter Lagerort/Silo
                k.Type = t
                k.ParamNr = 90
                k.Bezeichnung = "Lagerort/Silo"
                k.Einheit = 0
                k.Used = True
                ktTypXXXParams.Add(BuildKey(k.Type, k.ParamNr), k)

                'maximale Parameter-Nummer korrigieren
                p = Math.Max(p, k.ParamNr)

        End Select
    End Sub

    ''' <summary>
    ''' Gibt die statischen Werte für den Parameter p zurück
    ''' Aus der Datenbank KomponTypen (KT_Typ_Nr = 300, KT_ParamNr = p)
    '''  - Bezeichnung      Parameter-Bezeichnung (Produktions-Steuerung)
    '''  - Einheit          Einheit
    '''  - Used             Verwendet/Nicht verwendet (Kunden-Spezifisch)
    ''' </summary>
    ''' <param name="p">Interger Parameter-Nummer</param>
    ''' <returns>ktTyp300Param</returns>
    Public Shared Function ktXXXParam(t As Integer, p As Integer) As wb_Global.ktTypXXXParam
        If IsValidParameter(t, p) Then
            Return ktTypXXXParams(BuildKey(t, p))
        Else
            Dim k As New ktTypXXXParam
            k.Bezeichnung = "---"
            k.Used = False
            Return k
        End If
    End Function

    Public Shared Function MaxParam(t As Integer) As Integer
        If _ktTypMaxParams.ContainsKey(t) Then
            Return _ktTypMaxParams(t)
        Else
            Return wb_Global.UNDEFINED
        End If
    End Function

    Public Shared Function IsValidParameter(t As Integer, p As Integer) As Boolean
        Return ktTypXXXParams.ContainsKey(BuildKey(t, p))
    End Function

    Private Shared Function BuildKey(t As Integer, p As Integer) As String
        Return t.ToString("000") & p.ToString("000")
    End Function
End Class
