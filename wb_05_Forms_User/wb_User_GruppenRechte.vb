Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_User_GruppenRechte
    Inherits DockContent

    Const COLTyp = 0   'Hauptgruppe (1,2,200)
    Const COLIDx = 1   'Untergruppe (1..x)
    Const COLInp = 2   'EingabeTyp (403,405,406...)
    Const COLGrp = 3   'Erste Spalte Gruppe 1....
    Const COLAdm = 11  'Spalte Admin-Rechte

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
        sColNames.Add("")
        sColNames.Add("")
        sColNames.Add("")

        'Überschriften aus Tabelle Texte
        If Not LoadUserGruppenTexte(sColNames) Then
            'Überschriften aus Tabelle IAListe.Kommentar
            LoadUserGruppenKommentar(sColNames)
        End If

        'Prüfen ob Daten vorhanden sind
        If UserGruppenListe.Count > 0 Then
            'Gruppen-Rechte im Grid anzeigen
            UserGruppenGrid = New wb_ArrayGridViewUserGruppen(UserGruppenListe, sColNames)
            UserGruppenGrid.BackgroundColor = Me.BackColor
            UserGruppenGrid.GridLocation(pnlUserGruppenRechte)
            UserGruppenGrid.PerformLayout()
        End If

    End Sub

    Private Function LoadUserGruppenTexte(ByRef sColnames As List(Of String)) As Boolean
        'Erst mal sind keine Datensätze vorhanden
        LoadUserGruppenTexte = False

        'User-Gruppen aus Tabelle winback.IAListe
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        If winback.sqlSelect(wb_Functions.SetParams(wb_Sql_Selects.sqlUserGrpListeA, wb_Language.GetLanguageNr())) Then
            'alle Datensätze einlesen
            While winback.Read
                UserGruppe = New wb_User_Gruppe
                'Gruppen-Hauptbezeichnung(Oberbegriff)
                UserGruppe.GruppenBezeichnung = winback.sField("T_Text")
                sColnames.Add(UserGruppe.GruppenBezeichnung)
                'Gruppen-Nummer - Alle Rechte für diese Gruppe laden
                UserGruppe.LoadData(winback.iField("IAL_ItemID"))
                'Rechte-Objekt speichern
                UserGruppenListe.Add(UserGruppe)
                LoadUserGruppenTexte = True
            End While
            winback.Close()
        Else
            'Fehler bei der Abfrage der Daten
            LoadUserGruppenTexte = False
        End If
    End Function

    Private Function LoadUserGruppenKommentar(ByRef sColnames As List(Of String)) As Boolean
        'Erst mal sind keine Datensätze vorhanden
        LoadUserGruppenKommentar = False

        'User-Gruppen aus Tabelle winback.IAListe
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        If winback.sqlSelect(wb_Sql_Selects.sqlUserGrpListeB) Then
            'alle Datensätze einlesen
            While winback.Read
                UserGruppe = New wb_User_Gruppe
                'Gruppen-Hauptbezeichnung(Oberbegriff)
                UserGruppe.GruppenBezeichnung = winback.sField("IAL_Kommentar")
                sColnames.Add(UserGruppe.GruppenBezeichnung)
                'Gruppen-Nummer - Alle Rechte für diese Gruppe laden
                UserGruppe.LoadData(winback.iField("IAL_ItemID"))
                'Rechte-Objekt speichern
                UserGruppenListe.Add(UserGruppe)
                LoadUserGruppenKommentar = True
            End While
            winback.Close()
        Else
            'Fehler bei der Abfrage der Daten
            LoadUserGruppenKommentar = False
        End If
    End Function

    ''' <summary>
    ''' Speichern die Änderungen der Gruppenrechte in der WinBack Datenbank.
    ''' die Gruppenrechte stehen in der Tabelle winback.ItemParameter mit IP_ItemTyp gleich 1, 2, 200
    ''' 
    ''' Die Daten werden komplett gelöscht und anhand des Array wieder neu aufgebaut.
    ''' </summary>
    Private Sub wb_User_GruppenRechte_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SaveData()
    End Sub

    ''' <summary>
    ''' Wenn das Formular den Focus verliert werden die Änderungen an den Gruppen-Rechten gespeichert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_User_GruppenRechte_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        SaveData()
    End Sub

    Private Sub SaveData()

        'wenn Daten geändert worden sind, muss gespeichert werden
        If UserGruppenGrid.Changed Then
            If Not SaveUserGruppenRechte() Then
                MsgBox("Fehler beim Löschen/Speichern der Benutzer-Gruppen.", MsgBoxStyle.Exclamation, "WinBack-AddIn")
            Else
                'Anzeige aktualisieren
                'wb_User_Shared.Gruppe.iGruppe =
                wb_User_Shared.Liste_Click(Nothing)
            End If
        End If

        'Wenn GruppenTexte geändert worden sind
        If UserGruppenGrid.HeaderChanged Then
            If Not SaveUserGruppenTexte() Then
                MsgBox("Fehler beim Speichern der Gruppen-Bezeichnungen.", MsgBoxStyle.Exclamation, "WinBack-AddIn")
            Else
                'Daten neu laden (Gruppentexte)
                wb_User_Shared.LoadGrpTexte()
                'Anzeige aktualisieren
                wb_User_Shared.Reload(Nothing)
            End If
        End If

    End Sub

    Private Function SaveUserGruppenRechte() As Boolean
        'Datenbank-Verbindung
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim sqlValues As String = ""

        With UserGruppenGrid

            'Flag Gruppen-Rechte wurden geändert
            For j = COLGrp To .ColumnCount - 1
                'wenn die Gruppen-Rechte für diese Spalte geändert worden sind
                If .Columns(j).HeaderCell.Tag = "C" Then
                    'wenn die Gruppe der aktuell angezeigten Gruppe entspricht, wird das entsprechende Fenster neu berechnet
                    wb_User_Shared.Gruppe.iGruppe = GrpNr(j)
                    'Marke wieder löschen
                    .Columns(j).HeaderCell.Tag = ""
                End If
            Next

            'Daten löschen
            If winback.sqlCommand(wb_Sql_Selects.sqlUserGrpRemoveAll) Then

                'alle Datensätze aus dem Grid nacheinander in die Datenbank wieder einfügen
                For i = 0 To .RowCount - 1
                    For j = COLGrp To .ColumnCount - 1
                        sqlValues = .Rows(i).Cells(COLTyp).Value & "," & .Rows(i).Cells(COLIDx).Value & "," & .Rows(i).Cells(COLInp).Value & "," &
                                     GrpIdx(j).ToString & "," & GrpNr(j).ToString & "," & .Rows(i).Cells(j).Value
                        winback.sqlCommand(wb_Functions.SetParams(wb_Sql_Selects.sqlUserGrpInsert, sqlValues))
                    Next
                Next
            Else
                winback.Close()
                Return False
            End If

        End With
        winback.Close()
        Return True
    End Function

    Private Function SaveUserGruppenTexte() As Boolean
        'Gruppen-Texte in OrgaBack sichern
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            Dim osUserGruppen As New wb_SyncUserGruppen_OrgaBack
            With UserGruppenGrid
                For i = COLGrp To .ColumnCount - 1
                    osUserGruppen.DBUpdate(GrpNr(i), .Columns(i).HeaderCell.Value, "")
                Next
            End With
            osUserGruppen = Nothing
        End If

        'Datenbank-Verbindung
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Daten löschen
        If winback.sqlCommand(wb_Functions.SetParams(wb_Sql_Selects.sqlUserGrpTexteRemove, wb_Language.GetLanguageNr())) >= 0 Then
            'alle Datensätze aus dem Grid nacheinander in die Datenbank wieder einfügen
            With UserGruppenGrid
                For i = COLGrp To .ColumnCount - 1
                    winback.sqlCommand(wb_Functions.SetParams(wb_Sql_Selects.sqlUserGrpTexteInsert, wb_Language.GetLanguageNr(), GrpNr(i), .Columns(i).HeaderCell.Value))
                Next
            End With
        Else
            winback.Close()
            Return False
        End If
        winback.Close()
        Return True
    End Function

    ''' <summary>
    ''' Berechnet die Gruppen-Nummer aus der Spalte im Grid.
    ''' Die letzte Spalte enthält die Rechte für die Admin-Gruppe (99)
    ''' </summary>
    ''' <param name="Col"></param>
    ''' <returns></returns>
    Private Function GrpNr(Col As Integer) As Integer
        If Col < COLAdm Then
            Return Col - COLGrp + 1
        Else
            Return wb_Global.AdminUserGrpe
        End If
    End Function

    ''' <summary>
    ''' Berechnet den Gruppen-Index aus der Spalte im Grid.
    ''' Die letzte Spalte enthält die Rechte für die Admin-Gruppe (99)
    ''' </summary>
    ''' <param name="Col"></param>
    ''' <returns></returns>
    Private Function GrpIdx(Col As Integer) As Integer
        If Col < COLAdm Then
            Return Col - COLGrp + 2 + wb_Global.AdminUserGrpe
        Else
            Return wb_Global.AdminUserGrpe
        End If
    End Function

End Class
