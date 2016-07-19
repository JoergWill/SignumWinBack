Public Class wb_Filiale
    Private pFiliale As ArrayList = Nothing

    ''' <summary>
    ''' Liest aus der Tabelle dbo.Filialen alle Einträge aus. Im Feld dbo.Filiale.Typ wird festgelegt welcher Filial-Typ
    ''' dieser Filiale zugeordnet ist. Ist diese Filiale eine Produktions-Filiale wird Typ=4 (wb_Konfig.ProduktionsFiliale) eigetragen.
    ''' Erzeugt ArrayList pFiliale
    ''' </summary>
    Public Sub New()
        pFiliale = New ArrayList()
        '' TODO Filial-Daten aus Orgasoft.DB lesen
        pFiliale.Add("9999")
    End Sub

    ''' <summary>
    ''' FÜr Unit-Test. ArrayList mit Filial-Nummern füllen
    ''' </summary>
    Public WriteOnly Property AddFiliale As String
        Set(ByVal value As String)
            pFiliale.Add(value)
        End Set
    End Property

    ''' <summary>
    ''' Komma-separierter String enthält eine Filiale, die der Produktion zugeordnet ist
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property isProduction(ByVal FilialNr As String) As Boolean
        Get
            'Filial-String aufteilene in die einzelnen Filialen
            Dim sFiliale() As String = FilialNr.Split(",")

            'alle Filialen aus FilialNr
            For Each sf In sFiliale
                'alle Filialen aus dbo.Filialen.Typ = wb_Konfig.ProduktionsFiliale
                For Each pF In pFiliale
                    If pF = sf Then
                        Return True
                        Exit For
                    End If
                Next
            Next
            Return False
        End Get
    End Property
End Class
