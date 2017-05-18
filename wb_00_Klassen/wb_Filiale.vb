Public Class wb_Filiale
    Private Structure SortimentFiliale
        Public SortimentsKürzel As String
        Public FilialNr As String
    End Structure

    Private Shared pFiliale As ArrayList = Nothing
    Private Shared pSortiment As ArrayList = Nothing


    ''' <summary>
    ''' Liest aus der Tabelle dbo.Filialen alle Einträge aus. Im Feld dbo.Filiale.Typ wird festgelegt welcher Filial-Typ
    ''' dieser Filiale zugeordnet ist. Ist diese Filiale eine Produktions-Filiale wird Typ=4 (wb_Konfig.ProduktionsFiliale) eigetragen.
    ''' Erzeugt ArrayList pFiliale
    ''' 
    ''' Liest aus der Tabelle dbo.FilialeHatSortiment die Sortiments-Kürzel, die mit einer Filiale vom Typ "Produktion" verknüpft sind.
    ''' Erzeugt ArrayList pSortiment
    ''' </summary>


    Shared Sub New()
        pFiliale = New ArrayList()
        pSortiment = New ArrayList()

        'Datenbank-Verbindung öffnen - MsSQL
        Dim OrgasoftMain As New wb_Sql(My.Settings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        Dim FNr As String
        Dim Srt As String

        'Daten aus Tabelle Filialen lesen
        If OrgasoftMain.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlFiliale, wb_Global.ProduktionsFiliale)) Then
            While OrgasoftMain.Read
                FNr = (OrgasoftMain.sField("Filialnummer"))
                pFiliale.Add(FNr)
            End While
        End If

        'Kanal wieder schliessen
        OrgasoftMain.CloseRead()

        'Daten aus Tabelle FilialeHatSortiment lesen
        Dim sf As SortimentFiliale

        If OrgasoftMain.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlSortiment, wb_Global.ProduktionsFiliale)) Then
            While OrgasoftMain.Read
                sf.SortimentsKürzel = (OrgasoftMain.sField("SortimentsKürzel"))
                sf.FilialNr = (OrgasoftMain.sField("FilialNr"))
                pSortiment.Add(sf)
            End While
        End If

    End Sub

    ''' <summary>
    ''' FÜr Unit-Test. ArrayList mit Filial-Nummern füllen
    ''' </summary>
    Public Shared Sub AddFiliale(Value As String)
        pFiliale.Add(Value)
    End Sub

    ''' <summary>
    ''' Gibt True zurück, wenn eine der Filialen den Typ Produktion hat.
    ''' </summary>
    ''' <param name="FilialNr">Komma-separierter String enthält eine Filiale, die der Produktion zugeordnet ist</param>
    ''' <returns>True wenn eine der Filialen der Produktion zugeordnet</returns>
    Public Shared ReadOnly Property FilialeIstProduktion(FilialNr As String) As Boolean
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

    ''' <summary>
    ''' Liefert True zurück, wenn die übergebene Sortiment-Nummer (Kürzel) einer Filiale zugeordnet ist, die den Typ Produktion hat.
    ''' Der entsprechende Artikel wird in WinBack gelistet.
    ''' </summary>
    ''' <param name="SortimentNr"></param>
    ''' <returns>True wenn die Sortiment-Nummer einer Produktions-Filiale zuegordnet ist.</returns>
    Public Shared ReadOnly Property SortimentIstProduktion(SortimentNr As String) As Boolean
        Get
            'alle Sortiments-Kürzel aus dbo.FilialeHatSortiment
            Dim sf As SortimentFiliale
            For Each sf In pSortiment
                If SortimentNr = sf.SortimentsKürzel Then
                    Return True
                    Exit For
                End If
            Next
            Return False
        End Get
    End Property

    Public Shared ReadOnly Property FilialeAusSortiment(SortimentNr As String) As String
        Get
            Dim sf As SortimentFiliale
            For Each sf In pSortiment
                If SortimentNr = sf.SortimentsKürzel Then
                    Return sf.FilialNr
                End If
            Next
            Return ""
        End Get
    End Property
End Class
