Imports WinBack.wb_Global
Public Class wb_KomponParam300

    Private Structure Typ300
        Public _Wert As String
    End Structure

    Private Parameter(wb_Global.maxTyp300) As Typ300

    Public Sub New()
        Wert(T300_LinienGruppe) = wb_Global.UNDEFINED
        Wert(T300_RzNr) = wb_Global.UNDEFINED
    End Sub

    Public Property Wert(index As Integer) As String
        Get
            Return Parameter(index)._Wert
        End Get
        Set(value As String)
            Parameter(index)._Wert = value
        End Set
    End Property

    Public ReadOnly Property Liniengruppe As Integer
        Get
            If Wert(T300_LinienGruppe) IsNot Nothing Then
                Return wb_Functions.StrToInt(Wert(T300_LinienGruppe))
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
    End Property

    Public ReadOnly Property RzNr As Integer
        Get
            If Wert(T300_RzNr) IsNot Nothing Then
                Return wb_Functions.StrToInt(Wert(T300_RzNr))
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
    End Property

End Class
