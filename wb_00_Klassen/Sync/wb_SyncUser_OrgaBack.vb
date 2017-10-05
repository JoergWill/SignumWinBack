Public Class wb_SyncUser_OrgaBack
    Inherits wb_Sync

    Friend Overrides Function DBRead() As Boolean
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        _Data.Clear()

        If orgaback.sqlSelect(wb_Sql_Selects.mssqlMitarbeiterMFF500) Then
            While orgaback.Read
                _Item = New wb_SyncItem
                _Item.Os_Nummer = orgaback.iField("KassiererNummer")
                _Item.Os_Bezeichnung = orgaback.sField("Vorname") & " " & orgaback.sField("Nachname")
                _Item.Os_Gruppe = FormatNumber(orgaback.sField("Inhalt"), 0, 0,, 0)
                _Item.SyncOK = wb_Global.SyncState.NOK
                _Item.Sort = _Item.Os_Nummer

                'Datensatz wird nur verwendet wenn der Mitarbeiter einer Produktions-Filiale zugeordnet ist
                If MitarbeiterHatProduktionsFiliale(orgaback.sField("Filialzuordnung")) Then
                    _Data.Add(_Item)
                End If
            End While
        Else
            orgaback.Close()
            Return False
        End If

        'wenn keine Datensätze gefunden worden sind, fehlt das MFF500 in OrgaBack
        If _Data.Count = 0 Then
            'Meldung ausgeben
            MsgBox("Fehler OrgaBack !!" & vbCr & "MFF500 (WinBack-Gruppe) ist nicht definiert", MsgBoxStyle.Critical, "Lesen Mitarbeiter OrgaBack")

            orgaback.CloseRead()
            If orgaback.sqlSelect(wb_Sql_Selects.mssqlMitarbeiter) Then
                While orgaback.Read
                    _Item = New wb_SyncItem
                    _Item.Os_Nummer = orgaback.iField("KassiererNummer")
                    _Item.Os_Bezeichnung = orgaback.sField("Vorname") & " " & orgaback.sField("Nachname")
                    _Item.Os_Gruppe = "" 'in dieser Abfrage ist die Zuordnung Mitarbeiter zu WinBack-Gruppe nicht enthalten (MFF500)
                    _Item.SyncOK = wb_Global.SyncState.NOK
                    _Item.Sort = _Item.Os_Nummer

                    'Datensatz wird nur verwendet wenn der Mitarbeiter einer Produktions-Filiale zugeordnet ist
                    If MitarbeiterHatProduktionsFiliale(orgaback.sField("Filialzuordnung")) Then
                        _Data.Add(_Item)
                    End If
                End While
            Else
                orgaback.Close()
                Return False
            End If
        End If

        orgaback.Close()
        CheckData(wb_Global.SyncState.OrgaBackErr)
        Return True
    End Function

    ''' <summary>
    ''' Zerlegt das Datenbankfeld 'Filialzuordnung' aus der Tabelle dbo.Mitarbeiten in einzelne Strings mit der
    ''' Filial-Nummer. Ist eine der Filial-Nummern eine Produktions-Filiale (Typ=4) wird True zurückgegeben
    ''' ansonsten False
    ''' </summary>
    ''' <param name="Filialzuordnung">String - alle Filialen den der Mitarbeiter zugeordnet ist als Kommas-separierter String</param>
    ''' <returns>True wenn dre Mitarbeiter der Produktion zugeordnet ist</returns>
    Private Function MitarbeiterHatProduktionsFiliale(Filialzuordnung As String) As Boolean
        'Mitarbeiter-Zuordnung in Array erlegen
        Dim Filialen() As String = Split(Filialzuordnung, ",")
        For Each f In Filialen
            If wb_Filiale.FilialeIstProduktion(f) Then
                Return True
            End If
        Next
        Return False
    End Function

    Friend Overrides Function DBInsert(Nr As String, Text As String, Gruppe As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function DBUpdate(Nr As String, Text As String, Gruppe As String) As Boolean
        Throw New NotImplementedException()
    End Function

End Class
