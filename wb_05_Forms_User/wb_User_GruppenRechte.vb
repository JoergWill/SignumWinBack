Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_User_Shared

Public Class wb_User_GruppenRechte
    Inherits DockContent

    Dim UserGruppenListe As New ArrayList
    Dim UserGruppenGrid As wb_ArrayGridViewUserGruppen
    Dim UserGruppe As wb_User_Gruppe

    ''' <summary>
    ''' Grid der Gruppen-Rechte laden und anzeigen.
    ''' Die Verarbeitung von Anzeige und Edit erfolgt in der Klasse wb_ArrayGridViewUserGruppen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_User_GruppenRechte_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Spalten-Überschriften
        Dim sColNames As New List(Of String)
        'erste Spalte Gruppen-Bezeichnung
        sColNames.Add("Bezeichnung")
        sColNames.Add("")
        sColNames.Add("")
        sColNames.Add("")

        'User-Gruppen aus Tabelle winback.IAListe
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        If winback.sqlSelect(wb_Functions.SetParams(wb_Sql_Selects.sqlUserGrpListe, wb_Language.GetLanguageNr())) Then

            'alle Datensätze einlesen
            While winback.Read
                UserGruppe = New wb_User_Gruppe
                'Gruppen-Hauptbezeichnung(Oberbegriff)
                UserGruppe.GruppenBezeichnung = winback.sField("T_Text")
                sColNames.Add(UserGruppe.GruppenBezeichnung)
                'Gruppen-Nummer - Alle Rechte für diese Gruppe laden
                UserGruppe.LoadData(winback.iField("IAL_ItemID"))
                'Rechte-Objekt speichern
                UserGruppenListe.Add(UserGruppe)
            End While
            winback.Close()

            'Gruppen-Rechte im Grid anzeigen
            UserGruppenGrid = New wb_ArrayGridViewUserGruppen(UserGruppenListe, sColNames)
            UserGruppenGrid.BackgroundColor = Me.BackColor
            UserGruppenGrid.GridLocation(pnlUserGruppenRechte)
            UserGruppenGrid.PerformLayout()
        End If

    End Sub

    ''' <summary>
    ''' Speichern die Änderungen der Gruppenrechte in der WinBack Datenbank.
    ''' die Gruppenrechte stehen in der Tabelle winback.ItemParameter mit IP_ItemTyp gleich 1, 2, 200
    ''' 
    ''' Die Daten werden komplett gelöscht und anhand des Array wieder neu aufgebaut.
    ''' </summary>
    Private Sub wb_User_GruppenRechte_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim sqlValues As String = ""
        'wenn Daten geändert worden sind, muss gespeichert werden
        If UserGruppenGrid.Changed Then
            'Datenbank-Verbindung
            Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
            'Daten löschen
            If winback.sqlCommand(wb_Sql_Selects.sqlUserGrpRemoveAll) Then
                'alle Datensätze nacheinander in die Datenbank wieder einfügen
                For Each UserGrp As wb_User_Gruppe In UserGruppenListe
                    For Each UserGrpRecht As wb_Global.wb_GruppenRechte In UserGrp.UserRechte
                        sqlValues = UserGrpRecht.iTyp & "," & UserGrpRecht.iID & "," & UserGrpRecht.iAttrGrp & "," & UserGrp.Gruppe + 100 & "," & UserGrp.Gruppe & "," & UserGrpRecht.iAttribut
                        winback.sqlCommand(wb_Functions.SetParams(wb_Sql_Selects.sqlUserGrpInsert, sqlValues))
                        Debug.Print("Save UserGruppenRechte " & sqlValues)
                    Next
                Next
            Else
                MsgBox("Fehler beim Löschen/Speichern der Benutzer_Gruppen.", MsgBoxStyle.Exclamation, "WinBack-AddIn")
            End If
        End If
    End Sub
End Class