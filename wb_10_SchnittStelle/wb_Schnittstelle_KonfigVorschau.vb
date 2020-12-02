Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Schnittstelle_Shared

Public Class wb_Schnittstelle_KonfigVorschau
    Inherits DockContent
    Dim VorschauGrid As wb_ArrayGridViewSchnittstelle
    Dim VorschauResultData As New ArrayList


    Private Sub wb_Schnittstelle_KonfigVorschau_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Event-Handler (Aktualisierung der Vorschau)
        AddHandler eVorschauAktualisieren, AddressOf VorschauAktualisieren

    End Sub

    Public Sub VorschauAktualisieren(Sender As Object)
        'falls schon Daten vorhanden sind, löschen
        If Not IsNothing(VorschauGrid) Then
            VorschauGrid.Dispose()
            VorschauGrid = Nothing
        End If

        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String)
        For Each TabFeld In SchnittstelleTabelle.TabFelder
            sColNames.Add(TabFeld.Name)
        Next

        'Vorschau-Grid erstellen
        VorschauResultData.Clear()
        'Maximale Anzahl der Vorschau-Zeilen
        With SchnittstelleTabelle
            Dim MaxLines As Integer = Math.Min(AllLines.Count - .FirstRealLine, 11) - 1
            'alle Zeilen (maximal 10) ausgeben
            For i = .FirstRealLine To MaxLines
                .CheckFormat(AllLines(i))
                VorschauResultData.Add(.ResultFelder)
            Next
        End With


        'Daten im Grid anzeigen
        VorschauGrid = New wb_ArrayGridViewSchnittstelle(VorschauResultData, sColNames)
        VorschauGrid.ScrollBars = ScrollBars.Vertical
        VorschauGrid.BackgroundColor = Me.BackColor
        VorschauGrid.GridLocation(pnlVorschau)
        VorschauGrid.PerformLayout()
        VorschauGrid.Refresh()
        'Cursor wieder umschalten
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub wb_Schnittstelle_KonfigVorschau_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler eVorschauAktualisieren, AddressOf VorschauAktualisieren
    End Sub
End Class


