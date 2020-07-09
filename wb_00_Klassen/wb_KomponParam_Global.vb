Imports WinBack.wb_Global
''' <summary>
''' Ermittlung der Parameter für die Produktion
''' Gibt zu einem spezifischen Parameter p die entsprechenden statischen Werte zurück.
''' Vor dem ersten Aufruf wird die Hash-Table aus der Datenbank gelesen und aufgebaut.
''' </summary>
Public Class wb_KomponParam_Global

    Private Shared _ktTypMaxParams As New Hashtable
    Public Shared ktTypXXXParams As New Hashtable

    ''' <summary>
    ''' Vor dem ersten Aufruf der Funktionen in dieser Klasse wird der Shared-Konstruktor aufgerufen
    ''' Initialisierung und Aufbau der Hash-Table aus der Datenbank (Tabelle KomponTypen und Einheiten)
    ''' </summary>
    Shared Sub New()
        Dim k As ktTypXXXParam
        Dim Type As Integer = wb_Global.UNDEFINED
        _ktTypMaxParams.Clear()

        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompTypXXX, "<>200"))
        ktTypXXXParams.Clear()
        While winback.Read

            'Parameter-Datensatz (WinBack-Komponenten-Type/Parameter)
            k.Type = winback.iField("KT_Typ_Nr")

            'Die Komponenten(Parameter-)Type hat sich geändert
            If k.Type <> Type Then
                'Sonder-Parameter einfügen
                ExtendedParameter(Type, k.ParamNr)
                'Maximalwert der Parameter pro Type
                _ktTypMaxParams.Add(Type, k.ParamNr)
                'letzte Type merken
                Type = k.Type
            End If

            'Parameter-Nummer
            k.ParamNr = winback.iField("KT_ParamNr")
            'Parameter-Bezeichnung
            k.Bezeichnung = wb_Language.TextFilter(winback.sField("KT_Bezeichnung"))
            'Parameter-Einheit
            k.Einheit = winback.sField("E_Einheit")
            'Parameter-Format
            k.eFormat = winback.iField("KT_Format")
            'Parameter-Eingabe unterer Grenzwert
            k.eUG = winback.sField("KT_UnterGW")
            'Parameter-Eingabe oberer Grenzwert
            k.eOG = winback.sField("KT_OberGW")

            'Parameter aktiv
            If k.Type = ktParam.kt301 Then
                'Parameter aktiv (Nährwerte und Allergene
                k.Used = (winback.sField("KT_Rezept") = "X")
                'Fehler in der Datenbank - Format-Eintrag für Allergene
                If wb_KomponParam301_Global.IsAllergen(k.ParamNr) Then
                    k.eFormat = EnhEdit.EnhEdit_Global.wb_Format.fAllergen
                ElseIf wb_KomponParam301_Global.IsErnaehrung(k.ParamNr) Then
                    k.eFormat = EnhEdit.EnhEdit_Global.wb_Format.fYesNo
                End If
            Else
                'Anzeige des Parameter-Wertes abhängig von Parameter-Type und Parameter-Nummer
                k.Used = SetParameterUsed(k.Type, k.ParamNr)
            End If

            'Parameter-Datensatz speichern
            If k.Used Then
                Try
                    ktTypXXXParams.Add(BuildKey(k.Type, k.ParamNr), k)
                Catch
                End Try
            End If

        End While
        winback.Close()
    End Sub

    Private Shared Function SetParameterUsed(t As Integer, p As Integer) As Boolean
        'Komponenten-Typen (Parameter-Nummer kleiner 200)
        If t < wb_Global.ktParam.kt200 Then
            'Anzeige der Parameter abhängig von der Komponenten-Type
            Select Case wb_Functions.IntToKomponType(t)

                Case KomponTypen.KO_TYPE_AUTOKOMPONENTE
                    'Parameter
                    Select Case p
                        Case 4, 5, 7, 8, 9, wb_Global.T101_LagerOrt
                            Return True
                        Case Else
                            Return False
                    End Select

                Case KomponTypen.KO_TYPE_HANDKOMPONENTE
                    'Parameter
                    Select Case p
                        Case 3, 4, 7, 6, 5
                            Return True
                        Case Else
                            Return False
                    End Select

                Case KomponTypen.KO_TYPE_WASSERKOMPONENTE
                    'Parameter
                    Select Case p
                        Case 4, 5, 6, 7, 8, 9
                            Return True
                        Case Else
                            Return False
                    End Select

                Case KomponTypen.KO_TYPE_EISKOMPONENTE
                    'Parameter
                    Select Case p
                        Case 3, 4, 6, 7, 8, 9, 10
                            Return True
                        Case Else
                            Return False
                    End Select

                Case KomponTypen.KO_TYPE_STUECK
                    'Parameter
                    Select Case p
                        Case 3, 4, 6
                            Return True
                        Case Else
                            Return False
                    End Select

                Case KomponTypen.KO_TYPE_METER
                    'Parameter
                    Select Case p
                        Case 3, 4
                            Return True
                        Case Else
                            Return False
                    End Select

                Case KomponTypen.KO_TYPE_KNETER, KomponTypen.KO_TYPE_KNETERREZEPT
                    'Kneter-Komponenten haben keine Parameter
                    'Teigruhe im Kneter-Rezept wird in ExtendedParameter angelegt
                    Return False

                Case Else
                    Return False
            End Select
        Else
            'TODO erst mal alles ausgeben
            'abhängig von Parameter-Type
            Select Case t

                Case wb_Global.ktParam.kt200
                    Return True

                ' Nährwerte werden im Datenbank-Feld winback.KomponParams.KT_Rezept freigeschaltet
                Case wb_Global.ktParam.kt301
                    Return True

                Case Else
                    Return True
            End Select
        End If
    End Function

    ''' <summary>
    ''' Fügt, abhängig vom Konfiguration und Komponenten(Parameter)-Type zusätzliche
    ''' Parameter ein.
    ''' 
    '''     Type 101 - Lagerort/Silo
    '''     Type 129 - (Dummy)Paramtersatz Temperatur-Erfassung
    '''     
    ''' </summary>
    ''' <param name="t"></param>
    Private Shared Sub ExtendedParameter(ByRef t As Integer, ByRef p As Integer)

        'abhängig von der Komponenten-Type
        Select Case wb_Functions.IntToKomponType(t)

            'Automatik-Rohstoffe
            Case wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE
                'zusätzlicher Parameter Lagerort/Silo
                AddParameter(t, p, wb_Global.T101_LagerOrt, "Lagerort/Silo", 0, "", "")

            'nach Kneter-Komponente wird ein neuer Parameter-Satz KO_TYPE_KNETER_TEIGRUHE angelegt
            Case wb_Global.KomponTypen.KO_TYPE_KNETER
                'Dummy-Parameter für Teigruhe
                t = wb_Functions.KomponTypeToInt(wb_Global.KomponTypen.KO_TYPE_KNETER_TEIGRUHE)
                'zusätzlicher Parametersatz Teigruhe (Dummy-Kompontype)
                AddParameter(t, p, 0, "Teigruhe", "", "", "")
                AddParameter(t, p, 6, "Teigruhesatz löschen nach [hh:mm:ss]", "hh:mm:ss", "00:00:00", "99:59:59")
                AddParameter(t, p, 8, "Korrektur[Sek/°C]", 0, "Sek/°C", "9999")
                AddParameter(t, p, 10, "Untergrenze Teigruhezeit", "%", "0", "999", False)
                AddParameter(t, p, 11, "Obergrenze Teigruhezeit", "%", "0", "999", False)
                AddParameter(t, p, 12, "Toleranz nach Ende Teigruhe(gelb) [hh:mm:ss]", "hh:mm:ss", "00:00:00", "99:59:59")
                AddParameter(t, p, 13, "Toleranz vor Ablauf Teigruhe(gelb) [hh:mm:ss]", "hh:mm:ss", "00:00:00", "99:59:59")
        End Select
    End Sub

    Private Shared Sub AddParameter(ByRef Typ As Integer, ByRef MaxParamNr As Integer, ParamNr As Integer, Bezeichnung As String, Einheit As String, GWOben As String, GWUnten As String, Optional Used As Boolean = True)
        'neuer Parameter
        Dim k As ktTypXXXParam
        k.Type = Typ
        k.ParamNr = ParamNr
        k.Bezeichnung = Bezeichnung
        k.Einheit = Einheit
        k.Used = Used
        k.eOG = GWOben
        k.eUG = GWUnten
        'zur Liste hinzufügen
        ktTypXXXParams.Add(BuildKey(k.Type, k.ParamNr), k)
        'maximale Parameter-Nummer korrigieren
        MaxParamNr = Math.Max(MaxParamNr, ParamNr)
    End Sub

    ''' <summary>
    ''' Gibt die statischen Werte für den Parameter p zurück
    ''' Aus der Datenbank KomponTypen (KT_Typ_Nr = 300, KT_ParamNr = p)
    '''  - Bezeichnung      Parameter-Bezeichnung (Produktions-Steuerung)
    '''  - Einheit          Einheit
    '''  - Used             Verwendet/Nicht verwendet (Kunden-Spezifisch)
    ''' </summary>
    ''' <param name="p">Interger Parameter-Nummer</param>
    ''' <returns>ktTyp300Param</returns>
    Public Shared Function ktXXXParam(t As Integer, p As Integer) As wb_Global.ktTypXXXParam
        If IsValidParameter(t, p) Then
            Return ktTypXXXParams(BuildKey(t, p))
        Else
            Dim k As New ktTypXXXParam
            k.Bezeichnung = "---"
            k.Used = False
            Return k
        End If
    End Function

    Public Shared Function MaxParam(t As Integer) As Integer
        If _ktTypMaxParams.ContainsKey(t) Then
            Return _ktTypMaxParams(t)
        Else
            Return wb_Global.UNDEFINED
        End If
    End Function

    Public Shared Function IsValidParameter(t As Integer, p As Integer) As Boolean
        Return ktTypXXXParams.ContainsKey(BuildKey(t, p))
    End Function

    Private Shared Function BuildKey(t As Integer, p As Integer) As String
        Return t.ToString("000") & p.ToString("000")
    End Function
End Class
