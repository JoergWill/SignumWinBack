Imports WinBack.wb_Functions
Imports WinBack.wb_Global
Imports WinBack.wb_KomponParam301_Global

Public Class wb_KomponParam301
    Inherits wb_ChangeLog

    Private Structure Typ301
        Public _Allergen As AllergenInfo
        Public _Naehrwert As Double
        Public _FehlerKompName As String
    End Structure

    'Array aller Nährwerte/Allergene
    Private NaehrwertInfo(maxTyp301) As Typ301
    Private _TimeStamp
    Private _IsCalculated
    Private _NwtTabelle(wb_Global.maxTyp301) As Nwt

    ''' <summary>
    ''' Nach der Initialisierung sind die Nährwerte nicht berechnet
    ''' </summary>
    Public Sub New()
        _IsCalculated = False
    End Sub

    Public ReadOnly Property NwtTabelle As Array
        Get
            For i = 1 To wb_Global.maxTyp301
                _NwtTabelle(i).Visible = wb_KomponParam301_Global.kt301Param(i).Used
                _NwtTabelle(i).Nr = i
                _NwtTabelle(i).Text = wb_KomponParam301_Global.kt301Param(i).Bezeichnung
                _NwtTabelle(i).Wert = Wert(i)
                _NwtTabelle(i).Einheit = wb_KomponParam301_Global.kt301Param(i).Einheit
                _NwtTabelle(i).Header = wb_Functions.kt301GruppeToString(wb_KomponParam301_Global.kt301Param(i).Gruppe)
                _NwtTabelle(i).FehlerText = FehlerKompName(i)

                'Debug.Print("FEHLER :" & Rezept.KtTyp301.FehlerKompName(i))
                'If NwtTabelle(i).Visible Then
                '    Debug.Print(NwtTabelle(i).Header & " " & NwtTabelle(i).Text & " " & NwtTabelle(i).Wert & " " & NwtTabelle(i).Einheit)
                'End If
            Next
            Return _NwtTabelle
        End Get
    End Property

    ''' <summary>
    ''' Gibt False zurück, wenn das Array leer bzw. noch nicht berechnet ist (Lesen aus DB erforderlich)
    ''' True, wenn Daten vorhanden sind.
    ''' </summary>
    ''' <returns>Boolean - Nährwerte sind ermittelt und berechnet</returns>
    Public Property IsCalculated As Boolean
        Get
            Return _IsCalculated
        End Get
        Set(value As Boolean)
            _IsCalculated = value
        End Set
    End Property

    Public Property TimeStamp As DateTime
        Get
            Return _TimeStamp
        End Get
        Set(value As DateTime)
            _TimeStamp = value
        End Set
    End Property

    Public Property Allergen(index As Integer) As AllergenInfo
        Get
            If IsAllergen(index) Then
                Return NaehrwertInfo(index)._Allergen
            Else
                Return AllergenInfo.ERR
            End If
        End Get
        Set(value As AllergenInfo)
            If IsAllergen(index) Then
                NaehrwertInfo(index)._Allergen = value
                _IsCalculated = True
            End If
        End Set
    End Property

    ''' <summary>
    ''' Kommagetrennte Liste aller Allergene (Volltext) die enthalten sind
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AllergenListe_C As String
        Get
            AllergenListe_C = ""
            'Druchläuft alle Indizes
            For i = 1 To maxTyp301
                'Ist ein Allergen
                If IsAllergen(i) Then
                    'Ist enthalten
                    If Allergen(i) = AllergenInfo.C Then
                        'Liste nach Kommata getrennt
                        If AllergenListe_C <> "" Then
                            AllergenListe_C &= ","
                        End If
                        'Allergenbezeichnung hinzufügen
                        AllergenListe_C &= wb_KomponParam301_Global.kt301Param(i).Bezeichnung
                    End If
                End If
            Next
        End Get
    End Property

    ''' <summary>
    ''' Kommagetrennte Liste aller Allergene (Volltext) die in Spuren vorkommen
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AllergenListe_T As String
        Get
            AllergenListe_T = ""
            'Druchläuft alle Indizes
            For i = 1 To maxTyp301
                'Ist ein Allergen
                If IsAllergen(i) Then
                    'Ist enthalten
                    If Allergen(i) = AllergenInfo.T Then
                        'Liste nach Kommata getrennt
                        If AllergenListe_T <> "" Then
                            AllergenListe_T &= ","
                        Else
                            AllergenListe_T = "Spuren von "
                        End If
                        'Allergenbezeichnung hinzufügen
                        AllergenListe_T &= wb_KomponParam301_Global.kt301Param(i).KurzBezeichnung
                    End If
                End If
            Next
        End Get
    End Property

    Public ReadOnly Property AllergenKurzListe_C As String
        Get
            AllergenKurzListe_C = ""
            'Druchläuft alle Indizes
            For i = 1 To maxTyp301
                'Ist ein Allergen
                If IsAllergen(i) Then
                    'Ist enthalten
                    If Allergen(i) = AllergenInfo.C Then
                        'Liste nach Kommata getrennt
                        If AllergenKurzListe_C <> "" Then
                            AllergenKurzListe_C &= ","
                        End If
                        'Allergenbezeichnung hinzufügen
                        AllergenKurzListe_C &= i.ToString
                    End If
                End If
            Next
        End Get
    End Property

    Public ReadOnly Property AllergenKurzListe_T As String
        Get
            AllergenKurzListe_T = ""
            'Druchläuft alle Indizes
            For i = 1 To maxTyp301
                'Ist ein Allergen
                If IsAllergen(i) Then
                    'Ist enthalten
                    If Allergen(i) = AllergenInfo.T Then
                        'Liste nach Kommata getrennt
                        If AllergenKurzListe_T <> "" Then
                            AllergenKurzListe_T &= ","
                        End If
                        'Allergenbezeichnung hinzufügen
                        AllergenKurzListe_T &= i.ToString
                    End If
                End If
            Next
        End Get
    End Property

    Public Property Naehrwert(index As Integer) As Double
        Get
            If Not IsAllergen(index) Then
                Return NaehrwertInfo(index)._Naehrwert
            Else
                Return 0.0
            End If
        End Get
        Set(value As Double)
            If Not IsAllergen(index) Then
                NaehrwertInfo(index)._Naehrwert = value
                _IsCalculated = True
            End If
        End Set
    End Property

    Public Property Wert(index As Integer) As String
        Get
            If IsAllergen(index) Then
                Return NaehrwertInfo(index)._Allergen
            ElseIf index < wb_Global.maxTyp301 Then
                Return NaehrwertInfo(index)._Naehrwert
            Else
                Return ""
            End If
        End Get
        Set(value As String)
            If IsAllergen(index) Then
                'Änderungen loggen
                NaehrwertInfo(index)._Allergen = ChangeLogAdd(LogType.Alg, index, NaehrwertInfo(index)._Allergen, StringtoAllergen(value))
            ElseIf index < wb_Global.maxTyp301 Then
                'Änderungen loggen
                NaehrwertInfo(index)._Naehrwert = ChangeLogAdd(LogType.Nrw, index, NaehrwertInfo(index)._Naehrwert, StrToDouble(value))
            End If
        End Set
    End Property

    Public ReadOnly Property oWert(Index) As String
        Get
            If IsAllergen(Index) Then
                oWert = wb_Functions.AllergenToString(NaehrwertInfo(Index)._Allergen)
                'OrgaBack kann den Wert ERR nicht verarbeiten
                If oWert = "ERR" Then
                    oWert = "N"
                End If
                Return oWert
            Else
                Return wb_sql_Functions.MsDoubleToString(NaehrwertInfo(Index)._Naehrwert)
            End If

        End Get
    End Property

    Public Property FehlerKompName(index As Integer) As String
        Get
            Return NaehrwertInfo(index)._FehlerKompName
        End Get
        Set(value As String)
            NaehrwertInfo(index)._FehlerKompName = value
        End Set
    End Property

    Public WriteOnly Property dlNaehrWert(index As String) As String
        Set(value As String)
            Dim idx As Integer = DatenLinkToIndex(index)
            If idx > 0 Then
                Try
                    Naehrwert(idx) = CDbl(value)
                Catch ex As Exception
                    Trace.WriteLine("Fehler bei DatenLink - Index = " & index & " Wert = " & value)
                End Try
            Else
                Trace.WriteLine("Fehler bei DatenLink - Index " & index & " nicht definiert")
            End If
        End Set
    End Property

    Public WriteOnly Property dlAllergen(index As String) As String
        Set(value As String)
            Dim idx As Integer = DatenLinkToIndex(index)
            If idx > 0 Then
                Select Case value
                    Case "CONTAINED"
                        Allergen(idx) = AllergenInfo.C
                    Case "MAY_CONTAINED"
                        Allergen(idx) = AllergenInfo.T
                    Case Else
                        Allergen(idx) = AllergenInfo.ERR
                End Select
            Else
                Trace.WriteLine("Fehler bei DatenLink - Index " & index & " nicht definiert")
            End If
        End Set
    End Property

    Public WriteOnly Property PistorWert(index As String) As String
        Set(value As String)
            Dim idx As Integer = PistorToIndex(index)
            If idx > 0 Then
                If IsAllergen(idx) Then
                    PistorAllergen(idx) = value
                Else
                    PistorNaehrWert(idx) = value
                End If
            Else
                Trace.WriteLine("Fehler bei Pistor - Index " & Index & " nicht definiert")
            End If
        End Set
    End Property

    Public WriteOnly Property PistorNaehrWert(Index As Integer) As String
        Set(value As String)
            Naehrwert(Index) = wb_Functions.StrToDouble(value)
        End Set
    End Property

    Public WriteOnly Property PistorAllergen(Index As Integer) As String
        Set(value As String)
            Select Case value
                Case "0"
                    Allergen(Index) = AllergenInfo.N
                Case "1"
                    Allergen(Index) = AllergenInfo.C
                Case "2"
                    Allergen(Index) = AllergenInfo.T
                Case Else
                    Allergen(Index) = AllergenInfo.ERR
            End Select
        End Set
    End Property

    Public Sub ClearReport()
        ChangeLogClear()
    End Sub

    Public Function GetReport(Optional ReportAll As Boolean = vbFalse) As String
        Return ChangeLogReport(ReportAll)
    End Function

    ''' <summary>
    ''' Addiert alle Nährwerte und Allergene zum übergebenen KomponentenParameter-Array
    ''' Die Bezeichnungen der fehlerhaften Komponenten werden aneinander gehängt
    ''' </summary>
    ''' <param name="_ktTyp301"></param>
    Public Sub AddNwt(ByRef _ktTyp301 As wb_KomponParam301, Faktor As Double)
        For i = 0 To maxTyp301
            If IsAllergen(i) Then
                _ktTyp301.Allergen(i) = AddNwtAllergen(_ktTyp301.Allergen(i), Allergen(i))
            Else
                _ktTyp301.Naehrwert(i) += Naehrwert(i) * Faktor
            End If
            If FehlerKompName(i) <> "" Then
                If _ktTyp301.FehlerKompName(i) = "" Then
                    _ktTyp301.FehlerKompName(i) = FehlerKompName(i)
                Else
                    _ktTyp301.FehlerKompName(i) += "/" & FehlerKompName(i)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Addiert die Allergen-Info. Bei der Addition wird der jeweils größere Wert zurückgegeben.
    ''' Die Reihenfolge der Kontanten in wb_global entspricht der Reihenfolge der Wertigkeit.
    ''' </summary>
    ''' <param name="Allergen1"></param>
    ''' <param name="Allergen2"></param>
    Private Function AddNwtAllergen(Allergen1 As AllergenInfo, Allergen2 As AllergenInfo) As AllergenInfo

        'die Aufzählung der Konstanten entspricht der Reihenfolge der Wertigkeit.
        If Allergen1 > Allergen2 Then
            Return Allergen1
        Else
            Return Allergen2
        End If
    End Function

    ''' <summary>
    ''' Update aller geänderten Komponenten-Parameter in Tabelle winback.RohParams
    '''     RP_Ko_Nr
    '''     RP_Typ_Nr
    '''     RP_ParamNr
    '''     RP_Wert
    '''     RP_Kommentar
    '''     RP_Timestamp
    ''' </summary>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(KoNr As Integer, ByRef winback As wb_Sql) As Boolean
        'Result OK
        MySQLdbUpdate = True

        'alle Datensätze im Array durchlaufen
        For i = 0 To maxTyp301
            If IsValidParameter(i) Then
                MySQLdbUpdate = MySQLdbUpdate(KoNr, i, winback)
            End If
        Next
    End Function

    ''' <summary>
    ''' Update eines Komponenten-Parameters mit ParamNr in Tabelle winback.RohParams
    ''' </summary>
    ''' <param name="KoNr"></param>
    ''' <param name="ParamNr"></param>
    ''' <param name="winback"></param>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(KoNr As Integer, ParamNr As Integer, ByRef winback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
        Dim sql As String

        'Update-Statement wird dynamisch erzeugt
        If IsAllergen(ParamNr) Then
            'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
            sql = KoNr & ", 301, " & ParamNr.ToString & ", '" & wb_Functions.AllergenToString(Allergen(ParamNr)) & "', '" & kt301Param(ParamNr).Bezeichnung & "'"
        Else
            'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
            sql = KoNr & ", 301, " & ParamNr.ToString & ", '" & Wert(ParamNr) & "', '" & kt301Param(ParamNr).Bezeichnung & "'"
        End If

        'Update ausführen
        Return winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateRohParams, sql))
    End Function

    ''' <summary>
    ''' Update aller geänderten Komponenten-Parameter in Tabelle 
    ''' 
    ''' [dbo].[ArtikelNaehrwerte]
    '''     [ArtikelNr]
    '''     [Einheit]
    '''     [Farbe]                         immer 0
    '''     [Groesse]                       immer NULL
    '''     [StuecklistenVariantenNr]
    '''     [NaehrwertNr]
    '''     [Menge]
    '''     
    ''' [dbo].[ArtikelAllergene
    '''     [ArtikelNr]
    '''     [StuecklistenVariantenNr]
    '''     [AllergenNr]
    '''     [Kennzeichnung]
    ''' </summary>
    ''' <returns></returns>
    Public Function MsSQLdbUpdate(KoAlNum As String, Unit As Integer, orgaback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
        Dim sql As String
        'Result OK
        MsSQLdbUpdate = True

        'zunächst werden alle vorhandenen Einträge zur Komponente in der Tabelle ArtikelNaehrwerte und ArtikelAllergene gelöscht
        'TODO Was ist mit den Varianten für Allergene und Nährwerte
        sql = (wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlDeleteNwt, KoAlNum, Unit, 0))
        orgaback.sqlCommand(sql)
        sql = (wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlDeleteAlg, KoAlNum, 0))
        orgaback.sqlCommand(sql)

        'dann alle neuen Felder wieder eingesetzt
        'alle Datensätze im Array durchlaufen (Nährwerte/Allergene beginnen bei Index 1)
        For i = 1 To maxTyp301
            If IsValidParameter(i) Then

                'nur gültige Nährwerte/Allergene in DB schreiben (reduziert die Datenlast)
                sql = ""
                'Allergene haben in OrgaBack einen eigene Tabelle
                If IsAllergen(i) Then
                    If (NaehrwertInfo(i)._Allergen > wb_Global.AllergenInfo.N) Then
                        'TODO J.Erhardt-StoredProcedure erstellen zum Updaten der Nährwertinfo
                        sql = (wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertAlg, KoAlNum, i, oWert(i), 0))
                        'Debug.Print("Update OrgaBack Parameter " & i & " Wert " & Wert(i))
                    End If
                Else
                    If (NaehrwertInfo(i)._Naehrwert > 0) Then
                        'TODO J.Erhardt-StoredProcedure erstellen zum Updaten der Nährwertinfo
                        sql = (wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertNwt, KoAlNum, i, oWert(i), Unit, 0))
                        'Debug.Print("Update OrgaBack Parameter " & i & " Wert " & Wert(i))
                    End If
                End If

                'Update-Statement wird dynamisch erzeugt (nur wenn auch Daten vorhanden sind)
                If sql <> "" Then
                    If orgaback.sqlCommand(sql) < 0 Then
                        MsSQLdbUpdate = False
                    End If
                End If
            End If
        Next
    End Function

    ''' <summary>
    ''' Update EINES geänderte Komponenten-Parameter in Tabelle.
    ''' Da REPLACE be msSQL nicht funktioniert wird zuerst versucht, per UPDATE den Datensatz zu aktualisieren. Wenn 
    ''' das UPDATE nicht funktioniert (Datensatz nicht vorhanden) wird per INSERT der Datensatz neu angelegt
    ''' 
    ''' [dbo].[ArtikelNaehrwerte]
    '''     [ArtikelNr]
    '''     [Einheit]
    '''     [Farbe]                         immer 0
    '''     [Groesse]                       immer NULL
    '''     [StuecklistenVariantenNr]
    '''     [NaehrwertNr]
    '''     [Menge]
    '''     
    ''' [dbo].[ArtikelAllergene
    '''     [ArtikelNr]
    '''     [StuecklistenVariantenNr]
    '''     [AllergenNr]
    '''     [Kennzeichnung]
    ''' </summary>
    ''' <returns></returns>
    Public Function MsSQLdbUpdate(KoAlNum As String, ParamNr As Integer, Unit As Integer, orgaback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
        Dim sql_Delete As String
        Dim sql_Insert As String

        'Parameter-Nummer ist gültig
        If IsValidParameter(ParamNr) Then

            'Allergene haben in OrgaBack einen eigene Tabelle
            If IsAllergen(ParamNr) Then
                sql_Delete = (wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlDeleteParamAlg, KoAlNum, ParamNr, 0))
                sql_Insert = (wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertAlg, KoAlNum, ParamNr, oWert(ParamNr), 0))
                'Debug.Print("Update OrgaBack Parameter " & i & " Wert " & Wert(i))
            Else
                sql_Delete = (wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlDeleteParamNwt, KoAlNum, ParamNr, Unit, 0))
                sql_Insert = (wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertNwt, KoAlNum, ParamNr, oWert(ParamNr), Unit, 0))
                'Debug.Print("Update OrgaBack Parameter " & i & " Wert " & Wert(i))
            End If

            'Update-Statement wird versucht
            Select Case orgaback.sqlCommand(sql_Delete)
                Case < 0
                    'Fehler bei Zugriff auf die Datenbank
                    Return False
                Case 0, 1
                    'Delete war erfolgreich - Insert Datensatz versuchen
                    If orgaback.sqlCommand(sql_Insert) < 0 Then
                        'Insert fehlgeschlagen - Fehler
                        Return False
                    End If
                Case Else
                    'Unbekannter Fehler 
                    Return False
            End Select
        End If
        'Keine Änderung der Daten notwendig
        Return True
    End Function


End Class
