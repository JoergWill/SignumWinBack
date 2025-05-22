Imports WinBack.wb_Schnittstelle_Shared
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Schnittstelle_SetupVorschau
    Inherits DockContent
    Private WithEvents Vorschau As wb_ArrayGridViewSchnittstelleVorschau
    Private sColNames As New List(Of String)

    'Anzahl x Zeilen (Vorschau) einlesen
    Const cAnzahlZeilen = 10

    Private Sub wb_Schnittstelle_SetupVorschau_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler eVorschauAktualisieren, AddressOf VorschauAktualisieren
    End Sub

    Private Sub VorschauAktualisieren(sender As Object)

        'GridArray zur Anzeige der ersten x Zeilen aus der Schnittstellen-Datei
        Dim VorschauDaten(cAnzahlZeilen)() As String
        'die ersten x Zeilen einlesen (Roh-Daten aus der Import-Datei)
        VorschauDaten = Txxxx(SetupTabelleIndex).ImportOpenAndReadVorschau(OpenFileDialog.FileName, cAnzahlZeilen, cbCalculate.Checked)

        'Liste der Tabellen-Überschriften aus den Feldnamen der ausgewählten Tabelle
        sColNames.Clear()
        'Felder leer (mit Index) vorbelegen
        For i = 0 To VorschauDaten(0).Length - 1
            sColNames.Add(i.ToString)
        Next

        'Feldnamen entsprechend dem Index eintragen
        For Each f In Fxxxx
            If f.Tabelle = Txxxx(SetupTabelleIndex).TabName AndAlso (f.Idx > 0) Then
                'falls die Datenquelle weniger Felder als die Tabellen-Definition hat
                While f.Idx > sColNames.Count
                    'werden die "fehlenden" Spalten erzeugt
                    sColNames.Add("")
                End While
                'Spaltenüberschrift entspricht dem Feldnamen (ohne Tabelle)
                'plus Leerzeichen für die Darstellung des Drop-Down-Elements in der Überschrift
                sColNames(f.Idx - 1) = f.Name.Substring(6) & "   "
            End If
        Next

        'falls schon Daten vorhanden sind, löschen
        If Not IsNothing(Vorschau) Then
            Vorschau.Dispose()
            Vorschau = Nothing
        End If

        'Daten im Grid anzeigen
        Vorschau = New wb_ArrayGridViewSchnittstelleVorschau(VorschauDaten, sColNames)
        Vorschau.ScrollBars = ScrollBars.None
        Vorschau.BackgroundColor = Me.BackColor
        Vorschau.GridLocation(pnlVorschau)
        Vorschau.PerformLayout()
        Vorschau.Refresh()

        'Gesamtbreite des Grid (Scrollbar im Panel)
        pnlVorschau.AutoScrollMinSize = Vorschau.AutoScrollSize

    End Sub

    Private Sub BtnImportFile_Click(sender As Object, e As EventArgs) Handles BtnImportFile.Click

        'Datei-Auswahl-Dialog mit Default-Werten vorbelegen
        OpenFileDialog.InitialDirectory = wb_GlobalSettings.ImportPath
        OpenFileDialog.FileName = Txxxx(SetupTabelleIndex).FileMask
        OpenFileDialog.Filter = SetupTabelleName & "|" & OpenFileDialog.FileName & "|Alle|*.*"

        If OpenFileDialog.ShowDialog = DialogResult.OK Then
            tbImportFile.Text = OpenFileDialog.FileName
            VorschauAktualisieren(sender)
        End If
    End Sub

    Private Sub cbCalculate_CheckedChanged(sender As Object, e As EventArgs) Handles cbCalculate.CheckedChanged
        VorschauAktualisieren(sender)
    End Sub

    ''' <summary>
    ''' Im Vorschau-Grid wurde im Header(Spalte) ein anderes Tabellenfeld(Idx) ausgewählt.
    ''' Tabellendefinition ändern und Vorschau neu anzeigen
    ''' </summary>
    ''' <param name="Spalte"></param>
    ''' <param name="Idx"></param>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
    Private Sub cbTabelle(Spalte As Integer, Idx As Integer) Handles Vorschau.cbIndexChanged
        'Debug.Print(Spalte & " " & Idx)
        'Debug.Print(wb_Schnittstelle_Shared.Fxxxx(Idx).Name)

        'das Feld Fxxxx mit dem Index Idx bekommt jetzt einen neuen Index Spalte
        For Each f In Fxxxx
            If f.Tabelle = SetupTabelleName AndAlso f.Idx = Idx Then
                f.Idx = Spalte
                Exit For
            End If
        Next
        'TODO das alte feld muss noch gelöscht werden !!

        'Änderungen in der Feld-Definition "live" anzeigen
        wb_Schnittstelle_Shared.VorschauAktualisieren()
    End Sub

    Private Sub wb_Schnittstelle_SetupVorschau_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler eVorschauAktualisieren, AddressOf VorschauAktualisieren
    End Sub

End Class