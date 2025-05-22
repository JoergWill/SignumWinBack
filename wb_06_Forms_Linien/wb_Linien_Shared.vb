Public Class wb_Linien_Shared
    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)

    Public Shared aktBezeichnung As String
    Public Shared aktAdresse As String
    Public Shared aktPort As Integer

    Public Shared ReadOnly Property WinBackLinieNummer As String
        Get
            Dim AddressePort As String() = Split(aktAdresse, ":")
            If AddressePort.Length = 2 Then
                'VNC-Port (Linie + 10)
                aktPort = wb_Functions.StrToInt(AddressePort(1))
                'WinBack-Linie
                aktPort -= 10
                'maximal gültige Linie in der Produktion
                If aktPort <= wb_Global.MaxWinBackLinien Then
                    Return aktPort.ToString
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        End Get
    End Property

    Public Shared Sub Liste_Click(sender As Object)
        RaiseEvent eListe_Click(sender)
    End Sub

    Public Shared Sub Edit_Leave(sender As Object)
        RaiseEvent eEdit_Leave(sender)
    End Sub
End Class
