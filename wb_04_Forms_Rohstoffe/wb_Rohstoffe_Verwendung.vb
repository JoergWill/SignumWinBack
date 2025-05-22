Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking
Imports Microsoft.Office.Interop

Public Class wb_Rohstoffe_Verwendung
    Inherits DockContent

    Private xlApp As Excel.Application
    Private xlWorkBooks As Excel.Workbooks
    Private xlWorkBook As Excel.Workbook
    Private xlWorkSheets As Excel.Sheets

    Private Const ColRezNmr = 0
    Private Const ColRezVar = 1
    Private Const ColRezBez = 2
    Private Const ColSollMg = 3
    Private Const ColRezGew = 4

    Private Sub wb_Rohstoffe_Verwendung_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Rohstoff-tauschen im Popup-Menu
        VerwDataGridView.PopupItemAdd("Rohstoff in Rezepten ersetzen", "", Nothing, AddressOf RohstoffeTauschen, True)

        'Rohstoff-Verwendung Export nach Excel im Popup-Menu
        If wb_GlobalSettings.ExcelInstalled Then
            VerwDataGridView.PopupItemAdd("Rohstoff-Verwendung Liste Export nach Excel", "", Nothing, AddressOf RohstoffVerwendungExcel, False)
        End If
        'Rohstoff-Verwendung drucken im Popup-Menu
        VerwDataGridView.PopupItemAdd("Rohstoff-Verwendung Liste drucken", "", Nothing, AddressOf RohstoffVerwendungDrucken, True)

        'Beim ersten Aufruf wird der aktuelle Rohstoff angezeigt
        If RohStoff IsNot Nothing Then
            DetailInfo(sender, False)
        End If
        'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
        AddHandler eListe_Click, AddressOf DetailInfo
    End Sub

    Private Sub wb_Rohstoffe_Verwendung_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Rohstoffe_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Public Sub DetailInfo(sender As Object, Reload As Boolean)
        'DataGrid füllen
        VerwDataGridView.LoadVerwendung(RohStoff.Nr)
    End Sub

    Private Sub RohstoffeTauschen()
        'Dialog-Fenster Rohstoff im Rezept tauschen/ersetzen
        Dim RohstoffeTauschen As New wb_Rohstoffe_Tauschen
        'wenn Rezepturen geändert worden sind, wird die Anzeige aktualisiert
        If RohstoffeTauschen.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            'Liste aktualisieren
            Liste_Click(Nothing)
        End If
    End Sub

    Private Sub RohstoffVerwendungDrucken()
        'Drucker-Dialog
        Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog
        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Rohstoffe"
        'Report-File für List&Label-Vorlage
        pDialog.ListFileName = "RohstoffVerwendung.lst"

        'RootRezeptSchritt.Steps enthält alle Rezeptschritte als flache Liste 
        Dim VerwListe As New List(Of wb_RezListe)
        For i = 1 To VerwDataGridView.Rows.Count
            Dim x As New wb_RezListe
            x.RezeptNummer = VerwDataGridView.Rows(i - 1).Cells(ColRezNmr + 1).Value
            x.RezeptVariante = VerwDataGridView.Rows(i - 1).Cells(ColRezVar + 1).Value
            x.RezeptBezeichnung = VerwDataGridView.Rows(i - 1).Cells(ColRezBez + 1).Value
            x.Sollwert = VerwDataGridView.Rows(i - 1).Cells(ColSollMg + 1).Value
            x.RezeptGewicht = VerwDataGridView.Rows(i - 1).Cells(ColRezGew + 1).Value
            'zur Liste hinzufügen
            VerwListe.Add(x)
        Next

        'An die generische Liste binden
        pDialog.LL.SetDataBinding(VerwListe, String.Empty)
        'Kopfzeilen
        pDialog.LL_KopfZeile_1 = RohStoff.Bezeichnung
        'Parameter (Einheiten Rohstoff und Rezept)
        pDialog.LL_Parameter_1 = wb_Einheiten_Global.GetEinheitFromNr(RohStoff.Einheit)
        pDialog.LL_Parameter_2 = wb_Einheiten_Global.GetEinheitFromNr(wb_Global.wbEinheitKilogramm)

        pDialog.ShowDialog()
        'wieder freigeben
        pDialog.Close()
    End Sub

    Private Sub RohstoffVerwendungExcel()
        'Excel OLE-Verknüpfung
        xlApp = New Excel.Application
        xlWorkBooks = xlApp.Workbooks
        xlWorkBook = xlWorkBooks.Add()
        xlWorkSheets = xlWorkBook.Sheets

        'Array Überschrift
        Dim xlRange As Excel.Range
        'Array Strings
        Dim xslRange As Excel.Range
        'Array Double
        Dim xdlRange As Excel.Range

        'Nächstes(neues) Arbeitsblatt
        Dim xlWorkSheet As Excel.Worksheet
        xlWorkSheet = xlWorkSheets(1)
        'Rohstoff Name
        Dim RohstoffName As String = wb_Functions.XRenameToExcelTabName(RohStoff.Bezeichnung)
        xlWorkSheet.Name = RohstoffName

        'Array für die Daten (Zeilen,Spalten)
        Dim xlsArray(VerwDataGridView.Rows.Count + 2, ColRezGew + 1) As String
        Dim xldArray(VerwDataGridView.Rows.Count + 1, 2) As Double

        'In der ersten Zeile steht die Rezept-Bezeichnung
        xlWorkSheet.Range("A1").Value = RohStoff.Bezeichnung
        'In der zweiten Zeile stehen die Spalten-Überschriften
        xlsArray(0, ColRezNmr) = "Rezept"
        xlsArray(0, ColRezVar) = "Variante"
        xlsArray(0, ColRezBez) = "Bezeichnung"
        xlsArray(0, ColSollMg) = "Sollmenge"
        xlsArray(0, ColRezGew) = "Rezeptgewicht"

        'Get the range where the starting cell has the address
        xslRange = xlWorkSheet.Range("A2", Reflection.Missing.Value)
        xdlRange = xlWorkSheet.Range("D3", Reflection.Missing.Value)

        'String- und Double-Array mit Daten aus Rohstoff-Verwendung füllen
        Dim i As Integer = 0
        For i = 1 To VerwDataGridView.Rows.Count
            'Rezept-Nummer
            xlsArray(i, ColRezNmr) = VerwDataGridView.Rows(i - 1).Cells(ColRezNmr + 1).Value
            'Rezept-Variante (als String !)
            xlsArray(i, ColRezVar) = "V" & VerwDataGridView.Rows(i - 1).Cells(ColRezVar + 1).Value
            'Rezept-Bezeichnung
            xlsArray(i, ColRezBez) = VerwDataGridView.Rows(i - 1).Cells(ColRezBez + 1).Value

            'Sollwert im Rezept (als Double)
            xldArray(i - 1, 0) = wb_Functions.StrToDouble(VerwDataGridView.Rows(i - 1).Cells(ColSollMg + 1).Value)
            'Rezeptgewicht (als Double)
            xldArray(i - 1, 1) = wb_Functions.StrToDouble(VerwDataGridView.Rows(i - 1).Cells(ColRezGew + 1).Value)

        Next

        'zuerst die Strings ausgeben
        xslRange = xslRange.Resize(VerwDataGridView.Rows.Count + 2, ColRezGew + 1)
        xslRange.Value = xlsArray
        'anschliessend die Sollwerte (Numerisch) eintragen
        xdlRange = xdlRange.Resize(VerwDataGridView.Rows.Count + 2, 2)
        xdlRange.Value = xldArray

        'Überschrift (Rezeptname) in einer Zelle zusammenfassen
        xlRange = xlWorkSheet.Range("A1", "E1")
        xlRange.Merge()
        xlRange.HorizontalAlignment = Excel.Constants.xlCenter
        xlRange.Interior.ColorIndex = 6 'Yellow
        xlRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

        'Spaltenbreite Rohstoff-Nummer und Bezeichnung
        xlWorkSheet.Columns("A:E").EntireColumn.AutoFit()
        'Spalte Variante zentriert
        xlRange = xlWorkSheet.Range("B2", "B20")
        xlRange.HorizontalAlignment = Excel.Constants.xlCenter
        'Format Decimalzahl für die Sollwerte
        xlWorkSheet.Columns("D:E").EntireColumn.NumberFormat = "[Black][>0]0.00" + Chr(34) + " kg" + Chr(34) & ";[White][<=0]0.00"

        'Display Excel
        xlApp.Visible = True
        xlApp.UserControl = True
    End Sub

End Class

Public Class wb_RezListe
    Private _RezeptNummer As String
    Private _RezeptVariante As Integer
    Private _RezeptBezeichnung As String
    Private _Sollwert As Double
    Private _RezeptGewicht As Double

    Public Property RezeptNummer As String
        Get
            Return _RezeptNummer
        End Get
        Set(value As String)
            _RezeptNummer = value
        End Set
    End Property

    Public Property RezeptVariante As Integer
        Get
            Return _RezeptVariante
        End Get
        Set(value As Integer)
            _RezeptVariante = value
        End Set
    End Property

    Public Property RezeptBezeichnung As String
        Get
            Return _RezeptBezeichnung
        End Get
        Set(value As String)
            _RezeptBezeichnung = value
        End Set
    End Property

    Public Property Sollwert As Double
        Get
            Return _Sollwert
        End Get
        Set(value As Double)
            _Sollwert = value
        End Set
    End Property

    Public Property RezeptGewicht As Double
        Get
            Return _RezeptGewicht
        End Get
        Set(value As Double)
            _RezeptGewicht = value
        End Set
    End Property
End Class
