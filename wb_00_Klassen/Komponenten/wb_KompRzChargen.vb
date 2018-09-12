
Imports System.ComponentModel
Imports System.Windows.Forms

Public Class wb_KompRzChargen

    Public WithEvents ArtikelChargen As New wb_MinMaxOptCharge
    Public WithEvents TeigChargen As New wb_MinMaxOptCharge
    Public Event DataInvalidated()
    Private OnErrorSetFocus As Object = Nothing

    Private _DataValid As Boolean
    Private _RzNr As Integer = wb_Global.UNDEFINED

    ''' <summary>
    ''' Komponente wird geladen.
    ''' ComboBox Liniengruppe Rezepte(Teig) und Artikel füllen.
    ''' 
    ''' Wird nur im RunTime-Mode ausgeführt. Wenn "InitLiniengruppe" im Design-Mode ausgeführt wird (Programmieren/Speichern/Laden) wird
    ''' versucht über wb_LinienGlobal die winback.ini zu laden.
    ''' https://stackoverflow.com/questions/73515/how-to-tell-if-net-code-is-being-run-by-visual-studio-designer
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_KompRzChargen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Soll nicht ausgeführt werden wenn der Designer läuft.
        If (System.ComponentModel.LicenseManager.UsageMode <> System.ComponentModel.LicenseUsageMode.Designtime) And Not Me.DesignMode Then
            'Liniengruppen-Auswahlfelder initialisieren
            InitLinienGruppen()
        End If
    End Sub

    ''' <summary>
    ''' Daten aus dem Komponenten-Objekt lesen
    ''' 
    ''' Die Komponenten- und Rezeptnummer werden in der Komponenten-Klasse mit übergeben.
    ''' Die Komponentendaten werden mit MySQLdbRead() aus der Datenbank gelesen
    ''' </summary>
    ''' <param name="Komp"></param>
    Public Sub GetDataFromKomp(ByRef Komp As wb_Komponente)
        'Berechnung/Prüfung der Chargengrößen abschalten
        ArtikelChargen.ErrorCheck = False
        TeigChargen.ErrorCheck = False
        'Rezeptnummer aus Rohstoffdaten
        RzNr = Komp.RzNr
        'Komponentendaten aus Datenbank lesen
        Komp.MySQLdbRead(Komp.Nr)
        'Rezeptnummer und Name
        RezeptNummer = Komp.RezeptNummer
        RezeptName = Komp.RezeptName
        'Liniengruppen
        LinienGruppe = Komp.LinienGruppe
        ArtikelLiniengruppe = Komp.iArtikelLinienGruppe
        'Chargendaten aus Komponentendaten
        ArtikelChargen = Komp.ArtikelChargen
        TeigChargen = Komp.TeigChargen
    End Sub

    Private Sub InitLinienGruppen()
        'ComboBox Liniengruppe Rezepte(Teig) füllen
        cbLiniengruppe.Fill(wb_Linien_Global.LinienGruppen)
        'ComboBox Liniengruppe Artikel füllen
        cbArtikelLinienGruppe.Fill(wb_Linien_Global.LinienGruppen)
    End Sub

    ''' <summary>
    ''' Adaten sind gültig. Anzeigefelder freigeben/anzeigen
    ''' </summary>
    ''' <returns></returns>
    Public Property DataValid As Boolean
        Set(value As Boolean)
            _DataValid = value
            EnableKomponenten(value)
        End Set
        Get
            Return _DataValid
        End Get
    End Property

    ''' <summary>
    ''' Rezeptnummer intern (Index)
    ''' </summary>
    ''' <returns></returns>
    Public Property RzNr As Integer
        Get
            Return _RzNr
        End Get
        Set(value As Integer)
            _RzNr = value
        End Set
    End Property

    ''' <summary>
    ''' Rezeptnummer
    ''' </summary>
    ''' <returns></returns>
    Public Property RezeptNummer As String
        Get
            Return tRezeptNr.Text
        End Get
        Set(value As String)
            tRezeptNr.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Rezeptbezeichnung
    ''' </summary>
    ''' <returns></returns>
    Public Property RezeptName As String
        Get
            Return tRezeptName.Text
        End Get
        Set(value As String)
            tRezeptName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Artikel-Liniengruppe. Enthält den Aufarbeitungsplatz(Backort)
    ''' </summary>
    ''' <returns></returns>
    <Browsable(False), EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property ArtikelLiniengruppe As Integer
        Get
            If cbArtikelLinienGruppe IsNot Nothing Then
                Return cbArtikelLinienGruppe.GetKeyFromSelection
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
        Set(value As Integer)
            If cbArtikelLinienGruppe IsNot Nothing Then
                cbArtikelLinienGruppe.SetTextFromKey(value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Rezept-Liniengruppe
    ''' </summary>
    ''' <returns></returns>
    <Browsable(False), EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property LinienGruppe As Integer
        Get
            If cbLiniengruppe IsNot Nothing Then
                Return cbLiniengruppe.GetKeyFromSelection
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
        Set(value As Integer)
            If cbLiniengruppe IsNot Nothing Then
                cbLiniengruppe.SetTextFromKey(value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Rezeptur anzeigen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnRzpShow_Click(sender As Object, e As EventArgs) Handles BtnRzpShow.Click
        Me.Cursor = Cursors.WaitCursor
        Dim Rezeptur As New wb_Rezept_Rezeptur(RzNr, 1)
        'MDI-Fenster anzeigen
        Rezeptur.Show()
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Auswahl Rezeptur. Öffnet ein Auswahlfenster mit der Rezeptliste
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnRzpt_Click(sender As Object, e As EventArgs) Handles BtnRzpt.Click
        Dim RezeptAuswahl As New wb_Rezept_AuswahlListe
        If RezeptAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            RzNr = RezeptAuswahl.RezeptNr
            tRezeptNr.Text = RezeptAuswahl.RezeptNummer
            tRezeptName.Text = RezeptAuswahl.RezeptName
            'Flag setzen - Daten wurden geändert, speichern notwendig
            DataIsInvalid()
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
        If ArtikelChargen.ErrorCode <> wb_Global.MinMaxOptChargenError.NoError Then
            'Eingabe-Focus auf das auslösende Objekt setzen
            If OnErrorSetFocus IsNot Nothing Then
                OnErrorSetFocus.Focus()
            End If
            'Fehlermeldung entsprechend der Eingabe-Felder ausgeben
            MsgBox(wb_Functions.MinMaxOptChargeToString(ArtikelChargen.ErrorCode), MsgBoxStyle.Exclamation, "Fehler bei der Eingabe der Artikel-Chargengrößen")
        End If

        'Felder neu zeichnen
        MinMaxOptArtikelShowValues()
        'Flag setzen - Daten wurden geändert, speichern notwendig
        ArtikelChargen.HasChanged = True
        DataIsInvalid()
    End Sub

    Private Sub OnErrorMinMaxOptTeig(Sender As Object) Handles TeigChargen.OnError
        If TeigChargen.ErrorCode <> wb_Global.MinMaxOptChargenError.NoError Then
            If OnErrorSetFocus IsNot Nothing Then
                OnErrorSetFocus.Focus()
            End If
            'Fehlermeldung entsprechend der Eingabe-Felder ausgeben
            MsgBox(wb_Functions.MinMaxOptChargeToString(TeigChargen.ErrorCode), MsgBoxStyle.Exclamation, "Fehler bei der Eingabe der Rezept-Chargengrößen")
        End If

        'Felder neu zeichnen
        MinMaxRezeptShowValues()
        'Flag setzen - Daten wurden geändert, speichern notwendig
        TeigChargen.HasChanged = True
        DataIsInvalid()
    End Sub
#End Region

    ''' <summary>
    ''' Enable/Disable der einzelnen Formular-Steuerelemente
    ''' </summary>
    ''' <param name="Enable"></param>
    Private Sub EnableKomponenten(Enable As Boolean)
        'Button Rezept öffnen/auswählen
        BtnRzpShow.Enabled = Enable
        BtnRzpt.Enabled = Enable
        'ComboBox Liniengruppen
        cbLiniengruppe.Enabled = Enable
        cbArtikelLinienGruppe.Enabled = Enable
        'Panel Artikel/Teigchargen
        pArtikelChargen.Enabled = Enable
        ArtikelChargen.ErrorCheck = Enable
        pTeigChargen.Enabled = Enable
        TeigChargen.ErrorCheck = Enable

        'alte Zahlenwerte in den Felder löschen
        If Not Enable Then
            'Stückgewicht Artikel
            tStkGewicht.Text = ""
            'Rezeptnummer (Index)
            RzNr = wb_Global.UNDEFINED
            'Chargengrößen in kg
            tChrgMinkg.Text = ""
            tChrgOptkg.Text = ""
            tChrgMaxkg.Text = ""
            'Chargengrößen in Prozent
            tChrgMinPrz.Text = ""
            tChrgOptPrz.Text = ""
            tChrgMaxPrz.Text = ""
            'Chargengrößen in Stk
            tChrgMinStk.Text = ""
            tChrgOptStk.Text = ""
            tChrgMaxStk.Text = ""
            'Rezept
            tRezeptNr.Text = ""
            tRezeptName.Text = ""
            tRezGesamt.Text = ""
            'Liniengruppen
            cbArtikelLinienGruppe.Text = ""
            cbLiniengruppe.Text = ""
            'Chargengrößen Rezept in kg
            tRezMinkg.Text = ""
            tRezOptkg.Text = ""
            tRezMaxkg.Text = ""
            'Chargengrößen Rezept in Prozent
            tRezMinPrz.Text = ""
            tRezOptPrz.Text = ""
            tRezMaxPrz.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' Anzeigen der Artikel-Chargengrößen. Alle Zahlenwerte aus wb_ChargenMinMax in die entsprechenden Textfelder kopieren.
    ''' </summary>
    Private Sub MinMaxOptArtikelShowValues()
        tStkGewicht.Text = ArtikelChargen.StkGewicht & " gr"
        'Chargengrößen in kg
        tChrgMinkg.Text = ArtikelChargen.MinCharge.MengeInkg & " kg"
        tChrgOptkg.Text = ArtikelChargen.OptCharge.MengeInkg & " kg"
        tChrgMaxkg.Text = ArtikelChargen.MaxCharge.MengeInkg & " kg"
        'Chargengrößen in Prozent
        tChrgMinPrz.Text = ArtikelChargen.MinCharge.MengeInProzent & "%"
        tChrgOptPrz.Text = ArtikelChargen.OptCharge.MengeInProzent & "%"
        tChrgMaxPrz.Text = ArtikelChargen.MaxCharge.MengeInProzent & "%"
        'Chargengrößen in Stk
        tChrgMinStk.Text = ArtikelChargen.MinCharge.MengeInStk & " Stk"
        tChrgOptStk.Text = ArtikelChargen.OptCharge.MengeInStk & " Stk"
        tChrgMaxStk.Text = ArtikelChargen.MaxCharge.MengeInStk & " Stk"
    End Sub

    ''' <summary>
    ''' Anzeigen der Rezept-Chargengrößen. Alle Zahlenwerte aus wb_ChargenMinMax in die entsprechenden Textfelder kopieren.
    ''' </summary>
    Private Sub MinMaxRezeptShowValues()
        tRezGesamt.Text = TeigChargen.TeigGewicht & " kg"
        'Chargengrößen in kg
        tRezMinkg.Text = TeigChargen.MinCharge.MengeInkg & " kg"
        tRezOptkg.Text = TeigChargen.OptCharge.MengeInkg & " kg"
        tRezMaxkg.Text = TeigChargen.MaxCharge.MengeInkg & " kg"
        'Chargengrößen in Prozent
        tRezMinPrz.Text = TeigChargen.MinCharge.MengeInProzent & "%"
        tRezOptPrz.Text = TeigChargen.OptCharge.MengeInProzent & "%"
        tRezMaxPrz.Text = TeigChargen.MaxCharge.MengeInProzent & "%"
    End Sub

    ''' <summary>
    ''' Die Daten im Fenster haben sich durch Benutzer-Eingabe geändert.
    ''' Flag setzen (DataHasChanged). Über den Event DataInvalidated wird dem Haupt-Fenster mitgeteilt, dass die Daten vor dem Schliessen
    ''' oder Löschen des Fensters gesichert werden müssen.
    ''' </summary>
    Private Sub DataIsInvalid()
        RaiseEvent DataInvalidated()
    End Sub

End Class
