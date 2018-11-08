Imports System.IO
Imports WinBack
''' <summary>
''' Rezeptur für Kocher/Röster
'''  - Änderungsdatum
'''  - Rezept in Textfile schreiben
'''  - Rezept aus Textfile lesen
'''  - Master-Rezept (Schritte/Komponenten)
'''  - Status
'''     - Aktuell
'''     - Schreiben
'''     - Lesen
'''     - Neu
'''     - ...
''' </summary>
Public Class wb_Kocher_Rezept
    Private _Nummer As Integer = wb_Global.UNDEFINED
    Private _Filename As String = Nothing
    Private _Aenderungsdatum As DateTime
    Private _WinBackRzNr As Integer = wb_Global.UNDEFINED
    Private _WinBackRzName As String
    Private _SyncStatus As wb_Global.Kocher_SyncStatus
    Private _Rezeptur(wb_Global.KocherMaxSchritte) As wb_Kocher_RezeptSchritt

    Public Property Nummer As Integer
        Get
            Return _Nummer
        End Get
        Set(value As Integer)
            _Nummer = value
        End Set
    End Property

    Public Property WinBackRzNummer As String
        Set(value As String)
            If _Nummer = wb_Global.UNDEFINED Then
                _Nummer = wb_Functions.StrToInt(value.Remove(0, Len(wb_Global.KocherPreFix)))
            End If
        End Set
        Get
            If _Nummer <> wb_Global.UNDEFINED Then
                Return wb_Global.KocherPreFix & _Nummer.ToString
            Else
                Return wb_Global.KocherPreFix
            End If
        End Get
    End Property

    Private Property WinBackRzNr As Integer
        Get
            Return _WinBackRzNr
        End Get
        Set(value As Integer)
            _WinBackRzNr = value
        End Set
    End Property

    Public Property Filename As String
        Get
            If _Filename = Nothing Then
                _Filename = GetFileNameFromRzptNr()
            End If
            Return _Filename
        End Get
        Set(value As String)
            _Filename = value
            _Nummer = GetRzptNrFromFileName()
        End Set
    End Property

    Public Property Aenderungsdatum As Date
        Get
            Return _Aenderungsdatum
        End Get
        Set(value As Date)
            _Aenderungsdatum = value
        End Set
    End Property

    Public Property SyncStatus As wb_Global.Kocher_SyncStatus
        Get
            Return _SyncStatus
        End Get
        Set(value As wb_Global.Kocher_SyncStatus)
            _SyncStatus = value
        End Set
    End Property

    Private Function GetRzptNrFromFileName() As String
        Dim RzptNr() As String = _Filename.Substring(1).Split(".")
        Return RzptNr(0)
    End Function

    Private Function GetFileNameFromRzptNr() As String
        Return "R" & _Nummer & ".0.rzp"
    End Function

    ''' <summary>
    ''' Liest die Rezeptur aus der Kocher-Rezeptdatei
    ''' </summary>
    Public Sub TxtReadRezept()
        'Text-Datei öffnen
        Dim TxtReader As New StreamReader(wb_GlobalSettings.pKocherPath & Filename, System.Text.Encoding.ASCII)

        'Rezeptdatei zeilenweise auslesen
        Dim Line As String = ""
        Dim i As Integer = 0
        Dim Schritt As Integer

        'Schleife über alle Zeilen
        Do
            'zeilenweise auslesen
            Line = TxtReader.ReadLine
            'Leere Zeilen mit <CRLF> auslassen
            If Line <> "" Then
                'Zähler Zeilen
                i += 1
                'Rezeptschritt aus Index
                Schritt = wb_Kocher_Global.IdxToSchritt(i)
                'Parameter eintragen zu diesem Index(Schritt und Parameter-Nummer)
                If _Rezeptur(Schritt) Is Nothing Then
                    _Rezeptur(Schritt) = New wb_Kocher_RezeptSchritt
                End If
                _Rezeptur(Schritt).Index = i
                _Rezeptur(Schritt).Parameter = Line
            End If
        Loop Until TxtReader.EndOfStream

        'Datei wieder schliessen
        TxtReader.Close()
    End Sub

    ''' <summary>
    ''' Schreibt die Rezeptur in die Kocher-Rezeptdatei
    ''' </summary>
    Public Sub TxtWriteRezept()
        'Text-Datei öffnen
        Dim fs As New FileStream(wb_GlobalSettings.pKocherPath & Filename, FileMode.Create)
        Dim TxtWriter As New StreamWriter(fs, System.Text.Encoding.ASCII)

        'Rezeptdatei zeilenweise schreiben
        Dim Line As String = ""
        Dim i As Integer = 0
        Dim Schritt As Integer

        'Schleife über alle Schritte
        For i = 1 To wb_Global.KocherMaxSchritte * wb_Global.Kocher_IdxTeiler
            'Rezeptschritt aus Index
            Schritt = wb_Kocher_Global.IdxToSchritt(i)
            _Rezeptur(Schritt).Index = i
            'Zeile schreiben
            TxtWriter.WriteLine(_Rezeptur(Schritt).Parameter)
            TxtWriter.WriteLine()
        Next

        'Textfile schliessen
        TxtWriter.Close()
        fs.Dispose()
    End Sub


    ''' <summary>
    ''' Update der Kocher-Rezeptur in der WinBack-Datenbank. Wenn das Rezept in WinBack noch
    ''' nicht vorhanden ist, wird die Hülle aus einem Archiv-Muster-Rezept kopiert und anschliessend
    ''' die entsprechenden Parameter eingefügt.
    ''' 
    ''' Bei bestehenden Rezepten werden die WinBack-spezifischen Parameter (Handzugabe...) beibehalten
    ''' und mit entsprechenden Sollwerten verknüpft.
    ''' Die Rezepte sind mit Liniengruppe 98(Kocher) und Linie 98 verknüpft.
    ''' 
    ''' </summary>
    ''' <param name="Create"></param>
    Public Sub DBUpdateRezept(Create As Boolean)
        'Rezeptur lesen/erzeugen
        Dim Rzpt As New wb_Rezept(WinBackRzNummer, wb_Global.KocherLinienGruppe)

        'Rezept muss neu angelegt werden - wenn schon ein Rezept vorhanden ist löschen
        If Create Then
            Rzpt.MySQLdbDelete_HisRezept()
            Rzpt.MySQLdbDelete_HisRezeptSchritte()
            Rzpt.MySQLdbDelete_RezeptSchritte()
        End If

        'Wenn keine Rezeptschritte vorhanden sind, Rezepthülle von HisRezepte kopieren
        If Rzpt.RootRezeptSchritt.ChildSteps.Count = 0 Then
            'Liest alle Rezeptschritte aus HisRezepte(Nr -98/Variante 1/ÄnderungsIndex 0)
            Rzpt.MySQLdbSelect_RzSchritt(wb_Global.Kocher_HisRzNr, wb_Global.Kocher_HisVrnt, wb_Global.Kocher_HisAend)
        End If

        'Rezept-Bezeichung (aus Schritt 0)
        Rzpt.RezeptBezeichnung = _Rezeptur(0).RzptBezeichnung

        'Update Rezeptschritte - Abgleich der Rezeptschritte mit der Rezepthülle














    End Sub


End Class

