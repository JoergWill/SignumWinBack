Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_CheckDatabase
    Inherits DockContent
    Private UpdateDataBaseFiles As New List(Of String)

    Private Sub wb_Admin_CheckDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rtLogger.Text = ""
    End Sub

    Private Sub BtnStartCheck_Click(sender As Object, e As EventArgs) Handles BtnStartCheck.Click
        Trace.WriteLine("Check Database Start")
        If CheckDatabase() Then
            Trace.WriteLine("Check Database OK")
        Else
            Trace.WriteLine("Check Database Fail")
        End If
    End Sub

    Private Sub LogEvent(txt As String)
        LogEvent(txt & vbCrLf, Color.Black)
    End Sub

    Private Sub LogEvent(txt As String, TextFarbe As Color, Optional Err As Boolean = False)
        'Assert.IsTrue aus Fehlermeldung entfernen
        Dim a As Integer = txt.IndexOf("Assert.IsTrue")
        If (a > 0) Then
            txt = vbTab & "Fehler" & vbTab & txt.Remove(0, a + 15) & vbCrLf
        ElseIf Err Then
            txt = vbTab & txt & vbCrLf
        End If

        'Text farbig ausgeben
        rtLogger.SelectionColor = TextFarbe
        rtLogger.SelectedText = txt
    End Sub

    ''' <summary>
    ''' Fehlertext formatiert ausgeben
    ''' </summary>
    ''' <param name="CheckOK"></param>
    ''' <param name="CheckText"></param>
    ''' <returns></returns>
    Private Function CheckResultText(CheckOK As Boolean, CheckText As String) As Boolean
        If CheckOK Then
            LogEvent("OK" & vbTab, Color.Green)
        Else
            LogEvent("NOK" & vbTab, Color.Red)
        End If
        LogEvent(CheckText)
        Return CheckOK
    End Function

    ''' <summary>
    ''' Prüft alle WinBack-Datenbank-Tabellen auf Vollständigkeit.
    ''' Check ob alle Upates in der Datenbank vorhanden und richtig eingespielt sind.
    ''' </summary>
    ''' <returns></returns>
    Public Function CheckDatabase() As Boolean
        'Ergebnis vorbelegen
        CheckDatabase = True
        'Liste der Update-Files löschen
        UpdateDataBaseFiles.Clear()

        'Prüfe Tabelle winback.Liniengruppen - Formularsteuerung
        If Not CheckResultText(wb_Linien_Global.CheckDB(), "Check Datenbank winback.Liniengruppen - Formularsteuerung") Then
            LogEvent(wb_Linien_Global.ErrorText, Color.Red, True)
            CheckDatabase = False
            UpdateDataBaseFiles.Add(wb_Linien_Global.UpdateDatabaseFile)
        End If

        'Prüfe Tabelle winback.KomponTypen(300) - Parameter Produktion
        If Not CheckResultText(wb_KomponParam300_Global.CheckDB(), "Check Datenbank winback.KomponTypen(300) - Parameter Produktion") Then
            LogEvent(wb_KomponParam300_Global.ErrorText, Color.Red)
            CheckDatabase = False
            UpdateDataBaseFiles.Add(wb_KomponParam300_Global.UpdateDatabaseFile)
        End If

        'Prüfe Tabelle winback.KomponTypen(301) - Allergene und Nährwerte
        If Not CheckResultText(wb_KomponParam301_Global.CheckDB(), "Check Datenbank winback.KomponTypen(301) - Allergene und Nährwerte") Then
            LogEvent(wb_KomponParam301_Global.ErrorText, Color.Red)
            CheckDatabase = False
            UpdateDataBaseFiles.Add(wb_KomponParam301_Global.UpdateDatabaseFile)
        End If

        'Prüfe Tabelle winback.ItemParameter - User-Rechte(Gruppe -1)
        If Not CheckResultText(wb_AktRechte.CheckDB(), "Check Datenbank winback.ItemParameter(-1) - User-Rechte") Then
            LogEvent(wb_AktRechte.ErrorText, Color.Red, True)
            CheckDatabase = False
            UpdateDataBaseFiles.Add(wb_AktRechte.UpdateDatabaseFile)
        End If

        'Prüfe Tabelle winback.Einheiten - Spalte E_obNr
        If Not CheckResultText(wb_Einheiten_Global.CheckDB(), "Check Datenbank winback.Einheiten - Spalte E_obNr") Then
            LogEvent(wb_Einheiten_Global.ErrorText, Color.Red, True)
            CheckDatabase = False
            UpdateDataBaseFiles.Add(wb_Einheiten_Global.UpdateDatabaseFile)
        End If

        'Prüfe Tabelle winback.ENummern vorhanden
        If Not CheckResultText(wb_ZutatenListe_Global.CheckDB(), "Check Datenbank winback.ENummern") Then
            LogEvent(wb_Einheiten_Global.ErrorText, Color.Red, True)
            CheckDatabase = False
            UpdateDataBaseFiles.Add(wb_Einheiten_Global.UpdateDatabaseFile)
        End If

    End Function
End Class