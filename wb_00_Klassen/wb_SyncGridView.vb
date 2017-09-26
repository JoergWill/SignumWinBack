Public Class wb_SyncGridView
    Inherits wb_ArrayGridView

    Public Sub New(ByVal arr() As wb_Sync.Data, Optional ShowTooltips As Boolean = True)
        'Initialisierung nur mit gültigem Daten-Array
        If IsNothing(arr) Then Exit Sub
        'Grid Grundeistellungen
        MyBase._ShowTooltips = ShowTooltips
        InitGrid()
        'Daten anzeigen 
        InitData(arr)
    End Sub
    Public Overloads Sub InitData(ByVal arr() As wb_Global.Nwt)

        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        'Initialisierung nur mit gültigem Daten-Array
        If Not IsNothing(arr) Then
            ' Daten ins Grid eintragen
            FillGrid(arr)
            ' Spaltenansicht einrichten
            InitColumns()
        End If

        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(True)

    End Sub

    Private Overloads Sub FillGrid(ByVal arr() As wb_Global.Nwt)
        ' Die Arraydaten werden in das GridView eingetragen

    End Sub

End Class
