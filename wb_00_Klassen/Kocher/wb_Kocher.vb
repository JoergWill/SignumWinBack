Imports System.IO
Imports System.Net
Imports FluentFTP
Imports WinBack

Public Class wb_Kocher
    Private _Nummer As Integer
    Private _IP_Adresse As String
    Private _VerbindungsStatus As wb_Global.Kocher_VerbindungsStatus = wb_Global.Kocher_VerbindungsStatus.UNDEF
    Private _RezeptListe As New Hashtable

    Public Sub New()
        FtpTrace.LogFunctions = False
    End Sub

    ''' <summary>
    ''' Laufende Nummer des Kochers.
    ''' Identifiziert den Kocher in der Liste
    ''' </summary>
    ''' <returns></returns>
    Public Property Nummer As Integer
        Get
            Return _Nummer
        End Get
        Set(value As Integer)
            _Nummer = value
        End Set
    End Property

    ''' <summary>
    ''' Gibt die komplette IP_Adresse des Kochers zurück. Gesetzt wird nur das letzte Byte der IP-Adresse (BC9000Liste.BC9_IpAdresse)
    ''' </summary>
    ''' <returns></returns>
    Public Property IP_Adresse As String
        Get
            Return wb_GlobalSettings.IPBasisAdresse & "." & _IP_Adresse
        End Get
        Set(value As String)
            _IP_Adresse = value
        End Set
    End Property

    ''' <summary>
    ''' Aktueller Verbindungs-Status des Kochers
    '''     OK      - Kocher ist im Netzwerk erreichbar. Nicht verbunden
    '''     CONNECT - Kocher ist verbunden(aktiv)
    '''     ERR     - Kocher nicht im Netzwerk erreichbar (ausgeschaltet/ausgesteckt)
    ''' </summary>
    ''' <returns></returns>
    Public Property VerbindungsStatus As wb_Global.Kocher_VerbindungsStatus
        Get
            Return _VerbindungsStatus
        End Get
        Set(value As wb_Global.Kocher_VerbindungsStatus)
            _VerbindungsStatus = value
        End Set
    End Property

    Public Property RezeptListe As Hashtable
        Get
            Return _RezeptListe
        End Get
        Set(value As Hashtable)
            _RezeptListe = value
        End Set
    End Property

    ''' <summary>
    ''' Prüft die Verbindung zum Kocher und liest das Verzeichnis aller Rezepte inklusive Timestamp ein.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRezeptListe() As wb_Global.Kocher_VerbindungsStatus
        'Master(Server)
        If Nummer = wb_Global.KocherMaster Then

            'Prüfen ob das Verzeichis für die lokalen Kocher-Rezepte existiert
            If Not Directory.Exists(wb_GlobalSettings.pKocherPath) Then
                'wenn nicht, jetzt erstellen...
                Try
                    Directory.CreateDirectory(wb_GlobalSettings.pKocherPath)
                    ' Ordner wurde korrekt erstellt!
                Catch ex As Exception
                    VerbindungsStatus = wb_Global.Kocher_VerbindungsStatus.ERR
                    ' Ordner wurde nicht erstellt
                    MsgBox(ex.Message)
                    Return VerbindungsStatus
                End Try
            End If

            'Die Master-Rezepte liegen im lokalen Verzeichnis
            Dim d As DirectoryInfo = New DirectoryInfo(wb_GlobalSettings.pKocherPath)
            For Each f As FileInfo In d.GetFiles("*.rzp")
                'Rezeptinfo aus der Verzeichnisliste(DIR) dekodieren
                GetRezeptInfo(f)
            Next
            'Das lokale Verzeichnis ist immer verbunden
            VerbindungsStatus = wb_Global.Kocher_VerbindungsStatus.OK

        Else
            Try
                'Ftp-Verbindung zum Kocher
                Dim client As New FtpClient(IP_Adresse)
                client.Credentials() = New NetworkCredential(wb_Credentials.FTPUserAnonymous, wb_Credentials.FTPPassAnonymous)
                'Verbindung öffnen
                client.Connect()
                VerbindungsStatus = wb_Global.Kocher_VerbindungsStatus.CONNECT

                'Verzeichnis per Ftp holen
                Dim DirList() As FtpListItem = client.GetListing("*.rzp")
                For Each f As FtpListItem In DirList
                    GetRezeptInfo(f)
                Next

                'Verbindung wieder schliessen
                client.Disconnect()
                VerbindungsStatus = wb_Global.Kocher_VerbindungsStatus.OK
            Catch
                VerbindungsStatus = wb_Global.Kocher_VerbindungsStatus.ERR
            End Try
        End If

        'Gibt OK zurück wenn der Kocher verbunden ist
        Return VerbindungsStatus
    End Function

    Public Sub GetRezeptInfo(f As FileInfo)
        Dim ZeitStempel As DateTime = f.LastWriteTime
        Dim FileName As String = f.Name

        'zum HashTable hinzufügen
        AddCheckHashTable(FileName, ZeitStempel)
    End Sub

    Public Sub GetRezeptInfo(f As FtpListItem)
        Dim ZeitStempel As DateTime = f.Modified
        Dim FileName As String = f.Name

        'zum HashTable hinzufügen
        AddCheckHashTable(FileName, ZeitStempel)
    End Sub

    Private Sub AddCheckHashTable(FileName As String, ZeitStempel As DateTime)
        'Rezeptnummer aus Filename
        Dim Rzpt As New wb_Kocher_Rezept
        Rzpt.Filename = FileName

        Debug.Print("Kocher-Nummer " & Nummer)
        Debug.Print(" Rezeptnummer " & Rzpt.Nummer)
        Debug.Print(" DateTime " & ZeitStempel.ToString)

        'Rezept in HashTable einordnen
        If _RezeptListe.ContainsKey(Rzpt.Nummer) Then
            'Eintrag in Liste gefunden
            Rzpt = _RezeptListe(Rzpt.Nummer)
        Else
            'zu Liste hinzufügen
            _RezeptListe.Add(Rzpt.Nummer, Rzpt)
        End If

        'Änderungsdatum aktualisieren (Status wird automatisch angepasst)
        Rzpt.Aenderungsdatum = ZeitStempel
    End Sub

    ''' <summary>
    ''' Vergleicht die Rezeptliste mit dem Master-Rezept(Server)
    ''' 
    ''' Ausgehend von der eigenen Liste wird gegen das Verzeichnis auf dem Server(Master) geprüft.
    ''' Der entsprechende Status wird im Rezept eingetragen und die Aktion ausgelöst.
    ''' 
    '''  - Rezept auf Master nicht vorhanden        - Status.CopyNewFileToMaster
    '''     
    '''  - Zeitstempel identisch                    - Status.Ok
    '''  - Zeitstempel Master älter als Kocher      - Status.CopyFileToMaster
    '''  - Zeitstempel Master neuer als Kocher      - Status.CopyFileFromMaster
    ''' 
    ''' </summary>
    ''' <param name="cmpMaster"></param>
    Public Sub SyncToMaster(cmpMaster As wb_Kocher)
        ' der Master-Kocher muss definiert sein
        If cmpMaster IsNot Nothing And cmpMaster.VerbindungsStatus = wb_Global.Kocher_VerbindungsStatus.OK Then
            'alle Rezepte in der eigenen Liste
            For Each Rzpt As wb_Kocher_Rezept In RezeptListe.Values

                'Rezept in der Liste des anderen Kochers/Rösters vorhanden
                If cmpMaster.RezeptListe.ContainsKey(Rzpt.Nummer) Then
                    'Rezeptnummer gefunden
                    Dim cmpRzpt As wb_Kocher_Rezept = cmpMaster.RezeptListe(Rzpt.Nummer)

                    'Zeitstempel vergleichen
                    If Rzpt.Aenderungsdatum.AddMinutes(wb_Global.KocherSyncToleranz) > cmpRzpt.Aenderungsdatum Then
                        'eigene Rezeptur ist aktueller - Datei per FTP in Master-Verzeichnis kopieren
                        Rzpt.SyncStatus = wb_Global.Kocher_SyncStatus.CopyToMaster
                        SyncFTP(Rzpt)
                        'Status im Master-Rezept anpassen
                        cmpMaster.RezeptListe(Rzpt.Nummer).SyncStatus = Rzpt.SyncStatus

                    ElseIf Rzpt.Aenderungsdatum.AddMinutes(wb_Global.KocherSyncToleranz) < cmpRzpt.Aenderungsdatum Then
                        'eigene Rezeptur ist älter - von Master in das eigene Verzeichnis kopieren
                        Rzpt.SyncStatus = wb_Global.Kocher_SyncStatus.CopyFromMaster
                        SyncFTP(Rzpt)

                    Else
                        'beide Rezepte sind identisch - keine Aktion erforderlich
                        Rzpt.SyncStatus = wb_Global.Kocher_SyncStatus.Ok
                    End If
                Else
                    'Rezept-Nummer im Master-Verzeichnis nicht gefunden - Datei per FTP in Master-Verzeichnis kopieren
                    Rzpt.SyncStatus = wb_Global.Kocher_SyncStatus.CopyNewFileToMaster
                    'Rezept in Master-Liste aufnehmen
                    cmpMaster.RezeptListe.Add(Rzpt.Nummer, Rzpt)
                    SyncFTP(Rzpt)
                End If
            Next

            'alle Rezepte auf dem Master
            For Each Rzpt As wb_Kocher_Rezept In cmpMaster.RezeptListe.Values
                'Prüfen ob das Rezept auf dem Slave vorhanden fehlt
                If Not RezeptListe.Contains(Rzpt.Nummer) Then
                    Rzpt.SyncStatus = wb_Global.Kocher_SyncStatus.CopyNewFileFromMaster
                    SyncFTP(Rzpt)
                End If
            Next

        End If
    End Sub

    ''' <summary>
    ''' Kopiert die Rezept-Files per FTP vom Server zum Kocher oder vom Kocher zum Server. Wenn der Kopier-Vorgang
    ''' erfolgreich war, wird der Status auf OK gesetzt.
    ''' 
    ''' Da bei Win-CE der MFMT-Befehl zum Manipulieren des Timestamp nicht funktioniert, kann es bei mehreren Kochern
    ''' Probleme mit der Synchronisation geben: Der Timestamp auf dem Kocher ist immer der aktuelle Zeitpunkt des 
    ''' Schreibens auf das Panel, sicherheitshalber wird dann der Timestamp auf dem Server auch manipuliert, so dass
    ''' in der Folge alle Kocher aktualisiert werden.
    ''' Ist einer der Kocher zum Sync-Zeitpunkt nicht am Netz, werden die Änderungen erst später auf diesen Kocher 
    ''' übertragen. Änderungen, die offline auf diesem Kocher genmacht wurden, werden überschrieben.
    ''' </summary>
    ''' <param name="Rzpt"></param>
    Private Sub SyncFTP(ByRef Rzpt As wb_Kocher_Rezept)

        Dim client As New FtpClient(IP_Adresse)
        client.Credentials() = New NetworkCredential(wb_Credentials.FTPUserAnonymous, wb_Credentials.FTPPassAnonymous)
        'Verbindung öffnen
        client.Connect()

        'Upload/Download abhängig vom Rezept-Status
        Select Case Rzpt.SyncStatus

           'Upload File vom Master zum Kocher (ftp-Upload)
            Case wb_Global.Kocher_SyncStatus.CopyFromMaster, wb_Global.Kocher_SyncStatus.CopyNewFileFromMaster
                'ftp-Upload
                client.UploadFile(wb_GlobalSettings.pKocherPath & Rzpt.Filename, "/" & Rzpt.Filename)
                'Timestamp auf Master korrigieren - da MFMT auf Slave nicht funktioniert)
                FileSetTimestamp(Rzpt.Filename, Now)
                'Rezept-Sync-Status korrigieren
                Rzpt.SyncStatus = wb_Global.Kocher_SyncStatus.Ok

            'Download File vom Kocher zum Master (ftp-Download)
            Case wb_Global.Kocher_SyncStatus.CopyToMaster, wb_Global.Kocher_SyncStatus.CopyNewFileToMaster
                'ftp-Download
                client.DownloadFile(wb_GlobalSettings.pKocherPath & Rzpt.Filename, "/" & Rzpt.Filename)
                'Timestamp auf Master korrigieren
                FileSetTimestamp(Rzpt.Filename, Rzpt.Aenderungsdatum)

        End Select

        'Verbindung schliessen
        client.Disconnect()
    End Sub

    Private Sub FileSetTimestamp(f As String, t As DateTime)
        'Timestamp der Datei ändern
        With New System.IO.FileInfo(wb_GlobalSettings.pKocherPath & f)
            .LastAccessTime = t
            .LastWriteTime = t
        End With
    End Sub

    ''' <summary>
    ''' Alle neuen oder geänderten Rezepte auf dem Master(Server) müssen mit der WinBack-Datenbank
    ''' synchronisiert werden.
    ''' Die einzelnen Schritte werden in festgelegte Komponenten-Schemata eingetragen.
    ''' </summary>
    Public Sub SynctoDB()

        'alle Rezepte in der eigenen Liste(Master)
        For Each Rzpt As wb_Kocher_Rezept In RezeptListe.Values
            'abhängig vom Sync-Status
            Select Case Rzpt.SyncStatus

                Case wb_Global.Kocher_SyncStatus.CopyToMaster
                    'Bestehendes Rezept update
                    Debug.Print("Rezept " & Rzpt.Nummer & " update in winback-DB")
                    Rzpt.DBUpdateRezept(False)

                Case wb_Global.Kocher_SyncStatus.CopyNewFileToMaster
                    'Neues Rezept anlegen
                    Debug.Print("Rezept " & Rzpt.Nummer & " create in winback-DB")
                    Rzpt.DBUpdateRezept(True)

            End Select
        Next

    End Sub

End Class
