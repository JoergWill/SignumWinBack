Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_XNumber
    Inherits DockContent

    Private _ImportItems As New List(Of wb_ImportItem)

    ''' <summary>
    ''' Erzeugt eine Vorlagen-Datei im csv-Format zum Ändern der Artikel-Nummern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnTemplateArtikel_Click(sender As Object, e As EventArgs) Handles BtnTemplateArtikel.Click
        'alle Artikel aus WinBack exportieren (KA_Aktiv = 1 AND KO_Type = 0)
        ExportTemplate(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlXKompList, "KO_Type = 0"), wb_Global.XNumberType.Artikel)
        'Meldung ausgeben
        MsgBox("Export Template beendet", MsgBoxStyle.OkOnly, "Artikel")
    End Sub

    Private Sub BtnImportArtikel_Click(sender As Object, e As EventArgs) Handles BtnImportArtikel.Click
        'Btn deaktivieren
        BtnImportArtikel.Enabled = False
        'Alle Artikel aus der csv-Datei einlesen
        ImportUmsetzListe()

        'Sortieren und prüfen (Doppelte Nummern/Leere Felder...)
        If CheckUmsetzListe() Then
            'Text im Log-Fenster ausgeben
            LogEvent(vbCrLf & "Verarbeiten der Artikel-Umsetz-Liste")
            'Datenbank Update
            SqlUmsetzListe(wb_Global.XNumberType.Artikel)
            LogEvent(vbCrLf & "Verarbeiten der Artikel-Umsetz-Liste beendet")
        Else
            LogEvent("Verarbeiten der Artikel-Umsetz-Liste abgebrochen")
        End If
        'Btn wieder aktivieren
        BtnImportArtikel.Enabled = True
        'Meldung ausgeben
        MsgBox("Alle Daten verarbeitet", MsgBoxStyle.OkOnly, "Artikel")
    End Sub

    ''' <summary>
    ''' Erzeugt eine Vorlagen-Datei im csv-Format zum Ändern der Rohstoff-Nummern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnTemplateRohstoffe_Click(sender As Object, e As EventArgs) Handles BtnTemplateRohstoffe.Click
        'alle Rohstoffe aus WinBack exportieren (KA_Aktiv = 1 AND KO_Type >= 101 AND KO_Type <= 106)
        ExportTemplate(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlXKompList, "KO_Type > 100 AND KO_Type < 107"), wb_Global.XNumberType.Rohstoffe)
        'Meldung ausgeben
        MsgBox("Export Template beendet", MsgBoxStyle.OkOnly, "Rohstoffe")
    End Sub

    Private Sub BtnImportRohstoffe_Click(sender As Object, e As EventArgs) Handles BtnImportRohstoffe.Click
        'Btn deaktivieren
        BtnImportRohstoffe.Enabled = False
        'Alle Rohsotffe aus der csv-Datei einlesen
        ImportUmsetzListe()

        'Sortieren und prüfen (Doppelte Nummern/Leere Felder...)
        If CheckUmsetzListe() Then
            'Text im Log-Fenster ausgeben
            LogEvent(vbCrLf & "Verarbeiten der Rohstoff-Umsetz-Liste")
            'Datenbank Update
            SqlUmsetzListe(wb_Global.XNumberType.Rohstoffe)
            LogEvent(vbCrLf & "Verarbeiten der Rohstoff-Umsetz-Liste beendet")
        Else
            LogEvent("Verarbeiten der Rohstoff-Umsetz-Liste abgebrochen")
        End If
        'Btn wieder aktivieren
        BtnImportRohstoffe.Enabled = True
        'Meldung ausgeben
        MsgBox("Alle Daten verarbeitet", MsgBoxStyle.OkOnly, "Rohstoffe")
    End Sub

    ''' <summary>
    ''' Erzeugt eine Vorlagen-Datei im csv-Format zum Ändern der Rezept-Nummern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnTemplateRezepte_Click(sender As Object, e As EventArgs) Handles BtnTemplateRezepte.Click
        'alle Rezepte aus WinBack exportieren (RZ_Variante = 1)
        ExportTemplate(wb_Sql_Selects.sqlRezeptNrName, wb_Global.XNumberType.Rezepte)
        'Meldung ausgeben
        MsgBox("Export Template beendet", MsgBoxStyle.OkOnly, "Rezepte")
    End Sub

    Private Sub BtnImportRezepte_Click(sender As Object, e As EventArgs) Handles BtnImportRezepte.Click
        'Btn deaktivieren
        BtnImportRezepte.Enabled = False
        'Alle Rezepte aus der csv-Datei einlesen
        ImportUmsetzListe()

        'Sortieren und prüfen (Doppelte Nummern/Leere Felder...)
        If CheckUmsetzListe() Then
            'Text im Log-Fenster ausgeben
            LogEvent(vbCrLf & "Verarbeiten der Rezept-Umsetz-Liste")
            'Datenbank Update
            SqlUmsetzListe(wb_Global.XNumberType.Rezepte)
            LogEvent(vbCrLf & "Verarbeiten der Rezept-Umsetz-Liste beendet")
        Else
            LogEvent("Verarbeiten der Rezept-Umsetz-Liste abgebrochen")
        End If
        'Btn wieder aktivieren
        BtnImportRezepte.Enabled = True
        'Meldung ausgeben
        MsgBox("Alle Daten verarbeitet", MsgBoxStyle.OkOnly, "Rezepte")
    End Sub

    ''' <summary>
    ''' Erzeugt die Template-Files für Artikel/Rohstoffe/Rezepte aus der WinBack-Datenbank
    ''' </summary>
    ''' <param name="Sql"></param>
    Private Sub ExportTemplate(Sql As String, Xnum As wb_Global.XNumberType)
        'Export-Filename (*.csv)
        Dim dlg As New SaveFileDialog With {.RestoreDirectory = False, .InitialDirectory = wb_GlobalSettings.pTempPath,
                                            .FileName = ExportFileName(Xnum), .DefaultExt = ".csv", .Filter = "Excel(csv)|*.csv"}
        'Dialog-Fenster Export-Datei
        If dlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Try
                'Export-File erzeugen
                Dim ExportFile As New IO.FileInfo(dlg.FileName)
                'Datei neu anlegen
                Using fs As IO.FileStream = ExportFile.Open(IO.FileMode.Create, IO.FileAccess.Write)
                    Using sw As New IO.StreamWriter(fs)
                        'Kopfzeile schreiben
                        sw.WriteLine(ExportKopfZeile(Xnum))

                        'Datenbank-Verbindung öffnen - MySQL
                        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
                        'Prüfen ob ein Datensatz vorhanden ist
                        If winback.sqlSelect(Sql) Then
                            'Alle Datensätze sequentiell lesen
                            While winback.Read
                                sw.WriteLine(ExportDatenZeile(winback, Xnum))
                            End While
                        End If
                        winback.Close()

                        'Daten schreiben (Puffer leeren)
                        sw.Flush()
                    End Using
                    fs.Close()
                End Using
            Catch ex As Exception
                MsgBox("Fehler beim Erzeugen des Template-Files", MsgBoxStyle.Critical, "Template")
            End Try
        End If
    End Sub

    Private Function ExportFileName(Xnum As wb_Global.XNumberType) As String
        'Mandant aus den globalen Einstellungen
        Dim MandantName As String = wb_GlobalSettings.MandantName
        'Leeren Mandanten abfangen
        If MandantName <> "" Then
            MandantName = "_" & MandantName
        End If

        'Filename abhängig vom Template_Typ
        Select Case Xnum
            Case wb_Global.XNumberType.Artikel
                Return Date.Now().ToString("yyMMdd") & MandantName & "_Artikel.csv" ' Default file name
            Case wb_Global.XNumberType.Rohstoffe
                Return Date.Now().ToString("yyMMdd") & MandantName & "_Rohstoffe.csv" ' Default file name
            Case wb_Global.XNumberType.Rezepte
                Return Date.Now().ToString("yyMMdd") & MandantName & "_Rezepte.csv" ' Default file name
            Case Else
                Return "*.csv" ' Default file name
        End Select
    End Function

    Private Function ExportKopfZeile(Xnum As wb_Global.XNumberType) As String
        Select Case Xnum
            Case wb_Global.XNumberType.Artikel
                Return ("Nummer-Neu;Bezeichnung;Nummer-Alt;Index")
            Case wb_Global.XNumberType.Rohstoffe
                Return ("Nummer-Neu;Bezeichnung;Nummer-Alt;Index;Type")
            Case wb_Global.XNumberType.Rezepte
                Return ("Nummer-Neu;Bezeichnung;Nummer-Alt;Index")
            Case Else
                Return ""
        End Select
    End Function

    Private Function ExportDatenZeile(winback As wb_Sql, Xnum As wb_Global.XNumberType) As String
        Select Case Xnum
            Case wb_Global.XNumberType.Artikel
                'Alle Felder aus der SQL-Abfrage einlesen
                Return (winback.sField("KO_Nr_AlNum") & ";" & winback.sField("KO_Bezeichnung") & ";" & winback.sField("KO_Nr_AlNum") & ";" & winback.sField("KO_Nr"))
            Case wb_Global.XNumberType.Rohstoffe
                'Alle Felder aus der SQL-Abfrage einlesen
                Return (winback.sField("KO_Nr_AlNum") & ";" & winback.sField("KO_Bezeichnung") & ";" & winback.sField("KO_Nr_AlNum") & ";" & winback.sField("KO_Nr") & ";" & winback.sField("KO_Type"))
            Case wb_Global.XNumberType.Rezepte
                'Alle Felder aus der SQL-Abfrage einlesen
                Return (winback.sField("RZ_Nr_AlNum") & ";" & winback.sField("RZ_Bezeichnung") & ";" & winback.sField("RZ_Nr_AlNum") & ";" & winback.sField("RZ_Nr"))
            Case Else
                Return ""
        End Select
    End Function

    Private Sub ImportUmsetzListe()
        'Export-Filename (*.csv)
        Dim dlg As New OpenFileDialog With {.RestoreDirectory = False, .InitialDirectory = wb_GlobalSettings.pTempPath,
                                            .FileName = "*.csv", .DefaultExt = ".csv", .Filter = "Excel(csv)|*.csv"}
        'Dialog-Fenster Export-Datei
        If dlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            'Text im Log-Fenster ausgeben
            LogEvent("Einlesen der Umsetz-Liste " & dlg.FileName)

            'Import-File einlesen
            Using csvParser As New FileIO.TextFieldParser(dlg.FileName)
                With csvParser
                    ' Feld-Trennzeichen
                    .SetDelimiters(";")
                    ' Festlegen, ob die Datenfelder in Anführungszeichen stehen
                    .HasFieldsEnclosedInQuotes = False
                    ' Falls die 1. Zeile die Spaltennamen enthält
                    Dim Header As String = .ReadLine()

                    'Array Löschen
                    _ImportItems.Clear()
                    'alle Zeilen einlesen
                    Do While Not .EndOfData
                        ' alle Datenfelder der aktuellen Datenzeile lesen
                        Dim x As New wb_ImportItem
                        x.FieldData = .ReadFields()
                        _ImportItems.Add(x)
                    Loop
                    .Close()
                End With
            End Using
        End If
    End Sub

    Private Function CheckUmsetzListe() As Boolean
        'Text im Log-Fenster ausgeben
        LogEvent("Prüfen   der Umsetz-Liste")
        Dim Result As Boolean = True

        'Array ist gefüllt
        If _ImportItems.Count > 0 Then
            'eingelesenes Array sortieren (nach Nummer Neu)
            _ImportItems.Sort()

            'Leere Einträge entfernen
            SqlRemoveItems()
            'Doppelte Einträge suchen (NummerNeu)
            If SqlDoubleItems() Then
                Result = False
            End If
        Else
            'Text im Log-Fenster ausgeben
            LogEvent("Fehler - Umsetzliste ist Leer !")
            Return False
        End If

        'Fehler
        If Not Result AndAlso MsgBox("Fehler beim Einlesen der Umsetz-Daten." & "Daten trotzdem einlesen?", MsgBoxStyle.OkCancel, "Nummern tauschen") = vbOK Then
            Return True
        End If
        Return Result
    End Function

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
    Private Sub SqlUmsetzListe(XNum As wb_Global.XNumberType)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'alle Elemente der Liste durchlaufen
        For Each x In _ImportItems
            'SQL-Update - Bei Fehler wird der Import der Liste abgebrochen
            If Not SqlUmsetzItem(winback, x, XNum) Then
                Exit For
            End If
        Next
        winback.Close()
    End Sub

    Private Function SqlUmsetzItem(winback As wb_Sql, x As wb_ImportItem, Xnum As wb_Global.XNumberType) As Boolean
        'prüfen ob die sich alte und neue Nummer unterscheiden
        If x.NummerAlt <> x.NummerNeu Then
            'Ausgabe im Log-Fenster
            LogEvent(x.NummerAlt.PadLeft(15) & " -> " & x.NummerNeu.PadRight(15) & " (" & x.Index.PadLeft(5, "0") & ") " & x.Bezeichnung.PadRight(50), False)
            Application.DoEvents()

            'abhängig von der Listen-Type
            Select Case Xnum
                Case wb_Global.XNumberType.Artikel, wb_Global.XNumberType.Rohstoffe
                    'sql-Kommando - Ändern der Artikel-Nummer
                    Return (winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateKompAlNr, x.Index, x.NummerNeu)) > 0)
                Case wb_Global.XNumberType.Rezepte
                    'sql-Kommando - Ändern der Artikel-Nummer
                    Return (winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptUpdateNummer, x.Index, x.NummerNeu)) > 0)
                Case Else
                    Return False
            End Select
        Else
            'Ausgabe im Log-Fenster
            LogEvent(x.NummerAlt.PadLeft(15) & " == " & x.NummerNeu.PadRight(15) & " (" & x.Index.PadLeft(5, "0") & ") " & x.Bezeichnung.PadRight(50), False)
        End If
        Return True
    End Function

    Private Sub SqlRemoveItems()
        Dim _RemoveItems As New List(Of wb_ImportItem)
        _RemoveItems.Clear()
        For Each x In _ImportItems
            If x.NummerNeu = "" Then
                LogEvent("Fehler - Nummer fehlt. Zeile wird ignoriert: " & "(" & x.Index.PadLeft(5, "0") & ") " & x.Bezeichnung)
                _RemoveItems.Add(x)
            End If
        Next
        For Each x In _RemoveItems
            _ImportItems.Remove(x)
        Next
    End Sub

    Private Function SqlDoubleItems() As Boolean
        Dim Result As Boolean = False
        For i = 1 To _ImportItems.Count - 1
            If _ImportItems.Item(i - 1).NummerNeu = _ImportItems.Item(i).NummerNeu Then
                'Text im Log-Fenster ausgeben
                LogEvent("Fehler - Umsetzliste hat doppelte Einträge bei Nummer " & _ImportItems.Item(i).NummerNeu)
                Result = True
            End If
        Next
        Return Result
    End Function

    ''' <summary>
    ''' Freigabe der Import-Funktion über Check-Box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbGelesen_Click(sender As Object, e As EventArgs) Handles cbGelesen.Click
        BtnImportArtikel.Enabled = cbGelesen.Checked
        BtnImportRezepte.Enabled = cbGelesen.Checked
        BtnImportRohstoffe.Enabled = cbGelesen.Checked
    End Sub

    Private Sub LogEvent(txt As String, Optional Log As Boolean = False, Optional Scroll As Boolean = True)
        'Meldung im Log-Fenster ausgeben
        tbLogger.Text = tbLogger.Text & txt & vbCrLf
        'Meldung im Logger ausgeben/sichern
        If Log Then
            Trace.WriteLine("@I_" & txt)
        End If
        'Scroll zum Ende des Textes
        If Scroll Then
            tbLogger.SelectionStart = tbLogger.Text.Length
            tbLogger.SelectionLength = 0
            tbLogger.ScrollToCaret()
        End If
    End Sub

End Class
