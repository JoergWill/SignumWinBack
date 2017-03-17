Public Class Main
    Private Counter As Integer
    ' Create a black Panel to hide TabControls

    Private Sub NotifyIcon_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon.DoubleClick
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
    End Sub

    Private Sub Main_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = False
        End If
    End Sub

    Private Sub MainTimer_Tick(sender As Object, e As EventArgs) Handles MainTimer.Tick
        Counter = Counter + 1
        'lblCounter.Text = String.Format("Ticks {0:#}", Counter)
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Main_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' TabControl - HideTabs
        Wb_TabControl.HideTabs = True
        Wb_TabControl.Hide()
        ' Breite Fenster
        Const fBreite = 366
        ' Bildschirmmauflösung ermitteln
        Dim DesktopSize As Size
        DesktopSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize
        ' Fenster vertikal mximimieren
        Me.Height = DesktopSize.Height + 5
        Me.Top = 0
        Me.Width = fBreite
        Me.Left = DesktopSize.Width - fBreite + 7
    End Sub

    Private Sub BtnHide_Click(sender As Object, e As EventArgs) Handles BtnHide.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        If MessageBox.Show("Server-Task wirklich beenden ?" & vbNewLine & "Danach werden keine Hintergrund-Dienste mehr ausgeführt",
                           "WinBack Server-Task", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Close()
        End If
    End Sub

    Private Sub BtnClients_Click(sender As Object, e As EventArgs) Handles BtnClients.Click
        Wb_TabControl.SelectedTab = TabPageClients
        Wb_TabControl.Show()
    End Sub

    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        Wb_TabControl.SelectedTab = TabPageMessages
        Wb_TabControl.Show()
    End Sub
End Class
