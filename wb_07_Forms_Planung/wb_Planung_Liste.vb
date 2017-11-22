Imports System.Windows.Forms
Imports combit.ListLabel22.DataProviders
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_Liste
    Inherits DockContent
    Dim Produktion As New wb_Produktion

    Private Sub wb_Planung_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Datum heute plus einen Tag
        dtBestellungen.Value = DateTime.Today.AddDays(1) 'TODO Tage im vorraus in WinBack-ini festhalten
    End Sub

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

    Private Sub BtnBestellungen_Click(sender As Object, e As EventArgs) Handles BtnBestellungen.Click

        Dim ProduktionsDatum As String = dtBestellungen.Value.ToString("yyyyMMdd")

        'Daten aus der Stored-Procedure in OrgaBack einlesen
        Me.Cursor = Cursors.WaitCursor
        If Not Produktion.MsSQLdbProcedure_Produktionsauftrag(ProduktionsDatum, "2") Then
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
    ''' Neue Artikel-Zeile (mit Rezeptur anlegen)
    ''' TEST Artikel-Nummer 12
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnNeueCharge_Click(sender As Object, e As EventArgs) Handles btnNeueCharge.Click
        'TEST
        Produktion.AddArtikelCharge("1", "11102", 0, 240, wb_Global.ModusChargenTeiler.OptimalUndRest, True)
        Produktion.AddArtikelCharge("2", "11102", 0, 4, wb_Global.ModusChargenTeiler.OptimalUndRest, True)
        Produktion.AddArtikelCharge("3", "11102", 0, 4, wb_Global.ModusChargenTeiler.OptimalUndRest, True)
        Produktion.AddArtikelCharge("4", "11102", 0, 2, wb_Global.ModusChargenTeiler.OptimalUndRest, True)
        Produktion.AddArtikelCharge("1", "11103", 0, 4, wb_Global.ModusChargenTeiler.OptimalUndRest, True, "", 4)
        Produktion.AddArtikelCharge("1", "11101", 0, 34, wb_Global.ModusChargenTeiler.OptimalUndRest, True, "", 35, "Filiale Seestrasse 5 Stk geschnitten anliefern")
        Produktion.AddArtikelCharge("2", "11101", 0, 6, wb_Global.ModusChargenTeiler.OptimalUndRest, True)
        Produktion.AddArtikelCharge("3", "11101", 0, 6, wb_Global.ModusChargenTeiler.OptimalUndRest, True)
        Produktion.AddArtikelCharge("4", "11101", 0, 2, wb_Global.ModusChargenTeiler.OptimalUndRest, True)
        'Produktion.AddArtikelCharge("2", "", 7035, 500, wb_Global.ModusChargenTeiler.OptimalUndRest)
        'Virtual Tree anzeigen
        VirtualTree.DataSource = Produktion.RootProduktionsSchritt
    End Sub

    Private Sub BtVorproduktion_Click(sender As Object, e As EventArgs) Handles BtVorproduktion.Click
        Produktion.CalcVorproduktion(Produktion.RootProduktionsSchritt)
    End Sub

    Private Sub BtnBackZettelDrucken_Click(sender As Object, e As EventArgs) Handles BtnBackZettelDrucken.Click
        'Sortieren nach Teig(RezeptNummer), ArtikelNummer und Tour
        Produktion.RootProduktionsSchritt.SortBackZettel()

        Dim pDialog As New wb_PrinterDialog 'Drucker-Dialog

        'Druck-Daten
        pDialog.LL.DataSource = New ObjectDataProvider(Produktion.RootProduktionsSchritt.ChildSteps)
        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Produktion"
        pDialog.ListFileName = "BackZettel.lst"
        pDialog.ShowDialog()
    End Sub

    Private Sub BtnTeigListeDrucken_Click(sender As Object, e As EventArgs) Handles BtnTeigListeDrucken.Click
        'Sortieren nach Teig(RezeptNummer), ArtikelNummer und Tour
        Produktion.RootProduktionsSchritt.SortBackZettel()

        Dim pDialog As New wb_PrinterDialog 'Drucker-Dialog

        'Druck-Daten
        pDialog.LL.DataSource = New ObjectDataProvider(Produktion.RootProduktionsSchritt.ChildSteps)
        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Produktion"
        pDialog.ListFileName = "TeigListe.lst"
        pDialog.ShowDialog()
    End Sub

    Private Sub BtnTeigListeExport_Click(sender As Object, e As EventArgs) Handles BtnTeigListeExport.Click
        'Sortieren nach Teig(RezeptNummer), ArtikelNummer und Tour
        Produktion.RootProduktionsSchritt.SortProduktionsPlan()
        For Each a As wb_Produktionsschritt In Produktion.RootProduktionsSchritt.ChildSteps
            Debug.Print("Nach Sort " & a.Tour & "/" & a.RezeptNummer & "/" & a.RezeptBezeichnung & "/" & a.Sollmenge_Stk & "/" & a.TeigChargenTeilerResult & "/" & a.SortKriterium)
        Next
        'gleiche (Rest-)Teige zusammenfassen
        Produktion.TeigeZusammenfassen()

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

                'Artikelzeilen
                For Each a As wb_Produktionsschritt In Produktion.RootProduktionsSchritt.ChildSteps
                    'ChargenZeilen
                    For Each r As wb_Produktionsschritt In a.ChildSteps
                        If Not r.Optimiert Then
                            sw.WriteLine(ProdDatenSatz(r))
                            Debug.Print("Produktion " & ProdDatenSatz(r))
                        End If
                    Next
                Next
                sw.Flush()
            End Using
        End Using

        'per FTP zu WinBack übertragen
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        Dim Result As String = wb_Functions.FTP_Upload_File(T1001.FullName)
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
                Case 40     ' [40] Produktionsblock
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
                Case 44  ' [44] Produktionsmenge in [kg]
                    ExpStr = ExpStr + "ARZ_Sollmenge_kg" + SepStr
                Case Else
                    ExpStr = ExpStr + SepStr
            End Select
        Next
        Return ExpStr
    End Function

    Private Function ProdDatenSatz(x As wb_Produktionsschritt) As String
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
                Case 44  ' [44] Produktionsmenge in [kg]
                    ExpStr = ExpStr + wb_Functions.DoubleToXString(x.Sollwert_kg) + SepStr
                Case Else
                    ExpStr = ExpStr + SepStr
            End Select
        Next
        Return ExpStr

    End Function

End Class
