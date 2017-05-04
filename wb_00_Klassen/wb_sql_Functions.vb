Imports MySql.Data.MySqlClient
Imports WinBack.wb_Global
Imports WinBack.wb_Sql_Selects

''' <summary>
''' Sammlung von Statischen SQL-Funktionen
''' </summary>
Public Class wb_sql_Functions

    ''' <summary>
    ''' MySql-Ping. Verbindung zur Datenbank öffnen und einen Ping absenden.
    ''' Wenn die Verbindung funktioniert wird True zurückgegeben.
    ''' </summary>
    ''' <returns>True - Wenn die Verbindung zur Datenbank funktioniert
    ''' False - Wenn keine Verbindung zur Datenbank aufgebaut werden kann</returns>
    Public Shared Function ping() As Boolean

        Select Case My.Settings.WinBackDBType
            Case wb_Sql.dbType.mySql
                Dim MySqlCon As New MySqlConnection(My.Settings.WinBackConString)
                Try
                    MySqlCon.Open()
                    If MySqlCon.Ping Then
                        MySqlCon.Close()
                        MySqlCon.Dispose()
                        Return True
                    Else
                        MySqlCon.Close()
                        MySqlCon.Dispose()
                        Return False
                    End If
                Catch ex As Exception
                    MySqlCon.Close()
                    MySqlCon.Dispose()
                    Return False
                End Try

            Case wb_Sql.dbType.msSql
                'Dim msCon As New SqlConnection(My.Settings.OrgaBackMainConString)
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Daten aus Datenbank Feld Hinweise2.H2_Memo in String einlesen.
    ''' </summary>
    ''' <param name="Typ">Hinweise - Hinweis-Type </param>
    ''' <param name="idx">Integer - Komponenten/Artikelnummer</param>
    ''' <returns></returns>
    Public Shared Function ReadHinweise(Typ As Hinweise, idx As Integer) As String
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        Dim Typ1, Typ2 As Integer

        'H2_Typ und H2_Typ2 ermitteln
        If GetTyp(Typ, Typ1, Typ2) Then
            'Daten aus MySQL-Memo in String einlesen
            winback.sqlSelect(setParams(sqlSelectH2, Typ1, Typ2, idx))
            If winback.Read Then
                Return winback.sField("H2_Memo")
            Else
                Return ""
            End If
            winback.Close()
        Else
            Return "Typ nicht definiert " & Typ
            Exit Function
        End If
    End Function

    ''' <summary>
    ''' Daten in Datenbank Feld Hinweise2.H2_Memo schreiben
    ''' Die Änderungs-Nummer wird automatisch hochgezählt.
    ''' Änderungsdatum ist das aktuelle Datum.
    ''' Wenn 
    ''' </summary>
    ''' <param name="Typ">Hinweise - Hinweis-Type </param>
    ''' <param name="idx">Integer - Komponenten/Artikelnummer</param>
    ''' <param name="h2">String - Inhalt </param>
    ''' <param name="UserNr">Integer - Benutzer-Nummer (Optional) </param>
    ''' <param name="UserName">String - Benutzer-Name (Optional) </param>
    ''' <returns>True - Wenn erfolgreich
    ''' False - bei Fehler</returns>
    Public Shared Function WriteHinweise(Typ As Hinweise, idx As Integer, h2 As String, Optional UserNr As Integer = 0, Optional UserName As String = "") As Boolean
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        Dim Typ1, Typ2 As Integer
        Dim Aenderung_Nr As Integer = 1

        'H2_Typ und H2_Typ2 ermitteln
        If GetTyp(Typ, Typ1, Typ2) Then

            'Prüfen ob es schon einen Datensatz gibt
            winback.sqlSelect(setParams(sqlSelectH2, Typ1, Typ2, idx))
            If winback.Read Then
                'Änderungs-Nummer auslesen
                Aenderung_Nr = winback.iField("H2_Aenderung_Nr")
                'um Eins erhöhen
                Aenderung_Nr += 1

                'Update Datensatz
                winback.CloseRead()
                Dim s As String
                s = "H2_Aenderung_Nr = " & CStr(Aenderung_Nr) & "," &
                        "H2_Aenderung_Datum = '" & Date.Today.ToShortDateString & "'," &
                        "H2_Aenderung_User = " & CStr(UserNr) & "," &
                        "H2_Aenderung_Name = '" & UserName & "'," &
                        "H2_Memo = " & "'" & h2 & "'"
                winback.sqlCommand(setParams(sqlUpdateH2, Typ1, Typ2, idx, s))

                winback.Close()
                Return True
                Exit Function
            End If

            'Datensatz Insert
            winback.CloseRead()
            Dim s1, s2 As String
            s1 = "H2_Aenderung_Nr, H2_Aenderung_Datum, H2_Aenderung_User, H2_Aenderung_Name, H2_Memo"
            s2 = CStr(Aenderung_Nr) & ",'" & Date.Today.ToShortDateString & "'," & CStr(UserNr) & ",'" & UserName & "','" & h2 & "'"
            winback.sqlCommand(setParams(sqlInsertH2, Typ1, Typ2, idx, s1, s2))
            winback.Close()

            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Daten aus Datenbank Feld Hinweise2.H2_Memo löschen.
    ''' </summary>
    ''' <param name="Typ">Hinweise - Hinweis-Type </param>
    ''' <param name="idx">Integer - Komponenten/Artikelnummer</param>
    ''' <returns></returns>
    Public Shared Function DeleteHinweise(Typ As Hinweise, idx As Integer) As Boolean
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        Dim Typ1, Typ2 As Integer

        'H2_Typ und H2_Typ2 ermitteln
        If GetTyp(Typ, Typ1, Typ2) Then
            'alle Datensätze löschen
            winback.sqlCommand(setParams(sqlDeleteH2, Typ1, Typ2, idx))
            winback.Close()
            Return True
        Else
            Return False
        End If

    End Function

    Private Shared Function GetTyp(Typ As Hinweise, ByRef Typ1 As Integer, ByRef Typ2 As Integer) As Boolean
        Select Case Typ
            Case Hinweise.RezeptHinweise
                Typ1 = 2
                Typ2 = 0

            Case Hinweise.ArtikelHinweise
                Typ1 = 3
                Typ2 = 0

            Case Hinweise.UserInfo               '4/0/UsrNr
                Typ1 = 4
                Typ2 = 0

            Case Hinweise.ZutatenListe           '9/1/ArtNr
                Typ1 = 9
                Typ2 = 1

            Case Hinweise.MehlZusammensetzung    '9/2/ArtNr
                Typ1 = 9
                Typ2 = 2

            Case Hinweise.GebCharakteristik     '10/1/ArtNr
                Typ1 = 10
                Typ2 = 1

            Case Hinweise.Verzehrtipps          '10/2/ArtNr
                Typ1 = 10
                Typ2 = 2

            Case Hinweise.Wissenswertes         '10/3/ArtNr
                Typ1 = 10
                Typ2 = 3

            Case Hinweise.DeklBezRohstoff       '11/0/RohNr  
                Typ1 = 11
                Typ2 = 0

            Case Hinweise.MessageTextLinie      '20/0/LNr
                Typ1 = 20
                Typ2 = 0

            Case Hinweise.MessageTextUser       '20/1/UsrNr
                Typ1 = 20
                Typ2 = 1

            Case Hinweise.NaehrwertUpdate
                Typ1 = 21
                Typ2 = 0

            Case Else
                Return False
                Exit Function
        End Select
        Return True
    End Function
End Class

'CREATE TABLE winback.Hinweise2 (
'    H2_Typ int(10) Not NULL Default 0,
'    H2_Typ2 int(10) Not NULL Default 0,
'    H2_Id1 int(10) Not NULL Default 0,
'    H2_Id2 int(10) Not NULL Default 0,
'    H2_Aenderung_Nr int(10) Default 0,
'    H2_Aenderung_Datum datetime Default NULL,
'    H2_Aenderung_User int(10) Default -1,
'    H2_Aenderung_Name varchar(24) Default NULL,
'    H2_Text1 varchar(50) Default NULL,
'    H2_Text2 varchar(50) Default NULL,
'    H2_Memo text Default NULL,
'    H2_Timestamp timestamp(14),
'    PRIMARY KEY (H2_Typ, H2_Typ2, H2_Id1, H2_Id2)
')
'TYPE = MYISAM
