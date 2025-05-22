Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Schnittstelle_Log
    Inherits DockContent
    Private LogText As String

    Private Sub wb_Admin_Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Logger (TraceListener) einbinden
        AddHandler wb_Admin_Shared.NewLogText, AddressOf wb_Admin_LogEvent
    End Sub

    Private Sub wb_Admin_LogEvent(txt As String)
        Try
            'nur den Text der Meldung ausgeben
            LogText = LogText & Split(txt, vbTab)(1) & vbCr
            'Timer zur Aktualierung der Anzeige starten
            tLogAnzeigen.Enabled = True
        Catch
        End Try
    End Sub

    Private Sub tLogAnzeigen_Tick(sender As Object, e As EventArgs) Handles tLogAnzeigen.Tick
        'Timer wieder abschalten
        tLogAnzeigen.Enabled = False
        'Text-Anzeige aktualisieren
        tbLogger.Text = tbLogger.Text & LogText
        LogText = ""
        'Scroll zum Ende des Textes
        tbLogger.SelectionStart = tbLogger.Text.Length
        tbLogger.SelectionLength = 0
        tbLogger.ScrollToCaret()
        'Text anzeigen
        Application.DoEvents()
    End Sub

    Private Sub wb_Schnittstelle_Log_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler wb_Admin_Shared.NewLogText, AddressOf wb_Admin_LogEvent
    End Sub

End Class