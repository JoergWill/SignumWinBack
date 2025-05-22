Imports MySql.Data.MySqlClient

Public Class wb_SchnittstelleT4105
    Inherits wb_SchnittstelleTabelle


    Public Sub New()
        MyBase.New()
    End Sub

    Public Property Option_TW_Nummer As Integer
        Get
            Return wb_GlobalSettings.ExportChargenTWNr
        End Get
        Set(value As Integer)
            wb_GlobalSettings.ExportChargenTWNr = value
        End Set
    End Property

    Public Overrides ReadOnly Property TabName As String
        Get
            Return "T4105"
        End Get
    End Property
    Public Overrides ReadOnly Property sql As String
        Get
            Return wb_sql_Selects.setParams(wb_sql_Selects.sqlT4105, Option_TW_Nummer)
        End Get
    End Property

    Public Overrides ReadOnly Property sqlCount As String
        Get
            Return wb_sql_Selects.setParams(wb_sql_Selects.sqlT4105_Count, Option_TW_Nummer)
        End Get
    End Property


    Public Overrides Sub ImportWorker(winback As wb_Sql)
    End Sub

    Public Overrides Sub ExportWorker(ByRef sqlReader As MySqlDataReader, writer As System.IO.StreamWriter)
    End Sub

End Class
