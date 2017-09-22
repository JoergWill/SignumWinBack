Public Class wb_ZutatenListe_Global
    Private Shared eEListe As New Dictionary(Of String, wb_Global.ENummern)
    Private Shared sEListe As New Dictionary(Of String, String)

    ''' <summary>
    ''' Vor dem ersten Aufruf der Funktionen in dieser Klasse wird der shared-Konstruktor aufgerufen
    ''' Initialisierung und Aufbau der Hash-Table aus der Datenbank (Tabelle ENummern)
    ''' </summary>
    Shared Sub New()
        Dim E As wb_Global.ENummern
        Dim eName As String = ""

        Dim winback As New wb_Sql(wb_globalsettings.SqlConWinBack, wb_globalsettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlENummern)
        eEListe.Clear()
        sEListe.Clear()

        While winback.Read
            E.Nr = winback.iField("EN_Nr")
            E.Bezeichnung = winback.sField("EN_Bezeichnung")
            E.Text = winback.sField("EN_Name")
            E.Beschreibung = winback.sField("EN_Beschreibung")
            E.Bemerkung = winback.sField("EN_Bemerkung")
            E.Key = winback.sField("EN_Key")
            E.CleanLabel = winback.sField("EN_CleanLabel")

            If eName <> E.Text Then
                'Neuer Eintrag in sEListe (suche nach E-Nummer)
                Debug.Print("E.Bezeichnung " & E.Bezeichnung)
                If Not eEListe.ContainsKey(E.Text) Then
                    eEListe.Add(E.Text, E)
                    If Not sEListe.ContainsKey(E.Bezeichnung.ToLower) Then
                        sEListe.Add(E.Bezeichnung.ToLower, E.Text)
                    End If
                End If
                eName = E.Text
            Else
                'Neuer Eintrag in sEListe (suche nach String-Bezeichnung)
                Debug.Print("E.Bezeichnung " & E.Bezeichnung)
                If Not sEListe.ContainsKey(E.Bezeichnung.ToLower) Then
                    sEListe.Add(E.Bezeichnung.ToLower, E.Text)
                End If
            End If
        End While
        winback.Close()
    End Sub

    Public Shared Function Find_ENummer(ENr As String) As wb_Global.ENummern
        If eEListe.ContainsKey(ENr) Then
            Return eEListe(ENr)
        Else
            Return EmptyENumber()
        End If
    End Function

    Public Shared Function Find_EBezeichnung(Text As String) As wb_Global.ENummern
        If sEListe.ContainsKey(Text.ToLower) Then
            Return eEListe(sEListe(Text.ToLower))
        Else
            Return EmptyENumber()
        End If
    End Function

    Public Shared Function EmptyENumber() As wb_Global.ENummern
        EmptyENumber.Nr = -1
        EmptyENumber.Bemerkung = ""
        EmptyENumber.Beschreibung = ""
        EmptyENumber.Bezeichnung = ""
        EmptyENumber.Text = ""
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
