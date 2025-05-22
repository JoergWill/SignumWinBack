Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports combit.Reporting.DataProviders
Imports EnhEdit.EnhEdit_Global
Imports Infralution.Controls.VirtualTree
Imports Microsoft.Office.Interop

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
    Private _Deleted As Boolean = False

    Private _RezeptSchritt As wb_Rezeptschritt = Nothing    'aktuelle ausgewählter Rezeptschritt (Popup)
    Private _RezeptSchrittNeu As wb_Rezeptschritt = Nothing 'neuer Rezeptschritt (Auswahl-Liste)

    Private _HisSollwertDeltaStyle As New Infralution.Controls.StyleDelta
    Private _ProdStufeDeltaStyle As New Infralution.Controls.StyleDelta
    Private _TextKompDeltaStyle As New Infralution.Controls.StyleDelta

    Private xlApp As Microsoft.Office.Interop.Excel.Application
    Private xlWorkBooks As Excel.Workbooks
    Private xlWorkBook As Excel.Workbook
    Private xlWorkSheets As Excel.Sheets

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
    Public Sub New(RzNummer As Integer, RzVariante As Integer, Optional RzAendIndex As Integer = wb_Global.UNDEFINED, Optional EnableRezeptKopieren As Boolean = False)
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
        _TextKompDeltaStyle.Font = New Drawing.Font(FontArial, System.Drawing.FontStyle.Italic)

        'Rezeptnummer und Rezept-Variante merken
        _RzNummer = RzNummer
        _RzVariante = RzVariante
        _RzAendIndex = RzAendIndex

        ' TabControl - HideTabs
        Wb_TabControl.HideTabs = True
        'Default-Währung (€)
        lblEinhPreis.Text = wb_GlobalSettings.osDefaultWaehrung

        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbVariante.Fill(wb_Rezept_Shared.RzVariante)
        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbLiniengruppe.Fill(wb_Linien_Global.RezeptLinienGruppen)

        'Nährwerte Zutatenliste Einstellungen
        SwListeOptimieren.Checked = wb_GlobalSettings.NwtShowOptimized
        SwENummern.Checked = wb_GlobalSettings.NwtShowENummer

        'Einlesen Rezeptkopf und Rezeptschritte 
        GetRezeptur(_RzNummer, _RzVariante, _RzAendIndex, _Historical)
        'Nur Anzeige bei gelöschten Rezepten
        _Deleted = Rezept.Deleted

        'Virtual Tree anzeigen
        VirtualTree.DataSource = Rezept.RootRezeptSchritt
        'alle Zeilen aufklappen
        VirtualTree.RootRow.ExpandChildren(True)
        'falls keine Rezeptschritte vorhanden sind muss das Popup-Menu ausserhalb erstellt werden
        VT_MakeTreePopup()

        'Bei der Anzeige von Rezepten aus der Historie sind keine Änderungen zulässig
        If _Historical OrElse _Deleted Then
            tbRzNummer.Enabled = False
            tbRezeptName.Enabled = False
            tbRzKommentar.Enabled = False
            tbRzTeigTemp.Enabled = False
            tbKnetKennlinie.Enabled = False
            cbVariante.Enabled = False
            cbLiniengruppe.Enabled = False
            VirtualTree.Enabled = False

            BtnNwt.Enabled = False
            TextHinweise.ReadOnly = True

            'Wiederherstellen der Rezeptur über Btn Kopieren
            BtnKopieren.Text = "Wiederherstellen"

            'Button "Endgültig löschen"
            If _Deleted Then
                BtnLoeschen.Text = "Endgültig löschen"
            End If
        End If

        'Button RezeptKopieren aktiv
        BtnKopieren.Enabled = _Historical OrElse EnableRezeptKopieren

        'Button Löschen aktiv
        BtnLoeschen.Enabled = Not _Historical

        'Excel-Export Nährwerte nur wenn Excel installiert ist
        BtnExcelNwt.Enabled = wb_GlobalSettings.ExcelInstalled
        BtnExcelNwtDetails.Enabled = wb_GlobalSettings.ExcelInstalled

        'Cursor wieder zurücksetzen
        Me.Cursor = Cursors.Default

        'Rezept neu anlegen - Nummer eingeben
        If tbRzNummer.Text = "" Then
            tbRzNummer.Focus()
        End If
    End Sub

    Public ReadOnly Property AktRezeptschritt As wb_Rezeptschritt
        Get
            Return _RezeptSchritt
        End Get
    End Property

    Public Property RezeptBezeichnung As String
        Get
            Return tbRezeptName.Text
        End Get
        Set(value As String)
            tbRezeptName.Text = value
        End Set
    End Property

    Private Sub cbVariante_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbVariante.SelectedIndexChanged
        'Auswahl über DropDrown
        If cbVariante.Focus Then
            'Änderungen im aktuellen Rezept speichern
            RezeptSpeichern(sender)
            'Bezeichnung und Kommentar merken
            Dim Bezeichnung As String = Rezept.RezeptBezeichnung
            Dim Kommentar As String = Rezept.RezeptKommentar
            Dim Nummer As String = Rezept.RezeptNummer

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
                    Rezept.RezeptNummer = Nummer
                    'Rezept-Schritte und Rezeptkopf schreiben bei Schliessen
                    _RzChanged = True
                    _RzKopfChanged = True
                    'Rezept-Variante
                    tbRzVariante.Text = Rezept.Variante
                Else
                    'Eintrag in Combo-Box Liniengruppe korrigieren
                    cbLiniengruppe.SetTextFromKey(Rezept.LinienGruppe)
                    'Eintrag in Combo-Box Rezeptvariante korrigieren
                    cbVariante.SetTextFromKey(Rezept.Variante)
                    'Rezept-Variante merken
                    _RzVariante = Rezept.Variante
                    'Rezept-Variante
                    tbRzVariante.Text = Rezept.Variante
                End If
            End If

            'Virtual Tree anzeigen
            VirtualTree.DataSource = Rezept.RootRezeptSchritt
            'alle Zeilen aufklappen
            VirtualTree.RootRow.ExpandChildren(True)
            'Rezeptnummer kann nur bei Variante 1 geändert werden
            If Rezept.Variante = 1 Then
                tbRzNummer.Enabled = True
            Else
                tbRzNummer.Enabled = False
            End If

            'falls keine Rezeptschritte vorhanden sind muss das Popup-Menu ausserhalb erstellt werden
            VT_MakeTreePopup()
        End If
    End Sub

    Private Function GetRezeptur(RzNr As Integer, RzVariante As Integer, RzAendIndex As Integer, Historical As Boolean) As Boolean
        'Rezept-Historie
        If Not Historical Then
            'Rezeptkopf und Rezeptschritte aktuell (winback) - Start mit Backverlust 0.0 !! (kein Artikel definiert)
            Rezept = New wb_Rezept(_RzNummer, Nothing, 0.0, _RzVariante, "", "", True, True)
            Me.Text = Rezept.RezeptNummer & "/V" & Rezept.Variante & " " & Rezept.RezeptBezeichnung
        Else
            'Rezeptkopf und Rezeptschritte aus der Historie (wbdaten)
            Rezept = New wb_Rezept(_RzNummer, Nothing, _RzVariante, RzAendIndex)
            Me.Text = Rezept.RezeptNummer & "/V" & Rezept.Variante & " " & Rezept.RezeptBezeichnung & " Änderung " & RzAendIndex & " vom " & Rezept.AenderungDatum
        End If

        'Prüfen ob die gesuchte Variante existiert
        If Rezept.Variante = RzVariante Then

            'Berechnete Rezepturwerte anzeigen(bei historischen Rezepten wird der Preis nicht berechnet)
            ShowCalculateRezeptDaten(False, Not Historical)

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
            'Rezept-Variante
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

    Private Sub ShowCalculateRezeptDaten(Recalculate As Boolean, CalcPreis As Boolean)
        'Neuberechnung erzwingen
        Rezept.Recalculate = Recalculate

        'Gesamt-Rohstoffpreis der Rezeptur (aktuell berechnet)
        If CalcPreis Then
            tbRzPreis.Text = wb_Functions.FormatStr(Rezept.RezeptPreis, 2)
        End If
        'Rezeptgewicht (aktuell berechnet)
        tbRzGewicht.Text = wb_Functions.FormatStr(Rezept.RezeptGewicht, 3)
        'Mehlgesamt-Menge
        tbRzMehlmenge.Text = wb_Functions.FormatStr(Rezept.RezeptGesamtMehlmenge, 2)
        'Rezept TA
        tbRzTA.Text = CInt(Rezept.RezeptTA)
        'Rezept Teigtemperatur
        tbRzTeigTemp.Text = wb_Functions.FormatStr(Rezept.RezeptTeigTemperatur, 2)
    End Sub

    ''' <summary>
    ''' Doppelklick auf Rezeptgewicht. Rezeptur auf neues Rezeptgewicht umrechnen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbRzGewicht_DoubleClick(sender As Object, e As EventArgs) Handles tbRzGewicht.DoubleClick
        If MsgBox("Soll die Rezeptur auf ein neues Rezeptgewicht umgerechnet werden ?", MsgBoxStyle.Question, "Rezeptgewicht umrechnen") = MsgBoxResult.Ok Then
            'Änderung Rezeptgewicht zulassen
            tbRzGewicht.ReadOnly = False
            tbRzGewicht.BackColor = Color.White
            tbRzGewicht.TabStop = True
        End If
    End Sub

    ''' <summary>
    ''' Eingabefeld Rezeptgewicht wurde geändert. Rezeptur umrechnen und anzeigen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbRzGewicht_Leave(sender As Object, e As EventArgs) Handles tbRzGewicht.Leave
        'Prüfen, ob das Rezeptgewicht geändert werden soll/darf
        If Not tbRzGewicht.ReadOnly Then
            'Neues Rezeptgewicht
            Dim RezeptGesamtMengeNeu As Double = wb_Functions.StrToDouble(tbRzGewicht.Text)

            'Grenzen prüfen
            If (RezeptGesamtMengeNeu > 0) AndAlso (RezeptGesamtMengeNeu < wb_Global.MaxRezeptGroesse) AndAlso (RezeptGesamtMengeNeu <> Rezept.RezeptGewicht) Then
                'Rezeptmengen umrechnen
                Rezept.RecalcRezeptGewicht(RezeptGesamtMengeNeu)
                'Anzeige aktualisieren
                VT_Aktualisieren()
                'Rezeptur wurde geändert (speichern)
                _RzChanged = True
            Else
                MsgBox("Die Rezeptgröße muss zwischen 0.0 kg und " & wb_Global.MaxRezeptGroesse & " kg liegen", MsgBoxStyle.Exclamation)
            End If

            'neues(altes) Rezeptgewicht formatiert ausgeben
            tbRzGewicht.ReadOnly = True
            tbRzGewicht.BackColor = Color.Silver
            tbRzGewicht.Text = wb_Functions.FormatStr(Rezept.RezeptGewicht, 3)
            tbRzGewicht.TabStop = False
        End If
    End Sub

    ''' <summary>
    ''' Doppelklick auf TA-Feld.
    ''' Umrechnung der Wassermenge auf eine vorgegebene TA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbRzTA_DoubleClick(sender As Object, e As EventArgs) Handles tbRzTA.DoubleClick
        If MsgBox("Soll die Rezeptur auf eine neue TA umgerechnet werden ?", MsgBoxStyle.YesNo, "Wassermenge anpassen") = MsgBoxResult.Yes Then
            'Änderung Rezeptgewicht zulassen
            tbRzTA.ReadOnly = False
            tbRzTA.BackColor = Color.White
            tbRzTA.TabStop = True
        End If

    End Sub

    ''' <summary>
    ''' Eingabefeld TA wurde geändert. Wassermenge im Rezept neu berechnen. Es wird nur die Wassermenge im flachen Rezept angepasst. Rohstoffe mit
    ''' verknüpfter Rezeptur bleiben unverändert.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tbRzTA_Leave(sender As Object, e As EventArgs) Handles tbRzTA.Leave
        'Neue TA
        Dim RzTANeu As Double = wb_Functions.StrToDouble(tbRzTA.Text)
        'Änderung der TA
        If Not tbRzTA.ReadOnly AndAlso (RzTANeu <> Rezept.RezeptTA) Then
            'Grenzen prüfen
            If (RzTANeu > wb_Global.TA_min) AndAlso (RzTANeu < wb_Global.TA_max) Then
                'Wassermenge neu berechnen
                If Rezept.RecalcWasserMengeFromTA(RzTANeu) Then
                    'Anzeige aktualisieren
                    VT_Aktualisieren()
                    'Rezeptur wurde geändert (speichern)
                    _RzChanged = True
                    'TA ist wieder Readonly
                    tbRzTA.ReadOnly = True
                    tbRzTA.TabStop = False
                Else
                    MsgBox("Die Rezeptur enthält keine (Wasser)Komponente, die angepasst werden kann", MsgBoxStyle.Exclamation)
                    tbRzTA.Text = CInt(Rezept.RezeptTA)
                    tbRzTA.ReadOnly = True
                    tbRzTA.TabStop = False
                End If
            Else
                MsgBox("Die TA muss zwischen " & wb_Global.TA_min & " und " & wb_Global.TA_max & " liegen", MsgBoxStyle.Exclamation)
                tbRzTA.Text = CInt(Rezept.RezeptTA)
                tbRzTA.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Rezeptur kopieren/wiederherstellen(historische Rezepte)
    ''' Die bestehende (aktuelle) Rezeptur wird gespeichert. Danach wird ein neues Rezept angelegt und die Daten kopiert.
    ''' Anschliessend wird das neue Rezept in einem neuen Fenster geöffnet.
    ''' 
    ''' Bei der Anzeige der Rezepthistorie wird über den Button "Wiederherstellen" das Rezept aus dem gespeicherten
    ''' historischen Rezept überschrieben.
    ''' 
    ''' Bei gelöschen Rezepten wird über den Button "Wiederherstellen" die Liniengruppe korrigiert, damit ist das Rezept 
    ''' wieder lesbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnKopieren_Click(sender As Object, e As EventArgs) Handles BtnKopieren.Click
        'Mehrfach-Auslösen verhindern
        BtnKopieren.Enabled = False
        'Anzeige historische Rezepte - Wiederherstellen
        If _Historical Then
            'Restore erst nach Nachfrage
            If MsgBox("Soll das aktuelle Rezept mit dieser Version überschrieben werden", MsgBoxStyle.YesNo, "Rezeptur wiederherstellen") = MsgBoxResult.Yes Then
                'Rezept aus der Rezept-Historie wiederherstellen
                _RzChanged = True
                _RzKopfChanged = True
                RezeptSpeichern(sender)

                'wiederhergestelltes Rezept öffnen
                If MsgBox("Das Rezept wurde wiederhergestellt" & vbCrLf & "Soll das Rezept jetzt zum Bearbeiten geöffnet werden", MsgBoxStyle.YesNo, "Rezeptur wiederherstellen") = MsgBoxResult.Yes Then
                    Dim RestoreRezeptRezeptur As New wb_Rezept_Rezeptur(_RzNummer, _RzVariante)
                    RestoreRezeptRezeptur.Show()
                End If
                'Fenster schliessen - Änderungen sind im aktuellen Fenster nicht möglich
                Me.Close()
            Else
                'Button wiederherstellen
                BtnKopieren.Enabled = True
            End If
        ElseIf _Deleted Then
            'Restore erst nach Nachfrage
            If MsgBox("Soll das gelöschte Rezept wiederhergestellt werden", MsgBoxStyle.YesNo, "Rezeptur wiederherstellen") = MsgBoxResult.Yes Then
                'Rezept aus Papierkorb wiederherstellen
                Rezept.MySQLdbUndelete()
                'Fenster schliessen - Änderungen sind im aktuellen Fenster nicht möglich
                wb_Rezept_Shared.Liste_Refresh(Nothing)
                Close()
            End If
        Else
            'Rezept kopieren (Die Kopie wird in Rezept_Main erstellt und aufgerufen)
            wb_Rezept_Shared.Rezept_Copy(sender, Rezept.RezeptNr, Rezept.Variante)
        End If
    End Sub

    ''' <summary>
    ''' Rezeptur drucken.
    ''' Der Ausdruck erfolgt über Printer-Sub-Funktion (List+Label)
    ''' 
    ''' Parameter 1 enthält die aktuelle Einheit für das Rezeptgesamtgewicht (länderspezifisch)
    ''' Parameter 2 enthält eventuelle Rezepthinweis-Texte
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub BtnDrucken_Click(sender As Object, e As EventArgs) Handles BtnDrucken.Click
        'Druck-Daten
        Dim pDialog As New wb_PrinterDialog(False) With {
            .LL_KopfZeile_1 = "Letzte Änderung " & Rezept.AenderungDatum & " durch " & Rezept.AenderungName,
            .LL_Parameter_1 = "kg"
        } 'Drucker-Dialog

        'Rezept-Verarbeitungshinweise in Parameter_2 (Lesen wenn notwendig - aktuell geänderte Hinweistexte werden direkt verarbeitet)
        If _RzHinweiseChanged Then
            pDialog.LL_Parameter_2 = TextHinweise.Text
        Else
            pDialog.LL_Parameter_2 = RezeptHinweise.Memo(_RzNummer)
        End If

        'Die Kopfdaten werden im Root-Rezept-Schritt eingetragen
        Rezept.RootRezeptSchritt.Nummer = Rezept.RezeptNummer
        Rezept.RootRezeptSchritt.Bezeichnung = Rezept.RezeptBezeichnung
        Rezept.RootRezeptSchritt.Kommentar = Rezept.RezeptKommentar
        'Anzeige der Rezept-Variante im Gruppenkopf
        Rezept.RootRezeptSchritt.OberGW = Rezept.Variante
        'Anzeige Einheit Rezept-Gesamtmenge im Gruppenfuß
        Rezept.RootRezeptSchritt.Einheit = lblEinhRzGewicht.Text

        'RootRezeptSchritt.Steps enthält alle Rezeptschritte als flache Liste 
        pDialog.LL.DataSource = New ObjectDataProvider(Rezept.RootRezeptSchritt)

        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Rezepte"
        pDialog.ListFileName = "Rezeptur.lst"
        pDialog.ShowDialog()
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
            tb_ZutatenListe.BorderStyle = System.Windows.Forms.BorderStyle.None
            Show_ZutatenListe()
            BtnHinweise.Text = "Hinweise"
        Else
            If tb_Hinweise.Visible Then
                'Rezeptur anzeigen
                Wb_TabControl.SelectedTab = tb_Rezeptur
                BtnNwt.Text = "Nährwerte"
            Else
                'Rezept-Hinweise lesen
                If Not RezeptHinweise.ReadOK Then
                    'Nur ein Hinweis-Text für alle Varianten
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
            'Rezeptur neu aufbauen(Flag Rezept wurde geändert NICHT setzen)
            VT_Aktualisieren(False)
            ToolStripAllergenLegende.Visible = False
        End If

    End Sub

    Public Sub RzptLoeschen()
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String = wb_sql_Selects.setParams(wb_sql_Selects.sqlRezeptInKomp, Rezept.RezeptNr)
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
            If _Deleted Then
                If MsgBox("Soll dieses Rezept gelöscht werden ?", MsgBoxStyle.YesNo, "Rezeptur löschen") = vbNo Then
                    Exit Sub
                End If
                'Rezept löschen (wird endgültig aus der Datenbank gelöscht)
                Rezept.MySQLdbDelete(False)
                'Rezept-Verarbeitungshinweise löschen
                RezeptHinweise.Delete()
            Else
                If MsgBox("Soll dieses Rezept komplett gelöscht werden ?", MsgBoxStyle.YesNo, "Rezeptur endgültig löschen") = vbNo Then
                    Exit Sub
                End If
                'Rezept löschen (erst mal nur in den Papierkorb schieben)
                Rezept.MySQLdbDelete(True)
            End If

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
        Dim nwtGrid As New wb_ArrayGridViewKomponParam301(Rezept.KtTyp301.NwtTabelle) With {.BackgroundColor = Me.BackColor}
        nwtGrid.GridLocation(tb_Naehrwerte)
        nwtGrid.PerformLayout()
    End Sub

    ''' <summary>
    ''' Fenster schliessen
    ''' Änderungen an der Rezeptur werden gespeichert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        'Rezept Änderungen sichern ohne Nachfrage (nur wenn keine Rezepthistorie geöffnet)
        If Not _Historical Then
            RezeptSpeichern(sender)
        End If
        'Fenster schliessen (ohne Nachfrage)
        Close()
    End Sub

    ''' <summary>
    ''' Fenster schliessen. Falls notwendig Daten sichern. Beim Schliessen werden geänderte Rezepthinweise, Rezepturdaten und Rezeptkopf-Daten
    ''' in die MySQL-DB geschrieben.
    ''' Beim Sichern der Rezepturdaten wird auch eine Kopie der aktuellen Rezeptur in die Rezept-Historie geschrieben
    ''' Mit dem Sicherung des Rezeptkopfes wird der Änderungs-Index erhöht (falls die Rezeptur geändert wurde).
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Rezept_Rezeptur_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        'Rezept Änderungen sichern mit Nachfrage (nur wenn keine Rezepthistorie geöffnet)
        If Not _Historical AndAlso (_RzHinweiseChanged OrElse _RzChanged OrElse _RzKopfChanged) Then
            Select Case MsgBox("Das Rezept wurde geändert - Änderungen speichern?", MsgBoxStyle.YesNoCancel, "Rezeptur speichern")
                Case MsgBoxResult.Cancel
                    'Fenster nicht schliessen
                    e.Cancel = True
                Case MsgBoxResult.Yes
                    'Rezept speichern
                    RezeptSpeichern(sender)
            End Select
        End If

        'Excel aufräumen
        If xlWorkBook IsNot Nothing Then
            Marshal.FinalReleaseComObject(xlWorkBook)
            xlWorkBook = Nothing
        End If

        If xlWorkBooks IsNot Nothing Then
            Marshal.FinalReleaseComObject(xlWorkBooks)
            xlWorkBooks = Nothing
        End If

        Try
            If xlApp IsNot Nothing Then
                xlApp.Quit()
                Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing
            End If
        Catch ex As Exception
        End Try
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
            'Rezeptur speichern
            Rezept.MySQLdbWrite_RzSchritt(_RzNummer, _RzVariante, _Historical)
            'Die Nährwerte aller mit dieser Rezeptur verknüpften Artikel/Rohstoffe müssen neu berechnet und an OrgaBack geschrieben werden (Flag setzen)
            Rezept.ArtikelMarkieren()
        End If

        'Rezeptkopfdaten schreiben (beim Wiederherstellen der historischen Rezepte wird die letzte Änderungsnummer aus der Datenbank ermittelt)
        If _RzKopfChanged OrElse _RzChanged Then

            If _Historical Then
                'letzte Änderungs-Nummer aus der Tabelle Rezepte
                Rezept.RezeptKommentar = "Rezept vom " & Rezept.AenderungDatum & " Nr." & Rezept.AenderungNummer
                Rezept.AenderungNummer = wb_sql_Functions.getLastAenderungsIndex(_RzNummer)
            End If

            Rezept.MySQLdbWrite_Rezept(_RzChanged)
            wb_Rezept_Shared.Liste_Refresh(sender)
        End If

        'Reset Flags
        _RzHinweiseChanged = False
        _RzKopfChanged = False
        _RzChanged = False
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
            'Cursor wieder zurückschalten
            Me.Cursor = Cursors.Default
        End If
    End Sub

    ''' <summary>
    ''' Click auf das Virtual-Tree-Steuerelement. 
    ''' Anhand der Koordinaten wird ermittelt, ob der Click innerhalb der Titel-Zeile erfolgt ist.
    '''     Wenn e.Y innerhalb der Höhe der Titelzeile ist
    '''     dann gibt e.X die Position der Spalte an
    '''     
    ''' Wenn der Click auf die Spalte AnteilInProzent erfolgt, ändert sich der Anzeige-Modus
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_Click(sender As Object, e As EventArgs) Handles VirtualTree.Click
        'Maus-Koordinaten
        Dim Y As Integer = DirectCast(e, System.Windows.Forms.MouseEventArgs).Y
        Dim X As Integer = DirectCast(e, System.Windows.Forms.MouseEventArgs).X
        'Prüfen ob Click auf die Titelzeile 
        Dim SpalteNr As Integer = GetHeaderColumn(X, Y)

        'Click auf die Spalte Anteil/Prozent ändert die Funktion der Spalte
        If SpalteNr = 5 Then
            Debug.Print("Change Mehl%")
        End If
    End Sub

    ''' <summary>
    ''' Prüft ob die Click-Koordinaten innerhalb der Titelzeile liegen. In diesem Fall wird
    ''' die Spalten-Nummer zurückgegeben.
    ''' Ansonsten False(-1)
    '''
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <returns></returns>
    Private Function GetHeaderColumn(x As Integer, y As Integer) As Integer
        'Ergebnis vorbelegen
        Dim Result As Integer = -1
        'alle Spalten(Breite) durchlaufen
        Dim Colx As Integer = 0

        'Prüfen ob Click auf die Titelzeile
        If y < VirtualTree.HeaderHeight Then
            Result = 0
            'alle Spalten im Grid
            For Each Col As Infralution.Controls.VirtualTree.Column In VirtualTree.Columns
                Colx += Col.Width
                If Colx > x Then
                    Exit For
                End If
                Result += 1
            Next
        End If
        Return Result
    End Function

    Private Sub SwENummern_Click(sender As Object, e As EventArgs) Handles SwENummern.Click
        'Zutatenliste anzeigen, wenn notwendig berechnen
        Show_ZutatenListe()
    End Sub

    Private Sub SwListeOptimieren_Click(sender As Object, e As EventArgs) Handles SwListeOptimieren.Click
        'Zutatenliste neu berechnen
        Show_ZutatenListe(True)
    End Sub

    Private Sub BtnZutatenListeNeu_Click(sender As Object, e As EventArgs) Handles BtnZutatenListeNeu.Click
        'Zutatenliste neu berechnen
        Show_ZutatenListe(True)
    End Sub

    Private Sub Show_ZutatenListe(Optional ReCalc As Boolean = False)
        'Cursor anzeigen
        Me.Cursor = Cursors.WaitCursor
        'anzeigen ...
        Application.DoEvents()

        'Zutatenliste anzeigen/optimieren/Neu berechnen 
        tb_ZutatenListe.Text = Rezept.ZutatenListe(SwENummern.Checked, wb_GlobalSettings.NwtCalcQuid, SwListeOptimieren.Checked, ReCalc)

        'Mehlzusammensetzung (Berechnet aus der Zutatenliste - getrennt durch Zeilenvorschub)
        tbMehlZusammenSetzung.Text = Rezept.MehlZusammensetzung(vbCrLf)
        'Cursor wieder zurücksetzen
        Me.Cursor = Cursors.Default
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
        GLeFormat = _RezeptSchritt.Format
        GLeOG = _RezeptSchritt.OberGW
        GLeUG = _RezeptSchritt.UnterGW
        GLoValue = _RezeptSchritt.Sollwert
        ToolStripFormat.Visible = False

        'Grenzwerte im Toolstrip anzeigen
        If GLeFormat = wb_Format.fReal Then
            ToolStripFormat.Text = wb_Functions.FormatStr(GLeUG, 1) & " " & _RezeptSchritt.VirtTreeEinheit & " > NUM > " & wb_Functions.FormatStr(GLeOG, 1) & " " & _RezeptSchritt.VirtTreeEinheit
            ToolStripFormat.Visible = True
            GLoValue = wb_Functions.FormatStr(_RezeptSchritt.Sollwert, 3, -1, "sql")
        ElseIf GLeFormat = wb_Format.fString Then
            ToolStripFormat.Text = "TEXT"
            ToolStripFormat.Visible = True
            GLoValue = _RezeptSchritt.Sollwert
        End If

        'Verhindert dass einzelne Zellen markiert werden
        e.Cancel = True
    End Sub

    ''' <summary>
    ''' Edit-Funktion ist beendet. Der Sollwert wird in den Rezeptschritt geschrieben.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_SetCellValue(sender As Object, e As SetCellValueEventArgs) Handles VirtualTree.SetCellValue
        Dim Binding As RowBinding = _VirtualTree.GetRowBinding(e.Row)

        'aktuell ausgewählten Rezeptschritt
        _RezeptSchritt = DirectCast(e.Row.Item, wb_Rezeptschritt)
        If _RezeptSchritt IsNot Nothing Then

            'alten Sollwert merken (wird in RS_Wert_HIS gespeichert)
            _RezeptSchritt.SaveSollwert_org()

            'an dieser Stelle wird der Sollwert aus dem Editor in den Rezeptschritt geschrieben
            Binding.SetCellValue(e.Row, e.Column, e.OldValue, e.NewValue)
            'Rezeptdaten neu berechnen (auch die Preis-Information)
            ShowCalculateRezeptDaten(True, True)
            'Rezeptur wurde geändert
            _RzChanged = True
            ToolStripRezeptChange.Visible = True
            ToolStripFormat.Visible = False
            'Anzeige Preis und Prozent aktualisieren
            VT_Aktualisieren()
            '(@V1.8.6)Focus wieder auf die aktuelle Zeile - Sonst wird nach Edit-Ende die zweite Zeile von oben aktiviert !
            VirtualTree.SelectedRow = e.Row
        End If
    End Sub

    Private Sub VirtualTree_GetCellData(sender As Object, e As GetCellDataEventArgs) Handles VirtualTree.GetCellData
        'get the default binding for the given row And use it to populate the cell data
        Dim Binding As RowBinding = _VirtualTree.GetRowBinding(e.Row)
        If Binding IsNot Nothing Then
            Binding.GetCellData(e.Row, e.Column, e.CellData)

            'aktuell ausgewählten Rezeptschritt merken (Popup)
            _RezeptSchritt = DirectCast(e.Row.Item, wb_Rezeptschritt)
            Debug.Print("VirtualTree_GetCellData " & _RezeptSchritt.Bezeichnung & " UG/OG/Format " & _RezeptSchritt.UnterGW & "/" & _RezeptSchritt.OberGW & "/" & _RezeptSchritt.Format & "/" & _RezeptSchritt.Type.ToString)

            'Kein Drag von Wasser-/Kneter-Komponenten
            If (_RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE AndAlso _RezeptSchritt.ParamNr > 1) OrElse _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_KNETER Then
                RowBAllowDrop(Binding, False)
            Else
                RowBAllowDrop(Binding, True)
            End If

            'Produktions-Stufe Fett drucken
            If _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Then
                VirtualTree_SetFontStyle(e.CellData.EvenStyle, _ProdStufeDeltaStyle)
                VirtualTree_SetFontStyle(e.CellData.OddStyle, _ProdStufeDeltaStyle)
            End If

            'Textkomponente Kursiv drucken
            If _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE Then
                VirtualTree_SetFontStyle(e.CellData.EvenStyle, _TextKompDeltaStyle)
                VirtualTree_SetFontStyle(e.CellData.OddStyle, _TextKompDeltaStyle)
            End If

            'QUID-Relevante Rezeptzeilen in grün anzeigen
            If _RezeptSchritt.QUIDRelevant Then
                Dim QuidStyle As New Infralution.Controls.Style(e.CellData.OddStyle) With {.ForeColor = Color.Green}
                e.CellData.EvenStyle = QuidStyle
                e.CellData.OddStyle = QuidStyle
            End If

            'Rezeptzeilen mit fehlenden Nährwertangaben in rot anzeigen
            If _RezeptSchritt.KtTyp301DatenFehlerhaft Then
                Dim QuidStyle As New Infralution.Controls.Style(e.CellData.OddStyle) With {.ForeColor = Color.Red}
                e.CellData.EvenStyle = QuidStyle
                e.CellData.OddStyle = QuidStyle
            End If

            'Edit Bezeichnungs-Text
            If e.Column.Name = "ColBezeichnung" AndAlso wb_Functions.TypeIstText(_RezeptSchritt.Type) Then
                'Bei Anzeige der Rezept-Historie werden die geänderten Werte Fett/Kursiv dargestellt
                If _Historical AndAlso _RezeptSchritt.WertProd <> "" Then
                    VirtualTree_SetFontStyle(e.CellData.EvenStyle, _HisSollwertDeltaStyle)
                    VirtualTree_SetFontStyle(e.CellData.OddStyle, _HisSollwertDeltaStyle)
                End If
                'Editor aktiv - Bezeichnungstext
                GLoValue = _RezeptSchritt.Sollwert
                Exit Sub
            End If

            'Edit Sollwert
            If e.Column.Name = "ColSollwert" AndAlso (wb_Functions.TypeIstSollMenge(_RezeptSchritt.Type, 1) OrElse wb_Functions.TypeIstSollWert(_RezeptSchritt.Type, 3)) Then
                'Bei Anzeige der Rezept-Historie werden die geänderten Werte Fett/Kursiv dargestellt
                If _Historical AndAlso _RezeptSchritt.WertProd <> "" AndAlso _RezeptSchritt.WertProd <> "0" Then
                    VirtualTree_SetFontStyle(e.CellData.EvenStyle, _HisSollwertDeltaStyle)
                    VirtualTree_SetFontStyle(e.CellData.OddStyle, _HisSollwertDeltaStyle)
                End If
                'Editor aktiv - Sollwert
                GLoValue = _RezeptSchritt.fSollwert
                Exit Sub
            End If
        End If
        'Alle Anderen - Edit nicht erlaubt
        e.CellData.Editor = Nothing
    End Sub

    Private Sub RowBAllowDrop(RowB As RowBinding, YesNo As Boolean)
        RowB.AllowDropAboveRow = YesNo
        RowB.AllowDropBelowRow = YesNo
        RowB.AllowDropOnRow = False
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
    ''' Rezeptzeile wurde verschoben. (Drag and Drop)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_DragDrop(sender As Object, e As DragEventArgs) Handles VirtualTree.DragDrop
        _RzChanged = True
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
                AuswahlFilter = wb_Rohstoffe_Shared.AnzeigeFilter.Undefined
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
            If _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE OrElse _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_KESSEL Then
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
        _RezeptSchritt = New wb_Rezeptschritt(Rezept.RootRezeptSchritt, wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE) With {.SchrittNr = Rezept.RootRezeptSchritt.ChildSteps.Count, .Sollwert = VTP_RezeptSchrittGetText(Sender, "Produktions-Stufe")}
        'Focus auf Eingabe Sollwert
        VT_Aktualisieren(ColBezeichnung, False)
    End Sub

    ''' <summary>
    ''' Neuen Kessel nach Produktions-Stufe einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeuerKesselNachProduktionsStufe(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_KESSEL) With {.Sollwert = VTP_RezeptSchrittGetText(Sender, "Kessel")}
        _RezeptSchritt.Insert(_RezeptSchrittNeu, True)
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neuen Kessel oberhalb der aktuellen Kessel-Zeile einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeuerKesselDavor(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_KESSEL) With {.Sollwert = VTP_RezeptSchrittGetText(Sender, "Kessel")}
        _RezeptSchritt.Insert(_RezeptSchrittNeu, False)
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neuen Kessel unterhalb der aktuellen Kessel-Zeile einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeuerKesselDanach(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_KESSEL) With {.Sollwert = VTP_RezeptSchrittGetText(Sender, "Kessel")}
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
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE) With {.Sollwert = VTP_RezeptSchrittGetText(Sender, "Produktions-Stufe")}
        _RezeptSchritt.Insert(_RezeptSchrittNeu, True)
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neue Produktions-Stufe einfügen.
    ''' Ist die neue Produktions-Stufe die erste Produktions-Stufe im Rezept wird das Rezept nach dem Einfügen zunächst gespeichert!
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueProduktionsStufeDavor(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE) With {.Sollwert = VTP_RezeptSchrittGetText(Sender, "Produktions-Stufe")}
        _RezeptSchritt.Insert(_RezeptSchrittNeu, False)

        'Sonderfall neue (erste) Produktions-Stufe in Schritt 1
        If _RezeptSchritt.SchrittNr = 2 AndAlso _RezeptSchritt.Type <> wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Then
            'Änderungen im aktuellen Rezept speichern
            VT_Aktualisieren()
            RezeptSpeichern(Sender)
            'Prüfen ob die gesuchte Rezept-Variante vorhanden ist
            GetRezeptur(_RzNummer, _RzVariante, _RzAendIndex, _Historical)
        End If
        VT_Aktualisieren()
    End Sub

    Private Function VTP_RezeptSchrittGetText(Sender As Object, DefaultText As String) As String
        Dim s As String = TryCast(Sender, ToolStripMenuItem).Text
        If s <> "" Then
            Return s
        Else
            Return DefaultText
        End If
    End Function

    ''' <summary>
    ''' Neue Text-Komponente in ein leeres Rezept ein- oder an eine Rezeptur anfügen (Click in das leere Feld unterhalb VirtualTree)
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueTextKomponente(Sender As Object, e As EventArgs)
        _RezeptSchritt = New wb_Rezeptschritt(Rezept.RootRezeptSchritt, wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE) With {.Sollwert = VTP_RezeptSchrittGetText(Sender, "Kessel"), .SchrittNr = Rezept.RootRezeptSchritt.ChildSteps.Count}
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neue Text-Komponente vor der aktuellen Zeile (_Rezeptschritt) einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueTextKomponenteDavor(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE) With {.Sollwert = VTP_RezeptSchrittGetText(Sender, "Text")}
        _RezeptSchritt.Insert(_RezeptSchrittNeu, False)
        VT_Aktualisieren()
    End Sub

    ''' <summary>
    ''' Neue Text-Komponente nach der aktuellen Zeile einfügen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_NeueTextKomponenteDanach(Sender As Object, e As EventArgs)
        _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE) With {.Sollwert = VTP_RezeptSchrittGetText(Sender, "Text")}
        'Anfügen Rezeptschritt unterhalb Produktions-Stufe oder Kessel
        If _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE OrElse _RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_KESSEL Then
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
        'Text Sicherheits-Abfrage
        Dim MsgText As String

        If _RezeptSchritt.ChildSteps.Count > 0 Then
            Select Case _RezeptSchritt.Type
                Case wb_Global.KomponTypen.KO_TYPE_KESSEL, wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE
                    MsgText = "Diese Gruppe (" & _RezeptSchritt.Sollwert & ") und alle zugehörigen Rezeptschritte löschen"

                Case wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT
                    MsgText = "Dieses Kneter-Rezept und alle zugehörigen Schritte löschen"

                Case wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER
                    MsgText = "Diesen Rezeptschritt (" & _RezeptSchritt.Nummer & "/" & _RezeptSchritt.Bezeichnung & ") und alle zugehörigen Parameter löschen"

                Case Else
                    MsgText = "Diesen Rezeptschritt (" & _RezeptSchritt.Nummer & "/" & _RezeptSchritt.Bezeichnung & ") und alle zugehörigen Rezeptschritte löschen"
            End Select
        Else
            MsgText = "Diesen Rezeptschritt (" & _RezeptSchritt.Nummer & "/" & _RezeptSchritt.Bezeichnung & ") löschen"
        End If

        'Dialog-Box Abfrage
        If MsgBox(MsgText, MsgBoxStyle.YesNo, "Rezeptschritt löschen") = MsgBoxResult.Yes Then
            _RezeptSchritt.Delete()
            VT_Aktualisieren()
            'wenn kein Rezept-Schritt mehr vorhanden ist - Popup-Menu anpassen
            If Rezept.RootRezeptSchritt.ChildSteps.Count = 0 Then
                Debug.Print("Leeres Rezept")
                VT_MakeTreePopup(True)
            End If
        End If

    End Sub

    ''' <summary>
    ''' Aktuelle Rezeptzeile als QUID-Relevant markieren
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Private Sub VTP_QuidRelevant(Sender As Object, e As EventArgs)
        Debug.Print("Rezeptzeile " & _RezeptSchritt.Nummer & "als QUID-Relevant kennzeichnen")
        'Flag QUID-Relevant toggeln
        _RezeptSchritt.QUIDRelevant = Not _RezeptSchritt.QUIDRelevant
        'Anzeige aktualisieren
        VT_Aktualisieren()
    End Sub

    Private Sub VTP_RohstoffVerwaltung(Sender As Object, e As EventArgs)
        Debug.Print("Rohstoffverwaltung aufrufen Rohstoff " & _RezeptSchritt.Nummer & "/" & _RezeptSchritt.Bezeichnung)
        'im Hintergrund wird das Fenster Rohstoff-Verwaltung geöffnet (über Main-Shared)
        wb_Main_Shared.OpenForm(Me, "Rohstoffe")
    End Sub


    Private Sub VTP_NoFunction(Sender As Object, e As EventArgs)
        Trace.WriteLine("@E_Popup-Funktion noch nicht programmiert !")
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
        Dim RohstoffAuswahl As New wb_Rohstoffe_AuswahlListe

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
        If RohstoffAuswahl.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            _RezeptSchrittNeu = New wb_Rezeptschritt(Nothing, RohstoffAuswahl.RohstoffName) With {
                .RohNr = RohstoffAuswahl.RohstoffNr,
                .Nummer = RohstoffAuswahl.RohstoffNummer,
                .Kommentar = RohstoffAuswahl.RohstoffKommentar
            }

            'wenn der neue Rohstoff eine Teigtemperatur-Komponente ist, wird als Sollwert gleich die Solltemperatur aus den Rezeptkopfdaten eingetragen
            If wb_Functions.TypeIstTeigTemperaturSollwert(RohstoffAuswahl.RohstoffNr) Then
                _RezeptSchrittNeu.fSollwert = Rezept.RezeptTeigTemperatur
            Else
                _RezeptSchrittNeu.fSollwert = 0.0
            End If

            _RezeptSchrittNeu.ParamNr = 1
            _RezeptSchrittNeu.Type = RohstoffAuswahl.RohstoffType
            _RezeptSchrittNeu.Format = RohstoffAuswahl.RohstoffFormat
            _RezeptSchrittNeu.UnterGW = RohstoffAuswahl.RohstoffUG
            _RezeptSchrittNeu.OberGW = RohstoffAuswahl.RohstoffOG
            _RezeptSchrittNeu.Einheit = wb_Language.TextFilter(RohstoffAuswahl.RohstoffEinheit)

            'bei Kneterkomponenten müssen die Paramter aus Tabelle KomponParams gesetzt werden. Default-Sollwert 00:10:00
            _RezeptSchrittNeu.SetType118(True)

            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Anzeige im Virtual-Tree aktualisieren
    ''' </summary>
    Private Sub VT_Aktualisieren(Optional RezeptChanged As Boolean = True)
        VirtualTree.Invalidate()
        VirtualTree.DataSource = Rezept.RootRezeptSchritt
        'alle Zeilen aufklappen
        VirtualTree.RootRow.ExpandChildren(True)
        'Rezeptur wurde geändert
        _RzChanged = _RzChanged OrElse RezeptChanged
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
        'Umschalten auf SelectionMode.Cell, da sonst EditCurrentCell nicht funktioniert
        VirtualTree.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.Cell
        VirtualTree.SelectedColumn = EditColumn
        VirtualTree.EditCurrentCellInFocusRow()
        'Wieder zurückschalten auf SelectionMode.FullRow, der Optik wegen !
        VirtualTree.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow
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
            Dim sql As String
            Dim SchrittNr As Integer = rs.SchrittNr

            If rs.Type = wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT Then
                'Die Kneter-Rezeptur wird aus der Tabelle RohParams gelesen
                sql = wb_sql_Selects.setParams(wb_sql_Selects.sqlKneterRezept, rs.RohNr)
            Else
                'Nachfolgende Rezept-Schritte aus Tabelle KomponParams 
                sql = wb_sql_Selects.setParams(wb_sql_Selects.sqlRohstoffRez, rs.RohNr)
            End If

            'Datenbank-Verbindung
            winback.sqlSelect(sql)
            While winback.Read
                Dim Bezeichnung As String = wb_Functions.MySqlToUtf8(winback.sField("KO_Bezeichnung"))
                Dim rsc As New wb_Rezeptschritt(Nothing, Bezeichnung) With {.Kommentar = wb_Functions.MySqlToUtf8(winback.sField("KO_Kommentar"))}

                If rs.Type = wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT Then
                    SchrittNr += 1
                    rsc.SchrittNr = SchrittNr
                    rsc.RohNr = winback.iField("KO_Nr")
                    rsc.Nummer = winback.sField("KO_Nr_AlNum")
                    rsc.Type = wb_Functions.IntToKomponType(winback.iField("KT_Typ_Nr"))
                    rsc.ParamNr = 1
                    rsc.SetType118(True)
                Else
                    rsc.SchrittNr = rs.SchrittNr
                    rsc.RohNr = rs.RohNr
                    rsc.Nummer = rs.Nummer
                    rsc.Type = rs.Type
                    rsc.ParamNr = winback.iField("KT_ParamNr")
                    rsc.fSollwert = 0.0
                    rsc.Format = winback.iField("KT_Format")
                    rsc.OberGW = winback.iField("KT_OberGW")
                    rsc.UnterGW = winback.iField("KT_UnterGW")
                    rsc.Einheit = wb_Language.TextFilter(winback.sField("E_Einheit"))
                End If

                rs.InsertChild(rsc)
            End While
        End If
    End Sub

    Private Sub VT_MakeTreePopup(Optional LeeresRezept As Boolean = False)
        'Größe des Arrays entspricht der Anzahl der Einträge in der Enumeration in wb_global
        Dim _PopupFunctions([Enum].GetValues(GetType(wb_Global.TPopupFunctions)).Length + 1) As Boolean

        'Sonderfall - das Rezept ist leer
        If _RezeptSchritt Is Nothing OrElse LeeresRezept Then
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
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_QuidDeklaration) = True
                    _PopupFunctions(wb_Global.TPopupFunctions.TP_RohstoffVerwaltung) = True

            End Select

            '_PopupFunctions(wb_Global.TPopupFunctions.TP_Editieren) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_TTS_loeschen) = True

            '_PopupFunctions(wb_Global.TPopupFunctions.TP_Verschieben_Oben) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_Verschieben_Unten) = True

            '_PopupFunctions(wb_Global.TPopupFunctions.TP_TeigTemp) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_RohstoffVerwaltung) = True
            '_PopupFunctions(wb_Global.TPopupFunctions.TP_Naehrwerte_Laden) = True

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
            VT_MakeTreeDropDownPopup("Neue Produktions-Stufe", wb_Rezept_Shared.ProdStufeText, AddressOf VTP_NeueProduktionsStufe)
        End If

        'Neue Produktions-Stufe davor
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe_Davor) Then
            VT_MakeTreeDropDownPopup("Neue Produktions-Stufe davor", wb_Rezept_Shared.ProdStufeText, AddressOf VTP_NeueProduktionsStufeDavor)
        End If
        'Neue Produktions-Stufe danach
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueProduktionsStufe_Danach) Then
            VT_MakeTreeDropDownPopup("Neue Produktions-Stufe danach", wb_Rezept_Shared.ProdStufeText, AddressOf VTP_NeueProduktionsStufeDanach)
        End If

        'Neuer Kessel nach der Produktions-Stufe
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Darunter) Then
            VT_MakeTreeDropDownPopup("Neuer Kessel", wb_Rezept_Shared.KesselStufeText, AddressOf VTP_NeuerKesselNachProduktionsStufe)
        End If
        'Neuer Kessel davor
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Davor) Then
            VT_MakeTreeDropDownPopup("Neuer Kessel davor", wb_Rezept_Shared.KesselStufeText, AddressOf VTP_NeuerKesselDavor)
        End If
        'Neuer Kessel danach
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeuerKessel_Danach) Then
            VT_MakeTreeDropDownPopup("Neuer Kessel danach", wb_Rezept_Shared.KesselStufeText, AddressOf VTP_NeuerKesselDanach)
        End If

        'Neue Textkomponente
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente) Then
            VT_MakeTreeDropDownPopup("Neue Textkomponente", wb_Rezept_Shared.TextKomponenteText, AddressOf VTP_NeueTextKomponente)
        End If
        'Neue Textkomponente am Ende anfügen
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Darunter) Then
            VT_MakeTreeDropDownPopup("Neue Textkomponente anfügen", wb_Rezept_Shared.TextKomponenteText, AddressOf VTP_NeueTextKomponente)
        End If
        'Neue Textkomponente davor
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Davor) Then
            VT_MakeTreeDropDownPopup("Neue Textkomponente davor", wb_Rezept_Shared.TextKomponenteText, AddressOf VTP_NeueTextKomponenteDavor)
        End If
        'Neue Textkomponente danach
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_NeueTextKomponente_Danach) Then
            VT_MakeTreeDropDownPopup("Neue Textkomponente danach", wb_Rezept_Shared.TextKomponenteText, AddressOf VTP_NeueTextKomponenteDanach)
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
            VTPopUpMenu.Items.Add("Reset TTS-Parameter", Nothing, AddressOf VTP_NoFunction)
        End If

        'Nach oben verschieben
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_Verschieben_Oben) Then
            VTPopUpMenu.Items.Add("Zeile nach oben verschieben", Nothing, AddressOf VTP_NoFunction)
        End If
        'Nach unten verschieben
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_Verschieben_Unten) Then
            VTPopUpMenu.Items.Add("Zeile nach unten verschieben", Nothing, AddressOf VTP_NoFunction)
        End If

        'Anzeige Teigtemperatur-Parameter
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_TeigTemp) Then
            VTPopUpMenu.Items.Add("Anzeige der TTS-Parameter", Nothing, AddressOf VTP_NoFunction)
        End If

        'Rohstoff-Verwaltung
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_RohstoffVerwaltung) Then
            VTPopUpMenu.Items.Add("Rohstoff-Verwaltung", Nothing, AddressOf VTP_RohstoffVerwaltung)
        End If
        'Nährwerte laden
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_Naehrwerte_Laden) Then
            VTPopUpMenu.Items.Add("Nährwerte aktualisieren", Nothing, AddressOf VTP_NoFunction)
        End If
        'QUID-Deklaration
        If _PopupFunctions(wb_Global.TPopupFunctions.TP_QuidDeklaration) Then
            VTPopUpMenu.Items.Add("QUID relevant", Nothing, AddressOf VTP_QuidRelevant)
        End If

    End Sub

    Private Sub VT_MakeTreeDropDownPopup(Text As String, DropDown As List(Of String), OnClick As EventHandler)
        Dim MenuItemProdStufe As New ToolStripMenuItem(Text, Nothing, OnClick)
        If DropDown.Count > 0 Then
            For Each SubMenuItem As String In DropDown
                MenuItemProdStufe.DropDownItems.Add(SubMenuItem, Nothing, OnClick)
            Next
        End If
        VTPopUpMenu.Items.Add(MenuItemProdStufe)
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
    <CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnötige Zuweisung eines Werts.", Justification:="<Ausstehend>")>
    Private Sub VirtualTree_KeyUp(sender As Object, e As KeyEventArgs) Handles VirtualTree.KeyUp

        Debug.Print("KeyUp " & e.KeyCode.ToString)
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
            Case Keys.Return
                e = Nothing
        End Select

    End Sub

    Private Sub tbRezeptName_Leave(sender As Object, e As EventArgs) Handles tbRezeptName.Leave
        CheckTextBoxChanged(tbRezeptName, Rezept.RezeptBezeichnung)
    End Sub

    Private Sub tbRzKommentar_Leave(sender As Object, e As EventArgs) Handles tbRzKommentar.Leave
        CheckTextBoxChanged(tbRzKommentar, Rezept.RezeptKommentar)
    End Sub

    Private Sub tbRzTeigTemp_Leave(sender As Object, e As EventArgs) Handles tbRzTeigTemp.Leave
        CheckTextBoxChanged(tbRzTeigTemp, Rezept.RezeptTeigTemperatur)
    End Sub

    Private Sub tbRzNummer_Leave(sender As Object, e As EventArgs) Handles tbRzNummer.Leave
        'Rezeptnummer (Alpha) wurde geändert
        If (tbRzNummer.Text <> Rezept.RezeptNummer) AndAlso (tbRzNummer.Text <> "") Then
            'Prüfen ob die neue Rezeptnummer schon existiert (Neuanlage)
            If Rezept.MySQLdbCheck_RzKopf(tbRzNummer.Text) Then
                MsgBox("Diese Rezeptnummer exisitiert schon" & vbCrLf & "Bitte andere Nummer auswählen", MsgBoxStyle.Exclamation, "Rezept neu anlegen")
                'Originalwert wieder eintragen
                tbRzNummer.Text = Rezept.RezeptNummer
                tbRzNummer.Focus()
                Exit Sub
            End If
        End If

        'Rezeptnummer wurde geändert (sonst Exit Sub)
        Rezept.RezeptNummer = tbRzNummer.Text
        'alle Varianten bekommen die neue Rezeptnummer 
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim sql As String = wb_sql_Selects.setParams(wb_sql_Selects.sqlRezeptUpdateNummer, Rezept.RezeptNr, Rezept.RezeptNummer)
        'Alle Rezept-Varianten 
        Try
            winback.sqlCommand(sql)
        Catch ex As Exception
            MsgBox("Fehler beim Ändern der Rezeptnummer", MsgBoxStyle.Critical)
        End Try
        'Datenbankverbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Flag Rezeptkopf wurde geändert
    ''' </summary>
    ''' <param name="tb"></param>
    ''' <param name="Value"></param>
    ''' <returns></returns>
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

    Private Sub cbLiniengruppe_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLiniengruppe.SelectedIndexChanged
        If cbLiniengruppe.Focused Then
            ToolStripRezeptChange.Visible = True
            _RzKopfChanged = True
            Rezept.LinienGruppe = cbLiniengruppe.GetKeyFromSelection()
        End If
    End Sub

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
            e.Graphics.DrawString(VarianteText, e.Font, New SolidBrush(Color.DarkGray), e.Bounds.X, e.Bounds.Y)
        End If
    End Sub

    ''' <summary>
    ''' Erstellt Excel-File mit der Berechnung der Nährwerte und Zusammensetzung der Deklaration.
    ''' Dient zur Kontrolle und Fehler-Ermittlung bei der Zutatenliste
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnExcelNwt_Click(sender As Object, e As EventArgs) Handles BtnExcelNwt.Click
        'Erstellt eine flache Liste (ohne Unter-Rezepte)
        ExcelNwt(False)
    End Sub

    ''' <summary>
    ''' Erstellt Excel-File mit der Berechnung der Nährwerte und Zusammensetzung der Deklaration.
    ''' Dient zur Kontrolle und Fehler-Ermittlung bei der Zutatenliste
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnExcelNwtDetails_Click(sender As Object, e As EventArgs) Handles BtnExcelNwtDetails.Click
        'Erstellt eine detailierte Liste (mit allen Unter-Rezepten)
        ExcelNwt(True)
    End Sub

    Private Sub ExcelNwt(DetailAnsicht As Boolean)
        'Excel OLE-Verknüpfung
        xlApp = New Excel.Application
        xlWorkBooks = xlApp.Workbooks
        xlWorkBook = xlWorkBooks.Add()
        xlWorkSheets = xlWorkBook.Sheets

        'Wurzel-Rezept auf Tab-Seite 2ff erzeugen
        ExcelNwtPage(Rezept, xlWorkSheets, DetailAnsicht)

        'Zutatenliste auf Tab-Seite 1 erzeugen
        If DetailAnsicht Then
            ExcelZutatenPage(Rezept, xlWorkSheets)
        End If

        'Display Excel
        xlApp.Visible = True
        xlApp.UserControl = True
    End Sub

    <CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnötige Zuweisung eines Werts.", Justification:="<Ausstehend>")>
    <CodeAnalysis.SuppressMessage("Performance", "CA1829:Length/Count-Eigenschaft anstelle von Count() verwenden, wenn verfügbar", Justification:="<Ausstehend>")>
    Private Sub ExcelNwtPage(Rezept As wb_Rezept, xlWorkSheets As Excel.Sheets, DetailAnsicht As Boolean)

        'Nächstes(neues) Arbeitsblatt
        Dim xlWorkSheet As Excel.Worksheet
        xlWorkSheet = xlWorkSheets(1)
        'Rezept Name
        Dim RzName As String = wb_Functions.XRenameToExcelTabName(Rezept.RezeptBezeichnung)
        'Doppelte Worksheet-Bezeichnungen prüfen
        xlWorkSheet.Name = wb_Functions.CheckExcelWorkSheetNameExists(RzName, xlWorkSheets)

        'Array Strings
        Dim xslRange As Excel.Range
        'Array Double
        Dim xdlRange As Excel.Range
        'Array Überschrift
        Dim xlRange As Excel.Range
        'Konstanten
        Const colRohNumr = 0
        Const colRohName = 1
        Const colRohDekl = 2
        Const colRohGrpe = 3
        Const colSollwrt = 4
        Const colStrtNwt = 5
        Const colEndNwt = 12

        'Get the range where the starting cell has the address
        xslRange = xlWorkSheet.Range("A2", Reflection.Missing.Value)
        xdlRange = xlWorkSheet.Range("E3", Reflection.Missing.Value)

        'Alle Rezeptschritte als flache Liste
        Dim Steps As New ArrayList
        Steps = Rezept.RootRezeptSchritt.Steps

        'Aufzählung der Nährwerte und Allergene
        Dim nwtArray = {1, 2, 3, 4, 5, 6, 11, 12, 13, 14, 15, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154}
        'Array für die Daten (Zeilen,Spalten)
        Dim xlsArray(Steps.Count + 1, colStrtNwt + nwtArray.Count + 1) As String
        Dim xldArray(Steps.Count + 1, nwtArray.Count + 1) As Double

        'In der ersten Zeile steht die Rezept-Bezeichnung
        xlWorkSheet.Range("A1").Value = Rezept.RezeptBezeichnung
        'In der zweiten Zeile stehen die Spalten-Überschriften
        xlsArray(0, colRohNumr) = "Nummer"
        xlsArray(0, colRohName) = "Rohstoff"
        xlsArray(0, colRohDekl) = "Deklarations-Bezeichnung"
        xlsArray(0, colRohGrpe) = "Gruppe"
        xlsArray(0, colSollwrt) = "Sollwert"

        'Spaltenüberschriften Nährwerte und Allergene
        For j = colStrtNwt To nwtArray.Count + colSollwrt
            xlsArray(0, j) = wb_KomponParam301_Global.kt301Param(nwtArray(j - colStrtNwt)).Bezeichnung
        Next

        'String- und Double-Array mit Daten aus Rezeptschritten füllen
        Dim i As Integer = 0
        For Each RzSchritt As wb_Rezeptschritt In Steps
            'nur Sollwerte und Produktions-Stufen
            If wb_Functions.TypeIstSollMenge(RzSchritt.Type, RzSchritt.ParamNr) OrElse (RzSchritt.Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE) Then
                i += 1
                'Rohstoff-Nummer
                xlsArray(i, colRohNumr) = RzSchritt.Nummer
                'Rohstoff-Bezeichnung
                xlsArray(i, colRohName) = RzSchritt.VirtTreeBezeichnung
                'Rohstoff-Gruppe
                xlsArray(i, colRohGrpe) = RzSchritt.RohstoffGruppe
                'Sollwert im Rezept (als Double)
                xldArray(i - 1, 0) = RzSchritt.fSollwert

                'Produktions-Stufen haben keine Deklaration
                If RzSchritt.Type <> wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Then
                    'Rohstoff-Deklaration
                    xlsArray(i, colRohDekl) = RzSchritt.ZutatenListe.Zutaten
                Else
                    'Produktions-Stufe ohne Rohstoff-Nummer
                    xlsArray(i, colRohNumr) = ""
                    'Deklarationstext kennzeichnet die Produktions-Stufe
                    xlsArray(i, colRohDekl) = "-"
                    'Produktions-Stufe ohne Gruppe
                    xlsArray(i, colRohGrpe) = ""
                End If

                'Nährwerte und Allergene
                For j = 0 To nwtArray.Count - 1
                    If wb_KomponParam301_Global.IsAllergen(nwtArray(j)) Then
                        xlsArray(i, j + colStrtNwt) = wb_Functions.AllergenToString(RzSchritt.ktTyp301.Wert(nwtArray(j)), True)
                    Else
                        xldArray(i - 1, j + 1) = RzSchritt.ktTyp301.Wert(nwtArray(j))
                    End If
                Next

                'Rezept-im-Rezept
                If RzSchritt.RezeptNr > 0 AndAlso DetailAnsicht Then
                    If RzSchritt.RezeptImRezept.RezeptBezeichnung IsNot Nothing Then
                        'neues TabSheet erzeugen (an Stelle 1!)
                        xlWorkSheets.Add()
                        ExcelNwtPage(RzSchritt.RezeptImRezept, xlWorkSheets, DetailAnsicht)
                    End If
                End If
            End If
        Next

        'zuerst die Strings ausgeben
        xslRange = xslRange.Resize(i + 1, nwtArray.Count + colStrtNwt)
        xslRange.Value = xlsArray
        'anschliessend die Sollwerte (Numerisch) eintragen
        xdlRange = xdlRange.Resize(Math.Max(i, 1), colEndNwt)
        xdlRange.Value = xldArray

        'Überschrift (Rezeptname) in einer Zelle zusammenfassen
        xlRange = xlWorkSheet.Range("A1", "AD1")
        xlRange.Merge()
        xlRange.HorizontalAlignment = Excel.Constants.xlCenter
        xlRange.Interior.ColorIndex = 6 'Yellow
        xlRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

        'Spaltenbreite Rohstoff-Nummer und Bezeichnung
        xlWorkSheet.Columns("A:B").EntireColumn.AutoFit()

        'Ausrichtung Sollwerte und Deklaration
        xlWorkSheet.Columns.EntireColumn.VerticalAlignment = Excel.XlVAlign.xlVAlignTop

        'Spaltenüberschriften
        xlWorkSheet.Range("E2:AD2").Orientation = 90
        xlWorkSheet.Range("E2:AD2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        xlWorkSheet.Range("E2:AD2").VerticalAlignment = Excel.XlVAlign.xlVAlignBottom
        xlWorkSheet.Range("A2:D2").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
        xlWorkSheet.Range("A1:AD2").Font.Bold = True

        'Spalte Deklaration
        xlWorkSheet.Columns("C:C").EntireColumn.ColumnWidth = 40.0
        xlWorkSheet.Columns("D:D").EntireColumn.ColumnWidth = 20.0
        xlWorkSheet.Columns("C:D").EntireColumn.WrapText = True

        'Format Decimalzahl für die Sollwerte
        xlWorkSheet.Columns("E:E").EntireColumn.NumberFormat = "[Black][>0]0.00" & Chr(34) & " kg" & Chr(34) & ";[White][<=0]0.00"
        'Format Decimalzahl für die Nährwerte
        xlWorkSheet.Columns("F:P").EntireColumn.NumberFormat = "[Black][>0]0.00;[White][<=0]0.00"

        'Spaltenbreite Allergene
        xlWorkSheet.Columns("F:AD").EntireColumn.AutoFit()
        xlWorkSheet.Columns("P:AD").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

        'Produktions-Stufen formatieren
        For j = 0 To i
            If xlsArray(j, 2) = "-" Then
                xlWorkSheet.Rows.Item(j + 2).Font.Bold = True
                xlWorkSheet.Rows.Item(j + 2).Font.Italic = True
            End If
        Next
    End Sub

    Private Sub ExcelZutatenPage(Rezept As wb_Rezept, xlWorkSheets As Excel.Sheets)

        'Nächstes(neues) Arbeitsblatt
        Dim xlWorkSheet As Excel.Worksheet
        xlWorkSheet = xlWorkSheets(1)
        xlWorkSheet.Cells.Clear()

        'Sheet Name
        xlWorkSheet.Name = "Zutatenliste"

        'Array Strings
        Dim xslRange As Excel.Range
        'Array Double
        Dim xdlRange As Excel.Range
        'Array Überschrift
        Dim xlRange As Excel.Range

        'Get the range where the starting cell has the address
        xslRange = xlWorkSheet.Range("A3", Reflection.Missing.Value)
        xdlRange = xlWorkSheet.Range("B3", Reflection.Missing.Value)

        'Zutatenliste als flache(optimierte) Liste
        Dim xlZutaten As wb_ZutatenListe = Rezept.Zutaten

        'Array für die Daten (Zeilen,Spalten)
        Dim xlsArray(xlZutaten.Liste.Count + 1, 3) As String
        Dim xldArray(xlZutaten.Liste.Count + 1, 1) As Double
        'In der ersten Zeile steht die Rezept-Bezeichnung
        xlWorkSheet.Range("A1").Value = Rezept.RezeptBezeichnung
        'In der zweiten Zeile stehen die Spalten-Überschriften
        xlWorkSheet.Range("A2").Value = "Bezeichnung"
        xlWorkSheet.Range("B2").Value = "Anteil"
        xlWorkSheet.Range("C2").Value = "Rohstoff(e)"

        'String- und Double-Array mit Daten aus Rezeptschritten füllen
        Dim i As Integer = 0
        For Each z As wb_ZutatenElement In xlZutaten.Liste
            'Zutaten-Bezeichnung
            xlsArray(i, 0) = z.Zutaten
            'Zutaten-Menge
            xldArray(i, 0) = z.SollMenge
            'Zutaten-Rohstoff(e)
            xlsArray(i, 2) = z.Rohstoffe
            'Index
            i += 1
        Next

        'zuerst die Strings ausgeben
        xslRange = xslRange.Resize(i, 3)
        xslRange.Value = xlsArray
        'anschliessend die Sollwerte (Numerisch) eintragen
        xdlRange = xdlRange.Resize(i, 1)
        xdlRange.Value = xldArray

        'Überschrift (Rezeptname) in einer Zelle zusammenfassen
        xlRange = xlWorkSheet.Range("A1", "C1")
        xlRange.Merge()
        xlRange.HorizontalAlignment = Excel.Constants.xlCenter
        xlRange.Interior.ColorIndex = 6 'Yellow
        xlRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

        'Spaltenbreite Bezeichnung
        xlWorkSheet.Columns("A:C").EntireColumn.AutoFit()
        'Spalten-Überschriften
        xlWorkSheet.Range("A1:C2").Font.Bold = True

        'Format Decimalzahl für die Sollwerte
        xlWorkSheet.Columns("B:B").EntireColumn.NumberFormat = "[Black][>0]0.0000" & Chr(34) & " kg" & Chr(34) & ";[White][<=0]0.0000"
        xlWorkSheet.Columns("B:B").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
    End Sub

End Class