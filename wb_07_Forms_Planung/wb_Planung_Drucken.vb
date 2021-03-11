Imports System.Windows.Forms
Imports combit.ListLabel22.DataProviders
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_Drucken
    Inherits DockContent

    Private Structure wb_DruckAuftrag
        Private Name As String
        Private Drucken As Boolean
        Private Kommentar As Boolean
    End Structure

    Private DruckLinienGruppe As New ArrayList
    Private DruckAufarbeitung As New ArrayList

    Private DruckAuftragLinienGruppe As wb_ArrayGridViewDruckAuftrag
    Private DruckAuftragAufarbeitung As wb_ArrayGridViewDruckAuftrag

    Private sColNames As New List(Of String)
    Private LGruppe As wb_Global.wb_LinienGruppe = Nothing

    Private Sub wb_Planung_Drucken_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Vorbelegung des Grid aus der Tabelle ItemParameter abhängig von der Benutzer-Nummer
        Dim ItemAttr502 As String = ""
        Dim ItemAttr503 As String = ""
        Dim ItemAttr504 As String = ""
        Dim ItemAttr505 As String = ""

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlPlanungListe, wb_GlobalSettings.AktUserNr.ToString)
        winback.sqlSelect(sql)
        'Datensätze lesen
        While winback.Read
            Select Case winback.iField("IP_ItemAttr")
                Case 502
                    ItemAttr502 = winback.sField("IP_Wert5Str")
                Case 503
                    ItemAttr503 = winback.sField("IP_Wert5Str")
                Case 504
                    ItemAttr504 = winback.sField("IP_Wert5Str")
                Case 505
                    ItemAttr505 = winback.sField("IP_Wert5Str")
            End Select
        End While
        'Datenbank-Verbindung wieder schliessen
        winback.Close()

        'Spaltenüberschriften
        sColNames.Clear()
        sColNames.AddRange({"", "?D", "&Teigliste", "", ""})

        'Teig-Linien
        DruckLinienGruppe.Clear()
        For Each item As DictionaryEntry In wb_Linien_Global.RezeptLinienGruppen
            LGruppe.Bezeichnung = item.Value
            LGruppe.TeigZettelDrucken = False
            LGruppe.BackZettelDrucken = False
            LGruppe.LinienGruppe = item.Key
            'Daten aus der letzten Sitzung wiederherstellen (Tabelle ItemParameter IP_ItemAttr=502)
            LGruppe.bDrucken = ItemAttr502.Contains("x" & LGruppe.LinienGruppe.ToString & "x")
            DruckLinienGruppe.Add(LGruppe)
        Next

        'Daten im Grid anzeigen
        DruckAuftragLinienGruppe = New wb_ArrayGridViewDruckAuftrag(DruckLinienGruppe, sColNames)
        DruckAuftragLinienGruppe.ScrollBars = ScrollBars.Vertical
        DruckAuftragLinienGruppe.BackgroundColor = Me.BackColor
        DruckAuftragLinienGruppe.GridLocation(pnlLinienGruppe)
        DruckAuftragLinienGruppe.PerformLayout()
        DruckAuftragLinienGruppe.Refresh()

        'Spaltenüberschriften
        sColNames.Clear()
        sColNames.AddRange({"", "?D", "&Aufarbeitung", "?K", "?S"})
        DruckAufarbeitung.Clear()

        'Aufarbeitungs-Plätze
        For Each item As DictionaryEntry In wb_Linien_Global.ArtikelLinienGruppen
            LGruppe.Bezeichnung = item.Value
            LGruppe.TeigZettelDrucken = False
            LGruppe.BackZettelDrucken = False
            LGruppe.LinienGruppe = item.Key
            'Daten aus der letzten Sitzung wiederherstellen (Tabelle ItemParameter IP_ItemAttr=503)
            LGruppe.bDrucken = ItemAttr503.Contains("x" & LGruppe.LinienGruppe.ToString & "x")
            'Daten aus der letzten Sitzung wiederherstellen (Tabelle ItemParameter IP_ItemAttr=504)
            LGruppe.bKommentar = ItemAttr504.Contains("x" & LGruppe.LinienGruppe.ToString & "x")
            'Daten aus der letzten Sitzung wiederherstellen (Tabelle ItemParameter IP_ItemAttr=505)
            LGruppe.bSonderText = ItemAttr505.Contains("x" & LGruppe.LinienGruppe.ToString & "x")
            DruckAufarbeitung.Add(LGruppe)
        Next

        'Daten im Grid anzeigen
        DruckAuftragAufarbeitung = New wb_ArrayGridViewDruckAuftrag(DruckAufarbeitung, sColNames)
        DruckAuftragAufarbeitung.ScrollBars = ScrollBars.Vertical
        DruckAuftragAufarbeitung.BackgroundColor = Me.BackColor
        DruckAuftragAufarbeitung.GridLocation(pnlAufarbeitung)
        DruckAuftragAufarbeitung.PerformLayout()
        DruckAuftragAufarbeitung.Refresh()

    End Sub

    Private Sub BtnPrintTeigZettel_Click(sender As Object, e As EventArgs) Handles BtnPrintTeigZettel.Click
        'Sortieren nach Teig(RezeptNummer), ArtikelNummer und Tour
        wb_Planung_Shared.Produktion.RootProduktionsSchritt.SortBackZettel()

        'Daten filtern (Teigzettel)
        Dim TeigListe As New ArrayList
        TeigListe.Clear()
        'alle Einträge im Auswahl-Grid Teigzettel
        For Each s As wb_Global.wb_LinienGruppe In DruckAuftragLinienGruppe.GridArray
            'nur ausgewählte Liniengruppen drucken
            If s.bDrucken Then
                wb_Planung_Shared.FilterAndMark(TeigListe, True, wb_Global.NOFILTER, s.LinienGruppe, True)
            End If
        Next
        'Wenn nach dem Filtern Einträge vorhanden sind - Drucken
        If TeigListe.Count > 0 Then
            'Druck-Daten
            Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog
            pDialog.LL_KopfZeile_1 = "für " & wb_Planung_Shared.ProduktionsDatum.ToString("dddd") & ", den " & wb_Planung_Shared.ProduktionsDatum.ToString("dd.MM.yyyy")
            pDialog.LL.DataSource = New ObjectDataProvider(TeigListe)

            'List und Label-Verzeichnis für die Listen
            pDialog.ListSubDirectory = "Produktion"
            pDialog.ListFileName = "TeigListe.lst"
            pDialog.ShowDialog()
            pDialog = Nothing
        End If
    End Sub

    Private Sub BtnPrintAufarbeitung_Click(sender As Object, e As EventArgs) Handles BtnPrintAufarbeitung.Click
        'Sortieren nach Teig(RezeptNummer), ArtikelNummer und Tour
        wb_Planung_Shared.Produktion.RootProduktionsSchritt.SortBackZettel()

        'Daten filtern (Aufbereitungs-Ort)
        Dim BackZettel As New ArrayList
        BackZettel.Clear()
        'alle Einträge im Auswahl-Grid Aufareitung-Platz
        For Each s As wb_Global.wb_LinienGruppe In DruckAuftragAufarbeitung.GridArray
            'nur ausgewählte Aufarbeitungsplätze drucken
            If s.bDrucken Then
                wb_Planung_Shared.FilterAndMark(BackZettel, True, s.LinienGruppe, wb_Global.NOFILTER, True, s.bKommentar, s.bSonderText)
            End If
        Next
        'Wenn nach dem Filtern Einträge vorhanden sind - Drucken
        If BackZettel.Count > 0 Then
            'Druck-Daten
            Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog
            pDialog.LL_KopfZeile_1 = "für " & wb_Planung_Shared.ProduktionsDatum.ToString("dddd") & ", den " & wb_Planung_Shared.ProduktionsDatum.ToString("dd.MM.yyyy")
            pDialog.LL.DataSource = New ObjectDataProvider(BackZettel)

            'List und Label-Verzeichnis für die Listen
            pDialog.ListSubDirectory = "Produktion"
            pDialog.ListFileName = "BackZettel.lst"
            pDialog.ShowDialog()
            pDialog = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Speichert alle aktuellen Einstellungen abhängig von der User-Nummer 
    ''' 
    '''     ItemParameter.IP_ItemTyp    500
    '''     ItemParameter.ItemAttr      Liste-Art
    '''         502 - Drucken Teigliste
    '''         503 - Drucken Aufarbeitung
    '''         504 - Drucken Aufarbeitung Kommentare
    '''     ItemParameter.LfdNr         User-Nummer
    '''     ItemParameter.IP_Wert4Str   alle Liniengruppen-Nummern getrennt durch ein x
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Planung_Drucken_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'alle Einträge löschen
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlPlanungListeDelete, wb_GlobalSettings.AktUserNr.ToString)
        winback.sqlCommand(sql)
        'Teigliste
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlPlanungListeInsert, "502", wb_GlobalSettings.AktUserNr.ToString, GridToString(DruckAuftragLinienGruppe.GridArray, 502))
        winback.sqlCommand(sql)
        'Aufarbeitung(Backzettel)
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlPlanungListeInsert, "503", wb_GlobalSettings.AktUserNr.ToString, GridToString(DruckAuftragAufarbeitung.GridArray, 503))
        winback.sqlCommand(sql)
        'Aufarbeitung(Bemerkungen drucken)
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlPlanungListeInsert, "504", wb_GlobalSettings.AktUserNr.ToString, GridToString(DruckAuftragAufarbeitung.GridArray, 504))
        winback.sqlCommand(sql)
        'Aufarbeitung(SonderText drucken)
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlPlanungListeInsert, "505", wb_GlobalSettings.AktUserNr.ToString, GridToString(DruckAuftragAufarbeitung.GridArray, 505))
        winback.sqlCommand(sql)

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    Private Function GridToString(GridArray As Array, ItemAttr As Integer) As String
        'Liste aller Liniengruppen getrennt durch x
        Dim s As String = "x"
        'Schleife über alle Liniengruppen
        For Each Liniengruppe As wb_Global.wb_LinienGruppe In GridArray
            If (Liniengruppe.bDrucken And ItemAttr = 502) Or (Liniengruppe.bDrucken And ItemAttr = 503) Or (Liniengruppe.bKommentar And ItemAttr = 504) Or (Liniengruppe.bSonderText And ItemAttr = 505) Then
                s &= Liniengruppe.LinienGruppe & "x"
            End If
        Next
        'Ergebnis-String
        Return s
    End Function

End Class