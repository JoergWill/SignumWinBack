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

    Private Function RemoveIfIdentical(ByRef L1 As wb_Global.ZutatenListe, ByRef L2 As wb_Global.ZutatenListe) As Boolean
        'Markierung Fettdruck entfernen und merken
        L1.FettDruck = RemoveFettDruck(L1.Zutaten)
        L2.FettDruck = RemoveFettDruck(L2.Zutaten)

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

    Public Sub SplitIngredients()
        Dim lc As Integer = Liste.Count
        Dim i As Integer = 0
        Dim s As Integer = 0

        'Schleife über alle Elemente
        While i < lc
            s = Split(Liste(i), i)
            If s > 0 Then
                Liste.RemoveAt(i)
                i = i + (s - 1)
                lc = lc + (s - 1)
            End If
            i = i + 1
        End While
    End Sub

    Private Function Split(L1 As wb_Global.ZutatenListe, i As Integer) As Integer
        If L1.Zutaten.Contains(",") Then
            Dim a() As String = L1.Zutaten.Split(",")
            Dim z As wb_Global.ZutatenListe
            Dim FettDruckEin As Boolean = False

            For j = 1 To a.Length
                'Leerzeichnen vorne und hinten entfernen
                z.Zutaten = Trim(a(j - 1))
                z.SollMenge = L1.SollMenge
                z.SortMenge = j

                'Fettdruck
                If z.Zutaten.Contains("{") Then
                    FettDruckEin = True
                End If
                z.FettDruck = FettDruckEin
                If z.Zutaten.Contains("}") Then
                    FettDruckEin = False
                End If

                'Steuerzeichen vorne und hinten entfernen
                z.Zutaten = z.Zutaten.Trim("{")
                z.Zutaten = z.Zutaten.Trim("}")
                'Teilstring in Lise einfügen
                Liste.Insert(i + j, z)

            Next j
            Return a.Length
        Else
            Return 0
        End If
    End Function

    Private Function RemoveFettDruck(ByRef s As String) As Boolean
        If s.Contains("{") Then
            s.Trim("{")
            s.Trim("}")
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub Sort()

    End Sub

    Public Sub DebugPrint()
        For Each x In Liste
            If DirectCast(x, wb_Global.ZutatenListe).FettDruck Then
                Debug.Print("ZUTATENLISTE " & DirectCast(x, wb_Global.ZutatenListe).Zutaten.ToUpper & " " & DirectCast(x, wb_Global.ZutatenListe).SollMenge & "kg")
            Else
                Debug.Print("Zutatenliste " & DirectCast(x, wb_Global.ZutatenListe).Zutaten & " " & DirectCast(x, wb_Global.ZutatenListe).SollMenge & "kg")
            End If
        Next
    End Sub
End Class

'CREATE TABLE ENummern (
'  EN_Nr int(10) Not NULL Default '0',          E-Nummer als Integer
'  EN_Idx int(10) Not NULL Default '0',         E-Nummer Index (E400a...)
'  EN_Bezeichnung varchar(255) Default NULL,    Bezeichnung als Text (Riboflavin...)
'  EN_Name varchar(10) Default NULL,            E-Nummer als Text (E400)
'  EN_Beschreibung varchar(255) Default NULL,   Beschreibung der Wirkung, Herkunft, Herstellung
'  EN_Bemerkung varchar(255) Default NULL,      Bemerkung zum Inhalts-Stoffe (ungefährlich, giftig, verboten...)
'  EN_Key varchar(1) Default NULL,              Key-Word zur Steuerung (Anzeigen, Warnen...)
'  EN_CleanLabel varchar(1) Default NULL,       Kann in der Zutatenliste entfallen
'  EN_Timestamp timestamp(6) Not NULL,          Time-Stamp der letzten Änderung
'  PRIMARY KEY(EN_Nr, EN_Idx)
