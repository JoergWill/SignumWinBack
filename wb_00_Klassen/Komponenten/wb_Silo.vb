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
    End Sub

    '' <summary>
    '' Create a new step with the given parent
    '' </summary>
    '' <param name="parent">The parent step</param>
    '' <param name="name">The name of this step</param>
    Public Sub New(parent As wb_Silo, Bezeichnung As String)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        'Default-Einstellungen Silo-Grafik (Größe und X-Wert)
        _pnlSiloHeight = pnlSilo.Height
        _pnlSiloTop = pnlSilo.Top

        _parentStep = parent
        _KompBezeichnung = Bezeichnung

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

    Public WriteOnly Property Befuellung As Boolean
        Set(value As Boolean)
            _Befuellung = value
            tbBefMenge.Visible = value
            lbBefMenge.Visible = value
            BtnSiloNull.Visible = value
            BtnSiloTauschen.Visible = value
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
                tbIst.BackColor = tbIst.BackColor
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
            If RohstoffAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'Rohstoff-Nummer(alpha) und Bezeichnung ändern
                wb_Rohstoffe_Shared.RohStoff.Bezeichnung = RohstoffAuswahl.RohstoffName
                wb_Rohstoffe_Shared.RohStoff.Nummer = RohstoffAuswahl.RohstoffNummer

                'MySQLChange_Silo(KompNr, RohstoffAuswahl.RohstoffNummer, RohstoffAuswahl.RohstoffName)
                'Zuordnung in OrgaBack löschen
                If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
                    MsSQLDelete_MFF201(KompNummer)
                End If
                'Anzeige aktualisieren
                wb_Rohstoffe_Shared.Edit_Leave(sender)
            End If
        Else
            'Rohstoff wird noch verwendet
            MsgBox("Dieser Silo-Rohstoff wird noch in Rezepturen verwendet und kann nicht geändert werden !", MsgBoxStyle.Critical, "Silo-Rohstoff tauschen")
        End If

    End Sub

    ''' <summary>
    ''' Siloinhalt auf Null setzen.
    ''' Setzt einen internen Tara-Wert zum Nullen des Silos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSiloNull_Click(sender As Object, e As EventArgs) Handles BtnSiloNull.Click
        'Wenn die Restmenge größer als 2000 kg ist nachfragen
        If _IstMenge > 2000 Then
            If MsgBox("Soll der Silo-Füllstand wirklich auf Null gesetzt werden ?", MsgBoxStyle.Question, "Silo Null setzen") = MsgBoxResult.Ok Then
                Exit Sub
            End If
        Else
            'Tarawert merken
            _TaraWert = _IstMenge
            'Anzeige aktualisieren
            IstMenge = _IstMenge
        End If

        'Nullsetzen verbuchen - Wird im Moment nicht verwendet !
        If Not _Befuellung And BtnSiloNull.Visible Then
            'Die Daten werden im Objekt wb_Lagersilo gehalten
            Dim LagerSilo As New wb_LagerSilo
            LagerSilo.CopyFrom(Me)
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'Lieferung (Nullsetzen) verbuchen
            Dim Lieferungen As New wb_Lieferungen
            Lieferungen.Verbuchen(winback, LagerSilo)
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
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateKompName, KompNr, KompNummer_Neu, KompBezeichnung_Neu))
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
End Class
