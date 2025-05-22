Imports System.Reflection

Public Class wb_Silo

    Private _MaxMenge As Integer
    Private _IstMenge As Integer
    Private _RestMenge As Integer
    Private _VerbrauchtMenge As Double = 0.0
    Private _TaraWert As Integer
    Private _ChargenNummer As String
    Private _Preis As Double
    Private _SiloReiheMaxMenge As Integer = wb_Global.UNDEFINED
    Private _Befuellung As Boolean = False
    Private _KompNr As Integer
    Private _KompNummer As String
    Private _KompBezeichnung As String
    Private _KompKommentar As String
    Private _Aktiv As String

    Private _pnlSiloHeight As Integer
    Private _pnlSiloTop As Integer
    Const _pnlSiloMinHeight = 80

    Private _LagerOrt As String
    Private _SiloNr As Integer
    Private _RohSiloType As wb_Global.RohSiloTypen = wb_Global.RohSiloTypen.UNDEF

    Private _parentStep As wb_Silo
    Private _childSteps As New ArrayList()

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        'Default-Einstellungen Silo-Grafik (Größe und X-Wert)
        _pnlSiloHeight = pnlSilo.Height
        _pnlSiloTop = pnlSilo.Top

        'Wenn das WinBack.AddIn.ob_Rohstoffe_SiloInventur nicht geladen ist wird der Button ausgeblendet
        If Not wb_Main_Shared.IsRegistered("ob_Rohstoffe_SiloInventur") Then
            BtnBestandsKorrektur.Enabled = False
            BtnSiloNull.Enabled = False
        End If
    End Sub

    '' <summary>
    '' Create a new step with the given parent
    '' </summary>
    '' <param name="parent">The parent step</param>
    '' <param name="name">The name of this step</param>
    Public Sub New(parent As wb_Silo, Bezeichnung As String, Kommentar As String)
        Me.New()

        _parentStep = parent
        _KompBezeichnung = Bezeichnung
        _KompKommentar = Kommentar

        'Es gibt keinen Root-Knoten (erster Knoten in der Reihe)
        If Not (_parentStep Is Nothing) Then
            parent._childSteps.Add(Me)
        End If
    End Sub

    Public Sub CopyFrom(s As wb_Silo)
        MaxMenge = s.MaxMenge
        KompNr = s.KompNr
        KompNummer = s.KompNummer
        KompBezeichnung = s.KompBezeichnung
        KompKommentar = s.KompKommentar
        ChargenNummer = s.ChargenNummer
        LagerOrt = s.LagerOrt
        SiloNr = s.SiloNr
        Aktiv = s.Aktiv
        Preis = s.Preis
        _TaraWert = s.TaraWert
    End Sub

    ''' <summary>
    ''' Innere Funktion des Bubble-Sort-Algorithmus.
    ''' Führt den Vergleich der Silo-Nummer durch und vertauscht die Parameter falls notwendig
    ''' Wenn getauscht wurde, wird true zurückgegeben.
    ''' 
    '''     Funktion:
    '''      if A[j] > A[j+1] then {
    '''         temp := A[j];
    '''         A[j] := A[j+1];
    '''         A[j+1] := temp;
    '''         
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Function BubleSort(s As wb_Silo) As Boolean
        'Temporäre Variablen zum Tauschen
        Dim TempMaxMenge As Integer
        Dim TempKompNr As Integer
        Dim TempKompNummer As String
        Dim TempKompBezeichnung As String
        Dim TempKompKommentar As String
        Dim TempLagerOrt As String
        Dim TempSiloNr As Integer
        Dim TempAktiv As String

        'prüfen ob getauscht werden muss
        If SiloNr > s.SiloNr Then
            'Tauschen (Me -> Dummy) 
            TempMaxMenge = MaxMenge
            TempKompNr = KompNr
            TempKompNummer = KompNummer
            TempKompBezeichnung = KompBezeichnung
            TempKompKommentar = KompKommentar
            TempLagerOrt = LagerOrt
            TempSiloNr = SiloNr
            TempAktiv = Aktiv
            'Tauschen(s -> Me)
            CopyFrom(s)
            'Tauschen(Dummy -> s)
            s.MaxMenge = TempMaxMenge
            s.KompNr = TempKompNr
            s.KompNummer = TempKompNummer
            s.KompBezeichnung = TempKompBezeichnung
            s.KompKommentar = TempKompKommentar
            s.LagerOrt = TempLagerOrt
            s.SiloNr = TempSiloNr
            s.Aktiv = TempAktiv
            'Vergleich positiv- Tauschen war notwendig
            Return True
        Else
            'Vergleich negativ - kein Tauschen notwendig
            Return False
        End If
    End Function

    '' <summary>
    '' Parent dieses Silos
    '' </summary>
    Public Property ParentStep() As wb_Silo
        Get
            Return _parentStep
        End Get
        Set(ByVal value As wb_Silo)
            _parentStep = value
        End Set
    End Property

    '' <summary>
    '' Liste aller Silos mit gleicher Rohstoff-Nummer
    '' </summary>
    Public ReadOnly Property ChildSteps() As IList
        Get
            Return _childSteps
        End Get
    End Property

    ''' <summary>
    ''' Maximale Füllmenge. Entspricht dem Fassungsvermögen des Silos.
    ''' Steht in der Datenbank im Datenfeld LG_Kommentar
    ''' </summary>
    ''' <returns></returns>
    Public Property MaxMenge As Integer
        Set(value As Integer)
            _MaxMenge = value

            'Textfeld ausblenden wenn keine Silo-Größe angegeben ist
            If _MaxMenge > 0 Then
                tbMax.Text = _MaxMenge & " kg"
                tbMax.Visible = True
                lbMax.Visible = True
                pnlSilo.Visible = True
            Else
                tbMax.Visible = False
                lbMax.Visible = False
                pnlSilo.Visible = False
            End If
        End Set
        Get
            Return _MaxMenge
        End Get
    End Property

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property Befuellung As Boolean
        Set(value As Boolean)
            _Befuellung = value
            tbBefMenge.Visible = value
            lbBefMenge.Visible = value
            BtnSiloNull.Visible = value
            BtnSiloTauschen.Visible = value
            BtnBestandsKorrektur.Visible = Not value
        End Set
    End Property

    Public Property RestMenge As Integer
        Get
            Return _RestMenge
        End Get
        Set(value As Integer)
            _RestMenge = value
        End Set
    End Property

    Public ReadOnly Property BefMenge As Integer
        Get
            Return wb_Functions.StrToInt(tbBefMenge.Text)
        End Get
    End Property

    Public ReadOnly Property TaraWert As Integer
        Get
            Return _TaraWert
        End Get
    End Property

    Private Sub tbLieferung_TextChanged(sender As Object, e As EventArgs) Handles tbBefMenge.TextChanged
        tbBefMenge.Text = BefMenge.ToString & " kg"
        wb_Rohstoffe_Shared.BefMenge_Changed(sender)
    End Sub

    Private Sub tbLieferung_DoubleClick(sender As Object, e As EventArgs) Handles tbBefMenge.DoubleClick
        If BefMenge + RestMenge > 0 Then
            tbBefMenge.Text = (BefMenge + RestMenge).ToString & " kg"
            wb_Rohstoffe_Shared.BefMenge_Changed(sender)
        End If
    End Sub

    ''' <summary>
    ''' Größtes Silo in der Silo-Reihe.
    ''' Bestimmt den Faktor der Darstellung der Silo-Füllstände
    ''' </summary>
    Public Property SiloReiheMaxMenge As Integer
        Get
            Return _SiloReiheMaxMenge
        End Get
        Set(value As Integer)
            _SiloReiheMaxMenge = value

            'Höhe der Silo-Grafik im Verhältnis zu den anderen Silos der Reihe
            If _SiloReiheMaxMenge > 0 Then
                pnlSilo.Height = Math.Max(_pnlSiloHeight * _MaxMenge / _SiloReiheMaxMenge, _pnlSiloMinHeight)
                pnlSilo.Top = _pnlSiloTop + _pnlSiloHeight - pnlSilo.Height
            Else
                pnlSilo.Height = _pnlSiloHeight
                pnlSilo.Top = _pnlSiloTop
            End If
        End Set
    End Property

    ''' <summary>
    ''' Silo-Füllstand
    ''' </summary>
    ''' <returns></returns>
    Public Property IstMenge As Integer
        Get
            Return _IstMenge - _TaraWert
        End Get
        Set(value As Integer)
            'Silo-Füllstand aktualisieren
            _IstMenge = value - _TaraWert
            'Prüfen welches Silo das aktive Silo ist
            CheckBtn()

            'Textfeld rot anzeigen wenn Füllstand negativ
            If _IstMenge >= 0 Then
                tbIst.ForeColor = System.Drawing.Color.Black
                tbIst.Text = _IstMenge & " kg"

                'Anteil in Prozent zur Silo-Größe
                If _MaxMenge > 0 Then
                    'Prozent Füllstand (maximal 100%)
                    Dim Prz As Integer = Math.Min(100, (_IstMenge / _MaxMenge * 100))
                    pnlSilo.RowStyles.Item(0).Height = 100 - Prz
                    pnlSilo.RowStyles.Item(1).Height = Prz
                    tbSiloFuellstand.Visible = True
                Else
                    tbSiloFuellstand.Visible = False
                End If
            Else
                tbIst.Text = _IstMenge & " kg"
                tbIst.ForeColor = System.Drawing.Color.Red
                tbSiloFuellstand.Visible = False
            End If
        End Set
    End Property

    ''' <summary>
    ''' Komponenten-Nummer(Inter) zum Silo-Rohstoff
    ''' </summary>
    ''' <returns></returns>
    Public Property KompNr As Integer
        Get
            Return _KompNr
        End Get
        Set(value As Integer)
            _KompNr = value
        End Set
    End Property

    ''' <summary>
    ''' Rohstoff-Nummer(ASCII) zum Silo
    ''' </summary>
    ''' <returns></returns>
    Public Property KompNummer As String
        Get
            Return _KompNummer
        End Get
        Set(value As String)
            _KompNummer = value
            lblNummer.Text = "Nr. " & _KompNummer
        End Set
    End Property

    ''' <summary>
    ''' Rohstoff-Bezeichnung zum Silo
    ''' </summary>
    ''' <returns></returns>
    Public Property KompBezeichnung As String
        Get
            Return _KompBezeichnung
        End Get
        Set(value As String)
            _KompBezeichnung = value
            lblRohName.Text = _KompBezeichnung
        End Set
    End Property

    ''' <summary>
    ''' Lagerort zum Silo. Anhand der Lagerort-Bezeichnung wird auch die Silo-Type bestimmt
    '''     - (M)   Mehlsilo
    '''     - (MK)  Mittelkomponenten
    '''     - (KKA) Kleinkomponenten
    '''     - (BW)  Bodenwaage
    ''' </summary>
    ''' <returns></returns>
    Public Property LagerOrt As String
        Get
            Return _LagerOrt
        End Get
        Set(value As String)
            _LagerOrt = value
            _RohSiloType = wb_Rohstoffe_Shared.GetRohSiloType(_LagerOrt)
        End Set
    End Property

    ''' <summary>
    ''' Silonummer
    ''' </summary>
    ''' <returns></returns>
    Public Property SiloNr As Integer
        Get
            Return _SiloNr
        End Get
        Set(value As Integer)
            _SiloNr = value
            If _RohSiloType = wb_Global.RohSiloTypen.BW Then
                lblName.Text = "Beh." & _SiloNr.ToString
            Else
                lblName.Text = "Silo " & _SiloNr.ToString
            End If
        End Set
    End Property

    ''' <summary>
    ''' Silo-Type. Wird aus dem Lagerort ermittelt
    ''' 
    '''     - (M)   Mehlsilo
    '''     - (MK)  Mittelkomponenten
    '''     - (KKA) Kleinkomponenten
    '''     - (BW)  Bodenwaage
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RohSiloType As wb_Global.RohSiloTypen
        Get
            Return _RohSiloType
        End Get
    End Property

    ''' <summary>
    ''' Silo Aktiv/Hand/Aus
    ''' </summary>
    ''' <returns></returns>
    Public Property Aktiv As String
        Get
            Return _Aktiv
        End Get
        Set(value As String)
            _Aktiv = value

            'Flag anzeigen
            Select Case _Aktiv
                Case "A"
                    lbAktiv.Text = "Aktiv"
                    lbAktiv.Visible = True
                Case "H"
                    lbAktiv.Text = "Hand"
                    lbAktiv.Visible = True
                Case Else
                    lbAktiv.Visible = False
            End Select
        End Set
    End Property

    Public Property ChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
        Set(value As String)
            _ChargenNummer = value
        End Set
    End Property

    Public Property Preis As Double
        Get
            Return _Preis
        End Get
        Set(value As Double)
            _Preis = value
        End Set
    End Property

    Public Property VerbrauchtMenge As Double
        Get
            Return _VerbrauchtMenge
        End Get
        Set(value As Double)
            _VerbrauchtMenge = value
        End Set
    End Property

    Public Property KompKommentar As String
        Get
            Return _KompKommentar
        End Get
        Set(value As String)
            _KompKommentar = value
            lblKommentar.Text = _KompKommentar
        End Set
    End Property

    Private Sub CheckBtn()
        If wb_Rohstoffe_Shared.RohStoff.Nr = _KompNr Then
            BtnSiloTauschen.Visible = True
        Else
            BtnSiloTauschen.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Silo-Rohstoff neu definieren:
    '''     - Prüfen ob der Rohstoff noch in Rezepturen verwendet wird
    '''     - Verweis auf die winback.Komponente in OrgaBack löschen
    '''     - Name und Nummer aus der neuen Komponente kopieren
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSiloTauschen_Click(sender As Object, e As EventArgs) Handles BtnSiloTauschen.Click
        'Prüfen ob der Silo-Rohstoff noch in Rezepturen verwendet wird
        If Not wb_Rohstoffe_Shared.RohStoff.MySQLIsUsedInRecipe(KompNr) Then

            'Auswahl neuer Rohstoff für Silo (Rohstoff muss schon vorhanden sein)
            Dim RohstoffAuswahl As New wb_Rohstoffe_AuswahlListe
            'Anzeige nur Hand/Auto-Komponenten
            RohstoffAuswahl.Anzeige = wb_Rohstoffe_Shared.AnzeigeFilter.HandAuto

            'Anzeige Auswahl-Fenster
            If RohstoffAuswahl.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                'Rohstoff-Nummer(alpha) und Bezeichnung ändern
                wb_Rohstoffe_Shared.RohStoff.Bezeichnung = RohstoffAuswahl.RohstoffName
                wb_Rohstoffe_Shared.RohStoff.Nummer = RohstoffAuswahl.RohstoffNummer

                MySQLChange_Silo(KompNr, RohstoffAuswahl.RohstoffNummer, RohstoffAuswahl.RohstoffName)
                'Zuordnung in OrgaBack löschen
                If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
                    MsSQLDelete_MFF201(KompNummer)
                End If
                'Silo-Tabellen neu laden
                wb_Rohstoffe_Shared.Load_SiloTables()
                'Anzeige aktualisieren
                wb_Rohstoffe_Shared.Liste_Click(sender, True)
                'Rohstoff-Liste aktualisieren
                wb_Rohstoffe_Shared.Edit_Leave(sender)
            End If
        Else
            'Rohstoff wird noch verwendet
            MsgBox("Dieser Silo-Rohstoff wird noch in Rezepturen verwendet und kann nicht geändert werden !", MsgBoxStyle.Critical, "Silo-Rohstoff tauschen")
        End If

    End Sub

    ''' <summary>
    ''' Siloinhalt auf Null setzen.
    ''' Setzt einen internen Tara-Wert zum Nullen des Silos. Die Verbuchung erfolgt später über den Zugang(WE)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
    Private Sub BtnSiloNull_Click(sender As Object, e As EventArgs) Handles BtnSiloNull.Click
        'Wenn die Restmenge größer als 2000 kg ist nachfragen
        If (_IstMenge > 2000) AndAlso MsgBox("Soll der Silo-Füllstand wirklich auf Null gesetzt werden ?", MsgBoxStyle.Question, "Silo Null setzen") <> MsgBoxResult.Ok Then
            Exit Sub
        End If
        'Tarawert merken
        _TaraWert = _IstMenge
        'Anzeige aktualisieren
        IstMenge = _IstMenge

        'Nullsetzen verbuchen - Wird im Moment nicht verwendet !
        If Not _Befuellung AndAlso BtnSiloNull.Visible And False Then
            'Die Daten werden im Objekt wb_Lagersilo gehalten
            Dim LagerSilo As New wb_LagerSilo
            LagerSilo.CopyFrom(Me)
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'Lieferung (Nullsetzen) verbuchen
            Dim Lieferungen As New wb_Lieferungen
            Lieferungen.Verbuchen(winback, LagerSilo)
            'Bilanzmenge in winback-DB aktualisieren (lfd-Nummer wird nicht verwendet)
            Lieferungen.UpdateLagerorte(winback, LagerOrt)
            'Datenbank-Verbindung wieder schliessen
            winback.Close()
        End If
    End Sub

    ''' <summary>
    ''' Löscht in der OrgaBack-DB die Verknüpfung zwischen OrgaBack-Artikel(Rohstoff) und WinBack-Komponente.
    ''' Im MFF201 steht die winback-ID als direkter Verweis auf die interne Komponenten-Nr.
    ''' </summary>
    ''' <param name="KompNummer"></param>
    Private Sub MsSQLDelete_MFF201(KompNummer As String)
        'Datenbank-Verbindung öffnen - MsSQL
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Datensatz mit DELETE löschen
        orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlDeleteArtikelMFF, KompNummer, wb_Global.MFF_KO_Nr))
        'Verbindung zur Datenbank wieder schliessen
        orgaback.Close()
    End Sub

    ''' <summary>
    ''' Ändert Komponenten-Nummer(ASCII) und Komponenten-Bezeichnung in der WinBack-Datenbank.
    ''' </summary>
    ''' <param name="KompNr"></param>
    ''' <param name="KompNummer_Neu"></param>
    ''' <param name="KompBezeichnung_Neu"></param>
    Private Sub MySQLChange_Silo(KompNr As Integer, KompNummer_Neu As String, KompBezeichnung_Neu As String)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Rohstoff-Nummer und Bezeichnung ändern. Interne Komponenten-Nummer bleibt (Silo-Rohstoff)
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateKompName, KompNr, KompBezeichnung_Neu, KompNummer_Neu))
        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' DoppelClick auf Silo-Tab öffnet Parameter-Fenster
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Silo_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim SiloParameter As New wb_Rohstoffe_SiloParameter(Me)
        SiloParameter.ShowDialog()
    End Sub

    ''' <summary>
    ''' Öffnet Dialog-Fenster zur Bestandskorrektur und/oder Befüllung KKA/Sackschütte
    ''' 
    ''' Zunächst wird der Lagerbestand des aktuell angewählten Silos korrigiert: 
    '''     Bei einer Plus-Buchung wird ein neuer Waren-Eingang ohne Chargen-Nummer eingebucht.
    '''     Bei einer Minus-Buchung werden die bestehenden Lieferungen solange abgebucht, bis der neue Bilanzwert erreicht ist.
    '''     
    ''' Anschliessen wird in OrgaBack eine Inventurbuchung durchgeführt. Die Inventurbuchung enthält die Summe aller Silo-Füllstände
    ''' und eine Liste aller Chargen-Nummern aus den Silos mit der entsprechenden Rohstoff-Nummer.
    ''' Passende Handkomponenten werden ebenfalls im Bestand erfasst.
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnBestandsKorrektur_Click(sender As Object, e As EventArgs) Handles BtnBestandsKorrektur.Click, tbIst.DoubleClick

        'Dialog-Fenster - Neuer Silo-Füllstand
        Dim SiloKorrektur As New wb_Rohstoffe_SiloKorrektur(Me)
        If SiloKorrektur.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            'Cursor umschalten
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            'abhängig von der neuen Istmenge
            Select Case SiloKorrektur.KorrekturModus

                'Silo Nullsetzen
                Case wb_Global.KorrekturStatus.SILO_NULLEN
                    SiloNullen()

                'Silo Füllstand - Abgang buchen
                Case wb_Global.KorrekturStatus.SILO_MINUS
                    SiloMinus(SiloKorrektur.MengeNeu, SiloKorrektur.Istmenge)

                'Silo Füllstand - Zugang buchen
                Case wb_Global.KorrekturStatus.SILO_PLUS
                    SiloPlus(SiloKorrektur.MengeNeu, SiloKorrektur.Istmenge)
            End Select

            'Bestandskorrektur in OrgaBack durchführen
            SiloBestandsKorrektur_OrgaBack()

            'Cursor wieder zurückschalten
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub SiloNullen()
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Die Daten werden im Objekt wb_Lagersilo gehalten
        Dim LagerSilo As New wb_LagerSilo
        LagerSilo.CopyFrom(Me)
        'Lieferung (Nullsetzen) verbuchen
        Dim Lieferungen As New wb_Lieferungen
        Lieferungen.Verbuchen_Tara(winback, LagerSilo, wb_GlobalSettings.RohChargen_ErfassungAktiv)
        'Bilanzmenge in winback-DB aktualisieren (lfd-Nummer wird nicht verwendet)
        Lieferungen.UpdateLagerorte(winback, LagerOrt)
        'Anzeige aktualisieren
        IstMenge = 0

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    Private Sub SiloMinus(BilanzMengeNeu As Integer, IstMengeVorher As Integer)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Anzeige aktualisieren
        IstMenge = BilanzMengeNeu
        'Lieferung (Abgang) verbuchen
        Dim Lieferungen As New wb_Lieferungen
        Lieferungen.ProduktionVerbuchen(LagerOrt, (IstMengeVorher - BilanzMengeNeu).ToString)

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    Private Sub SiloPlus(BilanzMengeNeu As Integer, IstMengeVorher As Integer)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Die Daten werden im Objekt wb_Lagersilo gehalten
        Dim LagerSilo As New wb_LagerSilo
        LagerSilo.CopyFrom(Me)
        'Korrektur über Befüllung
        LagerSilo.BefMenge = BilanzMengeNeu - IstMengeVorher
        LagerSilo.ChargenNummer = "Korrektur"
        'Lieferung (Zugang) verbuchen
        Dim Lieferungen As New wb_Lieferungen
        Lieferungen.Bilanzmenge = IstMengeVorher
        Lieferungen.Verbuchen(winback, LagerSilo)
        'Anzeige aktualisieren
        IstMenge = Lieferungen.Bilanzmenge
        'Bilanzmenge in winback-DB aktualisieren (lfd-Nummer wird nicht verwendet)
        Lieferungen.UpdateLagerorte(winback, LagerOrt)

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Bestands-Korrektur in OrgaBack "rückwärts" über Inventur.
    ''' 
    ''' Erzeugt eine kommagetrennte Liste von Rohstoff-Chargennummern aus winback.Lieferungen mit dieser Rohstoff-Nummer(alpha) bis die Menge x erreicht ist
    ''' oder keine offenen Lieferungen mehr vorhanden sind.
    ''' 
    ''' Anschliessend wird eine InventurBuchung (mit der Gesamtmenge aller Silos und der Liste aller Chargen-Nummern) in OrgaBack erzeugt.
    ''' </summary>
    Private Sub SiloBestandsKorrektur_OrgaBack()

        'Gesamt-Bestand aller Silos und Handkomponenten mit dieser Rohstoff-Nummer
        Dim Bilanzmenge As Double
        Dim BilanzSumme As Double = 0.0
        'da die Chargen-Nummer in OrgaBack auf 15 Zeichen begrenzt ist, wird nur der Text 'Korrektur' als Chargen-Nummer eingetragen
        Dim ListeRohstoffChargenNummern As String = "Korrektur"

        'die nachfolgende Berechnung dauert länger
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'alle Bilanzmengen zu dieser Rohstoff-Nummer (negative Mengen werden ignoriert)
        Dim sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSiloGrpBilanz, KompNummer)

        'Prüfen ob ein Datensatz vorhanden ist
        If winback.sqlSelect(sql) Then
            'über alle Datensätze
            While winback.Read
                'Bilanzmengen aufsummieren
                Bilanzmenge = wb_Functions.StrToDouble(winback.sField("LG_BilanzMenge"))
                If Bilanzmenge > 0 Then
                    BilanzSumme += Bilanzmenge
                End If
            End While
        End If
        'Verbindung wieder freigeben
        winback.CloseRead()

        'Kommagetrennte Liste aller Chargen (Not used)
        'Dim Lieferungen As New wb_Lieferungen
        'ListeRohstoffChargenNummern = Lieferungen.GetChargenListe(winback, KompNummer, BilanzSumme)

        'Gesamt-Menge als Inventur-Buchung in OrgaBack eintragen
        If Not ob_Rohstoffe_SiloInventur.Bestandskorrektur(KompNummer, BilanzSumme, ListeRohstoffChargenNummern) Then
            'Berechnung beendet
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'Fehlermeldung ausgeben
            Me.Cursor = System.Windows.Forms.Cursors.Default
            MsgBox("Fehler bei der Bestandskorrektur" & vbCrLf & "Die Bestandsdaten konnten nicht in die Lagerkarte übernommen werden", MsgBoxStyle.Critical, "Bestandskorrektur")
        Else
            'ldf.Nummer letzter Eintrag aus der Lagerkarte OrgaBack
            Dim lfdNr As Integer = OrgaBackLagerkarteLfd()
            'Tabelle Lieferungen in WinBack aktualisieren (lfd-Nummer)
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateLagerOrte, LagerOrt, "LG_LF_Nr = " & lfdNr)
            winback.sqlCommand(sql)

            'Berechnung beendet
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'Silo-Bestandskorrektur wurde erfoglreich durchgeführt - Meldung ausgeben
            Me.Cursor = System.Windows.Forms.Cursors.Default
            MsgBox("Bestandskorrektur für Silo " & SiloNr & " erfolgreich durchgeführt" & vbCrLf & "Menge im Silo " & IstMenge & " kg." & vbCrLf & "Gesamt Lager  " & BilanzSumme & " kg", MsgBoxStyle.Information, "Bestandskorrektur")
        End If

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Gibt die letzte LfdNr. aus der Tabelle [dbo.ArtikelLagerkarte] zum Rohstoff(Silo) zurück
    ''' Wenn kein Eintrag existiert wird 0 zurückgegeben
    ''' </summary>
    ''' <returns></returns>
    Private Function OrgaBackLagerkarteLfd() As Integer
        'Datenbank-Verbindung öffnen OrgaBack-msSQL
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'alle Buchungen ausgehend von der letzten Buchung einlesen
        Dim Sql = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlArtikelLagerLast, KompNummer)

        Try
            If orgaback.sqlSelect(Sql) Then
                'wenn Einträge vorhanden sind
                If orgaback.Read Then
                    Return orgaback.iField("Lfd")
                End If
            End If
        Catch ex As Exception
        End Try

        'Default - Kein Datensatz gefunden
        Return 0
    End Function

End Class
