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
            If Parameter(index)._Wert IsNot Nothing Then
                Return Parameter(index)._Wert
            Else
                Return ""
            End If
        End Get
        Set(value As String)
            Parameter(index)._Wert = value
        End Set
    End Property

    Public ReadOnly Property Bezeichnung(index As Integer) As String
        Get
            Return kt300Param(index).Bezeichnung
        End Get
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
            If Wert(T300_Backverlust) IsNot Nothing AndAlso Wert(T300_Backverlust) IsNot "" Then
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

    Public Property OfenGruppe As Integer
        Get
            If Wert(T300_Ofengruppe) IsNot Nothing Then
                Return wb_Functions.StrToInt(Wert(T300_Ofengruppe))
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
        Set(value As Integer)
            Wert(T300_Ofengruppe) = value.ToString
        End Set
    End Property

    ''' <summary>
    ''' Update aller geänderten Komponenten-Parameter in Tabelle winback.KomponParams
    '''     RP_Ko_Nr
    '''     RP_Typ_Nr
    '''     RP_ParamNr
    '''     RP_Wert
    '''     RP_Kommentar
    '''     RP_Timestamp
    '''     
    ''' SQL_Anweisung REPLACE INTO KomponParams (KP_Ko_Nr, KP_ParamNr, KP_Wert, KP_Kommentar) VALUES (...)
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(KoNr As Integer, ByRef winback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
        Dim sql As String
        'Result OK
        Dim Result As Boolean = True

        'alle Datensätze im Array durchlaufen
        For i = 0 To maxTyp300
            If IsValidParameter(i) Then
                'Update-Statement wird dynamisch erzeugt
                'REPLACE INTO KomponParams(KP_Ko_Nr, KP_ParamNr, KP_Wert, KP_Kommentar) VALUES (...)
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateRohParams, KoNr & ", 300, " & i.ToString & ", '" & Wert(i) & "', '" & Bezeichnung(i) & "'")

                'Update ausführen
                If winback.sqlCommand(sql) < 0 Then
                    Trace.WriteLine("@E_Fehler bei Update kt300 " & sql)
                    Result = False
                End If
            End If
        Next
        Return Result
    End Function

    ''' <summary>
    ''' Update eines Komponenten-Parameters mit ParamNr in Tabelle winback.RohParams
    ''' </summary>
    ''' <param name="KoNr"></param>
    ''' <param name="ParamNr"></param>
    ''' <param name="winback"></param>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(KoNr As Integer, ParamNr As Integer, ByRef winback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
        Dim sql As String
        'Result OK
        Dim Result As Boolean = True

        'Update-Statement wird dynamisch erzeugt
        'REPLACE INTO RohParams(KP_Ko_Nr, KP_ParamNr, KP_Wert, KP_Kommentar) VALUES (...)
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateRohParams, KoNr & ", 300, " & ParamNr.ToString & ", '" & Wert(ParamNr) & "', '" & Bezeichnung(ParamNr) & "'")

        'Update ausführen
        If winback.sqlCommand(sql) < 0 Then
            Trace.WriteLine("@E_Fehler bei Update kt300 " & sql)
            Result = False
        End If
        Return Result
    End Function


End Class
