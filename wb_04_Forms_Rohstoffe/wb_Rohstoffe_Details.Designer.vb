Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Rohstoffe_Details
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Rohstoffe_Details))
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tRohstoffName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tRohstoffNummer = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tRohstoffKommentar = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbRohstoffGrp2 = New WinBack.wb_ComboBox()
        Me.cbRohstoffGrp1 = New WinBack.wb_ComboBox()
        Me.tRohstoffPreis = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'tRohstoffName
        '
        resources.ApplyResources(Me.tRohstoffName, "tRohstoffName")
        Me.tRohstoffName.Name = "tRohstoffName"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'tRohstoffNummer
        '
        resources.ApplyResources(Me.tRohstoffNummer, "tRohstoffNummer")
        Me.tRohstoffNummer.Name = "tRohstoffNummer"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'tRohstoffKommentar
        '
        resources.ApplyResources(Me.tRohstoffKommentar, "tRohstoffKommentar")
        Me.tRohstoffKommentar.Name = "tRohstoffKommentar"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'cbRohstoffGrp2
        '
        resources.ApplyResources(Me.cbRohstoffGrp2, "cbRohstoffGrp2")
        Me.cbRohstoffGrp2.FormattingEnabled = True
        Me.cbRohstoffGrp2.Name = "cbRohstoffGrp2"
        '
        'cbRohstoffGrp1
        '
        resources.ApplyResources(Me.cbRohstoffGrp1, "cbRohstoffGrp1")
        Me.cbRohstoffGrp1.FormattingEnabled = True
        Me.cbRohstoffGrp1.Name = "cbRohstoffGrp1"
        '
        'tRohstoffPreis
        '
        resources.ApplyResources(Me.tRohstoffPreis, "tRohstoffPreis")
        Me.tRohstoffPreis.Name = "tRohstoffPreis"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'wb_Rohstoffe_Details
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tRohstoffPreis)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tRohstoffKommentar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tRohstoffNummer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tRohstoffName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbRohstoffGrp2)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cbRohstoffGrp1)
        Me.Name = "wb_Rohstoffe_Details"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents cbRohstoffGrp1 As wb_ComboBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents cbRohstoffGrp2 As wb_ComboBox
    Friend WithEvents tRohstoffName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tRohstoffNummer As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents tRohstoffKommentar As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents tRohstoffPreis As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
End Class
