Public Class wb_OrgaBackProcess_WE
    Inherits wb_OrgaBackProcess

    Public Sub New(ProcessCode As String, ProcessNumber As String)
        'Daten aus [dbo].[GeschäftsvorfallPosition] einlesen
        MyBase.New(ProcessCode, ProcessNumber)
        'Debug-Ausgabe
        Debug.Print("WinBack.ob_Main.ProcessChanged Wareneingang verbucht Vorfall-Nummer " & ProcessNumber)
    End Sub

    Public Overrides Function DoAction(Action As String) As Boolean
        'test
        Return False
    End Function

End Class
