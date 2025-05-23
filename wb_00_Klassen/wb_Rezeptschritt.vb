﻿'Zutatenliste neu berechnen
Imports System.Reflection
Imports EnhEdit
Imports WinBack.wb_Global.KomponTypen
Imports WinBack.wb_Sql_Selects

Public Class wb_Rezeptschritt

    Private _Idx As Integer
    Private _SchrittNr As Integer
    Private _ParamNr As Integer
    Private _RohNr As Integer
    Private _Type As wb_Global.KomponTypen
    Private _Nummer As String
    Private _Bezeichnung As String
    Private _Kommentar As String
    Private _Sollwert As String
    Private _SollwertProzent As String
    Private _Einheit As String
    Private _Format As EnhEdit_Global.wb_Format
    Private _OberGW As String
    Private _UnterGW As String
    Private _PreisProKg As Double = 0
    Private _RezeptNr As Integer = -1
    Private _RezeptNummer As String
    Private _TA As Integer = wb_Global.TA_Undefined
    Private _RezGewicht As Double
    Private _BruttoRezGewicht As Double
    Private _NwtRezGewicht As Double
    Private _RezPreis As Double
    Private _ZaehltNichtZumRezeptGewicht As Boolean = False
    Private _ZaehltTrotzdemZumNwtGewicht As Boolean = False
    Private _LagerOrt As String
    Private _QUIDRelevant As Boolean = False
    Private _ktTyp301DatenFehlerhaft As Boolean = False
    Private _ktTyp301 As New wb_KomponParam301
    Private _ZutatenListe As New wb_ZutatenElement
    Private _ZutatenListeExtern As New wb_Hinweise(wb_Global.Hinweise.DeklBezRohstoff)
    Private _ZutatenListeIntern As New wb_Hinweise(wb_Global.Hinweise.DeklBezRohstoffIntern)
    Private _Backverlust As Double
    Private _Zuschnitt As Double
    Private _FreigabeProduktion As Boolean

    Private _RohstoffGruppe1 As Integer
    Private _RohstoffGruppe2 As Integer

    Private _parentStep As wb_Rezeptschritt
    Private _childSteps As New ArrayList()
    Public RezeptImRezept As wb_Rezept

    'Schnittstelle zu OrgaBack
    Dim oi As New ArrayList

    ' Bedeutung der Zusatzparameter
    '=============================
    '
    'Hand/Auto-Komponente  RS_Par1       - QUID-Relevant (RS_Par1 = -1)
    '
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
    Private _WertProd As String = ""


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
            If _property.CanWrite AndAlso _property.CanRead AndAlso _property.Name <> "ParentStep" AndAlso _property.Name <> "fSollwert" Then
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
        'Es gibt keinen Root-Knoten (erster Knoten in der Reihe)
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
                _Format = EnhEdit.EnhEdit_Global.wb_Format.fString
                _OberGW = wb_Global.wbRzptParamMax
                _UnterGW = wb_Global.wbRzptParamMin
            Case wb_Global.KomponTypen.KO_TYPE_KESSEL
                NewKomp = wb_Komponente.Kessel
                _Sollwert = NewKomp.Bezeichnung
                _Nummer = NewKomp.Nummer
                _Einheit = "-"
                _TA = 0
                _Format = EnhEdit.EnhEdit_Global.wb_Format.fString
                _OberGW = wb_Global.wbRzptParamMax
                _UnterGW = wb_Global.wbRzptParamMin
            Case wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE
                NewKomp = wb_Komponente.TextKomponente
                _Sollwert = NewKomp.Bezeichnung
                _Nummer = NewKomp.Nummer
                _Einheit = "-"
                _TA = 0
                _Format = EnhEdit.EnhEdit_Global.wb_Format.fString
                _OberGW = wb_Global.wbRzptParamMax
                _UnterGW = wb_Global.wbRzptParamMin
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
                _Idx = ParentStep.Idx
                Parent._childSteps.Add(Me)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Setzt alle Flags der ErnährungsForm auf True (Root-Rezeptschritt)
    ''' </summary>
    Public Sub InitKT301Params()
        _ktTyp301.ErnaehrungsForm(wb_Global.T301_Vegetarisch) = wb_Functions.StringtoErnaehrungsForm("Y")
        _ktTyp301.ErnaehrungsForm(wb_Global.T301_Vegan) = wb_Functions.StringtoErnaehrungsForm("Y")
        _ktTyp301.ErnaehrungsForm(wb_Global.T301_Halal) = wb_Functions.StringtoErnaehrungsForm("Y")
        _ktTyp301.ErnaehrungsForm(wb_Global.T301_Koscher) = wb_Functions.StringtoErnaehrungsForm("Y")
        _ktTyp301.IsCalculated = False
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
                Me.Idx = 1
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
            'Wenn der Rezeptschritt der erste Schritt von mehreren Schritten ist, Schritt-Nummer erhöhen
            If rs.ParamNr <= 1 Then
                RsStep += 1
            End If
            'alle Rezeptschritte aktualisieren
            rs.SchrittNr = RsStep
            'Child-Steps aktualisieren
            If wb_Functions.TypeHasChildSteps(rs.Type) Then
                RsStep = rs.ReCalcRzSteps(RsStep)
            End If
        Next
        Return RsStep
    End Function

    ''' <summary>
    ''' Index auf Rezeptnummer.
    ''' Ist für alle Rezeptschritte identisch. Verweis auf die Rezeptnummer (Drucken Rezeptur)
    ''' </summary>
    ''' <returns></returns>
    Public Property Idx As Integer
        Get
            Return _Idx
        End Get
        Set(value As Integer)
            _Idx = value
        End Set
    End Property

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
    ''' Sollwert als String.
    ''' Enthält die Rezepturdaten exakt entsprechend der Datenbank-Einträge (Dezimaltrenner Komma für numerische Werte)
    ''' Berechnungen müssen immer mit der entsprechend konvertierten Property fSollwert vorgenommen werden!
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
    ''' Sollwert als Double.
    ''' Enthält die Rezeptur-Sollwertdaten als Double-Wert. Ist eine Konvertierung nicht möglich wird 0.0F zurückgegeben!
    ''' </summary>
    ''' <returns></returns>
    Public Property fSollwert As Double
        Get
            Return wb_Functions.StrToDouble(_Sollwert)
        End Get
        Set(value As Double)
            _Sollwert = wb_Functions.FormatStr(value.ToString, 3, -1, ("sql"))
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

    ''' <summary>
    ''' Bezeichnung. Anzeige im VirtualTree (Rezeptur)
    ''' Bei Produktions-Stufen, Kessel und Text-Komponenten wird der Sollwert als Text angezeigt
    ''' bei allen anderen Komponenten-Typen die Komponenten-Bezeichnung.
    ''' </summary>
    ''' <returns>String - Bezeichnung</returns>
    Public Property VirtTreeBezeichnung() As String
        Get
            Select Case _Type
                Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE, KO_TYPE_KNETERREZEPT
                    Return _Sollwert
                Case Else
                    'Anzeige Kommentar statt Rezeptbezeichnung
                    If wb_GlobalSettings.KommentarStattBezeichnung AndAlso _Kommentar <> "" Then
                        If _RezeptNr > 0 Then
                            Return _Kommentar & wb_Global.RezeptImRezept
                        Else
                            Return _Kommentar
                        End If
                    Else
                        If _RezeptNr > 0 Then
                            Return _Bezeichnung & wb_Global.RezeptImRezept
                        Else
                            Return _Bezeichnung
                        End If
                    End If
            End Select
        End Get
        Set(value As String)
            Select Case _Type
                Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE, KO_TYPE_KNETERREZEPT
                    _Sollwert = value
                Case Else
                    'keine Änderung !
            End Select
        End Set
    End Property

    ''' <summary>
    ''' Sollwert. Anzeige im VitualTree (Rezeptur)
    ''' Bei Produktions-Stufen, Kessel und Text-Komponenten wird ein leeres Feld angezeigt,
    ''' bei Automatik, Hand, Eis, Wasser oder Verpackung/Stk wird der Sollwert formatiert mit 3 Nachkomma-Stellen angezeigt.
    ''' 
    ''' Hier wird das Ergebnis aus dem Editor (Enh_Edit) in die Datenstruktur eingetragen!
    ''' </summary>
    ''' <returns>String - Sollwert</returns>
    Public Property VirtTreeSollwert As String
        Get
            Select Case _Type
                Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE, KO_TYPE_KNETERREZEPT
                    Return ""
                Case Else
                    If wb_Functions.TypeIstSollMenge(_Type, 1) Then
                        'Sollwert aus Datenbank immer im Format de-DE (Komma als Dezimaltrenner)
                        Return wb_Functions.FormatStr(_Sollwert, 3, -1, "sql")
                        '    'TODO - wird nie True (Sinn?)
                        'ElseIf wb_Functions.TypeIstSollMenge(_Type, 2) Then
                        '    'Sollwert aus Datenbank immer im Format de-DE (Komma als Dezimaltrenner)
                        '    Return wb_Functions.FormatStr(_SollwertProzent, 3, -1, "sql")
                    Else
                        Return _Sollwert
                    End If
            End Select
        End Get
        Set(value As String)
            If wb_Functions.TypeIstSollMenge(_Type, 1) OrElse wb_Functions.TypeIstSollWert(_Type, 1) Then
                _Sollwert = wb_Functions.FormatSqlStr(value)
            ElseIf wb_Functions.TypeIstSollMenge(_Type, 2) OrElse wb_Functions.TypeIstSollWert(_Type, 2) Then
                _SollwertProzent = value
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
                Return wb_Functions.FormatStr(_Preis, 2) & wb_GlobalSettings.osDefaultWaehrung
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
                    If _ParamNr <= 1 AndAlso (_RezGewicht > 0) AndAlso Not ZaehltNichtZumRezeptGewicht Then
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

    Public Property Format As EnhEdit.EnhEdit_Global.wb_Format
        Get
            Return _Format
        End Get
        Set(value As EnhEdit.EnhEdit_Global.wb_Format)
            _Format = value
        End Set
    End Property

    Public Property OberGW As String
        Get
            Return _OberGW
        End Get
        Set(value As String)
            _OberGW = value
        End Set
    End Property

    Public Property UnterGW As String
        Get
            Return _UnterGW
        End Get
        Set(value As String)
            _UnterGW = value
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
    ''' Komponente eine unterlagerte Rezeptur.
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
    ''' Alphanumerische Rezeptnummer (nur für Ausdruck der Rezept-Struktur)
    ''' </summary>
    ''' <returns></returns>
    Public Property RezeptNummer As String
        Get
            Return _RezeptNummer
        End Get
        Set(value As String)
            _RezeptNummer = value
        End Set
    End Property

    ''' <summary>
    ''' Setzt die Werte für Einheit, Format und Sollwert bei Kneter-Komponenten.
    ''' Erzeugen von Kneter-Rezepten aus Komponenten(128)
    ''' Wenn NeueKomponente auf True steht, wird ein DefaultSollwert von 00:10:00 eingetragen
    ''' </summary>
    Public Sub SetType118(NeueKomponente As Boolean)
        Select Case _Type
            Case wb_Global.KomponTypen.KO_TYPE_KNETER
                Dim EinheitenIndex As String = wb_sql_Functions.getKomponParam(RohNr, wb_Global.KomponParams.EinheitenIndex)
                _Einheit = wb_Language.TextFilter(wb_sql_Functions.Lookup("Einheiten", "E_Einheit", "E_LfdNr = " & EinheitenIndex))
                _Format = wb_Functions.StrToFormat(wb_sql_Functions.getKomponParam(RohNr, wb_Global.KomponParams.Format))
                _UnterGW = wb_Functions.StrToDouble(wb_sql_Functions.getKomponParam(RohNr, wb_Global.KomponParams.UntererGrenzwert))
                _OberGW = wb_Functions.StrToDouble(wb_sql_Functions.getKomponParam(RohNr, wb_Global.KomponParams.ObererGrenzwert))
                'Sollwert nur bei neuen Komponenten überschreiben (Fehler bei Fonk 21-02-2022)
                If NeueKomponente Then
                    _Sollwert = wb_sql_Functions.getKomponParam(RohNr, wb_Global.KomponParams.Sollwert)
                End If

            Case wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT
                _Einheit = wb_Global.wbEinheitLeer
                _Format = EnhEdit.EnhEdit_Global.wb_Format.fString
                _UnterGW = wb_Global.wbRzptParamMin
                _OberGW = wb_Global.wbRzptParamMax

        End Select
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
                If (RezeptNr > 0) AndAlso RezeptImRezept IsNot Nothing Then
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
    ''' Brutto-Gewichtswert des Rezeptschrittes. Gibt den Sollwert der Rezept-Zeile zurück, unabhängig vom Flag
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
    ''' Nwt-Gewichtswert des Rezeptschrittes. Gibt den Sollwert der Rezept-Zeile zurück, abhängig vom Flag
    ''' 'zählt zum Rezeptgewicht' und 'zählt trotzdem zur Nährwertberechnung'
    ''' </summary>
    ''' <returns>Double - Sollwert</returns>
    Private ReadOnly Property _NwtGewicht As Double
        Get
            If Not ZaehltNichtZumRezeptGewicht OrElse ZaehltTrotzdemZumNwtGewicht Then
                Return wb_Functions.StrToDouble(_BruttoGewicht)
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
                'Debug.Print("RezeptGewicht Rezeptschritt " & x.SchrittNr & "/" & x.VirtTreeBezeichnung & "/" & x.Gewicht)
                Childgewicht = Childgewicht + x.Gewicht
            Next
            'Gewicht des Rezeptschrittes und Summe aller unterlagerten Rezeptschritte
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
    ''' Gibt das Nwt-Gewicht der Rezeptzeile zurück. Wenn diese Zeile weitere (Child)Rezeptzeile enthält wird zuerst das Gewicht der 
    ''' unterlagerten Zeilen berechnet und dann die Summe zurückgegeben.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NwtGewicht As Double
        Get
            Dim Childgewicht As Double = 0
            For Each x As wb_Rezeptschritt In ChildSteps
                Childgewicht = Childgewicht + x.NwtGewicht
            Next
            Return _NwtGewicht + Childgewicht
        End Get
    End Property

    ''' <summary>
    ''' Rezept-Gesamtgewicht an alle Rezeptschritte weiterpropagieren. Wird benötigt zur Berechnung/Anzeige des Anteils der Komponente am 
    ''' Rezeptgesamtgewicht auf der Rezeptzeile.
    ''' </summary>
    Public Property RezGewicht As Double
        Get
            Return _RezGewicht
        End Get
        Set(value As Double)
            For Each x As wb_Rezeptschritt In ChildSteps
                x.RezGewicht = value
            Next
            _RezGewicht = value
        End Set
    End Property

    ''' <summary>
    ''' Brutto-Rezept-Gesamtgewicht an alle Rezeptschritte weiterpropagieren.
    ''' </summary>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property BruttoRezGewicht As Double
        Set(value As Double)
            For Each x As wb_Rezeptschritt In ChildSteps
                x.BruttoRezGewicht = value
            Next
            _BruttoRezGewicht = value
        End Set
    End Property

    ''' <summary>
    ''' Nwt-Rezept-Gesamtgewicht an alle Rezeptschritte weiterpropagieren. Wird benötigt zur Berechnung der Nährwerte
    ''' </summary>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property NwtRezGewicht As Double
        Set(value As Double)
            For Each x As wb_Rezeptschritt In ChildSteps
                x.NwtRezGewicht = value
            Next
            _NwtRezGewicht = value
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
            If (RezeptNr > 0) AndAlso RezeptImRezept IsNot Nothing Then
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
            If (RezeptNr > 0) AndAlso RezeptImRezept IsNot Nothing Then
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
                If wb_Functions.TypeIstWasserSollmenge(_Type, ParamNr, TA) Then
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
                If (TA < 100) AndAlso (TA <> 0) AndAlso (TA > 0) Then
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
            If (RezeptNr > 0) AndAlso RezeptImRezept IsNot Nothing Then
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
    ''' Datenbank-Feld winback.Komponenten.KA_zaehlt_zu_RZ_Gesamtmenge
    ''' 
    '''                                     ZähltNichtZumRezeptgewicht  ZaehltTrotzdemZumNwtGewicht
    '''                                     
    '''    (KA_zaehlt_zu_RZ_Gesamtmenge = 3         True                         True) ALT !
    '''     KA_zaehlt_zu_RZ_Gesamtmenge = 1         True                         False
    '''     KA_zaehlt_zu_RZ_Gesamtmenge = 0         False                        True
    '''     KA_zaehlt_zu_RZ_Gesamtmenge = NULL      False                        True
    '''     
    ''' </summary>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1871:Two branches in a conditional structure should not have exactly the same implementation", Justification:="<Ausstehend>")>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property KA_zaehlt_zu_RZ_Gesamtmenge As String
        Set(value As String)
            Select Case value
                Case wb_Global.ZaehltZumRezeptGewicht
                    _ZaehltNichtZumRezeptGewicht = False
                    _ZaehltTrotzdemZumNwtGewicht = True
                Case wb_Global.ZaehltNichtZumRezeptGewicht
                    _ZaehltNichtZumRezeptGewicht = True
                    _ZaehltTrotzdemZumNwtGewicht = False
                Case Else
                    _ZaehltNichtZumRezeptGewicht = False
                    _ZaehltTrotzdemZumNwtGewicht = True
            End Select
        End Set
    End Property

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property KA_zaehlt_zu_NWT_Gesamtmenge As String
        Set(value As String)
            Select Case value
                Case wb_Global.ZaehltZumNwtGewicht
                    _ZaehltTrotzdemZumNwtGewicht = True
                Case Else
                    _ZaehltTrotzdemZumNwtGewicht = False
            End Select
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

    Public Property ZaehltTrotzdemZumNwtGewicht As Boolean
        Get
            Return _ZaehltTrotzdemZumNwtGewicht
        End Get
        Set(value As Boolean)
            _ZaehltTrotzdemZumNwtGewicht = value
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
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public ReadOnly Property ktTyp301 As wb_KomponParam301
        Get
            'wenn noch keine Allergen-Info berechnet wurde
            If Not _ktTyp301.IsCalculated Then

                'alle Werte löschen (sicherheitshalber)
                _ktTyp301.Clear()

                'Rezept im Rezept
                If (RezeptNr > 0) AndAlso RezeptImRezept IsNot Nothing AndAlso _NwtRezGewicht > 0 Then
                    RezeptImRezept.KtTyp301.AddNwt(_ktTyp301, _Sollwert / _NwtRezGewicht)
                Else

                    'Nährwert-Info aus Datenbank lesen
                    If wb_Functions.TypeIstSollMenge(_Type, _ParamNr) Then
                        ReadktTyp301()
                        'Debug.Print("Komponente " & Bezeichnung)
                    End If

                    'Umechnungs-Faktor Nährwerte 
                    Dim Faktor As Double = wb_Functions.StrToDouble(_Sollwert) / _NwtRezGewicht
                    'Wenn eine Backverlust angegeben ist, erhöht sich der Faktor entspechend
                    'da beim Backen das Endprodukt Gewicht verliert.
                    Faktor = (Faktor * 100) / (100 - Backverlust)

                    'Wenn der Parent-Rezeptschritt eine Produktions-Stufe oder ein Kessel ist, wird der Faktor
                    'gleich Eins gesetzt. (Die Gewichtung erfolgt über das Rezept-Gesamtgewicht
                    If Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE OrElse Type = wb_Global.KomponTypen.KO_TYPE_KESSEL Then
                        Faktor = 1
                    End If

                    'alle Unter-Rezept-Schritte berechnen
                    For Each x As wb_Rezeptschritt In ChildSteps
                        If _NwtRezGewicht > 0 Then
                            x.ktTyp301.AddNwt(_ktTyp301, Faktor)
                        End If
                    Next

                End If
            End If
            Return _ktTyp301
        End Get
    End Property

    ''' <summary>
    ''' Flag in den Nährwert-Angaben (kt301) sind ein/mehrere Parameter nicht gesetzt oder als fehlerhaft gekennzeichnet
    ''' (Allergen-Info X oder N)
    ''' </summary>
    ''' <returns></returns>
    Public Property KtTyp301DatenFehlerhaft As Boolean
        Get
            Return _ktTyp301DatenFehlerhaft
        End Get
        Set(value As Boolean)
            _ktTyp301DatenFehlerhaft = value
        End Set
    End Property

    ''' <summary>
    ''' Gibt die Zutatenliste mit Bezeichnung und Mengen-Angabe aller unterlagerten Rezeptschritte zurück
    ''' 
    ''' Abhängig vom Flag NwtInterneDeklaration wird aus der internen oder externen Deklaration gelesen.
    ''' Ist das Feld interne Deklaration leer, wird immer die externe Deklaration verwendet.
    ''' </summary>
    ''' <returns></returns>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2352:Indexed properties with more than one parameter should not be used", Justification:="<Ausstehend>")>
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public ReadOnly Property ZutatenListe(Optional Faktor As Double = 1, Optional ReCalc As Boolean = False) As wb_ZutatenElement
        Get

            'Lesen aus interner Deklaration der Rohstoffe
            If wb_GlobalSettings.NwtInterneDeklaration Then
                'Wenn noch nicht gelesen wurde, dann erst aus DB einlesen
                If Not _ZutatenListeIntern.ReadOK OrElse ReCalc Then
                    _ZutatenListeIntern.Read(Me.RohNr)
                End If
                'Die Zutaten zum Rohstoff sind im Memo-Feld abgelegt
                _ZutatenListe.Zutaten = _ZutatenListeIntern.Memo
            End If

            'Lesen aus externer Deklaration oder wenn die interne Deklaration leer ist
            If Not wb_GlobalSettings.NwtInterneDeklaration OrElse (_ZutatenListe.Zutaten = "") Then
                'Wenn noch nicht gelesen wurde, dann erst aus DB einlesen
                If Not _ZutatenListeExtern.ReadOK OrElse ReCalc Then
                    _ZutatenListeExtern.Read(Me.RohNr)
                End If
                'Die Zutaten zum Rohstoff sind im Memo-Feld abgelegt
                _ZutatenListe.Zutaten = _ZutatenListeExtern.Memo
            End If

            'Fehler abfangen
            If _ZutatenListe.Zutaten Is Nothing Then
                _ZutatenListe.Zutaten = ""
            End If

            'Flag NODEKLARATION im Rohstoff
            If _ZutatenListe.Zutaten = wb_Global.FlagKeineDeklaration Then
                'Flag - Keine Deklaration
                _ZutatenListe.KeineDeklaration = True
                'Flag Zutatenliste auflösen
                _ZutatenListe.Aufloesen = False
                'Deklarationsfeld bleibt leer
                _ZutatenListe.Zutaten = ""
            Else
                'Flag - Keine Deklaration
                _ZutatenListe.KeineDeklaration = False
                'Flag Zutatenliste auflösen
                If _ZutatenListe.Zutaten.StartsWith(wb_Global.FlagAufloesen) Then
                    _ZutatenListe.Aufloesen = True
                    _ZutatenListe.Zutaten = _ZutatenListe.Zutaten.TrimStart(wb_Global.FlagAufloesen)
                    'wenn der Deklarationstext nicht leer ist, werden die Bestandteile des Unterrezeptes in Klammern einzeln aufgeführt und nicht sortiert !!
                    If _ZutatenListe.Zutaten <> "" Then
                        _ZutatenListe.GrpRezNr = RezeptNr
                    Else
                        _ZutatenListe.GrpRezNr = wb_Global.UNDEFINED
                    End If
                Else
                    'wenn der Deklarationstext leer ist - wird der Rohstoff-Name verwendet
                    If _ZutatenListe.Zutaten = "" Then
                        _ZutatenListe.Zutaten = Bezeichnung
                    End If
                End If
            End If

            'Sollmenge aus Rezeptur
            _ZutatenListe.SollMenge = wb_Functions.StrToDouble(_Sollwert) * Faktor

            'Rohstoff-Gruppen
            _ZutatenListe.Grp1 = RohstoffGruppe1
            _ZutatenListe.Grp2 = RohstoffGruppe2

            'Quid-Angaben aus Rezeptur auslesen und als Property anlegen
            _ZutatenListe.Quid = QUIDRelevant
            _ZutatenListe.QuidProzent = 0

            'Querverweis Rohstoff in Zutatenliste eintragen
            _ZutatenListe.Rohstoffe = Bezeichnung

            Return _ZutatenListe
        End Get
    End Property

    Public Property Par1 As String
        Get
            'Bei Komponenten-Type 101 und 102 wird in RS_Par1=-1 das Flag QUID-Relevant gespeichert
            If Type = wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE Or wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE Then
                If _QUIDRelevant Then
                    Return wb_Global.RS_Par1_QUID
                Else
                    Return wb_Global.RS_Par1_NOQUID
                End If
            Else
                Return _Par1
            End If
        End Get
        Set(value As String)
            'Bei Komponenten-Type 101 und 102 wird in RS_Par1=-1 das Flag QUID-Relevant gespeichert
            If value = wb_Global.RS_Par1_QUID And (Type = wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE Or wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE) Then
                _QUIDRelevant = True
            Else
                _QUIDRelevant = False
            End If
            'Wert speichern
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

    Public Sub SaveSollwert_org()
        If _WertProd = "" AndAlso wb_Functions.TypeIstSollMenge(_Type, _ParamNr) Then
            _WertProd = Sollwert
        End If
    End Sub

    Public Property QUIDRelevant As Boolean
        Get
            Return _QUIDRelevant
        End Get
        Set(value As Boolean)
            If wb_Functions.TypeIstSollMenge(_Type, _ParamNr) AndAlso value Then
                'Setze Bit in Rezeptschritte.RS_Par1)
                _Par1 = wb_Global.RS_Par1_QUID
            End If
            _QUIDRelevant = value
        End Set
    End Property

    Public Property Backverlust As Double
        Get
            Return _Backverlust
        End Get
        Set(value As Double)
            _Backverlust = value
        End Set
    End Property

    Public Property Zuschnitt As Double
        Get
            Return _Zuschnitt
        End Get
        Set(value As Double)
            _Zuschnitt = value
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

    Public Property RohstoffGruppe1 As Integer
        Get
            Return _RohstoffGruppe1
        End Get
        Set(value As Integer)
            _RohstoffGruppe1 = value
        End Set
    End Property

    Public Property RohstoffGruppe2 As Integer
        Get
            Return _RohstoffGruppe2
        End Get
        Set(value As Integer)
            _RohstoffGruppe2 = value
        End Set
    End Property

    ''' <summary>
    ''' Rohstoffgruppe(n) as String
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RohstoffGruppe As String
        Get
            Dim Grp As String = ""

            'Bezeichnung aus Rohstoff-Gruppe 1
            If RohstoffGruppe1 > 0 AndAlso wb_Rohstoffe_Shared.RohGruppe.ContainsKey(RohstoffGruppe1) Then
                Grp = wb_Rohstoffe_Shared.RohGruppe(RohstoffGruppe1)
            End If

            'Bezeichnung aus Rohstoff-Gruppe 2
            If RohstoffGruppe2 > 0 AndAlso wb_Rohstoffe_Shared.RohGruppe.ContainsKey(RohstoffGruppe2) Then
                If Grp <> "" Then
                    Grp &= ", "
                End If
                Grp &= wb_Rohstoffe_Shared.RohGruppe(RohstoffGruppe2)
            End If

            'Zusammengesetzer String aus beiden Rohstoff-Gruppen
            Return Grp
        End Get
    End Property

    Public Property LagerOrt As String
        Get
            Return _LagerOrt
        End Get
        Set(value As String)
            _LagerOrt = value
        End Set
    End Property

    Public Function CalcIngredients(SollMenge As Double, Variante As Short) As IList
        'Umrechnungs-Faktor berechnen aus Sollmenge und Rezept-Gesamtgewicht
        Dim Faktor As Double = wb_Functions.SaveDiv(SollMenge, _RezGewicht)

        'Liste(IRecipeIngredient) löschen
        oi.Clear()
        For Each c As wb_Rezeptschritt In Steps

            'Es werden nur Rezeptschritte in die Liste aufgenommen, die einen Sollwert enthalten
            If wb_Functions.TypeIstSollMenge(c.Type, c.ParamNr) Then

                'Schnittstelle IRecipeIngredient
                Dim ri As New ob_RecipeIngredient
                ri.ArticleNo = c.Nummer
                ri.Variante = Variante
                ri.Unit = wb_Einheiten_Global.getobEinheitFromText(c.Einheit)

                'Fehler bei Niehaves (14-05-2020)
                'TODO siehe: https://stackoverflow.com/questions/40419003/avoiding-overflowexception-when-converting-from-double-to-decimal
                Try
                    ri.Amount = Convert.ToDecimal(c.fSollwert * Faktor)
                Catch
                    ri.Amount = 0
                End Try

                'Rezept im Rezept
                If (c.RezeptNr > 0) AndAlso c.RezeptImRezept IsNot Nothing Then
                    'Rezeptschritte aus Rezept-Im-Rezept hängen am RootRezeptschritt (es wird immer Variante 1 gelesen)
                    ri.Ingredients = c.RezeptImRezept.RootRezeptSchritt.CalcIngredients(ri.Amount, c.RezeptImRezept.Variante)
                    ri.ProductionArticle = True
                    ri.RecipeType = wb_Global.RecipeTypeProdVariabel
                Else
                    ri.ProductionArticle = False
                    ri.RecipeType = wb_Global.RecipeTypeNoRecipe
                End If
                'Rezeptzeile in Liste(IRecipeIngredient)
                oi.Add(ri)
            End If
        Next
        'Liste(IRecipeIngredient) zurückgeben
        Return oi
    End Function

    ''' <summary>
    ''' Fügt die Zutaten(Liste) für den aktuellen Rezeptschritt an die bestehende Liste an.
    ''' Je nach Inhalt des Deklarations-Feldes werden verschiedene Fälle unterschieden:
    ''' 
    '''     Deklarations-Feld 
    '''     
    '''         leer      -     Rohstoff-Name wird eingetragen
    '''         >         -     unterlagertes Rezept wird aufgelöst
    '''         Text      -     beliebiger Text wird übernommen
    '''         >Text     -     [Text] und Rezeptauflösung
    '''     
    ''' </summary>
    ''' <param name="zListe"></param>
    ''' <param name="Faktor"></param>
    Public Sub CalcZutaten(ByRef zListe As List(Of wb_ZutatenElement), Optional ReCalc As Boolean = False, Optional Faktor As Double = 1, Optional GrpRezNr As Integer = wb_Global.UNDEFINED)
        'Angaben zum Rezeptschritt in Liste anhängen
        Dim z As New wb_ZutatenElement
        z.GrpRezNr = GrpRezNr
        z = ZutatenListe(Faktor, ReCalc)

        'Deklarations-Bezeichnung des Rohstoffes an die Liste anhängen
        If Not z.KeineDeklaration Then
            If wb_Functions.TypeIstSollMenge(_Type, _ParamNr) Then
                zListe.Add(z)
                'Debug.Print("CalcZutaten - nach Step 1 (If TypeIstSollmenge)")
                'DebugPrintZutatenListe(zListe)
            End If

            'Aufruf der Routine vom Root-Rezeptschritt aus
            'unterlagerte Rezeptschritte werden auch im Array angehängt
            For Each x As wb_Rezeptschritt In ChildSteps
                x.CalcZutaten(zListe, ReCalc, Faktor, GrpRezNr)
            Next
            'Debug.Print("CalcZutaten - nach Step 2 (For Each x in ChildSteps)")
            'DebugPrintZutatenListe(zListe)

            'Zutatenliste auflösen
            If z.Aufloesen Then
                'Rezept im Rezept
                If (RezeptNr > 0) AndAlso RezeptImRezept IsNot Nothing Then
                    Dim f As Double = fSollwert / RezeptImRezept.RezeptGewicht
                    RezeptImRezept.RootRezeptSchritt.CalcZutaten(zListe, ReCalc, f, z.GrpRezNr)
                    'Debug.Print("CalcZutaten - nach Step 3 (If RezeptImRezept)")
                    'DebugPrintZutatenListe(zListe)
                End If
            Else
                'Gruppen-Nr Rezeptnummer mitnehmen
                _ZutatenListe.GrpRezNr = GrpRezNr
            End If
        End If
    End Sub

    Public Sub DebugPrintZutatenListe(zListe As List(Of wb_ZutatenElement))
        For Each x In zListe
            If x.FettDruck Then
                Debug.Print(x.Zutaten.ToUpper & " " & x.SollMenge & "kg" & "/" & x.GrpRezNr)
            Else
                Debug.Print(x.Zutaten & " " & x.SollMenge & "kg" & "/" & x.GrpRezNr)
            End If
        Next
    End Sub

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1066:Collapsible ""if"" statements should be merged", Justification:="<Ausstehend>")>
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
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
                    'TimeStamp wird nicht gelesen (wegen Fehler bei Konvertierung)
                    For i = 0 To winback.MySqlRead.FieldCount - 2
                        Try
                            Name = winback.MySqlRead.GetName(i)
                            Value = winback.MySqlRead.GetValue(i)
                        Catch ex As Exception
                            Name = wb_Global.UNDEFINED
                            Value = ""
                            If Debugger.IsAttached Then
                                Debug.Print(ex.Message)
                            End If
                        End Try

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
                                        'TODO Hier sollte global einstellbar die Kennung KEINE ANGABE ignoriert werden! (Niehaves)
                                        _ktTyp301.Allergen(ParamNr) = wb_Functions.StringtoAllergen(Value)
                                        If ((_ktTyp301.Allergen(ParamNr) = wb_Global.AllergenInfo.N) AndAlso Not wb_GlobalSettings.NwtAllergeneNoDefinitionErr) OrElse
                                            (_ktTyp301.Allergen(ParamNr) = wb_Global.AllergenInfo.ERR) Then
                                            _ktTyp301.FehlerKompName(ParamNr) = _Bezeichnung
                                            _ktTyp301DatenFehlerhaft = True
                                        End If
                                    ElseIf wb_KomponParam301_Global.IsErnaehrung(ParamNr) Then
                                        _ktTyp301.ErnaehrungsForm(ParamNr) = wb_Functions.StringtoErnaehrungsForm(Value)
                                        If (_ktTyp301.ErnaehrungsForm(ParamNr) = wb_Global.ErnaehrungsForm.X) OrElse (_ktTyp301.ErnaehrungsForm(ParamNr) = wb_Global.ErnaehrungsForm.ERR) Then
                                            _ktTyp301.FehlerKompName(ParamNr) = _Bezeichnung
                                            _ktTyp301DatenFehlerhaft = True
                                        End If
                                    Else
                                        If (Value IsNot Nothing) And (Value <> "") Then
                                            If _NwtRezGewicht > 0 Then
                                                _ktTyp301.Naehrwert(ParamNr) = wb_Functions.StrToDouble(Value) * wb_Functions.StrToDouble(_Sollwert) / _NwtRezGewicht
                                            Else
                                                _ktTyp301.Naehrwert(ParamNr) = 0
                                            End If
                                        Else
                                            _ktTyp301.FehlerKompName(ParamNr) = _Bezeichnung
                                            _ktTyp301DatenFehlerhaft = True
                                        End If

                                    End If
                                End If
                        End Select
                    Next
                Loop While winback.MySqlRead.Read
            End If
        End If

        'Datenbankverbindung wieder schliessen
        winback.Close()
        Return True
    End Function

End Class
