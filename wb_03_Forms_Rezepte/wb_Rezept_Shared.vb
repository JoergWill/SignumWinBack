
Public Class wb_Rezept_Shared

    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)
    Public Shared Event eListe_Refresh(Sender As Object)
    Public Shared Event eRezept_Copy(Sender As Object, RzNr As Integer, Variante As Integer)

    Public Shared RzVariante As New SortedList
    Public Shared RzGruppe As New SortedList

    Public Shared ProdStufeText As New List(Of String)
    Public Shared KesselStufeText As New List(Of String)
    Public Shared TextKomponenteText As New List(Of String)

    Public Shared Rezept As New wb_Rezept
    Private Shared _ErrorText As String = ""
    Private Shared _TabelleHisRezepteOK = False


    Enum AnzeigeFilter
        Undefined    ' nicht definiert
        Alle         ' alle Rezepte
        Produktion   ' alle Rezepte Produktion
        Sauerteig    ' alle Rezepte Sauerteig
        LinienGruppe ' alle Rezept mit Liniengruppe X
        RezeptGruppe ' alle Rezept mit Rezeptgruppe X
        Papierkorb   ' alle gelöschten Rezepte (Liniengruppe<0)
    End Enum

    Shared Sub New()
        LoadVariantenTexte()
        LoadRezeptGruppenTexte()
        LoadTextBausteine()
    End Sub

    Public Shared ReadOnly Property ErrorText As String
        Get
            Return _ErrorText
        End Get
    End Property

    Public Shared ReadOnly Property UpdateDatabaseFile As String
        Get
            Return "2.30_HisRezepte.sql"
        End Get
    End Property

    Public Shared ReadOnly Property CheckDB() As Boolean
        Get
            'Prüfen ob ein Udpdate der Lagerorte-Tabelle erforderlich ist
            Check_DBFelder()
            Return _TabelleHisRezepteOK
        End Get
    End Property

    Public Shared Sub Invalid()
        Rezept.Invalid()
    End Sub

    Public Shared Function Reload() As Boolean
        LoadVariantenTexte()
        LoadRezeptGruppenTexte()
        LoadTextBausteine()
        Return True
    End Function

    Private Shared Sub LoadVariantenTexte()
        'HashTable mit der Übersetzung der Variante-Nummer in die Varianten-Bezeichnung laden
        'wenn die Varianten-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect("SELECT RV_Nr, RV_Bezeichnung FROM RezeptVarianten")
        RzVariante.Clear()
        While winback.Read
            RzVariante.Add(winback.iField("RV_Nr"), winback.sField("RV_Bezeichnung"))
        End While
        winback.Close()
    End Sub

    ''' <summary>
    ''' Erzeugt eine SortedList mit der Zuordnung von Rezeptgruppen-Nummer zum Rezeptgruppen-Text.
    ''' Wird für die Auswahlfelder Rezeptgruppe verwendet.
    ''' </summary>
    Private Shared Sub LoadRezeptGruppenTexte()
        'HashTable mit der Übersetzung der Rezeptgruppen-Nummer in die Rezeptgruppen-Bezeichnung laden
        'wenn die Rezeptgruppen-Bezeichnung einen Verweis aus die Texte-Tabelle enthält wird die
        'entsprechende Übersetzung aus winback.Texte geladen
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect("SELECT II_ItemID, II_Kommentar FROM ItemIDs WHERE II_ItemTyp = 230")
        RzGruppe.Clear()
        RzGruppe.Add(0, "")
        While winback.Read
            RzGruppe.Add(winback.iField("II_ItemID"), winback.sField("II_Kommentar"))
        End While
        winback.Close()
    End Sub

    ''' <summary>
    ''' Erzeugt ein String-Array mit vorgegebenen Texten für die Rezepteingabe der Produktions-Stufen/Kessel/Textbausteine.
    ''' 
    ''' Die einzelnen Texte stehen in der Datenbank winback.ItemParameter:
    ''' 
    '''     IP_ItemTyp      IP_ItemID       IP_fdNr     IP_Wert4Str
    '''      3010               0            0...x      Texte Produktions-Stufen
    '''      3010               1            0...x      Texte Kessel-Stufen
    '''      3010               2            0...x      Texte Textkomponente
    ''' 
    ''' </summary>
    Private Shared Sub LoadTextBausteine()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim TextBausteinId As Integer
        winback.sqlSelect(wb_Sql_Selects.sqlTextBausteine)

        'alle Listen löschen
        ProdStufeText.Clear()
        KesselStufeText.Clear()
        TextKomponenteText.Clear()

        'Textbausteine lesen (aus Tabelle ItemParameter)
        While winback.Read
            TextBausteinId = winback.iField("IP_ItemID")
            Select Case TextBausteinId
                Case 0
                    ProdStufeText.Add(winback.sField("IP_Wert4str"))
                Case 1
                    KesselStufeText.Add(winback.sField("IP_Wert4str"))
                Case 2
                    TextKomponenteText.Add(winback.sField("IP_Wert4str"))
                Case Else
                    Trace.WriteLine("@E_Fehler beim Einlesen der Textbausteine - Tabelle ItemParameter(IP_ItemTyp=3010 IP_ItemID=" & TextBausteinId & ")")
            End Select
        End While
        winback.Close()
    End Sub

    Public Shared Sub Liste_Click(sender As Object)
        RaiseEvent eListe_Click(sender)
    End Sub

    Public Shared Sub Edit_Leave(sender As Object)
        RaiseEvent eEdit_Leave(sender)
    End Sub

    Public Shared Sub Liste_Refresh(sender As Object)
        RaiseEvent eListe_Refresh(sender)
    End Sub

    Public Shared Sub Rezept_Copy(sender As Object, RzNr As Integer, Variante As Integer)
        RaiseEvent eRezept_Copy(sender, RzNr, Variante)
    End Sub

    ''' <summary>
    ''' Neue Rezeptgruppe anlegen in winback.ItemIDs (II_ItemTyp=230)
    ''' Damit Rezepte mit dieser Rezeptgruppe in der Produktion auch sichtbar sind, muss auch für alle Benutzergruppen ein 
    ''' entsprechender Eintrag in der Tabelle ItemParameter vorgenommen werden.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function AddRezeptGruppe() As Boolean
        'Default Fehlermeldung
        _ErrorText = "Fehler beim Anlegen der neuen Rezeptgruppe"
        'Rezept-Gruppe Nummer
        Dim RG_Nr As Integer = 1

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Max-Wert Rezept-Gruppen-Nummer ermitteln
        winback.sqlSelect("SELECT MAX(II_ItemID) FROM ItemIDs WHERE II_ItemTyp=230")
        If winback.Read Then
            RG_Nr = winback.iField("MAX(II_ItemID)") + 1
        End If
        winback.CloseRead()

        'neue Rezeptgruppe anfügen
        If Not winback.sqlCommand("INSERT INTO ItemIDs (II_ItemTyp, II_ItemID, II_Kommentar) VALUES(230, " & RG_Nr.ToString & ", 'Rezeptgruppe " & RG_Nr.ToString & "')") > 0 Then
            winback.Close()
            Return False
        Else
            RzGruppe.Add(RG_Nr, "Rezeptgruppe " & RG_Nr.ToString)
            _ErrorText = ""
        End If

        'Benutzer-Rechte für diese Rezeptgruppe einrichten
        For Each UserGruppe As Integer In {1, 2, 3, 4, 5, 6, 7, 8, 99}
            winback.sqlCommand("INSERT INTO ItemParameter (IP_ItemTyp, IP_ItemID, IP_ItemAttr, IP_Lfd_Nr, IP_Wert1int, IP_Wert2int) VALUES(230, " & RG_Nr.ToString & ", 405, " & UserGruppe + 100 & "," & UserGruppe & ", 2)")
        Next

        winback.Close()
        Return True
    End Function

    ''' <summary>
    ''' Rezeptgruppe löschen in winback.ItemIDs (II_ItemTyp=230) und in winback.ItemParameter (Benutzer-Rechte Rezeptgruppen)
    ''' </summary>
    ''' <param name="Rg_Nr"></param>
    ''' <returns></returns>
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public Shared Function DeleteRezeptGruppe(Rg_Nr As Integer) As Boolean
        'Default Fehlermeldung
        _ErrorText = "Fehler beim Löschen der Rezeptgruppe"
        'Prüfen ob die Rezeptgruppen-Nummer existiert
        If RzGruppe.ContainsKey(Rg_Nr) Then
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'Prüfen ob die Rezeptgruppe noch verwendet wird
            If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptRezeptGruppe, Rg_Nr.ToString)) Then
                If winback.Read Then
                    If winback.iField("Used") = 0 Then
                        winback.CloseRead()

                        'Rezeptgruppe löschen aus Tabelle ItemIDs
                        If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDeleteRezeptGruppe, Rg_Nr.ToString)) < 0 Then
                            winback.Close()
                            Return False
                        End If
                        'Rezeptgruppe löschen aus Tabelle ItemParameter(Gruppen-Rechte Rezeptgruppen)
                        If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDeleteRezeptGruppenRechte, Rg_Nr.ToString)) < 0 Then
                            winback.Close()
                            Return False
                        End If
                    Else
                        'Rezeptgruppe wird noch verwendet
                        _ErrorText = "Rezeptgruppe " & Rg_Nr & " wird noch verwendet !"
                        winback.Close()
                        Return False
                    End If

                    'Löschen Rezeptgruppe und User-GruppenRechte war erfolgreich
                    winback.Close()
                    Return True
                End If
            Else
                winback.Close()
                Return False
            End If
        Else
            'Liniengruppe existiert nicht
            Return False
        End If
        Return False
    End Function

    Public Shared Function Add_RezeptVariante() As Integer
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'nächste freie Gruppen-Nummer ermitteln (Artikel UND Rohstoff-Gruppe)
        Dim RezVarianteNr As Integer = 1
        Do While RzVariante.ContainsKey(RezVarianteNr)
            RezVarianteNr += 1
        Loop

        'Datensatz neu anlegen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlAddNewRezVariante, RezVarianteNr))
        winback.Close()

        'Liste neu aufbauen
        RzVariante.Add(RezVarianteNr, "")

        'Neue RezeptVarianten-Nummer zurückgegeben
        Return RezVarianteNr
    End Function

#Disable Warning S3776 ' Cognitive Complexity of functions should not be too high
    Public Shared Function Delete_RezeptVariante(Nr As Integer) As Boolean
#Enable Warning S3776 ' Cognitive Complexity of functions should not be too high
        'Default Fehlermeldung
        _ErrorText = "Fehler beim Löschen der Rezeptvariante"

        'Variante '0' darf nicht gelöscht werden
        If Nr > 0 Then
            'Prüfen ob die Rezept-Variante existiert
            If RzVariante.ContainsKey(Nr) Then
                'Datenbank-Verbindung öffnen - MySQL
                Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

                'Prüfen ob die Rezeptvariante noch verwendet wird
                If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUsedRezVariante, Nr.ToString)) Then
                    If winback.Read Then
                        If winback.iField("Used") = 0 Then
                            'Rezeptvariante löschen
                            winback.CloseRead()
                            If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDeleteRezVariante, Nr.ToString)) Then
                                _ErrorText = ""
                                Return True
                            End If
                        Else
                            'Rezeptvariante wird noch verwendet
                            _ErrorText = "Rezeptvariante " & Nr & " wird noch verwendet !"
                            Return False
                        End If
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                'Rezeptvariante existiert nicht
                Return False
            End If
        Else
            'Rezeptvariante wird noch verwendet
            _ErrorText = "Rezeptvariante " & Nr & " darf nicht gelöscht werden !"
            Return False
        End If
        Return False
    End Function

    Public Shared Function Add_TextBaustein(TextBausteinType As Integer) As Integer
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)

        'nächste freie Textbaustein-Nummer ermitteln aus max(IP_Lfd_Nr)
        Dim TextBausteinNr As Integer = wb_sql_Functions.getNewProdStufeTextNr(TextBausteinType)

        'Datensatz neu anlegen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlAddTextBaustein, TextBausteinNr, TextBausteinType))
        winback.Close()

        'Liste neu aufbauen
        Select Case TextBausteinType
            Case 0
                ProdStufeText.Add(TextBausteinNr.ToString)
            Case 1
                KesselStufeText.Add(TextBausteinNr.ToString)
            Case 2
                TextKomponenteText.Add(TextBausteinNr.ToString)
            Case Else
                Trace.WriteLine("@E_Fehler beim Einfügen der Textbausteine - Tabelle ItemParameter(IP_ItemTyp=3010 IP_ItemID=" & TextBausteinType & ")")
        End Select

        'Neue RezeptVarianten-Nummer zurückgegeben
        Return TextBausteinNr
    End Function

    Public Shared Function Delete_TextBaustein(Nr As Integer, TextBausteinType As Integer) As Boolean
        'Default Fehlermeldung
        _ErrorText = "Fehler beim Löschen des Text-Bausteins"

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Textbaustein löschen
        If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDeleteTextBaustein, Nr.ToString, TextBausteinType.ToString)) Then
            _ErrorText = ""
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Prüft ob das Datenbankfeld wbdaten.HisRezepte.H_RZ_Bezeichnung die länge 60 Zeichen hat.
    ''' Wenn nicht. MUSS die Datenbank per Update-Script erweitert werden!
    ''' </summary>
    Private Shared Sub Check_DBFelder()
        Dim wbdaten As New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_GlobalSettings.WinBackDBType)
        'Prüfen ob Datenbankfeld LG_LF_Nummer vorhanden ist und die richtige Größe hat
        If wbdaten.sqlSelect(wb_Sql_Selects.sql_Check_H_RZ_Bezeichnung) Then
            If wbdaten.Read Then
                Dim FieldDesc As String = wbdaten.sField("Type")
                If FieldDesc.Contains("(60)") Then
                    _TabelleHisRezepteOK = True
                Else
                    _ErrorText = "Tabelle WbDaten.HisRezepte muss erweitert werden! (H_RZ_Bezeichnung muss VARCHAR(60) sein!)"
                    _TabelleHisRezepteOK = False
                End If
            End If
        End If
        wbdaten.Close()
    End Sub

End Class
