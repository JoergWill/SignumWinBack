Imports System.Reflection

Public Class wb_SiloSackware

    Private _IstMenge As Integer
    Private _RestMenge As Integer
    Private _VerbrauchtMenge As Double = 0.0
    Private _TaraWert As Integer
    Private _ChargenNummer As String
    Private _Preis As Double
    Private _SiloReiheMaxMenge As Integer = wb_Global.UNDEFINED
    Private _Befuellung As Boolean = False
    Private _KompNr As Integer
    Private _KompNummer As String
    Private _KompBezeichnung As String
    Private _Aktiv As String

    Private _pnlSiloHeight As Integer
    Private _pnlSiloTop As Integer
    Const _pnlSiloMinHeight = 80

    Private _LagerOrt As String
    Private _SiloNr As Integer
    Private _RohSiloType As wb_Global.RohSiloTypen = wb_Global.RohSiloTypen.UNDEF

    Private _parentStep As wb_SiloSackware
    Private _childSteps As New ArrayList()

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
    End Sub

    '' <summary>
    '' Create a new step with the given parent
    '' </summary>
    '' <param name="parent">The parent step</param>
    '' <param name="name">The name of this step</param>
    Public Sub New(parent As wb_SiloSackware, Bezeichnung As String)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        _parentStep = parent
        _KompBezeichnung = Bezeichnung

        'Es gibt keinen Root-Knoten (erster Knoten in der Reihe)
        If Not (_parentStep Is Nothing) Then
            parent._childSteps.Add(Me)
        End If
    End Sub

    Public Function CopyFrom(Nummer As String) As Boolean
        'Return-Wert vorbelegen
        CopyFrom = False
        'Prüfen ob es eine passende Handkomponente zu dieser Artikelnummer gibt 
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Handkomponente zu dieser Rohstoff-Nummer
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlHandkomponenten, Nummer))
        If winback.Read Then
            KompNr = winback.iField("KO_Nr")
            KompBezeichnung = winback.sField("KO_Bezeichnung")
            IstMenge = winback.sField("LG_Bilanzmenge")
            'MindestMenge = winback.sField("LG_Mindestmenge")

            KompNummer = Nummer
            CopyFrom = True
        End If
        _TaraWert = 0
        winback.Close()
    End Function

    Public WriteOnly Property Befuellung As Boolean
        Set(value As Boolean)
            _Befuellung = value
            tbBefMenge.Visible = value
            lbBefMenge.Visible = value
            BtnSiloNull.Visible = value
            BtnSiloTauschen.Visible = value
        End Set
    End Property

    Public Property RestMenge As Integer
        Get
            Return _RestMenge
        End Get
        Set(value As Integer)
            _RestMenge = value
        End Set
    End Property

    Public ReadOnly Property BefMenge As Integer
        Get
            Return wb_Functions.StrToInt(tbBefMenge.Text)
        End Get
    End Property

    Public ReadOnly Property TaraWert As Integer
        Get
            Return _TaraWert
        End Get
    End Property

    Private Sub tbLieferung_TextChanged(sender As Object, e As EventArgs) Handles tbBefMenge.TextChanged
        tbBefMenge.Text = BefMenge.ToString & " kg"
        wb_Rohstoffe_Shared.BefMenge_Changed(sender)
    End Sub

    Private Sub tbLieferung_DoubleClick(sender As Object, e As EventArgs) Handles tbBefMenge.DoubleClick
        If BefMenge + RestMenge > 0 Then
            tbBefMenge.Text = (BefMenge + RestMenge).ToString & " kg"
            wb_Rohstoffe_Shared.BefMenge_Changed(sender)
        End If
    End Sub

    ''' <summary>
    ''' Lagerbestand
    ''' </summary>
    ''' <returns></returns>
    Public Property IstMenge As Integer
        Get
            Return _IstMenge - _TaraWert
        End Get
        Set(value As Integer)
            'Silo-Füllstand aktualisieren
            _IstMenge = value - _TaraWert
            'Prüfen welches Silo das aktive Silo ist
            CheckBtn()

            'Textfeld rot anzeigen wenn Füllstand negativ
            If _IstMenge >= 0 Then
                tbIst.ForeColor = System.Drawing.Color.Black
                tbIst.Text = _IstMenge & " kg"
            Else
                tbIst.Text = _IstMenge & " kg"
                tbIst.BackColor = tbIst.BackColor
                tbIst.ForeColor = System.Drawing.Color.Red
            End If
        End Set
    End Property

    ''' <summary>
    ''' Komponenten-Nummer(Inter) zum Silo-Rohstoff
    ''' </summary>
    ''' <returns></returns>
    Public Property KompNr As Integer
        Get
            Return _KompNr
        End Get
        Set(value As Integer)
            _KompNr = value
        End Set
    End Property

    ''' <summary>
    ''' Rohstoff-Nummer(ASCII) zum Silo
    ''' </summary>
    ''' <returns></returns>
    Public Property KompNummer As String
        Get
            Return _KompNummer
        End Get
        Set(value As String)
            _KompNummer = value
            lblNummer.Text = "Nr. " & _KompNummer
        End Set
    End Property

    ''' <summary>
    ''' Rohstoff-Bezeichnung zum Silo
    ''' </summary>
    ''' <returns></returns>
    Public Property KompBezeichnung As String
        Get
            Return _KompBezeichnung
        End Get
        Set(value As String)
            _KompBezeichnung = value
            lblRohName.Text = _KompBezeichnung
        End Set
    End Property

    ''' <summary>
    ''' Lagerort zum Silo. Anhand der Lagerort-Bezeichnung wird auch die Silo-Type bestimmt
    '''     - (M)   Mehlsilo
    '''     - (MK)  Mittelkomponenten
    '''     - (KKA) Kleinkomponenten
    '''     - (BW)  Bodenwaage
    ''' </summary>
    ''' <returns></returns>
    Public Property LagerOrt As String
        Get
            Return _LagerOrt
        End Get
        Set(value As String)
            _LagerOrt = value
            _RohSiloType = wb_Rohstoffe_Shared.GetRohSiloType(_LagerOrt)
        End Set
    End Property

    ''' <summary>
    ''' Silonummer
    ''' </summary>
    ''' <returns></returns>
    Public Property SiloNr As Integer
        Get
            Return _SiloNr
        End Get
        Set(value As Integer)
            _SiloNr = value
            If _RohSiloType = wb_Global.RohSiloTypen.BW Then
                lblName.Text = "Beh." & _SiloNr.ToString
            Else
                lblName.Text = "Silo " & _SiloNr.ToString
            End If
        End Set
    End Property

    ''' <summary>
    ''' Silo-Type. Wird aus dem Lagerort ermittelt
    ''' 
    '''     - (M)   Mehlsilo
    '''     - (MK)  Mittelkomponenten
    '''     - (KKA) Kleinkomponenten
    '''     - (BW)  Bodenwaage
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RohSiloType As wb_Global.RohSiloTypen
        Get
            Return _RohSiloType
        End Get
    End Property

    Public Property ChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
        Set(value As String)
            _ChargenNummer = value
        End Set
    End Property

    Public Property Preis As Double
        Get
            Return _Preis
        End Get
        Set(value As Double)
            _Preis = value
        End Set
    End Property

    Public Property VerbrauchtMenge As Double
        Get
            Return _VerbrauchtMenge
        End Get
        Set(value As Double)
            _VerbrauchtMenge = value
        End Set
    End Property

    Private Sub CheckBtn()
        If wb_Rohstoffe_Shared.RohStoff.Nr = _KompNr Then
            BtnSiloTauschen.Visible = True
        Else
            BtnSiloTauschen.Visible = False
        End If
    End Sub

End Class
