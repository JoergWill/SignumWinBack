Imports System.IO

Public Class wb_Kocher_Sync

    ''' <summary>
    ''' Liest zur übergebenen Kocher-Nummer das FTP-Verzeichnis des CX ein und prüft ob neue Rezepte vorhanden sind.
    ''' Anschliessend wird ein Sync durchgeführt.
    ''' Wird von Server-Task zyklisch aufgerufen.
    ''' </summary>
    ''' <param name="KNr"></param>
    ''' <returns></returns>
    Public Function CheckKocher(KNr) As Integer
        'Kocher/Röster
        Dim Kocher As wb_Kocher = wb_Kocher_Global.KocherListe(KNr)

        'Verzeichnis und Verbindungs-Status des Kochers ermitteln
        If Kocher.GetRezeptListe() = wb_Global.Kocher_VerbindungsStatus.OK Then
            'neue oder geänderte Rezepte auf dem Master werden in die Datenbank konvertiert und eingetragen

            If KNr <> wb_Global.KocherMaster Then
                'Synchronisation mit Master durchführen (neue Rezepte von Slave zum Master)
                Kocher.SyncToMaster(wb_Kocher_Global.KocherListe(wb_Global.KocherMaster))
            Else
                'Sychronisation mit WinBack-Datenbank
                Kocher.SynctoDB()
            End If
        End If

        'weiter mit dem nächsten Kocher
        KNr += 1
        'wenn der Anzahl der Kocher erreicht ist, wird wieder die Nummer des ersten Kochers(Master) zurückgegeben
        If KNr > wb_Kocher_Global.AnzahlKocher Then
            'Start mit Master
            KNr = wb_Global.KocherMaster
        End If
        Return KNr
    End Function

End Class
