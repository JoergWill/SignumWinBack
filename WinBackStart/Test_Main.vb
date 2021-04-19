Imports System.Threading
Imports Infralution.Controls.VirtualTree

Public Class Test_Main
    Implements IMainMenu
    Dim Rezept As wb_Rezept

    Private _RezeptSchritt As wb_Rezeptschritt = Nothing    'aktuelle ausgewählter Rezeptschritt (Popup)

    Declare Auto Function SetParent Lib "user32.dll" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As Integer
    Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Private Const WM_SYSCOMMAND As Integer = 274
    Private Const SC_MAXIMIZE As Integer = 61488

#Region "MainForm"
    Public Property DkPnlConfigFileName As String Implements IMainMenu.DkPnlConfigFileName
        Get
            Return ""
        End Get
        Set(value As String)
        End Set
    End Property

    Public Function ExecuteCmd(Cmd As String, Prm As String) As Boolean Implements IMainMenu.ExecuteCmd
        Debug.Print("ExecuteCmd " & Cmd & " / " & Prm)
        Return True
    End Function
#End Region

    Private Sub Test_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Rezeptkopf und Rezeptschritte aktuell (winback) laden
        ' Backverlust 0.0 !
        Rezept = New wb_Rezept(2691, Nothing, 0.0, 1)

        'Virtual Tree anzeigen
        'VirtualTree.DataSource = Rezept.RootRezeptSchritt
        'alle Zeilen aufklappen
        'VirtualTree.RootRow.ExpandChildren(True)

    End Sub


    Private Sub VirtualTree_GetCellData(sender As Object, e As GetCellDataEventArgs)
        'get the default binding for the given row And use it to populate the cell data
        'Dim Binding As RowBinding = _VirtualTree.GetRowBinding(e.Row)
        'Binding.GetCellData(e.Row, e.Column, e.CellData)

        'aktuell ausgewählten Rezeptschritt merken (Popup)
        _RezeptSchritt = DirectCast(e.Row.Item, wb_Rezeptschritt)

        'Edit Bezeichnungs-Text
        If e.Column.Name = "ColBezeichnung" And wb_Functions.TypeIstText(_RezeptSchritt.Type) Then
            Exit Sub
        End If

        'Edit Sollwert
        If e.Column.Name = "ColSollwert" And (wb_Functions.TypeIstSollMenge(_RezeptSchritt.Type, 1) Or wb_Functions.TypeIstSollWert(_RezeptSchritt.Type, 3)) Then
            Exit Sub
        End If

        'Edit nicht erlaubt
        e.CellData.Editor = Nothing
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Debug.Print("test")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim p As New Process()
        p.StartInfo = New ProcessStartInfo("hh.exe", "C:\Users\Will.WINBACK\Source\Repos\Signum_WinBack\WinBackStart\WinBack.chm")
        p.StartInfo.CreateNoWindow = False
        p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            'p.StartInfo.WorkingDirectory = Directory
            p.Start()
        'p.WaitForExit()



        'Dim proc As New Process
        'proc = Process.Start("Calc.exe")
        p.WaitForInputIdle()
        'Thread.Sleep(1000)
        SetParent(p.MainWindowHandle, Me.Panel1.Handle)
        Thread.Sleep(1000)
        SendMessage(p.MainWindowHandle, WM_SYSCOMMAND, SC_MAXIMIZE, 0)



        'Help.ShowHelp(Panel1, "C:\Users\Will.WINBACK\Source\Repos\Signum_WinBack\WinBackStart\WinBack.chm")
    End Sub
End Class