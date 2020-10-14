Imports WinBack.wb_Global
Imports WinBack.wb_Functions
Imports WinBack

''' <summary>
''' Über die Parameter in der Tabelle Konfiguration wird die
''' Erfassung der Chargennummern gesteuert.
'''
'''     KonfigChargenErfEin       -Chargenerfassung aktiv
'''     KonfigGebindeGroessenTol  -Toleranzwert bis zur Erkennung Gebinde leer.
'''                                (Bis unter diese Grenze darf ausdosiert werden)
'''                            
'''     KonfigChargenErfVariante  -Verfahren nach Ablauf Gebinde
'''
'''         1 - darf weiter verwiegen, auch wenn Gebinde theoretisch leer.
'''         2 - darf nicht weiter verwiegen, wenn Verbrauch größer als Gebindegröße +KonfigGebindeGroessenTol.
'''         
''' 
''' </summary>
Public Class wb_Lieferungen
    Private KO_Nr As Integer
    Private KO_Type As KomponTypen
    Private KO_Nr_AlNum As String
    Private KO_Bezeichnung As String
    Private KA_Charge_Opt_kg As String
    Private LG_Ort As String
    Private LG_Bilanzmenge As String
    Private LG_LF_Nr As Integer = wb_Global.UNDEFINED
    Private LG_Silo_Nr As Integer = wb_Global.UNDEFINED

    Public Property Nr As Integer
        Set(value As Integer)
            KO_Nr = value
        End Set
        Get
            Return KO_Nr
        End Get
    End Property

    Public ReadOnly Property Type As KomponTypen
        Get
            Return KO_Type
        End Get
    End Property

    Public ReadOnly Property Bezeichnung As String
        Get
            Return KO_Bezeichnung
        End Get
    End Property

    ''' <summary>
    ''' Rohstoff/Artikel-Nummer (alpha-numerisch)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Nummer As String
        Get
            Return KO_Nr_AlNum
        End Get
    End Property

    Public Property Bilanzmenge As Double
        Get
            Return wb_Functions.StrToDouble(LG_Bilanzmenge)
        End Get
        Set(value As Double)
            LG_Bilanzmenge = wb_Functions.FormatStr(value.ToString, 2)
        End Set
    End Property

    Public ReadOnly Property LagerOrt As String
        Get
            Return LG_Ort
        End Get
    End Property

    Public Property LfdNr As Integer
        Get
            Return LG_LF_Nr
        End Get
        Set(value As Integer)
            LG_LF_Nr = value
        End Set
    End Property

    Public Property SiloNr As Integer
        Get
            Return LG_Silo_Nr
        End Get
        Set(value As Integer)
            LG_Silo_Nr = value
        End Set
    End Property

    Public ReadOnly Property GebindeGroesse As Double
        Get
            Return wb_Functions.StrToDouble(KA_Charge_Opt_kg)
        End Get
    End Property

    Public ReadOnly Property RohChargenErfassung As Boolean
        Get
            Return (GebindeGroesse > 0) And wb_GlobalSettings.RohChargen_ErfassungAktiv
        End Get
    End Property

    ''' <summary>
    ''' Abhängig vom Vorfallkürzel wird der aktuelle Datensatz aus dbo.ArtikelLagerkarte in winback.Lieferungen verbucht:
    ''' 
    '''     BR  -   Bruch/Schwund
    '''     WE  -   Wareneingang
    '''     WBA -   WinBack Produktion automatische Abbuchung
    '''     
    ''' </summary>
    ''' <param name="winback"></param>
    Public Sub Verbuchen(winback As wb_Sql, LagerKarte As wb_LagerKarte)
        'Vorgang in Log-File schreiben(INFO)
        Trace.WriteLine("@I_" & LagerKarte.Vorfall & "-" & LagerKarte.Lfd & "-" & Nummer & " Menge/Charge " & LagerKarte.Menge & "/" & LagerKarte.ChargenNummer)

        Select Case LagerKarte.Vorfall
            'WE wird direkt über den Vorfall verbucht (Version 1.7.3)
            'Case "WE"
            '    'Wareneingang in winback.Lieferungen verbuchen
            '    Verbuchen_Zugang(winback, LagerKarte, RohChargenErfassung)

            Case "BR"
                'Bruch/Schwund in winback.Lieferungen verbuchen
                Verbuchen_Abgang(winback, LagerKarte, RohChargenErfassung)

            Case "IV"
                'Inventur
                If LagerKarte.Menge > 0 Then
                    'Inventurbuchung - Lager zubuchen
                    Verbuchen_Zugang(winback, LagerKarte, RohChargenErfassung)
                Else
                    'Inventurbuchung - Lager abbuchen
                    Verbuchen_Abgang(winback, LagerKarte, RohChargenErfassung)
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Der aktuelle Datensatz aus Silo(wb_silo) wird als Zugang verbucht.
    ''' Wenn ein Tarawert eingetragen ist, wird zuvor das Silo auf Null gesetzt
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <param name="Silo"></param>
    Public Sub Verbuchen(winback As wb_Sql, Silo As wb_LagerSilo)
        'Test-Ausgabe
        Debug.Print("Silo/Menge/Tara " & Silo.KompBezeichnung & "/" & Silo.LagerOrt & "/" & Silo.BefMenge & "/" & Silo.TaraWert)

        'Prüfen ob Nullsetzen erforderlich
        If Silo.TaraWert <> 0 Then
            Verbuchen_Tara(winback, Silo, RohChargenErfassung)
        End If

        'Zugang verbuchen, wenn eine Befüllmenge angegeben ist
        If Silo.BefMenge >= 0 Then
            Verbuchen_Zugang(winback, Silo, RohChargenErfassung)
        End If
    End Sub

    Public Sub InitBestand(winback As wb_Sql, LagerKarte As wb_LagerKarte)
        'Vorgang in Log-File schreiben
        Trace.WriteLine("IB-Erste Bestandsbuchung-" & LagerKarte.Lfd & "-" & Nummer & " Menge/Charge " & LagerKarte.Menge & "/" & LagerKarte.ChargenNummer)

        'Bestand wird initialisiert
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelLieferungen, LagerOrt)
        winback.sqlCommand(sql)
        Bilanzmenge = 0
        LagerKarte.InitBestand()

        'Der letzte Eintrag aus dbo.ArtikelLagerkarte enthält auch den aktuellen Bestand
        Verbuchen_Zugang(winback, LagerKarte, RohChargenErfassung)
    End Sub

    ''' <summary>
    ''' Fügt einen neuen Datensatz (Wareneingang) an die Tabelle Lieferungen an. 
    ''' 
    '''     LF_LG_Ort           winback.Komponenten.LG_Ort
    '''     LF_Nr               dbo.ArtikelLagerkarte.Lfd
    '''     LF_Datum            dbo.ArtikelLagerkarte.Datum + dbo.ArtikelLagerkarte.Uhrzeit  
    '''     LF_Menge            dbo.ArtikelLagerkarte.Menge
    '''     LF_Lieferant
    '''     LF_gebucht          1 (neue Lieferung)
    '''     LF_Bemerkung        dbo.ArtikelLagerkarte.VorfallKürzel + dbo.ArtikelLagerkarte.Modul 
    '''     LF_Lager            0 (Produktion)
    '''     LF_BF_Charge        dbo.ArtikelLagerkarte.ChargenNummer
    '''     LF_Liniengruppe     NULL
    '''     LF_BF_Frist         NULL
    '''     LF_Verbrauch        0
    '''     LF_User_Nr          dbo.ArtikelLagerkarte.Mitarbeiter
    '''     LF_Preis            dbo.ArtikelLagerkarte.Preis
    '''     LF_PreisEinheit     0
    '''     LF_Timestamp
    ''' 
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <param name="LagerKarte"></param>
    ''' <param name="Chargenerfassung"></param>
    Private Sub Verbuchen_Zugang(winback As wb_Sql, LagerKarte As wb_LagerKarte, Chargenerfassung As Boolean)
        'Lieferung Datensatz anfügen
        InsertLieferung(winback, LagerKarte, LagerKarte.Menge)
        'Bilanzmenge neu berechnen
        Bilanzmenge = Bilanzmenge + LagerKarte.Menge
    End Sub

    ''' <summary>
    ''' Fügt einen neuen Datensatz (Wareneingang) an die Tabelle Lieferungen an. 
    ''' 
    '''     LF_LG_Ort           wb_LagerSilo.LG_Ort
    '''     LF_Nr               letzte Nummer aus winback.Lieferungen (Abfrage DB)
    '''     LF_Datum            aktuelles Datum/Uhrzeit
    '''     LF_Menge            wb_LagerSilo.BefüllMenge
    '''     LF_Lieferant
    '''     LF_gebucht          1 (neue Lieferung)
    '''     LF_Bemerkung        WE
    '''     LF_Lager            0 (Produktion)
    '''     LF_BF_Charge        wb_LagerSilo.ChargenNummer
    '''     LF_Liniengruppe     NULL
    '''     LF_BF_Frist         NULL
    '''     LF_Verbrauch        0
    '''     LF_User_Nr          User-Nummer aus GlobalSettings
    '''     LF_Preis            wb_LagerSilo.Preis
    '''     LF_PreisEinheit     0
    '''     LF_Timestamp
    ''' 
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <param name="Silo"></param>
    ''' <param name="Chargenerfassung"></param>
    Private Sub Verbuchen_Zugang(winback As wb_Sql, Silo As wb_LagerSilo, Chargenerfassung As Boolean)
        'letzten Eintrag aus winback.Lieferungen
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlLieferLfd, Silo.LagerOrt))

        'wenn Datensätze vorhanden sind
        If winback.Read Then
            'laufende Nummer plus Eins
            LfdNr = winback.iField("LF_Nr") + 1
        Else
            'neuer Eintrag
            LfdNr = 1
        End If

        'Verbindung wieder freigeben
        winback.CloseRead()

        'Silo-Menge Nullsetzen
        If (Silo.TaraWert <> 0) And (Silo.BefMenge > 0) Then
            'Datensatz "Nullsetzen" einfügen
            InsertLieferung(winback, Silo, 0)
            'lfd.Nummer plus Eins
            LfdNr += 1
        End If

        'Lieferung Datensatz anfügen
        InsertLieferung(winback, Silo, Silo.BefMenge)

        'Bilanzmenge neu berechnen
        Bilanzmenge = Bilanzmenge + Silo.BefMenge
    End Sub

    ''' <summary>
    ''' Verbucht Bruch und/oder Schwund in winback.Lieferungen. Wenn eine Chargen-Nummer angegeben ist, wird versucht, 
    ''' den oder die Datensätze mit dieser Nummer zu finden. 
    ''' Sind mehrere Datensätze vorhanden, werden solange die Liefermengen auf Null gesetzt, bis die Korrekturmenge erreicht ist.
    ''' 
    ''' Wenn der Datensatz nicht gefunden werden kann oder keine Chargen-Nummer angegeben ist, wird eine neue Zeile 
    ''' angefügt und der Bestand korrigiert.
    ''' 
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <param name="LagerKarte"></param>
    ''' <param name="Chargenerfassung"></param>
    Private Sub Verbuchen_Abgang(winback As wb_Sql, LagerKarte As wb_LagerKarte, Chargenerfassung As Boolean)
        'Liste aller Lieferungen mit dieser Chargen-Nummer
        Dim lfdNr As New List(Of Integer)
        lfdNr.Clear()
        'ursprüngliche Liefermenge aus winback.Lieferungen
        Dim lfdMenge As Double = 0.0
        'zu verbuchende (Negativ)Menge
        Dim BchMenge As Double = LagerKarte.Menge

        'Prüfen ob eine Chargen-Nummer vorhanden ist
        If LagerKarte.ChargenNummer <> "" Then
            'Durchsucht alle OFFENEN Einträge in Lieferungen mit dieser Chargen-Nummer
            winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlLieferCharge, LagerOrt, LagerKarte.ChargenNummer))

            'Liest alle Datensätze in absteigender Reihenfolge bis die gesamte Korrektur-Menge ausgebucht werden kann
            While winback.Read And (BchMenge < 0)
                lfdNr.Add(winback.iField("LF_Nr"))
                lfdMenge = winback.sField("LF_Menge")
                'die zu verbuchende (Negativ)Menge nach Korrekturbuchung in winback.Lieferungen
                BchMenge = BchMenge + lfdMenge
            End While
            winback.CloseRead()
        End If

        'Die ArrayList enthält alle Liefernummern, die durch die Bestandskorrekturbuchung geändert werden müssen.
        If lfdNr.Count = 0 Then
            'die ArrayList ist leer - In die Tabelle Lieferungen wird ein Korrektursatz eingefügt
            InsertLieferung(winback, LagerKarte, LagerKarte.Menge)
        Else
            'die Liefermenge im letzten Datesatz wird angepasst, alle anderen Liefermengen werden auf Null gesetzt.
            For Each Nr As Integer In lfdNr
                If Nr = lfdNr.Last Then
                    'wenn die Korrekturmenge größer als die Liefermenge ist
                    If BchMenge < 0 Then
                        'letzter Datensatz - Liefermenge wird auf Null gesetzt
                        UpdateLieferung(winback, Nr, 0, "WE-" & LagerKarte.Vorfall)
                        'dann wird noch zusätzlich ein Korrektursatz angefügt
                        InsertLieferung(winback, LagerKarte, BchMenge)
                    Else
                        'letzter (oder nur ein) Datensatz - Liefermenge korrigieren
                        UpdateLieferung(winback, Nr, BchMenge, "WE-" & LagerKarte.Vorfall)
                    End If
                Else
                    'mehrere Datensätze - Liefermenge wird auf Null gesetzt
                    UpdateLieferung(winback, Nr, 0, "WE-" & LagerKarte.Vorfall)
                End If
            Next
        End If

        'Bilanzmenge neu berechnen (Lagerkarte.Menge ist negativ)
        Bilanzmenge = Bilanzmenge + LagerKarte.Menge
    End Sub

    ''' <summary>
    ''' Siloinhalt auf Null setzen.
    ''' In der Tabelle Lieferungen wird eine entsprechende Zeile eingefügt. Alle vorhegehenden Lieferungen werden auf Status 3(erledigt) gesetzt.
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <param name="Silo"></param>
    ''' <param name="Chargenerfassung"></param>
    Private Sub Verbuchen_Tara(winback As wb_Sql, Silo As wb_LagerSilo, Chargenerfassung As Boolean)
        'Bilanzmenge auf Null setzen
        Bilanzmenge = 0
        'alle Lieferungen zu diesem Rohstoff als erledigt markieren
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSetStatusLieferung, Silo.LagerOrt))
    End Sub

    ''' <summary>
    ''' Fügt einen neuen Datensatz an die Tabelle winback.Lieferungen an. Die Daten werden aus dem LagerKarten-Objekt ermittelt.
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <param name="LagerKarte"></param>
    Private Sub InsertLieferung(winback As wb_Sql, LagerKarte As wb_LagerKarte, Menge As Double)
        'INSERT in Tabelle Lieferungen - Daten aus dem Lagerkarten-Objekt
        Dim Datum As Date = Date.Parse(LagerKarte.Datum & " " & LagerKarte.Uhrzeit)
        'TODO prüfen ob die Konvertierung (Zeile oben) funktioniert !!!
        'Background-Task
        InsertLieferung(winback, LagerKarte.Lfd, Datum, LagerKarte.Gebucht, LagerKarte.Vorfall, LagerKarte.VorfallNr, LagerKarte.ChargenNummer, LagerKarte.Preis, Menge)
    End Sub

    ''' <summary>
    ''' Fügt einen neuen Datensatz an die Tabelle winback.Lieferungen an. Die Daten werden aus dem Silo-Objekt ermittelt.
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <param name="Silo"></param>
    Private Sub InsertLieferung(winback As wb_Sql, Silo As wb_LagerSilo, Menge As Double)
        'Lieferung oder Null setzen 
        If Menge > 0 Then
            'INSERT in Tabelle Lieferungen - Daten aus dem Silo-Objekt (Lieferant ist gleich der Vorfall-Nummer aus OrgaBack)
            InsertLieferung(winback, LfdNr, Now, "1", "WE", Silo.VorfallNr, Silo.ChargenNummer, Silo.Preis, Menge)
        Else
            'INSERT in Tabelle Lieferungen - Null setzen
            InsertLieferung(winback, LfdNr, Now, "3", "Null setzen", "", "", "", 0.0)
        End If
    End Sub

    ''' <summary>
    ''' Fügt einen neuen Datensatz an die Tabelle winback.Lieferungen an.
    ''' Die fortlaufende Nummer(Lfd) muss bekannt sein.
    ''' </summary>
    ''' <param name="winback"></param>
    Private Sub InsertLieferung(winback As wb_Sql, Lfd As String, Datum As Date, Gebucht As String, Bemerkung As String, Lieferant As String, ChargenNummer As String, Preis As String, Menge As Double)
        'der INSERT-Befehl wird dynamisch erzeugt
        Dim sql_insert As String = "'" & LG_Ort & "', " & Lfd & ", '" & wb_sql_Functions.MySQLdatetime(Datum) & "', " &
                                   "'" & wb_Functions.FormatStr(Menge, 2) & "', '" & Lieferant & "', '" & Gebucht & "', '" & Bemerkung & "', " &
                                   "0" & ", '" & ChargenNummer & "', NULL, NULL, " & "0" & ", " & wb_GlobalSettings.AktUserNr & ", '" & Preis & "', " & "0"

        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlInsertWE, sql_insert)
        'INSERT ausführen
        winback.sqlCommand(sql)
    End Sub

    ''' <summary>
    ''' Ändert einen Datensatz in der Tabelle winback.Lieferungen. Anpassung der Liefermenge
    ''' </summary>
    ''' <param name="winback"></param>
    ''' <param name="lfd"></param>
    ''' <param name="lfMenge"></param>
    Private Sub UpdateLieferung(winback As wb_Sql, lfd As Integer, lfMenge As Double, lfBemerkung As String)
        'der UPDATE-Befehl wird dynamisch erzeugt
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateLieferung, LG_Ort, lfd.ToString, wb_Functions.FormatStr(lfMenge, 2), lfBemerkung)
        'INSERT ausführen
        winback.sqlCommand(sql)
    End Sub

    ''' <summary>
    ''' Setzt die laufende Nummer (Lagerorte.LG_LF_Nr) auf den letzten verarbeiteten Eintrag aus OrgaBack.
    ''' Bei der nächsten Abfrage werden dann nur neue, nicht verarbeitete Einträge aus OrgaBack gelesen.
    ''' </summary>
    ''' <param name="winback"></param>
    Public Sub UpdateLagerorteLfd(winback As wb_Sql, lfd As Integer)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateLagerort, lfd, wb_Functions.FormatStr(Bilanzmenge, 2), LagerOrt)
        'UPDATE ausführen
        winback.sqlCommand(sql)
    End Sub

    Public Sub UpdateLagerorte(winback As wb_Sql)
        UpdateLagerorteLfd(winback, wb_Global.UNDEFINED)
    End Sub

    ''' <summary>
    ''' Liest alle Datenfelder aus dem aktuellen Datensatz in das Lieferungen-Objekt
    ''' Die Daten werden anhand der Feldbezeichnung in die einzelnen Properties eingetragen.
    ''' 
    ''' Das letzte Datenfeld ist der TimeStamp und wird NICHT eingelesen, da es Probleme mit
    ''' der Konvertierung von MySQLDateTime in DateTime gibt
    ''' (https://bugs.mysql.com/bug.php?id=87120)
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <returns>True wenn kein Fehler aufgetreten ist</returns>
    Public Function MySQLdbRead(ByRef sqlReader As MySql.Data.MySqlClient.MySqlDataReader) As Boolean
        'Schleife über alle Datensätze
        Do
            'Parameter - Anzahl der Felder im DataSet
            'FieldCount-2 unterdrückt das Feld TimeStamp
            For i = 0 To sqlReader.FieldCount - 2
                Try
                    MySQLdbRead_Daten(sqlReader.GetName(i), sqlReader.GetValue(i))
                Catch ex As Exception
                    Debug.Print("Exception MySQLdbRead " & sqlReader.GetName(i))
                End Try
            Next
        Loop While sqlReader.Read
        Return True
    End Function

    ''' <summary>
    ''' Schreibt den Wert aus Value in die entprechende Property der Klasse. Der Feldname bestimmt das Ziel
    ''' </summary>
    ''' <param name="Name">String - Bezeichnung Datenbankfeld</param>
    ''' <param name="Value">Object - Wert Datenbankfeld(Inhalt)</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_Daten(Name As String, Value As Object) As Boolean
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        'Debug.Print("ReadLieferungen " & Name & "/" & Value)
        Try
            Select Case Name

                'Nummer(intern)
                Case "KO_Nr"
                    Nr = CInt(Value)
               'Type
                Case "KO_Type"
                    KO_Type = IntToKomponType(CInt(Value))
                'Bezeichnung
                Case "KO_Bezeichnung"
                    KO_Bezeichnung = wb_Functions.MySqlToUtf8(Value)
                'Nummer(alphanumerisch)
                Case "KO_Nr_AlNum"
                    KO_Nr_AlNum = Value
                'Gebindegröße (nur Rohstoffe)
                Case "KA_Charge_Opt_kg"
                    KA_Charge_Opt_kg = Value

                'Laufende Nummer entspricht der Lfd in dbo.ArtikelLagerKarte
                'Achtung(!) muss von SMALLINT auf INT erweitert werden
                Case "LG_LF_Nr"
                    LG_LF_Nr = CInt(Value)
                'Lagerort
                Case "LG_Ort"
                    LG_Ort = Value
                'Bilanzmenge
                Case "LG_Bilanzmenge"
                    LG_Bilanzmenge = Value
                'Silo-Nummer
                Case "LG_Silo_Nr"
                    LG_Silo_Nr = CInt(Value)

            End Select

        Catch ex As Exception
        End Try
        Return True
    End Function

End Class

'LF_LG_Ort         
'LF_Nr                
'LF_Datum          
'LF_Menge
'LF_Lieferant
'LF_gebucht
'LF_Bemerkung
'LF_Lager
'LF_BF_Charge
'LF_Liniengruppe
'LF_BF_Frist
'LF_Verbrauch
'LF_User_Nr
'LF_Preis
'LF_PreisEinheit
'LF_Timestamp
