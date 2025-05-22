Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_ChargenWasserTemp
    Inherits DockContent

    Public _ChargenZeile As wb_ChargenSchritt
    Private _ChargenNummer As String = ""
    Private _Result As Boolean = False

    Private Sub wb_ChargenWasserTemp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TabControl.HideTabs = True
    End Sub

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property ChargenZeile As wb_ChargenSchritt
        Set(value As wb_ChargenSchritt)
            _ChargenZeile = value
            'Fenster Log-File löschen
            tbLogFile.Text = ""
            'Berechnung starten
            _Result = GetTTSBerechnung()
            'Fenster Log-File hat keine Selektion
            tbLogFile.SelectionStart = 0
            tbLogFile.SelectionLength = 0
        End Set
    End Property

    Public ReadOnly Property ChargenStartZeit As String
        Get
            '_ChargenZeile.StartZeit = #09-07-2019 04:36:46#
            Return _ChargenZeile.StartZeit.ToString("yyyy-MM-dd HH:mm:ss")
        End Get
    End Property

    Public ReadOnly Property ChargenStartZeit(PlusSekunden) As String
        Get
            '_ChargenZeile.StartZeit = #09-07-2019 04:36:46#
            Return (_ChargenZeile.StartZeit.AddSeconds(PlusSekunden)).ToString("yyyy-MM-dd HH:mm:ss")
        End Get
    End Property

    Public ReadOnly Property ChargenStartDate As String
        Get
            '_ChargenZeile.StartZeit = #09-07-2019 04:36:46#
            Return _ChargenZeile.StartZeit.ToString("yyyy-MM-dd")
        End Get
    End Property

    Public ReadOnly Property ChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
    End Property

    Public ReadOnly Property Result As Boolean
        Get
            Return _Result
        End Get
    End Property

    ''' <summary>
    ''' Liest alle TTS-Daten aus /home/herbst/log/0s1-s.dbg
    ''' 
    ''' Zuerst wird anhand des Datums versucht die (richtige) Chargen-Nummer zu ermitteln.
    ''' (Die Chargen-Nummer kann sich nach dem Tageswechsel geändert haben, dann stimmen wb_daten und log-File nicht mehr überein)
    ''' 
    ''' Anhand der Chargen-Nummer werden dann alle Einträge mit dem Muster TTS:XXXX aus dem log-File ermittelt.
    ''' </summary>
    Private Function GetTTSBerechnung()
        'Maus-Zeiger anpassen
        Cursor = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Application.DoEvents()

        'alle Fehlermeldungen (zunächst) ausblenden
        ALG_Error.Visible = False
        ALG_Error_tts.Visible = False
        ALG_error_rmf.Visible = False
        'alle ausgeblendeten Labels sichtbar machen
        lbl_e_soll_neu.Visible = True
        ALG_e_soll_neu.Visible = True

        'Erster Versuch die Chargen-Nummer der TTS-Zeile im 0s1-s.dbg-File zu ermitteln
        _ChargenNummer = GetTTSChargenNr(ChargenStartZeit)
        Select Case _ChargenNummer
            Case "ERR"
                'Maus-Zeiger anpassen
                Cursor = System.Windows.Forms.Cursors.Default
                Return False
            Case ""
                'Timestamp nicht gefunden - nächster Versuch eine Sekunde später
                _ChargenNummer = GetTTSChargenNr(ChargenStartZeit(1))
                'Maus-Zeiger anpassen
                Cursor = System.Windows.Forms.Cursors.Default
                Return GetTTSLines(_ChargenNummer)
            Case Else
                'Maus-Zeiger anpassen
                Cursor = System.Windows.Forms.Cursors.Default
                Return GetTTSLines(_ChargenNummer)
        End Select
    End Function

    Private Function GetTTSChargenNr(StartZeit As String) As String
        'Kommando-Zeile - Chargen-Nummer ermitteln 
        Dim Cmd As String = wb_Global.TTSLogCmd_0 & Chr(34) & StartZeit & Chr(34) & wb_Global.TTSLogCmd_1
        Dim Result As String = wb_Functions.ExecSSH(wb_Credentials.SSHUser, wb_Credentials.SSHPass, wb_GlobalSettings.MySQLServerIP, Cmd)
        Dim ResultLines() As String = Result.Split(vbLf)
        Dim ChrgNummer As String = ""

        'Ergebnis auswerten
        If ResultLines.Count > 0 Then
            'Fehler bei Connect oder Command
            If ResultLines(0) = "ERR" Then
                ChrgNummer = "ERR"
            Else
                'Chargen-Nummer aus Log-File ermitteln
                Dim i As Integer = InStr(ResultLines(0), "TTS:")
                If i > 0 Then
                    ChrgNummer = Mid(ResultLines(0), i + 4, 4)
                End If
            End If
        End If
        Return ChrgNummer
    End Function

    Private Function GetTTSLines(ChargenNummer As String) As Boolean
        'Kommando-Zeile - TTS-Berechnung abfragen 
        Dim Cmd As String = wb_Global.TTSLogCmd_0 & Chr(34) & ChargenStartDate & Chr(34) & wb_Global.TTSLogCmd_1 & ChargenNummer
        Dim Result = wb_Functions.ExecSSH(wb_Credentials.SSHUser, wb_Credentials.SSHPass, wb_GlobalSettings.MySQLServerIP, Cmd)
        Dim TTSLines() As String = Result.Split(vbLf)

        'Ergebnis auswerten
        If TTSLines.Count > 0 Then
            'Fehler bei Connect oder Command
            If TTSLines(0) = "ERR" Then
                Return False
            Else
                If TTSLines.Count > 1 Then
                    'Log-File zeilenweise durchlaufen
                    For Each s As String In TTSLines
                        'zeilenweise in Ausgabe-Fenster Log-File schreiben
                        tbLogFile.Text &= DecodeTTSLog(s) & vbCrLf
                        'Bei doppelt-belegten Chargen-Nummern wird die Auswertung beendet
                        If EndeTTSLog(s) Then
                            Exit For
                        End If
                    Next
                    'fehlende Werte berechnen
                    CalcTTSLog()
                    Return True
                Else
                    Return False
                End If
            End If
        Else
            Return False
        End If
    End Function

    Private Function EndeTTSLog(s As String) As Boolean
        Static bEnde As Boolean
        If s.Contains("rs_par1/rs_par2/rs_par3") And bEnde Then
            Return True
        End If
        If s.Contains("Neue Korrekturwerte:") Then
            'Chargen-Nummer gefunden - Ende der TTS-Berechnung
            bEnde = True
        Else
            bEnde = False
        End If
        Return False
    End Function

    ''' <summary>
    ''' Ermittelt anhand der Token/SubToken die einzelnen Werte aus der Zeile des WinBack-Log-Files
    ''' </summary>
    ''' <param name="s"></param>
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high", Justification:="<Ausstehend>")>
    Private Function DecodeTTSLog(s As String)
        Dim t As New wb_SplitTTSLog(s)
        'Debug.Print("Token/SubToken " & t.Token & "/" & t.SubToken)
        Select Case t.Token

            'TTS-Parameter/Werte - Parameter aus Rezeptur    
            Case "rs_par1"
                If t.Values("/").Count = 3 Then
                    TTS_rs_par1.Text = t.Values("/")(0)
                    TTS_rs_par2.Text = t.Values("/")(1)
                    TTS_rs_par3.Text = t.Values("/")(2)
                End If
            'TTS-Parameter/Werte - Faktor aus Konfiguration (Wasser-Hand-Bild)
            Case "tts_p1"
                If t.Values("/").Count = 3 Then
                    TTS_tts_p1.Text = t.Values("/")(0)
                    TTS_tts_p2.Text = t.Values("/")(1)
                    TTS_tts_p3.Text = t.Values("/")(2)
                End If
            'TTS-Parameter/Werte - Berechnete Korrekturwerte
            Case "korr_p1"
                If t.Values("/").Count = 3 Then
                    TTS_korr_p1.Text = t.Values("/")(0)
                    TTS_korr_p2.Text = t.Values("/")(1)
                    TTS_korr_p3.Text = t.Values("/")(2)
                End If

            'RMF-Parameter - Raum
            Case "F_RT"
                If t.Values(":").Count = 1 Then
                    RMF_f_rt.Text = t.Values(":")(0)
                End If
            'RMF-Parameter - Mehl   
            Case "F_MT"
                If t.Values("/").Count = 3 Then
                    RMF_f_mt.Text = t.Values("/")(0)
                    RMF_at_m.Text = t.Values("/")(1)
                    RMF_fk_m.Text = t.Values("/")(2)
                End If
            'RMF-Parameter - Sauerteig
            Case "F_ST"
                If t.Values("/").Count = 3 Then
                    STF_f_st.Text = t.Values("/")(0)
                    STF_at_s.Text = t.Values("/")(1)
                    STF_fk_s.Text = t.Values("/")(2)
                End If
            'RMF-Parameter - Raum
            Case "RT_0"
                If t.Values("/").Count = 4 Then
                    RMF_rt_0.Text = t.Values("/")(0)
                    RMF_rt.Text = t.Values("/")(1)
                    RMF_rt_diff.Text = t.Values("/")(2)
                    RMF_rt_delta.Text = t.Values("/")(3)
                End If
            'RMF-Parameter - Mehl   
            Case "MT_0"
                If t.Values("/").Count = 4 Then
                    RMF_mt_0.Text = t.Values("/")(0)
                    RMF_mt.Text = t.Values("/")(1)
                    RMF_mt_diff.Text = t.Values("/")(2)
                    RMF_mt_delta.Text = t.Values("/")(3)
                End If
            'RMF-Parameter - Sauerteig
            Case "ST_0"
                If t.Values("/").Count = 4 Then
                    STF_st_0.Text = t.Values("/")(0)
                    STF_st.Text = t.Values("/")(1)
                    STF_st_diff.Text = t.Values("/")(2)
                    STF_st_delta.Text = t.Values("/")(3)
                    RMF_st_delta.Text = STF_st_delta.Text
                End If
            'RMF-Parameter - Berechnung
            Case "rmf_basis_wert"
                If t.Values("=").Count = 1 Then
                    RMF_basis.Text = t.Values(":")(0)
                End If

            'RMF Basis-Werte (falls die RMF-Berechnung nicht komplett durchgeführt wird)
            Case "RMF"
                Debug.Print(t.Token & "" & t.SubToken)
                If t.Values(" ").Count > 2 Then
                    Select Case t.Values(" ")(2)
                        Case "rmf_basis_wert"
                            RMF_basis.Text = t.Values(" ")(4)
                    End Select
                End If
            Case "delta_temp_rmf"
                Debug.Print(t.Token & "" & t.SubToken)
                If t.Values("=").Count = 1 Then
                    RMF_delta_temp.Text = t.Values("=")(0)
                End If


            'Berechnung Wasser-Temperatur
            Case "pKT103"
                Select Case t.SubToken
                    Case "temp_delta_tts"
                        If t.Values("=").Count = 1 Then
                            TTS_delta_tts.Text = t.Values("=")(0)
                            ALG_delta_tts.Text = t.Values("=")(0)
                        End If
                    Case "temp_delta_rmf"
                        If t.Values("=").Count = 1 Then
                            RMF_delta_temp.Text = t.Values("=")(0)
                        End If
                    Case "soll_temp_rezept"
                        If t.Values("=").Count = 1 Then
                            ALG_t_rezept.Text = t.Values("=")(0)
                        End If
                    Case "soll_temperatur"
                        If t.Values("=").Count = 2 Then
                            ALG_t_neu_vor_eis.Text = t.Values("=")(1)
                        End If
                End Select

            'Ergebnis Berechnung Wasser (vor exit_grund)
            Case "t_delta (vor Eis)"
                If t.Values("=").Count = 2 Then
                    ALG_t_delta.Text = t.Values("=")(1)
                End If

            Case "exit_grund"
                ErrorLabel(ALG_Error, t.SubToken)
                lbl_e_soll_neu.Visible = False
                ALG_e_soll_neu.Visible = False

            Case "Wassermenge"
                ErrorLabel(ALG_Error, t.SubToken)

            Case "Wassertemp"
                ErrorLabel(ALG_Error, t.SubToken)

            'Berechnung Eis-Menge
            Case "KT104ob"
                Select Case t.SubToken
                    Case "t_delta_in"
                        If t.Values("=").Count = 1 Then
                            ALG_t_delta.Text = t.Values("=")(0)
                        End If
                    Case "t_w_soll_neu"
                        If t.Values("=").Count = 1 Then
                            EIS_t_w_soll_neu.Text = t.Values("=")(0)
                        End If
                    Case "m_w_soll_neu"
                        If t.Values("=").Count = 1 Then
                            EIS_m_w_soll_neu.Text = t.Values("=")(0)
                        End If
                    Case "m_eis_soll_neu"
                        If t.Values("=").Count = 1 Then
                            EIS_m_eis_soll_neu.Text = t.Values("=")(0)
                        End If
                End Select

            'Berechnung ohne Eis-Menge
            Case "KT104ob_ohne"
                Select Case t.SubToken
                    Case "m_w_ars"
                        If t.Values("=").Count = 1 Then
                            EIS_m_w_soll_neu.Text = t.Values("=")(0)
                        End If
                    Case "t_w_ars"
                        If t.Values("=").Count = 1 Then
                            EIS_t_w_soll_neu.Text = t.Values("=")(0)
                            ALG_t_neu_vor_eis.Text = EIS_t_w_soll_neu.Text
                        End If
                End Select

            'Ergebnis Berechnung Solltemperatur neu
            Case "t_soll_neu"
                If t.Values("=").Count = 1 Then
                    EIS_t_w_soll_neu.Text = t.Values("=")(0)
                    ALG_t_neu_vor_eis.Text = EIS_t_w_soll_neu.Text
                End If


        End Select

        'formatierten String für Log-File-Ausgabe zurückgeben
        Return t.LogString
    End Function

    ''' <summary>
    ''' Berechnet die fehlenden Werte, die im log nicht gefunden werden konnten oder von WinBack nicht berechnet worden sind.
    ''' Anhand von Meldungen im log werden auch entsprechende Fehler/Warnhinweise ausgegeben.
    ''' </summary>
    Private Sub CalcTTSLog()
        'Delta-RMF wird nicht ausgeben und muss berechnet werden !
        'Dim dRMF_delta_temp As Double = wb_Functions.StrToDouble(RMF_delta_temp.Text)
        'Dim dRMF_basis As Double = wb_Functions.StrToDouble(RMF_basis.Text)
        'RMF_delta_rmf.Text = wb_Functions.FormatStr((dRMF_delta_temp - dRMF_basis).ToString, 1)
        RMF_delta_rmf.Text = CalcLabel(RMF_delta_temp, RMF_basis, "SUB")
        ALG_delta_rmf.Text = RMF_delta_rmf.Text

        'Fehler TTS_AUS_FLAG
        If ALG_delta_tts.Text.Contains("TTS_AUS_FLAG") Then
            ALG_delta_tts.Text = "0,0"
            TTS_delta_tts.Text = "0,0"
            ErrorLabel(ALG_Error_tts, "TTS ist aus/deaktiviert")
        End If

        'prüfen ob ALG_t_delta berechnet werden muss
        If ALG_t_delta.Text = "t_delta" Then
            '            ALG_t_delta.Text = wb_Functions.FormatStr(Val(ALG_delta_tts.Text) + Val(ALG_delta_rmf.Text), 1)
            ALG_t_delta.Text = CalcLabel(ALG_delta_tts, ALG_delta_rmf, "ADD")
        End If

        'Zusammenfassung Wasser/Eis-Berechnung
        ALG_w_soll_neu.Text = EIS_m_w_soll_neu.Text & " L / " & EIS_t_w_soll_neu.Text & " °C"
        ALG_e_soll_neu.Text = EIS_m_eis_soll_neu.Text & " kg"

    End Sub

    Private Function CalcLabel(lbl1 As System.Windows.Forms.Label, lbl2 As System.Windows.Forms.Label, calc As String) As String
        Dim Result As String = "0,0"
        Try
            'Label in Double wandeln
            Dim dlbl1 As Double = wb_Functions.StrToDouble(lbl1.Text)
            Dim dlbl2 As Double = wb_Functions.StrToDouble(lbl2.Text)

            'Rechenoperation durchführen
            Select Case calc.ToUpper
                Case "ADD"
                    Result = wb_Functions.FormatStr((dlbl1 + dlbl2).ToString, 1)
                Case "SUB"
                    Result = wb_Functions.FormatStr((dlbl1 - dlbl2).ToString, 1)
            End Select
        Catch ex As Exception
            Result = "0,0"
        End Try

        Return Result
    End Function

    Private Sub ErrorLabel(lbl As System.Windows.Forms.Label, Text As String)
        lbl.Text = Text
        lbl.Visible = True
    End Sub

    Private Sub BtnTTS_Click(sender As Object, e As EventArgs) Handles BtnTTS.Click
        TabControl.SelectTab(tp_TTS)
    End Sub

    Private Sub BtnRMF_Click(sender As Object, e As EventArgs) Handles BtnRMF.Click
        TabControl.SelectTab(tp_RMF)
    End Sub

    Private Sub Btn_MSG_Click(sender As Object, e As EventArgs) Handles Btn_MSG.Click
        TabControl.SelectTab(tp_MSG)
    End Sub

    Private Sub Btn_STF_Click(sender As Object, e As EventArgs) Handles Btn_STF.Click
        TabControl.SelectTab(tp_STF)
    End Sub

    Private Sub Btn_RMF_Back_Click(sender As Object, e As EventArgs) Handles Btn_RMF_Back.Click
        TabControl.SelectTab(tp_ALG)
    End Sub

    Private Sub Btn_STF_Back_Click(sender As Object, e As EventArgs) Handles Btn_STF_Back.Click
        TabControl.SelectTab(tp_RMF)
    End Sub

    Private Sub Btn_TTS_Back_Click(sender As Object, e As EventArgs) Handles Btn_TTS_Back.Click
        TabControl.SelectTab(tp_ALG)
    End Sub

    Private Sub Btn_MSG_Back_Click(sender As Object, e As EventArgs) Handles Btn_MSG_Back.Click
        TabControl.SelectTab(tp_ALG)
    End Sub

    ''' <summary>
    ''' Fenster wieder schliessen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_ALG_Back_Click(sender As Object, e As EventArgs) Handles Btn_ALG_Back.Click
        Close()
    End Sub
End Class