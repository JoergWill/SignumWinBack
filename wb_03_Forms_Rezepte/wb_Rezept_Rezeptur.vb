Imports System.Windows.Forms
Imports combit.ListLabel22.DataProviders

Public Class wb_Rezept_Rezeptur

    Dim Rezept As wb_Rezept
    'Dim LL_Rezeptur As New combit.ListLabel22.ListLabel()

    Public Sub New(RzNummer As Integer, RzVariante As Integer)

        'Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
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
        Debug.Print("LL Drucken")
        'TEST
        Label6.Text = Rezept.KtTyp301.Wert(wb_Global.T301_Kilokalorien)
        Label7.Text = Rezept.KtTyp301.Wert(wb_Global.T301_KiloJoule)
        'LL_Rezeptur.AutoProjectFile = "Rezeptur.lst"
        'LL_Rezeptur.AutoShowSelectFile = False
        'LL_Rezeptur.Print()
        'LL_Rezeptur.Design()
    End Sub

    Private Sub VirtualTree_Click(sender As Object, e As EventArgs) Handles VirtualTree.Click

    End Sub

    Private Sub VirtualTree_DoubleClick(sender As Object, e As EventArgs) Handles VirtualTree.DoubleClick

    End Sub
End Class