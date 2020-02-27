Public Class wb_User_Rechte_Shared

    Private Shared GrpAttribute As New List(Of wb_Global.wb_GrpAttr)

    Shared Sub New()
        'Daten initialisieren
        Load_UserGruppenRechteItems()
    End Sub

    ''' <summary>
    ''' Gibt zur Rechts-Type (401..406) und dem entsprechenden Wert den passenden Text aus.
    ''' </summary>
    ''' <param name="iRechteTyp"></param>
    ''' <param name="iWert"></param>
    ''' <returns></returns>
    Public Shared ReadOnly Property Text(iRechteTyp As Integer, iWert As Integer) As String
        Get
            For Each a As wb_Global.wb_GrpAttr In GrpAttribute
                If a.Attr = iRechteTyp And a.Wert = iWert Then
                    Return a.Text
                End If
            Next
            Return ""
        End Get
    End Property

    ''' <summary>
    ''' Click auf das entsprechende Feld in der Rechte-Matrix. Der Wert wird, abhängig vom Rechte-Typ 
    ''' getoggelt bzw. erhöht.
    ''' </summary>
    ''' <param name="iRechteTyp"></param>
    ''' <param name="iWert"></param>
    ''' <returns></returns>
    Public Shared Function Click(iRechteTyp As Integer, iWert As Integer) As Integer
        'Debug.Print("    Click " & iRechteTyp.ToString & "/" & iWert.ToString)
        Dim iFirst As Integer = wb_Global.UNDEFINED
        Dim iNext As Integer = wb_Global.UNDEFINED
        'durchläuft alle Atrribute der Gruppen-Rechte (402...405)
        For Each a As wb_Global.wb_GrpAttr In GrpAttribute
            If a.Attr = iRechteTyp Then
                'erster Wert aus der Gruppe(IRechteTyp)
                If iFirst = wb_Global.UNDEFINED Then
                    iFirst = a.Wert
                End If
                'nächsthöherer Wert 
                If iWert < a.Wert And iNext = wb_Global.UNDEFINED Then
                    iNext = a.Wert
                End If
            End If
        Next
        'Wenn der nächsthöhere Wert undefiniert ist, wird der erste Wert aus der Kette zurückgegeben
        If iNext = wb_Global.UNDEFINED Then
            Return iFirst
        Else
            Return iNext
        End If
    End Function

    ''' <summary>
    ''' Array aller Rechte-Gruppe Attribute (403..405) mit den entsprechenden möglichen Werten erstellen
    ''' </summary>
    Private Shared Sub Load_UserGruppenRechteItems()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        If winback.sqlSelect(wb_Functions.SetParams(wb_Sql_Selects.sqlUserRechteParam, wb_Language.GetLanguageNr())) Then
            While winback.Read
                Dim AttrText As wb_Global.wb_GrpAttr
                AttrText.Attr = winback.iField("T_Typ")
                AttrText.Text = winback.sField("T_Text")
                AttrText.Wert = wb_Functions.StrToInt(winback.sField("AT_Wert3str"))
                'Eindimensionale Liste aufbauen
                GrpAttribute.Add(AttrText)
                'Debug.Print("UserGruppenRechte " & AttrText.Attr & "/" & AttrText.Wert & "/" & AttrText.Text)
            End While
        End If
    End Sub
End Class

