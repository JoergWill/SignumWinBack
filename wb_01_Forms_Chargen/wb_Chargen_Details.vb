Imports Infralution.Controls.VirtualTree
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Chargen_Shared

Public Class wb_Chargen_Details
    Inherits DockContent
    Dim ChargenProduziert As New wb_Chargen

    Private _DeltaStyleBold As New Infralution.Controls.StyleDelta
    Private _DeltaStyleItalic As New Infralution.Controls.StyleDelta

    Private Sub wb_Chargen_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dieser Aufruf ist für den Designer erforderlich.
        'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        InitializeComponent()

        'New Style setzen (Delta/Italic)
        _DeltaStyleItalic.Font = New Drawing.Font(VirtualTree.Columns(0).CellStyle.Font, System.Drawing.FontStyle.Italic)
        'New Style setzen (Delta/Bold)
        _DeltaStyleBold.Font = New Drawing.Font(VirtualTree.Columns(0).CellStyle.Font, System.Drawing.FontStyle.Bold)

        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo()
        'wenn schon Daten angezeigt worden sind
        If ChargenProduziert.RootChargenSchritt.ChildSteps.Count > 0 Then
            'Anzeige Virtual-Tree löschen
            ChargenProduziert.RootChargenSchritt.ChildSteps.Clear()
            VirtualTree.Invalidate()
            'Tree neu zeichnen(leer)
            If ChargenProduziert.RootChargenSchritt IsNot Nothing Then
                VirtualTree.DataSource = ChargenProduziert.RootChargenSchritt
            End If
        End If

        'Daten laden
        ChargenProduziert.MySQLdbSelect_ChargenSchritte(Liste_TagesWechselNummer)

        'Virtual Tree anzeigen
        If ChargenProduziert.RootChargenSchritt IsNot Nothing Then
            VirtualTree.DataSource = ChargenProduziert.RootChargenSchritt
        End If
    End Sub

    ''' <summary>
    ''' Virtual-Tree Zeilen/Zellen mit geändertem Zeichensatz darstellen
    '''     
    '''     Artikelzeilen - Fett
    '''     Rezeptzeilen  - Kursiv
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_GetCellData(sender As Object, e As GetCellDataEventArgs) Handles VirtualTree.GetCellData
        'get the default binding for the given row And use it to populate the cell data
        Dim Binding As RowBinding = _VirtualTree.GetRowBinding(e.Row)
        Binding.GetCellData(e.Row, e.Column, e.CellData)

        Select Case DirectCast(e.Row.Item, wb_ChargenSchritt).Type
            Case wb_Global.KomponTypen.KO_ZEILE_ARTIKEL, wb_Global.KomponTypen.KO_ZEILE_DUMMYARTIKEL
                VirtualTree_SetFontStyle(e.CellData.EvenStyle, _DeltaStyleBold)
                VirtualTree_SetFontStyle(e.CellData.OddStyle, _DeltaStyleBold)
            Case wb_Global.KomponTypen.KO_ZEILE_REZEPT
                VirtualTree_SetFontStyle(e.CellData.EvenStyle, _DeltaStyleItalic)
                VirtualTree_SetFontStyle(e.CellData.OddStyle, _DeltaStyleItalic)
        End Select
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
    ''' Doppelclick auf Virtual-Tree Zeile
    '''     Teigtemperatur-Zeile    - Öffnet ein Fenster mit einer Auflistung aller Teige dieser Rezeptur (nur Temp-Mess-Zeile)
    '''     Wasser-Zeile            - Öffnet ein Fenster mit der Berechnung der Wasser-Soll-Temperatur aus log-File WinBack
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_CellDoubleClick(sender As Object, e As EventArgs) Handles VirtualTree.CellDoubleClick
        'Doppel-Click auf VirtualTree-Cell
        Dim sCellWidget As CellWidget = sender
        'Chargen-Rezeptschritt ermitteln
        Dim ChargenZeile As wb_ChargenSchritt = DirectCast(sCellWidget.Row.Item, wb_ChargenSchritt)
        'Auswertung nur in Komponenten-Zeilen
        If ChargenZeile.Type = wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE Then
            Detail_DblClick(sender, ChargenZeile)
        End If
    End Sub

    Private Sub VirtualTree_Click(sender As Object, e As EventArgs) Handles VirtualTree.Click

    End Sub
End Class