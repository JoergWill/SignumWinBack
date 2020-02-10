Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Chargen_Details
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Chargen_Details))
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
        Me.VirtualTree = New Infralution.Controls.VirtualTree.VirtualTree()
        Me.ObjectRowBinding1 = New Infralution.Controls.VirtualTree.ObjectRowBinding()
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColNummer
        '
        resources.ApplyResources(Me.ColNummer, "ColNummer")
        Me.ColNummer.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColNummer.CellStyle.HorzAlignment = CType(resources.GetObject("ColNummer.CellStyle.HorzAlignment"), System.Drawing.StringAlignment)
        Me.ColNummer.Movable = False
        Me.ColNummer.Name = "ColNummer"
        Me.ColNummer.Sortable = False
        '
        'ColCharge
        '
        resources.ApplyResources(Me.ColCharge, "ColCharge")
        Me.ColCharge.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColCharge.Movable = False
        Me.ColCharge.Name = "ColCharge"
        Me.ColCharge.Sortable = False
        '
        'ColBezeichnung
        '
        resources.ApplyResources(Me.ColBezeichnung, "ColBezeichnung")
        Me.ColBezeichnung.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColBezeichnung.CellStyle.VertAlignment = CType(resources.GetObject("ColBezeichnung.CellStyle.VertAlignment"), System.Drawing.StringAlignment)
        Me.ColBezeichnung.Movable = False
        Me.ColBezeichnung.Name = "ColBezeichnung"
        Me.ColBezeichnung.Sortable = False
        '
        'ColLinie
        '
        resources.ApplyResources(Me.ColLinie, "ColLinie")
        Me.ColLinie.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColLinie.CellStyle.HorzAlignment = CType(resources.GetObject("ColLinie.CellStyle.HorzAlignment"), System.Drawing.StringAlignment)
        Me.ColLinie.Name = "ColLinie"
        Me.ColLinie.Resizable = False
        Me.ColLinie.Sortable = False
        '
        'ColSollwert
        '
        resources.ApplyResources(Me.ColSollwert, "ColSollwert")
        Me.ColSollwert.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColSollwert.CellStyle.HorzAlignment = CType(resources.GetObject("ColSollwert.CellStyle.HorzAlignment"), System.Drawing.StringAlignment)
        Me.ColSollwert.Name = "ColSollwert"
        Me.ColSollwert.Resizable = False
        Me.ColSollwert.Sortable = False
        '
        'ColEinheit
        '
        resources.ApplyResources(Me.ColEinheit, "ColEinheit")
        Me.ColEinheit.CellEvenStyle.BackColor = System.Drawing.Color.White
        Me.ColEinheit.Name = "ColEinheit"
        Me.ColEinheit.Resizable = False
        Me.ColEinheit.Sortable = False
        '
        'ColIstmenge
        '
        resources.ApplyResources(Me.ColIstmenge, "ColIstmenge")
        Me.ColIstmenge.CellStyle.HorzAlignment = CType(resources.GetObject("ColIstmenge.CellStyle.HorzAlignment"), System.Drawing.StringAlignment)
        Me.ColIstmenge.Name = "ColIstmenge"
        '
        'ColEinheitIst
        '
        resources.ApplyResources(Me.ColEinheitIst, "ColEinheitIst")
        Me.ColEinheitIst.Name = "ColEinheitIst"
        '
        'ColZeit
        '
        resources.ApplyResources(Me.ColZeit, "ColZeit")
        Me.ColZeit.CellStyle.HorzAlignment = CType(resources.GetObject("ColZeit.CellStyle.HorzAlignment"), System.Drawing.StringAlignment)
        Me.ColZeit.Name = "ColZeit"
        '
        'ColStatusImage
        '
        resources.ApplyResources(Me.ColStatusImage, "ColStatusImage")
        Me.ColStatusImage.CellStyle.BorderColor = System.Drawing.Color.LightGray
        Me.ColStatusImage.CellStyle.BorderWidth = 0
        Me.ColStatusImage.CellStyle.ForeColor = System.Drawing.Color.Black
        Me.ColStatusImage.CellStyle.HorzAlignment = CType(resources.GetObject("ColStatusImage.CellStyle.HorzAlignment"), System.Drawing.StringAlignment)
        Me.ColStatusImage.CellStyle.VertAlignment = CType(resources.GetObject("ColStatusImage.CellStyle.VertAlignment"), System.Drawing.StringAlignment)
        Me.ColStatusImage.Movable = False
        Me.ColStatusImage.Name = "ColStatusImage"
        Me.ColStatusImage.Resizable = False
        Me.ColStatusImage.Selectable = False
        Me.ColStatusImage.Sortable = False
        '
        'ColUser
        '
        resources.ApplyResources(Me.ColUser, "ColUser")
        Me.ColUser.Name = "ColUser"
        '
        'ColPreis
        '
        resources.ApplyResources(Me.ColPreis, "ColPreis")
        Me.ColPreis.Name = "ColPreis"
        '
        'ColRohCharge
        '
        Me.ColRohCharge.AutoSizePolicy = Infralution.Controls.VirtualTree.ColumnAutoSizePolicy.AutoSize
        resources.ApplyResources(Me.ColRohCharge, "ColRohCharge")
        Me.ColRohCharge.Name = "ColRohCharge"
        '
        'ColParams
        '
        resources.ApplyResources(Me.ColParams, "ColParams")
        Me.ColParams.Name = "ColParams"
        '
        'VirtualTree
        '
        Me.VirtualTree.AllowMultiSelect = False
        resources.ApplyResources(Me.VirtualTree, "VirtualTree")
        Me.VirtualTree.AutoFitColumns = True
        Me.VirtualTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VirtualTree.CollapseImage = CType(resources.GetObject("VirtualTree.CollapseImage"), System.Drawing.Image)
        Me.VirtualTree.Columns.Add(Me.ColCharge)
        Me.VirtualTree.Columns.Add(Me.ColStatusImage)
        Me.VirtualTree.Columns.Add(Me.ColNummer)
        Me.VirtualTree.Columns.Add(Me.ColBezeichnung)
        Me.VirtualTree.Columns.Add(Me.ColParams)
        Me.VirtualTree.Columns.Add(Me.ColLinie)
        Me.VirtualTree.Columns.Add(Me.ColSollwert)
        Me.VirtualTree.Columns.Add(Me.ColEinheit)
        Me.VirtualTree.Columns.Add(Me.ColIstmenge)
        Me.VirtualTree.Columns.Add(Me.ColEinheitIst)
        Me.VirtualTree.Columns.Add(Me.ColZeit)
        Me.VirtualTree.Columns.Add(Me.ColUser)
        Me.VirtualTree.Columns.Add(Me.ColPreis)
        Me.VirtualTree.Columns.Add(Me.ColRohCharge)
        Me.VirtualTree.EditOnKeyPress = True
        Me.VirtualTree.ExpandImage = CType(resources.GetObject("VirtualTree.ExpandImage"), System.Drawing.Image)
        Me.VirtualTree.HeaderStyle.Font = CType(resources.GetObject("resource.Font"), System.Drawing.Font)
        Me.VirtualTree.LineStyle = Infralution.Controls.VirtualTree.LineStyle.Solid
        Me.VirtualTree.MainColumn = Me.ColNummer
        Me.VirtualTree.Name = "VirtualTree"
        Me.VirtualTree.RowBindings.Add(Me.ObjectRowBinding1)
        Me.VirtualTree.RowEvenStyle.BackColor = System.Drawing.Color.White
        Me.VirtualTree.RowStyle.BorderColor = System.Drawing.Color.LightGray
        Me.VirtualTree.ShowRootRow = False
        Me.VirtualTree.StyleTemplate = Infralution.Controls.VirtualTree.StyleTemplate.Vista
        '
        'ObjectRowBinding1
        '
        ObjectCellBinding1.Column = Me.ColNummer
        ObjectCellBinding1.Field = "VirtTreeNummer"
        ObjectCellBinding1.Style.HorzAlignment = CType(resources.GetObject("resource.HorzAlignment"), System.Drawing.StringAlignment)
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
        resources.ApplyResources(ObjectCellBinding9, "ObjectCellBinding9")
        ObjectCellBinding10.Column = Me.ColZeit
        ObjectCellBinding10.Field = "VirtTreeZeit"
        resources.ApplyResources(ObjectCellBinding11, "ObjectCellBinding11")
        ObjectCellBinding11.Column = Me.ColStatusImage
        ObjectCellBinding11.DrawPreviewBorder = False
        ObjectCellBinding11.Field = "VirtTreeStatus"
        ObjectCellBinding11.Style.BorderColor = System.Drawing.Color.LightGray
        ObjectCellBinding11.Style.BorderWidth = 1
        ObjectCellBinding11.Style.ForeColor = System.Drawing.Color.Black
        ObjectCellBinding12.Column = Me.ColUser
        ObjectCellBinding12.Field = "VirtTreeUser"
        ObjectCellBinding12.Style.HorzAlignment = CType(resources.GetObject("resource.HorzAlignment1"), System.Drawing.StringAlignment)
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
        resources.ApplyResources(Me.ObjectRowBinding1, "ObjectRowBinding1")
        Me.ObjectRowBinding1.Name = "ObjectRowBinding1"
        Me.ObjectRowBinding1.ParentProperty = "ParentStep"
        Me.ObjectRowBinding1.TypeName = "WinBack.wb_ChargenSchritt"
        '
        'wb_Chargen_Details
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.VirtualTree)
        Me.Name = "wb_Chargen_Details"
        CType(Me.VirtualTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
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
    Private WithEvents VirtualTree As Infralution.Controls.VirtualTree.VirtualTree
End Class
