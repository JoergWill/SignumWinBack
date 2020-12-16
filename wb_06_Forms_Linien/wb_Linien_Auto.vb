Imports WeifenLuo.WinFormsUI.Docking
Imports System.Globalization
Imports System.Threading
Imports System.Windows.Forms

Public Class wb_Linien_Auto
    Inherits DockContent

    Dim Produktion As New wb_Produktion_virtuell
    Dim oFont As Drawing.Font
    Dim iFont As Drawing.Font

    'Produktionsdaten einlesen für Linie
    Private _ProduktionLinie As Integer = wb_Global.UNDEFINED
    Private _ProduktionVariante As Integer = wb_Global.RezeptVarianteStandard

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        'ComboBox Linien füllen
        cbLinien.Fill(wb_Linien_Global.ProdLinien)

    End Sub

    Private Sub LinienAuto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Font für die Anzeige Artikelzeile im VirtualTree
        oFont = VirtualTree.Font
        iFont = New Drawing.Font(oFont.Name, oFont.Size, Drawing.FontStyle.Italic)

        cbLinien.Text = "Bitte Linie auswählen"
    End Sub

    Private Sub BtnAktualisieren_Click(sender As Object, e As EventArgs) Handles BtnAktualisieren.Click
        ReadProduktionWinBack(_ProduktionLinie, _ProduktionVariante)
    End Sub

    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click
        StartProduktionWinBack()
    End Sub

    Private Sub cbLinien_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLinien.SelectedIndexChanged
        _ProduktionLinie = cbLinien.GetKeyFromIndex(cbLinien.SelectedIndex)
        ReadProduktionWinBack(_ProduktionLinie, _ProduktionVariante)
        BtnStart.Focus()
    End Sub

    Private Sub ReadProduktionWinBack(ProdLinie As Integer, ProdVariante As Integer)
        'Cursor umschalten
        Me.Cursor = Cursors.WaitCursor

        'Prüfen ob schon Daten vorhanden sind
        If Produktion.RootProduktionsSchritt.ChildSteps.Count > 0 Then
            'alle Einträge löschen
            Produktion.RootProduktionsSchritt.ChildSteps.Clear()
            VirtualTree.Invalidate()
            'Tree neu zeichnen(leer)
            VirtualTree.DataSource = Produktion.RootProduktionsSchritt
        End If

        'Daten aus WinBack ArbRezepte und ArbRZSchritte einlesen
        If Not Produktion.MySQLdbSelect_ArbRzSchritte(ProdLinie, ProdVariante) Then
            'Default-Cursor
            Me.Cursor = Cursors.Default
            'keine Datensätze in der Vorlage
            MsgBox("Keine Datensätze in der Arbeitsrezepte-Liste", MsgBoxStyle.Exclamation, "Laden Produktionsdaten aus WinBack")
            VirtualTree.Invalidate()
        Else
            'Virtual Tree anzeigen
            VirtualTree.DataSource = Produktion.RootProduktionsSchritt
        End If
        'Default-Cursor
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub StartProduktionWinBack()
        'Daten aus der Produktion werden direkt nach wbdaten geschrieben
        Dim Tageswechsel As Boolean = True

        'Produktion durchlaufen (virtuelle Linie)
        Produktion.VirtProduktion(_ProduktionLinie, _ProduktionVariante, Tageswechsel)

        'Anzeige aktualisieren
        If Tageswechsel Then
            'alle Einträge löschen
            Produktion.RootProduktionsSchritt.ChildSteps.Clear()
            VirtualTree.Invalidate()
        End If

        'Tree neu zeichnen(leer)
        VirtualTree.DataSource = Produktion.RootProduktionsSchritt
    End Sub

End Class