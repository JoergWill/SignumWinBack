<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_Admin_TimerEdit
    Inherits System.Windows.Forms.Form

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
        Me.BtnRunNow = New System.Windows.Forms.Button()
        Me.cbAktIndex = New System.Windows.Forms.CheckBox()
        Me.nmAktIndex = New System.Windows.Forms.NumericUpDown()
        Me.lblIndex = New System.Windows.Forms.Label()
        Me.lblZyklus = New System.Windows.Forms.Label()
        Me.lblStartZeit = New System.Windows.Forms.Label()
        Me.cbEventAktiv = New System.Windows.Forms.CheckBox()
        Me.cbEventZyklus = New System.Windows.Forms.ComboBox()
        Me.nmEventZyklus = New System.Windows.Forms.NumericUpDown()
        Me.dtEventZyklus = New System.Windows.Forms.DateTimePicker()
        Me.dtEventTime = New System.Windows.Forms.DateTimePicker()
        Me.dtEventDate = New System.Windows.Forms.DateTimePicker()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.lblTimerName = New System.Windows.Forms.Label()
        Me.lblTimerHinweis = New System.Windows.Forms.Label()
        CType(Me.nmAktIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmEventZyklus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnRunNow
        '
        Me.BtnRunNow.Location = New System.Drawing.Point(640, 13)
        Me.BtnRunNow.Name = "BtnRunNow"
        Me.BtnRunNow.Size = New System.Drawing.Size(95, 28)
        Me.BtnRunNow.TabIndex = 29
        Me.BtnRunNow.Text = "Jetzt Starten"
        Me.BtnRunNow.UseVisualStyleBackColor = True
        '
        'cbAktIndex
        '
        Me.cbAktIndex.AutoSize = True
        Me.cbAktIndex.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAktIndex.Location = New System.Drawing.Point(506, 49)
        Me.cbAktIndex.Name = "cbAktIndex"
        Me.cbAktIndex.Size = New System.Drawing.Size(219, 21)
        Me.cbAktIndex.TabIndex = 28
        Me.cbAktIndex.Text = "Update alle Artikel in OrgaSoft"
        Me.cbAktIndex.UseVisualStyleBackColor = True
        '
        'nmAktIndex
        '
        Me.nmAktIndex.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmAktIndex.Location = New System.Drawing.Point(384, 48)
        Me.nmAktIndex.Maximum = New Decimal(New Integer() {9999999, 0, 0, 0})
        Me.nmAktIndex.Minimum = New Decimal(New Integer() {2, 0, 0, -2147483648})
        Me.nmAktIndex.Name = "nmAktIndex"
        Me.nmAktIndex.Size = New System.Drawing.Size(101, 25)
        Me.nmAktIndex.TabIndex = 27
        '
        'lblIndex
        '
        Me.lblIndex.AutoSize = True
        Me.lblIndex.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIndex.Location = New System.Drawing.Point(381, 30)
        Me.lblIndex.Name = "lblIndex"
        Me.lblIndex.Size = New System.Drawing.Size(86, 15)
        Me.lblIndex.TabIndex = 26
        Me.lblIndex.Text = "Aktueller Index"
        '
        'lblZyklus
        '
        Me.lblZyklus.AutoSize = True
        Me.lblZyklus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZyklus.Location = New System.Drawing.Point(293, 64)
        Me.lblZyklus.Name = "lblZyklus"
        Me.lblZyklus.Size = New System.Drawing.Size(42, 15)
        Me.lblZyklus.TabIndex = 25
        Me.lblZyklus.Text = "Zyklus"
        '
        'lblStartZeit
        '
        Me.lblStartZeit.AutoSize = True
        Me.lblStartZeit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartZeit.Location = New System.Drawing.Point(12, 64)
        Me.lblStartZeit.Name = "lblStartZeit"
        Me.lblStartZeit.Size = New System.Drawing.Size(50, 15)
        Me.lblStartZeit.TabIndex = 24
        Me.lblStartZeit.Text = "Startzeit"
        '
        'cbEventAktiv
        '
        Me.cbEventAktiv.AutoSize = True
        Me.cbEventAktiv.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEventAktiv.Location = New System.Drawing.Point(506, 83)
        Me.cbEventAktiv.Name = "cbEventAktiv"
        Me.cbEventAktiv.Size = New System.Drawing.Size(56, 21)
        Me.cbEventAktiv.TabIndex = 22
        Me.cbEventAktiv.Text = "aktiv"
        Me.cbEventAktiv.UseVisualStyleBackColor = True
        '
        'cbEventZyklus
        '
        Me.cbEventZyklus.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEventZyklus.FormattingEnabled = True
        Me.cbEventZyklus.Items.AddRange(New Object() {"Min:Sek", "Stunde(n)", "Tag(e)", "Monat(e)"})
        Me.cbEventZyklus.Location = New System.Drawing.Point(384, 81)
        Me.cbEventZyklus.Name = "cbEventZyklus"
        Me.cbEventZyklus.Size = New System.Drawing.Size(101, 25)
        Me.cbEventZyklus.TabIndex = 21
        Me.cbEventZyklus.TabStop = False
        '
        'nmEventZyklus
        '
        Me.nmEventZyklus.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmEventZyklus.Location = New System.Drawing.Point(296, 49)
        Me.nmEventZyklus.Name = "nmEventZyklus"
        Me.nmEventZyklus.Size = New System.Drawing.Size(69, 25)
        Me.nmEventZyklus.TabIndex = 20
        '
        'dtEventZyklus
        '
        Me.dtEventZyklus.CustomFormat = "mm:ss"
        Me.dtEventZyklus.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtEventZyklus.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtEventZyklus.Location = New System.Drawing.Point(296, 81)
        Me.dtEventZyklus.Name = "dtEventZyklus"
        Me.dtEventZyklus.ShowUpDown = True
        Me.dtEventZyklus.Size = New System.Drawing.Size(70, 25)
        Me.dtEventZyklus.TabIndex = 19
        Me.dtEventZyklus.Value = New Date(2018, 7, 9, 2, 10, 0, 0)
        '
        'dtEventTime
        '
        Me.dtEventTime.CustomFormat = ""
        Me.dtEventTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtEventTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtEventTime.Location = New System.Drawing.Point(132, 82)
        Me.dtEventTime.Name = "dtEventTime"
        Me.dtEventTime.ShowUpDown = True
        Me.dtEventTime.Size = New System.Drawing.Size(81, 25)
        Me.dtEventTime.TabIndex = 18
        '
        'dtEventDate
        '
        Me.dtEventDate.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtEventDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEventDate.Location = New System.Drawing.Point(15, 82)
        Me.dtEventDate.Name = "dtEventDate"
        Me.dtEventDate.Size = New System.Drawing.Size(111, 24)
        Me.dtEventDate.TabIndex = 17
        '
        'BtnClose
        '
        Me.BtnClose.Location = New System.Drawing.Point(640, 83)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(95, 28)
        Me.BtnClose.TabIndex = 16
        Me.BtnClose.Text = "OK"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'lblTimerName
        '
        Me.lblTimerName.AutoSize = True
        Me.lblTimerName.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimerName.Location = New System.Drawing.Point(12, 10)
        Me.lblTimerName.Name = "lblTimerName"
        Me.lblTimerName.Size = New System.Drawing.Size(92, 17)
        Me.lblTimerName.TabIndex = 15
        Me.lblTimerName.Text = "AktionsTimer"
        '
        'lblTimerHinweis
        '
        Me.lblTimerHinweis.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimerHinweis.Location = New System.Drawing.Point(12, 31)
        Me.lblTimerHinweis.Name = "lblTimerHinweis"
        Me.lblTimerHinweis.Size = New System.Drawing.Size(354, 31)
        Me.lblTimerHinweis.TabIndex = 23
        Me.lblTimerHinweis.Text = "Verwaltung der Timer-Einstellungen"
        '
        'wb_Admin_TimerEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(755, 132)
        Me.Controls.Add(Me.BtnRunNow)
        Me.Controls.Add(Me.cbAktIndex)
        Me.Controls.Add(Me.nmAktIndex)
        Me.Controls.Add(Me.lblIndex)
        Me.Controls.Add(Me.lblZyklus)
        Me.Controls.Add(Me.lblStartZeit)
        Me.Controls.Add(Me.cbEventAktiv)
        Me.Controls.Add(Me.cbEventZyklus)
        Me.Controls.Add(Me.nmEventZyklus)
        Me.Controls.Add(Me.dtEventZyklus)
        Me.Controls.Add(Me.dtEventTime)
        Me.Controls.Add(Me.dtEventDate)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.lblTimerName)
        Me.Controls.Add(Me.lblTimerHinweis)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_Admin_TimerEdit"
        Me.Text = "WinBack Scheduler"
        CType(Me.nmAktIndex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmEventZyklus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnRunNow As System.Windows.Forms.Button
    Friend WithEvents cbAktIndex As System.Windows.Forms.CheckBox
    Friend WithEvents nmAktIndex As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblIndex As System.Windows.Forms.Label
    Friend WithEvents lblZyklus As System.Windows.Forms.Label
    Friend WithEvents lblStartZeit As System.Windows.Forms.Label
    Friend WithEvents cbEventAktiv As System.Windows.Forms.CheckBox
    Friend WithEvents cbEventZyklus As System.Windows.Forms.ComboBox
    Friend WithEvents nmEventZyklus As System.Windows.Forms.NumericUpDown
    Friend WithEvents dtEventZyklus As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtEventTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtEventDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents lblTimerName As System.Windows.Forms.Label
    Friend WithEvents lblTimerHinweis As System.Windows.Forms.Label
End Class
