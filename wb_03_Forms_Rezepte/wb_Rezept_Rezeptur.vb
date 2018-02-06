Imports System.Windows.Forms
Imports combit.ListLabel22.DataProviders
Imports Infralution.Controls.VirtualTree

Public Class wb_Rezept_Rezeptur

    Dim Rezept As wb_Rezept
    Dim RezeptHinweise As New wb_Hinweise(wb_Global.Hinweise.RezeptHinweise)
    Dim NwtTabelle(wb_Global.maxTyp301) As wb_Global.Nwt

    Private _RzNummer As Integer
    Private _RzVariante As Integer
    Private _RzHinweiseChanged As Boolean

    Private _RezeptSchritt As wb_Rezeptschritt = Nothing
    Private _RezeptSchrittNeu As wb_Rezeptschritt = Nothing

    ''' <summary>
    ''' Objekt Rezeptur instanzieren
    ''' </summary>
    ''' <param name="RzNummer"></param>
    ''' <param name="RzVariante"></param>
    Public Sub New(RzNummer As Integer, RzVariante As Integer)

        'Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'Rezeptnummer und Rezept-Variante merken
        _RzNummer = RzNummer
        _RzVariante = RzVariante

        ' TabControl - HideTabs
        Wb_TabControl.HideTabs = True

        Rezept = New wb_Rezept(_RzNummer, Nothing, _RzVariante)
        Me.Text = Rezept.RezeptNummer & "/V" & Rezept.Variante & " " & Rezept.RezeptBezeichnung

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

        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbVariante.Fill(wb_Rezept_Shared.RzVariante)
        'Combo-Box(Rezept-Varianten) mit Werten füllen
        'cbLiniengruppe.Fill(wb_Rezept_Shared.LinienGruppe)
        cbLiniengruppe.Fill(wb_Linien_Global.LinienGruppen)
        'Eintrag in Combo-Box Liniengruppe ausfüllen
        cbLiniengruppe.SetTextFromKey(Rezept.LinienGruppe)
        'Eintrag in Combo-Box Rezeptvariante ausfüllen
        cbVariante.SetTextFromKey(Rezept.Variante)
        tbRzVariante.Text = Rezept.Variante

        'Virtual Tree anzeigen
        VirtualTree.DataSource = Rezept.RootRezeptSchritt
        'alle Zeilen aufklappen
        VirtualTree.RootRow.ExpandChildren(True)
        'falls keine Rezeptschritte vorhanden sind muss das Popup-Menu ausserhalb erstellt werden
        VT_MakeTreePopup()

        'Gesamt-Rohstoffpreis der Rezeptur (aktuell berechnet)
        tbRzPreis.Text = wb_Functions.FormatStr(Rezept.RezeptPreis, 2)
        'Rezeptgewicht (aktuell berechnet)
        tbRzGewicht.Text = wb_Functions.FormatStr(Rezept.RezeptGewicht, 3)
        'Mehlgesamt-Menge
        tbRzMehlmenge.Text = wb_Functions.FormatStr(Rezept.RezeptGesamtMehlmenge, 2)
        'Rezept TA
        tbRzTA.Text = CInt(Rezept.RezeptTA)

        'Cursor wieder zurücksetzen
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BtnDrucken_Click(sender As Object, e As EventArgs) Handles BtnDrucken.Click
        wb_Printer_Shared.LL.DataSource = New ObjectDataProvider(Rezept)
        wb_Printer_Shared.LL.Design()
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
        'Array aufbauen über alle Nähwerte - Grid aus KomponParam301_global, Werte aus Rezept.ktTyp301.Wert(_RootRezeptschritt)
        For i = 1 To wb_Global.maxTyp301
            NwtTabelle(i).Visible = wb_KomponParam301_Global.kt301Param(i).Used
            NwtTabelle(i).Nr = i
            NwtTabelle(i).Text = wb_KomponParam301_Global.kt301Param(i).Bezeichnung
            NwtTabelle(i).Wert = Rezept.KtTyp301.Wert(i)
            NwtTabelle(i).Einheit = wb_KomponParam301_Global.kt301Param(i).Einheit
            NwtTabelle(i).Header = wb_Functions.kt301GruppeToString(wb_KomponParam301_Global.kt301Param(i).Gruppe)
            NwtTabelle(i).FehlerText = Rezept.KtTyp301.FehlerKompName(i)
            Debug.Print("FEHLER :" & Rezept.KtTyp301.FehlerKompName(i))

            If NwtTabelle(i).Visible Then
                Debug.Print(NwtTabelle(i).Header & " " & NwtTabelle(i).Text & " " & NwtTabelle(i).Wert & " " & NwtTabelle(i).Einheit)
            End If
        Next

        'Daten im Grid anzeigen
        Dim nwtGrid As New wb_KomponParam301_GridView(NwtTabelle)
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
    ''' Fenster schliessen. Falls notwendig Daten sichern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Rezept_Rezeptur_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Flag Rezept-Hinweise sind geändert worden
        If _RzHinweiseChanged Then
            'Rezept-Verarbeitungs-Hinweise speichern
            RezeptHinweise.Memo = TextHinweise.Text
            RezeptHinweise.Write()
        End If

        'Rezeptur ist geändert worden
        'TODO Hier wird der Aufruf eingebaut Reeptur speichern !! wenn geändert wurde
        Rezept.MySQLdbWrite_RzSchritt(_RzNummer, _RzVariante)
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

    Private Sub VirtualTree_CellDoubleClick(sender As Object, e As EventArgs) Handles VirtualTree.CellDoubleClick
        'Doppel-Click auf VirtualTree-Cell
        Dim sCellWidget As CellWidget = sender
        'interne Rezeptnummer zum Rezeptschritt ermitteln
        Dim RezeptNr As Integer = DirectCast(sCellWidget.Tree.SelectedItem, wb_Rezeptschritt).RezeptNr
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
    End Sub

    ''' <summary>
    ''' Verhindert, dass einzelne Zellen markiert werden 
    ''' (Infralution Support): handle the SelectionChanging event and set Cancel to true. This prevents any selection occurring
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_SelectionChanging(sender As Object, e As SelectionChangingEventArgs) Handles VirtualTree.SelectionChanging
        e.Cancel = True
    End Sub

    Private Sub CellEditor2_InitializeControl(sender As Object, e As CellEditorInitializeEventArgs) Handles CellEditor2.InitializeControl

    End Sub

    ''' <summary>
    ''' Rechte-Maus-Click auf eine Zeile im VirtualTree.
    ''' 
    ''' Über e.Row.Item wird die entsprechende Zeile(wb_Rezeptschritt) im Grid ermittelt.
    ''' Abhängig vom Komponenten-Typ werden verschiedene Aktionen erlaubt/ausgelöst. (Popup-Menu)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_GetContextMenuStrip(sender As Object, e As GetContextMenuStripEventArgs) Handles VirtualTree.GetContextMenuStrip
        'aktuell ausgewählten Rezeptschritt merken (Popup)
        _RezeptSchritt = DirectCast(e.Row.Item, wb_Rezeptschritt)
        'Popup-Menu wird dynamisch erzeugt
        VT_MakeTreePopup()
    End Sub

    Private Sub VTP_NeuProduktionsStufe(sender As Object, e As EventArgs)

        Dim rs As wb_Rezeptschritt
        Debug.Print(_RezeptSchritt.Bezeichnung)
        'o.Delete()
        'VirtualTree.Invalidate()
        'VirtualTree.DataSource = Rezept.RootRezeptSchritt

        Dim RohstoffAuswahl As New wb_Rohstoff_AuswahlListe
        If RohstoffAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim RohNr As Integer = RohstoffAuswahl.RohstoffNr
            Dim RohNummer As String = RohstoffAuswahl.RohstoffNummer
            Dim RohName As String = RohstoffAuswahl.RohstoffName

            rs = New wb_Rezeptschritt(Nothing, RohName)
            rs.RohNr = RohNr
            rs.ParamNr = 1
            rs.Sollwert = "0,000"
            rs.Bezeichnung = RohName
            rs.Nummer = RohNummer

            _RezeptSchritt.Insert(rs, True)
            VirtualTree.Invalidate()
            VirtualTree.DataSource = Rezept.RootRezeptSchritt
        End If
    End Sub

    ''' <summary>
    ''' Neue Komponente(Auswahl) in leeres Rezept ein- oder an eine Rezeptur anfügen (Click in das leere Feld unterhalb VirtualTree)
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueKomponente(Sender As Object, e As EventArgs)
        'Auswahlliste Rohstoff
        If VT_AuswahlRohstoff() Then
            _RezeptSchritt = New wb_Rezeptschritt(Rezept.RootRezeptSchritt, _RezeptSchrittNeu.Bezeichnung)
            _RezeptSchritt.CopyFrom(_RezeptSchrittNeu)
            _RezeptSchritt.SchrittNr = Rezept.RootRezeptSchritt.ChildSteps.Count
            VT_Aktualisieren()
        End If
    End Sub


    ''' <summary>
    ''' Neue Komponente(Auswahl) vor der aktuellen Zeile einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueKomponenteDavor(Sender As Object, e As EventArgs)
        'Auswahlliste Rohstoff
        If VT_AuswahlRohstoff() Then
            _RezeptSchritt.Insert(_RezeptSchrittNeu, False)
            VT_Aktualisieren()
        End If
    End Sub


    ''' <summary>
    ''' Neue Komponente(Auswahl) nach der aktuellen Zeile einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueKomponenteDanach(Sender As Object, e As EventArgs)
        'Auswahlliste Rohstoff
        If VT_AuswahlRohstoff() Then
            _RezeptSchritt.Insert(_RezeptSchrittNeu, False)
            VT_Aktualisieren()
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

    Private Sub VTP_Delete(Sender As Object, e As EventArgs)
        _RezeptSchritt.Delete()
        VT_Aktualisieren()
    End Sub
    ''' <summary>
    ''' Anzeige der Rohstoff-Liste. 
    ''' Auswahl eines Rohstoffes für die Funktionen Einfügen, Anfügen, ..
    ''' </summary>
    ''' <returns></returns>
    Private Function VT_AuswahlRohstoff() As Boolean
        'Rohstoff-Auswahl-Liste
        Dim RohstoffAuswahl As New wb_Rohstoff_AuswahlListe
        If RohstoffAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, RohstoffAuswahl.RohstoffName)
            _RezeptSchrittNeu.RohNr = RohstoffAuswahl.RohstoffNr
            _RezeptSchrittNeu.ParamNr = 1
            _RezeptSchrittNeu.Sollwert = "0,000"
            _RezeptSchrittNeu.Nummer = RohstoffAuswahl.RohstoffNummer
            _RezeptSchrittNeu.Type = RohstoffAuswahl.RohstoffType
            _RezeptSchrittNeu.Einheit = RohstoffAuswahl.RohstoffEinheit
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
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_Loeschen) = True

                Case wb_Global.KomponTypen.KO_TYPE_KESSEL
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Davor) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Danach) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_Loeschen) = True

                Case wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT

                Case wb_Global.KomponTypen.KO_TYPE_KNETER

                Case Else
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Davor) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Danach) = True
                    '_PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Darunter) = True
                    '_PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Davor) = True

            End Select

            '_PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Darunter) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Danach) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_NeueKomponente_Darunter) = True

            '_PopupFunctions(wb_Global.TPopupFunctions.TP_Editieren) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_Loeschen) = True
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
        'Neue Produktions-Stufe
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe) Then
            VTPopUpMenu.Items.Add("Neue Produktions-Stufe", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'Neue Produktions-Stufe danach
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe_Davor) Then
            VTPopUpMenu.Items.Add("Neue Produktions-Stufe davor", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'Neue Produktions-Stufe davor
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe_Danach) Then
            VTPopUpMenu.Items.Add("Neue Produktions-Stufe danach", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If

        'Neuer Kessel nach der Produktions-Stufe
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Darunter) Then
            VTPopUpMenu.Items.Add("Neuer Kessel", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'Neuer Kessel davor
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Davor) Then
            VTPopUpMenu.Items.Add("Neuer Kessel davor", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'Neuer Kessel danach
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Danach) Then
            VTPopUpMenu.Items.Add("Neuer Kessel danach", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If

        'Neue Textkomponente
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente) Then
            VTPopUpMenu.Items.Add("Neue Textkomponente", Nothing, AddressOf VTP_NeueTextKomponente)
        End If
        'Neue Textkomponente am Ende anfügen
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Darunter) Then
            VTPopUpMenu.Items.Add("Neue Textkomponente anfügen", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'Neue Textkomponente davor
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Davor) Then
            VTPopUpMenu.Items.Add("Neue Textkomponente davor", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If
        'Neue Textkomponente danach
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Danach) Then
            VTPopUpMenu.Items.Add("Neue Textkomponente danach", Nothing, AddressOf VTP_NeueProduktionsStufe)
        End If

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

        'Bearbeiten
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_Editieren) Then
            VTPopUpMenu.Items.Add("Bearbeiten", Nothing, AddressOf VTP_NeueProduktionsStufe)
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
End Class