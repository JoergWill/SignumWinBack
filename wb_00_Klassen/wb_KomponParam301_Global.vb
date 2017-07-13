Imports WinBack.wb_Global

''' <summary>
''' Ermittlung der Parameter für Allergene und Nährwerte
''' Gibt zu einem spezifischen Parameter p die entsprechenden statischen Werte zurück.
''' Vor dem ersten Aufruf wird die Hash-Table aus der Datenbank gelesen und aufgebaut.
''' </summary>
Public Class wb_KomponParam301_Global

    ''' <summary>
    ''' Hash-Table mit den statischen Werten für die einzelnen Parameter p
    ''' </summary>
    Public Shared ktTyp301Params As New Hashtable

    ''' <summary>
    ''' Vor dem ersten Aufruf der Funktionen in dieser Klasse wird der shared-Konstruktor aufgerufen
    ''' Initialisierung und Aufbau der Hash-Table aus der Datenbank (Tabelle KomponTypen und Einheiten)
    ''' </summary>
    Shared Sub New()
        Dim k As ktTyp301Param
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlKompTyp301)
        ktTyp301Params.Clear()
        While winback.Read

            k.ParamNr = winback.iField("KT_ParamNr")
            k.Bezeichnung = wb_Functions.TextFilter(winback.sField("KT_Bezeichnung"))
            k.KurzBezeichnung = winback.sField("KT_KurzBez")
            k.Gruppe = wb_Functions.StringTokt301Gruppe(winback.sField("KT_Wert"))
            k.Einheit = winback.sField("E_Einheit")
            k.Feld = winback.sField("KT_Kommentar")
            k.Used = (winback.sField("KT_Rezept") = "X")
            Try
                ktTyp301Params.Add(k.ParamNr, k)
            Catch
            End Try
        End While
        winback.Close()
    End Sub

    ''' <summary>
    ''' Gibt die statischen Werte für den Parameter p zurück
    ''' Aus der Datenbank KomponTypen (KT_Typ_Nr = 301, KT_ParamNr = p)
    '''  - Bezeichnung      Allergen/Nährwert-Bezeichnung
    '''  - KurzBezeichnung  Bei Nährwerten die Zweitbezeichnung, bei Allergenen der Genitiv
    '''  - Gruppe           Allergen(Nährwert-Gruppe)
    '''  - Einheit          Einheit
    '''  - Feld             Eintrag in Seriendruck-Dokument(Word)
    '''  - Used             Verwendet/Nicht verwendet (Kunden-Spezifisch)
    ''' </summary>
    ''' <param name="p">Interger Parameter-Nummer</param>
    ''' <returns>ktTyp301Param</returns>
    Public Shared Function kt301Param(p As Integer) As wb_Global.ktTyp301Param
        Return ktTyp301Params(p)
    End Function
End Class
