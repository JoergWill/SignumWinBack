Imports EnhEdit

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Test_Main
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
        Me.components = New System.ComponentModel.Container()
        Dim ObjectCellBinding1 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding2 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding3 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding4 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding5 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding6 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Me.ColNr = New Infralution.Controls.VirtualTree.Column()
        Me.ColBezeichnung = New Infralution.Controls.VirtualTree.Column()
        Me.CellEditor1 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.EnhEdit2 = New EnhEdit.EnhEdit(Me.components)
        Me.ColPreis = New Infralution.Controls.VirtualTree.Column()
        Me.ColSollwert = New Infralution.Controls.VirtualTree.Column()
        Me.ColEinheit = New Infralution.Controls.VirtualTree.Column()
        Me.ColProzent = New Infralution.Controls.VirtualTree.Column()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.VirtualTree = New Infralution.Controls.VirtualTree.VirtualTree()
        Me.ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColNr
        '
        Me.ColNr.Caption = Nothing
        Me.ColNr.Name = "ColNr"
        '
        'ColBezeichnung
        '
        Me.ColBezeichnung.Caption = Nothing
        Me.ColBezeichnung.CellEditor = Me.CellEditor1
        Me.ColBezeichnung.Name = "ColBezeichnung"
        '
        'CellEditor1
        '
        Me.CellEditor1.CellAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.CellEditor1.Control = Me.EnhEdit2
        '
        'EnhEdit2
        '
        Me.EnhEdit2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EnhEdit2.eBackcolor = System.Drawing.Color.Empty
        Me.EnhEdit2.eBorderColor = System.Drawing.Color.Empty
        Me.EnhEdit2.eFont = Nothing
        Me.EnhEdit2.Location = New System.Drawing.Point(0, 0)
        Me.EnhEdit2.Name = "EnhEdit2"
        Me.EnhEdit2.Size = New System.Drawing.Size(100, 13)
        Me.EnhEdit2.TabIndex = 0
        'Me.EnhEdit2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.EnhEdit2.Visible = False
        '
        'ColPreis
        '
        Me.ColPreis.Caption = Nothing
        Me.ColPreis.Name = "ColPreis"
        '
        'ColSollwert
        '
        Me.ColSollwert.Caption = Nothing
        Me.ColSollwert.CellEditor = Me.CellEditor1
        Me.ColSollwert.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Far
        Me.ColSollwert.Name = "ColSollwert"
        '
        'ColEinheit
        '
        Me.ColEinheit.Caption = Nothing
        Me.ColEinheit.Name = "ColEinheit"
        '
        'ColProzent
        '
        Me.ColProzent.Caption = Nothing
        Me.ColProzent.Name = "ColProzent"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(48, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Test VirtualTree Editor"
        '
        'VirtualTree
        '
        Me.VirtualTree.Columns.Add(Me.ColNr)
        Me.VirtualTree.Columns.Add(Me.ColBezeichnung)
        Me.VirtualTree.Columns.Add(Me.ColPreis)
        Me.VirtualTree.Columns.Add(Me.ColSollwert)
        Me.VirtualTree.Columns.Add(Me.ColEinheit)
        Me.VirtualTree.Columns.Add(Me.ColProzent)
        Me.VirtualTree.Editors.Add(Me.CellEditor1)
        Me.VirtualTree.Location = New System.Drawing.Point(13, 70)
        Me.VirtualTree.Name = "VirtualTree"
        Me.VirtualTree.RowBindings.Add(Me.ObjectRowBinding1)
        Me.VirtualTree.ShowRootRow = False
        Me.VirtualTree.Size = New System.Drawing.Size(329, 285)
        Me.VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        Me.VirtualTree.TabIndex = 2
        '
        'ObjectRowBinding1
        '
        ObjectCellBinding1.Column = Me.ColNr
        ObjectCellBinding1.Field = "Nummer"
        ObjectCellBinding2.Column = Me.ColBezeichnung
        ObjectCellBinding2.Field = "VirtTreeBezeichnung"
        ObjectCellBinding3.Column = Me.ColPreis
        ObjectCellBinding3.Field = "VirtTreePreis"
        ObjectCellBinding4.Column = Me.ColSollwert
        ObjectCellBinding4.Field = "VirtTreeSollwert"
        ObjectCellBinding5.Column = Me.ColEinheit
        ObjectCellBinding5.Field = "VirtTreeEinheit"
        ObjectCellBinding6.Column = Me.ColProzent
        ObjectCellBinding6.Field = "VirtTreeProzent"
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding1)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding2)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding3)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding4)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding5)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding6)
        Me.ObjectRowBinding1.ChildProperty = "ChildSteps"
        Me.ObjectRowBinding1.Name = "ObjectRowBinding1"
        Me.ObjectRowBinding1.ParentProperty = "ParentStep"
        Me.ObjectRowBinding1.TypeName = "WinBack.wb_Rezeptschritt"
        '
        'Test_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1013, 600)
        Me.Controls.Add(Me.VirtualTree)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Test_Main"
        Me.Text = "WinBack läuft im Debug-Mode. Start mit Test"
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
    Friend WithEvents ColNr As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColBezeichnung As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColPreis As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColSollwert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColProzent As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ObjectRowBinding1 As Infralution.Controls.VirtualTree.ObjectRowBinding
    Friend WithEvents CellEditor1 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents EnhEdit2 As EnhEdit.EnhEdit
End Class
