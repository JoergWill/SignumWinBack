Imports Signum.OrgaSoft.AddIn.wb_Functions
Imports System.Windows.Forms

Public Class wb_User_Rechte
    Private Sub wb_User_Rechte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler wb_User_Shared.eListe_Click, AddressOf ShowGroupInfo
    End Sub

    'TreeView - Anzeige der aktiven Benutzer-Rechte(Gruppe)
    Private Sub ShowGroupInfo()
        Dim sBezeichnung, sKommentar, sText As String
        Dim iAttr As Integer

        'aktive Gruppe
        Dim aktUserGroup As Integer = wb_User_Shared.aktUserGroup
        'Abfrage der aktuellen Rechte aus der Datenbank
        Dim sql As String = "Select ItemTypen.IT_Bezeichnung, ItemIDs.II_Kommentar, AT_Wert2int, Texte.T_Text FROM ItemIDs " &
              "INNER JOIN " &
              "ItemTypen On ItemIDs.II_ItemTyp = ItemTypen.IT_ItemTyp " &
              "INNER JOIN " &
              "ItemParameter On ItemIDs.II_ItemID = ItemParameter.IP_ItemID AND " &
              "ItemIDs.II_ItemTyp = ItemParameter.IP_ItemTyp " +
              "INNER JOIN " &
              "IAttrParams On ItemParameter.IP_Wert2int = IAttrParams.AT_Wert3str AND " &
              "ItemParameter.IP_ItemAttr = IAttrParams.AT_Attr_Nr " &
              "INNER JOIN " &
              "Texte On IAttrParams.AT_Wert2int = Texte.T_TextIndex AND " &
              "IAttrParams.AT_Attr_Nr = Texte.T_Typ " &
              "WHERE ItemIDs.II_ItemTyp <= 230 AND ItemParameter.IP_Wert1int =" & aktUserGroup & " AND " &
              "Texte.T_Sprache = 0 " &
              "ORDER BY ItemIDs.II_ItemTyp, ItemIDs.II_ItemID;"

        Dim oldBezeichnung As String = ""
        Dim newNode As TreeNode = Nothing
        Dim subNode As TreeNode = Nothing
        TreeView.Nodes.Clear()

        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        If winback.sqlSelect(sql) Then
            While winback.Read
                sBezeichnung = winback.sField("IT_Bezeichnung")
                sKommentar = TextFilter(winback.sField("II_Kommentar"))
                sText = winback.sField("T_Text")
                iAttr = winback.iField("AT_Wert2int")

                If (TreeView.Nodes.Count = 0) Or oldBezeichnung <> sBezeichnung Then
                    newNode = New TreeNode(sBezeichnung)
                    newNode.ImageIndex = 0
                    TreeView.Nodes.Add(newNode)
                    oldBezeichnung = sBezeichnung
                Else
                    subNode = New TreeNode("(" & sText & ")" & " " & sKommentar)
                    subNode.ImageIndex = iAttr
                    newNode.Nodes.Add(subNode)
                End If

            End While
        End If
        winback.Close()
    End Sub
End Class