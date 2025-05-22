Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Chargen_ChartVTS
    Inherits DockContent
    Const MaxChart = 10

    Private _ChargenZeile As wb_ChargenSchritt
    Private _Messwert As wb_SauerteigMesswert
    Private _Ablauf As wb_SauerteigAblauf

    Private _VTS_Messwerte As New ArrayList
    Private _VTS_Ablauf As New ArrayList

    Public WriteOnly Property ChargenZeile As wb_ChargenSchritt
        Set(value As wb_ChargenSchritt)
            _ChargenZeile = value
        End Set
    End Property

    Private Sub wb_Chargen_ChartVTS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Tageswechsel-Nummer
        Dim TWNr As Integer = wb_Chargen_Shared.Liste_TagesWechselNummer
        'Behälter-Nummer
        Dim BehNr As Integer = _ChargenZeile.Linie + 100
        'Kanal-Nummer
        'TODO aus Tabelle Messreihen ermitteln
        Dim Kanal As Integer = _ChargenZeile.Linie + 100

        'Start/Endezeit Rezept(Sauerteig)
        Dim RzStartzeit As Date = _ChargenZeile.RezeptStartZeit
        Dim RzEndezeit As Date = _ChargenZeile.RezeptEndeZeit
        'Start/Endezeit in Unix-Timestamp umwandeln
        Dim UnixRzStartZeit As String = wb_Functions.TimeToUnix(RzStartzeit)
        Dim UnixRzEndeZeit As String = wb_Functions.TimeToUnix(RzEndezeit)

        'Temperatur-Messungen aus winback.Messungen laden
        MySqlLoadMessungen(UnixRzStartZeit, UnixRzEndeZeit)
        'Sauerteig-Rezeptfolge aus wbdaten.BAK_ArbRZSchritte laden
        MySqlLoadAblauf(TWNr, BehNr)

        'Diagramm anzeigen
        ShowTTSDiagramm(0)

    End Sub

    Private Sub ShowTTSDiagramm(Kanal As Integer)

        'Chart Sauerteig - Temperaturverlauf über Zeit
        chrtVTS.Series.Add("Temp")
        chrtVTS.Series("Temp").ChartType = DataVisualization.Charting.SeriesChartType.Line
        chrtVTS.Series("Temp").BorderWidth = 2
        chrtVTS.Series("Temp").YValuesPerPoint = 1
        chrtVTS.Series.Add("Mehl")
        chrtVTS.Series("Mehl").ChartType = DataVisualization.Charting.SeriesChartType.Line
        chrtVTS.Series("Mehl").BorderWidth = 15
        chrtVTS.Series("Mehl").YValuesPerPoint = 1
        chrtVTS.Series.Add("Wasser")
        chrtVTS.Series("Wasser").ChartType = DataVisualization.Charting.SeriesChartType.Line
        chrtVTS.Series("Wasser").BorderWidth = 15
        chrtVTS.Series("Wasser").YValuesPerPoint = 1
        chrtVTS.Series.Add("Kühlen")
        chrtVTS.Series("Kühlen").ChartType = DataVisualization.Charting.SeriesChartType.Line
        chrtVTS.Series("Kühlen").BorderWidth = 15
        chrtVTS.Series("Kühlen").YValuesPerPoint = 1
        chrtVTS.Series.Add("Rühren")
        chrtVTS.Series("Rühren").ChartType = DataVisualization.Charting.SeriesChartType.Line
        chrtVTS.Series("Rühren").BorderWidth = 15
        chrtVTS.Series("Rühren").YValuesPerPoint = 1

        'alle Einträge im Array
        For Each x As wb_SauerteigMesswert In _VTS_Messwerte
            If x.Id = Kanal Then
                chrtVTS.Series("Temp").Points.AddXY(x.TimeStamp, x.Wert)
            End If
        Next

        'Balken Sauerteig-Ablauf
        For Each x As wb_SauerteigAblauf In _VTS_Ablauf
            If x.StartTime > wb_Global.wbNODATE Then
                Select Case x.KomponType
                    Case wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL
                        chrtVTS.Series("Mehl").Points.AddXY(x.StartTime, 8)
                        chrtVTS.Series("Mehl").Points.AddXY(x.EndTime, 8)
                    Case wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER
                        chrtVTS.Series("Wasser").Points.AddXY(x.StartTime, 6)
                        chrtVTS.Series("Wasser").Points.AddXY(x.EndTime, 6)
                    Case wb_Global.KomponTypen.KO_TYPE_SAUER_RUEHREN
                        chrtVTS.Series("Rühren").Points.AddXY(x.StartTime, 4)
                        chrtVTS.Series("Rühren").Points.AddXY(x.EndTime, 4)
                    Case wb_Global.KomponTypen.KO_TYPE_SAUER_TEMP
                        chrtVTS.Series("Kühlen").Points.AddXY(x.StartTime, 2)
                        chrtVTS.Series("Kühlen").Points.AddXY(x.EndTime, 2)
                End Select
            End If
        Next

        chrtVTS.Series("Mehl").Points.First.Label = "Mehl"
        chrtVTS.Series("Wasser").Points.First.Label = "Wasser"
        chrtVTS.Series("Rühren").Points.First.Label = "Rühren"
        chrtVTS.Series("Kühlen").Points.First.Label = "Temperieren"

        'Dim h As Integer = 0
        'Dim TempFertig As Date = Now
        '    Höhe im Chart
        '    h += 1

        '    maximale Höhe im Chart erreicht
        '    If (h > MaxChart) And (x.FertigTempMess > TempFertig) Then
        '        h = 1
        '    End If

        '    Fertig-Zeitpunkt der ersten Charge merken
        '    If h = 1 Then
        '        TempFertig = x.FertigTempMess
        '    End If

        '    Balken Wasser - Dosierung
        '    chrtTTS.Series("Teig").Points.Last.Label = "Wassertemperatur " & x.WasserTemp & " °C"
        '    Debug.Print("Dosierung Wasser   " & x.StartWasser & "/" & x.FertigWasser)

        '    Balken Temperatur - Messung
        '    chrtTTS.Series("Temp").Points.AddXY(h, x.StartTempMess, x.FertigTempMess)
        '    chrtTTS.Series("Temp").Points.Last.Label = x.sDiffTempMess
        '    chrtTTS.Series("Temp").Points.Last.ToolTip = x.sTooltip
        '    Debug.Print("Temperatur-Messung " & x.StartTempMess & "/" & x.FertigTempMess)

        'Next

    End Sub

    ''' <summary>
    ''' Lade alle (Temperatur)Messungen aus allen Kanälen begrenzt durch Start/Ende-Zeit
    ''' </summary>
    ''' <param name="UnixStartZeit"></param>
    ''' <param name="UnixEndeZeit"></param>
    Private Sub MySqlLoadMessungen(UnixStartZeit As String, UnixEndeZeit As String)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'alle Datensätze aus winback.Messungen für diesen Zeitraum einlesen
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlVTSMessungen, UnixStartZeit, UnixEndeZeit)
        'Liste löschen
        _VTS_Messwerte.Clear()

        ''Datensätze aus Tabelle Messungen lesen
        If winback.sqlSelect(sql) Then
            Dim value As Object
            'Schleife über alle Datensätze
            While winback.Read
                'Speicher für Messwerte/Datum-Zeit
                _Messwert = New wb_SauerteigMesswert

                'alle Datenfelder
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
                _VTS_Messwerte.Add(_Messwert)
            End While

            'Den letzte Datensatz zum Array anfügen
            _VTS_Messwerte.Add(_Messwert)
        End If

        'Datenbank wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Lade alle Ablaufdaten zum Behälter mit dieser Tageswechsel-Nummer
    ''' </summary>
    ''' <param name="TWNr"></param>
    ''' <param name="BehNr"></param>
    Private Sub MySqlLoadAblauf(TWNr As Integer, BehNr As Integer)
        'Datenbank-Verbindung öffnen - MySQL
        Dim wbdaten = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)

        'alle Datensätze aus winback.Messungen für diesen Zeitraum einlesen
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlChargenDetails, TWNr, TWNr, "B_ARS_RunIdx", "B_ARZ_LiBeh_Nr = " & BehNr.ToString)
        'Liste löschen
        _VTS_Ablauf.Clear()

        ''Datensätze aus Tabelle Messungen lesen
        If wbdaten.sqlSelect(sql) Then
            Dim value As Object
            'Schleife über alle Datensätze
            While wbdaten.Read
                'Speicher für Messwerte/Datum-Zeit
                _Ablauf = New wb_SauerteigAblauf
                _Ablauf.BehNr = BehNr

                'alle Datenfelder
                For i = 0 To wbdaten.MySqlRead.FieldCount - 1
                    'Felder mit Typ DateTime müssen speziell eingelesen werden
                    If wbdaten.MySqlRead.GetFieldType(i).Name = "DateTime" Then
                        value = wbdaten.MySqlRead.GetMySqlDateTime(i)
                    Else
                        value = wbdaten.MySqlRead.GetValue(i)
                    End If
                    'Felder einlesen
                    MySQLdbRead_Fields(wbdaten.MySqlRead.GetName(i), value)
                Next

                'Daten einsortieren (Start/Endezeit Soll/Istwerte)
                _VTS_Ablauf.Add(_Ablauf)
            End While

            'Den letzte Datensatz zum Array anfügen
            _VTS_Ablauf.Add(_Ablauf)
        End If

        'Datenbank wieder schliessen
        wbdaten.Close()
    End Sub


    Private Function MySQLdbRead_Fields(Name As String, Value As Object)
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Debug
        'Debug.Print("Messungen " & Name & " " & Value.ToString)

        'Feldname aus der Datenbank
        Try
            Select Case Name

                'id (Kanal Messung)
                Case "id"
                    _Messwert.Id = Value
                'Timestamp (Format Unix)
                Case "timestamp"
                    _Messwert.UnixTimestamp = Value
                'Messwert
                Case "wert"
                    _Messwert.Wert = wb_Functions.StrToDouble(Value)

                'Start/Ende-Zeit Komponente
                Case "B_ARS_Gestartet"
                    _Ablauf.StartTime = wb_sql_Functions.MySQLDateTimeToDate(Value)
                Case "B_ARS_Beendet"
                    _Ablauf.EndTime = wb_sql_Functions.MySQLDateTimeToDate(Value)
                'Komponenten-Type
                Case "B_KT_Typ_Nr"
                    _Ablauf.KomponType = wb_Functions.IntToKomponType(Value)

            End Select
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

End Class