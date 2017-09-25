Public Class wb_DockBarPanelShared
    ''' <summary>
    ''' Liest die zuletzt gespeicherte Fenster-Position aus der winback.ini und setzt die entsprechenden 
    ''' Parameter im übergebenen Fenster.
    ''' Der File-Name der letzten aktuellen Dock-Bar-Konfiguration wird im Tag-Objekt gespeichert !!
    ''' </summary>
    ''' <param name="xForm"></param>
    ''' <param name="IniSektion"></param>
    Public Shared Sub SetFormBoundaries(xForm As Windows.Forms.Form, IniSektion As String)
        If xForm IsNot Nothing Then

            Dim IniFile As New wb_IniFile

            'Fensterposition aus winback.ini
            xForm.Top = IniFile.ReadInt(IniSektion, "Top")
            xForm.Left = IniFile.ReadInt(IniSektion, "Left")
            xForm.Width = IniFile.ReadInt(IniSektion, "Width")
            xForm.Height = IniFile.ReadInt(IniSektion, "Height")

            'Dispose
            IniFile = Nothing
        End If
    End Sub

    Public Shared Sub SaveFormBoundaries(Top As Integer, Left As Integer, Width As Integer, Height As Integer, LayoutFile As String, IniSektion As String)
        Dim IniFile As New wb_IniFile

        'Fensterposition in winback.ini sichern
        IniFile.WriteInt(IniSektion, "Top", Top)
        IniFile.WriteInt(IniSektion, "Left", Left)
        IniFile.WriteInt(IniSektion, "Width", Width)
        IniFile.WriteInt(IniSektion, "Height", Height)

        'Layout-File
        IniFile.WriteString(IniSektion, "LayoutFileName", LayoutFile)

        'Dispose
        IniFile = Nothing
    End Sub
End Class
