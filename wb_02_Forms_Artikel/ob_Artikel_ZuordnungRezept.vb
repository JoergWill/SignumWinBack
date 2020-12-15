Imports System.Drawing
Imports System.Windows.Forms
Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.GUI

Public Class ob_Artikel_ZuordnungRezept
    Implements IBasicFormUserControl

    'setzt das Flag _Extendee.Changed in ob_Artikel_DockingExtension
    Public Event DataInvalidated()
    Public Event DataUpdate()
    Public RohstoffCloud As wb_Rohstoffe_Cloud
    Private Nr As Integer = wb_Global.UNDEFINED

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
        'Focus wb_KompRzChargen ändern (Löst Event Leave aus)
        KompRzChargen.BtnRzpt.Focus()
        'Fenster kann geschlossen werden (nach Speichern Datensatz)
        Return False
    End Function

    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
    End Sub
#End Region

    Public Function Init() As Boolean Implements IBasicFormUserControl.Init
        'Fenstertext
        MyBase.Text = "WinBack Artikel Produktions-Parameter"
        'Panel verschieben
        pnlNoProduction.Left = 0
        lblProduktion.Text = ""
        lblProduktion.Dock = DockStyle.Fill
        'Form anzeigen
        Me.Show()
        Return True
    End Function

    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        Dim k As New wb_Komponente
        k = Parameter

        Select Case CommandId
            Case "INVALID"
                'alle Steuerelemente sperren
                KompRzChargen.DataValid = False
                KompRzChargen.Visible = False
                pnlNoProduction.Visible = False
                Nr = wb_Global.UNDEFINED

            Case "VALID"

            Case "wbFOUND"
                'Daten aus der Komponenten-Klasse lesen
                KompRzChargen.GetDataFromKomp(DirectCast(k, wb_Komponente))
                Nr = DirectCast(k, wb_Komponente).Nr
                'Anzeigen der Werte
                KompRzChargen.DataValid = True
                KompRzChargen.Visible = True
                pnlNoProduction.Visible = False

            Case "wbNOPRODUCTION"
                KompRzChargen.Visible = False
                pnlNoProduction.Visible = True

            Case "wbSAVE"
                'Daten in der Komponenten-Klasse sichern
                KompRzChargen.SaveData(DirectCast(k, wb_Komponente))

            Case "wbUPDATE"
                'Daten in der Komponenten-Klasse updaten (nur RzNr, Rezeptnummer, Rezeptname)
                KompRzChargen.UpdateData(DirectCast(k, wb_Komponente))
                'anschliessed die Daten wieder (mit neuer/geänderter) Rezeptnummer aus der Komponenten-Klasse lesen
                KompRzChargen.GetDataFromKomp(DirectCast(k, wb_Komponente))
                Nr = DirectCast(k, wb_Komponente).Nr
                'Anzeigen der Werte
                KompRzChargen.DataValid = True
                KompRzChargen.Visible = True

        End Select
        Return Nothing
    End Function

    ''' <summary>
    ''' Event Daten wurden geändert von wb_KompRZChargen wird weitergegeben an ob_Artikel_DockingExtension.
    ''' Setzt dort das Flag _Extendee.Changed. Damit wird beim Schliessen des Artikelfensters die Abfrage
    ''' "Daten wurden geändert Speichern Ja/Nein/Abbrechen" ausgelöst.
    ''' 
    ''' Achtung: auch wenn bei Verweise '0' eingetragen ist, wird diese Routine aufgerufen !!
    ''' </summary>
    Public Sub KomRzChargen_DataInvalidated() Handles KompRzChargen.DataInvalidated
        RaiseEvent DataInvalidated()
    End Sub

    Public Sub KompRzChargen_DataUpdate() Handles KompRzChargen.DataUpdate
        RaiseEvent DataUpdate()
    End Sub

    Private Sub lblProduktion_Paint(sender As Object, e As PaintEventArgs) Handles lblProduktion.Paint
        e.Graphics.TranslateTransform(CSng(lblProduktion.Width / 2), CSng(lblProduktion.Height / 2))
        e.Graphics.RotateTransform(335)
        e.Graphics.DrawString("Kein WinBack" & vbCrLf & "Artikel", lblProduktion.Font, Brushes.IndianRed, New Point(-150, -80))
        e.Graphics.ResetTransform()
    End Sub

    ''' <summary>
    ''' Anzeige/Änderung der Cloud-Zuordnung (in OrgaBack)
    ''' Die Komponenten-Daten werden in wb_Rohstoffe_Shared nochmals geladen, weil daS Fenster der Cloud-Zuordnung über wb_Rohstoffe_Shared arbeitet.
    ''' Das Fenster wird modal(!) angezeigt und speichert beim Schliessen die geänderten Zuordnungsdaten und Nährwertinformationen ab.
    ''' 
    ''' Werden hier Daten geändert, muss der Rohstoff in OrgaBack neu geladen werden um die Anzeige zu aktualisieren.
    ''' 
    ''' (@V1.8.1) 14-12-2020/JW Fehler bei Fonk
    ''' Der Event Edit_Leave wird deaktiviert. Sonst wird beim Speichern der Daten der jeweils aktuelle Rohstoff aus dem Grid in wb_Rohstoff_Liste geändert.
    ''' Wenn im Hintergrund das Listen-Fenster offen ist, ist diese Liste nicht mehr aktuell !
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub Cloud_Click(sender As Object, e As EventArgs) Handles KompRzChargen.Cloud_Click
        'Daten in Rohstoff(Shared) laden
        wb_Rohstoffe_Shared.RohStoff.MySQLdbRead(Nr)
        'Fenster Cloud-Zuordnung modal anzeigen - verhindert den Event Edit_Leave beim Speichern der Daten - Sonst wird evtl. ein falscher Rohstoff geändert
        RohstoffCloud = New wb_Rohstoffe_Cloud(True)
        RohstoffCloud.ShowDialog()
        'Matchcode in aktuellen Artikel eintragen
        KompRzChargen.ID = wb_Rohstoffe_Shared.RohStoff.MatchCode
        RohstoffCloud = Nothing
        'Daten in Rohstoff(Shared) wieder löschen
        wb_Rohstoffe_Shared.RohStoff.Invalid()
    End Sub

    ''' <summary>
    ''' Nährwerte des aktuellen Artikels neu berechnen und in OrgaBack-DB schreiben.
    ''' Damit die Nährwerte richtig angezeigt werden, muss der Artikel in OrgaBack neu eingelesen werden !
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub UpdateNwt_Click(sender As Object, e As EventArgs) Handles KompRzChargen.UpdateNwt_Click
        'Artikel Nährwerte update
        Dim nwtUpdateArtikel As New wb_nwtUpdateArtikel
        'Start bei Artikelnummer x - 1
        If nwtUpdateArtikel.UpdateNext(Nr - 1, True) Then
            'Die Daten sind in OrgaBack erst nach Laden des Artikels sichtbar
            MsgBox("Die aktualisierten Nährwerte und Allergen" & vbCrLf & "sind erst nach erneutem Aufruf des Artikels in OrgaBack sichtbar", MsgBoxStyle.OkOnly, "WinBack-AddIn")
            'Aktualisierung erforderlich
            RaiseEvent DataInvalidated()
        End If
        'Speicher wieder freigeben
        nwtUpdateArtikel = Nothing
    End Sub
End Class
