Public Class wb_DockBarPanelGlobal
    Public Shared Function DkPnlConfigName(FileName As String, FormName As String) As String
        'Extension entfernen
        FileName = System.IO.Path.GetFileNameWithoutExtension(FileName)
        'wb... entfernen
        FileName = FileName.Replace("wb", "")

        'Prüfen ob der Filename zu diesem Fenster gehört
        If InStr(FileName, FormName) = 1 Then
            'Form-Name entfernen
            Return FileName.Replace(FormName, "")
        Else
            'File gehört nicht zur Form
            Return ""
        End If
    End Function

    Public Shared Function GetDkPnlConfigNameList(DirName As String, FormName As String) As IList(Of String)
        'Ordner-Name ohne Backslash am Ende
        Dim oDir As New IO.DirectoryInfo(DirName.TrimEnd("\"))
        'Ergebnis-Array
        Dim FileNames As New List(Of String)
        FileNames.Clear()

        ' alle Dateien des Ordners
        Dim oFiles As System.IO.FileInfo() = oDir.GetFiles("*.xml")
        Dim oFile As System.IO.FileInfo
        ' Layout-Name
        Dim LayoutName As String = ""

        ' Datei-Array durchlaufen und in ListBox übertragen
        For Each oFile In oFiles
            LayoutName = wb_DockBarPanelGlobal.DkPnlConfigName(oFile.Name, FormName)
            If LayoutName <> "" Then
                FileNames.Add(LayoutName)
            End If
        Next

        Return FileNames
    End Function
End Class
