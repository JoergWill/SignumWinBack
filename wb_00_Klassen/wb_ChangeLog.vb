
''' <summary>
''' Schreibt alle Änderungen in einem Objekt in eine dynamische Liste.
''' 
''' </summary>
Public Class wb_ChangeLog
    Private Changes As New System.Collections.Generic.List(Of wb_Global.wb_ChangeLogEintrag)
    Private _ChangeLogAktiv As Boolean = False

    Friend Property ChangeLogAktiv As Boolean
        Get
            Return _ChangeLogAktiv
        End Get
        Set(value As Boolean)
            _ChangeLogAktiv = value
        End Set
    End Property

    Friend Sub ChangeLogClear()
        Changes.Clear()
        _ChangeLogAktiv = True
    End Sub

    Friend Sub ChangeLogAdd(Typ As wb_Global.LogType, Prm As Integer, OldValue As String, NewValue As String)
        Dim X As wb_Global.wb_ChangeLogEintrag
        'nur loggen wenn der alte und der neue Wert sich unterscheiden
        If _ChangeLogAktiv And (NewValue <> OldValue) Then
            X.Type = Typ
            X.OldValue = OldValue
            X.NewValue = NewValue
            Changes.Add(X)
        End If
    End Sub

    Friend Sub ChangeLogAdd(Typ As wb_Global.LogType, Prm As Integer, OldValue As Double, NewValue As Double, Optional Format As String = "{0,8:##0.000}")
        Me.ChangeLogAdd(Typ, Prm, String.Format(Format, OldValue), String.Format(Format, NewValue))
    End Sub

    Friend Sub ChangeLogAdd(Typ As wb_Global.LogType, Prm As Integer, OldValue As wb_Global.AllergenInfo, NewValue As wb_Global.AllergenInfo)
        Me.ChangeLogAdd(Typ, Prm, wb_Functions.AllergenToString(OldValue), wb_Functions.AllergenToString(NewValue))
    End Sub

    Friend Function ChangeLogReport() As String
        Dim x As wb_Global.wb_ChangeLogEintrag
        Dim s As String = ""
        For Each x In Changes
            s += x.OldValue + "/" + x.NewValue + vbNewLine
        Next
        _ChangeLogAktiv = False
        Return s
    End Function
End Class
