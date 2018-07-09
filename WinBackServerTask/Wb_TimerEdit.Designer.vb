<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_TimerEdit
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
        Me.lblTimerName = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.dtEventDate = New System.Windows.Forms.DateTimePicker()
        Me.dtEventTime = New System.Windows.Forms.DateTimePicker()
        Me.dtEventZyklus = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        'lblTimerName
        '
        Me.lblTimerName.AutoSize = True
        Me.lblTimerName.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimerName.Location = New System.Drawing.Point(12, 9)
        Me.lblTimerName.Name = "lblTimerName"
        Me.lblTimerName.Size = New System.Drawing.Size(92, 17)
        Me.lblTimerName.TabIndex = 0
        Me.lblTimerName.Text = "AktionsTimer"
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(640, 83)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(95, 28)
        Me.BtnClose.TabIndex = 1
        Me.BtnClose.Text = "OK"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'dtEventDate
        '
        Me.dtEventDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtEventDate.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtEventDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEventDate.Location = New System.Drawing.Point(15, 82)
        Me.dtEventDate.Name = "dtEventDate"
        Me.dtEventDate.Size = New System.Drawing.Size(111, 24)
        Me.dtEventDate.TabIndex = 2
        '
        'dtEventTime
        '
        Me.dtEventTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtEventTime.CustomFormat = ""
        Me.dtEventTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtEventTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtEventTime.Location = New System.Drawing.Point(132, 82)
        Me.dtEventTime.Name = "dtEventTime"
        Me.dtEventTime.ShowUpDown = True
        Me.dtEventTime.Size = New System.Drawing.Size(81, 25)
        Me.dtEventTime.TabIndex = 3
        '
        'dtEventZyklus
        '
        Me.dtEventZyklus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtEventZyklus.CustomFormat = "dd HH:mm:ss"
        Me.dtEventZyklus.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtEventZyklus.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtEventZyklus.Location = New System.Drawing.Point(296, 81)
        Me.dtEventZyklus.Name = "dtEventZyklus"
        Me.dtEventZyklus.ShowUpDown = True
        Me.dtEventZyklus.Size = New System.Drawing.Size(150, 25)
        Me.dtEventZyklus.TabIndex = 4
        Me.dtEventZyklus.Value = New Date(2018, 7, 9, 2, 10, 0, 0)
        '
        'wb_TimerEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 118)
        Me.Controls.Add(Me.dtEventZyklus)
        Me.Controls.Add(Me.dtEventTime)
        Me.Controls.Add(Me.dtEventDate)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.lblTimerName)
        Me.Name = "wb_TimerEdit"
        Me.Text = "Timer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTimerName As Label
    Friend WithEvents BtnClose As Button
    Friend WithEvents dtEventDate As DateTimePicker
    Friend WithEvents dtEventTime As DateTimePicker
    Friend WithEvents dtEventZyklus As DateTimePicker
End Class
