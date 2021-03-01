<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wb_Main_Exception
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_Main_Exception))
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnRestart = New System.Windows.Forms.Button()
        Me.BtnContinue = New System.Windows.Forms.Button()
        Me.PnlPicture = New System.Windows.Forms.Panel()
        Me.tbException = New System.Windows.Forms.TextBox()
        Me.lblText = New System.Windows.Forms.Label()
        Me.BtnMail = New System.Windows.Forms.Button()
        Me.BtnShow = New System.Windows.Forms.Button()
        Me.PnlPicture.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnExit
        '
        resources.ApplyResources(Me.BtnExit, "BtnExit")
        Me.BtnExit.DialogResult = System.Windows.Forms.DialogResult.Abort
        Me.BtnExit.Image = Global.WinBack.My.Resources.Resources.IconDlgCancel_16x16
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'BtnRestart
        '
        resources.ApplyResources(Me.BtnRestart, "BtnRestart")
        Me.BtnRestart.DialogResult = System.Windows.Forms.DialogResult.Retry
        Me.BtnRestart.Image = Global.WinBack.My.Resources.Resources.IconDlgRestart_16x16
        Me.BtnRestart.Name = "BtnRestart"
        Me.BtnRestart.UseVisualStyleBackColor = True
        '
        'BtnContinue
        '
        resources.ApplyResources(Me.BtnContinue, "BtnContinue")
        Me.BtnContinue.DialogResult = System.Windows.Forms.DialogResult.Ignore
        Me.BtnContinue.Image = Global.WinBack.My.Resources.Resources.HakenGrn_16x16
        Me.BtnContinue.Name = "BtnContinue"
        Me.BtnContinue.UseVisualStyleBackColor = True
        '
        'PnlPicture
        '
        Me.PnlPicture.BackColor = System.Drawing.Color.Transparent
        Me.PnlPicture.BackgroundImage = Global.WinBack.My.Resources.Resources.Exception
        resources.ApplyResources(Me.PnlPicture, "PnlPicture")
        Me.PnlPicture.Controls.Add(Me.tbException)
        Me.PnlPicture.Name = "PnlPicture"
        '
        'tbException
        '
        resources.ApplyResources(Me.tbException, "tbException")
        Me.tbException.Name = "tbException"
        Me.tbException.ReadOnly = True
        Me.tbException.TabStop = False
        '
        'lblText
        '
        resources.ApplyResources(Me.lblText, "lblText")
        Me.lblText.Name = "lblText"
        '
        'BtnMail
        '
        resources.ApplyResources(Me.BtnMail, "BtnMail")
        Me.BtnMail.Image = Global.WinBack.My.Resources.Resources.IconEMail_16x16
        Me.BtnMail.Name = "BtnMail"
        Me.BtnMail.UseVisualStyleBackColor = True
        '
        'BtnShow
        '
        resources.ApplyResources(Me.BtnShow, "BtnShow")
        Me.BtnShow.Image = Global.WinBack.My.Resources.Resources.IconDlgLog_16x16
        Me.BtnShow.Name = "BtnShow"
        Me.BtnShow.UseVisualStyleBackColor = True
        '
        'wb_Main_Exception
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnShow)
        Me.Controls.Add(Me.BtnMail)
        Me.Controls.Add(Me.lblText)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnRestart)
        Me.Controls.Add(Me.BtnContinue)
        Me.Controls.Add(Me.PnlPicture)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wb_Main_Exception"
        Me.PnlPicture.ResumeLayout(False)
        Me.PnlPicture.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnExit As Windows.Forms.Button
    Friend WithEvents BtnContinue As Windows.Forms.Button
    Friend WithEvents BtnRestart As Windows.Forms.Button
    Friend WithEvents PnlPicture As Windows.Forms.Panel
    Friend WithEvents lblText As Windows.Forms.Label
    Friend WithEvents BtnMail As Windows.Forms.Button
    Friend WithEvents BtnShow As Windows.Forms.Button
    Friend WithEvents tbException As Windows.Forms.TextBox
End Class
