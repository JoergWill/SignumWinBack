Public Class wb_Planung_Shared

    Public Shared ErrorList As New List(Of wb_ProduktionPlanungError)
    Public Shared Event eListe_Refresh(Sender As Object)

    ''' <summary>
    ''' Formatierte Liste aller Fehler als String
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property LongErrorList As String
        Get
            'Fehlermeldung wird dynamisch erzeugt
            Dim ErrorText As String = ""
            For Each ProdPlanError In ErrorList
                If ErrorText <> "" Then
                    ErrorText = ErrorText & vbCrLf & ProdPlanError.FehlerText
                Else
                    ErrorText = ProdPlanError.FehlerText
                End If
            Next
            Return ErrorText
        End Get
    End Property

    ''' <summary>
    ''' Formatierte Liste aller fehlerhaften Artikelnummer als String
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property ShortErrorList As String
        Get
            'Fehlermeldung wird dynamisch erzeugt
            Dim ErrorText As String = ""
            For Each ProdPlanError In ErrorList
                If ErrorText <> "" Then
                    ErrorText = ErrorText & "," & ProdPlanError.ArtikelNummer
                Else
                    ErrorText = ProdPlanError.ArtikelNummer
                End If
            Next
            Return ErrorText
        End Get
    End Property

    ''' <summary>
    ''' Fehlerliste anzeigen/aktualisieren
    ''' </summary>
    ''' <param name="sender"></param>
    Public Shared Sub Liste_Refresh(sender As Object)
        RaiseEvent eListe_Refresh(sender)
    End Sub

End Class
