Public MustInherit Class wb_nwtCL
    Friend _cnt As Integer

    Public Enum wb_CloudType
        WinBackCloud
        DatenLink
        UNDEFINED
    End Enum

    ''' <summary>
    ''' Anzahl der gefundenen Datensätze
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property cnt As Integer
        Get
            Return _cnt
        End Get
    End Property

    Public Overridable ReadOnly Property CloudType As wb_CloudType
        Get
            Return wb_CloudType.UNDEFINED
        End Get
    End Property

    Public Overridable Function GetProductData(id As String) As Integer
        Return wb_Global.UNDEFINED
    End Function

    Public Overridable Function GetProductData(id As String, ByRef nwtDaten As wb_Komponente) As Integer
        Return wb_Global.UNDEFINED
    End Function

    Public Overridable Function getProducList() As ArrayList
        Return Nothing
    End Function

End Class
