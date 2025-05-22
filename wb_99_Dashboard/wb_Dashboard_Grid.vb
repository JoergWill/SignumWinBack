Imports WeifenLuo.WinFormsUI.Docking
Imports System.Windows.Forms
Imports System.Management

Public Class wb_Dashboard_Grid
    Inherits DockContent

    Dim DashArtikel As wb_DashArtikel
    Dim DashRezept As wb_DashRezept
    Dim DashRohstoff As wb_DashRohstoff
    Dim DashUser As wb_DashUser
    Dim DashLinien As wb_DashLinien
    Dim DashChargen As wb_DashChargen
    Dim DashAdmin As wb_DashAdmin
    Dim DashKneter As wb_DashKneter

    Private DashToMove As Control
    Public SubMenuItems As New List(Of wb_Global.wbMenuItem)


    Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Dim m As wb_Global.wbMenuItem

        'Menuzeile (Menu-Stripe für WinBack-AnyWhere)
        m = New wb_Global.wbMenuItem
        m.MenuType = wb_Global.wbMenuType.MainMenu
        m.Text = "Info"
        m.Bild = My.Resources.Resources.orgaback_logo_header_jpg_64x64
        m.Click = AddressOf BtnInfo_Click
        SubMenuItems.Add(m)

        ''Menuzeile (Menu-Stripe für WinBack-AnyWhere)
        'm = New wb_Global.wbMenuItem
        'm.MenuType = wb_Global.wbMenuType.MainMenu
        'm.Text = "Admin"
        'm.Bild = My.Resources.Resources.Admin_32x32
        'SubMenuItems.Add(m)

        'Menuzeile (Menu-Stripe für WinBack-AnyWhere)
        m = New wb_Global.wbMenuItem
        m.MenuType = wb_Global.wbMenuType.MainMenu
        m.Text = "Abmelden"
        m.Bild = My.Resources.Resources.UserDetails_32x32
        m.Click = AddressOf BtnAbmelden_Click
        SubMenuItems.Add(m)

        'Menuzeile (Menu-Stripe für WinBack-AnyWhere)
        m = New wb_Global.wbMenuItem
        m.MenuType = wb_Global.wbMenuType.MainMenu
        m.Text = "Beenden"
        m.Bild = My.Resources.Resources.IconDelete_24x24
        m.Click = AddressOf BtnBeenden_Click
        SubMenuItems.Add(m)

        'alle Einträge in der Liste löschen 
        wb_Dashboard_Shared.DashBoards.Clear()

        'alle Dashes neu erzeugen
        DashArtikel = New wb_DashArtikel
        DashRezept = New wb_DashRezept
        DashRohstoff = New wb_DashRohstoff
        DashUser = New wb_DashUser
        DashLinien = New wb_DashLinien
        DashChargen = New wb_DashChargen
        DashAdmin = New wb_DashAdmin
        DashKneter = New wb_DashKneter

    End Sub

    Private Sub wb_Dashboard_Grid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Checkbox Dashboard anzeigen
        cbDashBoard.Checked = wb_GlobalSettings.ShowDashboard

        'Popup-Menu erstellen
        PopUpDash.Items.Clear()
        'winback.ini
        Dim IniFile As New wb_IniFile

        'Laden der einzelnen Dashes
        For Each Dash In wb_Dashboard_Shared.DashBoards

            'Maus-Events zum Verschieben der einzelnen Dashes
            AddHandler Dash.MouseDown, AddressOf DashMouseDown
            AddHandler Dash.MouseUp, AddressOf DashMouseUp

            'Dash ins Layout einreihen (egal ob sichtbar oder nicht)
            GetDashLayout(Dash, IniFile)
            'Control ins FlowPanel einsetzen
            FlowLayoutPanel.Controls.Add(Dash)
            'Reihenfolge im FlowPanel
            FlowLayoutPanel.Controls.SetChildIndex(Dash, Dash.FlowLayoutIndex)
        Next

        'Dashboard immer sichtbar bei Programm-Variante AnyWhere (nicht abschaltbar)
        cbDashBoard.Visible = (wb_GlobalSettings.pVariante <> wb_Global.ProgVariante.AnyWhere)
    End Sub

    ''' <summary>
    ''' Läd alle Relevanten Daten zum Dash-Element aus der winback.ini und
    ''' baut das Popup-Menu für die Auswahl der Anzeige auf.
    ''' </summary>
    ''' <param name="Dash"></param>
    ''' <returns></returns>
    Private Function GetDashLayout(ByVal Dash As wb_DashElement, IniFile As wb_IniFile) As Boolean
        'Index zum Popup-Menu
        Dash.Index = PopUpDash.Items.Count
        'Dash-Reihenfolge im FlowLayoutPanel
        LoadItems(Dash, IniFile)

        'Dash zum Popup-Menu hinzufügen
        Dim mMenuItem As ToolStripMenuItem = New ToolStripMenuItem(Dash.Title, Nothing, AddressOf DashPopupClick)
        mMenuItem.Checked = True
        mMenuItem.CheckState = Dash.CheckState
        mMenuItem.Enabled = wb_AktRechte.RechtOK(Dash.Tag, wb_AktUser.SuperUser)

        PopUpDash.Items.Add(TryCast(mMenuItem, ToolStripMenuItem))
        Return Dash.ShowMe
    End Function

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
    Private Sub DashPopupClick(Sender As Object, e As EventArgs)
        'ToolStripMenue-Item
        Dim m As ToolStripMenuItem = TryCast(Sender, ToolStripMenuItem)
        'Dash zum Element aus der Popup-Liste
        For Each Dash As wb_DashElement In wb_Dashboard_Shared.DashBoards
            'Dash zum Popup-Menu-Item gefunden
            If Dash.Title = m.Text Then
                'Sichtbarkeit umkehren
                Dash.ShowMe = Not Dash.ShowMe
                'Check-State im Popup-Menu
                TryCast(PopUpDash.Items(Dash.Index), ToolStripMenuItem).CheckState = Dash.CheckState
                'und fertig
                Exit For
            End If
        Next
    End Sub

    Private Sub DashMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'dieses Dash soll verschoben werden
        DashToMove = DirectCast(sender, wb_DashElement)
        'Mauszeiger ändern
        Cursor.Current = Cursors.Hand
    End Sub

    Private Sub DashMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'TryCast ist notwendig, weil das Dash-Objekt eventuell schon disposed ist
        Dim Dash As wb_DashElement = TryCast(sender, wb_DashElement)
        'Da MouseUp auch nach MouseClick ausgelöst wird - vorher prüfen, ob das Dash-Element schon gelöscht wurde(nach Fensterwechsel)
        If Dash IsNot Nothing AndAlso Not Dash.IsDisposed Then
            'ein kleiner Umweg über den screen, da e nur die Koordinaten innerhalb des Controls liefert
            Dim Tpt As Drawing.Point = Dash.PointToScreen(e.Location)
            'Das Control ermittel welches unter der Maus ist
            Dim c As Control = FlowLayoutPanel.GetChildAtPoint(FlowLayoutPanel.PointToClient(Tpt), GetChildAtPointSkip.None)
            'Jetzt brauchen wir den Index des Controls, vor dem wir das Label einfügen wollen
            Dim i As Integer = FlowLayoutPanel.Controls.IndexOf(c)
            'und verschieben
            FlowLayoutPanel.Controls.SetChildIndex(DashToMove, i)
            TryCast(DashToMove, wb_DashElement).FlowLayoutIndex = i
            'Mauszeiger ändern
            Cursor.Current = Cursors.Default
        End If
    End Sub

    Private Sub cbDashBoard_Click(sender As Object, e As EventArgs) Handles cbDashBoard.Click
        wb_GlobalSettings.ShowDashboard = cbDashBoard.Checked
        SaveItems()
    End Sub

    Public Sub LoadItems(ByVal Dash As wb_DashElement, IniFile As wb_IniFile)
        Dash.ShowMe = IniFile.ReadBool("DashBoard", Dash.Title & "_Visible", True)
        Dash.FlowLayoutIndex = IniFile.ReadInt("DashBoard", Dash.Title & "_Index", 99)
        'TODO TEST alle Rechte TEST
        Dash.Enabled = wb_AktUser.RechtOK(Dash.Tag, False) Or True
    End Sub

    Public Sub SaveItems()
        Dim IniFile As New wb_IniFile

        For Each Dash In wb_Dashboard_Shared.DashBoards
            IniFile.WriteBool("DashBoard", Dash.Title & "_Visible", Dash.ShowMe)
            IniFile.WriteInt("DashBoard", Dash.Title & "_Index", FlowLayoutPanel.Controls.IndexOf(Dash))
        Next
    End Sub

    Private Sub wb_Dashboard_Grid_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Einstellungen sichern
        SaveItems()
        'alle Events wieder freigeben
        For Each Dash In wb_Dashboard_Shared.DashBoards
            RemoveHandler Dash.MouseDown, AddressOf DashMouseDown
            RemoveHandler Dash.MouseUp, AddressOf DashMouseUp
        Next
        'Liste aller Dashboards leeren
        wb_Dashboard_Shared.DashBoards.Clear()
    End Sub

    Private Sub BtnInfo_Click(sender As Object, e As EventArgs)
        wb_Main_Shared.OpenForm(sender, "About")
    End Sub

    Private Sub BtnAbmelden_Click(sender As Object, e As EventArgs)
        wb_Main_Shared.SendMessage(sender, "Logout")
    End Sub

    Private Sub BtnBeenden_Click(sender As Object, e As EventArgs)
        wb_Main_Shared.SendMessage(sender, "Close")
    End Sub
End Class

