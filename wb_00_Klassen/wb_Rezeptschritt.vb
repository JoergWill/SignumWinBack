Imports System.Reflection
Imports WinBack.wb_Global.KomponTypen

Public Class wb_Rezeptschritt

    Private _SchrittNr As Integer
    Private _ParamNr As Integer
    Private _RohNr As Integer
    Private _Type As wb_Global.KomponTypen
    Private _Nummer As String
    Private _Bezeichnung As String
    Private _Sollwert As String
    Private _Einheit As String
    Private _PreisProKg As Double = 0
    Private _Prozent As Double
    Private _RezeptNr As Integer
    Private _TA As Integer = wb_Global.TA_Undefined
    Private _RezGewicht As Double
    Private _RezPreis As Double

    Private _parentStep As wb_Rezeptschritt
    Private _childSteps As New ArrayList()

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
            If _property.CanWrite And _property.CanRead Then
                _property.SetValue(Me, _property.GetValue(rs, Nothing))
            End If
        Next
    End Sub

    '' <summary>
    '' Create a new part with the given parent
    '' </summary>
    '' <param name="parent">The parent part</param>
    '' <param name="name">The name of this part</param>
    Public Sub New(parent As wb_Rezeptschritt, Bezeichnung As String)
        _parentStep = parent
        _Bezeichnung = Bezeichnung
        _TA = wb_Global.TA_Undefined
        If Not (_parentStep Is Nothing) Then
            parent._childSteps.Add(Me)
        End If
    End Sub 'New

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

    Public Property Sollwert As String
        Get
            Return _Sollwert
        End Get
        Set(value As String)
            _Sollwert = value
        End Set
    End Property

    Public ReadOnly Property VirtTreeBezeichnung() As String
        Get
            Debug.Print("Get VirtTreeBezeichnung " & _Type.ToString & " " & _Sollwert)
            Select Case _Type
                Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE
                    Return _Sollwert
                Case Else
                    If _RezeptNr > 0 Then
                        Return _Bezeichnung & "®"
                    Else
                        Return _Bezeichnung
                    End If
            End Select
        End Get
    End Property

    Public Property VirtTreeSollwert As String
        Get
            Select Case _Type
                Case KO_TYPE_PRODUKTIONSSTUFE, KO_TYPE_KESSEL, KO_TYPE_TEXTKOMPONENTE
                    Return ""
                Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE, KO_TYPE_WASSERKOMPONENTE
                    Return wb_Functions.FormatStr(_Sollwert, 3)
                Case Else
                    Return _Sollwert
            End Select
        End Get
        Set(value As String)
            '_Sollwert = value
        End Set
    End Property

    Public ReadOnly Property VirtTreePreis As String
        Get
            'TESTTEST
            Return TA.ToString
            If _Preis > 0 Then
                Return wb_Functions.FormatStr(_Preis, 2) + "€"
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property VirtTreeEinheit As String
        Get
            Select Case _Type
                Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE, KO_TYPE_WASSERKOMPONENTE, KO_TYPE_TEMPERATURERFASSUNG, KO_TYPE_KNETER
                    Return _Einheit
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public ReadOnly Property VirtTreeProzent As String
        Get
            Select Case _Type
                Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE, KO_TYPE_WASSERKOMPONENTE
                    If _ParamNr <= 1 And (_RezGewicht > 0) Then
                        Dim Prozent As Double = (Sollwert / _RezGewicht) * 100
                        Return wb_Functions.FormatStr(Prozent, 2)
                    Else
                        Return ""
                    End If
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public Property Einheit As String
        Get
            Return _Einheit
        End Get
        Set(value As String)
            _Einheit = value
        End Set
    End Property

    Public Property Prozent As Double
        Get
            Return 0
        End Get
        Set(value As Double)
            _Prozent = value
        End Set
    End Property

    Public Property Type As wb_Global.KomponTypen
        Get
            Return _Type
        End Get
        Set(value As wb_Global.KomponTypen)
            _Type = value
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

    Public ReadOnly Property TA As Integer
        Get
            'wird nur beim ersten Aufruf und nur bei Bedarf aus der Datenbank gelesen
            If _TA = wb_Global.TA_Undefined Then
                Select Case _Type
                    Case KO_TYPE_WASSERKOMPONENTE, wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER
                        _TA = wb_Global.TA_Wasser
                    Case wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE, wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE
                        _TA = wb_Functions.StrToInt(wb_sql_Functions.getKomponParam(_RohNr, 7, wb_Global.TA_Null))
                    Case wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL
                        _TA = wb_Functions.StrToInt(wb_sql_Functions.getKomponParam(_RohNr, 22, wb_Global.TA_Null))
                    Case wb_Global.KomponTypen.KO_TYPE_SAUER_ZUGABE
                        _TA = wb_Functions.StrToInt(wb_sql_Functions.getKomponParam(_RohNr, 3, wb_Global.TA_Null))
                    Case wb_Global.KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE
                        _TA = wb_Functions.StrToInt(wb_sql_Functions.getKomponParam(_RohNr, 4, wb_Global.TA_Null))

                    Case Else
                        _TA = wb_Global.TA_Null
                End Select
            End If
            Return _TA
        End Get
    End Property

    ''' <summary>
    ''' Gewichtswert des Rezeptschrittes. Gibt den Sollwert der Rezept-Zeile zurück, wenn diese eine Komponente enthält, die 
    ''' zum Rezeptgewicht zählt und das Flag 'zählt zum Rezeptgewicht' gesetzt ist.
    ''' </summary>
    ''' <returns>Double - Sollwert</returns>
    ''' 'TODO zählt zum Rezeptgewicht berücksichtigen !!!
    ''' 'TODO ordentliche umwandlung in DOUBLE
    Private ReadOnly Property _Gewicht As Double
        Get
            Select Case _Type
                Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE
                    Return _Sollwert
                Case wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
                    If _ParamNr = 1 Then
                        Return _Sollwert
                    Else
                        Return 0
                    End If
                Case Else
                    Return 0
            End Select
        End Get
    End Property

    Public ReadOnly Property Gewicht As Double
        Get
            Dim Childgewicht As Double = 0
            For Each x As wb_Rezeptschritt In ChildSteps
                Childgewicht = Childgewicht + x.Gewicht
            Next
            Return _Gewicht + Childgewicht
        End Get
    End Property

    Public WriteOnly Property RezGewicht As Double
        Set(value As Double)
            For Each x As wb_Rezeptschritt In ChildSteps
                x.RezGewicht = value
            Next
            _RezGewicht = value
        End Set
    End Property

    Private ReadOnly Property _TA_Mehlmenge As Double
        Get
            'Mehlanteil der aktuellen Komponente berechnen
            _TA_Mehlmenge = 0
            'Mehl hat TA=100
            If TA = wb_Global.TA_Mehl Then
                _TA_Mehlmenge = Sollwert
            End If
            'Sauerteig-Komponente (hat eigene TA) - Mehlanteil herausrechnen
            If TA > wb_Global.TA_Mehl Then
                _TA_Mehlmenge = (100 / TA) * Sollwert
            End If
        End Get
    End Property

    Public ReadOnly Property TA_Mehlmenge As Double
        Get
            Dim ChildTA_Mehlmenge As Double = 0
            For Each x As wb_Rezeptschritt In ChildSteps
                ChildTA_Mehlmenge = ChildTA_Mehlmenge + x.TA_Mehlmenge
            Next
            Return _TA_Mehlmenge + ChildTA_Mehlmenge
        End Get
    End Property

    Private ReadOnly Property _TA_Wassermenge As Double
        Get
            'Wasseranteil der aktuellen Komponente berechnen
            _TA_Wassermenge = 0
            'Wasserkomponente oder Eis oder Sauerteig-Wasser oder Handwasser
            If (ParamNr = 1) And ((TA = wb_Global.TA_Wasser) Or (_Type = KO_TYPE_WASSERKOMPONENTE) Or (_Type = KO_TYPE_EISKOMPONENTE) Or (_Type = KO_TYPE_SAUER_WASSER)) Then
                _TA_Wassermenge = Sollwert
            End If

            'Sauerteig-Komponente (hat eigene TA) - Wasseranteil herausrechnen
            If TA > 100 Then
                _TA_Wassermenge = Sollwert * (1 - (100 / TA))
            End If

            'Komponente mit Flüssiganteil TA < 100 - Wasseranteil herausrechnen
            'z.B. Flüssighefe
            '
            'Hier wird (fälschlicherweise) als TA der Wasseranteil eingetragen
            'also bei Flüssighefe mit 50% Wasser - TA 50
            If (TA < 100) And (TA <> 0) And (TA > 0) Then
                _TA_Wassermenge = Sollwert * (TA / 100)
            End If
        End Get
    End Property

    Public ReadOnly Property TA_Wassermenge As Double
        Get
            Dim ChildTA_Wassermenge As Double = 0
            For Each x As wb_Rezeptschritt In ChildSteps
                ChildTA_Wassermenge = ChildTA_Wassermenge + x.TA_Wassermenge
            Next
            Return _TA_Wassermenge + ChildTA_Wassermenge
        End Get
    End Property
    Private ReadOnly Property _Preis As Double
        Get
            Select Case _Type
                Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE
                    Return _PreisProKg * _Sollwert
                Case KO_TYPE_WASSERKOMPONENTE
                    If _ParamNr = 1 Then
                        Return _PreisProKg * _Sollwert
                    Else
                        Return 0
                    End If
                Case Else
                    Return 0
            End Select
        End Get
    End Property

    Public ReadOnly Property Preis As Double
        Get
            Dim ChildPreis As Double = 0
            For Each x As wb_Rezeptschritt In ChildSteps
                ChildPreis = ChildPreis + x.Preis
            Next
            Return _Preis + ChildPreis
        End Get
    End Property

    Public Property PreisProKg As Double
        Get
            Return _PreisProKg
        End Get
        Set(value As Double)
            _PreisProKg = value
        End Set
    End Property

End Class
