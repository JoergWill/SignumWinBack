Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_User_Shared
Imports PCSC
Imports PCSC.Iso7816
Imports PCSC.Monitoring

Public Class wb_User_Details
    Inherits DockContent

    Private Shared ReadOnly _contextFactory As IContextFactory = ContextFactory.Instance
    Private _hContext As ISCardContext
    Private _UserGrpHasChanged As Boolean = False
    Private _Monitor As ISCardMonitor
    Private _ReaderName As String

    Const ReadingMode = 3   'Lese 7 Bytes aus den Klebe-Tags

    Private Sub wb_User_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Combo-Box mit Werten füllen
        GrpLoad(sender)
        'Liste der Card-Reader
        If LoadReaderList() Then
            'Kartenleser aktivieren
            StartReader()
        End If

        'Anzeige der Benutzer-Details
        DetailInfo(sender)

        'In OrgaBack können die Benutzer-Namen nicht geändert werden
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            tUserName.ReadOnly = True
        Else
            tPersonalNr.Visible = False
            lblPersonalNummer.Visible = False
        End If

        AddHandler eListe_Click, AddressOf DetailInfo
        AddHandler eData_Reload, AddressOf GrpLoad
    End Sub

    Private Sub GrpLoad(sender As Object)
        'Combo-Box mit Werten füllen
        cbUserGrp.Fill(GrpTexte)
    End Sub

    Private Sub DataHasChanged(sender As Object, e As EventArgs) Handles tUserName.Leave, tUserPass.Leave
        If wb_Functions.StrToInt(User.Passwort) > 0 Then
            User.Name = tUserName.Text
            User.PersonalNr = tPersonalNr.Text
            User.Passwort = tUserPass.Text
            User.RFID = tUserRFID.Text
            User.iGruppe = cbUserGrp.GetKeyFromSelection()

            'DEBUG
            'Debug.Print("UserDetails.DataHasChanged")
            'Debug.Print(User.Name)
            'Debug.Print(User.PersonalNr)
            'Debug.Print(User.Passwort)
            'Debug.Print(User.RFID)
            'Debug.Print(User.iGruppe)

            'Daten schreiben 
            Edit_Leave(sender)
            'Liste aktualisieren
            Reload(sender)
        End If
    End Sub

    Public Sub DetailInfo(sender As Object)
        'User Name
        tUserName.Text = User.Name
        'User Personalnummer
        tPersonalNr.Text = User.PersonalNr
        'User Passwort
        tUserPass.Text = User.Passwort
        'User RFID
        tUserRFID.Text = User.RFID
        'Eintrag in Combo-Box ausfüllen
        cbUserGrp.SetTextFromKey(User.iGruppe)
    End Sub

    Private Sub wb_User_Details_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_User_Shared.eListe_Click, AddressOf DetailInfo
        RemoveHandler wb_User_Shared.eData_Reload, AddressOf GrpLoad
    End Sub

    Private Sub cbUserGrp_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbUserGrp.SelectionChangeCommitted, cbxReaderList.SelectionChangeCommitted
        _UserGrpHasChanged = True
        lblName.Focus()
    End Sub

    Private Sub cbUserGrp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbUserGrp.SelectedIndexChanged, cbxReaderList.SelectedIndexChanged
        If _UserGrpHasChanged Then
            _UserGrpHasChanged = False
            DataHasChanged(sender, e)
        End If
        'Focus auf ein beliebiges Label-Element - Schönheits-OP für DropDown-Element
        lblName.Select()
    End Sub

    Private Function LoadReaderList()
        Dim readerList As String()
        Try
            cbxReaderList.DataSource = Nothing
            _hContext = _contextFactory.Establish(SCardScope.System)
            readerList = _hContext.GetReaders()
            _hContext.Release()
            If readerList.Length > 0 Then
                cbxReaderList.DataSource = readerList
                Return True
            Else
                cbxReaderList.Text = "Kein Kartenleser gefunden"
                cbxReaderList.Enabled = False
                Return False
            End If
        Catch ex As Exceptions.PCSCException
            'MsgBox("Fehler beim Suchen des Kartenlesers " & ex.Message & " (" & ex.SCardError.ToString() & ")")
            cbxReaderList.Text = "Kein Kartenleser gefunden"
            cbxReaderList.Enabled = False
            Return False
        End Try
    End Function

    Private Sub StartReader()
        Dim MonitorFactory As MonitorFactory = MonitorFactory.Instance
        _Monitor = MonitorFactory.Create(SCardScope.System)

        'Event Card-Inserted
        AddHandler _Monitor.CardInserted, AddressOf CardInit
        'Card-Reader starten
        _ReaderName = cbxReaderList.Text
        _Monitor.Start(_ReaderName)

        'readerName = cbxReaderList.Text
        'readingMode = txtReadingMode.Text
    End Sub

    ''' <summary>
    ''' Event Card-Inserted. Tritt auf, wenn die Karte auf das Lese-Gerät gelegt wird.
    ''' Ruft die Lese-Routine der SC_Karte auf und liest die Serien-Nummer ein
    ''' 
    ''' Der RFID-Code wird threadsicher in die Textbox geschrieben
    ''' https://riptutorial.com/de/vb-net/example/6235/thread-sichere-aufrufe-mit-control-invoke----durchfuhren
    ''' </summary>
    ''' <param name="eventName"></param>
    ''' <param name="e"></param>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification:="<Ausstehend>")>
    <CodeAnalysis.SuppressMessage("Major Bug", "S2583:Conditionally executed code should be reachable", Justification:="<Ausstehend>")>
    Sub CardInit(eventName As SCardMonitor, e As CardStatusEventArgs)
        Dim RfID As String = ""

        Select Case ReadingMode
            Case 1, 2
                RfID = SendUID4Byte()
            Case 3, 4
                RfID = SendUID7Byte()
            Case Else
                Throw New Exception("Falscher Reading Mode - CardReader")
        End Select

        'RFID-Code threadsicher in Textbox eintragen
        If tUserRFID.InvokeRequired Then
            tUserRFID.Invoke(Sub() tUserRFIDText(RfID))
        Else
            tUserRFIDText(RfID)
        End If

    End Sub

    ''' <summary>
    ''' Text threadsicher in Textbox eintragen.
    ''' Vorher wird noch geprüft, ob diese ID schon verwendet wird.
    ''' </summary>
    ''' <param name="Text"></param>
    Private Sub tUserRFIDText(Text As String)
        If wb_User_Shared.User.Exist("*", Text) Then
            MsgBox("Die ID-Nummer existiert schon!" & vbCrLf & "Dieser Chip wurde schon einem anderen Mitarbeiter zugewiesen", MsgBoxStyle.Critical, "Fehler Chip-ID")
        Else
            tUserRFID.Text = Text
            DataHasChanged(Nothing, Nothing)
        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Major Bug", "S2583:Conditionally executed code should be reachable", Justification:="<Ausstehend>")>
    Private Function SendUID4Byte() As String
        Try
            Using context = _contextFactory.Establish(SCardScope.System)
                Using rfidReader = context.ConnectReader(_ReaderName, SCardShareMode.Shared, SCardProtocol.Any)
                    Using rfidReader.Transaction(SCardReaderDisposition.Leave)

                        Dim apdu As Byte() = {&HFF, &HCA, &H0, &H0, &H4}
                        Dim sendPci = SCardPCI.GetPci(rfidReader.Protocol)
                        Dim receivePci = New SCardPCI()

                        Dim receiveBuffer = New Byte(255) {}
                        Dim command = apdu.ToArray()
                        Dim bytesReceived = rfidReader.Transmit(sendPci, command, command.Length, receivePci, receiveBuffer, receiveBuffer.Length)
                        Dim responseApdu = New ResponseApdu(receiveBuffer, bytesReceived, IsoCase.Case2Short, rfidReader.Protocol)

                        If ReadingMode = 1 Then
                            Dim uid As String = BitConverter.ToString(responseApdu.GetData())
                            uid = uid.Replace("-", "")

                            'Chip-ID eintragen
                            Return uid

                        ElseIf ReadingMode = 2 Then
                            Dim uid As Byte() = New Byte(3) {}
                            Dim revuid As Byte() = New Byte(3) {}
                            Array.Copy(responseApdu.GetData(), uid, 4)
                            Array.Copy(uid, revuid, 4)
                            Array.Reverse(revuid, 0, 4)

                            Dim uid2 As String = BitConverter.ToString(revuid)
                            uid2 = uid2.Replace("-", "")

                            'Chip-ID eintragen
                            Return uid2
                        Else
                            'never used
                            Return False
                        End If
                    End Using
                End Using
            End Using
        Catch
            'Error Handling should be developed
        End Try

        Return True
    End Function

    <CodeAnalysis.SuppressMessage("Major Bug", "S2583:Conditionally executed code should be reachable", Justification:="<Ausstehend>")>
    Private Function SendUID7Byte() As String
        Try
            Using context = _contextFactory.Establish(SCardScope.System)
                Using rfidReader = context.ConnectReader(_ReaderName, SCardShareMode.Shared, SCardProtocol.Any)
                    Using rfidReader.Transaction(SCardReaderDisposition.Leave)

                        Dim apdu As Byte() = {&HFF, &HCA, &H0, &H0, &H7}
                        Dim sendPci = SCardPCI.GetPci(rfidReader.Protocol)
                        Dim receivePci = New SCardPCI()

                        Dim receiveBuffer = New Byte(255) {}
                        Dim command = apdu.ToArray()
                        Dim bytesReceived = rfidReader.Transmit(sendPci, command, command.Length, receivePci, receiveBuffer, receiveBuffer.Length)
                        Dim responseApdu = New ResponseApdu(receiveBuffer, bytesReceived, IsoCase.Case2Short, rfidReader.Protocol)

                        If ReadingMode = 3 Then
                            Dim uid As String = BitConverter.ToString(responseApdu.GetData())
                            uid = uid.Replace("-", "")

                            'Chip-ID eintragen
                            Return uid

                        ElseIf ReadingMode = 4 Then
                            Dim uid As Byte() = New Byte(6) {}
                            Dim revuid As Byte() = New Byte(6) {}
                            Array.Copy(responseApdu.GetData(), uid, 7)
                            Array.Copy(uid, revuid, 7)
                            Array.Reverse(revuid, 0, 7)

                            Dim uid2 As String = BitConverter.ToString(revuid)
                            uid2 = uid2.Replace("-", "")

                            'Chip-ID eintragen
                            Return uid2
                        End If
                    End Using
                End Using
            End Using
        Catch
            'Error Handling should be developed
        End Try

        Return True
    End Function

    Private Sub Btn_RemoveID_Click(sender As Object, e As EventArgs) Handles Btn_RemoveID.Click
        tUserRFIDText(wb_Global.UNDEFINED.ToString)
    End Sub
End Class