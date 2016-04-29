Imports System.Windows.Forms
Imports Signum.OrgaSoft.AddIn.WinBack.wb_Konfig
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Linien_Liste
    Inherits DockContent

    Public Property aktBezeichnung As String
        Set(value As String)
            If VNCviewIsSelected() Then
                VNCview.SelectedItems.Item(0).Text = value
            End If
        End Set
        Get
            If VNCviewIsSelected() Then
                Return VNCview.SelectedItems.Item(0).Text
            Else
                Return vbNull
            End If
        End Get
    End Property

    Public Property aktAdresse As String
        Set(value As String)
            If VNCviewIsSelected() Then
                VNCview.SelectedItems.Item(0).Name = value
            End If
        End Set
        Get
            If VNCviewIsSelected() Then
                Return VNCview.SelectedItems.Item(0).Name
            Else
                Return vbNull
            End If
        End Get
    End Property

    Private Sub wb_Linien_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Auslesen aus .ini-Datei
        Dim IniFile As New Signum.OrgaSoft.AddIn.WinBack.wb_Konfig
        Dim i As Integer = 0
        Dim IPAdresse, IPComment As String
        Dim ListItem As ListViewItem

        Do
            i += 1
            IPAdresse = IniFile.ReadString("VNC", "IP" & i.ToString)
            IPComment = IniFile.ReadString("VNC", "Comment" & i.ToString)
            If IPAdresse IsNot "" Then
                ListItem = VNCview.Items.Add(IPAdresse, IPComment, 0)
            Else
                Exit Do
            End If
        Loop
    End Sub

    Public Sub SaveItems()
        Dim IniFile As New Signum.OrgaSoft.AddIn.WinBack.wb_Konfig
        Dim i As Integer
        For i = 1 To VNCview.Items.Count
            IniFile.WriteString("VNC", "IP" & i.ToString, VNCview.Items(i - 1).Name)
            If IniFile.WriteResult = False Then
                Exit For
            End If
            IniFile.WriteString("VNC", "Comment" & i.ToString, VNCview.Items(i - 1).Text)
        Next
    End Sub

    Public Event ItemSelected()
    Private Sub VNCview_Click(sender As Object, e As EventArgs) Handles VNCview.Click
        If VNCviewIsSelected() Then
            RaiseEvent ItemSelected()
        End If
    End Sub

    Private Sub VNCview_DoubleClick(sender As Object, e As EventArgs) Handles VNCview.DoubleClick
        Dim cmdLinie, cmdParam As String
        Dim p As New Process

        'VNC-Viewer starten
        cmdLinie = "c:\Programme\Winback\vncviewer.exe"
        If VNCviewIsSelected() Then
            cmdParam = VNCview.SelectedItems.Item(0).Name() & " /password herbst"

            p.StartInfo.FileName = cmdLinie
            p.StartInfo.Arguments = cmdParam
            p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardOutput = False
            p.StartInfo.CreateNoWindow = False
            p.Start()

        End If
    End Sub

    Private Function VNCviewIsSelected() As Boolean
        Try
            If (VNCview.SelectedItems(0).Index < VNCview.Items.Count) And (VNCview.SelectedItems(0).Index >= 0) Then
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try
    End Function

End Class


