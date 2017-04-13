Imports System.Collections
Imports System.IO
Imports System.Net
Imports System.Text

'Paket muss installiert werden
' can be installed from package manager console like this:
' install-package Newtonsoft.json
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class wb_nwtCloud
    Private _pass As String
    Private _url As String
    Private _errorCode As HttpStatusCode
    Private data As List(Of JToken)

    Sub New(Pass As String, Url As String)
        'Passwort
        _pass = Pass
        'Url
        _url = Url
    End Sub

    Public WriteOnly Property Pass As String
        Set(value As String)
            _pass = value
        End Set
    End Property

    Public WriteOnly Property Url As String
        Set(value As String)
            _url = value
        End Set
    End Property

    ''' <summary>
    ''' WinBack-Cloud-Connector::GetErrorCode(Array) 
    ''' 
    ''' Gibt den Fehler-Code der Abfrage zurück
    ''' </summary>
    Public ReadOnly Property ErrorCode As String
        Get
            If _errorCode = HttpStatusCode.OK Then
                Return "OK"
            Else
                Return ""
            End If
        End Get
    End Property

    ''' <summary>
    ''' WinBack-Cloud-Connector::GetResult(Array) 
    ''' 
    ''' Gibt das Ergebnis der Abfrage als JSON-String zurück
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <param name="Index"> Integer Index des Result-JSON-Objekts</param>
    Public Function GetResult(Index As Integer) As String
        If Index = data.Count - 1 Then
            Return data(Index).ToString
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' WinBack-Cloud-Connector::http-Kommando zusammensetzen
    ''' 
    ''' Setzt den HTTP-String entsprechend dem angeforderten Kommando zusammen
    ''' und setzt die Anfrage ab
    ''' </summary>
    ''' <param name="cmd"> String Kommando</param>
    ''' <param name="param"> String Kommando-Parameter</param>
    Private Function httpString(cmd As String, param As String) As Integer
        ' Internet-Connector
        Dim request As WebRequest
        Try
            request = WebRequest.Create(_url & "/" & cmd & param)
            request.Method = "GET"
            request.Timeout = 10000
            ' Authorisierung mit Passwort
            request.Headers.Clear()
            request.Headers.Add("Authorization: Basic " & _pass)
            request.ContentType = "application/x-www-form-urlencoded"

            ' Antwort (OK)
            Dim response As WebResponse = request.GetResponse()
            _errorCode = CType(response, HttpWebResponse).StatusCode

            ' Ergebnis-String
            If _errorCode = HttpStatusCode.OK Then
                Dim dataStream As Stream = response.GetResponseStream()
                Dim reader As New StreamReader(dataStream)
                Dim responseFromServer As String = reader.ReadToEnd()
                'Debug.Print("Response from Server :" & responseFromServer)

                'wenn das erste Zeichen ein "[" ist handelt es sich um eine JSON-Array
                If Left(responseFromServer, 1) = "[" Then
                    'Get JSON-Data
                    Dim ser As JArray = JArray.Parse(responseFromServer)
                    data = ser.Children().ToList
                    Return data.Count
                Else
                    'wenn nicht, wird ein Array daraus gemacht...
                    Dim ser As JArray = JArray.Parse("[" & responseFromServer & "]")
                    data = ser.Children().ToList
                    Return 1
                End If
            Else
                Return -1
            End If
        Catch e As Exception
            _errorCode = HttpStatusCode.Unused
            Return -9
        End Try
    End Function

    ''' <summary>
    ''' WinBack-Cloud-Connector::lookupProduct by article_description
    ''' 
    ''' Gibt die Anzahl der Datensätze zurück, deren Name dem Suchtext entspricht
    ''' Die Datensätze können über GetResult abgefragt werden. 
    ''' </summary>
    ''' <param name="ds"> String Suchbegriff Rohstoff-Bezeichnung</param>
    Public Function lookupProductName(ds As String) As Integer
        ' Kommando lookupProduct
        Dim nCmd = "rohstoffe"
        Dim nPrm = "?name=" & ds
        Return httpString(nCmd, nPrm)
    End Function

    ''' <summary>
    ''' WinBack-Cloud-Connector::lookupProduct by by article And supplier
    ''' 
    ''' Gibt die Anzahl der Datensätze zurück, deren Name und Lieferant dem Suchtext entspricht
    ''' Die Datensätze können über GetResult abgefragt werden. 
    ''' </summary>
    ''' <param name="ds"> String Suchbegriff Rohstoff-Bezeichnung</param>
    ''' <param name="lf"> String Suchbegriff Lieferant</param>
    Public Function lookupProduct(ds As String, lf As String) As Integer
        ' Kommando lookupProduct + supplier
        Dim nCmd = "rohstoffe"
        Dim nPrm = "?name=" + ds + "&lieferant=" + lf
        Return httpString(nCmd, nPrm)
    End Function

    ''' <summary>
    ''' WinBack-Cloud-Connector::getProductData
    ''' 
    ''' Liest das Produktdatenblatt des Rohstoffen mit der angegebenen ID aus den Cloud.
    ''' Die Datensätze können über GetResult abgefragt werden. 
    ''' </summary>
    ''' <param name="id"> String Rohstoff-ID</param>
    Public Function GetProductData(id As String) As Integer
        ' Kommando getProductData
        Dim nCmd = "rohstoffe"
        Dim nPrm = "/" & id
        Return httpString(nCmd, nPrm)
    End Function

    ''' <summary>
    ''' Abfrage der Daten aus der WinBack Cloud (Hetzner-Server)
    ''' Das Ergebnis ist ein verschachteltes JSON-Objekt
    '''     {
    '''       "rid": 2502,
    '''       "name":             {"0": "Gouda 48% Reibkäse"},
    '''       "lieferant":        {"0": "Poppinga Käseservice"},
    '''       "deklarationsname": {"0": "{Kuhmilch, Lab}, Salz, Beta Carotin (E 160a), Calciumchlorid (E 509), Kartoffelstärke"},
    '''       "inhalt":           {"1": "358",
    '''                            "2": "1491",
    '''                            "3": "24",
    '''                            "4": "1",
    '''                            "5": "29",
    '''                            "6": "0",
    '''                            "201": "0",
    '''                            "202": "1960",
    '''                            "11": "1",
    '''                            "12": "20",
    '''                            "13": "1",
    '''                            "141": "k",
    '''                            "14": "760",
    '''                            "142": "k",
    '''                            "15": "0",
    '''                            "143": "k",
    '''                            "144": "k",
    '''                            "145": "k",
    '''                            "146": "k",
    '''                            "147": "c",
    '''                            "148": "k",
    '''                            "149": "k",
    '''                            "150": "k",
    '''                            "151": "k",
    '''                            "152": "k",
    '''                            "153": "k",
    '''                            "154": "k"},
    '''       "stufe": 0,
    '''       "aenderungsindex": "20170331094858"
    '''     }
    ''' 
    ''' </summary>
    ''' <param name="iD"></param>
    ''' <returns>TimeStamp (DateTime) - Änderungsdatum aus der Cloud</returns>
    Public Function GetProductData(id As String, ByRef nwtDaten As wb_ktTypX) As Integer
        'Produktdaten aus WinBack-Cloud lesen
        If Me.GetProductData(id) > 0 Then
            'Ergebnis ist ein verschachteltes JSON-Objekt
            Dim jsonData As JObject = JObject.Parse(Me.GetResult(0))
            'Rohstoff-Bezeichnung
            nwtDaten.Bezeichung = GetJData(jsonData, "name")
            'Rohstoff-Lieferant(Text)
            nwtDaten.Lieferant = GetJData(jsonData, "lieferant")
            'Rohstoff-Deklaration
            nwtDaten.Deklaration = GetJData(jsonData, "deklarationsname")

            'Array Nährwerte/Allergene
            Dim nwtInhalt As JToken = jsonData.GetValue("inhalt")
            For Each element As JProperty In nwtInhalt
                Try
                    nwtDaten.ktTyp301.Wert(CInt(element.Name)) = element.Value.ToString
                Catch
                End Try
            Next

            'Datum/Uhrzeit der letzten Änderung
            Try
                nwtDaten.ktTyp301.TimeStamp = wb_Functions.ConvertJSONTimeStringToDateTime(jsonData.GetValue("aenderungsindex").ToString)
            Catch
                nwtDaten.ktTyp301.TimeStamp = #11/22/1964 00:00:00#
            End Try
            Return 1
        Else
            Return 0
        End If
    End Function

    Private Function GetJData(JData As JObject, JFieldName As String) As String
        Try
            Dim JName As JToken = JData.GetValue(JFieldName)
            Return JName("0").ToString
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' WinBack-Cloud-Connector::DebugResultSet
    ''' 
    ''' Gibt das JSON-Object mit dem angegebene Index als String aus
    ''' </summary>
    ''' <param name="Index"> Integer Anzahl der Datensätze</param>
    Public Sub DebugResultSet(Index As Integer)
        If Index < data.Count And Index >= 0 Then
            Console.WriteLine(data(Index).ToString)
        Else
            Console.WriteLine("Fehler " & Me.ErrorCode)
        End If
    End Sub

End Class
