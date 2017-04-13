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

                'Nährwert-Info aus der Cloud lesen (Datum der letzten Änderung). Die Daten werden in nwtDaten eingetragen
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

    ''' <summary>
    ''' Abfrage der Nährwerte aus der Cloud
    ''' Abhängig von der ID wird die jeweilige Routine für Abfrage und Dekodierung
    ''' der Nährwert-Daten aufgerufen
    ''' Die Daten werden in nwtDaten eingtragen
    ''' 
    '''     DL-xxxx Datenlink
    '''     xxxx    WinBack-Cloud
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <returns>Gibt das Datum der letzten Änderung in der Cloud zurück</returns>
    Public Function GetNaehrwerte(ID As String) As Date
        If Left(ID, 3) = "DL-" Then
            Return GetNaehrwerteDatenlink(ID)
        Else
            Return GetNaehrwerteHetzner(ID)
        End If
    End Function

    ''' <summary>
    ''' Abfrage der Daten aus der WinBack Cloud (Hetzner-Server)
    ''' Die Daten werden in nwtDaten eingtragen
    ''' 
    ''' </summary>
    ''' <param name="iD"></param>
    ''' <returns>TimeStamp (DateTime) - Änderungsdatum aus der Cloud</returns>
    Private Function GetNaehrwerteHetzner(iD As String) As Date
        Dim nwt As New wb_nwtCloud(wb_Credentials.WinBackCloud_Pass, wb_Credentials.WinBackCloud_Url)
        If nwt.GetProductData(iD, nwtDaten) > 0 Then
            Return nwtDaten.ktTyp301.TimeStamp
        Else
            Return #11/22/1964 00:00:00#
        End If
    End Function

    ''' <summary>
    ''' Abfrage der Daten aus DatenLink
    ''' Das Ergebnis ist ein verschachteltes XML-Objekt
    ''' Die Daten werden in nwtDaten eingtragen
    ''' </summary>
    ''' <param name="iD"></param>
    ''' <returns>TimeStamp (DateTime) - Änderungsdatum aus der Cloud</returns>
    Private Function GetNaehrwerteDatenlink(iD As String) As Date
        'Create new instance of nwtCloud
        Dim dl As New wb_nwtDatenLink(wb_Credentials.Datenlink_PAT, wb_Credentials.Datenlink_CAT, wb_Credentials.Datenlink_Url)
        If dl.GetProductData(iD, nwtDaten) > 0 Then
            'Datum/Uhrzeit der letzten Änderung
            Return nwtDaten.ktTyp301.TimeStamp
        Else
            Return #11/22/1964 00:00:00#
        End If
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
