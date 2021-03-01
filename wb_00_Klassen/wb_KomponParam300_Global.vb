Imports WinBack.wb_Global

''' <summary>
''' Ermittlung der Parameter für die Produktion
''' Gibt zu einem spezifischen Parameter p die entsprechenden statischen Werte zurück.
''' Vor dem ersten Aufruf wird die Hash-Table aus der Datenbank gelesen und aufgebaut.
''' </summary>
Public Class wb_KomponParam300_Global
    ''' <summary>
    ''' Hash-Table mit den statischen Werten für die einzelnen Parameter p
    ''' </summary>
    Private Shared _ErrorText As String = ""
    Private Shared _UpdateDatabaseFile As String = ""
    Public Shared ktTyp300Params As New Hashtable

    ''' <summary>
    ''' Vor dem ersten Aufruf der Funktionen in dieser Klasse wird der shared-Konstruktor aufgerufen
    ''' Initialisierung und Aufbau der Hash-Table aus der Datenbank (Tabelle KomponTypen und Einheiten)
    ''' </summary>
    Shared Sub New()
        Dim k As ktTyp300Param
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompTypXXX, "=300"))
        ktTyp300Params.Clear()
        While winback.Read

            k.ParamNr = winback.iField("KT_ParamNr")
            k.Bezeichnung = wb_Language.TextFilter(winback.sField("KT_Bezeichnung"))
            k.Einheit = winback.sField("E_Einheit")
            k.Used = (winback.sField("KT_Rezept") = "X")
            Try
                ktTyp300Params.Add(k.ParamNr, k)
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
    ''' <returns>ktTyp300Param</returns>
    Public Shared Function kt300Param(p As Integer) As wb_Global.ktTyp300Param
        If ktTyp300Params.ContainsKey(p) Then
            Return ktTyp300Params(p)
        Else
            Dim k As New ktTyp300Param
            k.Bezeichnung = "---"
            k.Used = False
            Return k
        End If
    End Function

    Public Shared Function IsValidParameter(index As Integer) As Boolean
        Return ktTyp300Params.ContainsKey(index)
    End Function

    ''' <summary>
    ''' Prüft ob die Datenbank alle notwendigen Daten und Einträge enthält.
    ''' Die Datenbank muss Einträge für die Parameter Produktion(300) enthalten:
    ''' 
    ''' Public Const T300_Backverlust = 1
    ''' Public Const T300_ProdVorlauf = 2
    ''' Public Const T300_Zuschnitt = 3
    ''' Public Const T300_LinienGruppe = 5
    ''' Public Const T300_RzNr = 6
    ''' Public Const T300_RezeptNummer = 7
    ''' Public Const T300_RezeptName = 8
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CheckDB() As Boolean
        'FehlerText löschen
        _ErrorText = ""
        'Datenbank-UpdateFile (Update WinBack.Datenbank kann das Problem lösen)
        _UpdateDatabaseFile = "2.30_KomponTypen.sql"

        'alle Parameter(Update) prüfen
        Try
            wb_Functions.AssertTrue(ktTyp300Params.ContainsKey(wb_Global.T300_Backverlust), "Parameter(1) Backverlust fehlt in Tabelle winback.KomponTypen")
            wb_Functions.AssertTrue(ktTyp300Params.ContainsKey(wb_Global.T300_ProdVorlauf), "Parameter(2) ProduktionsVorlauf fehlt in Tabelle winback.KomponTypen")
            wb_Functions.AssertTrue(ktTyp300Params.ContainsKey(wb_Global.T300_Zuschnitt), "Parameter(3) Zuschnitt fehlt in Tabelle winback.KomponTypen")
            wb_Functions.AssertTrue(ktTyp300Params.ContainsKey(wb_Global.T300_LinienGruppe), "Parameter(5) ProdLinienGruppe fehlt in Tabelle winback.KomponTypen")
            wb_Functions.AssertTrue(ktTyp300Params.ContainsKey(wb_Global.T300_RzNr), "Parameter(6) RzNr fehlt in Tabelle winback.KomponTypen")
            wb_Functions.AssertTrue(ktTyp300Params.ContainsKey(wb_Global.T300_RezeptNummer), "Parameter(7) RezeptNummer fehlt in Tabelle winback.KomponTypen")
            wb_Functions.AssertTrue(ktTyp300Params.ContainsKey(wb_Global.T300_RezeptName), "Parameter(8) RezeptName fehlt in Tabelle winback.KomponTypen")
        Catch ex As Exception
            Trace.WriteLine("Fehler in Komponenten-Parameter Typ 300 - Datensätze fehlen !")
            _ErrorText = ex.Message
            Trace.WriteLine(_ErrorText)
            Return False
        End Try
        Return True
    End Function

End Class
