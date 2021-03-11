Imports System.Reflection
Imports WinBack.wb_Global.KomponTypen

Public Class wb_Produktionsschritt
    Implements IComparable

    Private _parentStep As wb_Produktionsschritt
    Private _childSteps As New ArrayList()

    Private Shared _SortOrder As wb_Global.SortOrder
    Private _SortKriteriumBackPlan As String
    Private _SortKriteriumProdPlan As String

    Private _Optimiert As Boolean = False
    Private _Linie As Integer = wb_Global.UNDEFINED
    Private _LinienGruppe As Integer = wb_Global.UNDEFINED
    Private _ArtikelLinienGruppe As Integer = wb_Global.UNDEFINED
    Private _LinienGruppeZusatzText As String = ""
    Private _Typ As String
    Private _Tour As String
    Private _ChargenNummer As String
    Private _ARS_Index As Integer
    Private _ARZ_Index As Integer
    Private _ArtikelIndex As Integer
    Private _Schritt As Integer
    Private _RunIndex As Integer
    Private _AuftragsNummer As String
    Private _IstInProduktion As Boolean
    Private _MengeInProduktion As Double
    Private _Status As Integer

    Private _ArtikelNummer As String
    Private _ArtikelBezeichnung As String
    Private _ArtikelNr As Integer
    Private _StkGewicht As Double
    Private _RezeptNummer As String
    Private _RezeptBezeichnung As String
    Private _RezeptNr As Integer
    Private _RezeptVar As Integer

    Private _OptChargekg As Double
    Private _MinChargekg As Double
    Private _MaxChargekg As Double
    Private _ProdVorlauf As Integer
    Private _Aufloesen As Boolean
    Private _FreigabeProduktion As Boolean

    Private _Sollwert As String
    Private _Istwert As String
    Private _SollwertProzent As String
    Private _Sollwert_kg As Double
    Private _Sollmenge_Stk As Double
    Private _Sollwert_TeilungText As String
    Private _ParamNr As Integer
    Private _Einheit As String
    Private _TeigChargen As wb_Global.ChargenMengen
    Private _Bestellt_Stk As Double
    Private _LagerBestand As Double = wb_Global.UNDEFINED
    Private _LagerOrt As String = ""
    Private _StartZeit As DateTime

    Private _Bestellt_SonderText As String
    Private _Bestellt_KundeNr As String
    Private _Bestellt_Kunde As String
    Private _Bestellt_Menge_Stk As String
    Private _Bestellt_Text As String
    Private _Bestellt_Drucken As Boolean
    Private _BestelltSonderText_Drucken As Boolean

    Private _KO_Typ As wb_Global.KomponTypen
    Private _KO_Nr As Integer
    Private _KO_Nummer As String
    Private _KO_Bezeichnung As String
    Private _KO_Einheit As String
    Private _KO_Charge As String


    ''' <summary>
    ''' Kopiert alle Properties dieser Klasse auf die Properties der übergebenen Klasse.
    ''' Geschrieben werden nur die Properties, die nicht als ReadOnly deklariert sind.
    ''' 
    ''' aus: https://stackoverflow.com/questions/531384/how-to-loop-through-all-the-properties-of-a-class
    '''
    ''' Dient dazu, die Inhalte eines Produktions-Schrittes auf einen anderen zu kopieren.
    ''' Durch die Schleife über alle Properties ist die Funktion unabhängig von eventuellen Erweiterungen.
    ''' </summary>
    ''' <param name="rs">wb_Produktionsschritt nimmt die Werte der Properties der Klasse auf</param>
    Public Sub CopyFrom(rs As wb_Produktionsschritt)
        Dim _type As Type = Me.GetType()
        Dim properties() As PropertyInfo = _type.GetProperties()
        For Each _property As PropertyInfo In properties
            If _property.CanWrite And _property.CanRead Then
                _property.SetValue(Me, _property.GetValue(rs, Nothing))
            End If
        Next
    End Sub

    ''' <summary>
    ''' Kopiert alle Artikel-Daten in den aktuellen Produktions-Schritt
    ''' </summary>
    ''' <param name="rs">wb_Komponenten hält alle notwendigen Werte der Artikel für die Produktion</param>
    Public Sub CopyFromKomponenten(rs As wb_Komponente, ChargenTyp As Integer)
        With rs
            Typ = ChargenTyp
            ArtikelNummer = .Nummer
            StkGewicht = wb_Functions.StrToDouble(.ArtikelChargen.StkGewicht)
            RezeptNr = .RzNr
            RezeptNummer = .RezeptNummer
            RezeptVar = 1
            RezeptBezeichnung = .RezeptName
            LinienGruppe = .LinienGruppe
            ArtikelLinienGruppe = .iArtikelLinienGruppe
            OptChargekg = .ArtikelChargen.OptCharge.fMengeInkg
            MaxChargekg = .ArtikelChargen.MaxCharge.fMengeInkg
            MinChargekg = .ArtikelChargen.MinCharge.fMengeInkg

            LagerBestand = wb_Functions.StrToDouble(.Bilanzmenge)
            LagerOrt = .Lagerort
            ProdVorlauf = .ProdVorlauf
            FreigabeProduktion = .FreigabeProduktion
        End With
    End Sub

    Public Sub CopyFromRezeptschritt(rs As wb_Produktionsschritt)
        'alle Properties kopieren
        CopyFrom(rs)
        'Komponentenzeile in Artikel-Felder kopieren
        _ArtikelNummer = _KO_Nummer
        _ArtikelBezeichnung = _KO_Bezeichnung
        _ArtikelNr = _KO_Nr
        _Einheit = _KO_Einheit
        _Typ = _KO_Typ
    End Sub

    Public Sub CopyFromRezeptSchritt(rs As wb_Rezeptschritt, Faktor As Double)
        With rs
            'als Rezepturschritt kennzeichnen
            Typ = .Type
            'Komponenten-Nummer
            ArtikelNummer = .Nummer
            'Komponenten-Bezeichnung
            ArtikelBezeichnung = .Bezeichnung
            'interne Komponenten-Nummer
            ArtikelNr = .RohNr
            'Verweis auf Rezeptnummer
            RezeptNr = .RezeptNr
            'Freigabe Produktion
            FreigabeProduktion = .FreigabeProduktion

            'Einheit
            If wb_Functions.TypeHatEinheit(Typ) Then
                Einheit = .Einheit
            Else
                Einheit = ""
            End If

            'Sollwert auf Rezeptgröße umrechnen
            If wb_Functions.TypeIstSollMenge(.Type, .ParamNr) Then
                Sollwert_kg = wb_Functions.StrToDouble(.Sollwert) * Faktor
                Sollwert = wb_Functions.FormatStr(Sollwert_kg.ToString, 3)
                SollwertProzent = wb_Functions.FormatStr(.SollwertProzent, 3)
            Else
                Sollwert = .Sollwert
            End If
            'Parameter Nummer
            ParamNr = .ParamNr
            'Lagerort(wird benötigt um den aktuellen Lagerbestand in der Produktionsplanung anzuzeigen)
            LagerOrt = .LagerOrt
        End With
    End Sub

    '' <summary>
    '' Create a new step with the given parent
    '' </summary>
    '' <param name="parent">The parent step</param>
    '' <param name="name">The name of this step</param>
    Public Sub New(parent As wb_Produktionsschritt, Bezeichnung As String)
        _parentStep = parent
        Me.ArtikelBezeichnung = Bezeichnung
        If Not (_parentStep Is Nothing) Then
            parent._childSteps.Add(Me)
        End If
    End Sub 'New

    '' <summary>
    '' Parent dieses Produktionsschrittes
    '' </summary>
    Public Property ParentStep() As wb_Produktionsschritt
        Get
            Return _parentStep
        End Get
        Set(ByVal value As wb_Produktionsschritt)
            _parentStep = value
        End Set
    End Property

    '' <summary>
    '' Liste aller Child-Produktionsschritte
    '' </summary>
    Public ReadOnly Property ChildSteps() As IList
        Get
            Return _childSteps
        End Get
    End Property

    ''' <summary>
    ''' Sortieren BackListe
    ''' Die einzelnen Produktions-Schritte werden sortiert nach Teig(Rezeptnummer), Artikelnummer und Tour
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Return String.Compare(SortKriterium, DirectCast(obj, wb_Produktionsschritt).SortKriterium)
    End Function

    Public Sub SortBackZettel()
        _SortOrder = wb_Global.SortOrder.BackZettel
        _childSteps.Sort()
    End Sub

    Public Sub SortProduktionsPlan()
        _SortOrder = wb_Global.SortOrder.ProdPlan
        _childSteps.Sort()
    End Sub

    ''' <summary>
    ''' Charge als produziert markieren.
    ''' Teigzettel/Backzettel gedruckt und/oder Produktionsliste übertragen
    ''' </summary>
    Public Sub ChargeWirdProduziert()
        If Typ = KO_TYPE_ARTIKEL Or Typ = KO_ZEILE_ARTIKEL Then
            IstInProduktion = True
        End If
    End Sub

    ''' <summary>
    ''' Flag Charge ist in Produktion übertragen worden (Teigliste/Backzettel/csv-File)
    ''' </summary>
    ''' <returns></returns>
    Public Property IstInProduktion As Boolean
        Get
            Return _IstInProduktion
        End Get
        Set(value As Boolean)
            _IstInProduktion = value
        End Set
    End Property

    ''' <summary>
    ''' Istwert Menge in Produktion aus vorherigen Berechnungen/Produktionsplanungen.
    ''' Der Wert wird von OrgaBack übernommen.
    ''' </summary>
    ''' <returns></returns>
    Public Property MengeInProduktion As Double
        Get
            Return _MengeInProduktion
        End Get
        Set(value As Double)
            _MengeInProduktion = value
        End Set
    End Property

    ''' <summary>
    ''' Startzeit formatiert ausgeben
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property VirtTreeStart As String
        Get
            Select Case Typ

                Case KO_TYPE_ARTIKEL, KO_ZEILE_ARTIKEL, KO_ZEILE_REZEPT
                    'Datum Produktionsplan für den ....
                    Dim ProdStartZeit As DateTime = DateTime.Parse(wb_GlobalSettings.ProdPlanDatum)
                    'gültige Startzeit eingetragen
                    If StartZeit <> wb_Global.wbNODATE Then
                        If ProdVorlauf > 0 Then
                            Dim BerechneteStartZeit As DateTime = wb_Functions.AddDateTime(ProdStartZeit, StartZeit, True).Subtract(TimeSpan.FromHours(ProdVorlauf))
                            Return BerechneteStartZeit.ToString("dd.MM.yy hh:mm")
                        Else
                            Return wb_Functions.AddDateTime(ProdStartZeit, StartZeit, True).ToString("dd.MM.yy hh:mm")
                        End If
                    Else
                        Return ""
                    End If

                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public ReadOnly Property VirtTreeCharge As String
        Get
            If _Optimiert Then
                Return ""
            Else
                Return _ChargenNummer
            End If
        End Get
    End Property

    Public ReadOnly Property VirtTreeNummer As String
        Get
            If Typ = wb_Global.KomponTypen.KO_ZEILE_REZEPT Then
                Return RezeptNummer
            Else
                Return ArtikelNummer
            End If
        End Get
    End Property

    ''' <summary>
    ''' Bezeichnung. Anzeige im VirtualTree
    ''' </summary>
    ''' <returns>String - Bezeichnung</returns>
    Public ReadOnly Property VirtTreeBezeichnung() As String
        Get
            If Typ = KO_ZEILE_REZEPT Then
                Return RezeptBezeichnung
            ElseIf wb_Functions.TypeIstText(Typ) Then
                Return _Sollwert
            Else
                Return _ArtikelBezeichnung
            End If
        End Get
    End Property

    ''' <summary>
    ''' Bei zusammengefassten Chargen wird hier die Information über die neue Charge ausgegeben.
    ''' 
    ''' Bei Produktionsdaten aus OrgaBack steht hier der Sondertext der Bestellung.
    ''' Bei den Sondertexten werden evtl. enthaltene CR und CRLF durch Leerzeichen ersetzt.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property VirtTreeKommentar As String
        Get
            If _Optimiert Then
                If _childSteps.Count > 0 Then
                    If _ChargenNummer <> "" Then
                        Return "zusammengefasst in Charge " & _ChargenNummer
                    Else
                        Return "zusammengefasst"
                    End If
                Else
                    Return "Fehler"
                End If
            Else
                If Bestellt_SonderText IsNot Nothing Then
                    Return _Bestellt_SonderText.Replace(vbCrLf, " ")
                Else
                    Return ""
                End If
            End If
        End Get
    End Property

    Public ReadOnly Property VirtTreeLinie As String
        Get
            Select Case Typ
                Case KO_TYPE_ARTIKEL, KO_ZEILE_ARTIKEL
                    If _ArtikelLinienGruppe > 0 Then
                        Return wb_Linien_Global.GetKurzNameFromLinienGruppe(_ArtikelLinienGruppe)
                    Else
                        Return ""
                    End If

                Case KO_ZEILE_REZEPT
                    If _LinienGruppe = wb_Global.LinienGruppeSauerteig Then
                        Return "ST"
                    ElseIf _LinienGruppe > 0 Then
                        Return wb_Linien_Global.GetKurzNameFromLinienGruppe(_LinienGruppe)
                    Else
                        Return ""
                    End If

                Case Else
                    Return ""
            End Select

            'TEST
            'Return _ArtikelLinienGruppe.ToString & "/" & _LinienGruppe
        End Get
    End Property

    Public ReadOnly Property VirtTreeTour As String
        Get
            If _Tour = "0" Then
                Return "-"
            Else
                If Typ = KO_ZEILE_ARTIKEL Then
                    Return Tour
                Else
                    Return ""
                End If
            End If
        End Get
    End Property

    ''' <summary>
    ''' Sollwert. Anzeige im VitualTree. Unterscheidung anhand der Type:
    '''     -   Artikel-Chargen-Zeilen  Sollmenge in Stück
    '''     -   Rezept-Chargen-Zeilen   Sollmenge in kg
    '''     -   Rezept-Schritte         Sollwert als formatierter Zahlenwert
    '''                                 enthält der Rezept-Sollwert einen Textbaustein, wird kein Sollwert ausgegeben
    ''' 
    ''' </summary>
    ''' <returns>String - Sollwert</returns>
    Public Property VirtTreeSollwert As String
        Get
            If Typ = KO_ZEILE_ARTIKEL Then
                If Sollmenge_Stk <> 0 Then
                    Return wb_Functions.FormatStr(Sollmenge_Stk, 0)
                Else
                    Return ""
                End If

            ElseIf Typ = KO_ZEILE_REZEPT Then
                Return wb_Functions.FormatStr(Sollwert_kg, 3)

            Else
                Select Case Typ
                    Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE
                        Return ""
                    Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE, KO_TYPE_WASSERKOMPONENTE
                        Return wb_Functions.FormatStr(_Sollwert, 3)
                    Case KO_TYPE_SAUER_ZUGABE
                        If ParamNr = 1 Then
                            Return wb_Functions.FormatStr(_Sollwert, 3)
                        Else
                            Return wb_Functions.FormatStr(_SollwertProzent, 3)
                        End If
                    Case Else
                        Return _Sollwert
                End Select
            End If
        End Get
        Set(value As String)
            '_Sollwert = value
        End Set
    End Property

    ''' <summary>
    ''' Lager-Bestand. Anzeige im VirtualTree. Unterscheidung anhand der Type:
    '''     -   Artikel-Chargen-Zeile   Lagerbestand in Stück
    '''     -   Rezept-Chargen-Zeile    keine Anzeige
    '''     -   Rezept-Schritte         Anzeige Lagerbestand als formatierter Zahlenwert in kg
    '''     
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property VirtTreeBestand As String
        Get
            If Typ = KO_ZEILE_ARTIKEL Then
                'TODO Hier könnte der Froster-Bestand stehen ?? mit Kennzeichen FR? aus OrgaBack abfragen
                'Return wb_Functions.FormatStr(_LagerBestand, 3)
                Return ""

            ElseIf Typ = KO_ZEILE_REZEPT Then
                Return ""

            Else
                Select Case Typ
                    Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE
                        Return ""
                    Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE
                        'unrealistische und Werte kleiner/gleich Null ausblenden
                        If LagerBestand > 0 And LagerBestand < wb_Global.MAXLAGERBESTAND Then
                            Return wb_Functions.FormatStr(LagerBestand, 3)
                        Else
                            Return "-"
                        End If
                    Case Else
                        Return ""
                End Select
            End If
        End Get
    End Property

    ''' <summary>
    ''' Istwert Anzeige im VirtualTree. Unterscheidung anhand der Type:
    '''     -   Artikel-Chargen-Zeile   
    '''     -   Rezept-Chargen-Zeile    
    '''     -   Rezept-Schritte         
    '''     
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property VirtTreeIstwert As String
        'TODO wird diese Funktion gebraucht ??? (virtuelle Linie ?) - Kommetar verbessern !!!
        Get
            If Typ = KO_ZEILE_ARTIKEL Then
                If Sollmenge_Stk <> 0 Then
                    Return wb_Functions.FormatStr(Sollmenge_Stk, 0)
                Else
                    Return ""
                End If

            ElseIf Typ = KO_ZEILE_REZEPT Then
                Return wb_Functions.FormatStr(Sollwert_kg, 3)

            Else
                Select Case Typ
                    Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE
                        Return ""
                    Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE, KO_TYPE_WASSERKOMPONENTE
                        Return wb_Functions.FormatStr(_Istwert, 3)
                    Case KO_TYPE_SAUER_ZUGABE
                        If ParamNr = 1 Then
                            Return wb_Functions.FormatStr(_Istwert, 3)
                        Else
                            Return wb_Functions.FormatStr(_SollwertProzent, 3)
                        End If
                    Case Else
                        Return _Istwert
                End Select
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gibt eine boolschen Wert zurück, ob der aktuelle Schritt in ListUndLabel gedruckt werden soll. Bei Dummy-Artikeln
    ''' wird keine Zeile im Report ausgegeben.
    ''' </summary>
    ''' <returns>Flag Zeile in Report drucken</returns>
    Public ReadOnly Property VirtTreePrintBackZettel As Boolean
        Get
            If Typ = KO_ZEILE_DUMMYARTIKEL Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gibt eine boolschen Wert zurück, ob der aktuelle Schritt in ListUndLabel gedruckt werden soll. Bei optimierten Zeilen
    ''' wird keine Zeile im Report ausgegeben.
    ''' </summary>
    ''' <returns>Flag Zeile in Report drucken</returns>
    Public ReadOnly Property VirtTreePrintTeigListe As Boolean
        Get
            If _Optimiert Then
                Return False
            End If
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Gibt FÜr Artikelzeilen die Summe aller Sollwerte der Child-Steps zurück
    ''' Zur Anzeige der Teigmenge bzw. Teig-Gesamt-Menge in der Backliste
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property VirtTreeSumSollwerte As Double
        Get
            VirtTreeSumSollwerte = 0
            If Typ = wb_Global.KomponTypen.KO_ZEILE_ARTIKEL Then
                For Each c As wb_Produktionsschritt In ChildSteps
                    VirtTreeSumSollwerte = VirtTreeSumSollwerte + c.Sollwert_kg
                Next
            End If
        End Get
    End Property

    Public ReadOnly Property VirtTreeEinheit As String
        Get
            If Typ = KO_ZEILE_ARTIKEL Then
                If Sollmenge_Stk <> 0 Then
                    Return "Stk"
                Else
                    Return ""
                End If

            ElseIf Typ = KO_ZEILE_REZEPT Then
                Return "kg"
            Else
                Return Einheit
            End If
        End Get
    End Property

    Public ReadOnly Property VirtTreeEinheitBestand As String
        Get
            Select Case Typ
                Case KO_ZEILE_ARTIKEL
                    Return ""
                Case KO_ZEILE_REZEPT
                    Return ""
                Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE
                    Return ""
                Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE
                    Return Einheit
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public Property AuftragsNummer As String
        Get
            Return _AuftragsNummer
        End Get
        Set(value As String)
            _AuftragsNummer = value
        End Set
    End Property

    Public Property Typ As String
        Get
            Return _Typ
        End Get
        Set(value As String)
            _Typ = value
        End Set
    End Property

    Public Property ArtikelNummer As String
        Get
            Return _ArtikelNummer
        End Get
        Set(value As String)
            _ArtikelNummer = value
            setSortKriterium()
        End Set
    End Property

    Public Property ArtikelBezeichnung As String
        Get
            Return _ArtikelBezeichnung
        End Get
        Set(value As String)
            _ArtikelBezeichnung = value
        End Set
    End Property

    Public Property ArtikelNr As Integer
        Get
            Return _ArtikelNr
        End Get
        Set(value As Integer)
            _ArtikelNr = value
        End Set
    End Property

    Public Property StkGewicht As Double
        Get
            Return _StkGewicht
        End Get
        Set(value As Double)
            _StkGewicht = value
        End Set
    End Property

    Public Property RezeptNummer As String
        Get
            Return _RezeptNummer
        End Get
        Set(value As String)
            _RezeptNummer = value
            setSortKriterium()
        End Set
    End Property

    Public Property RezeptNr As Integer
        Get
            Return _RezeptNr
        End Get
        Set(value As Integer)
            _RezeptNr = value
        End Set
    End Property

    Public Property RezeptVar As Integer
        Get
            Return _RezeptVar
        End Get
        Set(value As Integer)
            'Variante 0 wird automatisch in 1 gewandelt
            'TODO Achtung Sauerteig !!
            _RezeptVar = Math.Max(value, 1)
        End Set
    End Property

    Public Property RezeptBezeichnung As String
        Get
            Return _RezeptBezeichnung
        End Get
        Set(value As String)
            _RezeptBezeichnung = value
        End Set
    End Property

    ''' <summary>
    ''' Produktions-Vorlauf in [h].
    ''' Wird für die Produktionsplanung für Rohstoffe mit anhängender Rezeptur verwendet. Der Produktionsvorlauf
    ''' definiert, wie weit im Voraus die Produktion für den Rohstoff gestartet werden muss. (Reifezeiten....)
    ''' 
    ''' Datenfeld winback.Komponenten.KA_Prod_Linie (Unsigned TinyInt)
    ''' </summary>
    ''' <returns></returns>
    Public Property ProdVorlauf As Integer
        Get
            Return _ProdVorlauf
        End Get
        Set(value As Integer)
            _ProdVorlauf = value
        End Set
    End Property

    Public Property Sollwert As String
        Get
            Return _Sollwert
        End Get
        Set(value As String)
            _Sollwert = value
        End Set
    End Property

    Public Property SollwertProzent As String
        Get
            Return _SollwertProzent
        End Get
        Set(value As String)
            _SollwertProzent = value
        End Set
    End Property

    Public Property Sollwert_kg As Double
        Get
            Return _Sollwert_kg
        End Get
        Set(value As Double)
            _Sollwert_kg = value
        End Set
    End Property

    Public Property Sollmenge_Stk As Double
        Get
            Return _Sollmenge_Stk
        End Get
        Set(value As Double)
            _Sollmenge_Stk = value
        End Set
    End Property

    Public Property ParamNr As Integer
        Get
            Return _ParamNr
        End Get
        Set(value As Integer)
            _ParamNr = value
        End Set
    End Property

    Public Property Sollwert_TeilungText As String
        Get
            Return _Sollwert_TeilungText
        End Get
        Set(value As String)
            _Sollwert_TeilungText = value
        End Set
    End Property

    Public Property Einheit As String
        Get
            Return _Einheit
        End Get
        Set(value As String)
            _Einheit = value
        End Set
    End Property
    Public ReadOnly Property obEinheit As Integer
        Get
            Return wb_Einheiten_Global.getobEinheitFromText(_Einheit, wb_Global.obEinheitStk)
        End Get
    End Property

    Public Property Bestellt_Stk As Double
        Get
            Return _Bestellt_Stk
        End Get
        Set(value As Double)
            _Bestellt_Stk = value
        End Set
    End Property

    Public ReadOnly Property Differenz_Stk As Double
        Get
            Return Bestellt_Stk - Sollmenge_Stk
        End Get
    End Property

    ''' <summary>
    ''' Enthält, getrennt durch CRLF alle Sondertexte bzw. Kundenbestellungen für diese Tour.
    ''' Aus pq_Produktionsauftrag
    ''' </summary>
    ''' <returns></returns>
    Public Property Bestellt_SonderText As String
        Get
            Return _Bestellt_SonderText
        End Get
        Set(value As String)
            _Bestellt_SonderText = value
        End Set
    End Property

    Private Sub SplitSonderText(t As String)
        'Aufteilen in die einzelnen Sondertexte
        Dim SonderText() As String = Split(t, vbCrLf)

        'Flag KundenBestellung und Sondertexte drucken
        If _BestelltSonderText_Drucken And Not _Bestellt_Drucken And (SonderText.Length >= 2) Then
            'Prüfen und Markieren wenn Sondertexte zu den Kunden/Filialbestellungen vorhanden sind
            For j = 1 To SonderText.Length Step 2
                'Feld Bemerkungs-Text ist leer
                If Not SplitSpaceContainsText(SonderText(j)) Then
                    'alle anderen Text-Felder markieren (nicht drucken)
                    SonderText(j - 1) = "NoPrint NoPrint"
                    SonderText(j - 0) = "NoPrint NoPrint"
                End If
            Next
        End If

        Dim i As Integer = 0
        For Each s As String In SonderText
            'Schleifenzähler (Aufteilung der einzlnen Positionen)
            i += 1
            Select Case i
                Case 1
                    'erster Eintrag Kunde-Nummer und Kunde-Name (getrennt durch Space)
                    SplitSpace(s, _Bestellt_KundeNr, _Bestellt_Kunde)
                Case 2
                    'erster Eintrag Stückzahl und Bemerkung-Text (getrennt durch Space)
                    SplitSpace(s, _Bestellt_Menge_Stk, _Bestellt_Text)
                Case 3
                    'nachfolgender Eintrag Kunde-Nummer und Kunde-Name (CRLF einfügen)
                    SplitSpace(s, _Bestellt_KundeNr, _Bestellt_Kunde, True)
                Case 4
                    'nachfolgender Eintrag Stückzahl und Bemerkung-Text (CRLF einfügen)
                    SplitSpace(s, _Bestellt_Menge_Stk, _Bestellt_Text, True)
                    'Schleife wieder auf Anfang
                    i = i - 2
            End Select
        Next
    End Sub

    Private Function SplitSpaceContainsText(s As String) As Boolean
        Dim x() As String = {"", ""}
        If s <> "" Then
            x = Split(s, " ", 2)
        End If
        Return (x(1) <> "")
    End Function

    Private Sub SplitSpace(s As String, ByRef s1 As String, ByRef s2 As String, Optional crlf As Boolean = False)
        Dim x() As String = {"", ""}
        If s <> "" Then
            x = Split(s, " ", 2)
        End If

        If x(0) <> "NoPrint" And x(1) <> "NoPrint" Then
            If crlf Then
                s1 = s1 & vbCrLf & x(0)
                s2 = s2 & vbCrLf & x(1)
            Else
                s1 = x(0)
                s2 = x(1)
            End If
        Else
            If Not crlf Then
                s1 = ""
                s2 = ""
            End If
        End If
    End Sub

    Public ReadOnly Property Bestellt_Kunde As String
        Get
            Return _Bestellt_Kunde
        End Get
    End Property

    Public ReadOnly Property Bestellt_KundeNr As String
        Get
            Return _Bestellt_KundeNr
        End Get
    End Property

    Public ReadOnly Property Bestellt_Menge_Stk As String
        Get
            If _Bestellt_Menge_Stk <> "" Then
                'Einträge formatieren
                Dim i As Integer = 0
                Dim x() As String = Split(_Bestellt_Menge_Stk, vbCrLf)
                Dim b As String = ""
                For Each s As String In x
                    i = wb_Functions.StrToInt(s)
                    b += i.ToString("#,#.") + " Stk" & vbCrLf
                Next
                Return b
            End If
            Return _Bestellt_Menge_Stk
        End Get
    End Property

    Public ReadOnly Property Bestellt_Text As String
        Get
            Return _Bestellt_Text
        End Get
    End Property

    Public Property Bestellt_Drucken As Boolean
        Get
            Return _Bestellt_Drucken Or _BestelltSonderText_Drucken
        End Get
        Set(value As Boolean)
            _Bestellt_Drucken = value
            If value Then
                SplitSonderText(Bestellt_SonderText)
            End If
        End Set
    End Property

    Public Property BestelltSonderText_Drucken As Boolean
        Get
            Return _BestelltSonderText_Drucken
        End Get
        Set(value As Boolean)
            _BestelltSonderText_Drucken = value
            If value Then
                SplitSonderText(Bestellt_SonderText)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gibt True zurück, wenn die Zeile in der Produktionsplanung angezeigt werden soll.
    ''' Abhängig von Liniengruppe, Aufarbeitungsplatz und Filter-Einstellungen.
    ''' </summary>
    ''' <param name="_FilterAufarbeitung"></param>
    ''' <param name="_FilterLinienGruppe"></param>
    ''' <returns></returns>
    Public Function Filter(_FilterAufarbeitung As Integer, _FilterLinienGruppe As Integer, CheckAufloesen As Boolean, KundenBestellungTextDrucken As Boolean, SonderTextDrucken As Boolean) As Boolean
        'Filter Aufarbeitungsplatz
        If _FilterAufarbeitung > 0 And _ArtikelLinienGruppe > 0 And _FilterAufarbeitung <> _ArtikelLinienGruppe Then
            Return False
        End If

        'Filter Liniengruppe
        If _FilterLinienGruppe > 0 And _LinienGruppe > 0 And _FilterLinienGruppe <> _LinienGruppe Then
            Return False
        End If

        'Filter Aufloesen
        'If Not Aufloesen And CheckAufloesen Then
        '    Return False
        'End If

        'Flag KundenText drucken Ja/Nein
        Bestellt_Drucken = KundenBestellungTextDrucken
        'Flag Sondertext zur Kundenbestellung drucken J/N
        BestelltSonderText_Drucken = SonderTextDrucken

        Return True
    End Function

    Public Property LinienGruppe As Integer
        Get
            Return _LinienGruppe
        End Get
        Set(value As Integer)
            _LinienGruppe = value
            'Startzeit Liniengruppe aus LinienGruppenTabelle
            StartZeit = wb_Linien_Global.GetStartzeit(_LinienGruppe)
            'Sortierkriterium setzen TODO ???
            setSortKriterium()
        End Set
    End Property

    Public ReadOnly Property LinienGruppeText As String
        Get
            Return wb_Linien_Global.GetBezeichnung(_LinienGruppe)
        End Get
    End Property

    ''' <summary>
    ''' Zusatztext für ListLabel-Formular
    ''' z.B. für die Sauerteig-Linie als Patzhalter für die Start-Zeit
    ''' </summary>
    ''' <returns></returns>
    Public Property LinienGruppeZusatzText As String
        Get
            Return _LinienGruppeZusatzText
        End Get
        Set(value As String)
            _LinienGruppeZusatzText = value
        End Set
    End Property

    Public Property ArtikelLinienGruppe As Integer
        Get
            Return _ArtikelLinienGruppe
        End Get
        Set(value As Integer)
            _ArtikelLinienGruppe = value
            'Startzeit Liniengruppe aus LinienGruppenTabelle
            StartZeit = wb_Linien_Global.GetStartzeit(_ArtikelLinienGruppe)
            'Sortierkriterium setzen
            setSortKriterium()
        End Set
    End Property

    Public ReadOnly Property ArtikelLinienGruppeText As String
        Get
            Return wb_Linien_Global.GetBezeichnung(_ArtikelLinienGruppe)
        End Get
    End Property

    Public Property Linie As Integer
        Get
            If _Linie = wb_Global.UNDEFINED Then
                _Linie = wb_Linien_Global.GetLinieFromLinienGruppe(LinienGruppe)
            End If
            Return _Linie
        End Get
        Set(value As Integer)
            _Linie = value
            If _LinienGruppe = wb_Global.UNDEFINED Then
                LinienGruppe = wb_Linien_Global.GetLinienGruppeFromLinie(_Linie)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gibt die Tour-Nummer aus der Produktions-Planung zurück. Ist als Tour-Nummer 0 eingetragen, wird 
    ''' ein Leer-String zurückgegeben
    ''' </summary>
    ''' <returns></returns>
    Public Property Tour As String
        Get
            If _Tour = "0" Then
                Return ""
            Else
                Return _Tour
            End If
        End Get
        Set(value As String)
            _Tour = value
            setSortKriterium()
        End Set
    End Property
    Public ReadOnly Property iTour As Integer
        Get
            Return wb_Functions.StrToInt(_Tour)
        End Get
    End Property
    Public ReadOnly Property IsVorProduktion As Boolean
        Get
            If _Tour IsNot Nothing Then
                If _Tour.Length > 0 Then
                    If _Tour(0) = "V" Then
                        Return True
                    End If
                End If
            End If
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Startzeit der Charge. 
    ''' Wird ermittelt aus der Startzeit der Liniengruppe und dem Produktions-Vorlauf aus den Artikelstammdaten
    ''' </summary>
    ''' <returns></returns>
    Public Property StartZeit As DateTime
        Get
            Return _StartZeit
        End Get
        Set(value As Date)
            _StartZeit = value
        End Set
    End Property

    ''' <summary>
    ''' Setzt die Variablen SortKriteriumBackPlan und SortKriteriumProPlan sobald sich eine der Eingangs-Properties ändert. Über _SortOrder wird dann die 
    ''' entsprechende Variable zur Sortierung ausgewählt.
    ''' 
    '''     BackPlan - Sortierung nach ArtikelLinienGruppe, Rezeptnummer, Artikelnummer
    '''     ProdPlan - Sortierung nach Rezeptnummer, Chargenteiler, Tour, Artikelnummer
    ''' </summary>
    Private Sub setSortKriterium()
        'Sortieren Backplan
        If RezeptNummer IsNot Nothing And ArtikelNummer IsNot Nothing And Tour IsNot Nothing Then
            _SortKriteriumBackPlan = ArtikelLinienGruppe.ToString.PadLeft(3, "0"c) & RezeptNummer.PadLeft(10, "0"c) & ArtikelNummer.PadLeft(16, "0"c) & Tour.PadLeft(3, "0"c)
        Else
            _SortKriteriumBackPlan = Nothing
        End If
        'Sortieren Produktions-Liste (Teigzettel)
        'If RezeptNummer IsNot Nothing And Tour IsNot Nothing And ArtikelNummer IsNot Nothing Then
        '    _SortKriteriumProdPlan = Linie.ToString.PadLeft(3, "0"c) & RezeptNummer.PadLeft(10, "0"c) & TeigChargenTeilerResult & Tour.PadLeft(3, "0"c) & ArtikelNummer.PadLeft(16, "0"c)
        'Else
        '    _SortKriteriumProdPlan = Nothing
        'End If
        _SortKriteriumProdPlan = SortString(Linie.ToString, 3) & SortString(RezeptNummer, 10) & TeigChargenTeilerResult & SortString(Tour, 3) & SortString(ArtikelNummer, 16)
    End Sub

    Private Function SortString(s As String, len As Integer) As String
        If s Is Nothing Then
            s = "0"
        End If
        Return s.PadLeft(len, "0"c)
    End Function

    ''' <summary>
    ''' Datenfeld für Sortierung der Liste
    ''' enthält LinienGruppe + Teignummer + Artikelnumer + Tour als String, so dass die Sortierung über ein Feld erfolgen kann
    ''' Teignummern und Artikelnummer werden mit führenden Nullen aufgefüllt.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SortKriterium As String
        Get
            Select Case _SortOrder
                Case wb_Global.SortOrder.BackZettel
                    Return _SortKriteriumBackPlan

                Case wb_Global.SortOrder.ProdPlan
                    Return _SortKriteriumProdPlan

                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public Property TeigChargen As wb_Global.ChargenMengen
        Get
            Return _TeigChargen
        End Get
        Set(value As wb_Global.ChargenMengen)
            _TeigChargen = value
            setSortKriterium()
        End Set
    End Property

    ''' <summary>
    ''' Anzeige der Teigchargen-Optimalmenge in ListUndLabel
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TeigOptMengekg As Double
        Get
            Return _TeigChargen.MengeOpt
        End Get
    End Property

    ''' <summary>
    ''' Anzeige der Anzahl der Optimalchargen in ListUndLabel
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TeigOptMengeStk As Integer
        Get
            Return _TeigChargen.AnzahlOpt
        End Get
    End Property

    ''' <summary>
    ''' Anzeige der Teigchargen-Restmenge in ListUndLabel
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TeigRestMengekg As Double
        Get
            Return _TeigChargen.MengeRest
        End Get
    End Property

    ''' <summary>
    ''' Anzeige der Anzahl der Restchargen in ListUndLabel
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TeigRestMengeStk As Integer
        Get
            Return _TeigChargen.AnzahlRest
        End Get
    End Property

    Public ReadOnly Property TeigChargenTeilerResult As String
        Get
            Return _TeigChargen.Result.ToString
        End Get
    End Property

    ''' <summary>
    ''' Anzeige der Prozentualen Größe der Restteigmenge bezogen auf die Optimalcharge in ListUndLabel
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TeigRestMengeProzent As Double
        Get
            Return wb_Functions.ProzentSatz(OptChargekg, _TeigChargen.MengeRest)
        End Get
    End Property

    Public Property ChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
        Set(value As String)
            _ChargenNummer = value
        End Set
    End Property

    Public ReadOnly Property iChargenNummer As Integer
        Get
            Return wb_Functions.StrToInt(_ChargenNummer)
        End Get
    End Property


    Public Property Optimiert As Boolean
        Get
            Return _Optimiert
        End Get
        Set(value As Boolean)
            _Optimiert = value
        End Set
    End Property

    Public Property OptChargekg As Double
        Get
            Return _OptChargekg
        End Get
        Set(value As Double)
            _OptChargekg = value
        End Set
    End Property

    Public Property MinChargekg As Double
        Get
            Return _MinChargekg
        End Get
        Set(value As Double)
            _MinChargekg = value
        End Set
    End Property

    Public Property MaxChargekg As Double
        Get
            Return _MaxChargekg
        End Get
        Set(value As Double)
            _MaxChargekg = value
        End Set
    End Property

    Public Property LagerBestand As Double
        Get
            If _LagerBestand = wb_Global.UNDEFINED Then
                Dim Lager As New wb_LagerOrt(LagerOrt)
                _LagerBestand = wb_Functions.StrToDouble(Lager.Bilanzmenge)
            End If
            Return _LagerBestand
        End Get
        Set(value As Double)
            _LagerBestand = wb_Functions.StrToDouble(value)
        End Set
    End Property

    Public Property KO_Typ As wb_Global.KomponTypen
        Get
            Return _KO_Typ
        End Get
        Set(value As wb_Global.KomponTypen)
            _KO_Typ = value
        End Set
    End Property

    Public Property KO_Nr As Integer
        Get
            Return _KO_Nr
        End Get
        Set(value As Integer)
            _KO_Nr = value
        End Set
    End Property

    Public Property KO_Nummer As String
        Get
            Return _KO_Nummer
        End Get
        Set(value As String)
            _KO_Nummer = value
        End Set
    End Property

    Public Property KO_Bezeichnung As String
        Get
            Return _KO_Bezeichnung
        End Get
        Set(value As String)
            _KO_Bezeichnung = value
        End Set
    End Property

    Public Property KO_Einheit As String
        Get
            Return _KO_Einheit
        End Get
        Set(value As String)
            _KO_Einheit = value
        End Set
    End Property

    Public Property Istwert As String
        Get
            Return _Istwert
        End Get
        Set(value As String)
            _Istwert = value
        End Set
    End Property

    Public Property Status As Integer
        Get
            Return _Status
        End Get
        Set(value As Integer)
            _Status = value
        End Set
    End Property

    Public Property RunIndex As Integer
        Get
            Return _RunIndex
        End Get
        Set(value As Integer)
            _RunIndex = value
        End Set
    End Property

    Public Property ARZ_Index As Integer
        Get
            Return _ARZ_Index
        End Get
        Set(value As Integer)
            _ARZ_Index = value
        End Set
    End Property

    Public Property ARS_Index As Integer
        Get
            Return _ARS_Index
        End Get
        Set(value As Integer)
            _ARS_Index = value
        End Set
    End Property

    Public Property Schritt As Integer
        Get
            Return _Schritt
        End Get
        Set(value As Integer)
            _Schritt = value
        End Set
    End Property

    Public Property KO_Charge As String
        Get
            Return _KO_Charge
        End Get
        Set(value As String)
            _KO_Charge = value
        End Set
    End Property

    Public Property ArtikelIndex As Integer
        Get
            Return _ArtikelIndex
        End Get
        Set(value As Integer)
            _ArtikelIndex = value
        End Set
    End Property

    Public Property Aufloesen As Boolean
        Get
            Return _Aufloesen
        End Get
        Set(value As Boolean)
            _Aufloesen = value
        End Set
    End Property

    Public Property FreigabeProduktion As Boolean
        Get
            Return _FreigabeProduktion
        End Get
        Set(value As Boolean)
            _FreigabeProduktion = value
        End Set
    End Property

    Public Property LagerOrt As String
        Get
            Return _LagerOrt
        End Get
        Set(value As String)
            _LagerOrt = value
        End Set
    End Property

End Class
