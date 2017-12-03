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
    Private _Typ As String
    Private _Tour As String
    Private _ChargenNummer As String
    Private _AuftragsNummer As String

    Private _ArtikelNummer As String
    Private _ArtikelBezeichnung As String
    Private _ArtikelNr As Integer
    Private _RezeptNummer As String
    Private _RezeptBezeichnung As String
    Private _RezeptNr As Integer
    Private _RezeptVar As Integer
    Private _Sollwert As String
    Private _Sollwert_kg As Double
    Private _Sollmenge_Stk As Double
    Private _Sollwert_TeilungText As String
    Private _Einheit As String
    Private _TeigChargen As wb_Global.ChargenMengen
    Private _Bestellt_Stk As Double
    Private _Bestellt_SonderText As String

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
    Public Sub CopyFromKomponenten(rs As wb_Komponenten, ChargenTyp As Integer)
        With rs
            Typ = ChargenTyp
            ArtikelNummer = .Nummer
            RezeptNr = .RzNr
            RezeptNummer = .RezeptNummer
            RezeptVar = 1
            RezeptBezeichnung = .RezeptName
            LinienGruppe = .LinienGruppe
        End With
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
            Else
                Sollwert = .Sollwert
            End If
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
    '' Parent dieses Rezeptschrittes
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
    '' Liste aller Child-Rezeptschritte
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

    Public ReadOnly Property VirtTreeStart As String
        Get
            Return ""
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

    Public ReadOnly Property VirtTreeKommentar As String
        Get
            If _Optimiert Then
                Return "zusammengefasst in Charge " & _ChargenNummer
            Else
                Return _Bestellt_SonderText
            End If
        End Get
    End Property

    Public ReadOnly Property VirtTreeLinie As String
        Get
            If _LinienGruppe = wb_Global.LinienGruppeSauerteig Then
                Return "ST"
            ElseIf _LinienGruppe > 0 Then
                Return _LinienGruppe
            Else
                Return ""
            End If
        End Get
    End Property
    Public ReadOnly Property VirtTreeTour As String
        Get
            Return _Tour
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
                Return wb_Functions.FormatStr(Sollmenge_Stk, 0)

            ElseIf Typ = KO_ZEILE_REZEPT Then
                Return wb_Functions.FormatStr(Sollwert_kg, 3)

            Else
                Select Case Typ
                    Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE
                        Return ""
                    Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE, KO_TYPE_WASSERKOMPONENTE
                        Return wb_Functions.FormatStr(_Sollwert, 3)
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
                Return "Stk"
            ElseIf Typ = KO_ZEILE_REZEPT Then
                Return "kg"
            Else
                Return Einheit
            End If
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

    Public Property Sollwert As String
        Get
            Return _Sollwert
        End Get
        Set(value As String)
            _Sollwert = value
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

    Public Property Bestellt_Stk As Double
        Get
            Return _Bestellt_Stk
        End Get
        Set(value As Double)
            _Bestellt_Stk = value
        End Set
    End Property

    Public Property Bestellt_SonderText As String
        Get
            Return _Bestellt_SonderText
        End Get
        Set(value As String)
            _Bestellt_SonderText = value
        End Set
    End Property

    Public Property LinienGruppe As Integer
        Get
            Return _LinienGruppe
        End Get
        Set(value As Integer)
            _LinienGruppe = value
            setSortKriterium()
        End Set
    End Property

    Public ReadOnly Property LinienGruppeText As String
        Get
            Return wb_Linien_Global.GetBezeichnung(_LinienGruppe)
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

    Public Property Tour As String
        Get
            Return _Tour
        End Get
        Set(value As String)
            _Tour = value
            setSortKriterium()
        End Set
    End Property

    Private Sub setSortKriterium()
        'Sortieren Backplan
        If RezeptNummer IsNot Nothing And ArtikelNummer IsNot Nothing And Tour IsNot Nothing Then
            _SortKriteriumBackPlan = LinienGruppe.ToString.PadLeft(3, "0"c) & RezeptNummer.PadLeft(10, "0"c) & ArtikelNummer.PadLeft(16, "0"c) & Tour.PadLeft(3, "0"c)
        Else
            _SortKriteriumBackPlan = Nothing
        End If
        'Sortieren Produktions-Liste
        If RezeptNummer IsNot Nothing And Tour IsNot Nothing And ArtikelNummer IsNot Nothing Then
            _SortKriteriumProdPlan = RezeptNummer.PadLeft(10, "0"c) & TeigChargenTeilerResult & Tour.PadLeft(3, "0"c) & ArtikelNummer.PadLeft(16, "0"c)
        Else
            _SortKriteriumProdPlan = Nothing
        End If
    End Sub

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
    ''' Anzeige der Prozentualen Größe der Restteigmenge in ListUndLabel
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TeigRestMengeProzent As Double
        Get
            Return wb_Functions.ProzentSatz(_TeigChargen.MengeOpt, _TeigChargen.MengeRest)
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

    Public Property Optimiert As Boolean
        Get
            Return _Optimiert
        End Get
        Set(value As Boolean)
            _Optimiert = value
        End Set
    End Property
End Class
