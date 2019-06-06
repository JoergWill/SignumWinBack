Imports System.IO
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_Log
    Inherits DockContent

    Private Sub wb_Admin_Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'CheckBox AutoStart
        cbLogAutoStart.Checked = wb_GlobalSettings.Log4netAutoStart
        'CheckBox Aktiv
        cbLogAktiv.Checked = wb_Admin_Shared.LoggerAktiv

        'alle aktuellen Einträge aus TraceListener anzeigen
        loadLoggerFromStringArray()

        'Logger (TraceListener) einbinden
        AddHandler wb_Admin_Shared.NewLogText, AddressOf wb_Admin_LogEvent
    End Sub

    Private Sub wb_Admin_LogEvent(txt As String)
        tbLogger.Text = tbLogger.Text + txt & vbCr
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
        ''wenn schon eine Textdatei existiert
        'If cbLogTextFile.Checked And File.Exists(myDocPathLogFile) Then

        '    'alle Einträge aus dem Textfile laden
        '    Dim objStreamReader As StreamReader
        '    Dim strLine As String

        '    'Pass the file path and the file name to the StreamReader constructor.
        '    objStreamReader = New StreamReader(myDocPathLogFile)

        '    'Read the first line of text.
        '    strLine = objStreamReader.ReadLine

        '    'Continue to read until you reach the end of the file.
        '    Do While Not strLine Is Nothing
        '        tbLogger.Text = tbLogger.Text + strLine + vbCrLf
        '        'Read the next line.
        '        strLine = objStreamReader.ReadLine
        '    Loop
        '    'Close the file.
        '    objStreamReader.Close()
        'End If
    End Sub

    Private Sub loadLoggerFromStringArray()
        'alle aktuellen Einträge aus den Log-Events
        For Each x As String In wb_Admin_Shared.LogEvents
            tbLogger.Text = tbLogger.Text + x
        Next
    End Sub
End Class