Public Class wb_User_Gruppe
    Private _Rechte As New ArrayList
    Private _iGruppe As Integer = 0
    Private _GruppenBezeichnung As String = ""

    Public Sub LoadData(ByVal Gruppe As Integer)

        'Prüfen ob die Daten neu geladen werden müssen (Gruppe hat sich geändert)
        If Gruppe <> _iGruppe Then
            'Laden der Daten aus winback.ItemTypen winback.ItemIDs
            Dim GruppenRechte As wb_Global.wb_GruppenRechte
            _Rechte.Clear()
            _iGruppe = Gruppe

            Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
            If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserRechte, Gruppe, wb_Language.GetLanguageNr())) Then
                While winback.Read

                    'Rechtegruppe Bezeichnung
                    GruppenRechte.OberBegriff = winback.sField("IT_Bezeichnung")
                    GruppenRechte.Bezeichnung = wb_Language.TextFilter(winback.sField("II_Kommentar"))

                    'Eingabe-Typ
                    GruppenRechte.iAttrGrp = winback.iField("AT_Attr_Nr")

                    'Haupt- und Nebengruppe
                    GruppenRechte.iTyp = winback.iField("IP_ItemTyp")
                    GruppenRechte.iID = winback.iField("IP_ItemID")

                    'Parameter User-Berechtigung (ja/nein/dauerhaft...)
                    GruppenRechte.iAttribut = winback.iField("IP_Wert2int")
                    GruppenRechte.sAttribut = winback.sField("T_Text")

                    _Rechte.Add(GruppenRechte)
                End While
            End If
            winback.Close()
        End If
    End Sub

    Public ReadOnly Property UserRechte As ArrayList
        Get
            Return _Rechte
        End Get
    End Property

    Public ReadOnly Property GrpTyp As Integer


    Public ReadOnly Property Gruppe As Integer
        Get
            Return _iGruppe
        End Get
    End Property

    Public Property GruppenBezeichnung As String
        Get
            Return _GruppenBezeichnung
        End Get
        Set(value As String)
            _GruppenBezeichnung = value
        End Set
    End Property

    Public ReadOnly Property count As Integer
        Get
            Return _Rechte.Count
        End Get
    End Property

    Public ReadOnly Property OberBegriff(Index As Integer) As String
        Get
            If Index >= 0 And Index < count Then
                Return _Rechte(Index).Oberbegriff
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property Bezeichnung(Index As Integer) As String
        Get
            If Index >= 0 And Index < count Then
                Return _Rechte(Index).Bezeichnung
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property sAtttribut(Index As Integer) As String
        Get
            If Index >= 0 And Index < count Then
                Return _Rechte(Index).sAtttribut
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property iAtttribut(Hauptgruppe As Integer, Nebengruppe As Integer) As Integer
        Get
            For Each r As wb_Global.wb_GruppenRechte In _Rechte
                If r.iTyp = Hauptgruppe And r.iID = Nebengruppe Then
                    Return r.iAttribut
                End If
            Next
            Return wb_Global.UNDEFINED
        End Get
    End Property

    Public ReadOnly Property iAttrGrp(Hauptgruppe As Integer, Nebengruppe As Integer) As Integer
        Get
            For Each r As wb_Global.wb_GruppenRechte In _Rechte
                If r.iTyp = Hauptgruppe And r.iID = Nebengruppe Then
                    Return r.iAttrGrp
                End If
            Next
            Return wb_Global.UNDEFINED
        End Get
    End Property

End Class
