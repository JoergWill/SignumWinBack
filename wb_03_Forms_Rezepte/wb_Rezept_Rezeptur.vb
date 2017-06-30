Imports System.Windows.Forms
Imports combit.ListLabel22.DataProviders

Public Class wb_Rezept_Rezeptur

    Dim Rezept As New wb_Rezept
    'Dim LL_Rezeptur As New combit.ListLabel22.ListLabel()


    Friend Sub FillVirtualTree()
        VirtualTree.DataSource = Rezept.RootRezeptSchritt
    End Sub

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'LL_Rezeptur.DataSource = New ObjectDataProvider(Rezept)
        'LL_Rezeptur.AutoProjectType = combit.ListLabel22.LlProject.List

        Debug.Print("Nach LL.DataSource")
    End Sub

    Private Sub BtnDrucken_Click(sender As Object, e As EventArgs) Handles BtnDrucken.Click
        Debug.Print("LL Drucken")
        'LL_Rezeptur.AutoProjectFile = "Rezeptur.lst"
        'LL_Rezeptur.AutoShowSelectFile = False
        'LL_Rezeptur.Print()
        'LL_Rezeptur.Design()
    End Sub

    Private Sub VirtualTree_Click(sender As Object, e As EventArgs) Handles VirtualTree.Click

    End Sub

    Private Sub VirtualTree_DoubleClick(sender As Object, e As EventArgs) Handles VirtualTree.DoubleClick

    End Sub
End Class