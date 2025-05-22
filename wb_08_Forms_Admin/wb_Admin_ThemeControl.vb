Imports System.Drawing
Imports System.Windows.Forms

Public NotInheritable Class wb_Admin_ThemeControl
    Const ColorInfoColumn = 2
    Const ColorColumn = 1

    Dim ThemeColorList As New List(Of ThemeColor)
    Dim HeaderLinies As New List(Of String)

    Public Event Ac_LoadColorTable(FileName As String)

    ''' <summary>
    ''' Beim Laden der Form werden die Farbcodes aus der Ini-Datei eingelesen.
    ''' Die Schaltflächen mit den Default-Themes werden dynamisch erzeugt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AboutWinback_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Standard-Themes dynamisch erzeugen
        Dim Top As Integer = BtnDefault_Black.Top
        Dim Button As New Button

        For Each DefTheme As String In System.Enum.GetNames(GetType(RibbonTheme))
            'alle Buttons ausser "Black"    
            If DefTheme <> "Black" Then
                With Button
                    Top += 35
                    .Text = DefTheme
                    .Top = Top
                    .Left = BtnDefault_Black.Left
                    .Width = BtnDefault_Black.Width
                    .Height = BtnDefault_Black.Height
                    .Name = "BtnDefault_" & DefTheme
                    .Tag = DefTheme
                    AddHandler Button.Click, AddressOf BtnDefault_Click
                End With
                'Button hinzufügen
                Me.Controls.Add(Button)
                Button = New Button
            End If
        Next
    End Sub

    Private Sub Admin_ThemeControl_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Theme aus Text-File (ini) einlesen und in Grid darstellen
        ReadFromFile()
    End Sub

    Private Sub WriteToFile()
        Dim Data As New List(Of String)

        'Header
        For Each s In HeaderLinies
            Data.Add(s)
        Next

        'Data
        For Each t In ThemeColorList
            Data.Add(Trim(t.Name) & " = " & Trim(t.Color))
        Next

        'Write Data to Text-File
        System.IO.File.WriteAllLines(wb_GlobalSettings.pColorThemePath, Data)
    End Sub

    Private Sub ReadFromFile()
        Dim Header As Boolean = True

        HeaderLinies.Clear()
        ThemeColorList.Clear()

        Try

            For Each Zeile As String In System.IO.File.ReadAllLines(wb_GlobalSettings.pColorThemePath)
                Dim Data() As String = Zeile.Split("=")

                If Header Then
                    HeaderLinies.Add(Data(0))
                    If Data(0) = "[ColorTable]" Then
                        Header = False
                    End If
                Else
                    ThemeColorList.Add(New ThemeColor(Data(0), Data(1)))
                End If
            Next
        Catch ex As Exception

        End Try

        'Grid mit Daten füllen
        ThemeColorGridView.DataSource = ThemeColorList
        'Hintergrund-Farben anzeigen (First Time)
        For i = 0 To ThemeColorGridView.RowCount - 1
            ThemeColorGridView.Rows(i).Cells(ColorColumn).Style.BackColor =
            ColorTranslator.FromHtml(ThemeColorGridView.Item(ColorInfoColumn, i).Value)
        Next

        Dim GridSize As Integer = 0
        For i = 0 To ThemeColorGridView.ColumnCount - 1
            GridSize += ThemeColorGridView.Columns(i).Width
        Next
        ThemeColorGridView.Width = GridSize + SystemInformation.VerticalScrollBarWidth + 3

    End Sub

    ''' <summary>
    ''' Die Daten im Grid haben sich geändert. Wenn die Spalte mit der Farb-Information sich geändert hat, wird der Hintergrund der Spalte mit 
    ''' der Farb-Anzeige entsprechend dem Farb-Code eingefärbt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles ThemeColorGridView.CellValueChanged
        If e.ColumnIndex = ColorInfoColumn Then
            ThemeColorGridView.Rows(e.RowIndex).Cells(ColorColumn).Style.BackColor =
            ColorTranslator.FromHtml(ThemeColorGridView.Item(e.ColumnIndex, e.RowIndex).Value)
        End If
    End Sub

    ''' <summary>
    ''' Doppelclick auf eine Zelle im Grid. Öffnet die Farb-Auswahl 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles ThemeColorGridView.CellDoubleClick
        If e.ColumnIndex = ColorInfoColumn Then
            ColorDialog.Color = ColorTranslator.FromHtml(ThemeColorGridView.Item(e.ColumnIndex, e.RowIndex).Value)
            If ColorDialog.ShowDialog = DialogResult.OK Then
                ThemeColorGridView.Rows(e.RowIndex).Cells(ColorInfoColumn).Value = " " &
                String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", ColorDialog.Color.A, ColorDialog.Color.R, ColorDialog.Color.G, ColorDialog.Color.B)

                'Buttons anpassen
                BtnSave.Enabled = True
                BtnSaveAndClose.Enabled = True
                BtnSaveAsDefault.Enabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Fenster schliessen. Es wird nicht gespeichert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        DialogResult = DialogResult.Abort
        Close()
    End Sub

    ''' <summary>
    ''' Click auf einen Theme-Button oder Speichern als Default. Über die Tag-Eigenschaft des Button wird 
    ''' der Event ausgelöst und der entsprechende Wert als Parameter übergeben.
    ''' 
    ''' Der Event wird über AboutWinBack weitergeleitet an die Main-Form (WinBack)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnDefault_Click(sender As Object, e As EventArgs) Handles BtnDefault_Black.Click, BtnSaveAsDefault.Click
        RaiseEvent Ac_LoadColorTable(DirectCast(sender, Button).Tag)
        If DirectCast(sender, Button).Tag = "SaveAsDefault" Then
            ReadFromFile()
            BtnSaveAsDefault.Enabled = False
        Else
            BtnSaveAsDefault.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Speichert die aktuelle Konfiguration aus dem Grid in die ini-Datei. Anschliessend wird über einen
    ''' Event die ini-Datei in WinBack geladen und der RibbonTab neu gezeichnet
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        WriteToFile()
        RaiseEvent Ac_LoadColorTable(wb_GlobalSettings.pColorThemePath)
        BtnSave.Enabled = False
    End Sub

    ''' <summary>
    ''' Speichert die aktuelle Konfiguration aus dem Grid in die ini-Datei. Das Fenster wird geschlossen und über einen Event wird
    ''' in der Main-Form (WinBack) der Ribbon-Tab neu geladen und gezeichnet.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSaveAndClose_Click(sender As Object, e As EventArgs) Handles BtnSaveAndClose.Click
        WriteToFile()
        RaiseEvent Ac_LoadColorTable(wb_GlobalSettings.pColorThemePath)
        DialogResult = DialogResult.Yes
    End Sub

End Class



Public Class ThemeColor
    Private _Nme As String
    Private _Clr As String

    Public Sub New(n As String, c As String)
        _Nme = n
        _Clr = c
    End Sub

    Public Property Name As String
        Get
            Return _Nme
        End Get
        Set(value As String)
            _Nme = value
        End Set
    End Property

    Public ReadOnly Property PlaceHolder As String
        Get
            Return ""
        End Get
    End Property

    Public Property Color As String
        Get
            Return _Clr
        End Get
        Set(value As String)
            _Clr = value
        End Set
    End Property

End Class
