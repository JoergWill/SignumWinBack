Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_Parameter
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

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
        Me.VirtualTree = New Infralution.Controls.VirtualTree.VirtualTree()
        Me.CellEditor2 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.CellEditor1 = New Infralution.Controls.VirtualTree.CellEditor()
        Me.EnhEdit1 = New EnhEdit.EnhEdit(Me.components)
        Me.ColType = New Infralution.Controls.VirtualTree.Column()
        Me.ColNr = New Infralution.Controls.VirtualTree.Column()
        Me.ColBezeichnung = New Infralution.Controls.VirtualTree.Column()
        Me.ColWert = New Infralution.Controls.VirtualTree.Column()
        Me.ColEinheit = New Infralution.Controls.VirtualTree.Column()
        Me.ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VirtualTree
        '
        Me.VirtualTree.AllowMultiSelect = False
        Me.VirtualTree.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VirtualTree.AutoFitColumns = True
        Me.VirtualTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VirtualTree.Columns.Add(Me.ColType)
        Me.VirtualTree.Columns.Add(Me.ColNr)
        Me.VirtualTree.Columns.Add(Me.ColBezeichnung)
        Me.VirtualTree.Columns.Add(Me.ColWert)
        Me.VirtualTree.Columns.Add(Me.ColEinheit)
        Me.VirtualTree.EditOnKeyPress = True
        Me.VirtualTree.Editors.Add(Me.CellEditor2)
        Me.VirtualTree.Editors.Add(Me.CellEditor1)
        Me.VirtualTree.HeaderHeight = 24
        Me.VirtualTree.HeaderStyle.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.VirtualTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.None
        Me.VirtualTree.Location = New System.Drawing.Point(24, 23)
        Me.VirtualTree.Name = "VirtualTree"
        Me.VirtualTree.RowBindings.Add(Me.ObjectRowBinding1)
        Me.VirtualTree.RowEvenStyle.BackColor = System.Drawing.Color.PowderBlue
        Me.VirtualTree.RowStyle.BorderColor = System.Drawing.Color.LightGray
        Me.VirtualTree.ShowRootRow = False
        Me.VirtualTree.Size = New System.Drawing.Size(781, 399)
        Me.VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        Me.VirtualTree.TabIndex = 9
        '
        'CellEditor1
        '
        Me.CellEditor1.CellAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CellEditor1.Control = Me.EnhEdit1
        Me.CellEditor1.UseCellPadding = True
        '
        'EnhEdit1
        '
        Me.EnhEdit1.AcceptsReturn = True
        Me.EnhEdit1.AcceptsTab = True
        Me.EnhEdit1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EnhEdit1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EnhEdit1.eBackcolor = System.Drawing.Color.Empty
        Me.EnhEdit1.eBorderColor = System.Drawing.Color.Empty
        Me.EnhEdit1.eFont = Nothing
        Me.EnhEdit1.Location = New System.Drawing.Point(-40, -1)
        Me.EnhEdit1.Name = "EnhEdit1"
        Me.EnhEdit1.Size = New System.Drawing.Size(10, 13)
        Me.EnhEdit1.TabIndex = 8
        Me.EnhEdit1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.EnhEdit1.Visible = False
        '
        'ColType
        '
        Me.ColType.Caption = "Type"
        Me.ColType.Name = "ColType"
        Me.ColType.Width = 155
        '
        'ColNr
        '
        Me.ColNr.Caption = "Nr"
        Me.ColNr.Name = "ColNr"
        Me.ColNr.Width = 155
        '
        'ColBezeichnung
        '
        Me.ColBezeichnung.Caption = "Parameter"
        Me.ColBezeichnung.Name = "ColBezeichnung"
        Me.ColBezeichnung.Width = 155
        '
        'ColWert
        '
        Me.ColWert.Caption = "Wert"
        Me.ColWert.Name = "ColWert"
        Me.ColWert.Width = 155
        '
        'ColEinheit
        '
        Me.ColEinheit.Caption = "Einh"
        Me.ColEinheit.Name = "ColEinheit"
        Me.ColEinheit.Width = 155
        '
        'ObjectRowBinding1
        '
        Me.ObjectRowBinding1.AllowDrag = True
        Me.ObjectRowBinding1.AllowDropAboveRow = True
        Me.ObjectRowBinding1.AllowDropBelowRow = True
        ObjectCellBinding1.Column = Me.ColType
        ObjectCellBinding1.Field = "TypNr"
        ObjectCellBinding2.Column = Me.ColNr
        ObjectCellBinding2.Field = "ParamNr"
        ObjectCellBinding3.Column = Me.ColBezeichnung
        ObjectCellBinding3.Field = "Bezeichnung"
        ObjectCellBinding4.Column = Me.ColWert
        ObjectCellBinding4.Field = "Wert"
        ObjectCellBinding5.Column = Me.ColEinheit
        ObjectCellBinding5.Field = "Einheit"
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding1)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding2)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding3)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding4)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding5)
        Me.ObjectRowBinding1.ChildProperty = "ChildSteps"
        Me.ObjectRowBinding1.Height = 24
        Me.ObjectRowBinding1.Name = "ObjectRowBinding1"
        Me.ObjectRowBinding1.ParentProperty = "ParentStep"
        Me.ObjectRowBinding1.Style.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.ObjectRowBinding1.TypeName = "WinBack.wb_KomponParam"
        '
        'wb_Rohstoffe_Parameter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 460)
        Me.Controls.Add(Me.VirtualTree)
        Me.Controls.Add(Me.EnhEdit1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "wb_Rohstoffe_Parameter"
        Me.Text = "Rohstoff Parameter"
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
    Friend WithEvents CellEditor2 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents CellEditor1 As Infralution.Controls.VirtualTree.CellEditor
    Friend WithEvents EnhEdit1 As EnhEdit.EnhEdit
    Friend WithEvents ObjectRowBinding1 As Infralution.Controls.VirtualTree.ObjectRowBinding
    Friend WithEvents ColType As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColNr As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColBezeichnung As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColWert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheit As Infralution.Controls.VirtualTree.Column
End Class
