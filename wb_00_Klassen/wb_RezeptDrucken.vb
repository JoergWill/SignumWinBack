Imports combit.ListLabel22.DataProviders

Public Class wb_RezeptDrucken

    Private RezeptSteps As New List(Of wb_Rezeptschritt)
    Private RezeptListe As New List(Of wb_Rezept)

    Friend Sub Print()
        'Start mit der aktuellen Rezeptnummer (aus wb_RezeptShared)
        Dim RzNr As Integer = wb_Rezept_Shared.Rezept.RezeptNr
        'zur Liste hinzufügen
        AddRezeptToList(RzNr)
        '(Flache) Liste aller Rezeptschritte
        AddRezeptStepsToList()

        'Drucken
        Debug.Print("Drucken ...")

        'Druck-Daten
        Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog

        'RootRezeptSchritt.Steps enthält alle Rezeptschritte als flache Liste 
        pDialog.LL.DataSource = New ObjectDataProvider(RezeptSteps)
        'Anzeige Einheit Rezept-Gesamtmenge im Gruppenfuß
        pDialog.LL_Parameter_1 = "kg"

        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Rezepte"
        pDialog.ListFileName = "RezepturStruktur.lst"
        pDialog.ShowDialog()
        pDialog = Nothing

    End Sub

    Private Sub AddRezeptToList(Nr As Integer)
        'Endlos-Schleifen oder doppelte Rezepte verhindern
        For Each Rzpt As wb_Rezept In RezeptListe
            If Rzpt.RezeptNr = Nr Then
                Exit Sub
            End If
        Next

        'Rezept laden
        Dim Rezept = New wb_Rezept(Nr, Nothing, 0.0, 1, "", "", True, False)
        'zur Liste hinzufügen
        RezeptListe.Add(Rezept)

        'Scan über alle Rezeptschritte
        For Each ChildRzSchritt As wb_Rezeptschritt In Rezept.RootRezeptSchritt.Steps
            Debug.Print("Print Rezept - Steps " & ChildRzSchritt.Nummer & " " & ChildRzSchritt.VirtTreeBezeichnung)
            If ChildRzSchritt.RezeptNr > 0 Then
                AddRezeptToList(ChildRzSchritt.RezeptNr)
            End If
        Next
    End Sub

    Private Sub AddRezeptStepsToList()
        For Each Rzpt As wb_Rezept In RezeptListe
            For Each ChildRzSchritt As wb_Rezeptschritt In Rzpt.RootRezeptSchritt.Steps
                'Die Kopfdaten werden im Root-Rezept-Schritt eingetragen
                ChildRzSchritt.Idx = Rzpt.RezeptNr
                ChildRzSchritt.Par1 = Rzpt.RezeptNummer
                ChildRzSchritt.Par2 = Rzpt.RezeptBezeichnung
                ChildRzSchritt.Par3 = Rzpt.Variante
                ChildRzSchritt.RezGewicht = Rzpt.RezeptGewicht
                'Rezeptschritt zur (flachen) Liste hinzufügen
                RezeptSteps.Add(ChildRzSchritt)
            Next
        Next
    End Sub

End Class
