Public Class wb_Schnittstelle_Shared


    Private Shared _FileName As String
    Private Shared _AllLines As String()

    Public Shared SchnittstelleTabelle As New wb_SchnittstelleTabelle
    Public Shared Event eVorschauAktualisieren(Sender As Object)

    ''' <summary>
    ''' Liest alle Zeilen aus FileName in das Array _AllLines
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property Vorschau_FileName As String
        Get
            Return _FileName
        End Get
        Set(value As String)
            'Dateiname Import-File
            _FileName = value
            'alle Zeilen in ein eindimensionales Array einlesen
            _AllLines = System.IO.File.ReadAllLines(_FileName)

            'erste Daten-Zeile prüfen (ohne Überschrift)
            If _AllLines.Length > SchnittstelleTabelle.FirstRealLine Then
                SchnittstelleTabelle.CheckFormat(_AllLines(SchnittstelleTabelle.FirstRealLine - 1))
            End If
        End Set
    End Property

    Public Shared ReadOnly Property AllLines As String()
        Get
            Return _AllLines
        End Get
    End Property

    Public Shared Sub LoadAndShow()
        RaiseEvent eVorschauAktualisieren(Nothing)
    End Sub
End Class
