Public Class wb_Admin_UpdateWinBack

    Private _WinBackUpdateVersion As String = "0.0.0"
    Private _SignumUpdateVersion As String = "0.0.0"
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

    Public ReadOnly Property ErrorMessage As String
        Get
            Return _ErrorMessage
        End Get
    End Property

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        'Update-Button ausblenden
        'Button2.Enabled = False

        'Aktuelle Version
        tbWinBack.Text = wb_GlobalSettings.WinBackVersion

        'Windows-Version
        tbWindowsVersion.Text = wb_GlobalSettings.GetOSVersion

        'Windows-Version 32/64Bit
        If Environment.Is64BitOperatingSystem Then
            tbOSBit.Text = "32Bit"
        Else
            tbOSBit.Text = "64Bit"
        End If

        'WinBack-AddIn 32/64Bit
        If Environment.Is64BitProcess Then
            tbWinBackBit.Text = "32Bit"
        Else
            tbWinBackBit.Text = "64Bit"
        End If

        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            tbOrgaBack.Visible = True
            lblOrgaBack.Visible = True
            tbOrgaBackBit.Visible = True
            tbOrgaBackUpdate.Visible = True
            tbOrgaBack.Text = wb_GlobalSettings.OrgaBackVersion
        Else
            tbOrgaBack.Visible = False
            lblOrgaBack.Visible = False
            tbOrgaBackBit.Visible = False
            tbOrgaBackUpdate.Visible = False
        End If
    End Sub


    ''' <summary>
    ''' Prüft ob eine neuere Version im Internet zum Download verfügbar ist.
    ''' Die Versions-Nummer wird aus der Datei OrgaBack.txt ermittelt.
    ''' 
    ''' Bei Programm-Variante OrgaBack.
    '''     Ob diese Version installiert werden kann ist abhängig von der Version der Signum.OrgaSoft.Contracts.dll
    ''' </summary>
    ''' <returns></returns>
    Public Function CheckUpdateVersion() As Boolean
        Return wb_Functions.CompareVersion(wb_GlobalSettings.WinBackVersion, WinBackUpdateVersion)
    End Function

    ''' <summary>
    ''' Prüft ob die (neuere) Version im Internet unter der aktuellen OrgaBack-Version lauffähig ist.
    ''' Voraussetzung ist, dass die Version von Signum.OrgaBack.Contracts kompatibel zur erforderlichen Version
    ''' des Downloads von WinBack-AddIn ist.
    ''' </summary>
    ''' <returns></returns>
    Public Function CheckOrgaBackVersion() As Boolean
        Return wb_Functions.CompareVersion(wb_GlobalSettings.SignumContractsVersion, SignumUpdateVersion)
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
            Dim url As String = wb_Global.WinBackUpdateHttp & wb_Global.WinBackUpdateVersionFile
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
                Dim Rec() As String = Split(RecString, vbCrLf)

                'genau 2 Zeilen im Receive-Buffer)
                If Rec.Count = 2 Then
                    'Versions-Nummer WinBack-Office/AddIn/ServerTask
                    'Debug.Print(Rec(0))
                    WinBackUpdateVersion = Mid(Rec(0), 2)

                    'Voraussetzung für Update (Versions-Nummer Signum.OrgaSoft.Contracts)
                    'Debug.Print(Rec(1))
                    SignumUpdateVersion = Mid(Rec(1), 9)
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
    ''' Wenn der Download erfolgreich war, wird das Setup mit Parameter /update gestartet.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExecuteUpdateVersion()
        'Download WinBackSetup.msi 
        Try
            'Download File - no credentials - ShowUI - Timeout - Overwrite
            My.Computer.Network.DownloadFile(wb_Global.WinBackUpdateHttp & wb_GlobalSettings.WinBackUpdateSetupExe,
                                             wb_GlobalSettings.pWinBackUpdatePath & wb_GlobalSettings.WinBackUpdateSetupExe, "", "", True, 2000, True)
        Catch ex As Exception
            MsgBox("Fehler beim Download " & vbCrLf & ex.Message)
            Return False
        End Try

        'WinBackSetup.msi ausführen - Parameter /update
        Dim SetupStartInfo As New ProcessStartInfo
        SetupStartInfo.FileName = wb_GlobalSettings.pWinBackUpdatePath & wb_GlobalSettings.WinBackUpdateSetupExe
        'SetupStartInfo.Arguments = "-update"
        Process.Start(SetupStartInfo)

        Return True
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnCheckUpdate.Click
        'Update-Version prüfen
        If GetUpdateVersion() Then
            'WinBack ist nicht aktuell
            If CheckUpdateVersion() Then
                If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.WinBack Then
                    MsgBox("Es ist eine neue Version von WinBack-Office verfügbar", MsgBoxStyle.Information, "Update-Check")
                    BtnUpdate.Enabled = True
                Else
                    'OrgaBack muss aktuell oder neuer sein, sonst ist kein WinBack-Update möglich
                    If Not CheckOrgaBackVersion() Then
                        MsgBox("Es ist eine neue Version der WinBack-AddIn verfügbar", MsgBoxStyle.Information, "Update-Check")
                        BtnUpdate.Enabled = True
                    Else
                        MsgBox("Es ist eine neue Version der WinBack-AddIn verfügbar" & vbCrLf &
                               "Vor dem Update muss OrgaBack ebenfalls aktualisiert werden! ", MsgBoxStyle.Exclamation, "Update-Check")
                    End If
                End If
            End If
        Else
            MsgBox(ErrorMessage, MsgBoxStyle.Critical, "Fehler bei Update-Prüfung")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        ExecuteUpdateVersion()
    End Sub

End Class