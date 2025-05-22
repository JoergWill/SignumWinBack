Imports WeifenLuo.WinFormsUI.Docking
Imports combit.Reporting.DataProviders

''' <summary>
''' MDI-Main-Fenster Rohstoff-Vewaltung
''' Abgeleitet von DockPanel_Main
'''     In DockPanelMain werden alle Funktionen für die Verwaltung der Layouts abgewickelt
'''     Default-Layout, Laden, Sichern, Löschen der Layouts
'''     Die Layouts werden, im Unterschied zu OrgaBack nicht vom jeweiligen Fenster sondern
'''     von der WinBack-Main-Form verwaltet.
''' </summary>
Public Class Rohstoffe_Main

    Public RohstoffListe As New wb_Rohstoffe_Liste
    Public RohstoffDetails As wb_Rohstoffe_Details
    Public RohstoffVerwendung As wb_Rohstoffe_Verwendung
    Public RohstoffParameter As wb_Rohstoffe_Parameter
    Public RohstoffTextHinweise As wb_Rohstoffe_TextHinweise
    Public RohstoffLieferung As wb_Rohstoffe_Lieferung
    Public RohstoffNwt As wb_Rohstoffe_Nwt
    Public RohstoffSilo As wb_Rohstoffe_Silo
    Public RohstoffCloud As wb_Rohstoffe_Cloud

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENDETAILS         -   Detail-Fenster wird geöffnet und angezeigt.
    '''     OPENVERWENDUNG      -   Fenster Verwendung in Rezepten wird geöffnet und angezeigt.
    '''     OPENPARAMETER       -   Parameter-Fenster wird geöffnet und angezeigt.
    '''     NWT                 -   Anzeige der Nährwerte wird geöffnet und angezeigt.
    '''     CLOUD               -   Verknüpfung zur WinBack-Cloud
    '''     
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    ''' <returns></returns>
    Public Overrides Function ExtendedCmd(Cmd As String, Prm As String) As Boolean
        Select Case Cmd
            Case "OPENLISTE"
                RohstoffListe.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENDETAILS"
                If Not DockIsVisible("wb_Rohstoffe_Details") Then
                    RohstoffDetails = New wb_Rohstoffe_Details
                    RohstoffDetails.Show(DockPanel, DockState.Document)
                End If
                Return True
            Case "OPENVERWENDUNG"
                If Not DockIsVisible("wb_Rohstoffe_Verwendung") Then
                    RohstoffVerwendung = New wb_Rohstoffe_Verwendung
                    RohstoffVerwendung.Show(DockPanel, DockState.Document)
                End If
                Return True
            Case "OPENPARAMETER"
                If Not DockIsVisible("wb_Rohstoffe_Parameter") Then
                    RohstoffParameter = New wb_Rohstoffe_Parameter
                    RohstoffParameter.Show(DockPanel, DockState.Document)
                End If
                Return True
            Case "OPENTEXTHINWEISE"
                If Not DockIsVisible("wb_Rohstoffe_TextHinweise") Then
                    RohstoffTextHinweise = New wb_Rohstoffe_TextHinweise
                    RohstoffTextHinweise.Show(DockPanel, DockState.Document)
                End If
                Return True
            Case "OPENLIEFERUNGEN"
                If Not DockIsVisible("wb_Rohstoffe_Lieferung") Then
                    RohstoffLieferung = New wb_Rohstoffe_Lieferung
                    RohstoffLieferung.Show(DockPanel, DockState.Document)
                End If
                Return True
            Case "NWT"
                If Not DockIsVisible("wb_Rohstoffe_Nwt") Then
                    RohstoffNwt = New wb_Rohstoffe_Nwt
                    RohstoffNwt.Show(DockPanel, DockState.Document)
                End If
                Return True
            Case "SILO"
                If Not DockIsVisible("wb_Rohstoffe_Silo") Then
                    RohstoffSilo = New wb_Rohstoffe_Silo
                    RohstoffSilo.Show(DockPanel, DockState.Document)
                End If
                Return True
            Case "CLOUD"
                If Not DockIsVisible("wb_Rohstoffe_Cloud") Then
                    RohstoffCloud = New wb_Rohstoffe_Cloud
                    RohstoffCloud.Show(DockPanel, DockState.Document)
                End If
                Return True
            Case "NEW"
                RohstoffNeuAnlegen()
                Return True
            Case "DELETE"
                RohstoffLöschen()
                Return True
            Case "LISTE_DRUCKEN"
                Return ListeDrucken()
            Case "SETFILTER"
                If RohstoffListe IsNot Nothing Then
                    RohstoffListe.Anzeige = CType(Prm, wb_Rohstoffe_Shared.AnzeigeFilter)
                End If
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Default-Layout anzeigen.
    ''' Falls keine Layout-Definitionen verhanden sind, wird das Haupt-Fenster (Liste) angezeigt.
    ''' </summary>
    Public Overrides Sub setDefaultLayout()
        DockPanel.Theme = wb_GlobalSettings.Theme
        RohstoffListe.Show(DockPanel, DockState.DockLeft)
        RohstoffListe.CloseButtonVisible = False
        OrgaBackOffice.LayoutFilename = "Default"
    End Sub

    ''' <summary>
    ''' Gibt für den jeweiligen Form-Namen die entsprechenden Klasse zurück, die dann im Dock dargestellt wird.
    ''' Füllt das Array DockPanelList in der Basis-Klasse
    ''' </summary>
    ''' <param name="persistString"></param>
    ''' <returns></returns>
    Public Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_Rohstoffe_Liste"
                _DockPanelList.Add(RohstoffListe)
                Return RohstoffListe
            Case "WinBack.wb_Rohstoffe_Details"
                RohstoffDetails = New wb_Rohstoffe_Details
                _DockPanelList.Add(RohstoffDetails)
                Return RohstoffDetails
            Case "WinBack.wb_Rohstoffe_Verwendung"
                RohstoffVerwendung = New wb_Rohstoffe_Verwendung
                _DockPanelList.Add(RohstoffVerwendung)
                Return RohstoffVerwendung
            Case "WinBack.wb_Rohstoffe_TextHinweise"
                RohstoffTextHinweise = New wb_Rohstoffe_TextHinweise
                _DockPanelList.Add(RohstoffTextHinweise)
                Return RohstoffTextHinweise
            Case "WinBack.wb_Rohstoffe_Parameter"
                RohstoffParameter = New wb_Rohstoffe_Parameter
                _DockPanelList.Add(RohstoffParameter)
                Return RohstoffParameter
            Case "WinBack.wb_Rohstoffe_Nwt"
                RohstoffNwt = New wb_Rohstoffe_Nwt
                _DockPanelList.Add(RohstoffNwt)
                Return RohstoffNwt
            Case "WinBack.wb_Rohstoffe_Silo"
                RohstoffSilo = New wb_Rohstoffe_Silo
                _DockPanelList.Add(RohstoffSilo)
                Return RohstoffNwt
            Case "WinBack.wb_Rohstoffe_Lieferung"
                RohstoffLieferung = New wb_Rohstoffe_Lieferung
                _DockPanelList.Add(RohstoffLieferung)
                Return RohstoffLieferung
            Case "WinBack.wb_Rohstoffe_Cloud"
                RohstoffCloud = New wb_Rohstoffe_Cloud
                _DockPanelList.Add(RohstoffCloud)
                Return RohstoffCloud
            Case Else
                Return Nothing
        End Select
    End Function

    ''' <summary>
    ''' MID-Form wird geöffnet. Vorher wurde schon in der Basis-Klasse die DockBar-Konfiguration geladen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub FormOpen(Sender As Object, e As EventArgs)

    End Sub

    ''' <summary>
    ''' MDI-Form wird geschlossen. Vorher wurde schon in der Basis-Klasse die DockBar-Konfiguration gesichert.
    ''' Schliesst alle erzeugten Fenster.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub FormClose(Sender As Object, e As FormClosedEventArgs)
        'alle erzeugten Fenster wieder schliessen
        wb_Functions.CloseAndDisposeSubForm(RohstoffDetails)
        wb_Functions.CloseAndDisposeSubForm(RohstoffListe)
        wb_Functions.CloseAndDisposeSubForm(RohstoffVerwendung)
        wb_Functions.CloseAndDisposeSubForm(RohstoffTextHinweise)
        wb_Functions.CloseAndDisposeSubForm(RohstoffLieferung)
        wb_Functions.CloseAndDisposeSubForm(RohstoffParameter)
        wb_Functions.CloseAndDisposeSubForm(RohstoffNwt)
        wb_Functions.CloseAndDisposeSubForm(RohstoffSilo)
        wb_Functions.CloseAndDisposeSubForm(RohstoffCloud)

        'alle "alten" Daten löschen
        wb_Rohstoffe_Shared.Invalid()
    End Sub

    Public Sub RohstoffNeuAnlegen()
        Dim Komponente As New wb_Komponente
        Dim KompNrNeu As Integer = Komponente.MySQLdbNew(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE)
        RohstoffListe.ResetFilter()
        RohstoffListe.RefreshData(KompNrNeu)
        Komponente = Nothing
    End Sub

    Public Sub RohstoffLöschen()
        Dim Komponente As New wb_Komponente
        Dim KompNrDel As Integer = wb_Rohstoffe_Shared.RohStoff.Nr

        'MultiSelect
        If RohstoffListe.MoreThanOneRowSelected Then
            Dim CntDeleteOK As Integer = 0
            Dim CntDeletFail As Integer = 0

            For Each r As DataGridViewRow In RohstoffListe.SelectedRows
                KompNrDel = wb_Functions.StrToInt(r.Cells(wb_Rohstoffe_Liste.ColumnKompNr).Value.ToString)
                If Komponente.MySQLdbCanBeDeleted(KompNrDel) Then
                    Komponente.Nr = KompNrDel
                    Komponente.MySQLdbDelete()
                    CntDeleteOK += 1
                Else
                    CntDeletFail += 1
                End If
            Next
            RohstoffListe.RefreshData()
            MsgBox(CntDeleteOK & " Rohstoff(e) gelöscht" & vbCr & CntDeletFail & " Fehler", MsgBoxStyle.Information)
        Else
            If Komponente.MySQLdbCanBeDeleted(KompNrDel) Then
                Komponente.Nr = KompNrDel
                Komponente.MySQLdbDelete()
                RohstoffListe.RefreshData()
            Else
                MsgBox(Komponente.LastErrorText)
            End If
        End If
        Komponente = Nothing
    End Sub

    Private Function ListeDrucken() As Boolean
        'sicherheitshalber abfragen
        If Not IsNothing(RohstoffListe) Then

            'Druck-Daten
            Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog

            'Liste aller Rohstoffe aus den DataGridView
            pDialog.LL.DataSource = New AdoDataProvider(RohstoffListe.DataGridView.LLData)

            'List und Label-Verzeichnis für die Listen
            pDialog.ListSubDirectory = "Rohstoffe"
            pDialog.ListFileName = "RohstoffListe.lst"
            pDialog.ShowDialog()
            pDialog = Nothing
        End If
        Return True
    End Function

End Class