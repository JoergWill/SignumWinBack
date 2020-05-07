Public Class wb_User
    Private IP_Wert4Str As String
    Private IP_Wert5Str As String
    Private IP_ItemID As Integer
    Private IP_Wert1int As String
    Private IP_Wert2int As String
    Private DataHasChanged As Boolean
    ''' <summary>
    ''' Eine der Mitarbeiter-Eigenschaften wurde geändert
    ''' </summary>
    ''' <returns>
    ''' True  - Eingenschaften sind geändert worden, der Datensatz muss gespeichert werden
    ''' False - keine Änderung, kein Speichern notwendig
    '''     </returns>
    Public ReadOnly Property Changed As Boolean
        Get
            Return DataHasChanged
        End Get
    End Property

    ''' <summary>
    ''' Mitarbeiter-Name. String max 250 Zeichen
    ''' Tabelle winback.ItemParameter.IP_Wert4Str
    ''' </summary>
    ''' <returns></returns>
    Public Property Name As String
        Set(value As String)
            'TODO max.Länge Name prüfen ggf. Fehlermeldung ausgeben
            If value <> IP_Wert4Str And value <> "" Then
                DataHasChanged = True
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
            If value <> IP_Wert2int And value <> "" Then
                DataHasChanged = True
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
            If value <> IP_Wert5Str And value <> "" Then
                DataHasChanged = True
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
            If value <> IP_ItemID And value <> 0 Then
                DataHasChanged = True
            End If
            IP_ItemID = value
        End Set
        Get
            Return IP_ItemID
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
            If value <> IP_Wert1int And value <> "" Then
                If Not Me.Exist(value) Then
                    DataHasChanged = True
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
        'Benutzer-Name
        IP_Wert4Str = dataGridView.Field("IP_Wert4str")
        'Benutzer-Personalnummer
        IP_Wert5Str = dataGridView.Field("IP_Wert2int")
        'Benutzer-Gruppe
        IP_ItemID = CInt(dataGridView.Field("IP_ItemID"))
        'Passwort
        IP_Wert1int = dataGridView.Field("IP_Wert1int")
        'Personal-Nummer
        IP_Wert2int = dataGridView.Field("IP_Wert2int")
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
        If DataHasChanged Then
            dataGridView.Field("IP_Wert4str") = IP_Wert4Str
            dataGridView.Field("IP_Wert5str") = IP_Wert5Str
            dataGridView.Field("IP_ItemID") = wb_Functions.StrToInt(IP_ItemID)
            dataGridView.Field("IP_lfd_Nr") = wb_Functions.StrToInt(IP_Wert1int)
            dataGridView.Field("IP_Wert1int") = wb_Functions.StrToInt(IP_Wert1int)
            dataGridView.Field("IP_Wert2int") = wb_Functions.StrToInt(IP_Wert2int)
            DataHasChanged = False
            Return True
        Else
            Return False
        End If
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
        Return AddNew(wb_Global.NewUserName, wb_Global.NewUserPass, wb_Global.NewUserPass, wb_Global.NewUserGrpe)
    End Function

    ''' <summary>
    ''' Neuen Mitarbeiter anlegen.
    ''' Die Mitarbeiter-Nummer ist Unique-Key. Wenn die Mitarbeiter-Nummer schon vorhanden ist, wird
    ''' False zurückgeliefert.
    ''' </summary>
    ''' <param name="Name"> String Mitarbeiter-Name</param>
    ''' <param name="PersonalNr"> String Mitarbeiter-PersonalNr numerisch max. 10 Stellen</param>
    ''' <param name="Passwort"> String Mitarbeiter-Passwort numerisch max. 10 Stellen</param>
    ''' <param name="Gruppe">String - User-Gruppe (1..9, 99)</param>
    ''' <returns>
    ''' True - Einfügen war erfolgreich
    ''' False - Fehler beim Einfügen
    ''' </returns>
    Function AddNew(Name As String, PersonalNr As String, Passwort As String, Gruppe As String) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Try
            'Neuen Benutzer in Datenbank einfügen
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserInsert, Name, Passwort, Gruppe))
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
    ''' <param name="Gruppe">String - User-Gruppe (1..9, 99)</param>
    ''' <returns>
    ''' True - Ändernn war erfolgreich
    ''' False - Fehler beim Ändernn
    ''' </returns>
    Function Update(OldPersonalNr As String, Name As String, NewPersonalNr As String, Gruppe As String) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim ret As Integer = 0
        Try
            'Update Benutzer in Datenbank
            ret = winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserUpdate, Name, NewPersonalNr, Gruppe, OldPersonalNr))
        Catch
            'Verbindung wieder schliessen
            winback.Close()
            Return False
        End Try
        'Verbindung wieder schliessen
        winback.Close()
        Return (ret > 0)
    End Function

    ''' <summary>
    ''' Mitarbeiter-PersonalNr ändern. Wenn keine alte Personal-Nr angegeben ist, wird nach dem Namen gesucht.(Sync)
    ''' </summary>
    ''' <param name="Name"> String Mitarbeiter-Name</param>
    ''' <param name="OldPersonalNr"> String Mitarbeiter-PersonalNr (alt) numerisch max. 10 Stellen</param>
    ''' <param name="NewPersonalNr"> String Mitarbeiter-PersonalNr (neu) numerisch max. 10 Stellen</param>
    ''' <returns>
    ''' True - Ändernn war erfolgreich
    ''' False - Fehler beim Ändernn
    ''' </returns>
    Function Update(OldPersonalNr As String, Name As String, NewPersonalNr As String) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Try
            'Update Benutzer in Datenbank
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserPNrUpdate, Name, NewPersonalNr, OldPersonalNr))
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
    ''' Eintrag Mitarbeiter löschen. Das Löschen der Mitarbeiter ist in WinBack unkritisch,
    ''' da in allen Verweisen auch der Name im Klartext mitgespeichert wird.
    ''' </summary>
    ''' <param name="PersonalNr"> String Mitarbeiter-Passwort numerisch max. 10 Stellen</param>
    Sub Delete(Password As String, Optional PersonalNr As String = "")
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Try
            If PersonalNr <> "" Then
                'Benutzer aus Datenbank löschen (WinBack - Passwort)
                winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlWinBackUserDelete, PersonalNr))
            Else
                'Benutzer aus Datenbank löschen (OrgaBack - Personalnummer)
                winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlOrgaBackUserDelete, PersonalNr))
            End If
        Catch
        Finally
            winback.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Prüft ob ein Mitarbeiter mit [Passwort] schon existiert
    ''' </summary>
    ''' <param name="Passwort"></param>
    ''' <returns>
    ''' True - Passwort ist schon vergeben
    ''' False - Passwort ist nicht verwendet
    ''' </returns>
    Function Exist(Passwort As String) As Boolean
        Dim winback As New wb_Sql(wb_globalsettings.SqlConWinBack, wb_globalsettings.WinBackDBType)
        Dim i As Integer = 0

        Try
            'Zählt alle Benutzer mit diesem Passwort
            winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserExists, Passwort))
            If winback.Read Then
                i = winback.iField("IP_Cnt")
            End If
        Catch
        Finally
            winback.Close()
        End Try
        Return (i > 0)
    End Function
End Class
