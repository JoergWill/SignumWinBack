
Public Class wb_Rezept_Shared

    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)
    Public Shared Event eListe_Refresh(Sender As Object)
    Public Shared Event eRezept_Copy(Sender As Object, RzNr As Integer, Variante As Integer)

    Public Shared RzVariante As New SortedList
    Public Shared RzGruppe As New SortedList
    Public Shared ProdStufeText As New List(Of String)
    Public Shared Rezept As New wb_Rezept

    Enum AnzeigeFilter
        Undefined   ' nicht definiert
        Alle        ' alle Rezepte
        Produktion  ' alle Rezepte Produktion
        Sauerteig   ' alle Rezepte Sauerteig
    End Enum

    Shared Sub New()
        LoadVariantenTexte()
        LoadRezeptGruppenTexte()
        LoadProduktionsStufenTexte()
    End Sub

    Public Shared Sub Invalid()
        Rezept.Invalid()
    End Sub

    Public Shared Function Reload() As Boolean
        LoadVariantenTexte()
        LoadRezeptGruppenTexte()
        LoadProduktionsStufenTexte()
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

    ''' <summary>
    ''' Erzeugt ein String-Array mit vorgegebenen Texten für die Rezepteingabe der Produktions-Stufen.
    ''' Die Texte kommen aus der Tabelle winback.ItemParameter mit IP_Item_Typ=3010
    ''' </summary>
    Private Shared Sub LoadProduktionsStufenTexte()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlTexteProdStufe)
        ProdStufeText.Clear()
        While winback.Read
            ProdStufeText.Add(winback.sField("IP_Wert4str"))
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

    Public Shared Sub Rezept_Copy(sender As Object, RzNr As Integer, Variante As Integer)
        RaiseEvent eRezept_Copy(sender, RzNr, Variante)
    End Sub

End Class
