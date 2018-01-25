Imports WeifenLuo.WinFormsUI.Docking

''' <summary>
''' MDI-Main-Fenster Rohstoff-Vewaltung
''' Abgeleitet von DockPanel_Main
'''     In DockPanelMain werden alle Funktionen für die Verwaltung der Layouts abgewickelt
'''     Default-Layout, Laden, Sichern, Löschen der Layouts
'''     Die Layouts werden, im Unetrschied zu OrgaBack nicht vom jeweiligen Fenster sondern
'''     von der WinBack-Main-Form verwaltet.
''' </summary>
Public Class Rohstoffe_Main

    Public RohstoffListe As New wb_Rohstoffe_Liste
    Public RohstoffDetails As New wb_Rohstoffe_Details
    Public RohstoffVerwendung As New wb_Rohstoffe_Verwendung
    Public RohstoffParameter As New wb_Rohstoffe_Parameter

    ''' <summary>
    ''' Execute-Command von Winback-Main-Form.
    ''' Routine wird von Winback-Main aufgerufen um verschiedene Funktionen in der MDI-Form auszuführen.
    ''' 
    '''     OPENDETAILS         -   Detail-Fenster wird geöffnet und angezeigt.
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
                RohstoffDetails = New wb_Rohstoffe_Details
                RohstoffDetails.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENVERWENDUNG"
                RohstoffVerwendung = New wb_Rohstoffe_Verwendung
                RohstoffVerwendung.Show(DockPanel, DockState.DockLeft)
                Return True
            Case "OPENPARAMETER"
                RohstoffParameter = New wb_Rohstoffe_Parameter
                RohstoffParameter.Show(DockPanel, DockState.DockLeft)
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
        RohstoffListe.Show(DockPanel, DockState.DockLeft)
        RohstoffListe.CloseButtonVisible = False
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
            Case "WinBack.wb_Rohstoff_Liste"
                _DockPanelList.Add(RohstoffListe)
                Return RohstoffListe
            Case "WinBack.wb_Rohstoff_Details"
                _DockPanelList.Add(RohstoffDetails)
                Return RohstoffDetails
            Case "WinBack.wb_Rohstoff_Verwendung"
                _DockPanelList.Add(RohstoffVerwendung)
                Return RohstoffVerwendung
            Case "WinBack.wb_Rohstoff_Parameter"
                _DockPanelList.Add(RohstoffParameter)
                Return RohstoffParameter
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
        'HashTable mit der Übersetzung der Gruppen-Nummer zu Gruppen-Bezeichnung
        wb_Rohstoffe_Shared.Load_RohstoffTables()
    End Sub

    ''' <summary>
    ''' MDI-Form wird geschlossen. Vorher wurde schon in der Basis-Klasse die DockBar-Konfiguration gesichert.
    ''' Schliesst alle erzeugten Fenster.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub FormClose(Sender As Object, e As FormClosedEventArgs)
        'alle erzeugten Fenster wieder schliessen
        RohstoffDetails.Close()
        RohstoffListe.Close()
    End Sub
End Class