﻿Imports WinBack.wb_Global

''' <summary>
''' Ermittlung der Parameter für die Produkt-Information
''' Gibt zu einem spezifischen Parameter p die entsprechenden statischen Werte zurück.
''' Vor dem ersten Aufruf wird die Hash-Table aus der Datenbank gelesen und aufgebaut.
''' </summary>
Public Class wb_KomponParam201_Global
    ''' <summary>
    ''' Hash-Table mit den statischen Werten für die einzelnen Parameter p
    ''' </summary>
    Public Shared ktTyp201Params As New Hashtable

    ''' <summary>
    ''' Vor dem ersten Aufruf der Funktionen in dieser Klasse wird der shared-Konstruktor aufgerufen
    ''' Initialisierung und Aufbau der Hash-Table aus der Datenbank (Tabelle KomponTypen und Einheiten)
    ''' </summary>
    Shared Sub New()
        Dim k As ktTyp200Param
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompTypXXX, "=201"))
        ktTyp201Params.Clear()
        While winback.Read

            k.ParamNr = winback.iField("KT_ParamNr")
            k.Bezeichnung = wb_Language.TextFilter(winback.sField("KT_Bezeichnung"))
            k.Einheit = winback.sField("E_Einheit")
            k.Used = (winback.sField("KT_Rezept") = "X")
            k.Feld = winback.sField("KT_Kommentar")
            Try
                ktTyp201Params.Add(k.ParamNr, k)
            Catch
            End Try
        End While
        winback.Close()
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
    Public Shared Function kt201Param(p As Integer) As wb_Global.ktTyp200Param
        If ktTyp201Params.ContainsKey(p) Then
            Return ktTyp201Params(p)
        Else
            Dim k As New ktTyp200Param
            k.Bezeichnung = "---"
            k.Used = False
            Return k
        End If
    End Function

    Public Shared Function IsValidParameter(index As Integer) As Boolean
        Return ktTyp201Params.ContainsKey(index)
    End Function

End Class
