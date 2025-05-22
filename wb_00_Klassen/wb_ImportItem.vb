
Public Class wb_ImportItem
    Implements IComparable

    Const NNeu = 0
    Const NAlt = 2
    Const Bzng = 1
    Const Indx = 3
    Const kTpe = 4

    Private _FieldData(5) As String

    Public Property NummerNeu As String
        Get
            Return _FieldData(NNeu)
        End Get
        Set(value As String)
            _FieldData(NNeu) = value
        End Set
    End Property

    Public Property NummerAlt As String
        Get
            Return _FieldData(NAlt)
        End Get
        Set(value As String)
            _FieldData(NAlt) = value
        End Set
    End Property


    Public Property Bezeichnung As String
        Get
            Return _FieldData(Bzng)
        End Get
        Set(value As String)
            _FieldData(Bzng) = value
        End Set
    End Property

    Public Property Index As String
        Get
            Return _FieldData(Indx)
        End Get
        Set(value As String)
            _FieldData(Indx) = value
        End Set
    End Property

    Public Property KompType As String
        Get
            Return _FieldData(kTpe)
        End Get
        Set(value As String)
            _FieldData(kTpe) = value
        End Set
    End Property

    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S2365:Properties should not make collection or array copies", Justification:="<Ausstehend>")>
    Public Property FieldData As String()
        Get
            Return _FieldData
        End Get
        Set(value As String())
            _FieldData = value
        End Set
    End Property

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Return String.Compare(NummerNeu, DirectCast(obj, wb_ImportItem).NummerNeu)
    End Function

End Class
