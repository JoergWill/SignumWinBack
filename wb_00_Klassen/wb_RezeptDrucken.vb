Imports combit.ListLabel22.DataProviders

Public Class wb_RezeptDrucken

    Private RootRezeptschritt As New List(Of wb_Rezeptschritt)

    Friend Sub Print()
        'Start mit der aktuellen Rezeptnummer (aus wb_RezeptShared)
        Dim RzNr As Integer = wb_Rezept_Shared.Rezept.RezeptNr
        'zur Liste hinzufügen
        AddRezeptToList(RzNr)

        'Drucken
        Debug.Print("Drucken ...")

        'Druck-Daten
        Dim pDialog As New wb_PrinterDialog(False) 'Drucker-Dialog

        'RootRezeptSchritt.Steps enthält alle Rezeptschritte als flache Liste 
        pDialog.LL.DataSource = New ObjectDataProvider(RootRezeptschritt)

        'List und Label-Verzeichnis für die Listen
        pDialog.ListSubDirectory = "Rezepte"
        pDialog.ListFileName = "RezepturStruktur.lst"
        pDialog.ShowDialog()
        pDialog = Nothing

    End Sub

    Private Sub AddRezeptToList(Nr As Integer)
        'Rezept laden
        Dim Rezept = New wb_Rezept(Nr, Nothing, 0.0)
        'Die Kopfdaten werden im Root-Rezept-Schritt eingetragen
        Rezept.RootRezeptSchritt.Nummer = Rezept.RezeptNummer
        Rezept.RootRezeptSchritt.Kommentar = Rezept.RezeptKommentar
        'Anzeige der Rezept-Variante im Gruppenkopf
        Rezept.RootRezeptSchritt.OberGW = Rezept.Variante
        'Anzeige Einheit Rezept-Gesamtmenge im Gruppenfuß
        'TODO sollte aus Recources-Datei kommen(Übersetzung!)
        Rezept.RootRezeptSchritt.Einheit = "kg"

        'zur Liste hinzufügen
        RootRezeptschritt.Add(Rezept.RootRezeptSchritt)

        'Scan über alle Rezeptschritte
        For Each ChildRzSchritt As wb_Rezeptschritt In Rezept.RootRezeptSchritt.ChildSteps
            If ChildRzSchritt.RezeptNr > 0 Then
                AddRezeptToList(ChildRzSchritt.RezeptNr)
            End If
        Next

    End Sub
End Class
