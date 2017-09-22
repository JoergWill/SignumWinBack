
Public Class wb_Rezept_Shared

    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)

    Public Shared RzVariante As New SortedList
    Public Shared LinienGruppe As New SortedList

    Public Shared aktRzNr As Integer            'Rezept-Index
    Public Shared aktRzNummer As String         'Rezept-Nummer  (alpha)
    Public Shared aktRzName As String           'Rezept-Name    (alpha)
    Public Shared aktRzKommentar As String
    Public Shared aktRzGewicht As Double
    Public Shared aktRzLinienGrp As Integer
    Public Shared aktRzVariante As Integer

    Public Shared aktChangeNr As Integer
    Public Shared aktChangeDatum As String
    Public Shared aktChangeName As String

    Public Shared aktChargeMin As Double
    Public Shared aktChargeMax As Double
    Public Shared aktChargeOpt As Double

    Public Sub New()
        LoadVariantenTexte()
        LoadLinienGruppenTexte()

    End Sub

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

    Private Shared Sub LoadLinienGruppenTexte()
        'HashTable mit der Übersetzung der Liniengruppen-Nummer in die Liniengruppen-Bezeichnung laden
        'wenn die Liniengruppen-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(wb_globalsettings.SqlConWinBack, wb_globalsettings.WinBackDBType)
        winback.sqlSelect("SELECT LG_Nr, LG_Bezeichnung FROM LinienGruppen")
        LinienGruppe.Clear()
        While winback.Read
            LinienGruppe.Add(winback.iField("LG_Nr"), winback.sField("LG_Bezeichnung"))
        End While
        winback.Close()
    End Sub

    Public Shared Sub Liste_Click(sender As Object)
        RaiseEvent eListe_Click(sender)
    End Sub

    Public Shared Sub Edit_Leave(sender As Object)
        RaiseEvent eEdit_Leave(sender)
    End Sub
End Class
