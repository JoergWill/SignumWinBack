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
    Private _TA As Integer
    Private _RezGewicht As Double
    Private _RezPreis As Double

    Private _parentPart As wb_Rezeptschritt
    Private _childParts As New ArrayList()

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
        _parentPart = parent
        _Bezeichnung = Bezeichnung
        If Not (_parentPart Is Nothing) Then
            parent._childParts.Add(Me)
        End If
    End Sub 'New

    '' <summary>
    '' The parent of this part
    '' </summary>
    Public Property ParentPart() As wb_Rezeptschritt
        Get
            Return _parentPart
        End Get
        Set(ByVal value As wb_Rezeptschritt)
            _parentPart = value
        End Set
    End Property

    '' <summary>
    '' A list of the child parts
    '' </summary>
    Public ReadOnly Property ChildParts() As IList
        Get
            Return _childParts
        End Get
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

    Public Property TA As Integer
        Get
            Return _TA
        End Get
        Set(value As Integer)
            'TODO muss berechnet werden aus KomponParameter ParamNr=7 auslesen für jede einzelne Komponente
            _TA = value
        End Set
    End Property

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
            For Each x As wb_Rezeptschritt In ChildParts
                Childgewicht = Childgewicht + x.Gewicht
            Next
            Return _Gewicht + Childgewicht
        End Get
    End Property

    Public WriteOnly Property RezGewicht As Double
        Set(value As Double)
            For Each x As wb_Rezeptschritt In ChildParts
                x.RezGewicht = value
            Next
            _RezGewicht = value
        End Set
    End Property

    Private ReadOnly Property _Preis As Double
        Get
            Select Case _Type
                Case KO_TYPE_AUTOKOMPONENTE, KO_TYPE_HANDKOMPONENTE, KO_TYPE_EISKOMPONENTE
                    Return _PreisProKg * _Sollwert

                Case Else
                    Return 0
            End Select
        End Get
    End Property

    Public ReadOnly Property Preis As Double
        Get
            Dim ChildPreis As Double = 0
            For Each x As wb_Rezeptschritt In ChildParts
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
