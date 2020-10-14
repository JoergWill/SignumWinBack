Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking
Imports System.Windows.Forms

Public Class wb_Rohstoffe_Liste
    Inherits DockContent

    Const ColumnKompNr As Integer = 3
    Const ColumnRzpIdx As Integer = 5

    Private _Anzeige As AnzeigeFilter = AnzeigeFilter.Undefined
    Private _Link As LinkFilter = LinkFilter.Undefined

    Public WriteOnly Property Anzeige As AnzeigeFilter
        Set(value As AnzeigeFilter)
            _Anzeige = value
            SetDataGridViewFilter()
        End Set
    End Property

    Public WriteOnly Property Link As LinkFilter
        Set(value As LinkFilter)
            _Link = value
            SetDataGridViewFilter()
        End Set
    End Property

    ''' <summary>
    ''' Textbausteine für die Filterfunktion
    ''' Anzeige für Fenstertitel und Ausdruck
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FilterText As String
        Get
            Select Case _Anzeige
                Case AnzeigeFilter.Alle, AnzeigeFilter.Install
                    FilterText = "Alle WinBack-Rohstoffe"
                Case AnzeigeFilter.Auto
                    FilterText = "WinBack-Rohstoffe automatische Dosierung"
                Case AnzeigeFilter.Hand
                    FilterText = "WinBack-Rohstoffe Tisch-Bodenwaage"
                Case AnzeigeFilter.Sauerteig
                    FilterText = "WinBack-Rohstoffe Sauerteig-Herstellung"
                Case Else
                    FilterText = "Alle WinBack-Rohstoffe"
            End Select

            Select Case _Link
                Case LinkFilter.Cloud
                    FilterText = FilterText + " verbunden mit der Cloud"
                Case LinkFilter.Cloud
                    FilterText = FilterText + " mit hinterlegter Rezeptur"
                Case LinkFilter.NoLink
                    FilterText = FilterText + " ohne Verbindung zu Cloud/Rezeptur"
            End Select
        End Get
    End Property

    Private Sub SetDataGridViewFilter()
        'Filterstring
        Dim Filter As String = ""

        'Filter Hand/Auto/Sauer/Installation
        Select Case _Anzeige
            Case AnzeigeFilter.Alle        ' alle aktiven Rohstoffe Typ > 100
                Filter = "(KO_Type > 100) AND KA_aktiv = 1"
            Case AnzeigeFilter.Hand        ' alle aktiven Rohstoffe Typ 102
                Filter = "(KO_Type = 102) AND KA_aktiv = 1"
            Case AnzeigeFilter.Auto        ' alle aktiven Rohstoffe Typ 101,103,104
                Filter = "((KO_Type = 101) OR (KO_Type = 103) or (KO_Type = 104)) AND KA_aktiv = 1"
            Case AnzeigeFilter.Sauerteig   ' alle aktiven Rohstoffe Sauerteig
                Filter = "(KO_Type < 100) AND (KO_Type > 0) AND KA_aktiv = 1"
            Case AnzeigeFilter.Install     ' alle aktiven und inaktiven Rohstoffe
                Filter = "(KO_Type > 100)"
            Case AnzeigeFilter.Sonstige    ' alle Rohstoffe Typ 105,106
                Filter = "(KO_Type > 100) AND KA_aktiv = 1"
            Case Else
                Filter = "(KO_Type > 100) AND KA_aktiv = 1"
        End Select

        'Filter Rezept/Cloud
        Select Case _Link
            Case LinkFilter.Cloud
                Filter = Filter + " AND KA_Matchcode <> ''"
            Case LinkFilter.Rzpt
                Filter = Filter + " AND KA_RZ_NR > 0"
            Case LinkFilter.NoLink
                Filter = Filter + " AND KA_RZ_NR = 0 AND NOT KA_Matchcode <> ''"
        End Select

        'Filter anwenden
        DataGridView.Filter = Filter
        'FensterText anzeigen
        Me.Text = FilterText
    End Sub

    Private Sub wb_Rohstoffe_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "Nummer", "Name", "A", "&Kommentar"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "KO_Bezeichnung"

        'DataGrid Popup-Menu (Aktiv/Hand)
        Dim evH As New EventHandler(AddressOf DataGridView_PopupClick)
        DataGridView.PopupItemAdd("Aktivieren", "A", Nothing, evH)
        DataGridView.PopupItemAdd("Hand", "H", Nothing, evH)
        DataGridView.PopupItemAdd("Deaktivieren", "D", Nothing, evH, True)

        'DataGrid Popup-Menu Filter
        DataGridView.PopupItemAdd("Filter:", "Flt", Nothing, evH, True, False)
        DataGridView.PopupItemAdd("nur Hand", "Hand", Nothing, evH, False, True)
        DataGridView.PopupItemAdd("nur Auto", "Auto", Nothing, evH, False, True)
        DataGridView.PopupItemAdd("nur Sauerteig", "Sauer", Nothing, evH, False, True)
        DataGridView.PopupItemAdd("Installation", "Inst", Nothing, evH, True, True)

        'DataGrid Popup-Menu Rezept/Cloud/Alle
        DataGridView.PopupItemAdd("Verbunden mit:", "Link", Nothing, evH, True, False)
        DataGridView.PopupItemAdd("Rezept", "Rzpt", Nothing, evH, False, True)
        DataGridView.PopupItemAdd("Cloud", "Cloud", Nothing, evH, False, True)
        DataGridView.PopupItemAdd("Ohne", "Ohne", Nothing, evH, True, True)

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRohstoffSimpleLst, "RohstoffListe")
        'DataGrid Initialisierung Anzeige ohne Sauerteig, nur aktive Rohstoffe
        Me.Anzeige = AnzeigeFilter.Alle

        AddHandler eEdit_Leave, AddressOf SaveData
    End Sub

    Public Sub RefreshData()
        'Daten neu einlesen
        DataGridView.RefreshData()
        'Event auslösen - Aktualisierung der Anzeige in den Detail-Fenstern
        Liste_Click(Nothing)
    End Sub

    Public Sub RefreshData(KoNr As Integer)
        DataGridView.RefreshData()
        DataGridView.SelectData(ColumnKompNr, KoNr.ToString)
        'Event auslösen - Aktualisierung der Anzeige in den Detail-Fenstern
        Liste_Click(Nothing)
    End Sub

    Public Sub ResetFilter()
        DataGridView.ResetFilter()
    End Sub

    Private Sub wb_Rohstoffe_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("RohstoffListe")
        'Event wieder freigeben
        RemoveHandler wb_Rohstoffe_Shared.eEdit_Leave, AddressOf SaveData
    End Sub

    ''' <summary>
    ''' Datensatz in Datenbank sichern. Wird über Event eEdit_Leave() aufgerufen
    ''' </summary>
    Private Sub SaveData(sender)
        'Daten in Datenbank sichern
        If RohStoff.SaveData(DataGridView) Then
            DataGridView.UpdateDataBase()
        End If
    End Sub

    ''' <summary>
    ''' Die Selektion im DataGridView hat sich geändert. Wenn Daten in den Detail-Fenstern geändert wurden, wird
    ''' diese Änderung vor dem Laden der neuen Daten in der Datenbank gesichert.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView_HasChanged(sender As Object, e As EventArgs) Handles DataGridView.HasChanged
        'Debug.Print("Rohstoffe DataGridView has Changed: Bezeichnung alt " & RohStoff.Bezeichnung)
        'Daten laden aus winback.Komponenten in GridView
        RohStoff.LoadData(DataGridView)
        'Detail-Daten aus winback.Komponenten laden in Objekt wb_Rohstoffe_Shared.Rohstoff
        RohStoff.MySQLdbRead(RohStoff.Nr)
        'Event auslösen - Aktualisierung der Anzeige in den Detail-Fenstern
        Liste_Click(Nothing)
        'Nach dem Update der Detailfenster wird der Focus wieder zurückgesetzt (Eingabe Suchmaske)
        DataGridView.Focus()
    End Sub

    'Anstelle des Feldes KO_Nr wird das Feld LG_aktiv ausgegeben
    'die Daten kommen aus einer HashTable (KO_Nr - LG_aktiv)
    Private Sub DataGridView_CellFormatting(sender As Object, e As Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView.CellFormatting
        Try
            If e.ColumnIndex = ColumnKompNr Then
                If RohAktiv.ContainsKey(CInt(e.Value)) Then
                    e.Value = RohAktiv(CInt(e.Value.ToString))
                Else
                    e.Value = ""
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub DataGridView_CellDoubleClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView.CellDoubleClick
        'Zeile im Grid
        Dim eRow As Integer = e.RowIndex
        'Kein Doppelclick auf die Überschriftenzeile
        If eRow > 0 Then
            Dim RezeptNr As Integer = wb_Functions.ValueToInt(DataGridView.Item(ColumnRzpIdx, eRow).Value)
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

    ''' <summary>
    ''' PopUp-Menu Click.
    ''' Rohstoff Aktiv/Hand/Deaktiviert (Silo-Umschaltung)
    ''' 
    ''' Bei der Siloumschaltung werden alle anderen Rohstoffe mit identischer Rohstoff-Nummer deaktiviert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView_PopupClick(ByVal sender As Object, ByVal e As EventArgs)
        'Flag setzen (aus PopUp)
        Dim Flag As String = CType(sender, Windows.Forms.ToolStripMenuItem).Tag
        'Zeile im DataGridView (aus MouseOver)
        Dim iRow As Integer = DataGridView.HoverRow
        'Rohstoff-Nummer (alphanumerisch)
        Dim RohNummer As String = DataGridView.Field("KO_Nr_AlNum", iRow)
        'Rohstoff-Nummer (index)
        Dim RohNr As Integer = DataGridView.Field("KO_Nr", iRow)
        'Lagerort zum Rohstoff
        Dim LagerOrt As String = DataGridView.Field("KA_Lagerort", iRow)

        'Flag Aktiv/Hand/Deaktiviert
        Select Case CType(sender, Windows.Forms.ToolStripMenuItem).Tag
            Case "A"
                'Schleife über alle Rohstoffe mit der gleichen Nummer
                For i = 0 To DataGridView.RowCount - 1
                    If DataGridView.Field("KO_Nr_AlNum", i) = RohNummer Then
                        If DataGridView.Field("KA_Lagerort", i) = LagerOrt Then
                            UpdateRohAktiv("A", LagerOrt, RohNr)
                        Else
                            UpdateRohAktiv("", DataGridView.Field("KA_Lagerort", i), DataGridView.Field("KO_Nr", i))
                        End If
                    End If
                Next
                'Daten neu einlesen
                DataGridView.RefreshData()

            Case "H"
                'Rohstoff auf Hand setzen
                UpdateRohAktiv("H", LagerOrt, RohNr)
                'Daten neu einlesen
                DataGridView.RefreshData()

            Case "D"
                'Rohstoff deaktivieren
                UpdateRohAktiv("", LagerOrt, RohNr)
                'Daten neu einlesen
                DataGridView.RefreshData()


            Case "Flt"
                'Anzeige alle Rohstoffe (Filter löschen)
                Me.Anzeige = AnzeigeFilter.Alle
                Dim UnCheckName As New List(Of String) From {"Hand", "Auto", "Sauer", "Inst"}
                DataGridView.PopupItemsUncheck(UnCheckName)
            Case "Hand"
                'Anzeige nur Hand-Komponenten
                Me.Anzeige = AnzeigeFilter.Hand
                Dim UnCheckName As New List(Of String) From {"Auto", "Sauer", "Inst"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Auto"
                'Anzeige nur Automatik-Komponenten
                Me.Anzeige = AnzeigeFilter.Auto
                Dim UnCheckName As New List(Of String) From {"Hand", "Sauer", "Inst"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Sauer"
                'Anzeige nur Sauerteig-Komponenten
                Me.Anzeige = AnzeigeFilter.Sauerteig
                Dim UnCheckName As New List(Of String) From {"Hand", "Auto", "Inst"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Inst"
                'Anzeige alle Komponenten (Installation)
                Me.Anzeige = AnzeigeFilter.Install
                Dim UnCheckName As New List(Of String) From {"Hand", "Auto", "Sauer"}
                DataGridView.PopupItemsUncheck(UnCheckName)


            Case "Link"
                'Anzeige alle Rohstoffe (Filter löschen)
                Me.Link = LinkFilter.Alle
                Dim UnCheckName As New List(Of String) From {"Rzpt", "Cloud", "Ohne"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Rzpt"
                'Anzeige alle Rohstoffe die mit einer Rezeptur verknüpft sind
                Me.Link = LinkFilter.Rzpt
                Dim UnCheckName As New List(Of String) From {"Cloud", "Ohne"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Cloud"
                'Anzeige alle Rohstoffe die mit der Cloud verknüpft sind
                Me.Link = LinkFilter.Cloud
                Dim UnCheckName As New List(Of String) From {"Rzpt", "Ohne"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Ohne"
                'Anzeige alle Rohstoffe ohne Verknüpfung (Rezept/Cloud)
                Me.Link = LinkFilter.NoLink
                Dim UnCheckName As New List(Of String) From {"Rzpt", "Cloud"}
                DataGridView.PopupItemsUncheck(UnCheckName)

        End Select
    End Sub

    ''' <summary>
    ''' Erzeugt eine neue Klasse LagerOrt. Das entsprechende Flag wird in der Datenbank gesetzt.
    ''' Anschliessend wird die Hash-Table in wb_Rohstoffe_global korrigiert.
    ''' </summary>
    ''' <param name="Flag"></param>
    ''' <param name="LagerOrt"></param>
    ''' <param name="RohNr"></param>
    Private Sub UpdateRohAktiv(Flag As String, LagerOrt As String, RohNr As Integer)
        'Daten aus Tabelle Lagerort
        Dim LgOrt As New wb_LagerOrt(LagerOrt)
        LgOrt.Aktiv = Flag

        'Hash-Table aktualisieren
        If RohAktiv.ContainsKey(RohNr) Then
            'Flag in Rohstoff(Tabell LG_Aktiv) eintragen
            RohStoff.LagerOrtAktiv = Flag
            'Flag in HashTable updaten
            RohAktiv(RohNr) = Flag
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