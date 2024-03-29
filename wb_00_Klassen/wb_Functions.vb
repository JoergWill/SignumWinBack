﻿Imports System.Globalization
Imports System.IO
Imports System.Management
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text
Imports System.Windows.Forms
Imports EnhEdit.EnhEdit_Global
Imports ICSharpCode.SharpZipLib.BZip2
Imports Microsoft.Win32

''' <summary>
''' Beschreibung:
''' Sammlung von Statischen Funktionen
''' </summary>

Public Class wb_Functions

    Private Shared Encoding_iso8859_1 As Encoding = Encoding.GetEncoding("iso-8859-1")
    Private Shared Encoding_iso8859_5 As Encoding = Encoding.GetEncoding("iso-8859-5")
    Private Shared Encoding_iso8859_15 As Encoding = Encoding.GetEncoding("iso-8859-15")

    ''' <summary>
    ''' Erzeugt einen String aus Key-Down-Ereignissen
    ''' alle gültigen Zeichen werden an den String angehängt,
    ''' ungültige und Steuerzeichen werden mit False zurück-
    ''' gegeben (KeyDown-Handler = False)
    ''' </summary>
    ''' <param name="KeyCode"> - Char KeyCode der gedrückten Taste</param>
    ''' <param name="s">- String alle gültigen Zeichen aus KeyCode</param>
    ''' <returns>False wenn Steuerzeichen erkannt werden</returns>
    Public Shared Function KeyToString(KeyCode As Char, ByRef s As String) As Boolean
        Select Case Convert.ToUInt16(KeyCode)
                'normale Buchstaben
            Case 32, 33, 35 To 43, 45, 47, 64 To 93, 97 To 122, 129 To 154, 192 To 223, 228, 246, 252
                s = s + KeyCode.ToString
                Return True
                'Ziffern 0 bis 9
            Case 48 To 57
                s = s + KeyCode.ToString
                Return True
                'Backspace (Gibt True zurück wenn ein Zeichen gelöscht wurde)
            Case 8
                If s.Length > 0 Then
                    s = s.Remove(s.Length - 1)
                    Return True
                Else
                    Return False
                End If

                'alle anderen Zeichen sind nicht zulässig
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Wandelt einen String im WinBack-Cloud-Datumsformat (yyyymmddHHmmss) in DateTime um.
    ''' Wenn die Konvertierung feherhaft ist, wird der 22.11.1964 00:00:00 zurückgegeben.
    ''' </summary>
    ''' <param name="JSONTimeString"></param>
    ''' <returns></returns>
    Public Shared Function ConvertJSONTimeStringToDateTime(JSONTimeString As String) As DateTime
        Try
            Dim dt As String = JSONTimeString.Substring(0, 4) & JSONTimeString.Substring(6, 2) & JSONTimeString.Substring(4, 2)
            dt = dt & JSONTimeString.Substring(8, 6)
            Return DateTime.ParseExact(dt, "yyyyddMMHHmmss", Nothing)
        Catch ex As Exception
            Return #11/22/1964 00:00:00#
        End Try
    End Function

    ''' <summary>
    ''' Wandelt die Datum/Uhrzeit-Angabe aus DataLink (Created 2013-09-03T08:14:03)
    ''' in DateTime um
    ''' </summary>
    ''' <param name="DataLinkTimeString"></param>
    ''' <returns></returns>
    Public Shared Function ConvertDataLinkTimeStringToDateTime(DataLinkTimeString As String) As DateTime
        Try
            Dim dt As String = DataLinkTimeString.Substring(0, 10) & DataLinkTimeString.Substring(11, 8)
            Return DateTime.ParseExact(dt, "yyyy-dd-MMHH:mm:ss", Nothing)
        Catch ex As Exception
            Return #11/22/1964 00:00:00#
        End Try
    End Function

    ''' <summary>
    ''' Gibt True zurück, wenn die übergebene ID eine Datenlink-ID ist,
    ''' sonst False
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Shared Function IsDatenLinkID(Id As String) As Boolean
        If Strings.Left(Id, 3) = "DL-" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function ConvertUSDateStringToDate(us As String) As Date
        Try
            Return DateTime.ParseExact(us, "yyyyMMdd", Nothing)
        Catch ex As Exception
            Return #11/22/1964 00:00:00#
        End Try
    End Function

    ''' <summary>
    ''' Addiert zwei Datum/Zeitwerte und gibt das Ergebnis als DateTime zurück.
    ''' Ist der zu addierende Wert nur ein Zeitwert (Year=1, Month=1, Day=1) wird
    ''' nur der Zeitwert addiert.
    ''' </summary>
    ''' <param name="d1"></param>
    ''' <param name="d2"></param>
    ''' <returns></returns>
    Public Shared Function AddDateTime(d1 As DateTime, d2 As DateTime, Optional IgnoreDatum As Boolean = False) As DateTime
        AddDateTime = d1
        Try
            AddDateTime = d1.AddSeconds(d2.Second).AddMinutes(d2.Minute).AddHours(d2.Hour)
            'Wenn die Daten für das Datum gültig sind
            If ((d2.Year <> 1) And (d2.Month <> 1) And (d2.Day <> 1) And Not IgnoreDatum) Then
                AddDateTime = AddDateTime.AddDays(d2.Day).AddMonths(d2.Month).AddYears(d2.Year)
            End If
        Catch ex As Exception
        End Try
    End Function

    ''' <summary>
    ''' Wandelt einen String in AllergenInfo um. Wenn der String umgültig ist wird ERR zurückgegeben
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Shared Function StringtoAllergen(s As String) As wb_Global.AllergenInfo
        Select Case s.ToUpper
            Case "C"
                Return wb_Global.AllergenInfo.C
            Case "K"
                Return wb_Global.AllergenInfo.K
            Case "N"
                Return wb_Global.AllergenInfo.N
            Case "T"
                Return wb_Global.AllergenInfo.T
            Case "X", ""
                Return wb_Global.AllergenInfo.X
            Case Else
                Return wb_Global.AllergenInfo.ERR
        End Select
    End Function

    ''' <summary>
    ''' Wandelt einen String in Ernährungsform um. Wenn der String umgültig ist wird ERR zurückgegeben
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Shared Function StringtoErnaehrungsForm(s As String) As wb_Global.ErnaehrungsForm
        Select Case s.ToUpper
            Case "N", "FALSE"
                Return wb_Global.ErnaehrungsForm.N
            Case "Y", "J", "TRUE"
                Return wb_Global.ErnaehrungsForm.Y
            Case "-", ""
                Return wb_Global.ErnaehrungsForm.X
            Case Else
                Return wb_Global.ErnaehrungsForm.ERR
        End Select
    End Function

    ''' <summary>
    ''' Wandelt die AllergenInfo in einen String um.
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Shared Function AllergenToString(s As String, Optional Nur_CT As Boolean = False) As String
        Select Case s
            Case wb_Global.AllergenInfo.C
                Return "C"
            Case wb_Global.AllergenInfo.K
                If Nur_CT Then
                    Return ""
                Else
                    Return "K"
                End If
            Case wb_Global.AllergenInfo.T
                Return "T"
            Case wb_Global.AllergenInfo.N, 0
                If Nur_CT Then
                    Return ""
                Else
                    Return "N"
                End If
            Case Else
                If Nur_CT Then
                    Return ""
                Else
                    Return "ERR"
                End If
        End Select
    End Function

    ''' <summary>
    ''' Wandelt die Ernährungsform-Information in einen String um.
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Shared Function ErnaehrungToString(s As String) As String
        Select Case s
            Case wb_Global.ErnaehrungsForm.Y
                Return "J"
            Case wb_Global.ErnaehrungsForm.N
                Return "N"
            Case wb_Global.ErnaehrungsForm.X, 0
                'TODO Prüfen !!! "-" ist nicht möglich - Konflikt OrgaBack-DB
                'Return "-"
                Return "N"
            Case Else
                Return "ERR"
        End Select
    End Function

    Public Shared Function ModusChargenTeilerToString(c As wb_Global.ModusChargenTeiler) As String
        Select Case c
            Case wb_Global.ModusChargenTeiler.XGleiche            'Aufteilung in gleich große Chargen
                Return "Gleich große Chargen"
            Case wb_Global.ModusChargenTeiler.NurOptimal          'Aufteilung nur in Optimal-Chargen
                Return "Nur Optimalchargen"
            Case wb_Global.ModusChargenTeiler.OptimalUndRest      'Aufteilung in Optimal- und Rest-Chargen
                Return "Optimalchargen und Rest"
            Case wb_Global.ModusChargenTeiler.MaximalUndRest      'Aufteilung in Maximal- und Rest-Chargen
                Return "Maximalchargen und Rest"
            Case wb_Global.ModusChargenTeiler.RezeptGroesse       'Aufteilung in Rezept-Größe (keine Chargen angegeben)
                Return "Rezeptgröße"
            Case Else
                Return ""
        End Select
    End Function
    Enum ModusTeigOptimierung
        NurTeigeKleinerMinChargen
        AlleTeige
        AlleTeigeAlleTouren
    End Enum

    Public Shared Function ModusTeigOptimierungToString(c As wb_Global.ModusChargenTeiler) As String
        Select Case c
            Case wb_Global.ModusTeigOptimierung.NurTeigeKleinerMinChargen   'alle Teige kleiner als Min-Charge zusammenfassen
                Return "alle Teige kleiner als Minimumcharge"
            Case wb_Global.ModusTeigOptimierung.AlleTeige                   'alle Teige pro Tour zusammenfassen
                Return "alle Teige pro Tour"
            Case wb_Global.ModusTeigOptimierung.AlleTeigeAlleTouren         'alle Teige zusammenfassen unabhängig von Tour
                Return "alle Teige alle Touren"
            Case Else
                Return ""
        End Select
    End Function

    ''' <summary>
    ''' Wandelt LogType in String
    ''' </summary>
    ''' <param name="LogType"></param>
    ''' <returns></returns>
    Public Shared Function LogTypeToString(LogType As wb_Global.LogType) As String
        Select Case LogType
            Case wb_Global.LogType.X     'Unbestimmt
                Return "XXX"
            Case wb_Global.LogType.Stm   'Stammdaten
                Return "Stm"
            Case wb_Global.LogType.Prm   'Parameter
                Return "Prm"
            Case wb_Global.LogType.Alg   'Allergen
                Return "Alg"
            Case wb_Global.LogType.Nrw   'Nährwert
                Return "Nrw"
            Case Else
                Return "xxx"
        End Select
    End Function

    ''' <summary>
    ''' Wandelt einen Integer-Wert in einen Komponenten-Typ um. Wenn der Integer-Wert ungültig ist,
    ''' wird KO_TYPE_UNDEFINED zurückgegeben
    ''' </summary>
    ''' <param name="KO_Type"></param>
    ''' <returns></returns>
    Public Shared Function IntToKomponType(KO_Type As Integer) As wb_Global.KomponTypen
        Select Case KO_Type
            Case -1
                Return wb_Global.KomponTypen.KO_ZEILE_ARTIKEL
            Case -2
                Return wb_Global.KomponTypen.KO_ZEILE_REZEPT
            Case -3
                Return wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE
            Case 0
                Return wb_Global.KomponTypen.KO_TYPE_ARTIKEL

            Case 101
                Return wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE
            Case 102
                Return wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE
            Case 103
                Return wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
            Case 104
                Return wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE
            Case 105
                Return wb_Global.KomponTypen.KO_TYPE_STUECK
            Case 106
                Return wb_Global.KomponTypen.KO_TYPE_METER

            Case 111
                Return wb_Global.KomponTypen.KO_TYPE_TEMPERATURERFASSUNG
            Case 118
                Return wb_Global.KomponTypen.KO_TYPE_KNETER
            Case 119
                Return wb_Global.KomponTypen.KO_TYPE_TEIGZETTEL
            Case 128
                Return wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT
            Case 129
                Return wb_Global.KomponTypen.KO_TYPE_KNETER_TEIGRUHE

            Case 121
                Return wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE
            Case 122
                Return wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE
            Case 123
                Return wb_Global.KomponTypen.KO_TYPE_KESSEL

            Case 1
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL
            Case 3
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER
            Case 4
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_TEMP
            Case 10
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_DIGITAL
            Case 11
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_ANALOG
            Case 16
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_WARTEN
            Case 17
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_RUEHREN
            Case 19
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_ZUGABE
            Case 20
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_STATUS
            Case 21
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_TEXT
            Case 22
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE
            Case 30
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_REZEPT_START
            Case 31
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_REPEAT

            Case Else
                Return wb_Global.KomponTypen.KO_TYPE_UNDEFINED
        End Select
    End Function

    Public Shared Function KomponTypeToInt(KO_Type As wb_Global.KomponTypen) As Integer
        Select Case KO_Type
            Case wb_Global.KomponTypen.KO_ZEILE_ARTIKEL
                Return -1
            Case wb_Global.KomponTypen.KO_ZEILE_REZEPT
                Return -2
            Case wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE
                Return -3
            Case wb_Global.KomponTypen.KO_TYPE_ARTIKEL
                Return 0

            Case wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE
                Return 101
            Case wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE
                Return 102
            Case wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
                Return 103
            Case wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE
                Return 104
            Case wb_Global.KomponTypen.KO_TYPE_STUECK
                Return 105
            Case wb_Global.KomponTypen.KO_TYPE_METER
                Return 106

            Case wb_Global.KomponTypen.KO_TYPE_TEMPERATURERFASSUNG
                Return 111
            Case wb_Global.KomponTypen.KO_TYPE_KNETER
                Return 118
            Case wb_Global.KomponTypen.KO_TYPE_TEIGZETTEL
                Return 119
            Case wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT
                Return 128
            Case wb_Global.KomponTypen.KO_TYPE_KNETER_TEIGRUHE
                Return 129

            Case wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE
                Return 121
            Case wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE
                Return 122
            Case wb_Global.KomponTypen.KO_TYPE_KESSEL
                Return 123

            Case wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL
                Return 1
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER
                Return 3
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_TEMP
                Return 4
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_DIGITAL
                Return 10
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_ANALOG
                Return 11
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_WARTEN
                Return 16
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_RUEHREN
                Return 17
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_ZUGABE
                Return 19
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_STATUS
                Return 20
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_TEXT
                Return 21
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE
                Return 22
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_REZEPT_START
                Return 30
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_REPEAT
                Return 31

            Case wb_Global.KomponTypen.KO_TYPE_UNDEFINED
                Return -99
            Case Else
                Return -99
        End Select
    End Function

    Public Shared Function StrToFormat(value As String) As wb_Format
        Select Case value
            Case "1"
                Return wb_Format.fString
            Case "2"
                Return wb_Format.fInteger
            Case "3"
                Return wb_Format.fReal
            Case "4"
                Return wb_Format.fTime
            Case "5"
                Return wb_Format.fBoolean
            Case "6"
                Return wb_Format.fAllergen
            Case "7"
                Return wb_Format.fYesNo
            Case Else
                Return wb_Format.FUndefined
        End Select
    End Function

    Public Shared Function IntToProduktionsTyp(i As Integer) As wb_Global.wbSatzTyp
        Select Case i
            Case 1
                Return wb_Global.wbSatzTyp.Rezept
            Case 2
                Return wb_Global.wbSatzTyp.Artikel
            Case Else
                Return wb_Global.wbSatzTyp.UnDefined
        End Select
    End Function

    ''' <summary>
    ''' Ermittelt, ob Typ und Parameter-Nummer einen Gewichts-relevanten Sollwert enthalten.
    '''     Automatik-Komponenten (Produktion und Sauerteig)
    '''     Hand-Verwiegung 
    '''     Wasser-Sollmenge (Produktion und Sauerteig)
    '''     Anstellgut Sauerteig
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <param name="Param"></param>
    ''' <returns>Boolean - True wenn der Typ einen Sollwert enthält, False wenn der Typ keinen Sollwert (Gewicht/Menge/Länge) enthält, der umgerechnet werden muss/kann</returns>
    Public Shared Function TypeIstSollMenge(Type As wb_Global.KomponTypen, Param As Integer) As Boolean
        If Type = wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE Or
           Type = wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE Or
           Type = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE Or
           Type = wb_Global.KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE Or
           Type = wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL Or
           Type = wb_Global.KomponTypen.KO_TYPE_SAUER_ZUGABE Or
           Type = wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE And Param = 1 Or
           Type = wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER And Param = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Ermittelt ob Type und Parameter-Nummer einen (anderen)Sollwert enthalten. (Keinen Gewichtswert!)
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <param name="Param"></param>
    ''' <returns></returns>
    Public Shared Function TypeIstSollWert(Type As wb_Global.KomponTypen, Param As Integer) As Boolean
        If Type = wb_Global.KomponTypen.KO_TYPE_TEMPERATURERFASSUNG Or
           Type = wb_Global.KomponTypen.KO_TYPE_KNETER Or
           Type = wb_Global.KomponTypen.KO_TYPE_TEIGZETTEL Or
           Type = wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE And Param = 3 Or
           Type = wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER And Param = 3 Then
            Return True
        Else
            Return False
        End If
    End Function


    ''' <summary>
    ''' Ermittelt ob Rohstoffe/Artikel mit dieser Komponenten-Type Nährwert-Informationen haben können
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    Public Shared Function TypeHatNwt(Type As wb_Global.KomponTypen) As Boolean
        If Type = wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE Or
           Type = wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE Or
           Type = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE Or
           Type = wb_Global.KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE Or
           Type = wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL Or
           Type = wb_Global.KomponTypen.KO_TYPE_SAUER_ZUGABE Or
           Type = wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE Or
           Type = wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Ermittelt ob Type und Parameter-Nummer einen Text als Sollwert enthalten.
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    Public Shared Function TypeIstText(Type As wb_Global.KomponTypen) As Boolean
        If Type = wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE Or
           Type = wb_Global.KomponTypen.KO_TYPE_KESSEL Or
           Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Or
           Type = wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Ermittelt ob Type und Parameter-Nummer einen Wasser-Mengen-Sollwert enthalten.
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    Public Shared Function TypeIstWasserSollmenge(Type As wb_Global.KomponTypen, Param As Integer, TA As Integer) As Boolean
        If (Param = 1) And ((TA = wb_Global.TA_Wasser) Or
            (Type = wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE) Or
            (Type = wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE) Or
            (Type = wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER)) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Ermittelt anhand der Komponenten-Type ob eine Einheit(Text) ausgeben werden soll.
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    Public Shared Function TypeHatEinheit(Type As wb_Global.KomponTypen) As Boolean
        If Type = wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE Or
            Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Or
            Type = wb_Global.KomponTypen.KO_TYPE_KESSEL Or
            Type = wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Ermittelt anhand der Komponenten-Type und der Komponenten-Nr ob die Komponente
    ''' einen Teigtemperatur-Sollwert enthält
    ''' 
    '''     Teigtemperaturmessung   KT111
    '''     Kneterkomponente        KT118 - Einheit (ParamNr.4) = 5 (Grad Celsius)
    ''' </summary>
    ''' <param name="KompNr"></param>
    ''' <returns></returns>
    Public Shared Function TypeIstTeigTemperaturSollwert(KompNr As Integer) As Boolean
        If wb_Rohstoffe_Shared.TeigTempRohstoffe.Contains(KompNr) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Ermittelt anhand der Komponenten-Type ob Child-Steps vorhanden sind, oder ob beim Einfügen in
    ''' Rezepturen mehrere Zeilen erforderlich sind.
    ''' (Wasser/Eis/Kneter/Produktions-Stufe/Kessel)
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    Public Shared Function TypeHasChildSteps(Type As wb_Global.KomponTypen) As Boolean
        If Type = wb_Global.KomponTypen.KO_TYPE_KESSEL Or
           Type = wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE Or
           Type = wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE Or
           Type = wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER Or
           Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Or
           Type = wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Wandelt einen String in kt301Gruppen um. Beim Einlesen der Hash-Table aus der Tabelle KomponTypen
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Shared Function StringTokt301Gruppe(s As String) As wb_Global.ktTyp301Gruppen
        Select Case s.ToLower
            Case "big 4"
                Return wb_Global.ktTyp301Gruppen.Big4
            Case "big 8"
                Return wb_Global.ktTyp301Gruppen.Big8
            Case "vitamine"
                Return wb_Global.ktTyp301Gruppen.Vitamine
            Case "kohlenhydratzusammen"
                Return wb_Global.ktTyp301Gruppen.Kohlenhydrate
            Case "mineralstoffe"
                Return wb_Global.ktTyp301Gruppen.Mineralstoffe
            Case "spurenelemente"
                Return wb_Global.ktTyp301Gruppen.SpurenElemente
            Case "allergene"
                Return wb_Global.ktTyp301Gruppen.Allergene
            Case "gluten"
                Return wb_Global.ktTyp301Gruppen.Gluten
            Case "schalenfrüchte"
                Return wb_Global.ktTyp301Gruppen.Schalenfrüchte
            Case "gesamtkennzahlen"
                Return wb_Global.ktTyp301Gruppen.Gesamtkennzahlen
            Case "ernährungsformen"
                Return wb_Global.ktTyp301Gruppen.ErnaehrungsFormen
            Case Else
                Return wb_Global.ktTyp301Gruppen.xxx
        End Select
    End Function

    ''' <summary>
    ''' Wandelt eine kt301Gruppe in einen String um. (AnzeigeText)
    ''' </summary>
    ''' <param name="k"></param>
    ''' <returns></returns>
    Public Shared Function kt301GruppeToString(k As wb_Global.ktTyp301Gruppen) As String
        Select Case k
            Case wb_Global.ktTyp301Gruppen.Big4
                Return "Big 4"
            Case wb_Global.ktTyp301Gruppen.Big8
                Return "Big 8"
            Case wb_Global.ktTyp301Gruppen.Vitamine
                Return "Vitamine"
            Case wb_Global.ktTyp301Gruppen.Kohlenhydrate
                Return "Kohlenhydrate"
            Case wb_Global.ktTyp301Gruppen.Mineralstoffe
                Return "Mineralstoffe"
            Case wb_Global.ktTyp301Gruppen.SpurenElemente
                Return "Spurenelemente"
            Case wb_Global.ktTyp301Gruppen.Allergene
                Return "Allergene"
            Case wb_Global.ktTyp301Gruppen.Gluten
                Return "Gluten"
            Case wb_Global.ktTyp301Gruppen.Schalenfrüchte
                Return "Schalenfrüchte"
            Case wb_Global.ktTyp301Gruppen.Gesamtkennzahlen
                Return "Gesamt"
            Case wb_Global.ktTyp301Gruppen.ErnaehrungsFormen
                Return "Ernährung"
            Case Else
                Return "Undefiniert"
        End Select
    End Function

    ''' <summary>
    ''' Wandelt die Artikelgruppe aus OrgaBack in die WinBack-Komponenten-Type um.
    ''' Die Artikelgruppen in OrgaBack sind frei definiert. Die Zuordnung von Artikelgruppe zu Komponenten-Type wird in winback.ini festgelegt
    ''' (Stammdaten-Gruppen-Artikelgruppen)
    '''     [OrgaBack].[dbo].[Artikelgruppe]
    ''' </summary>
    ''' <param name="obKType"></param>
    ''' <returns></returns>
    Friend Shared Function obKtypeToKType(obKType As String) As wb_Global.KomponTypen

        If obKType = wb_GlobalSettings.OsGrpBackwaren Then
            Return wb_Global.KomponTypen.KO_TYPE_ARTIKEL

        ElseIf obKType = wb_GlobalSettings.OsGrpRohstoffe Then
            Return wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE

        Else
            Return wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE
        End If
    End Function

    ''' <summary>
    ''' Wandelt die DatenLink-Nährwert und Allergen-Bezeichnungen in 
    ''' WinBack-Index-Nummern um
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns>index (Integer)</returns>
    Public Shared Function DatenLinkToIndex(name As String) As Integer
        Select Case name

            'Nährwerte
            Case "GCAL"
                Return 1
            Case "GJ"
                Return 2
            Case "ZE"
                Return 3
            Case "ZK"
                Return 4
            Case "ZF"
                Return 5
            Case "KD"
                Return 11
            Case "FS"
                Return 12
            Case "ZB"
                Return 13
            Case "GMKO"
                Return 202

            'Allergene
            Case "GLUTEN"
                Return 141
            Case "GLUTEN_WHEAT"
                Return 170
            Case "GLUTEN_RYE"
                Return 171
            Case "GLUTEN_BARLEY"
                Return 172
            Case "GLUTEN_SPELT"
                Return 173
            Case "GLUTEN_KAMUT"
                Return 174
            Case "GLUTEN_OAT"
                Return 175
            Case "CRUSTACEANS"
                Return 142
            Case "EGGS"
                Return 143
            Case "SEAFOOD"
                Return 144
            Case "PEANUTS"
                Return 145
            Case "SOY"
                Return 146
            Case "MILK"
                Return 147
            Case "EDIBLE_NUTS"
                Return 148
            Case "EDIBLE_NUTS_ALMONDS"
                Return 180
            Case "EDIBLE_NUTS_HAZELNUTS"
                Return 181
            Case "EDIBLE_NUTS_WALNUTS"
                Return 182
            Case "EDIBLE_NUTS_CASHEW"
                Return 183
            Case "EDIBLE_NUTS_PECANNUTS"
                Return 184
            Case "EDIBLE_NUTS_BRASIL_NUTS"
                Return 185
            Case "EDIBLE_NUTS_PISTACIOS"
                Return 186
            Case "EDIBLE_NUTS_MACADAMIA_NUTS"
                Return 187
            Case "CELERY"
                Return 149
            Case "MUSTARD"
                Return 150
            Case "SESAME"
                Return 151
            Case "SULFOR_DIOXIDE_SULFITE"
                Return 152
            Case "LUPINES"
                Return 153
            Case "MOLLUSCS"
                Return 154

            Case Else
                Return -1
        End Select
    End Function

    ''' <summary>
    ''' Wandelt die Pistor-Nährwert und Allergen-Bezeichnungen/Nummern in 
    ''' WinBack-Index-Nummern um.
    ''' </summary>
    ''' <param name="PistorNr"></param>
    ''' <returns>index (Integer)</returns>
    Public Shared Function PistorToIndex(PistorNr As Integer) As Integer
        'Die Pistor-Tabelle beginnt bei Index 1(!)
        Select Case PistorNr + 1

            'Nährwerte
            Case 34     'Energie in kcal
                Return 1
            Case 32     'Energie in kJ
                Return 2
            Case 36     'Eiweiss
                Return 3
            Case 38     'Kohlenhydrate
                Return 4
            Case 40     'Fett
                Return 5
            Case 52     'Ballaststoffe (Nahrungsfasern)
                Return 13
            Case 56     'Salz
                Return 202
            Case 58     '(davon) Zucker
                Return 11
            Case 60     'davon gesättigte Fettsäuren
                Return 12

            Case 63     'Gluten
                Return 141
            Case 64     'Milch(laktose)
                Return 147
            Case 66     'Eier
                Return 143
            Case 67     'Fische
                Return 144
            Case 68     'Krebstiere
                Return 142
            Case 69     'Sojabohnen
                Return 146
            Case 70     'Erdnüsse
                Return 145
            Case 71     'Hartschalenobst
                Return 148
            Case 72     'Sesamsamen
                Return 151
            Case 73     'Sellerie
                Return 149
            Case 74     'Senf
                Return 150
            Case 75     'Sulfite
                Return 152
            Case 76     'Lupinen
                Return 153
            Case 77     'Weichtiere
                Return 154


            Case Else
                Return -1
        End Select
    End Function

    Public Shared Function PistorToText(Idx As String) As Integer
        Select Case Idx

            'Rohstoff
            Case "Nummer"           'Rohstoff-Nummer
                Return 0

            'Texte
            Case "Bezeichnung"      'Rohstoff-Bezeichnung
                Return 3
            Case "Zutatenliste"     'Zutatenliste
                Return 26
            Case "Deklaration"      'Deklarationstext
                Return 27

            Case Else
                Return -1
        End Select

    End Function


    Public Shared Function StringToDBType(Value As String) As wb_Sql.dbType
        Select Case Value.ToLower
            'winback läuft unter MySQL 
            Case "mysql"
                Return wb_Sql.dbType.mySql
            'WinBack läuft unter MicroSoft SQL
            Case "mssql"
                Return wb_Sql.dbType.msSql
            Case Else
                Return wb_Sql.dbType.undef
        End Select
    End Function

    ''' <summary>
    ''' Gibt den entsprechenden Fehlertext zum Fehler-Code bei der Eingabeprüfung von 
    ''' Minimal/Optimal/Maximal-Charge zurück
    ''' </summary>
    ''' <param name="ErrCode"></param>
    ''' <returns></returns>
    Public Shared Function MinMaxOptChargeToString(ErrCode As wb_Global.MinMaxOptChargenError) As String
        Select Case ErrCode
            Case wb_Global.MinMaxOptChargenError.NoError     'kein Fehler
                Return ""
            Case wb_Global.MinMaxOptChargenError.MinGrOpt    'Minimal-Charge größer als Optimal-Charge
                Return "Die Minimal-Charge muss kleiner als die Optimal-Charge sein"
            Case wb_Global.MinMaxOptChargenError.MinGrMax  'Minimal-Charge größer als Maximal-Charge
                Return "Die Minimal-Charge muss kleiner als die Maximal-Charge sein"
            Case wb_Global.MinMaxOptChargenError.OptGrMax    'Optimal-Charge größer als Maximal-Charge
                Return "Die Optimal-Charge muss kleiner als die Maximal-Charge sein"
            Case wb_Global.MinMaxOptChargenError.OptKlMin    'Optimal-Charge kleiner als Minimal-Charge
                Return "Die Optimal-Charge muss größer als die Minimal-Charge sein"
            Case wb_Global.MinMaxOptChargenError.MaxKlOpt    'Maximal-Charge kleiner als Optimal-Charge
                Return "Die Maximal-Charge muss größer als die Optimal-Charge sein"
            Case wb_Global.MinMaxOptChargenError.MaxKlMin    'Maximal-Charge kleiner als Minimal-Charge
                Return "Die Maximal-Charge muss größer als die Minimal-Charge sein"
            Case Else
                Return "Unbekannter Fehler bei der Eingabe der Chargengrößen"
        End Select
    End Function

    ''' <summary>
    ''' Erzeugt einen Debug-Eintrag im Log wenn die Bedingung wahr ist.
    ''' (Ersetzt Assert.IsTrue). Damit entfällt die Microsoft.VisualStudio.QualityTools.UnitTest.dll
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <param name="Text"></param>
    Public Shared Sub AssertTrue(Condition As Boolean, Text As String)
        If Not Condition Then
            Trace.WriteLine(Text)
            Throw New Exception(Text)
        End If
    End Sub

    ''' <summary>
    ''' Ersetzt die Platzhalter [0]..[4] im String durch die Parameter Param0..Param4
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <param name="Param0"></param>
    ''' <param name="Param1"></param>
    ''' <param name="Param2"></param>
    ''' <param name="Param3"></param>
    ''' <param name="Param4"></param>
    ''' <returns></returns>
    Public Shared Function SetParams(Text As String, Param0 As String, Optional Param1 As String = "\",
                                     Optional Param2 As String = "\", Optional Param3 As String = "\",
                                     Optional Param4 As String = "\") As String
        Text = Replace(Text, "[0]", Param0)
        If Param1 <> "\" Then
            Text = Replace(Text, "[1]", Param1)
        End If
        If Param2 <> "\" Then
            Text = Replace(Text, "[2]", Param2)
        End If
        If Param3 <> "\" Then
            Text = Replace(Text, "[3]", Param3)
        End If
        If Param4 <> "\" Then
            Text = Replace(Text, "[4]", Param4)
        End If
        Return Text
    End Function

    ''' <summary>
    ''' Formatiert einen String mit der angegebenen Vorkomma und Nachkomma-Stelle
    ''' </summary>
    ''' <param name="value">Zahlenwert als String</param>
    ''' <param name="VorKomma">Anzahl der Vorkomma-Stellen</param>
    ''' <param name="NachKomma">Anzahl der Nachkomma-Stellen</param>
    ''' <param name="Culture">Ländereinstellung (Default de-DE)</param>
    ''' <returns></returns>
    Public Shared Function FormatStr(value As String, NachKomma As Integer, Optional VorKomma As Integer = -1, Optional ByVal Culture As String = Nothing) As String
        Dim wert As Double
        Try
            If value IsNot "" And value IsNot Nothing And value IsNot "-" Then
                ' Für Datenbank-Felder muss unabhängig von der Ländereinstellung die Umwandlung mit
                ' der Einstellung de-DE erfolgen
                If Culture IsNot Nothing Then
                    wert = Convert.ToDouble(value, New System.Globalization.CultureInfo(Culture))
                Else
                    wert = Convert.ToDouble(value)
                End If
            Else
                Return "-"
                Exit Function
            End If

            If NachKomma <> 0 Then
                If VorKomma < 0 Then
                    Return wert.ToString("F" & NachKomma.ToString)
                Else
                    Return Right(Space(VorKomma) & wert.ToString("F" & NachKomma.ToString), VorKomma + NachKomma + 1)
                End If
            Else
                If VorKomma < 0 Then
                    Return wert.ToString("F0")
                Else
                    Return Right(Space(VorKomma) & wert.ToString("F" & NachKomma.ToString), VorKomma)
                End If
            End If
        Catch
            Return "-"
        End Try
    End Function

    ''' <summary>
    ''' Formatiert einen String im Muster 00:00:00
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Public Shared Function FormatTimeStr(Value As String) As String
        Dim ts As String() = Value.Split(":")
        Dim ti(3) As Integer

        'alle Bestandteile in Integer wandeln (sicherheitshalber)
        For i = 0 To ts.Length - 1
            ti(i) = StrToInt(ts(i))
        Next



        'Uhrzeit auf sinnvolle Werte begrenzen

        'Sekunden maximal 59
        If (ts.Length > 2) Then
            If (ti(2) > 59) Then
                ti(2) = ti(2) - 60
                ti(1) = ti(1) + 1
            End If
        End If

        'Minuten maximal 59
        If (ts.Length > 1) Then
            If (ti(1) > 59) Then
                ti(1) = ti(1) - 60
                ti(0) = ti(0) + 1
            End If
        End If

        'Stunden maximal 23h
        If ti(0) > 23 Then
            ti(0) = 23
        End If

        Select Case ts.Length
            Case 0
                Return "00:00:00"
            Case 1
                Return Right("00" & ti(0).ToString, 2) & ":00:00"
            Case 2
                Return Right("00" & ti(0).ToString, 2) & ":" & Right("00" & ti(1).ToString, 2) & ":00"
            Case Else
                Return Right("00" & ti(0).ToString, 2) & ":" & Right("00" & ti(1).ToString, 2) & ":" & Right("00" & ti(2).ToString, 2)
        End Select
    End Function

    ''' <summary>
    ''' Entfernt alle Leerzeichen und Sonderzeichen aus einem String
    ''' Rezeptname -> Excel-TabSheetBezeichnung
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Shared Function XRenameToExcelTabName(s As String) As String
        s = s.Replace(" ", "")
        s = s.Replace("\", "")
        s = XRemoveSonderZeichen(s)
        'Länge auf 30 Zeichen begrenzen
        If Len(s) > 30 Then
            s = s.Substring(0, 30)
        End If
        Return s
    End Function

    ''' <summary>
    ''' Entfernt alle "störenden" Sonderzeichen aus einem String
    '''     ' - wird ersatzlos gestrichen (verhindert Speichern eines Strings in DB)
    '''     \ - wird ersetzt durch bcksl; (Pfadangaben Windows!)
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Shared Function XRemoveSonderZeichen(s As String, Optional nwtZutaten As Boolean = False) As String
        If s IsNot Nothing Then
            s = s.Replace("'", "")
            s = s.Replace("\", "&bcksl;")
            If Not nwtZutaten Then
                Return s
            Else
                s = s.Replace("{", "")
                s = s.Replace("}", "")
                s = s.Replace(">", "")
                s = s.Replace("<", "")
                Return s
            End If
        Else
            Return ""
        End If
    End Function

    Public Shared Function XRestoreSonderZeichen(s As String) As String
        If s IsNot Nothing Then
            s = s.Replace("&bcksl;", "\")
            Return s
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Fügt einen String kommagetrennt an einen bestehenden Ausdruck an. 
    ''' Ist der bestehende Ausdruck leer wird nur der String zurückgegeben.
    ''' 
    ''' Wenn der anzuhängende String schon enthalten ist, wird der Original-String zurückgegeben
    ''' </summary>
    ''' <param name="OldString"></param>
    ''' <param name="NewString"></param>
    ''' <returns></returns>
    Public Shared Function AddCSV(OldString As String, NewString As String) As String
        If OldString.Contains(NewString) Then
            Return OldString
        Else
            If OldString = "" Then
                Return NewString
            Else
                Return OldString & ", " & NewString
            End If
        End If
    End Function

    ''' <summary>
    ''' Begrenzt einen String in der Länge auf x Zeichen. 
    ''' Wenn Dots = True ist, werden am Schluss drei Punkte angehängt, als Hinweis, dass gekürzt wurde.
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="AnzZeichen"></param>
    ''' <param name="Dots"></param>
    ''' <returns></returns>
    Public Shared Function Truncate(s As String, AnzZeichen As Integer, Optional Dots As Boolean = False) As String
        If IsNothing(s) Then
            Return ""
        End If
        If s.Length <= AnzZeichen Then
            Return s
        Else
            If Dots Then
                Return s.Substring(0, AnzZeichen - 3) & "..."
            Else
                Return s.Substring(0, AnzZeichen)
            End If
        End If
    End Function

    ''' <summary>
    ''' Wandelt einen Double-Wert in einen String um. Dabei wird das Dezimal-Trennzeichen als Punkt dargestellt !!!!
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function DoubleToXString(d As Double) As String
        Dim s As String = d.ToString("G")
        s = s.Replace(",", ".")
        Return s
    End Function

    ''' <summary>
    ''' Wandelt einen String sicher in Float um. Das Zahlenformat kann US/DE sein. Punkte werden vor der Konvertierung in Koma umgewandelt.
    ''' 1000er - Trennzeichen sind nicht erlaubt.
    ''' Wenn die Umwandlung per TryParse fehlschlägt (Result=False) wird die einfache Umwandlung per val() versucht. Damit können auch Werte
    ''' umgewandelt werden, die Strings enthalten (z.B. 10kg)
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns>Kovertierten String im Format Double</returns>
    Public Shared Function StrToDouble(value As String) As Double
        If value IsNot Nothing Then
            Dim d As Double
            Try
                value = value.Replace(".", ",")
                If Double.TryParse(value, NumberStyles.Number, CultureInfo.CreateSpecificCulture("de-DE"), d) Then
                    Return d
                Else
                    'mögliche Strings oder Sonderzeichen entfernen
                    value = New System.Text.RegularExpressions.Regex("[a-zA-ZüöäÜÖÄß%°\\s\\n]").Replace(value, String.Empty)
                    If Double.TryParse(value, NumberStyles.Number, CultureInfo.CreateSpecificCulture("de-DE"), d) Then
                        Return d
                    Else
                        Return 0.0F
                    End If
                End If
            Catch ex As Exception
                Return 0.0F
            End Try
        Else
            Return 0.0F
        End If
    End Function

    ''' <summary>
    ''' Wandelt einen String sicher in Integer um. Wenn die Umwandlung fehlschlägt wird 0 zurückgegeben.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns>Konvertierten String im Format Integer</returns>
    Public Shared Function StrToInt(value As String) As Integer
        Dim i As Integer
        If value IsNot Nothing Then
            Try
                value = value.Replace(".", ",")
                If Integer.TryParse(value, NumberStyles.Number, CultureInfo.CreateSpecificCulture("de-DE"), i) Then
                    Return i
                Else
                    Try
                        Return Int(Val(value))
                    Catch
                        Return 0
                    End Try
                End If
            Catch ex As Exception
                Return 0
            End Try
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Wandelt ein Objekt sicher in einen Integer-Wert um
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Public Shared Function ValueToInt(Value As Object) As Integer
        'TODO Laufzeit testen/optimieren
        Dim i As Integer
        Try
            If Value IsNot Nothing And Not IsDBNull(Value) Then
                Integer.TryParse(Value, NumberStyles.Number, CultureInfo.CreateSpecificCulture("de-DE"), i)
                Return i
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Wandelt ein Objekt sicher in einen Float-Wert um
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Public Shared Function ValueToDouble(Value As Object) As Double
        'TODO Laufzeit testen/optimieren
        Dim d As Double
        Try
            If Value IsNot Nothing And Not IsDBNull(Value) Then
                Value = Value.Replace(".", ",")
                Double.TryParse(Value, NumberStyles.Number, CultureInfo.CreateSpecificCulture("de-DE"), d)
                Return d
            Else
                Return 0.0F
            End If
        Catch ex As Exception
            Return 0.0F
        End Try
    End Function

    ''' <summary>
    ''' Wandelt einen String aus der MySQL-Datenbank (Latin-1) in Utf-8.
    ''' Abhängig vom Ländercode wird die Übersetzung aus der entsprechenden Code-Page vorgenommen
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function MySqlToUtf8(Value As String) As String
        'abhängig von der eingestellten Code-Page
        Select Case wb_GlobalSettings.ConvertMySQL_CodePage
            Case wb_Global.MySqlCodepage.iso8859_15
                Return Value
            Case wb_Global.MySqlCodepage.iso8859_5
                Dim o15 As Byte() = Encoding_iso8859_15.GetBytes(Value)
                Return Encoding_iso8859_5.GetString(o15)
            Case wb_Global.MySqlCodepage.iso8859_1
                Dim o15 As Byte() = Encoding_iso8859_15.GetBytes(Value)
                Return Encoding_iso8859_1.GetString(o15)
            Case Else
                Return Value
        End Select
    End Function

    ''' <summary>
    ''' Wandelt einen String von Utf-8 nach Latin1 (Schreiben in MySql-Datenbank)
    ''' Abhängig vom Ländercode wird die Übersetzung aus der entsprechenden Code-Page vorgenommen
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Public Shared Function UTF8toMySql(Value As String) As String
        'abhängig von der eingestellten Code-Page
        'TODO für UnitTest muss die Sprache einstellbar sein !!
        Select Case wb_GlobalSettings.ConvertMySQL_CodePage
            Case wb_Global.MySqlCodepage.iso8859_15
                Return Value
            Case wb_Global.MySqlCodepage.iso8859_5
                Dim o5 As Byte() = Encoding_iso8859_5.GetBytes(Value)
                Return Encoding_iso8859_15.GetString(o5)
            Case wb_Global.MySqlCodepage.iso8859_1
                Dim o5 As Byte() = Encoding_iso8859_1.GetBytes(Value)
                Return Encoding_iso8859_15.GetString(o5)
            Case Else
                Return Value
        End Select
    End Function

    ''' <summary>
    ''' Vergleicht zwei Versions-Strings miteinander. Format V.V.V
    '''     
    ''' Rückgabe-Wert:
    '''     VersionIst ist älter als VersionNeu     -   True
    '''     VersionIst ist gleich    VersionNeu     -   False 
    '''     VersionIst ist neuer als VersionNeu     -   False
    ''' </summary>
    ''' <param name="VersionIst"></param>
    ''' <param name="VersionNeu"></param>
    ''' <returns></returns>
    Public Shared Function CompareVersion(VersionIst As String, VersionNeu As String) As Boolean
        Try
            'Aufteilen in die einzelnen Versions-Nummern
            Dim Vi() As String = Split(VersionIst, ".")
            Dim Vn() As String = Split(VersionNeu, ".")

            'Vergleich Hauptversion
            If Convert.ToInt16(Vi(0)) < Convert.ToInt16(Vn(0)) Then
                Return True
            End If
            'Vergleich Nebenversion
            If Convert.ToInt16(Vi(1)) < Convert.ToInt16(Vn(1)) Then
                Return True
            End If
            'Vergleich Releaseversion
            If Convert.ToInt16(Vi(2)) < Convert.ToInt16(Vn(2)) Then
                Return True
            End If
        Catch
        End Try
        Return False
    End Function


    Public Shared Function SaveDiv(Divident As Double, Divisor As Double) As Double
        If Divisor <> 0 Then
            Return Divident / Divisor
        Else
            Return 0
        End If
    End Function

    Public Shared Function ProzentSatz(Grundwert As Double, Prozentwert As Double, Optional Dezimalstellen As Integer = 0) As Double
        Return Math.Round(100 * SaveDiv(Prozentwert, Grundwert), Dezimalstellen)
    End Function

    Public Shared Sub CloseAndDisposeSubForm(ByRef frm As Windows.Forms.Form)
        If frm IsNot Nothing Then
            frm.Close()
            frm = Nothing
        End If
    End Sub

    Public Shared Function FTP_Upload_File(ByVal filetoupload As String, pathtoupload As String) As String
        ', ByVal ftpuri As String, ByVal ftpusername As String, ByVal ftppassword As String) As Long
        Dim FtpURI As String = "ftp://" & wb_GlobalSettings.MySQLServerIP & pathtoupload
        Dim FtpUser As String = wb_GlobalSettings.MySQLUser
        Dim FtpPass As String = wb_GlobalSettings.MySQLPass

        ' Create a web request that will be used to talk with the server and set the request method to upload a file by ftp.
        Dim ftpRequest As FtpWebRequest = CType(WebRequest.Create(FtpURI), FtpWebRequest)

        Try
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile

            ' Confirm the Network credentials based on the user name and password passed in.
            ftpRequest.Credentials = New NetworkCredential(FtpUser, FtpPass)

            ' Read into a Byte array the contents of the file to be uploaded 
            Dim bytes() As Byte = System.IO.File.ReadAllBytes(filetoupload)

            ' Transfer the byte array contents into the request stream, write and then close when done.
            ftpRequest.ContentLength = bytes.Length
            Using UploadStream As Stream = ftpRequest.GetRequestStream()
                UploadStream.Write(bytes, 0, bytes.Length)
                UploadStream.Close()
            End Using
        Catch ex As Exception
            Return ex.Message
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Löscht alle Dateien aus einem Verzeichnis, die älter als xDays sind
    ''' </summary>
    ''' <param name="Directory"></param>
    ''' <param name="FilePattern"></param>
    ''' <param name="xDays"></param>
    Public Shared Sub DeleteOldFiles(Directory As String, FilePattern As String, xDays As Integer)
        Try
            Dim Dir As New DirectoryInfo(Directory)
            Dim Heute As DateTime = DateTime.Now
            For Each fi As FileInfo In Dir.GetFiles(FilePattern, SearchOption.TopDirectoryOnly)
                If fi.CreationTime.AddDays(xDays) < Heute Then
                    fi.Delete()
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Pdf-File mit der Windows-Default-Anwendung anzeigen. (pdf-Viewer)
    ''' </summary>
    ''' <param name="Filename"></param>
    Public Shared Sub ShowPdf(Filename As String)
        Dim pr As New Process
        Try
            pr.StartInfo.FileName = Filename
            pr.StartInfo.Verb = "Open"
            pr.Start()
        Catch ex As Exception
            MsgBox("Beim Anzeigen der pdf-Datei ist ein Fehler aufgetreten." & vbCrLf & vbCrLf & "Bitte prüfen, ob ein entsprechender pdf-Viewer installiert ist", MsgBoxStyle.Critical, "pdf-File anzeigen")
        End Try
        pr = Nothing
    End Sub

    ''' <summary>
    ''' Für ein Batch-File im Verzeichnis MySQLBatch aus. Über Argument wird %2 an das Batch-File übergeben
    ''' </summary>
    ''' <param name="Directory"></param>
    ''' <param name="BatchFile"></param>
    ''' <param name="Argument"></param>
    ''' <param name="WaitUntilReady"></param>
    Public Shared Function DoBatch(Directory As String, BatchFile As String, Argument As String, WaitUntilReady As Boolean) As Boolean
        Dim cmd As String = Chr(34) + wb_GlobalSettings.pAddInPath + "\" + BatchFile + Chr(34)
        Dim arg As String = Chr(34) + Directory + Chr(34) + " " + Chr(34) + Argument + Chr(34)
        'Batch-File ausführen
        Return ExeBatch(Directory, cmd, arg, WaitUntilReady)
    End Function

    ''' <summary>
    ''' Für ein Batch-File im Verzeichnis MySQLBatch aus. Arg1/Arg2 werden als %2 und %3 an das Batch-File übergeben
    ''' </summary>
    ''' <param name="Directory"></param>
    ''' <param name="BatchFile"></param>
    ''' <param name="Arg1"></param>
    ''' <param name="Arg2"></param>
    ''' <param name="WaitUntilReady"></param>
    Public Shared Function DoBatch(Directory As String, BatchFile As String, Arg1 As String, Arg2 As String, WaitUntilReady As Boolean) As Boolean
        Dim cmd As String = Chr(34) + wb_GlobalSettings.pAddInPath + "\" + BatchFile + Chr(34)
        Dim arg As String = Chr(34) + Directory + Chr(34) + " " + Chr(34) + Arg1 + Chr(34) + " " + Chr(34) + Arg2 + Chr(34)
        'Batch-File ausführen
        Return ExeBatch(Directory, cmd, arg, WaitUntilReady)
    End Function

    Private Shared Function ExeBatch(Directory As String, cmd As String, arg As String, WaitUntilReady As Boolean) As Boolean
        Dim p As New Process()
        Try
            p.StartInfo = New ProcessStartInfo(cmd, arg)
            p.StartInfo.CreateNoWindow = True
            p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            p.StartInfo.WorkingDirectory = Directory
            p.Start()
            p.WaitForExit()
            Return (p.ExitCode = 0)
        Catch ex As Exception
            MsgBox("Fehler beim Start von " & cmd & " " & arg, MsgBoxStyle.Critical, "Batch-File")
            Return False
        End Try
    End Function

    '   Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long
    '   ShellExecute(0&, "open", wb_GlobalSettings.pProgrammPath & "Orgasoft.NET", "", "", 1)

    ''' <summary>
    ''' Programm neustarten. Wenn das WinBack-AddIn unter OrgaSoft läuft, wird der OrgaSoft-Prozess gekillt und neu gestartet !
    ''' </summary>
    Public Shared Sub Restart()
        'Programm-Variante OrgaBask startet Orgasoft-Instanz neu
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            'Programm-Verzeichnis Signum OrgaSoft
            Dim SignumDirectory As String = System.AppDomain.CurrentDomain.BaseDirectory
            'Dateiname Signum OrgaSoft
            Dim SignumFileName As String = System.AppDomain.CurrentDomain.FriendlyName
            'Prozess-Info
            Dim StartInfo As New ProcessStartInfo()
            StartInfo.FileName = SignumDirectory & SignumFileName
            'Restart durchführen
            Process.Start(StartInfo)
            Process.GetCurrentProcess().Kill()
        Else
            'Restart (normal)
            Application.Restart()
        End If
    End Sub

    ''' <summary>
    ''' Beendet das laufende Programm. Wenn das WinBack-AddIn unter OrgaSoft läuft, wird der OrgaSoft-Prozess gekillt !
    ''' </summary>
    Public Shared Sub ExitProgram()
        'Programm-Variante OrgaBask startet Orgasoft-Instanz neu
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            Process.GetCurrentProcess().Kill()
        Else
            Application.Exit()
        End If
    End Sub


    ''' <summary>
    ''' Startet eine ssh-Sitzung und führt ein Kommando auf dem Linux-Rechner mit der angegebenen IP-Adresse aus
    ''' (siehe auch https://www.vb-paradise.de/index.php/Thread/121384-Visual-Basic-Windows-Application-SSH-NET-Verbindung-aufbauen/)
    ''' Renci.SshNet.dll wird über NuGet verwaltet !!
    ''' </summary>
    ''' <param name="User">String - Username</param>
    ''' <param name="Pass">String - Passwort</param>
    ''' <param name="Host">String - IP-Adresse</param>
    ''' <param name="Command">String - Shell-Kommando</param>
    ''' <returns>Ausgabe der Command-Shell</returns>
    Public Shared Function ExecSSH(User As String, Pass As String, Host As String, Command As String) As String
        'Create the objects needed to make the connection'
        Dim connInfo As New Renci.SshNet.PasswordConnectionInfo(Host, User, Pass)
        'Need to hold the command'
        Dim sshClient As New Renci.SshNet.SshClient(connInfo)
        Dim Output As Renci.SshNet.SshCommand
        Try
            'connect to the server'
            sshClient.Connect()
            'Run the command and put the results into the output object. In this case'
            Output = sshClient.RunCommand(Command)

            'Exit-Status
            If Output.ExitStatus > 0 Then
                If Output.Error = "" Then
                    'Kommando war OK, aber nicht erfolgreich (Ergebnis und Fehlercode sind leer)
                    sshClient.Disconnect()
                    Return Output.Result
                Else
                    'Fehler beim Ausführen des Kommandos
                    Trace.WriteLine("@E_Fehler bei ssh-Command " & Output.Error)
                    'Close the connection.'
                    sshClient.Disconnect()
                    Return "ERR" & vbLf & Output.Error
                End If
            Else
                'Close the connection.'
                sshClient.Disconnect()
                Return Output.Result
            End If

        Catch ex As Exception
            'Close the connection.'
            sshClient.Disconnect()
            Return "ERR" & vbLf & "Keine Verbindung zu " & Host
        End Try
    End Function

    Public Shared Function ExecSSH(User As String, Pass As String, Host As String, Command As List(Of String)) As List(Of String)
        'Create the objects needed to make the connection'
        Dim connInfo As New Renci.SshNet.PasswordConnectionInfo(Host, User, Pass)
        'Need to hold the command'
        Dim sshClient As New Renci.SshNet.SshClient(connInfo)
        Dim Output As Renci.SshNet.SshCommand
        Dim Result As New List(Of String)
        'connect to the server'
        sshClient.Connect()
        'Run the commands and put the results into the output object. In this case'
        For Each cmd As String In Command
            Output = sshClient.RunCommand(cmd)
            Result.Add(Output.Result)
        Next
        'Close the connection.'
        sshClient.Disconnect()
        Return Result
    End Function

    ''' <summary>
    ''' Speichert das angegebene Array binär in die angegebene Datei.
    ''' </summary>
    ''' <param name="Filename">Dateiname</param>
    ''' <param name="arr">Array, das gespeichert werden soll</param>
    Public Shared Function ArraySave(ByVal Filename As String, ByVal arr As Object) As Boolean

        Dim fs As FileStream = Nothing
        Dim Success As Boolean = False

        Try
            ' Datei zum Schreiben öffnen
            fs = New FileStream(Filename, FileMode.Create, FileAccess.Write)

            ' Array serialisieren und speichern
            Dim formatter As New BinaryFormatter()
            formatter.Serialize(fs, arr)
            Success = True

        Catch ex As Exception
        Finally
            ' Datei schließen
            If Not IsNothing(fs) Then fs.Close()
        End Try

        Return (Success)
    End Function

    ''' <summary>
    ''' Liest ein zuvor gespeichertes Array aus der angegebenen Datei aus.
    ''' </summary>
    ''' <param name="Filename">Dateiname</param>
    ''' <param name="arr">Array, das die Daten enthalten soll.</param>
    Public Shared Function ArrayRead(ByVal Filename As String, ByRef arr As Object) As Boolean

        Dim Success As Boolean = False

        ' Prüfen, ob Datei existiert
        If IO.File.Exists(Filename) Then
            Dim fs As FileStream = Nothing
            Try
                ' Datei zum Lesen öffnen
                fs = New FileStream(Filename, FileMode.Open, FileAccess.Read)

                ' Daten deserialiseren und dem Array zuweisen
                Dim formatter As New BinaryFormatter()
                arr = formatter.Deserialize(fs)
                Success = True

            Catch ex As Exception
            Finally
                ' Datei schließen
                If Not IsNothing(fs) Then fs.Close()
            End Try
        End If

        Return (Success)
    End Function

    ''' <summary>
    ''' Datei komprimieren in .bz2
    ''' Der File-Typ ist beliebig. Das Zielverzeichniss muss Schreib-Rechte haben. Nach erfolgreicher Operation wird
    ''' True zurückgeliefert.
    ''' </summary>
    ''' <remarks>
    ''' SharpZipLibrary samples
    '''  Copyright (c) 2007, AlphaSierraPapa
    '''  All rights reserved.
    ''' 
    ''' ' Redistribution and use in source and binary forms, with or without modification, are
    '''  permitted provided that the following conditions are met:
    ''' 
    '''  - Redistributions of source code must retain the above copyright notice, this list
    '''    of conditions and the following disclaimer.
    ''' 
    '''  - Redistributions in binary form must reproduce the above copyright notice, this list
    '''    of conditions and the following disclaimer in the documentation and/or other materials
    '''    provided with the distribution.
    ''' 
    '''  - Neither the name of the SharpDevelop team nor the names of its contributors may be used to
    '''    endorse or promote products derived from this software without specific prior written
    '''    permission.
    ''' 
    '''  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS AS IS AND ANY EXPRESS
    ''' OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
    ''' AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
    ''' CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
    ''' DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
    ''' DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
    ''' IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
    ''' OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    ''' </remarks>
    ''' <param name="InFileName"> String Dateiname und Pfad inlusive Extension</param>
    ''' <param name="OutFileName"> String Dateiname und Pfad inlusive Extension (.bz2)</param>
    ''' <returns>
    ''' True - Komprimieren war erfolgreich
    ''' False - Fehler beim Lesen/Schreiben
    ''' </returns>
    Public Shared Function bz2CompressFile(InFileName As String, OutFileName As String) As Boolean
        'Compression of single-file archive
        Dim fsInputFile As FileStream, fsBZ2Archive As FileStream
        Try
            fsInputFile = File.OpenRead(InFileName)
            fsBZ2Archive = File.Create(OutFileName)
            BZip2.Compress(fsInputFile, fsBZ2Archive, True, 4026)
            fsInputFile.Close()
        Catch
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Datei dekomprimieren aus .bz2
    ''' Der File-Typ ist beliebig. Das Zielverzeichniss muss Schreib-Rechte haben. Nach erfolgreicher Operation wird
    ''' True zurückgeliefert.
    ''' </summary>
    ''' <remarks>
    ''' SharpZipLibrary samples
    '''  Copyright (c) 2007, AlphaSierraPapa
    '''  All rights reserved.
    ''' 
    ''' ' Redistribution and use in source and binary forms, with or without modification, are
    '''  permitted provided that the following conditions are met:
    ''' 
    '''  - Redistributions of source code must retain the above copyright notice, this list
    '''    of conditions and the following disclaimer.
    ''' 
    '''  - Redistributions in binary form must reproduce the above copyright notice, this list
    '''    of conditions and the following disclaimer in the documentation and/or other materials
    '''    provided with the distribution.
    ''' 
    '''  - Neither the name of the SharpDevelop team nor the names of its contributors may be used to
    '''    endorse or promote products derived from this software without specific prior written
    '''    permission.
    ''' 
    '''  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS AS IS AND ANY EXPRESS
    ''' OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
    ''' AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
    ''' CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
    ''' DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
    ''' DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
    ''' IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
    ''' OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    ''' </remarks>
    ''' <param name="InFileName"> String Dateiname und Pfad inlusive Extension (.bz2)</param>
    ''' <param name="OutFileName"> String Dateiname und Pfad inlusive Extension</param>
    ''' <returns>
    ''' True - Dekomprimieren war erfolgreich
    ''' False - Fehler beim Lesen/Schreiben/Dekomprimieren
    ''' </returns>
    Public Shared Function bz2DecompressFile(InFileName As String, OutFileName As String) As Boolean
        Dim fsBZ2Archive As FileStream, fsOutput As FileStream
        Try
            fsBZ2Archive = File.OpenRead(InFileName)
            fsOutput = File.Create(OutFileName)
            BZip2.Decompress(fsBZ2Archive, fsOutput, True)
            fsBZ2Archive.Close()
            fsOutput.Close()
        Catch
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Extrahiert aus Environment.StackTrace die Programm-Zeile(n) der aufrufenden Routine:
    ''' (Beispiel)
    ''' 
    '''     bei System.Environment.GetStackTrace(Exception e, Boolean needFileInfo)
    '''     bei System.Environment.get_StackTrace()
    '''     bei WinBack.wb_TraceListener.WriteLine(String message) In C:\Users\will.WINBACK\Source\Repos\Signum_WinBack\wb_00_Klassen\wb_TraceListener.vb:Zeile 36.
    '''     bei System.Diagnostics.TraceInternal.WriteLine(String message)
    '''     bei System.Diagnostics.Trace.WriteLine(String message)
    '''     bei WinBackUnitTest.UnitTest_wb_TraceLogger.TestTraceWriteLn() In C:\Users\will.WINBACK\Source\Repos\Signum_WinBack\WinBackUnitTest\UnitTest_wb_TraceLogger.vb:Zeile 39.
    '''         =======
    '''     bei System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor)
    '''     bei System.Reflection.RuntimeMethodInfo.UnsafeInvokeInternal(Object obj, Object[] parameters, Object[] arguments)
    '''     bei System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.TestMethodRunner.DefaultTestMethodInvoke(Object[] args)
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.TestMethodRunner.RunTestMethod()
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.TestMethodRunner.ExecuteTest()
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.TestMethodRunner.ExecuteInternal()
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.TestMethodRunner.Execute()
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.UnitTestRunner.RunInternal(TestMethod testMethod, Boolean isDataDriven, Dictionary`2 runParameters)
    '''     bei Microsoft.VisualStudio.TestPlatform.MSTestFramework.UnitTestRunner.RunSingleTest(String name, String fullClassName, Boolean isAsync, Dictionary`2 runParameters)
    '''     
    ''' Ist OnlyOneLine True, wird nur der erste Aufruf mit dem Inhalt WinBack zurückgegeben. 
    ''' Der Ergebnis-String enthält Komma-getrennt, die aufrufende Routine und die Zeilen-Nummer
    ''' </summary>
    ''' <param name="Stack"></param>
    ''' <returns></returns>
    Public Shared Function GetLocalStackTrace(Stack As String, OnlyOneLinie As Boolean) As ArrayList
        Dim subStack As String = ""
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim ResultStack As New ArrayList

        'Ergebnis-Liste löschen
        ResultStack.Clear()
        Do
            'Aufteilen nach CRLF
            i = Stack.IndexOf(vbCrLf, j)
            If (i < 0) Then
                i = Len(Stack)
            End If
            'eine Zeile aus Stacktrace
            subStack = Stack.Substring(j, i - j)
            If subStack.Contains("WinBack") And Not subStack.Contains("TraceListener") And Not _
               subStack.Contains("MyApplication.Main") And Not subStack.Contains("Lambda$") Then

                'beim ersten Auftreten des passenden Musters wird der String zerlegt und die Schleife verlassen
                'TODO was passiert bei einem englischen VisualStudio?
                Dim x1 As Integer = subStack.IndexOf(" bei ")
                Dim x2 As Integer = subStack.IndexOf(" in ")
                Dim x3 As Integer = subStack.IndexOf(":Zeile")

                If (x1 > 0) And (x2 > 0) And (x3 > 0) And (x2 - x1) > 5 And (x3 - x2) > 4 Then
                    'Stack-Trace in Einzelteile zerlegen
                    Dim s1 As String = subStack.Substring(x1 + 5, x2 - x1 - 5)
                    Dim s2 As String = subStack.Substring(x2 + 4, x3 - x2 - 4)
                    Dim s3 As Integer = Val(subStack.Substring(x3 + 7))

                    'Ergebniszeile zusammenbauen
                    ResultStack.Add("Z" & s3.ToString("D5") & vbTab & s1)

                    'nur die erste Aufrufzeile ausgeben - dann Exit
                    If OnlyOneLinie Then
                        Return ResultStack
                        Exit Do
                    End If
                End If
            End If
            'nächste Zeile
            j = i + 2
        Loop Until (i < 0) Or (i = Len(Stack))

        ResultStack.Add("---")
        Return ResultStack
    End Function

    ''' <summary>
    ''' Gibt den kompletten Aufrufbaum aller WinBack-Routinen zurück.
    ''' Der Ergebnis-String enthält Komma-getrennt, die aufrufende Routine und die Zeilen-Nummer
    ''' </summary>
    ''' <param name="Stack"></param>
    ''' <returns></returns>
    Public Shared Function GetStackTraceTree(Stack As String) As String
        'Aufruf-Baum aus Environment.Stack_Trace(Array)
        Dim x = wb_Functions.GetLocalStackTrace(Stack, False)
        'Ergebnis-String aus Array zusammensetzen (CRLF als Zeilentrenner)
        Dim s As String = vbCrLf
        For Each t As String In x
            s &= vbTab & t & vbCrLf
        Next
        Return s
    End Function

    ''' <summary>
    ''' Ermittelt die Liste aller aktiven Prozesse. 
    ''' Gibt True zurück, wenn OrgaBack läuft !
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetProcessRunning(ProcessName As String) As Boolean
        'Alle Prozesse durchlaufen
        For Each oProcess As Process In Process.GetProcesses
            ' Prozess-Infos ermitteln
            Debug.Print("ID/ProzessName " & oProcess.Id.ToString & " " & oProcess.ProcessName)
            If oProcess.ProcessName = ProcessName Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Prüft ob OrgaBack auf dem Server installiert ist.
    ''' Ausgelesen wird der Registry-Key "Software\Signum GmbH Darmstadt\OrgaSoft.NET\CompaniesHeight"
    ''' 
    ''' Wenn der Key vorhanden ist, wird True zurückgegeben.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function OrgaBackIsInstalled() As Boolean
        Return RegKeyNameExists(Microsoft.Win32.RegistryHive.CurrentUser, "Software\Signum GmbH Darmstadt\OrgaSoft.NET", "CompaniesHeight")
    End Function

    ''' <summary>
    ''' Prüfen ob ein Registry-Pfad existiert
    ''' </summary>
    ''' <param name="hive"></param>
    ''' <param name="path"></param>
    ''' <param name="keyName"></param>
    ''' <returns></returns>
    Public Shared Function RegKeyNameExists(ByVal hive As RegistryHive, ByVal path As String, ByVal keyName As String) As Boolean
        Dim regKey As RegistryKey
        Select Case hive
            Case RegistryHive.CurrentUser
                regKey = Registry.CurrentUser.OpenSubKey(path)  ' CurrentUser
            Case RegistryHive.LocalMachine
                regKey = Registry.LocalMachine.OpenSubKey(path) ' LocalMachine
            Case Else
                ' Throw New ArgumentException("Nur HKLM und HKCU sind erlaubt")
                Return False
        End Select

        'Schlüssel nicht vorhanden
        If regKey Is Nothing Then Return False
        'Schlüssel suchen
        For Each regKeyName As String In regKey.GetValueNames()
            'Gefunden
            If regKeyName.Trim.ToUpper = keyName.Trim.ToUpper Then Return True
        Next
        'Nicht gefunden
        Return False
    End Function

    Public Shared Function CheckProgramInstalled(ProgName As String) As Boolean
        Dim appkey As RegistryKey
        appkey = Registry.LocalMachine.OpenSubKey("Software")
        For Each app In appkey.GetSubKeyNames
            If app = ProgName Then
                Return True
            End If
        Next
        appkey = Registry.CurrentUser.OpenSubKey("Software")
        For Each app In appkey.GetSubKeyNames
            If app = ProgName Then
                Return True
            End If
        Next
        Return False
    End Function

    Shared Function GetCurrentIpV4Address() As IPAddress
        Dim addresses As IPAddress() = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList
        Return addresses.FirstOrDefault(Function(ip) ip.AddressFamily = AddressFamily.InterNetwork)
    End Function

    Shared Function GetCurrentIpV4AddressString() As String
        Dim ip As IPAddress = GetCurrentIpV4Address()
        Return If(ip, "").ToString()
    End Function

    Shared Function AddPath(txtNewPath As String) As Boolean
        Dim objEv As ManagementObjectSearcher = New ManagementObjectSearcher("SELECT * FROM Win32_Environment")
        For Each objMgmt As ManagementObject In objEv.Get

            Debug.Print("Mgmt-Name " & objMgmt("Name"))
            Debug.Print("Mgmt-User " & objMgmt("UserName"))

            If objMgmt("Name") = "Path" And objMgmt("UserName") = "<SYSTEM>" Then
                Dim strPath As String = objMgmt("VariableValue")
                If strPath.ToLower.IndexOf("cvsnt") >= 0 Then
                    If objMgmt("VariableValue").ToString.Substring(objMgmt("VariableValue").ToString.Length - 1) = ";" Then
                        objMgmt("VariableValue") = objMgmt("VariableValue") + Trim(txtNewPath) + ";"
                    Else
                        objMgmt("VariableValue") = objMgmt("VariableValue") + ";" + Trim(txtNewPath) + ";"
                    End If
                End If
                'TODO verlangt Admin-Rechte !!
                objMgmt.Put()
                MessageBox.Show("Path Added Successfully")
                Return True
            End If
        Next
        Return False
    End Function
End Class
