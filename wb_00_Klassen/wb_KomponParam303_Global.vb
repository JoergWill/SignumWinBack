Imports WinBack.wb_Global

''' <summary>
''' Ermittlung der Parameter für die Produktion
''' Gibt zu einem spezifischen Parameter p die entsprechenden statischen Werte zurück.
''' Vor dem ersten Aufruf wird die Hash-Table aus der Datenbank gelesen und aufgebaut.
''' </summary>
Public Class wb_KomponParam303_Global
    ''' <summary>
    ''' Hash-Table mit den statischen Werten für die einzelnen Parameter p
    ''' </summary>
    Private Shared _ErrorText As String = ""
    Private Shared _UpdateDatabaseFile As String = ""
    Public Shared ktTyp303Params As New Hashtable

    ''' <summary>
    ''' Vor dem ersten Aufruf der Funktionen in dieser Klasse wird der shared-Konstruktor aufgerufen
    ''' Initialisierung und Aufbau der Hash-Table aus der Datenbank (Tabelle KomponTypen und Einheiten)
    ''' </summary>
    Shared Sub New()
        Dim k As ktTyp303Param
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompTypXXX, "=303"))
        ktTyp303Params.Clear()
        While winback.Read

            k.ParamNr = winback.iField("KT_ParamNr")
            k.Bezeichnung = wb_Language.TextFilter(winback.sField("KT_Bezeichnung"))
            k.Einheit = winback.sField("E_Einheit")
            k.Used = (winback.sField("KT_Rezept") = "X")
            Try
                ktTyp303Params.Add(k.ParamNr, k)
            Catch
            End Try
        End While
        winback.Close()
    End Sub

    Public Shared ReadOnly Property ErrorText As String
        Get
            Return _ErrorText
        End Get
    End Property

    Public Shared ReadOnly Property UpdateDatabaseFile As String
        Get
            Return _UpdateDatabaseFile
        End Get
    End Property

    ''' <summary>
    ''' Gibt die statischen Werte für den Parameter p zurück
    ''' Aus der Datenbank KomponTypen (KT_Typ_Nr = 300, KT_ParamNr = p)
    '''  - Bezeichnung      Parameter-Bezeichnung (Produktions-Steuerung)
    '''  - Einheit          Einheit
    '''  - Used             Verwendet/Nicht verwendet (Kunden-Spezifisch)
    ''' </summary>
    ''' <param name="p">Interger Parameter-Nummer</param>
    ''' <returns>ktTyp303Param</returns>
    Public Shared Function kt303Param(p As Integer) As wb_Global.ktTyp303Param
        If ktTyp303Params.ContainsKey(p) Then
            Return ktTyp303Params(p)
        Else
            Dim k As New ktTyp303Param
            k.Bezeichnung = "---"
            k.Used = False
            Return k
        End If
    End Function

    Public Shared Function IsValidParameter(index As Integer) As Boolean
        Return ktTyp303Params.ContainsKey(index)
    End Function

    Public Shared Function IsEU(index As Integer) As Boolean
        If index >= minTyp303EU And index <= maxTyp303EU Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function IsBioVerband(index As Integer) As Boolean
        If index >= minTyp303BioVerband And index <= maxTyp303BioVerband Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Prüft ob die Datenbank alle notwendigen Daten und Einträge enthält.
    ''' Die Datenbank muss Einträge für die Parameter Produktion(300) enthalten:
    ''' 
    ''' Public Const T303_EU-Butter = 1
    ''' Public Const T303_EU-Alkohol = 2
    ''' Public Const T303_BioVerband = 11
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CheckDB() As Boolean
        'FehlerText löschen
        _ErrorText = ""
        'Datenbank-UpdateFile (Update WinBack.Datenbank kann das Problem lösen)
        _UpdateDatabaseFile = "2.30_KomponTypen_BioVerband.sql"

        'alle Parameter(Update) prüfen
        Try
            wb_Functions.AssertTrue(ktTyp303Params.ContainsKey(wb_Global.T303_EU_Butter), "Parameter(1) EU-Butter Tabelle winback.KomponTypen")
            wb_Functions.AssertTrue(ktTyp303Params.ContainsKey(wb_Global.T303_EU_Alkohol), "Parameter(2) EU-Alkohol fehlt in Tabelle winback.KomponTypen")
            wb_Functions.AssertTrue(ktTyp303Params.ContainsKey(wb_Global.T303_BioVerband), "Parameter(11) Bio-Verband fehlt in Tabelle winback.KomponTypen")
        Catch ex As Exception
            Trace.WriteLine("Fehler in Komponenten-Parameter Typ 303 - Datensätze fehlen !")
            _ErrorText = ex.Message
            Trace.WriteLine("@E_" & _ErrorText)
            Return False
        End Try
        Return True
    End Function

End Class
