Imports WinBack.wb_Sql_Selects

'Paket muss installiert werden
' can be installed from package manager console like this:
' install-package Newtonsoft.json
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
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
    ''' <returns></returns>
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
                nwtDaten.ktTyp301.Wert(CInt(element.Name)) = element.Value.ToString
            Next

            'Datum/Uhrzeit der letzten Änderung
            nwtDaten.TimeStamp = wb_Functions.ConvertJSONTimeStringToDateTime(jsonData.GetValue("aenderungsindex").ToString)
            Return nwtDaten.TimeStamp
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
    Private Function GetNaehrwerteDatenlink(iD As String) As Date
        Return #11/22/1964 00:00:00#
    End Function

    Public Sub New()
        'letzte aktualisierte Komponenten-ID aus der winback.ini lesen
        Dim IniFile As New wb_IniFile
        KO_Nr = IniFile.ReadInt("Cloud", "UpdateNaehrwerteKONr")
    End Sub


    '---------------------------------------------------------------------------------------------------------------------
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
