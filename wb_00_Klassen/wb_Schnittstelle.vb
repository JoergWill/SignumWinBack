Public Class wb_Schnittstelle

    Private _Name As String
    Private _ImportVerzeichnis As String
    Private _ExportVerzeichnis As String

    Public Tabellen As New List(Of wb_SchnittstelleTabelle)

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property

    Public Property ImportVerzeichnis As String
        Get
            Return _ImportVerzeichnis
        End Get
        Set(value As String)
            _ImportVerzeichnis = value
        End Set
    End Property

    Public Property ExportVerzeichnis As String
        Get
            Return _ExportVerzeichnis
        End Get
        Set(value As String)
            _ExportVerzeichnis = value
        End Set
    End Property
End Class
