Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking
Imports System.Windows.Forms

Public Class wb_Rohstoffe_Cloud
    Inherits DockContent

    Dim sColNames As New List(Of String)
    Dim nwt As New wb_nwtCl_WinBack(wb_Credentials.WinBackCloud_Pass, wb_Credentials.WinBackCloud_Url)
    Dim dl As New wb_nwtCl_DatenLink(wb_Credentials.Datenlink_PAT, wb_Credentials.Datenlink_CAT, wb_Credentials.Datenlink_Url)

    Dim WithEvents CloudResultGrid As wb_ArrayGridViewNwt
    Dim WithEvents nwtGrid As wb_ArrayGridViewKomponParam301

    Private Sub wb_Rohstoffe_Cloud_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo
        'Daten vom aktuellen Rohstoff anzeigen
        If RohStoff.Nr > 0 Then
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

        'wenn schon eine Verknüpfung vorhanden ist wird Tab-Page Verknüpfung löschen angezeigt
        If tCloudID.Text <> "" Then
            'Verknüpfung zur Cloud löschen/aktualisieren
            ChangeTab(tpCloudResult)

        ElseIf wb_Rohstoffe_Shared.RohStoff.RzNr > 0 Then
            'Rohstoff ist mit einer Rezeptur verknüpft
            ChangeTab(tpRezept)

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
                btnMail.Visible = False
                'Suche Rohstoff-Bezeichnung in Cloud (WinBack/Datenlink)
                Wb_TabControl.SelectedTab = tpCloudSuchen

            Case tpCloudGefunden.Name
                'Fenster-Titel
                Me.Text = My.Resources.tpCloudTitelCloud
                lblCloudID.Visible = False
                tCloudID.Visible = False

                'Ausgabe Ergebnis
                Dim cnt As Integer = DirectCast(Parameter, wb_nwtCL).cnt
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
                        ShowResultGrid(Parameter)
                        Wb_TabControl.SelectedTab = tpCloudGefunden

                    Case Else
                        'mehrere Rohstoffe gefunden
                        lblErgebnisText.Text = wb_Functions.SetParams(My.Resources.tpCloudRohstoffeGefunden, cnt)
                        'Anzeige Liste aller gefundenen Rohstoffe
                        ShowResultGrid(Parameter)
                        Wb_TabControl.SelectedTab = tpCloudGefunden
                End Select

            Case tpCloudAnzeige.Name
                'Fenster-Titel
                Me.Text = My.Resources.tpCloudTitelCloud
                lblCloudID.Visible = True
                tCloudID.Visible = True

                'Nährwertdaten in Objekt schreiben und anzeigen
                ShowNwtGrid(DirectCast(Parameter, String))
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
                'Löschen und Aktualisieren aus der Cloud
                Wb_TabControl.SelectedTab = tpCloudResult

            Case tpRezept.Name
                'Fenster-Titel
                Me.Text = My.Resources.tpCloudTitelRezept
                lblCloudID.Visible = False
                tCloudID.Visible = False
                'Daten aus der Komponenten-Klasse lesen
                KompRzChargen.GetDataFromKomp(RohStoff)
                'Anzeigen der Werte
                KompRzChargen.DataValid = True
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
        CloudResultGrid = New wb_ArrayGridViewNwt(Cloud.getProducList, sColNames)
        CloudResultGrid.ScrollBars = ScrollBars.Vertical
        CloudResultGrid.BackgroundColor = Me.BackColor
        CloudResultGrid.GridLocation(pnlNwtGrid)
        CloudResultGrid.PerformLayout()
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

    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click

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
End Class