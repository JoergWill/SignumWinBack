Imports System.Drawing
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_CheckDatabase
    Inherits DockContent

    Private UpdateDataBaseFiles As New List(Of String)
    Private FirstRun As Boolean = True

    Private Sub wb_Admin_CheckDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rtLogger.Text = ""
        If FirstRun Then
            FirstRun = False
            BtnStartCheck_Click(sender, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Führt ein einzelnes Datenbank-Update durch. Der Filename des Patchfiles wird als Parameter übergeben.
    ''' War das Update erfolgreich wird True zurückgegeben
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    Public Function DoDBUpdate(FileName As String) As Boolean
        'Updates durchführen
        If checkUpdateFile(FileName) Then
            'Modales Fenster Update-Datenbank öffnen
            BtnDoUpdate_Click(Nothing, Nothing)
            Return True
        Else
            Return False
        End If
    End Function

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1066:Mergeable ""if"" statements should be combined", Justification:="<Ausstehend>")>
    Private Sub BtnStartCheck_Click(sender As Object, e As EventArgs) Handles BtnStartCheck.Click
        If CheckDatabase Then
            Trace.WriteLine("@I_Check Database OK")
            BtnDBUpdates.Enabled = False
            BtnDoUpdate.Enabled = False
        Else
            Trace.WriteLine("@E_Check Database Fail")
            BtnDBUpdates.Enabled = True
            BtnDoUpdate.Enabled = False
        End If

        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            If Not CheckAddIns Then
                Trace.WriteLine("@E_Check AddIns Fail")
            End If
        End If
    End Sub

    Private Sub LogEvent(txt As String)
        LogEvent(txt & vbCrLf, Color.Black)
    End Sub

    Private Sub LogEvent(txt As String, TextFarbe As Color, Optional Err As Boolean = False)
        If Err Then
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
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public Function CheckDatabase() As Boolean
        'Ergebnis vorbelegen
        Dim Result As Boolean = True
        'Liste der Update-Files löschen
        UpdateDataBaseFiles.Clear()

        'Falls die DB-Verbindung zu WinBack nicht funktioniert/falsch initialisiert ist
        Try

            'Prüfe Tabelle winback.Liniengruppen - Formularsteuerung
            If Not CheckResultText(wb_Linien_Global.CheckDB(), "Check Datenbank winback.Liniengruppen - Formularsteuerung") Then
                LogEvent(wb_Linien_Global.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_Linien_Global.UpdateDatabaseFile)
            End If

            'Prüfe Tabelle winback.Komponenten - KA_zaehlt_zu_NWT_Gesamtmenge
            If Not CheckResultText(wb_Rohstoffe_Shared.CheckDB(), "Check Datenbank winback.Komponenten - DB-Feld KA_zaehlt_zu_NWT_Gesamtmenge") Then
                LogEvent(wb_Rohstoffe_Shared.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_Rohstoffe_Shared.UpdateDatabaseFile)
            End If

            'Prüfe Tabelle winback.KomponTypen(101/102) - Parameter TA Unterer Grenzwert Eingabe (-1.0)
            If Not CheckResultText(wb_KomponParam_Global.CheckDB_A(), "Check Datenbank winback.KomponTypen(101/102) - UG Eingabe TA (Parameter 7)") Then
                LogEvent(wb_KomponParam_Global.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_KomponParam_Global.UpdateDatabaseFile)
            End If
            If Not CheckResultText(wb_KomponParam_Global.CheckDB_B(), "Check Datenbank winback.KomponParams(KP_Wert Datenfeld Länge)") Then
                LogEvent(wb_KomponParam_Global.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_KomponParam_Global.UpdateDatabaseFile)
            End If

            'Prüfe Tabelle winback.KomponTypen(300) - Parameter Produktion
            If Not CheckResultText(wb_KomponParam300_Global.CheckDB(), "Check Datenbank winback.KomponTypen(300) - Parameter Produktion") Then
                LogEvent(wb_KomponParam300_Global.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_KomponParam300_Global.UpdateDatabaseFile)
            End If

            'Prüfe Tabelle winback.KomponTypen(301) - Allergene und Nährwerte
            If Not CheckResultText(wb_KomponParam301_Global.CheckDB(), "Check Datenbank winback.KomponTypen(301) - Allergene und Nährwerte") Then
                LogEvent(wb_KomponParam301_Global.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_KomponParam301_Global.UpdateDatabaseFile)
            End If

            'Klasse initialisieren
            wb_AktSysKonfig.SysKonfigOK("30", False)
            'Prüfe Tabelle winback.ItemParameter - User-Rechte(Gruppe -1)
            If Not CheckResultText(wb_AktRechte.CheckDB_User(), "Check Datenbank winback.ItemParameter(-1) - User-Rechte") Then
                LogEvent(wb_AktRechte.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_AktRechte.UpdateDatabaseFile)
            End If
            'Prüfe Tabelle winback.ItemParameter - User-Rechte(Gruppe -1)
            If Not CheckResultText(wb_AktRechte.CheckDB_Prod(), "Check Datenbank winback.ItemParameter(-1) - Produktion") Then
                LogEvent(wb_AktRechte.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_AktRechte.UpdateDatabaseFile)
            End If
            'Prüfe User-Gruppen Tabelle ItemParameter(IP_Wert1Int=99) Gruppe 99 muss alle Rechte mit den richtigen Attributen enthalten
            Dim UserGruppe As New wb_User_Gruppe
            If Not CheckResultText(UserGruppe.CheckDB_Grp99(), "Check Datenbank UserGruppenRechte(99)") Then
                LogEvent(UserGruppe.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(UserGruppe.UpdateDatabaseFile)
            End If
            'Prüfe Rezept-Gruppen Tabelle ItemParameter(IP_Wert1Int=99) Gruppe 99 muss mindestens 4 Einträge für die Rezeptgruppe enthalten
            If Not CheckResultText(wb_User_Rechte_Shared.CheckDB_RezGrp(), "Check Datenbank UserRezeptGruppenRechte") Then
                LogEvent(wb_User_Rechte_Shared.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_User_Rechte_Shared.UpdateDatabaseFile)
            End If
            'Prüfe UserRechte-Gruppen Tabelle ItemParameter(IP_Wert1Int=99) Gruppe 99 muss mindestens x Einträge für die UserGruppenRechte enthalten
            If Not CheckResultText(wb_User_Rechte_Shared.CheckDB_User(), "Check Datenbank UserGruppenRechte") Then
                LogEvent(wb_User_Rechte_Shared.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_User_Rechte_Shared.UpdateDatabaseFile)
            End If
            'Prüfe User-Gruppen-Rechte Tabelle IALIste und ItemIDs müssen alle Einträge aus ItemParameter enthalten (INNER JOIN)
            If Not CheckResultText(wb_User_Rechte_Shared.CheckDB_IAListe(), "Check Datenbank UserRechte - IAListe") Then
                LogEvent(wb_User_Rechte_Shared.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_User_Rechte_Shared.UpdateDatabaseFile)
            End If
            'Prüfe User-Gruppen-Rechte Tabelle IALIste und ItemIDs müssen alle Einträge aus ItemParameter enthalten (INNER JOIN)
            If Not CheckResultText(wb_User_Rechte_Shared.CheckDB_ItemIDs(), "Check Datenbank UserRechte - ItemIDs") Then
                LogEvent(wb_User_Rechte_Shared.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_User_Rechte_Shared.UpdateDatabaseFile)
            End If

            'Prüfe Tabelle winback.Einheiten - Spalte E_obNr
            If Not CheckResultText(wb_Einheiten_Global.CheckDB(), "Check Datenbank winback.Einheiten - Spalte E_obNr") Then
                LogEvent(wb_Einheiten_Global.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_Einheiten_Global.UpdateDatabaseFile)
            End If

            'Prüfe Tabelle winback.ENummern vorhanden
            If Not CheckResultText(wb_ZutatenListe_Global.CheckDB(), "Check Datenbank winback.ENummern") Then
                LogEvent(wb_ZutatenListe_Global.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_ZutatenListe_Global.UpdateDatabaseFile)
            End If

            'Prüfe Tabelle winback.Lagerorte.LG_LF_Nr hat Datentyp Integer(11)
            If Not CheckResultText(wb_LagerOrt.CheckDB(), "Check Datenbank winback.Lagerorte") Then
                LogEvent(wb_LagerOrt.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_LagerOrt.UpdateDatabaseFile)
            End If

            'Prüfe Tabelle wbdaten.HisRezepte.H_RZ_Bezeichnung hat Länge 60
            If Not CheckResultText(wb_Rezept_Shared.CheckDB(), "Check Datenbank wbdaten.HisRezepte.H_RZ_Bezeichnung") Then
                LogEvent(wb_Rezept_Shared.ErrorText, Color.Red, True)
                Result = False
                UpdateDataBaseFiles.Add(wb_Rezept_Shared.UpdateDatabaseFile)
            End If
        Catch
            Trace.WriteLine("@E_Fehler bei Check-Database")
        End Try
        Return Result
    End Function

    Private Function CheckAddIns() As Boolean
        'Ergebnis vorbelegen
        Dim Result As Boolean = True
        For Each sAddInNames In {"ob_Artikel_DockingExtension", "ob_Artikel_Services", "ob_RecipeProvider", "ob_Rohstoffe_SiloInventur", "ob_Planung_DockingExtension", "ob_Admin_OrgaBackSettings"}
            If Not CheckAddInRegistered(sAddInNames) Then
                Result = False
            End If
        Next
        Return Result
    End Function

    Private Function CheckAddInRegistered(Name As String) As Boolean
        If Not wb_Main_Shared.IsRegistered(Name) Then
            LogEvent("WinBack.AddIn." & Name & " ist nicht registriert !", Color.Red, True)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Prüft im DBUpdate-Verzeichnis, ob die notwendigen Update-Files als .bak File vorliegen
    ''' Wenn keine passenden Dateien gefunden werden, wird auf www.winbackde/software gesucht
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnDBUpdates_Click(sender As Object, e As EventArgs) Handles BtnDBUpdates.Click
        'Ausgabe Hinweis-Text
        LogEvent(vbCrLf)
        'Liste aller notwendigen Update-Files
        For Each updFile As String In UpdateDataBaseFiles
            LogEvent("Update" & vbTab, Color.Green)
            LogEvent(updFile)

            'Update-Button wird nur aktiviert, wenn eines der Update-Files vorhanden ist
            BtnDoUpdate.Enabled = checkUpdateFile(updFile)
        Next
    End Sub

    Private Function checkUpdateFile(updFile As String) As Boolean
        'Ergebnis
        Dim Result As Boolean = False

        'Prüfen ob Update-File vorhanden ist
        If Not My.Computer.FileSystem.FileExists(wb_GlobalSettings.pDBUpdatePath & updFile) Then
            If CheckBakFile(updFile) Then
                CheckResultText(True, updFile & " wiederhergestellt")
                Result = True
            Else
                If CheckGetFile(updFile) Then
                    CheckResultText(True, updFile & " download")
                    Result = True
                Else
                    CheckResultText(False, updFile & " nicht vorhanden")
                    Result = False
                End If
            End If
        Else
            CheckResultText(True, updFile & " vorhanden")
            Result = True
        End If

        Return Result
    End Function

    ''' <summary>
    ''' Prüft das DBUPdate-Verzeichnis, ob eine Datei mit Endung .bak vorliegt und
    ''' bennennt diese wieder in das Original um
    ''' </summary>
    ''' <param name="updFile"></param>
    ''' <returns></returns>
    Private Function CheckBakFile(updFile As String) As Boolean
        'Ergebnis vorbelegen
        Dim Result As Boolean = False

        'Datei(en) umbenennen
        Try
            For Each fName As String In My.Computer.FileSystem.GetFiles(wb_GlobalSettings.pDBUpdatePath, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, updFile & ".bak")
                Try
                    FileSystem.Rename(fName, fName.Replace(".bak", ""))
                    Result = True
                Catch ex As Exception
                    Result = True
                End Try
            Next
        Catch ex As Exception
            Result = True
        End Try
        Return Result
    End Function

    ''' <summary>
    ''' Versucht von www.winback.de/software ein DBUpdateFile zu laden
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    Private Function CheckGetFile(FileName As String) As Boolean
        'TODO Implementation fehlt
        Return False
    End Function

    ''' <summary>
    ''' Fenster Update Database modal öffnen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnDoUpdate_Click(sender As Object, e As EventArgs) Handles BtnDoUpdate.Click
        Dim UpdateDatabase As New wb_Admin_UpdateDatabase
        UpdateDatabase.ShowDialog()
    End Sub

End Class