
Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Statistik_RohVerbrauch
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
        ListeStatistik = New wb_ListeStatistik()
        BtnDrucken = New System.Windows.Forms.Button()
        BtnBerechnen = New System.Windows.Forms.Button()
        SuspendLayout()
        ' 
        ' ListeStatistik
        ' 
        ListeStatistik.Dock = System.Windows.Forms.DockStyle.Fill
        ListeStatistik.ListeBerechnet = False
        ListeStatistik.Location = New System.Drawing.Point(0, 0)
        ListeStatistik.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        ListeStatistik.Name = "ListeStatistik"
        ListeStatistik.Size = New System.Drawing.Size(648, 688)
        ListeStatistik.TabIndex = 0
        ' 
        ' BtnDrucken
        ' 
        BtnDrucken.Location = New System.Drawing.Point(396, 537)
        BtnDrucken.Name = "BtnDrucken"
        BtnDrucken.Size = New System.Drawing.Size(246, 43)
        BtnDrucken.TabIndex = 20
        BtnDrucken.Text = "Drucken"
        BtnDrucken.UseVisualStyleBackColor = True
        ' 
        ' BtnBerechnen
        ' 
        BtnBerechnen.Location = New System.Drawing.Point(396, 586)
        BtnBerechnen.Name = "BtnBerechnen"
        BtnBerechnen.Size = New System.Drawing.Size(246, 43)
        BtnBerechnen.TabIndex = 19
        BtnBerechnen.Text = "Berechnen"
        BtnBerechnen.UseVisualStyleBackColor = True
        ' 
        ' wb_Statistik_RohVerbrauch
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(648, 688)
        Controls.Add(BtnDrucken)
        Controls.Add(BtnBerechnen)
        Controls.Add(ListeStatistik)
        Name = "wb_Statistik_RohVerbrauch"
        Text = "Statistik Rohstoffe Verbrauch"
        ResumeLayout(False)

    End Sub

    Friend WithEvents ListeStatistik As wb_ListeStatistik
    Friend WithEvents BtnDrucken As System.Windows.Forms.Button
    Friend WithEvents BtnBerechnen As System.Windows.Forms.Button
End Class
