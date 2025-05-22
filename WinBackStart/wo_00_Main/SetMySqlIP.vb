Imports System.Net

Public Class SetMySqlIP

    Public ReadOnly Property MySQLServerIP As String
        Get
            Return cbAdressen.Text
        End Get
    End Property

    Private Sub BtnScan_Click(sender As Object, e As EventArgs) Handles BtnScan.Click
        'Buttons deaktivieren
        BtnOK.Enabled = False
        BtnScan.Enabled = False
        BtnCancel.Enabled = False

        'Mauszeiger umschalten
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Info-Test Scan aktiv
        lblInfo.Text = "Scan läuft"
        lblInfo.ForeColor = Color.Red

        'IP-Adresse im Auswahlfenster löschen
        cbAdressen.Text = ""
        cbAdressen.Items.Clear()
        Application.DoEvents()

        'Scan starten
        Dim Scan As String = wb_Sql_FindServerIP.FindWinBackServer
        'Mauszeiger Default
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'Ergebnis des Scans
        Select Case wb_Sql_FindServerIP.ResultScans
            Case 0
                lblInfo.Text = "Kein WinBack-Server gefunden"

            Case 1
                lblInfo.Text = "WinBack-Server gefunden"
                lblInfo.ForeColor = Color.Black
                cbAdressen.Items.Add(Scan)

            Case Else
                lblInfo.Text = "WinBack-Server gefunden. Bitte den aktiven Server auswählen"
                lblInfo.ForeColor = Color.Black
                For Each s In wb_Sql_FindServerIP.Servers
                    cbAdressen.Items.Add(s)
                Next
        End Select

        'Button wieder aktivierem
        BtnScan.Enabled = True
        BtnCancel.Enabled = True
    End Sub

    Private Sub cbAdressen_TextChanged(sender As Object, e As EventArgs) Handles cbAdressen.TextChanged
        Dim IPBytes As Integer = UBound(Split(cbAdressen.Text, "."))
        BtnOK.Enabled = IPAddress.TryParse(cbAdressen.Text, Nothing) AndAlso IPBytes = 3
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        'Prüfen ob die IP-Adresse gültig ist..
        lblInfo.Text = "IP-Adresse wird geprüft"
        lblInfo.ForeColor = Color.Red

        If wb_Sql_FindServerIP.FindWinBackServer(cbAdressen.Text) Then
            'Adresse wurd gefunden und ist gültig
            DialogResult = DialogResult.OK
        Else
            'Adresse ist KEIN WinBack-Server
            DialogResult = DialogResult.None
            lblInfo.Text = "IP-Adresse eintragen oder scannen ..."
            lblInfo.ForeColor = Color.Black
        End If
    End Sub
End Class