Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_Admin_Sync
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
        Me.btnSyncUserGruppen = New System.Windows.Forms.Button()
        Me.tbSyncResult = New System.Windows.Forms.Panel()
        Me.btnSyncUser = New System.Windows.Forms.Button()
        Me.btnSyncStart = New System.Windows.Forms.Button()
        Me.btnExportPrint = New System.Windows.Forms.Button()
        Me.lvSyncResult = New System.Windows.Forms.ListView()
        Me.clHeader0 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnSyncArtikel = New System.Windows.Forms.Button()
        Me.btnSyncRohstoffe = New System.Windows.Forms.Button()
        Me.btnTryToMatch = New System.Windows.Forms.Button()
        Me.btnRemoveDBL = New System.Windows.Forms.Button()
        Me.btnRemoveNotUsed = New System.Windows.Forms.Button()
        Me.btnInitLieferungen = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnSyncUserGruppen
        '
        Me.btnSyncUserGruppen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSyncUserGruppen.Location = New System.Drawing.Point(12, 12)
        Me.btnSyncUserGruppen.Name = "btnSyncUserGruppen"
        Me.btnSyncUserGruppen.Size = New System.Drawing.Size(135, 43)
        Me.btnSyncUserGruppen.TabIndex = 0
        Me.btnSyncUserGruppen.Text = "Mitarbeiter-Gruppen"
        Me.btnSyncUserGruppen.UseVisualStyleBackColor = True
        '
        'tbSyncResult
        '
        Me.tbSyncResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSyncResult.BackColor = System.Drawing.Color.DarkGray
        Me.tbSyncResult.ForeColor = System.Drawing.SystemColors.InfoText
        Me.tbSyncResult.Location = New System.Drawing.Point(153, 12)
        Me.tbSyncResult.Name = "tbSyncResult"
        Me.tbSyncResult.Size = New System.Drawing.Size(1020, 508)
        Me.tbSyncResult.TabIndex = 1
        '
        'btnSyncUser
        '
        Me.btnSyncUser.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSyncUser.Location = New System.Drawing.Point(12, 61)
        Me.btnSyncUser.Name = "btnSyncUser"
        Me.btnSyncUser.Size = New System.Drawing.Size(135, 43)
        Me.btnSyncUser.TabIndex = 2
        Me.btnSyncUser.Text = "Mitarbeiter"
        Me.btnSyncUser.UseVisualStyleBackColor = True
        '
        'btnSyncStart
        '
        Me.btnSyncStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSyncStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnSyncStart.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSyncStart.Location = New System.Drawing.Point(1058, 532)
        Me.btnSyncStart.Name = "btnSyncStart"
        Me.btnSyncStart.Size = New System.Drawing.Size(115, 43)
        Me.btnSyncStart.TabIndex = 3
        Me.btnSyncStart.Text = "Synchronisation ausführen"
        Me.btnSyncStart.UseVisualStyleBackColor = False
        '
        'btnExportPrint
        '
        Me.btnExportPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportPrint.Location = New System.Drawing.Point(12, 532)
        Me.btnExportPrint.Name = "btnExportPrint"
        Me.btnExportPrint.Size = New System.Drawing.Size(135, 43)
        Me.btnExportPrint.TabIndex = 11
        Me.btnExportPrint.Text = "Ergebnis Drucken/Exportieren"
        Me.btnExportPrint.UseVisualStyleBackColor = True
        '
        'lvSyncResult
        '
        Me.lvSyncResult.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lvSyncResult.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clHeader0, Me.clHeader1, Me.clHeader2, Me.clHeader3, Me.clHeader4, Me.clHeader5, Me.clHeader6})
        Me.lvSyncResult.Location = New System.Drawing.Point(153, 526)
        Me.lvSyncResult.Name = "lvSyncResult"
        Me.lvSyncResult.Size = New System.Drawing.Size(432, 66)
        Me.lvSyncResult.TabIndex = 10
        Me.lvSyncResult.UseCompatibleStateImageBehavior = False
        Me.lvSyncResult.View = System.Windows.Forms.View.Details
        '
        'clHeader0
        '
        Me.clHeader0.Text = ""
        '
        'clHeader1
        '
        Me.clHeader1.Text = "Gesamt"
        Me.clHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'clHeader2
        '
        Me.clHeader2.Text = "Fehler"
        Me.clHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'clHeader3
        '
        Me.clHeader3.Text = "Schreiben"
        Me.clHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'clHeader4
        '
        Me.clHeader4.Text = "Update"
        Me.clHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'clHeader5
        '
        Me.clHeader5.Text = "Miss"
        Me.clHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'clHeader6
        '
        Me.clHeader6.Text = "Doppelt"
        Me.clHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSyncArtikel
        '
        Me.btnSyncArtikel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSyncArtikel.Location = New System.Drawing.Point(12, 189)
        Me.btnSyncArtikel.Name = "btnSyncArtikel"
        Me.btnSyncArtikel.Size = New System.Drawing.Size(135, 43)
        Me.btnSyncArtikel.TabIndex = 13
        Me.btnSyncArtikel.Text = "Artikel"
        Me.btnSyncArtikel.UseVisualStyleBackColor = True
        '
        'btnSyncRohstoffe
        '
        Me.btnSyncRohstoffe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSyncRohstoffe.Location = New System.Drawing.Point(12, 238)
        Me.btnSyncRohstoffe.Name = "btnSyncRohstoffe"
        Me.btnSyncRohstoffe.Size = New System.Drawing.Size(135, 43)
        Me.btnSyncRohstoffe.TabIndex = 14
        Me.btnSyncRohstoffe.Text = "Rohstoffe"
        Me.btnSyncRohstoffe.UseVisualStyleBackColor = True
        '
        'btnTryToMatch
        '
        Me.btnTryToMatch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTryToMatch.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnTryToMatch.Location = New System.Drawing.Point(664, 532)
        Me.btnTryToMatch.Name = "btnTryToMatch"
        Me.btnTryToMatch.Size = New System.Drawing.Size(115, 43)
        Me.btnTryToMatch.TabIndex = 15
        Me.btnTryToMatch.Text = "Match (Versuch)"
        Me.btnTryToMatch.UseVisualStyleBackColor = True
        '
        'btnRemoveDBL
        '
        Me.btnRemoveDBL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveDBL.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRemoveDBL.Location = New System.Drawing.Point(785, 532)
        Me.btnRemoveDBL.Name = "btnRemoveDBL"
        Me.btnRemoveDBL.Size = New System.Drawing.Size(115, 43)
        Me.btnRemoveDBL.TabIndex = 16
        Me.btnRemoveDBL.Text = "Doppelte Einträge löschen"
        Me.btnRemoveDBL.UseVisualStyleBackColor = True
        '
        'btnRemoveNotUsed
        '
        Me.btnRemoveNotUsed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveNotUsed.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRemoveNotUsed.Location = New System.Drawing.Point(906, 532)
        Me.btnRemoveNotUsed.Name = "btnRemoveNotUsed"
        Me.btnRemoveNotUsed.Size = New System.Drawing.Size(115, 43)
        Me.btnRemoveNotUsed.TabIndex = 17
        Me.btnRemoveNotUsed.Text = "Unbenutzte Einträge löschen"
        Me.btnRemoveNotUsed.UseVisualStyleBackColor = True
        '
        'BtnInitLieferungen
        '
        Me.btnInitLieferungen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnInitLieferungen.Location = New System.Drawing.Point(12, 287)
        Me.btnInitLieferungen.Name = "BtnInitLieferungen"
        Me.btnInitLieferungen.Size = New System.Drawing.Size(135, 43)
        Me.btnInitLieferungen.TabIndex = 18
        Me.btnInitLieferungen.Text = "Initialisierung Bestand/Liefermengen "
        Me.btnInitLieferungen.UseVisualStyleBackColor = True
        '
        'wb_Admin_Sync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1185, 600)
        Me.Controls.Add(Me.btnInitLieferungen)
        Me.Controls.Add(Me.btnRemoveNotUsed)
        Me.Controls.Add(Me.btnRemoveDBL)
        Me.Controls.Add(Me.btnTryToMatch)
        Me.Controls.Add(Me.btnSyncRohstoffe)
        Me.Controls.Add(Me.btnSyncArtikel)
        Me.Controls.Add(Me.lvSyncResult)
        Me.Controls.Add(Me.btnExportPrint)
        Me.Controls.Add(Me.btnSyncStart)
        Me.Controls.Add(Me.btnSyncUser)
        Me.Controls.Add(Me.tbSyncResult)
        Me.Controls.Add(Me.btnSyncUserGruppen)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "wb_Admin_Sync"
        Me.Text = "Synchronisation Datenbank"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnSyncUserGruppen As System.Windows.Forms.Button
    Friend WithEvents tbSyncResult As System.Windows.Forms.Panel
    Friend WithEvents btnSyncUser As System.Windows.Forms.Button
    Friend WithEvents btnSyncStart As System.Windows.Forms.Button
    Friend WithEvents btnExportPrint As System.Windows.Forms.Button
    Friend WithEvents lvSyncResult As System.Windows.Forms.ListView
    Friend WithEvents clHeader0 As System.Windows.Forms.ColumnHeader
    Friend WithEvents clHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents clHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents clHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents clHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents clHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnSyncArtikel As System.Windows.Forms.Button
    Friend WithEvents btnSyncRohstoffe As System.Windows.Forms.Button
    Friend WithEvents btnTryToMatch As System.Windows.Forms.Button
    Friend WithEvents clHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnRemoveDBL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveNotUsed As System.Windows.Forms.Button
    Friend WithEvents btnInitLieferungen As System.Windows.Forms.Button
End Class
