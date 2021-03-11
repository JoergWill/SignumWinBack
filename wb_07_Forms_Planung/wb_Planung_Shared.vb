Public Class wb_Planung_Shared

    Public Shared ErrorList As New List(Of wb_ProduktionPlanungError)
    Public Shared Event eListe_Refresh(Sender As Object)
    Public Shared Produktion As New wb_Produktion

    Private Shared _ProdPlanGedruckt As Boolean = False
    Private Shared _ProduktionsDatum As Date

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

    Public Shared Property ProdPlanGedruckt As Boolean
        Get
            Return _ProdPlanGedruckt
        End Get
        Set(value As Boolean)
            _ProdPlanGedruckt = value
        End Set
    End Property

    Public Shared Property ProduktionsDatum As Date
        Get
            Return _ProduktionsDatum
        End Get
        Set(value As Date)
            _ProduktionsDatum = value
        End Set
    End Property
    Public Shared ReadOnly Property ProduktionsDatumStr As String
        Get
            Return _ProduktionsDatum.ToString("D")
        End Get
    End Property

    ''' <summary>
    ''' Fehlerliste anzeigen/aktualisieren
    ''' </summary>
    ''' <param name="sender"></param>
    Public Shared Sub Liste_Refresh(sender As Object)
        RaiseEvent eListe_Refresh(sender)
    End Sub

    ''' <summary>
    ''' Filtert die Produktions-Planungs-Schritte nach Aufarbeitung und Linengruppe
    ''' </summary>
    ''' <param name="a"></param>
    Public Shared Sub FilterAndMark(ByRef a As ArrayList, CheckAufloesen As Boolean, FilterAufarbeitung As Integer, FilterLinienGruppe As Integer, Optional AddToList As Boolean = False, Optional KundenBestellungTextDrucken As Boolean = True, Optional SonderTextDrucken As Boolean = False)
        'ArrayList leeren
        If Not AddToList Then
            a.Clear()
        End If

        'Alle Produktions-Schritte durchlaufen
        For Each child In Produktion.RootProduktionsSchritt.ChildSteps
            'Filtern nach Aufarbeitungsplatz und Liniengruppe
            If TryCast(child, wb_Produktionsschritt).Filter(FilterAufarbeitung, FilterLinienGruppe, CheckAufloesen, KundenBestellungTextDrucken, SonderTextDrucken) Then
                'Liste aufbauen
                a.Add(child)
                'Charge als produziert markieren
                TryCast(child, wb_Produktionsschritt).ChargeWirdProduziert()
            End If
        Next

        'Marker setzen Daten wurden an Produktion übertragen setzen
        ProdPlanGedruckt = True
    End Sub

End Class
