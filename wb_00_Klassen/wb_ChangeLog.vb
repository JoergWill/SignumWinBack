Imports WinBack.wb_Global
''' <summary>
''' Schreibt alle Änderungen in einem Objekt in eine dynamische Liste.
''' 
''' </summary>
Public Class wb_ChangeLog
    Private Changes As New System.Collections.Generic.List(Of wb_ChangeLogEintrag)
    Private _ChangeLogAktiv As Boolean = False
    Private _ChangeLogChanged As Boolean = False

    ''' <summary>
    ''' Logging aktiv ja/nein
    ''' </summary>
    ''' <returns>Logging aktiv</returns>
    Protected Property ChangeLogAktiv As Boolean
        Get
            Return _ChangeLogAktiv
        End Get
        Set(value As Boolean)
            _ChangeLogAktiv = value
        End Set
    End Property

    ''' <summary>
    ''' Der letzte Aufruf von ChangeLog hat eine Differenz zwischen altem und neuem Wert ergeben.
    ''' Der alte Wert wurde überschrieben (es hat eine Änderung stattgefunden - Update DB erforderlich.
    ''' </summary>
    ''' <returns></returns>
    Public Property ChangeLogChanged As Boolean
        Get
            Return _ChangeLogChanged
        End Get
        Set(value As Boolean)
            _ChangeLogChanged = value
        End Set
    End Property

    ''' <summary>
    ''' ChangeLog löschen. Mit dem Löschen wird das Logging aktiviert
    ''' </summary>
    Protected Sub ChangeLogClear()
        Changes.Clear()
        _ChangeLogAktiv = True
    End Sub

    ''' <summary>
    ''' Vergleicht die Werte von OldValue und NewValue. Wenn sich die Werte unterscheiden, wird
    ''' ein Eintrag im Change-Log hinzugefügt. Der Änderungs-Report kann über GetLogReport abgerufen werden
    ''' </summary>
    ''' <param name="Typ">WinBack Log-Type (Stammdaten)</param>
    ''' <param name="Prm">Parameter-Nummer</param>
    ''' <param name="OldValue">Ursprünglicher Wert (aus DatenBank/Objekt ...)</param>
    ''' <param name="NewValue">Neuer Wert aus (Cloud/Eingabe)</param>
    ''' <returns>Gibt den neuen Wert zurück</returns>
    Protected Function ChangeLogAdd(Typ As LogType, Prm As Integer, OldValue As String, NewValue As String) As String
        Dim X As wb_Global.wb_ChangeLogEintrag
        'prüfen ob neue Daten vorliegen
        _ChangeLogChanged = (NewValue <> OldValue)
        'nur loggen wenn der alte und der neue Wert sich unterscheiden
        If _ChangeLogAktiv And _ChangeLogChanged Then
            X.Type = Typ
            X.ParamNr = Prm
            X.OldValue = OldValue
            X.NewValue = NewValue
            Changes.Add(X)
        End If
        Return NewValue
    End Function

    ''' <summary>
    ''' Vergleicht die Werte von OldValue und NewValue. Wenn sich die Werte unterscheiden, wird
    ''' ein Eintrag im Change-Log hinzugefügt. Der Änderungs-Report kann über GetLogReport abgerufen werden
    ''' </summary>
    ''' <param name="Typ">WinBack Log-Type (Parameter/Nährwerte)</param>
    ''' <param name="Prm">Parameter-Nummer</param>
    ''' <param name="OldValue">Ursprünglicher Wert (aus DatenBank/Objekt ...)</param>
    ''' <param name="NewValue">Neuer Wert aus (Cloud/Eingabe)</param>
    ''' <param name="Format">Zahlenformat</param>
    ''' <returns>Gibt den neuen Wert zurück</returns>
    Protected Function ChangeLogAdd(Typ As LogType, Prm As Integer, OldValue As Double, NewValue As Double, Optional Format As String = "{0,8:##0.000}") As Double
        Me.ChangeLogAdd(Typ, Prm, String.Format(Format, OldValue), String.Format(Format, NewValue))
        Return NewValue
    End Function

    ''' <summary>
    ''' Vergleicht die Werte von OldValue und NewValue. Wenn sich die Werte unterscheiden, wird
    ''' ein Eintrag im Change-Log hinzugefügt. Der Änderungs-Report kann über GetLogReport abgerufen werden
    ''' </summary>
    ''' <param name="Typ">WinBack Log-Type (Allergene)</param>
    ''' <param name="Prm">Parameter-Nummer</param>
    ''' <param name="OldValue">Ursprünglicher Wert (aus DatenBank/Objekt ...)</param>
    ''' <param name="NewValue">Neuer Wert aus (Cloud/Eingabe)</param>
    ''' <returns>Gibt den neuen Wert zurück</returns>
    Protected Function ChangeLogAdd(Typ As LogType, Prm As Integer, OldValue As AllergenInfo, NewValue As AllergenInfo) As AllergenInfo
        Me.ChangeLogAdd(Typ, Prm, wb_Functions.AllergenToString(OldValue), wb_Functions.AllergenToString(NewValue))
        Return NewValue
    End Function

    ''' <summary>
    ''' Gibt alle Änderungen seit dem letzten ChangeLogClear aus.
    ''' Per Default werden nur die in der Datenbank aktivierten Parameter ausgegeben.
    ''' Nach der Ausgabe wird das Logging deaktiviert.
    ''' </summary>
    ''' <param name="ReportAll">Gibt alle Änderungen aus, auch die nicht aktiven Parameter</param>
    ''' <returns>(String) Änderungen</returns>
    Protected Function ChangeLogReport(Optional ReportAll As Boolean = vbFalse) As String
        Dim x As wb_ChangeLogEintrag
        Dim s As String = ""
        Dim UeberschriftAllergene As Boolean = False
        Dim UeberschriftNaehrwert As Boolean = False

        For Each x In Changes
            Select Case x.Type

                'Allergene
                Case LogType.Alg
                    If wb_KomponParam301_Global.kt301Param(x.ParamNr).Used Or ReportAll Then
                        'in der ersten Zeile eine Überschrift drucken
                        If Not UeberschriftAllergene Then
                            s += vbCrLF & "Allergene [alt/neu]" & vbCrLF
                            UeberschriftAllergene = True
                        End If
                        s += x.OldValue + "/" + x.NewValue + " " + wb_KomponParam301_Global.kt301Param(x.ParamNr).Bezeichnung + vbCrLF
                    End If

                'Nährwerte
                Case wb_Global.LogType.Nrw
                    If wb_KomponParam301_Global.kt301Param(x.ParamNr).Used Or ReportAll Then
                        'in der ersten Zeile eine Überschrift drucken
                        If Not UeberschriftNaehrwert Then
                            s += vbCrLF & "Nährwerte [alt/neu]" & vbCrLF
                            UeberschriftNaehrwert = True
                        End If
                        s += x.OldValue + " " + wb_KomponParam301_Global.kt301Param(x.ParamNr).Einheit + "/"
                        s += x.NewValue + " " + wb_KomponParam301_Global.kt301Param(x.ParamNr).Einheit + " "
                        s += wb_KomponParam301_Global.kt301Param(x.ParamNr).Bezeichnung + vbCrLF
                    End If

                'Deklarationsbezeichnung
                Case wb_Global.LogType.Dkl
                    Select Case x.ParamNr
                        Case Parameter.Tx_DeklarationExtern
                            s += vbCrLF & "Deklaration [alt]" & vbCrLF
                            s += x.OldValue + vbCrLF
                            s += vbCrLF & "Deklaration [neu]" & vbCrLF
                            s += x.NewValue + vbCrLF
                        Case Parameter.Tx_Mehlzusammensetzung
                            s += vbCrLF & "Mehl-Zusammensetzung [alt] " & x.OldValue
                            s += vbCrLF & "Mehl-Zusammensetzung [neu] " & x.NewValue & vbCrLF
                    End Select

                'Parameter
                Case wb_Global.LogType.Prm
                    Select Case x.ParamNr
                        Case Parameter.Tx_Bezeichnung
                            s += vbCrLF & "Bezeichnung [alt] " & x.OldValue
                            s += vbCrLF & "Bezeichnung [neu] " & x.NewValue & vbCrLF

                        Case Parameter.Tx_Lieferant
                            s += vbCrLF & "Lieferant [alt] " & x.OldValue
                            s += vbCrLF & "Lieferant [neu] " & x.NewValue & vbCrLF
                    End Select

                'Fehler
                Case wb_Global.LogType.Err
                    s += vbCrLF & "Fehler " & x.NewValue
                'Hinweis
                Case wb_Global.LogType.Msg
                    s += vbCrLF & "Hinweis " & x.NewValue

                Case Else
                    If ReportAll Then
                        s += x.OldValue + "/" + x.NewValue + vbCrLF
                    End If
            End Select
        Next
        _ChangeLogAktiv = False
        Return s
    End Function
End Class
