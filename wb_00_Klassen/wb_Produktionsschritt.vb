﻿Imports System.Reflection

Public Class wb_Produktionsschritt
    Private _parentStep As wb_Produktionsschritt
    Private _childSteps As New ArrayList()

    Private _ArtikelBezeichnung As String
    Private _RezeptBezeichnung As String
    Private _AuftragsNummer As String
    Private _Typ As String
    Private _ArtikelNummer As String
    Private _RezeptNummer As String
    Private _RezeptNr As Integer
    Private _RezeptVar As Integer
    Private _Linie As Integer
    Private _Sollwert_kg As Double
    Private _Sollwert_Stk As Double

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
    Public Sub CopyFrom(rs As wb_Produktionsschritt)
        Dim _type As Type = Me.GetType()
        Dim properties() As PropertyInfo = _type.GetProperties()
        For Each _property As PropertyInfo In properties
            If _property.CanWrite And _property.CanRead Then
                _property.SetValue(Me, _property.GetValue(rs, Nothing))
            End If
        Next
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

    Public ReadOnly Property VirtTreeStart As String
        Get
            Return ""
        End Get
    End Property

    Public ReadOnly Property VirtTreeCharge As String
        Get
            Return ""
        End Get
    End Property

    Public ReadOnly Property VirtTreeNummer As String
        Get
            If Typ = wb_Global.wbDatenArtikel Then
                Return ArtikelNummer
            Else
                Return RezeptNummer
            End If
        End Get
    End Property

    ''' <summary>
    ''' Bezeichnung. Anzeige im VirtualTree
    ''' </summary>
    ''' <returns>String - Bezeichnung</returns>
    Public ReadOnly Property VirtTreeBezeichnung() As String
        Get
            If Typ = wb_Global.wbDatenArtikel Then
                Return _ArtikelBezeichnung
            Else
                Return RezeptBezeichnung
            End If
        End Get
    End Property

    Public ReadOnly Property VirtTreeKommentar As String
        Get
            Return ""
        End Get
    End Property

    Public ReadOnly Property VirtTreeLinie As String
        Get
            Return _Linie
        End Get
    End Property


    ''' <summary>
    ''' Sollwert. Anzeige im VitualTree
    ''' </summary>
    ''' <returns>String - Sollwert</returns>
    Public Property VirtTreeSollwert As String
        Get
            If Typ = wb_Global.wbDatenArtikel Then
                Return wb_Functions.FormatStr(Sollwert_Stk, 0)
            Else
                Return wb_Functions.FormatStr(Sollwert_kg, 3)
            End If
        End Get
        Set(value As String)
            '_Sollwert = value
        End Set
    End Property
    Public ReadOnly Property VirtTreeEinheit As String
        Get
            If Typ = wb_Global.wbDatenArtikel Then
                Return "Stk"
            Else
                Return "kg"
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
        End Set
    End Property

    Public Property RezeptNummer As String
        Get
            Return _RezeptNummer
        End Get
        Set(value As String)
            _RezeptNummer = value
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

    Public Property ArtikelBezeichnung As String
        Get
            Return _ArtikelBezeichnung
        End Get
        Set(value As String)
            _ArtikelBezeichnung = value
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

    Public Property Sollwert_Stk As Double
        Get
            Return _Sollwert_Stk
        End Get
        Set(value As Double)
            _Sollwert_Stk = value
        End Set
    End Property

    Public Property Linie As Integer
        Get
            Return _Linie
        End Get
        Set(value As Integer)
            _Linie = value
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
End Class