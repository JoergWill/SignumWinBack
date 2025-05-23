﻿Imports WinBack.wb_Language

Public Class wb_Rohstoffe_Shared
    Public Shared Event eListe_Click(sender As Object, Reload As Boolean)
    Public Shared Event eEdit_Leave(sender As Object)
    Public Shared Event eParam_Changed(Sender As Object)
    Public Shared Event eBefMenge_Changed(sender As Object)
    Public Shared Event eSelect_Data(sender As Object, KoNr As Integer)

    Public Shared RohGruppe As New SortedList
    Public Shared TeigTempRohstoffe As New SortedList
    Public Shared MehlGruppe As New List(Of wb_MehlGruppe)
    Public Shared NoMehlGruppe As New List(Of wb_MehlGruppe)
    Public Shared RohAktiv As New Hashtable
    Public Shared RohSilos_NachNummer As New Hashtable
    Public Shared RohSilos_NachTyp As New List(Of wb_Silo)
    Public Shared RohStoff As New wb_Komponente
    Public Shared SiloReiheMaxMenge As Integer = wb_Global.UNDEFINED

    Private Shared _ErrorText As String = ""
    Private Shared _RohstoffeInGruppe As New ArrayList
    Private Shared _TabelleKomponentenOK As Boolean = False

    Enum AnzeigeFilter
        Undefined   ' nicht definiert
        Alle        ' alle aktiven Rohstoffe Typ > 100
        Hand        ' alle aktiven Rohstoffe Typ 102
        Auto        ' alle aktiven Rohstoffe Typ 101,103,104
        HandAuto    ' alle Rohstoffe Type 101,102
        Sauerteig   ' alle aktiven Rohstoffe Sauerteig
        Install     ' alle inaktiven Rohstoffe
        Sonstige    ' alle Rohstoffe Typ 105,106
        RezeptKomp  ' alle aktiven Komponenten für Rezeptverwaltung (101..104, 118,128)
        OhneKneter  ' alle aktiven Komponenten für die Rezeptverwaltung ohne 118/128
        NurKneter   ' alle aktiven Komponenten 118
        RohstoffGrp ' alle Rohstoffe mit Rezeptgruppe X
    End Enum

    Enum LinkFilter
        Undefined   ' nicht definiert
        Alle        ' alle aktiven Rohstoffe 
        Rzpt        ' alle aktiven Rohstoffe die mit einer Rezeptur verknüpft sind
        Cloud       ' alle aktiven Rohstoffe die mit der Cloud verknüpft sind
        NoLink     ' alle aktiven Rohstoffe die nicht verknüpft sind (Rezept oder Cloud)
    End Enum

    Shared Sub New()
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        Load_RohstoffTables()
        'HashTable aller Silo-Rohstoffe mit Lagerort BW,MK,M,KKA
        Load_SiloTables()
        'HashTable aller Rohstoffe zur Teigtemperatur-Erfassung
        Load_TeigTempTables()
        'Prüfen ob ein Udpdate der Komponenten-Tabelle erforderlich ist
        Check_DBFelder()
    End Sub

    Public Shared Sub Invalid()
        RohStoff.Invalid()
    End Sub

    ''' <summary>
    ''' Reload aller oder definierter Listen(Tabellen) aus den Stammdaten nach Änderung der Parameter
    '''     -   Rohstoffe   (Rohstoff-Gruppen)
    '''     -   Silo        (Silo-Rohstoffe und Maximal-Mengen)
    '''     -   TeigTemp    (Rohstoffe mit Type 118)
    '''     
    ''' Wird keine Tabelle angegeben, werden alle Listen neu geladen
    ''' </summary>
    ''' <param name="Item"></param>
    Public Shared Sub Reload(Optional Item As String = "")
        Select Case Item

            Case "Rohstoffe"
                'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
                Load_RohstoffTables()
            Case "Silo"
                'HashTable aller Silo-Rohstoffe mit Lagerort BW,MK,M,KKA
                Load_SiloTables()
            Case "TeigTemp"
                'HashTable aller Rohstoffe zur Teigtemperatur-Erfassung
                Load_TeigTempTables()

            Case Else
                'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
                Load_RohstoffTables()
                'HashTable aller Silo-Rohstoffe mit Lagerort BW,MK,M,KKA
                Load_SiloTables()
                'HashTable aller Rohstoffe zur Teigtemperatur-Erfassung
                Load_TeigTempTables()
        End Select
    End Sub

    Public Shared ReadOnly Property ErrorText As String
        Get
            Return _ErrorText
        End Get
    End Property

    Public Shared ReadOnly Property UpdateDatabaseFile As String
        Get
            Return "2.30_Komponenten.sql"
        End Get
    End Property

    Public Shared ReadOnly Property CheckDB() As Boolean
        Get
            If _TabelleKomponentenOK Then
                Return True
            Else
                _ErrorText = "Tabelle WinBack.Komponenten muss erweitert werden! (Rezeptgewicht - Zählt zur Nährwertberechnung)"
                Return False
            End If
        End Get
    End Property

#Disable Warning S2352 ' Indexed properties with more than one parameter should not be used
    ''' <summary>
    ''' Liefert eine Liste aller Rohstoffe die zur Rohstoff-Gruppe gehören
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property RohstoffeInGruppe(Grp As Integer, Optional GrpNummer As Integer = 0) As ArrayList
#Enable Warning S2352 ' Indexed properties with more than one parameter should not be used
        Get
            'Liste leeren
            _RohstoffeInGruppe.Clear()

            'Array mit allen Rohstoffen aus beiden Gruppe füllen
            Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
            'Filter nach Gruppe1 und/oder Gruppe2
            Dim sql As String = ""
            Select Case GrpNummer
                Case 0
                    sql = "KA_Grp1 = " & Grp.ToString & " OR KA_Grp2 = " & Grp.ToString
                Case 1
                    sql = "KA_Grp1 = " & Grp.ToString
                Case 2
                    sql = "KA_Grp2 = " & Grp.ToString
            End Select

            'Alle Rohstoffe aus dieser Rohstoff-Gruppe
            winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRohstoffInGrp, sql))
            While winback.Read
                Dim x As New wb_StatistikListenElement
                x.Nr = winback.iField("KO_Nr")
                x.Nummer = winback.sField("KO_Nr_AlNum")
                x.Bezeichnung = winback.sField("KO_Bezeichnung")
                _RohstoffeInGruppe.Add(x)
            End While

            'Datenbank-Verbindung wieder schliessen
            winback.Close()
            Return _RohstoffeInGruppe
        End Get
    End Property

    ''' <summary>
    '''HashTable mit der Übersetzung der Rohstoffgruppen-Nummer in die Rohstoffgruppen-Bezeichnung laden
    '''wenn die Rohstoffgruppen-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
    '''entsprechende Übersetzung aus winback.Texte geladen
    '''
    '''Erzeugt eine Liste der Mehle aus der Rohstoff-Gruppe-Tabelle. Aus dieser Liste werden die
    '''Mehlanteile über die Zutatenliste berechnet.
    '''
    '''Erzeugt eine Liste der Rohstoff-Gruppen mit der Zuordnung KEINE MEHLZUSAMMENSETZUNG BERECHNEN
    '''Ist ein Rohstoff mit dieser Gruppe in einer Rezeptur verzeichnet, wird keine Mehlzusammensetzung
    '''berechnet.
    '''
    ''' </summary>
    Private Shared Sub Load_RohstoffTables()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'SortedList Rohstoff-Gruppen
        winback.sqlSelect(wb_Sql_Selects.sqlRohstoffGrp)
        RohGruppe.Clear()
        While winback.Read
            If Not RohGruppe.ContainsKey((winback.iField("IP_Wert1int"))) Then
                'Rohstoff-Gruppen
                RohGruppe.Add(winback.iField("IP_Wert1int"), TextFilter(winback.sField("IP_Wert4str")))
                'Rohstoff-Gruppen mit Flag Deklaration (Mehlzusammensetzung)
                If winback.iField("IP_Wert2int") = 1 Then
                    Dim MGrp As New wb_MehlGruppe
                    MGrp.GruppeNr = winback.iField("IP_Wert1int")
                    MGrp.Bezeichnung = TextFilter(winback.sField("IP_Wert4str"))
                    MehlGruppe.Add(MGrp)
                End If
                'Rohstoff-Gruppen mit Flag NoDeklaration (Keine Mehlzusammensetzung für dieses Rezept bestellen)
                If winback.iField("IP_Wert2int") = 2 Then
                    Dim MGrp As New wb_MehlGruppe
                    MGrp.GruppeNr = winback.iField("IP_Wert1int")
                    MGrp.Bezeichnung = TextFilter(winback.sField("IP_Wert4str"))
                    NoMehlGruppe.Add(MGrp)
                End If
            End If
        End While
        winback.CloseRead()

        'HashTable aktive Rohstoffe (Silo-Umschaltung)
        winback.sqlSelect(wb_Sql_Selects.sqlRohstoffAut)
        RohAktiv.Clear()
        While winback.Read
            RohAktiv.Add(winback.iField("KO_Nr"), TextFilter(winback.sField("LG_aktiv")))
        End While
        winback.Close()
    End Sub

    ''' <summary>
    ''' Liest alle (aktiven) Silo-Rohstoffe aus der WinBack-Datenbank. Die Rohstoffe sind sortiert
    ''' nach Lagerort und Komponenten-Nummer.
    ''' Die maximale Füllstand (Silo-Größe) steht in der Tabelle Lagerorte im Feld LG_Kommentar)
    ''' </summary>
    Public Shared Sub Load_SiloTables()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim KompNummer As String
        Dim KompBezeichnung As String
        Dim KompKommentar As String
        Dim SiloRohstoff As wb_Silo

        'Liste aller aktiven Silo-Rohstoffe
        winback.sqlSelect(wb_Sql_Selects.sqlSiloRohstoffe)

        'Hastable löschen
        RohSilos_NachNummer.Clear()
        'alle Datensätze lesen
        While winback.Read

            'Rohstoff-Nummer
            KompNummer = winback.sField("KO_Nr_AlNum")
            KompBezeichnung = winback.sField("KO_Bezeichnung")
            KompKommentar = winback.sField("KO_Kommentar")
            'Debug.Print("Rohstoffe Silos " & KompNummer & "/" & KompBezeichnung & "/ " & KompKommentar)

            'Prüfen ob die Rohstoff-Nummer schon existiert
            If RohSilos_NachNummer.ContainsKey(KompNummer) Then
                SiloRohstoff = New wb_Silo(RohSilos_NachNummer(KompNummer), KompBezeichnung, KompKommentar)
            Else
                SiloRohstoff = New wb_Silo(Nothing, "", "")
            End If

            'Silo-Daten
            SiloRohstoff.KompNr = winback.iField("KO_Nr")
            SiloRohstoff.KompNummer = KompNummer
            SiloRohstoff.KompKommentar = KompKommentar
            SiloRohstoff.KompBezeichnung = KompBezeichnung
            SiloRohstoff.LagerOrt = winback.sField("LG_Ort")
            'Silo maximale Füllmenge steht im Kommentar-Feld
            SiloRohstoff.MaxMenge = wb_Functions.StrToInt(winback.sField("LG_Kommentar"))
            SiloRohstoff.SiloNr = winback.iField("LG_Silo_Nr")
            'Silo aktiv
            SiloRohstoff.Aktiv = winback.sField("LG_aktiv")

            'wenn bisher kein anderer Rohstoff mit dieser Nummer exisitiert
            If SiloRohstoff.ParentStep Is Nothing Then
                RohSilos_NachNummer.Add(KompNummer, SiloRohstoff)
            End If

            'Flache Liste aller Silo-Rohstoffe
            RohSilos_NachTyp.Add(SiloRohstoff)

        End While
        'Datenbankverbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Liste aller Rohstoffe zur Teigtemperatur-Erfassung
    ''' Alle Rohstoffe mit Komponenten-Type 111
    ''' Alle Rohstoffe mit Komponenten-Type 118 die im Parameter 4(Einheit) den Wert 5(Grad Celsius) haben
    ''' </summary>
    Private Shared Sub Load_TeigTempTables()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim KompNr As Integer
        Dim KompBezeichnung As String
        'Hastable löschen
        TeigTempRohstoffe.Clear()
        'Liste aller Teigtemperatur-Messung-Rohstoffe
        If winback.sqlSelect(wb_Sql_Selects.sqlTeigTempRohstoffe) Then
            'alle Datensätze lesen
            While winback.Read
                'Rohstoff-Nummer
                KompNr = winback.sField("KO_Nr")
                KompBezeichnung = winback.sField("KO_Bezeichnung")
                TeigTempRohstoffe.Add(KompNr, KompBezeichnung)
            End While
        End If
        'Datenbankverbindung wieder schliessen
        winback.Close()
    End Sub

    Public Shared Function Add_RohstoffGruppe() As Integer
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'nächste freie Gruppen-Nummer ermitteln (Artikel UND Rohstoff-Gruppe)
        Dim RohGrpNr As Integer = 1
        Do While RohGruppe.ContainsKey(RohGrpNr) OrElse wb_Artikel_Shared.ArtGruppe.ContainsKey(RohGrpNr)
            RohGrpNr += 1
        Loop

        'Datensatz neu anlegen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlAddNewArtRohGruppe, RohGrpNr, "0"))
        winback.Close()

        'Liste neu aufbauen
        RohGruppe.Add(RohGrpNr, "")

        'Neue Artikel-Gruppen-Nummer zurückgegeben
        Return RohGrpNr
    End Function

    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public Shared Function Delete_RohstoffGruppe(Nr As Integer) As Boolean
        'Default Fehlermeldung
        _ErrorText = "Fehler beim Löschen der Rohstoffgruppe"
        'Prüfen ob die Artikel-Gruppen-Nummer existiert
        If RohGruppe.ContainsKey(Nr) Then
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'Prüfen ob die Artikelgruppe noch verwendet wird
            If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUsedRohArtGruppe, Nr.ToString)) Then
                If winback.Read Then
                    If winback.iField("Used") = 0 Then
                        'Artikelgruppe löschen
                        winback.CloseRead()
                        If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDeleteRohArtGruppe, Nr.ToString)) Then
                            _ErrorText = ""
                            Return True
                        End If
                    Else
                        'Artikelgruppe wird noch verwendet
                        _ErrorText = "Rohstoffgruppe " & Nr & " wird noch verwendet !"
                        Return False
                    End If
                    Return False
                End If
            Else
                Return False
            End If
        Else
            'Artikelgruppe existiert nicht
            Return False
        End If
        Return False
    End Function

    Public Shared Sub Liste_Click(sender As Object, Optional Reload As Boolean = False)
        RaiseEvent eListe_Click(sender, Reload)
    End Sub

    Public Shared Sub Edit_Leave(sender As Object)
        RaiseEvent eEdit_Leave(sender)
    End Sub

    Public Shared Sub BefMenge_Changed(sender As Object)
        RaiseEvent eBefMenge_Changed(sender)
    End Sub

    Public Shared Sub Select_Data(sender As Object, KoNr As Integer)
        RaiseEvent eSelect_Data(sender, KoNr)
    End Sub

    Public Shared Sub Param_Changed(sender As Object)
        'alle geänderten Rohstoff-Parameter in Datenbank schreiben (WinBack und OrgaBack)
        RohStoff.SaveParameterArray()
        'Parameter-Fenster neu aufbauen (Anzeige)
        RaiseEvent eParam_Changed(sender)
    End Sub

    Public Shared Sub RezChrg_Changed(Sender As Object)
        'Daten in der Komponenten-Klasse sichern
        RohStoff.UpdateDB()
    End Sub

    ''' <summary>
    ''' Prüft ob zu dieser Komponenten-Nummer mehrere Automatik-Rohstoffe mit dieser Nummer
    ''' existieren. Wenn eine Silo-Umschaltung möglich ist wird True zurückgegeben.
    ''' </summary>
    ''' <param name="KompNummer"></param>
    ''' <returns></returns>
    Public Shared Function HatSiloUmschaltung(KompNummer As String) As Boolean
        'TODO hier prüfen ob eine Handkomponente dazu existiert!
        Return (AnzahlSilos(KompNummer) >= 1)
    End Function

    ''' <summary>
    ''' Gibt die Anzahl der Rohstoffe mit dieser Nummer zurück.
    ''' </summary>
    ''' <param name="KompNummer"></param>
    ''' <returns></returns>
    Public Shared Function AnzahlSilos(KompNummer As String) As Integer
        If KompNummer IsNot Nothing Then
            Dim Result As Integer = 0
            If RohSilos_NachNummer.ContainsKey(KompNummer) Then
                Dim s As wb_Silo = RohSilos_NachNummer(KompNummer)
                Result += 1
                SiloReiheMaxMenge = Math.Max(SiloReiheMaxMenge, s.MaxMenge)
                For Each c As wb_Silo In s.ChildSteps
                    Result += 1
                    SiloReiheMaxMenge = Math.Max(SiloReiheMaxMenge, c.MaxMenge)
                Next
                Return Result
            Else
                Return -1
            End If
        End If
        Return -1
    End Function

    ''' <summary>
    ''' Gibt die Anzahl der Rohstoffe mit dieser Type zurück.
    ''' </summary>
    ''' <param name="RohSiloType"></param>
    ''' <returns></returns>
    Public Shared Function AnzahlSilos(RohSiloType As wb_Global.RohSiloTypen) As Integer
        If RohSiloType <> wb_Global.RohSiloTypen.UNDEF Then
            Dim Result As Integer = 0
            For Each d As DictionaryEntry In RohSilos_NachNummer
                Dim s As wb_Silo = RohSilos_NachNummer(d.Key)
                If s.RohSiloType = RohSiloType Then
                    Result += 1
                    SiloReiheMaxMenge = Math.Max(SiloReiheMaxMenge, s.MaxMenge)
                    For Each c As wb_Silo In s.ChildSteps
                        Result += 1
                        SiloReiheMaxMenge = Math.Max(SiloReiheMaxMenge, c.MaxMenge)
                    Next
                End If
            Next
            Return Result
        Else
            Return -1
        End If
    End Function

    Public Shared Function GetRohSiloType(Lagerort As String) As wb_Global.RohSiloTypen
        If Len(Lagerort) > 2 Then
            Select Case Left(Lagerort, 2)
                Case "BW"
                    Return wb_Global.RohSiloTypen.BW
                Case "MK"
                    Return wb_Global.RohSiloTypen.MK
                Case "KK"
                    Return wb_Global.RohSiloTypen.KKA
                Case "M0", "M1", "M2", "M3"
                    Return wb_Global.RohSiloTypen.M
                Case Else
                    Return wb_Global.RohSiloTypen.UNDEF
            End Select
        Else
            Return wb_Global.RohSiloTypen.UNDEF
        End If
    End Function

    ''' <summary>
    ''' Prüft ob das Datenbankfeld winback.Komponenten.KA_zaehlt_zu_NWT_Gesamtmenge vorhanden ist.
    ''' Wenn nicht. MUSS die Datenbank per Update-Script erweitert werden!
    ''' </summary>
    Private Shared Sub Check_DBFelder()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Prüfen ob Datenbankfeld KA_zaehlt_zu_NWT_Gesamtmenge vorhanden ist
        If winback.sqlSelect(wb_Sql_Selects.sqlCheckNwtGesamtmenge) AndAlso winback.Read Then
            _TabelleKomponentenOK = True
        End If
        winback.Close()
    End Sub

    ''' <summary>
    ''' Markiert die übergegebene Komponente (Update Nährwert-Info notwendig oder Nährwertinfo fehlerhaft)
    ''' </summary>
    ''' <param name="Marker"></param>
    Public Shared Sub MySQLdbSetMarker(Marker As wb_Global.ArtikelMarker, Optional Ko_Nr As Integer = wb_Global.UNDEFINED)
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Interne Komponenten-Nummer muss definiert sein
        If Ko_Nr < 0 Then
            Ko_Nr = RohStoff.Nr
        End If

        'Update Komponente in winback.Komponenten
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompSetMarker, Ko_Nr, Marker))
        winback.Close()
    End Sub

    Public Shared Function GetRohstoffGruppeFromNr(Nr As Integer) As String
        'Bezeichnung aus Rohstoff-Gruppe Nr
        If Nr > 0 Then
            If RohGruppe.ContainsKey(Nr) Then
                Return RohGruppe(Nr)
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function

End Class




