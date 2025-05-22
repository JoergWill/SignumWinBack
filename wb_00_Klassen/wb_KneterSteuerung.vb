Public Class wb_KneterSteuerung
    Inherits wb_TwinCatSteuerung

    Public KneterListe As New List(Of wb_KneterAnzeige_HD)

    Public Sub New(IpAdresse As String)
        MyBase.New(IpAdresse)
    End Sub

    Public Overloads Property Connected As Boolean
        Get
            Return _Connected
        End Get
        Set(value As Boolean)
            _Connected = value
            'Verbindungs-Status an die einzelnen Kneter propagieren
            For Each Kneter In KneterListe
                Kneter.Connected = _Connected
            Next
        End Set
    End Property

End Class
