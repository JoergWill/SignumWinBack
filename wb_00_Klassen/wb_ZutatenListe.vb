Public Class wb_ZutatenListe
    Public Liste As New ArrayList

    Public Sub Clear()
        Liste.Clear()
    End Sub

    Public Sub Del_Doubletten()
        Dim lc As Integer = Liste.Count - 1
        Dim i As Integer = 0
        Dim j As Integer

        'Schleife über alle Elemente
        While i <= lc
            'Schleife über alle nachfolgenden Elemente
            j = i + 1
            While j <= lc
                If RemoveIfIdentical(Liste(i), Liste(j)) Then
                    Liste.RemoveAt(j)
                    lc = lc - 1
                End If
                j = j + 1
            End While
            i = i + 1
        End While
    End Sub

    Private Function RemoveIfIdentical(ByRef L1 As wb_Global.ZutatenListe, L2 As wb_Global.ZutatenListe) As Boolean

        'wenn beide Einträge identisch sind
        If (L1.Zutaten = L2.Zutaten) And (L1.Grp1 = L2.Grp1) And (L2.Grp2 = L2.Grp2) Then
            'Mengen addieren
            'TODO QUID-Mengen addieren
            L1.SollMenge += L2.SollMenge
            L1.SortMenge += L2.SortMenge
            'den zweiten Eintrag löschen
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub Sort()

    End Sub

    Public Sub DebugPrint()
        For Each x In Liste
            Debug.Print("Zutatenliste " & DirectCast(x, wb_Global.ZutatenListe).Zutaten & " " & DirectCast(x, wb_Global.ZutatenListe).SollMenge & "kg")
        Next
    End Sub
End Class
