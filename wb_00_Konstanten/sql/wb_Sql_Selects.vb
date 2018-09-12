Partial Public Class wb_Sql_Selects

    Public Shared Function setParams(sql As String, Param0 As String, Optional Param1 As String = "-",
                                     Optional Param2 As String = "-", Optional Param3 As String = "-",
                                     Optional Param4 As String = "-") As String
        'Platzhalter im Text durch Parameter ersetzen
        Return wb_Functions.SetParams(sql, Param0, Param1, Param2, Param3, Param4)
    End Function

End Class
