Imports WinBack.wb_Functions
Imports WinBack.wb_Global
Imports WinBack.wb_KomponParam300_Global

Public Class wb_KomponParam300

    Private Structure Typ300
        Public _Wert As String
    End Structure

    Private Parameter(wb_Global.maxTyp300) As Typ300

    Public Sub New()
        Wert(T300_LinienGruppe) = wb_Global.UNDEFINED
        Wert(T300_RzNr) = wb_Global.UNDEFINED
    End Sub

    Public Property Wert(index As Integer) As String
        Get
            Return Parameter(index)._Wert
        End Get
        Set(value As String)
            Parameter(index)._Wert = value
        End Set
    End Property

    Public Property Liniengruppe As Integer
        Get
            If Wert(T300_LinienGruppe) IsNot Nothing Then
                Return wb_Functions.StrToInt(Wert(T300_LinienGruppe))
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
        Set(value As Integer)
            Wert(T300_LinienGruppe) = value.ToString
        End Set
    End Property

    Public ReadOnly Property RzNr As Integer
        Get
            If Wert(T300_RzNr) IsNot Nothing Then
                Return wb_Functions.StrToInt(Wert(T300_RzNr))
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
    End Property

    Public Property Backverlust As Double
        Get
            If Wert(T300_Backverlust) IsNot Nothing Then
                Return wb_Functions.StrToDouble(Wert(T300_Backverlust))
            Else
                Return 0.0
            End If
        End Get
        Set(value As Double)
            Wert(T300_Backverlust) = value.ToString
        End Set
    End Property

    Public Property Zuschnitt As Double
        Get
            If Wert(T300_Zuschnitt) IsNot Nothing Then
                Return wb_Functions.StrToDouble(Wert(T300_Zuschnitt))
            Else
                Return 0.0
            End If
        End Get
        Set(value As Double)
            Wert(T300_Zuschnitt) = value.ToString
        End Set
    End Property

    ''' <summary>
    ''' Update aller geänderten Komponenten-Parameter in Tabelle winback.RohParams
    '''     RP_Ko_Nr
    '''     RP_Typ_Nr
    '''     RP_ParamNr
    '''     RP_Wert
    '''     RP_Kommentar
    '''     RP_Timestamp
    '''     
    ''' SQL_Anweisung REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(KoNr As Integer, ByRef winback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
        Dim sql As String
        'Result OK
        MySQLdbUpdate = True

        'alle Datensätze im Array durchlaufen
        For i = 0 To maxTyp300
            If IsValidParameter(i) Then
                'Update-Statement wird dynamisch erzeugt
                'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateRohParams, KoNr & ", 300, " & i.ToString & ", '" & Wert(i) & "', '" & kt300Param(i).Bezeichnung & "'")
                'Update ausführen
                If winback.sqlCommand(sql) < 0 Then
                    Trace.WriteLine("@E_Fehler bei Update kt300 " & sql)
                    MySQLdbUpdate = False
                Else
                    Debug.Print("@I_Update kt300 " & sql)
                End If
            End If
        Next
    End Function

End Class
