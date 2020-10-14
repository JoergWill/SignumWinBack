Imports System.Text.RegularExpressions

Public Class wb_ZutatenListe
    Public Liste As New List(Of wb_ZutatenElement)

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

    Public Function Print(ShowENummern As Boolean) As String
        Dim s As String = ""
        Dim z As String
        Dim GrpRezNr As Integer = 0

        For Each x As wb_ZutatenElement In Liste

            If Not ShowENummern And x.eNr > 0 Then
                z = wb_ZutatenListe_Global.Find_ENummer(x.Zutaten).Bezeichnung
            Else
                z = x.Zutaten
            End If

            If x.FettDruck Then
                z = z.ToUpper
            End If

            If x.GrpRezNr <> GrpRezNr And GrpRezNr <> 0 Then
                s = s & ")"
                GrpRezNr = 0
            End If

            If x.GrpRezNr > 0 And GrpRezNr = 0 Then
                z = z & "("
                GrpRezNr = x.GrpRezNr
            End If

            If s = "" Then
                s = z
            ElseIf s.EndsWith("(") Then
                s = s & z
            Else
                s = s & ", " & z
            End If
        Next

        'letzter Eintrag einer RezeptGruppe
        If GrpRezNr > 0 Then
            s = s & ")"
        End If

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
            For Each x As wb_ZutatenElement In Liste
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
                Else
                    j = j + 1
                End If
            End While
            i = i + 1
        End While
    End Sub

    Private Function RemoveIfIdentical(ByRef L1 As wb_ZutatenElement, ByRef L2 As wb_ZutatenElement) As Boolean
        'Markierung Fettdruck entfernen und merken
        L1.FettDruck = RemoveFettDruck(L1.Zutaten) Or L1.FettDruck
        L2.FettDruck = RemoveFettDruck(L2.Zutaten) Or L2.FettDruck

        'wenn beide Einträge identisch sind
        '        If (L1.Zutaten = L2.Zutaten) And (L1.Grp1 = L2.Grp1) And (L2.Grp2 = L2.Grp2) Then  TEST(Gruppen werden nicht geprüft)
        If (L1.Zutaten = L2.Zutaten) Then
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
            'Gruppen übernehmen wenn vorhanden
            If L1.Grp1 = 0 Then
                L2.Grp1 = L2.Grp1
            End If
            If L1.Grp2 = 0 Then
                L2.Grp2 = L2.Grp2
            End If

            'den zweiten Eintrag löschen
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RemoveFettDruck(ByRef s As String) As Boolean
        If s = Nothing Or s = "" Or (s.IndexOf(",") > 0) Then
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

    Private Function _Split_ingredients(ByVal L1 As wb_ZutatenElement, i As Integer) As Integer
        If L1.Zutaten IsNot Nothing And L1.Zutaten <> "" Then

            'Filtert Prozent-Angaben aus der Zutatenliste
            L1.Zutaten = FilterDezimalProzent(L1.Zutaten)
            'Semikolon durch Komma ersetzen
            L1.Zutaten = L1.Zutaten.Replace(";", ",")

            'Filtert eingebettete kommagetrennte Zutatenlisten in Klammern aus
            L1.Zutaten = FilterKlammer(L1.Zutaten)

            'Die Zutatenliste besteht aus mehreren Elementen
            If L1.Zutaten.Contains(",") Then
                Dim a() As String = L1.Zutaten.Split(",")
                Dim FettDruckEin As Boolean = False

                'Schleife über alle Elemente der Liste
                For j = 1 To a.Length
                    'Ein einzelnes Element der Zutatenliste
                    Dim z As New wb_ZutatenElement
                    z.CopyFrom(L1)
                    'Leerzeichnen vorne und hinten entfernen
                    z.Zutaten = Trim(a(j - 1))
                    'Reihenfolge der Zutaten
                    z.SortMenge = a.Length - j

                    'String optimieren
                    FettDruckEin = _Opt_Ingredients(z, FettDruckEin)
                    'Teilstring in Liste einfügen
                    Liste.Insert(i + j, z)

                Next j
                Return a.Length
            Else
                'Ein einzelnes Element der Zutatenliste
                Dim z As New wb_ZutatenElement
                'einziges Element der Zutatenliste
                z.CopyFrom(L1)
                z.SortMenge = 1
                _Opt_Ingredients(z, False)
                'Teilstring in Liste ersetzen
                Liste.RemoveAt(i)
                Liste.Insert(i, z)
                Return 0
            End If
        Else
            Return 0
        End If
    End Function

    Private Function _Opt_Ingredients(ByRef z As wb_ZutatenElement, FettDruckEin As Boolean) As Boolean
        'Fettdruck
        z.FettDruck = FettDruckEin Or z.Zutaten.Contains("{") Or z.FettDruck
        'Rückgabewert (Fettdruck bleibt aktiv)
        _Opt_Ingredients = (FettDruckEin Or z.Zutaten.Contains("{")) And Not z.Zutaten.Contains("}")

        'Steuerzeichen vorne und hinten/im String entfernen
        z.Zutaten = z.Zutaten.Trim("{")
        'z.Zutaten = z.Zutaten.Trim("}")
        z.Zutaten = z.Zutaten.Replace("}", "")
    End Function

    ''' <summary>
    ''' Wandelt alle Komma-getrennten Prozent-Angaben im String in Dezimal-Punkte um.
    ''' Die ist notwendig, da die einzelnen Zutaten Komma-getrennt aufgeführt sind und damit
    ''' die Prozent-Angaben die Sortierung/Trennung der Zutaten beeinflussen
    ''' 
    '''     3,5% Milch      3.5% Milch
    '''     Ei 99,9 %       Ei 99.9%
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Private Function FilterDezimalProzent(s As String) As String
        'Falls vor dem %-Zeichen ein Leerzeichen steht
        s = s.Replace(" %", "%")
        'für alle %-Angaben im String
        Dim p As Integer = 0
        Do
            p = _FilterDezimalProzent(s, p)
        Loop Until p <= 0
        'Ergebnis-String mit Dezimalpunkt in den Prozent-Angaben
        Return s
    End Function

    Private Function _FilterDezimalProzent(ByRef s As String, k As Integer) As Integer
        'Position des %-Zeichens im String
        Dim i As Integer = s.IndexOf("%", k)

        'Prüfen ob überhaupt ein Prozent-Zeichen enthalten ist
        If i > 0 Then
            'ab diesem Zeichen bis zum String-Anfang
            For j = i To k Step -1
                'Sonderzeichen und % ausfiltern
                Select Case s.Substring(j, 1)
                    Case "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "%"
                        'Do nothing
                    Case ","
                        s = s.Remove(j, 1).Insert(j, ".")
                    Case Else
                        Exit For
                End Select
            Next
        End If
        Return i + 1
    End Function

    ''' <summary>
    ''' Filtert Zutatenlisten, die in Klammern gesetzt sind als eigenständiges Zutaten-Element aus. 
    ''' Die Klammern werden durch Semikolon ersetzt, damit wird dieses Element nicht aufgesplittet.
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Private Function FilterKlammer(s As String) As String
        'Falls vor dem C-Zeichen ein Leerzeichen steht
        s = s.Replace(" (", "(")
        'für alle Zutaten-Elemente im String
        Dim p As Integer = 0
        Do
            p = _FilterKlammer(s, p)
        Loop Until p <= 0
        'Ergebnis-String mit Dezimalpunkt in den Prozent-Angaben
        Return s
    End Function

    Private Function _FilterKlammer(ByRef s As String, p As Integer) As Integer
        'Position des %-Zeichens im String
        Dim i As Integer = s.IndexOf("(", p)
        Dim j As Integer = 0
        Dim k As Integer = 0

        'Prüfen ob überhaupt ein Klammer-Zeichen enthalten ist
        If i > 0 Then
            'ab diesem Zeichen bis zum String-Ende
            For j = i To s.Length - 1
                'Sonderzeichen und % ausfiltern
                Select Case s.Substring(j, 1)
                    Case ","
                        'Komma wird durch Semikolon ersetzt
                        s = s.Remove(j, 1).Insert(j, "\")
                    Case ("(")
                        'verschachtelte Klammern
                        k += 1
                    Case ")"
                        'geschlossene Klammer beendet die Routine
                        k -= 1
                        If k = 0 Then
                            Exit For
                        End If
                End Select
            Next
        End If
        Return j
    End Function

    Public Sub Convert_ToEnr()
        For i = 0 To Liste.Count - 1
            'TODO geklammerte Zutatenlisten in einzelne Bestandteile aufteilen und gruppieren
            'geklammerte Zutatenlisten werden nicht in E-Nummern aufgeteilt
            If Not Liste(i).Zutaten.Contains("\") Then
                _Convert_ToENr(Liste(i))
            End If
        Next
    End Sub

    Private Sub _Convert_ToENr(ByRef L As wb_ZutatenElement)
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
        'Leere Einträg entfernen
        _DelEmpty()
        'Sortieren nach Sollmenge im Rezept
        Liste.Sort()
    End Sub

    Private Sub _DelEmpty()
        'leere Einträge entfernen
        Dim lc As Integer = Liste.Count - 1
        For i = 0 To lc
            If i < Liste.Count Then
                If Liste(i).Zutaten = "" Then
                    Liste.RemoveAt(i)
                    lc -= 1
                    If i < lc Then Exit For
                End If
            End If
        Next
    End Sub

    Public Sub DebugPrint()
        For Each x In Liste
            If x.FettDruck Then
                Debug.Print("ZUTATENLISTE " & x.Zutaten.ToUpper & " " & x.SollMenge & "kg" & "/" & x.GrpRezNr)
            Else
                Debug.Print("Zutatenliste " & x.Zutaten & " " & x.SollMenge & "kg" & "/" & x.GrpRezNr)
            End If
        Next
    End Sub
End Class

Public Class wb_ZutatenElement
    Implements IComparable

    Public Zutaten As String = ""
    Public eNr As Integer
    Public FettDruck As Boolean
    Public KeineDeklaration As Boolean
    Public Aufloesen As Boolean
    Public GrpRezNr As Integer
    Public SollMenge As Double
    Public SortMenge As Double
    Public Grp1 As Integer
    Public Grp2 As Integer
    Public Quid As Boolean
    Public QuidProzent As Double


    Public Sub CopyFrom(z As wb_ZutatenElement)
        Zutaten = Trim(z.Zutaten)
        eNr = z.eNr
        FettDruck = z.FettDruck
        KeineDeklaration = z.KeineDeklaration
        Aufloesen = z.Aufloesen
        GrpRezNr = z.GrpRezNr
        SollMenge = z.SollMenge
        SortMenge = z.SollMenge
        Grp1 = z.Grp1
        Grp2 = z.Grp2
        Quid = z.Quid
        QuidProzent = z.QuidProzent
    End Sub

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        'Vergleich
        Dim o As wb_ZutatenElement = obj

        'Sollwerte vergleichen
        If SollMenge < o.SollMenge Then
            Return 1
        ElseIf SollMenge > o.SollMenge Then
            Return -1
        Else
            'Sollmengen sind gleich - Sortierfolge nach Reihenfolge der Zutaten
            If SortMenge < o.SortMenge Then
                Return 1
            ElseIf SortMenge > o.SortMenge Then
                Return -1
            Else
                Return 0
            End If
        End If
    End Function
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
