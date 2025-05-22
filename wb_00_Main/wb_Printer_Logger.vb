Imports System.ComponentModel
Imports combit.Reporting

''' <summary>
''' Eigene Logger-Klasse für List+Label.
''' Die Sub-Routinen werden von direkt aus List+Label aufgerufen. Entsprechend der Einstellungen werden die Meldungen an den WinBack-Logger
''' weitergegeben.
''' Die Logger-Klasse wird in wb_PrinterDialog an die List+Label-Klasse angehängt.
''' 
''' Achtung: Bei aktivierten Log-Ausgaben kann sich der Aufruf der List+Label-Komponenten verzögern.
''' </summary>
Public Class wb_Printer_Logger
    Implements ILlLogger

    ''' <summary>
    ''' List+Label-Debug-Meldung ausgeben.
    ''' Die Debug-Meldung wird über den WinBack-TraceListener als Debug-Meldung in den entsprechenden Logger-Kanal geschrieben
    ''' </summary>
    ''' <param name="category"></param>
    ''' <param name="message"></param>
    ''' <param name="args"></param>
    Public Sub Debug(category As LogCategory, <Localizable(False)> message As String, ParamArray args() As Object) Implements ILlLogger.Debug
        If WantOutput(LogLevels.Debug, category) Then
            Trace.WriteLine("@D_" & message)
        End If
    End Sub

    ''' <summary>
    ''' List+Label-Debug-Meldung ausgeben.
    ''' Die Debug-Meldung wird über den WinBack-TraceListener als Debug-Meldung in den entsprechenden Logger-Kanal geschrieben
    ''' </summary>
    ''' <param name="indentationDelta"></param>
    ''' <param name="category"></param>
    ''' <param name="message"></param>
    ''' <param name="args"></param>
    Public Sub Debug(indentationDelta As Integer, category As LogCategory, <Localizable(False)> message As String, ParamArray args() As Object) Implements ILlLogger.Debug
        If WantOutput(LogLevels.Debug, category) Then
            Trace.WriteLine("@D_" & message)
        End If
    End Sub

    ''' <summary>
    ''' List+Label-Info-Meldung ausgeben.
    ''' Die Info-Meldung wird über den WinBack-TraceListener als Info-Meldung in den entsprechenden Logger-Kanal geschrieben
    ''' </summary>
    ''' <param name="category"></param>
    ''' <param name="message"></param>
    ''' <param name="args"></param>
    Public Sub Info(category As LogCategory, <Localizable(False)> message As String, ParamArray args() As Object) Implements ILlLogger.Info
        If WantOutput(LogLevels.Debug, category) Then
            Trace.WriteLine("@I_" & message)
        End If
    End Sub

    ''' <summary>
    ''' List+Label-Info-Meldung ausgeben.
    ''' Die Info-Meldung wird über den WinBack-TraceListener als Info-Meldung in den entsprechenden Logger-Kanal geschrieben
    ''' </summary>
    ''' <param name="indentationDelta"></param>
    ''' <param name="category"></param>
    ''' <param name="message"></param>
    ''' <param name="args"></param>
    Public Sub Info(indentationDelta As Integer, category As LogCategory, <Localizable(False)> message As String, ParamArray args() As Object) Implements ILlLogger.Info
        If WantOutput(LogLevels.Debug, category) Then
            Trace.WriteLine("@I_" & message)
        End If
    End Sub

    ''' <summary>
    ''' List+Label-Warn-Meldung ausgeben.
    ''' Die Warn-Meldung wird über den WinBack-TraceListener als Warn-Meldung in den entsprechenden Logger-Kanal geschrieben
    ''' </summary>
    ''' <param name="category"></param>
    ''' <param name="message"></param>
    ''' <param name="args"></param>
    Public Sub Warn(category As LogCategory, <Localizable(False)> message As String, ParamArray args() As Object) Implements ILlLogger.Warn
        If WantOutput(LogLevels.Debug, category) Then
            Trace.WriteLine("@W_" & message)
        End If
    End Sub

    ''' <summary>
    ''' List+Label-Fehler-Meldung ausgeben.
    ''' Die Fehler-Meldung wird über den WinBack-TraceListener als Fehler-Meldung in den entsprechenden Logger-Kanal geschrieben
    ''' </summary>
    ''' <param name="category"></param>
    ''' <param name="message"></param>
    ''' <param name="args"></param>
    Public Sub [Error](category As LogCategory, <Localizable(False)> message As String, ParamArray args() As Object) Implements ILlLogger.Error
        If WantOutput(LogLevels.Debug, category) Then
            Trace.WriteLine("@F_" & message)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="level"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    Public Function WantOutput(level As LogLevels, category As LogCategory) As Boolean Implements ILlLogger.WantOutput
        ' Check if log level is wanted
        Select Case level
            Case LogLevels.Debug
                If Not wb_GlobalSettings.Log4net_LL_DebugLevel Then
                    Return False
                End If
            Case LogLevels.Info
                If Not wb_GlobalSettings.Log4net_LL_InfoLevel Then
                    Return False
                End If
            Case LogLevels.Warning
                If Not wb_GlobalSettings.Log4net_LL_WarnLevel Then
                    Return False
                End If
            Case LogLevels.[Error]
                If Not wb_GlobalSettings.Log4net_ErrorLevel Then
                    Return False
                End If
        End Select

        ' Then check if category is selected
        Select Case category
            Case LogCategory.API
                Return wb_GlobalSettings.Log4net_LL_EnableApiCalls
            Case LogCategory.DataProvider
                Return wb_GlobalSettings.Log4net_LL_EnableDataProvider
            Case LogCategory.Licensing
                Return wb_GlobalSettings.Log4net_LL_EnableLicensing
            Case LogCategory.Net
                Return wb_GlobalSettings.Log4net_LL_EnableDotNetComponent
            Case LogCategory.Printer
                Return wb_GlobalSettings.Log4net_LL_EnablePrinterInformation
            Case Else
                Return wb_GlobalSettings.Log4net_LL_EnableOther
        End Select

        'Notausgang
        Return True
    End Function
End Class
