Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_User_GruppenRechte
    Inherits DockContent

    Const COLTyp = 0   'Hauptgruppe (1,2,200)
    Const COLIDx = 1   'Untergruppe (1..x)
    Const COLInp = 2   'EingabeTyp (403,405,406...)
    Const COLGrp = 3   'Erste Spalte Gruppe 1....
    Const COLAdm = 11  'Spalte Admin-Rechte

    Private ReadOnly _RezeptGruppenRechte As Boolean
    Private ReadOnly UserGruppenListe As New ArrayList
    Private UserGruppenGrid As wb_ArrayGridViewUserGruppen
    Private UserGruppe As wb_User_Gruppe

    ''' <summary>
    ''' Verwaltung der Benutzer-Gruppen-Rechte und der Benutzer-Rezeptgruppen-Rechte.
    ''' 
    ''' Die Benutzergruppen-Rechte werden in der Tabelle winack.ItemParameter definiert
    ''' Für jede Benutzergruppe (0..7 und 99) werden die einzelnen Berechtigungen je in
    ''' einem Datensatz gespeichert.
    ''' 
    ''' Die Tabelle winback.ItemIDs enthält die Auflistung aller Rechte
    ''' 
    '''     IP_ItemTyp  IP_ItemID    IP_ItemAttr AT_ParamNr [Text]
    '''     ---------------------------------------------------------------------------------------
    '''     Sauerteig(Hauptmenu)
    '''         1             0           405       1       Behälter                nicht verfügbar
    '''         1             0           405       2       Behälter                verfügbar
    '''         1             1           405       1       Zeiten                  nicht verfügbar
    '''         1             1           405       2       Zeiten                  verfügbar
    '''         1             2           405       1          -                    nicht verfügbar
    '''         1             2           405       2          -                    verfügbar
    '''         1             3           405       1       Rezepte                 nicht verfügbar
    '''         1             3           405       2       Rezepte                 verfügbar
    '''         1             4           405       1          -                    nicht verfügbar
    '''         1             4           405       2          -                    verfügbar
    '''         1             5           405       1       Service                 nicht verfügbar
    '''         1             5           405       2       Service                 verfügbar
    '''         1             6           405       1       Install                 nicht verfügbar
    '''         1             6           405       2       Install                 verfügbar
    '''         1             7           405       1       Hilfe                   nicht verfügbar
    '''         1             7           405       2       Hilfe                   verfügbar
    '''       -------------------------------------------------------------------------------------
    '''       Produktion(Hauptmenu)
    '''         2             0           405       1       Produkt                 nicht verfügbar
    '''         2             0           405       2       Produkt                 verfügbar
    '''         2             1           405       1       Chargen                 nicht verfügbar
    '''         2             1           405       2       Chargen                 verfügbar
    '''         2             2           405       1       Artikel                 nicht verfügbar
    '''         2             2           405       2       Artikel                 verfügbar
    '''         2             3           405       1       Rezepte                 nicht verfügbar
    '''         2             3           405       2       Rezepte                 verfügbar
    '''         2             4           405       1       Material                nicht verfügbar
    '''         2             4           405       2       Material                verfügbar
    '''         2             5           405       1       Service                 nicht verfügbar
    '''         2             5           405       2       Service                 verfügbar
    '''         2             6           405       1       Install                 nicht verfügbar
    '''         2             6           405       2       Install                 verfügbar
    '''         2             7           405       1       Hilfe                   nicht verfügbar
    '''         2             7           405       2       Hilfe                   verfügbar
    '''       --------------------------------------------------------------------------------------
    '''       WinBack-Büro(Rechte)
    '''         2            10           405       1       Rezepte readonly        aus
    '''         2            10           405       2       Rezepte readonly        ein
    '''         2            11           405       1       Rohstoffe readonly      aus
    '''         2            11           405       2       Rohstoffe readonly      ein
    '''         2            12           405       1       Rohst.Flags ändern      nicht verfügbar
    '''         2            12           405       2       Rohst.Flags ändern      verfügbar
    '''         2            13           405       1       Rzpt.V1 readonly        aus
    '''         2            13           405       2       Rzpt.V1 readonly        ein
    '''         2            14           405       1       Rzpt.spz.Rohstoffe      aus
    '''         2            14           405       2       Rzpt.spz.Rohstoffe      ein
    '''       
    '''         2            20           405       1       Benutzer                nicht verfügbar
    '''         2            20           405       2       Benutzer                verfügbar
    '''         2            21           405       1       VNC                     nicht verfügbar
    '''         2            21           405       2       VNC                     verfügbar
    '''       --------------------------------------------------------------------------------------
    '''       WinBack-Büro(Lizenzen)
    '''         2            22           405       1       Statistik               nicht verfügbar
    '''         2            22           405       2       Statistik               verfügbar
    '''         2            23           405       1       Historie                nicht verfügbar
    '''         2            23           405       2       Historie                verfügbar
    '''         2            24           405       1       Import                  nicht verfügbar
    '''         2            24           405       2       Import                  verfügbar
    '''         2            25           405       1       Excel                   nicht verfügbar
    '''         2            25           405       2       Excel                   verfügbar
    '''         2            26           405       1       Bakelink                nicht verfügbar
    '''         2            26           405       2       Bakelink                verfügbar
    '''         2            27           405       1       Bestellwesen            nicht verfügbar
    '''         2            27           405       2       Bestellwesen            verfügbar
    '''         2            28           405       1       Nährwerte               nicht verfügbar
    '''         2            28           405       2       Nährwerte               verfügbar
    '''         2            29           405       1       Nährwerte-Cloud         nicht verfügbar
    '''         2            29           405       2       Nährwerte-Cloud         verfügbar
    '''         2            30           405       1       Produktionsplanung      nicht verfügbar
    '''         2            30           405       2       Produktionsplanung      verfügbar
    '''         2            31           405       1       Planung Verbuchen       nicht verfügbar
    '''         2            31           405       2       Planung Verbuchen       verfügbar
    '''       --------------------------------------------------------------------------------------
    '''       Produktion(Rechte)
    '''         200           0           404       1       Ändern Wassermenge      nein
    '''         200           0           404       2       Ändern Wassermenge      einmalig
    '''         200           0           404       3       Ändern Wassermenge      dauerhaft
    '''         200           1           403       1       Ändern Eismenge         nein
    '''         200           1           403       2       Ändern Eismenge         einmalig
    '''         200           1           403       3       Ändern Eismenge         dauerhaft
    '''         200           2           402       1       Chargen löschen         nein
    '''         200           2           402       2       Chargen löschen         ja
    '''         200           3           402       1       Handwaage-Freigabe      nein
    '''         200           3           402       2       Handwaage-Freigabe      ja
    '''         200           4           402       1       Alternativ Rohstoff     nein
    '''         200           4           402       2       Alternativ Rohstoff     ja
    '''         200           5           402       1       RMF ein/aus             nein
    '''         200           5           402       2       RMF ein/aus             ja
    '''         200           6           402       1       Störung quittieren      nein
    '''         200           6           402       2       Störung quittieren      ja
    '''         200           7           402       1       Quitt. Tischwaage       nein
    '''         200           7           402       2       Quitt. Tischwaage       ja
    '''         200           8           404       1       Ändern Wassertemp       nein
    '''         200           8           404       2       Ändern Wassertemp       einmalig
    '''         200           8           404       3       Ändern Wassertemp       dauerhaft
    '''         200           9           402       1       Chargengröße ändern     nein
    '''         200           9           402       2       Chargengröße ändern     ja
    '''         200           11          402       1       Freigabe "Start"        nein
    '''         200           11          402       2       Freigabe "Start"        ja
    '''         200           12          402       1       Freigabe "Handwaage"    nein
    '''         200           12          402       2       Freigabe "Handwaage"    ja
    '''         200           13          402       1       Freigabe "Mehlwaage"    nein
    '''         200           13          402       2       Freigabe "Mehlwaage     ja
    '''         200           14          402       1       Freigabe "BW"           nein
    '''         200           14          402       2       Freigabe "BW"           ja
    '''         200           15          402       1       Freigabe "MK"           nein
    '''         200           15          402       2       Freigabe "MK"           ja
    '''         200           16          402       1       Freigabe "KKA"          nein
    '''         200           16          402       2       Freigabe "KKA"          ja
    '''         200           17          402       1       Freigabe "Wasser"       nein
    '''         200           17          402       2       Freigabe "Wasser"       ja
    '''         200           18          402       1       Freigabe "Kneter"       nein
    '''         200           18          402       2       Freigabe "Kneter"       ja
    '''         200           19          402       1       Freigabe "Neu"          nein
    '''         200           19          402       2       Freigabe "Neu"          ja
    '''         200           20          402       1       Freigabe "Löschen"      nein
    '''         200           20          402       2       Freigabe "Löschen"      ja
    '''         200           21          402       1       Freigabe "Übernehmen"   nein
    '''         200           21          402       2       Freigabe "Übernehmen"   ja
    '''         200           22          402       1       Freigabe "Temperatur"   nein
    '''         200           22          402       2       Freigabe "Temperatur"   ja
    '''         --------------------------------------------------------------------------------------
    '''         
    ''' 
    ''' 
    ''' 
    ''' 
    ''' Die Rezeptgruppen können aktuell nicht sinnvoll eingesetzt werden, da über die
    ''' Artikelauswahl keine Einschränkung nach Rezeptgruppen erfolgt und damit ein
    ''' Schutz der Rezepturen nicht gewährleistet ist.
    ''' Die einzig sinnvolle Anwendung ist der Schutz der Rezepte vor Änderungen abhängig 
    ''' von Benutzergruppen (z.B. Konditorei und Brot/Brötchen-Linie)
    ''' 
    ''' Grundsätzlich muss für JEDE Benutzergruppe mindestens EIN Eintrag in der Tabelle
    ''' winback.ItemParameter mit IP_ItemTyp=230 (RezeptgruppenRechte) vorhanden sein:
    ''' 
    '''     IP_ItemTyp  IP_ItemID   IP_ItemAttr IP_Lfd_Nr   IP_Wert1int
    '''       230          0            405         1           1    
    '''       230          0            405         2           2    
    '''       230          0            405         3           3    
    '''       230          0            405         4           4    
    '''       230          0            405         5           5    
    '''       230          0            405         6           6    
    '''       230          0            405         7           7    
    '''       230          0            405         8           8   
    '''       230          0            405        99          99    
    ''' 
    '''     IP_Wert1int -   UserGruppe
    '''     IP_ItemID   -   RezeptGruppe
    '''     
    ''' Der Wert IP_ItemID=0 entpricht dem Default-Wert. Rezepte mit RezeptGruppe=0 können immer
    ''' von allen Benutzern gelesen werden, die einen beliebigen Eintrag in ItemParameter haben.
    ''' 
    ''' </summary>
    ''' <param name="RezeptGruppenRechte"></param>
    Public Sub New(Optional RezeptGruppenRechte As Boolean = False)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        _RezeptGruppenRechte = RezeptGruppenRechte

        'Anzeige der Rezeptgruppen zu User-Gruppen anstatt der Programm-Rechte
        Select Case _RezeptGruppenRechte
            Case True
                Me.Text = "Rechte-Matrix der Rezept-Gruppen"
            Case False
                Me.Text = "Rechte-Matrix der Benutzer-Gruppen"
        End Select

    End Sub

    ''' <summary>
    ''' Grid der Gruppen-Rechte laden und anzeigen.
    ''' Die Verarbeitung von Anzeige und Edit erfolgt in der Klasse wb_ArrayGridViewUserGruppen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_User_GruppenRechte_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Spalten-Überschriften
        Dim sColNames As New List(Of String) From {"", "", ""}

        'Überschriften aus Tabelle Texte
        If Not LoadUserGruppenTexte(sColNames) Then
            'Überschriften aus Tabelle IAListe.Kommentar
            LoadUserGruppenKommentar(sColNames)
        End If

        'Prüfen ob Daten vorhanden sind
        If UserGruppenListe.Count > 0 Then
            'Gruppen-Rechte im Grid anzeigen
            UserGruppenGrid = New wb_ArrayGridViewUserGruppen(UserGruppenListe, sColNames) With {.BackgroundColor = Me.BackColor}
            UserGruppenGrid.GridLocation(pnlUserGruppenRechte)
            UserGruppenGrid.PerformLayout()
        End If

    End Sub

    ''' <summary>
    ''' Laden der Benutzer-Gruppen-Name (Bezeichnungstexte) aus der Texte-Tabelle
    ''' </summary>
    ''' <param name="sColnames"></param>
    ''' <returns></returns>
    Private Function LoadUserGruppenTexte(ByRef sColnames As List(Of String)) As Boolean
        'Erst mal sind keine Datensätze vorhanden
        Dim Result As Boolean = False

        'User-Gruppen aus Tabelle winback.IAListe
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        If winback.sqlSelect(wb_Functions.SetParams(wb_Sql_Selects.sqlUserGrpListeA, wb_Language.GetLanguageNr())) Then
            'alle Datensätze einlesen
            While winback.Read
                'Gruppen-Hauptbezeichnung(Oberbegriff)
                UserGruppe = New wb_User_Gruppe With {.GruppenBezeichnung = winback.sField("T_Text")}
                sColnames.Add(UserGruppe.GruppenBezeichnung)
                'Gruppen-Nummer - Alle Rechte für diese Gruppe laden
                UserGruppe.LoadData(winback.iField("IAL_ItemID"), _RezeptGruppenRechte)
                'Rechte-Objekt speichern
                UserGruppenListe.Add(UserGruppe)
                Result = True
            End While
            winback.Close()
        Else
            'Fehler bei der Abfrage der Daten
            Result = False
        End If
        Return Result
    End Function

    ''' <summary>
    ''' Laden der Benutzer-Gruppen-Name (Bezeichnungstexte) aus der Kommentar-Spalte der Tabelle IAListe
    ''' </summary>
    ''' <param name="sColnames"></param>
    ''' <returns></returns>
    Private Function LoadUserGruppenKommentar(ByRef sColnames As List(Of String)) As Boolean
        'Erst mal sind keine Datensätze vorhanden
        Dim Result As Boolean = False

        'User-Gruppen aus Tabelle winback.IAListe
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        If winback.sqlSelect(wb_Sql_Selects.sqlUserGrpListeB) Then
            'alle Datensätze einlesen
            While winback.Read
                'Gruppen-Hauptbezeichnung(Oberbegriff)
                UserGruppe = New wb_User_Gruppe With {.GruppenBezeichnung = winback.sField("IAL_Kommentar")}
                sColnames.Add(UserGruppe.GruppenBezeichnung)
                'Gruppen-Nummer - Alle Rechte für diese Gruppe laden
                UserGruppe.LoadData(winback.iField("IAL_ItemID"), _RezeptGruppenRechte)
                'Rechte-Objekt speichern
                UserGruppenListe.Add(UserGruppe)
                Result = True
            End While
            winback.Close()
        Else
            'Fehler bei der Abfrage der Daten
            Result = False
        End If
        Return Result
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

    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Private Sub SaveData()
        If UserGruppenGrid IsNot Nothing Then
            'wenn Daten geändert worden sind, muss gespeichert werden
            If UserGruppenGrid.Changed Then
                If _RezeptGruppenRechte Then
                    If Not SaveUserRezGruppenRechte() Then
                        MsgBox("Fehler beim Löschen/Speichern der Rezept-Gruppen.", MsgBoxStyle.Exclamation, "WinBack-AddIn")
                    End If
                Else
                    If Not SaveUserGruppenRechte() Then
                        MsgBox("Fehler beim Löschen/Speichern der Benutzer-Gruppen.", MsgBoxStyle.Exclamation, "WinBack-AddIn")
                    Else
                        'Anzeige aktualisieren
                        'wb_User_Shared.Gruppe.iGruppe =
                        wb_User_Shared.Liste_Click(Nothing)
                    End If
                End If
            End If

            'Wenn GruppenTexte geändert worden sind
            If UserGruppenGrid.HeaderChanged Then
                If Not SaveUserGruppenTexte() Then
                    MsgBox("Fehler beim Speichern der Gruppen-Bezeichnungen.", MsgBoxStyle.Exclamation, "WinBack-AddIn")
                Else
                    'Daten neu laden (Gruppentexte)
                    wb_User_Shared.Reload()
                    'Anzeige aktualisieren
                    wb_User_Shared.Reload(Nothing)
                End If
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

    Private Function SaveUserRezGruppenRechte() As Boolean
        'Datenbank-Verbindung
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim sqlValues As String = ""

        With UserGruppenGrid

            'Daten löschen
            If winback.sqlCommand(wb_Sql_Selects.sqlUserRezGrpRemoveAll) Then

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
