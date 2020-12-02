Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Schnittstelle_Shared

Public Class wb_Schnittstelle_Konfig
    Inherits DockContent

    Private KonfigFiles As String()
    Private Schnittstelle As wb_Schnittstelle
    Private SchnittstelleFelderGrid As wb_ArrayGridViewSchnittstelleFelder


    Private Sub wb_Schnittstelle_Konfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Liste der verfügbaren Schnittstellen
        cbFormatSchnittstelle.Items.Clear()

        'alle Definitions-Dateien auflisten
        Try
            'Suche alle Konfigurations-Files (Format XWinBacknnnnnn.xml
            KonfigFiles = System.IO.Directory.GetFiles(wb_GlobalSettings.pXConfigPath)

            'Namen der Schnittstellen entspricht dem Filenamen ohne Pfad und Extension
            For Each s As String In KonfigFiles
                cbFormatSchnittstelle.Items.Add(Path.GetFileNameWithoutExtension(s))
            Next
        Catch ex As Exception
        End Try

        'Eingabefelder erst mal deaktivieren
        grpDefault.Enabled = False
        grpVerzeichnisse.Enabled = False
        grpTabelle.Enabled = False
        grpTabelleFelder.Enabled = False

        'Eingabefelder Tabellen
        tbTabelleName.Enabled = False

        'Default-Schnittstelle Einstellungen
        tbDefaultImport.Text = wb_GlobalSettings.DefaultImportSchnittstelle
        tbDefaultExport.Text = wb_GlobalSettings.DefaultExportSchnittstelle

        'Wenn eine Default-Schnittstelle definiert ist, dann wird diese Konfiguration als erstes geladen
        If wb_GlobalSettings.DefaultImportSchnittstelle <> "" Then
            cbFormatSchnittstelle.SelectedItem = wb_GlobalSettings.DefaultImportSchnittstelle
        ElseIf wb_GlobalSettings.DefaultExportSchnittstelle <> "" Then
            cbFormatSchnittstelle.SelectedItem = wb_GlobalSettings.DefaultImportSchnittstelle
        End If

    End Sub

    Private Sub wb_Schnittstelle_Konfig_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Konfiguration abschliessne
        SchnittstelleTabelle.KonfigLocked = True
        'Filename der Konfigurations-Datei
        Dim FName As String = wb_GlobalSettings.pXConfigPath & "\" & cbFormatSchnittstelle.SelectedItem & ".xml"
        'Daten aus der aktuellen Schnittstellendefinition sichern
        WriteXML(FName)
    End Sub

    Private Sub cbFormatSchnittstelle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFormatSchnittstelle.SelectedIndexChanged
        'Filename der Konfigurations-Datei
        Dim FName As String = wb_GlobalSettings.pXConfigPath & "\" & cbFormatSchnittstelle.SelectedItem & ".xml"

        'Schnittstellen-Konfiguration laden
        Schnittstelle = New wb_Schnittstelle
        ReadXML(FName)

        'Name der Schnittstelle
        Schnittstelle.Name = cbFormatSchnittstelle.SelectedItem
        'Default-Schnittstelle
        If tbDefaultExport.Text = Schnittstelle.Name Then
            cbDefaultExport.Checked = True
        Else
            cbDefaultExport.Checked = False
        End If
        If (tbDefaultImport.Text = Schnittstelle.Name) Then
            cbDefaultImport.Checked = True
        Else
            cbDefaultImport.Checked = False
        End If

        'Eingabe-Felder aktivieren
        grpDefault.Enabled = True
        grpVerzeichnisse.Enabled = True
        grpTabelle.Enabled = True

        'Import/Export-Verzeichnis sind allgemein definiert
        tbImportVerz.Text = Schnittstelle.ImportVerzeichnis
        tbExportVerz.Text = Schnittstelle.ExportVerzeichnis

        'Liste aller Tabellen
        cbTabelle.Items.Clear()
        For Each SchnittstelleTabelle In Schnittstelle.Tabellen
            cbTabelle.Items.Add(SchnittstelleTabelle.TabName)
        Next

    End Sub

    Private Sub BtnNewFile_Click(sender As Object, e As EventArgs) Handles BtnNewFile.Click
        SaveFileDialog.InitialDirectory = wb_GlobalSettings.pXConfigPath
        SaveFileDialog.Filter = "XML files(.xml)|*.xml"

        'Leere Hülle speichern
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            cbFormatSchnittstelle.Items.Add(Path.GetFileNameWithoutExtension(SaveFileDialog.FileName))
            cbFormatSchnittstelle.SelectedIndex = cbFormatSchnittstelle.Items.Count - 1
        End If
    End Sub

    Private Sub BtnLoadFile_Click(sender As Object, e As EventArgs) Handles BtnLoadFile.Click
        OpenFileDialog.InitialDirectory = wb_GlobalSettings.pXConfigPath
        OpenFileDialog.Filter = "XML files(.xml)|*.xml"

        'Datei (Schnittstellen-Definition) laden
        If OpenFileDialog.ShowDialog = DialogResult.OK Then
            'Prüfen ob diese Definition schon in der Liste existiert
            Dim FName As String = (Path.GetFileNameWithoutExtension(OpenFileDialog.FileName))
            cbFormatSchnittstelle.SelectedItem = FName
            If Not cbFormatSchnittstelle.SelectedItem = FName Then
                cbFormatSchnittstelle.Items.Add(FName)
                cbFormatSchnittstelle.SelectedItem = FName
            End If
        End If
    End Sub

    Private Sub ReadXML(FileName As String)
        'Prüfen ob Datei (schon) existiert
        If System.IO.File.Exists(FileName) Then
            Dim XmlReader As New Xml.Serialization.XmlSerializer(GetType(wb_Schnittstelle))
            Dim XmlFile As New StreamReader(FileName)
            Try
                Schnittstelle = CType(XmlReader.Deserialize(XmlFile), wb_Schnittstelle)
            Catch ex As Exception
            End Try
            XmlFile.Close()
        End If
    End Sub

    Private Sub WriteXML(FileName As String)
        Dim XmlWriter As New System.Xml.Serialization.XmlSerializer(GetType(wb_Schnittstelle))
        Dim XmlFile As New System.IO.StreamWriter(FileName)
        XmlWriter.Serialize(XmlFile, Schnittstelle)
        XmlFile.Close()
    End Sub

    Private Sub BtnImportVerz_Click(sender As Object, e As EventArgs) Handles BtnImportVerz.Click
        If FolderBrowserDialog.ShowDialog = DialogResult.OK Then
            tbImportVerz.Text = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub BtnExportVerz_Click(sender As Object, e As EventArgs) Handles BtnExportVerz.Click
        If FolderBrowserDialog.ShowDialog = DialogResult.OK Then
            tbExportVerz.Text = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    ''' <summary>
    ''' Explorer öffnen. Start im Import-Verzeichnis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnImportExplorer_Click(sender As Object, e As EventArgs) Handles BtnImportExplorer.Click
        Process.Start("explorer.exe", tbImportVerz.Text)
    End Sub

    ''' <summary>
    ''' Explorer öffnet. Start im Export-Verzeichnis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnExportExplorer_Click(sender As Object, e As EventArgs) Handles BtnExportExplorer.Click
        Process.Start("explorer.exe", tbExportVerz.Text)
    End Sub

    Private Sub tbImportVerz_TextChanged(sender As Object, e As EventArgs) Handles tbImportVerz.TextChanged
        Schnittstelle.ImportVerzeichnis = tbImportVerz.Text
    End Sub

    Private Sub tbExportVerz_TextChanged(sender As Object, e As EventArgs) Handles tbExportVerz.TextChanged
        Schnittstelle.ExportVerzeichnis = tbExportVerz.Text
    End Sub

    Private Sub cbDefaultImport_CheckedChanged(sender As Object, e As EventArgs) Handles cbDefaultImport.CheckedChanged
        If cbDefaultImport.Checked Then
            tbDefaultImport.Text = cbFormatSchnittstelle.SelectedItem
            wb_GlobalSettings.DefaultImportSchnittstelle = cbFormatSchnittstelle.SelectedItem
        Else
            tbDefaultImport.Text = ""
            wb_GlobalSettings.DefaultImportSchnittstelle = ""
        End If
    End Sub

    Private Sub cbDefaultExport_CheckedChanged(sender As Object, e As EventArgs) Handles cbDefaultExport.CheckedChanged
        If cbDefaultExport.Checked Then
            tbDefaultExport.Text = cbFormatSchnittstelle.SelectedItem
            wb_GlobalSettings.DefaultExportSchnittstelle = cbFormatSchnittstelle.SelectedItem
        Else
            tbDefaultExport.Text = ""
            wb_GlobalSettings.DefaultExportSchnittstelle = ""
        End If
    End Sub

    Private Sub BtnNewTable_Click(sender As Object, e As EventArgs) Handles BtnNewTable.Click
        tbTabelleName.Enabled = True
        tbTabelleName.Focus()
    End Sub

    Private Sub tbTabelleName_Leave(sender As Object, e As EventArgs) Handles tbTabelleName.Leave
        grpTabelleFelder.Enabled = True

        If Not CheckAddComboBoxItem(tbTabelleName.Text & "-Export", cbTabelle) And
            Not CheckAddComboBoxItem(tbTabelleName.Text & "-Import", cbTabelle) Then
            MsgBox("Diese Tabellen-Definition existiert schon !", MsgBoxStyle.Exclamation, "Schnittstelle WinBack-Office-Pro")
        End If
    End Sub

    Private Sub cbEnable_CheckedChanged(sender As Object, e As EventArgs) Handles cbEnable.CheckedChanged
        SchnittstelleTabelle.Enabled = cbEnable.Checked
    End Sub

    Private Function CheckAddComboBoxItem(cbItem As String, cbBox As ComboBox) As Boolean
        cbBox.SelectedItem = cbItem
        If cbBox.SelectedItem = cbItem Then
            Return False
        Else
            cbBox.Items.Add(cbItem)
            cbBox.SelectedIndex = cbBox.Items.Count - 1
            Return True
        End If
    End Function

    Private Sub cbTabelle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTabelle.SelectedIndexChanged
        Debug.Print("Tabelle " & cbTabelle.SelectedItem)

        'Prüfen ob die Tabelle schon als Definition in der Schnittstelle existiert
        For Each SchnittstelleTabelle In Schnittstelle.Tabellen
            If SchnittstelleTabelle.TabName = cbTabelle.SelectedItem Then
                Debug.Print("Tabellendefinition gefunden " & cbTabelle.SelectedItem)
                SchnittstellenTabelleDetails()
                'Grid anzeigen Tabellen-Felder Definition
                SchnittstelleTabelleFelder()
                Exit Sub
            End If
        Next

        'Tabelle nicht gefunden - neu anlegen
        Dim SnTab As New wb_SchnittstelleTabelle
        'Werte vorbelegen
        SnTab.TabName = cbTabelle.SelectedItem
        SnTab.Enabled = True
        'Tabelle laden aktivieren
        grpTabelleFelder.Enabled = True

        'Schnittstelle Tabelle-Definition hinzufügen
        Schnittstelle.Tabellen.Add(SnTab)

    End Sub

    Private Sub SchnittstellenTabelleDetails()
        'Tabellen-Definition aktiv/gültig
        cbEnable.Checked = SchnittstelleTabelle.Enabled
        'Tabelle Name
        tbTabelleName.Text = SchnittstelleTabelle.TabName

        'Button Tabelle laden aktivieren
        grpTabelleFelder.Enabled = True

        'Trennzeichen
        cbKomma.Checked = SchnittstelleTabelle.TrennzeichenKomma
        cbSemikolon.Checked = SchnittstelleTabelle.TrennzeichenSemikolon
        cbTab.Checked = SchnittstelleTabelle.TrennzeichenTab
        cbSpace.Checked = SchnittstelleTabelle.TrennzeichenSpace
        cbSonder.Checked = SchnittstelleTabelle.TrennzeichenSonder
        tbSonder.Text = SchnittstelleTabelle.Trennzeichen
    End Sub

    Private Sub SchnittstelleTabelleFelder()
        'Liste der Tabellen-Überschriften
        Dim sColNames As New List(Of String) From {"Idx", "Name"}
        'Daten im Grid anzeigen
        SchnittstelleFelderGrid = New wb_ArrayGridViewSchnittstelleFelder(SchnittstelleTabelle.TabFelder, sColNames)
        SchnittstelleFelderGrid.ScrollBars = ScrollBars.Vertical
        SchnittstelleFelderGrid.BackgroundColor = Me.BackColor
        SchnittstelleFelderGrid.GridLocation(pnlFelder)
        SchnittstelleFelderGrid.PerformLayout()
        SchnittstelleFelderGrid.Refresh()
    End Sub

    Private Sub BtnLoadTabelle_Click(sender As Object, e As EventArgs) Handles BtnLoadTabelle.Click
        'Import-Pfad
        OpenFileDialog.InitialDirectory = tbImportVerz.Text
        OpenFileDialog.FileName = ""

        'Extension aus Muster
        Dim FileExtension As String = Path.GetExtension(tbFileNameSchema.Text)
        If FileExtension <> "" Then
            OpenFileDialog.Filter = "Import files(.xml)|*." & FileExtension
        Else
            OpenFileDialog.Filter = "Import files(.*)|*.*"
        End If

        'Datei (Schnittstellen-Definition) laden
        If OpenFileDialog.ShowDialog = DialogResult.OK Then
            Debug.Print("File Load OK")
        End If

        'Filename für Vorschau
        wb_Schnittstelle_Shared.Vorschau_FileName = OpenFileDialog.FileName
        'Vorschau aktualisieren
        ShowPreview()

        'Prüfen ob die Anzahl der gelesenen Felder mit der Definition übereinstimmt
        If SchnittstelleTabelle.TabFelder.Count <> SchnittstelleTabelle.ResultFelder.Count Then
            If MsgBox("Die Anzahl der Datenfelder in der Musterdatei entspricht nicht der Definition" & vbCrLf & "Soll die Import-Definition angepasst werden?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                SchnittstelleTabelle.KonfigLocked = False
                LoadAndShow()
            End If
        End If
    End Sub

    Private Sub cbTrennZeichen_Click(sender As Object, e As EventArgs) Handles cbSpace.Click, cbSemikolon.Click, cbKomma.Click, cbTab.Click, cbSonder.Click
        SchnittstelleTabelle.TrennzeichenSpace = cbSpace.Checked
        SchnittstelleTabelle.TrennzeichenKomma = cbKomma.Checked
        SchnittstelleTabelle.TrennzeichenSemikolon = cbSemikolon.Checked
        SchnittstelleTabelle.TrennzeichenTab = cbTab.Checked
        SchnittstelleTabelle.TrennzeichenSonder = cbSonder.Checked
        'Vorschau aktualisieren
        ShowPreview()
    End Sub

    Private Sub tbSonder_Leave(sender As Object, e As EventArgs) Handles tbSonder.Leave
        SchnittstelleTabelle.Trennzeichen = tbSonder.Text
        'Vorschau aktualisieren
        ShowPreview()
    End Sub

    ''' <summary>
    ''' Vorschau(Fenster) neu aufbauen
    ''' </summary>
    Private Sub ShowPreview()
        If wb_Schnittstelle_Shared.Vorschau_FileName <> "" Then
            wb_Schnittstelle_Shared.LoadAndShow()
        End If
    End Sub
End Class