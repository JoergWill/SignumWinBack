Public MustInherit Class wb_nwtCL
    Friend _cnt As Integer

    ''' <summary>
    ''' Anzahl der gefundenen Datensätze
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property cnt As Integer
        Get
            Return _cnt
        End Get
    End Property

    Public Overridable Function GetProductData(id As String) As Integer
    End Function

    Public Overridable Function GetProductData(id As String, ByRef nwtDaten As wb_Komponente) As Integer
    End Function

    Public Overridable Function getProducList() As ArrayList
    End Function

End Class
