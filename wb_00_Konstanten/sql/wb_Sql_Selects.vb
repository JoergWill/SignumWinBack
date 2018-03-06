Partial Public Class wb_Sql_Selects


    Public Shared Function setParams(sql As String, Param0 As String, Optional Param1 As String = "-",
                                     Optional Param2 As String = "-", Optional Param3 As String = "-",
                                     Optional Param4 As String = "-") As String
        sql = Replace(sql, "[0]", Param0)
        If Param1 <> "-" Then
            sql = Replace(sql, "[1]", Param1)
        End If
        If Param2 <> "-" Then
            sql = Replace(sql, "[2]", Param2)
        End If
        If Param3 <> "-" Then
            sql = Replace(sql, "[3]", Param3)
        End If
        If Param4 <> "-" Then
            sql = Replace(sql, "[4]", Param4)
        End If
        Return sql
    End Function

End Class
