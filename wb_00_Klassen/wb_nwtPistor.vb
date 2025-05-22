Imports System.IO
Imports WinBack.wb_Sql_Selects

Public Class wb_nwtPistor

    Private _InfoText As String = ""
    Private _Filename As String = wb_GlobalSettings.ImportPathPistor
    Private MyReader As Microsoft.VisualBasic.FileIO.TextFieldParser

    Public ReadOnly Property InfoText As String
        Get
            Return DateTime.Now.ToLongTimeString & " " & _InfoText
        End Get
    End Property

    Public Property Filename As String
        Get
            Return _Filename
        End Get
        Set(value As String)
            _Filename = value
        End Set
    End Property

    Public Sub New()
        If _Filename <> "" Then
            If File.Exists(_Filename) Then
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
        'cvs-Datei zeilenweise einlesen
        Dim currentRow As String()

        'Import-File ist vorhanden
        If MyReader IsNot Nothing Then
            'Lesen bis Datei-Ende
            If Not MyReader.EndOfData Then
                Try
                    'Zeilenweise auslesen
                    currentRow = MyReader.ReadFields()
                    'Update Komponenten-Daten in WinBack und OrgaBack
                    UpdateKomponente(currentRow(wb_Functions.PistorToText("Nummer")), currentRow)
                    Return True

                Catch ex As FileIO.MalformedLineException
                    _InfoText = "Line " & ex.Message & " is not valid and will be skipped."
                    Return True

                End Try
            End If

            'alle Datensätze eingelesen und verarbeitet
            _InfoText = "Alle Daten importiert"
            MyReader.Close()

            'Nach dem letzten Datensatz wird die Import-Datei umbenannt
            If File.Exists(_Filename & ".bak") Then
                File.Delete(_Filename & ".bak")
            End If
            File.Move(_Filename, _Filename & ".bak")

            Return False
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
    Public Function UpdateKomponente(Nummer As String, Data As String()) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Komponenten-Objekt nimmt die aktuellen Daten auf
        Dim nwtDaten As New wb_Komponente
        UpdateKomponente = False

        'Datensatz aus Tabelle Komponenten lesen (nur Rohstoffe). Liest auch Rohstoffe mit führendem 'R'
        If winback.sqlSelect(setParams(sqlSelectRoh_AlNum, Nummer, "R" & Nummer)) Then
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

                'Änderungs-Log löschen
                nwtDaten.ClearReport()
                'Nährwert-Info aus dem String-Array in nwtDaten eintragen
                GetNaehrwerte(Data, nwtDaten)
                'Zutatenliste und sonstige Angaben in nwtDaten eintragen
                GetTexte(Data, nwtDaten)

                'Änderungen in Datenbank schreiben (WinBack und OrgaBack)
                DbUpdateNaehrwerte(nwtDaten, True)
                'Protokoll der Änderungen speichern in Hinweise
                nwtDaten.SaveReport()
                'Protokoll der Änderungen ausgeben
                'Debug.Print("Report " & nwtDaten.GetReport)

                'Ausgabe-Text
                _InfoText = "(" & nwtDaten.Nr.ToString("000000") & ") " & nwtDaten.Nummer & " " & nwtDaten.Bezeichnung
                UpdateKomponente = True
            Else
                _InfoText = "(Pistor) R" & Nummer & " nicht gefunden"
            End If
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
        Debug.Print("Update (Komp)Zutatenliste in WinBack " & nwtDaten.Nummer & " " & nwtDaten.Bezeichnung)
        nwtDaten.MySqldbUpdate_Zutatenliste()
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
        'Schleife über alle Elemente im Array
        For i = 0 To Data.Count - 1
            nwtdaten.ktTyp301.PistorWert(i) = Data(i)
        Next
    End Sub

    Public Sub GetTexte(Data As String(), nwtdaten As wb_Komponente)
        'ZutatenListe - Deklaration extern
        nwtdaten.DeklBezeichungExtern = Data(wb_Functions.PistorToText("Deklaration"))
        'ZutatenListe - Deklaration intern
        nwtdaten.DeklBezeichungIntern = Data(wb_Functions.PistorToText("Zutatenliste"))
    End Sub

End Class
