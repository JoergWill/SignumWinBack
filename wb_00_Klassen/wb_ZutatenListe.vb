Imports System.Text.RegularExpressions

Public Class wb_ZutatenListe
    Public Liste As New ArrayList

    Public Sub Clear()
        Liste.Clear()
    End Sub

    Public Sub Opt()
        Del_Doubletten()
        Split_Ingredients()
        Convert_ToEnr()
        Del_Doubletten()
        Sort()
    End Sub

    Public Function Print(Mode As wb_Global.ZutatenListeMode) As String
        Dim s As String = ""
        Dim z As String

        For Each x As wb_Global.ZutatenListe In Liste

            If Mode = wb_Global.ZutatenListeMode.Hide_ENummer And x.eNr > 0 Then
                z = wb_ZutatenListe_Global.Find_ENummer(x.Zutaten).Text
            Else
                z = x.Zutaten
            End If

            If x.FettDruck Then
                z = z.ToUpper
            End If

            If s = "" Then
                s = z
            Else
                s = s & ", " & z
            End If
        Next

        'falls noch fehlerhafte Einträge vorhanden sind, jetzt entfernen
        s = s.Replace(",,", ",")
        s = s.Replace(", ,", ",")

        Return s
    End Function

    ''' <summary>
    ''' Erzeugt aus der Zutatenliste die Mehlzusammensetzung in Prozent als String.
    ''' 
    ''' Die (flache) Zutatenliste wird einzeln durchlaufen und alle Sollwerte der Rohstoffe,
    ''' die zu einer Rohstoff-Gruppe mit Kennung 'Deklarieren' gehören, werden addiert.
    ''' 
    ''' Am Ende wird der prozentuale Anteil der Mengen berechnet und sortiert als String
    ''' ausgegeben.
    ''' </summary>
    ''' <param name="TrennZeichen"></param>
    ''' <returns></returns>
    Public Function PrintMehlZusammenSetzung(TrennZeichen As String) As String
        'Gesamtsummer aller Werte in allen Gruppen
        Dim Summe As Double = 0
        'Ergebnis-String Menge pro Rohstoff-Gruppe in Prozent
        Dim Result As String = ""

        'Schleife über alle Rohstoff-Gruppen mit Flag Deklaration
        For Each MehlGruppe As wb_MehlGruppe In wb_Rohstoffe_Shared.MehlGruppe

            'Am Anfang der Schleife Menge auf Null setzen
            MehlGruppe.ClearMenge()

            'Schleife über alle ZutatenListen
            For Each x As wb_Global.ZutatenListe In Liste
                'Rohstoff in der Zutatenliste gehört zu dieser Gruppe(Mehlsorte)
                If x.Grp1 = MehlGruppe.GruppeNr Or x.Grp2 = MehlGruppe.GruppeNr Then
                    MehlGruppe.Add(x.SollMenge)
                End If
            Next

            'Summe aller Gruppen - Notwendig zur Berechnung des prozentualen Anteils
            Summe += MehlGruppe.MengeGesKg
        Next

        'Ergebnisliste sortieren nach Mengen-Anteil
        wb_Rohstoffe_Shared.MehlGruppe.Sort()

        'String aus allen verwendeten Mehlsorten erzeugen, getrennt durch Trennzeichen
        For Each MehlGruppe As wb_MehlGruppe In wb_Rohstoffe_Shared.MehlGruppe
            Result = MehlGruppe.GetResultString(Result, TrennZeichen, Summe)
        Next
        Return Result
    End Function

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
        L1.FettDruck = RemoveFettDruck(L1.Zutaten) Or L1.FettDruck
        L2.FettDruck = RemoveFettDruck(L2.Zutaten) Or L2.FettDruck

        'wenn beide Einträge identisch sind
        If (L1.Zutaten = L2.Zutaten) And (L1.Grp1 = L2.Grp1) And (L2.Grp2 = L2.Grp2) Then
            'Mengen addieren
            'TODO QUID-Mengen addieren
            L1.SollMenge += L2.SollMenge
            L1.SortMenge += L2.SortMenge
            'Fettdruck-Bit übernehmen
            L1.FettDruck = L1.FettDruck Or L2.FettDruck
            'E-Nummer falls vorhanden
            If L1.eNr = 0 Then
                L1.eNr = L2.eNr
            End If
            'den zweiten Eintrag löschen
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RemoveFettDruck(ByRef s As String) As Boolean
        If s = Nothing Or s = "" Then
            Return False
        End If
        If Left(s, 1) = "*" Then
            s = s.Trim("*")
            s = s.Trim("{")
            s = s.Trim("}")
            Return True
        End If
        If s.Contains("{") Then
            s = s.Trim("{")
            s = s.Trim("}")
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub Split_Ingredients()
        Dim lc As Integer = Liste.Count
        Dim i As Integer = 0
        Dim s As Integer = 0

        'Schleife über alle Elemente
        While i < lc
            s = _Split_ingredients(Liste(i), i)
            If s > 0 Then
                Liste.RemoveAt(i)
                i = i + (s - 1)
                lc = lc + (s - 1)
            End If
            i = i + 1
        End While
    End Sub

    Private Function _Split_ingredients(L1 As wb_Global.ZutatenListe, i As Integer) As Integer
        If L1.Zutaten IsNot Nothing And L1.Zutaten <> "" Then
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
                    'Teilstring in Liste einfügen
                    Liste.Insert(i + j, z)

                Next j
                Return a.Length
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function

    Public Sub Convert_ToEnr()
        For i = 0 To Liste.Count - 1
            _Convert_ToENr(Liste(i))
        Next
    End Sub

    Private Sub _Convert_ToENr(ByRef L As wb_Global.ZutatenListe)
        Dim eNr As String
        Dim e As wb_Global.ENummern = wb_ZutatenListe_Global.EmptyENumber

        eNr = Get_ENummer(L.Zutaten)
        If eNr <> "" Then
            e = wb_ZutatenListe_Global.Find_ENummer(eNr)
        Else
            e = wb_ZutatenListe_Global.Find_EBezeichnung(L.Zutaten)
        End If

        If e.Nr > 0 Then
            L.Zutaten = e.Text
            L.eNr = e.Nr
        End If
    End Sub

    Public Function Get_ENummer(s As String) As String
        If s IsNot Nothing And s <> "" Then
            Dim rgx As New Regex("E\d{4}[a-z)]|E\d{4}|E \d{4}[a-z]|E \d{4}|E\d{3}[a-z)]|E\d{3}|E \d{3}[a-z]|E \d{3}")
            Dim x As String = rgx.Match(s).Value
            Return x.Replace(" ", "")
        Else
            Return ""
        End If
    End Function

    Public Sub Sort()
        'leere Einträge entfernen
        Dim lc As Integer = Liste.Count - 1
        For i = 0 To lc
            If i < Liste.Count Then
                If DirectCast(Liste(i), wb_Global.ZutatenListe).Zutaten = "" Then
                    Liste.RemoveAt(i)
                    lc -= 1
                    If i < lc Then Exit For
                End If
            End If
        Next
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
