Imports WinBack.wb_Rohstoffe_Shared
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rohstoffe_Silo
    Inherits DockContent

    Private _SiloBefuellung As wb_OrgaBackProcessPosition
    Private _AnzahlSilos As Integer
    Private _Befuellung As Boolean = False
    Private _SiloVerteilung As New List(Of wb_LagerSilo)

    Dim sType As wb_Global.RohSiloTypen = wb_Global.RohSiloTypen.UNDEF
    Dim aType As wb_Global.RohSiloTypen = wb_Global.RohSiloTypen.UNDEF
    Dim sFuellStand As New Hashtable

    Dim SiloBef As New wb_SiloBef
    Dim SiloSackWare As New wb_SiloSackWare
    Dim SiloRstf As wb_Silo

    Public ReadOnly Property SiloVerteilung As IList
        Get
            Return _SiloVerteilung
        End Get
    End Property

    ''' <summary>
    ''' Zeigt dieses Fenster als Dialog-Fenster an.
    ''' Wird von ob_Main_Menu aufgerufen, wenn Lieferungen eingebucht werden müssen (Vorfall WE), die auf auf mehrere WinBack-Silos
    ''' verteilt werden können. (Identische Rohstoff-Nummern)
    ''' </summary>
    ''' <param name="SiloBefuellung"></param>
    Public Sub ShowBefuellungDialog(SiloBefuellung As wb_OrgaBackProcessPosition)
        'Fenster zeigt Befüllung an (Dialog)
        _Befuellung = True
        'Event-Handler (Wert der Befüllmenge ändert sich - Berechnung der Restmenge)
        AddHandler wb_Rohstoffe_Shared.eBefMenge_Changed, AddressOf CalcBefuellMengen
        'Daten aus dem WE-Vorfall
        _SiloBefuellung = SiloBefuellung
        'Anzeige Silos und Füllstände
        DetailInfo_Silo()
        'Fenster-Titel
        Me.Text = "Lieferung auf Silos verteilen"
        'Fenster anzeigen
        Me.ShowDialog()
    End Sub

    ''' <summary>
    ''' Fenster wird geladen.
    ''' Wenn das Fenster nicht als modales Fenster angezeigt wird (kein Befüll-Dialog) werden alle Silos aus dieser Gruppe
    ''' angezeigt. Wird ein anderer Rohstoff im Listen-Fenster angewählt, wird die Anzeige aktualisiert oder ausgeblendet.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Rohstoffe_Silo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Fenster wird modal(Dialog) aufgerufen. Befüllung Silos
        If Not Me.Modal Then
            'Fenster zeigt Silo-Füllstände an
            _Befuellung = False
            'Event-Handler (Klick auf Rohstoff-Liste -> Anzeige der Detail-Info)
            AddHandler eListe_Click, AddressOf DetailInfo

            'Beim ersten Aufruf werden alle Silos passend zum Lagerort des aktuellen Rohstoffes angezeigt.
            If RohStoff IsNot Nothing Then
                DetailInfo(sender)
            End If
        End If

    End Sub

    ''' <summary>
    ''' Fenster wird geschlossen. Handle wieder entfernen.
    ''' Aus den einzelnen Controls (Silos) werden die Soll-Daten zur Befüllung in eine Liste(wb_LagerSilo) kopiert
    ''' Anhand dieser Liste wird dann die Verteilung der Liefermenge in die einzelnen Silos vorgenommen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Rohstoffe_Silo_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Event-Zeiger wieder freigeben
        RemoveHandler wb_Rohstoffe_Shared.eListe_Click, AddressOf DetailInfo
        RemoveHandler wb_Rohstoffe_Shared.eBefMenge_Changed, AddressOf CalcBefuellMengen

        'Liste aller WinBack-Lieferungen (Enthält auch Null-Setzen)
        _SiloVerteilung.Clear()
        'Verteilung der Liefermenge auf die einzelnen Silos
        For i = 1 To TableLayoutPanel.Controls.Count - 1
            Dim s As New wb_LagerSilo
            s.CopyFrom(TableLayoutPanel.Controls.Item(i))
            _SiloVerteilung.Add(s)
        Next

        'Speicher freigeben
        TableLayoutPanel.Controls.Clear()
    End Sub

    ''' <summary>
    ''' Anzeige der Rohstoff-Silo-Details.
    ''' Wird aufgerufen durch Event eListe_Click(). Aktualisiert die Anzeigefelder (Nummer/Text/Kommentar...)
    ''' </summary>
    Private Sub DetailInfo(sender)
        'Prüfen ob zu dieser Rohstoff-Nummer ein/mehrere Silo's exisitieren
        sType = wb_Rohstoffe_Shared.GetRohSiloType(RohStoff.Lagerort)
        If (sType <> wb_Global.RohSiloTypen.UNDEF) Then

            'Silo-Type hat sich geändert
            If (sType <> aType) Then
                'Anzeige ausblenden
                TableLayoutPanel.Visible = False
                TableLayoutPanel.Controls.Clear()
                TableLayoutPanel.ColumnCount = 0

                'Panels für die Silo-Füllstände anzeigen (alle Silos mit dieser Lagerort-Type)
                For Each d As DictionaryEntry In RohSilos_NachNummer
                    'Suche Silo-Daten für diese Rohstoff-Nummer
                    SiloRstf = RohSilos_NachNummer(d.Key)
                    'wenn das Silo den gesuchten Lagerort-Typ hat
                    If SiloRstf.RohSiloType = sType Then
                        'neues Panel
                        AddPanel(SiloRstf)
                        'alle Silos mit identischer Rohstoff-Nummer 
                        For Each SiloRstf_Child As wb_Silo In SiloRstf.ChildSteps
                            'im Panel anzeigen 
                            AddPanel(SiloRstf_Child)
                        Next
                    End If
                Next
                'Anzeige sortieren
                SortPanelBef()
                'Anzeige wieder einblenden
                TableLayoutPanel.Visible = True
            End If

            'Füllstände einlesen
            GetSiloFuellstand_DB(sType)
            'Füllstände in Grafik einblenden
            SetSiloFuellStand()
        Else
            TableLayoutPanel.Visible = False
        End If

        'Silo-Type merken
        aType = sType
    End Sub

    ''' <summary>
    ''' Anzeige der Rohstoff-Silo-Details. Wird aufgerufen beim Öffnen des Fenster als modaler Dialog (Befüllung)
    ''' </summary>
    Private Sub DetailInfo_Silo()
        'Rohstoff-Nummer aus der Prozessinfo (WE)
        Dim RohstoffNummer As String = _SiloBefuellung.ArtikelNummer

        'wenn die Rohstoff-Nummer gültig ist
        If RohstoffNummer IsNot Nothing Then
            'Rohstoff-Nummer ist ein Silo-Rohstoff
            If RohSilos_NachNummer.ContainsKey(RohstoffNummer) Then

                'Anzeige ausblenden
                TableLayoutPanel.Visible = False
                'alle (alten) Einträge löschen
                TableLayoutPanel.Controls.Clear()
                TableLayoutPanel.ColumnCount = 0

                'Anzeige Befülldaten
                AddPanelBef()
                'Chargen-Nummer aus Befüllung
                SiloBef.ChargenNummer = _SiloBefuellung.SerienNummer
                SiloBef.KompNummer = RohstoffNummer
                SiloBef.Preis = _SiloBefuellung.Preis

                'Anzeige Silos mit identischer Rohstoffnummer
                SiloRstf = RohSilos_NachNummer(RohstoffNummer)
                'Chargen-Nummer aus Befüllung
                SiloRstf.ChargenNummer = _SiloBefuellung.SerienNummer
                SiloRstf.Preis = _SiloBefuellung.Preis
                'Anzeige Sackware (Handkomponenten mit identischer Rohstoff-Nummer)
                AddPanelSackWare(SiloRstf)
                'neues Panel
                AddPanel(SiloRstf)
                'alle Silos mit identischer Rohstoff-Nummer 
                For Each SiloRstf_Child As wb_Silo In SiloRstf.ChildSteps
                    'Chargen-Nummer aus Befüllung
                    SiloRstf_Child.ChargenNummer = _SiloBefuellung.SerienNummer
                    SiloRstf_Child.Preis = _SiloBefuellung.Preis
                    'im Panel anzeigen 
                    AddPanel(SiloRstf_Child)
                Next
                'Anzeige wieder einblenden
                TableLayoutPanel.Visible = True

            End If
        End If

        'Füllstände einlesen
        GetSiloFuellstand_DB(RohstoffNummer)
        'Füllstände in Grafik einblenden
        SetSiloFuellStand()
    End Sub

    ''' <summary>
    ''' Silo-Grafik in Panel anfügen. Das Silo-Objekt wird neu erzeugt und kopiert notwendigen Daten
    ''' aus dem übergebenen Objekt.
    ''' Wenn die angezeigten Objekte nicht neu erzeugt werden, wird beim Schliessen des Fensters auch 
    ''' das globale Objekt gelöscht (Fehler in vb.net)
    ''' </summary>
    ''' <param name="SiloRstf_x"></param>
    Private Sub AddPanel(SiloRstf_x As wb_Silo)
        Dim SiloPanel As New wb_Silo
        SiloPanel.Name = "wb_Silo"
        SiloPanel.CopyFrom(SiloRstf_x)
        SiloPanel.Befuellung = _Befuellung

        TableLayoutPanel.Controls.Add(SiloPanel)
        TableLayoutPanel.ColumnCount = TableLayoutPanel.Controls.Count
        SiloReiheMaxMenge = Math.Max(SiloReiheMaxMenge, SiloRstf_x.MaxMenge)
    End Sub

    ''' <summary>
    ''' Sackware-Grafik in Panel anfügen. Wenn es eine passende Handkomponente zu dieser Rohstoff-Nummer gibt, wird eine
    ''' entsprechende Grafik angezeigt.
    ''' Damit können Wareneingänge (Silo-Rohstoff) auch auf Sackware gebucht werden. 
    ''' 
    ''' Wichtig für Kleinkomponenten und Sackschütten. Diese können dann beim Befüllen umgebucht werden auf die Silo-Rohstoffe.
    ''' </summary>
    ''' <param name="SiloRstf_x"></param>
    Private Sub AddPanelSackWare(SiloRstf_x As wb_Silo)
        'passende Handkomponente dazu suchen
        Dim SiloSackWarePanel As New wb_SiloSackware
        SiloSackWare.Name = "wb_SiloSackWare"
        If SiloSackWare.CopyFrom(SiloRstf_x.KompNummer) Then
            SiloSackWare.Befuellung = _Befuellung
            SiloSackWare.ChargenNummer = SiloRstf_x.ChargenNummer
            SiloSackWarePanel.Preis = SiloRstf_x.Preis

            TableLayoutPanel.Controls.Add(SiloSackWare)
            TableLayoutPanel.ColumnCount = TableLayoutPanel.Controls.Count
        End If
    End Sub

    ''' <summary>
    ''' Befüll-Grafik in Panel einfügen.
    ''' </summary>
    Private Sub AddPanelBef()
        'Liefermenge aus den Vorfall-Daten
        SiloBef.LieferMenge = CInt(_SiloBefuellung.GelieferteMenge)
        TableLayoutPanel.Controls.Add(SiloBef)
        TableLayoutPanel.ColumnCount = TableLayoutPanel.Controls.Count
    End Sub


    ''' <summary>
    ''' Einfacher Bubble-Sort-Algorithmus
    ''' </summary>
    Private Sub SortPanelBef()
        'Flag Sortieren notwendig
        Dim bSort As Boolean = True
        'Anzahl der Silos
        Dim n As Integer = TableLayoutPanel.Controls.Count
        'Schleife rückwärts über alle Silos
        For i = (n - 1) To 1 Step -1
            'Sortieren aktiv
            If bSort Then
                'Reset Flag
                bSort = False
                'Schleife über alle Silos
                For j = 0 To i - 1
                    'Bubble-Sort (Vergleich und Tauschen Silo(j) und Silo(j+1) - gibt true zurück, wenn getauscht wurde
                    bSort = DirectCast(TableLayoutPanel.Controls.Item(j), wb_Silo).BubleSort(DirectCast(TableLayoutPanel.Controls.Item(j + 1), wb_Silo))
                Next
            Else
                'Sortiervorgang fertig
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' Ermittelt die Füllstände aus der Lagerorte-Tabelle und zeigt diese im User-Control an
    ''' Gibt die Füllständer der Silos zu dieser Rohstoff-Type zurück
    ''' </summary>
    Private Sub GetSiloFuellstand_DB(sType As wb_Global.RohSiloTypen)
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim SiloNummer As Integer = 0
        Dim SiloLagerOrt As String = ""
        Dim Bilanzmenge As Integer = 0
        Dim sql As String = ""

        'alle Datensätze lesen
        sFuellStand.Clear()
        'Silo-Füllstände aus winback.Lagerorte
        Select Case sType
            Case wb_Global.RohSiloTypen.BW
                sql = "BW%"
            Case wb_Global.RohSiloTypen.M
                sql = "M__"
            Case wb_Global.RohSiloTypen.MK
                sql = "MK%"
            Case wb_Global.RohSiloTypen.KKA
                sql = "KK%"
        End Select
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSiloGrpLager, sql))
        While winback.Read
            'Silo-Nummer
            SiloNummer = winback.iField("LG_Silo_Nr")
            'Lagerort
            SiloLagerOrt = winback.sField("LG_Ort")
            'Bilanzmenge
            Bilanzmenge = wb_Functions.StrToInt(winback.sField("LG_Bilanzmenge"))
            'Daten in HashTable schreiben
            sFuellStand.Add(SiloLagerOrt, Bilanzmenge)
        End While

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Ermittelt die Füllstände aus der Lagerorte-Tabelle und zeigt diese im User-Control an
    ''' Gibt die Füllstände aller Silos zu dieser Rohstoff-Nummer zurück
    ''' </summary>
    Private Sub GetSiloFuellstand_DB(KompNummer As String)
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim SiloNummer As Integer = 0
        Dim SiloLagerOrt As String = ""
        Dim Bilanzmenge As Integer = 0

        'alle Datensätze lesen
        sFuellStand.Clear()
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSiloGrpNummer, KompNummer))
        While winback.Read
            'Silo-Nummer
            SiloNummer = winback.iField("LG_Silo_Nr")
            'Lagerort
            SiloLagerOrt = winback.sField("LG_Ort")
            'Bilanzmenge
            Bilanzmenge = wb_Functions.StrToInt(winback.sField("LG_Bilanzmenge"))
            'Daten in HashTable schreiben
            sFuellStand.Add(SiloLagerOrt, Bilanzmenge)
        End While

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Füllstand aus HashTable in die Silo-Grafik übertragen
    ''' </summary>
    Private Sub SetSiloFuellStand()
        'alle Silos im Panel
        For i = 0 To TableLayoutPanel.Controls.Count - 1
            'Berechnung nur für Silos
            If i > 0 Or Not _Befuellung Then
                'unterscheiden zwischen Silo und Sack(Handkomponente)
                Select Case TableLayoutPanel.Controls.Item(i).Name
                    Case "wb_Silo"
                        SiloRstf = TableLayoutPanel.Controls.Item(i)
                        If sFuellStand.ContainsKey(SiloRstf.LagerOrt) Then
                            SiloRstf.IstMenge = sFuellStand(SiloRstf.LagerOrt)
                            SiloRstf.SiloReiheMaxMenge = SiloReiheMaxMenge
                        End If
                End Select
            Else
                SiloRstf.IstMenge = 0
            End If
        Next
    End Sub

    '''' <summary>
    '''' Die Befüllmenge in einem der Silos hat sich geändert.
    '''' Die Mengen über alle Silos werden neu berechnet.
    '''' </summary>
    '''' <param name="sender"></param>
    Private Sub CalcBefuellMengen(sender As Object)
        'Liefermenge schon verteilt
        Dim GesVerteilt As Integer = 0
        'Liefermenge summiert über alle Silos
        For i = 0 To TableLayoutPanel.Controls.Count - 1
            'Berechnung nur für Silos
            If i > 0 Or Not _Befuellung Then
                'unterscheiden zwischen Silo und Sack(Handkomponente)
                Select Case TableLayoutPanel.Controls.Item(i).Name
                    Case "wb_Silo"
                        SiloRstf = TableLayoutPanel.Controls.Item(i)
                        GesVerteilt += SiloRstf.BefMenge
                    Case "wb_SiloSackWare"
                        SiloSackWare = TableLayoutPanel.Controls.Item(i)
                        GesVerteilt += SiloSackWare.BefMenge
                End Select
            End If
        Next
        'anzeigen
        SiloBef.Verteilt = GesVerteilt
        'Restmenge an alle Silos weitergeben
        For i = 0 To TableLayoutPanel.Controls.Count - 1
            If i > 0 Or Not _Befuellung Then
                'unterscheiden zwischen Silo und Sack(Handkomponente)
                Select Case TableLayoutPanel.Controls.Item(i).Name
                    Case "wb_Silo"
                        SiloRstf = TableLayoutPanel.Controls.Item(i)
                        SiloRstf.RestMenge = SiloBef.RestMenge
                    Case "wb_SiloSackWare"
                        SiloSackWare = TableLayoutPanel.Controls.Item(i)
                        SiloSackWare.RestMenge = SiloBef.RestMenge
                End Select
            End If
        Next
    End Sub
End Class
