Public Class wb_User
    Private IP_Wert4Str As String
    Private IP_ItemID As Integer
    Private IP_Wert1int As String
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
            If value <> IP_Wert4Str Then
                DataHasChanged = True
            End If
            IP_Wert4Str = value
        End Set
        Get
            Return IP_Wert4Str
        End Get
    End Property
    ''' <summary>
    ''' Mitarbeiter-Gruppe. Numerisch maximal 10 Stellen
    ''' Tabelle winback.ItemParameter.IP_ItemID
    ''' </summary>
    ''' <returns></returns>
    Public Property iGruppe As Integer
        Set(value As Integer)
            If value <> IP_ItemID Then
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
            If value <> IP_Wert1int Then
                If Not Exist(value) Then
                    DataHasChanged = True
                    IP_Wert1int = value
                Else
                    Throw New Exception("Mitarbeiter-Kennwort ist schon vorhanden")
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
        IP_Wert4Str = dataGridView.Field("IP_Wert4Str")
        'Benutzer-Gruppe
        IP_ItemID = CInt(dataGridView.Field("IP_ItemID"))
        'Passwort
        IP_Wert1int = dataGridView.Field("IP_Wert1int")
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
            dataGridView.Field("IP_Wert4Str") = IP_Wert4Str
            dataGridView.Field("IP_ItemID") = IP_ItemID
            dataGridView.Field("IP_Wert1int") = IP_Wert1int
            dataGridView.Field("IP_lfd_Nr") = IP_Wert1int
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
        Return AddNew(wb_Global.NewUserName, wb_Global.NewUserPass, wb_Global.NewUserGrpe)
    End Function

    ''' <summary>
    ''' Neuen Mitarbeiter anlegen.
    ''' Die Mitarbeiter-Nummer ist Unique-Key. Wenn die Mitarbeiter-Nummer schon vorhanden ist, wird
    ''' False zurückgeliefert.
    ''' </summary>
    ''' <param name="Name"> String Mitarbeiter-Name</param>
    ''' <param name="Passwort"> String Mitarbeiter-Passwort numerisch max. 10 Stellen</param>
    ''' <param name="Gruppe">String - User-Gruppe (1..9, 99)</param>
    ''' <returns>
    ''' True - Einfügen war erfolgreich
    ''' False - Fehler beim Einfügen
    ''' </returns>
    Function AddNew(Name As String, Passwort As String, Gruppe As String) As Boolean
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
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
    ''' Eintrag Mitarbeiter löschen. Das Löschen der Mitarbeiter ist in WinBack unkritisch,
    ''' da in allen Verweisen auch der Name im Klartext mitgespeichert wird.
    ''' In Verbindung mit OrgaBack ist ein Löschen der Datensätze nicht vorgesehen.
    ''' </summary>
    ''' <param name="Passwort"> String Mitarbeiter-Passwort numerisch max. 10 Stellen</param>
    Sub Delete(Passwort As String)
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        Try
            'Benutzer aus Datenbank löschen
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserDelete, Passwort))
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
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        Dim i As Integer = 0

        Try
            'Neuen Benutzer in Datenbank einfügen
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
