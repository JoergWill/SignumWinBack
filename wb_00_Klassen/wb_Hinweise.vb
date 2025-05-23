﻿Imports WinBack.wb_Global
Imports WinBack.wb_Sql_Selects
''' <summary>
''' Eigene Klasse für winback.Hinweise2.
''' Aus wb_sql_Function herausfaktorieren!
''' </summary>
Public Class wb_Hinweise

    Private H2_Typ As Integer = 0
    Private H2_Typ2 As Integer = 0
    Private H2_Id1 As Integer = 0
    Private H2_Id2 As Integer = 0
    Private H2_Aenderung_Nr As Integer = 0
    Private H2_Aenderung_Datum As Date
    Private H2_UserNr As Integer = 0
    Private H2_UserName As String
    Private H2_Memo As String
    Private H2_ReadOK As Boolean = False

    Public ReadOnly Property Typ As Integer
        Get
            Return H2_Typ
        End Get
    End Property

    Public ReadOnly Property Typ2 As Integer
        Get
            Return H2_Typ2
        End Get
    End Property

    Public Property KompNr As Integer
        Get
            Return H2_Id1
        End Get
        Set(value As Integer)
            H2_Id1 = value
        End Set
    End Property

    Public Property ArtikelNr As Integer
        Get
            Return H2_Id1
        End Get
        Set(value As Integer)
            H2_Id1 = value
        End Set
    End Property

    Public Property Memo As String
        Get
            Return H2_Memo
        End Get
        Set(value As String)
            H2_Memo = wb_Functions.XRemoveSonderZeichen(value)
        End Set
    End Property

    Public ReadOnly Property Memo(Idx As Integer) As String
        Get
            If Not ReadOK Then
                Read(Idx)
            End If
            Return H2_Memo
        End Get
    End Property

    Public ReadOnly Property Aenderung_Datum As Date
        Get
            Return H2_Aenderung_Datum
        End Get
    End Property

    Public ReadOnly Property ReadOK As Boolean
        Get
            Return H2_ReadOK
        End Get
    End Property

    ''' <summary>
    ''' Objekt initialisieren. Die Daten werden nicht gelesen.
    ''' </summary>
    ''' <param name="DataTyp"></param>
    Public Sub New(DataTyp As Hinweise)
        'H2_Typ und H2_Typ2 ermitteln
        GetTyp(DataTyp, H2_Typ, H2_Typ2)
        'Daten sind noch nicht gelesen
        H2_ReadOK = False
    End Sub

    ''' <summary>
    ''' Objekt initialiseren. Die entsprechenden Daten werden gelesen
    ''' </summary>
    ''' <param name="DataTyp">Datentyp</param>
    ''' <param name="idx">Rohstoff/Artikel/Rezeptnummer</param>
    Public Sub New(DataTyp As Hinweise, idx As Integer, Optional idx2 As Integer = wb_Global.UNDEFINED)
        'H2_Typ und H2_Typ2 ermitteln
        If GetTyp(DataTyp, H2_Typ, H2_Typ2) Then
            H2_ReadOK = Read(idx, idx2)
        End If
    End Sub

    ''' <summary>
    ''' Daten aus Datenbank winback.Hinweise2 einlesen.
    ''' </summary>
    ''' <param name="idx">Integer - Komponenten/Artikelnummer</param>
    ''' <returns></returns>
    Public Function Read(idx As Integer, Optional idx2 As Integer = wb_Global.UNDEFINED) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'Artikel/Komponenten/Rezeptnummer merken
        H2_Id1 = idx
        H2_Id2 = idx2

        'Daten aus MySQL-Memo in String einlesen
        H2_ReadOK = False
        If idx2 > wb_Global.UNDEFINED Then
            winback.sqlSelect(setParams(sqlSelectH2X, H2_Typ, H2_Typ2, idx, idx2))
        Else
            winback.sqlSelect(setParams(sqlSelectH2, H2_Typ, H2_Typ2, idx))
        End If

        If winback.Read Then
            H2_Aenderung_Nr = winback.sField("H2_Aenderung_Nr")
            H2_UserNr = winback.iField("H2_Aenderung_User")
            H2_UserName = winback.sField("H2_Aenderung_Name")
            H2_Memo = winback.sField("H2_Memo")
            H2_Id1 = idx
            Try
                H2_Aenderung_Datum = winback.sField("H2_Aenderung_Datum")
            Catch ex As Exception
            End Try
            H2_ReadOK = True
        End If
        winback.Close()
        Return H2_ReadOK
    End Function

    ''' <summary>
    ''' Daten in Datenbank winback.Hinweise2 schreiben
    ''' Die Änderungs-Nummer wird automatisch hochgezählt.
    ''' Änderungsdatum ist das aktuelle Datum.
    ''' </summary>
    ''' <returns>True - Wenn erfolgreich
    ''' False - bei Fehler</returns>
    Public Function Write() As Boolean
        'Index muss gesetzt sein
        If (H2_Id1 > 0) Then

            Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
            Dim s1, s2 As String

            'Vor Insert/Update müssen die Daten gelesen werden
            If Not H2_ReadOK Then
                winback.sqlSelect(setParams(sqlSelectH2, H2_Typ, H2_Typ2, H2_Id1))
                If winback.Read Then
                    H2_Aenderung_Nr = winback.sField("H2_Aenderung_Nr")
                    H2_ReadOK = True
                End If
                winback.CloseRead()
            End If

            'Änderungsdatum ist das aktuelle Datum
            H2_Aenderung_Datum = Date.Now
            'aktuellen Benutzer NUmmer/Name eintragen
            H2_UserNr = wb_GlobalSettings.AktUserNr
            H2_UserName = wb_GlobalSettings.AktUserName

            'Daten sind gelesen/Datensatz vorhanden
            If H2_ReadOK Then
                'Änderung-Nummer um Eins nach oben zählen
                H2_Aenderung_Nr += 1
                'Daten in Memo-Feld speichern (Update)
                s1 = "H2_Aenderung_Nr = " & CStr(H2_Aenderung_Nr) & ", H2_Aenderung_Datum = '" & wb_sql_Functions.MySQLdatetime(H2_Aenderung_Datum) & "'," &
                 "H2_Aenderung_User = " & CStr(H2_UserNr) & ",H2_Aenderung_Name = '" & H2_UserName & "'," &
                 "H2_Memo = " & "'" & H2_Memo & "'"
                winback.sqlCommand(setParams(sqlUpdateH2, H2_Typ, H2_Typ2, H2_Id1, s1))
                winback.Close()
            Else
                'Änderung-Nummer Eins
                H2_Aenderung_Nr = 1
                'Daten in Memo-Feld speichern (Insert)
                s1 = "H2_Aenderung_Nr, H2_Aenderung_Datum, H2_Aenderung_User, H2_Aenderung_Name, H2_Memo"
                s2 = CStr(H2_Aenderung_Nr) & ",'" & wb_sql_Functions.MySQLdatetime(H2_Aenderung_Datum) & "'," & CStr(H2_UserNr) & ",'" & H2_UserName & "','" & H2_Memo & "'"
                winback.sqlCommand(setParams(sqlInsertH2, H2_Typ, H2_Typ2, H2_Id1, s1, s2))
                winback.Close()
            End If
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Löscht den entsprechenden Eintrag in der Datenbank
    ''' </summary>
    ''' <returns></returns>
    Public Function Delete() As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'alle Datensätze löschen
        Dim i As Integer = winback.sqlCommand(setParams(sqlDeleteH2, H2_Typ, H2_Typ2, H2_Id1))
        winback.Close()
        Return (i > 0)
    End Function

    ''' <summary>
    ''' Setzt alle Variablen wieder auf Null,Nothing oder Undefined.
    ''' Wird aufgerufen, wenn eine neue(andere) Komponente geladen werden soll
    ''' </summary>
    Public Sub Invalid()
        H2_ReadOK = False
        H2_Memo = ""
    End Sub

    ''' <summary>
    ''' Zuordnung von Hinweis.Datentyp zu H2_Typ,H2_Typ2 in der Datenbank winback.Hinweise2
    ''' 
    '''         Datentyp              H2_Typ  H2_Typ2   H2_Id2
    '''         ===================================================
    '''         RezeptHinweise          2       0       RezepNr
    '''         ArtikelHinweise         3       0       ArtikelNr
    '''         UserInfo                4       0       UserNr
    '''         Konfiguration           5       0       KonfigId
    '''         ZutatenListe            9       1       ArtikelNr
    '''         MehlZusammensetzung     9       2       ArtikelNr
    '''         GebCharakteristik      10       1       ArtikelNr
    '''         Verzehrtipps           10       2       ArtikelNr
    '''         Wissenswertes          10       3       ArtikelNr
    '''         DeklBezRohstoff        11       0       RohstoffNr  
    '''         DeklBezRohstoffIntern  11       1       RohstoffNr  
    '''         MessageTextLinie       20       0       LinienNr
    '''         MessageTextUser        20       1       UserNr
    '''         NaehrwertUpdate        21       0       RohstoffNr
    '''         
    ''' </summary>
    ''' <param name="DataTyp"></param>
    ''' <param name="Typ1"></param>
    ''' <param name="Typ2"></param>
    ''' <returns></returns>
    Private Shared Function GetTyp(DataTyp As Hinweise, ByRef Typ1 As Integer, ByRef Typ2 As Integer) As Boolean
        Select Case DataTyp
            Case Hinweise.RezeptHinweise        '2/0/RzNr
                Typ1 = 2
                Typ2 = 0

            Case Hinweise.ArtikelHinweise       '3/0/ArtNr
                Typ1 = 3
                Typ2 = 0

            Case Hinweise.UserInfo               '4/0/UsrNr
                Typ1 = 4
                Typ2 = 0

            Case Hinweise.Konfiguration          '5/0/KonfigEintragNr/KonfigGrp
                Typ1 = 5
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

            Case Hinweise.DeklBezRohstoffIntern '11/1/RohNr  
                Typ1 = 11
                Typ2 = 1

            Case Hinweise.MessageTextLinie      '20/0/LNr
                Typ1 = 20
                Typ2 = 0

            Case Hinweise.MessageTextUser       '20/1/UsrNr
                Typ1 = 20
                Typ2 = 1

            Case Hinweise.NaehrwertUpdate       '21/0/RohNr
                Typ1 = 21
                Typ2 = 0

            Case Else
                Return False
                Exit Function
        End Select
        Return True
    End Function


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
End Class
