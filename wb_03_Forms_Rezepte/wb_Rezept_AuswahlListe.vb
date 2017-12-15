Public Class wb_Rezept_AuswahlListe
    Private _RezeptNr As Integer = wb_Global.UNDEFINED
    Private _RezeptNummer As String = ""

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
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridView_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView.DoubleClick
        _RezeptNr = DataGridView.iField("RZ_Nr")
        _RezeptNummer = DataGridView.iField("RZ_Nr_AlNum")
        Me.Close()
    End Sub

End Class