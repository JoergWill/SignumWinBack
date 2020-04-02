Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Chargen_Details
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ObjectCellBinding1 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding2 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding3 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding4 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding5 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding6 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding7 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding8 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding9 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding10 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding11 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding12 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding13 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding14 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Dim ObjectCellBinding15 As Infralution.Controls.VirtualTree.ObjectCellBinding = New Infralution.Controls.VirtualTree.ObjectCellBinding()
        Me.ColNummer = New Infralution.Controls.VirtualTree.Column()
        Me.ColCharge = New Infralution.Controls.VirtualTree.Column()
        Me.ColBezeichnung = New Infralution.Controls.VirtualTree.Column()
        Me.ColLinie = New Infralution.Controls.VirtualTree.Column()
        Me.ColSollwert = New Infralution.Controls.VirtualTree.Column()
        Me.ColEinheit = New Infralution.Controls.VirtualTree.Column()
        Me.ColIstmenge = New Infralution.Controls.VirtualTree.Column()
        Me.ColEinheitIst = New Infralution.Controls.VirtualTree.Column()
        Me.ColZeit = New Infralution.Controls.VirtualTree.Column()
        Me.ColStatusImage = New Infralution.Controls.VirtualTree.Column()
        Me.ColUser = New Infralution.Controls.VirtualTree.Column()
        Me.ColPreis = New Infralution.Controls.VirtualTree.Column()
        Me.ColRohCharge = New Infralution.Controls.VirtualTree.Column()
        Me.ColParams = New Infralution.Controls.VirtualTree.Column()
        Me.ChargenTree = New Infralution.Controls.VirtualTree.VirtualTree()
        Me.ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        CType(Me.ChargenTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColNummer
        '
        Me.ColNummer.AutoFitWeight = 0!
        Me.ColNummer.Caption = "Nummer"
        Me.ColNummer.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColNummer.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Far
        Me.ColNummer.MinWidth = 100
        Me.ColNummer.Movable = False
        Me.ColNummer.Name = "ColNummer"
        Me.ColNummer.Sortable = False
        '
        'ColCharge
        '
        Me.ColCharge.Caption = "Charge"
        Me.ColCharge.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColCharge.MinWidth = 50
        Me.ColCharge.Movable = False
        Me.ColCharge.Name = "ColCharge"
        Me.ColCharge.Sortable = False
        Me.ColCharge.Width = 57
        '
        'ColBezeichnung
        '
        Me.ColBezeichnung.Caption = "Bezeichnung"
        Me.ColBezeichnung.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColBezeichnung.CellStyle.VertAlignment = System.Drawing.StringAlignment.Center
        Me.ColBezeichnung.MinWidth = 100
        Me.ColBezeichnung.Movable = False
        Me.ColBezeichnung.Name = "ColBezeichnung"
        Me.ColBezeichnung.Sortable = False
        Me.ColBezeichnung.Width = 107
        '
        'ColLinie
        '
        Me.ColLinie.AutoFitWeight = 0!
        Me.ColLinie.Caption = "Linie"
        Me.ColLinie.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColLinie.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Center
        Me.ColLinie.MinWidth = 40
        Me.ColLinie.Name = "ColLinie"
        Me.ColLinie.Resizable = False
        Me.ColLinie.Sortable = False
        Me.ColLinie.Width = 40
        '
        'ColSollwert
        '
        Me.ColSollwert.AutoFitWeight = 0!
        Me.ColSollwert.Caption = "Sollwert"
        Me.ColSollwert.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColSollwert.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Far
        Me.ColSollwert.MaxAutoSizeWidth = 120
        Me.ColSollwert.MinWidth = 100
        Me.ColSollwert.Name = "ColSollwert"
        Me.ColSollwert.Resizable = False
        Me.ColSollwert.Sortable = False
        '
        'ColEinheit
        '
        Me.ColEinheit.Caption = Nothing
        Me.ColEinheit.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColEinheit.Name = "ColEinheit"
        Me.ColEinheit.Resizable = False
        Me.ColEinheit.Sortable = False
        Me.ColEinheit.Width = 30
        '
        'ColIstmenge
        '
        Me.ColIstmenge.AutoFitWeight = 0!
        Me.ColIstmenge.Caption = "Istwert"
        Me.ColIstmenge.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Far
        Me.ColIstmenge.MaxAutoSizeWidth = 120
        Me.ColIstmenge.MinWidth = 100
        Me.ColIstmenge.Name = "ColIstmenge"
        '
        'ColEinheitIst
        '
        Me.ColEinheitIst.AutoFitWeight = 0!
        Me.ColEinheitIst.Caption = Nothing
        Me.ColEinheitIst.Name = "ColEinheitIst"
        Me.ColEinheitIst.Width = 30
        '
        'ColZeit
        '
        Me.ColZeit.AutoFitWeight = 0!
        Me.ColZeit.Caption = "Zeit"
        Me.ColZeit.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Center
        Me.ColZeit.MinWidth = 110
        Me.ColZeit.Name = "ColZeit"
        Me.ColZeit.Width = 110
        '
        'ColStatusImage
        '
        Me.ColStatusImage.AutoFitWeight = 0!
        Me.ColStatusImage.Caption = Nothing
        Me.ColStatusImage.CellStyle.BorderColor = System.Drawing.Color.LightGray
        Me.ColStatusImage.CellStyle.BorderWidth = 0
        Me.ColStatusImage.CellStyle.ForeColor = System.Drawing.Color.Black
        Me.ColStatusImage.CellStyle.HorzAlignment = System.Drawing.StringAlignment.Center
        Me.ColStatusImage.CellStyle.VertAlignment = System.Drawing.StringAlignment.Center
        Me.ColStatusImage.MaxAutoSizeWidth = 30
        Me.ColStatusImage.Movable = False
        Me.ColStatusImage.Name = "ColStatusImage"
        Me.ColStatusImage.Resizable = False
        Me.ColStatusImage.Selectable = False
        Me.ColStatusImage.ShowPinIcon = False
        Me.ColStatusImage.Sortable = False
        Me.ColStatusImage.Width = 30
        '
        'ColUser
        '
        Me.ColUser.Caption = "User"
        Me.ColUser.Name = "ColUser"
        Me.ColUser.Width = 37
        '
        'ColPreis
        '
        Me.ColPreis.Caption = "Preis"
        Me.ColPreis.Name = "ColPreis"
        Me.ColPreis.Width = 37
        '
        'ColRohCharge
        '
        Me.ColRohCharge.AutoSizePolicy = Infralution.Controls.VirtualTree.ColumnAutoSizePolicy.AutoSize
        Me.ColRohCharge.Caption = "Lot"
        Me.ColRohCharge.MinWidth = 50
        Me.ColRohCharge.Name = "ColRohCharge"
        Me.ColRohCharge.Width = 50
        '
        'ColParams
        '
        Me.ColParams.AutoFitWeight = 0!
        Me.ColParams.Caption = "Parameter"
        Me.ColParams.MaxAutoSizeWidth = 120
        Me.ColParams.MinWidth = 120
        Me.ColParams.Name = "ColParams"
        Me.ColParams.Width = 120
        '
        'ChargenTree
        '
        Me.ChargenTree.AllowMultiSelect = False
        Me.ChargenTree.AutoFitColumns = True
        Me.ChargenTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ChargenTree.Columns.Add(Me.ColCharge)
        Me.ChargenTree.Columns.Add(Me.ColStatusImage)
        Me.ChargenTree.Columns.Add(Me.ColNummer)
        Me.ChargenTree.Columns.Add(Me.ColBezeichnung)
        Me.ChargenTree.Columns.Add(Me.ColParams)
        Me.ChargenTree.Columns.Add(Me.ColLinie)
        Me.ChargenTree.Columns.Add(Me.ColSollwert)
        Me.ChargenTree.Columns.Add(Me.ColEinheit)
        Me.ChargenTree.Columns.Add(Me.ColIstmenge)
        Me.ChargenTree.Columns.Add(Me.ColEinheitIst)
        Me.ChargenTree.Columns.Add(Me.ColZeit)
        Me.ChargenTree.Columns.Add(Me.ColUser)
        Me.ChargenTree.Columns.Add(Me.ColPreis)
        Me.ChargenTree.Columns.Add(Me.ColRohCharge)
        Me.ChargenTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChargenTree.EditOnKeyPress = True
        Me.ChargenTree.HeaderHeight = 24
        Me.ChargenTree.HeaderStyle.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.ChargenTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.Solid
        Me.ChargenTree.Location = New System.Drawing.Point(0, 0)
        Me.ChargenTree.MainColumn = Me.ColNummer
        Me.ChargenTree.Name = "ChargenTree"
        Me.ChargenTree.RowBindings.Add(Me.ObjectRowBinding1)
        Me.ChargenTree.RowEvenStyle.BackColor = System.Drawing.Color.White
        Me.ChargenTree.RowStyle.BorderColor = System.Drawing.Color.LightGray
        Me.ChargenTree.ShowRootRow = False
        Me.ChargenTree.Size = New System.Drawing.Size(952, 540)
        Me.ChargenTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        Me.ChargenTree.TabIndex = 16
        '
        'ObjectRowBinding1
        '
        ObjectCellBinding1.Column = Me.ColNummer
        ObjectCellBinding1.Field = "VirtTreeNummer"
        ObjectCellBinding1.Style.HorzAlignment = System.Drawing.StringAlignment.Center
        ObjectCellBinding2.Column = Me.ColCharge
        ObjectCellBinding2.Field = "VirtTreeCharge"
        ObjectCellBinding3.Column = Me.ColBezeichnung
        ObjectCellBinding3.Field = "VirtTreeBezeichnung"
        ObjectCellBinding4.Column = Me.ColLinie
        ObjectCellBinding4.Field = "VirtTreeLinie"
        ObjectCellBinding5.Column = Me.ColSollwert
        ObjectCellBinding5.Field = "VirtTreeSollwert"
        ObjectCellBinding6.Column = Me.ColEinheit
        ObjectCellBinding6.Field = "VirtTreeEinheit"
        ObjectCellBinding7.Column = Me.ColNummer
        ObjectCellBinding7.Field = "VirtTreeNummer"
        ObjectCellBinding8.Column = Me.ColIstmenge
        ObjectCellBinding8.Field = "VirtTreeIstwert"
        ObjectCellBinding9.Column = Me.ColEinheitIst
        ObjectCellBinding9.Field = "VirtTreeEinheit"
        ObjectCellBinding9.PreviewSize = New System.Drawing.Size(0, 0)
        ObjectCellBinding10.Column = Me.ColZeit
        ObjectCellBinding10.Field = "VirtTreeZeit"
        ObjectCellBinding11.AutoFitHeight = False
        ObjectCellBinding11.Column = Me.ColStatusImage
        ObjectCellBinding11.DrawPreviewBorder = False
        ObjectCellBinding11.Field = "VirtTreeStatus"
        ObjectCellBinding11.PreviewSize = New System.Drawing.Size(16, 16)
        ObjectCellBinding11.ShowPreview = True
        ObjectCellBinding11.ShowText = False
        ObjectCellBinding11.Style.BorderColor = System.Drawing.Color.LightGray
        ObjectCellBinding11.Style.BorderWidth = 1
        ObjectCellBinding11.Style.ForeColor = System.Drawing.Color.Black
        ObjectCellBinding12.Column = Me.ColUser
        ObjectCellBinding12.Field = "VirtTreeUser"
        ObjectCellBinding12.Style.HorzAlignment = System.Drawing.StringAlignment.Center
        ObjectCellBinding13.Column = Me.ColPreis
        ObjectCellBinding13.Field = "VirtTreePreis"
        ObjectCellBinding14.Column = Me.ColRohCharge
        ObjectCellBinding14.Field = "VirtTreeRohCharge"
        ObjectCellBinding15.Column = Me.ColParams
        ObjectCellBinding15.Field = "VirtTreeParams"
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding1)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding2)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding3)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding4)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding5)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding6)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding7)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding8)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding9)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding10)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding11)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding12)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding13)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding14)
        Me.ObjectRowBinding1.CellBindings.Add(ObjectCellBinding15)
        Me.ObjectRowBinding1.ChildProperty = "ChildSteps"
        Me.ObjectRowBinding1.Name = "ObjectRowBinding1"
        Me.ObjectRowBinding1.ParentProperty = "ParentStep"
        Me.ObjectRowBinding1.TypeName = "WinBack.wb_ChargenSchritt"
        '
        'wb_Chargen_Details
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(952, 540)
        Me.Controls.Add(Me.ChargenTree)
        Me.Name = "wb_Chargen_Details"
        Me.Text = "Chargen"
        CType(Me.ChargenTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ChargenTree As Infralution.Controls.VirtualTree.VirtualTree
    Friend WithEvents ColCharge As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColNummer As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColBezeichnung As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColLinie As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColSollwert As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColIstmenge As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColEinheitIst As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ObjectRowBinding1 As Infralution.Controls.VirtualTree.ObjectRowBinding
    Friend WithEvents ColZeit As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColUser As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColPreis As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColRohCharge As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColStatusImage As Infralution.Controls.VirtualTree.Column
    Friend WithEvents ColParams As Infralution.Controls.VirtualTree.Column
End Class
