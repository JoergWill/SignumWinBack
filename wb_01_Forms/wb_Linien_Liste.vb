Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports Signum.OrgaSoft.AddIn.OrgasoftMain.wb_Konfig
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

    Public ReadOnly Property countItems As Integer
        Get
            Return VNCview.Items.Count
        End Get
    End Property

    Private Sub wb_Linien_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadItems()
    End Sub

    Public Sub LoadItems()
        ' Auslesen aus .ini-Datei
        Dim IniFile As New Signum.OrgaSoft.AddIn.OrgasoftMain.wb_Konfig
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
        Dim IniFile As New Signum.OrgaSoft.AddIn.OrgasoftMain.wb_Konfig
        Dim i As Integer
        For i = 1 To VNCview.Items.Count
            IniFile.WriteString("VNC", "IP" & i.ToString, VNCview.Items(i - 1).Name)
            If IniFile.WriteResult = False Then
                Exit For
            End If
            IniFile.WriteString("VNC", "Comment" & i.ToString, VNCview.Items(i - 1).Text)
        Next
        ' Letzter Eintrag ist leer
        IniFile.WriteString("VNC", "IP" & i.ToString, "")
        IniFile.WriteString("VNC", "Comment" & i.ToString, "")
    End Sub

    Public Sub AddItems(key As String, Text As String)
        Dim ListItem As ListViewItem

        With VNCview
            ListItem = .Items.Add(key, Text, 0)
            .SelectedItems.Clear()
            '.Select()
            '.Items(.Items.Count - 1).Selected = True
        End With
    End Sub
    Public Sub SelectLastItem()
        VNCview.Items(VNCview.Items.Count - 1).Selected = True
    End Sub
    Public Sub AddFromDataBase()
        Dim IPLinie, IPAdresse, IPComment As String

        'alle Einträge löschen
        VNCview.Clear()

        Dim winback As New wb_Sql(My.Settings.MySQLConWinBack, wb_Sql.dbType.mySql)
        If winback.sqlSelect("SELECT * FROM Linien") Then
            While winback.Read
                IPLinie = (winback.iField("L_Nr") + 10).ToString
                IPAdresse = My.Settings.MySQLServerIP & ":" & IPLinie
                IPComment = winback.sField("L_Bezeichnung")
                AddItems(IPAdresse, IPComment)
            End While
        End If
        winback.Close()

    End Sub

    Public Sub RemoveItem()
        Dim i As Integer = 0
        With VNCview
            While i < .Items.Count
                If .Items(i).Selected Then
                    .Items(i).Remove()
                End If
                i += 1
            End While
        End With
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


