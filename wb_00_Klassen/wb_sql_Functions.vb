'14.06.2016/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Sammlung von Statischen SQL-Funktionen
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports WinBack.wb_Global

Public Class wb_sql_Functions
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

    Public Shared Function ReadHinweise(Typ As Hinweise, idx As Integer) As String
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        Dim Typ1, Typ2 As Integer

        Select Case Typ
            Case Hinweise.RezeptHinweise
                Typ1 = 2
                Typ2 = 0

            Case Hinweise.ArtikelHinweise
                Typ1 = 3
                Typ2 = 0

            Case Else
                Return "Typ nicht definiert " & Typ
                Exit Function
        End Select

        winback.sqlSelect("SELECT H2_Memo FROM Hinweise2 WHERE H2_Typ=" & CStr(Typ1) & " AND H2_Typ2=" & CStr(Typ2) & " AND H2_Id1=" & CStr(idx))
        If winback.Read Then
            Return winback.sField("H2_Memo")
        Else
            Return ""
        End If
        winback.Close()

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
