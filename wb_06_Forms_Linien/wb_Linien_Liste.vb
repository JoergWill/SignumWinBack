Imports System.Resources
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Linien_Liste
    Inherits DockContent
    Dim IList As ImageList

    Public ReadOnly Property countItems As Integer
        Get
            Return VNCview.Items.Count
        End Get
    End Property

    ''' <summary>
    ''' Die Image-List für den List-View muss dynamisch erzuegt werden, da sonst das Programm in der
    ''' englischen Windows-Version abstürzt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Linien_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Image-List erzeugen und das Bild aus der Resourcen-Datei laden.
        IList = New ImageList
        IList.Images.Add(PictureVNC.Image)
        IList.ImageSize = PictureVNC.Size
        VNCview.LargeImageList = IList
        'Liste nach Änderung aktualisieren
        AddHandler wb_Linien_Shared.eEdit_Leave, AddressOf LinienInfo
        LoadItems()
    End Sub

    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S2340:""Do"" loops should not be used without a ""While"" or ""Until"" condition", Justification:="<Ausstehend>")>
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

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
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

        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
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
            wb_Linien_Shared.aktAdresse = VNCview.SelectedItems.Item(0).Name
            wb_Linien_Shared.aktBezeichnung = VNCview.SelectedItems.Item(0).Text
            wb_Linien_Shared.Liste_Click(sender)
        End If
    End Sub

    Public Sub LinienInfo(sender As Object)
        If VNCviewIsSelected() Then
            VNCview.SelectedItems.Item(0).Text = wb_Linien_Shared.aktBezeichnung
            VNCview.SelectedItems.Item(0).Name = wb_Linien_Shared.aktAdresse
        End If
    End Sub

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1066:Collapsible ""if"" statements should be merged", Justification:="<Ausstehend>")>
    Private Sub VNCview_DoubleClick(sender As Object, e As EventArgs) Handles VNCview.DoubleClick
        'VNC-Viewer starten

        If VNCviewIsSelected() Then
            If Not wb_Functions.RunExternalProgramm("vncviewer.exe", VNCview.SelectedItems.Item(0).Name() & " /password herbst") Then
                MsgBox("Fehler beim Starten des VNC-Viewers", MsgBoxStyle.Exclamation, "WinBack-Produktions-Linien")
            End If
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
        LinienInfo(sender)
        SaveItems()
        RemoveHandler wb_Linien_Shared.eEdit_Leave, AddressOf LinienInfo
    End Sub
End Class


