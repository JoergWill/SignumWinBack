Imports WinBack.wb_Sql_Selects

'Paket muss installiert werden
' can be installed from package manager console like this:
' install-package Newtonsoft.json
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Xml
Imports System.Globalization

Public Class wb_nwtUpdate
    Implements IDisposable
    Protected disposed As Boolean = False

    Private KO_Nr As Integer = 0
    Private _InfoText As String = ""
    Public nwtDaten As New wb_ktTypX

    Public ReadOnly Property InfoText As String
        Get
            Return DateTime.Now.ToLongTimeString & " " & _InfoText
        End Get
    End Property

    Public Function UpdateNext() As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)

        'nächsten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(setParams(sqlUpdateNWT, KO_Nr.ToString)) Then
            If winback.Read Then
                'Index - KO_Nr
                KO_Nr = winback.iField("KO_Nr")
                'alphanumerische Komponenten-Nummer
                Dim KO_Nr_AlNum As String = winback.sField("KO_Nr_AlNum")
                'Komponenten-Bezeichnung
                Dim KO_Bezeichnung As String = winback.sField("KO_Bezeichnung")
                'Match-Code (DL-Datenlink oder ID-WinBack-Cloud)
                Dim KA_Matchcode As String = winback.sField("KA_Matchcode")

                'Nährwert-Info aus der Cloud lesen (Datum der letzten Änderung)
                Dim LastChange As Date = GetNaehrwerte(KA_Matchcode)



                'Ausgabe-Text
                _InfoText = "KO-Nr " & KO_Nr.ToString("00000 ") & KO_Bezeichnung & " " & LastChange.ToString

                winback.Close()
                Return True
            Else
                'EOF() - ReStart bei KO_Nr = 0
                KO_Nr = 0
                _InfoText = ""
                winback.Close()
                Return False
            End If
            winback.Close()
            Return False
        End If
        winback.Close()
        Return False
    End Function

    Public Function GetNaehrwerte(ID As String) As Date
        If Left(ID, 3) = "DL-" Then
            Return GetNaehrwerteDatenlink(ID)
        Else
            Return GetNaehrwerteHetzner(ID)
        End If
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
    Private Function GetNaehrwerteHetzner(iD As String) As Date
        Dim nwt As New wb_nwtCloud(wb_Credentials.WinBackCloud_Pass, wb_Credentials.WinBackCloud_Url)
        If nwt.GetProductData(iD) > 0 Then

            'Ergebnis ist ein verschachteltes JSON-Objekt
            Dim jsonData As JObject = JObject.Parse(nwt.GetResult(0))
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
                Return nwtDaten.ktTyp301.TimeStamp
            Catch
                Return #11/22/1964 00:00:00#
            End Try
        Else
            Return #11/22/1964 00:00:00#
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
    Private Function GetNaehrwerteDatenlink(iD As String) As Date
        'Create new instance of nwtCloud
        Dim dl As New wb_nwtDatenLink(wb_Credentials.Datenlink_PAT, wb_Credentials.Datenlink_CAT, wb_Credentials.Datenlink_Url)
        If dl.GetProductData(iD) > 0 Then

            'Auswertung XML-Info
            Dim objXML As New Xml.XmlDocument
            objXML.LoadXml(dl.GetXMLResult)
            Debug.Print(objXML.InnerXml)

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
                                    If (dl.getDistributorData(CompanyID) > 0) Then
                                        'Hersteller/Lieferant im Klartext ermitteln
                                        nwtDaten.Lieferant = GetDatenLinkLieferant(dl.GetResult(0))
                                    End If
                            End Select
                        Next
                    Case "Product"
                        For Each oItem As XmlNode In oNode.ChildNodes
                            For Each oTag As XmlNode In oItem.ChildNodes
                                Select Case oTag.Name
                                    Case "TextValue"
                                        'Komponenten-Bezeichnung
                                        nwtDaten.Bezeichung = oTag.InnerText
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
            'Datum/Uhrzeit der letzten Änderung
            Return nwtDaten.ktTyp301.TimeStamp
        Else
            Return #11/22/1964 00:00:00#
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

    Public Sub New()
        'letzte aktualisierte Komponenten-ID aus der winback.ini lesen
        Dim IniFile As New wb_IniFile
        KO_Nr = IniFile.ReadInt("Cloud", "UpdateNaehrwerteKONr")
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                ' Insert Code to free managed resource
            End If
            'Beim Programm-Ende wird die aktuelle Komponenten-Nummer gesichert
            Dim IniFile As New wb_IniFile
            IniFile.WriteInt("Cloud", "UpdateNaehrwerteKONr", KO_Nr)
        End If
        Me.disposed = True
    End Sub

#Region " IDisposable Support "
    ' Do not change or add Overridable to these methods.
    ' Put cleanup code in Dispose(ByVal disposing As Boolean).
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region
End Class
