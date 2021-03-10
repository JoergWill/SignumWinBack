
Imports System.Drawing
Imports System.Reflection
Imports Signum.OrgaSoft.GUI

Public Class wb_Main_Shared
    Private Shared _MainProgress As wb_Main_Progress = Nothing
    Private Shared _MainProgressVisible As Boolean = False
    Private Shared _MenuButtons As New List(Of IButton)
    Private Shared _ShaddowButtons As New List(Of IButton)

    Public Shared Event eOpenForm(sender As Object, FormName As String)
    Public Shared Event eTimer(sender As Object, e As String)
    Public Shared Event eSendMessage(sender As Object, Message As String)

    Public Shared Sub AddMenuButton(Gruppe As IGroup, Name As String, Text As String, ToolTip As String, PictureSmall As Image, PictureLarge As Image, ClickHandler As EventHandler, Tag As Integer)
        Dim MenuBtn As IButton = Gruppe.AddButton(Tag.ToString("000") & Name, Text, ToolTip, PictureSmall, PictureLarge, ClickHandler)
        _MenuButtons.Add(MenuBtn)
    End Sub

    Public Shared Sub CheckMenu()
        'pürfen ob schon ein WinBack-Menu existiert
        If _MenuButtons.Count > 0 Then
            'Login als Superuser
            Dim SuperUser As Boolean = (wb_GlobalSettings.OrgaBackEmployee = "SYS")
            'erste Gruppe
            Dim Group As IGroup = _MenuButtons.First.Parent
            setGroupVisible(Group, False)

            'Alle Signum-Menu-Buttons durchlaufen und einzeln ein/ausschalten
            For Each Btn In _MenuButtons
                'Neue Gruppe (Gruppe nur ausblenden wenn WinBack-Guppe)
                If Btn.Parent.Name <> Group.Name Then
                    Group = Btn.Parent
                    setGroupVisible(Group, False)
                End If
                'Button ein/ausblenden
                Btn.Visible = wb_AktRechte.RechtOK(Left(Btn.Name, 3), SuperUser)
                'Wenn ein Button sichtbar ist, bleibt die Gruppe sichtbar
                setGroupVisible(Group, Btn.Visible Or Group.Visible)
            Next
        End If
    End Sub
    Private Shared Sub setGroupVisible(ByRef Group As IGroup, visible As Boolean)
        'Property Visible darf nur bei WinBack-Gruppen geändert werden
        If Group.Name.StartsWith("WinBack") Then
            Group.Visible = visible
        End If
    End Sub

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

            Windows.Forms.Application.DoEvents()
        End If
    End Sub

    Public Shared Sub HideProgressBar()
        If _MainProgressVisible Then
            If _MainProgress IsNot Nothing Then
                If _MainProgressVisible Then
                    _MainProgress.Close()
                    _MainProgress = Nothing
                End If
            End If
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

        Dim sAssemblyFileName As String = sAssemblyName + ".dll"
        Dim sAssemblyPath As String

        If sAssemblyName.EndsWith(".resources") Then
            Trace.WriteLine("AssemblyName resources " & sAssemblyName)
            'TODO Das WinBack-AddIn fällt hier in Belgien auf die Nase !!
            Return DefaultAssembly

            'Dim sResourceDirectory As String = IO.Path.Combine(sApplicationDirectory, sAssemblyCulture)
            'sAssemblyPath = IO.Path.Combine(sResourceDirectory, sAssemblyFileName)
        Else
            sAssemblyPath = IO.Path.Combine(sApplicationDirectory & "dll\", sAssemblyFileName)
            If IO.File.Exists(sAssemblyPath) Then
                Trace.WriteLine("AssemblyName dll " & sAssemblyPath)
                Return If(Debugger.IsAttached, Reflection.Assembly.LoadFile(sAssemblyPath), Assembly.Load(IO.File.ReadAllBytes(sAssemblyPath)))
            Else
                Trace.WriteLine("AssemblyName resources " & sAssemblyName)
                Return DefaultAssembly
            End If
        End If
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