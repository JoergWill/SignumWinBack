Public Class wb_Silo

    Private _MaxMenge As Integer
    Private _IstMenge As Integer
    Private _SiloReiheMaxMenge As Integer = wb_Global.UNDEFINED
    Private _pnlSiloHeight As Integer
    Private _pnlSiloTop As Integer

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        _pnlSiloHeight = pnlSilo.Height
        _pnlSiloTop = pnlSilo.Top
    End Sub


    Public Property MaxMenge As Integer
        Set(value As Integer)
            _MaxMenge = value
            'Textfeld ausblenden wenn keine Silo-Größe angegeben ist
            If _MaxMenge > 0 Then
                tbMax.Text = _MaxMenge & " kg"
                tbMax.Visible = True
                lbMax.Visible = True
            Else
                tbMax.Visible = False
                lbMax.Visible = False
            End If
        End Set
        Get
            Return _MaxMenge
        End Get
    End Property

    Public Property SiloReiheMaxMenge As Integer
        Get
            Return _SiloReiheMaxMenge
        End Get
        Set(value As Integer)
            _SiloReiheMaxMenge = value

            'Höhe der Silo-Grafik im Verhältnis zu den anderen Silos der Reihe
            If _SiloReiheMaxMenge > 0 Then
                pnlSilo.Height = _pnlSiloHeight * _MaxMenge / _SiloReiheMaxMenge
                pnlSilo.Top = _pnlSiloTop + _pnlSiloHeight - pnlSilo.Height
            Else
                pnlSilo.Height = _pnlSiloHeight
                pnlSilo.Top = _pnlSiloTop
            End If
        End Set
    End Property

    Public Property IstMenge As Integer
        Set(value As Integer)
            _IstMenge = value
            'Textfeld ausblenden wenn keine Silo-Größe angegeben ist
            If _IstMenge > 0 Then
                tbIst.Text = _IstMenge & " kg"
                tbIst.Visible = True
                lbIst.Visible = True
            Else
                tbIst.Visible = False
                lbIst.Visible = False
            End If
        End Set
        Get
            Return _IstMenge
        End Get
    End Property
End Class
