Public Class wb_Rezept_Rezeptur

    Dim Rezept As wb_Rezept
    Dim NwtTabelle(wb_Global.maxTyp301) As wb_Global.Nwt

    'Dim LL_Rezeptur As New combit.ListLabel22.ListLabel()

    Public Sub New(RzNummer As Integer, RzVariante As Integer)

        'Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

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
        'TEST
        Dim x = Rezept.BruttoRezeptGewicht
        Label6.Text = Rezept.KtTyp301.Wert(wb_Global.T301_Kilokalorien)
        Label7.Text = Rezept.KtTyp301.Wert(wb_Global.T301_Zucker)
    End Sub

    Private Sub BtnDrucken_Click(sender As Object, e As EventArgs) Handles BtnDrucken.Click
        Debug.Print("LL Drucken")
        'LL_Rezeptur.AutoProjectFile = "Rezeptur.lst"
        'LL_Rezeptur.AutoShowSelectFile = False
        'LL_Rezeptur.Print()
        'LL_Rezeptur.Design()
    End Sub

    Private Sub VirtualTree_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub VirtualTree_DoubleClick(sender As Object, e As EventArgs)

    End Sub

    ''' <summary>
    ''' Anzeige der berechneten Nährwerte der Rezeptur.
    ''' Die Daten werden im einem ein-dimensionalen Array vorbereitet und dann in einem eigenen Objekt (abgeleitet von DataGridView) angezeigt.
    ''' 
    ''' Das Array besteht aus dem Grundgerüst (Nummer, Bezeichnung, Einheit, Gruppe). Diese Daten kommen aus dem Hash-Table kt301Param(Nr)
    ''' Die Nährwert-Info kommt aus dem Array ktTyp301.Wert von Rezept._RootRezeptSchritt. 
    ''' Die Berechnung der Nährwerte startet über ktTyp301(Get) im RootRezeptschritt (Rekursiv) über alle unterlagerten Rezeptschritte.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnNwt_Click(sender As Object, e As EventArgs) Handles BtnNwt.Click
        If Wb_TabControl.SelectedIndex = 0 Then
            BtnNwt.Text = "Rezeptur"
            'Array aufbauen über alle Nähwerte - Grid aus KomponParam301_global, Werte aus Rezept.ktTyp301.Wert(_RootRezeptschritt)
            For i = 1 To wb_Global.maxTyp301
                NwtTabelle(i).Visible = wb_KomponParam301_Global.kt301Param(i).Used
                NwtTabelle(i).Nr = i
                NwtTabelle(i).Text = wb_KomponParam301_Global.kt301Param(i).Bezeichnung
                NwtTabelle(i).Wert = Rezept.KtTyp301.Wert(i)
                NwtTabelle(i).Einheit = wb_KomponParam301_Global.kt301Param(i).Einheit
                NwtTabelle(i).Header = wb_KomponParam301_Global.kt301Param(i).Gruppe

                If NwtTabelle(i).Visible Then
                    Debug.Print(wb_Functions.kt301GruppeToString(NwtTabelle(i).Header) & " " & NwtTabelle(i).Text & " " & NwtTabelle(i).Wert & " " & NwtTabelle(i).Einheit)
                End If
            Next

            'Daten im Grid anzeigen
            Dim nwtGrid As New wb_KomponParam301_GridView(NwtTabelle)
            nwtGrid.Location(tb_Naehrwerte, 0, 0, tb_Naehrwerte.Width, tb_Naehrwerte.Height)
            Wb_TabControl.SelectedTab = tb_Naehrwerte
        Else
            Wb_TabControl.SelectedTab = tb_Rezeptur
            BtnNwt.Text = "Nährwerte"
        End If

    End Sub
End Class