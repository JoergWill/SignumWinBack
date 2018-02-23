Imports System.Collections
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml

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
                                XMLdata.Add(System.Text.Encoding.UTF8.GetString(b64byte))
                                Return 1
                            Else
                                Return -1
                            End If
                        'Daten als JSON
                        Case "getDistributorData"
                            If (ser.SelectToken("status") = "SUCCESS") Then
                                JSONdata.Clear()
                                JSONdata.Add(responseFromServer)
                                Return 1
                            Else
                                Return -1
                            End If
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
    ''' Abfrage der Daten aus DatenLink
    ''' Das Ergebnis ist ein verschachteltes XML-Objekt
    ''' <!-- 
    ''' <?xml version="1.0" encoding="UTF-8">
    ''' <Datenlink defaultTextLang="DEU" version="1.00" xsischemaLocation="urn:datenlink:specification:1.0 http://entwickler.datenlink.info/schema/specification/1.0/datenlinkXML.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="urn:datenlink:specification:1.0">
    '''
    '''     <Header>
    '''         <Created>2013-09-03T08:14:03</Created>
    '''         <Generator>
    '''             <Software> datenlink</Software>
    '''             <Url> http : //www.datenlink.info</Url>
    '''             <Version>1.1</Version>
    '''         </Generator>
    '''         <DataSource>
    '''             <Ressource> datenlink</Ressource>
    '''             <Version>1</Version>
    '''         </DataSource>
    '''     </Header>
    '''     <Manufacturer>
    '''         <CompanyID type = "DATENLINK" > 613cb7a0-e47f-11E3-80E1-d43d7ed6cafe</CompanyID>
    '''     </Manufacturer>
    '''
    '''     <Product>
    '''         <ProductName>
    '''             <TextValue> B??KO BiO Weizenschrot mittel</TextValue>
    '''         </ProductName>
    '''         <ProductNumbers>
    '''             <ProductNumber type = "MANUFACTURER" > 102055</ProductNumber>
    '''         </ProductNumbers>
    '''         <ProductVersionIdentifier>
    '''             <ValidFrom>2013-09-03</ValidFrom>
    '''             <LabelingMethod>
    '''                 <TextValue> Zur Zeit keine Differenzierung notwendig</TextValue>
    '''             </LabelingMethod>
    '''             <LabelingValue>
    '''                 <TextValue>...</TextValue>
    '''             </LabelingValue>
    '''         </ProductVersionIdentifier>
    '''     </Product>
    '''
    '''     <FoodFacts>
    '''         <DeclarationName key = "0E66905" />
    '''         <NutritionFacts>
    '''             <NutritionFactsBasedOn>
    '''                 <Value unit = "g" > 100</Value>
    '''             </NutritionFactsBasedOn>
    '''             <NutritionFactsItem key="GJ">
    '''                 <Value unit = "kJ" > 1293</Value>
    '''             </NutritionFactsItem>
    '''             <NutritionFactsItem key="ZF">
    '''                 <Value unit = "g" > 2.4</Value>
    '''             </NutritionFactsItem>
    '''             <NutritionFactsItem key="FS">
    '''                 <Value unit = "g" > 0.34</Value>
    '''             </NutritionFactsItem>
    '''             <NutritionFactsItem key="ZK">
    '''                 <Value unit = "g" > 59.5</Value>
    '''             </NutritionFactsItem>
    '''             <NutritionFactsItem key="KD">
    '''                 <Value unit = "g" > 0.72</Value>
    '''             </NutritionFactsItem>
    '''             <NutritionFactsItem key="ZB">
    '''                 <Value unit = "g" > 10</Value>
    '''             </NutritionFactsItem>
    '''             <NutritionFactsItem key="ZE">
    '''                 <Value unit = "g" > 11.4</Value>
    '''             </NutritionFactsItem>
    '''             <NutritionFactsItem key="GMKO">
    '''                 <Value unit = "g" smaller="true">0.01</Value>
    '''             </NutritionFactsItem>
    '''         </NutritionFacts>
    '''
    '''         <AllergenLabeling>
    '''             <AllergenList type="EU" containmentType="CONTAINED">
    '''                 <AllergenListItem key="GLUTEN">
    '''                     <AllergenListItem key = "GLUTEN_WHEAT" />
    '''                 </AllergenListItem>
    '''             </AllergenList>
    '''         </AllergenLabeling>
    '''         <IngredientLists>
    '''             <IngredientList>
    '''                 <Ingredient key="1695B8D">
    '''                     <SpecificationList type="FIX">
    '''                         <Specification key = "BIO_PRODUCT" />
    '''                     </SpecificationList>
    '''                 </Ingredient>
    '''             </IngredientList>
    '''         </IngredientLists>
    '''     </FoodFacts>
    ''' </Datenlink> 
    ''' --></summary>
    ''' <param name="iD"></param>
    ''' <returns>TimeStamp (DateTime) - Änderungsdatum aus der Cloud</returns>

    Public Function GetProductData(id As String, ByRef nwtDaten As wb_Komponente) As Integer
        'Produktdaten von Datenlink lesen
        If Me.GetProductData(id) > 0 Then
            'Auswertung XML-Info
            Dim objXML As New Xml.XmlDocument
            objXML.LoadXml(Me.GetXMLResult)

            For Each oNode As XmlNode In objXML.ChildNodes(1).ChildNodes
                Select Case oNode.Name
                    Case "Header"
                        For Each oItem As XmlNode In oNode.ChildNodes
                            Select Case oItem.Name
                                Case "Created"
                                    nwtDaten.ktTyp301.TimeStamp = wb_Functions.ConvertDataLinkTimeStringToDateTime(oItem.InnerText)
                            End Select
                        Next
                    Case "Manufacturer"
                        For Each oItem As XmlNode In oNode.ChildNodes
                            Select Case oItem.Name
                                Case "CompanyID"
                                    Dim CompanyID As String = oItem.InnerText
                                    'wenn eine Hersteller-Kennung angegeben ist
                                    If (Me.getDistributorData(CompanyID) > 0) Then
                                        'Hersteller/Lieferant im Klartext ermitteln
                                        nwtDaten.Lieferant = GetDatenLinkLieferant(Me.GetResult(0))
                                    End If
                            End Select
                        Next
                    Case "Product"
                        For Each oItem As XmlNode In oNode.ChildNodes
                            For Each oTag As XmlNode In oItem.ChildNodes
                                Select Case oTag.Name
                                    Case "TextValue"
                                        'Komponenten-Bezeichnung
                                        nwtDaten.Bezeichnung = oTag.InnerText
                                    Case "ProductNumber"
                                        'Bestellnummer beim Hersteller/Lieferant
                                        nwtDaten.BestellNummer = oTag.InnerText
                                End Select
                            Next
                        Next
                    Case "FoodFacts"
                        For Each oItem As XmlNode In oNode.ChildNodes
                            Select Case oItem.Name
                                Case "NutritionFacts"
                                    For Each oTag As XmlNode In oItem.ChildNodes
                                        If oTag.Attributes.Count > 0 Then
                                            'Nährwerte - GCAL/GJ/ZE/ZK/ZF/KD/FS/ZB/GMKO
                                            nwtDaten.ktTyp301.dlNaehrWert(oTag.Attributes(0).InnerText) = oTag.InnerText
                                        Else
                                            'Nährwerte
                                            nwtDaten.ktTyp301.dlNaehrWert(oTag.Name) = oTag.InnerText
                                        End If
                                    Next
                                Case "AllergenLabeling"
                                    'CONTAINED/MAY_CONTAINED                    
                                    For Each oTag As XmlNode In oItem.ChildNodes
                                        Debug.Print(oTag.Attributes(1).InnerText)

                                        For Each oDetail As XmlNode In oTag.ChildNodes
                                            'Allergene
                                            For Each oAttribute As XmlAttribute In oDetail.Attributes
                                                Debug.Print(oAttribute.InnerText)
                                                nwtDaten.ktTyp301.dlAllergen(oAttribute.InnerText) = oTag.Attributes(1).InnerText
                                            Next
                                            'Allergene Details
                                            If oDetail.HasChildNodes Then
                                                For Each oDeailChild As XmlNode In oDetail.ChildNodes
                                                    For Each oAttribute As XmlAttribute In oDeailChild.Attributes
                                                        Debug.Print(oAttribute.InnerText)
                                                        nwtDaten.ktTyp301.dlAllergen(oAttribute.InnerText) = oTag.Attributes(1).InnerText
                                                    Next
                                                Next
                                            End If

                                        Next
                                    Next
                                Case "IngredientLists"
                            End Select
                        Next
                End Select
            Next
            Return 1
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Auswerten der Lieferanten-Daten (JSON)
    ''' {
    '''     "status":"SUCCESS"
    '''     "data":
    '''         {
    '''             "name":     "BÄKO-Zentrale Süddeutschland eG"
    '''             "id":       "613cb7a0-e47f-11e3-80e1-d43d7ed6cafe"
    '''             "location":
    '''                 {
    '''                     "address":
    '''                         {
    '''                             "street":"Benzstr."
    '''                             "number":"3"
    '''                         }
    '''                     "city":
    '''                         {
    '''                             "zipcode":"68526"
    '''                             "name":"Ladenburg"
    '''                         }
    '''                     "country":
    '''                         {   
    '''                             "iso":"DE"
    '''                             "name":"Deutschland"
    '''                          }
    '''                 }
    '''         }
    ''' }
    ''' </summary>
    ''' <param name="JString"></param>
    ''' <returns></returns>
    Private Function GetDatenLinkLieferant(JString As String) As String
        Dim jsonData As JObject = JObject.Parse(JString)
        Dim JData As JToken = jsonData.GetValue("data")
        Return JData("name").ToString
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

