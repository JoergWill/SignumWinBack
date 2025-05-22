Imports MySql.Data.MySqlClient

Public Class wb_SchnittstelleT1006
    Inherits wb_SchnittstelleTabelle

    Private _OptionSauerteig As Boolean

    Public T1006_Nummer As New wb_SchnittstelleFeld("T1006_RZ_Nr_AlNum", 1)                             'Rezeptnummer
    Public T1006_Variante As New wb_SchnittstelleFeld("T1006_RZ_Variante_Nr", 2)                        'Rezeptvariante
    Public T1006_Name As New wb_SchnittstelleFeld("T1006_RZ_Bezeichnung", 4, "", "STR")                 'Rezeptbezeichung
    Public T1006_Kommentar As New wb_SchnittstelleFeld("T1006_RZ_Kommentar", 5, "", "STR")              'Kommentar/Kurzname
    Public T1006_Matchcode As New wb_SchnittstelleFeld("T1006_RZ_Matchcode", 6, "", "STR")              'Matchcode
    Public T1006_Liniengrp As New wb_SchnittstelleFeld("T1006_RZ_Liniengruppe", 7, "1")                 'Liniengruppe
    Public T1006_VarName As New wb_SchnittstelleFeld("T1006_RZ_Variante", 8, "", "STR")                 'Rezeptvariante Name
    Public T1006_VHinweise As New wb_SchnittstelleFeld("T1006_RZ_Verarbeitungshinweise", 11, "", "TXT") 'Rezept-Verarbeitungshinweise
    Public T1006_Teigtemp As New wb_SchnittstelleFeld("T1006_RZ_Teigtemperatur", 12)                    'Teigtemperatur Rezeptkopf
    Public T1006_Gewicht As New wb_SchnittstelleFeld("T1006_RZ_Gewicht", 13)                            'Rezeptgewicht
    Public T1006_MinChargeKg As New wb_SchnittstelleFeld("T1006_RZ_Charge_Min")                         'Minimal-Charge in kg
    Public T1006_MaxChargeKg As New wb_SchnittstelleFeld("T1006_RZ_Charge_Max")                         'Maximal-Charge In kg
    Public T1006_OptChargeKg As New wb_SchnittstelleFeld("T1006_RZ_Charge_Opt")                         'Optimal-Charge in kg
    Public T1006_UniqueNo As New wb_SchnittstelleFeld("T1006_UniqueNo")                                 'Unique Number HS-Soft

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides ReadOnly Property TabName As String
        Get
            Return "T1006"
        End Get
    End Property

    Public Overrides ReadOnly Property sql As String
        Get
            If OptionSauerteig Then
                Return wb_sql_Selects.sqlT1006_ST
            Else
                Return wb_sql_Selects.sqlT1006
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property sqlCount As String
        Get
            If OptionSauerteig Then
                Return wb_sql_Selects.sqlT1006_ST_Count
            Else
                Return wb_sql_Selects.sqlT1006_Count
            End If
        End Get
    End Property

    Public Property OptionSauerteig As Boolean
        Get
            Return _OptionSauerteig
        End Get
        Set(value As Boolean)
            _OptionSauerteig = value
        End Set
    End Property

    Public Overrides Sub ImportWorker(winback As wb_Sql)

        'Log-Ausgabe
        If wb_Schnittstelle_Shared.DebugMode_Max Then
            Trace.WriteLine("Rezeptnummer           " & T1006_Nummer.ToString)
            Trace.WriteLine("Bezeichnung            " & T1006_Name.ToString)
            Trace.WriteLine("Liniengruppe           " & T1006_Liniengrp.ToString)
            Trace.WriteLine("Gewicht                " & T1006_Gewicht.ToString)
            Trace.WriteLine("MinChargeKg            " & T1006_MinChargeKg.ToString)
            Trace.WriteLine("MaxChargeKg            " & T1006_MaxChargeKg.ToString)
            Trace.WriteLine("OptChargeKg            " & T1006_OptChargeKg.ToString)
            Trace.WriteLine("Rezeptvariante         " & T1006_Variante.ToString)
            Trace.WriteLine("VarianteName           " & T1006_VarName.ToString)
            Trace.WriteLine("Verarbeitungs-Hinweise " & T1006_VHinweise.ToString)
            Trace.WriteLine("Teigtemperatur         " & T1006_Teigtemp.ToString)
            Trace.WriteLine("UniqueNumber           " & T1006_UniqueNo.ToString)
        End If

        'Prüfen ob schon ein Rezept mit dieser Nummer und dieser Rezept-Variante existiert
        Dim Sql As String = wb_sql_Selects.setParams(wb_sql_Selects.sqlRezeptNummer, T1006_Nummer.ToString, T1006_Variante.ToString)
        Dim Rz_Nr As Integer
        Dim RZ_Variante As String
        Dim RZ_AenderungNr As Integer

        'Datensätze aus Tabelle Rezepte lesen
        If winback.sqlSelect(Sql) Then
            If winback.Read Then
                'Index und (gefundene) Variante aus Datenbank lesen
                Rz_Nr = winback.iField("RZ_Nr")
                RZ_Variante = winback.sField("RZ_Variante_Nr")
                RZ_AenderungNr = winback.iField("RZ_Aenderung_Nr")
                winback.CloseRead()

                'wenn die Variante schon vorhanden ist
                If RZ_Variante = T1006_Variante.ToString Then
                    'Update Datensatz
                    SqlUpdate(Rz_Nr, RZ_Variante, RZ_AenderungNr, winback)
                Else
                    'Datensatz mit Index und neuer Variante anlegen
                    SqlInsert(Rz_Nr, RZ_Variante, winback)
                End If
            Else
                'Rezeptnummer nicht gefunden - Datensatz neu anlegen
                Rz_Nr = wb_sql_Functions.getNewRezeptNummer
                RZ_Variante = T1006_Variante.ToInt
                winback.CloseRead()

                'Variante muss 0-(Sauerteig) oder 1-(Hauptvariante) sein
                If RZ_Variante <= 1 Then
                    'Datensatz mit neuem Index anlegen
                    SqlInsert(Rz_Nr, RZ_Variante, winback)
                Else
                    'Fehler - Rezept ohne Stamm-Variante
                    Trace.WriteLine("@E_Rezept/Variante " & T1006_Nummer.ToString & "/" & T1006_Variante.ToString & " hat keine Standard-Variante(1) und kann nicht angelegt werden")
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' Rezeptkopf - Update in WinBack-Datenbank
    ''' </summary>
    ''' <param name="RezeptNr"></param>
    ''' <param name="RezeptVariante"></param>
    ''' <param name="winback"></param>
    Private Sub SqlUpdate(RezeptNr As Integer, RezeptVariante As Integer, AenderungNummer As Integer, winback As wb_Sql)
        'Ausgabe in Log-Fenster
        If wb_Schnittstelle_Shared.DebugMode_Max OrElse wb_Schnittstelle_Shared.Simulation Then
            Trace.WriteLine("Rezept/Variante geändert     " & T1006_Nummer.ToString & "/" & T1006_Variante.ToString & "/" & T1006_Name.ToString)
        End If

        If Not wb_Schnittstelle_Shared.Simulation Then
            'sql-Kommando UPDATE bilden
            Dim sqlData = "RZ_Nr_AlNum = '" & T1006_Nummer.ToString & "', RZ_Bezeichnung = '" & wb_Functions.Truncate(T1006_Name.ToString, 30) & "', " &
                      "RZ_Gewicht = '" & wb_Functions.FormatStr(T1006_Gewicht.ToString, 3) & "', " &
                      "RZ_Charge_Opt = '" & wb_Functions.FormatStr(T1006_OptChargeKg.ToString, 3) & "', " &
                      "RZ_Charge_Min = '" & wb_Functions.FormatStr(T1006_MinChargeKg.ToString, 3) & "', " &
                      "RZ_Charge_Max = '" & wb_Functions.FormatStr(T1006_MaxChargeKg.ToString, 3) & "', " &
                      "RZ_Liniengruppe = " & T1006_Liniengrp.ToString & ", RZ_Teigtemperatur = '" & T1006_Teigtemp.ToString & "' "
            '             "RZ_Aenderung_Datum = '" & wb_sql_Functions.MySQLdatetime(Date.Now) & "', " &
            '             "RZ_Aenderung_Name = '" & wb_GlobalSettings.AktUserName & "', RZ_Aenderung_User = " & wb_GlobalSettings.AktUserNr & ", RZ_Aenderung_Nr = " & AenderungNummer
            'Datensatz ändern
            winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlRezeptUpdate, RezeptNr, RezeptVariante, sqlData))
        End If
    End Sub

    ''' <summary>
    ''' Rezeptkopf - neu anlegen in WinBack-Datenbank
    ''' </summary>
    ''' <param name="RezeptNr"></param>
    ''' <param name="RezeptVariante"></param>
    ''' <param name="winback"></param>
    Private Sub SqlInsert(RezeptNr As Integer, RezeptVariante As Integer, winback As wb_Sql)
        'Ausgabe in Log-Fenster
        If wb_Schnittstelle_Shared.DebugMode_Max OrElse wb_Schnittstelle_Shared.Simulation Then
            Trace.WriteLine("Rezept/Variante neu angelegt " & T1006_Nummer.ToString & "/" & T1006_Variante.ToString & "/" & T1006_Name.ToString)
        End If

        If Not wb_Schnittstelle_Shared.Simulation Then
            'sql-Kommando INSERT bilden
            Dim sqlFeld = "RZ_Nr, RZ_Variante_Nr, RZ_Nr_AlNum, RZ_Bezeichnung, RZ_Gewicht, RZ_Charge_Opt, RZ_Charge_Min, RZ_Charge_Max, RZ_Aenderung_Datum," &
                      "RZ_Aenderung_Name, RZ_Aenderung_User, RZ_Aenderung_Nr, RZ_Teigtemperatur, RZ_Liniengruppe"
            Dim sqlData = RezeptNr & "," & RezeptVariante & ", '" & T1006_Nummer.ToString & "', '" & T1006_Name.ToString & "','" & T1006_Gewicht.ToString & "','" &
                          T1006_OptChargeKg.ToString & "','" & T1006_MinChargeKg.ToString & "','" & T1006_MaxChargeKg.ToString & "','" & wb_sql_Functions.MySQLdatetime(Date.Now) & "','" &
                          "Import von " & wb_GlobalSettings.AktUserName & "'," & wb_GlobalSettings.AktUserNr & ",0," & T1006_Teigtemp.ToString & "," & T1006_Liniengrp.ToString

            'Datensatz neu anlegen
            winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlAddNewRezept, sqlFeld, sqlData))
        End If
    End Sub

    ''' <summary>
    ''' In BuildExportString werden alle Datenfelder aus der Datenbank-Tabelle abhängig vom DBIndex (aus der Schnittstellen-Konfig)
    ''' in die jeweiligen Felder einsortiert. 
    ''' Anschliessend werden die Felder nacheinander (sortiert nach Idx) ausgegeben und in die Export-Datei geschrieben
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <param name="Writer"></param>
    Public Overrides Sub ExportWorker(ByRef sqlReader As MySqlDataReader, Writer As System.IO.StreamWriter)
        'Datensatz in Export-File schreiben
        BuildExportString("T1006", sqlReader, Writer)
    End Sub


    'CREATE TABLE Rezepte (
    '	 RZ_Nr                      INT(10),
    '	 RZ_Variante_Nr             SMALLINT(3),
    '	 RZ_Nr_AlNum                VARCHAR(8),
    '	 RZ_Bezeichnung             VARCHAR(60),
    '	 RZ_Gewicht                 VARCHAR(30),
    '	 RZ_Kommentar               VARCHAR(30),
    '	 RZ_Kurzname                VARCHAR(16),
    '	 RZ_Matchcode               VARCHAR(10),
    '	 RZ_Type                    CHAR(1),
    '	 RZ_Charge_Opt              VARCHAR(30),
    '	 RZ_Charge_Min              VARCHAR(30),
    '	 RZ_Charge_Max              VARCHAR(30),
    '	 RZ_Aenderung_Datum         DATETIME,
    '	 RZ_Aenderung_User          INT(10),
    '	 RZ_Aenderung_Name          VARCHAR(24),
    '	 RZ_Aenderung_Nr            SMALLINT(5),
    '	 RZ_Teigtemperatur          VARCHAR(10),
    '	 RZ_Kneterkennlinie         SMALLINT(5),
    '	 RZ_Verarbeitungshinweise   VARCHAR(100),
    '	 RZ_Liniengruppe            TINYINT(3),
    '	 RZ_Gruppe                  INT(10),
    '	 KA_Gruppe                  INT(10),
    '	 RZ_Timestamp               TIMESTAMP
    ')
End Class
