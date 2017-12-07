Public Class wb_CheckedListBox
    Inherits Windows.Forms.CheckedListBox

    Private _SelIndex As Integer = 0
    Private _WinBackIniSektion As String = Nothing
    Private _WinBackIniKey As String = Nothing
    Public Delegate Function GetText(i As Integer) As String

    Public Property SelIndex As Integer
        Get
            Return _SelIndex
        End Get
        Set(value As Integer)
            RemoveAllSelections()
            _SelIndex = value
            If Items.Count > _SelIndex Then
                SetItemChecked(_SelIndex, True)
            End If
        End Set
    End Property

    Public Sub Fill(cbItems As Array, ConvertToText As GetText)
        If ConvertToText IsNot Nothing Then
            For Each x In cbItems
                Me.Items.Add(ConvertToText(x))
            Next
        Else
            For Each x In cbItems
                Me.Items.Add(x)
            Next
        End If
    End Sub

    Private Sub SelIndexChanged(sender As Object, e As EventArgs) Handles Me.SelectedIndexChanged
        'Index setzen
        SelIndex = Me.SelectedIndex
    End Sub

    Private Sub RemoveAllSelections()
        For Each x As Integer In Me.CheckedIndices
            SetItemChecked(x, False)
        Next
    End Sub

End Class
