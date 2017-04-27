
''' <summary>
''' Lesen/Schreiben der ini-Datei. Der Datei-Name
''' wird bei der Initialisierung festgelegt.
''' 
''' Default: C:\ProgramData\OrgaSoft\WinBack.ini
''' </summary>
Public Class wb_IniFile
    ' Private Variablen
    Private Pfad As String
    Private _SilentMode As Boolean = False
    Private _ReadResult As Boolean = True
    Private _WriteResult As Boolean = True

    ' Instantiierung ohne Pfad 
    Sub New() 'kein Pfad notwendig...

        'Pfad = My.Application.Info.DirectoryPath & "\" & My.Application.Info.Title & ".ini"
        'C:\ProgramData\OrgaSoft\AddIn
        Pfad = "C:\ProgramData\OrgaSoft\WinBack.ini"
    End Sub

    ' Konstruktor für setzen des Pfades
    ' Instanziieren mit z.B.: 
    Sub New(ByVal Pfad_der_ini As String)
        Pfad = Pfad_der_ini
    End Sub

    ' wird von Unit-Test auf True gesetzt - verhindert die Ausgabe per MsgBox
    WriteOnly Property SilentMode As Boolean
        Set(value As Boolean)
            _SilentMode = value
        End Set
    End Property

    ReadOnly Property ReadResult() As Boolean
        Get
            Return _ReadResult
        End Get
    End Property
    ReadOnly Property WriteResult() As Boolean
        Get
            Return _WriteResult
        End Get
    End Property

    ' DLL-Funktionen zum LESEN der INI deklarieren
    Private Declare Ansi Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (
            ByVal lpApplicationName As String, ByVal lpSchlüsselName As String, ByVal lpDefault As String,
            ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    'DLL-Funktion zum SCHREIBEN in die INI deklarieren - Return-value ist 0 bei Fehler
    Private Declare Ansi Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (
            ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String,
            ByVal lpFileName As String) As Integer

    Private Function TestIniPfadEmpty(Sektion As String, Schlüssel As String) As Boolean
        If Pfad = "" Then
            If Not _SilentMode Then MsgBox("Es ist kein Pfad zur INI angegeben. Deshalb ist das Auslesen des Wertes nicht möglich." _
                        & vbCrLf & vbCrLf & "Angeforderte Sektion: " & Sektion & vbCrLf & "Angeforderter Schlüssel: " _
                        & Schlüssel, MsgBoxStyle.Exclamation, "Pfad zur INI-Datei fehlt")
            Return False
        Else
            Return True
        End If
    End Function

    Private Function TestIniPfadExists(Sektion As String, Schlüssel As String) As Boolean
        If IO.File.Exists(Pfad) = False Then
            If Not _SilentMode Then MsgBox("Die angegebene INI-Datei exstiert auf diesem Rechner nicht. Deshalb ist das " _
                        & "Auslesen des Wertes nicht möglich." & vbCrLf & vbCrLf & "INI-Datei: " & Pfad _
                        & vbCrLf & "Angeforderte Sektion: " & Sektion & vbCrLf & "Angeforderter Schlüssel: " _
                        & Schlüssel, MsgBoxStyle.Exclamation, "Pfad zur INI-Datei fehlt")
            Return False
        Else
            Return True
        End If
    End Function

    Private Function TestIniOrdnerExists(Sektion As String, Schlüssel As String) As Boolean
        Dim Ordner As String
        Ordner = IO.Path.GetDirectoryName(Pfad)
        If IO.Directory.Exists(Ordner) = False Then
            If Not _SilentMode Then MsgBox("Die angegebene Ordner für die INI-Datei exstiert auf diesem Rechner nicht. Deshalb ist das " _
                        & "Schreiben des Wertes nicht möglich." & vbCrLf & vbCrLf & "Fehlender Ordner: " & Ordner _
                        & vbCrLf & "Angeforderte Sektion: " & Sektion & vbCrLf & "Zu schreibender Schlüssel: " _
                        & Schlüssel, MsgBoxStyle.Exclamation, "Pfad zur INI-Datei existiet nicht")
            Return False
        Else
            Return True
        End If
    End Function

    Public Sub DeleteIniFile()
        Try
            IO.File.Delete(Pfad)
            _ReadResult = True
        Catch ex As Exception
            _ReadResult = False
        End Try
    End Sub

    Public Function ReadString(ByVal Sektion As String, ByVal Schlüssel As String, Optional ByVal Standardwert As String = "", Optional ByVal BufferSize As Integer = 1024) As String
        Try
            ' Testen, ob ein Pfad zur INI vorhanden ist und ob die Datei existiert
            If Not TestIniPfadEmpty(Sektion, Schlüssel) Or Not TestIniPfadExists(Sektion, Schlüssel) Then
                ReadString = Standardwert
                _ReadResult = False
                Exit Function
            End If

            ' Auslesen des Wertes
            Dim sTemp As String = Space(BufferSize)
            Dim Length As Integer = GetPrivateProfileString(Sektion, Schlüssel, Standardwert, sTemp, BufferSize, Pfad)
            _ReadResult = True
            Return Left(sTemp, Length)

        Catch ex As Exception
            ReadString = Standardwert
            _ReadResult = False
        End Try

    End Function

    Public Function ReadInt(ByVal Sektion As String, ByVal Schlüssel As String, Optional ByVal Standardwert As Integer = 0) As Integer
        Return CInt(Val(ReadString(Sektion, Schlüssel, Standardwert.ToString)))
    End Function

    Public Function ReadBool(ByVal Sektion As String, ByVal Schlüssel As String, Optional ByVal Standardwert As Boolean = 0) As Boolean
        Return If(ReadString(Sektion, Schlüssel, Standardwert.ToString), "1", "0")
    End Function

    Public Sub WriteString(ByVal Sektion As String, ByVal Schlüssel As String, ByVal Wert As String)
        Try
            ' Testen, ob ein Pfad zur INI vorhanden ist und ob das Verzeichnis existiert
            If Not TestIniPfadEmpty(Sektion, Schlüssel) Or Not TestIniOrdnerExists(Sektion, Schlüssel) Then
                _WriteResult = False
                Exit Sub
            End If

            ' Wenn die ini-Datei nicht existiert wird sie neu angelegt
            If Not (System.IO.File.Exists(Pfad)) Then
                Dim NewIniFile As Short = FreeFile()
                FileOpen(NewIniFile, Pfad, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FileClose(NewIniFile)
            End If

            ' Schreiben in die INI durchführen
            If WritePrivateProfileString(Sektion, Schlüssel, Wert, Pfad) > 0 Then
                _WriteResult = True
            Else
                If Not _SilentMode Then MsgBox("Fehler beim Speichern der Daten in der Konfiguration. Deshalb ist das " _
                        & "Schreiben des Wertes nicht möglich." & vbCrLf & vbCrLf & "Verzeichnis: " & Pfad _
                        & vbCrLf & "Angeforderte Sektion: " & Sektion & vbCrLf & "Zu schreibender Schlüssel: " _
                        & Schlüssel, MsgBoxStyle.Exclamation, "Fehler beim Speichern der Daten in der Konfiguration")
                _WriteResult = False
            End If

        Catch ex As Exception
            _WriteResult = False
        End Try
    End Sub

    Public Sub WriteInt(ByVal Sektion As String, ByVal Schlüssel As String, ByVal Wert As Integer)
        WriteString(Sektion, Schlüssel, Wert.ToString)
    End Sub

    Public Sub WriteBool(ByVal Sektion As String, ByVal Schlüssel As String, ByVal Wert As Boolean)
        WriteString(Sektion, Schlüssel, If(Wert, "1", "0"))
    End Sub
End Class
'End Namespace
