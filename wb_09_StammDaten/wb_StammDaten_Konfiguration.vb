Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_StammDaten_Konfiguration
    Inherits DockContent

    Const ColBez = 0    'Spalte Bezeichnung
    Const ColWrt = 2    'Spalte Wert
    Const ColKmt = 3    'Spalte Kommentar
    Const ColId1 = 4    'Spalte ID1 - Index auf Erläuterungstext in winback.Hinweise2
    Const ColId2 = 5    'Spalte ID2 - Gruppierung der Konfigurationsparameter (Filter)

    Private FilterGrp As New SortedList

    Private Sub wb_Gruppen_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten, die mit + beginnen sind editierbar
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Bezeichnung", "", "+Wert", "&Kommentar", "", ""}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "KF_Kommentar"

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKonfiguration, "%"), "WinBackKonfig")

        'Spalte Wert zentriert darstellen
        DataGridView.Columns(ColBez).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridView.Columns(ColBez).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        'Default Spaltenbreite für Bezeichnung und Wert
        DataGridView.Columns(ColBez).Width = 250
        DataGridView.Columns(ColWrt).Width = 200

        'Multi-Select nicht zulassen
        DataGridView.MultiSelect = False

        'Combo-Box(Filter) mit Werten füllen
        LoadFilterTexte()
        cbKonfigGruppen.Fill(FilterGrp, True)

    End Sub

    ''' <summary>
    ''' Filter-Auswahl hat sich geändert.
    ''' DataGridView-Filter (Feld KF_Id2) neu festlegen.
    ''' 
    ''' Alte Einträge haben KF_Id2 = 99
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbKonfigGruppen_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKonfigGruppen.SelectionChangeCommitted
        Dim Filter_Id2 As Integer = cbKonfigGruppen.GetKeyFromSelection
        Dim Filter As String = "KF_Id2"

        'Abhängig von der Filter-ID
        If Filter_Id2 = 0 Then
            'alle (ohne veraltete) Einträge
            Filter &= "<> 99"
        Else
            'alle Einträge mit KF_Id2 = H2_Id2 (Filterkriterium)
            Filter &= "= " & Filter_Id2.ToString
        End If

        'Filter anwenden
        DataGridView.Filter = Filter
        'Focus von Auswahl-Liste entfernen
        BtnOK.Focus()
    End Sub

    Private Sub wb_StammDaten_ArtRohGruppen_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("WinBackKonfig")
    End Sub

    ''' <summary>
    ''' Zeigt im WebBrowser-Control einen beliebigen HTML-Text dynamisch zur Laufzeit an
    ''' </summary>
    ''' <param name="WebBrowser">WebBrowser-Control</param>
    ''' <param name="HtmlText">HTML-Text, der angezeigt werden sol</param>
    Private Sub wbShowHTML(ByVal WebBrowser As WebBrowser, ByVal HtmlText As String)
        'Clear
        wbClearHTML(WebBrowser)
        'HTML-Textstring in das WebBrowser-Control schreiben
        WebBrowser.Document.Body.InnerHtml = HtmlText
    End Sub

    Private Sub wbClearHTML(ByVal WebBrowser As WebBrowser)
        With WebBrowser
            If IsNothing(.Url) OrElse .Url.AbsoluteUri <> "about:blank" Then
                ' zunächst eine leere Seite laden
                .Navigate("about:blank")
                Application.DoEvents()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Der Datensatz-Zeiger hat sich geändert - Anzeige der HTML-Kommentare ändern
    ''' Der Event HasChanged kommt mit einer eingestellten Verzögerung, damit nicht beim schnellen Scrollen
    ''' jedesmal die Abfrage startet.
    ''' 
    ''' Die Erläuterungs-Texte zu den einzelnen Konfigurations-Einträgen in winback.Konfiguration stehen in
    ''' der Tabelle Hinweise2 
    ''' 
    '''     Hinweise.H2_Typ Konfiguration (H2_Typ = 5)
    '''     Hinweise_H2.Id1 fortlaufende Nummer der Einträge    -   Konfiguration.KF_Id1
    '''     Hinweise.H2_Id2 bezeichnet die Gruppe               -   Konfiguration.KF_Id2
    '''     Hinweise.Memo   Erläuerungs-Text im HTML-Format
    '''     
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView_HasChanged(sender As Object, e As EventArgs) Handles DataGridView.HasChanged
        'Versuche die Zusatzfelder zu lesen
        Try
            Dim Id1 As Integer = DataGridView.iField("KF_Id1")
            Dim Id2 As Integer = DataGridView.iField("KF_Id2")

            If Id1 > 0 Then
                Dim Hinweise As New wb_Hinweise(wb_Global.Hinweise.Konfiguration, Id1, Id2)
                If Hinweise.ReadOK Then
                    wbShowHTML(WebBrowser, Hinweise.Memo)
                Else
                    wbShowHTML(WebBrowser, "")
                End If
            Else
                wbShowHTML(WebBrowser, "")
            End If

        Catch ex As Exception
            'Datenbank-Tabelle muss erweitert werden - HTML-Text erstellen
            Dim sHTML As String = "<h1>Die Konfigurations-Tabelle muss erweitert werden</h1>" &
                                  "<p>Die Felder Id1 und Id2 fehlen in der winback.Konfiguration" &
                                  "</br>Kann über Admin-DBUpdate gepatcht werden</p>"

            ' HTML-Text im WebBrowser anzeigen
            wbShowHTML(WebBrowser, sHTML)

        End Try
    End Sub

    ''' <summary>
    ''' Filter Anzeige der Einträge in der Konfigurations-Tabelle winback.
    ''' 
    ''' Die Filter-Kriterien kommen aus der Tabelle winback.Hinweise2
    '''     Alle Einträge mit H2_Typ =5 sind Hinweis-Texte für die Konfiguration
    '''     In H2_Text1 steht der Oberbegriff für die Gruppierung und damit der
    '''     Beschreibungs-Text für den Filter.
    '''     In H2_Id2 steht die Filter-ID.
    '''     
    ''' Die Filter-ID korrespondiert mit dem Feld in Konfiguration.KF_Id2
    ''' </summary>
    Private Sub LoadFilterTexte()
        'HashTable mit der Übersetzung der Filter-ID(H2_Id2) in die Filter-Bezeichnung laden
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect("SELECT H2_Id2, H2_Text1 FROM Hinweise2 WHERE H2_Typ = 5 ORDER BY H2_Id2")
        Dim H2_Id2 As Integer = wb_Global.UNDEFINED
        FilterGrp.Clear()

        While winback.Read
            If H2_Id2 <> winback.iField("H2_Id2") Then
                H2_Id2 = winback.iField("H2_Id2")
                FilterGrp.Add(H2_Id2, winback.sField("H2_Text1"))
            End If
        End While
        winback.Close()
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub
End Class


