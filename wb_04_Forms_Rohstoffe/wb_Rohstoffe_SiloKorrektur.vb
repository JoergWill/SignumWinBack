Public Class wb_Rohstoffe_SiloKorrektur

    Private _KompNr As Integer
    Private _KompNummer As String
    Private _KompBezeichnung As String
    Private _LagerOrt As String
    Private _SiloNr As Integer
    Private _Istmenge As Integer
    Private _MengeNeu As Integer = wb_Global.UNDEFINED


    Public Sub CopyFrom(Silo As wb_Silo)
        KompNr = Silo.KompNr
        KompNummer = Silo.KompNummer
        KompBezeichnung = Silo.KompBezeichnung
        LagerOrt = Silo.LagerOrt
        SiloNr = Silo.SiloNr
        Istmenge = Silo.IstMenge
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
            lblNummer.Text = "Nr. " & value
        End Set
    End Property

    Public Property KompBezeichnung As String
        Get
            Return _KompBezeichnung
        End Get
        Set(value As String)
            _KompBezeichnung = value
            lblName.Text = value
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
            lblName.Text = "Silo " & value.ToString
        End Set
    End Property

    Public Property Istmenge As Integer
        Get
            Return _Istmenge
        End Get
        Set(value As Integer)
            _Istmenge = value
            tbIst.Text = _Istmenge & " kg"
            'Textfeld rot anzeigen wenn Füllstand negativ
            If _Istmenge >= 0 Then
                tbIst.ForeColor = System.Drawing.Color.Black
            Else
                tbIst.BackColor = tbIst.BackColor
                tbIst.ForeColor = System.Drawing.Color.Red
            End If
        End Set
    End Property

    Public Property MengeNeu As Integer
        Get
            'Kein Wert eingegeben - Istwert wird übernommen
            If _MengeNeu = wb_Global.UNDEFINED Then
                tbBestNeu.Text = tbIst.Text
            End If
            Return wb_Functions.StrToInt(tbBestNeu.Text)
        End Get
        Set(value As Integer)
            _MengeNeu = value
            tbBestNeu.Text = value.ToString & " kg"
        End Set
    End Property

    Public ReadOnly Property KorrekturModus As wb_Global.KorrekturStatus
        Get
            If MengeNeu = 0 Then
                Return wb_Global.KorrekturStatus.SILO_NULLEN
            ElseIf MengeNeu < Istmenge Then
                Return wb_Global.KorrekturStatus.SILO_MINUS
            ElseIf MengeNeu > Istmenge Then
                Return wb_Global.KorrekturStatus.SILO_PLUS
            Else
                Return wb_Global.KorrekturStatus.SILO_NOP
            End If
        End Get
    End Property

    Public Sub New(Silo As wb_Silo)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        CopyFrom(Silo)
    End Sub

    ''' <summary>
    ''' Laden des Formulars.
    ''' Überschrift anpassen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Rohstoffe_SiloParameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Fenster-Text
        Me.Text = "Bestandskorrektur Silo " & SiloNr.ToString & " - " & KompNummer & " " & KompBezeichnung
    End Sub

    Private Sub tbBestNeu_Leave(sender As Object, e As EventArgs) Handles tbBestNeu.Leave
        MengeNeu = wb_Functions.StrToInt(tbBestNeu.Text)
    End Sub

    Private Sub BtnNullSetzen_Click(sender As Object, e As EventArgs) Handles BtnNullSetzen.Click
        'Neuer Bestand 0kg
        MengeNeu = 0
        'Bestandskorrektur durchführen
        BtnBestandKorrektur_Click(sender, e)
    End Sub

    ''' <summary>
    ''' Bestandskorrektur Silo.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnBestandKorrektur_Click(sender As Object, e As EventArgs) Handles BtnBestandKorrektur.Click
        'Bestandskorrektur durchführen wenn die Mengen korrigiert werden müssen
        If MengeNeu <> Istmenge Then
            DialogResult = System.Windows.Forms.DialogResult.OK
        Else
            DialogResult = System.Windows.Forms.DialogResult.Cancel
        End If
        Close()
    End Sub

End Class