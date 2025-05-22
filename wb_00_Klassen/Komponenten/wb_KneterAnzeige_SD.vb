Imports System.Windows.Forms

Public Class wb_KneterAnzeige_SD

    Public Const cReadMem = 56

    Private _Connected As Boolean
    Private _Index As Integer
    Private _ShowMe As Boolean
    Private _Kneter As New wb_Kneter

    Public Sub New(KneterNr As Integer, Optional Enable As Boolean = True)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'Index entspricht der Kneter-Nummer
        Index = KneterNr
        'Kneter anzeigen
        ShowMe = Enable
        pgKnetenMischen.CustomFont = New System.Drawing.Font("Arial", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub

    Public Property Connected As Boolean
        Get
            Return _Connected
        End Get
        Set(value As Boolean)
            _Connected = value
            ShowTitle()
        End Set
    End Property

    Public Property Title As String
        Get
            Return lblTitel.Text
        End Get
        Set(value As String)
            lblTitel.Text = value
        End Set
    End Property

    Public Property Index As Integer
        Get
            Return _Index
        End Get
        Set(value As Integer)
            _Index = value
            ShowTitle()
        End Set
    End Property

    Public Property ShowMe As Boolean
        Get
            Return _ShowMe
        End Get
        Set(value As Boolean)
            _ShowMe = value
            Me.Visible = value
        End Set
    End Property

    Private Sub ShowTitle()
        'keine Verbindung zur SPS
        If Not _Connected Then
            'Anzeige Kneter nicht verbunden
            Title = "Kneter " & Index & " (nicht verbunden)"
            pgKnetenMischen.CustomText = ""
            'Anzeige Rezept-Name/Nummer Soll-/Istwerte löschen
            ClearRzSollIst()
        Else
            'Titel anzeigen
            Title = "Kneter " & Index
        End If
    End Sub

    Private Sub ShowData()
        If _Connected Then
            Select Case _Kneter.PlcStatus
                Case 5
                    pgKnetenMischen.CustomText = "Warten auf Start"
                    ClearRzSollIst()
                Case 120
                    pgKnetenMischen.CustomText = "Stop von WinBack"
                    ShowRzSollIst()
                Case Else
                    pgKnetenMischen.CustomText = _Kneter.KompBezeichnung
                    ShowRzSollIst()
            End Select
        End If
    End Sub

    Private Sub ShowRzSollIst()
        'Rezeptnummer und Rezept-Bezeichnung
        lblRezeptNummer.Text = _Kneter.RezeptNummer
        lblRezept.Text = _Kneter.RezeptBezeichnung

        'Soll- und Istwerte
        lblSollwert.Text = _Kneter.sSollwert
        lblIstwert.Text = _Kneter.sIstwert
        lblTeigruhe.Text = _Kneter.sTeigruhe
        lblFertig.Text = _Kneter.sFertig
        lblFertigUhrzeit.Text = _Kneter.sFertigUhrzeit

        'Fortschritts-Anzeige
        pgKnetenMischen.CustomValue = _Kneter.dProzent
    End Sub

    Private Sub ClearRzSollIst()
        'Rezeptnummer und Rezept-Bezeichnung löschen
        lblRezeptNummer.Text = ""
        lblRezept.Text = ""

        'Soll- und Istwerte löschen
        lblSollwert.Text = "-"
        lblIstwert.Text = "-"
        lblTeigruhe.Text = "-"
        lblFertig.Text = "-"

        'Fortschritts-Anzeige
        pgKnetenMischen.CustomValue = 0
    End Sub

    ''' <summary>
    ''' Lesen der aktuellen Kneter-Daten aus der SPS.
    ''' 
    ''' Diese Routine wird vom Haupt-Programm zyklisch aufgerufen und liest die aktuellen
    ''' Daten aus der Kneter-SPS.
    ''' Wenn sich die Chargen-Nummer oder der Status des Kneters ändern, werden die aktuellen
    ''' Daten zu der übertragenen Chargen-Nummer aus der Tabelle winback.ArbRezepte gelesen.
    ''' 
    ''' Damit stehen alle Daten zu diesem Knet-Ablauf im Kneter-Objekt und können angezeigt werden.
    ''' </summary>
    ''' <param name="BinRead"></param>
    Public Sub ReadFromStream(ByRef BinRead As IO.BinaryReader)
        'Lesen der aktuellen Kneter-Daten aus dem ADS-Stream)
        _Kneter.ReadFromStream(BinRead, Index)

        'Anzeige-Daten aktualisieren und anzeigen
        If ShowMe Then
            'Daten anzeigen
            ShowData()
        End If
    End Sub

End Class
