Public Class wb_Planung_Neu
    Private RezeptAuswahl As wb_Rezept_AuswahlListe
    Private ArtikelAuswahl As wb_Artikel_AuswahlListe
    Private _RezNr As Integer = wb_Global.UNDEFINED
    Private _ArtNr As Integer = wb_Global.UNDEFINED
    Private ProduktionNeu As wb_Produktion

    Private ChargenBerechnung As wb_Global.ChargenMengen
    Private _TeigGesMenge As Double
    Private _TeigGesMengeOrg As Double = wb_Global.UNDEFINED
    Private _StkGewicht As Double
    Private _TeigGewicht As Double

    Private _RezOptkg As Double
    Private _RezMinkg As Double
    Private _RezMaxkg As Double

    Private _OptChrMenge As Double
    Private _MinChrMenge As Double
    Private _MaxChrMenge As Double

    Private _AnzGesStck As Integer
    Private _AnzOptChargen As Integer
    Private _AnzRstChargen As Integer

    Public Sub New(ByRef Produktion As wb_Produktion)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        ProduktionNeu = Produktion
    End Sub


    Private Sub wb_Planung_Neu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'alle Felder löschen
        ClearKomponenten()
        pTeigChargen.Enabled = False
        pArtikelChargen.Enabled = False
        'Vorproduktion auflösen ist Default
        cbAufloesen.Checked = True

        'Combo-Box(Rezept-Varianten) mit Werten füllen
        cbVariante.Fill(wb_Rezept_Shared.RzVariante)
        'Combo-Box(Liniengruppen) mit Werten füllen
        cbLiniengruppe.Fill(wb_Linien_Global.RezeptLinienGruppen)

        'CheckedListBox Hintergrund an Formular anpassen
        cbChargenTeiler.BackColor = BackColor
        'CheckedListBox füllen 
        cbChargenTeiler.Fill(System.Enum.GetValues(GetType(wb_Global.ModusChargenTeiler)), AddressOf wb_Functions.ModusChargenTeilerToString)
        'CheckedListBox Voreinstellungen aus WinBack.ini
        cbChargenTeiler.SelIndex = wb_GlobalSettings.ChargenTeiler

    End Sub

    ''' <summary>
    ''' Chargen-Teiler wird geändert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbChargenTeiler_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbChargenTeiler.SelectedIndexChanged
        'Chargenteiler global
        CheckChargenTeiler()
        wb_GlobalSettings.ChargenTeiler = cbChargenTeiler.SelIndex
        'wenn schon eine Berechnung durchgeführt wurde
        If _TeigGesMengeOrg <> wb_Global.UNDEFINED Then
            'Berechnung erneut (mit anderem Teiler) durchführen
            tGesMengeKg.Text = _TeigGesMengeOrg.ToString("F3")
            'Berechnung starten
            tGesMengeKg.Modified = True
            tGesMengeKg_Leave(sender, e)
        End If
    End Sub

    ''' <summary>
    ''' Wenn die Chargen über Artikel-Auswahl erzeugt werden, ist der Chargen-Teiler Rezeptgewicht nicht erlaubt
    ''' </summary>
    Private Sub CheckChargenTeiler()
        If pArtikelChargen.Enabled Then
            'ChargenTeiler Rezeptgewicht nicht erlaubt
            If cbChargenTeiler.SelIndex = wb_Global.ModusChargenTeiler.RezeptGroesse Then
                MsgBox("Artikel-Chargen können nicht auf Rezept-Größe aufgeteilt werden !", MsgBoxStyle.Exclamation, "WinBack")
                cbChargenTeiler.SelIndex = wb_Global.ModusChargenTeiler.OptimalUndRest
            End If
        End If
    End Sub
    ''' <summary>
    ''' Auswahlfenster Artikel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tArtikelNummer_DoubleClick(sender As Object, e As EventArgs) Handles tArtikelNummer.DoubleClick, tArtikelName.DoubleClick
        'Auswahl-Dialog anzeigen
        ArtikelAuswahl = New wb_Artikel_AuswahlListe()
        'Daten zur Rezeptnummer auswerten
        GetArtikelAuswahlDaten()

        tGesMengeStk.Focus()
        ArtikelAuswahl = Nothing
        tArtikelNummer.Modified = False
    End Sub

    ''' <summary>
    ''' Artikelnummer suchen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tArtikelNummer_Leave(sender As Object, e As EventArgs) Handles tArtikelNummer.Leave
        'Flag Nummer wurde geändert
        If tArtikelNummer.Modified Then
            'Reset Flag
            tArtikelNummer.Modified = False
            'Rezept mit dieser Nummer suchen
            ArtikelAuswahl = New wb_Artikel_AuswahlListe(tArtikelNummer.Text, "")
            'Daten zur Rezeptnummer auswerten
            GetArtikelAuswahlDaten()

            tGesMengeStk.Focus()
            ArtikelAuswahl = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Auswahlfenster Rezeptur
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tRezeptNummer_DoubleClick(sender As Object, e As EventArgs) Handles tRezeptNummer.DoubleClick, tRezeptName.DoubleClick
        'Auswahl-Dialog anzeigen
        RezeptAuswahl = New wb_Rezept_AuswahlListe()
        'alle Felder löschen
        ClearKomponenten()
        'Daten zur Rezeptnummer auswerten
        GetRezeptAuswahlDaten()

        tGesMengeKg.Focus()
        RezeptAuswahl = Nothing
        tRezeptNummer.Modified = False
    End Sub

    ''' <summary>
    ''' Rezeptnummer suchen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tRezeptNummer_Leave(sender As Object, e As EventArgs) Handles tRezeptNummer.Leave
        'Flag Nummer wurde geändert
        If tRezeptNummer.Modified Then
            'Reset Flag
            tRezeptNummer.Modified = False
            'alle Felder löschen
            ClearKomponenten()
            'Rezept mit dieser Nummer suchen
            RezeptAuswahl = New wb_Rezept_AuswahlListe(tRezeptNummer.Text, "")
            'Daten zur Rezeptnummer auswerten
            GetRezeptAuswahlDaten()

            tGesMengeKg.Focus()
            RezeptAuswahl = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Rezeptbezeichnung suchen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tRezeptName_Leave(sender As Object, e As EventArgs) Handles tRezeptName.Leave
        'Flag Name wurde geändert
        If tRezeptName.Modified Then
            'Reset Flag
            tRezeptName.Modified = False
            'alle Felder löschen
            ClearKomponenten()
            'Rezept mit dieser Nummer suchen
            RezeptAuswahl = New wb_Rezept_AuswahlListe("", tRezeptName.Text)
            'Daten zur Rezeptnummer auswerten
            GetRezeptAuswahlDaten()

            tGesMengeKg.Focus()
            RezeptAuswahl = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Daten aus der aktuellen DataGridView auswerten. Wenn das Ergebnis nicht eindeutig ist,
    ''' wird das Auswahlfenster aufgerufen.
    ''' </summary>
    Private Function GetRezeptAuswahlDaten(Optional OpenDialog As Boolean = True) As Boolean
        Dim diagResult As Windows.Forms.DialogResult = Windows.Forms.DialogResult.None

        'abhängig von der Anzahl der Ergebnis-Datensätze
        Select Case RezeptAuswahl.RowCount
            Case 0
                'Filter löschen
                RezeptAuswahl.DataGridView.Filter = ""
                'Auswahl-Dialog anzeigen
                If OpenDialog Then
                    diagResult = RezeptAuswahl.ShowDialog()
                End If
            Case 1
                diagResult = Windows.Forms.DialogResult.OK
            Case Else
                'Auswahl-Dialog anzeigen
                If OpenDialog Then
                    diagResult = RezeptAuswahl.ShowDialog()
                End If
        End Select

        'Rezept gefunden und gültig
        If diagResult = Windows.Forms.DialogResult.OK Then

            'Rezeptnummer und Bezeichnung
            tRezeptNummer.Text = RezeptAuswahl.RezeptNummer
            tRezeptName.Text = RezeptAuswahl.RezeptName
            _RezNr = RezeptAuswahl.RezeptNr

            'Eintrag in Combo-Box Liniengruppe ausfüllen
            cbLiniengruppe.SetTextFromKey(RezeptAuswahl.RezeptLinienGruppe)
            'Eintrag in Combo-Box Rezeptvariante ausfüllen (Default ist Variante 1)
            cbVariante.SetTextFromKey(1)

            'Chargengrößen in kg
            tRezGesamt.Text = RezeptAuswahl.RezeptChargeMin.TeigGewicht & " kg"
            tRezMinkg.Text = RezeptAuswahl.RezeptChargeMin.MengeInkg & " kg"
            tRezMaxkg.Text = RezeptAuswahl.RezeptChargeMax.MengeInkg & " kg"
            tRezOptkg.Text = RezeptAuswahl.RezeptChargeOpt.MengeInkg & " kg"

            'Chargengrößen in %
            tRezMinPrz.Text = RezeptAuswahl.RezeptChargeMin.MengeInProzent & " %"
            tRezMaxPrz.Text = RezeptAuswahl.RezeptChargeMax.MengeInProzent & " %"
            tRezOptPrz.Text = RezeptAuswahl.RezeptChargeOpt.MengeInProzent & " %"

            'Chargengrößen Artikel ausblenden
            pTeigChargen.Enabled = True
            pArtikelChargen.Enabled = False

            'Auswahl OK
            Return True
        End If

        'kein Rezept gefunden 
        Return False
    End Function

    ''' <summary>
    ''' Daten aus der aktuellen DataGridView auswerten. Wenn das Ergebnis nicht eindeutig ist,
    ''' wird das Auswahlfenster aufgerufen.
    ''' </summary>
    Private Sub GetArtikelAuswahlDaten()
        Dim diagResult As Windows.Forms.DialogResult

        'abhängig von der Anzahl der Ergebnis-Datensätze
        Select Case ArtikelAuswahl.RowCount
            Case 0
                'Filter löschen
                ArtikelAuswahl.DataGridView.Filter = ""
                'Auswahl-Dialog anzeigen
                diagResult = ArtikelAuswahl.ShowDialog()
            Case 1
                diagResult = Windows.Forms.DialogResult.OK
            Case Else
                'Auswahl-Dialog anzeigen
                diagResult = ArtikelAuswahl.ShowDialog()
        End Select

        If diagResult = Windows.Forms.DialogResult.OK Then

            'Artikelnummer und Bezeichnung
            tArtikelNummer.Text = ArtikelAuswahl.ArtikelNummer
            tArtikelName.Text = ArtikelAuswahl.ArtikelName
            'Artikelnummer (Index)
            _ArtNr = ArtikelAuswahl.ArtikelNr

            'ChargenTeiler Rezeptgewicht nicht erlaubt
            If cbChargenTeiler.SelIndex = wb_Global.ModusChargenTeiler.RezeptGroesse Then
                cbChargenTeiler.SelIndex = wb_Global.ModusChargenTeiler.OptimalUndRest
                wb_GlobalSettings.ChargenTeiler = cbChargenTeiler.SelIndex
            End If

            'Rezeptnummer (Index)
            _RezNr = ArtikelAuswahl.RezNr
            'Rezept mit dieser Nummer suchen
            RezeptAuswahl = New wb_Rezept_AuswahlListe(_RezNr)

            'Daten zur Rezeptnummer auswerten
            If GetRezeptAuswahlDaten() Then
                pTeigChargen.Enabled = False
            End If

            'Teigmenge
            ArtikelAuswahl.Teiggewicht = RezeptAuswahl.RezeptChargeMin.TeigGewicht

            'Chargengrößen in kg
            tStkGewicht.Text = ArtikelAuswahl.ArtikelChargeMin.StkGewicht & " gr"
            tChrgMinkg.Text = ArtikelAuswahl.ArtikelChargeMin.MengeInkg & " kg"
            tChrgMaxkg.Text = ArtikelAuswahl.ArtikelChargeMax.MengeInkg & " kg"
            tChrgOptkg.Text = ArtikelAuswahl.ArtikelChargeOpt.MengeInkg & " kg"

            'Chargengrößen in %
            tChrgMinPrz.Text = ArtikelAuswahl.ArtikelChargeMin.MengeInProzent & " %"
            tChrgMaxPrz.Text = ArtikelAuswahl.ArtikelChargeMax.MengeInProzent & " %"
            tChrgOptPrz.Text = ArtikelAuswahl.ArtikelChargeOpt.MengeInProzent & " %"

            'Chargengrößen in Stk
            tChrgMinStk.Text = ArtikelAuswahl.ArtikelChargeMin.MengeInStk
            tChrgMaxStk.Text = ArtikelAuswahl.ArtikelChargeMax.MengeInStk
            tChrgOptStk.Text = ArtikelAuswahl.ArtikelChargeOpt.MengeInStk


            'Chargengrößen Artikel einblenden
            pArtikelChargen.Enabled = True

        End If
    End Sub

    ''' <summary>
    ''' Werte löschen der einzelnen Formular-Steuerelemente
    ''' </summary>
    Private Sub ClearKomponenten()

        'Artikel Bezeichnung und Nummer löschen
        tArtikelName.Text = ""
        tArtikelNummer.Text = ""

        'Stückgewicht Artikel
        tStkGewicht.Text = ""
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
        tRezeptName.Text = ""
        tRezGesamt.Text = ""
        'Liniengruppen
        cbLiniengruppe.Text = ""
        'Chargengrößen Rezept in kg
        tRezMinkg.Text = ""
        tRezOptkg.Text = ""
        tRezMaxkg.Text = ""
        'Chargengrößen Rezept in Prozent
        tRezMinPrz.Text = ""
        tRezOptPrz.Text = ""
        tRezMaxPrz.Text = ""

        'Reset Farben
        tGesMengeStk.BackColor = Drawing.Color.White
        tGesMengeKg.BackColor = Drawing.Color.White
        lblChargenResult.Text = ""

    End Sub

    Private Sub GetDoubleFromText()
        'Eingabefeld Gesamtmenge in Stück
        _AnzGesStck = wb_Functions.StrToDouble(tGesMengeStk.Text)
        'Eingabefeld Gesamtmenge in kg
        _TeigGesMenge = wb_Functions.StrToDouble(tGesMengeKg.Text)

        'Stückgewicht
        _StkGewicht = wb_Functions.StrToDouble(tStkGewicht.Text)
        If _StkGewicht = 0 Then
            _StkGewicht = 1000
        End If

        'Rezept-Gesamtmenge
        _TeigGewicht = wb_Functions.StrToDouble(tRezGesamt.Text)

        'Chargengrößen in kg (Teig)
        _RezOptkg = wb_Functions.StrToDouble(tRezOptkg.Text)
        _RezMinkg = wb_Functions.StrToDouble(tRezMinkg.Text)
        _RezMaxkg = wb_Functions.StrToDouble(tRezMaxkg.Text)

        'Chargengrößen in kg (Artikel)
        _OptChrMenge = wb_Functions.StrToDouble(tChrgOptkg.Text)
        _MinChrMenge = wb_Functions.StrToDouble(tChrgMinkg.Text)
        _MaxChrMenge = wb_Functions.StrToDouble(tChrgMaxkg.Text)

        'Chargen-Teiler Modus Rezeptgröße 
        If cbChargenTeiler.SelIndex = wb_Global.ModusChargenTeiler.RezeptGroesse Then
            _RezMaxkg = wb_Functions.StrToDouble(tRezGesamt.Text)
            _MaxChrMenge = wb_Functions.StrToDouble(tRezGesamt.Text)
        End If
    End Sub

    Private Sub tGesMengeStk_Leave(sender As Object, e As EventArgs) Handles tGesMengeStk.Leave
        'Eingabefeld wurde geändert
        If tGesMengeStk.Modified Then
            tGesMengeStk.Modified = False

            'Rechenwerte aus den Textfeldern ermitteln
            GetDoubleFromText()

            'Berechnung Teig-Gesamtmenge
            _TeigGesMenge = _AnzGesStck * _StkGewicht / 1000
            tGesMengeKg.Text = _TeigGesMenge.ToString("F3")
            'Teig-GesamtMenge merken
            _TeigGesMengeOrg = _TeigGesMenge

            'Chargen-Aufteilung berechnen (Artikel)
            ChargenBerechnung = ProduktionNeu.CalcChargenMenge(_TeigGesMenge, _MinChrMenge, _MaxChrMenge, _OptChrMenge, cbChargenTeiler.SelIndex, False)
            ShowCalcChargenResult()
        End If
    End Sub

    Private Sub tGesMengeKg_Leave(sender As Object, e As EventArgs) Handles tGesMengeKg.Leave
        'Eingabefeld wurde geändert
        If tGesMengeKg.Modified Then
            tGesMengeKg.Modified = False

            'Rechenwerte aus den Textfeldern ermitteln
            GetDoubleFromText()
            'Teig-GesamtMenge merken
            _TeigGesMengeOrg = _TeigGesMenge

            'Berechnung Artikel-Stückzahl Gesamt
            If pArtikelChargen.Enabled Then
                _AnzGesStck = _TeigGesMenge * 1000 / _StkGewicht
                tGesMengeStk.Text = _AnzGesStck.ToString("F0")
                'Chargen-Aufteilung berechnen (Artikel)
                ChargenBerechnung = ProduktionNeu.CalcChargenMenge(_TeigGesMenge, _MinChrMenge, _MaxChrMenge, _OptChrMenge, cbChargenTeiler.SelIndex, False)
            Else
                'Chargen-Aufteilung berechnen (Rezept)
                ChargenBerechnung = ProduktionNeu.CalcChargenMenge(_TeigGesMenge, _RezMinkg, _RezMaxkg, _RezOptkg, cbChargenTeiler.SelIndex, False)
            End If

            'Ergebnis anzeigen
            ShowCalcChargenResult()
        End If
    End Sub

    ''' <summary>
    ''' Anzeigen der berechneten Chargengrößen
    '''     wb_global.OK                  'Chargenaufteilung in Ordnung
    '''     wb_global.EM1                 'Nach Aufteilung in Optimalchargen bleibt eine Restmenge offen, die nicht produziert werden kann
    '''     wb_global.EM2                 'Nach Aufteilung in Optimalchargen wird mehr produziert als gefordert
    '''     wb_global.EM3                 'Nur eine Restcharge kleiner als die Minimalcharge. Muss zusammengefasst werden.
    '''     wb_global.EP1                 'Sollmenge nicht erreicht, Restmenge unterhalb Mindestcharge
    '''     wb_global.EP9                 'Keine Chargengrößen angegeben, Aufteilung nach Rezeptgröße
    '''     
    ''' </summary>
    Private Sub ShowCalcChargenResult()

        'Ergebnisse in Formular eintragen
        tAnzOptimal.Text = ChargenBerechnung.AnzahlOpt
        tAnzRest.Text = ChargenBerechnung.AnzahlRest
        tMengeOptimal.Text = ChargenBerechnung.MengeOpt.ToString("F3")
        tMengeRest.Text = ChargenBerechnung.MengeRest.ToString("F3")

        'Korrektur Gesamtmenge
        _TeigGesMenge = (ChargenBerechnung.MengeOpt * ChargenBerechnung.AnzahlOpt + ChargenBerechnung.MengeRest * ChargenBerechnung.AnzahlRest)
        tGesMengeKg.Text = _TeigGesMenge.ToString("F3")

        'Ergebnis der Berechnung
        Select Case ChargenBerechnung.Result
            Case wb_Global.ChargenTeilerResult.EM1
                lblChargenResult.Text = "Nach der Aufteilung in Optimalchargen bleibt eine Restmenge offen, die nicht produziert werden kann"
                SetFieldColor(Drawing.Color.Red)
            Case wb_Global.ChargenTeilerResult.EM2
                lblChargenResult.Text = "Nach der Aufteilung in Optimalchargen wird mehr produziert als gefordert"
                SetFieldColor(Drawing.Color.Yellow)
            Case wb_Global.ChargenTeilerResult.EM3
                lblChargenResult.Text = "Nach der Aufteilung in Optimalchargen bleibt eine Restmenge offen, die kleiner ist als die Minimal-Charge"
                SetFieldColor(Drawing.Color.Red)

            Case wb_Global.ChargenTeilerResult.EP1, wb_Global.ChargenTeilerResult.EP2
                lblChargenResult.Text = "Die Sollmenge kann nicht erreicht werden, die Restmenge ist kleiner ist als die Minimal-Charge"
                SetFieldColor(Drawing.Color.Red)
            Case wb_Global.ChargenTeilerResult.EP9
                lblChargenResult.Text = "Aufteilung in gleiche Teige (Rezeptgröße)"
                SetFieldColor(Drawing.Color.DodgerBlue)

            Case wb_Global.ChargenTeilerResult.OK
                lblChargenResult.Text = ""
                SetFieldColor(Drawing.Color.LightGreen)
        End Select

        'Korrektur Sollstückzahl
        If pArtikelChargen.Enabled Then
            _AnzGesStck = _TeigGesMenge * 1000 / _StkGewicht
            tGesMengeStk.Text = _AnzGesStck.ToString("F0")
        End If

    End Sub

    Private Sub SetFieldColor(Farbe As Drawing.Color)
        'Berechnung nach Artikel-Stk
        If pArtikelChargen.Enabled Then
            tGesMengeStk.BackColor = Farbe
        End If
        'Teigmenge
        tGesMengeKg.BackColor = Farbe
        '(Fehlermeldung) Chargen-Berechnung
        lblChargenResult.ForeColor = Farbe
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If pArtikelChargen.Enabled Then
            ProduktionNeu.AddChargenZeile("", "", _ArtNr, _AnzGesStck, 0, 0, cbChargenTeiler.SelIndex, cbAufloesen.Checked, False)
        Else
            ProduktionNeu.AddChargenZeile("", _RezNr, _TeigGesMenge, cbChargenTeiler.SelIndex, cbAufloesen.Checked)
        End If
    End Sub
End Class