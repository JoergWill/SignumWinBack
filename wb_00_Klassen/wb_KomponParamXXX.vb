﻿Public Class wb_KomponParamXXX
    Private Structure TypXXX
        Public _ParamNr As Integer
        Public _Wert As String
    End Structure

    Private Parameter(wb_Global.maxTypXXX) As TypXXX

    Public Property Wert(index As Integer) As String
        Get
            If index < wb_Global.maxTypXXX Then
                Return Parameter(index)._Wert
            Else
                Return ""
            End If
        End Get
        Set(value As String)
            If index < wb_Global.maxTypXXX Then
                Parameter(index)._Wert = value
            End If
        End Set
    End Property

    Public Property ParamNr(index As Integer) As Integer
        Get
            Return Parameter(index)._ParamNr
        End Get
        Set(value As Integer)
            Parameter(index)._ParamNr = value
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
    ''' SQL_Anweisung REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(KoNr As Integer, ByRef winback As wb_Sql) As Boolean
        'Update-Statement wird dynamisch erzeugt    
        Dim sql As String
        'Result OK
        MySQLdbUpdate = True

        ''alle Datensätze im Array durchlaufen
        'For i = 0 To maxTyp200
        '    If IsValidParameter(i) Then
        '        'Update-Statement wird dynamisch erzeugt
        '        'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
        '        sql = KoNr & ", 200, " & i.ToString & ", '" & Wert(i) & "', '" & kt200Param(i).Bezeichnung & "'"
        '        'Update ausführen
        '        If Not winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateRohParams, sql)) Then
        '            MySQLdbUpdate = False
        '        End If
        '    End If
        'Next
    End Function
End Class