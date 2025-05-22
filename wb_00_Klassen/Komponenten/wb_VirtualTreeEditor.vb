Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Design
Imports Infralution.Controls.VirtualTree
Imports WinBack

Public Class wb_VirtualTreeEditor
    Inherits Drawing.Design.UITypeEditor

    Public Overloads Sub PaintValue(value As Object, canvas As Graphics, rectangle As Rectangle)
        'Debug.Print("Paint Value")
    End Sub

    '
    ' Zusammenfassung:
    '     Zeichnet die Darstellung eines Objektwerts mit dem angegebenen System.Drawing.Design.PaintValueEventArgs.
    '
    ' Parameter:
    '   e:
    '     Eine System.Drawing.Design.PaintValueEventArgs-Klasse, die die zu zeichnenden
    '     Werte und den Zeichenbereich angibt.
    Public Overrides Sub PaintValue(e As PaintValueEventArgs)
        'Debug.Print("Paint Value")
    End Sub
End Class
