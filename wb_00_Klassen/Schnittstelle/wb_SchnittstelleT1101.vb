Imports MySql.Data.MySqlClient

Public Class wb_SchnittstelleT1101
    Inherits wb_SchnittstelleTabelle

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides ReadOnly Property TabName As String
        Get
            Return "T1101"
        End Get
    End Property

    Public Overrides ReadOnly Property sql As String
    '    Get
    '        Return ""
    '    End Get
    'End Property

    Public Overrides ReadOnly Property sqlCount As String
    '    Get
    '        Return ""
    '    End Get
    'End Property

    Public Overrides Sub ImportWorker(winback As wb_Sql)
    End Sub

    Public Overrides Sub ExportWorker(ByRef sqlReader As MySqlDataReader, writer As System.IO.StreamWriter)
    End Sub

End Class
