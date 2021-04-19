Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI

Public Class ob_Artikel_VerwendungRezept
    Implements IBasicFormUserControl
    Private NrAkt As Integer = 0
    Private NummerAkt As String = Nothing
    Private BezeichnungAkt As String = Nothing

#Region "Signum"
    ''' <summary>
    ''' Eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn
    ''' </summary>
    Public ReadOnly Property FormKey As String Implements IBasicFormUserControl.FormKey
        Get
            Return "@ob_ArtikelDocking_VerwendungRezept"
        End Get
    End Property

    ''' <summary>
    ''' Minimale Höhe des UserControls
    ''' </summary>
    Public ReadOnly Property MinHeight As Integer Implements IBasicFormUserControl.MinHeight
        Get
            Return Me.MinimumSize.Height
        End Get
    End Property

    ''' <summary>
    ''' Minimale Breite des UserControls
    ''' </summary>
    Public ReadOnly Property MinWidth As Integer Implements IBasicFormUserControl.MinWidth
        Get
            Return Me.MinimumSize.Width
        End Get
    End Property

    ''' <summary>
    ''' Gibt an, ob man die Größe dieses UserControls ändern darf
    ''' </summary>
    Public ReadOnly Property Sizable As Boolean Implements IBasicFormUserControl.Sizable
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Bezeichnung und Caption des UserControls
    ''' </summary>
    Public Shadows ReadOnly Property Text() As String Implements IBasicFormUserControl.Text
        Get
            Return MyBase.Text
        End Get
    End Property

    Public Event Close As EventHandler Implements IBasicFormUserControl.Close

    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
    End Sub
#End Region

    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        Select Case CommandId
            Case "INVALID"
                NrAkt = 0
                'Tabelle Verwendung leeren
                HisDataGridView.ClearVerwendung()
            Case "VALID"

            Case "wbFOUND"
                Debug.Print("Artikel_VerwendungRezept: wbFOUND")
                NrAkt = DirectCast(Parameter, wb_Komponente).Nr
                NummerAkt = DirectCast(Parameter, wb_Komponente).Nummer
                BezeichnungAkt = DirectCast(Parameter, wb_Komponente).Nummer
                'Tabelle Verwendung mit Daten füllen
                HisDataGridView.LoadVerwendung(NrAkt)
        End Select
        Return Nothing
    End Function

    ''' <summary>
    ''' Diese Function wird aufgerufen, wenn das Fenster geschlossen werden soll.
    ''' </summary>
    ''' <param name="Reason"></param>
    ''' <returns>
    ''' False, wenn das Fenster geschlossen werden darf
    ''' True, wenn das Fenster geöffnet bleiben muss
    ''' </returns>
    ''' <remarks></remarks>
    Public Function FormClosing(Reason As Short) As Boolean Implements IBasicFormUserControl.FormClosing
        Return False
    End Function

    Public Function Init() As Boolean Implements IBasicFormUserControl.Init
        Trace.WriteLine("Init()")
        MyBase.Text = "Rohstoff Verwendung im Rezept"
        'Rohstoff-tauschen im Popup-Menu
        HisDataGridView.PopupItemAdd("Rohstoff in Rezepten ersetzen", "", Nothing, AddressOf RohstoffeTauschen, True)
        'Formular anzeigen
        Me.Show()
        Return True
    End Function

    Private _DockingExtension As IDockingExtension

    Public Sub New(DockingExtension As IDockingExtension)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _DockingExtension = DockingExtension

        Trace.WriteLine("Sub New(Docking Extension)")
    End Sub

    Private Sub RohstoffeTauschen()
        'Dialog-Fenster Rohstoff im Rezept tauschen/ersetzen
        Dim RohstoffeTauschen As New wb_Rohstoffe_Tauschen(NrAkt, NummerAkt, BezeichnungAkt)
        ''aktueller Rohstoff (OrgaBack)
        'RohstoffeTauschen.NummerAkt = NummerAkt
        'RohstoffeTauschen.BezeichnungAkt = BezeichnungAkt
        'RohstoffeTauschen.NrAkt = NrAkt
        'wenn Rezepturen geändert worden sind, wird die Anzeige aktualisiert
        If RohstoffeTauschen.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'Liste aktualisieren
            HisDataGridView.ClearVerwendung()
            HisDataGridView.LoadVerwendung(NrAkt)
        End If
    End Sub

End Class
