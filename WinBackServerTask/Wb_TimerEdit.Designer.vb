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
        Me.nmEventZyklus = New System.Windows.Forms.NumericUpDown()
        Me.cbEventZyklus = New System.Windows.Forms.ComboBox()
        Me.cbEventAktiv = New System.Windows.Forms.CheckBox()
        Me.lblTimerHinweis = New System.Windows.Forms.Label()
        Me.lblStartZeit = New System.Windows.Forms.Label()
        Me.lblZyklus = New System.Windows.Forms.Label()
        Me.lblIndex = New System.Windows.Forms.Label()
        Me.nmAktIndex = New System.Windows.Forms.NumericUpDown()
        Me.cbAktIndex = New System.Windows.Forms.CheckBox()
        CType(Me.nmEventZyklus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmAktIndex, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.BtnClose.Location = New System.Drawing.Point(640, 112)
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
        Me.dtEventDate.Location = New System.Drawing.Point(15, 111)
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
        Me.dtEventTime.Location = New System.Drawing.Point(132, 111)
        Me.dtEventTime.Name = "dtEventTime"
        Me.dtEventTime.ShowUpDown = True
        Me.dtEventTime.Size = New System.Drawing.Size(81, 25)
        Me.dtEventTime.TabIndex = 3
        '
        'dtEventZyklus
        '
        Me.dtEventZyklus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtEventZyklus.CustomFormat = "mm:ss"
        Me.dtEventZyklus.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtEventZyklus.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtEventZyklus.Location = New System.Drawing.Point(296, 110)
        Me.dtEventZyklus.Name = "dtEventZyklus"
        Me.dtEventZyklus.ShowUpDown = True
        Me.dtEventZyklus.Size = New System.Drawing.Size(70, 25)
        Me.dtEventZyklus.TabIndex = 4
        Me.dtEventZyklus.Value = New Date(2018, 7, 9, 2, 10, 0, 0)
        '
        'nmEventZyklus
        '
        Me.nmEventZyklus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.nmEventZyklus.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmEventZyklus.Location = New System.Drawing.Point(296, 78)
        Me.nmEventZyklus.Name = "nmEventZyklus"
        Me.nmEventZyklus.Size = New System.Drawing.Size(69, 25)
        Me.nmEventZyklus.TabIndex = 5
        '
        'cbEventZyklus
        '
        Me.cbEventZyklus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbEventZyklus.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEventZyklus.FormattingEnabled = True
        Me.cbEventZyklus.Items.AddRange(New Object() {"Min:Sek", "Stunde(n)", "Tag(e)", "Monat(e)"})
        Me.cbEventZyklus.Location = New System.Drawing.Point(384, 110)
        Me.cbEventZyklus.Name = "cbEventZyklus"
        Me.cbEventZyklus.Size = New System.Drawing.Size(101, 25)
        Me.cbEventZyklus.TabIndex = 6
        Me.cbEventZyklus.TabStop = False
        '
        'cbEventAktiv
        '
        Me.cbEventAktiv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbEventAktiv.AutoSize = True
        Me.cbEventAktiv.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEventAktiv.Location = New System.Drawing.Point(506, 112)
        Me.cbEventAktiv.Name = "cbEventAktiv"
        Me.cbEventAktiv.Size = New System.Drawing.Size(56, 21)
        Me.cbEventAktiv.TabIndex = 7
        Me.cbEventAktiv.Text = "aktiv"
        Me.cbEventAktiv.UseVisualStyleBackColor = True
        '
        'lblTimerHinweis
        '
        Me.lblTimerHinweis.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimerHinweis.Location = New System.Drawing.Point(12, 30)
        Me.lblTimerHinweis.Name = "lblTimerHinweis"
        Me.lblTimerHinweis.Size = New System.Drawing.Size(354, 31)
        Me.lblTimerHinweis.TabIndex = 8
        Me.lblTimerHinweis.Text = "Verwaltung der Timer-Einstellungen"
        '
        'lblStartZeit
        '
        Me.lblStartZeit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStartZeit.AutoSize = True
        Me.lblStartZeit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartZeit.Location = New System.Drawing.Point(12, 93)
        Me.lblStartZeit.Name = "lblStartZeit"
        Me.lblStartZeit.Size = New System.Drawing.Size(50, 15)
        Me.lblStartZeit.TabIndex = 9
        Me.lblStartZeit.Text = "Startzeit"
        '
        'lblZyklus
        '
        Me.lblZyklus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblZyklus.AutoSize = True
        Me.lblZyklus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZyklus.Location = New System.Drawing.Point(293, 93)
        Me.lblZyklus.Name = "lblZyklus"
        Me.lblZyklus.Size = New System.Drawing.Size(42, 15)
        Me.lblZyklus.TabIndex = 10
        Me.lblZyklus.Text = "Zyklus"
        '
        'lblIndex
        '
        Me.lblIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblIndex.AutoSize = True
        Me.lblIndex.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIndex.Location = New System.Drawing.Point(381, 59)
        Me.lblIndex.Name = "lblIndex"
        Me.lblIndex.Size = New System.Drawing.Size(86, 15)
        Me.lblIndex.TabIndex = 11
        Me.lblIndex.Text = "Aktueller Index"
        '
        'nmAktIndex
        '
        Me.nmAktIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.nmAktIndex.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmAktIndex.Location = New System.Drawing.Point(384, 77)
        Me.nmAktIndex.Maximum = New Decimal(New Integer() {9999999, 0, 0, 0})
        Me.nmAktIndex.Minimum = New Decimal(New Integer() {2, 0, 0, -2147483648})
        Me.nmAktIndex.Name = "nmAktIndex"
        Me.nmAktIndex.Size = New System.Drawing.Size(101, 25)
        Me.nmAktIndex.TabIndex = 12
        '
        'cbAktIndex
        '
        Me.cbAktIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbAktIndex.AutoSize = True
        Me.cbAktIndex.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAktIndex.Location = New System.Drawing.Point(506, 78)
        Me.cbAktIndex.Name = "cbAktIndex"
        Me.cbAktIndex.Size = New System.Drawing.Size(219, 21)
        Me.cbAktIndex.TabIndex = 13
        Me.cbAktIndex.Text = "Update alle Artikel in OrgaSoft"
        Me.cbAktIndex.UseVisualStyleBackColor = True
        '
        'wb_TimerEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 147)
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
        Me.Name = "wb_TimerEdit"
        Me.Text = "Timer"
        CType(Me.nmEventZyklus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmAktIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTimerName As Label
    Friend WithEvents BtnClose As Button
    Friend WithEvents dtEventDate As DateTimePicker
    Friend WithEvents dtEventTime As DateTimePicker
    Friend WithEvents dtEventZyklus As DateTimePicker
    Friend WithEvents nmEventZyklus As NumericUpDown
    Friend WithEvents cbEventZyklus As ComboBox
    Friend WithEvents cbEventAktiv As CheckBox
    Friend WithEvents lblTimerHinweis As Label
    Friend WithEvents lblStartZeit As Label
    Friend WithEvents lblZyklus As Label
    Friend WithEvents lblIndex As Label
    Friend WithEvents nmAktIndex As NumericUpDown
    Friend WithEvents cbAktIndex As CheckBox
End Class
