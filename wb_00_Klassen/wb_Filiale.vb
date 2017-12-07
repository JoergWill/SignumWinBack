
''' <summary>
''' Liest aus der Tabelle dbo.Filialen alle Einträge aus. Im Feld dbo.Filiale.Typ wird festgelegt welcher Filial-Typ
''' dieser Filiale zugeordnet ist. Ist diese Filiale eine Produktions-Filiale wird Typ=4 (wb_Konfig.ProduktionsFiliale) eigetragen.
''' Erzeugt ArrayList pFiliale
'''
''' Liest aus der Tabelle dbo.FilialeHatSortiment die Sortiments-Kürzel, die mit einer Filiale vom Typ "Produktion" verknüpft sind.
''' Erzeugt ArrayList pSortiment
''' </summary>
Public Class wb_Filiale

    Private Shared pFiliale As ArrayList = Nothing
    Private Shared pSortiment As ArrayList = Nothing
    Private Shared hFiliale As New SortedList

    ''' <summary>
    ''' Wird automatisch beim Aufruf einer der shared Functions aufgerufen (Shared Object)
    ''' Initialisiert die Felder pFiliale und pSortiment
    ''' </summary>
    Shared Sub New()
        'Wird nur verwendet in Programm-Variante OrgaBack
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            pFiliale = New ArrayList()
            pSortiment = New ArrayList()

            'Datenbank-Verbindung öffnen - MsSQL
            Dim OrgasoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
            Trace.WriteLine("OrgaBackMainConString= " & wb_GlobalSettings.OrgaBackMainConString)
            Dim FNr As String
            Dim FName As String
            Dim Srt As String

            'Daten aus Tabelle Filialen lesen
            If OrgasoftMain.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlFiliale, wb_Global.ProduktionsFiliale)) Then
                While OrgasoftMain.Read
                    FNr = (OrgasoftMain.sField("Filialnummer"))
                    FName = (OrgasoftMain.sField("Name1"))
                    pFiliale.Add(FNr)
                    hFiliale.Add(FNr, FName)
                End While
            End If

            'Kanal wieder schliessen
            OrgasoftMain.CloseRead()

            'Daten aus Tabelle FilialeHatSortiment lesen
            If OrgasoftMain.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlSortiment, wb_Global.ProduktionsFiliale)) Then
                While OrgasoftMain.Read
                    Srt = (OrgasoftMain.sField("SortimentsKürzel"))
                    pSortiment.Add(Srt)
                End While
            End If
        Else
            hFiliale.Add("1", "Bäckerei")
            hFiliale.Add("2", "Konditorei")
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
            If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
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
            Else
                Return True
            End If
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
            Try
                If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
                    'alle Sortiments-Kürzel aus dbo.FilialeHatSortiment
                    For Each sf In pSortiment
                        If SortimentNr = sf Then
                            Return True
                            Exit For
                        End If
                    Next
                    Return False
                Else
                    Return True
                End If
            Catch
                Return False
            End Try
        End Get
    End Property

    Public Shared ReadOnly Property ProduktionsFilialen As SortedList
        Get
            Return hFiliale
        End Get
    End Property

    Public Shared Function IdxProduktionsFiliale(Key As String) As Integer
        IdxProduktionsFiliale = 0
        If Key IsNot Nothing Then
            If hFiliale.ContainsKey(Key) Then
                Return hFiliale.IndexOfKey(Key)
            End If
        End If
    End Function
End Class
