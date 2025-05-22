Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_User_Details
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.tPersonalNr = New System.Windows.Forms.TextBox()
        Me.tUserPass = New System.Windows.Forms.TextBox()
        Me.tUserName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblPersonalNummer = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tUserRFID = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Btn_RemoveID = New System.Windows.Forms.Button()
        Me.cbxReaderList = New WinBack.wb_ComboBox()
        Me.cbUserGrp = New WinBack.wb_ComboBox()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(9, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Gruppe:"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblName.Location = New System.Drawing.Point(9, 5)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(83, 13)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Benutzer Name:"
        '
        'tPersonalNr
        '
        Me.tPersonalNr.Location = New System.Drawing.Point(290, 21)
        Me.tPersonalNr.Name = "tPersonalNr"
        Me.tPersonalNr.ReadOnly = True
        Me.tPersonalNr.Size = New System.Drawing.Size(180, 20)
        Me.tPersonalNr.TabIndex = 5
        '
        'tUserPass
        '
        Me.tUserPass.Location = New System.Drawing.Point(290, 66)
        Me.tUserPass.Name = "tUserPass"
        Me.tUserPass.Size = New System.Drawing.Size(103, 20)
        Me.tUserPass.TabIndex = 3
        '
        'tUserName
        '
        Me.tUserName.Location = New System.Drawing.Point(12, 21)
        Me.tUserName.Name = "tUserName"
        Me.tUserName.Size = New System.Drawing.Size(272, 20)
        Me.tUserName.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(287, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Passwort"
        '
        'lblPersonalNummer
        '
        Me.lblPersonalNummer.AutoSize = True
        Me.lblPersonalNummer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPersonalNummer.Location = New System.Drawing.Point(287, 5)
        Me.lblPersonalNummer.Name = "lblPersonalNummer"
        Me.lblPersonalNummer.Size = New System.Drawing.Size(90, 13)
        Me.lblPersonalNummer.TabIndex = 7
        Me.lblPersonalNummer.Text = "Personal-Nummer"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(287, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Chip-ID"
        '
        'tUserRFID
        '
        Me.tUserRFID.Location = New System.Drawing.Point(290, 117)
        Me.tUserRFID.Name = "tUserRFID"
        Me.tUserRFID.ReadOnly = True
        Me.tUserRFID.Size = New System.Drawing.Size(103, 20)
        Me.tUserRFID.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(9, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Kartenleser:"
        '
        'Btn_RemoveID
        '
        Me.Btn_RemoveID.Location = New System.Drawing.Point(399, 114)
        Me.Btn_RemoveID.Name = "Btn_RemoveID"
        Me.Btn_RemoveID.Size = New System.Drawing.Size(71, 23)
        Me.Btn_RemoveID.TabIndex = 12
        Me.Btn_RemoveID.Text = "Löschen"
        Me.Btn_RemoveID.UseVisualStyleBackColor = True
        '
        'cbxReaderList
        '
        Me.cbxReaderList.FormattingEnabled = True
        Me.cbxReaderList.Location = New System.Drawing.Point(12, 117)
        Me.cbxReaderList.Name = "cbxReaderList"
        Me.cbxReaderList.Size = New System.Drawing.Size(272, 21)
        Me.cbxReaderList.TabIndex = 11
        Me.cbxReaderList.TabStop = False
        '
        'cbUserGrp
        '
        Me.cbUserGrp.FormattingEnabled = True
        Me.cbUserGrp.Location = New System.Drawing.Point(12, 66)
        Me.cbUserGrp.Name = "cbUserGrp"
        Me.cbUserGrp.Size = New System.Drawing.Size(272, 21)
        Me.cbUserGrp.TabIndex = 4
        Me.cbUserGrp.TabStop = False
        '
        'wb_User_Details
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 164)
        Me.Controls.Add(Me.Btn_RemoveID)
        Me.Controls.Add(Me.cbxReaderList)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tUserRFID)
        Me.Controls.Add(Me.lblPersonalNummer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tPersonalNr)
        Me.Controls.Add(Me.cbUserGrp)
        Me.Controls.Add(Me.tUserPass)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.tUserName)
        Me.Controls.Add(Me.Label2)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "wb_User_Details"
        Me.Text = "Mitarbeiter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents tUserName As System.Windows.Forms.TextBox
    Friend WithEvents tUserPass As System.Windows.Forms.TextBox
    Friend WithEvents cbUserGrp As wb_ComboBox
    Friend WithEvents tPersonalNr As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblPersonalNummer As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tUserRFID As System.Windows.Forms.TextBox
    Friend WithEvents cbxReaderList As wb_ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Btn_RemoveID As System.Windows.Forms.Button
End Class
