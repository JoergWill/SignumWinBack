Imports WinBack.wb_User_Shared
Imports WinBack.wb_Global
Imports System.Windows.Forms

Public Class wb_User_Rechte

    'Event User aus Liste ausgewählt - Detail-Info anzeigen - User-Rechte
    Private Sub wb_User_Rechte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler eListe_Click, AddressOf GroupInfo
    End Sub

    'TreeView - Anzeige der aktiven Benutzer-Rechte(Gruppe)
    Private Sub GroupInfo()
        'aktive Gruppe
        Dim Oberbegriff As String = ""
        Dim newNode As TreeNode = Nothing
        Dim subNode As TreeNode = Nothing

        'Benutzer-Rechte laden
        Gruppe.LoadData(User.iGruppe)
        TreeView.Nodes.Clear()

        'Rechte-Struktur im TreeView anzeigen
        For Each UR As wb_GruppenRechte In Gruppe.UserRechte
            If Oberbegriff <> UR.OberBegriff Then
                newNode = New TreeNode(UR.OberBegriff)
                newNode.ImageIndex = 0
                TreeView.Nodes.Add(newNode)
                Oberbegriff = UR.OberBegriff
            Else
                subNode = New TreeNode("(" & UR.sAttribut & ")" & " " & UR.Bezeichnung)
                subNode.ImageIndex = UR.iAttribut
                newNode.Nodes.Add(subNode)
            End If
        Next

        'alle Einträge anzeigen
        TreeView.ExpandAll()
    End Sub
End Class