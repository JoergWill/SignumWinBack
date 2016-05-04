Imports MySql
Imports MySql.Data
Imports MySql.Data.MySqlClient
'...
Namespace OrgasoftMain
    Public Class MySqlConnect

        Private Connected As Boolean

        Property IsConnected As Boolean
            Get                         'Die Get-Klausel gibt den angegebenen Wert zurück, hier "m_Name".
                Return Connected
            End Get
            Set(ByVal value As Boolean) 'Die Set-Klausel gibt es bei ReadOnly-Propertys nicht, da Set einen Wert ändert.
                Connected = value
            End Set
        End Property

        'Dim conn As New MySqlConnection
        '    Dim myConnectionString As String


        '    myConnectionString = "server=host;uid=user;pwd=pw;database=db; "
        'conn.ConnectionString = myConnectionString
        'Try
        'conn.Open()
        'Catch ex As Exception
        'MessageBox.Show(ex.Message)
        'End Try
        'conn.close
    End Class
End Namespace
