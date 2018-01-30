Imports System.Windows.Forms
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI

Public Class ob_Artikel_ZuordnungRezept
    Implements IBasicFormUserControl
    Private RzNr As Integer = 0
    Private WithEvents ArtikelChargen As New wb_MinMaxOptCharge
    Private WithEvents TeigChargen As New wb_MinMaxOptCharge
    Private OnErrorSetFocus As Object


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
        MyBase.Text = "WinBack Artikel Produktions-Parameter"

        'Form anzeigen
        Me.Show()

        'ComboBox Liniengruppe Rezepte(Teig) füllen
        cbLiniengruppe.Fill(wb_Linien_Global.LinienGruppen)
        'ComboBox Liniengruppe Artikel füllen
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
                '(interne) Rezeptnummer
                RzNr = DirectCast(Parameter, wb_Komponenten).RzNr
                tRezeptNr.Text = DirectCast(Parameter, wb_Komponenten).RezeptNummer
                tRezeptName.Text = DirectCast(Parameter, wb_Komponenten).RezeptName

                'Chargengrößen Artikel(Objekt)
                ArtikelChargen = DirectCast(Parameter, wb_Komponenten).ArtikelChargen
                'Chargengrößen Rezept(Objekt)
                TeigChargen = DirectCast(Parameter, wb_Komponenten).TeigChargen
                'Chargengrößen Artikel anzeigen
                MinMaxOptArtikelShowValues()
                'Chargengrößen Rezept(Teig) anzeigen
                MinMaxRezeptShowValues()
                'Liniengruppe Artikel
                cbArtikelLinienGruppe.SetTextFromKey(DirectCast(Parameter, wb_Komponenten).ArtikelLinienGruppe)
                'Liniengruppe Rezept(Teig)
                cbLiniengruppe.SetTextFromKey(DirectCast(Parameter, wb_Komponenten).LinienGruppe)

                'alle Steuerelemente aktivieren
                EnableKomponenten(True)

            Case "wbSAVE"
                DirectCast(Parameter, wb_Komponenten).ArtikelChargen = ArtikelChargen
                DirectCast(Parameter, wb_Komponenten).RzNr = RzNr

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

    Private Sub BtnRzptChange_Click(sender As Object, e As EventArgs) Handles BtnRzptChange.Click
        Dim RezeptAuswahl As New wb_Rezept_AuswahlListe
        If RezeptAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            RzNr = RezeptAuswahl.RezeptNr
            tRezeptNr.Text = RezeptAuswahl.RezeptNummer
            tRezeptName.Text = RezeptAuswahl.RezeptName
        End If

    End Sub

#Region "Änderung Chargen"
    Private Sub tStkGewicht_Leave(sender As Object, e As EventArgs) Handles tStkGewicht.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus
        ArtikelChargen.StkGewicht = tStkGewicht.Text
    End Sub

    Private Sub tChrgMinkg_Leave(sender As Object, e As EventArgs) Handles tChrgMinkg.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        ArtikelChargen.MinCharge.MengeInkg = tChrgMinkg.Text
    End Sub

    Private Sub tChrgMinPrz_Leave(sender As Object, e As EventArgs) Handles tChrgMinPrz.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        ArtikelChargen.MinCharge.MengeInProzent = tChrgMinPrz.Text
    End Sub

    Private Sub tChrgMinStk_Leave(sender As Object, e As EventArgs) Handles tChrgMinStk.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        ArtikelChargen.MinCharge.MengeInStk = tChrgMinStk.Text
    End Sub

    Private Sub tChrgMaxkg_Leave(sender As Object, e As EventArgs) Handles tChrgMaxkg.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        ArtikelChargen.MaxCharge.MengeInkg = tChrgMaxkg.Text
    End Sub

    Private Sub tChrgMaxPrz_Leave(sender As Object, e As EventArgs) Handles tChrgMaxPrz.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        ArtikelChargen.MaxCharge.MengeInProzent = tChrgMaxPrz.Text
    End Sub

    Private Sub tChrgMaxStk_Leave(sender As Object, e As EventArgs) Handles tChrgMaxStk.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        ArtikelChargen.MaxCharge.MengeInStk = tChrgMaxStk.Text
    End Sub

    Private Sub tChrgOptkg_Leave(sender As Object, e As EventArgs) Handles tChrgOptkg.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        ArtikelChargen.OptCharge.MengeInkg = tChrgOptkg.Text
    End Sub

    Private Sub tChrgOptPrz_Leave(sender As Object, e As EventArgs) Handles tChrgOptPrz.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        ArtikelChargen.OptCharge.MengeInProzent = tChrgOptPrz.Text
    End Sub
    Private Sub tChrgOptStk_Leave(sender As Object, e As EventArgs) Handles tChrgOptStk.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        ArtikelChargen.OptCharge.MengeInStk = tChrgOptStk.Text
    End Sub

    Private Sub tRezMinkg_Leave(sender As Object, e As EventArgs) Handles tRezMinkg.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        TeigChargen.MinCharge.MengeInkg = tRezMinkg.Text
    End Sub

    Private Sub tRezMinPrz_Leave(sender As Object, e As EventArgs) Handles tRezMinPrz.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        TeigChargen.MinCharge.MengeInProzent = tRezMinPrz.Text
    End Sub

    Private Sub tRezOptkg_Leave(sender As Object, e As EventArgs) Handles tRezOptkg.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        TeigChargen.OptCharge.MengeInkg = tRezOptkg.Text
    End Sub

    Private Sub tRezOptPrz_Leave(sender As Object, e As EventArgs) Handles tRezOptPrz.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        TeigChargen.OptCharge.MengeInProzent = tRezOptPrz.Text
    End Sub

    Private Sub tRezMaxkg_Leave(sender As Object, e As EventArgs) Handles tRezMaxkg.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        TeigChargen.MaxCharge.MengeInkg = tRezMaxkg.Text
    End Sub

    Private Sub tRezMaxPrz_Leave(sender As Object, e As EventArgs) Handles tRezMaxPrz.Leave
        'Objekt merken - Im Fehlerfall (Dialogbox) wird der Focus auf dieses Objekt zurückgesetzt
        OnErrorSetFocus = sender
        'geänderten Wert eintragen - löst OnChange-Ereignis aus, im Fehlerfall wird OnError ausgelöst
        TeigChargen.MaxCharge.MengeInProzent = tRezMaxPrz.Text
    End Sub

    Private Sub OnErrorMinMaxOptArtikel(sender As Object) Handles ArtikelChargen.OnError
        'TODO Artikel/Komponenten umstellen auf ChargenMengen-Klasse (Einzelwerte entfallen)
        If ArtikelChargen.ErrorCode <> wb_Global.MinMaxOptChargenError.NoError Then
            'Eingabe-Focus auf das auslösende Objekt setzen
            OnErrorSetFocus.Focus()
            'Fehlermeldung entsprechend der Eingabe-Felder ausgeben
            MsgBox(wb_Functions.MinMaxOptChargeToString(ArtikelChargen.ErrorCode), MsgBoxStyle.Exclamation, "Fehler bei der Eingabe der Artikel-Chargengrößen")
        End If
        'Felder neu zeichnen
        MinMaxOptArtikelShowValues()
    End Sub

    Private Sub OnErrorMinMaxOptTeig(Sender As Object) Handles TeigChargen.OnError
        If TeigChargen.ErrorCode <> wb_Global.MinMaxOptChargenError.NoError Then
            OnErrorSetFocus.focus()
            'Fehlermeldung entsprechend der Eingabe-Felder ausgeben
            MsgBox(wb_Functions.MinMaxOptChargeToString(TeigChargen.ErrorCode), MsgBoxStyle.Exclamation, "Fehler bei der Eingabe der Rezept-Chargengrößen")
        End If
        'Felder neu zeichnen
        MinMaxRezeptShowValues()
    End Sub
#End Region
    Private Sub MinMaxOptArtikelShowValues()
        tStkGewicht.Text = ArtikelChargen.StkGewicht & " gr"

        tChrgMinkg.Text = ArtikelChargen.MinCharge.MengeInkg & " kg"
        tChrgOptkg.Text = ArtikelChargen.OptCharge.MengeInkg & " kg"
        tChrgMaxkg.Text = ArtikelChargen.MaxCharge.MengeInkg & " kg"

        tChrgMinPrz.Text = ArtikelChargen.MinCharge.MengeInProzent & "%"
        tChrgOptPrz.Text = ArtikelChargen.OptCharge.MengeInProzent & "%"
        tChrgMaxPrz.Text = ArtikelChargen.MaxCharge.MengeInProzent & "%"

        tChrgMinStk.Text = ArtikelChargen.MinCharge.MengeInStk & " Stk"
        tChrgOptStk.Text = ArtikelChargen.OptCharge.MengeInStk & " Stk"
        tChrgMaxStk.Text = ArtikelChargen.MaxCharge.MengeInStk & " Stk"
    End Sub

    Private Sub MinMaxRezeptShowValues()
        tRezGesamt.Text = TeigChargen.TeigGewicht & " kg"

        tRezMinkg.Text = TeigChargen.MinCharge.MengeInkg & " kg"
        tRezOptkg.Text = TeigChargen.OptCharge.MengeInkg & " kg"
        tRezMaxkg.Text = TeigChargen.MaxCharge.MengeInkg & " kg"

        tRezMinPrz.Text = TeigChargen.MinCharge.MengeInProzent & "%"
        tRezOptPrz.Text = TeigChargen.OptCharge.MengeInProzent & "%"
        tRezMaxPrz.Text = TeigChargen.MaxCharge.MengeInProzent & "%"
    End Sub

    Private Sub EnableKomponenten(Enable As Boolean)
        BtnRzpShow.Enabled = Enable
        BtnRzptChange.Enabled = Enable

        cbLiniengruppe.Enabled = Enable
        cbArtikelLinienGruppe.Enabled = Enable

        pArtikelChargen.Enabled = Enable
        pTeigChargen.Enabled = Enable
    End Sub

End Class
