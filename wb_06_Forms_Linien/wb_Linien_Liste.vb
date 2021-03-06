﻿Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Linien_Liste
    Inherits DockContent

    Public ReadOnly Property countItems As Integer
        Get
            Return VNCview.Items.Count
        End Get
    End Property

    Private Sub wb_Linien_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler wb_Linien.eEdit_Leave, AddressOf LinienInfo
        LoadItems()
    End Sub

    Public Sub LoadItems()
        ' Auslesen aus .ini-Datei
        Dim IniFile As New wb_IniFile
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
        IniFile = Nothing
    End Sub

    Public Sub SaveItems()
        Dim IniFile As New wb_IniFile
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
        IniFile = Nothing
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

        Dim winback As New wb_Sql(wb_globalsettings.SqlConWinBack, wb_globalsettings.WinBackDBType)
        If winback.sqlSelect("SELECT * FROM Linien") Then
            While winback.Read
                IPLinie = (winback.iField("L_Nr") + 10).ToString
                IPAdresse = wb_GlobalSettings.MySQLServerIP & ":" & IPLinie
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

    Private Sub VNCview_Click(sender As Object, e As EventArgs) Handles VNCview.Click
        If VNCviewIsSelected() Then
            wb_Linien.aktAdresse = VNCview.SelectedItems.Item(0).Name
            wb_Linien.aktBezeichnung = VNCview.SelectedItems.Item(0).Text
            wb_Linien.Liste_Click(sender)
        End If
    End Sub

    Public Sub LinienInfo()
        If VNCviewIsSelected() Then
            VNCview.SelectedItems.Item(0).Text = wb_Linien.aktBezeichnung
            VNCview.SelectedItems.Item(0).Name = wb_Linien.aktAdresse
        End If
    End Sub

    Private Sub VNCview_DoubleClick(sender As Object, e As EventArgs) Handles VNCview.DoubleClick
        Dim cmdLinie, cmdParam As String
        Dim p As New Process

        'VNC-Viewer starten
        If wb_GlobalSettings.pAddInPath IsNot Nothing Then
            cmdLinie = wb_GlobalSettings.pAddInPath & "vncviewer.exe"
        Else
            cmdLinie = wb_GlobalSettings.PProgrammPath & "vncviewer.exe"
        End If
        If VNCviewIsSelected() Then
            cmdParam = VNCview.SelectedItems.Item(0).Name() & " /password herbst"
            Try
                p.StartInfo.FileName = cmdLinie
                p.StartInfo.Arguments = cmdParam
                p.StartInfo.UseShellExecute = False
                p.StartInfo.RedirectStandardOutput = False
                p.StartInfo.CreateNoWindow = False
                p.Start()
            Catch ex As Exception
                MsgBox("Fehler beim Starten des VNC-Viewers", MsgBoxStyle.Exclamation, "WinBack-Produktions-Linien")
            End Try
        End If
    End Sub

    Private Function VNCviewIsSelected() As Boolean
        Try
            Return (VNCview.SelectedItems.Count > 0)
        Catch
            Return False
        End Try
    End Function

    Private Sub wb_Linien_Liste_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        LinienInfo()
        SaveItems()
    End Sub
End Class


