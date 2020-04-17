Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking
Imports System.Windows.Forms

Public Class wb_Rohstoffe_Cloud
    Inherits DockContent

    Dim sColNames As New List(Of String)
    Dim nwt As New wb_nwtCl_WinBack(wb_Credentials.WinBackCloud_Pass, wb_Credentials.WinBackCloud_Url)
    Dim dl As New wb_nwtCl_DatenLink(wb_Credentials.Datenlink_PAT, wb_Credentials.Datenlink_CAT, wb_Credentials.Datenlink_Url)
    Dim cnt As Integer = wb_Global.UNDEFINED
    Dim cntDokumente As Integer = wb_Global.UNDEFINED

    Dim WithEvents CloudResultGrid As wb_ArrayGridViewNwt
    Dim WithEvents nwtGrid As wb_ArrayGridViewKomponParam301

    Private Sub wb_Rohstoffe_Cloud_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo

        'Beim ersten Aufruf wird der aktuelle Rohstoff angezeigt
        If RohStoff IsNot Nothing Then
            DetailInfo()
        End If
    End Sub

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'Tabs ausblenden
        Wb_TabControl.HideTabs = True
        'Start auf Seite(1)-Suchen
        ChangeTab(tpCloudSuchen)
    End Sub

    ''' <summary>
    ''' Zeigt Rohstoffnummer und Rohstoffname an. 
    ''' Wenn schon eine Cloud-ID vorhanden ist, wird auf die Tab-Page Löschen/Aktualisieren umgeschaltet
    ''' Ist eine Rezeptur mit dem Rohstoff verknüpft, werden die Chargen-Größen und Rezeptnummer und Bezeichnung angezeigt.
    ''' </summary>
    Private Sub DetailInfo()
        'Rohstoff-Nummer
        tRohstoffNummer.Text = wb_Rohstoffe_Shared.RohStoff.Nummer
        'Rohstoff-Bezeichnung
        tRohstoffName.Text = wb_Rohstoffe_Shared.RohStoff.Bezeichnung
        'ID in der Cloud
        tCloudID.Text = wb_Rohstoffe_Shared.RohStoff.MatchCode
        'kann vorproduziert werden
        cbFreigabeProduktion.Checked = wb_Rohstoffe_Shared.RohStoff.FreigabeProduktion

        'Anzahl der gefundenen Datensätze
        cnt = wb_Global.UNDEFINED
        'Anzahl der Dokumente zum Rohstoff
        cntDokumente = wb_Global.UNDEFINED

        'wenn schon eine Verknüpfung vorhanden ist wird Tab-Page Verknüpfung löschen angezeigt
        If tCloudID.Text <> "" Then
            'Button 'Zurück' hat keine Funktion
            Btn_Result_Back.Enabled = False
            'Verknüpfung zur Cloud löschen/aktualisieren
            ChangeTab(tpCloudResult)

        ElseIf wb_Rohstoffe_Shared.RohStoff.RzNr > 0 Then
            'Rohstoff ist mit einer Rezeptur verknüpft
            ChangeTab(tpRezept, wb_Rohstoffe_Shared.RohStoff.Nr)

        ElseIf wb_Functions.TypeHatNwt(RohStoff.Type) Then
            'Suchen Rohstoff in der Cloud
            ChangeTab(tpCloudSuchen)
        Else
            'Kein Rohstoff der Nährwerte hat
            ChangeTab(tpKompType)
        End If
    End Sub

    ''' <summary>
    ''' Anzeige der Tab-Page wechseln. Vor dem Einblenden der Seite werden die Datenfelder aktualisiert.
    ''' Enthält die Ablauf-Logik (Regeln zum Wechsel der Pages)
    ''' </summary>
    ''' <param name="NewTab"></param>
    Private Sub ChangeTab(NewTab As TabPage, Optional Parameter As Object = Nothing)
        Select Case NewTab.Name

            Case tpCloudSuchen.Name
                'Fenster-Titel
                Me.Text = My.Resources.tpCloudTitelCloud
                lblCloudID.Visible = False
                tCloudID.Visible = False

                'HilfeText
                lblHilfeText.Text = My.Resources.tpCloudSuchenTxt
                lblHilfeText.ForeColor = System.Drawing.Color.Black
                'Suchtext
                tSuchtextBezeichnung.Text = tRohstoffName.Text
                'Schaltfläche Mail unsichtbar
                'btnMail.Visible = False
                'Suche Rohstoff-Bezeichnung in Cloud (WinBack/Datenlink)
                Wb_TabControl.SelectedTab = tpCloudSuchen

            Case tpCloudGefunden.Name
                'Fenster-Titel
                Me.Text = My.Resources.tpCloudTitelCloud
                lblCloudID.Visible = False
                tCloudID.Visible = False

                'Ausgabe Ergebnis
                If Parameter IsNot Nothing Then
                    cnt = DirectCast(Parameter, wb_nwtCL).cnt
                End If
                Select Case cnt
                    Case 0
                        'Keinen Rohstoff gefunden
                        lblHilfeText.Text = My.Resources.tpCloudSuchenMail
                        lblHilfeText.ForeColor = System.Drawing.Color.Red
                        'Schaltfläche Mail sichtbar
                        btnMail.Visible = True

                    Case 1
                        'genau einen Rohstoff gefunden
                        lblErgebnisText.Text = My.Resources.tpCloudEinenRohstoffGefunden
                        'Anzeige Liste aller gefundenen Rohstoffe
                        If Parameter IsNot Nothing Then
                            ShowResultGrid(Parameter)
                        End If
                        Wb_TabControl.SelectedTab = tpCloudGefunden

                    Case < 0
                        'Fehler bei Abfrage der Rohstoffe
                        lblHilfeText.Text = "Fehler bei der Abfrage aus der Cloud"
                        lblHilfeText.ForeColor = System.Drawing.Color.Red
                        Wb_TabControl.SelectedTab = tpCloudSuchen

                    Case Else
                        'mehrere Rohstoffe gefunden
                        lblErgebnisText.Text = wb_Functions.SetParams(My.Resources.tpCloudRohstoffeGefunden, cnt)
                        'Anzeige Liste aller gefundenen Rohstoffe
                        If Parameter IsNot Nothing Then
                            ShowResultGrid(Parameter)
                        End If
                        Wb_TabControl.SelectedTab = tpCloudGefunden
                End Select

            Case tpCloudAnzeige.Name
                'Fenster-Titel
                Me.Text = My.Resources.tpCloudTitelCloud
                lblCloudID.Visible = True
                tCloudID.Visible = True

                'Nährwertdaten in Objekt schreiben und anzeigen
                If Parameter IsNot Nothing Then
                    ShowNwtGrid(DirectCast(Parameter, String))
                End If
                'Anzeige Nährwerte des ausgewählten Rohstoffes
                Wb_TabControl.SelectedTab = tpCloudAnzeige

            Case tpCloudResult.Name
                'Fenster-Titel
                Me.Text = My.Resources.tpCloudTitelCloud
                lblCloudID.Visible = True
                tCloudID.Visible = True

                'Anzeige Zutatenliste und Bezeichnung
                tbDeklarationIntern.Text = RohStoff.DeklBezeichungIntern
                tbDeklarationExtern.Text = RohStoff.DeklBezeichungExtern

                'Prüfen ob Dokumente zum Rohstoff in der Cloud hinterlegt sind
                cntDokumente = nwt.GetProductSheetCount(RohStoff.MatchCode)
                If cntDokumente > 0 Then
                    BtnProduktDatenblatt.Visible = True
                    If cntDokumente > 1 Then
                        BtnProduktDatenblatt.Text = cntDokumente.ToString & "Produkt-Datenblätter"
                    Else
                        BtnProduktDatenblatt.Text = " 1 Produkt-Datenblatt"
                    End If
                Else
                        BtnProduktDatenblatt.Visible = False
                End If
                'Löschen und Aktualisieren aus der Cloud
                Wb_TabControl.SelectedTab = tpCloudResult

            Case tpRezept.Name
                'Fenster-Titel
                Me.Text = My.Resources.tpCloudTitelRezept
                lblCloudID.Visible = False
                tCloudID.Visible = False
                'Rezeptnummer ist definiert
                If Parameter <> wb_Global.UNDEFINED Then
                    'Daten aus der Komponenten-Klasse lesen
                    KompRzChargen.GetDataFromKomp(RohStoff)
                    'Anzeigen der Werte
                    KompRzChargen.DataValid = True
                End If
                'Rohstoff ist mit Rezeptur verknüpft
                Wb_TabControl.SelectedTab = tpRezept

            Case Else
                'Fenster-Titel
                Me.Text = My.Resources.tpCloudTitelCloud
                lblCloudID.Visible = False
                tCloudID.Visible = False

                'Rohstoff-Type hat keine Nährwerte
                lblKeineNwt.Text = My.Resources.tpCloudKompType
                Wb_TabControl.SelectedTab = tpKompType
        End Select
    End Sub

    ''' <summary>
    ''' Zeigt die Liste aller gefundenen Rohstoffe aus der Cloud an.
    ''' Datenlink liefert die Bezeichnung und Lieferanten, aus der WinBack-Cloud werden
    ''' Bezeichnung, Lieferant und Deklarationsbezeichnung angezeigt.
    ''' </summary>
    ''' <param name="Cloud"></param>
    Private Sub ShowResultGrid(Cloud As wb_nwtCL)
        'Tabelle-Überschriften
        sColNames.Clear()
        If Cloud.CloudType = wb_nwtCL.wb_CloudType.DatenLink Then
            sColNames.AddRange({"", "Bezeichnung", "&Lieferant", ""})
        Else
            sColNames.AddRange({"", "Bezeichnung", "Lieferant", "&Deklarationsbezeichnung"})
        End If

        'Daten im Grid anzeigen
        If CloudResultGrid IsNot Nothing Then
            CloudResultGrid.Dispose()
        End If
        CloudResultGrid = New wb_ArrayGridViewNwt(Cloud.getProductList, sColNames)
        CloudResultGrid.ScrollBars = ScrollBars.Vertical
        CloudResultGrid.BackgroundColor = Me.BackColor
        CloudResultGrid.GridLocation(pnlNwtGrid)
        CloudResultGrid.PerformLayout()
        CloudResultGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    ''' <summary>
    ''' Zeigt die Tabelle aller Allergene und Nährwerte aus der Cloud für den
    ''' ausgewählten Rohstoff an. Das Grid zeigt nur Allergene und Nährwerte an,
    ''' die als verwendet gekennzeichnet werden.
    ''' </summary>
    ''' <param name="rid"></param>
    Private Sub ShowNwtGrid(rid As String)
        'Nährwertdaten in Komponenten-Objekt schreiben
        If wb_Functions.IsDatenLinkID(rid) Then
            'Datenlink
            dl.GetProductData(rid, wb_Rohstoffe_Shared.RohStoff)
        Else
            'WinBack-Cloud
            nwt.GetProductData(rid, wb_Rohstoffe_Shared.RohStoff)
        End If

        'ID aus der Cloud
        tCloudID.Text = rid
        wb_Rohstoffe_Shared.RohStoff.MatchCode = rid

        'Daten im Grid anzeigen
        If nwtGrid IsNot Nothing Then
            nwtGrid.Dispose()
        End If
        nwtGrid = New wb_ArrayGridViewKomponParam301(RohStoff.ktTyp301.NwtTabelle)
        nwtGrid.BackgroundColor = Me.BackColor
        nwtGrid.GridLocation(pnlNwt)
        nwtGrid.PerformLayout()
    End Sub

    ''' <summary>
    ''' Suchen Rohstoff/Lieferant in der WinBack-Cloud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCloud_Click(sender As Object, e As EventArgs) Handles btnCloud.Click
        'Suche nach Rohstoff oder Rohstoff/Lieferant
        If tSuchtextLieferant.Text = "" Then
            nwt.lookupProductName(tSuchtextBezeichnung.Text)
        Else
            nwt.lookupProduct(tSuchtextBezeichnung.Text, tSuchtextLieferant.Text)
        End If

        'Ergebnis der Cloud-Suche anzeigen
        ChangeTab(tpCloudGefunden, nwt)
    End Sub

    ''' <summary>
    ''' Suchen Rohstoff/Lieferant bei Datenlink
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnDatenLink_Click(sender As Object, e As EventArgs) Handles btnDatenLink.Click
        'Suche nach Rohstoff
        dl.lookupProductName(tSuchtextBezeichnung.Text)

        'Ergebnis der Cloud-Suche anzeigen
        ChangeTab(tpCloudGefunden, dl)
    End Sub

    Private Sub CloudResultGrid_DoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles CloudResultGrid.CellMouseDoubleClick
        'ID in der Cloud aus Spalte(0) im Grid
        Dim rid As String = DirectCast(sender, wb_ArrayGridViewNwt).Rows(e.RowIndex).Cells(0).Value

        'Anzeige der Nährwert-Informationen
        ChangeTab(tpCloudAnzeige, rid)
    End Sub

    ''' <summary>
    ''' Verbindung zur Cloud löschen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
        'Verknüpfung zur Cloud löschen (MatchCode) - die Nährwert-Informationen bleiben erhalten
        tCloudID.Text = ""
        wb_Rohstoffe_Shared.RohStoff.MatchCode = wb_Global.UNDEFINED
        'Änderung in Datenbank schreiben
        wb_Rohstoffe_Shared.Edit_Leave(sender)

        'Meldung ausgeben
        MsgBox("Die Verknüpfung zur Cloud wurde gelöscht" & vbCrLf & "Die Nährwerte und Allergen-Informationen bleiben erhalten", MsgBoxStyle.OkOnly, "WinBack-Cloud")

        'Tab umschalten Suchen Rohstoff in der Cloud
        ChangeTab(tpCloudSuchen)
    End Sub

    ''' <summary>
    ''' Anforderungsmail an nwt.winback.de verschicken.
    ''' Rohstoff-Bezeichnung und Lieferant werden automatisch in den Mailtext eingefügt.
    ''' 
    ''' Der Mail-Versandt erfolgt über das Windows-Standard-Mail-Programm.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnMail_Click(sender As Object, e As EventArgs) Handles btnMail.Click
        Dim Mail As New wb_Mail
        If Not Mail.StartMail_CloudAnforderung(tRohstoffName.Text, tSuchtextLieferant.Text) Then
            MsgBox("Fehler beim Aufruf des Mail-Programmes. Bitte kontaktieren Sie Ihren Administrator ", MsgBoxStyle.Critical, "Fehler beim E-Mail-Versand")
        Else
            lblHilfeText.Text = ""
        End If
    End Sub

    Private Sub BtnRezept_Click(sender As Object, e As EventArgs) Handles BtnRezept.Click
        'Anzeige Verknüpfung Rezeptur
        ChangeTab(tpRezept)
        'Fenster Rezeptliste öffnen
        KompRzChargen.BtnRzpt_Click(sender, e)
    End Sub

    ''' <summary>
    ''' Daten aus der Cloud (manuell) aktualisieren
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnResult_Akt_Click(sender As Object, e As EventArgs) Handles btnResult_Akt.Click
        'Buttons deaktivieren
        btnResult_Akt.Enabled = False
        btnDisconnect.Enabled = False

        Dim nwtUpdateKomponenten As New wb_nwtUpdate
        'Update der Daten aus der Cloud
        nwtUpdateKomponenten.GetNaehrwerte(wb_Rohstoffe_Shared.RohStoff.MatchCode, wb_Rohstoffe_Shared.RohStoff)
        'Updates in Datenbank(en) sichern
        nwtUpdateKomponenten.DbUpdateNaehrwerte(wb_Rohstoffe_Shared.RohStoff, (wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack))

        'Updates anzeigen - in allen aktiven Fenstern
        wb_Rohstoffe_Shared.Liste_Click(sender)

        'Rückmeldung - Daten wurden aktualisiert
        MsgBox("Die Nährwert-Informationen wurden aktualisiert", MsgBoxStyle.OkOnly, "WinBack-Cloud")
        'Buttons wieder aktivieren
        btnResult_Akt.Enabled = True
        btnDisconnect.Enabled = True
    End Sub

    ''' <summary>
    ''' Klick auf Button "Weiter". Abhängig vom aktuellen Tab wird die nächste Aktion eingeleitet
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_Weiter_Click(sender As Object, e As EventArgs) Handles btnFound_Vor.Click, btnShow_Vor.Click, btnResult_OK.Click
        Select Case Wb_TabControl.SelectedTab.Name

            'Anzeige der Such-Ergebnisse
            Case tpCloudGefunden.Name
                'ID in der Cloud aus Spalte(0) im Grid
                Dim rid As String = CloudResultGrid.SelectedRows(0).Cells(0).Value
                'Anzeige der Nährwert-Informationen
                ChangeTab(tpCloudAnzeige, rid)

            'Anzeige der Nährwerte und Allergene
            Case tpCloudAnzeige.Name
                'Button 'Zurück'und 'Speichern' einblenden
                Btn_Result_Back.Enabled = True
                btnResult_OK.Enabled = True
                'Anzeige Nährwerte/Allergen OK. Weiter mit Anzeige Deklarations-Bezeichnung
                ChangeTab(tpCloudResult)

            'Anzeige der Deklarations-Bezeichnung (intern/extern)
            Case tpCloudResult.Name
                'Button 'Speichern' deaktivieren
                btnResult_OK.Enabled = False
                'Verknüpfung zur Cloud speichern
                wb_Rohstoffe_Shared.Edit_Leave(sender)
                'Nährwerte/Allergene speichern
                wb_Rohstoffe_Shared.RohStoff.MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
                wb_Rohstoffe_Shared.RohStoff.MySqldbUpdate_Zutatenliste()
                If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
                    wb_Rohstoffe_Shared.RohStoff.MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
                    wb_Rohstoffe_Shared.RohStoff.MsSqldbUpdate_Zutatenliste()
                End If
                'Meldung ausgeben
                MsgBox("Nährwerte/Allergene und die Verknüpfung zur Cloud wurden gespeichert !", MsgBoxStyle.OkOnly, "Nährwerte und Allergene")
                'alle Fenster aktualisieren
                wb_Rohstoffe_Shared.Liste_Click(sender)
        End Select
    End Sub

    ''' <summary>
    ''' Klick auf Button "Zurück". ab hängig vom aktuellen Tab wird zurückgeschaltet auf die vorherige Aktion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_Zurueck_Click(sender As Object, e As EventArgs) Handles btnFound_Back.Click, btnShow_Back.Click, Btn_Result_Back.Click
        Select Case Wb_TabControl.SelectedTab.Name

            'Anzeige der Such-Ergebnisse
            Case tpCloudGefunden.Name
                'Zurück zur Eingabe der Suchkriterien
                ChangeTab(tpCloudSuchen)

            'Anzeige der Nährwerte und Allergene
            Case tpCloudAnzeige.Name
                'Ergebnis der Cloud-Suche anzeigen(Die Anzahl der Datensätze ist in cnt gespeichert)
                ChangeTab(tpCloudGefunden, Nothing)

            'Anzeige Deklarationsbezeichnung extern/intern
            Case tpCloudResult.Name
                'Anzeige der Nährwerte und Allergene
                ChangeTab(tpCloudAnzeige, Nothing)

        End Select
    End Sub

    ''' <summary>
    ''' Event Daten wurden geändert von wb_KompRZChargen wird weitergegeben an wb_Rohstoffe_Shared
    ''' 
    ''' Achtung: auch wenn bei Verweise '0' eingetragen ist, wird diese Routine aufgerufen !!
    ''' </summary>
    Public Sub KomRzChargen_DataInvalidated() Handles KompRzChargen.DataInvalidated
        KompRzChargen.SaveData(RohStoff)
        wb_Rohstoffe_Shared.RezChrg_Changed(Nothing)
    End Sub

    Private Sub BtnProduktDatenblatt_Click(sender As Object, e As EventArgs) Handles BtnProduktDatenblatt.Click
        Dim RohstoffDokumente As New wb_Rohstoff_Dokumente(nwt, RohStoff.MatchCode)
        RohstoffDokumente.ShowDialog()
    End Sub

    Private Sub cbFreigabeProduktion_Click(sender As Object, e As EventArgs) Handles cbFreigabeProduktion.Click
        wb_Rohstoffe_Shared.RohStoff.FreigabeProduktion = cbFreigabeProduktion.Checked
    End Sub
End Class