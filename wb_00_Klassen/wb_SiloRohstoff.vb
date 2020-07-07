Public Class wb_SiloRohstoff
    Private _KompNr As Integer
    Private _KompNummer As String
    Private _KompBezeichnung As String
    Private _LagerOrt As String
    Private _SiloNr As Integer
    Private _MaxMenge As Integer
    Private _IstMenge As Integer
    Private _RohSiloType As wb_Global.RohSiloTypen = wb_Global.RohSiloTypen.UNDEF
    Private _SiloReiheMaxMenge As Integer = wb_Global.UNDEFINED
    Private _parentStep As wb_SiloRohstoff
    Private _childSteps As New ArrayList()
    Public Silo As New wb_Silo()

    '' <summary>
    '' Create a new step with the given parent
    '' </summary>
    '' <param name="parent">The parent step</param>
    '' <param name="name">The name of this step</param>
    Public Sub New(parent As wb_SiloRohstoff, Bezeichnung As String)
        _parentStep = parent
        _KompBezeichnung = Bezeichnung
        'Es gibt keinen Root-Knoten (erster Knoten in der Reihe)
        If Not (_parentStep Is Nothing) Then
            parent._childSteps.Add(Me)
        End If
    End Sub

    '' <summary>
    '' Parent dieses Silos
    '' </summary>
    Public Property ParentStep() As wb_SiloRohstoff
        Get
            Return _parentStep
        End Get
        Set(ByVal value As wb_SiloRohstoff)
            _parentStep = value
        End Set
    End Property

    '' <summary>
    '' Liste aller Silos mit gleicher Rohstoff-Nummer
    '' </summary>
    Public ReadOnly Property ChildSteps() As IList
        Get
            Return _childSteps
        End Get
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
            Silo.lblNummer.Text = "Nr. " & _KompNummer
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
            Silo.lblRohName.Text = _KompBezeichnung
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
                Silo.lblName.Text = "Beh." & _SiloNr.ToString
            Else
                Silo.lblName.Text = "Silo " & _SiloNr.ToString
            End If
        End Set
    End Property

    ''' <summary>
    ''' Maximale Füllmenge. Entspricht dem Fassungsvermögen des Silos.
    ''' Steht in der Datenbank im Datenfeld LG_Kommentar
    ''' </summary>
    ''' <returns></returns>
    Public Property MaxMenge As Integer
        Get
            Return _MaxMenge
        End Get
        Set(value As Integer)
            _MaxMenge = value
            Silo.MaxMenge = value
        End Set
    End Property

    ''' <summary>
    ''' Silo-Füllstand
    ''' </summary>
    ''' <returns></returns>
    Public Property IstMenge As Integer
        Get
            Return _IstMenge
        End Get
        Set(value As Integer)
            _IstMenge = value
            Silo.Istmenge = value
        End Set
    End Property

    ''' <summary>
    ''' Größtes Silo in der Silo-Reihe.
    ''' Bestimmt den Faktor der Darstellung der Silo-Füllstände
    ''' </summary>
    Public WriteOnly Property SiloReiheMaxMenge As Integer
        Set(value As Integer)
            'Füllmenge des größten Silos der Silo-Reihe
            Silo.SiloReiheMaxMenge = value
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
End Class
