Imports WinBack.wb_Global
Imports WinBack.wb_KomponParam200_Global

Public Class wb_KomponParam200
    Private Structure Typ200
        Public _Wert As String
    End Structure

    Private Parameter(wb_Global.maxTyp200) As Typ200

    Public Sub New()
        Wert(T200_Verkaufsgewicht) = wb_Global.UNDEFINED
        Wert(T200_Warengruppe) = wb_Global.UNDEFINED
    End Sub

    Public Property Wert(index As Integer) As String
        Get
            Return Parameter(index)._Wert
        End Get
        Set(value As String)
            Parameter(index)._Wert = value
        End Set
    End Property

    Public Property Warengruppe As Integer
        Get
            If Wert(T200_Warengruppe) IsNot Nothing Then
                Return wb_Functions.StrToInt(Wert(T200_Warengruppe))
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
        Set(value As Integer)
            Wert(T200_Warengruppe) = value.ToString
        End Set
    End Property

    Public Property Verkaufsgewicht As Double
        Get
            If Wert(T200_Verkaufsgewicht) IsNot Nothing Then
                Return wb_Functions.StrToDouble(Wert(T200_Verkaufsgewicht))
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
        Set(value As Double)
            Wert(T200_Verkaufsgewicht) = value.ToString
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
        For i = 0 To maxTyp200
            If IsValidParameter(i) Then
                'Update-Statement wird dynamisch erzeugt
                'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
                sql = KoNr & ", 200, " & i.ToString & ", '" & Wert(i) & "', '" & kt200Param(i).Bezeichnung & "'"
                'Update ausführen
                If Not winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateRohParams, sql)) Then
                    MySQLdbUpdate = False
                End If
            End If
        Next
    End Function

End Class
