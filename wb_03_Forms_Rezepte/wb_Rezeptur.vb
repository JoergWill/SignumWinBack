Imports System.Windows.Forms

Public Class wb_Rezeptur

    Public Function GetRezeptur() As Boolean
        'The control NEEDS the SmallImageList property set. It uses the ImageSize property of that to draw the plus-signs, root lines and indent correctly
        TreeListView.SmallImageList = New System.Windows.Forms.ImageList()

        'Rezeptur in Grid einlesen
        Dim itemA As New TreeListViewItem("577", 1)
        itemA.SubItems.Add("Weizenmehl Type 550")
        itemA.SubItems.Add("25,60 €")
        itemA.SubItems.Add("10,000")
        itemA.SubItems.Add("kg")
        itemA.SubItems.Add("96,15%")
        TreeListView.Items.Add(itemA)

        Dim itemB As New TreeListViewItem("20", 2)
        itemB.SubItems.Add("Schüttwasser")
        itemB.SubItems.Add("")
        itemB.SubItems.Add("2,000")
        itemB.SubItems.Add("kg")
        itemB.Expand()
        TreeListView.Items.Add(itemB)

        Dim itemC As New TreeListViewItem("20", 2)
        itemC.SubItems.Add("Temperatur")
        itemC.SubItems.Add("")
        itemC.SubItems.Add("24,500")
        itemC.SubItems.Add("°C")
        itemB.Items.Add(itemC)

        Dim itemD As New TreeListViewItem("20", 2)
        itemD.SubItems.Add("Auslauf")
        itemD.SubItems.Add("")
        itemD.SubItems.Add("1,000")
        itemB.Items.Add(itemD)

        Dim itemE As New TreeListViewItem("574", 1)
        itemE.SubItems.Add("Hefe")
        itemE.SubItems.Add("2,24 €")
        itemE.SubItems.Add("2,000")
        itemE.SubItems.Add("kg")
        itemE.SubItems.Add("19,23%")
        TreeListView.Items.Add(itemE)



        Return True
    End Function

    Private Sub TreeListView_BeforeLabelEdit(sender As Object, e As TreeListViewBeforeLabelEditEventArgs) Handles TreeListView.BeforeLabelEdit

        e.Cancel = False
    End Sub

    Private Sub TreeListView_Resize(sender As Object, e As EventArgs) Handles TreeListView.Resize
        TreeListView.SetAutoColWidth(1)
    End Sub
End Class