Public Class wb_User
    Private IP_lfdNr As String
    Private IP_Wert1int As String   'Passwort WinBack
    Private IP_Wert2int As String   'Personalnummer
    Private IP_Wert4Str As String   'Name
    Private IP_Wert5Str As String   'RFID-Code
    Private IP_ItemID As Integer    'Rechte-Gruppe

    Private _DataHasChanged As Boolean
    Private _GruppeHasChanged As Boolean

    ''' <summary>
    ''' Eine der Mitarbeiter-Eigenschaften wurde geändert
    ''' </summary>
    ''' <returns>
    ''' True  - Eingenschaften sind geändert worden, der Datensatz muss gespeichert werden
    ''' False - keine Änderung, kein Speichern notwendig
    '''     </returns>
    Public ReadOnly Property Changed As Boolean
        Get
            Return _DataHasChanged
        End Get
    End Property

    Public Sub Invalid()
        IP_Wert4Str = ""
        IP_Wert5Str = ""
        IP_ItemID = wb_Global.UNDEFINED
        IP_Wert1int = ""
        IP_Wert2int = ""

        _DataHasChanged = False
        _GruppeHasChanged = False
    End Sub

    ''' <summary>
    ''' Mitarbeiter-Name. String max 250 Zeichen
    ''' Tabelle winback.ItemParameter.IP_Wert4Str
    ''' </summary>
    ''' <returns></returns>
    Public Property Name As String
        Set(value As String)
            'TODO max.Länge Name prüfen ggf. Fehlermeldung ausgeben
            If value <> IP_Wert4Str AndAlso value <> "" Then
                _DataHasChanged = True
            End If
            IP_Wert4Str = wb_Functions.XRemoveSonderZeichen(value)
        End Set
        Get
            Return IP_Wert4Str
        End Get
    End Property

    ''' <summary>
    ''' Mitarbeiter-Personalnummer. String max 250 Zeichen
    ''' Tabelle winback.ItemParameter.IP_Wert2Int
    ''' </summary>
    ''' <returns></returns>
    Public Property PersonalNr As String
        Set(value As String)
            'TODO max.Länge Name prüfen ggf. Fehlermeldung ausgeben
            If value <> IP_Wert2int AndAlso value <> "" Then
                _DataHasChanged = True
            End If
            IP_Wert2int = wb_Functions.XRemoveSonderZeichen(value)
        End Set
        Get
            If IP_Wert2int = "0" Then
                Return ""
            Else
                Return IP_Wert2int
            End If
        End Get
    End Property

    ''' <summary>
    ''' Mitarbeiter-RFID/NFS. String max 250 Zeichen
    ''' Tabelle winback.ItemParameter.IP_Wert5Str
    ''' </summary>
    ''' <returns></returns>
    Public Property RFID As String
        Set(value As String)
            'TODO max.Länge Name prüfen ggf. Fehlermeldung ausgeben
            If value <> IP_Wert5Str AndAlso value <> "" Then
                _DataHasChanged = True
            End If
            'RFID-Eintrag löschen !
            If value = wb_Global.UNDEFINED.ToString Then
                value = ""
            End If
            IP_Wert5Str = wb_Functions.XRemoveSonderZeichen(value)
        End Set
        Get
            Return IP_Wert5Str
        End Get
    End Property

    ''' <summary>
    ''' Mitarbeiter-Gruppe. Numerisch maximal 10 Stellen
    ''' Tabelle winback.ItemParameter.IP_ItemID
    ''' </summary>
    ''' <returns></returns>
    Public Property iGruppe As Integer
        Set(value As Integer)
            'TODO max.Wert Gruppe prüfen ggf. Exception auslösen
            If value <> IP_ItemID AndAlso value <> 0 Then
                _DataHasChanged = True
                _GruppeHasChanged = True
            End If
            IP_ItemID = value
        End Set
        Get
            If IP_ItemID < 1 Then
                Return 1
            Else
                Return IP_ItemID
            End If
        End Get
    End Property

    ''' <summary>
    ''' Mitarbeiter Passwort. Numerisch maximal 10 Stellen
    ''' Tabelle winback.ItemParameter.IP_Wert1int
    ''' </summary>
    ''' <returns></returns>
    Public Property Passwort As String
        Set(value As String)
            'TODO max.Wert Passwort abfragen ggf. Fehlermeldung ausgeben
            If value <> IP_Wert1int AndAlso value <> "" Then
                If Not Me.Exist(value) Then
                    _DataHasChanged = True
                    IP_Wert1int = value
                End If
            End If
        End Set
        Get
            Return IP_Wert1int
        End Get
    End Property

    ''' <summary>
    ''' User-Detail-Daten laden
    ''' </summary>
    ''' <param name="dataGridView"> DataGrid Mitarbeiter-Liste</param>
    Friend Sub LoadData(dataGridView As wb_DataGridView)
        'lfd.Nummer
        IP_lfdNr = dataGridView.Field("IP_lfd_Nr")
        'Passwort
        IP_Wert1int = dataGridView.Field("IP_Wert1int")
        'Benutzer-Personalnummer
        IP_Wert2int = dataGridView.Field("IP_Wert2int")
        'Benutzer-Gruppe
        IP_ItemID = CInt(dataGridView.Field("IP_ItemID"))
        'Benutzer-Name
        IP_Wert4Str = dataGridView.Field("IP_Wert4str")
        'Benutzer-RFID
        IP_Wert5Str = dataGridView.Field("IP_Wert5str")
    End Sub

    ''' <summary>
    ''' User-Detail-Daten speichern. 
    ''' Gibt True zurück, wenn sich die Daten geändert haben
    ''' IP_lfd_Nr ist UniqueKey und entspricht dem Passwort
    ''' </summary>
    ''' <param name="dataGridView"> DataGrid Mitarbeiter-Liste</param>
    ''' <returns>
    ''' True  - Eigenschaften des aktuelle User haben sich geändert
    ''' False - Keine Änderung
    ''' </returns>
    Friend Function SaveData(dataGridView As wb_DataGridView) As Boolean
        If _DataHasChanged Then
            dataGridView.iField("IP_ItemID") = iGruppe
            dataGridView.Field("IP_lfd_Nr") = wb_Functions.StrToInt(IP_lfdNr)
            dataGridView.Field("IP_Wert1int") = wb_Functions.StrToInt(IP_Wert1int)
            dataGridView.Field("IP_Wert2int") = wb_Functions.StrToInt(IP_Wert2int)
            dataGridView.Field("IP_Wert4str") = IP_Wert4Str
            dataGridView.Field("IP_Wert5str") = IP_Wert5Str

            'wenn sich die Benutzergruppe geändert hat, muss in OrgaBack das MFF500 geschrieben werden
            If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack AndAlso _GruppeHasChanged Then
                msSQLUpdate(PersonalNr, iGruppe)
            End If

            'Reset Flags
            _DataHasChanged = False
            _GruppeHasChanged = False
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Update Benutzergruppe in OrgaBack 
    ''' Zunächst muss aus der Tabelle dbo.[Mitarbeiter] anhand der Personal-Nummer das Mitarbeiter-Kürzel ermittelt werden.
    ''' Anhand des Mitarbeit-Kürzels wird in der Tabelle dbo.[MitarbeiterHatMultifeld] dann das MFF500 mit der Gruppen-Nummer geschrieben
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Function msSQLUpdate(PersNr As String, GruppeNr As Integer) As Boolean
        'Verbindung zu MsSQL-Datenbank
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Gruppen-Nummer formatieren
        Dim sGrpNr As String
        sGrpNr = Strings.Right("0000" & GruppeNr.ToString, 4)

        'Mitarbeiter-Kürzel ermitteln
        If orgaback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlMitarbeiter, PersNr)) Then
            If orgaback.Read Then
                'Mitarbeiter-Kürzel
                Dim Mitarbeiter As String = orgaback.sField("MitarbeiterKürzel")
                orgaback.CloseRead()
                'Gruppe für Mitarbeiter mit diesem Kürzel updaten (wenn das Update fehlschlägt...)
                Try
                    orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlUpdateMitarbeiterMFF, Mitarbeiter, sGrpNr))
                Catch ex As Exception
                End Try
            End If
        End If

        'Datenbank-Verbindung wieder schliessen
        orgaback.Close()
        Return True
    End Function

    ''' <summary>
    ''' Neuen Mitarbeiter anlegen. In der Datenbank wird ein Dummy-Eintrag eingefügt
    ''' Die aufrufende Funktion muss für die Aktualisierung der offenen Views sorgen.
    ''' Die Mitarbeiter-Nummer ist Unique-Key. Wenn die Mitarbeiter-Nummer schon vorhanden ist, wird
    ''' False zurückgeliefert.
    ''' </summary>
    ''' <returns>
    ''' True - Einfügen war erfolgreich
    ''' False - Fehler beim Einfügen</returns>
    Public Function AddNew() As Boolean
        'Eventuell vorhandenen (Leer-)Datensatz löschen
        Delete(wb_Global.NewUserPass)
        'Dummy-Datensatz anlegen (User:Neu Pass:_1 Gruppe:1)
        Return AddNew(wb_Global.NewUserName, wb_Global.NewUserPass, wb_Global.NewUserGrpe)
    End Function

    ''' <summary>
    ''' Neuen Mitarbeiter anlegen.
    ''' Die Mitarbeiter-Nummer ist Unique-Key. Wenn die Mitarbeiter-Nummer schon vorhanden ist, wird
    ''' False zurückgeliefert.
    ''' Vor dem Speichern muss geprüft werden, ob die neue Personalnummer in WinBack schon als
    ''' Passwort(Mitarbeiter-Nummer) exisitiert. (Die neue Personal-Nummer ist der Default für das WinBack-Passwort)
    '''     Wenn ja, wird das Passwort solange um Eins erhöht, bis keine Duplikate mehr auftreten!
    ''' </summary>
    ''' <param name="Name"> String Mitarbeiter-Name</param>
    ''' <param name="PersonalNr"> String Mitarbeiter-PersonalNr numerisch max. 10 Stellen</param>
    ''' <param name="Gruppe">String - User-Gruppe (1..9, 99)</param>
    ''' <returns>
    ''' True - Einfügen war erfolgreich
    ''' False - Fehler beim Einfügen
    ''' </returns>
    Function AddNew(Name As String, PersonalNr As String, Gruppe As String) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Try
            'Prüfen ob die neue Personal-Nummer schon als Passwort in WinBack existiert
            Dim NewPasswort As Integer = wb_Functions.StrToInt(PersonalNr)
            'Wenn diese Nummer schon exisitiert, wird das Passwort um Eins erhöht
            While _Exists(NewPasswort.ToString, winback)
                NewPasswort += 1
            End While

            'Wenn die Gruppe ungültig ist wird die Default-Gruppe eingetragen
            If Gruppe <= 0 Then
                Gruppe = wb_Global.NewUserGrpe
            End If

            'Neuen Benutzer in Datenbank einfügen
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserInsert, Name, PersonalNr, Gruppe, NewPasswort.ToString))
        Catch
            'Verbindung wieder schliessen
            winback.Close()
            Return False
        End Try
        'Verbindung wieder schliessen
        winback.Close()
        Return True
    End Function

    ''' <summary>
    ''' Mitarbeiterdaten ändern.
    ''' </summary>
    ''' <param name="Name"> String Mitarbeiter-Name</param>
    ''' <param name="OldPersonalNr"> String Mitarbeiter-PersonalNr (alt) numerisch max. 10 Stellen</param>
    ''' <param name="NewPersonalNr"> String Mitarbeiter-PersonalNr (neu) numerisch max. 10 Stellen</param>
    ''' <returns>
    ''' True - Ändern war erfolgreich
    ''' False - Fehler beim Ändernn
    ''' </returns>
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Function Update(OldPersonalNr As String, Name As String, NewPersonalNr As String) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim ret As Boolean = False

        'Prüfen, ob die Personal-Nummer geändert werden soll
        If OldPersonalNr <> NewPersonalNr AndAlso OldPersonalNr <> "" Then
            Try
                'Prüfen ob schon ein Bneutzer mit der neuen Personal-Nummer existiert
                If Not _Exists(NewPersonalNr, winback) Then
                    'Update Benutzer in Datenbank
                    ret = (winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserPNrUpdate, Name, NewPersonalNr, OldPersonalNr)) > 0)
                Else
                    MsgBox("Diese Personal-Nummer existiert schon!", MsgBoxStyle.Critical, "Fehler Personal-Nummer")
                End If
            Catch
                Debug.Print("Fehler beim Ändern der Personal-Nummer")
            End Try
        Else
            Try
                'Die alte Personal-Nummer muss angegeben werden
                If OldPersonalNr <> "" Then
                    'Prüfen ob schon ein Bneutzer mit der neuen Personal-Nummer existiert
                    If _Exists(NewPersonalNr, winback) Then
                        'Update Benutzer in Datenbank - Die User-Gruppe wird nicht verändert
                        ret = (winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserUpdate, Name, NewPersonalNr, OldPersonalNr)) > 0)
                    Else
                        'Benutzer in WinBack neu anlegen
                        ret = AddNew(Name, NewPersonalNr, wb_Global.NewUserGrpe)
                    End If
                Else
                    'Benutzer in WinBack neu anlegen
                    ret = AddNew(Name, NewPersonalNr, wb_Global.NewUserGrpe)
                End If
            Catch
                'Verbindung wieder schliessen
                winback.Close()
                Return False
            End Try
        End If

        'Verbindung wieder schliessen
        winback.Close()
        Return ret
    End Function

    ''' <summary>
    ''' Eintrag Mitarbeiter löschen. Das Löschen der Mitarbeiter ist in WinBack unkritisch,
    ''' da in allen Verweisen auch der Name im Klartext mitgespeichert wird.
    ''' </summary>
    ''' <param name="PersonalNr"> String Mitarbeiter-Passwort numerisch max. 10 Stellen</param>
    Function Delete(Password As String, Optional PersonalNr As String = "") As Boolean
        Dim Result As Boolean = True
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Try
            If PersonalNr = "" Then
                'Benutzer aus Datenbank löschen (WinBack - Passwort)
                winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlWinBackUserDelete, Password))
            Else
                'Benutzer aus Datenbank löschen (OrgaBack - Personalnummer)
                winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlOrgaBackUserDelete, PersonalNr))
            End If
        Catch
            Result = False
        Finally
            winback.Close()
        End Try
        Return Result
    End Function

    ''' <summary>
    ''' Prüft ob ein Mitarbeiter mit [Passwort] schon existiert
    ''' </summary>
    ''' <param name="Passwort"></param>
    ''' <returns>
    ''' True - Passwort ist schon vergeben
    ''' False - Passwort ist nicht verwendet
    ''' </returns>
    Function Exist(Passwort As String, Optional RFID As String = "*") As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim Result As Boolean

        'Abfrage Passwort oder RFID
        If RFID = "*" Then
            Result = _Exists(Passwort, winback)
        Else
            Result = _ExistsID(RFID, winback)
        End If

        winback.Close()
        Return Result
    End Function

    Private Function _Exists(Passwort As String, winback As wb_Sql) As Boolean
        Dim i As Integer = 0

        Try
            'Zählt alle Benutzer mit diesem Passwort
            winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserExists, Passwort))
            If winback.Read Then
                i = winback.iField("IP_Cnt")
            End If
        Catch
            winback.CloseRead()
            Return True
        End Try
        winback.CloseRead()
        Return (i > 0)
    End Function

    Private Function _ExistsID(RFID As String, winback As wb_Sql) As Boolean
        Dim i As Integer = 0

        Try
            'Zählt alle Benutzer mit dieser ID
            winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserExistsID, RFID))
            If winback.Read Then
                i = winback.iField("IP_Cnt")
            End If
        Catch
            winback.CloseRead()
            Return True
        End Try
        winback.CloseRead()
        Return (i > 0)
    End Function
End Class
