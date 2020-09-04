
Public Class wb_Rezept_Shared

    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)
    Public Shared Event eListe_Refresh(Sender As Object)

    Public Shared RzVariante As New SortedList
    Public Shared RzGruppe As New SortedList
    Public Shared Rezept As New wb_Rezept

    Shared Sub New()
        LoadVariantenTexte()
        LoadRezeptGruppenTexte()
    End Sub

    Public Shared Function Reload() As Boolean
        LoadVariantenTexte()
        LoadRezeptGruppenTexte()
        Return True
    End Function

    Private Shared Sub LoadVariantenTexte()
        'HashTable mit der Übersetzung der Variante-Nummer in die Varianten-Bezeichnung laden
        'wenn die Varianten-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(wb_globalsettings.SqlConWinBack, wb_globalsettings.WinBackDBType)
        winback.sqlSelect("SELECT RV_Nr, RV_Bezeichnung FROM RezeptVarianten")
        RzVariante.Clear()
        While winback.Read
            RzVariante.Add(winback.iField("RV_Nr"), winback.sField("RV_Bezeichnung"))
        End While
        winback.Close()
    End Sub

    ''' <summary>
    ''' Erzeugt eine SortedList mit der Zuordnung von Rezeptgruppen-Nummer zum Rezeptgruppen-Text.
    ''' Wird für die Auswahlfelder Rezeptgruppe verwendet.
    ''' </summary>
    Private Shared Sub LoadRezeptGruppenTexte()
        'HashTable mit der Übersetzung der Rezeptgruppen-Nummer in die Rezeptgruppen-Bezeichnung laden
        'wenn die Rezeptgruppen-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect("SELECT II_ItemID, II_Kommentar FROM ItemIDs WHERE II_ItemTyp = 230")
        RzGruppe.Clear()
        RzGruppe.Add(0, "")
        While winback.Read
            RzGruppe.Add(winback.iField("II_ItemID"), winback.sField("II_Kommentar"))
        End While
        winback.Close()
    End Sub

    Public Shared Sub Liste_Click(sender As Object)
        RaiseEvent eListe_Click(sender)
    End Sub

    Public Shared Sub Edit_Leave(sender As Object)
        RaiseEvent eEdit_Leave(sender)
    End Sub

    Public Shared Sub Liste_Refresh(sender As Object)
        RaiseEvent eListe_Refresh(sender)
    End Sub

End Class
