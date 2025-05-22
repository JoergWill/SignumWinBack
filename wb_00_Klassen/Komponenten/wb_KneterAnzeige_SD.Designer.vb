<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_KneterAnzeige_SD
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
        Me.lblTitel = New System.Windows.Forms.Label()
        Me.lblRezept = New System.Windows.Forms.Label()
        Me.lblRezeptNummer = New System.Windows.Forms.Label()
        Me.lblSollwert = New System.Windows.Forms.Label()
        Me.lblIstwert = New System.Windows.Forms.Label()
        Me.lblTeigruhe = New System.Windows.Forms.Label()
        Me.lblFertig = New System.Windows.Forms.Label()
        Me.lblFertigUhrzeit = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pgKnetenMischen = New WinBack.wb_ProgressBar()
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
        'lblRezept
        '
        Me.lblRezept.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRezept.Location = New System.Drawing.Point(307, 3)
        Me.lblRezept.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblRezept.Name = "lblRezept"
        Me.lblRezept.Size = New System.Drawing.Size(776, 29)
        Me.lblRezept.TabIndex = 1
        Me.lblRezept.Text = "Rezept"
        '
        'lblRezeptNummer
        '
        Me.lblRezeptNummer.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRezeptNummer.Location = New System.Drawing.Point(159, 3)
        Me.lblRezeptNummer.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblRezeptNummer.Name = "lblRezeptNummer"
        Me.lblRezeptNummer.Size = New System.Drawing.Size(144, 29)
        Me.lblRezeptNummer.TabIndex = 10
        Me.lblRezeptNummer.Text = "RzNummer"
        '
        'lblSollwert
        '
        Me.lblSollwert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSollwert.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSollwert.Location = New System.Drawing.Point(402, 43)
        Me.lblSollwert.Name = "lblSollwert"
        Me.lblSollwert.Size = New System.Drawing.Size(137, 28)
        Me.lblSollwert.TabIndex = 12
        Me.lblSollwert.Text = "Sollwert"
        Me.lblSollwert.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblIstwert
        '
        Me.lblIstwert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIstwert.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIstwert.Location = New System.Drawing.Point(538, 43)
        Me.lblIstwert.Name = "lblIstwert"
        Me.lblIstwert.Size = New System.Drawing.Size(137, 28)
        Me.lblIstwert.TabIndex = 13
        Me.lblIstwert.Text = "Istwert"
        Me.lblIstwert.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTeigruhe
        '
        Me.lblTeigruhe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTeigruhe.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTeigruhe.Location = New System.Drawing.Point(674, 43)
        Me.lblTeigruhe.Name = "lblTeigruhe"
        Me.lblTeigruhe.Size = New System.Drawing.Size(137, 28)
        Me.lblTeigruhe.TabIndex = 14
        Me.lblTeigruhe.Text = "Teigruhe"
        Me.lblTeigruhe.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblFertig
        '
        Me.lblFertig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFertig.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFertig.Location = New System.Drawing.Point(810, 43)
        Me.lblFertig.Name = "lblFertig"
        Me.lblFertig.Size = New System.Drawing.Size(137, 28)
        Me.lblFertig.TabIndex = 16
        Me.lblFertig.Text = "FertigIn"
        Me.lblFertig.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblFertigUhrzeit
        '
        Me.lblFertigUhrzeit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFertigUhrzeit.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFertigUhrzeit.Location = New System.Drawing.Point(946, 43)
        Me.lblFertigUhrzeit.Name = "lblFertigUhrzeit"
        Me.lblFertigUhrzeit.Size = New System.Drawing.Size(137, 28)
        Me.lblFertigUhrzeit.TabIndex = 17
        Me.lblFertigUhrzeit.Text = "FertigUm"
        Me.lblFertigUhrzeit.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(399, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 16)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Sollwert:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(535, 27)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 16)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Istwert:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(671, 27)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 16)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Teigruhe:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(807, 27)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 16)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Fertig in:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(943, 27)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 16)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Fertig um:"
        '
        'pgKnetenMischen
        '
        Me.pgKnetenMischen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pgKnetenMischen.CustomColor = System.Drawing.Color.Lime
        Me.pgKnetenMischen.CustomFont = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pgKnetenMischen.CustomText = "                            "
        Me.pgKnetenMischen.CustomValue = 0
        Me.pgKnetenMischen.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pgKnetenMischen.Location = New System.Drawing.Point(5, 43)
        Me.pgKnetenMischen.Name = "pgKnetenMischen"
        Me.pgKnetenMischen.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.pgKnetenMischen.Size = New System.Drawing.Size(398, 28)
        Me.pgKnetenMischen.TabIndex = 9
        Me.pgKnetenMischen.Text = "Kneten/Mischen"
        Me.pgKnetenMischen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'wb_KneterAnzeige
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblFertigUhrzeit)
        Me.Controls.Add(Me.lblFertig)
        Me.Controls.Add(Me.lblTeigruhe)
        Me.Controls.Add(Me.lblIstwert)
        Me.Controls.Add(Me.lblSollwert)
        Me.Controls.Add(Me.lblRezeptNummer)
        Me.Controls.Add(Me.pgKnetenMischen)
        Me.Controls.Add(Me.lblRezept)
        Me.Controls.Add(Me.lblTitel)
        Me.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Name = "wb_KneterAnzeige"
        Me.Size = New System.Drawing.Size(1123, 79)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitel As System.Windows.Forms.Label
    Friend WithEvents lblRezept As System.Windows.Forms.Label
    Friend WithEvents pgKnetenMischen As wb_ProgressBar
    Friend WithEvents lblRezeptNummer As System.Windows.Forms.Label
    Friend WithEvents lblSollwert As System.Windows.Forms.Label
    Friend WithEvents lblIstwert As System.Windows.Forms.Label
    Friend WithEvents lblTeigruhe As System.Windows.Forms.Label
    Friend WithEvents lblFertig As System.Windows.Forms.Label
    Friend WithEvents lblFertigUhrzeit As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
