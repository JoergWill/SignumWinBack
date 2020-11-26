Public Class wb_Admin_TimerEdit
    Const SecondsPerHour = 3600
    Const SecondsPerDay = 86400
    Const SecondsPerYear = 31536000

    Private _Index As Integer
    Private _TimerName As String
    Public _TimerAktIndex As String = ""
    Public Event RunTimer(sender As Object, Index As Integer)

    Enum TimerEinheit
        MinSek = 0
        Stunden = 1
        Tage = 2
        Jahre = 3
    End Enum

    ''' <summary>
    ''' Zeiger auf den Datensatz im Grid
    ''' </summary>
    ''' <returns></returns>
    Public Property Index As Integer
        Get
            Return _Index
        End Get
        Set(value As Integer)
            _Index = value
        End Set
    End Property

    ''' <summary>
    ''' Task-Bezeichung aus Datenbank
    ''' </summary>
    Public WriteOnly Property TimerName As String
        Set(value As String)
            _TimerName = value
            'Timer-Kommentar-Text
            Select Case value
                Case "office_nwt"
                    lblTimerName.Text = "Aktualisierung Rohstoffe"
                    lblTimerHinweis.Text = "Aktualisiert zyklisch alle Nährwert-Informationen der Rohstoffe aus der Cloud"
                    cbAktIndex.Text = "Update ALLE Rohstoffe in OrgaBack"
                    lblIndex.Text = "Index (WinBack)Komponenten-Nummer"

                Case "office_artikel"
                    lblTimerName.Text = "Aktualisierung Artikel"
                    lblTimerHinweis.Text = "Berechnet die Nährwerte und Allergene der Artikel, deren Rezepturen geänderte Rohstoffe(Update) enthalten"
                    cbAktIndex.Text = "Update ALLE Artikel in OrgaBack"
                    lblIndex.Text = "Index (WinBack)Artikel-Nummer"

                Case "office_chargen"
                    lblTimerName.Text = "Produktion und Verbrauch"
                    lblTimerHinweis.Text = "Zyklisches Update aller produzierten Artikel und der dafür verbrauchten Rohstoffe"
                    cbAktIndex.Text = ""
                    lblIndex.Text = "Index (WinBack)Tageswechsel-Nummer"

                Case Else
                    lblTimerName.Text = value
                    cbAktIndex.Text = ""
                    lblIndex.Text = ""
            End Select
        End Set
    End Property

    Public WriteOnly Property TimerBezeichnung As String
        Set(value As String)
            'Fenster-Titel
            Me.Text = "Timer " & value
        End Set
    End Property

    ''' <summary>
    ''' Task Start
    ''' </summary>
    ''' <returns></returns>
    Public Property TimerStart As DateTime
        Get
            Dim seconds As Integer = dtEventTime.Value.Hour * SecondsPerHour + dtEventTime.Value.Minute * 60 + dtEventTime.Value.Second
            Return dtEventDate.Value.AddSeconds(seconds)
        End Get
        Set(value As DateTime)
            Try
                'Task-Start Datum
                dtEventDate.Value = value.Date
                'Task-Start Uhrzeit
                dtEventTime.Value = value
            Catch
                'Task-Start Datum
                dtEventDate.Value = Now.Date
                'Task-Start Uhrzeit
                dtEventTime.Value = Now
            End Try
        End Set
    End Property

    Public Property TimerZyklus As Integer
        Get
            Select Case cbEventZyklus.SelectedIndex
                Case TimerEinheit.MinSek
                    Return dtEventZyklus.Value.Minute * 60 + dtEventZyklus.Value.Second
                Case TimerEinheit.Stunden
                    Return nmEventZyklus.Value * SecondsPerHour
                Case TimerEinheit.Tage
                    Return nmEventZyklus.Value * SecondsPerDay
                Case TimerEinheit.Jahre
                    Return nmEventZyklus.Value * SecondsPerYear
                Case Else
                    Return wb_Global.UNDEFINED

            End Select
        End Get
        Set(value As Integer)

            Select Case value

                Case < SecondsPerHour
                    'Anzeige in Minuten und Sekunden
                    cbEventZyklus.SelectedIndex = TimerEinheit.MinSek
                    dtEventZyklus.Value = Convert.ToDateTime("01.01.2000")
                    dtEventZyklus.Value = dtEventZyklus.Value.AddSeconds(value)

                Case < SecondsPerDay
                    'Anzeige in Stunden
                    cbEventZyklus.SelectedIndex = TimerEinheit.Stunden
                    nmEventZyklus.Value = Int(value / SecondsPerHour)

                Case < 864000           '(10 Tage)
                    'Anzeige in Tagen
                    cbEventZyklus.SelectedIndex = TimerEinheit.Tage
                    nmEventZyklus.Value = Int(value / SecondsPerDay)

                Case Else
                    'Anzeige in Jahren
                    cbEventZyklus.SelectedIndex = TimerEinheit.Jahre
                    nmEventZyklus.Value = Int(value / 31536000)

            End Select

            'abhängig von Eingabewert und Einheit wird das passende Eingabefeld eingeblendet
            SetEingabeFeld()

        End Set

    End Property

    Public Property TimerAktiv As Boolean
        Get
            Return cbEventAktiv.Checked
        End Get
        Set(value As Boolean)
            cbEventAktiv.Checked = value
        End Set
    End Property

    Public Property TimerAktIndex As String
        Get
            Return nmAktIndex.Value
        End Get
        Set(value As String)
            nmAktIndex.Value = wb_Functions.StrToInt(value)
            If value = wb_Global.obUpdateAll.ToString Then
                cbAktIndex.Checked = True
            End If
        End Set
    End Property

    Private Sub SetEingabeFeld()
        'Eingabe-Feld Eingabe Minuten/Sekunden ein/ausblenden
        If cbEventZyklus.SelectedIndex = TimerEinheit.MinSek Then
            dtEventZyklus.Visible = True
        Else
            dtEventZyklus.Visible = False
        End If

        'Eingabe-Feld Numerische Eingabe ein/ausblenden
        nmEventZyklus.Visible = Not dtEventZyklus.Visible

        'Eingabefelder sperren
        cbEventZyklus.Enabled = cbEventAktiv.Checked
        nmEventZyklus.Enabled = cbEventAktiv.Checked
        dtEventZyklus.Enabled = cbEventAktiv.Checked
        dtEventDate.Enabled = cbEventAktiv.Checked
        dtEventTime.Enabled = cbEventAktiv.Checked

        'Anzeige Checkbox Sonderfunktionen
        If cbAktIndex.Text = "" Then
            cbAktIndex.Visible = False
        End If
        'Anzeige Index Sonderfunktionen
        If (lblIndex.Text = "") Or (TimerAktIndex = wb_Global.obUpdateAll.ToString) Then
            lblIndex.Visible = False
            nmAktIndex.Visible = False
        Else
            lblIndex.Visible = True
            nmAktIndex.Visible = True
        End If
    End Sub

    Private Sub cbEventZyklus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbEventZyklus.SelectedIndexChanged
        SetEingabeFeld()
        BtnClose.Focus()
    End Sub

    Private Sub wb_TimerEdit_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        nmEventZyklus.Top = dtEventZyklus.Top
        SetEingabeFeld()
    End Sub

    Private Sub cbEventAktiv_CheckedChanged(sender As Object, e As EventArgs) Handles cbEventAktiv.CheckedChanged
        SetEingabeFeld()
    End Sub

    Private Sub cbAktIndex_CheckedChanged(sender As Object, e As EventArgs) Handles cbAktIndex.CheckedChanged
        If cbAktIndex.Checked Then
            'alten Wert merken
            _TimerAktIndex = TimerAktIndex
            TimerAktIndex = wb_Global.obUpdateAll.ToString
        Else
            'ursprünglichen Wert wieder eintragen
            TimerAktIndex = _TimerAktIndex
        End If
        SetEingabeFeld()
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Close()
    End Sub

    Private Sub BtnRunNow_Click(sender As Object, e As EventArgs) Handles BtnRunNow.Click
        RaiseEvent RunTimer(sender, Index)
    End Sub

End Class