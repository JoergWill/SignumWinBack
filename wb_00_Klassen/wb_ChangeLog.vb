Imports WinBack.wb_Global
''' <summary>
''' Schreibt alle Änderungen in einem Objekt in eine dynamische Liste.
''' 
''' </summary>
Public Class wb_ChangeLog
    Private Changes As New System.Collections.Generic.List(Of wb_ChangeLogEintrag)
    Private _ChangeLogAktiv As Boolean = False

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
        'nur loggen wenn der alte und der neue Wert sich unterscheiden
        If _ChangeLogAktiv And (NewValue <> OldValue) Then
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
    ''' Nach der Ausgabe wird das Logging deaktiviert
    ''' </summary>
    ''' <returns>(String) Änderungen</returns>
    Protected Function ChangeLogReport() As String
        Dim x As wb_Global.wb_ChangeLogEintrag
        Dim s As String = ""
        For Each x In Changes
            Select Case x.Type

                'Allergene
                Case wb_Global.LogType.Alg
                    If wb_Functions.kt301Param(x.ParamNr).Used Then
                        s += x.OldValue + "/" + x.NewValue + " " + wb_Functions.kt301Param(x.ParamNr).Bezeichnung + vbNewLine
                    End If

                'Nährwerte
                Case wb_Global.LogType.Nrw
                    If wb_Functions.kt301Param(x.ParamNr).Used Then
                        s += x.OldValue + " " + wb_Functions.kt301Param(x.ParamNr).Einheit + "/"
                        s += x.NewValue + " " + wb_Functions.kt301Param(x.ParamNr).Einheit + " "
                        s += wb_Functions.kt301Param(x.ParamNr).Bezeichnung + vbNewLine
                    End If

                Case Else
                    s += x.OldValue + "/" + x.NewValue + vbNewLine
            End Select
        Next
        _ChangeLogAktiv = False
        Return s
    End Function
End Class
