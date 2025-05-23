﻿Imports WinBack.wb_User_Shared

Public Class wb_User_Liste

    'Event Form wird geladen
    Private Sub wb_User_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "", "Name", "Gruppe", "", "&PersonalNr"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        'LoadGrpTexte()
        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "IP_Wert4str"
        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlUsersListe, "UserListe")

        'Throw New System.Exception("TEST FEHLER")

        'Detail-Daten sind geändert worden - in Datenbank speichern
        AddHandler eEdit_Leave, AddressOf SaveData
        AddHandler eData_Reload, AddressOf RefreshData
    End Sub

    Public Sub RefreshData(sender As Object)
        'Daten neu einlesen
        DataGridView.RefreshData()
    End Sub

    ''' <summary>
    ''' Einen Datensatz in der Mitarbeiter-Liste suchen. Wenn der Datensatz gefunden wurde, wird True zurückgegeben.
    ''' </summary>
    ''' <param name="col"> (Integer) Spalte in der gesucht werden soll</param>
    ''' <param name="s">   (String)  Suchbegriff</param>
    ''' <returns>
    ''' True - Wert gefunden
    ''' False - Wert nicht gefunden</returns>
    Public Function SelectData(col As Integer, s As String)
        'Datensatz suchen
        Return DataGridView.SelectData(col, s)
    End Function

    'Event Form wird geschlossen
    Private Sub wb_User_Liste_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        SaveData(sender)
        'Event-Handler freigeben
        RemoveHandler eEdit_Leave, AddressOf SaveData
        RemoveHandler eData_Reload, AddressOf RefreshData
        'Layout sichern
        DataGridView.SaveToDisk("UserListe")
    End Sub

    'Datensatz in Datenbank sichern
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1172:Unused procedure parameters should be removed", Justification:="<Ausstehend>")>
    Private Sub SaveData(sender As Object)
        'Daten in Datenbank sichern
        If User.SaveData(DataGridView) Then
            Debug.Print("DataGridView.UpdateDataBase")
            DataGridView.UpdateDataBase()
        End If
    End Sub

    'Datensatz-Zeiger wurde geändert
    Private Sub DataGridView_HasChanged(sender As Object, e As EventArgs) Handles DataGridView.HasChanged
        'Daten zum aktuell ausgewählten User laden
        User.LoadData(DataGridView)
        'Event auslösen - Aktualisierung der Anzeige in den Detail-Fenstern
        Liste_Click(Nothing)
    End Sub

    'Anstelle der Gruppen-Nummer wird die Gruppen-Bezeichnung ausgegeben
    'die Texte kommen aus eine HashTable
    Const GrpIdxColumn As Integer = 3
    Private Sub DataGridView_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView.CellFormatting
        Try
            If e.ColumnIndex = GrpIdxColumn Then
                If (CInt(e.Value) > 0) Then
                    e.Value = GrpTexte(CInt(e.Value)).ToString
                Else
                    e.Value = ""
                End If
            End If
        Catch
            Debug.Print(e.Value)
        End Try
    End Sub

End Class