Imports Microsoft.VisualStudio.TestTools.UnitTesting
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
    Private Shared _ErrorText As String = ""
    Private Shared _UpdateDatabaseFile As String = ""
    Private Shared ktTyp301Params As New Hashtable

    ''' <summary>
    ''' Vor dem ersten Aufruf der Funktionen in dieser Klasse wird der shared-Konstruktor aufgerufen
    ''' Initialisierung und Aufbau der Hash-Table aus der Datenbank (Tabelle KomponTypen und Einheiten)
    ''' </summary>
    Shared Sub New()
        Dim k As ktTyp301Param
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompTypXXX, "=301"))
        ktTyp301Params.Clear()
        While winback.Read

            k.ParamNr = winback.iField("KT_ParamNr")
            k.Bezeichnung = wb_Language.TextFilter(winback.sField("KT_Bezeichnung"))
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
        winback = Nothing
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
        If ktTyp301Params.ContainsKey(p) Then
            Return ktTyp301Params(p)
        Else
            Dim k As New ktTyp301Param
            k.Bezeichnung = "---"
            k.Used = False
            Return k
        End If

    End Function

    Public Shared Function IsAllergen(index As Integer) As Boolean
        If index >= minTyp301Allergen And index <= maxTyp301Allergen Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function IsErnaehrung(index As Integer) As Boolean
        If index >= minTyp301Ernaehrung And index <= maxTyp301Ernaehrung Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function IsValidParameter(index As Integer) As Boolean
        Return ktTyp301Params.ContainsKey(index)
    End Function


    ''' <summary>
    ''' Prüft ob die Datenbank alle notwendigen Daten und Einträge enthält.
    ''' Die Datenbank muss Einträge für die Parameter Produktion(300) enthalten:
    ''' 
    ''' Public Const T301_Vegetarisch = 210
    ''' Public Const T301_Vegan = 211
    ''' Public Const T301_Koscher = 212
    ''' Public Const T301_Halal = 213
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CheckDB() As Boolean
        'FehlerText löschen
        _ErrorText = ""
        'Datenbank-UpdateFile (Update WinBack.Datenbank kann das Problem lösen)
        _UpdateDatabaseFile = "2.30_ErweiterungAllergene.sql"

        'alle Parameter(Update) prüfen
        Try
            Assert.IsTrue(ktTyp301Params.ContainsKey(wb_Global.T301_Vegetarisch), "Parameter(210) Vegetarisch fehlt in winback.KomponTypen")
            Assert.IsTrue(ktTyp301Params.ContainsKey(wb_Global.T301_Vegan), "Parameter(211) Vegan fehlt in winback.KomponTypen")
            Assert.IsTrue(ktTyp301Params.ContainsKey(wb_Global.T301_Koscher), "Parameter(212) Koscher fehlt in winback.KomponTypen")
            Assert.IsTrue(ktTyp301Params.ContainsKey(wb_Global.T301_Halal), "Parameter(213) Halal fehlt in winback.KomponTypen")
        Catch ex As Exception
            Trace.WriteLine("Fehler in Komponenten-Parameter Typ 301 - Datensätze fehlen !")
            _ErrorText = ex.Message
            Trace.WriteLine(_ErrorText)
            Return False
        End Try
        Return True
    End Function

End Class

