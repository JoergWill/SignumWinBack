Public Class wb_KomponParamXXX
    Private Structure TypXXX
        Public _ParamNr As Integer
        Public _Wert As String
    End Structure

    Private Parameter(wb_Global.maxTypXXX) As TypXXX

    Public Property Wert(index As Integer) As String
        Get
            If index < wb_Global.maxTypXXX Then
                Return Parameter(index)._Wert
            Else
                Return ""
            End If
        End Get
        Set(value As String)
            If index < wb_Global.maxTypXXX Then
                Parameter(index)._Wert = value
            End If
        End Set
    End Property

    Public Property ParamNr(index As Integer) As Integer
        Get
            Return Parameter(index)._ParamNr
        End Get
        Set(value As Integer)
            Parameter(index)._ParamNr = value
        End Set
    End Property

    ''' <summary>
    ''' Update aller geänderten Komponenten-Parameter in Tabelle winback.KomponParams
    '''     KP_Ko_Nr
    '''     KP_ParamNr
    '''     KP_Wert
    '''     KP_Kommentar
    '''     KP_Timestamp
    '''     
    ''' SQL_Anweisung REPLACE INTO KomponParams (KP_Ko_Nr, KP_ParamNr, KP_Wert, KP_Kommentar) VALUES (...)
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(KoNr As Integer, ParamNr As Integer, ByRef winback As wb_Sql) As Boolean
        'Update ausführen REPLACE INTO KomponParams (KP_Ko_Nr, KP_ParamNr, KP_Wert, KP_Kommentar) VALUES (...)
        Return winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateKompParams, KoNr.ToString, ParamNr.ToString, Wert(ParamNr)))
    End Function

End Class

''''alle Datensätze im Array durchlaufen
''For i = 0 To wb_Global.maxTypXXX
''If wb_KomponParam_Global.IsValidParameter(t, i) Then
''End If
''Next
