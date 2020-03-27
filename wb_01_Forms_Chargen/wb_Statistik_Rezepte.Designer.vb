Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Statistik_Rezepte
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
        Me.BtnBerechnen = New System.Windows.Forms.Button()
        Me.ListeStatistik = New WinBack.wb_ListeStatistik()
        Me.BtnDrucken = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnBerechnen
        '
        Me.BtnBerechnen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnBerechnen.Location = New System.Drawing.Point(339, 583)
        Me.BtnBerechnen.Name = "BtnBerechnen"
        Me.BtnBerechnen.Size = New System.Drawing.Size(212, 43)
        Me.BtnBerechnen.TabIndex = 15
        Me.BtnBerechnen.Text = "Berechnen"
        Me.BtnBerechnen.UseVisualStyleBackColor = True
        '
        'ListeStatistik
        '
        Me.ListeStatistik.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListeStatistik.Location = New System.Drawing.Point(0, 0)
        Me.ListeStatistik.Name = "ListeStatistik"
        Me.ListeStatistik.Size = New System.Drawing.Size(577, 678)
        Me.ListeStatistik.TabIndex = 17
        '
        'BtnDrucken
        '
        Me.BtnDrucken.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnDrucken.Location = New System.Drawing.Point(339, 534)
        Me.BtnDrucken.Name = "BtnDrucken"
        Me.BtnDrucken.Size = New System.Drawing.Size(212, 43)
        Me.BtnDrucken.TabIndex = 18
        Me.BtnDrucken.Text = "Drucken"
        Me.BtnDrucken.UseVisualStyleBackColor = True
        '
        'wb_Statistik_Rezepte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 678)
        Me.Controls.Add(Me.BtnDrucken)
        Me.Controls.Add(Me.BtnBerechnen)
        Me.Controls.Add(Me.ListeStatistik)
        Me.Name = "wb_Statistik_Rezepte"
        Me.Text = "Statistik Rezepte"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnBerechnen As Windows.Forms.Button
    Friend WithEvents ListeStatistik As wb_ListeStatistik
    Friend WithEvents BtnDrucken As Windows.Forms.Button
End Class
