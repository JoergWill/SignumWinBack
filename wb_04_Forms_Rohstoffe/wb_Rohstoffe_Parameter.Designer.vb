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
        Me.ColType = New Infralution.Controls.VirtualTree.Column()
        Me.ColNr = New Infralution.Controls.VirtualTree.Column()
        Me.ColBezeichnung = New Infralution.Controls.VirtualTree.Column()
        Me.ColWert = New Infralution.Controls.VirtualTree.Column()
        Me.ColEinheit = New Infralution.Controls.VirtualTree.Column()
        Me.VirtualTree = New Infralution.Controls.VirtualTree.VirtualTree()
        Me.ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColType
        '
        Me.ColType.AutoFitWeight = 0!
        Me.ColType.AutoSizePolicy = Infralution.Controls.VirtualTree.ColumnAutoSizePolicy.AutoIncrease
        Me.ColType.Caption = ""
        Me.ColType.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColType.MaxAutoSizeWidth = 30
        Me.ColType.Name = "ColType"
        Me.ColType.Width = 30
        '
        'ColNr
        '
        Me.ColNr.AutoFitWeight = 0!
        Me.ColNr.AutoSizePolicy = Infralution.Controls.VirtualTree.ColumnAutoSizePolicy.AutoIncrease
        Me.ColNr.Caption = ""
        Me.ColNr.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColNr.MaxAutoSizeWidth = 50
        Me.ColNr.MinWidth = 50
        Me.ColNr.Name = "ColNr"
        Me.ColNr.Resizable = False
        Me.ColNr.Sortable = False
        Me.ColNr.Width = 50
        '
        'ColBezeichnung
        '
        Me.ColBezeichnung.AutoSizePolicy = Infralution.Controls.VirtualTree.ColumnAutoSizePolicy.AutoIncrease
        Me.ColBezeichnung.Caption = "Parameter"
        Me.ColBezeichnung.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColBezeichnung.MinWidth = 150
        Me.ColBezeichnung.Name = "ColBezeichnung"
        Me.ColBezeichnung.Width = 293
        '
        'ColWert
        '
        Me.ColWert.AutoFitWeight = 0!
        Me.ColWert.AutoSizePolicy = Infralution.Controls.VirtualTree.ColumnAutoSizePolicy.AutoIncrease
        Me.ColWert.Caption = "Wert"
        Me.ColWert.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColWert.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Far
        Me.ColWert.MaxAutoSizeWidth = 70
        Me.ColWert.MinWidth = 70
        Me.ColWert.Name = "ColWert"
        Me.ColWert.Sortable = False
        Me.ColWert.Width = 70
        '
        'ColEinheit
        '
        Me.ColEinheit.AutoFitWeight = 0!
        Me.ColEinheit.AutoSizePolicy = Infralution.Controls.VirtualTree.ColumnAutoSizePolicy.AutoIncrease
        Me.ColEinheit.Caption = "Einh"
        Me.ColEinheit.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColEinheit.MaxAutoSizeWidth = 50
        Me.ColEinheit.MinWidth = 50
        Me.ColEinheit.Name = "ColEinheit"
        Me.ColEinheit.Width = 50
        '
        'VirtualTree
        '
        Me.VirtualTree.AllowMultiSelect = False
        Me.VirtualTree.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.VirtualTree.AutoFitColumns = True
        Me.VirtualTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VirtualTree.Columns.Add(Me.ColType)
        Me.VirtualTree.Columns.Add(Me.ColNr)
        Me.VirtualTree.Columns.Add(Me.ColBezeichnung)
        Me.VirtualTree.Columns.Add(Me.ColWert)
        Me.VirtualTree.Columns.Add(Me.ColEinheit)
        Me.VirtualTree.EditOnKeyPress = True
        Me.VirtualTree.HeaderHeight = 24
        Me.VirtualTree.HeaderStyle.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.VirtualTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.None
        Me.VirtualTree.Location = New System.Drawing.Point(-1, -1)
        Me.VirtualTree.MainColumn = Me.ColType
        Me.VirtualTree.Name = "VirtualTree"
        Me.VirtualTree.RowBindings.Add(Me.ObjectRowBinding1)
        Me.VirtualTree.RowEvenStyle.BackColor = System.Drawing.Color.PowderBlue
        Me.VirtualTree.RowStyle.BorderColor = System.Drawing.Color.LightGray
        Me.VirtualTree.ShowRootRow = False
        Me.VirtualTree.Size = New System.Drawing.Size(495, 463)
        Me.VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        Me.VirtualTree.TabIndex = 9
        '
        'ObjectRowBinding1
        '
        Me.ObjectRowBinding1.AllowDrag = True
        Me.ObjectRowBinding1.AllowDropAboveRow = True
        Me.ObjectRowBinding1.AllowDropBelowRow = True
        ObjectCellBinding1.Column = Me.ColType
        ObjectCellBinding1.Field = "VirtualTree_TypNr"
        ObjectCellBinding2.Column = Me.ColNr
        ObjectCellBinding2.Field = "VirtualTree_ParamNr"
        ObjectCellBinding2.Style.HorzAlignment = System.Drawing.StringAlignment.Center
        ObjectCellBinding3.Column = Me.ColBezeichnung
        ObjectCellBinding3.Field = "Bezeichnung"
        ObjectCellBinding4.Column = Me.ColWert
        ObjectCellBinding4.Field = "VirtualTree_Wert"
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
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "wb_Rohstoffe_Parameter"
        Me.Text = "Rohstoff Parameter"
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
    Friend WithEvents ObjectRowBinding1 As Infralution.Controls.VirtualTree.ObjectRowBinding
    Friend WithEvents ColType As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColNr As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColBezeichnung As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColWert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheit As Infralution.Controls.VirtualTree.Column
End Class
