Imports System.IO
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_Log
    Inherits DockContent
    'Set a variable to the My Documents path.
    Dim myDocPathLogFile As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & wb_Global.LogFileName

    Private Sub wb_Admin_Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set CheckBox-Eintrag
        cbLogTextFile.Checked = wb_GlobalSettings.LogToTextFile
        cbLogDataBase.Checked = wb_GlobalSettings.logToDataBase

        'wenn ein Textfile vorhanden ist, kann es auch angezeigt werden
        BtnLoadTextFile.Enabled = File.Exists(myDocPathLogFile)

        'alle aktuellen Einträge aus TraceListener anzeigen
        loadLoggerFromStringArray()

        'Logger (TraceListener) einbinden
        AddHandler wb_Admin_Shared.NewLogText, AddressOf wb_Admin_LogEvent
    End Sub

    Private Sub wb_Admin_LogEvent(txt As String)
        tbLogger.Text = tbLogger.Text + txt & vbCr
    End Sub


    Private Sub wb_Admin_Log_FormClosed(sender As Object, e As Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Logger wieder freigeben

    End Sub

    Private Sub cbLogTextFile_CheckedChanged(sender As Object, e As EventArgs) Handles cbLogTextFile.CheckedChanged
        wb_GlobalSettings.LogToTextFile = cbLogTextFile.Checked
        'Button Anzeige Textfile wird nur dann aktiv wenn auch ein Textfile existiert oder erzeugt wird
        BtnLoadTextFile.Enabled = cbLogTextFile.Checked Or File.Exists(myDocPathLogFile)
    End Sub

    Private Sub cbLogDataBase_CheckedChanged(sender As Object, e As EventArgs) Handles cbLogDataBase.CheckedChanged
        wb_GlobalSettings.logToDataBase = cbLogDataBase.Checked
    End Sub

    Private Sub BtnLoadTextFile_Click(sender As Object, e As EventArgs) Handles BtnLoadTextFile.Click
        'Mauszeiger anpassen
        Me.Cursor = Cursors.WaitCursor
        'alle alten Einträge löschen
        tbLogger.Text = ""
        'alle Einträge aus Textfile einlesen und anzeigen
        LoadLoggerFromTextFile()
        'alle aktuellen Einträge aus TraceListener anzeigen
        loadLoggerFromStringArray()
        'Mauszeiger anpassen
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadLoggerFromTextFile()
        'wenn schon eine Textdatei existiert
        If cbLogTextFile.Checked And File.Exists(myDocPathLogFile) Then

            'alle Einträge aus dem Textfile laden
            Dim objStreamReader As StreamReader
            Dim strLine As String

            'Pass the file path and the file name to the StreamReader constructor.
            objStreamReader = New StreamReader(myDocPathLogFile)

            'Read the first line of text.
            strLine = objStreamReader.ReadLine

            'Continue to read until you reach the end of the file.
            Do While Not strLine Is Nothing
                tbLogger.Text = tbLogger.Text + strLine + vbCrLf
                'Read the next line.
                strLine = objStreamReader.ReadLine
            Loop
            'Close the file.
            objStreamReader.Close()
        End If
    End Sub

    Private Sub loadLoggerFromStringArray()
        'alle aktuellen Einträge aus den Log-Events
        For Each x As String In wb_Admin_Shared.LogEvents
            tbLogger.Text = tbLogger.Text + x
        Next
    End Sub
End Class