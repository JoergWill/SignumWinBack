Imports WinBack.wb_Artikel_Shared

Public Class wb_Planung_ListeFehlerArtikel

    Private _Artikel As New wb_Komponente
    Private _ErrIndex As Integer
    Private _ReadOK As Boolean = False

    Public Sub New(ArtikelNr As String, ErrIndex As Integer)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        'Zeiger auf den Fehler in der Liste
        _ErrIndex = ErrIndex

        'Artikel lesen
        If _Artikel.MySQLdbRead(0, ArtikelNr) Then
            DetailInfo(Nothing)
            'Lesen der Artikeldaten war erfolgreich
            _ReadOK = True
        End If
    End Sub

    ''' <summary>
    ''' Flag - Lesesn der Artikeldaten war erfolgreich. Artikel vorhanden
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ReadOK As Boolean
        Get
            Return _ReadOK
        End Get
    End Property

    ''' <summary>
    ''' Anzeige der Artikel-Details.
    ''' Wird aufgerufen durch Event eListe_Click(). Aktualisiert die Anzeigefelder (Nummer/Text/Kommentar...)
    ''' </summary>
    Private Sub DetailInfo(sender)
        'Textfelder
        tArtikelNummer.Text = _Artikel.Nummer
        tArtikelName.Text = _Artikel.Bezeichnung
        tArtikelKommentar.Text = _Artikel.Kommentar

        'Rezeptzuordnung - Chargengrößen
        KompRzChargen.GetDataFromKomp(_Artikel)
        'Anzeigen der Werte
        KompRzChargen.DataValid = True
        'Keine Buttons für Nährwerte/Cloud
        KompRzChargen.HideButtons()

        'Bestell- und Chargendaten
        With wb_Planung_Shared.ErrorList(_ErrIndex)

            'Fehlertext
            tbErrorText.Text = .ChrTeilerResultToString
            tbErrorText.ForeColor = System.Drawing.Color.Red

            'Bestellte Menge
            tBestStk.Text = wb_Functions.FormatStr(.Bestellt_Stk.ToString, 0) & " Stk"
            'Bestellte Menge gesamt in kg
            Dim Bestellt_kg As Double = (KompRzChargen.StkGewicht * .Bestellt_Stk) / 1000
            tBstkg.Text = wb_Functions.FormatStr(Bestellt_kg.ToString, 1) & " kg"

            'Ganze Chargen
            tChrgStk.Text = wb_Functions.FormatStr(.TeigChargen.AnzahlOpt, 0) & " x"
            tChrgGr.Text = wb_Functions.FormatStr(.TeigChargen.MengeOpt, 1) & " kg"
            Dim Opt_kg As Double = .TeigChargen.MengeOpt * .TeigChargen.AnzahlOpt
            tChrgkg.Text = wb_Functions.FormatStr(Opt_kg, 1) & " kg"

            'Rest-Chargen
            tRestStk.Text = wb_Functions.FormatStr(.TeigChargen.AnzahlRest, 0) & " x"
            tRestGr.Text = wb_Functions.FormatStr(.TeigChargen.MengeRest, 1) & " kg"
            Dim Rest_kg As Double = .TeigChargen.MengeRest * .TeigChargen.AnzahlRest
            tRestkg.Text = wb_Functions.FormatStr(Rest_kg, 1) & " kg"
        End With
    End Sub

    Private Sub DataInvalidated() Handles KompRzChargen.DataInvalidated
        'Daten wurden geändert - Datensatz speichern
        KompRzChargen.SaveData(_Artikel)
        'Update nur Parameter (NICHT Artikelbezeichnung... diese werden nur in GridUpdate aktualisiert)
        _Artikel.MySQLdbUpdate(False)
    End Sub

End Class