Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Details
    Inherits DockContent

    Private _DeklBezeichungExtern As String = Nothing

    Private Sub wb_Rohstoffe_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box(Rohstoff-Gruppe) mit Werten füllen
        cbRohstoffGrp1.Fill(RohGruppe)
        cbRohstoffGrp2.Fill(RohGruppe)

        'Feld Rohstoff-Preis ist in Variante OrgaBack readonly
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            tbRohstoffPreis.ReadOnly = False
            tbRohstoffPreis.BackColor = tRohstoffNummer.BackColor
        End If
        'Default Währung(€)
        ePreis.Text = wb_GlobalSettings.osDefaultWaehrung
        'interne Deklaration verwenden
        cbInterneDeklaration.Checked = wb_GlobalSettings.NwtInterneDeklaration

        'Rohstoff-ID und Rohstoff-Type sind nur für Admin-User sichtbar
        If (wb_GlobalSettings.AktUserGruppe = wb_Global.AdminUserGrpe) Or Debugger.IsAttached Then
            tID.Visible = True
            tType.Visible = True
            lblID.Visible = True
            lblType.Visible = True
            cbAktiv.Enabled = True
        Else
            tID.Visible = False
            tType.Visible = False
            lblID.Visible = False
            lblType.Visible = False
            cbAktiv.Enabled = False
        End If

        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo

        'Beim ersten Aufruf wird der aktuelle Rohstoff angezeigt. Sonst wird beim Öffnen des Detail-Info-Fensters
        'der Inhalt der Textfelder gelöscht !!
        If RohStoff IsNot Nothing Then
            DetailInfo(sender)
        End If
    End Sub

    Private Sub wb_Rohstoffe_Details_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Rohstoffe_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    ''' <summary>
    ''' Anzeige der Rohstoff-Details.
    ''' Wird aufgerufen durch Event eListe_Click(). Aktualisiert die Anzeigefelder (Nummer/Text/Kommentar...)
    ''' </summary>
    Private Sub DetailInfo(sender)
        'Textfelder
        tRohstoffNummer.Text = RohStoff.Nummer
        tRohstoffName.Text = RohStoff.Bezeichnung
        tRohstoffKommentar.Text = RohStoff.Kommentar
        cbAktiv.Checked = RohStoff.Aktiv

        'ID und Komponententype sind nur für Gruppe Admin sichtbar
        If (wb_GlobalSettings.AktUserGruppe = wb_Global.AdminUserGrpe) Or Debugger.IsAttached Then
            tID.Text = RohStoff.Nr
            tType.Text = wb_Functions.KomponTypeToInt(RohStoff.Type)
        End If

        If wb_Functions.TypeIstSollMenge(RohStoff.Type, 1) Then
            'Panel Detail-Daten sichtbar
            pnlDetails.Visible = True

            'Preis und Gebindegröße
            tbRohstoffPreis.Text = RohStoff.Preis
            tbGebindeGroesse.Text = RohStoff.GebindeGroesse

            'Lagerbestand und Mindestmenge
            tbBilanzmenge.Text = RohStoff.Bilanzmenge
            tbMindestMenge.Text = RohStoff.MindestMenge
            'wenn die Mindestmenge unterschritten ist, rot markieren
            If RohStoff.MindestmengeUnterschritten Then
                tbBilanzmenge.BackColor = Drawing.Color.Red
            Else
                tbBilanzmenge.BackColor = Drawing.Color.LightGray
            End If

            'Anzeige Zutatenliste und Bezeichnung
            ShowDeklaration()
            'Flag zählt nicht zum Rezeptgewicht
            cbRezeptGewicht.Checked = RohStoff.ZaehltNichtZumRezeptGewicht
            'Flag zählt zur Nährwertberechnung
            cbNwtBerechnung.Checked = RohStoff.ZaehltTrotzdemZumNwtGewicht
            cbNwtBerechnung.Enabled = cbRezeptGewicht.Checked
            'kann vorproduziert werden
            cbFreigabeProduktion.Checked = wb_Rohstoffe_Shared.RohStoff.FreigabeProduktion

            'Rezeptur verknüpft
            If RohStoff.RzNr > 0 Then
                'Flag Zutatenliste auflösen
                cbAufloesen.Enabled = True
                cbAufloesen.Checked = CheckZutatenAufloesen()
                'Rezept-Nummer
                lbRezNr.Enabled = True
                tbRezNr.Text = RohStoff.RezeptNummer
                'Rezept-Name
                lblRezept.Enabled = True
                tbRezName.Text = RohStoff.RezeptName
            Else
                'Flag Zutatenliste auflösen
                cbAufloesen.Enabled = False
                'Rezept-Nummer
                lbRezNr.Enabled = False
                tbRezNr.Text = ""
                'Rezept-Name
                lblRezept.Enabled = False
                tbRezName.Text = ""
            End If

            'Auswahlfelder Rohstoff-Gruppen
            cbRohstoffGrp1.SetTextFromKey(RohStoff.Gruppe1)
            cbRohstoffGrp2.SetTextFromKey(RohStoff.Gruppe2)
        Else
            'Panel Detail-Daten ausblenden
            pnlDetails.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Anzeige der Deklarations-Felder. Wenn der Haken bei "Rohstoff wird nicht deklariert" gesetzt ist,
    ''' werden die Felder ausgeblendet
    ''' </summary>
    Private Sub ShowDeklaration()
        If RohStoff.DeklBezeichungExtern = wb_Global.FlagKeineDeklaration Then
            lblDeklIntern.Visible = False
            tbDeklarationIntern.Visible = False
            lblDeklExtern.Visible = False
            tbDeklarationExtern.Visible = False
            cbKeineDeklaration.Checked = True
            cbAufloesen.Visible = False
            cbInterneDeklaration.Visible = False
        Else
            lblDeklIntern.Visible = True
            tbDeklarationIntern.Visible = True
            lblDeklExtern.Visible = True
            tbDeklarationExtern.Visible = True
            cbKeineDeklaration.Checked = False
            cbAufloesen.Visible = True
            cbInterneDeklaration.Visible = True
        End If
        tbDeklarationIntern.Text = RohStoff.DeklBezeichungIntern
        tbDeklarationExtern.Text = RohStoff.DeklBezeichungExtern
    End Sub

    ''' <summary>
    ''' Die Daten in den Eingabe-Feldern (Nummer/Text/Kommentar) haben sich geändert)
    ''' Wird aufgerufen durch [Textfeld].Leave(). Aktualisiert die Datenfelder in wb_Rohstoff_Global und löst
    ''' dann den Event Edit_Leave() aus.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tRohstoffName.Leave, tRohstoffNummer.Leave, tRohstoffKommentar.Leave, tbRohstoffPreis.Leave, tbGebindeGroesse.Leave, cbRohstoffGrp2.Leave, cbRohstoffGrp1.Leave, cbRezeptGewicht.Click, cbAktiv.Click, cbNwtBerechnung.Click
        'Wenn die Bearbeitung/Speichern freigegeben ist
        If RohStoff.Type <> wb_Global.KomponTypen.KO_TYPE_UNDEFINED Then
            'Bezeichnungstexte
            RohStoff.Bezeichnung = tRohstoffName.Text
            RohStoff.Kommentar = tRohstoffKommentar.Text
            RohStoff.Nummer = tRohstoffNummer.Text
            RohStoff.GebindeGroesse = tbGebindeGroesse.Text

            'Rohstoff-Gruppe
            RohStoff.Gruppe1 = cbRohstoffGrp1.GetKeyFromSelection
            RohStoff.Gruppe2 = cbRohstoffGrp2.GetKeyFromSelection
            'Rohstoff zählt nicht zum Rezeptgewicht
            RohStoff.ZaehltNichtZumRezeptGewicht = cbRezeptGewicht.Checked
            RohStoff.ZaehltTrotzdemZumNwtGewicht = cbNwtBerechnung.Checked
            cbNwtBerechnung.Enabled = cbRezeptGewicht.Checked
            'Rohstoff ist aktiv
            RohStoff.Aktiv = cbAktiv.Checked

            'RohStoff-Preis (nur Prog-Version WinBack)
            If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
                RohStoff.Preis = tbRohstoffPreis.Text
            End If

            'Daten wurden geändert - Datensatz speichern
            Edit_Leave(sender)
        End If
    End Sub

    ''' <summary>
    ''' Das Eingabefeld "Mindestmenge" wurde verlassen. Der Inhalt wird in die
    ''' Komponenten-Daten eingetragen und in der Datenbank gesichert. (SET)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbMindestMenge_Leave(sender As Object, e As EventArgs) Handles tbMindestMenge.Leave
        wb_Rohstoffe_Shared.RohStoff.MindestMenge = tbMindestMenge.Text
    End Sub

    ''' <summary>
    ''' Das Eingabefeld "externe Deklaration" wurde verlassen. Der Inhalt wird in die
    ''' Komponenten-Daten eingetragen und in der Datenbank gesichert. (SET)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbDeklarationExtern_Leave(sender As Object, e As EventArgs) Handles tbDeklarationExtern.Leave
        wb_Rohstoffe_Shared.RohStoff.DeklBezeichungExtern = tbDeklarationExtern.Text
        cbAufloesen.Checked = CheckZutatenAufloesen()
    End Sub

    ''' <summary>
    ''' Das Eingabefeld "interne Deklaration" wurde verlassen. Der Inhalt wird in die
    ''' Komponenten-Daten eingetragen und in der Datenbank gesichert. (SET)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbDeklarationIntern_Leave(sender As Object, e As EventArgs) Handles tbDeklarationIntern.Leave
        wb_Rohstoffe_Shared.RohStoff.DeklBezeichungIntern = tbDeklarationIntern.Text
        cbAufloesen.Checked = CheckZutatenAufloesen()
    End Sub

    Private Sub cbKeineDeklaration_Click(sender As Object, e As EventArgs) Handles cbKeineDeklaration.Click
        If cbKeineDeklaration.Checked Then
            'alten DeklarationsText merken(sicherheitshalber)
            If tbDeklarationExtern.Text <> wb_Global.FlagKeineDeklaration Then
                _DeklBezeichungExtern = tbDeklarationExtern.Text
            End If
            wb_Rohstoffe_Shared.RohStoff.DeklBezeichungExtern = wb_Global.FlagKeineDeklaration
            'Felder ausblenden
            ShowDeklaration()
        Else
            If _DeklBezeichungExtern IsNot Nothing Then
                'alten Wert wieder eintragen
                wb_Rohstoffe_Shared.RohStoff.DeklBezeichungExtern = _DeklBezeichungExtern
            Else
                wb_Rohstoffe_Shared.RohStoff.DeklBezeichungExtern = ""
            End If
            'Felder wieder einblenden
            ShowDeklaration()
        End If
    End Sub

    ''' <summary>
    ''' Globale Einstellung Zutatenliste - interne Deklaration verwenden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbInterneDeklaration_Click(sender As Object, e As EventArgs) Handles cbInterneDeklaration.Click
        wb_GlobalSettings.NwtInterneDeklaration = cbInterneDeklaration.Checked
        cbAufloesen.Checked = CheckZutatenAufloesen()
    End Sub

    ''' <summary>
    ''' Prüft ob das Flag Zutaten-Auflösen gesetzt ist oder nicht
    ''' </summary>
    Private Function CheckZutatenAufloesen() As Boolean
        If cbInterneDeklaration.Checked Then
            If tbDeklarationIntern.Text.StartsWith(">") Then
                Return True
            End If
        Else
            If tbDeklarationExtern.Text.StartsWith(">") Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Sub cbAufloesen_Click(sender As Object, e As EventArgs) Handles cbAufloesen.Click
        If cbAufloesen.Checked Then
            wb_Rohstoffe_Shared.RohStoff.Deklaration = ">" & wb_Rohstoffe_Shared.RohStoff.Deklaration
        Else
            wb_Rohstoffe_Shared.RohStoff.Deklaration = wb_Rohstoffe_Shared.RohStoff.Deklaration.TrimStart(">")
        End If
        ShowDeklaration()
    End Sub

    Private Sub cbFreigabeProduktion_Click(sender As Object, e As EventArgs) Handles cbFreigabeProduktion.Click
        wb_Rohstoffe_Shared.RohStoff.FreigabeProduktion = cbFreigabeProduktion.Checked
        'Änderung in WinBack-DB speichern
        wb_Rohstoffe_Shared.RohStoff.MySQLdbUpdate()
    End Sub
End Class