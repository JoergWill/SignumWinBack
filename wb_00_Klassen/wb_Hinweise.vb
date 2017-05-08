Imports MySql.Data.MySqlClient
Imports WinBack.wb_Global
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
    Private ReadOK As Boolean = False

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
            Return H2_Id2
        End Get
        Set(value As Integer)
            H2_Id2 = value
        End Set
    End Property

    Public Property ArtikelNr As Integer
        Get
            Return H2_Id2
        End Get
        Set(value As Integer)
            H2_Id2 = value
        End Set
    End Property

    Public Property Memo As String
        Get
            Return H2_Memo
        End Get
        Set(value As String)
            H2_Memo = value
        End Set
    End Property

    ''' <summary>
    ''' Objekt initialisieren. Die Daten werden nicht gelesen.
    ''' </summary>
    ''' <param name="DataTyp"></param>
    Public Sub New(DataTyp As Hinweise)
        'H2_Typ und H2_Typ2 ermitteln
        GetTyp(DataTyp, H2_Typ, H2_Typ2)
        'Daten sind noch nicht gelesen
        ReadOK = False
    End Sub

    ''' <summary>
    ''' Objekt initialiseren. Die entsprechenden Daten werden gelesen
    ''' </summary>
    ''' <param name="DataTyp">Datentyp</param>
    ''' <param name="idx">Rohstoff/Artikel/Rezeptnummer</param>
    Public Sub New(DataTyp As Hinweise, idx As Integer)
        'H2_Typ und H2_Typ2 ermitteln
        If GetTyp(DataTyp, H2_Typ, H2_Typ2) Then
            ReadOK = Read(idx)
        End If
    End Sub

    ''' <summary>
    ''' Daten aus Datenbank winback.Hinweise2 einlesen.
    ''' </summary>
    ''' <param name="idx">Integer - Komponenten/Artikelnummer</param>
    ''' <returns></returns>
    Public Function Read(idx As Integer) As Boolean
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)

        'Daten aus MySQL-Memo in String einlesen
        ReadOK = False
        winback.sqlSelect(setParams(sqlSelectH2, H2_Typ, H2_Typ2, idx))
        If winback.Read Then
            H2_Aenderung_Nr = winback.sField("H2_Aenderung_Nr")
            H2_Aenderung_Datum = winback.sField("H2_Aenderung_Datum")
            H2_UserNr = winback.iField("H2_UserNr")
            H2_UserName = winback.sField("H2_UserName")
            H2_Memo = winback.sField("H2_Memo")
            H2_Id2 = idx
            ReadOK = True
        End If
        winback.Close()
        Return ReadOK
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
        If (H2_Id2 > 0) Then

            Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
            Dim s1, s2 As String

            'Vor Insert/Update müssen die Daten gelesen werden
            If Not ReadOK Then
                winback.sqlSelect(setParams(sqlSelectH2, H2_Typ, H2_Typ2, H2_Id2))
                If winback.Read Then
                    H2_Aenderung_Nr = winback.sField("H2_Aenderung_Nr")
                    ReadOK = True
                End If
                winback.CloseRead()
            End If

            'Änderungsdatum ist das aktuelle Datum
            H2_Aenderung_Datum = Date.Today.ToShortDateString
            'aktuellen Benutzer NUmmer/Name eintragen
            H2_UserNr = My.Settings.AktUserNr
            H2_UserName = My.Settings.AktUser

            'Daten sind gelesen/Datensatz vorhanden
            If ReadOK Then
                'Änderung-Nummer um Eins nach oben zählen
                H2_Aenderung_Nr += 1
                'Daten in Memo-Feld speichern (Update)
                s1 = "H2_Aenderung_Nr = " & CStr(H2_Aenderung_Nr) & ", H2_Aenderung_Datum = '" & H2_Aenderung_Datum & "'," &
                 "H2_Aenderung_User = " & CStr(H2_UserNr) & ",H2_Aenderung_Name = '" & H2_UserName & "'," &
                 "H2_Memo = " & "'" & H2_Memo & "'"
                winback.sqlCommand(setParams(sqlUpdateH2, H2_Typ, H2_Typ2, H2_Id2, s1))
                winback.Close()
            Else
                'Änderung-Nummer Eins
                H2_Aenderung_Nr = 1
                'Daten in Memo-Feld speichern (Insert)
                s1 = "H2_Aenderung_Nr, H2_Aenderung_Datum, H2_Aenderung_User, H2_Aenderung_Name, H2_Memo"
                s2 = CStr(H2_Aenderung_Nr) & ",'" & H2_Aenderung_Datum & "'," & CStr(H2_UserNr) & ",'" & H2_UserName & "','" & H2_Memo & "'"
                winback.sqlCommand(setParams(sqlInsertH2, H2_Typ, H2_Typ2, H2_Id2, s1, s2))
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
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        'alle Datensätze löschen
        Dim i As Integer = winback.sqlCommand(setParams(sqlDeleteH2, H2_Typ, H2_Typ2, H2_Id2))
        winback.Close()
        Return (i > 0)
    End Function


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
