''' <summary>
''' Finalize wird aufgerufen, wenn die Applikation (OrgaSoft) beendet wird.
'''     Im Log-File werden die letzten Einträge gesichert und die Datei wird geschlossen.
'''     
''' Implement the “static finalizer” idiom to remove dispose the resource at the end of the application lifetime
''' aus https://stackoverflow.com/questions/5633527/how-to-dispose-shared-variable-in-vb-net
''' </summary>
Public Class wb_Finalizer_Shared
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        'Letzter Eintrag im Log-File
        Trace.WriteLine("@I_Log-File geschlossen")
    End Sub
End Class
