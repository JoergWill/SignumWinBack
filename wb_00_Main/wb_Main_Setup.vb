Imports System.Xml

Public Class wb_Main_Setup
    Private Sub wb_Main_Setup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Pfad zur OrgaSoft.ini
        Dim OrgaSoftIni As String = wb_GlobalSettings.pWinBackIniPath

        'Default-Einträge (wenn keine Mandanten oder nur einer eingetragen sind)
        tbMandanten.TabPages.Clear()
        tbMandanten.TabPages.Add("Default")
        'Mandanten in Tab-Control eintragen
        For Each m As wb_Global.obMandant In wb_GlobalSettings.Mandanten
            tbMandanten.TabPages.Add(m.MandantName)
        Next

        'Default-Eintrag
        tbMandanten.SelectTab(0)
    End Sub

    Private Sub tbMandanten_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbMandanten.SelectedIndexChanged
        Dim i As Integer = tbMandanten.SelectedIndex
        If i > 0 Then
            tbMandant.Text = (i).ToString & "/" & wb_GlobalSettings.Mandanten.Count
            tbMandantName.Text = DirectCast(wb_GlobalSettings.Mandanten(i - 1), wb_Global.obMandant).MandantName
            tbOrgaBackAdmin.Text = DirectCast(wb_GlobalSettings.Mandanten(i - 1), wb_Global.obMandant).AdminDBName
        Else
            tbMandant.Text = ""
            tbMandantName.Text = DirectCast(wb_GlobalSettings.Mandanten(1), wb_Global.obMandant).MandantName
            tbOrgaBackAdmin.Text = DirectCast(wb_GlobalSettings.Mandanten(1), wb_Global.obMandant).AdminDBName
        End If
    End Sub

    Private Sub BtnScan_Click(sender As Object, e As EventArgs) Handles BtnScan.Click
        ScanMySQL()
    End Sub

    Private Sub ScanMySQL()
        'eigene IP-Adresse ermitteln
        Dim ip As String = wb_Functions.GetCurrentIpV4AddressString()
        'in einzelne Teile zerlegen
        Dim iptp() As String = Split(ip, ".")
        Dim i As String

        'ips to ping - Scan über alle Subnetze
        IPListBox.Items.Clear()
        For j = 1 To 254
            i = iptp(0) & "." & iptp(1) & "." & iptp(2) & "." & j.ToString
            Dim foo As New pingObj
            foo.addr = Net.IPAddress.Parse(i)
            foo.Ip = i
            Dim t As New Threading.Thread(AddressOf pingem)
            t.IsBackground = True
            t.Start(foo)
        Next
    End Sub

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S108:Nested blocks of code should not be left empty", Justification:="<Ausstehend>")>
    Private Sub pingem(p As Object)
        Dim pobj As pingObj = DirectCast(p, pingObj)
        Dim MyPing As New System.Net.NetworkInformation.Ping
        Dim Myreply As System.Net.NetworkInformation.PingReply = MyPing.Send(pobj.addr)

        If Myreply.Status = Net.NetworkInformation.IPStatus.Success Then
            'IP-Adresse gefunden - prüfen ob ein MySQL-Server antwortet
            Dim conn As New MySql.Data.MySqlClient.MySqlConnection
            'MySQL-Server - Verbindung mit Default-Daten
            conn.ConnectionString = "server=" & pobj.Ip & ";port=3306;user=herbst;pass=herbst;database=winback;Connect Timeout=5"
            Try
                conn.Open()
                conn.Close()
                Me.Invoke(Sub() IPListBox.Items.Add(pobj.Ip))
            Catch ex As Exception
            End Try
        End If
    End Sub

End Class

Class pingObj
    Property addr As Net.IPAddress
    Property Ip As String
End Class

