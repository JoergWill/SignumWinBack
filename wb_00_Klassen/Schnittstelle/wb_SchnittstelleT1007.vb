Imports MySql.Data.MySqlClient

Public Class wb_SchnittstelleT1007
    Inherits wb_SchnittstelleTabelle

    Private _OptionSauerteig As Boolean

    Public T1007_Nummer As New wb_SchnittstelleFeld("T1007_RZ_Nr_AlNum", 1)         'Rezeptnummer
    Public T1007_Variante As New wb_SchnittstelleFeld("T1007_RS_RZ_Variante_Nr", 2) 'Rezeptvariante
    Public T1007_Schritt As New wb_SchnittstelleFeld("T1007_RS_Schritt_Nr", 4)      'RezeptSchritt
    Public T1007_Parameter As New wb_SchnittstelleFeld("T1007_RS_ParamNr", 5)       'ParamNummer
    Public T1007_KompNummer As New wb_SchnittstelleFeld("T1007_KO_Nr_AlNum", 6)     'Komponente
    Public T1007_KompType As New wb_SchnittstelleFeld("T1007_KO_Type", 7)           'Type
    Public T1007_Wert As New wb_SchnittstelleFeld("T1007_RS_Wert", 8)               'Sollwert
    Public T1007_Einheit As New wb_SchnittstelleFeld("T1007_E_Einheit", 9)          'Einheit

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides ReadOnly Property TabName As String
        Get
            Return "T1007"
        End Get
    End Property

    Public Overrides ReadOnly Property sql As String
        Get
            If OptionSauerteig Then
                Return wb_sql_Selects.sqlT1007_ST
            Else
                Return wb_sql_Selects.sqlT1007
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property sqlCount As String
        Get
            If OptionSauerteig Then
                Return wb_sql_Selects.sqlT1007_ST_Count
            Else
                Return wb_sql_Selects.sqlT1007_Count
            End If
        End Get
    End Property

    Public Property OptionSauerteig As Boolean
        Get
            Return _OptionSauerteig
        End Get
        Set(value As Boolean)
            _OptionSauerteig = value
        End Set
    End Property

    Public Overrides Sub ImportWorker(winback As wb_Sql)
    End Sub

    ''' <summary>
    ''' In BuildExportString werden alle Datenfelder aus der Datenbank-Tabelle abhängig vom DBIndex (aus der Schnittstellen-Konfig)
    ''' in die jeweiligen Felder einsortiert. 
    ''' Anschliessend werden die Felder nacheinander (sortiert nach Idx) ausgegeben und in die Export-Datei geschrieben
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <param name="Writer"></param>
    Public Overrides Sub ExportWorker(ByRef sqlReader As MySqlDataReader, Writer As System.IO.StreamWriter)
        'Datensatz in Export-File schreiben
        BuildExportString("T1007", sqlReader, Writer)
    End Sub

End Class
