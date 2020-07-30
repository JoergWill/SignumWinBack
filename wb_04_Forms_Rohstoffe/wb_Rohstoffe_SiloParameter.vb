Public Class wb_Rohstoffe_SiloParameter

    Private _KompNr As Integer
    Private _KompNummer As String
    Private _KompBezeichnung As String
    Private _LagerOrt As String
    Private _SiloNr As Integer

    Public Sub CopyFrom(Silo As wb_Silo)
        KompNr = Silo.KompNr
        KompNummer = Silo.KompNummer
        KompBezeichnung = Silo.KompBezeichnung
        LagerOrt = Silo.LagerOrt
        SiloNr = Silo.SiloNr
    End Sub

    Public Property KompNr As Integer
        Get
            Return _KompNr
        End Get
        Set(value As Integer)
            _KompNr = value
        End Set
    End Property

    Public Property KompNummer As String
        Get
            Return _KompNummer
        End Get
        Set(value As String)
            _KompNummer = value
        End Set
    End Property

    Public Property KompBezeichnung As String
        Get
            Return _KompBezeichnung
        End Get
        Set(value As String)
            _KompBezeichnung = value
        End Set
    End Property

    Public Property LagerOrt As String
        Get
            Return _LagerOrt
        End Get
        Set(value As String)
            _LagerOrt = value
        End Set
    End Property

    Public Property SiloNr As Integer
        Get
            Return _SiloNr
        End Get
        Set(value As Integer)
            _SiloNr = value
        End Set
    End Property

    Public Sub New(Silo As wb_Silo)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        CopyFrom(Silo)
    End Sub

    Private Sub wb_Rohstoffe_SiloParameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Fenster-Text
        Me.Text = "Parameter Silo " & SiloNr.ToString & " - " & KompNummer & " " & KompBezeichnung
    End Sub
End Class