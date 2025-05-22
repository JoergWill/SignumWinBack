Imports System.Timers

Public Class wb_DashRezept
    Inherits wb_DashElement

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'Titeltext der Kachel
        Title = "Rezepte"
        'User-Rechte werden über Tag abgebildet
        Tag = 103
    End Sub

    Private Sub wb_Dash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Hintergrundbild der Kachel
        Icon = WinBack.My.Resources.Resources.MainRezept_64x64
        'Wert(Anzahl der Artikel) Hintergrund-Farbe
        WertBackColor = Drawing.Color.LightGray
    End Sub

    ''' <summary>
    ''' Click auf Artikel - Main-Form-Artikel aufrufen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Overrides Sub wb_DashElement_Click(sender As Object, e As EventArgs)
        'Rezeptverwaltung aufrufen
        wb_Main_Shared.OpenForm(sender, "Rezepte")
    End Sub

    ''' <summary>
    ''' Nach dem Anzeigen des Artikel-Dash wird ermittelt, wieviele Artikel in der WinBack-Datenbank
    ''' vorhanden sind.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub StartupTimer_Tick(sender As Object, e As EventArgs)
        ' Timer beenden (OneShot)
        MyBase.StartupTimer_Tick(sender, e)

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Abfrage Anzahl der Rezepte (Haupt-Variante)
        If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezAnzahl, " = 1")) AndAlso winback.Read Then
            Wert = winback.iField("Anzahl").ToString
        End If
        'Datenbank wieder schliessen
        winback.Close()
    End Sub
End Class
