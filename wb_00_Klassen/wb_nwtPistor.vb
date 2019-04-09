Imports System.IO
Imports WinBack.wb_Sql_Selects

Public Class wb_nwtPistor

    Private _InfoText As String = ""
    Private MyReader As Microsoft.VisualBasic.FileIO.TextFieldParser

    Public ReadOnly Property InfoText As String
        Get
            Return DateTime.Now.ToLongTimeString & " " & _InfoText
        End Get
    End Property

    Public Sub New()
        'Pfad zum Import-File
        Dim Filename As String = wb_GlobalSettings.ImportPathPistor
        If Filename <> "" Then
            If File.Exists(Filename) Then
                'Import Text-File öffnen
                MyReader = New FileIO.TextFieldParser(Filename)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(";")
                _InfoText = "Import Pistor-Datensatz"
            Else
                _InfoText = "Keine (neuen) Daten zum Import vorhanden"
            End If
        Else
            _InfoText = "Kein Import-Pfad für Pistor-Daten eingetragen"
        End If
    End Sub

    Public Function ReadNext() As Boolean
        Dim currentRow As String()
        If MyReader IsNot Nothing Then
            If Not MyReader.EndOfData Then
                Try
                    'Zeilenweise auslesen
                    currentRow = MyReader.ReadFields()

                    'Update Komponenten-Daten in WinBack und OrgaBack
                    UpdateKomponente(currentRow(0), currentRow, False)
                    Return True

                Catch ex As FileIO.MalformedLineException
                    _InfoText = "Line " & ex.Message & " is not valid and will be skipped."
                    Return True
                End Try
            End If
            _InfoText = "Keine (neuen) Daten zum Import vorhanden"
        End If
        Return False
    End Function

    ''' <summary>
    ''' Sucht die Komponente mit der angegebenen Nummer aus der Datenbank
    ''' Einlesen der Komponenten-Daten in ein Komponenten-Objekt
    ''' Übertragen der Nährwert-Daten in die WinBack-Datenbank. Es wird ein Report generiert und
    ''' die Daten werden aktualisiert.
    ''' </summary>
    ''' <returns>True wenn der Datensatz gefunden und aktualisiert wurde</returns>
    Public Function UpdateKomponente(Nummer As String, Data As String(), Optional ByRef bUpdateOrgaBack As Boolean = False) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Komponenten-Objekt nimmt die aktuellen Daten auf
        Dim nwtDaten As New wb_Komponente
        UpdateKomponente = False

        'Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(setParams(sqlSelectKomp_AlNum, Nummer)) Then
            If winback.Read Then
                'Komponentendaten aus DB in Object nwtDaten einlesen
                nwtDaten.MySQLdbRead(winback.MySqlRead)
                winback.CloseRead()

                'Stammdaten und Nährwerte aus DB in (Object)nwtDaten einlesen
                If winback.sqlSelect(setParams(sqlgetNWT, nwtDaten.Nr)) Then
                    'Lesen KO-Nr
                    If winback.Read Then
                        nwtDaten.MySQLdbRead(winback.MySqlRead)
                    End If
                End If
                winback.CloseRead()

                'Datum der letzen Nährwert-Aktualisierung in der WinBack-Datenbank
                Dim WinBackChangeDate As Date = nwtDaten.ktTyp301.TimeStamp
                'Änderungs-Log löschen
                nwtDaten.ClearReport()
                'Nährwert-Info aus dem Array in nwtDaten eintragen
                GetNaehrwerte(Data, nwtDaten)

                'Änderungen in Datenbank schreiben (WinBack und OrgaBack)
                DbUpdateNaehrwerte(nwtDaten, True)
                'Protokoll der Änderungen speichern in Hinweise
                nwtDaten.SaveReport()
                'Protokoll der Änderungen ausgeben
                'Debug.Print("Report " & nwtDaten.GetReport)

                'Ausgabe-Text
                _InfoText = "(" & nwtDaten.Nr.ToString("00000") & ") " & nwtDaten.Bezeichnung
                    UpdateKomponente = True
                End If
            Else
                _InfoText = "Komponente " & Nummer & " nicht gefunden"
                'Reset Flag alle Artikel in OrgaBack updaten
                bUpdateOrgaBack = False
            End If

        winback.Close()
        Return UpdateKomponente
    End Function

    ''' <summary>
    ''' Schreibt die Nährwertinfo (nach Update) in die WinBack und OrgaBack-Datenbank
    ''' Wenn Artikel von der Nährwert-Änderung betroffen sind, wird der entsprechende 
    ''' Artikel-Datensatz markiert.
    ''' </summary>
    ''' <param name="UpdateOrgaBack"></param>
    Public Sub DbUpdateNaehrwerte(nwtDaten As wb_Komponente, UpdateOrgaBack As Boolean)
        'Änderungen der Nährwerte in Komponente(Rohstoff) sichern
        Debug.Print("Update (Komp)Nährwerte in WinBack " & nwtDaten.Nummer & " " & nwtDaten.Bezeichnung)
        nwtDaten.MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
        'Änderungen der Zutatenliste in Komponente(Rohstoff) sichern
        'Debug.Print("Update (Komp)Zutatenliste in WinBack " & nwtDaten.Nummer & " " & nwtDaten.Bezeichnung)
        'nwtDaten.MySqldbUpdate_Zutatenliste()
        'Alle Artikel, welche diese Komponente in Rezepturen verwenden markieren
        'die Nährwerte müssen neu berechnet werden. Farbige Markierung in der Artikel-Liste
        nwtDaten.MySQLdbSetMarkerRzptListe(wb_Global.ArtikelMarker.nwtUpdate)

        'Änderungen der Komponenten-Parameter(Rohstoff) in OrgaBack-DB schreiben
        'Gibt true zurück, wenn der Artikel in OrgaBack existiert
        Debug.Print("Update (Komp)Nährwerte in OrgaBack " & nwtDaten.Nummer & " " & nwtDaten.Bezeichnung)
        If nwtDaten.MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301) Then
            'Zutaten-und Allergenliste in OrgaBack updaten
            Debug.Print("Update (Komp)Zutatenliste in OrgaBack " & nwtDaten.Nummer & " " & nwtDaten.Bezeichnung)
            nwtDaten.MsSqldbUpdate_Zutatenliste()
        End If
    End Sub

    ''' <summary>
    ''' Übertragen der Nährwerte aus dem Array in das Komponenten-Objekt
    ''' Die Daten werden in nwtDaten eingtragen
    ''' 
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <param name="nwtdaten"></param>
    Public Sub GetNaehrwerte(Data As String(), nwtdaten As wb_Komponente)
        'WinBack Index
        Dim idx As Integer

        'Schleife über alle Elemente im Array
        For i = 1 To Data.Count
            'WinBack-Index aus Tabelle ermitteln (Array-Index plus 1 !!)
            idx = wb_Functions.PistorToIndex(i)
            If idx > 0 Then
                nwtdaten.ktTyp301.Wert(idx) = Data(i - 1)
            End If
        Next

    End Sub


End Class
