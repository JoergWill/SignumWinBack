Imports WinBack.wb_Artikel_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Artikel_Liste
    Inherits DockContent

    Private Sub wb_Artikel_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "Nummer", "Name", "&Rezept", "Kommentar"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "KO_Bezeichnung"
        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlArtikelLst, "ArtikelListe")

        AddHandler eEdit_Leave, AddressOf SaveData
    End Sub

    Public Sub RefreshData()
        'Daten neu einlesen
        DataGridView.RefreshData()
    End Sub

    'Anstelle der Rezept-Nummer (Idx) wird die Rezept-Bezeichnung ausgegeben
    'die Texte kommen aus eine HashTable
    Const RzpIdxColumn As Integer = 3
    Private Sub DataGridView_CellFormatting(sender As Object, e As Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView.CellFormatting
        Try
            If e.ColumnIndex = RzpIdxColumn Then
                If CInt(e.Value) > 0 Then
                    If wb_Artikel_Shared.Rzpt.ContainsKey(CInt(e.Value)) Then
                        e.Value = wb_Artikel_Shared.Rzpt(CInt(e.Value)).ToString
                    Else
                        e.Value = "Rezept fehlt (Index) " & CInt(e.Value)
                    End If
                Else
                    e.Value = ""
                End If
            End If
        Catch
            Debug.Print("CellFormatting " & e.Value)
        End Try
    End Sub

    Private Sub DataGridView_CellDoubleClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView.CellDoubleClick
        'Zeile im Grid
        Dim eRow As Integer = e.RowIndex
        'Kein Doppelclick auf die Überschriftenzeile
        If eRow > 0 Then
            'Die RezeptNummer steht in Spalte 1
            Dim RezeptNr As Integer = wb_Functions.ValueToInt(DataGridView.Item(RzpIdxColumn, eRow).Value)
            'Wenn die Rezeptnummer gültig ist
            If RezeptNr > 0 Then
                Me.Cursor = Windows.Forms.Cursors.WaitCursor
                'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen (immer Variante 1)
                Dim Rezeptur As New wb_Rezept_Rezeptur(RezeptNr, 1)
                Rezeptur.Show()
                Me.Cursor = Windows.Forms.Cursors.Default
            End If
        End If
    End Sub

    Private Sub DataGridView_HasChanged(sender As Object, e As EventArgs) Handles DataGridView.HasChanged
        'Debug.Print("Rohstoffe DataGridView has Changed: Bezeichnung alt " & RohStoff.Bezeichnung)
        'Daten laden aus winback.Komponenten in GridView
        Artikel.LoadData(DataGridView)
        'Detail-Daten aus winback.Komponenten laden in Objekt wb_Rostoffe_Shared.Rohstoff
        Artikel.MySQLdbRead(Artikel.Nr)
        'Event auslösen - Aktualisierung der Anzeige in den Detail-Fenstern
        Liste_Click(Nothing)
        'Nach dem Update der Detailfenster wird der Focus wieder zurückgesetzt (Eingabe Suchmaske)
        DataGridView.Focus()
    End Sub

    Private Sub wb_Artikel_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("ArtikelListe")
        'Event wieder freigeben
        RemoveHandler wb_Artikel_Shared.eEdit_Leave, AddressOf SaveData
    End Sub

    ''' <summary>
    ''' Datensatz in Datenbank sichern. Wird über Event eEdit_Leave() aufgerufen
    ''' </summary>
    Private Sub SaveData(sender)
        'Daten in Datenbank sichern
        If Artikel.SaveData(DataGridView) Then
            DataGridView.UpdateDataBase()
        End If
    End Sub

End Class

'CREATE TABLE winback.Komponenten (
'    KO_Nr int(10) UNSIGNED Not NULL Default 0,
'    KO_Type tinyint(3) UNSIGNED Not NULL Default 0,
'    KO_Bezeichnung varchar(60) Default NULL,
'    KO_Kommentar varchar(50) Default NULL,
'    KO_Nr_AlNum varchar(16) Default NULL,
'    KO_Temp_Korr smallint(5) Default 0,
'    KA_Nr int(11) Default 0,
'    KA_Kurzname varchar(16) Default NULL,
'    KA_Matchcode varchar(10) Default NULL,
'    KA_Art tinyint(3) UNSIGNED Default 0,
'    KA_Artikel_Typ smallint(6) Default 0,
'    KA_RZ_Nr int(10) UNSIGNED Default 0,
'    KA_Lagerort varchar(16) Default NULL,
'    KA_Prod_Linie tinyint(3) UNSIGNED Default 0,
'    KA_Stueckgewicht varchar(20) Default NULL,
'    KA_Charge_Opt varchar(30) Default NULL,
'    KA_Charge_Min varchar(30) Default NULL,
'    KA_Charge_Max varchar(30) Default NULL,
'    KA_Charge_Opt_kg varchar(30) Default NULL,
'    KA_Charge_Min_kg varchar(30) Default NULL,
'    KA_Charge_Max_kg varchar(30) Default NULL,
'    KA_RS_veraenderbar Char(1) Default NULL,
'    KA_RS_abh_von_RZ_Menge Char(1) Default NULL,
'    KA_RS_aendert_WasMenge Char(1) Default NULL,
'    KA_zaehlt_zu_RZ_Gesamtmenge Char(1) Default NULL,
'    KA_spez_WKap varchar(30) Default NULL,
'    KA_alternativ_RS varchar(16) Default NULL,
'    KA_Verarbeitungshinweise varchar(100) Default NULL,
'    KA_aktiv smallint(5) Default 1,
'    KA_Preis varchar(20) Default NULL,
'    KA_PreisEinheit smallint(5) Default 0,
'    KA_Grp1 int(10) Default 0,
'    KA_Grp2 int(10) Default 0,
'    KA_Timestammp timestamp(14),
'    PRIMARY KEY (KO_Nr)
')
'TYPE = MYISAM
'AVG_ROW_LENGTH = 96;