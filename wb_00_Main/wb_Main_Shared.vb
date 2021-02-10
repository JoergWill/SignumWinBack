
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
    Public Shared Sub AddShaddowButton(Gruppe As IGroup, Name As String, Text As String, ToolTip As String, PictureSmall As Image, PictureLarge As Image, Tag As Integer)
        Dim MenuBtn As IButton = Gruppe.AddButton(Tag.ToString("000") & Name, "", ToolTip, PictureSmall, PictureLarge, Nothing)
        MenuBtn.Visible = False
        _MenuButtons.Add(MenuBtn)
    End Sub

    Public Shared Sub CheckMenu()
        'Login als Superuser
        Dim SuperUser As Boolean = (wb_GlobalSettings.OrgaBackEmployee = "SYS")
        Dim GroupVisible As Boolean
        Dim GroupName As String = ""

        'Alle Signum-Menu-Buttons durchlaufen und einzeln ein/ausschalten
        For Each Btn In _MenuButtons
            'Buttons ohne ClickHandler sind Shaddow-Buttons
            If Btn.Text = "" Then
                'wenn alle Elemente der Gruppe ausgeblendet sind, wird der Shaddow-Button aktiviert
                Btn.Visible = Not GroupVisible
            Else
                'Neue Gruppe
                If Btn.Parent.Name <> GroupName Then
                    GroupName = Btn.Parent.Name
                    GroupVisible = False
                End If
                'Button ein/ausblenden
                Btn.Visible = wb_AktRechte.RechtOK(Left(Btn.Name, 3), SuperUser)
                'Wenn ein Button sichtbar ist, bleibt die Gruppe sichtbar
                If Btn.Visible Then
                    GroupVisible = True
                End If
            End If

            'Damit der Menu-Tab(Gruppe) nicht (fälschlicherweise beim nächsten Login in OrgaBack) gelöscht wird muss ein Dummy-Button aktiviert werden.
            'ShowShaddowButton(Btn.Name, Btn.Visible)
        Next
    End Sub
    Private Shared Sub ShowShaddowButton(Name As String, Visible As Boolean)
        Dim Tag As String = Left(Name, 3)
        For Each ShaddowBtn In _ShaddowButtons
            If Tag = Left(ShaddowBtn.Name, 3) Then
                ShaddowBtn.Visible = Not Visible
                Exit For
            End If
        Next
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
            'TODO Das WinBack-AddIn fällt hier in Belgien auf die Nase !!
            Return DefaultAssembly

            'Debug.Print("AssemblyName resources " & sAssemblyName)
            'Dim sResourceDirectory As String = IO.Path.Combine(sApplicationDirectory, sAssemblyCulture)
            'sAssemblyPath = IO.Path.Combine(sResourceDirectory, sAssemblyFileName)
        Else
            sAssemblyPath = IO.Path.Combine(sApplicationDirectory & "dll\", sAssemblyFileName)
            If IO.File.Exists(sAssemblyPath) Then
                Return If(Debugger.IsAttached, Reflection.Assembly.LoadFile(sAssemblyPath), Assembly.Load(IO.File.ReadAllBytes(sAssemblyPath)))
            Else
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