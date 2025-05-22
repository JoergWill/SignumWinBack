'---------------------------------------------------------
'17.03.2017/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Ableitung der Klasse System.Windows.Forms.TabControl'

'Add a HideTabs property to turn on/off TabItems
'aus: http://dotnetrix.co.uk/tabcontrol.htm

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<ToolboxBitmap(GetType(System.Windows.Forms.TabControl))>
Public Class wb_TabControl
    Inherits System.Windows.Forms.TabControl
    Private m_HideTabs As Boolean

    <DefaultValue(False), RefreshProperties(RefreshProperties.All)>
    Public Property HideTabs() As Boolean
        Get
            Return m_HideTabs
        End Get
        Set(ByVal Value As Boolean)
            If m_HideTabs = Value Then Return
            m_HideTabs = Value
            If Value = True Then Me.Multiline = True
            Me.UpdateStyles()
        End Set
    End Property

    <RefreshProperties(RefreshProperties.All)>
    Public Overloads Property Multiline() As Boolean
        Get
            If Me.HideTabs Then Return True
            Return MyBase.Multiline
        End Get
        Set(ByVal Value As Boolean)
            If Me.HideTabs Then
                MyBase.Multiline = True
            Else
                MyBase.Multiline = Value
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property DisplayRectangle() As System.Drawing.Rectangle
        Get
            If Me.HideTabs Then
                Return New Rectangle(0, 0, Width, Height)
            Else
                Dim tabStripHeight, itemHeight As Int32

                If Me.Alignment <= TabAlignment.Bottom Then
                    itemHeight = Me.ItemSize.Height
                Else
                    itemHeight = Me.ItemSize.Width
                End If

                If Me.Appearance = TabAppearance.Normal Then
                    tabStripHeight = 5 + (itemHeight * Me.RowCount)
                Else
                    tabStripHeight = (3 + itemHeight) * Me.RowCount
                End If
                Select Case Me.Alignment
                    Case TabAlignment.Top
                        Return New Rectangle(4, tabStripHeight, Width - 8, Height - tabStripHeight - 4)
                    Case TabAlignment.Bottom
                        Return New Rectangle(4, 4, Width - 8, Height - tabStripHeight - 4)
                    Case TabAlignment.Left
                        Return New Rectangle(tabStripHeight, 4, Width - tabStripHeight - 4, Height - 8)
                    Case TabAlignment.Right
                        Return New Rectangle(4, 4, Width - tabStripHeight - 4, Height - 8)
                End Select
            End If
        End Get
    End Property

End Class
