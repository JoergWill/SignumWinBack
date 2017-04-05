Imports WinBack.wb_Functions
Imports WinBack.wb_Global

Public Class wb_ktTypX
    Private KO_Nr As Integer
    Private KO_Type As KomponTypen
    Private KO_Nr_AlNum As String
    Private KO_Bezeichnung As String
    Private KO_Kommentar As String

    Public ktTyp301 As wb_ktTyp301

    Public ReadOnly Property Nr As Integer
        Get
            Return KO_Nr
        End Get
    End Property

    Public ReadOnly Property Type As KomponTypen
        Get
            Return KO_Type
        End Get
    End Property

    Public Property Nummer As String
        Set(value As String)
            KO_Nr_AlNum = value
        End Set
        Get
            Return KO_Nr_AlNum
        End Get
    End Property

    Public Property Bezeichung As String
        Set(value As String)
            KO_Bezeichnung = value
        End Set
        Get
            Return KO_Bezeichnung
        End Get
    End Property

    Public Property Kommentar As String
        Set(value As String)
            KO_Kommentar = value
        End Set
        Get
            Return KO_Kommentar
        End Get
    End Property

End Class
