Imports System.IO
Imports System.Net

'Paket muss installiert werden:
' can be installed from package manager console like this:
' install-package Newtonsoft.json
Imports Newtonsoft.Json.Linq

Public Class wb_nwtCl_OpenFoodFacts
    Inherits wb_nwtCL

#Disable Warning SYSLIB0014
    Private _url As String
    Private _errorCode As HttpStatusCode
    Private data As List(Of JToken)
    Dim JSONdata As New List(Of JToken)

    Public Overrides ReadOnly Property CloudType As wb_CloudType
        Get
            Return wb_nwtCL.wb_CloudType.OpenFoodFacts
        End Get
    End Property

    ''' <summary>
    ''' Erzeugt das Connection-Objekt zur Abfrage der Nährwerte aus der OpenFoodFacts-Cloud
    ''' URL muss mit übergeben werden.
    ''' </summary>
    ''' <param name="Url"></param>
    Sub New(Url As String)
        'Url
        _url = Url
    End Sub

    Public WriteOnly Property Url As String
        Set(value As String)
            _url = value
        End Set
    End Property

    ''' <summary>
    ''' OpenFoodFact-Cloud-Connector::GetErrorCode(Array) 
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
    ''' OpenFoodFacts-Cloud-Connector::http-Kommando zusammensetzen
    ''' 
    ''' Setzt den HTTP-String entsprechend dem angeforderten Kommando zusammen
    ''' und setzt die Anfrage ab
    ''' </summary>
    ''' <param name="cmd"> String Kommando</param>
    ''' <param name="param"> String Kommando-Parameter</param>
    ''' <param name="getFile"> Boolean Get-Result als File speichern</param>
    ''' <param name="getFileName"> String Dateiname wenn getResult = True</param>
    Private Function httpString(cmd As String, param As String, opt As String, Optional getFile As Boolean = False, Optional getFileName As String = "") As Integer
        ' Internet-Connector
        Dim request As WebRequest

        Try
            request = WebRequest.Create(_url & "/" & cmd & param & opt)
            request.Method = "GET"
            'request.Timeout = 10000
            ' Authorisierung ohne User/Passwort
            request.Headers.Clear()

            'Request ins Log-File schreiben
            Trace.WriteLine("@I_WebRequest " & _url & "/" & cmd & param & opt)

            ' Antwort (OK)
            Dim response As WebResponse = request.GetResponse()
            _errorCode = CType(response, HttpWebResponse).StatusCode

            ' Ergebnis-String
            If _errorCode = HttpStatusCode.OK Then
                Dim dataStream = response.GetResponseStream()
                Dim reader As New StreamReader(dataStream)
                Dim responseFromServer As String = reader.ReadToEnd()
                'Debug.Print("Response from Server :" & responseFromServer)
                Dim ser As JObject = JObject.Parse(responseFromServer)

                'Abhängig vom Kommando auswerten
                Select Case cmd
                        'Anzahl der Datensätze in JSON {count, page, page_count, page_size, products[], skip}
                    Case "cgi/search.pl?"
                        JSONdata.Clear()
                        JSONdata.AddRange(ser.SelectToken("products"))
                        Return ser.SelectToken("count")

                    Case "api/v0/product"
                        JSONdata.Clear()
                        JSONdata.AddRange(ser.SelectToken("product"))
                        Return ser.SelectToken("status")

                    Case Else
                        Return 0
                End Select
            Else
                Return -1
            End If
        Catch e As Exception
            Trace.WriteLine("@E_WebRequest " & _url & "/" & cmd & param & " Exception " & e.ToString)
            _errorCode = HttpStatusCode.Unused
            Return -9
        End Try
    End Function

    ''' <summary>
    ''' OpenFoodFacts-Cloud-Connector::lookupProduct by article_description
    ''' 
    ''' Gibt die Anzahl der Datensätze zurück, deren Name dem Suchtext entspricht
    ''' Die Datensätze können über GetResult abgefragt werden. 
    ''' </summary>
    ''' <param name="ds"> String Suchbegriff Rohstoff-Bezeichnung</param>
    Public Function lookupProductName(ds As String) As Integer
        ' Kommando lookupProduct
        Dim nCmd = "cgi/search.pl?"
        Dim nPrm = "search_terms=" & ds
        Dim nOpt = "&search_simple=1&json=1"
        _cnt = httpString(nCmd, nPrm, nOpt)
        Return _cnt
    End Function

    ''' <summary>
    ''' liefert die Liste aller gefundenen Produkte und Lieferanten nach Suchen in der Cloud.
    ''' Aus dem Json-Array werden Name und Lieferant in eine Liste geschrieben
    ''' 
    ''' {
    '''     "count"197,
    '''     "page":1,
    '''     ...
    '''     products:
    '''         [
    '''             {
    '''                 "_id"                   :   "4311501333778",
    '''                 "brands"                :   "Edeka Bio",
    '''                 "allergens_tags         :   ["en:gluten"]
    '''                 "ingredients_text_de"   :   ["_Weizenmehl_"]
    '''                 "product_name_de"       :   "Weizenmehl Type 550"
    '''                 ...
    '''            }
    '''         ]
    ''' }
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function getProductList() As ArrayList
        Dim a As New ArrayList
        Dim n As wb_Global.NwtCloud
        'Dim t As JObject

        'Schleife über alle Einträge 
        For Each jt As JToken In JSONdata

            'EAN-Code!
            n.ean = jt("_id")
            'Id
            n.id = "EAN" & n.ean

            'Rohstoff-Bezeichnung (aus openfoodfacts.org)
            n.deklarationsname = ""
            n.name = jt("product_name_de")
            If n.name = "" Then
                n.name = jt("product_name")
            End If

            'Lieferant (Hersteller)
            n.lieferant = jt("brands")
            'Datensatz zur Liste hinzufügen
            a.Add(n)
        Next
        Return a
    End Function

    ''' <summary>
    ''' OpenFoodFacts-Cloud-Connector::getProductData
    ''' 
    ''' Liest das Produktdatenblatt des Rohstoffen mit der angegebenen ID aus den Cloud.
    ''' Die Datensätze können über GetResult abgefragt werden. 
    ''' </summary>
    ''' <param name="id"> String Rohstoff-ID</param>
    Public Overrides Function GetProductData(id As String) As Integer
        ' Kommando getProductData
        Dim nCmd = "api/v0/product"
        Dim nPrm = "/" & id
        Dim nOpt = ".json"
        Return httpString(nCmd, nPrm.Replace("EAN", ""), nOpt)
    End Function

    Public Overrides Function GetProductData(id As String, ByRef nwtDaten As wb_Komponente) As Integer
        'Produktdaten zum EAN-Code aus OpenFoodFacts lesen
        If Me.GetProductData(id) > 0 Then
            'Ergebnis ist ein verschachteltes JSON-Objekt
            'Dim jt As JToken = JSONdata(0)
            For Each jt As JProperty In JSONdata
                Debug.Print(jt.Name)

                Select Case jt.Name

                    'Rohstoff-Bezeichnung NICHT ÜBERNEHMEN
                    Case "product_name"
                        'nwtDaten.Bezeichnung = jt("name")

                    'Rohstoff-Deklaration (aus der Cloud IMMER in die externe Deklaration schreiben)
                    Case "ingredients_text"
                        nwtDaten.DeklBezeichungExtern = jt.Value

                    'Rohstoff-Lieferant(Text)
                    Case "brands"
                        nwtDaten.Lieferant = jt.Value

                    Case "allergens_tags"
                        'Array Nährwerte/Allergene
                        Dim allergens As JArray = jt.Value
                        For Each element In allergens
                            Try
                                nwtDaten.ktTyp301.ffAllergen(element.ToString) = "CONTAINED"
                            Catch
                            End Try
                        Next

                    Case "nutriments"
                        'Array Nährwerte
                        Dim nwt As JObject = jt.Value
                        For Each element In nwt
                            Try
                                Debug.Print(element.ToString)
                                nwtDaten.ktTyp301.ffNaehrWert(element.Key) = element.Value
                            Catch
                            End Try
                        Next

                End Select


            Next





            ''Datum/Uhrzeit der letzten Änderung
            'Try
            '    nwtDaten.ktTyp301.TimeStamp = wb_Functions.ConvertJSONTimeStringToDateTime(JSONdata("interface_version_modified").ToString)
            'Catch
            '    nwtDaten.ktTyp301.TimeStamp = #11/22/1964 00:00:00#
            'End Try
            Return 1
        Else
            Return 0
        End If

    End Function

    Private Function GetJData(JData As JObject, JFieldName As String) As String
        Try
            Dim JName As JToken = JData.GetValue(JFieldName)
            If Not IsNothing(JName("0")) Then
                Return JName("0").ToString
            End If
        Catch
        End Try
        Return ""
    End Function

#Enable Warning SYSLIB0014
End Class
