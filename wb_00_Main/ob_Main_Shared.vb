Imports System.Drawing
Imports Signum.OrgaSoft.GUI

''' <summary>
''' ACHTUNG ab OrgaSoft Version 3.6.1 werden einige Properties geändert:
'''     Breaking Changes:
'''     Signum.OrgaSoft.GUI.IButton => Signum.OrgaSoft.GUI.IRibbonButton
'''     Signum.OrgaSoft.GUI.IGroup => Signum.OrgaSoft.GUI.IRibbonGroup
'''     Signum.OrgaSoft.GUI.ITab => Signum.OrgaSoft.GUI.IRibbonTab
'''     
''' </summary>
Public Class ob_Main_Shared
    'VOR VERSION 3.6.1
    'Private Shared _MenuButtons As New List(Of IButton)
    Private Shared _MenuButtons As New List(Of IRibbonButton)

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S107:Procedures should not have too many parameters", Justification:="<Ausstehend>")>
    Public Shared Sub AddMenuButton(Gruppe As IGroup, Name As String, Text As String, ToolTip As String, PictureSmall As Image, PictureLarge As Image, ClickHandler As EventHandler, Tag As Integer)
        'VOR VERSION 3.6.1
        'Dim MenuBtn As IButton = Gruppe.AddButton(Tag.ToString("000") & Name, Text, ToolTip, PictureSmall, PictureLarge, ClickHandler)
        Dim MenuBtn As IRibbonButton = Gruppe.AddButton(Tag.ToString("000") & Name, Text, ToolTip, PictureSmall, PictureLarge, ClickHandler)
        _MenuButtons.Add(MenuBtn)
    End Sub

    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public Shared Sub CheckMenu(oTabs As IList(Of ITab), bProduktion As Boolean)

        'Prüfen ob schon ein WinBack-Menu existiert
        If _MenuButtons.Count > 0 Then
            'erste Gruppe
            Dim Group As IGroup = _MenuButtons.First.Parent
            setGroupVisible(Group, False)

            'Prüfen ob schon ein Eintrag wbMain_Menu existiert
            Dim mtab As ITab
            For Each mtab In oTabs
                If mtab.Name = "wb_MainMenu" Then
                    'Hauptmenu-Eintrag 'Produktion' unsichtbar
                    mtab.Visible = False

                    'Alle Signum-Menu-Buttons durchlaufen und einzeln ein/ausschalten
                    For Each Btn In _MenuButtons
                        'Neue Gruppe (Gruppe nur ausblenden wenn WinBack-Guppe)
                        If Btn.Parent.Name <> Group.Name Then
                            Group = Btn.Parent
                            setGroupVisible(Group, False)
                        End If
                        'Button ein/ausblenden
                        Btn.Visible = wb_AktRechte.RechtOK(Left(Btn.Name, 3), wb_AktUser.SuperUser) AndAlso bProduktion
                        'Wenn ein Button sichtbar ist, bleibt die Gruppe sichtbar
                        setGroupVisible(Group, Btn.Visible OrElse Group.Visible)
                        'Wenn eine Gruppe sichtbar ist, wird auch immer der MainMenu-Button sichtbar
                        If Group.Visible AndAlso bProduktion Then
                            mtab.Visible = True
                        End If
                    Next
                End If
            Next
        End If
    End Sub
    Private Shared Sub setGroupVisible(ByRef Group As IGroup, visible As Boolean)
        'Property Visible darf nur bei WinBack-Gruppen geändert werden
        If Group.Name.StartsWith("WinBack") Then
            Group.Visible = visible
        End If
    End Sub

End Class
