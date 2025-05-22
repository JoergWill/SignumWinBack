Imports System.Net
Imports System.Net.Mail

Public Class wb_Mail
    Private _mHost As String = Nothing
    Private _mSender As String = Nothing
    Private _mSenderPass As String = Nothing
    Private _mError As String

    ''' <summary>
    ''' smtp-Host für Mail-Client
    ''' z.B. smtp.web.de
    ''' 
    ''' Ist der Mail-Host nicht definiert, wird der Eintrag aus winback.ini verwendet
    ''' </summary>
    ''' <returns></returns>
    Public Property mHost As String
        Get
            If _mHost = Nothing Then
                Return wb_GlobalSettings.mHost
            Else
                Return _mHost
            End If
        End Get
        Set(value As String)
            _mHost = value
        End Set
    End Property

    ''' <summary>
    ''' Benutzername Mail-Host.
    ''' (Absender-Adresse z.B. xxx@web.de)
    ''' </summary>
    ''' <returns></returns>
    Public Property mSenderAddr As String
        Get
            If _mSender = Nothing Then
                Return wb_GlobalSettings.mSenderAddr
            Else
                Return _mSender
            End If
        End Get
        Set(value As String)
            _mSender = value
        End Set
    End Property

    ''' <summary>
    ''' Passwort Mail-Host.
    ''' </summary>
    ''' <returns></returns>
    Public Property mSenderPass As String
        Get
            If _mSenderPass = Nothing Then
                Return wb_GlobalSettings.mSenderPass
            Else
                Return _mSenderPass
            End If
        End Get
        Set(value As String)
            _mSenderPass = value
        End Set
    End Property

    ''' <summary>
    ''' Falls ein Fehler beim Senden aufgetreten ist, wird hier die Fehlermeldung (Exception)
    ''' zurückgegeben
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property mError As String
        Get
            Return _mError
        End Get
    End Property

    ''' <summary>
    ''' Mail senden. Wenn das Versenden der eMail fehlschlägt, wird False zurückgegeben. 
    ''' Der Fehlertext steht dann in mError.
    ''' </summary>
    ''' <param name="mRecipient"></param>Mail-Adresse Empfänger
    ''' <param name="mSubject"></param>Mail Betreff
    ''' <param name="mBody"></param>Mail Inhalt
    ''' <param name="mSsl"></param>ssl-Verschlüsselung aktivieren
    ''' <returns></returns>
    Public Function SendMail(mRecipient As String, mSubject As String, mBody As String, Optional mSsl As Boolean = True) As Boolean
        'Mail-Client (Host)
        Dim myClient As New SmtpClient(mHost)
        'verschlüsselte Verbindung aktivieren
        myClient.EnableSsl = mSsl

        'Sender-Mail-Adresse und Passwort
        myClient.Credentials = New NetworkCredential(mSenderAddr, mSenderPass)
        'eMail versenden
        Try
            _mError = ""
            myClient.Send(mSenderAddr, mRecipient, mSubject, mBody)
            Return True
        Catch ex As Exception
            _mError = ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Startet das Standard-Mail-Programm (outlook) und übergibt als Parameter
    ''' </summary>
    ''' <param name="mRecipient"></param>   Empfängeradresse
    ''' <param name="mSubject"></param>     Betreff
    ''' <param name="mBody"></param>        Nachricht
    ''' <returns></returns>
    Public Function StartMail(mRecipient As String, mSubject As String, mBody As String, Optional mAttachment As String = "") As Boolean
        Try
            _mError = ""
            If mAttachment = "" Then
                Process.Start("mailto:" & mRecipient & "?subject=" & mSubject & "&body=" & mBody)
            Else
                Process.Start("mailto:" & mRecipient & "?subject=" & mSubject & "&body=" & mBody & "&Attach=" & mAttachment)
            End If
            Return True
        Catch ex As Exception
            _mError = ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Startet über Startmail das Standard eMail-Programm und übergibt den Anforderungstext und die 
    ''' Abfrage-Mail-Adresse.
    ''' </summary>
    ''' <param name="Rohstoff"></param>
    ''' <param name="Lieferant"></param>
    ''' <returns></returns>
    Public Function StartMail_CloudAnforderung(Rohstoff As String, Lieferant As String) As Boolean
        'Empfänger-Adresse (nwt@winback.de)
        Dim Recipient As String = wb_Credentials.WinBackCloud_MailAdr
        'Anforderung Dateneingabe in die Cloud (Betreffzeile)
        Dim Subject As String = My.Resources.tpCloudMailSubject
        'Anforderung Dateneingabe in die Cloud (Text)
        Dim Body As String = PrepareMailText(My.Resources.tpCloudMailText, Rohstoff, Lieferant)

        'Mail-Programm aufrufen
        Return StartMail(Recipient, Subject, Body)
    End Function

    ''' <summary>
    ''' Startet über Startmail das Standard eMail-Programm und übergibt den Fehlertext und die Stacktrace-Information
    ''' </summary>
    ''' <param name="StackTrace"></param>
    ''' <param name="Message"></param>
    ''' <returns></returns>
    Public Function StartMail_Exception(StackTrace As String, Message As String) As Boolean
        'Empfänger-Adresse (software@winback.de)
        Dim Recipient As String = wb_Credentials.WinBackSoftware_MailAdr
        'Fehlermeldung von WinBack-AddIn (Betreffzeile)
        Dim Subject As String = My.Resources.tpExceptionMailSubject
        'Mail-Text 
        Dim Body As String = PrepareMailText(My.Resources.tpExceptionMailText, Message, StackTrace)

        'Mail-Programm aufrufen
        Return StartMail(Recipient, Subject, Body)
    End Function

    ''' <summary>
    ''' Ersetzt die Platzhalter [0]..[4] im String durch die Parameter Param0..Param4
    ''' Die Kodierung wird nach UTF8 gewandelt.
    ''' Leerzeichen werden durch %20 ersetzt
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <param name="Param0"></param>
    ''' <param name="Param1"></param>
    ''' <param name="Param2"></param>
    ''' <param name="Param3"></param>
    ''' <param name="Param4"></param>
    ''' <returns></returns>
    Public Function PrepareMailText(Text As String, Param0 As String, Optional Param1 As String = "-",
                                           Optional Param2 As String = "-", Optional Param3 As String = "-",
                                           Optional Param4 As String = "-") As String

        'Platzhalter im Text durch Parameter ersetzen
        Text = wb_Functions.SetParams(Text, Param0, Param1, Param2, Param3, Param4)
        'Umwandeln in UTF8
        Text = System.Web.HttpUtility.UrlEncode(Text, System.Text.Encoding.UTF8)
        'alle Leerzeichen in HTML umwandeln (%20)
        Text = Text.Replace("+", "%20")
        'Umgewandelten Text zurückgeben
        Return Text
    End Function


End Class
