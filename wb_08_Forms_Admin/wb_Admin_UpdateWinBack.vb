Imports System.Windows.Forms

Public Class wb_Admin_UpdateWinBack

#Disable Warning SYSLIB0014
    Private _WinBackUpdateVersion As String = "0.0.0.0"
    Private _WinBackVersion As String = "0.0.0.0"
    Private _SignumUpdateVersion As String = "0.0.0.0"
    Private _SignumVersion As String = "0.0.0.0"
    Private _ErrorMessage As String = ""

    ''' <summary>
    ''' Version der im Internet (www.winback.de/software) bereitsgestellten Version von WinBack-AddIn
    ''' </summary>
    ''' <returns></returns>
    Public Property WinBackUpdateVersion As String
        Get
            Return _WinBackUpdateVersion
        End Get
        Set(value As String)
            _WinBackUpdateVersion = value
            tbWinbackUpdate.Text = "V" & value
        End Set
    End Property

    Public Property WinBackVersion As String
        Get
            Return _WinBackVersion
        End Get
        Set(value As String)
            _WinBackVersion = value
            tbWinBack.Text = "V" & value
        End Set
    End Property

    ''' <summary>
    ''' Notwendige Version von Signum.OrgaSoft.Contracts.dll für ein Update des WinBack-AddIn
    ''' </summary>
    ''' <returns></returns>
    Public Property SignumUpdateVersion As String
        Get
            Return _SignumUpdateVersion
        End Get
        Set(value As String)
            _SignumUpdateVersion = value
            tbOrgaBackUpdate.Text = "V" & value
        End Set
    End Property

    Public Property SignumVersion As String
        Get
            Return _SignumVersion
        End Get
        Set(value As String)
            _SignumVersion = value
            tbOrgaBack.Text = "V" & value
        End Set
    End Property

    Public ReadOnly Property ErrorMessage As String
        Get
            Return _ErrorMessage
        End Get
    End Property

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        'Aktuelle Version
        WinBackVersion = wb_GlobalSettings.WinBackVersion

        'Windows-Version
        tbWindowsVersion.Text = wb_GlobalSettings.GetOSVersion

        'Windows-Version 32/64Bit
        If Environment.Is64BitOperatingSystem Then
            tbOSBit.Text = "64Bit"
        Else
            tbOSBit.Text = "32Bit"
        End If

        'WinBack-AddIn 32/64Bit
        If Environment.Is64BitProcess Then
            tbWinBackBit.Text = "64Bit"
        Else
            tbWinBackBit.Text = "32Bit"
        End If

        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            tbOrgaBack.Visible = True
            lblOrgaBack.Visible = True
            tbOrgaBackBit.Visible = True
            tbOrgaBackUpdate.Visible = True
            SignumVersion = wb_GlobalSettings.OrgaBackVersion
        Else
            tbOrgaBack.Visible = False
            lblOrgaBack.Visible = False
            tbOrgaBackBit.Visible = False
            tbOrgaBackUpdate.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Beim Anzeigen der Form wird automatisch geprüft, ob eine aktuellere Version im Internet verfügbar ist.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Admin_UpdateWinBack_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Update-Button aktivieren wenn Updates verfügbar
        CheckUpdateBtn(True)
    End Sub

    ''' <summary>
    ''' Prüft ob eine neuere Version im Internet zum Download verfügbar ist.
    ''' Die Versions-Nummer wird aus der Datei OrgaBack.txt ermittelt.
    ''' 
    ''' Bei Programm-Variante OrgaBack.
    '''     Ob diese Version installiert werden kann ist abhängig von der Version der Signum.OrgaSoft.Contracts.dll
    ''' </summary>
    ''' <returns></returns>
    Public Function CheckUpdateVersion() As wb_Global.CompareVersionResult
        Return wb_Functions.CompareVersion(wb_GlobalSettings.WinBackVersion, WinBackUpdateVersion)
    End Function

    ''' <summary>
    ''' Prüft ob die (neuere) Version im Internet unter der aktuellen OrgaBack-Version lauffähig ist.
    ''' Voraussetzung ist, dass die Version von Signum.OrgaBack.Contracts kompatibel zur erforderlichen Version
    ''' des Downloads von WinBack-AddIn ist.
    ''' </summary>
    ''' <returns></returns>
    Public Function CheckOrgaBackVersion() As Boolean
        Return wb_Functions.CompareVersion(wb_GlobalSettings.SignumContractsVersion, SignumUpdateVersion) = wb_Global.CompareVersionResult.NoUpdate
    End Function

    ''' <summary>
    ''' Prüft ob eine beliebige Version im Internet zum Download verfügbar ist.
    ''' Die Versions-Nummer wird aus der Datei OrgaBack.txt ermittelt.
    ''' 
    ''' Bei Programm-Variante OrgaBack.
    '''     Ob diese Version installiert werden kann ist abhängig von der Version der Signum.OrgaSoft.Contracts.dll
    '''
    ''' Gibt True zurück, wenn eine gültige Version im Internet verfügbar ist.
    ''' </summary>
    ''' <returns></returns>

    Public Function GetUpdateVersion() As Boolean
        Try
            Dim url As String = wb_Credentials.WinBackUpdateHttp & wb_Credentials.WinBackUpdateVersionFile
            Dim myRequest As System.Net.WebRequest = System.Net.WebRequest.Create(url) 'Request erstellen
            Dim myResponse As System.Net.WebResponse = myRequest.GetResponse() 'Respons speichern
            Dim myStream As System.IO.Stream = myResponse.GetResponseStream() 'Datenstream aus dem Respons extrahieren
            Dim myReader As New System.IO.BinaryReader(myStream) 'Binärer "Leser" zum Lesen des Streams

            'Maximal-Länge begrenzen
            If myResponse.ContentLength < 100 Then
                'Buffer für die empfangenen Daten
                Dim Bytes() As Byte = myReader.ReadBytes(myResponse.ContentLength)
                'in String umwandeln
                Dim RecString = System.Text.Encoding.ASCII.GetString(Bytes)
                'String aufteilen in die einzelnen Zeilen
                Dim Rec() As String = Split(RecString, vbLf)

                'genau 2 Zeilen im Receive-Buffer)
                If Rec.Count = 2 Then
                    'Versions-Nummer WinBack-Office/AddIn/ServerTask
                    'Debug.Print(Rec(0))
                    WinBackUpdateVersion = Mid(Rec(0), 1)

                    'Voraussetzung für Update (Versions-Nummer Signum.OrgaSoft.Contracts)
                    'Debug.Print(Rec(1))
                    SignumUpdateVersion = Mid(Rec(1), 1)
                Else
                    _ErrorMessage = "Fehler beim Download der Versions-Information (Datei ungültig)"
                    Return False
                End If
            Else
                _ErrorMessage = "Fehler beim Download der Versions-Information (Datei zu lang)"
                Return False
            End If

        Catch exc As Exception
            _ErrorMessage = "Fehler beim Download der Versions-Information (nicht verfügbar)"
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Download des WinBack-Setup-Files(WinBackSetup.msi) von www.winback.de in das Update Verzeichnis (..OrgaBack/AddIn/Update)
    ''' Wenn der Download erfolgreich war, wird das Setup mit Parameter /i gestartet.
    ''' 
    ''' Upload der WinBackSetup-msi NUR über FTP-Programm. Ein direkter Upload über die Web-Oberfläche bei IONOS ist in der Größe 
    ''' beschränkt.
    ''' 
    ''' Ein Minor-Update wird über WinBackUpdate.exe durchgeführt. Das Setup-File wird nur bei einem Major-Update benötigt.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExecuteUpdateVersion(UpdateMode As wb_Global.CompareVersionResult) As Boolean
        Try
            Select Case UpdateMode
                Case wb_Global.CompareVersionResult.MinorUpdate

                    'Parameter für WinBackUpdate.exe abhängig von der Proramm-Variante
                    Dim UpdateParameter As String = ""
                    Select Case wb_GlobalSettings.pVariante
                        Case wb_Global.ProgVariante.OrgaBack
                            UpdateParameter = " /U /O /N"
                        Case wb_Global.ProgVariante.WinBack
                            UpdateParameter = " /U /W /N"
                        Case wb_Global.ProgVariante.WBServerTask
                            UpdateParameter = " /U /B /N"
                        Case wb_Global.ProgVariante.AnyWhere
                            UpdateParameter = " /U /A /N"
                    End Select

                    'WinBackUpdate.exe ausführen - Parameter /U /xx /N (anschliessend Neustart OrgaBack/OrgaBack-Office/Background-Task/AnyWhere)
                    Call Shell(wb_GlobalSettings.pWinBackUpdatePath & wb_GlobalSettings.WinBackUpgradeExe & UpdateParameter & Chr(34) & wb_GlobalSettings.MyOwnExeFileName & Chr(34), AppWinStyle.NormalFocus, False)

                Case wb_Global.CompareVersionResult.VersionUpdate
                    'Mauszeiger Wait
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                    Application.DoEvents()

                    'Download winback.de/software/WinBackSetup.msi 
                    'Download File - no credentials - ShowUI - Timeout - Overwrite
                    My.Computer.Network.DownloadFile(wb_Credentials.WinBackUpdateHttp & wb_GlobalSettings.WinBackUpdateSetupExe,
                                             wb_GlobalSettings.pWinBackUpdatePath & wb_GlobalSettings.WinBackUpdateSetupExe, "", "", False, 2000, True)
                    'Mauszeiger Default
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    'WinBackSetup.msi ausführen - Parameter /update
                    Call Shell("msiexec /i " & wb_GlobalSettings.pWinBackUpdatePath & wb_GlobalSettings.WinBackUpdateSetupExe)
            End Select

        Catch ex As Exception
            MsgBox("Fehler beim Download/Update " & vbCrLf & ex.Message)
            Return False
        End Try

        Return True
    End Function

    Public Function CheckUpdate(ShowMsg As Boolean, ShowForm As Boolean) As wb_Global.CompareVersionResult
        'Ergebnis der Update-Prüfung
        Dim Result As wb_Global.CompareVersionResult = wb_Global.CompareVersionResult.NoUpdate

        'Versions-Nummern ermitteln
        If GetUpdateVersion() Then
            'nach der Update-Prüfung ist das Fenster nicht mehr sichtbar
            If ShowForm Then
                Me.Show()
            End If
            'Update-Version prüfen
            Result = CheckUpdateVersion()

            'Anzeige Ergebnis Update-Check
            If ShowMsg Then

                Select Case Result
                    Case wb_Global.CompareVersionResult.MinorUpdate
                        'WinBack ist nicht aktuell
                        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
                            MsgBox("Es ist ein Update von OrgaBack-Office verfügbar", MsgBoxStyle.Information, "Update-Check")
                        ElseIf CheckOrgaBackVersion() Then
                            'OrgaBack muss aktuell oder neuer sein, sonst ist kein WinBack-Update möglich
                            MsgBox("Es ist ein Update des WinBack-AddIn verfügbar", MsgBoxStyle.Information, "Update-Check")
                        End If

                    Case wb_Global.CompareVersionResult.VersionUpdate
                        'WinBack ist nicht aktuell (Major Update)
                        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
                            MsgBox("Es ist eine neue Version von OrgaBack-Office verfügbar", MsgBoxStyle.Information, "Update-Check")
                        ElseIf CheckOrgaBackVersion() Then
                            'OrgaBack muss aktuell oder neuer sein, sonst ist kein WinBack-Update möglich
                            MsgBox("Es ist eine neue Version des WinBack-AddIn verfügbar", MsgBoxStyle.Information, "Update-Check")
                        Else
                            MsgBox("Es ist eine neue Version der WinBack-AddIn verfügbar" & vbCrLf &
                                   "Vor dem Update muss OrgaBack ebenfalls aktualisiert werden! ", MsgBoxStyle.Exclamation, "Update-Check")
                        End If

                End Select
            End If
        Else
            If ShowMsg Then
                MsgBox(ErrorMessage, MsgBoxStyle.Critical, "Fehler bei Update-Prüfung")
            End If
        End If
        Return Result
    End Function

    ''' <summary>
    ''' Auf Update prüfen.
    ''' Download der Versions-Info aus dem Internet
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnCheckUpdate_Click(sender As Object, e As EventArgs) Handles BtnCheckUpdate.Click
        'Update-Button aktivieren wenn Updates verfügbar
        CheckUpdateBtn(True)
    End Sub

    Private Sub CheckUpdateBtn(ShowMsg As Boolean)
        Select Case CheckUpdate(ShowMsg, True)
            Case wb_Global.CompareVersionResult.MinorUpdate
                BtnUpdate.Text = "Upgrade"
                BtnUpdate.Enabled = True
                BtnUpdate.Tag = wb_Global.CompareVersionResult.MinorUpdate

            Case wb_Global.CompareVersionResult.VersionUpdate
                BtnUpdate.Text = "Update"
                BtnUpdate.Enabled = True
                BtnUpdate.Tag = wb_Global.CompareVersionResult.VersionUpdate
            Case Else
                BtnUpdate.Enabled = False
        End Select
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        'Windows-Installer starten (Update)
        If ExecuteUpdateVersion(BtnUpdate.Tag) Then
            'Programm beenden
            wb_Functions.ExitProgram()
        End If
    End Sub

#Enable Warning SYSLIB0014
End Class