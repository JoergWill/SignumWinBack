﻿Imports WinBack.wb_Global
Imports WinBack.wb_KomponParam220_Global

Public Class wb_KomponParam220
    Private Structure Typ220
        Public _Wert As String
    End Structure

    Private Parameter(wb_Global.maxTyp220) As Typ220

    Public Sub New()
    End Sub

    Public Property Wert(index As Integer) As String
        Get
            Return Parameter(index)._Wert
        End Get
        Set(value As String)
            Parameter(index)._Wert = value
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
        For i = 0 To maxTyp220
            If IsValidParameter(i) Then
                'Update-Statement wird dynamisch erzeugt
                'REPLACE INTO RohParams (RP_Ko_Nr, RP_Typ_Nr, RP_ParamNr, RP_Wert, RP_Kommentar) VALUES (...)
                sql = KoNr & ", 220, " & i.ToString & ", '" & Wert(i) & "', '" & kt220Param(i).Bezeichnung & "'"
                'Update ausführen
                If Not winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateRohParams, sql)) Then
                    MySQLdbUpdate = False
                End If
            End If
        Next
    End Function
End Class