Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Linien_Kneter
    Inherits DockContent

    Dim KneterSPS As New List(Of wb_KneterSteuerung)
    Dim KneterAnzahl As Integer = 0
    Const MB100 = 100

    Sub New()

        'Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'alle SPS-Steuerungen mit den verknüpften Knetern
        KneterSetup()

        'alle Kneter im Panel anzeigen
        For Each SPS As wb_KneterSteuerung In KneterSPS
            For Each Kn In SPS.KneterListe
                tlpKneter.Controls.Add(Kn)
            Next
        Next

        'zyklische Abfrage (Timer aktivieren)
        tTwinCatADS.Enabled = True

    End Sub

    ''' <summary>
    ''' Liest die Konfiguration der Kneter-Steuerung aus der winback-Datenbank und erzeugt die entsprechenden Kneter-Anzeige Objekte.
    ''' 
    ''' Die Anzahl der Kneter steht in winback.AnlagenParameter (AP_Typ_Nr = 118 AND AP_ID_Nr = 0 AND AP_Param_Nr = 7
    ''' Die Kneter-Nummer(Bezeichnung) steht in ....
    ''' 
    ''' Es wird zunächst ein Array von SPS-Steuerungen mit der dazugehörigen IP-Adresse erzeugt.
    ''' Jedes SPS-Steuerungs-Objekt enthält eine Liste von Objekten zur Anzeige der Kneter (1..4) und
    ''' eine Verbindung zur Steuerung(wb_TwinCatADS)
    ''' 
    ''' Im Anzeige-Objekt (wb_KneterAnzeige) ist das Kneter-Objekt(wb_Kneter) enthalten. Der Kneter liest seine 
    ''' aktuellen Daten aus dem Memory-Stream der SPS-Steuerung.
    ''' 
    ''' Jedes Kneter-Objekt hält eine Liste mit Kneterschritten(wb_Kneterschritte), die aus der Datenbank und aus der SPS-Steuerung
    ''' gebildet werden.
    ''' </summary>
    Private Sub KneterSetup()
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim KneterIP As String
        Dim KneterNr As Integer = 0

        'Anzahl der Kneter aus winback.AnlagenParameter
        If winback.sqlSelect(wb_Sql_Selects.sqlKneterAnzahl) AndAlso winback.Read Then
            KneterAnzahl = winback.iField("AP_Wert1int")
        End If
        winback.CloseRead()

        'alle Steuerungen mit BC9-Typ=16 (Kneter)
        If winback.sqlSelect(wb_Sql_Selects.sqlKneterBCListe) Then
            While winback.Read
                'IP-Adresse Kneter-BC
                KneterIP = wb_GlobalSettings.IPBasisAdresse & "." & winback.sField("BC9_IpAdresse")
                'neue Kneter SPS-Steuerung
                Dim SPS As New wb_KneterSteuerung(KneterIP)

                'Liste aller Kneter für diese Steuerung (maximal 4)
                For i = 1 To 4
                    'Anzahl der Kneter hochzählen
                    KneterNr += 1
                    'Wenn die Maximalzahl der Kneter noch nicht erreicht ist...
                    If KneterNr <= KneterAnzahl Then
                        'Kneter zur Liste hinzufügen
                        Dim Kneter As New wb_KneterAnzeige_HD(KneterNr)
                        SPS.KneterListe.Add(Kneter)
                    End If
                Next

                'Liste aller Kneter-Steuerungen
                KneterSPS.Add(SPS)
            End While
        End If
    End Sub

    Private Sub tTwinCatADS_Tick(sender As Object, e As EventArgs) Handles tTwinCatADS.Tick

        'alle Steuerungen abfragen
        For Each SPS As wb_KneterSteuerung In KneterSPS
            Dim AnzahlBytes As Integer = SPS.KneterListe.Count * wb_KneterAnzeige_HD.cReadMem

            'TwinCat verbinden
            If SPS.TryConnect() Then
                If SPS.ReadMem(MB100, AnzahlBytes) Then
                    'alle Kneter in dieser Steuerung (zyklisch)abfragen
                    For Each Kn In SPS.KneterListe
                        Kn.ReadFromStream(SPS.TwinCatADS.BinReader)
                    Next
                End If
            Else
                Debug.Print("Keine Verbindung zur SPS-Steuerung " & SPS.Ip)
            End If
        Next
    End Sub

End Class