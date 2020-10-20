
Imports System.ComponentModel
Imports System.Windows.Forms

Public Class wb_KompRzChargen

    Public WithEvents ArtikelChargen As New wb_MinMaxOptCharge
    Public WithEvents TeigChargen As New wb_MinMaxOptCharge
    Public Event DataInvalidated()
    Public Event DataUpdate()
    Public Event Cloud_Click(sender As Object, e As EventArgs)
    Public Event UpdateNwt_Click(sender As Object, e As EventArgs)

    Private OnErrorSetFocus As Object = Nothing
    Private _DataValid As Boolean
    Private _KompNr As Integer = wb_Global.UNDEFINED
    Private _RzNr As Integer = wb_Global.UNDEFINED
    Private _KompType As wb_Global.KomponTypen = wb_Global.KomponTypen.KO_TYPE_UNDEFINED
    Private _ID As String = wb_Global.UNDEFINED

    ''' <summary>
    ''' Laden der Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_KompRzChargen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Button Update Nährwerte Artikel verschieben
        BtnUpdateNwt.Top = BtnCloud.Top
    End Sub

    ''' <summary>
    ''' Daten aus dem Komponenten-Objekt lesen
    ''' 
    ''' Die Komponenten- und Rezeptnummer werden in der Komponenten-Klasse mit übergeben.
    ''' Die Komponentendaten werden mit MySQLdbRead() aus der Datenbank gelesen
    ''' </summary>
    ''' <param name="Komp"></param>
    Public Sub GetDataFromKomp(ByRef Komp As wb_Komponente)
        'Komponentendaten aus Datenbank lesen
        Komp.MySQLdbRead(Komp.Nr)
        'Komponenten-Nummer
        _KompNr = Komp.Nr

        'Komponententype
        _KompType = Komp.Type
        'Verknüpfung zur Cloud
        _ID = Komp.MatchCode

        'Rezeptnummer und Name (Ruft wb_Komponente.GetProduktionsDaten() auf
        RzNr = Komp.RzNr
        RezeptNummer = Komp.RezeptNummer
        RezeptName = Komp.RezeptName

        'Liniengruppen
        InitLinienGruppen()
        LinienGruppe = Komp.LinienGruppe
        ArtikelLiniengruppe = Komp.iArtikelLinienGruppe
        'Backverlust
        Backverlust = Komp.Backverlust
        'Produktions-Vorlauf
        ProdVorlauf = Komp.ProdVorlauf
        'Verkaufs-Gewicht
        Verkaufsgewicht = Komp.VerkaufsGewicht
        'Zuschnitt-Verlust
        Zuschnitt = Komp.Zuschnittverlust

        'Chargendaten aus Komponentendaten
        ArtikelChargen.CopyFrom(Komp.ArtikelChargen)
        TeigChargen.CopyFrom(Komp.TeigChargen)
    End Sub

    ''' <summary>
    ''' (geänderte) Daten wieder in das Komponenten-Objekt schreiben.
    ''' Die Werte für Rezeptnummer, Mehlzusammensetzung und Artikel-Liniengruppe werden in OrgaBack-MFF zurückgeschrieben
    ''' </summary>
    ''' <param name="Komp"></param>
    Public Sub SaveData(ByRef Komp As wb_Komponente)
        'Backort/Aufarbeitung in MFF200
        Komp.iArtikelLinienGruppe = ArtikelLiniengruppe
        Komp.LinienGruppe = LinienGruppe

        'Rezept-Nr in MFF 201
        Komp.RzNr = RzNr
        'Rezeptnummer(alpha) in MFF202
        Komp.RezeptNummer = RezeptNummer
        'Rezeptbezeichnung in MFF203
        Komp.RezeptName = RezeptName
        'MatchCode
        Komp.MatchCode = ID

        'Backverlust
        Komp.Backverlust = Backverlust
        'Zuschnitt
        Komp.Zuschnittverlust = Zuschnitt
        'Vorlauf Produktion
        Komp.ProdVorlauf = ProdVorlauf

        'Chargengrößen (werden mit wb_Komponente.UpdateDB() gesichert)
        Komp.TeigChargen.CopyFrom(TeigChargen)
        Komp.ArtikelChargen.CopyFrom(ArtikelChargen)
    End Sub

    ''' <summary>
    ''' Schreibt nur die geänderte Rezeptnummer/Name in das Komponenten-Objekt.
    ''' </summary>
    ''' <param name="Komp"></param>
    Public Sub UpdateData(ByRef Komp As wb_Komponente)
        'Rezept-Nr in MFF 201
        Komp.RzNr = RzNr
        'Rezeptnummer(alpha) in MFF202
        Komp.RezeptNummer = RezeptNummer
        'Rezeptbezeichnung in MFF203
        Komp.RezeptName = RezeptName
        'wenn sich die Zuordnung von Rezeptur zum Artikel geändert hat, muss die Nährwert-Berechnung neu durchgeführt werden
        Komp.NwtMarker = wb_Global.ArtikelMarker.nwtUpdate
        'geänderte Daten in DB schreiben
        Komp.UpdateDB()
    End Sub


    Private Sub InitLinienGruppen()
        'ComboBox Liniengruppe Rezepte(Teig) füllen
        cbLiniengruppe.Fill(wb_Linien_Global.RezeptLinienGruppen)
        'ComboBox Liniengruppe Artikel füllen
        cbArtikelLinienGruppe.Fill(wb_Linien_Global.ArtikelLinienGruppen)
    End Sub

    ''' <summary>
    ''' Daten sind gültig. Anzeigefelder freigeben/anzeigen
    ''' </summary>
    ''' <returns></returns>
    Public Property DataValid As Boolean
        Set(value As Boolean)
            'Wenn die Felder nach Deaktivierung wieder sichtbar sind - Anzeige aktualisieren
            If value Then
                If _RzNr > 0 Then
                    'Anzeigefelder einblenden
                    EnableKomponenten(True)
                    MinMaxOptArtikelShowValues()
                    MinMaxOptRezeptShowValues()
                Else
                    'Anzeigefelder ein/ausblenden
                    EnableKomponenten(False)
                End If
            Else
                'Rezeptnummer (Index)
                RzNr = wb_Global.UNDEFINED
                'Anzeigefelder ein/ausblenden
                EnableKomponenten(False)
            End If
            'Wert speichern
            _DataValid = value
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
            'Text Button "Auswählen/Ändern"
            If _RzNr > 0 Then
                BtnRzpt.Text = "Ändern/ Löschen"
                BtnRzpt.Enabled = True
                BtnRzpShow.Enabled = True
                'keine Verknüpfung zur Cloud möglich
                BtnCloud.Enabled = False
                'Für Artikel Button "Update Nährwerte" einblenden
                If KompType = wb_Global.KomponTypen.KO_TYPE_ARTIKEL Then
                    BtnUpdateNwt.Visible = True
                Else
                    BtnUpdateNwt.Visible = False
                End If
            Else
                BtnRzpt.Text = "Auswählen"
                BtnRzpShow.Enabled = False
                BtnUpdateNwt.Visible = False

                'Für Rohstoffe kann eine Verknüpfung zur Cloud hergestellt werden
                If KompType = wb_Global.KomponTypen.KO_TYPE_ARTIKEL Then
                    BtnCloud.Enabled = False
                Else
                    BtnCloud.Enabled = True
                End If

                'wenn schon eine Verknüpfung zur Cloud vorhanden ist kann keine Rezeptur zugewiesen werden
                If ID = "" Or ID = "-1" Then
                    BtnRzpt.Enabled = True
                Else
                    BtnRzpt.Enabled = False
                End If
            End If
        End Set
    End Property

    Public ReadOnly Property KompType As wb_Global.KomponTypen
        Get
            Return _KompType
        End Get
    End Property

    Public Property ID As String
        Get
            Return _ID
        End Get
        Set(value As String)
            _ID = value
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
    ''' Backverlust in Prozent (max 100%)
    ''' </summary>
    ''' <returns></returns>
    <Browsable(False), EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Backverlust As Double
        Get
            Return wb_Functions.StrToDouble(tBackverlust.Text)
        End Get
        Set(value As Double)
            If Backverlust > 100 Then
                value = 100
            End If
            tBackverlust.Text = wb_Functions.FormatStr(value.ToString, 3, 2) & " %"
        End Set
    End Property

    ''' <summary>
    ''' Zuschnitt-Verlsut in Prozent (max 100%)
    ''' </summary>
    ''' <returns></returns>
    <Browsable(False), EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Zuschnitt As Double
        Get
            Return wb_Functions.StrToDouble(tZuschnitt.Text)
        End Get
        Set(value As Double)
            If Zuschnitt > 100 Then
                value = 100
            End If
            tZuschnitt.Text = wb_Functions.FormatStr(value.ToString, 3, 2) & " %"
        End Set
    End Property

    <Browsable(False), EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property ProdVorlauf As Integer
        Get
            Return wb_Functions.StrToInt(tProdVorlauf.Text)
        End Get
        Set(value As Integer)
            If ProdVorlauf > 127 Then
                value = 127
            End If
            tProdVorlauf.Text = value.ToString("###") & " h"
        End Set
    End Property

    <Browsable(False), EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Verkaufsgewicht As Double
        Get
            Return wb_Functions.StrToDouble(tVkGewicht.Text)
        End Get
        Set(value As Double)
            If value > wb_Global.UNDEFINED Then
                tVkGewicht.Text = wb_Functions.FormatStr(value.ToString, 3) & " kg"
                BtnCalcNassGewicht.Visible = True
            Else
                tVkGewicht.Text = "- kg"
                BtnCalcNassGewicht.Visible = False
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
    ''' Auswahl Rezeptur. Öffnet ein Auswahlfenster mit der Rezeptliste.
    ''' Innerhalb der Rezeptliste kann über den Button Clear die Zuordung zwischen Artikel
    ''' und Rezeptur gelöscht werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub BtnRzpt_Click(sender As Object, e As EventArgs) Handles BtnRzpt.Click
        Dim RezeptAuswahl As New wb_Rezept_AuswahlListe
        RezeptAuswahl.BtnClear.Enabled = True

        If RezeptAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
            RzNr = RezeptAuswahl.RezeptNr
            tRezeptNr.Text = RezeptAuswahl.RezeptNummer
            tRezeptName.Text = RezeptAuswahl.RezeptName

            'Flag setzen - Daten wurden geändert, speichern notwendig
            DataIsInvalid()
            'Update Komponenten-Daten
            RaiseEvent DataUpdate()
        End If
    End Sub

    ''' <summary>
    ''' Das Auswahlfeld Liniengruppe (DropDown) wurde geändert. Flag setzen-Daten müssen gespeichert werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbLiniengruppe_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbLiniengruppe.SelectionChangeCommitted
        'Flag setzen - TeigChargen wurden geändert - Speichert die Rezeptur-Parameter!
        TeigChargen.HasChanged = True
        'Flag setzen - Daten wurden geändert, speichern notwendig
        DataIsInvalid()
    End Sub

    ''' <summary>
    ''' Das Auswahlfeld ArtikelLiniengruppe (DropDown) wurde geändert. Flag setzen-Daten müssen gespeichert werden.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbArtikelLinienGruppe_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbArtikelLinienGruppe.SelectionChangeCommitted
        'Flag setzen - Daten wurden geändert, speichern notwendig
        DataIsInvalid()
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
        MinMaxOptRezeptShowValues()
        'Flag setzen - Daten wurden geändert, speichern notwendig
        TeigChargen.HasChanged = True
        DataIsInvalid()
    End Sub

    Private Sub tProdVorlauf_Leave(sender As Object, e As EventArgs) Handles tProdVorlauf.Leave
        'Flag setzen - Daten wurden geändert, speichern notwendig
        DataIsInvalid()
    End Sub

    Private Sub tBackverlust_Leave(sender As Object, e As EventArgs) Handles tBackverlust.Leave
        'Flag setzen - Daten wurden geändert, speichern notwendig
        DataIsInvalid()
    End Sub

#End Region

    ''' <summary>
    ''' Enable/Disable der einzelnen Formular-Steuerelemente
    ''' </summary>
    ''' <param name="Enable"></param>
    Private Sub EnableKomponenten(Enable As Boolean)
        'ComboBox Liniengruppen
        cbLiniengruppe.Enabled = Enable
        cbArtikelLinienGruppe.Enabled = Enable
        'Panel Artikel/Teigchargen
        pArtikelChargen.Enabled = Enable
        ArtikelChargen.ErrorCheck = Enable
        pTeigChargen.Enabled = Enable
        TeigChargen.ErrorCheck = Enable
        tProdVorlauf.Enabled = Enable
        tBackverlust.Enabled = Enable

        'alte Zahlenwerte in den Felder löschen
        If Not Enable Then
            'Stückgewicht Artikel
            tStkGewicht.Text = ""
            'Backverlust
            tBackverlust.Text = ""
            'Produktions-Vorlauf
            tProdVorlauf.Text = ""
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
    ''' Backverlust und Vorlauf Produktion werden in den Properties formatiert und angezeigt
    ''' </summary>
    Private Sub MinMaxOptArtikelShowValues()
        'Stückgewicht
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
    Private Sub MinMaxOptRezeptShowValues()
        tRezGesamt.Text = ArtikelChargen.TeigGewicht & " kg"
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

    Private Sub BtnCloud_Click(sender As Object, e As EventArgs) Handles BtnCloud.Click
        RaiseEvent Cloud_Click(sender, e)
    End Sub

    Private Sub BtnUpdateNwt_Click(sender As Object, e As EventArgs) Handles BtnUpdateNwt.Click
        RaiseEvent UpdateNwt_Click(sender, e)
    End Sub

    ''' <summary>
    ''' Berechnet das Nassgewicht aus Stk-Gewicht(Verkauf), Zuschnitt-Verlust und Backverlust
    '''     Gewicht Teigling = (StkGewicht/(100-Backverlust))*100
    '''     Nass-Gewicht     = (TeiglGewicht/(100-Zuschnitt))*100
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnCalcNassGewicht_Click(sender As Object, e As EventArgs) Handles BtnCalcNassGewicht.Click
        'Gewicht Teigling vor dem Backen
        Dim tg As Double = ((Verkaufsgewicht * 1000) / (100 - Backverlust)) * 100
        'Nass-Gewicht in Gramm
        Dim ng As Double = (tg / (100 - Zuschnitt)) * 100
        'auf ganze Zahl runden
        ArtikelChargen.StkGewicht = wb_Functions.FormatStr(ng.ToString, 0)
    End Sub

End Class
