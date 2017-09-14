Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI
Imports WinBack.wb_Sql_Selects

Public Class ob_Artikel_VerwendungRezept
    Implements IBasicFormUserControl
    Private Nr As Integer = 0

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

    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        Select Case CommandId
            Case "INVALID"
                Nr = 0

            Case "VALID"

            Case "wbFOUND"
                Nr = DirectCast(Parameter, wb_Komponenten).Nr
                'Liste der Tabellen-Überschriften
                'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
                'Spalten ohne Bezeichnung werden ausgeblendet.
                'Die Rezept-Variante wird nicht mit ausgegeben, da sonst eine Exception auftritt
                Dim sColNames As New List(Of String) From {"Nr", "&Bezeichnung"}
                For Each sName In sColNames
                    HisDataGridView.ColNames.Add(sName)
                Next

                'DataGrid füllen
                HisDataGridView.LoadData(setParams(sqlRohstoffUse, Nr), "RohstoffVerwendung")

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
        MyBase.Text = "Rohstoff Verwendung im Rezept"
        Me.Show()
        Return True
    End Function

    Private _DockingExtension As IDockingExtension

    Public Sub New(DockingExtension As IDockingExtension)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _DockingExtension = DockingExtension

    End Sub

End Class
