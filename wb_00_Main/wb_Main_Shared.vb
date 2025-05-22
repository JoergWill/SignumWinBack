
Imports System.Reflection
Imports combit.Reporting

Public Class wb_Main_Shared
    Private Shared _MainProgress As wb_Main_Progress = Nothing
    Private Shared _MainProgressVisible As Boolean = False
    Private Shared _AddInList As New List(Of String)

    'Eigenen Logger für List&Label definieren
    Public Shared ILL_Logger As New wb_Printer_Logger
    'List&Label Debug-Ausgaben aktivieren
#If DebugLL Then
    Public shared WithEvents LL As New ListLabel(ILL_Logger)
#Else
    Public Shared WithEvents LL As New ListLabel()
#End If

    Public Shared Event eOpenForm(sender As Object, FormName As String)
    Public Shared Event eTimer(sender As Object, e As String)
    Public Shared Event eSendMessage(sender As Object, Message As String)


    Public Shared Property MainProgressVisible
        Get
            Return _MainProgressVisible
        End Get
        Set(value)
            _MainProgressVisible = value
        End Set
    End Property

    Public Shared Sub ShowProgressBar()
        If Not _MainProgressVisible Then
            If _MainProgress Is Nothing Then
                _MainProgress = New wb_Main_Progress
            End If

            If Not MainProgressVisible Then
                _MainProgress.Show()
            Else
                _MainProgress.BringToFront()
            End If

            System.Windows.Forms.Application.DoEvents()
        End If
    End Sub

    Public Shared Sub HideProgressBar()
        If _MainProgressVisible AndAlso _MainProgress IsNot Nothing Then
            _MainProgress.Close()
            _MainProgress = Nothing
        End If
    End Sub

    ''' <summary>
    ''' MyAssemblyResolve-Event-Handler
    ''' Anmerkung: Da ResolveEvents.requestingAssembly nicht funktioniert, wird geprüft, ob die angeforderte dll im 
    '''            AddIn-dll-Verzeichnis existiert.
    '''            Ist die dll nicht vorhanden, wird der Default-Wert zurückgegeben.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <returns></returns>
    Public Shared Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs, DefaultAssembly As Assembly) As Assembly
        Console.WriteLine("Resolving...")

        Dim sApplicationDirectory As String = wb_GlobalSettings.pAddInPath
        Dim sAssemblyName As String = New AssemblyName(args.Name).Name

        Dim arrFields As String() = args.Name.Split(","c)
        Dim sAssemblyCulture As String = arrFields(2).Substring(arrFields(2).IndexOf("="c) + 1)

        Dim sAssemblyFileName As String = sAssemblyName & ".dll"
        Dim sAssemblyPath As String

        If sAssemblyName.EndsWith(".resources") Then
            'Trace.WriteLine("@I_AssemblyName resources " & sAssemblyName)
            Dim sResourceDirectory As String = IO.Path.Combine(sApplicationDirectory, sAssemblyCulture)
            sAssemblyPath = IO.Path.Combine(sResourceDirectory, sAssemblyFileName)
            'TODO Das WinBack-AddIn fällt hier bei einem nicht deutschen Windows (Belgien/Fonk) auf die Nase. Besser nichts zurückgeben !
            'Return DefaultAssembly
            'Return If(Debugger.IsAttached, Reflection.Assembly.LoadFile(sAssemblyPath), Assembly.Load(IO.File.ReadAllBytes(sAssemblyPath)))
            Return Nothing
        Else
            sAssemblyPath = IO.Path.Combine(sApplicationDirectory & wb_Global.SubDir_dll, sAssemblyFileName)
            If IO.File.Exists(sAssemblyPath) Then
                If Not sAssemblyPath.ToLower.Contains("log4net.dll") Then
                    'Trace.WriteLine("@I_AssemblyName dll " & sAssemblyPath)
                End If
                Return If(Debugger.IsAttached, Reflection.Assembly.LoadFile(sAssemblyPath), Assembly.Load(IO.File.ReadAllBytes(sAssemblyPath)))
            Else
                'Trace.WriteLine("@I_AssemblyName not exists " & sAssemblyName)
                Return DefaultAssembly
            End If
        End If
    End Function

    ''' <summary>
    ''' Hier registrieren sich alle WinBack-AddIns.
    ''' 
    ''' Anhand der Liste kann dann in wb_Main geprüft werden, ob alle AddIn-Komponenten in OrgaBack registiert sind.
    ''' Wenn die User-Berechtigungen in OrgaBack nicht alle WinBack-AddIns zulassen, sind manche Funktionen gesperrt!
    ''' </summary>
    ''' <param name="Name"></param>
    Public Shared Sub RegisterAddIn(Name As String)
        _AddInList.Add(Name)
    End Sub

    ''' <summary>
    ''' Prüft ob ein WinBack.AddIn mit diesem Namen registriert ist
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    Public Shared Function IsRegistered(Name As String) As Boolean
        For Each s In _AddInList
            If s = Name Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Öffnet die als Name übergebene Form innerhalb des (OrgaBack)WinBack-Main-Formulars. 
    ''' Dies ist notwendig, wenn von einer anderen Main-Form aus umgeschaltet werden soll. Damit kann von z.B. der Rohstoff-Verwaltung
    ''' direkt in die Artikel- oder Rezeptverwaltung umgeschaltet werden.
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="FormName"></param>
    Public Shared Sub OpenForm(Sender As Object, FormName As String)
        RaiseEvent eOpenForm(Sender, FormName)
    End Sub

    ''' <summary>
    ''' Event Timer-Einstellungen/Parameter/Status hat sich geändert.
    ''' Anzeige-Grid neu aufbauen
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    Public Shared Sub TimerMsg(Sender As Object, e As String)
        RaiseEvent eTimer(Sender, e)
    End Sub

    Public Shared Sub SendMessage(Sender As Object, Message As String)
        RaiseEvent eSendMessage(Sender, Message)
    End Sub

End Class