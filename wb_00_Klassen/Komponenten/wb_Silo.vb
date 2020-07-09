Public Class wb_Silo

    Private _MaxMenge As Integer
    Private _IstMenge As Integer
    Private _SiloReiheMaxMenge As Integer = wb_Global.UNDEFINED
    Private _KompNr As Integer
    Private _KompNummer As String
    Private _KompBezeichnung As String

    Private _pnlSiloHeight As Integer
    Private _pnlSiloTop As Integer

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'Default-Einstellungen Silo-Grafik (Größe und X-Wert)
        _pnlSiloHeight = pnlSilo.Height
        _pnlSiloTop = pnlSilo.Top
    End Sub

    Public Property MaxMenge As Integer
        Set(value As Integer)
            _MaxMenge = value

            'Textfeld ausblenden wenn keine Silo-Größe angegeben ist
            If _MaxMenge > 0 Then
                tbMax.Text = _MaxMenge & " kg"
                tbMax.Visible = True
                lbMax.Visible = True
            Else
                tbMax.Visible = False
                lbMax.Visible = False
            End If
        End Set
        Get
            Return _MaxMenge
        End Get
    End Property

    Public Property SiloReiheMaxMenge As Integer
        Get
            Return _SiloReiheMaxMenge
        End Get
        Set(value As Integer)
            _SiloReiheMaxMenge = value

            'Höhe der Silo-Grafik im Verhältnis zu den anderen Silos der Reihe
            If _SiloReiheMaxMenge > 0 Then
                pnlSilo.Height = _pnlSiloHeight * _MaxMenge / _SiloReiheMaxMenge
                pnlSilo.Top = _pnlSiloTop + _pnlSiloHeight - pnlSilo.Height
            Else
                pnlSilo.Height = _pnlSiloHeight
                pnlSilo.Top = _pnlSiloTop
            End If
        End Set
    End Property

    Public Property IstMenge As Integer
        Get
            Return _IstMenge
        End Get
        Set(value As Integer)
            'Silo-Füllstand aktualisieren
            _IstMenge = value
            'Prüfen welches Silo das aktive Silo ist
            CheckBtn()

            'Textfeld ausblenden wenn keine Silo-Größe angegeben ist
            If _IstMenge > 0 Then
                tbIst.Text = _IstMenge & " kg"
                tbIst.Visible = True
                lbIst.Visible = True

                'Anteil in Prozent zur Silo-Größe
                If _MaxMenge > 0 Then
                    Dim Prz As Integer = (_IstMenge / _MaxMenge * 100)
                    pnlSilo.RowStyles.Item(0).Height = 100 - Prz
                    pnlSilo.RowStyles.Item(1).Height = Prz
                    tbSiloFuellstand.Visible = True
                Else
                    tbSiloFuellstand.Visible = False
                End If
            Else
                tbIst.Visible = False
                lbIst.Visible = False
                tbSiloFuellstand.Visible = False
            End If
        End Set
    End Property

    Public Property KompNr As Integer
        Get
            Return _KompNr
        End Get
        Set(value As Integer)
            _KompNr = value
        End Set
    End Property

    Public Property KompNummer As String
        Get
            Return _KompNummer
        End Get
        Set(value As String)
            _KompNummer = value
            lblNummer.Text = "Nr. " & _KompNummer
        End Set
    End Property

    Public Property KompBezeichnung As String
        Get
            Return _KompBezeichnung
        End Get
        Set(value As String)
            _KompBezeichnung = value
            lblRohName.Text = _KompBezeichnung
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
            Dim RohstoffAuswahl As New wb_Rohstoff_AuswahlListe
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
                    MsSQLDelete_MFF200(KompNummer)
                End If
                'Anzeige aktualisieren
                wb_Rohstoffe_Shared.Edit_Leave(sender)
            End If
        Else
            'Rohstoff wird noch verwendet
            MsgBox("Dieser Silo-Rohstoff wird noch in Rezepturen verwendet und kann nicht geändert werden !", MsgBoxStyle.Critical, "Silo-Rohstoff tauschen")
        End If

    End Sub

    Private Sub MsSQLDelete_MFF200(KompNummer As String)
        'Datenbank-Verbindung öffnen - MsSQL
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Datensatz mit DELETE löschen
        orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlDeleteArtikelMFF, KompNummer, wb_Global.MFF_KO_Nr))
        'Verbindung zur Datenbank wieder schliessen
        orgaback.Close()
    End Sub

    Private Sub MySQLChange_Silo(KompNr As Integer, KompNummer_Neu As String, KompBezeichnung_Neu As String)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Rohstoff-Nummer und Bezeichnung ändern. Interne Komponenten-Nummer bleibt (Silo-Rohstoff)
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateKompName, KompNr, KompNummer_Neu, KompBezeichnung_Neu))
        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub


End Class
