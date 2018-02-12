Public Class wb_AktSysKonfig
    Inherits wb_AktRechte

    ''' <summary>
    ''' Liest die freigegebenen Module aus der Tabelle ItemParameter.
    ''' Die System-Konfiguration wird gespeichert unter User-Gruppe -1
    ''' </summary>
    Shared Sub New()
        'Systemkonfiguration aus Tabelle ItemParameter einlesen (User-Gruppe -1)
        UpdateUserGruppenRechteTabelle(-1)
    End Sub

    ''' <summary>
    ''' Globale System-Konfiguation.
    ''' 
    ''' Unabhängig vom angemeldeten Benutzer werden nicht lizensierte Elemente ausgeblendet
    ''' (User-Gruppe -1)
    ''' </summary>
    ''' <param name="Tag"></param>
    ''' <returns></returns>
    Friend Shared Function SysKonfigOK(Tag As String) As Boolean
        Return RechtOK(Tag, False)
    End Function
End Class
