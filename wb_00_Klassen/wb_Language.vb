Public Class wb_Language

    Shared Language As String
    Public Shared TexteTabelle As New Hashtable

    Public Shared Sub SetLanguage(Lang As String)
        Dim IniFile As New wb_IniFile
        If Lang = "" Then
            Language = IniFile.ReadString("winback", "Language", "de-DE")
        Else
            Language = Lang
            IniFile.WriteString("winback", "Language", Language)
        End If
        My.Settings.AktLanguage = Language
    End Sub

    Public Shared Function GetLanguage() As String
        Try
            Language = My.Settings.AktLanguage
        Catch
            Language = "de-DE"
        End Try
        Return Language
    End Function

    ''' <summary>
    ''' Zuordnung ISO-Sprache zu WinBack-Sprache-Nr
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetLanguageNr() As String
        Select Case Left(Language, 2)
            Case "de"       'Deutsch
                Return "0"
            Case "hu"       'Ungarisch
                Return "1"
            Case "nl"       'Niederländisch
                Return "2"
            Case "en"       'Englisch(US)
                Return "3"
            Case "pt"       'Portugisisch
                Return "4"
            Case "sl"       'Slovenisch
                Return "5"
            Case "ru"       'Russisch
                Return "6"
            Case "fr"       'Französisch
                Return "7"
            Case "es"       'Spanisch
                Return "8"
            Case "sk"       'Slovakisch
                Return "9"
            Case "ro"       'Rumänisch
                Return "10"
            Case Else
                Return 0
        End Select
    End Function

    ''' <summary>
    ''' Hash-Table mit den Übersetzungen für die jeweilige Sprache vorladen. Die HashTable übersetzt
    ''' die Texte aus der WinBack-Datenbank die mit @[x,y] gekennzeichnet sind in die jeweilige Landes-Sprache
    ''' </summary>
    ''' <param name="Sprache"></param>
    Public Shared Sub LoadTexteTabelle(Sprache As String)
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlWinBackTxte, Sprache))
        TexteTabelle.Clear()
        While winback.Read
            TexteTabelle.Add("@[" & winback.sField("T_Typ") & "," & winback.sField("T_TextIndex") & "]", winback.sField("T_Text"))
        End While
        winback.Close()
    End Sub

    ''' <summary>
    '''Text aus Datenbank lesen - Übersetzung
    ''' von Herbert Bsteh aus winback (Kylix)
    ''' Erste Zahl (Texttyp), zweite Zahl (Textindex)
    '''
    ''' Gibt den Text ohne Klammer zurück wenn
    ''' kein Text in der Datenbank gefunden wurde
    ''' </summary>
    ''' <param name="Text">String im Format @[Typ,Index]</param>
    ''' <returns>String - Übersetzung aus winback.Texte</returns>
    Public Shared Function TextFilter(Text As String) As String
        Dim Hash As String

        If Len(Text) > 6 Then
            If Left(Text, 2) = "@[" Then
                Hash = Left(Text, InStr(Text, "]"))
                Try
                    If wb_Language.TexteTabelle.ContainsKey(Hash) Then
                        Return wb_Language.TexteTabelle(Hash).ToString
                    End If
                Catch
                End Try
                Return Mid(Text, Len(Hash) + 1)
            End If
        End If
        Return Text
    End Function
End Class
