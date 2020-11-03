Public Class wb_ProduktionPlanungError
    Private _ArtikelNummer As String
    Private _Artikelbezeichnung As String
    Private _ErrorCode As wb_Global.ChargenTeilerResult

    Public Property ArtikelNummer As String
        Get
            Return _ArtikelNummer
        End Get
        Set(value As String)
            _ArtikelNummer = value
        End Set
    End Property

    Public Property Artikelbezeichnung As String
        Get
            Return _Artikelbezeichnung
        End Get
        Set(value As String)
            _Artikelbezeichnung = value
        End Set
    End Property

    Public Property ErrorCode As wb_Global.ChargenTeilerResult
        Get
            Return _ErrorCode
        End Get
        Set(value As wb_Global.ChargenTeilerResult)
            _ErrorCode = value
        End Set
    End Property

    ''' <summary>
    ''' Ergebnis der Chargen-Aufteilung als Text
    ''' 
    '''     OK      'Chargenaufteilung in Ordnung
    '''     EM1     'Nach Aufteilung in Optimalchargen bleibt eine Restmenge offen, die nicht produziert werden kann
    '''     EM2     'Nach Aufteilung in Optimalchargen wird mehr produziert als gefordert
    '''     EM3     'Nur eine Restcharge, Restmenge unterhalb Mindestchargen - Chargen gleicher Teige müssen zusammengefasst werden.
    '''     EP1     'Sollmenge nicht erreicht, Restmenge unterhalb Mindestcharge
    '''     EP2     'Sollmenge nicht erreicht, Restmenge unterhalb Mindestcharge
    '''     EP9     'Keine Chargengrößen angegeben, Aufteilung nach Rezeptgröße
    '''     ART     'Artikelnummer nicht gefunden
    '''     REZ     'Rezeptnummer nicht gefunden    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ErrorText As String
        Get
            Select Case ErrorCode
                Case wb_Global.ChargenTeilerResult.EM1
                    Return "Nach Aufteilung in Optimalchargen bleibt eine Restmenge kleiner als die Minimalcharge"
                Case wb_Global.ChargenTeilerResult.EM2
                    Return "Nach Aufteilung in Optimalchargen wird mehr produziert als geplant"
                Case wb_Global.ChargenTeilerResult.EM3
                    Return "Nur eine Restcharge unterhalb der Minimalcharge"
                Case wb_Global.ChargenTeilerResult.EP1, wb_Global.ChargenTeilerResult.EP2
                    Return "Sollmenge nicht erreicht, Restmenge unterhalb der Minimalcharge"
                Case wb_Global.ChargenTeilerResult.EP9
                    Return "Keine Chargengrößen angegeben, Aufteilung nach Rezeptgröße"
                Case wb_Global.ChargenTeilerResult.ART
                    Return "Artikelnummer in WinBack nicht gefunden oder kein Rezept verknüpft"
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public ReadOnly Property FehlerText As String
        Get
            Try
                If Artikelbezeichnung IsNot Nothing Then
                    Return ArtikelNummer.PadLeft(10) & " " & Artikelbezeichnung.PadRight(25) & vbCrLf & ErrorText
                Else
                    Return ArtikelNummer.PadLeft(10) & " " & ErrorText
                End If
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
End Class
