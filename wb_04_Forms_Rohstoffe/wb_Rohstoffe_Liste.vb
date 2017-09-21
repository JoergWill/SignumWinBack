Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Liste
    Inherits DockContent

    Public WriteOnly Property Anzeige As AnzeigeFilter
        Set(value As AnzeigeFilter)
            Select Case value
                Case AnzeigeFilter.Alle        ' alle aktiven Rohstoffe Typ > 100
                    DataGridView.Filter = "(KO_Type > 100) AND KA_aktiv = 1"
                Case AnzeigeFilter.Hand        ' alle aktiven Rohstoffe Typ 102
                    DataGridView.Filter = "(KO_Type = 102) AND KA_aktiv = 1"
                Case AnzeigeFilter.Auto        ' alle aktiven Rohstoffe Typ 101,103,104
                    DataGridView.Filter = "(KO_Type = 101) OR KO_Type = 103 or KO_Type = 104) AND KA_aktiv = 1"
                Case AnzeigeFilter.Sauerteig   ' alle aktiven Rohstoffe Sauerteig
                    DataGridView.Filter = "(KO_Type < 100) AND KA_aktiv = 1"
                Case AnzeigeFilter.Install     ' alle inaktiven Rohstoffe
                    DataGridView.Filter = "(KO_Type > 100) AND KA_aktiv = 1"
                Case AnzeigeFilter.Sonstige    ' alle Rohstoffe Typ 105,106
                    DataGridView.Filter = "(KO_Type > 100) AND KA_aktiv = 1"
                Case Else
                    DataGridView.Filter = ""
            End Select
        End Set
    End Property

    Private Sub wb_Rohstoffe_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Nummer", "Name", "A", "&Kommentar"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRohstoffLst, "RohstoffListe")
        'DataGrid Initialisierung Anzeige ohne Sauerteig, nur aktive Rohstoffe
        Me.Anzeige = AnzeigeFilter.Alle
    End Sub

    Public Sub RefreshData()
        'Daten neu einlesen
        DataGridView.RefreshData()
    End Sub

    Private Sub wb_Rohstoffe_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("RohstoffListe")
    End Sub

    Private Sub DataGridView_HasChanged(sender As Object, e As EventArgs) Handles DataGridView.HasChanged
        RohStoff.LoadData(DataGridView)
        'Event auslösen - Aktualisierung der Anzeige in den Detail-Fenstern
        Liste_Click(Nothing)
    End Sub

    'Anstelle des Feldes KO_Nr wird das Feld LG_aktiv ausgegeben
    'die Daten kommen aus einer HashTable (KO_Nr - LG_aktiv)
    Const AktivIdxColumn As Integer = 2
    Const RzpIdxColumn As Integer = 3

    Private Sub DataGridView_CellFormatting(sender As Object, e As Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView.CellFormatting
        Try
            If e.ColumnIndex = AktivIdxColumn Then
                e.Value = RohAktiv(CInt(e.Value.ToString))
            End If
        Catch
        End Try
    End Sub

    Private Sub DataGridView_CellDoubleClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView.CellDoubleClick
        'Zeile im Grid
        Dim eRow As Integer = e.RowIndex
        'Die RezeptNummer steht in Spalte 4
        'TODO als Konstante definieren in wb_sql_Selects
        Dim RezeptNr As Integer = wb_Functions.ValueToInt(DataGridView.Item(RzpIdxColumn, eRow).Value)
        'Wenn die Rezeptnummer gültig ist
        If RezeptNr > 0 Then
            Me.Cursor = Windows.Forms.Cursors.WaitCursor
            'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen (immer Variante 1)
            Dim Rezeptur As New wb_Rezept_Rezeptur(RezeptNr, 1)
            Rezeptur.Show()
            Me.Cursor = Windows.Forms.Cursors.Default
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