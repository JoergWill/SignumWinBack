Public Class wb_Main_Progress_Shared
    Private Shared _MainProgress As wb_Main_Progress = Nothing
    Private Shared _MainProgressVisible As Boolean = False

    Public Shared Property MainProgressVisible
        Get
            Return _MainProgressVisible
        End Get
        Set(value)
            _MainProgressVisible = value
        End Set
    End Property

    Public Shared Sub ShowProgressBar()
        If Not _MainProgressVisible Then
            If _MainProgress Is Nothing Then
                _MainProgress = New wb_Main_Progress
            End If

            If Not MainProgressVisible Then
                _MainProgress.Show()
            Else
                _MainProgress.BringToFront()
            End If

            Windows.Forms.Application.DoEvents()
        End If
    End Sub

    Public Shared Sub HideProgressBar()
        If _MainProgressVisible Then
            If _MainProgress IsNot Nothing Then
                If _MainProgressVisible Then
                    _MainProgress.Close()
                    _MainProgress = Nothing
                End If
            End If
        End If
    End Sub
End Class
