Imports WinBack

Public Class wb_Rohstoff_AuswahlListe
    Private _RohstoffNr As Integer = wb_Global.UNDEFINED
    Private _RohstoffNummer As String = ""
    Private _RohstoffName As String = ""
    Private _RohstoffType As wb_Global.KomponTypen
    Private _RohstoffEinheit As String = ""

    Public Property RohstoffNr As Integer
        Get
            Return _RohstoffNr
        End Get
        Set(value As Integer)
            _RohstoffNr = value
        End Set
    End Property

    Public Property RohstoffNummer As String
        Get
            Return _RohstoffNummer
        End Get
        Set(value As String)
            _RohstoffNummer = value
        End Set
    End Property

    Public Property RohstoffName As String
        Get
            Return _RohstoffName
        End Get
        Set(value As String)
            _RohstoffName = value
        End Set
    End Property

    Public Property RohstoffType As wb_Global.KomponTypen
        Get
            Return _RohstoffType
        End Get
        Set(value As wb_Global.KomponTypen)
            _RohstoffType = value
        End Set
    End Property

    Public Property RohstoffEinheit As String
        Get
            Return _RohstoffEinheit
        End Get
        Set(value As String)
            _RohstoffEinheit = value
        End Set
    End Property

    Private Sub wb_Rezept_AuswahlListe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Nummer", "&Name"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRohstoffLst, "RohstoffListe")
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridView_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView.DoubleClick
        RohstoffNr = DataGridView.iField("KO_Nr")
        RohstoffNummer = DataGridView.Field("KO_Nr_AlNum")
        RohstoffName = DataGridView.Field("KO_Bezeichnung")
        RohstoffType = wb_Functions.IntToKomponType(DataGridView.iField("KO_Type"))
        RohstoffEinheit = DataGridView.Field("E_Einheit")
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

End Class