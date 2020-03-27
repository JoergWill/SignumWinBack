Imports combit.ListLabel22.DataProviders
Imports Infralution.Controls.VirtualTree
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Chargen_Shared

Public Class wb_Chargen_Details
    Inherits DockContent
    Dim ChargenProduziert As New wb_Chargen

    Private _DeltaStyleBold As New Infralution.Controls.StyleDelta
    Private _DeltaStyleItalic As New Infralution.Controls.StyleDelta
    Private _StatistikType As wb_Global.StatistikType

    Private Sub wb_Chargen_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'New Style setzen (Delta/Italic)
        _DeltaStyleItalic.Font = New Drawing.Font(ChargenTree.Columns(0).CellStyle.Font, System.Drawing.FontStyle.Italic)
        'New Style setzen (Delta/Bold)
        _DeltaStyleBold.Font = New Drawing.Font(ChargenTree.Columns(0).CellStyle.Font, System.Drawing.FontStyle.Bold)

        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Chargen_Shared.eListe_Click, AddressOf DetailInfo
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler wb_Chargen_Shared.eListe_Print, AddressOf DetailPrint
    End Sub

    ''' <summary>
    ''' Berechnen der Chargen.
    ''' Abhängig vom Statistik-Typ wird die entsprechende Abfrage in der Klasse wb_Chargen ausgeführt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="StatistikType"></param>
    Public Sub DetailInfo(sender As Object, StatistikType As wb_Global.StatistikType)

        If StatistikType <> wb_Global.StatistikType.DontChange Then
            _StatistikType = StatistikType
        End If

        'wenn schon Daten angezeigt worden sind
        If ChargenProduziert.RootChargenSchritt.ChildSteps.Count > 0 Then
            'Anzeige Virtual-Tree löschen
            ChargenProduziert.RootChargenSchritt.ChildSteps.Clear()
            ChargenTree.Invalidate()
            'Tree neu zeichnen(leer)
            If (ChargenProduziert.RootChargenSchritt IsNot Nothing) Then
                ChargenTree.DataSource = ChargenProduziert.RootChargenSchritt
            End If
        End If

        'Daten laden
        ChargenProduziert.MySQLdbSelect_ChargenSchritte(StatistikType)

        'Virtual Tree anzeigen
        If ChargenProduziert.RootChargenSchritt IsNot Nothing Then
            ChargenTree.DataSource = ChargenProduziert.RootChargenSchritt
        End If

        'Detail-Fenster in den Vordergrund bringen
        Me.BringToFront()
    End Sub

    ''' <summary>
    ''' Chargen-Details drucken.
    ''' Ruft den Drucker-Dialog mit dem entsprechenden Formular, abhängig vom Statistik-Typ auf
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="StatistikType"></param>
    Public Sub DetailPrint(sender As Object, StatistikType As wb_Global.StatistikType)
        'Druck-Daten
        Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog

        'Daten generieren für List&Label (flache Liste)
        Dim StatistikDaten As New ArrayList
        'alle Einträge in der berechneten Chargenliste
        For Each a As wb_ChargenSchritt In ChargenProduziert.RootChargenSchritt.ChildSteps
            'Artikel-Zeile
            a.ChrgType = wb_Global.ChargenTypen.CHRG_ARTIKEL
            StatistikDaten.Add(a)
            For Each r As wb_ChargenSchritt In a.ChildSteps
                'Rezept-Zeilen
                r.ChrgType = wb_Global.ChargenTypen.CHRG_REZEPT
                StatistikDaten.Add(r)
                'For Each k As wb_ChargenSchritt In r.ChildSteps
                '    'Komponenten (flache Liste)
                '    k.ChrgType = wb_Global.ChargenTypen.CHRG_UNDEF
                '    StatistikDaten.Add(k)
                'Next
            Next
        Next
        'Array zuordnen zu List&Label
        pDialog.LL.DataSource = New ObjectDataProvider(StatistikDaten)

        'List und Label-Listen-Name abhängig vom Statistik-Typ
        Select Case StatistikType
            Case wb_Global.StatistikType.ChargenAuswertung
                pDialog.ListFileName = "ChargenAuswertung.lst"
            Case wb_Global.StatistikType.StatistikRezepte
                pDialog.ListFileName = "StatistikRezepte.lst"
                pDialog.LL_KopfZeile_1 = "vom " & wb_Chargen_Shared.FilterVon.ToString("dd.MM.yyyy") & " bis " & wb_Chargen_Shared.FilterVon.ToString("dd.MM.yyyy")
            Case wb_Global.StatistikType.StatistikRohstoffeDetails
                pDialog.ListFileName = "StatistikRohstDetails.lst"
            Case wb_Global.StatistikType.StatistikRohstoffeVerbrauch
                pDialog.ListFileName = "StatistikRohstVerbrauch.lst"
        End Select

        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Chargen"
        pDialog.ShowDialog()
        pDialog = Nothing
    End Sub


    ''' <summary>
    ''' Virtual-Tree Zeilen/Zellen mit geändertem Zeichensatz darstellen
    '''     
    '''     Artikelzeilen - Fett
    '''     Rezeptzeilen  - Kursiv
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub VirtualTree_GetCellData(sender As Object, e As GetCellDataEventArgs) Handles ChargenTree.GetCellData
        'get the default binding for the given row And use it to populate the cell data
        Dim Binding As RowBinding = _ChargenTree.GetRowBinding(e.Row)
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
    Private Sub VirtualTree_CellDoubleClick(sender As Object, e As EventArgs) Handles ChargenTree.CellDoubleClick
        'Doppel-Click auf VirtualTree-Cell
        Dim sCellWidget As CellWidget = sender
        'Chargen-Rezeptschritt ermitteln
        Dim ChargenZeile As wb_ChargenSchritt = DirectCast(sCellWidget.Row.Item, wb_ChargenSchritt)
        'Auswertung nur in Komponenten-Zeilen
        If ChargenZeile.Type = wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE Then
            Detail_DblClick(sender, ChargenZeile)
        End If
    End Sub

    Private Sub wb_Chargen_Details_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Chargen_Shared.eListe_Click, AddressOf DetailInfo
    End Sub
End Class