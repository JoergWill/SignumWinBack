Imports WinBack.wb_Global
Imports WinBack.wb_KomponParam201_Global

Public Class wb_KomponParam201
    Private Structure Typ201
        Public _Wert As String
    End Structure

    Private Parameter(wb_Global.maxTyp201) As Typ201

    Public Sub New()
    End Sub

    Public Property Wert(index As Integer) As String
        Get
            Return Parameter(index)._Wert
        End Get
        Set(value As String)
            Parameter(index)._Wert = value
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
    '''     
    ''' SQL_Anweisung REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(KoNr As Integer, ByRef winback As wb_Sql) As Boolean
        'Result OK
        Dim Result As Boolean = True

        'alle Datensätze im Array durchlaufen
        For i = 0 To maxTyp201
            If IsValidParameter(i) Then
                Result = Result AndAlso MySQLdbUpdate(KoNr, i, winback)
            End If
        Next
        'Ergebnis zurückgeben
        Return Result
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
        'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
        sql = KoNr & ", 201, " & ParamNr.ToString & ", '" & Wert(ParamNr) & "', '" & kt201Param(ParamNr).Bezeichnung & "'"

        'Update ausführen
        Return winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateRohParams, sql))
    End Function

End Class
