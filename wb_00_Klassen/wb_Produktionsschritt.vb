Imports System.Reflection

Public Class wb_Produktionsschritt
    Private _Bezeichnung As String

    Private _parentStep As wb_Produktionsschritt
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
        _Bezeichnung = Bezeichnung
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
    ''' Bezeichnung. Anzeige im VirtualTree (Rezeptur)
    ''' Bei Produktions-Stufen, Kessel und Text-Komponenten wird der Sollwert als Text angezeigt
    ''' bei allen anderen Komponenten-Typen die Komponenten-Bezeichnung.
    ''' 'TODO Bei Sprachumschaltung wird wahlweise der Kommentar angezeigt
    ''' </summary>
    ''' <returns>String - Bezeichnung</returns>
    Public ReadOnly Property VirtTreeBezeichnung() As String
        Get
            Return "Test"
        End Get
    End Property

    ''' <summary>
    ''' Sollwert. Anzeige im VitualTree (Rezeptur)
    ''' Bei Produktions-Stufen, Kessel und Text-Komponenten wird ein leeres Feld angezeigt,
    ''' bei Automatik, Hand, Eis und Wasser wird der Sollwert formatiert mit 3 Nachkomma-Stellen angezeigt.
    ''' </summary>
    ''' <returns>String - Sollwert</returns>
    Public Property VirtTreeSollwert As String
        Get
            Return "10.05"
        End Get
        Set(value As String)
            '_Sollwert = value
        End Set
    End Property

End Class
