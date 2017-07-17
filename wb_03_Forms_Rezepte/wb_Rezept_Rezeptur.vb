Imports Infralution.Controls.VirtualTree

Public Class wb_Rezept_Rezeptur

    Dim Rezept As wb_Rezept
    Dim RezeptHinweise As New wb_Hinweise(wb_Global.Hinweise.RezeptHinweise)
    Dim NwtTabelle(wb_Global.maxTyp301) As wb_Global.Nwt

    Private _RzNummer As Integer
    Private _RzVariante As Integer
    Private _RzHinweiseChanged As Boolean

    ''' <summary>
    ''' Objekt Rezeptur instanzieren
    ''' </summary>
    ''' <param name="RzNummer"></param>
    ''' <param name="RzVariante"></param>
    Public Sub New(RzNummer As Integer, RzVariante As Integer)

        'Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'Rezeptnummer und Rzept-Variante merken
        _RzNummer = RzNummer
        _RzVariante = RzVariante

        'TODO Rezeptdaten aus DB lesen und anzeigen

        ' TabControl - HideTabs
        Wb_TabControl.HideTabs = True

        Rezept = New wb_Rezept(RzNummer, Nothing, RzVariante)
        'Virtual Tree anzeigen
        VirtualTree.DataSource = Rezept.RootRezeptSchritt
        'alle Zeilen aufklappen
        VirtualTree.RootRow.ExpandChildren(True)

        'Gesamt-Rohstoffpreis der Rezeptur (aktuell berechnet)
        Label2.Text = Rezept.RezeptPreis
        'Rezeptgewicht (aktuell berechnet)
        Label3.Text = Rezept.RezeptGewicht
        'Mehlgesamt-Menge
        Label4.Text = Rezept.RezeptGesamtMehlmenge
        'Rezept TA
        Label5.Text = CInt(Rezept.RezeptTA)
    End Sub

    Private Sub BtnDrucken_Click(sender As Object, e As EventArgs) Handles BtnDrucken.Click
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
            Wb_TabControl.SelectedTab = tb_Naehrwerte
            ToolStripAllergenLegende.Visible = True
            'Nährwerte-Grid aufbauen und anzeigen
            NwtGrid()
        Else
            BtnNwt.Text = "Nährwerte"
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
        Dim sCellWidget As CellWidget = sender
        Dim RezeptNr As Integer = DirectCast(sCellWidget.Tree.SelectedItem, wb_Rezeptschritt).RezeptNr
        If RezeptNr > 0 Then
            'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen
            Dim Rezeptur As New wb_Rezept_Rezeptur(RezeptNr, 1)
            'Fenster-Text
            'TODO Rezeptname und Alpha-Nummer im Fenster-Titel anzeigen
            'Rezeptur.Text = wb_Rezept_Shared.aktRzNummer + " " + wb_Rezept_Shared.aktRzName
            'MDI-Fenster anzeigen
            Rezeptur.Show()
        End If
    End Sub
End Class