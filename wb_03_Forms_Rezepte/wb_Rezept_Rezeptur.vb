Imports System.Drawing
Imports System.Windows.Forms
Imports combit.ListLabel22.DataProviders
Imports EnhEdit.EnhEdit_Global
Imports Infralution.Controls.VirtualTree

Public Class wb_Rezept_Rezeptur

    Dim Rezept As wb_Rezept
    Dim RezeptHinweise As New wb_Hinweise(wb_Global.Hinweise.RezeptHinweise)

    Private _RzNummer As Integer
    Private _RzVariante As Integer
    Private _RzAendIndex As Integer
    Private _RzChanged As Boolean = False
    Private _RzKopfChanged As Boolean = False
    Private _RzHinweiseChanged As Boolean
    Private _Historical As Boolean = False

    Private _RezeptSchritt As wb_Rezeptschritt = Nothing    'aktuelle ausgewählter Rezeptschritt (Popup)
    Private _RezeptSchrittNeu As wb_Rezeptschritt = Nothing 'neuer Rezeptschritt (Auswahl-Liste)

    Private _HisSollwertDeltaStyle As New Infralution.Controls.StyleDelta
    Private _ProdStufeDeltaStyle As New Infralution.Controls.StyleDelta

    'Private _HisSollwertDeltaStyle As New Infralution.Controls.StyleDelta
    'Private _HisSollwertChangedStyle As Infralution.Controls.Style

    ''' <summary>
    ''' Objekt Rezeptur instanzieren.
    ''' 
    ''' Optional kann eine Änderungs-Nummer mit übergeben werden, dann werden die Rezept-Daten aus der Historie geladen.
    ''' Eine Änderung der Rezeptur ist dann nicht möglich
    ''' </summary>
    ''' <param name="RzNummer"></param>
    ''' <param name="RzVariante"></param>
    Public Sub New(RzNummer As Integer, RzVariante As Integer, Optional RzAendIndex As Integer = wb_Global.UNDEFINED)
        'Nur Anzeige bei historischen Rezepten
        _Historical = (RzAendIndex <> wb_Global.UNDEFINED)

        'Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        'New Style setzen (Delta/Italic+Bold)
        Dim FontFamilyArial As New FontFamily("Arial")
        Dim FontArial As New Font(FontFamilyArial, 16, FontStyle.Regular, GraphicsUnit.Pixel)
        _HisSollwertDeltaStyle.Font = New Drawing.Font(FontArial, System.Drawing.FontStyle.Italic + System.Drawing.FontStyle.Bold)
        _ProdStufeDeltaStyle.Font = New Drawing.Font(FontArial, System.Drawing.FontStyle.Bold)

        'Rezeptnummer und Rezept-Variante merken
        _RzNummer = RzNummer
        _RzVariante = RzVariante
        _RzAendIndex = RzAendIndex

        ' TabControl - HideTabs
        Wb_TabControl.HideTabs = True

        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbVariante.Fill(wb_Rezept_Shared.RzVariante)
        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbLiniengruppe.Fill(wb_Linien_Global.RezeptLinienGruppen)

        'Einlesen Rezeptkopf und Rezeptschritte 
        GetRezeptur(_RzNummer, _RzVariante, _RzAendIndex, _Historical)
        'Virtual Tree anzeigen
        VirtualTree.DataSource = Rezept.RootRezeptSchritt
        'alle Zeilen aufklappen
        VirtualTree.RootRow.ExpandChildren(True)
        'falls keine Rezeptschritte vorhanden sind muss das Popup-Menu ausserhalb erstellt werden
        VT_MakeTreePopup()

        'Bei der Anzeige von Rezepten aus der Historie sind keine Änderungen zulässig
        If _Historical Then
            tbRzNummer.Enabled = False
            tbRezeptName.Enabled = False
            tbRzKommentar.Enabled = False
            tbRzTeigTemp.Enabled = False
            tbKnetKennlinie.Enabled = False

            cbVariante.Enabled = False
            cbLiniengruppe.Enabled = False

            VirtualTree.Enabled = False
        End If

        'Cursor wieder zurücksetzen
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cbVariante_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbVariante.SelectedIndexChanged
        'Auswahl über DropDrown
        If cbVariante.Focus Then
            'Änderungen im aktuellen Rezept speichern
            RezeptSpeichern(sender)
            'Bezeichnung und Kommentar merken
            Dim Bezeichnung As String = Rezept.RezeptBezeichnung
            Dim Kommentar As String = Rezept.RezeptKommentar

            'Neue Rezept-Variante
            _RzVariante = cbVariante.GetKeyFromSelection()
            'Prüfen ob die gesuchte Rezept-Variante vorhanden ist
            If Not GetRezeptur(_RzNummer, _RzVariante, _RzAendIndex, _Historical) Then
                'Frage ob eine neue(leere) Variante erzeugt werden soll
                If MsgBox("Diese Rezept-Variante existiert nicht!" & vbCrLf & "Soll eine neue Variante dieses Rezeptes erzeugt werden", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    'Leere Hülle mit RezeptNr und Variante erzeugen - Rezeptschritte bleiben erhalten !
                    Rezept.MySQLdbNew(_RzVariante)
                    'Rezept-Name und Kommentar vom Stamm-Rezept
                    Rezept.RezeptBezeichnung = Bezeichnung
                    Rezept.RezeptKommentar = Kommentar
                    'Rezept-Schritte UNd Rezeptkopf schreiben bei Schliessen
                    _RzChanged = True
                    _RzKopfChanged = True
                Else
                    'Eintrag in Combo-Box Liniengruppe korrigieren
                    cbLiniengruppe.SetTextFromKey(Rezept.LinienGruppe)
                    'Eintrag in Combo-Box Rezeptvariante korrigieren
                    cbVariante.SetTextFromKey(Rezept.Variante)
                    'Rezept-Variante merken
                    _RzVariante = Rezept.Variante
                End If
            End If

            'Virtual Tree anzeigen
            VirtualTree.DataSource = Rezept.RootRezeptSchritt
            'alle Zeilen aufklappen
            VirtualTree.RootRow.ExpandChildren(True)
            'falls keine Rezeptschritte vorhanden sind muss das Popup-Menu ausserhalb erstellt werden
            VT_MakeTreePopup()
        End If
    End Sub

    Private Function GetRezeptur(RzNr As Integer, RzVariante As Integer, RzAendIndex As Integer, Historical As Boolean) As Boolean
        'Rezept-Historie
        If Not Historical Then
            'Rezeptkopf und Rezeptschritte aktuell (winback) - Start mit Backverlust 0.0 !! (kein Artikel definiert)
            Rezept = New wb_Rezept(_RzNummer, Nothing, 0.0, _RzVariante)
            Me.Text = Rezept.RezeptNummer & "/V" & Rezept.Variante & " " & Rezept.RezeptBezeichnung
        Else
            'Rezeptkopf und Rezeptschritte aus der Historie (wbdaten)
            Rezept = New wb_Rezept(_RzNummer, Nothing, _RzVariante, RzAendIndex)
            Me.Text = Rezept.RezeptNummer & "/V" & Rezept.Variante & " " & Rezept.RezeptBezeichnung & " Änderung " & RzAendIndex & " vom " & Rezept.AenderungDatum
        End If

        'Prüfen ob die gesuchte Variante existiert
        If Rezept.Variante = RzVariante Then

            'Berechnete Rezepturwerte anzeigen
            If Not Historical Then
                ShowCalculateRezeptDaten(False)
            End If

            'Rezeptnummer
            tbRzNummer.Text = Rezept.RezeptNummer
            'Rezept-Name
            tbRezeptName.Text = Rezept.RezeptBezeichnung
            'Kommentar-Feld
            tbRzKommentar.Text = Rezept.RezeptKommentar
            'Teigtemperatur (Rezept)
            tbRzTeigTemp.Text = wb_Functions.FormatStr(Rezept.RezeptTeigTemperatur, 2)

            'Änderung Nummer
            tbRzAendNr.Text = Rezept.AenderungNummer
            'Änderung Datum
            tbRzAendDatum.Text = Rezept.AenderungDatum
            'Änderung Name
            tbRzAendName.Text = Rezept.AenderungName
            'Rzept-Variante
            tbRzVariante.Text = Rezept.Variante

            'Eintrag in Combo-Box Liniengruppe ausfüllen
            cbLiniengruppe.SetTextFromKey(Rezept.LinienGruppe)
            'Eintrag in Combo-Box Rezeptvariante ausfüllen
            cbVariante.SetTextFromKey(Rezept.Variante)

            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ShowCalculateRezeptDaten(Recalculate As Boolean)
        'Neuberechnung erzwingen
        Rezept.Recalculate = Recalculate

        'Gesamt-Rohstoffpreis der Rezeptur (aktuell berechnet)
        tbRzPreis.Text = wb_Functions.FormatStr(Rezept.RezeptPreis, 2)
        'Rezeptgewicht (aktuell berechnet)
        tbRzGewicht.Text = wb_Functions.FormatStr(Rezept.RezeptGewicht, 3)
        'Mehlgesamt-Menge
        tbRzMehlmenge.Text = wb_Functions.FormatStr(Rezept.RezeptGesamtMehlmenge, 2)
        'Rezept TA
        tbRzTA.Text = CInt(Rezept.RezeptTA)
    End Sub

    ''' <summary>
    ''' Rezeptur drucken.
    ''' Der Ausdruck erfolgt über Printer-Sub-Funktion (List+Label)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnDrucken_Click(sender As Object, e As EventArgs) Handles BtnDrucken.Click
        'TODO anpassen (siehe Produktionsplanung)
        wb_Printer_Shared.LL.DataSource = New ObjectDataProvider(Rezept)
        wb_Printer_Shared.LL.Design()
    End Sub

    ''' <summary>
    ''' Aktuelle Rezeptur löschen.
    ''' Löschen ist nur möglich, wenn die Rezeptur nicht mehr verwendet wird und, bei Variante Eins, wenn keine weitere Variante existiert
    ''' Vor dem Löschen erfolgt eine Sicherheits-Abfrage
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnLoeschen_Click(sender As Object, e As EventArgs) Handles BtnLoeschen.Click
        'Rezeptkopf und Rezeptur löschen
        RzptLoeschen()
    End Sub

    ''' <summary>
    ''' Anzeige der berechneten Nährwerte der Rezeptur.
    ''' Berechnung über ktTyp301(Get) im Root-Rezeptschritt. Aufbau und Anzeige des DatenGrid in Subroutine nwtGrid()
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnNwt_Click(sender As Object, e As EventArgs) Handles BtnNwt.Click
        If tb_Rezeptur.Visible Then
            BtnNwt.Text = "Rezeptur"
            BtnHinweise.Text = "Zutatenliste"
            Wb_TabControl.SelectedTab = tb_Naehrwerte
            ToolStripAllergenLegende.Visible = True
            'Nährwerte-Grid aufbauen und anzeigen
            NwtGrid()
        Else
            BtnNwt.Text = "Nährwerte"
            BtnHinweise.Text = "Hinweise"
            Wb_TabControl.SelectedTab = tb_Rezeptur
            ToolStripAllergenLegende.Visible = False
        End If

    End Sub

    Public Sub RzptLoeschen()
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptInKomp, Rezept.RezeptNr)
        Dim Count As Integer = -1

        'Suche nach RezeptNr
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                Count = winback.iField("Used")
            End If
        End If
        'Datenbank wieder schliessen
        winback.Close()

        'Prüfen ob die Rezeptur noch in verschiedenen Artikeln verwendet wird
        If Count > 0 Then

            'Die Hauptvariante kann nur gelöscht werden, wenn das Rezept nicht mehr verwendet wird
            If Rezept.Variante = 1 Then
                MsgBox("Dieses Rezept wird noch verwendet" & vbCrLf & "Die Hauptvariante kann nicht gelöscht werden", MsgBoxStyle.Exclamation, "Rezeptur löschen")
                Exit Sub
            Else
                If MsgBox("Dieses Rezept wird noch verwendet" & vbCrLf & "Soll diese Variante trotzdem gelöscht werden ?", MsgBoxStyle.YesNo, "Rezeptur löschen") = vbNo Then
                    Exit Sub
                End If
            End If
        Else

            'Rezept kann gelöscht werden - Sicherheitshalber nochmal nachfragen
            If MsgBox("Soll dieses Rezept komplett gelöscht werden ?", MsgBoxStyle.YesNo, "Rezeptur löschen") = vbNo Then
                Exit Sub
            End If

            'Rezept-Historie löschen
            Rezept.MySQLdbDelete_HisRezept()
            Rezept.MySQLdbDelete_HisRezeptSchritte()

            'Rezept löschen
            Rezept.MySQLdbDelete_Rezept()
            Rezept.MySQLdbDelete_RezeptSchritte()

            'Rezept-Verarbeitungshinweise löschen
            RezeptHinweise.Delete()

            'Rezept muss nicht mehr gespeichert werden
            _RzChanged = False
            _RzKopfChanged = False

            'Fenster schliessen
            wb_Rezept_Shared.Liste_Refresh(Nothing)
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Anzeige der berechneten Nährwerte der Rezeptur im DatenGrid.
    ''' 
    ''' Die Daten werden im einem ein-dimensionalen Array vorbereitet und dann in einem eigenen Objekt (abgeleitet von DataGridView) angezeigt.
    ''' 
    ''' Das Array besteht aus dem Grundgerüst (Nummer, Bezeichnung, Einheit, Gruppe). Diese Daten kommen aus dem Hash-Table kt301Param(Nr)
    ''' Die Nährwert-Info kommt aus dem Array ktTyp301.Wert von Rezept._RootRezeptSchritt. 
    ''' Die Berechnung der Nährwerte startet über ktTyp301(Get) im RootRezeptschritt (Rekursiv) über alle unterlagerten Rezeptschritte.
    ''' </summary>
    Private Sub NwtGrid()
        'Daten im Grid anzeigen
        Dim nwtGrid As New wb_ArrayGridViewKomponParam301(Rezept.KtTyp301.NwtTabelle)
        nwtGrid.BackgroundColor = Me.BackColor
        nwtGrid.GridLocation(tb_Naehrwerte)
        nwtGrid.PerformLayout()
    End Sub

    ''' <summary>
    ''' Anzeige/Eingabe/Änderung des Text-Verarbeitungs-Hinweises für die Rezeptur.
    ''' Die Verarbeitungshinweise werden in der Tabelle winback.Hinweise2 abgelegt.
    ''' 'TODO evtl. Unterscheidung in verschiedene Fremdsprachen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnHinweise_Click(sender As Object, e As EventArgs) Handles BtnHinweise.Click
        If tb_Naehrwerte.Visible Then
            'Anzeige Zutatenliste
            Wb_TabControl.SelectedTab = tb_Zutaten
            tb_ZutatenListe.BorderStyle = Windows.Forms.BorderStyle.None
            Show_ZutatenListe()
        Else
            If tb_Hinweise.Visible Then
                'Rezeptur anzeigen
                Wb_TabControl.SelectedTab = tb_Rezeptur
            Else
                'Rezept-Hinweise lesen
                If Not RezeptHinweise.ReadOK Then
                    'TODO Rzeptvariante in Zukunft berücksichtigen
                    If RezeptHinweise.Read(_RzNummer) Then
                        TextHinweise.Text = RezeptHinweise.Memo
                        _RzHinweiseChanged = False
                    End If
                End If
                Wb_TabControl.SelectedTab = tb_Hinweise
            End If
        End If
    End Sub

    ''' <summary>
    ''' Fenster schliessen
    ''' Änderungen an der Rezeptur werden gespeichert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Fenster schliessen. Falls notwendig Daten sichern. Beim Schliessen werden geänderte Rezepthinweise, Rezepturdaten und Rezeptkopf-Daten
    ''' in die MySQL-DB geschrieben.
    ''' Beim Sichern der Rezepturdaten wird auch eine Kopie der aktuellen Rezeptur in die Rezept-Historie geschrieben
    ''' Mit dem Sicherung des Rezeptkopfes wird der Änderungs-Index erhöht (falls die Rezeptur geändert wurde).
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Rezept_Rezeptur_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RezeptSpeichern(sender)
    End Sub

    Private Sub RezeptSpeichern(sender As Object)
        'Flag Rezept-Hinweise sind geändert worden
        If _RzHinweiseChanged Then
            'Rezept-Verarbeitungs-Hinweise speichern
            RezeptHinweise.Memo = TextHinweise.Text
            RezeptHinweise.Write()
        End If

        'Rezeptur ist geändert worden
        If _RzChanged Then
            Rezept.MySQLdbWrite_RzSchritt(_RzNummer, _RzVariante)
        End If

        'Die Nährwerte aller mit dieser Rezeptur verknüpften Artikel/Rohstoffe müssen neu berechnet und an OrgaBack geschrieben werden (Flag setzen)
        If _RzChanged Then
            Rezept.ArtikelMarkieren()
        End If

        'Rezeptkopfdaten schreiben
        If _RzKopfChanged Or _RzChanged Then
            Rezept.MySQLdbWrite_Rezept(_RzChanged)
            wb_Rezept_Shared.Liste_Refresh(sender)
        End If
    End Sub

    ''' <summary>
    ''' Anzeige der Rezept-Hinweise.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextHinweise_Click(sender As Object, e As EventArgs) Handles TextHinweise.Click
        'Flag setzen - Rezepthinweise speichern
        _RzHinweiseChanged = True
        ToolStripRezeptChange.Visible = True
    End Sub

    ''' <summary>
    ''' Doppel-Click auf eine Rezept-Zeile öffnet das Rezept-im-Rezept, falls vorhanden.
    ''' 
    ''' Wenn VirtualTree.RowSelect = False ist, muss die Rezeptnummer über
    '''     Dim RezeptNr As Integer = DirectCast(sCellWidget.Tree.SelectedItem, wb_Rezeptschritt).RezeptNr
    ''' ermittelt werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_CellDoubleClick(sender As Object, e As EventArgs) Handles VirtualTree.CellDoubleClick
        'Doppel-Click auf VirtualTree-Cell
        Dim sCellWidget As CellWidget = sender
        'interne Rezeptnummer zum Rezeptschritt ermitteln
        Dim RezeptNr As Integer = DirectCast(sCellWidget.Row.Item, wb_Rezeptschritt).RezeptNr
        'wenn es eine Rezept-Im-Rezept-Struktur gibt wird das Rezept in einem neuen Fenster geöffnet
        If RezeptNr > 0 Then
            'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen
            Me.Cursor = Cursors.WaitCursor
            Dim Rezeptur As New wb_Rezept_Rezeptur(RezeptNr, _RzVariante)
            'MDI-Fenster anzeigen
            Rezeptur.Show()
        End If
    End Sub

    Private Sub BtnVerwendung_Click(sender As Object, e As EventArgs) Handles BtnVerwendung.Click
        If tb_Verwendung.Visible Then
            'Rezeptur anzeigen
            Wb_TabControl.SelectedTab = tb_Rezeptur
        Else
            'Verwendung anzeigen
            'Liste der Tabellen-Überschriften
            'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
            'Spalten ohne Bezeichnung werden ausgeblendet
            Dim sColNames As New List(Of String) From {"Nummer", "&Name", "Kommentar"}
            For Each sName In sColNames
                GridView_RzVerwendung.ColNames.Add(sName)
            Next

            'DataGrid füllen
            GridView_RzVerwendung.LoadData(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptVerwendung, _RzNummer), "Rezept-Verwendung")
            Wb_TabControl.SelectedTab = tb_Verwendung
        End If

    End Sub

    Private Sub SwENummern_Click(sender As Object, e As EventArgs) Handles SwENummern.Click
        Show_ZutatenListe()
    End Sub

    Private Sub Show_ZutatenListe()
        If SwENummern.Checked Then
            tb_ZutatenListe.Text = Rezept.ZutatenListe(wb_Global.ZutatenListeMode.Show_ENummer)
        Else
            tb_ZutatenListe.Text = Rezept.ZutatenListe(wb_Global.ZutatenListeMode.Hide_ENummer)
        End If

        'Mehlzusammensetzung (Berechnet aus der Zutatenliste - getrennt durch Zeilenvorschub)
        tbMehlZusammenSetzung.Text = Rezept.MehlZusammensetzung(vbCrLf)
    End Sub

    ''' <summary>
    ''' Eingabe-Feld wurde selektiert. (Mausklick oder Tastatur-Navigation)
    ''' Setzt die Parameter für den Editor: Format, Eingabe: Ober- und Untergrenze
    ''' 
    ''' Verhindert, dass einzelne Zellen markiert werden 
    ''' (Infralution Support): handle the SelectionChanging event and set Cancel to true. This prevents any selection occurring
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_SelectionChanging(sender As Object, e As SelectionChangingEventArgs) Handles VirtualTree.SelectionChanging
        'Daten aus dem aktuell ausgewählten Rezeptschritt
        _RezeptSchritt = DirectCast(e.StartRow.Item, wb_Rezeptschritt)

        'Einstellungen Editor
        'Debug.Print("VirtualTree_SelectionChanging " & _RezeptSchritt.Bezeichnung & " UG/OG/Format " & _RezeptSchritt.UnterGW & "/" & _RezeptSchritt.OberGW & "/" & _RezeptSchritt.Format)

        'Eingabe Text/Sollwert
        If _RezeptSchritt.Format = wb_Format.fString Then
            DirectCast(EnhEditText.Control, EnhEdit.EnhEdit).Init = True
            DirectCast(EnhEditText.Control, EnhEdit.EnhEdit).eFormat = _RezeptSchritt.Format
            DirectCast(EnhEditText.Control, EnhEdit.EnhEdit).eOG = _RezeptSchritt.OberGW
            DirectCast(EnhEditText.Control, EnhEdit.EnhEdit).eUG = _RezeptSchritt.UnterGW
        Else
            DirectCast(EnhEdit.Control, EnhEdit.EnhEdit).Init = True
            DirectCast(EnhEdit.Control, EnhEdit.EnhEdit).eFormat = _RezeptSchritt.Format
            DirectCast(EnhEdit.Control, EnhEdit.EnhEdit).eOG = _RezeptSchritt.OberGW
            DirectCast(EnhEdit.Control, EnhEdit.EnhEdit).eUG = _RezeptSchritt.UnterGW
        End If

        'Verhindert dass einzelne Zellen markiert werden
        e.Cancel = True
    End Sub

    Private Sub VirtualTree_SetCellValue(sender As Object, e As SetCellValueEventArgs) Handles VirtualTree.SetCellValue
        Dim Binding As RowBinding = _VirtualTree.GetRowBinding(e.Row)

        'aktuell ausgewählten Rezeptschritt
        _RezeptSchritt = DirectCast(e.Row.Item, wb_Rezeptschritt)
        'alten Sollwert merken (wird in RS_Wert_HIS gespeichert)
        _RezeptSchritt.SaveSollwert_org()

        Binding.SetCellValue(e.Row, e.Column, e.OldValue, e.NewValue)
        ShowCalculateRezeptDaten(True)
        'Rezeptur wurde geändert
        _RzChanged = True
        ToolStripRezeptChange.Visible = True
    End Sub

    Private Sub VirtualTree_GetCellData(sender As Object, e As GetCellDataEventArgs) Handles VirtualTree.GetCellData
        'get the default binding for the given row And use it to populate the cell data
        Dim Binding As RowBinding = _VirtualTree.GetRowBinding(e.Row)
        Binding.GetCellData(e.Row, e.Column, e.CellData)

        'aktuell ausgewählten Rezeptschritt merken (Popup)
        _RezeptSchritt = DirectCast(e.Row.Item, wb_Rezeptschritt)

        If _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Then
            VirtualTree_SetFontStyle(e.CellData.EvenStyle, _ProdStufeDeltaStyle)
            VirtualTree_SetFontStyle(e.CellData.OddStyle, _ProdStufeDeltaStyle)
        End If

        'Edit Bezeichnungs-Text
        If e.Column.Name = "ColBezeichnung" And wb_Functions.TypeIstText(_RezeptSchritt.Type) Then
            'Bei Anzeige der Rezept-Historie werden die geänderten Werte Fett/Kursiv dargestellt
            If _Historical And _RezeptSchritt.WertProd <> "" Then
                VirtualTree_SetFontStyle(e.CellData.EvenStyle, _HisSollwertDeltaStyle)
                VirtualTree_SetFontStyle(e.CellData.OddStyle, _HisSollwertDeltaStyle)
            End If
            'Editor aktiv - Bezeichnungstext
            Exit Sub
        End If

        'Edit Sollwert
        If e.Column.Name = "ColSollwert" And (wb_Functions.TypeIstSollMenge(_RezeptSchritt.Type, 1) Or wb_Functions.TypeIstSollWert(_RezeptSchritt.Type, 3)) Then
            'Bei Anzeige der Rezept-Historie werden die geänderten Werte Fett/Kursiv dargestellt
            If _Historical And _RezeptSchritt.WertProd <> "" Then
                VirtualTree_SetFontStyle(e.CellData.EvenStyle, _HisSollwertDeltaStyle)
                VirtualTree_SetFontStyle(e.CellData.OddStyle, _HisSollwertDeltaStyle)
            End If
            'Editor aktiv - Sollwert
            Exit Sub
        End If

        'Alle Anderen - Edit nicht erlaubt
        e.CellData.Editor = Nothing
    End Sub

    ''' <summary>
    ''' Rechte-Maus-Click auf eine Zeile im VirtualTree.
    ''' 
    ''' Über e.Row.Item wird die entsprechende Zeile(wb_Rezeptschritt) im Grid ermittelt.
    ''' Abhängig vom Komponenten-Typ werden verschiedene Aktionen erlaubt/ausgelöst. (Popup-Menu)
    ''' Wenn die aktuelle Rezeptzeile ein Child eine Rezeptschrittes ist (Wassertemperatur o.ä.) 
    ''' wird auf den jeweiligen Parent verwiesen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_GetContextMenuStrip(sender As Object, e As GetContextMenuStripEventArgs) Handles VirtualTree.GetContextMenuStrip
        'aktuell ausgewählten Rezeptschritt merken (Popup)
        _RezeptSchritt = DirectCast(e.Row.Item, wb_Rezeptschritt)
        'Click auf Rezept-Zeile mit Parameter ungleich 1
        If _RezeptSchritt.ParamNr <> 1 Then
            _RezeptSchritt = _RezeptSchritt.ParentStep
        End If

        'Popup-Menu wird dynamisch erzeugt
        VT_MakeTreePopup()
    End Sub


    ''' <summary>
    ''' Setzt den Font.Style für die angegebene Zelle auf Bold/Italic
    ''' Anzeige Artikel/Rezept-Zeilen
    ''' </summary>
    ''' <param name="ColumnStyle"></param>
    Private Sub VirtualTree_SetFontStyle(ByRef ColumnStyle As Infralution.Controls.Style, DeltaStyle As Infralution.Controls.StyleDelta)
        Dim _ChangedStyle = New Infralution.Controls.Style(ColumnStyle, DeltaStyle)
        ColumnStyle = _ChangedStyle
    End Sub

    ''' <summary>
    ''' Neue Komponente(Auswahl) in leeres Rezept ein- oder an eine Rezeptur anfügen (Click in das leere Feld unterhalb VirtualTree)
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueKomponente(Sender As Object, e As EventArgs)
        'Auswahlliste Rohstoff
        If VT_AuswahlRohstoff() Then
            'Es gibt keinen ausgewählten Rezeptschritt - neu erstellen
            _RezeptSchritt = New wb_Rezeptschritt(Rezept.RootRezeptSchritt, _RezeptSchrittNeu.Bezeichnung)
            _RezeptSchritt.CopyFrom(_RezeptSchrittNeu)
            _RezeptSchritt.SchrittNr = Rezept.RootRezeptSchritt.ChildSteps.Count + 1
            VT_AddChildSteps(_RezeptSchritt)
            'Focus auf Eingabe Sollwert
            VT_Aktualisieren(ColSollwert, False)
        End If
    End Sub

    ''' <summary>
    ''' Neue Komponente(Auswahl) vor der aktuellen Zeile (_Rezeptschritt) einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueKomponenteDavor(Sender As Object, e As EventArgs)
        'Auswahl der Rohstoffe in der Liste begrenzen
        Dim AuswahlFilter As wb_Rohstoffe_Shared.AnzeigeFilter
        Select Case _RezeptSchritt.Type
            Case wb_Global.KomponTypen.KO_TYPE_KNETER
                AuswahlFilter = wb_Rohstoffe_Shared.AnzeigeFilter.NurKneter
            Case wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT
                AuswahlFilter = wb_Rohstoffe_Shared.AnzeigeFilter.OhneKneter
            Case Else
                AuswahlFilter = wb_Rohstoffe_Shared.AnzeigeFilter.OhneKneter
        End Select

        'Auswahlliste Rohstoff
        If VT_AuswahlRohstoff(AuswahlFilter) Then
            _RezeptSchritt.Insert(_RezeptSchrittNeu, False)
            VT_AddChildSteps(_RezeptSchrittNeu)
            'Focus auf Eingabe Sollwert
            VT_Aktualisieren(ColSollwert, True)
        End If
    End Sub

    ''' <summary>
    ''' Neue Komponente(Auswahl) nach der aktuellen Zeile einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueKomponenteDanach(Sender As Object, e As EventArgs)
        'Auswahl der Rohstoffe in der Liste begrenzen
        Dim AuswahlFilter As wb_Rohstoffe_Shared.AnzeigeFilter
        Select Case _RezeptSchritt.Type
            Case wb_Global.KomponTypen.KO_TYPE_KNETER
                AuswahlFilter = wb_Rohstoffe_Shared.AnzeigeFilter.NurKneter
            Case wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT
                AuswahlFilter = wb_Rohstoffe_Shared.AnzeigeFilter.OhneKneter
            Case Else
                AuswahlFilter = wb_Rohstoffe_Shared.AnzeigeFilter.Undefined
        End Select

        'Auswahlliste Rohstoff
        If VT_AuswahlRohstoff(AuswahlFilter) Then
            'Bei KneterKomponenten
            'Anfügen Rezeptschritt unterhalb Produktions-Stufe oder Kessel
            If _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Or _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_KESSEL Then
                _RezeptSchritt.InsertChild(_RezeptSchrittNeu)
            Else
                _RezeptSchritt.Insert(_RezeptSchrittNeu, True)
            End If
            VT_AddChildSteps(_RezeptSchrittNeu)
            'Der neu eingefügte Rezeptschritt wird der aktuelle Rezeptschritt (Tastatur-Bedienung INSERT)
            _RezeptSchritt = _RezeptSchrittNeu
            'Focus auf Eingabe Sollwert
            VT_Aktualisieren(ColSollwert, False)
        End If
    End Sub

    ''' <summary>
    ''' Neue Produktions-Stufe in ein leeres Rezept ein- oder an eine Rezeptur anfügen (Click in das leere Feld unterhalb VirtualTree)
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueProduktionsStufe(Sender As Object, e As EventArgs)
        _RezeptSchritt = New wb_Rezeptschritt(Rezept.RootRezeptSchritt, wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE)
        _RezeptSchritt.SchrittNr = Rezept.RootRezeptSchritt.ChildSteps.Count
        'Focus auf Eingabe Sollwert
        VT_Aktualisieren(ColBezeichnung, False)
    End Sub

    ''' <summary>
    ''' Neuen Kessel nach Produktions-Stufe einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeuerKesselNachProduktionsStufe(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_KESSEL)
        _RezeptSchritt.Insert(_RezeptSchrittNeu, True)
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neuen Kessel oberhalb der aktuellen Kessel-Zeile einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeuerKesselDavor(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_KESSEL)
        _RezeptSchritt.Insert(_RezeptSchrittNeu, False)
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neuen Kessel unterhalb der aktuellen Kessel-Zeile einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeuerKesselDanach(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_KESSEL)
        'Anfügen Rezeptschritt unterhalb Kessel
        _RezeptSchritt.Insert(_RezeptSchrittNeu, True)
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neue Produktions-Stufe nach demr aktuellen Zeile (_Rezeptschritt) einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueProduktionsStufeDanach(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE)
        _RezeptSchritt.Insert(_RezeptSchrittNeu, True)
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neue Produktions-Stufe einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueProduktionsStufeDavor(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE)
        _RezeptSchritt.Insert(_RezeptSchrittNeu, False)
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neue Text-Komponente in ein leeres Rezept ein- oder an eine Rezeptur anfügen (Click in das leere Feld unterhalb VirtualTree)
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueTextKomponente(Sender As Object, e As EventArgs)
        _RezeptSchritt = New wb_Rezeptschritt(Rezept.RootRezeptSchritt, wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE)
        _RezeptSchritt.SchrittNr = Rezept.RootRezeptSchritt.ChildSteps.Count
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neue Text-Komponente vor der aktuellen Zeile (_Rezeptschritt) einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueTextKomponenteDavor(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE)
        _RezeptSchritt.Insert(_RezeptSchrittNeu, False)
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neue Text-Komponente nach der aktuellen Zeile einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueTextKomponenteDanach(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE)
        'Anfügen Rezeptschritt unterhalb Produktions-Stufe oder Kessel
        If _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Or _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_KESSEL Then
            _RezeptSchritt.InsertChild(_RezeptSchrittNeu)
        Else
            _RezeptSchritt.Insert(_RezeptSchrittNeu, True)
        End If
        _RezeptSchritt.SchrittNr = Rezept.RootRezeptSchritt.ChildSteps.Count
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Aktuelles Kneter-Rezept speichern in Tabelle RohParams
    ''' 
    ''' Alle Child-Steps unterhalb der Kneter-Kopfzeile werden als neues Rezept in der Tabelle RohParams
    ''' gespeichert
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_KneterRezept_Speichern(Sender As Object, e As EventArgs)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'vorhandene Kneter-Rezeptur in Datenbank löschen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelKneterRzt, _RezeptSchritt.RohNr))

        'Index
        Dim Idx As Integer = 0

        'Schleife über alle Rezeptschritte
        For Each rz As wb_Rezeptschritt In _RezeptSchritt.ChildSteps
            Idx += 1
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlInsKneterRzt, _RezeptSchritt.RohNr, Idx & ",'" & rz.RohNr & "','" & rz.Bezeichnung & "'")
            winback.sqlCommand(sql)
        Next
        winback.Close()

        'Meldung ausgeben
        MsgBox("Die Kneter-Rezeptur wurde unter " & _RezeptSchritt.Bezeichnung & " gespeichert ", MsgBoxStyle.Information, "Kneter-Rezeptur")

    End Sub

    ''' <summary>
    ''' Aktuelle Rezeptzeile löschen (Popup oder DEL-Key)
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_Delete(Sender As Object, e As EventArgs)
        _RezeptSchritt.Delete()
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Anzeige der Rohstoff-Liste. 
    ''' Auswahl eines Rohstoffes für die Funktionen Einfügen, Anfügen, ..
    ''' 
    ''' Der Rezeptschritt hat keine Verbindung zur Rezeptur und wird nur zur Auswahl des Rohstoffes verwendet.
    ''' Die Verknüpfung erfolgt über Rezeptschritt.Insert
    ''' </summary>
    ''' <returns></returns>
    Private Function VT_AuswahlRohstoff(Optional AnzeigeFilter As wb_Rohstoffe_Shared.AnzeigeFilter = wb_Rohstoffe_Shared.AnzeigeFilter.Undefined) As Boolean
        'Rohstoff-Auswahl-Liste
        Dim RohstoffAuswahl As New wb_Rohstoff_AuswahlListe

        'Rohstoff-Auswahl filtern Sauerteig/Produktion
        If _RzVariante = 0 Then
            RohstoffAuswahl.Anzeige = wb_Rohstoffe_Shared.AnzeigeFilter.Sauerteig
        Else
            If AnzeigeFilter <> wb_Rohstoffe_Shared.AnzeigeFilter.Undefined Then
                RohstoffAuswahl.Anzeige = AnzeigeFilter
            Else
                RohstoffAuswahl.Anzeige = wb_Rohstoffe_Shared.AnzeigeFilter.RezeptKomp
            End If
        End If

        'Anzeige Auswahl-Fenster
        If RohstoffAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, RohstoffAuswahl.RohstoffName)
            _RezeptSchrittNeu.RohNr = RohstoffAuswahl.RohstoffNr
            _RezeptSchrittNeu.Nummer = RohstoffAuswahl.RohstoffNummer
            _RezeptSchrittNeu.Kommentar = RohstoffAuswahl.RohstoffKommentar
            _RezeptSchrittNeu.ParamNr = 1
            _RezeptSchrittNeu.Sollwert = "0,000"
            _RezeptSchrittNeu.Type = RohstoffAuswahl.RohstoffType
            _RezeptSchrittNeu.Format = RohstoffAuswahl.RohstoffFormat
            _RezeptSchrittNeu.UnterGW = RohstoffAuswahl.RohstoffUG
            _RezeptSchrittNeu.OberGW = RohstoffAuswahl.RohstoffOG
            _RezeptSchrittNeu.Einheit = wb_Language.TextFilter(RohstoffAuswahl.RohstoffEinheit)
            'bei Kneterkomponenten müssen die Paramter aus Tabelle KomponParams gesetzt werden
            _RezeptSchrittNeu.SetType118()
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Anzeige im Virtual-Tree aktualisieren
    ''' </summary>
    Private Sub VT_Aktualisieren()
        VirtualTree.Invalidate()
        VirtualTree.DataSource = Rezept.RootRezeptSchritt
        'alle Zeilen aufklappen
        VirtualTree.RootRow.ExpandChildren(True)
        'Rezeptur wurde geändert
        _RzChanged = True
        ToolStripRezeptChange.Visible = True
    End Sub

    ''' <summary>
    ''' Anzeige im Virtual-Tree aktualisieren. Danach Editor auf Sollwert starten.
    ''' Über EditColumn wird die entsprechende Spalte (Edit) ausgewählt.
    ''' </summary>
    ''' <param name="EditColumn"></param>
    Private Sub VT_Aktualisieren(EditColumn As Column, Before As Boolean)
        Dim FocusItem As wb_Rezeptschritt = VirtualTree.FocusItem
        VT_Aktualisieren()
        VirtualTree.FocusItem = FocusItem
        If Before Then
            VirtualTree.SelectPriorRow()
        Else
            VirtualTree.SelectNextRow()
        End If
        VirtualTree.SelectedColumn = EditColumn
        VirtualTree.EditCurrentCellInFocusRow()
    End Sub


    ''' <summary>
    ''' Wenn der neu eingefügte Rezeptschritt mehrere Zeilen haben kann werden diese aus der KomponTypen-Tabelle
    ''' ausgelesen und als Child-Rezeptschritte eingefügt.
    ''' 
    ''' Kneter-Zeilen werden aus der Tabelle RohParams eingefügt
    ''' </summary>
    Private Sub VT_AddChildSteps(ByRef rs As wb_Rezeptschritt)
        If wb_Functions.TypeHasChildSteps(rs.Type) Then
            Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
            Dim sql As String = ""
            Dim SchrittNr As Integer = rs.SchrittNr

            If rs.Type = wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT Then
                'Die Kneter-Rezeptur wird aus der Tabelle RohParams gelesen
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKneterRezept, rs.RohNr)
            Else
                'Nachfolgende Rezept-Schritte aus Tabelle KomponParams 
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRohstoffRez, rs.RohNr)
            End If

            'Datenbank-Verbindung
            winback.sqlSelect(sql)
            While winback.Read
                Dim Bezeichnung As String = wb_Functions.MySqlToUtf8(winback.sField("KO_Bezeichnung"))
                Dim rsc As New wb_Rezeptschritt(Nothing, Bezeichnung)
                rsc.Kommentar = wb_Functions.MySqlToUtf8(winback.sField("KO_Kommentar"))

                If rs.Type = wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT Then
                    SchrittNr += 1
                    rsc.SchrittNr = SchrittNr
                    rsc.RohNr = winback.iField("KO_Nr")
                    rsc.Nummer = winback.sField("KO_Nr_AlNum")
                    rsc.Type = wb_Functions.IntToKomponType(winback.iField("KT_Typ_Nr"))
                    rsc.ParamNr = 1
                    rsc.SetType118()
                Else
                    rsc.SchrittNr = rs.SchrittNr
                    rsc.RohNr = rs.RohNr
                    rsc.Nummer = rs.Nummer
                    rsc.Type = rs.Type
                    rsc.ParamNr = winback.iField("KT_ParamNr")
                    rsc.Sollwert = "0,000"
                    rsc.Einheit = wb_Language.TextFilter(winback.sField("E_Einheit"))
                End If

                rs.InsertChild(rsc)
            End While
        End If
    End Sub

    Private Sub VT_MakeTreePopup()
        'Größe des Arrays entspricht der Anzahl der Einträge in der Enumeration in wb_global
        Dim _PopupFunctions([Enum].GetValues(GetType(wb_Global.TPopupFunctions)).Length + 1) As Boolean

        'Sonderfall - das Rezept ist leer
        If _RezeptSchritt Is Nothing Then
            _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe) = True
            _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente) = True
            _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente) = True
        Else
            'Type des aktuell ausgewählten Rezeptschrittes
            Dim rsTypeAkt As wb_Global.KomponTypen = _RezeptSchritt.Type

            'Popup-Menu wird abhängig von der ausgewählten Zeile dynamisch erstellt
            Select Case rsTypeAkt

                Case wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe_Davor) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_Loeschen) = True

                    'neuer Kessel/Komponente nur wenn noch keine Child-Schritte vorhanden sind
                    If _RezeptSchritt.ChildSteps.Count = 0 Then
                        _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Darunter) = True
                        _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Danach) = True
                        _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Danach) = True
                    Else
                        'wenn der erste Child-Step ein Kessel ist dürfen nur noch Kessel angefügt werden
                        'sonst nur Komponenten oder Text-Zeilen
                        If DirectCast(_RezeptSchritt.ChildSteps(0), wb_Rezeptschritt).Type = wb_Global.KomponTypen.KO_TYPE_KESSEL Then
                            _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Darunter) = True
                        Else
                            _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Danach) = True
                            _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Danach) = True
                        End If
                    End If

                Case wb_Global.KomponTypen.KO_TYPE_KESSEL
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Davor) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_Loeschen) = True

                Case wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Davor) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Davor) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_KneterRezept_Speichern) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_Loeschen) = True

                Case wb_Global.KomponTypen.KO_TYPE_KNETER
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Davor) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_Loeschen) = True

                Case Else
                    'Vor dem ersten Rezeptschritt kann eine Produktions-Stufe eingefügt werden
                    If _RezeptSchritt.SchrittNr = 1 Then
                        _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe_Davor) = True
                    End If
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Davor) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Davor) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_Loeschen) = True

            End Select

            '_PopupFunctions(wb_Global.TPopupFunctions.TP_Editieren) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_TTS_loeschen) = True

            '_PopupFunctions(wb_Global.TPopupFunctions.TP_Verschieben_Oben) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_Verschieben_Unten) = True

            '_PopupFunctions(wb_Global.TPopupFunctions.TP_TeigTemp) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_RohstoffVerwaltung) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_Naehrwerte_Laden) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_QuidDeklaration) = True

        End If

        'Popup-Menu erstellen
        VTPopUpMenu.Items.Clear()

        'Neue Komponente
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente) Then
            VTPopUpMenu.Items.Add("Neue Komponente", Nothing, AddressOf VTP_NeueKomponente)
        End If
        'Neue Komponente am Ende anfügen
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Darunter) Then
            VTPopUpMenu.Items.Add("Neue Komponente anfügen", Nothing, AddressOf VTP_NeueKomponenteDanach)
        End If
        'Neue Komponente davor
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Davor) Then
            VTPopUpMenu.Items.Add("Neue Komponente davor", Nothing, AddressOf VTP_NeueKomponenteDavor)
        End If
        'Neue Komponente danach
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Danach) Then
            VTPopUpMenu.Items.Add("Neue Komponente danach", Nothing, AddressOf VTP_NeueKomponenteDanach)
        End If

        'Neue Produktions-Stufe
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe) Then
            VTPopUpMenu.Items.Add("Neue Produktions-Stufe", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'Neue Produktions-Stufe davor
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe_Davor) Then
            VTPopUpMenu.Items.Add("Neue Produktions-Stufe davor", Nothing, AddressOf VTP_NeueProduktionsStufeDavor)
        End If
        'Neue Produktions-Stufe danach
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe_Danach) Then
            VTPopUpMenu.Items.Add("Neue Produktions-Stufe danach", Nothing, AddressOf VTP_NeueProduktionsStufeDanach)
        End If

        'Neuer Kessel nach der Produktions-Stufe
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Darunter) Then
            VTPopUpMenu.Items.Add("Neuer Kessel", Nothing, AddressOf VTP_NeuerKesselNachProduktionsStufe)
        End If
        'Neuer Kessel davor
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Davor) Then
            VTPopUpMenu.Items.Add("Neuer Kessel davor", Nothing, AddressOf VTP_NeuerKesselDavor)
        End If
        'Neuer Kessel danach
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Danach) Then
            VTPopUpMenu.Items.Add("Neuer Kessel danach", Nothing, AddressOf VTP_NeuerKesselDanach)
        End If

        'Neue Textkomponente
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente) Then
            VTPopUpMenu.Items.Add("Neue Textkomponente", Nothing, AddressOf VTP_NeueTextKomponente)
        End If
        'Neue Textkomponente am Ende anfügen
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Darunter) Then
            VTPopUpMenu.Items.Add("Neue Textkomponente anfügen", Nothing, AddressOf VTP_NeueTextKomponente)
        End If
        'Neue Textkomponente davor
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Davor) Then
            VTPopUpMenu.Items.Add("Neue Textkomponente davor", Nothing, AddressOf VTP_NeueTextKomponenteDavor)
        End If
        'Neue Textkomponente danach
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Danach) Then
            VTPopUpMenu.Items.Add("Neue Textkomponente danach", Nothing, AddressOf VTP_NeueTextKomponenteDanach)
        End If

        'Bearbeiten
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_Editieren) Then
            VTPopUpMenu.Items.Add("Bearbeiten", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'Kneter-Rezept speichern
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_KneterRezept_Speichern) Then
            VTPopUpMenu.Items.Add("Kneter-Rezeptur speichern", Nothing, AddressOf VTP_KneterRezept_Speichern)
        End If
        'Löschen
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_Loeschen) Then
            VTPopUpMenu.Items.Add("Löschen", Nothing, AddressOf VTP_Delete)
        End If
        'TTS-Parameter löschen
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_TTS_loeschen) Then
            VTPopUpMenu.Items.Add("Reset TTS-Parameter", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If

        'Nach oben verschieben
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_Verschieben_Oben) Then
            VTPopUpMenu.Items.Add("Zeile nach oben verschieben", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'Nach unten verschieben
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_Verschieben_Unten) Then
            VTPopUpMenu.Items.Add("Zeile nach unten verschieben", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If

        'Anzeige Teigtemperatur-Parameter
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_TeigTemp) Then
            VTPopUpMenu.Items.Add("Anzeige der TTS-Parameter", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If

        'Rohstoff-Verwaltung
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_RohstoffVerwaltung) Then
            VTPopUpMenu.Items.Add("Rohstoff-Verwaltung", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'Nährwerte laden
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_Naehrwerte_Laden) Then
            VTPopUpMenu.Items.Add("Nährwerte aktualisieren", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'QUID-Deklaration
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_QuidDeklaration) Then
            VTPopUpMenu.Items.Add("QUID relevant", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If

    End Sub

    ''' <summary>
    ''' Click auf eine Rezept-Zeile
    ''' Der aktuelle Rezeptschritt wird gespeichert. Damit können über die Tastatur INSERT/DEL die Rezeptschritte eingefügt/gelöscht werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_CellClick(sender As Object, e As EventArgs) Handles VirtualTree.CellClick
        'aktuell ausgewählten Rezeptschritt merken
        _RezeptSchritt = DirectCast(sender.Row.Item, wb_Rezeptschritt)
    End Sub

    ''' <summary>
    ''' Taste gedrück innerhalb des virtual Tree.
    ''' Anhand des Tasten-Codes wird die entsprechende Sub-Routine ausgeführt (INSERT/DEL)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_KeyUp(sender As Object, e As KeyEventArgs) Handles VirtualTree.KeyUp

        Select Case e.KeyCode
            Case Keys.Insert
                'Sonderfall - das Rezept ist leer
                If _RezeptSchritt Is Nothing Then
                    VTP_NeueKomponente(sender, e)
                Else
                    VTP_NeueKomponenteDanach(sender, e)
                End If
            Case Keys.Delete
                VTP_Delete(sender, e)
        End Select

    End Sub

    Private Sub tbRezeptName_Leave(sender As Object, e As EventArgs) Handles tbRezeptName.Leave
        CheckTextBoxChanged(tbRezeptName, Rezept.RezeptBezeichnung)
    End Sub

    Private Sub tbRzKommentar_Leave(sender As Object, e As EventArgs) Handles tbRzKommentar.Leave
        CheckTextBoxChanged(tbRzKommentar, Rezept.RezeptKommentar)
    End Sub

    Private Sub tbRzNummer_Leave(sender As Object, e As EventArgs) Handles tbRzNummer.Leave
        CheckTextBoxChanged(tbRzNummer, Rezept.RezeptNummer)
    End Sub

    Private Function CheckTextBoxChanged(tb As TextBox, ByRef Value As String) As Boolean
        If tb.Text <> Value Then
            _RzKopfChanged = True
            Value = tb.Text
            ToolStripRezeptChange.Visible = True
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Zeichnet die Texte der Combo-Box Rezept-Varianten. Wenn die Variante für dieses Rezept vorhanden ist, 
    ''' wird der Text in Schwarz gezeichnet, sonst in grau.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbVariante_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles cbVariante.DrawItem
        'Variante-Nr des Eintrags
        Dim VarianteText As String = cbVariante.Items(e.Index).ToString
        Dim Variante As Integer = cbVariante.GetKeyFromText(VarianteText)
        'wenn die Variante für dieses Rezept vorhanden ist, wird der Text in Schwarz gezeichnet, sonst in grau
        If Rezept.HasVariante(Variante) Then
            e.Graphics.DrawString(VarianteText, e.Font, New SolidBrush(Color.Black), e.Bounds.X, e.Bounds.Y)
        Else
            e.Graphics.DrawString(VarianteText, e.Font, New SolidBrush(Color.LightGray), e.Bounds.X, e.Bounds.Y)
        End If
    End Sub

End Class