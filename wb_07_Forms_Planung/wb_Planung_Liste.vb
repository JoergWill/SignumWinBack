Imports System.Windows.Forms
Imports combit.ListLabel22.DataProviders
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_Liste
    Inherits DockContent
    Dim Produktion As New wb_Produktion
    Dim oFont As Drawing.Font
    Dim iFont As Drawing.Font

    Private _FilterLinienGruppe As Integer = wb_Global.UNDEFINED
    Private _FilterAufarbeitung As Integer = wb_Global.UNDEFINED
    Private _ProdPlanGedruckt As Boolean = False

    'Bestellungen einlesen für Produktions-Datum
    Private _ProduktionsDatum As String = ""
    Private _ProduktionsFilialeNummer As String = ""

    Private Sub BtnVorlage_Click(sender As Object, e As EventArgs) Handles BtnVorlage.Click
        'Fenster Auswahl Vorlage anzeigen
        Dim Vorlage As New wb_Planung_Vorlage
        Vorlage.ShowDialog(Me)
        Me.Cursor = Cursors.WaitCursor

        If Vorlage.TWNr > 0 Then
            'Daten aus wbDaten einlesen
            If Not Produktion.MySQLdbSelect_ArbRzSchritte(Vorlage.TWNr) Then
                'Default-Cursor
                Me.Cursor = Cursors.Default
                'keine Datensätze in der Vorlage
                MsgBox("Keine Datensätze in dieser Vorlage", MsgBoxStyle.Exclamation, "Laden Produktionsdaten aus Vorlage")
                VirtualTree.Invalidate()
            Else
                'Virtual Tree anzeigen
                VirtualTree.DataSource = Produktion.RootProduktionsSchritt
            End If
        End If
        'Default-Cursor
        Me.Cursor = Cursors.Default

    End Sub

    ''' <summary>
    ''' Abruf-Datum der Bestell-Liste von OrgaBack. Voreingestellt ist immer das aktuelle Datum plus x Tage
    ''' Auswahl der Filiale aus Drop-Down-Liste. Die Liste wird aus den OrgaBack-Filialen mit Filialtyp Produktion erzeugt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Planung_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste mit Produktions-Filialen
        cbProduktionsFiliale.Fill(wb_Filiale.ProduktionsFilialen)
        'ComboBox Liniengruppe Rezepte(Teig) füllen
        cbLiniengruppe.Fill(wb_Linien_Global.RezeptLinienGruppen, True)
        'ComboBox Liniengruppe Artikel füllen
        cbArtikelLinienGruppe.Fill(wb_Linien_Global.ArtikelLinienGruppen, True)

        'Font für die Anzeige Artikelzeile im VirtualTree
        oFont = VirtualTree.Font
        iFont = New Drawing.Font(oFont.Name, oFont.Size, Drawing.FontStyle.Italic)

        'OrgaBack aktiv
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            'Einlesen der Bestellungen nur wenn auch OrgaBack aktiv ist
            BtnBestellungen.Enabled = True
            'erste Filiale auswählen
            cbProduktionsFiliale.SelectedIndex = wb_Filiale.IdxProduktionsFiliale(wb_GlobalSettings.ProdPlanfiliale)
            'Produktions-Datum
            dtBestellungen.Value = wb_GlobalSettings.ProdPlanDatum
            'Bestellungen automatisch einlesen
            If wb_GlobalSettings.ProdPlanReadOnOpen Then
                ReadBestellungenOrgaBack()
            End If
        Else
            'Einlesen der Bestellungen nur wenn auch OrgaBack aktiv ist
            BtnBestellungen.Enabled = False
            'erste Filiale auswählen
            cbProduktionsFiliale.SelectedIndex = 0
            'Datum heute plus x Tage
            dtBestellungen.Value = DateTime.Today.AddDays(wb_GlobalSettings.osProdTageVoraus)
        End If
    End Sub

    ''' <summary>
    ''' Form Planung_Main soll geschlossen werden. 
    ''' Vorher muss geprüft werden, ob die Produktions-Daten in dbo.XXX.MengeInProduktion geschrieben werden sollen.
    ''' 
    ''' Gibt False zurück, wenn das Fenster geschlossen werden kann.
    ''' </summary>
    ''' <returns></returns>
    Public Function FormClosingFromMain() As Boolean
        'wenn Daten für die Produktion generiert worden sind (Teigliste/Backzettel/Produktionsliste)
        If _ProdPlanGedruckt Then
            'Nachfragen
            Select Case MsgBox("Sollen die Daten aus der Planung als 'in Produktion' verbucht werden ?", MsgBoxStyle.Question & MsgBoxStyle.YesNoCancel, "Produktions-Planung")

                Case MsgBoxResult.Yes
                    'Datensätze in dbo.XXX.MengeInProduktion eintragen
                    WriteProduktionOrgaBack()
                    Return False

                Case MsgBoxResult.No
                    'keine Einträge in dbo.XXX
                    Return False

                Case Else
                    'Fenster nicht schliessen
                    Return True

            End Select
        End If
        Return False
    End Function

    Private Sub BtnBestellungen_Click(sender As Object, e As EventArgs) Handles BtnBestellungen.Click
        ReadBestellungenOrgaBack()
    End Sub

    Private Sub ReadBestellungenOrgaBack()
        'Cursor umschalten
        Me.Cursor = Cursors.WaitCursor

        'Bestellungen einlesen für Produktions-Datum
        _ProduktionsDatum = dtBestellungen.Value.ToString("yyyyMMdd")
        _ProduktionsFilialeNummer = cbProduktionsFiliale.GetKeyFromSelection()

        'Prüfen ob schon Daten vorhanden sind
        If Produktion.RootProduktionsSchritt.ChildSteps.Count > 0 Then
            If MsgBox("Die Liste enthält schon Produktions-Daten!" & vbCrLf & "Sollen diese vorher gelöscht werden?", MsgBoxStyle.YesNo, "Laden Produktionsdaten aus Bestellung") = vbYes Then
                'alle Einträge löschen
                Produktion.RootProduktionsSchritt.ChildSteps.Clear()
                VirtualTree.Invalidate()
                'Tree neu zeichnen(leer)
                VirtualTree.DataSource = Produktion.RootProduktionsSchritt
            End If
        End If

        'Daten aus der Stored-Procedure in OrgaBack einlesen
        If Not Produktion.MsSQLdbProcedure_Produktionsauftrag(_ProduktionsDatum, _ProduktionsFilialeNummer) Then
            'Default-Cursor
            Me.Cursor = Cursors.Default
            'keine Datensätze in der Vorlage
            MsgBox("Keine Datensätze in der Bestell-Liste", MsgBoxStyle.Exclamation, "Laden Produktionsdaten aus Bestellung")
            VirtualTree.Invalidate()
        Else
            'Virtual Tree anzeigen
            VirtualTree.DataSource = Produktion.RootProduktionsSchritt
        End If
        'Default-Cursor
        Me.Cursor = Cursors.Default

    End Sub

    ''' <summary>
    ''' Übermittelt die Stückzahlen der Artikel, die in die Produktion übertragen worden sind (Teigzettel/Backzettel/cvs-File) als MengeInProduktion an OrgaBack
    ''' </summary>
    Private Sub WriteProduktionOrgaBack()
        'Alle Produktions-Schritte in der Liste durchlaufen
        For Each p As wb_Produktionsschritt In Produktion.RootProduktionsSchritt.ChildSteps
            'wenn dieser Produktions-Schritt gedruckt/übertragen wurde
            If p.IstInProduktion Then
                'Datensatz in ProduktionAktuell updaten/schreiben
                MsSQLdbUpdate_ProduktionAktuell(_ProduktionsFilialeNummer, _ProduktionsDatum, p.iTour, p.ArtikelNummer, wb_Global.obEinheitStk, wb_Global.obDEFAULTCOLOR, wb_Global.obDEFAULTSIZE, p.Sollmenge_Stk, p.MengeInProduktion)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Schreibt die Stückzahlen "MengeInProduktion" in die Tabelle dbo.ProduktionAktuell.
    ''' Wenn schon ein Datensatz vorhanden ist, wird dieser überschrieben.
    ''' 
    ''' Der Wert MengeInProduktion wird aus dem vorhierigen Wert MengeInProduktion aus der Abfrage dbo.pq_Produktionsauftrag mit der Sollstückzahl addiert.
    ''' </summary>
    ''' <param name="FilialeNr"></param>
    ''' <param name="LieferDatum"></param>
    ''' <param name="TourNr"></param>
    ''' <param name="ArtikelNr"></param>
    ''' <param name="Einheit"></param>
    ''' <param name="Farbe"></param>
    ''' <param name="Groesse"></param>
    ''' <param name="MengeInProduktion"></param>
    Private Sub MsSQLdbUpdate_ProduktionAktuell(FilialeNr As Integer, LieferDatum As String, TourNr As Integer, ArtikelNr As String, Einheit As Integer, Farbe As Integer, Groesse As String, Sollmenge As Double, MengeInProduktion As Double)
        'Datenbank-Verbindung öffnen - MsSQL
        Dim OrgasoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        Dim sMengeInProduktion As String = (Sollmenge + MengeInProduktion).ToString("#.###")

        'Prüfen ob ein Eintrag für diesen Artikel/Produktionsdatum/Filiale in OrgaBack.dbo.ProduktionAktuell existiert
        If OrgasoftMain.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlProduktionAktuell, FilialeNr, LieferDatum, TourNr, ArtikelNr)) Then
            Trace.WriteLine("@I_Write MengeInProduktion OrgaBack " & ArtikelNr & "/" & Sollmenge & "/" & MengeInProduktion)

            If Not OrgasoftMain.Read Then
                'Datensatz noch nicht vorhanden in OrgaBack
                Debug.Print("Tour/Lieferdatum/Artikel " & TourNr & "/" & LieferDatum & "/" & ArtikelNr & "/" & " nicht in OrgaBack gefunden")
                'Lesen beendet
                OrgasoftMain.CloseRead()
                'Datensatz neu anlegen (INSERT)
                Dim sqlInsert As String = FilialeNr.ToString & ",'" & LieferDatum & "'," & TourNr.ToString & ",'" & ArtikelNr & "'," & Einheit.ToString & "," &
                                          Farbe.ToString & ",'" & Groesse & "'," & sMengeInProduktion
                OrgasoftMain.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertProduktionAktuell, sqlInsert))

            Else
                'Datensatz schon vorhanden in OrgaBack
                Debug.Print("Tour/Lieferdatum/Artikel " & TourNr & "/" & LieferDatum & "/" & ArtikelNr & "/" & " schon vorhanden")
                'Lesen beendet
                OrgasoftMain.CloseRead()
                'Datensatz ändern (UPDATE)
                OrgasoftMain.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlUpdateProduktionAktuell, FilialeNr, LieferDatum, TourNr, ArtikelNr, sMengeInProduktion))
            End If
        Else
            Trace.WriteLine("@E_SQL-Fehler Write MengeInProduktion OrgaBack " & ArtikelNr & "/" & Sollmenge & "/" & MengeInProduktion)
        End If

        'Verbindung zur Datenbank wieder schliessen
        OrgasoftMain.Close()
    End Sub

    ''' <summary>
    ''' Neue Artikel-Zeile (mit Rezeptur anlegen)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnNeueCharge_Click(sender As Object, e As EventArgs) Handles btnNeueCharge.Click
        'neue Charge anlegen
        Dim NeueCharge As New wb_Planung_Neu(Produktion)
        NeueCharge.ShowDialog()

        'Virtual Tree anzeigen
        VirtualTree.DataSource = Produktion.RootProduktionsSchritt
    End Sub

    ''' <summary>
    ''' Vorproduktion berechnen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtVorproduktion_Click(sender As Object, e As EventArgs) Handles BtVorproduktion.Click
        Produktion.CalcVorproduktion(Produktion.RootProduktionsSchritt)
        'Virtual Tree anzeigen
        VirtualTree.DataSource = Produktion.RootProduktionsSchritt
    End Sub

    ''' <summary>
    ''' Filtert die Produktions-Planungs-Schritte nach Aufarbeitung und Linengruppe
    ''' </summary>
    ''' <param name="a"></param>
    Private Sub FilterAndMark(ByRef a As ArrayList, CheckAufloesen As Boolean)
        'ArrayList leeren
        a.Clear()
        'Alle Produktions-Schritte durchlaufen
        For Each child In Produktion.RootProduktionsSchritt.ChildSteps
            'Filtern nach Aufarbeitungsplatz und Liniengruppe
            If TryCast(child, wb_Produktionsschritt).Filter(_FilterAufarbeitung, _FilterLinienGruppe, CheckAufloesen) Then
                'Liste aufbauen
                a.Add(child)
                'Charge als produziert markieren
                TryCast(child, wb_Produktionsschritt).ChargeWirdProduziert()
            End If
        Next

        'Marker setzen Daten wurden an Produktion übertragen setzen
        _ProdPlanGedruckt = True

    End Sub

    ''' <summary>
    ''' Backzettel drucken
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnBackZettelDrucken_Click(sender As Object, e As EventArgs) Handles BtnBackZettelDrucken.Click
        'Sortieren nach Teig(RezeptNummer), ArtikelNummer und Tour
        Produktion.RootProduktionsSchritt.SortBackZettel()

        'Daten filtern (Aufbereitungs-Ort)
        Dim BackZettel As New ArrayList
        FilterAndMark(BackZettel, False)

        'Druck-Daten
        Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog
        pDialog.LL_KopfZeile_1 = "für " & dtBestellungen.Value.ToString("dddd") & ", den " & dtBestellungen.Value.ToString("dd.MM.yyyy")
        pDialog.LL.DataSource = New ObjectDataProvider(BackZettel)

        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Produktion"
        pDialog.ListFileName = "BackZettel.lst"
        pDialog.ShowDialog()
        pDialog = Nothing
    End Sub

    Private Sub BtnTeigListeDrucken_Click(sender As Object, e As EventArgs) Handles BtnTeigListeDrucken.Click
        'Sortieren nach Teig(RezeptNummer), ArtikelNummer und Tour
        Produktion.RootProduktionsSchritt.SortBackZettel()

        'Daten filtern (Aufbereitungs-Ort)
        Dim TeigListe As New ArrayList
        FilterAndMark(TeigListe, True)

        'Druck-Daten
        Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog
        pDialog.LL_KopfZeile_1 = "für " & dtBestellungen.Value.ToString("dddd") & ", den " & dtBestellungen.Value.ToString("dd.MM.yyyy")
        pDialog.LL.DataSource = New ObjectDataProvider(TeigListe)

        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Produktion"
        pDialog.ListFileName = "TeigListe.lst"
        pDialog.ShowDialog()
        pDialog = Nothing
    End Sub

    Private Sub BtnTeigListeExport_Click(sender As Object, e As EventArgs) Handles BtnTeigListeExport.Click
        'Sortieren nach Teig(RezeptNummer), Ergebnis ChargenAufteilung, Tour und ArtikelNummer
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        Produktion.RootProduktionsSchritt.SortProduktionsPlan()
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        'gleiche (Rest-)Teige zusammenfassen
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        'Produktion.TeigeZusammenfassen(wb_GlobalSettings.TeigOptimierung)
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        'Neu erstellte Chargen anzeigen 
        VirtualTree.DataSource = Produktion.RootProduktionsSchritt

        'Produktions-Daten nach WinBack exportieren
        ProdDatenExport()
    End Sub

    Private Sub DebugPrint(s As String)
        For Each a As wb_Produktionsschritt In Produktion.RootProduktionsSchritt.ChildSteps
            Debug.Print(s & " Tour/Artikel/Rezept/Sollmenge/TeilerResult " & a.Tour & "/" & a.ArtikelNummer & "-" & a.ArtikelBezeichnung & "/" & a.RezeptNummer & "-" & a.RezeptBezeichnung & "/" & a.Sollmenge_Stk & " Stk -" & a.Sollwert_kg & " kg /" & a.TeigChargenTeilerResult)
        Next
    End Sub

    Private Sub ProdDatenExport()
        'Export-File erzeugen
        Dim T1001 As New IO.FileInfo(wb_GlobalSettings.GetFileName("T1001"))

        'wenn das Verzeichnis nicht existiert wird es erzeugt
        If Not IO.Directory.Exists(T1001.DirectoryName) Then
            T1001.Directory.Create()
        End If

        'wenn die Datei schon exisitiert, wird sie vorher gelöscht
        If T1001.Exists Then
            T1001.Delete()
        End If

        'Datei neu anlegen
        Using fs As IO.FileStream = T1001.Open(IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
            Using sw As New IO.StreamWriter(fs)

                'Kopfzeilen schreiben
                sw.WriteLine(ProdDatenKopfZeile_1)
                sw.WriteLine(ProdDatenKopfZeile_2)

                'Daten filtern (Aufbereitungs-Ort)
                Dim ProduktionsListe As New ArrayList
                FilterAndMark(ProduktionsListe, False)

                'Artikelzeilen
                For Each a As wb_Produktionsschritt In ProduktionsListe
                    'ChargenZeilen
                    For Each r As wb_Produktionsschritt In a.ChildSteps
                        If Not r.Optimiert Then
                            sw.WriteLine(ProdDatenSatz(r))
                        End If
                    Next
                Next

                'TODO Aufarbeitungs-Plätze auskommentieren und prodimport anpassen/prüfen
                'Sonst werden doppelte Arbeitsaufträge erzeugt !!
                ''Artikelzeilen Aufarbeitung (Artikel mit LiniengruppeArtikel > 100)
                'For Each a As wb_Produktionsschritt In ProduktionsListe
                '    'ChargenZeilen
                '    For Each r As wb_Produktionsschritt In a.ChildSteps
                '        If Not a.Optimiert And a.ArtikelLinienGruppe <> wb_Global.UNDEFINED Then
                '            sw.WriteLine(ProdDatenSatz(r, True))
                '        End If
                '    Next
                'Next

                'Daten schreiben (Puffer leeren)
                sw.Flush()
            End Using
        End Using

        'per FTP zu WinBack übertragen
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        Dim Result As String = wb_Functions.FTP_Upload_File(T1001.FullName, wb_Global.WinBackServerProdDirectory)
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default

        'Ergebnis der Datenübertragung anzeigen
        If Result IsNot Nothing Then
            MessageBox.Show("Fehler bei der Datenübertragung zum WinBack-Server" & vbCrLf & Result, "Übertragen der Produktionsdaten zu WinBack", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            MessageBox.Show("Alle Produktionsdaten übertragen" & vbCrLf & Result, "Übertragen der Produktionsdaten zu WinBack", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Function ProdDatenKopfZeile_1() As String
        Dim ExpStr As String = ""
        Dim SepStr As String = ","

        For i = 1 To 48
            Select Case i
                Case 5      ' [5] Bestellnummer
                    ExpStr = ExpStr + "Bestellnummer" + SepStr
                Case 14     ' [14] Anzahl der Produktions-Chargen (immer 1)
                    ExpStr = ExpStr + "MengeOptChargen" + SepStr
                Case 20     ' [20] Herstellungsdatum
                    ExpStr = ExpStr + "Herstellungsdatum" + SepStr
                Case 22     ' [22] Artikelnummer
                    ExpStr = ExpStr + "Artikelnummer" + SepStr
                Case 24     ' [24] Variante
                    ExpStr = ExpStr + "Variante" + SepStr
                Case 28     ' [28] Rezeptnummer (nur Info)
                    ExpStr = ExpStr + "Rezepturnummer" + SepStr
                Case 40     ' [40] Produktionsblock (Artikel-Linien-Nummer !!!)
                    ExpStr = ExpStr + "Produktionsblock" + SepStr
                Case 41     ' [41] Sollchargengröße in %
                    ExpStr = ExpStr + "ChargeRestProzent" + SepStr
                Case 42     ' [42] Sollanzahl Optimalchargen
                    ExpStr = ExpStr + "AnzOptChargen" + SepStr
                Case 43     ' [43] Sollanzahl Restchargen
                    ExpStr = ExpStr + "AnzRestChargen" + SepStr
                Case 44     ' [44] Produktionsmenge in [kg]
                    ExpStr = ExpStr + "Sollmenge" + SepStr
                Case 45     ' [45] Sollmenge Restchargen
                    ExpStr = ExpStr + "MengeRestChargen" + SepStr
                Case 46     ' [46] Gesamtmenge Auftrag
                    ExpStr = ExpStr + "GesMengeAuftrag" + SepStr
                Case 47     ' [47] Sollzeit
                    ExpStr = ExpStr + "SollZeit" + SepStr
                Case 48     ' [48] Produktionsfolge
                    ExpStr = ExpStr + "ProdFolge" + SepStr
                Case Else
                    ExpStr = ExpStr + SepStr
            End Select
        Next
        Return ExpStr
    End Function

    Private Function ProdDatenKopfZeile_2() As String
        Dim ExpStr As String = ""
        Dim SepStr As String = ","

        For i = 1 To 48
            Select Case i
                Case 5  ' [5] Bestellnummer
                    ExpStr = ExpStr + "ARZ_Best_Nr" + SepStr
                Case 14  ' [14] Anzahl der Produktions-Chargen (immer 1)
                    ExpStr = ExpStr + "ARZ_Sollmenge" + SepStr
                Case 20  ' [20] Herstellungsdatum
                    ExpStr = ExpStr + "ARZ_Zp_Gestartet" + SepStr
                Case 22  ' [22] Artikelnummer
                    ExpStr = ExpStr + "ARZ_KA_NrAlNum" + SepStr
                Case 24  ' [24] Variante
                    ExpStr = ExpStr + "ARZ_RZ_Typ" + SepStr
                Case 28 ' [28] Rezeptnummer (nur Info)
                    ExpStr = ExpStr + "ARZ_RZ_Nr_AlNum" + SepStr
                Case 40 ' [40] Produktionsblock (Artikel-Linien-Nummer !!!)
                    ExpStr = ExpStr + "ARZ_LiBeh_Nr" + SepStr
                Case 44  ' [44] Produktionsmenge in [kg]
                    ExpStr = ExpStr + "ARZ_Sollmenge_kg" + SepStr
                Case Else
                    ExpStr = ExpStr + SepStr
            End Select
        Next
        Return ExpStr
    End Function

    Private Function ProdDatenSatz(x As wb_Produktionsschritt, Optional bArtikelLiniengruppe As Boolean = False) As String
        Dim ExpStr As String = ""
        Dim SepStr As String = ","

        For i = 1 To 48
            Select Case i
                Case 5  ' [5] Bestellnummer
                    ExpStr = ExpStr + x.AuftragsNummer + SepStr
                Case 14  ' [14] Anzahl der Produktions-Chargen (immer 1)
                    ExpStr = ExpStr + "1" + SepStr
                Case 20  ' [20] Herstellungsdatum
                    ExpStr = ExpStr + DateTime.Now.ToString("yyyyMMdd") + SepStr
                Case 22  ' [22] Artikelnummer (entfällt bei Rezept-Chargen)
                    ExpStr = ExpStr + x.ArtikelNummer + SepStr
                Case 24  ' [24] Variante
                    ExpStr = ExpStr + x.RezeptVar.ToString + SepStr
                Case 28 ' [28] Rezeptnummer (nur Info)
                    ExpStr = ExpStr + x.RezeptNummer + SepStr
                Case 40 ' [40] Artikel-Liniengruppe
                    If bArtikelLiniengruppe Then
                        ExpStr = ExpStr + (x.ArtikelLinienGruppe).ToString + SepStr
                    Else
                        ExpStr = ExpStr + SepStr
                    End If
                Case 44  ' [44] Produktionsmenge in [kg]
                    ExpStr = ExpStr + wb_Functions.DoubleToXString(x.Sollwert_kg) + SepStr
                Case Else
                    ExpStr = ExpStr + SepStr
            End Select
        Next
        Return ExpStr

    End Function

    ''' <summary>
    ''' Filtert die Anzeige der Chargen nach Liniengruppe/Aufarbeitungsplatz.
    ''' Die Methode GetChildren gibt eine Liste aller Child-Knoten zurück, die angezeigt werden sollen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_GetChildren(sender As Object, e As Infralution.Controls.VirtualTree.GetChildrenEventArgs) Handles VirtualTree.GetChildren
        '<see cref = "http://www.infralution.com/phpBB2/viewtopic.php?t=1499&highlight=filter+rows" />
        'use the default binding to get all the children
        Dim binding As Infralution.Controls.VirtualTree.RowBinding = VirtualTree.GetRowBinding(e.Row)
        Dim children As IList = binding.GetChildrenForRow(e.Row)

        'return a list containing only the children you want to be visible
        Dim visibleChildren As New ArrayList
        For Each child In children
            If TryCast(child, wb_Produktionsschritt).Filter(_FilterAufarbeitung, _FilterLinienGruppe, False) Then
                visibleChildren.Add(child)
            End If
        Next

        e.Children = visibleChildren
        'e.Children = children
    End Sub

    ''' <summary>
    ''' Anzeige-Filter Liniengruppe.
    ''' Die entsprechende Liniengruppen-Nummer wird in wb_LinienGlobal aus dem Bezeichnungstext ermittelt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbLiniengruppe_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLiniengruppe.SelectedIndexChanged
        If cbLiniengruppe.Text <> wb_Global.TextAlle Then
            _FilterLinienGruppe = wb_Linien_Global.GetLinienGruppeFromName(cbLiniengruppe.Text)
        Else
            _FilterLinienGruppe = wb_Global.UNDEFINED
        End If
        VirtualTreeRepaint()
    End Sub

    ''' <summary>
    ''' Anzeige-Filter Aufarbeitungsplatz.
    ''' Die entsprechende Aufarbeitungsplatzgruppen-Nummer wird in wb_LinienGlobal aus dem Bezeichnungstext ermittelt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbArtikelLinienGruppe_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbArtikelLinienGruppe.SelectedIndexChanged
        If cbArtikelLinienGruppe.Text <> wb_Global.TextAlle Then
            _FilterAufarbeitung = wb_Linien_Global.GetLinienGruppeFromName(cbArtikelLinienGruppe.Text)
        Else
            _FilterAufarbeitung = wb_Global.UNDEFINED
        End If
        VirtualTreeRepaint()
    End Sub

    Private Sub VirtualTreeRepaint()
        'Tree neu zeichnen(leer)
        VirtualTree.Invalidate()
        VirtualTree.DataSource = Produktion.RootProduktionsSchritt
    End Sub

    'Private Sub VirtualTree_GetRowData(sender As Object, e As Infralution.Controls.VirtualTree.GetRowDataEventArgs) Handles VirtualTree.GetRowData
    '    'e.Row.Expanded
    '    'e.RowData.Icon = Icon
    'End Sub

    'Private Sub VirtualTree_GetCellData(sender As Object, e As Infralution.Controls.VirtualTree.GetCellDataEventArgs) Handles VirtualTree.GetCellData
    '    Static bfont As Boolean
    '    'Get the binding for the given row and get the cell data
    '    Dim RowBinding As Infralution.Controls.VirtualTree.RowBinding = VirtualTree.GetRowBinding(e.Row)
    '    RowBinding.GetCellData(e.Row, e.Column, e.CellData)


    '    If e.Column.Name = "ColState" Then
    '        If e.CellData.Value = wb_Global.KomponTypen.KO_ZEILE_ARTIKEL Then
    '            bFont = True
    '        Else
    '            bfont = False
    '        End If
    '    End If
    '    If bfont Then
    '        e.Column.CellStyle.Font = iFont
    '    Else
    '        e.Column.CellStyle.Font = oFont
    '    End If

    'End Sub
End Class
