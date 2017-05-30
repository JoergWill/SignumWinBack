<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rezeptur
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim TreeListViewItemCollectionComparer1 As System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer = New System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer()
        Me.TreeListView = New System.Windows.Forms.TreeListView()
        Me.cNummer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cBezeichnung = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cPreis = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cSollwert = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cEinheit = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cProzent = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'TreeListView
        '
        Me.TreeListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TreeListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cNummer, Me.cBezeichnung, Me.cPreis, Me.cSollwert, Me.cEinheit, Me.cProzent})
        TreeListViewItemCollectionComparer1.Column = 0
        TreeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.None
        Me.TreeListView.Comparer = TreeListViewItemCollectionComparer1
        Me.TreeListView.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeListView.GridLines = True
        Me.TreeListView.HideSelection = False
        Me.TreeListView.HoverSelection = True
        Me.TreeListView.LabelEdit = True
        Me.TreeListView.Location = New System.Drawing.Point(12, 232)
        Me.TreeListView.MultiSelect = False
        Me.TreeListView.Name = "TreeListView"
        Me.TreeListView.OwnerDraw = True
        Me.TreeListView.Size = New System.Drawing.Size(704, 276)
        Me.TreeListView.Sorting = System.Windows.Forms.SortOrder.None
        Me.TreeListView.TabIndex = 0
        Me.TreeListView.UseCompatibleStateImageBehavior = False
        '
        'cNummer
        '
        Me.cNummer.Text = "Nummer"
        Me.cNummer.Width = 78
        '
        'cBezeichnung
        '
        Me.cBezeichnung.Text = "Bezeichnung"
        Me.cBezeichnung.Width = 314
        '
        'cPreis
        '
        Me.cPreis.Text = "Preis"
        Me.cPreis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.cPreis.Width = 82
        '
        'cSollwert
        '
        Me.cSollwert.Text = ""
        Me.cSollwert.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.cSollwert.Width = 88
        '
        'cEinheit
        '
        Me.cEinheit.Text = ""
        Me.cEinheit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.cEinheit.Width = 30
        '
        'cProzent
        '
        Me.cProzent.Text = ""
        Me.cProzent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'wb_Rezeptur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 520)
        Me.Controls.Add(Me.TreeListView)
        Me.Name = "wb_Rezeptur"
        Me.Text = "Rezeptur"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cNummer As Windows.Forms.ColumnHeader
    Friend WithEvents cBezeichnung As Windows.Forms.ColumnHeader
    Friend WithEvents cPreis As Windows.Forms.ColumnHeader
    Friend WithEvents cSollwert As Windows.Forms.ColumnHeader
    Friend WithEvents cEinheit As Windows.Forms.ColumnHeader
    Friend WithEvents cProzent As Windows.Forms.ColumnHeader
    Friend WithEvents TreeListView As Windows.Forms.TreeListView
End Class
