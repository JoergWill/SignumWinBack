Imports WinBack.wb_Global

Public Class wb_Konfig

    Shared Language As String
    Public Shared TexteTabelle As New Hashtable

    Public Shared Filiale As New wb_Filiale

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
    ''' Liest die zuletzt gespeicherte Fenster-Position aus der winback.ini und setzt die entsprechenden 
    ''' Parameter im übergebenen Fenster.
    ''' Der File-Name der letzten aktuellen Dock-Bar-Konfiguration wird im Tag-Objekt gespeichert !!
    ''' </summary>
    ''' <param name="xForm"></param>
    ''' <param name="IniSektion"></param>
    Public Shared Sub SetFormBoundaries(xForm As Windows.Forms.Form, IniSektion As String)
        Dim IniFile As New WinBack.wb_IniFile

        'Fensterposition aus winback.ini
        xForm.Top = IniFile.ReadInt(IniSektion, "Top")
        xForm.Left = IniFile.ReadInt(IniSektion, "Left")
        xForm.Width = IniFile.ReadInt(IniSektion, "Width")
        xForm.Height = IniFile.ReadInt(IniSektion, "Height")

        'Dispose
        IniFile = Nothing
    End Sub

    Public Shared Sub SaveFormBoundaries(Top As Integer, Left As Integer, Width As Integer, Height As Integer, LayoutFile As String, IniSektion As String)
        Dim IniFile As New WinBack.wb_IniFile

        'Fensterposition in winback.ini sichern
        IniFile.WriteInt(IniSektion, "Top", Top)
        IniFile.WriteInt(IniSektion, "Left", Left)
        IniFile.WriteInt(IniSektion, "Width", Width)
        IniFile.WriteInt(IniSektion, "Height", Height)

        'Layout-File
        IniFile.WriteString(IniSektion, "LayoutFileName", LayoutFile)

        'Dispose
        IniFile = Nothing
    End Sub
End Class
