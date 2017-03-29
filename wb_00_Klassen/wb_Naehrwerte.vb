Imports System.IO
Imports System.Net
Imports System.Text

'Paket muss installiert werden
' can be installed from package manager console like this:
' install-package Newtonsoft.json
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class wb_Naehrwerte
    'Private Sub submit_Click(sender As System.Object, e As System.EventArgs) Handles submit.Click
    '    Dim user As String
    '    Dim pass As String
    '    user = uname.Text
    '    pass = passwd.Text

    '    Dim request As WebRequest = WebRequest.Create("http://domain.com/test.php")
    '    request.Method = "POST"
    '    Dim postData As String
    '    postData = "username=" & user & "&password=" & pass
    '    Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
    '    request.ContentType = "application/x-www-form-urlencoded"
    '    request.ContentLength = byteArray.Length
    '    Dim dataStream As Stream = request.GetRequestStream()
    '    dataStream.Write(byteArray, 0, byteArray.Length)
    '    dataStream.Close()
    '    Dim response As WebResponse = request.GetResponse()
    '    Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
    '    dataStream = response.GetResponseStream()
    '    Dim reader As New StreamReader(dataStream)
    '    Dim responseFromServer As String = reader.ReadToEnd()
    '    If responseFromServer = "0" Then
    '        MsgBox("Login Failed")
    '    Else
    '        MsgBox("json data")



    '        Dim json As String = responseFromServer
    '        Dim ser As JObject = JObject.Parse(json)
    '        Dim data As List(Of JToken) = ser.Children().ToList
    '        Dim output As String = ""

    '        For Each item As JProperty In data
    '            item.CreateReader()
    '            Select Case item.Name
    '                Case "comments"
    '                    output += "Comments:" + vbCrLf
    '                    For Each comment As JObject In item.Values
    '                        Dim u As String = comment("user")
    '                        Dim d As String = comment("date")
    '                        Dim c As String = comment("comment")
    '                        output += u + vbTab + d + vbTab + c + vbCrLf
    '                    Next

    '                Case "messages"
    '                    output += "Messages:" + vbCrLf
    '                    For Each msg As JObject In item.Values
    '                        Dim f As String = msg("from")
    '                        Dim t As String = msg("to")
    '                        Dim d As String = msg("date")
    '                        Dim m As String = msg("message")
    '                        Dim s As String = msg("status")
    '                        output += f + vbTab + t + vbTab + d + vbTab + m + vbTab + s + vbCrLf
    '                    Next

    '            End Select
    '        Next
    '        MsgBox(output)


    '    End If
    '    reader.Close()
    '    dataStream.Close()
    '    response.Close()
    'End Sub
End Class
