Public Class wb_Rezept_AuswahlListe
    Private _RezeptNr As Integer = wb_Global.UNDEFINED
    Private _RezeptNummer As String = ""
    Private _RezeptName As String = ""
    Private _RezeptListe As New ArrayList
    Private _MultiSelect As Boolean = False

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

    Public WriteOnly Property MultiSelect As Boolean
        Set(value As Boolean)
            _MultiSelect = value
        End Set
    End Property

    Public Property RezeptName As String
        Get
            Return _RezeptName
        End Get
        Set(value As String)
            _RezeptName = value
        End Set
    End Property

    Private Sub wb_Rezept_AuswahlListe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        GetResult()
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

        'MultiSelect
        For Each dl As Windows.Forms.DataGridViewRow In DataGridView.SelectedRows
            Dim RezListenElement As New wb_StatistikListenElement
            RezListenElement.Nr = dl.Cells("RZ_Nr").Value
            RezListenElement.Nummer = dl.Cells("RZ_Nr_AlNum").Value
            RezListenElement.Bezeichnung = dl.Cells("RZ_Bezeichnung").Value

            _RezeptListe.Add(RezListenElement)
        Next

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class