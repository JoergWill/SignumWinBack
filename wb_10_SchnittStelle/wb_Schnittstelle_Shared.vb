Imports System.Windows.Forms
Imports System.Windows.Forms.Control

Public Class wb_Schnittstelle_Shared

    'Liste aller Tabellen-Definitionen
    Public Shared Txxxx As New List(Of wb_SchnittstelleTabelle)
    Public Shared Fxxxx As New List(Of wb_SchnittstelleFeld)

    'Tabellen-Definitionen (abgeleitet von wb_SchnittstellenTabelle)
    Public Shared T1001 As New wb_SchnittstelleT1001    'Artikel/Rohstoffe
    Public Shared T1002 As New wb_SchnittstelleT1002    'Nährwerte
    Public Shared T1006 As New wb_SchnittstelleT1006    'Rezeptkopf
    Public Shared T1007 As New wb_SchnittstelleT1007    'Rezeptur
    Public Shared T1101 As New wb_SchnittstelleT1101    'Produktionsdaten
    Public Shared T4105 As New wb_SchnittstelleT4105    'Chargenprotkoll Kopfdaten (nur Export)
    Public Shared T4106 As New wb_SchnittstelleT4106    'Chargenprotokoll Details (nur Export)
    Public Shared T4107 As New wb_SchnittstelleT4107    'Lieferungen/Bestandskorrektur

    Public Shared Event eExportProgressChange(Sender As Object)
    Public Shared Event eImportProgressChange(Sender As Object)
    Public Shared Event eFormatChanged(Sender As Object)
    Public Shared Event eVorschauAktualisieren(Sender As Object)

    Private Shared _ExpertMode As Boolean = False
    Private Shared _DebugMode As Boolean = False
    Private Shared _Simulation As Boolean = False
    Private Shared _DebugLevel As Integer = 0
    Private Shared _Bezeichnung As String
    Private Shared _Version As String
    Private Shared _Datum As String
    Private Shared _User As String
    Private Shared _Format As String

    Private Shared _ImportVerzeichnis As String
    Private Shared _ExportVerzeichnis As String
    Private Shared _Encoding As String
    Private Shared _ExportProgress As Integer
    Private Shared _ImportProgress As Integer

    Private Shared _SetupTabelleIndex As Integer
    Private Shared _SetupTabelleName As String

    Public Const Db_Info = 1

    Private Const cSektionHeader = 0
    Private Const cSektionVerzeichnisse = 1
    Private Const cSektionFelderImport = 2
    Private Const cSektionFelderExport = 3

    Private Const cSchnittstelle = 0
    Private Const cVersion = 1
    Private Const cDatum = 2
    Private Const cAutor = 3
    Private Const CFormat = 4
    Private Const cEncoding = 5

    Public Shared Property ExpertMode As Boolean
        Get
            Return _ExpertMode
        End Get
        Set(value As Boolean)
            _ExpertMode = value
        End Set
    End Property

    Public Shared Property Simulation As Boolean
        Get
            Return _Simulation
        End Get
        Set(value As Boolean)
            _Simulation = value
        End Set
    End Property

    Public Shared WriteOnly Property DebugLevel As Integer
        Set(value As Integer)
            _DebugLevel = value
        End Set
    End Property
    Public Shared WriteOnly Property DebugMode As Boolean
        Set(value As Boolean)
            _DebugMode = value
        End Set
    End Property
    Public Shared ReadOnly Property DebugMode_Info As Boolean
        Get
            Return _DebugMode AndAlso _DebugLevel >= 0
        End Get
    End Property
    Public Shared ReadOnly Property DebugMode_Detail As Boolean
        Get
            Return _DebugMode AndAlso _DebugLevel >= 1
        End Get
    End Property
    Public Shared ReadOnly Property DebugMode_Max As Boolean
        Get
            Return _DebugMode AndAlso _DebugLevel >= 2
        End Get
    End Property

    Public Shared Property Bezeichnung As String
        Get
            Return _Bezeichnung
        End Get
        Set(value As String)
            _Bezeichnung = value
        End Set
    End Property

    Public Shared Property ImportVerzeichnis As String
        Get
            Return _ImportVerzeichnis
        End Get
        Set(value As String)
            _ImportVerzeichnis = value
        End Set
    End Property

    Public Shared Property ExportVerzeichnis As String
        Get
            Return _ExportVerzeichnis
        End Get
        Set(value As String)
            _ExportVerzeichnis = value
        End Set
    End Property

    Public Shared Property sEncoding As String
        Get
            Return _Encoding
        End Get
        Set(value As String)
            _Encoding = value
        End Set
    End Property
    Public Shared ReadOnly Property Encoding As Text.Encoding
        Get
            Select Case _Encoding.ToUpper
                Case "UTF8"
                    Return System.Text.Encoding.UTF8
                Case "ASCII"
                    Return System.Text.Encoding.ASCII
                Case "ISO-8859-1"
                    Return System.Text.Encoding.GetEncoding("iso-8859-1")
                Case Else
                    Return System.Text.Encoding.ASCII
            End Select
        End Get
    End Property

    Public Shared Property ExportProgress As Integer
        Get
            Return _ExportProgress
        End Get
        Set(value As Integer)
            _ExportProgress = value
            RaiseEvent eExportProgressChange(Nothing)
        End Set
    End Property

    Public Shared Property ImportProgress As Integer
        Get
            Return _ImportProgress
        End Get
        Set(value As Integer)
            _ImportProgress = value
            RaiseEvent eImportProgressChange(Nothing)
        End Set
    End Property

    Public Shared Function CountExportChecked(PanelCollection As ControlCollection) As Integer
        Dim Result As Integer = 0
        For Each p In PanelCollection
            If TypeOf p Is System.Windows.Forms.Panel Then
                For Each c In CType(p, System.Windows.Forms.Panel).Controls
                    If TypeOf c Is System.Windows.Forms.CheckBox AndAlso CType(c, CheckBox).Name.Contains("ExportT") AndAlso CType(c, CheckBox).Checked Then
                        Result += 100
                    End If
                Next
            End If
        Next
        Return Result
    End Function

    Public Shared Property Version As String
        Get
            Return _Version
        End Get
        Set(value As String)
            _Version = value
        End Set
    End Property

    Public Shared Property Datum As String
        Get
            Return _Datum
        End Get
        Set(value As String)
            _Datum = value
        End Set
    End Property

    Public Shared Property User As String
        Get
            Return _User
        End Get
        Set(value As String)
            _User = value
        End Set
    End Property

    Public Shared Property Format As String
        Get
            Return _Format
        End Get
        Set(value As String)
            _Format = value
        End Set
    End Property

    Public Shared Property SetupTabelleName As String
        Get
            Return _SetupTabelleName
        End Get
        Set(value As String)
            If value.Substring(0, 1) = "T" Then
                _SetupTabelleName = value.Substring(0, 5)
            Else
                _SetupTabelleName = value
            End If
        End Set
    End Property

    Public Shared Property SetupTabelleIndex As Integer
        Get
            Return _SetupTabelleIndex
        End Get
        Set(value As Integer)
            _SetupTabelleIndex = value
            If value >= 0 Then
                SetupTabelleName = Txxxx(value).TabName
            End If
        End Set
    End Property

    Public Shared Function getFxxx() As List(Of wb_SchnittstelleFeld)
        Dim Result As New List(Of wb_SchnittstelleFeld)
        For Each f In Fxxxx
            If f.Name.Substring(0, 5) = SetupTabelleName Then
                Result.Add(f)
            End If
        Next
        Return Result
    End Function

    ''' <summary>
    ''' Das Schnittstellen-Format hat sich geändert.
    ''' Nach dem Einlesen der Konfigurations-Daten aus der Text-Datei wird der Event eFormatChanged ausgelöst,
    ''' damit werden ale Fenster aktualisiert.
    ''' </summary>
    ''' <param name="FName"></param>
    <CodeAnalysis.SuppressMessage("Major Bug", "S2583:Conditionally executed code should be reachable", Justification:="<Ausstehend>")>
    Public Shared Sub FormatChanged(FName As String)
        Dim Sektion As Integer = 0
        Dim LineItems As String()

        'alle Tabellen-Definitionen löschen
        InitTabellen()

        'Konfig-Datei
        Dim Reader As New System.IO.StreamReader(FName)
        'bis zum Ende der Datei
        Do Until Reader.EndOfStream
            'csv-Datei zeilenweise auslesen
            LineItems = Split(Reader.ReadLine.ToString, ";")

            'Kommentarzeilen unterteilen die einzelnen Sektionen
            If LineItems(0) = "#" Then
                Sektion += 1
                'nur gültige Zeilen einlesen
            ElseIf LineItems.Length > 1 Then
                Select Case Sektion
                    Case cSektionHeader
                        'Titel-Version-Datum-Autor
                        Bezeichnung = wb_Functions.GetStringFromArray(LineItems, cSchnittstelle)
                        Version = wb_Functions.GetStringFromArray(LineItems, cVersion)
                        Datum = wb_Functions.GetStringFromArray(LineItems, cDatum)
                        User = wb_Functions.GetStringFromArray(LineItems, cAutor)
                        Format = wb_Functions.GetStringFromArray(LineItems, CFormat)
                        sEncoding = wb_Functions.GetStringFromArray(LineItems, cEncoding)

                        If DebugMode_Detail Then
                            Trace.WriteLine("Schnittstelle " & Bezeichnung)
                            Trace.WriteLine("Version       " & Version)
                            Trace.WriteLine("Datum/Autor   " & Datum & "/" & User)
                            Trace.WriteLine("Type/Format   " & Format)
                            Trace.WriteLine("Encoding      " & sEncoding)
                        End If

                    Case cSektionVerzeichnisse
                        'Tabellen-Export-Import-Verzeichnisse und Masken
                        InitDirectory(LineItems)

                    Case cSektionFelderImport
                        'Tabelle-Feld-Nummer-Kommentar
                        InitFelder(LineItems, cSektionFelderImport)

                    Case cSektionFelderExport
                        'Tabelle-Feld-Nummer-Kommentar
                        InitFelder(LineItems, cSektionFelderExport)

                    Case Else


                End Select
            End If
        Loop

        RaiseEvent eFormatChanged(Nothing)
    End Sub

    Public Shared Sub FormatChanged()
        RaiseEvent eFormatChanged(Nothing)
    End Sub

    Public Shared Sub ProgressChange()
        RaiseEvent eExportProgressChange(Nothing)
    End Sub

    Public Shared Sub VorschauAktualisieren()
        RaiseEvent eVorschauAktualisieren(Nothing)
    End Sub

    Private Shared Sub InitTabellen()
        'alle Tabellen-Definitionen löschen
        For Each Tabelle In Txxxx
            Tabelle.Init()
        Next
    End Sub

    Private Shared Sub InitDirectory(Items As String())
        'alle Tabellen-Definitionen einlesen
        For Each Tabelle In Txxxx
            If Tabelle.TabName = Items(0) Then
                Tabelle.InitDirectory(Items)
            End If
        Next
    End Sub

    Private Shared Sub InitFelder(Items As String(), ImportExport As Integer)
        'alle Feld-Definitionen einlesen
        For Each Feld In Fxxxx
            If Feld.Name = Items(0) Then
                Select Case ImportExport
                    Case cSektionFelderImport
                        Feld.InitImport(Items)
                    Case cSektionFelderExport
                        Feld.InitExport(Items)
                End Select
            End If
        Next
    End Sub


End Class
