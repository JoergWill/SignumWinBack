Public Class wb_AktSysKonfig
    Inherits wb_AktRechte

    ''' <summary>
    ''' Liest die freigegebenen Module aus der Tabelle ItemParameter.
    ''' Die System-Konfiguration wird gespeichert unter User-Gruppe -1
    ''' 
    ''' Die (fehlenden) Daten werden über DBUpdate in die Tabelle ItemParameter eingetragen
    '''     C:\Dokumente\Projekte\Signum_WinBack\DBUpdate\2.30_AktSysKonfig.sql
    ''' </summary>
    Shared Sub New()
        'Systemkonfiguration aus Tabelle ItemParameter einlesen (User-Gruppe -1)
        UpdateUserGruppenRechteTabelle(wb_Global.SysKonfigGrpe)
    End Sub

    ''' <summary>
    ''' Globale System-Konfiguation.
    ''' 
    ''' Unabhängig vom angemeldeten Benutzer werden nicht lizensierte Elemente ausgeblendet
    ''' (User-Gruppe -1)
    ''' </summary>
    ''' <param name="Tag"></param>
    ''' <returns></returns>
    Friend Shared Function SysKonfigOK(Tag As String, Superuser As Boolean) As Boolean
        Return RechtOK(Tag, Superuser, wb_Global.SysKonfigGrpe)
    End Function
End Class
