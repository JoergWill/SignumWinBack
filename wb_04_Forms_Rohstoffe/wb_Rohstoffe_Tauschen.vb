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
        If RohstoffAuswahl.ShowDialog() = Windows.Forms.DialogResult.OK Then
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

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'Gibt die Anzahl der geänderten Rezept-Zeilen zurück
            Dim AnzahlRezeptZeilen As Integer = 0

            'Tauschen oder Ersetzen
            If cbTauschen.Checked Then
                'nächste freie Rohstoff-Nummer suchen
                Dim DummyRohNr As Integer = wb_sql_Functions.getNewKomponNummer()

                'Rohstoff A zunächst durch Dummy ersetzen
                Dim AnzRez_A As Integer = TauscheRohstoffeImRezept(NrAkt, DummyRohNr, winback)
                'Rohstoff B wird ersetzt durch Rohstoff A
                AnzahlRezeptZeilen = TauscheRohstoffeImRezept(_NrNeu, NrAkt, winback)
                'Dummy wird Rohstoff B
                Dim AnzRez_B As Integer = TauscheRohstoffeImRezept(DummyRohNr, _NrNeu, winback)

                'alle zwei Datenbank-Operationen müssen die gleiche Anzahl an Datensätzen zurückliefern
                If AnzRez_A <> AnzRez_B Then
                    'Fehler
                    AnzahlRezeptZeilen = -1
                Else
                    'Gesamtzahl aller Änderungen
                    AnzahlRezeptZeilen += AnzRez_A
                End If
            Else
                'Rohstoff A wird ersetzt durch Rohstoff B
                AnzahlRezeptZeilen = TauscheRohstoffeImRezept(NrAkt, _NrNeu, winback)
            End If

            'Datenbank-Verbindung wieder schliessen
            winback.Close()

            'Auswertung Ergebnis
            Select Case AnzahlRezeptZeilen
                Case -1
                    MsgBox("Fehler beim Ändern der Rezepturen !", MsgBoxStyle.Exclamation)
                    DialogResult = Windows.Forms.DialogResult.Abort
                Case 0
                    MsgBox("Rohstoff wird in keinen Rezepturen verwendet", MsgBoxStyle.Information)
                    DialogResult = Windows.Forms.DialogResult.No
                Case Else
                    MsgBox("Es wurden " & AnzahlRezeptZeilen.ToString & " Rezept-Zeilen geändert", MsgBoxStyle.Information)
                    DialogResult = Windows.Forms.DialogResult.OK
            End Select
        Else
            MsgBox("Keine Änderungen durchgeführt", MsgBoxStyle.Information)
            DialogResult = Windows.Forms.DialogResult.No
        End If

        'Fenster wieder schliessen
        Close()
    End Sub

    ''' <summary>
    ''' Ersetzt in allen Rezepten die angegebene alte gegen die neue Rohstoff-Nummer.
    ''' Wird als neue Rohstoff-Nummer -1 übergeben (Tausch), wird zuerst geprüft ob diese Nummer schon in Rezepturen exisiert.
    ''' 
    ''' Gibt die Anzahl der ersetzen Rezept-Zeilen zurück.
    ''' </summary>
    ''' <param name="NrAlt"></param>
    ''' <param name="NrNeu"></param>
    Private Function TauscheRohstoffeImRezept(NrAlt As Integer, NrNeu As Integer, winback As wb_Sql) As Integer
        'sql-Kommando Rohstoffe im Rezept tauschen
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptRohst, NrAlt.ToString, NrNeu.ToString)

        'Rohstoffe im Rezept tauschen
        TauscheRohstoffeImRezept = winback.sqlCommand(sql)
    End Function

End Class