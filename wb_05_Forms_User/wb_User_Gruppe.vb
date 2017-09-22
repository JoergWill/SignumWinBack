Public Class wb_User_Gruppe
    Dim Rechte As New ArrayList
    Dim iGruppe As Integer = 0

    Public Sub LoadData(ByVal Gruppe As Integer)

        'Prüfen ob die Daten neu geladen werden müssen (Gruppe hat sich geändert)
        If Gruppe <> iGruppe Then
            'Laden der Daten aus winback.ItemTypen winback.ItemIDs
            Dim GruppenRechte As wb_Global.wb_GruppenRechte
            Rechte.Clear()
            iGruppe = Gruppe

            Dim winback As New wb_Sql(wb_globalsettings.SqlConWinBack, wb_globalsettings.WinBackDBType)
            If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserRechte, Gruppe, wb_Konfig.GetLanguageNr())) Then
                While winback.Read
                    GruppenRechte.OberBegriff = winback.sField("IT_Bezeichnung")
                    GruppenRechte.Bezeichnung = wb_Functions.TextFilter(winback.sField("II_Kommentar"))
                    GruppenRechte.sAttribut = winback.sField("T_Text")
                    GruppenRechte.iAttribut = winback.iField("AT_Wert2int")
                    Rechte.Add(GruppenRechte)
                End While
            End If
            winback.Close()
        End If
    End Sub

    Public ReadOnly Property UserRechte As ArrayList
        Get
            Return Rechte
        End Get
    End Property

    Public ReadOnly Property Gruppe As Integer
        Get
            Return iGruppe
        End Get
    End Property

    Public ReadOnly Property count As Integer
        Get
            Return Rechte.Count
        End Get
    End Property

    Public ReadOnly Property OberBegriff(Index As Integer) As String
        Get
            If Index >= 0 And Index < count Then
                Return Rechte(Index).Oberbegriff
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property Bezeichnung(Index As Integer) As String
        Get
            If Index >= 0 And Index < count Then
                Return Rechte(Index).Bezeichnung
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property sAtttribut(Index As Integer) As String
        Get
            If Index >= 0 And Index < count Then
                Return Rechte(Index).sAtttribut
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property iAtttribut(Index As Integer) As Integer
        Get
            If Index >= 0 And Index < count Then
                Return Rechte(Index).iAtttribut
            Else
                Return 0
            End If
        End Get
    End Property

End Class
