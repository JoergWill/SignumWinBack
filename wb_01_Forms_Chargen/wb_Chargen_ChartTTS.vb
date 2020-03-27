Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Chargen_ChartTTS
    Inherits DockContent
    Const MaxChart = 10

    Private _ChargenZeile As wb_ChargenSchritt
    Private _TTS As New wb_ChargenTTS
    Private _TTSChargen As New ArrayList

    Public WriteOnly Property ChargenZeile As wb_ChargenSchritt
        Set(value As wb_ChargenSchritt)
            _ChargenZeile = value
        End Set
    End Property

    Private Sub wb_Chargen_ChartTTS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Tageswechsel-Nummer
        Dim TWNr As Integer = wb_Chargen_Shared.Liste_TagesWechselNummer
        'Rezept-Nummer/Rezept Variante
        Dim RzNr = _ChargenZeile.RezeptNr
        Dim RzVr = _ChargenZeile.RezeptVar

        'Liste löschen
        _TTSChargen.Clear()
        'Start Wasser/Start Temperaturmessung aller Teige mit dieser Rezeptnummer aus wbdaten laden
        MySqlLoadWbDaten(TWNr, RzNr, RzVr)

        'Diagramm anzeigen
        ShowTTSDiagramm()

    End Sub

    Private Sub ShowTTSDiagramm()

        'Chart Teig-Herstellung(Wasser-Dosierung)
        chrtTTS.Series.Add("Teig")
        chrtTTS.Series("Teig").ChartType = DataVisualization.Charting.SeriesChartType.RangeBar
        chrtTTS.Series("Teig").YValuesPerPoint = 2
        'Chart Teigtemperatur-Messung 
        chrtTTS.Series.Add("Temp")
        chrtTTS.Series("Temp").YValuesPerPoint = 2
        chrtTTS.Series("Temp").ChartType = DataVisualization.Charting.SeriesChartType.RangeBar

        Dim h As Integer = 0
        Dim TempFertig As Date = Now
        'alle Einträge im Array
        For Each x As wb_ChargenTTS In _TTSChargen
            'Höhe im Chart
            h += 1

            'maximale Höhe im Chart erreicht
            If (h > MaxChart) And (x.FertigTempMess > TempFertig) Then
                h = 1
            End If

            'Fertig-Zeitpunkt der ersten Charge merken
            If h = 1 Then
                TempFertig = x.FertigTempMess
            End If

            'Balken Wasser-Dosierung
            chrtTTS.Series("Teig").Points.AddXY(h, x.StartWasser, x.FertigWasser)
            chrtTTS.Series("Teig").Points.Last.Label = "Wassertemperatur " & x.WasserTemp & " °C"
            'Debug.Print("Dosierung Wasser   " & x.StartWasser & "/" & x.FertigWasser)

            'Balken Temperatur-Messung
            chrtTTS.Series("Temp").Points.AddXY(h, x.StartTempMess, x.FertigTempMess)
            chrtTTS.Series("Temp").Points.Last.Label = x.sDiffTempMess
            chrtTTS.Series("Temp").Points.Last.ToolTip = x.sTooltip
            'Debug.Print("Temperatur-Messung " & x.StartTempMess & "/" & x.FertigTempMess)

        Next

    End Sub

    ''' <summary>
    ''' Lade alle Chargen-Daten zu dieser Rezeptnummer/RezeptVariante
    ''' Es werden nur die Datensätze geladen, die folgende Komponenten haben:
    '''     - Wasserdosierung
    '''     - Eis
    '''     - Teigtemperaturmessung
    '''     
    ''' </summary>
    ''' <param name="TWNr"></param>
    ''' <param name="RzNr"></param>
    ''' <param name="RzVr"></param>
    ''' <param name="Limit"></param>
    Private Sub MySqlLoadWbDaten(TWNr As Integer, RzNr As Integer, RzVr As Integer, Optional Limit As Integer = 0)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)

        'alle Datensätze eines Tages oder x Datensätze in der Zukunft/Vergangenheit
        Dim sqltw As String
        Select Case Limit
            Case > 1
                sqltw = "LIMIT "
            Case < -1
                sqltw = "LIMIT "
            Case Else
                sqltw = "B_ARZ_TW_Nr = " & TWNr.ToString
        End Select
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlChargenTTS, sqltw, RzNr, RzVr)

        'Datensätze aus Tabelle Rezeptschritte lesen
        If winback.sqlSelect(sql) Then
            Dim value As Object
            'Schleife über alle Datensätze
            While winback.Read

                'neuer Datensatz (ChargenNummer hat sich geändert)
                Dim ChargenNummer As Integer = winback.iField("B_ARZ_Charge_Nr")
                If _TTS.ChargenNummer <> ChargenNummer Then
                    If _TTS.ChargenNummer <> wb_Global.UNDEFINED Then
                        AddToList(_TTS)
                        _TTS = New wb_ChargenTTS
                    End If
                    _TTS.ChargenNummer = ChargenNummer
                End If

                For i = 0 To winback.MySqlRead.FieldCount - 1
                    'Felder mit Typ DateTime müssen speziell eingelesen werden
                    If winback.MySqlRead.GetFieldType(i).Name = "DateTime" Then
                        value = winback.MySqlRead.GetMySqlDateTime(i)
                    Else
                        value = winback.MySqlRead.GetValue(i)
                    End If
                    'Felder einlesen
                    MySQLdbRead_Fields(winback.MySqlRead.GetName(i), value)
                Next

                'Daten einsortieren (Start/Endezeit Soll/Istwerte)
                _TTS.ProcessData()

            End While

            'Den letzte Datensatz zum Array anfügen
            AddToList(_TTS)
        End If

        winback.Close()

    End Sub

    Private Function AddToList(TTS As wb_ChargenTTS) As Boolean
        If TTS.FertigTempMess <> wb_Global.wbNODATE And TTS.FertigWasser <> wb_Global.wbNODATE Then
            _TTSChargen.Add(TTS)
            Return True
        End If
        Return False
    End Function

    Private Function MySQLdbRead_Fields(Name As String, Value As Object)
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Debug
        'Debug.Print("BAK_ArbRezepte-BAK_ArbRZ_Schritte " & Name & " " & Value.ToString)

        'Feldname aus der Datenbank
        Try
            Select Case Name

                Case "B_ARS_Ko_Nr"
                    _TTS.KomponentenNr = wb_Functions.StrToInt(Value)
                'Komponententype
                Case "B_KT_Typ_Nr"
                    _TTS.KomponentenType = wb_Functions.IntToKomponType(wb_Functions.StrToInt(Value))
                'Parameter-Nummer
                Case "B_ARS_ParamNr"
                    _TTS.KomponentenParamNr = wb_Functions.StrToInt(Value)

                'Sollwert
                Case "B_ARS_Wert"
                    _TTS.ARS_Wert = Value
                Case "B_ARS_RS_Wert"
                    _TTS.ARS_RS_Wert = Value

                'Istwert
                Case "B_ARS_Istwert"
                    _TTS.Istwert = Value

                'Parameter
                Case "B_ARS_RS_Par1"
                    _TTS.Rs_Par1 = Value
                Case "B_ARS_RS_Par2"
                    _TTS.Rs_Par2 = Value
                Case "B_ARS_RS_Par3"
                    _TTS.Rs_Par3 = Value

                'Zeit
                Case "B_ARS_Gestartet"
                    _TTS.StartZeit = wb_sql_Functions.MySQLDateTimeToDate(Value)
                Case "B_ARS_Beendet"
                    _TTS.EndeZeit = wb_sql_Functions.MySQLDateTimeToDate(Value)

            End Select
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

End Class