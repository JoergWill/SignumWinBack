Public Class wb_User_Gruppe
    Private _Rechte As New ArrayList
    Private _iGruppe As Integer = wb_Global.UNDEFINED
    Private _GruppenBezeichnung As String = ""
    Private _ErrorText As String = ""
    Private _UpdateDatabaseFile As String = ""

    Public ReadOnly Property ErrorText As String
        Get
            Return _ErrorText
        End Get
    End Property

    Public ReadOnly Property UpdateDatabaseFile As String
        Get
            Return _UpdateDatabaseFile
        End Get
    End Property

    ''' <summary>
    ''' Aktuell angezeigte/bearbeitet User-Gruppe
    ''' Wird die Gruppen-Nummer geschrieben und ist der Wert gleich der aktuellen Gruppe, wird die
    ''' Gruppen-Nummer auf ungültig gesetzt.
    ''' 
    ''' Der nächste LoadData-Befehl läde dann die aktualisierten Werte aus der Datenbank
    ''' </summary>
    ''' <returns></returns>
    Public Property iGruppe As Integer
        Get
            Return _iGruppe
        End Get
        Set(value As Integer)
            If value = _iGruppe Then
                _iGruppe = wb_Global.UNDEFINED
            End If
        End Set
    End Property

    Public Sub LoadData(ByVal Gruppe As Integer, RezeptGruppenRechte As Boolean)
        Dim LfdNr As Integer = wb_Global.UNDEFINED

        'Prüfen ob die Daten neu geladen werden müssen (Gruppe/Parameter der Gruppe haben sich geändert)
        If (Gruppe <> _iGruppe) Then
            'Laden der Daten aus winback.ItemTypen winback.ItemIDs
            Dim GruppenRechte As wb_Global.wb_GruppenRechte
            _Rechte.Clear()
            _iGruppe = Gruppe

            Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
            Dim sql As String

            'Anzeige Usergruppen-Rechte oder Rezeptgruppen zu Usergruppen
            If RezeptGruppenRechte Then
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezUserRechte, Gruppe, wb_Language.GetLanguageNr())
            Else
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserRechte, Gruppe, wb_Language.GetLanguageNr())
            End If
            If winback.sqlSelect(sql) Then
                While winback.Read

                    'Rechtegruppe Bezeichnung
                    GruppenRechte.OberBegriff = winback.sField("IT_Bezeichnung")
                    GruppenRechte.Bezeichnung = wb_Language.TextFilter(winback.sField("II_Kommentar"))
                    GruppenRechte.iWert1 = winback.iField("IP_Wert1int")
                    GruppenRechte.iLfdNr = winback.iField("IP_Lfd_Nr")
                    'Debug.Print("GruppenBezeichnung / Wert1Int " & GruppenRechte.Bezeichnung & " / " & GruppenRechte.iLfdNr)

                    'Nur den ersten Datensatz mit diesem Passwort einlesen (Falls Fehler in der Datenbank/doppelte Einträge)
                    If LfdNr = wb_Global.UNDEFINED Then
                        LfdNr = GruppenRechte.iLfdNr
                    End If

                    'Eingabe-Typ
                    GruppenRechte.iAttrGrp = winback.iField("AT_Attr_Nr")

                    'Haupt- und Nebengruppe
                    GruppenRechte.iTyp = winback.iField("IP_ItemTyp")
                    GruppenRechte.iID = winback.iField("IP_ItemID")

                    'Parameter User-Berechtigung (ja/nein/dauerhaft...)
                    GruppenRechte.iAttribut = winback.iField("IP_Wert2int")
                    GruppenRechte.sAttribut = winback.sField("T_Text")

                    'Nur den ersten Datensatz mit diesem Passwort einlesen (Falls Fehler in der Datenbank/doppelte Einträge)
                    If GruppenRechte.iLfdNr = LfdNr Then
                        _Rechte.Add(GruppenRechte)
                    End If

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

    Public ReadOnly Property Count As Integer
        Get
            Return _Rechte.Count
        End Get
    End Property

    Public ReadOnly Property OberBegriff(Index As Integer) As String
        Get
            If Index >= 0 AndAlso Index < Count Then
                Return _Rechte(Index).Oberbegriff
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property Bezeichnung(Index As Integer) As String
        Get
            If Index >= 0 AndAlso Index < Count Then
                Return _Rechte(Index).Bezeichnung
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property sAtttribut(Index As Integer) As String
        Get
            If Index >= 0 AndAlso Index < Count Then
                Return _Rechte(Index).sAtttribut
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property iAtttribut(Hauptgruppe As Integer, Nebengruppe As Integer) As Integer
        Get
            For Each r As wb_Global.wb_GruppenRechte In _Rechte
                If r.iTyp = Hauptgruppe AndAlso r.iID = Nebengruppe Then
                    Return r.iAttribut
                End If
            Next
            Return wb_Global.UNDEFINED
        End Get
    End Property

    Public ReadOnly Property iAttrGrp(Hauptgruppe As Integer, Nebengruppe As Integer) As Integer
        Get
            For Each r As wb_Global.wb_GruppenRechte In _Rechte
                If r.iTyp = Hauptgruppe AndAlso r.iID = Nebengruppe Then
                    Return r.iAttrGrp
                End If
            Next
            Return wb_Global.UNDEFINED
        End Get
    End Property

    Public Function CheckDB_Grp99() As Boolean
        'User-Gruppe 99 (alle Rechte) einlesen
        LoadData(wb_Global.AdminUserGrpe, False)

        If Not Count >= 44 Then
            'Datenbank-UpdateFile (Update WinBack.Datenbank kann das Problem lösen)
            _UpdateDatabaseFile = "2.30_UserAlleRechte.sql"
            _ErrorText = "Fehler in Tabelle ItemParameter - Usergruppen-Rechte(99) - Datensätze fehlen !"
            Trace.WriteLine("@E_" & _ErrorText)
            Return False
        End If

        'Kein Fehler 
        _ErrorText = ""
        Return True
    End Function

End Class
