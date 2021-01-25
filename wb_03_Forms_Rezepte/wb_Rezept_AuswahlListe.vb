Public Class wb_Rezept_AuswahlListe
    Private _RezeptNr As Integer = wb_Global.UNDEFINED
    Private _RezeptNummer As String = ""
    Private _RezeptName As String = ""
    Private _RezeptGewicht As String
    Private _RezeptChargeMin As New wb_Charge
    Private _RezeptChargeMax As New wb_Charge
    Private _RezeptChargeOpt As New wb_Charge
    Private _RezeptLinienGruppe As Integer

    Private _RezeptListe As New ArrayList
    Private _MultiSelect As Boolean = False

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        InitDataGrid()
    End Sub

    Public Sub New(RzNr As Integer)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        InitDataGrid(RzNr)
    End Sub

    Public Sub New(RezeptNummer As String, RezeptName As String)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        InitDataGrid(wb_Global.UNDEFINED, RezeptNummer, RezeptName)
    End Sub

    Public WriteOnly Property MultiSelect As Boolean
        Set(value As Boolean)
            _MultiSelect = value
        End Set
    End Property

    Public ReadOnly Property RowCount
        Get
            'wenn das Ergebnis eindeutig ist
            If DataGridView.RowCount = 1 Then
                GetResult()
            End If
            'Anzahl der Datensätze 
            Return DataGridView.RowCount
        End Get
    End Property

    Public ReadOnly Property RezeptNummer As String
        Get
            Return _RezeptNummer
        End Get
    End Property

    Public ReadOnly Property RezeptNr As Integer
        Get
            Return _RezeptNr
        End Get
    End Property

    Public ReadOnly Property RezeptListe As ArrayList
        Get
            Return _RezeptListe
        End Get
    End Property

    Public ReadOnly Property RezeptName As String
        Get
            Return _RezeptName
        End Get
    End Property

    Public ReadOnly Property RezeptChargeMin As wb_Charge
        Get
            Return _RezeptChargeMin
        End Get
    End Property

    Public ReadOnly Property RezeptChargeMax As wb_Charge
        Get
            Return _RezeptChargeMax
        End Get
    End Property

    Public ReadOnly Property RezeptChargeOpt As wb_Charge
        Get
            Return _RezeptChargeOpt
        End Get
    End Property

    Public ReadOnly Property RezeptLinienGruppe As Integer
        Get
            Return _RezeptLinienGruppe
        End Get
    End Property

    Private Sub InitDataGrid(Optional RzNr As Integer = wb_Global.UNDEFINED, Optional RezeptNummer As String = "", Optional RezeptName As String = "")
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "Nummer", "&Name"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRezeptListe, "RezeptListe")
        DataGridView.MultiSelect = _MultiSelect

        'DataGrid filtern nach Rezept-Nummer (Index)
        If RzNr <> wb_Global.UNDEFINED Then
            DataGridView.Filter = "RZ_Nr = " & RzNr
        End If

        'DataGrid filtern nach Rezept-Nummer (alpha)
        If RezeptNummer <> "" Then
            DataGridView.Filter = "RZ_Nr_AlNum = '" & RezeptNummer & "'"
        End If
        'DataGrid filtern nach Rezept-Bezeichnung (alpha)
        If RezeptName <> "" Then
            DataGridView.Filter = "RZ_Bezeichnung LIKE '%" & RezeptName & "%'"
        End If
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        _RezeptNr = 0
        _RezeptNummer = ""
        _RezeptName = ""
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.Abort
    End Sub

    Private Sub DataGridView_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView.DoubleClick
        GetResult()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' Rezept neu anlegen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        'neues Rezept anlegen
        Dim Rezept As New wb_Rezept
        Dim RezeptNrNeu As Integer = Rezept.MySQLdbNew(wb_Global.LinienGruppeStandard)

        'Speicher wieder freigeben
        Rezept = Nothing

        'Das neu erzeugte Rezept gleich öffnen
        Dim Rezeptur As New wb_Rezept_Rezeptur(RezeptNrNeu, wb_Global.RezeptVarianteStandard)
        Rezeptur.tbRzNummer.Focus()
        Rezeptur.ShowDialog()

        'Rezeptnummer und Name übernehmen
        _RezeptNr = RezeptNrNeu
        _RezeptNummer = Rezeptur.tbRzNummer.Text
        _RezeptName = Rezeptur.tbRezeptName.Text

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        GetResult()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' Ergebnis Auswahl zusammenfassen
    ''' Wenn nur eine Zeile ausgewählt worden ist, werden Rezeptnummer und Bezeichnung zurückgegeben. Bei 
    ''' Multiselect nur Rezept-Index als Liste
    ''' </summary>
    Private Sub GetResult()
        'Ergebnis-Liste leeren (immer)
        _RezeptListe.Clear()

        'Aktuelle Auswahl (MultiSelect = False)
        _RezeptNr = DataGridView.iField("RZ_Nr")
        _RezeptNummer = DataGridView.Field("RZ_Nr_AlNum")
        _RezeptName = DataGridView.Field("RZ_Bezeichnung")
        _RezeptLinienGruppe = DataGridView.iField("RZ_Liniengruppe")

        _RezeptChargeMin.TeigGewicht = DataGridView.Field("RZ_Gewicht")
        _RezeptChargeMin.MengeInkg = DataGridView.Field("RZ_Charge_Min")
        _RezeptChargeOpt.TeigGewicht = _RezeptChargeMin.TeigGewicht
        _RezeptChargeOpt.MengeInkg = DataGridView.Field("RZ_Charge_Opt")
        _RezeptChargeMax.TeigGewicht = _RezeptChargeMin.TeigGewicht
        _RezeptChargeMax.MengeInkg = DataGridView.Field("RZ_Charge_Max")

        'MultiSelect
        For Each dl As Windows.Forms.DataGridViewRow In DataGridView.SelectedRows
            Dim RezListenElement As New wb_StatistikListenElement
            RezListenElement.Nr = dl.Cells("RZ_Nr").Value
            RezListenElement.Nummer = dl.Cells("RZ_Nr_AlNum").Value
            RezListenElement.Bezeichnung = dl.Cells("RZ_Bezeichnung").Value

            _RezeptListe.Add(RezListenElement)
        Next
    End Sub

End Class