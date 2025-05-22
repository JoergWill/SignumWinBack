Imports WinBack.wb_Functions
Imports WinBack.wb_Global
Imports WinBack.wb_KomponParam303_Global

Public Class wb_KomponParam303

    Private Structure Typ303
        Public _Wert As String
    End Structure

    Private Parameter(wb_Global.maxTyp303) As Typ303

    Public Sub New()
        Wert(T303_EU_Butter) = wb_Global.ErnaehrungsForm.N
        Wert(T303_EU_Alkohol) = wb_Global.ErnaehrungsForm.N
        Wert(T303_BioVerband) = wb_Global.UNDEFINED
    End Sub

    Public Property Wert(index As Integer) As String
        Get
            Return Parameter(index)._Wert
        End Get
        Set(value As String)
            Parameter(index)._Wert = value
        End Set
    End Property

    Public Property EU_Butter As Double
        Get
            If Wert(T303_EU_Butter) IsNot Nothing Then
                Return wb_Functions.StrToDouble(Wert(T303_EU_Butter))
            Else
                Return 0.0
            End If
        End Get
        Set(value As Double)
            Wert(T303_EU_Butter) = value.ToString
        End Set
    End Property

    Public Property EU_Alkohol As Double
        Get
            If Wert(T303_EU_Alkohol) IsNot Nothing Then
                Return wb_Functions.StrToInt(Wert(T303_EU_Alkohol))
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
        Set(value As Double)
            Wert(T303_EU_Alkohol) = value.ToString
        End Set
    End Property

    ''' <summary>
    ''' Update aller geänderten Komponenten-Parameter in Tabelle winback.RohParams
    '''     RP_Ko_Nr
    '''     RP_Typ_Nr
    '''     RP_ParamNr
    '''     RP_Wert
    '''     RP_Kommentar
    '''     RP_Timestamp
    ''' </summary>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(KoNr As Integer, ByRef winback As wb_Sql) As Boolean
        'Result OK
        MySQLdbUpdate = True

        'alle Datensätze im Array durchlaufen
        For i = 0 To maxTyp303
            If IsValidParameter(i) Then
                MySQLdbUpdate = MySQLdbUpdate(KoNr, i, winback)
            End If
        Next
    End Function

    ''' <summary>
    ''' Update eines Komponenten-Parameters mit ParamNr in Tabelle winback.RohParams
    ''' </summary>
    ''' <param name="KoNr"></param>
    ''' <param name="ParamNr"></param>
    ''' <param name="winback"></param>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(KoNr As Integer, ParamNr As Integer, ByRef winback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
        Dim sql As String

        'Update-Statement wird dynamisch erzeugt
        If IsEU(ParamNr) Then
            'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
            sql = KoNr & ", 303, " & ParamNr.ToString & ", '" & Wert(ParamNr) & "', '" & kt303Param(ParamNr).Bezeichnung & "'"
        Else
            'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
            sql = KoNr & ", 303, " & ParamNr.ToString & ", '" & Wert(ParamNr) & "', '" & kt303Param(ParamNr).Bezeichnung & "'"
        End If

        'Update ausführen
        Return winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateRohParams, sql))
    End Function

    ''' <summary>
    ''' Update aller geänderten Komponenten-Parameter in Tabelle 
    ''' 
    ''' [dbo].[ArtikelNaehrwerte]
    '''     [ArtikelNr]
    '''     [Einheit]
    '''     [Farbe]                         immer 0
    '''     [Groesse]                       immer NULL
    '''     [StuecklistenVariantenNr]
    '''     [NaehrwertNr]
    '''     [Menge]
    '''     
    ''' [dbo].[ArtikelAllergene
    '''     [ArtikelNr]
    '''     [StuecklistenVariantenNr]
    '''     [AllergenNr]
    '''     [Kennzeichnung]
    ''' </summary>
    ''' <returns></returns>
    Public Function MsSQLdbUpdate(KoAlNum As String, Unit As Integer, orgaback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
#Disable Warning BC42024 ' Nicht verwendete lokale Variable: "sql".
        Dim sql As String
#Enable Warning BC42024 ' Nicht verwendete lokale Variable: "sql".
        'Result OK
        MsSQLdbUpdate = True

        'TODO - Festlegen in welcher OrgaBack-Tabelle diese Daten gespeichert werden müssen!

    End Function

    ''' <summary>
    ''' Update EINES geänderte Komponenten-Parameter in Tabelle.
    ''' Da REPLACE be msSQL nicht funktioniert wird zuerst versucht, per UPDATE den Datensatz zu aktualisieren. Wenn 
    ''' das UPDATE nicht funktioniert (Datensatz nicht vorhanden) wird per INSERT der Datensatz neu angelegt
    ''' 
    ''' [dbo].[ArtikelNaehrwerte]
    '''     [ArtikelNr]
    '''     [Einheit]
    '''     [Farbe]                         immer 0
    '''     [Groesse]                       immer NULL
    '''     [StuecklistenVariantenNr]
    '''     [NaehrwertNr]
    '''     [Menge]
    '''     
    ''' [dbo].[ArtikelAllergene
    '''     [ArtikelNr]
    '''     [StuecklistenVariantenNr]
    '''     [AllergenNr]
    '''     [Kennzeichnung]
    ''' </summary>
    ''' <returns></returns>
    Public Function MsSQLdbUpdate(KoAlNum As String, ParamNr As Integer, Unit As Integer, orgaback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
#Disable Warning BC42024 ' Nicht verwendete lokale Variable: "sql_Delete".
        Dim sql_Delete As String
#Enable Warning BC42024 ' Nicht verwendete lokale Variable: "sql_Delete".
#Disable Warning BC42024 ' Nicht verwendete lokale Variable: "sql_Insert".
        Dim sql_Insert As String
#Enable Warning BC42024 ' Nicht verwendete lokale Variable: "sql_Insert".

        'TODO - Festlegen in welcher OrgaBack-Tabelle diese Daten gespeichert werden müssen!

#Disable Warning BC42353 ' Die Funktion "MsSQLdbUpdate" gibt nicht für alle Codepfade einen Wert zurück. Fehlt eine Return-Anweisung?
    End Function
#Enable Warning BC42353 ' Die Funktion "MsSQLdbUpdate" gibt nicht für alle Codepfade einen Wert zurück. Fehlt eine Return-Anweisung?


End Class
