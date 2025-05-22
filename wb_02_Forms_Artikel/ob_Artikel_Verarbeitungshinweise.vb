Imports System.Windows.Forms
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI

Public Class ob_Artikel_Verarbeitungshinweise
    Implements IBasicFormUserControl

    Private dpi As String = ""
    Private Page As Integer = 1
    Private MaxPages As Integer = 1
    Private PdfName As String
    Private PfdVerzeichnis As String
    Private KomponType As Integer = wb_Global.UNDEFINED
    Private RzNr As Integer = wb_Global.UNDEFINED

    'setzt das Flag _Extendee.Changed in ob_Artikel_DockingExtension
    Public Event DataInvalidated()
    Public Event DataUpdate()

#Region "Signum"
    ''' <summary>
    ''' Eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn
    ''' </summary>
    Public ReadOnly Property FormKey As String Implements IBasicFormUserControl.FormKey
        Get
            Return "@ob_ArtikelDocking_Verarbeitungshinweise"
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

    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S1186:Methods should not be empty", Justification:="<Ausstehend>")>
    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
    End Sub
#End Region

    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        Select Case CommandId
            Case "INVALID"
                lblArtikelHinweis.Text = ""
                cbAufloesung.Text = ""
                ShowElements(False, False)

            Case "wbFOUND"
                'Debug.Print("Artikel_Verarbeitungshinweise: wbFOUND")
                KomponType = DirectCast(Parameter, wb_Komponente).Type
                RzNr = DirectCast(Parameter, wb_Komponente).RzNr

                'Artikel-Verabeitungshinweise nicht für Rohstoffe/Artikel ohne Rezept-Zuordnung
                ShowElements(True, (RzNr > 0))

                'Datei-Name Verarbeitungshinweis (pdf)
                PdfName = DirectCast(Parameter, wb_Komponente).VerarbeitungsHinweise
                'Verzeichnis Verarbeitungshinweis (pdf)
                PfdVerzeichnis = DirectCast(Parameter, wb_Komponente).VerarbeitungsHinweisePfad
                'Auflösung (Umwandlung pdf nach png)
                dpi = DirectCast(Parameter, wb_Komponente).VerarbeitungsHinweise_DPI
                If dpi <> "" AndAlso dpi <> "0" Then
                    cbAufloesung.Text = dpi & " dpi"
                Else
                    cbAufloesung.Text = "Default"
                End If

                'PDF-Seite anzeigen
                Page = 1
                ShowPDF(Page)

            Case "wbNOPRODUCTION"
                ShowElements(False, False)

            Case "wbSAVE"
                'Daten in der Komponenten-Klasse sichern (wenn Daten geändert worden sind)
                DirectCast(Parameter, wb_Komponente).VerarbeitungsHinweise = PdfName
                DirectCast(Parameter, wb_Komponente).VerarbeitungsHinweisePfad = PfdVerzeichnis
                DirectCast(Parameter, wb_Komponente).VerarbeitungsHinweise_DPI = dpi

        End Select
        Return Nothing
    End Function

    Private Sub ShowPDF(Seite As Integer)
        'wenn ein Artikel-Verarbeitungshinweis vorhanden ist
        If PfdVerzeichnis <> "" AndAlso (KomponType = wb_Global.KomponTypen.KO_TYPE_ARTIKEL) Then
            'Mauszeiger anpassen
            Me.Cursor = Cursors.WaitCursor
            'Dateiname Artikelhinweis (pdf)
            tHinweisName.Text = PdfName & ".pdf"
            'Dateiname pdf-File
            wb_ShowPDF.ShowPdfDokument(PfdVerzeichnis & "\" & PdfName & ".pdf", VorschauPDF, Seite, dpi)
            MaxPages = wb_ShowPDF.MaxPages
        Else
            VorschauPDF.Image = Nothing
            MaxPages = 1
        End If
        'Mauszeiger anpassen
        Me.Cursor = Cursors.Default
        'Anzeigefelder ein/ausblenden
        ShowElements(True, (RzNr > 0))
    End Sub

    ''' <summary>
    ''' Eingabefelder anzeigen/verbergen Enable/Disable abhängig von Komponenten-Type und Artikelgruppe (WinBack)
    ''' </summary>
    ''' <param name="Show"></param>
    ''' <param name="Enable"></param>
    Private Sub ShowElements(Show As Boolean, Enable As Boolean)
        'Eingabe-Elemente anzeigen
        Panel1.Visible = Show
        Panel2.Visible = Show
        'Eingabe-Elemente freigeben
        If Show Then
            Panel1.Enabled = Enable
            Panel2.Enabled = Enable
        End If

        'wenn mehrere Seiten im pdf vorhanden sind, werden die Buttons angezeigt
        If MaxPages > 1 Then
            BtnPageMinus.Visible = True
            BtnPagePlus.Visible = True
        Else
            BtnPageMinus.Visible = False
            BtnPagePlus.Visible = False
        End If

    End Sub

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
        MyBase.Text = "Artikel Verarbeitungshinweise"
        'Formular anzeigen
        Me.Show()
        Return True
    End Function

    <CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Ungelesene private Member entfernen", Justification:="<Ausstehend>")>
    Private _DockingExtension As IDockingExtension

    Public Sub New(DockingExtension As IDockingExtension)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _DockingExtension = DockingExtension
    End Sub

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1066:Collapsible ""if"" statements should be merged", Justification:="<Ausstehend>")>
    Private Sub BtnLoadPdf_Click(sender As Object, e As EventArgs) Handles BtnLoadPdf.Click
        'Nur für WinBack-Verkaufsartikel
        If KomponType = wb_Global.KomponTypen.KO_TYPE_ARTIKEL OrElse KomponType = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE Then
            'Datei-Auswahl-Dialog
            If OpenPdfFile.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                'Daten geändert -speichern bei Ende
                RaiseEvent DataInvalidated()
                'Pfad und Datei-Name
                PdfName = IO.Path.GetFileNameWithoutExtension(OpenPdfFile.SafeFileName)
                PfdVerzeichnis = IO.Path.GetDirectoryName(OpenPdfFile.FileName)

                'Anzahl der Seiten im pdf
                Page = 1
                ShowPDF(Page)
            End If
        End If
    End Sub

    Private Sub BtnTransferPdf_Click(sender As Object, e As EventArgs) Handles BtnTransferPdf.Click
        'Mauszeiger anpassen
        Me.Cursor = Cursors.WaitCursor

        'Artikelhinweise per FTP an WinBack-Server übertragen
        If Not wb_ShowPDF.TransferPdfDokument(PdfName, PfdVerzeichnis, VorschauPDF, dpi) Then
            Select Case wb_ShowPDF.ErrCode
                Case wb_ShowPDF.ErrorCodes.ErrLoadPdf
                    MsgBox(wb_ShowPDF.ErrText, MsgBoxStyle.Critical, "Verarbeitungshinweise")
                Case wb_ShowPDF.ErrorCodes.ErrFTP
                    MsgBox(wb_ShowPDF.ErrText, MsgBoxStyle.Exclamation, "Fehler Artikel-Hinweis")
            End Select
        End If

        'Mauszeiger anpassen
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BtnRotateR_Click(sender As Object, e As EventArgs) Handles BtnRotateR.Click
        VorschauPDF.Image.RotateFlip(Drawing.RotateFlipType.Rotate90FlipNone)
        VorschauPDF.Refresh()
    End Sub

    Private Sub BtnRotateL_Click(sender As Object, e As EventArgs) Handles BtnRotateL.Click
        VorschauPDF.Image.RotateFlip(Drawing.RotateFlipType.Rotate270FlipNone)
        VorschauPDF.Refresh()
    End Sub

    Private Sub BtnPageMinus_Click(sender As Object, e As EventArgs) Handles BtnPageMinus.Click
        If Page > 1 Then
            Page = Page - 1
            'pdf-File anzeigen
            ShowPDF(Page)
        End If
    End Sub

    Private Sub BtnPagePlus_Click(sender As Object, e As EventArgs) Handles BtnPagePlus.Click
        If Page < MaxPages Then
            Page = Page + 1
            'pdf-File neu einlesen
            ShowPDF(Page)
        End If
    End Sub

    Private Sub cbAufloesung_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAufloesung.SelectedIndexChanged
        'Daten geändert -speichern bei Ende
        RaiseEvent DataInvalidated()

        'Auflösung einstellen
        If cbAufloesung.SelectedIndex > 0 Then
            dpi = wb_Functions.StrToInt(cbAufloesung.Text).ToString
        End If
        'PDF-Seite anzeigen
        Page = 1
        ShowPDF(Page)
        'Focus neu setzen
        cbAufloesung.SelectionLength = 0
        BtnTransferPdf.Focus()
    End Sub

End Class
