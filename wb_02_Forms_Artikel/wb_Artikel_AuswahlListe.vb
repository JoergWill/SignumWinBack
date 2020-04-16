Public Class wb_Artikel_AuswahlListe
    Private _ArtikelNr As Integer = wb_Global.UNDEFINED
    Private _ArtikelNummer As String = ""
    Private _ArtikelName As String = ""
    Private _ArtikelChargeMin As New wb_Charge
    Private _ArtikelChargeMax As New wb_Charge
    Private _ArtikelChargeOpt As New wb_Charge
    Private _ArtikelLinienGruppe As Integer
    Private _RezNr As Integer

    Private _ArtikelListe As New ArrayList
    Private _MultiSelect As Boolean = False

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        InitDataGrid()
    End Sub

    Public Sub New(ArtikelNummer As String, ArtikelName As String)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        InitDataGrid(ArtikelNummer, ArtikelName)
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

    Public ReadOnly Property ArtikelNummer As String
        Get
            Return _ArtikelNummer
        End Get
    End Property

    Public ReadOnly Property ArtikelNr As Integer
        Get
            Return _ArtikelNr
        End Get
    End Property

    Public ReadOnly Property ArtikelListe As ArrayList
        Get
            Return _ArtikelListe
        End Get
    End Property

    Public ReadOnly Property ArtikelName As String
        Get
            Return _ArtikelName
        End Get
    End Property

    Public WriteOnly Property Teiggewicht As String
        Set(value As String)
            _ArtikelChargeMin.TeigGewicht = value
            _ArtikelChargeMax.TeigGewicht = value
            _ArtikelChargeOpt.TeigGewicht = value
        End Set
    End Property

    Public ReadOnly Property ArtikelChargeMin As wb_Charge
        Get
            Return _ArtikelChargeMin
        End Get
    End Property

    Public ReadOnly Property ArtikelChargeMax As wb_Charge
        Get
            Return _ArtikelChargeMax
        End Get
    End Property

    Public ReadOnly Property ArtikelChargeOpt As wb_Charge
        Get
            Return _ArtikelChargeOpt
        End Get
    End Property

    Public ReadOnly Property ArtikelLinienGruppe As Integer
        Get
            Return _ArtikelLinienGruppe
        End Get
    End Property

    Public ReadOnly Property RezNr As Integer
        Get
            Return _RezNr
        End Get
    End Property

    Private Sub InitDataGrid(Optional ArtikelNummer As String = "", Optional ArtikelName As String = "")
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "Nummer", "&Name"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlArtikelLst, "ArtikelListe")
        DataGridView.MultiSelect = _MultiSelect

        'DataGrid filtern nach Artikel-Nummer (alpha)
        If ArtikelNummer <> "" Then
            DataGridView.Filter = "KO_Nr_AlNum = '" & ArtikelNummer & "'"
        End If
        'DataGrid filtern nach Artikel-Bezeichnung (alpha)
        If ArtikelName <> "" Then
            DataGridView.Filter = "KO_Bezeichnung LIKE '%" & ArtikelName & "%'"
        End If
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        _ArtikelNr = 0
        _ArtikelNummer = ""
        _ArtikelName = ""
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

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        GetResult()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' Ergebnis Auswahl zusammenfassen
    ''' Wenn nur eine Zeile ausgewählt worden ist, werden Artikelnummer und Bezeichnung zurückgegeben. Bei 
    ''' Multiselect nur Artikel-Index als Liste
    ''' </summary>
    Private Sub GetResult()
        'Ergebnis-Liste leeren (immer)
        _ArtikelListe.Clear()

        'Aktuelle Auswahl (MultiSelect = False)
        _ArtikelNr = DataGridView.iField("KO_Nr")
        _ArtikelNummer = DataGridView.Field("KO_Nr_AlNum")
        _ArtikelName = DataGridView.Field("KO_Bezeichnung")
        _RezNr = DataGridView.Field("KA_RZ_Nr")

        _ArtikelChargeMin.StkGewicht = DataGridView.Field("KA_Stueckgewicht")
        _ArtikelChargeMin.MengeInStk = DataGridView.Field("KA_Charge_Min")
        _ArtikelChargeOpt.StkGewicht = _ArtikelChargeMin.StkGewicht
        _ArtikelChargeOpt.MengeInStk = DataGridView.Field("KA_Charge_Opt")
        _ArtikelChargeMax.StkGewicht = _ArtikelChargeMin.StkGewicht
        _ArtikelChargeMax.MengeInStk = DataGridView.Field("KA_Charge_Max")

        'MultiSelect
        For Each dl As Windows.Forms.DataGridViewRow In DataGridView.SelectedRows
            Dim ArtListenElement As New wb_StatistikListenElement
            ArtListenElement.Nr = dl.Cells("KO_Nr").Value
            ArtListenElement.Nummer = dl.Cells("KO_Nr_AlNum").Value
            ArtListenElement.Bezeichnung = dl.Cells("KO_Bezeichnung").Value

            _ArtikelListe.Add(ArtListenElement)
        Next
    End Sub
End Class