Public Class wb_SchnittstelleFeld
    Private _Name As String
    Private _DBTabelle As String
    Private _DBFeld As String
    Private _IdxRead As Integer
    Private _IdxWrite As Integer

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property

    Public Property DBTabelle As String
        Get
            Return _DBTabelle
        End Get
        Set(value As String)
            _DBTabelle = value
        End Set
    End Property

    Public Property DBFeld As String
        Get
            Return _DBFeld
        End Get
        Set(value As String)
            _DBFeld = value
        End Set
    End Property

    Public Property IdxRead As Integer
        Get
            Return _IdxRead
        End Get
        Set(value As Integer)
            _IdxRead = value
        End Set
    End Property

    Public Property IdxWrite As Integer
        Get
            Return _IdxWrite
        End Get
        Set(value As Integer)
            _IdxWrite = value
        End Set
    End Property

    ''' <summary>
    ''' Wandelt den übergebenen Import-String abhängig vom 
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Function Convert(s As String) As String
        Return s
    End Function
End Class
