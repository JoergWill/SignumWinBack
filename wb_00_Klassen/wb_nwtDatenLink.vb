Imports System.Collections
Imports System.IO
Imports System.Net
Imports System.Text

'Paket muss installiert werden
' can be installed from package manager console like this:
' install-package Newtonsoft.json
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class wb_nwtDatenLink
    Private _pat As String
    Private _cat As String
    Private _url As String
    Private _errorCode As String

    Dim XMLdata As New List(Of String)
    Dim JSONdata As New List(Of JToken)

    Sub New(PAT As String, CAT As String, Url As String)
        'PAT - Application Token
        _pat = PAT
        'CAT - Company Token
        _cat = CAT
        'Url
        _url = Url
    End Sub

    Public WriteOnly Property PAT As String
        Set(value As String)
            _pat = value
        End Set
    End Property

    Public WriteOnly Property CAT As String
        Set(value As String)
            _cat = value
        End Set
    End Property

    Public WriteOnly Property Url As String
        Set(value As String)
            _url = value
        End Set
    End Property

    ''' <summary>
    ''' Datnlink-Connector::GetErrorCode(Array) 
    ''' 
    ''' Gibt den Fehler-Code der Abfrage zurück
    ''' </summary>
    Public ReadOnly Property ErrorCode As String
        Get
            Return _errorCode
        End Get
    End Property

    ''' <summary>
    ''' Datenlink-Connector::GetResult(Array) 
    ''' 
    ''' Gibt das Ergebnis der Abfrage als JSON-String zurück
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <param name="Index"> Integer Index des Result-JSON-Objekts</param>
    Public Function GetResult(Index As Integer) As String
        If Index = JSONdata.Count - 1 Then
            Return JSONdata(Index).ToString
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Datenlink-Connector::GetXMLResult(Array) 
    ''' 
    ''' Gibt das Ergebnis der Abfrage als XML-String zurück
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Public Function GetXMLResult() As String
        Return XMLdata(0)
    End Function


    ''' <summary>
    ''' Datenlink-Connector::http-Kommando zusammensetzen
    ''' 
    ''' Setzt den HTTP-String entsprechend dem angeforderten Kommando zusammen
    ''' und setzt die Anfrage ab
    ''' </summary>
    ''' <param name="cmd"> String Kommando</param>
    ''' <param name="param"> String Kommando-Parameter</param>
    ''' <param name="service"> String</param>
    Private Function httpString(cmd As String, param As String, service As String) As Integer
        ' Internet-Connector
        Dim datastream As Stream
        Dim request As WebRequest
        Try
            request = WebRequest.Create(_url & cmd & ".json?Lang=DEU")
            request.Timeout = 10000
            ' Post or Get
            If cmd = "getComponentList" Then
                request.Method = "GET"
            Else
                request.Method = "POST"
            End If
            ' Authorisierung mit Application-Token
            request.Headers.Clear()
            request.Headers.Add("X-DL-TOKEN-APPLICATION:" & _pat)
            If service = "company" Then
                request.Headers.Add("X-DL-TOKEN-COMPANY:" & _cat)
            End If

            'Post-Data
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(param)
            request.ContentLength = byteArray.Length
            request.ContentType = "application/x-www-form-urlencoded"
            datastream = request.GetRequestStream()
            datastream.Write(byteArray, 0, byteArray.Length)
            datastream.Close()

            ' Antwort (OK)
            Dim response As WebResponse = request.GetResponse()
            _errorCode = CType(response, HttpWebResponse).StatusDescription
            Debug.Print("WebResponse " & _errorCode)

            ' Ergebnis-String
            If _errorCode = "OK" Then
                datastream = response.GetResponseStream()
                Dim reader As New StreamReader(datastream)
                Dim responseFromServer As String = reader.ReadToEnd()
                'Debug.Print("Response from Server :" & responseFromServer)
                Dim ser As JObject = JObject.Parse(responseFromServer)

                ' Status = ERROR
                If ser.SelectToken("status") = "ERROR" Then
                    'Abhängig vom Kommando auswerten
                    Select Case cmd
                        Case "lookupProduct"
                            _errorCode = ser.SelectToken("data.error_code")
                        Case "getProductVersionData"
                            _errorCode = ser.SelectToken("data.error_code")
                        Case "getDistributorData"
                            _errorCode = ser.SelectToken("data")
                    End Select
                    Return -1
                Else
                    'Abhängig vom Kommando auswerten
                    Select Case cmd
                        'Validate erfolgreich
                        Case "validateCompanyToken"
                            Return -(ser.SelectToken("status") = "SUCCESS")
                        'Anzahl der Datensätze in JSON data.meta_data.result_data.products_total_count 
                        Case "lookupProduct"
                            Return ser.SelectToken("data.meta_data.result_data.products_total_count")
                        'Daten werden als Base64-kodiertes XML-Files zurückgegeben
                        Case "getProductVersionData"
                            'Daten dekodieren
                            If ser.SelectToken("status") = "SUCCESS" Then
                                Dim b64data As String = ser.SelectToken("data.base64_data")
                                Dim b64byte As Byte() = Convert.FromBase64String(b64data)
                                'Ergebnis-String (XML)
                                XMLdata.Clear()
                                XMLdata.Add(System.Text.Encoding.ASCII.GetString(b64byte))
                                Return 1
                            Else
                                Return -1
                            End If
                        'Daten als JSON
                        Case "getDistributorData"
                            Return -(ser.SelectToken("status") = "SUCCESS")
                        Case Else
                            Return -1
                    End Select
                End If
            Else
                Return -1
            End If
        Catch e As Exception
            _errorCode = e.ToString
            Return -9
        End Try
    End Function

    ''' <summary>
    ''' Datenlink-Connector::lookupProduct by article_description
    ''' 
    ''' Gibt die Anzahl der Datensätze zurück, deren Name dem Suchtext entspricht
    ''' Die Datensätze können über GetResult abgefragt werden. 
    ''' </summary>
    ''' <param name="ds"> String Suchbegriff Rohstoff-Bezeichnung</param>
    Public Function lookupProductName(ds As String) As Integer
        ' Kommando lookupProduct
        Dim nCmd = "lookupProduct"
        Dim nPrm = "identifier=PRODUCTNAME&value=" + ds + "&results_per_page=50"
        Return httpString(nCmd, nPrm, "company")
    End Function

    ''' <summary>
    ''' Datenlink-Connector::getProductData
    ''' 
    ''' Liest das Produktdatenblatt des Rohstoffen mit der angegebenen ID aus den Cloud.
    ''' Die Datensätze können über GetResult abgefragt werden. 
    ''' </summary>
    ''' <param name="id"> String Rohstoff-ID</param>
    Public Function GetProductData(id As String) As Integer
        ' Kommando getProductData
        Dim nCmd = "getProductVersionData"
        Dim nPrm = "datenlink_id=" & id
        Return httpString(nCmd, nPrm, "company")
    End Function

    ''' <summary>
    ''' Datenlink-Connector::getDistributorData
    ''' 
    ''' Liest die Lieferantendaten des Rohstoffes mit der angegebenen ID aus den Cloud.
    ''' Die Datensätze können über GetResult abgefragt werden. 
    ''' </summary>
    ''' <param name="id"> String Rohstoff-ID</param>
    Public Function getDistributorData(id As String) As Integer
        ' Kommando getDistributorData
        Dim nCmd = "getDistributorData"
        Dim nPrm = "id=" & id
        Return httpString(nCmd, nPrm, "program")
    End Function

    ''' <summary>
    ''' Datenlink-Connector::validateCompanyToken
    ''' 
    ''' </summary>
    Public Function validateCompanyToken()
        ' Kommando validateCompanyToken
        Dim nCmd = "validateCompanyToken"
        Dim nPrm = "token=" & _cat
        Return httpString(nCmd, nPrm, "program")
    End Function

    ''' <summary>
    ''' Datenlink-Connector::DebugResultSet
    ''' 
    ''' Gibt das JSON-Object mit dem angegebene Index als String aus
    ''' </summary>
    ''' <param name="Index"> Integer Anzahl der Datensätze</param>
    Public Sub DebugResultSet(Index As Integer)
        If Index < XMLdata.Count And Index >= 0 Then
            Console.WriteLine(XMLdata(Index).ToString)
        Else
            Console.WriteLine("Fehler " & Me.ErrorCode)
        End If
    End Sub

End Class

