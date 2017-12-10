Imports System.Windows.Forms
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI

Public Class ob_Artikel_ZuordnungRezept
    Implements IBasicFormUserControl
    Private RzNr As Integer = 0

#Region "Signum"

    Private _DockingExtension As IDockingExtension

    Public Sub New(DockingExtension As IDockingExtension)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _DockingExtension = DockingExtension

    End Sub

    ''' <summary>
    ''' Eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn
    ''' </summary>
    Public ReadOnly Property FormKey As String Implements IBasicFormUserControl.FormKey
        Get
            Return "@ob_ArtikelDocking_ZuordnungRezept"
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

    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
    End Sub
#End Region

    Public Function Init() As Boolean Implements IBasicFormUserControl.Init
        MyBase.Text = "WinBack Artikel-Rezept-Zuordnung"

        'Form anzeigen
        Me.Show()

        'ComboBox Liniengruppe füllen
        cbLiniengruppe.Fill(wb_Linien_Global.LinienGruppen)
        cbArtikelLinienGruppe.Fill(wb_Linien_Global.LinienGruppen)

        Return True
    End Function

    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        Select Case CommandId
            Case "INVALID"

                'alle Steuerelemente sperren
                EnableKomponenten(False)

                RzNr = 0
                tRezeptNr.Text = ""
                tRezeptName.Text = ""

            Case "VALID"

            Case "wbFOUND"
                RzNr = DirectCast(Parameter, wb_Komponenten).RzNr
                tRezeptNr.Text = DirectCast(Parameter, wb_Komponenten).RezeptNummer
                tRezeptName.Text = DirectCast(Parameter, wb_Komponenten).RezeptName

                cbLiniengruppe.SetTextFromKey(DirectCast(Parameter, wb_Komponenten).LinienGruppe)
                cbArtikelLinienGruppe.SetTextFromKey(DirectCast(Parameter, wb_Komponenten).ArtikelLinienGruppe)

                tStkGewicht.Text = DirectCast(Parameter, wb_Komponenten).StkGewicht

                'alle Steuerelemente aktivieren
                EnableKomponenten(True)

        End Select
        Return Nothing
    End Function

    Private Sub BtnRzpShow_Click(sender As Object, e As EventArgs) Handles BtnRzpShow.Click
        Me.Cursor = Cursors.WaitCursor
        Dim Rezeptur As New wb_Rezept_Rezeptur(RzNr, 1)
        'MDI-Fenster anzeigen
        Rezeptur.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub EnableKomponenten(Enable As Boolean)
        BtnRzpShow.Enabled = Enable
        BtnRzptChange.Enabled = Enable

        cbLiniengruppe.Enabled = Enable
        cbArtikelLinienGruppe.Enabled = Enable
    End Sub
End Class
