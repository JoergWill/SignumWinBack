Imports WinBack.wb_Functions
Imports WinBack.wb_Global
Imports WinBack.wb_KomponParam301_Global

Public Class wb_KomponParam301
    Inherits wb_ChangeLog

    Private Structure Typ301
        Public _Allergen As AllergenInfo
        Public _ErnaehrungsForm As ErnaehrungsForm
        Public _Naehrwert As Double
        Public _FehlerKompName As String
    End Structure

    Private Structure ArtikelEinheiten
        Public _InEinheit As Integer
        Public _Umrechnungsfaktor As Double
    End Structure

    'Array aller Nährwerte/Allergene
    Private NaehrwertInfo(maxTyp301) As Typ301
    Private Umrechnung As New List(Of ArtikelEinheiten)
    Private _TimeStamp
    Private _IsCalculated As Boolean = False
    Private _NwtTabelle(wb_Global.maxTyp301) As Nwt
    Private _FaktorStkGewicht As Double = wb_Global.UNDEFINED

    ''' <summary>
    ''' Nach der Initialisierung sind die Nährwerte nicht berechnet
    ''' </summary>
    Public Sub New()
        _IsCalculated = False
        _FaktorStkGewicht = 1
    End Sub

    Public Sub Clear()
        'TODO TEST
        For i = 1 To wb_Global.maxTyp301
            If IsAllergen(i) Then
                'Änderungen loggen
                NaehrwertInfo(i)._Allergen = StringtoAllergen("N")
            ElseIf IsErnaehrung(i) Then
                'Änderungen loggen
                NaehrwertInfo(i)._ErnaehrungsForm = StringtoErnaehrungsForm("N")
            ElseIf i <= wb_Global.maxTyp301 Then
                'Änderungen loggen
                NaehrwertInfo(i)._Naehrwert = 0.0
            End If
            FehlerKompName(i) = ""

        Next
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
                _NwtTabelle(i).ErrIntern = CalcNaehrwerteKonsistent(i)

                'Debug.Print("FEHLER :" & Rezept.KtTyp301.FehlerKompName(i))
                'If NwtTabelle(i).Visible Then
                '    Debug.Print(NwtTabelle(i).Header & " " & NwtTabelle(i).Text & " " & NwtTabelle(i).Wert & " " & NwtTabelle(i).Einheit)
                'End If
            Next
            Return _NwtTabelle
        End Get
    End Property

    ''' <summary>
    ''' Prüft ob die Nährwerte-Tabelle in sich konsistent ist
    ''' 
    '''     Umrechnung  1,000 kcal       =   4,184 kJ
    '''     Umrechnung  1,000 gr Natrium =   2,540 gr Salz
    '''     
    ''' Gibt True zurück, wenn die Umrechnung der Nährwertangabe auf den korrespondierenden Wert nicht innerhalb der Toleranz liegt
    ''' False, wenn es keinen korrespondierenden Wert gibt oder die Umrechnung innerhalb der Toleranz liegt
    ''' </summary>
    ''' <param name="Index"></param>
    ''' <returns></returns>
    Private Function CalcNaehrwerteKonsistent(Index As Integer) As Boolean

        Select Case Index
            Case wb_Global.T301_Kilokalorien, wb_Global.T301_KiloJoule
                Return CalcNaehrwerteUmrechnung(wb_Global.T301_Kilokalorien, wb_Global.T301_KiloJoule, 4.184, 1)
            Case wb_Global.T301_Natrium, wb_Global.T301_GesamtKochsalz
                Return CalcNaehrwerteUmrechnung(wb_Global.T301_Natrium, wb_Global.T301_GesamtKochsalz, 2.54, 1)
        End Select

        Return False
    End Function
    Private Function CalcNaehrwerteUmrechnung(i1 As Integer, i2 As Integer, Faktor As Double, Toleranz As Double) As Boolean
        If Faktor <> 0 Then
            Dim Wert1 As Double = wb_Functions.StrToDouble(Wert(i1))
            Dim Wert2 As Double = wb_Functions.StrToDouble(Wert(i2))

            Dim Diffx As Double = Math.Abs(Wert1 * Faktor - Wert2)
            Dim Wertx As Double = Math.Max(Wert1, (Wert2 / Faktor))
            Dim TolPz As Double = Wertx / 100 * Toleranz
            Return (Diffx > TolPz)
        Else
            Return False
        End If
    End Function

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

    Public Property ErnaehrungsForm(index As Integer) As ErnaehrungsForm
        Get
            If IsErnaehrung(index) Then
                Return NaehrwertInfo(index)._ErnaehrungsForm
            Else
                Return ErnaehrungsForm.ERR
            End If
        End Get
        Set(value As ErnaehrungsForm)
            If IsErnaehrung(index) Then
                NaehrwertInfo(index)._ErnaehrungsForm = value
                _IsCalculated = True
            End If
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
                If IsAllergen(i) AndAlso Allergen(i) = AllergenInfo.C Then
                    'Liste nach Kommata getrennt
                    If AllergenListe_C <> "" Then
                        AllergenListe_C &= ","
                    End If
                    'Allergenbezeichnung hinzufügen
                    AllergenListe_C &= wb_KomponParam301_Global.kt301Param(i).Bezeichnung
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
                'Ist ein Allergen und ist in Spuren enthalten
                If IsAllergen(i) AndAlso Allergen(i) = AllergenInfo.T Then
                    'Liste nach Kommata getrennt
                    If AllergenListe_T <> "" Then
                        AllergenListe_T &= ","
                    Else
                        AllergenListe_T = "Spuren von "
                    End If
                    'Allergenbezeichnung hinzufügen
                    AllergenListe_T &= wb_KomponParam301_Global.kt301Param(i).KurzBezeichnung
                End If
            Next
        End Get
    End Property

    Public ReadOnly Property AllergenKurzListe_C As String
        Get
            AllergenKurzListe_C = ""
            'Druchläuft alle Indizes
            For i = 1 To maxTyp301
                'Ist ein Allergen und ist enthalten
                If IsAllergen(i) AndAlso Allergen(i) = AllergenInfo.C Then
                    'Liste nach Kommata getrennt
                    If AllergenKurzListe_C <> "" Then
                        AllergenKurzListe_C &= ","
                    End If
                    'Allergenbezeichnung hinzufügen
                    AllergenKurzListe_C &= i.ToString
                End If
            Next
        End Get
    End Property

    Public ReadOnly Property AllergenKurzListe_T As String
        Get
            AllergenKurzListe_T = ""
            'Druchläuft alle Indizes
            For i = 1 To maxTyp301
                'Ist ein Allergen und ist in Spuren enthalten
                If IsAllergen(i) AndAlso Allergen(i) = AllergenInfo.T Then
                    'Liste nach Kommata getrennt
                    If AllergenKurzListe_T <> "" Then
                        AllergenKurzListe_T &= ","
                    End If
                    'Allergenbezeichnung hinzufügen
                    AllergenKurzListe_T &= i.ToString
                End If
            Next
        End Get
    End Property

    Public Property Naehrwert(index As Integer) As Double
        Get
            If Not IsAllergen(index) AndAlso Not IsErnaehrung(index) Then
                Return NaehrwertInfo(index)._Naehrwert
            Else
                Return 0.0
            End If
        End Get
        Set(value As Double)
            If Not IsAllergen(index) AndAlso Not IsErnaehrung(index) Then
                NaehrwertInfo(index)._Naehrwert = value
                _IsCalculated = True
            End If
        End Set
    End Property

    Public Property Wert(index As Integer) As String
        Get
            If IsAllergen(index) Then
                Return NaehrwertInfo(index)._Allergen
            ElseIf IsErnaehrung(index) Then
                Return NaehrwertInfo(index)._ErnaehrungsForm
            ElseIf index <= wb_Global.maxTyp301 Then
                Return wb_Functions.FormatStr(NaehrwertInfo(index)._Naehrwert, 3)
            Else
                Return ""
            End If
        End Get
        Set(value As String)
            If IsAllergen(index) Then
                'Änderungen loggen
                NaehrwertInfo(index)._Allergen = ChangeLogAdd(LogType.Alg, index, NaehrwertInfo(index)._Allergen, StringtoAllergen(value))
            ElseIf IsErnaehrung(index) Then
                'Änderungen loggen
                NaehrwertInfo(index)._ErnaehrungsForm = ChangeLogAdd(LogType.Alg, index, NaehrwertInfo(index)._ErnaehrungsForm, StringtoErnaehrungsForm(value))
            ElseIf index <= wb_Global.maxTyp301 Then
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
            ElseIf IsErnaehrung(Index) Then
                oWert = wb_Functions.ErnaehrungToString(NaehrwertInfo(Index)._Allergen)
                'OrgaBack kann den Wert ERR nicht verarbeiten
                If oWert = "ERR" Then
                    oWert = "N"
                End If
                Return oWert
            Else
                'berechneter Wert für OrgaBack (Tabelle dbo.ArtikelNaehrwerte)
                Return CalculateOrgaBackNaehrwert(NaehrwertInfo(Index)._Naehrwert, Index)
            End If

        End Get
    End Property

    ''' <summary>
    ''' Umrechnungs-Faktor Nährwerte
    ''' Bei OrgaBack werden die Nährwerte in der Tabelle dbo.ArtikelNaehrwerte bezogen auf das Netto-Stückgewicht angegeben.
    ''' 
    ''' Der Umrechnungs-Faktor berechnet sich aus dem Artikel-Nassgewicht und dem Backverlust (ergibt OrgaBack-Netto-Gewicht)
    ''' </summary>
    ''' <returns></returns>
    Public Property FaktorStkGewicht As Double
        Get
            If _FaktorStkGewicht = wb_Global.UNDEFINED Then
                Return 1
            Else
                Return _FaktorStkGewicht
            End If
        End Get
        Set(value As Double)
            _FaktorStkGewicht = value
        End Set
    End Property

    ''' <summary>
    ''' Berechnet den Nährwert für OrgaBack (Tabelle dbo.ArtikelNaehrwerte) aus den WinBack-Nährwert-Angaben (bezogen auf 100gr)
    ''' bezogen auf das Netto-Artikelgewicht.
    ''' Zusätzlich werden die verschiedenen Umrechnungsfaktoren für die unterschiedlichen Einheiten berücksichtigt (gr,mg,ug)
    ''' Die Umrechnungs-Faktoren kommen aus der Tabelle dbo.Naehwerte
    ''' </summary>
    ''' <param name="n"></param>
    ''' <returns></returns>
    Private Function CalculateOrgaBackNaehrwert(n As Double, idx As Integer) As String
        'Der Faktor berechnet sich aus dem Stückgewicht in kg(!) und dem Wert bezogen auf 100gr
        Dim f As Double = wb_KomponParam301_Global.oFaktor(idx) * FaktorStkGewicht
        Return wb_sql_Functions.MsDoubleToString(n * f)
    End Function
    Private Function CalculateOrgaBackNaehrwert(n As Double, idx As Integer, Faktor As Double) As String
        'Der Faktor berechnet sich aus dem Stückgewicht in kg(!) und dem Wert bezogen auf 100gr
        Dim f As Double = wb_KomponParam301_Global.oFaktor(idx) * Faktor
        Return wb_sql_Functions.MsDoubleToString(n * f)
    End Function

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
                    Trace.WriteLine("@E_Fehler bei DatenLink - Index = " & index & " Wert = " & value)
                End Try
            Else
                Trace.WriteLine("@E_Fehler bei DatenLink - Index " & index & " nicht definiert")
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
                Trace.WriteLine("@E_Fehler bei DatenLink - Index " & index & " nicht definiert")
            End If
        End Set
    End Property

    Public WriteOnly Property ffNaehrWert(index As String) As String
        Set(value As String)
            Dim idx As Integer = OpenFoodFactsToIndex(index)
            If idx > 0 Then
                Try
                    Naehrwert(idx) = CDbl(value.Replace(".", ","))
                Catch ex As Exception
                    Trace.WriteLine("@E_Fehler bei OpenFoodFacts - Index = " & index & " Wert = " & value)
                End Try
            Else
                Trace.WriteLine("@E_Fehler bei DatenLink - Index " & index & " nicht definiert")
            End If
        End Set
    End Property

    Public WriteOnly Property ffAllergen(index As String) As String
        Set(value As String)
            Dim idx As Integer = OpenFoodFactsToIndex(index)
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
                Trace.WriteLine("@E_Fehler bei OpenFoodFacts - Index " & index & " nicht definiert")
            End If
        End Set
    End Property

    Public WriteOnly Property PistorWert(index As String) As String
        Set(value As String)
            Dim idx As Integer = PistorToIndex(index)
            If idx > 0 Then
                If IsAllergen(index) Then
                    PistorAllergen(idx) = value
                Else
                    PistorNaehrWert(idx) = value
                End If
            Else
                Trace.WriteLine("@E_Fehler bei Pistor - Index " & index & " nicht definiert")
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
            ElseIf IsErnaehrung(i) Then
                _ktTyp301.ErnaehrungsForm(i) = AddNwtErnaehrungsForm(_ktTyp301.ErnaehrungsForm(i), ErnaehrungsForm(i))
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
    ''' Addiert die ErnährungsForm-Info. Bei der Addition wird nur dann 'Y' zurückgegeben, wenn beide Attribute 'Y' sind.
    ''' </summary>
    ''' <param name="Ernaehrung1"></param>
    ''' <param name="Ernaehrung2"></param>
    Private Function AddNwtErnaehrungsForm(Ernaehrung1 As ErnaehrungsForm, Ernaehrung2 As ErnaehrungsForm) As ErnaehrungsForm

        'Nur wenn beide Parameter 'Y' haben ist das Ergebnis 'Y'
        If Ernaehrung1 = wb_Global.ErnaehrungsForm.Y AndAlso Ernaehrung2 = wb_Global.ErnaehrungsForm.Y Then
            Return wb_Global.ErnaehrungsForm.Y
        Else
            Return wb_Global.ErnaehrungsForm.N
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
        Dim Result As Boolean = True

        'alle Datensätze im Array durchlaufen
        For i = 0 To maxTyp301
            If IsValidParameter(i) Then
                Result = Result AndAlso MySQLdbUpdate(KoNr, i, winback)
            End If
        Next
        'Ergebnis zurückgeben
        Return Result
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
        ElseIf IsErnaehrung(ParamNr) Then
            'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
            sql = KoNr & ", 301, " & ParamNr.ToString & ", '" & wb_Functions.ErnaehrungToString(ErnaehrungsForm(ParamNr)) & "', '" & kt301Param(ParamNr).Bezeichnung & "'"
        Else
            'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
            sql = KoNr & ", 301, " & ParamNr.ToString & ", '" & Wert(ParamNr) & "', '" & kt301Param(ParamNr).Bezeichnung & "'"
        End If

        'Update ausführen
        Return winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateRohParams, sql))
    End Function

    ''' <summary>
    ''' Update ALLER geänderten Komponenten-Parameter in Tabelle 
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
    '''     
    ''' IN WELCHE TABELLE WERDEN DIE WERTE GESCHRIEBEN (VEGAN, HALAL....) Mail vom 16.01.2020/22.10.2021
    ''' Tabelle Allergene festgelegt JE/Mail vom 24.05.2022
    '''     
    ''' Es wird NUR die Variante 0 geschrieben. Alle anderen OrgaBack(!)-Varianten werden, bei Bedarf, von OrgaBack-Reorg-Rezepturen erzeugt und geschrieben.
    ''' Das ist nur notwendig, wenn der Artikel eine OrgaBack-Rezeptur(Stückliste) erhält. Damit wird dann auch die Default-Variante gesetzt. Die Variante 0
    ''' enthält im diesem Fall nur das Default-Rezept. Die Nährwerte werden von der Stückliste anhand der Standard-Variante in Variante 0 geschrieben!
    ''' 
    ''' Für reine Rohstoffe werden beim Reorg KEINE Werte in die anderen Varianten geschrieben.
    '''       Eventuell muss dann doch in alle Varianten geschreiben werden...
    '''       Vorerst wird eine Hinweismeldung ausgegeben (laut Info Ute Wamser machen Varianten bei Rohstoffen keinen Sinn)
    ''' 
    ''' Damit die Berechnung der Nährwerte auf der OrgaBack-Seite anhand der Varianten funktioniert, muss in dem Artikel/Halbfertig-Produkt der Stückliste
    ''' die zum Artikel passende Einheit hinterlegt sein!
    ''' 
    ''' FÜR ALLE WINBACK-ARTIKEL UND HALBFERITG-PRODUKTE WERDEN DAHER DIE NÄHRWERTE IMMER IN MEHREN EINHEITEN (STK UND KG) IN DIE ORGABACK-DB GESCHRIEBEN
    ''' (Problem Niehaves vom 30.03.2023)
    ''' Die entsprechenden Einheiten müssen beim Artikel in OrgaBack vorhanden sein (Abfrage [dbo].Umrechnung zur ArtikelNr)
    ''' 
    ''' WinBack berechnet die Nährwerte bezogen auf 100g. In OrgaBack müssen die Nährwerte bezogen auf die Einheit in der Datenbank-Tabelle abgelegt werden.
    ''' Die Umrechnung für die Ausgabe der /100g-Werte erfolgt in OrgaBack dann durch Berechnung anhand des Netto-Gewichtes.
    ''' Die Funktion wird mit Parameter Artikelnummer und Unit aufgerufen. Unit hat entweder den Wert kg(11) oder Stk(0)
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function MsSQLdbUpdate(KoAlNum As String, Unit As Integer, orgaback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
        Dim sql As String
        'Result OK
        Dim Result As Boolean = True
        'Umrechnungs-Faktor(Einheiten)
        Dim x As ArtikelEinheiten
        'Stücklisten-Varianten
        Dim StkLstVar As List(Of Integer) = GetStkListenVarianten(KoAlNum, orgaback)

        'Liste aller Einheiten zu dieser ArtikelNummer
        sql = wb_sql_Selects.setParams(wb_sql_Selects.mssqlSelUmrechnung, KoAlNum, Unit)
        Umrechnung.Clear()

        'Default-Einheit
        x = New ArtikelEinheiten With {._InEinheit = Unit}
        If Unit = obEinheitStk Then
            x._Umrechnungsfaktor = FaktorStkGewicht
        Else
            x._Umrechnungsfaktor = FaktorStkGewicht
        End If
        Umrechnung.Add(x)

        'alle anderen Einheiten zu dieser Artikelnummer
        If orgaback.sqlSelect(sql) Then
            While orgaback.Read
                x = New ArtikelEinheiten With {._InEinheit = orgaback.iField("InEinheit")}
                If Unit = obEinheitStk Then
                    x._Umrechnungsfaktor = wb_Functions.StrToDouble(orgaback.sField("Umrechnungsfaktor")) * FaktorStkGewicht
                Else
                    x._Umrechnungsfaktor = wb_Functions.StrToDouble(orgaback.sField("Umrechnungsfaktor")) * FaktorStkGewicht
                End If
                Umrechnung.Add(x)
            End While
        End If
        'Lesen beendet
        orgaback.CloseRead()

        'zunächst werden alle vorhandenen Einträge zur Komponente für alle Stücklistenvarianten in der Tabelle ArtikelNaehrwerte und ArtikelAllergene gelöscht.
        For Each x In Umrechnung
            sql = wb_sql_Selects.setParams(wb_sql_Selects.mssqlDeleteNwt, KoAlNum, x._InEinheit)
            orgaback.sqlCommand(sql)
        Next

        'Alle Nährwerte zu dieser Artikelnummer in allen Stücklistenvarianten löschen
        sql = wb_sql_Selects.setParams(wb_sql_Selects.mssqlDeleteAlg, KoAlNum)
        orgaback.sqlCommand(sql)

        'dann alle neuen Felder für alle Stücklistenvarianten wieder schreiben
        For Each s In StkLstVar
            'alle Datensätze im Array durchlaufen (Nährwerte/Allergene beginnen bei Index 1)
            For i = 1 To maxTyp301
                If IsUsedParameter(i) AndAlso IsOrgaBackParameter(i) Then

                    'Allergene haben in OrgaBack einen eigene Tabelle
                    If IsAllergen(i) Then
                        If (NaehrwertInfo(i)._Allergen > wb_Global.AllergenInfo.N) Then
                            'Debug.Print("Update OrgaBack Parameter " & i & " Wert " & Wert(i))
                            sql = wb_sql_Selects.setParams(wb_sql_Selects.mssqlInsertAlg, KoAlNum, i, oWert(i), s.ToString)
                            Result = (orgaback.sqlCommand(sql) < 0)
                        End If
                    ElseIf IsErnaehrung(i) Then
                        If (NaehrwertInfo(i)._ErnaehrungsForm > wb_Global.ErnaehrungsForm.X) Then
                            'Debug.Print("Update OrgaBack Parameter " & i & " Wert " & Wert(i))
                            sql = wb_sql_Selects.setParams(wb_sql_Selects.mssqlInsertErng, KoAlNum, i, oWert(i), s.ToString)
                            Result = (orgaback.sqlCommand(sql) < 0)
                        End If
                    Else
                        If (NaehrwertInfo(i)._Naehrwert > 0) Then
                            'Debug.Print("Update OrgaBack Parameter " & i & " Wert " & Wert(i))

                            'Schreiben Nährwert in allen vorhandenen Einheiten (mit der entsprechenden Umrechnung)
                            For Each x In Umrechnung
                                sql = wb_sql_Selects.setParams(wb_sql_Selects.mssqlInsertNwt, KoAlNum, i, CalculateOrgaBackNaehrwert(NaehrwertInfo(i)._Naehrwert, i, x._Umrechnungsfaktor), x._InEinheit, s.ToString)
                                Result = (orgaback.sqlCommand(sql) < 0)
                            Next

                            ''Umrechnen von kg in Stk oder umgekehrt
                            'Select Case Unit
                            '    Case wb_Global.obEinheitKilogramm
                            '        sql_b = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertNwt, KoAlNum, i, oWert(i), wb_Global.obEinheitStk, "0")
                            '    Case wb_Global.obEinheitStk
                            '        sql_b = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertNwt, KoAlNum, i, oWert(i), wb_Global.obEinheitKilogramm, "0")
                            'End Select
                        End If
                    End If

                    ''Update-Statement wird dynamisch erzeugt (nur wenn auch Daten vorhanden sind)
                    'If sql <> "" AndAlso orgaback.sqlCommand(sql) < 0 Then
                    '    Result = False
                    'End If

                    'If sql_b <> "" AndAlso orgaback.sqlCommand(sql_b) < 0 Then
                    '    Result = False
                    'End If
                End If
            Next
        Next
        Return Result
    End Function

    ''' <summary>
    ''' Update EINES geänderte Komponenten-Parameter in Tabelle.
    ''' Da REPLACE bei msSQL nicht funktioniert wird zuerst versucht, per UPDATE den Datensatz zu aktualisieren. Wenn 
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
    Public Function MsSQLdbUpdate(KoAlNum As String, ParamNr As Integer, Unit As Integer, orgaback As wb_Sql, StkLstVar As List(Of Integer)) As Boolean
        'Update-Statement wird dynamisch erzeugt    
        Dim sql_Delete As String
        Dim sql_Insert As String
        Dim Result As Boolean = True

        'Parameter-Nummer ist gültig
        If IsValidParameter(ParamNr) AndAlso IsOrgaBackParameter(ParamNr) Then

            For Each s In StkLstVar

                'Allergene haben in OrgaBack einen eigene Tabelle
                If IsAllergen(ParamNr) Then
                    sql_Delete = (wb_sql_Selects.setParams(wb_sql_Selects.mssqlDeleteParamAlg, KoAlNum, ParamNr, s.ToString))
                    sql_Insert = (wb_sql_Selects.setParams(wb_sql_Selects.mssqlInsertAlg, KoAlNum, ParamNr, oWert(ParamNr), s.ToString))
                    'Debug.Print("Update OrgaBack Parameter " & i & " Wert " & Wert(i))
                ElseIf IsErnaehrung(ParamNr) Then
                    sql_Delete = (wb_sql_Selects.setParams(wb_sql_Selects.mssqlDeleteParamAlg, KoAlNum, ParamNr, s.ToString))
                    sql_Insert = (wb_sql_Selects.setParams(wb_sql_Selects.mssqlInsertAlg, KoAlNum, ParamNr, oWert(ParamNr), s.ToString))
                    'Debug.Print("Update OrgaBack Parameter " & i & " Wert " & Wert(i))
                Else
                    sql_Delete = (wb_sql_Selects.setParams(wb_sql_Selects.mssqlDeleteParamNwt, KoAlNum, ParamNr, Unit, s.ToString))
                    sql_Insert = (wb_sql_Selects.setParams(wb_sql_Selects.mssqlInsertNwt, KoAlNum, ParamNr, oWert(ParamNr), Unit, s.ToString))
                    'Debug.Print("Update OrgaBack Parameter " & i & " Wert " & Wert(i))
                End If

                'Update-Statement wird versucht
                Select Case orgaback.sqlCommand(sql_Delete)
                    Case < 0
                        'Fehler bei Zugriff auf die Datenbank
                        Result = False
                    Case 0, 1
                        'Delete war erfolgreich - Insert Datensatz versuchen
                        If orgaback.sqlCommand(sql_Insert) < 0 Then
                            'Insert fehlgeschlagen - Fehler
                            Result = False
                        End If
                    Case Else
                        'Unbekannter Fehler 
                        Result = False
                End Select
            Next
        End If

        'Keine Änderung der Daten notwendig
        Return Result
    End Function

    Public Function GetStkListenVarianten(KoAlNum As String, orgaback As wb_Sql) As List(Of Integer)
        ' Liste aller Stücklisten-Varianten zu diesem Artikel
        Dim Result As New List(Of Integer)
        Dim sql As String = wb_sql_Selects.setParams(wb_sql_Selects.mssqlSelStkListeVariante, KoAlNum)
        If orgaback.sqlSelect(sql) Then
            While orgaback.Read
                Dim StkListeVariante As Integer = orgaback.iField("StuecklistenVariantenNr")
                Result.Add(StkListeVariante)
            End While
        End If
        'Lesen beendet
        orgaback.CloseRead()
        'Liste zurückgeben
        Return Result
    End Function

End Class
