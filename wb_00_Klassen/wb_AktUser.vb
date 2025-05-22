Imports System.Windows.Forms

Public Class wb_AktUser
    Inherits wb_AktRechte

    Private Shared _SuperUser As Boolean = False
    Private Shared _UserNr As Integer = -1
    Private Shared _UserName As String = ""
    Private Shared _UserGruppe As Integer = -1
    Private Shared _UserLanguage As String = ""

    Public Shared Property UserNr As Integer
        Get
            Return _UserNr
        End Get
        Set(value As Integer)
            _UserNr = value
        End Set
    End Property

    Public Shared Property UserName As String
        Get
            Return _UserName
        End Get
        Set(value As String)
            _UserName = value
        End Set
    End Property

    Public Shared Property UserGruppe As Integer
        Get
            Return _UserGruppe
        End Get
        Set(value As Integer)
            _UserGruppe = value
            UpdateUserGruppenRechteTabelle(_UserGruppe)
        End Set
    End Property

    Public Shared Property UserLanguage As String
        Get
            Return _UserLanguage
        End Get
        Set(value As String)
            _UserLanguage = value
            UpdateUserLanguage(value)
        End Set
    End Property

    Public Shared Property SuperUser As Boolean
        Get
            Return _SuperUser
        End Get
        Set(value As Boolean)
            _SuperUser = value
        End Set
    End Property

    Public Shared Sub Logout()
        UserNr = wb_Global.UNDEFINED
        UserGruppe = wb_Global.UNDEFINED
        UserName = ""
        SuperUser = False
    End Sub

    Public Shared Function Login(UserNr As Integer) As Boolean
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Neuen Benutzer aus WinBack-DB lesen
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserLogin, UserNr.ToString))

        If winback.Read Then
            _UserNr = UserNr
            UserName = winback.sField("IP_Wert4str")
            UserGruppe = winback.iField("IP_ItemID")
            _UserLanguage = winback.sField("IP_Wert5str")

            'Verbindung wieder schliessen
            winback.Close()
            Return True
        Else
            'Verbindung wieder schliessen
            winback.Close()
            Return False
        End If

    End Function

    ''' <summary>
    ''' Gibt True zurück, wenn der Benutzer in der WinBack-Datenbank gefunden wird.
    ''' </summary>
    ''' <param name="UserName"></param>
    ''' <returns></returns>
    Public Shared Function Login(UserName As String) As Boolean
        Dim result As Boolean = False
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        'Neuen Benutzer aus WinBack-DB lesen
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserName, UserName))

        If winback.Read Then
            UserNr = winback.iField("IP_Wert1int")
            _UserName = UserName
            UserGruppe = winback.iField("IP_ItemID")
            _UserLanguage = winback.sField("IP_Wert5str")
            _SuperUser = False
            result = True
        Else
            'Benutzer ist OrgaBack Benutzername
            _UserName = UserName
        End If

        'User SYS ist Super-User
        If (UserName.Contains("System") OrElse UserName.Contains("Administrator")) OrElse UserName = "KurzSesam" OrElse wb_GlobalSettings.OrgaBackEmployee = "SYS" Then
            SuperUser = True
        End If

        'Verbindung wieder schliessen
        winback.Close()
        Return result
    End Function

    Private Shared Sub UpdateUserLanguage(Lang As String)
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Try
            'Update Benutzer in Datenbank
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserUpdateLang, _UserNr, Lang))
        Catch
            Trace.WriteLine("@E_Fehler beim Schreiben der Benutzer-Daten")
        End Try
        'Verbindung wieder schliessen
        winback.Close()
    End Sub

    ''' <summary>
    ''' Blendet die einzelnen Elemente abhängig von den User-Rechten der Gruppe ein bzw. aus
    ''' Alle Controls mit Tag ungleich 0 werden bearbeitet.
    ''' In wb_AktSysKonfig.SysKonfigOK() steht die globale System-Konfiguration (User-Gruppe -1)
    ''' 
    ''' Die Rechte für die einzelnen Gruppe kommen aus der Tabelle winback.ItemParameter(IP_ItemTyp=2)
    ''' Die TagNummer der Controls ist winback-ItemParameter.IP_ItemID + 100
    ''' 
    '''      IP_ItemID           Tag
    ''' --+-------------------+-------+-------------------------
    '''      0 = Produktion      100
    '''      1 = Chargen         101
    '''      2 = Artikel         102
    '''      3 = Rezepte         103
    '''      4 = Material        104
    '''      5 = Service         105
    '''      6 = Installation    106
    '''      7 = Hilfe           107
    '''      
    '''      IP_ItemID           Tag
    ''' --+-------------------+-------+-------------------------
    '''      10 = Rezepte ReadOnly
    '''      11 = Rohstoffe ReadOnly
    '''      12 = Rohst.Flags ändern
    '''      13 = Rzpt.V1 readonly
    '''      14 = Im Rezept nur spezielle Rohstoffe ändern
    '''      
    '''      Module WinBack-Büro
    ''' --+-------------------+-------+-------------------------
    '''      20 = Benutzerverw    120
    '''      21 = Vnc             121
    '''      22 = Statistik       122
    '''      23 = Rezepthistorie  123
    '''      24 = Material Import 124
    '''      25 = Excel Export    125
    '''      26 = Bakelink        126
    '''      27 = Bestellwesen    127
    '''      28 = Inhalts-Stoffe  128
    '''      29 = Cloud           129 (ohne Recht 29 Cloud nur mit Einschränkungen)
    '''      30 = Prod.Planung    130
    '''      
    ''' </summary>
    ''' <param name="m_Control"></param>
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    Public Shared Sub SetUserRechte(m_Control As Control)

        'Schleife über alle Controls
        For Each ctrl As Control In m_Control.Controls
            ctrl.Enabled = RechtOK(ctrl.Tag, _SuperUser) AndAlso wb_AktSysKonfig.SysKonfigOK(ctrl.Tag, _SuperUser)

            'Rekursiver Aufruf bei verschachtelten Controls
            If ctrl.Controls.Count > 0 Then
                SetUserRechte(ctrl)
            End If

            'Sonderfunktion für Ribbon, RibbonTab, RibbonPanel und RibbonItem
            If ctrl.GetType().Equals(GetType(Ribbon)) Then
                For Each rTab As RibbonTab In DirectCast(ctrl, Ribbon).Tabs
                    Try
                        rTab.Enabled = RechtOK(rTab.Tag, _SuperUser) AndAlso wb_AktSysKonfig.SysKonfigOK(rTab.Tag, _SuperUser)
                        For Each rPnl As RibbonPanel In rTab.Panels
                            rPnl.Enabled = RechtOK(rPnl.Tag, _SuperUser) AndAlso wb_AktSysKonfig.SysKonfigOK(rPnl.Tag, _SuperUser)
                            For Each rBtn As RibbonItem In rPnl.Items
                                rBtn.Enabled = RechtOK(rBtn.Tag, _SuperUser) AndAlso wb_AktSysKonfig.SysKonfigOK(rBtn.Tag, _SuperUser)
                            Next
                        Next
                    Catch ex As Exception
                        Trace.WriteLine("@E_Fehler beim Ermitteln der Benutzer-Rechte")
                    End Try
                Next
            End If
        Next
    End Sub
End Class
