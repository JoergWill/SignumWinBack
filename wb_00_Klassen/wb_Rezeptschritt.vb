Imports System.Reflection
Imports WinBack.wb_Global.KomponTypen
Imports WinBack.wb_Sql_Selects

Public Class wb_Rezeptschritt

    Private _SchrittNr As Integer
    Private _ParamNr As Integer
    Private _RohNr As Integer
    Private _Type As wb_Global.KomponTypen
    Private _Nummer As String
    Private _Bezeichnung As String
    Private _Kommentar As String
    Private _Sollwert As String
    Private _Einheit As String
    Private _PreisProKg As Double = 0
    Private _RezeptNr As Integer = -1
    Private _TA As Integer = wb_Global.TA_Undefined
    Private _RezGewicht As Double
    Private _BruttoRezGewicht As Double
    Private _RezPreis As Double
    Private _ZaehltNichtZumRezeptGewicht As Boolean
    Private _ktTyp301 As New wb_KomponParam301
    Private _ZutatenListe As wb_Global.ZutatenListe
    Private _ZutatenListeExtern As New wb_Hinweise(wb_Global.Hinweise.DeklBezRohstoff)
    Private _ZutatenListeIntern As New wb_Hinweise(wb_Global.Hinweise.DeklBezRohstoffIntern)

    Private _parentStep As wb_Rezeptschritt
    Private _childSteps As New ArrayList()
    Public RezeptImRezept As wb_Rezept

    ' Bedeutung der Zusatzparameter
    '=============================
    ' Wasser-Mengen-Satz   RS_Par2       - letzte Dosiertemperatur
    '
    ' Wasser-Temp-Satz     RS_Par1       - TTS-Korrekturwert 1
    '                      RS_Par2       - TTS-Korrekturwert 2
    '                      RS_Par2       - TTS-Korrekturwert 3
    '                      RS_Wert_Prod  - RMF-Basiswert dieses Rezeptes

    ' Bedeutung der Zusatzparameter Eis
    '=================================
    ' Eis-Mengen-Satz      RS_Wert       - Handausgleichswert (manuelle Mengenkorrektur Eis in Produktion)
    '                      RS_Par1
    '                      RS_Par2
    '                      RS_Par3       - Mindestmenge Eis
    Private _Par1 As String
    Private _Par2 As String
    Private _Par3 As String
    Private _WertProd As String


    ''' <summary>
    ''' Kopiert alle Properties dieser Klasse auf die Properties der übergebenen Klasse.
    ''' Geschrieben werden nur die Properties, die nicht als ReadOnly deklariert sind.
    ''' 
    ''' aus: https://stackoverflow.com/questions/531384/how-to-loop-through-all-the-properties-of-a-class
    '''
    ''' Dient dazu, die Inhalte eines Rezeptschrittes auf einen anderen zu kopieren.
    ''' Durch die Schleife über alle Properties ist die Funktion unabhängig von eventuellen Erweiterungen.
    ''' </summary>
    ''' <param name="rs">wb_Rezeptschritt nimmt die Werte der Properties der Klasse auf</param>
    Public Sub CopyFrom(rs As wb_Rezeptschritt)
        Dim _type As Type = Me.GetType()
        Dim properties() As PropertyInfo = _type.GetProperties()
        For Each _property As PropertyInfo In properties
            If _property.CanWrite And _property.CanRead And _property.Name <> "ParentStep" Then
                _property.SetValue(Me, _property.GetValue(rs, Nothing))
            End If
        Next
    End Sub

    '' <summary>
    '' Create a new step with the given parent
    '' </summary>
    '' <param name="parent">The parent step</param>
    '' <param name="name">The name of this step</param>
    Public Sub New(parent As wb_Rezeptschritt, Bezeichnung As String)
        _parentStep = parent
        _Bezeichnung = Bezeichnung
        _TA = wb_Global.TA_Undefined
        If Not (_parentStep Is Nothing) Then
            parent._childSteps.Add(Me)
        End If
    End Sub 'New

    ''' <summary>
    ''' Einen neuen Rezeptschritt mit der angegebenen Komponenten-Type anhängen.
    ''' Die Komponenten-Daten werden in wb_Komponenten statisch aus dem ersten Datensatz einer Komponente
    ''' mit dem passenden Kompoententyp erzeugt.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="KomponType"></param>
    Public Sub New(Parent As wb_Rezeptschritt, KomponType As wb_Global.KomponTypen)
        Dim NewKomp As wb_Komponente = Nothing

        'neuer Rezeptschritt abhängig von der Komponenten-Type
        Select Case KomponType
            Case wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE
                NewKomp = wb_Komponente.ProduktionsStufe
                _Sollwert = NewKomp.Bezeichnung
                _Nummer = NewKomp.Nummer
                _Einheit = "-"
                _TA = 0
            Case wb_Global.KomponTypen.KO_TYPE_KESSEL
                NewKomp = wb_Komponente.Kessel
                _Sollwert = NewKomp.Bezeichnung
                _Nummer = NewKomp.Nummer
                _Einheit = "-"
                _TA = 0
            Case wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE
                NewKomp = wb_Komponente.TextKomponente
                _Sollwert = NewKomp.Bezeichnung
                _Nummer = NewKomp.Nummer
                _Einheit = "-"
                _TA = 0
            Case Else
                NewKomp = Nothing
        End Select

        'neue Komponente(Type) anhängen
        If NewKomp IsNot Nothing Then
            _parentStep = Parent
            _Bezeichnung = NewKomp.Bezeichnung
            _Nummer = NewKomp.Nummer
            _RohNr = NewKomp.Nr
            _Type = KomponType
            _ParamNr = 1

            If Not (_parentStep Is Nothing) Then
                Parent._childSteps.Add(Me)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Fügt einen Rezeptschritt VOR oder NACH dem aktuellen Schritt ein
    ''' </summary>
    ''' <param name="rs"></param>
    Public Sub Insert(rs As wb_Rezeptschritt, InsertAfter As Boolean)
        If ParentStep IsNot Nothing Then
            'Index des aktuellen Rezept-Schritts
            Dim idx As Integer = ParentStep.ChildSteps.IndexOf(Me)
            'wenn nach dem aktuellen Schritt eingefügt werden soll - Index + 1
            If InsertAfter Then
                idx += 1
            End If

            'Rezeptschritt am Index einfügen
            ParentStep.ChildSteps.Insert(idx, rs)
            'Parent für den neuen Rezeptschritt ist der Parent des aktuellen Rezeptschrittes
            rs.ParentStep = ParentStep
            'Numerierung der Rezeptschritte neu aufbauen
            ParentStep.ReCalcRzSteps(ParentStep.SchrittNr)
        End If
    End Sub

    ''' <summary>
    ''' Fügt einen Child-Rezeptschritt NACH dem aktuellen Schritt ein.
    ''' Der aktuelle Schritt wird dabei zum Parent
    ''' </summary>
    ''' <param name="rs"></param>
    Public Sub InsertChild(rs As wb_Rezeptschritt)
        'Rezeptschritt als erstes Child einfügen (Index = 0)
        ChildSteps.Add(rs)
        'Parent für den neuen Rezeptschritt ist der aktuelle Schritt
        rs.ParentStep = Me
        'Numerierung der Rezeptschritte neu aufbauen
        'ReCalcRzSteps(SchrittNr)
    End Sub

    ''' <summary>
    ''' Löscht den aktuellen Rezeptschritt (Me)
    ''' </summary>
    Public Sub Delete()
        'Diesen Eintrag in der Child-Liste des Parent-Step löschen
        If ParentStep IsNot Nothing Then
            ParentStep.ChildSteps.Remove(Me)
        End If
    End Sub

    '' <summary>
    '' Parent dieses Rezeptschrittes
    '' </summary>
    Public Property ParentStep() As wb_Rezeptschritt
        Get
            Return _parentStep
        End Get
        Set(ByVal value As wb_Rezeptschritt)
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
    ''' Liste aller Rezeptschritte. Es werden alle Child-Rezeptschritte durchlaufen und als flache Liste nach oben
    ''' weiter propagiert.
    ''' Der Root-Rezeptschritt enthält eine Liste aller Rezeptschritte (Rezeptur)
    ''' Wird zum Drucken und Speichern der Rezeptur verwendet
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Steps As IList
        Get
            Dim _Steps As New ArrayList
            If Me.SchrittNr > 0 Then
                _Steps.Add(Me)
            End If

            For Each c As wb_Rezeptschritt In ChildSteps
                For Each x As wb_Rezeptschritt In c.Steps
                    _Steps.Add(x)
                Next
            Next
            Return _Steps
        End Get
    End Property

    ''' <summary>
    ''' Berechnet die Schritt-Nummer aller Child-Steps neu
    ''' Nach Insert/Delete
    ''' </summary>
    ''' <param name="RsStep">Start-Index</param>
    ''' <returns></returns>
    Public Function ReCalcRzSteps(RsStep As Integer) As Integer
        For Each rs As wb_Rezeptschritt In ChildSteps
            If rs.ParamNr <= 1 Then
                RsStep += 1
                rs.SchrittNr = RsStep
            End If
            If wb_Functions.TypeHasChildSteps(rs.Type) Then
                RsStep = rs.ReCalcRzSteps(RsStep)
            End If
        Next
        Return RsStep
    End Function

    ''' <summary>
    ''' (Interne) Komponenten-Nummer
    ''' </summary>
    Public Property RohNr As Integer
        Get
            Return _RohNr
        End Get
        Set(value As Integer)
            _RohNr = value
        End Set
    End Property

    ''' <summary>
    ''' Schritt-Nummer im Rezeptablauf
    ''' </summary>
    ''' <returns></returns>
    Public Property SchrittNr As Integer
        Get
            Return _SchrittNr
        End Get
        Set(value As Integer)
            _SchrittNr = value
        End Set
    End Property

    ''' <summary>
    ''' Parameter-Nummer
    ''' Wird verwendet für mehrzeilige Komponenten (Wasser, Eis...)
    ''' </summary>
    ''' <returns></returns>
    Public Property ParamNr As Integer
        Get
            Return _ParamNr
        End Get
        Set(value As Integer)
            _ParamNr = value
        End Set
    End Property

    ''' <summary>
    ''' Komponenten-Nummer (alpha-numerisch)
    ''' </summary>
    ''' <returns></returns>
    Public Property Nummer As String
        Get
            Return _Nummer
        End Get
        Set(value As String)
            _Nummer = value
        End Set
    End Property

    '' <summary>
    '' Komponenten-Bezeichnung
    '' </summary>
    Public Property Bezeichnung() As String
        Get
            Return _Bezeichnung
        End Get
        Set(value As String)
            _Bezeichnung = value
        End Set
    End Property

    ''' <summary>
    ''' Komponenten-Kommentar (bei Sprachumschaltung die deutsche Bezeichnung)
    ''' </summary>
    ''' <returns></returns>
    Public Property Kommentar As String
        Get
            Return _Kommentar
        End Get
        Set(value As String)
            _Kommentar = value
        End Set
    End Property

    ''' <summary>
    ''' Sollwert
    ''' </summary>
    ''' <returns></returns>
    Public Property Sollwert As String
        Get
            Return _Sollwert
        End Get
        Set(value As String)
            _Sollwert = value
        End Set
    End Property

    ''' <summary>
    ''' Bezeichnung. Anzeige im VirtualTree (Rezeptur)
    ''' Bei Produktions-Stufen, Kessel und Text-Komponenten wird der Sollwert als Text angezeigt
    ''' bei allen anderen Komponenten-Typen die Komponenten-Bezeichnung.
    ''' </summary>
    ''' <returns>String - Bezeichnung</returns>
    Public Property VirtTreeBezeichnung() As String
        Get
            Select Case _Type
                Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE
                    Return _Sollwert
                Case Else
                    'Anzeige Kommentar statt Rezeptbezeichnung
                    If wb_GlobalSettings.KommentarStattBezeichnung And _Kommentar <> "" Then
                        If _RezeptNr > 0 Then
                            Return _Kommentar & "®"
                        Else
                            Return _Kommentar
                        End If
                    Else
                        If _RezeptNr > 0 Then
                            Return _Bezeichnung & "®"
                        Else
                            Return _Bezeichnung
                        End If
                    End If
            End Select
        End Get
        Set(value As String)
            Select Case _Type
                Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE
                    _Sollwert = value
                Case Else
                    _Bezeichnung = value
            End Select
        End Set
    End Property

    ''' <summary>
    ''' Sollwert. Anzeige im VitualTree (Rezeptur)
    ''' Bei Produktions-Stufen, Kessel und Text-Komponenten wird ein leeres Feld angezeigt,
    ''' bei Automatik, Hand, Eis, Wasser oder Verpackung/Stk wird der Sollwert formatiert mit 3 Nachkomma-Stellen angezeigt.
    ''' </summary>
    ''' <returns>String - Sollwert</returns>
    Public Property VirtTreeSollwert As String
        Get
            Select Case _Type
                Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE
                    Return ""
                    'Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE, KO_TYPE_WASSERKOMPONENTE
                    '    Return wb_Functions.FormatStr(_Sollwert, 3)
                Case Else
                    If wb_Functions.TypeIstSollMenge(_Type, 1) Then
                        Return wb_Functions.FormatStr(_Sollwert, 3)
                    Else
                        Return _Sollwert
                    End If
            End Select
        End Get
        Set(value As String)
            If wb_Functions.TypeIstSollMenge(_Type, 1) Or wb_Functions.TypeIstSollWert(_Type, 1) Then
                _Sollwert = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Preis. Anzeige im Virtual-Tree (Rezeptur)
    ''' Wenn ein Preis eingetragen ist, wird der Wert formatiert auf 2 Stellen ausgegeben
    ''' </summary>
    ''' <returns>String- Preis</returns>
    Public ReadOnly Property VirtTreePreis As String
        Get
            If _Preis > 0 Then
                Return wb_Functions.FormatStr(_Preis, 2) + "€"
            Else
                Return ""
            End If
        End Get
    End Property

    ''' <summary>
    ''' Einheit. Anzeige im Virtual-Tree (Rezeptur)
    ''' Gibt für Automatik, Hand, Eis, Wasser, Kneter und Temperatur-Erfassungs-Komponenten die entsprechende Einheit aus der Datenbank zurück
    ''' </summary>
    ''' <returns>String- Einheit</returns>
    Public ReadOnly Property VirtTreeEinheit As String
        Get
            'Select Case _Type
            '    Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE, KO_TYPE_WASSERKOMPONENTE, KO_TYPE_TEMPERATURERFASSUNG, KO_TYPE_KNETER
            '        Return _Einheit
            '    Case Else
            '        Return ""
            'End Select
            If wb_Functions.TypeHatEinheit(_Type) Then
                Return _Einheit
            Else
                Return ""
            End If
        End Get
    End Property

    ''' <summary>
    ''' Prozent-Spalte. Anzeige der Anteile im Virtual-Tree(Rezeptur)
    ''' Gibt wahlweise den prozentualen Anteil an der Gesamt-Teigmenge, den Anteil bezogen auf die Mehlmenge oder die TA zurück.
    ''' </summary>
    ''' <returns>String - Anteil in Prozent/TA</returns>
    Public ReadOnly Property VirtTreeProzent As String
        Get
            Select Case _Type
                Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE, KO_TYPE_WASSERKOMPONENTE
                    If _ParamNr <= 1 And (_RezGewicht > 0) And Not ZaehltNichtZumRezeptGewicht Then
                        Dim Prozent As Double = (wb_Functions.StrToDouble(_Sollwert) / _RezGewicht) * 100
                        Return wb_Functions.FormatStr(Prozent, 2)
                    Else
                        If ZaehltNichtZumRezeptGewicht Then
                            Return "*"
                        Else
                            Return ""
                        End If
                    End If
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Einheit
    ''' </summary>
    ''' <returns></returns>
    Public Property Einheit As String
        Get
            Return _Einheit
        End Get
        Set(value As String)
            _Einheit = value
        End Set
    End Property

    ''' <summary>
    ''' Komponenten-Type der Komponente in der Rezept-Zeile
    ''' </summary>
    ''' <returns>KompontTypen - KomponentenType</returns>
    Public Property Type As wb_Global.KomponTypen
        Get
            Return _Type
        End Get
        Set(value As wb_Global.KomponTypen)
            _Type = value
        End Set
    End Property

    ''' <summary>
    ''' Rezept-Nummer aus Komponente in der Rezept-Zeile. Wenn die Rezeptnummer größer als Null ist, beeinhaltet die
    ''' Komponente eine untrelagerte Rezeptur.
    ''' </summary>
    ''' <returns>Integer - Rezeptnummer</returns>
    Public Property RezeptNr As Integer
        Get
            Return _RezeptNr
        End Get
        Set(value As Integer)
            _RezeptNr = value
        End Set
    End Property

    ''' <summary>
    ''' Setzt die Werte für Einheit, Format und Sollwert bei Kneter-Komponenten.
    ''' Erzeugen von Kneter-Rezepten aus Komponenten(128)
    ''' </summary>
    Public Sub SetType118()
        If _Type = wb_Global.KomponTypen.KO_TYPE_KNETER Then
            Dim EinheitenIndex As String = wb_sql_Functions.getKomponParam(RohNr, 4)
            _Einheit = wb_Language.TextFilter(wb_sql_Functions.Lookup("Einheiten", "E_Einheit", "E_LfdNr = " & EinheitenIndex))
            _Sollwert = wb_sql_Functions.getKomponParam(RohNr, 1)
            '_Format =  wb_sql_Functions.getKomponParam(RohNr, 9)
        End If
    End Sub

    ''' <summary>
    ''' TA der Rezeptzeile aus KomponentenParametern lesen. Wenn die Komponente auf ein Rezept zeigt (RezeptNr größer Null)
    ''' dann wird die TA aus dem Unter-Rezept berechnet. (Rekursive Funktion)
    ''' </summary>
    ''' <returns>Integer - TA</returns>
    Public ReadOnly Property TA As Integer
        Get
            'wird nur beim ersten Aufruf und nur bei Bedarf aus der Datenbank gelesen
            If _TA = wb_Global.TA_Undefined Then

                'Rezept im Rezept
                If (RezeptNr > 0) And RezeptImRezept IsNot Nothing Then
                    'TA aus Rezept-im-Rezept
                    _TA = RezeptImRezept.RezeptTA
                Else
                    'TA aus Komponente
                    Select Case _Type
                        Case KO_TYPE_WASSERKOMPONENTE, KO_TYPE_SAUER_WASSER
                            _TA = wb_Global.TA_Wasser
                        Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE
                            _TA = wb_Functions.StrToInt(wb_sql_Functions.getKomponParam(_RohNr, 7, wb_Global.TA_Null))
                        Case KO_TYPE_SAUER_MEHL
                            _TA = wb_Functions.StrToInt(wb_sql_Functions.getKomponParam(_RohNr, 22, wb_Global.TA_Null))
                        Case KO_TYPE_SAUER_ZUGABE
                            _TA = wb_Functions.StrToInt(wb_sql_Functions.getKomponParam(_RohNr, 3, wb_Global.TA_Null))
                        Case KO_TYPE_SAUER_AUTO_ZUGABE
                            _TA = wb_Functions.StrToInt(wb_sql_Functions.getKomponParam(_RohNr, 4, wb_Global.TA_Null))

                        Case Else
                            _TA = wb_Global.TA_Null
                    End Select
                End If
            End If
            Return _TA
        End Get
    End Property

    ''' <summary>
    ''' Gewichtswert des Rezeptschrittes. Gibt den Sollwert der Rezept-Zeile zurück, wenn diese eine Komponente enthält, die 
    ''' zum Rezeptgewicht zählt und das Flag 'zählt zum Rezeptgewicht' gesetzt ist.
    ''' </summary>
    ''' <returns>Double - Sollwert</returns>
    Private ReadOnly Property _Gewicht As Double
        Get
            If Not ZaehltNichtZumRezeptGewicht Then
                Return wb_Functions.StrToDouble(_BruttoGewicht)
            Else
                Return 0
            End If
        End Get
    End Property

    ''' <summary>
    ''' Brutto-Gewichtswert des Rezeptschrittes. Gibt den Sollwert der Rezept-Zeile zurück, unabhängig vom Falg
    ''' 'zählt zum Rezeptgewicht'
    ''' </summary>
    ''' <returns>Double - Sollwert</returns>
    Private ReadOnly Property _BruttoGewicht As Double
        Get
            If wb_Functions.TypeIstSollMenge(_Type, _ParamNr) Then
                Return wb_Functions.StrToDouble(_Sollwert)
            Else
                Return 0
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gibt das Gewicht der Rezeptzeile zurück. Wenn diese Zeile weitere (Child)Rezeptzeile enthält wird zuerst das Gewicht der 
    ''' unterlagerten Zeilen berechnet und dann die Summe zurückgegeben.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Gewicht As Double
        Get
            Dim Childgewicht As Double = 0
            For Each x As wb_Rezeptschritt In ChildSteps
                Childgewicht = Childgewicht + x.Gewicht
            Next
            Return _Gewicht + Childgewicht
        End Get
    End Property

    ''' <summary>
    ''' Gibt das Brutto-Gewicht der Rezeptzeile zurück. Wenn diese Zeile weitere (Child)Rezeptzeile enthält wird zuerst das Gewicht der 
    ''' unterlagerten Zeilen berechnet und dann die Summe zurückgegeben.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property BruttoGewicht As Double
        Get
            Dim Childgewicht As Double = 0
            For Each x As wb_Rezeptschritt In ChildSteps
                Childgewicht = Childgewicht + x.BruttoGewicht
            Next
            Return _BruttoGewicht + Childgewicht
        End Get
    End Property

    ''' <summary>
    ''' Rezept-Gesamtgewicht an alle Rezeptschritte weiterpropagieren. Wird benötigt zur Berechnung/Anzeige des Anteils der Komponente am 
    ''' Rezeptgesamtgewicht auf der Rezeptzeile.
    ''' </summary>
    Public WriteOnly Property RezGewicht As Double
        Set(value As Double)
            For Each x As wb_Rezeptschritt In ChildSteps
                x.RezGewicht = value
            Next
            _RezGewicht = value
        End Set
    End Property

    ''' <summary>
    ''' Brutto-Rezept-Gesamtgewicht an alle Rezeptschritte weiterpropagieren. Wird benötigt zur Berechnung der Nährwerte
    ''' </summary>
    Public WriteOnly Property BruttoRezGewicht As Double
        Set(value As Double)
            For Each x As wb_Rezeptschritt In ChildSteps
                x.BruttoRezGewicht = value
            Next
            _BruttoRezGewicht = value
        End Set
    End Property

    ''' <summary>
    ''' Gibt die Mehlmenge der Komponente dieser Rezeptzeile zurück. 
    '''     bei Mehl-Komponenten (TA = 100) wird die Sollmenge zurückgegeben.
    '''     bei Sauerteig (TA > 100) wird der Mehlanteil berechnet.
    ''' Wenn die Komponente auf ein Rezept zeigt, wird der Mehlanteil dieses Rezeptes berechnet und mit der Sollmenge verrechnet.
    ''' </summary>
    ''' <returns>Double - Mehlmenge</returns>
    Private ReadOnly Property _TA_Mehlmenge As Double
        Get
            'Rezept im Rezept
            If (RezeptNr > 0) And RezeptImRezept IsNot Nothing Then
                'TA aus Rezept-im-Rezept
                If RezeptImRezept.RezeptGewicht > 0 Then
                    _TA_Mehlmenge = (RezeptImRezept.RezeptGesamtMehlmenge * wb_Functions.StrToDouble(_Sollwert)) / RezeptImRezept.RezeptGewicht
                Else
                    _TA_Mehlmenge = 0
                End If
            Else
                'Mehlanteil der aktuellen Komponente berechnen
                _TA_Mehlmenge = 0
                'Mehl hat TA=100
                If TA = wb_Global.TA_Mehl Then
                    _TA_Mehlmenge = wb_Functions.StrToDouble(_Sollwert)
                End If
                'Sauerteig-Komponente (hat eigene TA) - Mehlanteil herausrechnen
                If TA > wb_Global.TA_Mehl Then
                    _TA_Mehlmenge = (100 / TA) * wb_Functions.StrToDouble(_Sollwert)
                End If
            End If
        End Get
    End Property

    ''' <summary>
    ''' Addiert die Mehlmenge des aktuellen Rezeptschrittes und die Mehlmengen der untergeordneten Rezeptschritte im Baum (Child-Steps).
    ''' </summary>
    ''' <returns>Double - Mehlmenge</returns>
    Public ReadOnly Property TA_Mehlmenge As Double
        Get
            Dim ChildTA_Mehlmenge As Double = 0
            For Each x As wb_Rezeptschritt In ChildSteps
                ChildTA_Mehlmenge = ChildTA_Mehlmenge + x.TA_Mehlmenge
            Next
            Return _TA_Mehlmenge + ChildTA_Mehlmenge
        End Get
    End Property

    ''' <summary>
    ''' Gibt die Wassermenge der Komponente dieser Rezeptzeile zurück. 
    '''     bei Wasser-Komponenten (TA = -1) wird die Sollmenge zurückgegeben.
    '''     bei Sauerteig (TA > 100) wird der Wasseranteil berechnet.
    ''' Wenn die Komponente auf ein Rezept zeigt, wird der Wasseranteil dieses Rezeptes berechnet und mit der Sollmenge verrechnet.
    ''' </summary>
    ''' <returns>Double - Wassermenge</returns>
    Private ReadOnly Property _TA_Wassermenge As Double
        Get
            'Rezept im Rezept
            If (RezeptNr > 0) And RezeptImRezept IsNot Nothing Then
                'TA aus Rezept-im-Rezept
                If RezeptImRezept.RezeptGewicht > 0 Then
                    _TA_Wassermenge = (RezeptImRezept.RezeptGesamtWassermenge * wb_Functions.StrToDouble(_Sollwert)) / RezeptImRezept.RezeptGewicht
                Else
                    _TA_Wassermenge = 0
                End If
            Else
                'Wasseranteil der aktuellen Komponente berechnen
                _TA_Wassermenge = 0
                'Wasserkomponente oder Eis oder Sauerteig-Wasser oder Handwasser
                If (ParamNr = 1) And ((TA = wb_Global.TA_Wasser) Or (_Type = KO_TYPE_WASSERKOMPONENTE) Or (_Type = KO_TYPE_EISKOMPONENTE) Or (_Type = KO_TYPE_SAUER_WASSER)) Then
                    _TA_Wassermenge = wb_Functions.StrToDouble(_Sollwert)
                End If

                'Sauerteig-Komponente (hat eigene TA) - Wasseranteil herausrechnen
                If TA > 100 Then
                    _TA_Wassermenge = wb_Functions.StrToDouble(_Sollwert) * (1 - (100 / TA))
                End If

                'Komponente mit Flüssiganteil TA < 100 - Wasseranteil herausrechnen
                'z.B. Flüssighefe
                '
                'Hier wird (fälschlicherweise) als TA der Wasseranteil eingetragen
                'also bei Flüssighefe mit 50% Wasser - TA 50
                If (TA < 100) And (TA <> 0) And (TA > 0) Then
                    _TA_Wassermenge = wb_Functions.StrToDouble(_Sollwert) * (TA / 100)
                End If
            End If
        End Get
    End Property

    ''' <summary>
    ''' Addiert die Wassermenge des aktuellen Rezeptschrittes und die Wassermengen der untergeordneten Rezeptschritte im Baum (Child-Steps).
    ''' </summary>
    ''' <returns>Double - Wassermenge</returns>
    Public ReadOnly Property TA_Wassermenge As Double
        Get
            Dim ChildTA_Wassermenge As Double = 0
            For Each x As wb_Rezeptschritt In ChildSteps
                ChildTA_Wassermenge += x.TA_Wassermenge
            Next
            Return _TA_Wassermenge + ChildTA_Wassermenge
        End Get
    End Property

    ''' <summary>
    ''' Gibt den EK-Preis der Komponente dieser Rezeptzeile zurück. 
    ''' Wenn die Komponente auf ein Rezept zeigt, wird der Preis dieses Rezeptes berechnet und mit der Sollmenge verrechnet.
    ''' </summary>
    ''' <returns>Double - Preis</returns>
    Private ReadOnly Property _Preis As Double
        Get
            'Rezept im Rezept
            If (RezeptNr > 0) And RezeptImRezept IsNot Nothing Then
                If (RezeptImRezept.RezeptGewicht > 0) Then
                    'Preis aus Rezept-im-Rezept
                    _PreisProKg = (RezeptImRezept.RezeptPreis) / RezeptImRezept.RezeptGewicht
                Else
                    _PreisProKg = 0
                End If
                Return _PreisProKg * wb_Functions.StrToDouble(_Sollwert)
            Else
                If wb_Functions.TypeIstSollMenge(_Type, _ParamNr) Then
                    Return _PreisProKg * wb_Functions.StrToDouble(_Sollwert)
                Else
                    Return 0
                End If
            End If
        End Get
    End Property

    ''' <summary>
    ''' Addiert den Preis der Komponente der aktuellen Rezeptzeile mit den Preisen der untergeordneten Zeilen (Child-Steps)
    ''' </summary>
    ''' <returns>Double - Preis</returns>
    Public ReadOnly Property Preis As Double
        Get
            Dim ChildPreis As Double = 0
            For Each x As wb_Rezeptschritt In ChildSteps
                ChildPreis += x.Preis
            Next
            Return _Preis + ChildPreis
        End Get
    End Property

    ''' <summary>
    ''' Preis pro kg eines Rohstoffes aus der Datenbank
    ''' </summary>
    ''' <returns></returns>
    Public Property PreisProKg As Double
        Get
            Return _PreisProKg
        End Get
        Set(value As Double)
            _PreisProKg = value
        End Set
    End Property

    ''' <summary>
    ''' Rezeptzeile zählt nicht zum Rezeptgesamtgewicht
    ''' </summary>
    ''' <returns>Boolean - Zählt nicht zum Rezeptgewicht</returns>
    Public Property ZaehltNichtZumRezeptGewicht As Boolean
        Get
            Return _ZaehltNichtZumRezeptGewicht
        End Get
        Set(value As Boolean)
            _ZaehltNichtZumRezeptGewicht = value
        End Set
    End Property

    ''' <summary>
    ''' Gibt die Komponentendaten(Nährwerte) des aktuellen Rezeptschrittes zurück. Wenn die Komponenten-Parameter noch nicht vorhanden sind,
    ''' werden zuerst alle Daten zum Rezeptschritt anhand der Komponenten-Nummer aus der Datenbank gelesen.
    ''' 
    ''' Wenn die Komponente auf ein Rezept zeigt (Rezept-im_Rezept), wird zunächst das Unter-Rezept berechnet und dann alle Werte addiert.
    ''' Alle untergeordneten Rezeptschritte im Baum werden berechnet und mit der aktuellen Zeile addiert.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ktTyp301 As wb_KomponParam301
        Get
            'wenn noch keine Allergen-Info berechnet wurde
            If Not _ktTyp301.IsCalculated Then

                'Rezept im Rezept
                If (RezeptNr > 0) And RezeptImRezept IsNot Nothing And _BruttoRezGewicht > 0 Then
                    RezeptImRezept.KtTyp301.AddNwt(_ktTyp301, _Sollwert / _BruttoRezGewicht)
                Else

                    'Nährwert-Info aus Datenbank lesen
                    If wb_Functions.TypeIstSollMenge(_Type, _ParamNr) Then
                        ReadktTyp301()
                        Debug.Print("Komponente " & Bezeichnung)
                    End If

                    'alle Unter-Rezept-Schritte berechnen
                    For Each x As wb_Rezeptschritt In ChildSteps
                        If _BruttoRezGewicht > 0 Then
                            x.ktTyp301.AddNwt(_ktTyp301, wb_Functions.StrToDouble(_Sollwert) / _BruttoRezGewicht)
                        End If
                    Next

                End If
            End If
            Return _ktTyp301
        End Get
    End Property

    ''' <summary>
    ''' Gibt die Zutatenliste mit Bezeichnung und Mengen-Angabe aller unterlagerten Rezeptschritte zurück
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ZutatenListe(Optional Faktor As Double = 1) As wb_Global.ZutatenListe
        Get
            'Wenn noch nicht gelesen wurde, dann erst aus DB einlesen
            If Not _ZutatenListeExtern.ReadOK Then
                _ZutatenListeExtern.Read(Me.RohNr)
            End If
            'Die Zutaten zum Rohstoff sind im Memo-Feld abgelegt
            _ZutatenListe.Zutaten = _ZutatenListeExtern.Memo
            _ZutatenListe.SollMenge = wb_Functions.StrToDouble(_Sollwert) * Faktor
            'TODO Rohstoff-Gruppen auslesen und als Property anlegen
            _ZutatenListe.Grp1 = 0
            _ZutatenListe.Grp2 = 0
            'TODO Quid-Angaben aus Rezeptur auslesen und als Property anlegen
            _ZutatenListe.Quid = False
            _ZutatenListe.QuidProzent = 0

            Return _ZutatenListe
        End Get
    End Property

    Public Property Par1 As String
        Get
            Return _Par1
        End Get
        Set(value As String)
            _Par1 = value
        End Set
    End Property

    Public Property Par2 As String
        Get
            Return _Par2
        End Get
        Set(value As String)
            _Par2 = value
        End Set
    End Property

    Public Property Par3 As String
        Get
            Return _Par3
        End Get
        Set(value As String)
            _Par3 = value
        End Set
    End Property

    Public Property WertProd As String
        Get
            Return _WertProd
        End Get
        Set(value As String)
            _WertProd = value
        End Set
    End Property

    Public Sub CalcZutaten(ByRef zListe As ArrayList, Optional Faktor As Double = 1)
        'Angaben zum Rezeptschritt in Liste anhängen
        If wb_Functions.TypeIstSollMenge(_Type, _ParamNr) Then
            zListe.Add(ZutatenListe(Faktor))
        End If

        'unterlagerte Rezeptschritte werden auch im Array angehängt
        For Each x As wb_Rezeptschritt In ChildSteps
            x.CalcZutaten(zListe, Faktor)
        Next

        'Rezept im Rezept
        If (RezeptNr > 0) And RezeptImRezept IsNot Nothing Then
            Dim f As Double = Sollwert / RezeptImRezept.RezeptGewicht
            RezeptImRezept.RootRezeptSchritt.CalcZutaten(zListe, f)
        End If

    End Sub

    Private Function ReadktTyp301() As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim Name As String
        Dim Value As Object
        Dim ParamNr, ParamTyp As Integer

        'Komponenten-Parameter aus Datenbank lesen
        If winback.sqlSelect(setParams(sqlgetNWT, _RohNr.ToString)) Then
            'Lesen KO-Nr
            If winback.Read Then
                'Schleife über alle Parameter-Datensätze
                'Bis alle Datensätze eingelesen sind
                Do
                    'Parameter - Anzahl der Felder im DataSet
                    For i = 0 To winback.MySqlRead.FieldCount - 1
                        Name = winback.MySqlRead.GetName(i)
                        Value = winback.MySqlRead.GetValue(i)

                        'Feldname aus der Datenbank
                        Select Case Name

                           'Parameter-Nummer
                            Case "RP_ParamNr"
                                ParamNr = CInt(Value)

                           'Parameter-Typ
                            Case "RP_Typ_Nr"
                                ParamTyp = CInt(Value)

                           'Parameter-Wert
                            Case "RP_Wert"
                                If ParamTyp = 301 Then

                                    If wb_KomponParam301_Global.IsAllergen(ParamNr) Then
                                        'TODO if undefined set merker
                                        _ktTyp301.Allergen(ParamNr) = wb_Functions.StringtoAllergen(Value)
                                        If (_ktTyp301.Allergen(ParamNr) = wb_Global.AllergenInfo.N) Or (_ktTyp301.Allergen(ParamNr) = wb_Global.AllergenInfo.ERR) Then
                                            _ktTyp301.FehlerKompName(ParamNr) = _Bezeichnung
                                        End If
                                    Else
                                        If (Value IsNot Nothing) And (Value <> "") Then
                                            If _BruttoRezGewicht > 0 Then
                                                _ktTyp301.Naehrwert(ParamNr) = wb_Functions.StrToDouble(Value) * wb_Functions.StrToDouble(_Sollwert) / _BruttoRezGewicht
                                            Else
                                                _ktTyp301.Naehrwert(ParamNr) = 0
                                            End If
                                        Else
                                            _ktTyp301.FehlerKompName(ParamNr) = _Bezeichnung
                                        End If


                                        'TODO if undefined set merker
                                    End If
                                End If
                        End Select
                    Next
                Loop While winback.MySqlRead.Read
            End If
        End If
        Return True
    End Function

End Class
