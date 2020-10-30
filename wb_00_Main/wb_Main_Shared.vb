Imports System.Reflection

Public Class wb_Main_Shared
    Private Shared _MainProgress As wb_Main_Progress = Nothing
    Private Shared _MainProgressVisible As Boolean = False
    Public Shared Event eOpenForm(sender As Object, FormName As String)


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
    Public Shared Function MyAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Console.WriteLine("Resolving...")

        Dim sApplicationDirectory As String = wb_GlobalSettings.pAddInPath
        Dim sAssemblyName As String = New AssemblyName(args.Name).Name

        Dim arrFields As String() = args.Name.Split(","c)
        Dim sAssemblyCulture As String = arrFields(2).Substring(arrFields(2).IndexOf("="c) + 1)

        Dim sAssemblyFileName As String = sAssemblyName + ".dll"
        Dim sAssemblyPath As String

        If sAssemblyName.EndsWith(".resources") Then
            Dim sResourceDirectory As String = IO.Path.Combine(sApplicationDirectory, sAssemblyCulture)
            sAssemblyPath = IO.Path.Combine(sResourceDirectory, sAssemblyFileName)
        Else
            sAssemblyPath = IO.Path.Combine(sApplicationDirectory & "dll\", sAssemblyFileName)
        End If

        If IO.File.Exists(sAssemblyPath) Then
            Return If(Debugger.IsAttached, Reflection.Assembly.LoadFile(sAssemblyPath), Assembly.Load(IO.File.ReadAllBytes(sAssemblyPath)))
        Else
            Return GetType(ob_Main_Menu).Assembly
        End If
    End Function


    Public Shared Sub OpenForm(Sender As Object, FormName As String)
        RaiseEvent eOpenForm(Sender, FormName)
    End Sub
End Class
