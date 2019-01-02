Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_OrgaBackParams
    Inherits DockContent
    Dim FilialListe As wb_ArrayGridViewSortimente
    Dim ArtikelGruppenListe As wb_ArrayGridViewSortimente
    Dim ArtikelGruppen As ArrayList
    Dim sColNames As New List(Of String)

    Private Sub wb_Admin_OrgaBackParams_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Sortimente in OrgaBack mit Filiale Typ Produktion
        Show_OrgaBackSortimente()
        'Liste der ArtikelGruppen in OrgaBack mit der Entsprechung in WinBack
        Show_OrgaBackArtikelGruppen()
    End Sub

    Private Sub Show_OrgaBackSortimente()
        'Liste der Tabellen-Überschriften
        sColNames.Clear()
        sColNames.AddRange({"Sortiment", "Bezeichnung", "&Filiale"})


        'Daten im Grid anzeigen
        FilialListe = New wb_ArrayGridViewSortimente(wb_Filiale.aSortiment, sColNames)
        FilialListe.ScrollBars = ScrollBars.Vertical
        FilialListe.BackgroundColor = Me.BackColor
        FilialListe.GridLocation(pnlSortimente)
        FilialListe.PerformLayout()
        FilialListe.Refresh()
    End Sub

    Private Sub Show_OrgaBackArtikelGruppen()
        'Artikelgruppen aus OrgaBack
        Dim ArtikelGruppe As wb_Global.OrgaBackSortiment
        Dim Artikelgruppen As New ArrayList

        'Liste der Tabellen-Überschriften
        sColNames.Clear()
        sColNames.AddRange({"Nr", "Artikelgruppe", "&WinBack"})

        'Datenbank-Verbindung öffnen - MsSQL
        Dim OrgasoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)

        'Prüfen ob der Artikel in OrgaBack existiert
        OrgasoftMain.sqlSelect(wb_Sql_Selects.mssqlSelArtikelGruppe)
        While OrgasoftMain.Read
            ArtikelGruppe.Srt = OrgasoftMain.sField("Artikelgruppe")
            ArtikelGruppe.SName = OrgasoftMain.sField("Bezeichnung")
            'Abgleich mit WinBack-Konstanten (winback.ini)
            Select Case ArtikelGruppe.Srt
                Case wb_GlobalSettings.OsGrpBackwaren
                    ArtikelGruppe.FName = "WinBack-Artikel"
                Case wb_GlobalSettings.OsGrpRohstoffe
                    ArtikelGruppe.FName = "WinBack-Rohstoff"
                Case Else
                    ArtikelGruppe.FName = ""
            End Select

            'zum Array anfügen
            ArtikelGruppen.Add(ArtikelGruppe)
        End While

        'Daten im Grid anzeigen
        ArtikelGruppenListe = New wb_ArrayGridViewSortimente(ArtikelGruppen, sColNames)
        ArtikelGruppenListe.ScrollBars = ScrollBars.Vertical
        ArtikelGruppenListe.BackgroundColor = Me.BackColor
        ArtikelGruppenListe.GridLocation(pnlArtikelGruppen)
        ArtikelGruppenListe.PerformLayout()
        ArtikelGruppenListe.Refresh()

    End Sub
End Class
