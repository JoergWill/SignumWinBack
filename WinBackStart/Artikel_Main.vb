﻿Imports WeifenLuo.WinFormsUI.Docking

''' <summary>
''' MDI-Main-Fenster Artikelverwaltung
''' Abgeleitet von DockPanel_Main
'''     In DockPanelMain werden alle Funktionen für die Verwaltung der Layouts abgewickelt
'''     Default-Layout, Laden, Sichern, Löschen der Layouts
'''     Die Layouts werden, im Unetrschied zu OrgaBack nicht vom jeweiligen Fenster sondern
'''     von der WinBack-Main-Form verwaltet.
''' </summary>
Public Class Artikel_Main

    Public ArtikelListe As New wb_Artikel_Liste     'Default-Fenster    (wird beim Öffnen immer angezeigt)
    Public ArtikelDetails As wb_Artikel_Details     'Detail-Fenster     (wird bei Bedarf erzeugt und angezeigt)
    Public ArtikelHinweise As wb_Artikel_Hinweise   'Hinweise-Fenster   (wird bei Bedarf erzeugt und angezeigt)
    Public ArtikelParameter As wb_Artikel_Parameter 'Parameter-Fenster  (wird bei Bedarf erzeugt und angezeigt)

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENDLISTE          -   Listen-Fenster wird geöffnet und angezeigt.
    '''     OPENDETAILS         -   Detail-Fenster wird geöffnet und angezeigt.
    '''     
    ''' </summary>
    ''' <param name="Cmd"></param>
    ''' <param name="Prm"></param>
    ''' <returns></returns>
    Public Overrides Function ExtendedCmd(Cmd As String, Prm As String) As Boolean
        Select Case Cmd
            Case "OPENLISTE"
                ArtikelListe.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENDETAILS"
                If IsNothingOrDisposed(ArtikelDetails) Then
                    ArtikelDetails = New wb_Artikel_Details
                End If
                ArtikelDetails.Show(DockPanel, DockState.Document)
                Return True
            Case "OPENHINWEISE"
                If IsNothingOrDisposed(ArtikelHinweise) Then
                    ArtikelHinweise = New wb_Artikel_Hinweise
                End If
                ArtikelHinweise.Show(DockPanel, DockState.Document)
                Return True
            Case "OPENPARAMETER"
                If IsNothingOrDisposed(ArtikelParameter) Then
                    ArtikelParameter = New wb_Artikel_Parameter
                End If
                ArtikelParameter.Show(DockPanel, DockState.Document)
                Return True
            Case "NEW"
                ArtikelNeuAnlegen()
                Return True
            Case "DELETE"
                ArtikelLöschen()
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
        ArtikelListe.Show(DockPanel, DockState.DockLeft)
        ArtikelListe.CloseButtonVisible = False
        WinBack.LayoutFilename = "Default"
    End Sub

    ''' <summary>
    ''' Gibt für den jeweiligen Form-Namen die entsprechenden Klasse zurück, die dann im Dock dargestellt wird.
    ''' Füllt das Array DockPanelList in der Basis-Klasse
    ''' </summary>
    ''' <param name="persistString"></param>
    ''' <returns></returns>
    Public Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_Artikel_Liste"
                ArtikelListe.CloseButtonVisible = False
                _DockPanelList.Add(ArtikelListe)
                Return ArtikelListe

            Case "WinBack.wb_Artikel_Details"
                ArtikelDetails = New wb_Artikel_Details
                _DockPanelList.Add(ArtikelDetails)
                Return ArtikelDetails

            Case "WinBack.wb_Artikel_Hinweise"
                ArtikelHinweise = New wb_Artikel_Hinweise
                _DockPanelList.Add(ArtikelHinweise)
                Return ArtikelHinweise

            Case "WinBack.wb_Artikel_Parameter"
                ArtikelParameter = New wb_Artikel_Parameter
                _DockPanelList.Add(ArtikelParameter)
                Return ArtikelParameter

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
        wb_Functions.CloseAndDisposeSubForm(ArtikelListe)
        wb_Functions.CloseAndDisposeSubForm(ArtikelDetails)
        wb_Functions.CloseAndDisposeSubForm(ArtikelHinweise)
        wb_Functions.CloseAndDisposeSubForm(ArtikelParameter)

        'alle Spuren in Artikel_Shared löschen
        wb_Artikel_Shared.Invalid()
    End Sub

    Public Sub ArtikelNeuAnlegen()
        Dim Komponente As New wb_Komponente
        Dim KompNrNeu As Integer = Komponente.MySQLdbNew(wb_Global.KomponTypen.KO_TYPE_ARTIKEL)
        ArtikelListe.RefreshData(KompNrNeu)
        Komponente = Nothing
    End Sub

    Public Sub ArtikelLöschen()
        'Sicherheitsabfrage Artikel löschen
        If MsgBox("Den Artikel " & wb_Artikel_Shared.Artikel.Nummer & " " & wb_Artikel_Shared.Artikel.Bezeichnung & " löschen?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim Komponente As New wb_Komponente
            Dim KompNrDel As Integer = wb_Rohstoffe_Shared.RohStoff.Nr
            If Komponente.MySQLdbCanBeDeleted(KompNrDel) Then
                Komponente.Nr = KompNrDel
                Komponente.MySQLdbDelete()
                ArtikelListe.RefreshData()
            Else
                MsgBox(Komponente.LastErrorText)
            End If
            Komponente = Nothing
        End If
    End Sub
End Class