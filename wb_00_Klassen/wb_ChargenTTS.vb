Imports WinBack

Public Class wb_ChargenTTS

    Private _TWNr As Integer
    Private _ChargenNummer As Integer = wb_Global.UNDEFINED
    Private _RzNr As Integer

    Private _KomponentenNr As Integer
    Private _KomponentenParamNr As Integer
    Private _KomponentenType As wb_Global.KomponTypen
    Private _ARS_Wert As String
    Private _ARS_RS_Wert As String
    Private _Istwert As String
    Private _StartZeit As DateTime
    Private _EndeZeit As DateTime

    Private _StartWasser As DateTime
    Private _FertigWasser As DateTime
    Private _FertigTempMess As DateTime

    Private _WasserMenge As String
    Private _WasserTemp As String
    Private _rsPar1 As String
    Private _rsPar2 As String
    Private _rsPar3 As String
    Private _DBrsPar1 As String
    Private _DBrsPar2 As String
    Private _DBrsPar3 As String

    Private _Raumtemp As String
    Private _Mehltemp As String

    Private _EisMenge As String
    Private _TeigTemp As String

    Public Property TWNr As Integer
        Get
            Return _TWNr
        End Get
        Set(value As Integer)
            _TWNr = value
        End Set
    End Property

    Public Property ChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
        Set(value As String)
            _ChargenNummer = value
        End Set
    End Property

    Public Property RzNr As Integer
        Get
            Return _RzNr
        End Get
        Set(value As Integer)
            _RzNr = value
        End Set
    End Property

    Public Property StartWasser As Date
        Get
            Return _StartWasser
        End Get
        Set(value As Date)
            _StartWasser = value
        End Set
    End Property

    Public Property FertigWasser As Date
        Get
            Return _FertigWasser
        End Get
        Set(value As Date)
            _FertigWasser = value
        End Set
    End Property

    Public ReadOnly Property StartTempMess As Date
        Get
            Return _FertigWasser
        End Get
    End Property

    Public Property FertigTempMess As Date
        Get
            Return _FertigTempMess
        End Get
        Set(value As Date)
            _FertigTempMess = value
        End Set
    End Property

    Public ReadOnly Property DiffTempMess As Integer
        Get
            Return _FertigTempMess.Subtract(_StartWasser).TotalMinutes
        End Get
    End Property
    Public ReadOnly Property sDiffTempMess As String
        Get
            If DiffTempMess > 40 Then
                Return "Messung nach " & DiffTempMess.ToString("###") & " Minuten"
            Else
                Return DiffTempMess.ToString("###") & " Min"
            End If
        End Get
    End Property

    Public ReadOnly Property sTooltip As String
        Get
            'Teigtemperatur dieser Charge
            Dim ToolTip As String = "Teigtemperatur " & TeigTemp & " °C"

            'Raum-/Mehlfühler
            If _Raumtemp <> "" Then
                ToolTip = ToolTip & vbCrLf & "Raum-Mehlfühler " & wb_Functions.FormatStr(_Raumtemp, 1) & " °C /" & wb_Functions.FormatStr(_Mehltemp, 1) & " °C"
            End If

            'Parameter TTS
            If Rs_Par1 <> "" Then
                ToolTip = ToolTip & vbCrLf & "Parameter      " & Rs_Par1 & "/" & Rs_Par2 & "/" & Rs_Par3
            End If

            Return ToolTip
        End Get
    End Property

    Public Property WasserMenge As String
        Get
            Return _WasserMenge
        End Get
        Set(value As String)
            _WasserMenge = value
        End Set
    End Property

    Public Property WasserTemp As String
        Get
            Return wb_Functions.FormatStr(_WasserTemp, 1)
        End Get
        Set(value As String)
            _WasserTemp = value
        End Set
    End Property

    Public Property Rs_Par1 As String
        Get
            Return _rsPar1
        End Get
        Set(value As String)
            _DBrsPar1 = value
        End Set
    End Property

    Public Property Rs_Par2 As String
        Get
            Return _rsPar2
        End Get
        Set(value As String)
            _DBrsPar2 = value
        End Set
    End Property

    Public Property Rs_Par3 As String
        Get
            Return _rsPar3
        End Get
        Set(value As String)
            _DBrsPar3 = value
        End Set
    End Property

    Public Property EisMenge As String
        Get
            Return _EisMenge
        End Get
        Set(value As String)
            _EisMenge = value
        End Set
    End Property

    Public Property TeigTemp As String
        Get
            Return wb_Functions.FormatStr(_TeigTemp, 1)
        End Get
        Set(value As String)
            _TeigTemp = value
        End Set
    End Property

    Public Property KomponentenNr As Integer
        Get
            Return _KomponentenNr
        End Get
        Set(value As Integer)
            _KomponentenNr = value
        End Set
    End Property

    Public Property KomponentenParamNr As Integer
        Get
            Return _KomponentenParamNr
        End Get
        Set(value As Integer)
            _KomponentenParamNr = value
        End Set
    End Property

    Public Property KomponentenType As wb_Global.KomponTypen
        Get
            Return _KomponentenType
        End Get
        Set(value As wb_Global.KomponTypen)
            _KomponentenType = value
        End Set
    End Property

    Public Property ARS_Wert As String
        Get
            Return _ARS_Wert
        End Get
        Set(value As String)
            _ARS_Wert = value
        End Set
    End Property

    Public Property ARS_RS_Wert As String
        Get
            Return _ARS_RS_Wert
        End Get
        Set(value As String)
            _ARS_RS_Wert = value
        End Set
    End Property

    Public Property StartZeit As Date
        Get
            Return _StartZeit
        End Get
        Set(value As Date)
            _StartZeit = value
        End Set
    End Property

    Public Property EndeZeit As Date
        Get
            Return _EndeZeit
        End Get
        Set(value As Date)
            _EndeZeit = value
        End Set
    End Property

    Public Property Istwert As String
        Get
            Return _Istwert
        End Get
        Set(value As String)
            _Istwert = value
        End Set
    End Property

    ''' <summary>
    ''' Alle gesammelten Daten aus der Datenbank-Abfrage abhängig von KomponentenType und Parameter-Nummer in die einzelnen Felder 
    ''' einsortieren
    ''' </summary>
    Public Sub ProcessData()
        Select Case KomponentenType
            Case wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE

                Select Case KomponentenParamNr
                    Case 1
                        StartWasser = StartZeit
                        FertigWasser = EndeZeit
                        WasserMenge = Istwert

                    Case 3
                        WasserTemp = Istwert
                        _rsPar1 = _DBrsPar1
                        _rsPar2 = _DBrsPar2
                        _rsPar3 = _DBrsPar3
                End Select

            Case wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE
                EisMenge = ARS_Wert

            Case wb_Global.KomponTypen.KO_TYPE_TEMPERATURERFASSUNG
                FertigTempMess = EndeZeit
                TeigTemp = Istwert

            Case wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE
                If _DBrsPar1 <> "" Then
                    _Raumtemp = _DBrsPar1
                End If
                If _DBrsPar2 <> "" Then
                    _Mehltemp = _DBrsPar2
                End If


        End Select
    End Sub
End Class
