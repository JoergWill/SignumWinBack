Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack
Imports WinBack.wb_Rohstoffe_Shared

Public Class wb_Rohstoffe_Tauschen
    Inherits DockContent

    Private _NummerAkt As String
    Private _NrAkt As Integer
    Private _BezeichnungAkt As String
    Private _RohstoffType As wb_Global.KomponTypen

    Private _NrNeu As Integer = wb_Global.UNDEFINED

    Public Property NummerAkt As String
        Get
            Return _NummerAkt
        End Get
        Set(value As String)
            _NummerAkt = value
            tbRohNrOrg.Text = _NummerAkt
        End Set
    End Property

    Public Property NrAkt As Integer
        Get
            Return _NrAkt
        End Get
        Set(value As Integer)
            _NrAkt = value
        End Set
    End Property

    Public Property BezeichnungAkt As String
        Get
            Return _BezeichnungAkt
        End Get
        Set(value As String)
            _BezeichnungAkt = value
            tbRohNameOrg.Text = _BezeichnungAkt
        End Set
    End Property

    Public Property RohstoffType As wb_Global.KomponTypen
        Get
            Return _RohstoffType
        End Get
        Set(value As wb_Global.KomponTypen)
            _RohstoffType = value
        End Set
    End Property

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Public Sub New(Nr, Nummer, Bezeichnung)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        NrAkt = Nr
        NummerAkt = Nummer
        BezeichnungAkt = Bezeichnung
    End Sub

    Private Sub wb_Rohstoffe_Tauschen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'wenn die Rohstoff-Daten noch nicht definiert sind - Daten aus wb_Rohstoff_Shared
        If NummerAkt Is Nothing Then
            'Rohstoff Nummer
            NummerAkt = RohStoff.Nummer
            NrAkt = RohStoff.Nr
            'Rohstoff Name
            BezeichnungAkt = RohStoff.Bezeichnung
            'Rohstoff-Type
            RohstoffType = RohStoff.Type
        End If
    End Sub

    Private Sub tbRohNeu_Click(sender As Object, e As EventArgs) Handles tbRohNrNeu.Click, tbRohNrNeu.DoubleClick, tbRohNameNeu.DoubleClick, tbRohNameNeu.Click
        'Rohstoff-Auswahl-Liste
        Dim RohstoffAuswahl As New wb_Rohstoffe_AuswahlListe
        'Auswahl Sauerteig/Produktion
        If RohstoffType < wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL Then
            RohstoffAuswahl.Anzeige = AnzeigeFilter.RezeptKomp
        Else
            RohstoffAuswahl.Anzeige = AnzeigeFilter.Sauerteig
        End If
        'Auswahldialog Rohstoff
        If RohstoffAuswahl.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            tbRohNameNeu.Text = RohstoffAuswahl.RohstoffName
            tbRohNrNeu.Text = RohstoffAuswahl.RohstoffNummer
            _NrNeu = RohstoffAuswahl.RohstoffNr
        End If
    End Sub

    Private Sub cbTauschen_Click(sender As Object, e As EventArgs) Handles cbTauschen.Click
        If cbTauschen.Checked Then
            lbRohstoffNeu.Text = "tauschen mit"
            BtnOK.Text = "Rohstoffe tauschen"
            lblTauschen.Visible = True
        Else
            lbRohstoffNeu.Text = "ersetzen durch"
            BtnOK.Text = "Rohstoffe ersetzen"
            lblTauschen.Visible = False
        End If
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        'Prüfe ob ein Rohstoff ausgewählt wurde
        If _NrNeu <> wb_Global.UNDEFINED Then
            'Cursor umschalten
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'Gibt die Anzahl der geänderten Rezept-Zeilen zurück
            Dim AnzahlRezeptZeilen As Integer = 0

            'Tauschen oder Ersetzen
            If cbTauschen.Checked Then
                'nächste freie Rohstoff-Nummer suchen
                Dim DummyRohNr As Integer = wb_sql_Functions.getNewKomponNummer()

                'Rohstoff A zunächst durch Dummy ersetzen
                Dim AnzRez_A As Integer = TauscheRohstoffeImRezept(NrAkt, DummyRohNr, winback, False)
                'Rohstoff B wird ersetzt durch Rohstoff A
                AnzahlRezeptZeilen = TauscheRohstoffeImRezept(_NrNeu, NrAkt, winback, cbRezAenderungSpeichern.Checked)
                'Dummy wird Rohstoff B
                Dim AnzRez_B As Integer = TauscheRohstoffeImRezept(DummyRohNr, _NrNeu, winback, cbRezAenderungSpeichern.Checked)

                'alle Rezepte mit Rohstoff A updaten(Nährwert-Berechnung)
                wb_Rohstoffe_Shared.MySQLdbSetMarker(wb_Global.ArtikelMarker.nwtOK, NrAkt)
                'alle Rezepte mit Rohstoff B updaten(Nährwert-Berechnung)
                wb_Rohstoffe_Shared.MySQLdbSetMarker(wb_Global.ArtikelMarker.nwtOK, _NrNeu)

                'alle zwei Datenbank-Operationen müssen die gleiche Anzahl an Datensätzen zurückliefern
                If AnzRez_A <> AnzRez_B Then
                    'Fehler
                    AnzahlRezeptZeilen = -1
                Else
                    'Gesamtzahl aller Änderungen
                    AnzahlRezeptZeilen += AnzRez_A
                End If

                'Ereignis im Log-File festhalten
                Trace.WriteLine("@I_Rohstoff " & NrAkt & " tauschen mit " & _NrNeu & " Anzahl der Änderungen " & AnzahlRezeptZeilen)

            Else
                'Rohstoff A wird ersetzt durch Rohstoff B
                AnzahlRezeptZeilen = TauscheRohstoffeImRezept(NrAkt, _NrNeu, winback, cbRezAenderungSpeichern.Checked)
                'alle Rezepte mit Rohstoff Neu updaten(Nährwert-Berechnung)
                wb_Rohstoffe_Shared.MySQLdbSetMarker(wb_Global.ArtikelMarker.nwtOK, _NrNeu)

                'Ereignis im Log-File festhalten
                Trace.WriteLine("@I_Rohstoff " & NrAkt & " ersetzen durch " & _NrNeu & " Anzahl der Änderungen " & AnzahlRezeptZeilen)
            End If

            'Datenbank-Verbindung wieder schliessen
            winback.Close()
            'Cursor wieder auf Default
            Me.Cursor = System.Windows.Forms.Cursors.Default

            'Auswertung Ergebnis
            Select Case AnzahlRezeptZeilen
                Case -1
                    MsgBox("Fehler beim Ändern der Rezepturen !", MsgBoxStyle.Exclamation)
                    DialogResult = System.Windows.Forms.DialogResult.Abort
                Case 0
                    MsgBox("Rohstoff wird in keinen Rezepturen verwendet", MsgBoxStyle.Information)
                    DialogResult = System.Windows.Forms.DialogResult.No
                Case Else
                    MsgBox("Es wurden " & AnzahlRezeptZeilen.ToString & " Rezept-Zeilen geändert", MsgBoxStyle.Information)
                    DialogResult = System.Windows.Forms.DialogResult.OK
            End Select
        Else
            MsgBox("Keine Änderungen durchgeführt", MsgBoxStyle.Information)
            DialogResult = System.Windows.Forms.DialogResult.No
        End If

        'Fenster wieder schliessen
        Close()
    End Sub

    ''' <summary>
    ''' Ersetzt in allen Rezepten die angegebene alte gegen die neue Rohstoff-Nummer.
    ''' Gibt die Anzahl der ersetzen Rezept-Zeilen zurück.
    ''' </summary>
    ''' <param name="NrAlt"></param>
    ''' <param name="NrNeu"></param>
    Private Function TauscheRohstoffeImRezept(NrAlt As Integer, NrNeu As Integer, winback As wb_Sql, Optional RezeptHistorieSchreiben As Boolean = False) As Integer
        'sql-Kommando Rohstoffe im Rezept tauschen
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptRohst, NrAlt.ToString, NrNeu.ToString)
        'Rohstoffe im Rezept tauschen
        TauscheRohstoffeImRezept = winback.sqlCommand(sql)

        'Änderungs-Index und Rezept-Historie schreiben
        If RezeptHistorieSchreiben Then
            'Liste aller Rezepte mit dem Rohstoff mit der Nummer (NrAlt)
            Dim RzNr As Integer = wb_Global.UNDEFINED
            Dim RzListe As New ArrayList
            RzListe.Clear()

            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompInRZSchritte, NrNeu.ToString)
            winback.sqlSelect(sql)
            While winback.Read
                If RzNr <> winback.iField("RS_RZ_Nr") Then
                    RzNr = winback.iField("RS_RZ_Nr")
                    RzListe.Add(RzNr)
                End If
            End While
            winback.CloseRead()

            'Liste abarbeiten - Alle Rezepte aufrufen-einlesen und wieder speichern (Änderngsdatum und Rezepthistorie schreiben)
            For Each RzNr In RzListe
                'Rezeptkopf einlesen
                Dim Rzpt As New wb_Rezept(RzNr)
                'alle Rezeptschritte aus der Datenbank einlesen
                Rzpt.MySQLdbSelect_RzSchritt(RzNr, Rzpt.Variante)
                'Rezept wieder speichern (aktuelles Datum und HisRezepte schreiben)
                Rzpt.MySQLdbWrite_Rezept(True)
            Next
        End If
    End Function

End Class