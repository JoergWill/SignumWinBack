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
    Private _ProduktionVariante As Integer = wb_Global.UNDEFINED

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        'Produktionsdaten einlesen für Linie ...
        _ProduktionLinie = 3
        _ProduktionVariante = 1

    End Sub

    Private Sub LinienAuto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Font für die Anzeige Artikelzeile im VirtualTree
        oFont = VirtualTree.Font
        iFont = New Drawing.Font(oFont.Name, oFont.Size, Drawing.FontStyle.Italic)

        ReadProduktionWinBack()

    End Sub

    Private Sub ReadProduktionWinBack()
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
        If Not Produktion.MySQLdbSelect_ArbRzSchritte(_ProduktionLinie, _ProduktionVariante) Then
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


End Class