Imports System.Reflection
Imports WinBack

Public Class wb_ChargenSchritt

    Private _parentStep As wb_ChargenSchritt
    Private _childSteps As New ArrayList()

    Private _ChargenNummer As String
    Private _AuftragsNummer As String
    Private _Status As Integer = wb_Global.UNDEFINED

    Private _ArtikelNummer As String
    Private _ArtikelBezeichnung As String
    Private _ArtikelNr As Integer

    Private _RezeptNummer As String
    Private _RezeptBezeichnung As String
    Private _RezeptNr As Integer
    Private _RezeptVar As Integer

    Private _KomponentenBezeichnung As String
    Private _KomponentenNummer As String
    Private _KomponentenNr As Integer
    Private _KomponentenType As wb_Global.KomponTypen
    Private _KomponentenParamNr As Integer

    Private _Linie As Integer = wb_Global.UNDEFINED
    Private _Schritt As Integer = wb_Global.UNDEFINED
    Private _SchrittIndex As Integer = wb_Global.UNDEFINED

    Private _Type As wb_Global.KomponTypen
    Private _ChrgType As wb_Global.ChargenTypen
    Private _Sollwert As String
    Private _Sollmenge_kg As Double
    Private _Sollmenge_Stk As Double
    Private _Sollmenge_Stk_gesamt As Double
    Private _Istwert As String
    Private _Istmenge_kg As Double = wb_Global.UNDEFINED
    Private _Istmenge_Stk As Double = wb_Global.UNDEFINED
    Private _Einheit As String

    Private _RS_Par1 As String
    Private _RS_Par2 As String
    Private _RS_Par3 As String

    Private _StartZeit As DateTime
    Private _EndeZeit As DateTime
    Private _UserNummer As Integer
    Private _User As String

    Private _Preis As Double
    Private _RohCharge As String

    ''' <summary>
    ''' Kopiert alle Properties dieser Klasse auf die Properties der übergebenen Klasse.
    ''' Geschrieben werden nur die Properties, die nicht als ReadOnly deklariert sind.
    ''' 
    ''' aus: https://stackoverflow.com/questions/531384/how-to-loop-through-all-the-properties-of-a-class
    '''
    ''' Dient dazu, die Inhalte eines Chargen-Schrittes auf einen anderen zu kopieren.
    ''' Durch die Schleife über alle Properties ist die Funktion unabhängig von eventuellen Erweiterungen.
    ''' </summary>
    ''' <param name="rs">wb_ChargenSchritt nimmt die Werte der Properties der Klasse auf</param>
    Public Sub CopyFrom(rs As wb_ChargenSchritt)
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
    Public Sub New(parent As wb_ChargenSchritt)
        _parentStep = parent
        If Not (_parentStep Is Nothing) Then
            parent._childSteps.Add(Me)
        End If
    End Sub 'New

    '' <summary>
    '' Parent dieses Rezeptschrittes
    '' </summary>
    Public Property ParentStep() As wb_ChargenSchritt
        Get
            Return _parentStep
        End Get
        Set(ByVal value As wb_ChargenSchritt)
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

    Public Property ChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
        Set(value As String)
            _ChargenNummer = value
        End Set
    End Property

    Public Property AuftragsNummer As String
        Get
            Return _AuftragsNummer
        End Get
        Set(value As String)
            _AuftragsNummer = value
        End Set
    End Property

    Public Property Status As Integer
        Get
            If _Status = wb_Global.UNDEFINED Then
                _Status = wb_Global.ChargenStatus.CS_UNBEARBEITET

                Select Case Type
                    Case wb_Global.KomponTypen.KO_ZEILE_DUMMYARTIKEL, wb_Global.KomponTypen.KO_ZEILE_ARTIKEL, wb_Global.KomponTypen.KO_ZEILE_REZEPT
                        For Each c As wb_ChargenSchritt In ChildSteps
                            If _Status < c.Status Then
                                _Status = c.Status
                            End If
                        Next
                    Case wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE
                        Return _Status
                    Case Else
                        Return 0
                End Select
            End If
            Return _Status
        End Get
        Set(value As Integer)
            _Status = value
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

    Public Property ArtikelBezeichnung As String
        Get
            Return _ArtikelBezeichnung
        End Get
        Set(value As String)
            _ArtikelBezeichnung = value
        End Set
    End Property

    Public Property ArtikelNr As Integer
        Get
            Return _ArtikelNr
        End Get
        Set(value As Integer)
            _ArtikelNr = value
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

    Public Property RezeptBezeichnung As String
        Get
            Return _RezeptBezeichnung
        End Get
        Set(value As String)
            _RezeptBezeichnung = value
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
            _RezeptVar = value
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
    Public Property LinienGruppe As Integer
        Get
            'TODO Liniengruppe  nach LINIE
            Return _Linie
        End Get
        Set(value As Integer)
            _Linie = value
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

    Public Property ChrgType As wb_Global.ChargenTypen
        Get
            Return _ChrgType
        End Get
        Set(value As wb_Global.ChargenTypen)
            _ChrgType = value
        End Set
    End Property

    Public Property Schritt As Integer
        Get
            Return _Schritt
        End Get
        Set(value As Integer)
            _Schritt = value
        End Set
    End Property

    Public Property SchrittIndex As Integer
        Get
            Return _SchrittIndex
        End Get
        Set(value As Integer)
            _SchrittIndex = value
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

    Public Property Sollmenge_kg As Double
        Get
            Return _Sollmenge_kg
        End Get
        Set(value As Double)
            _Sollmenge_kg = value
        End Set
    End Property

    Public Property Sollmenge_Stk As Double
        Get
            Return CInt(_Sollmenge_Stk)
        End Get
        Set(value As Double)
            _Sollmenge_Stk = value
        End Set
    End Property

    Public Property Sollmenge_Stk_gesamt As Double
        Get
            Return CInt(_Sollmenge_Stk_gesamt)
        End Get
        Set(value As Double)
            _Sollmenge_Stk_gesamt = value
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

    Public ReadOnly Property Istmenge_kg As Double
        Get
            If _Istmenge_kg = wb_Global.UNDEFINED Then
                Select Case Type
                    'Rezept-Kopf-Zeile
                    Case wb_Global.KomponTypen.KO_ZEILE_REZEPT
                        'Istmenge(kg) Null setzen
                        _Istmenge_kg = 0.0
                        'Summe aller Istwerte in den ChildSteps (Rezept-Komponenten)
                        For Each c As wb_ChargenSchritt In ChildSteps
                            'Komponente hat Sollwert
                            If wb_Functions.TypeIstSollMenge(c._KomponentenType, c._KomponentenParamNr) Then
                                'Komponente zählt (nicht) zum Rezeptgewicht
                                If Not wb_Chargen_Shared.ZaehltNichtZumRezeptgewicht(c.KomponentenNr) Then
                                    _Istmenge_kg += wb_Functions.StrToDouble(c.Istwert)
                                End If
                            End If
                        Next
                    'Artikel-Zeile
                    Case wb_Global.KomponTypen.KO_ZEILE_ARTIKEL
                        'Istmenge(kg) Null setzen
                        _Istmenge_kg = 0.0
                        'Summe aller Istwerte in den ChildSteps (Rezept-Kopf-Zeilen)
                        For Each c As wb_ChargenSchritt In ChildSteps
                            _Istmenge_kg += wb_Functions.StrToDouble(c.Istmenge_kg)
                        Next
                End Select
            End If
            Return _Istmenge_kg
        End Get
        'Set(value As Double)
        '    _Istmenge_kg = value
        'End Set
    End Property

    Public ReadOnly Property Istmenge_Stk As Double
        Get
            If _Istmenge_Stk = wb_Global.UNDEFINED Then
                If Type = wb_Global.KomponTypen.KO_ZEILE_ARTIKEL Then
                    'Stückgewicht ermitteln (aus Sollmenge und Sollgewicht)
                    Dim StkGewicht As Double = wb_Functions.SaveDiv(_Sollmenge_kg, _Sollmenge_Stk)
                    'Istmenge ist Stk aus Istwert in kg und StkGewicht
                    _Istmenge_Stk = wb_Functions.SaveDiv(Istmenge_kg, StkGewicht)
                End If
            End If
            Return CInt(_Istmenge_Stk)
        End Get
        'Set(value As Double)
        '    _Istmenge_Stk = value
        'End Set
    End Property

    Public Property Einheit As String
        Get
            Return _Einheit
        End Get
        Set(value As String)
            _Einheit = value
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

    Public Property UserNummer As Integer
        Get
            Return _UserNummer
        End Get
        Set(value As Integer)
            _UserNummer = value
        End Set
    End Property

    Public Property User As String
        Get
            Return _User
        End Get
        Set(value As String)
            _User = value
        End Set
    End Property

    Public Property Preis As Double
        Get
            Return _Preis
        End Get
        Set(value As Double)
            _Preis = value
        End Set
    End Property

    Public Property RohCharge As String
        Get
            Return _RohCharge
        End Get
        Set(value As String)
            _RohCharge = value
        End Set
    End Property

    Public ReadOnly Property VirtTreeCharge As String
        Get
            Select Case Type
                Case wb_Global.KomponTypen.KO_ZEILE_ARTIKEL
                    Return _AuftragsNummer
                Case wb_Global.KomponTypen.KO_ZEILE_REZEPT
                    Return _ChargenNummer
                Case Else
                    Return ""
            End Select

        End Get
    End Property

    Public ReadOnly Property VirtTreeStatus As Drawing.Icon
        Get
            Select Case Status
                Case wb_Global.ChargenStatus.CS_UNBEARBEITET
                    Return Nothing
                Case wb_Global.ChargenStatus.CS_IN_ARBEIT
                    Return Nothing

                Case wb_Global.ChargenStatus.CS_OK
                    Return My.Resources.Status_02_16x16

                Case wb_Global.ChargenStatus.CS_WARNUNG
                    Return My.Resources.Status_03_16x16
                Case wb_Global.ChargenStatus.CS_ERR_HAND
                    Return My.Resources.Status_04_16x16
                Case wb_Global.ChargenStatus.CS_ERR_AUTO
                    Return My.Resources.Status_05_16x16

                Case wb_Global.ChargenStatus.CS_MULTISTART
                    Return My.Resources.Status_06_16x16
                Case wb_Global.ChargenStatus.CS_NACHTSTART
                    Return My.Resources.Status_07_16x16
                Case wb_Global.ChargenStatus.CS_STARTGESPEICHERT
                    Return My.Resources.Status_08_16x16
                Case Else
                    Return Nothing
            End Select
        End Get
    End Property

    Public ReadOnly Property VirtTreeNummer As String
        Get
            Select Case Type
                Case wb_Global.KomponTypen.KO_ZEILE_ARTIKEL, wb_Global.KomponTypen.KO_ZEILE_DUMMYARTIKEL
                    Return _ArtikelNummer
                Case wb_Global.KomponTypen.KO_ZEILE_REZEPT
                    Return _RezeptNummer
                Case Else
                    Return _KomponentenNummer
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Bezeichnung. Anzeige im VirtualTree
    ''' </summary>
    ''' <returns>String - Bezeichnung</returns>
    Public ReadOnly Property VirtTreeBezeichnung() As String
        Get
            Select Case Type
                Case wb_Global.KomponTypen.KO_ZEILE_ARTIKEL, wb_Global.KomponTypen.KO_ZEILE_DUMMYARTIKEL
                    Return _ArtikelBezeichnung
                Case wb_Global.KomponTypen.KO_ZEILE_REZEPT
                    Return _RezeptBezeichnung
                Case Else
                    If wb_Functions.TypeIstText(KomponentenType) Then
                        Return _Sollwert
                    Else
                        Return _KomponentenBezeichnung
                    End If
            End Select
        End Get
    End Property

    Public ReadOnly Property VirtTreeParams As String
        Get
            If Type = wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE Then
                Select Case KomponentenType
                    Case wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE
                        'bei Silo-Komponenten Mehl/Raum-Temperatur anzeigen
                        If (RS_Par1 <> "") And (RS_Par2 <> "") Then
                            Dim Rt As String = "RT" & wb_Functions.FormatStr(RS_Par1, 1) & "°C"
                            Dim Mt As String = "MT" & wb_Functions.FormatStr(RS_Par2, 1) & "°C"
                            Return Rt & " / " & Mt
                        End If
                    Case wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
                        'bei Wasser die Auslauf-Nummer anzeigen
                        If (RS_Par1 <> "") And (KomponentenParamNr = 1) Then
                            Dim ANr As String = "A" & wb_Functions.FormatStr(RS_Par1, 0)
                            Return ANr
                        End If
                    Case Else
                        Return ""
                End Select
            End If
            Return ""
        End Get
    End Property

    Public ReadOnly Property VirtTreeLinie As String
        Get
            Return Linie
        End Get
    End Property

    Public ReadOnly Property VirtTreeSollwert As String
        Get
            Select Case Type
                Case wb_Global.KomponTypen.KO_ZEILE_DUMMYARTIKEL
                    Return ""
                Case wb_Global.KomponTypen.KO_ZEILE_ARTIKEL
                    Return _Sollmenge_Stk_gesamt
                Case wb_Global.KomponTypen.KO_ZEILE_REZEPT
                    Return wb_Functions.FormatStr(_Sollmenge_kg, 3)
                Case Else
                    If wb_Functions.TypeIstSollMenge(KomponentenType, 1) Then
                        Return wb_Functions.FormatStr(_Sollwert, 3)
                    Else
                        Return _Sollwert
                    End If
            End Select
        End Get
    End Property

    Public ReadOnly Property VirtTreeEinheit As String
        Get
            Select Case Type
                Case wb_Global.KomponTypen.KO_ZEILE_DUMMYARTIKEL
                    Return ""
                Case wb_Global.KomponTypen.KO_ZEILE_ARTIKEL
                    'TODO Internationalisierung !!
                    Return "Stk"
                Case wb_Global.KomponTypen.KO_ZEILE_REZEPT
                    Return "kg"
                Case Else
                    Select Case KomponentenType
                        Case wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT
                            Return ""
                        Case wb_Global.KomponTypen.KO_TYPE_SAUER_TEXT, wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE
                            Return ""
                        Case Else
                            Return _Einheit
                    End Select
            End Select
        End Get
    End Property

    Public ReadOnly Property VirtTreeIstwert As String
        Get
            Select Case Type
                Case wb_Global.KomponTypen.KO_ZEILE_DUMMYARTIKEL
                    Return ""
                Case wb_Global.KomponTypen.KO_ZEILE_ARTIKEL
                    Return Istmenge_Stk
                Case wb_Global.KomponTypen.KO_ZEILE_REZEPT
                    Return wb_Functions.FormatStr(Istmenge_kg.ToString, 3)
                Case Else
                    If wb_Functions.TypeIstSollMenge(KomponentenType, 1) Then
                        Return wb_Functions.FormatStr(Istwert, 3)
                    Else
                        Return _Istwert
                    End If
            End Select
        End Get
    End Property

    Public ReadOnly Property VirtTreeZeit As String
        Get
            If (Status = wb_Global.ChargenStatus.CS_UNBEARBEITET) Or (_StartZeit = wb_Global.wbNODATE) Then
                Return ""
            Else
                If KomponentenType = wb_Global.KomponTypen.KO_TYPE_TEMPERATURERFASSUNG Then
                    Return _EndeZeit.ToString("dd.MM.yyyy HH:mm:ss")
                Else
                    Return _StartZeit.ToString("dd.MM.yyyy HH:mm:ss")
                End If
            End If
        End Get
    End Property

    Public ReadOnly Property VirtTreeUser As String
        Get
            Select Case Type
                Case wb_Global.KomponTypen.KO_ZEILE_REZEPT
                    'Falls kein User eingetragen ist (Fehler in WinBack) - wird der User der vorhergehenden Charge eingetragen
                    Dim UserName As String = ""
                    For Each c As wb_ChargenSchritt In ChildSteps
                        If c.User = "" Then
                            c.User = UserName
                        Else
                            UserName = c.User
                        End If
                    Next
                    Return ""
                Case wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE
                    'Rezeptzeilen - User nur anzeigen wenn bearbeitet
                    If Status > wb_Global.ChargenStatus.CS_IN_ARBEIT Then
                        Return User
                    Else
                        Return ""
                    End If
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public ReadOnly Property VirtTreePreis As String
        Get
            If (_Preis > 0) And (Type = wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE) Then
                Return wb_Functions.FormatStr(_Preis.ToString, 2)
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property VirtTreeRohCharge As String
        Get
            Return _RohCharge
        End Get
    End Property

    Public Property KomponentenBezeichnung As String
        Get
            Return _KomponentenBezeichnung
        End Get
        Set(value As String)
            _KomponentenBezeichnung = value
        End Set
    End Property

    Public Property KomponentenNummer As String
        Get
            Return _KomponentenNummer
        End Get
        Set(value As String)
            _KomponentenNummer = value
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

    Public Property KomponentenType As wb_Global.KomponTypen
        Get
            Return _KomponentenType
        End Get
        Set(value As wb_Global.KomponTypen)
            _KomponentenType = value
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

    Public Property RS_Par1 As String
        Get
            Return _RS_Par1
        End Get
        Set(value As String)
            _RS_Par1 = value
        End Set
    End Property

    Public Property RS_Par2 As String
        Get
            Return _RS_Par2
        End Get
        Set(value As String)
            _RS_Par2 = value
        End Set
    End Property

    Public Property RS_Par3 As String
        Get
            Return _RS_Par3
        End Get
        Set(value As String)
            _RS_Par3 = value
        End Set
    End Property
End Class

'wbdaten.BAK_ArbRezepte
'
'B_ARZ_TW_Nr
'B_ARZ_TW_Idx
'B_ARZ_LiBeh_Nr
'B_ARZ_Index
'B_ARZ_Charge_Nr
'B_ARZ_Best_Nr
'B_ARZ_Nr
'B_ARZ_RZ_Variante_Nr
'B_ARZ_Bezeichnung
'B_ARZ_Typ
'B_ARZ_Erststart
'B_ARZ_Startzeit
'B_ARZ_Endezeit
'B_ARZ_Art_Einheit
'B_ARZ_Sollmenge_kg
'B_ARZ_Sollmenge_stueck
'B_ARZ_Anstellgut_kg
'B_ARZ_Status
'B_ARZ_Kommentar
'B_ARZ_zv_status
'B_ARZ_Zp_Gestartet
'B_ARZ_Zp_Beendet
'B_ARZ_KA_NrAlNum
'B_ARZ_Art_Index
'B_ARZ_RZ_Typ
'B_ARZ_Seg_Nr
'B_RZ_Nr_AlNum
'B_RZ_Bezeichnung
'B_RZ_Gewicht
'B_ARZ_Timestamp
'B_ARZ_Ch_Einheit


'wbdaten.BAK_ArbRezSchritte
'
'B_ARS_TW_Nr
'B_ARS_TW_Idx
'B_ARS_Beh_Nr
'B_ARS_RunIdx
'B_ARS_Charge_Nr
'B_ARS_Art_Index
'B_ARS_RZ_Nr
'B_ARS_Index
'B_ARS_Schritt_Nr
'B_ARS_Schritt_SubNr
'B_ARS_Ko_Nr
'B_ARS_ParamNr
'B_ARS_Wert
'B_ARS_Wert_org
'B_ARS_RS_Wert
'B_ARS_RS_Par1
'B_ARS_RS_Par2
'B_ARS_RS_Par3
'B_ARS_Istwert
'B_ARS_Gestartet
'B_ARS_Beendet
'B_ARS_User_Nr
'B_ARS_User_Name
'B_ARS_Status
'B_KO_Nr_AlNum
'B_KO_Bezeichnung
'B_KO_Temp_Korr
'B_KT_Typ_Nr
'B_KT_Rezept
'B_KT_Bezeichnung
'B_KT_KurzBez
'B_KT_EinheitIndex
'B_KP_Wert
'B_KT_Format
'B_KT_Laenge
'B_KT_UnterGW
'B_KT_OberGW
'B_E_Einheit
'B_ARS_Timestamp
'B_ARS_BF_Charge
'B_ARS_Preis
'B_ARS_PreisEinheit

