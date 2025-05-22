<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_DashElement
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.lblTitel = New System.Windows.Forms.Label()
        Me.lblWert = New System.Windows.Forms.Label()
        Me.StartupTimer = New System.Windows.Forms.Timer(Me.components)
        Me.pBox = New System.Windows.Forms.PictureBox()
        CType(Me.pBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitel
        '
        Me.lblTitel.AutoSize = True
        Me.lblTitel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitel.Location = New System.Drawing.Point(2, 3)
        Me.lblTitel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTitel.Name = "lblTitel"
        Me.lblTitel.Size = New System.Drawing.Size(32, 16)
        Me.lblTitel.TabIndex = 0
        Me.lblTitel.Text = "Titel"
        '
        'lblWert
        '
        Me.lblWert.AutoSize = True
        Me.lblWert.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWert.Location = New System.Drawing.Point(126, 52)
        Me.lblWert.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblWert.Name = "lblWert"
        Me.lblWert.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.lblWert.Size = New System.Drawing.Size(40, 22)
        Me.lblWert.TabIndex = 1
        Me.lblWert.Text = "9999"
        '
        'StartupTimer
        '
        Me.StartupTimer.Interval = 1000
        '
        'pBox
        '
        Me.pBox.Location = New System.Drawing.Point(64, 64)
        Me.pBox.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pBox.Name = "pBox"
        Me.pBox.Size = New System.Drawing.Size(64, 64)
        Me.pBox.TabIndex = 2
        Me.pBox.TabStop = False
        '
        'wb_DashElement
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.lblWert)
        Me.Controls.Add(Me.pBox)
        Me.Controls.Add(Me.lblTitel)
        Me.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "wb_DashElement"
        Me.Size = New System.Drawing.Size(200, 200)
        CType(Me.pBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitel As System.Windows.Forms.Label
    Friend WithEvents lblWert As System.Windows.Forms.Label
    Friend WithEvents StartupTimer As System.Windows.Forms.Timer
    Private WithEvents pBox As System.Windows.Forms.PictureBox
End Class
