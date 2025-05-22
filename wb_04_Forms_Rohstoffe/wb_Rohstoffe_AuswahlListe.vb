Imports EnhEdit
Imports WinBack
Imports WinBack.wb_Rohstoffe_Shared

Public Class wb_Rohstoffe_AuswahlListe
    Private _RohstoffNr As Integer = wb_Global.UNDEFINED
    Private _RohstoffNummer As String = ""
    Private _RohstoffName As String = ""
    Private _RohstoffKommentar As String = ""
    Private _RohstoffType As wb_Global.KomponTypen
    Private _RohstoffEinheit As String = ""
    Private _Filter As String = ""
    Private _RohstoffFormat As EnhEdit_Global.wb_Format
    Private _RohstoffUG As String
    Private _RohstoffOG As String
    Private _RohstoffListe As New ArrayList
    Private _MultiSelect As Boolean = False

    Public WriteOnly Property MultiSelect As Boolean
        Set(value As Boolean)
            _MultiSelect = value
        End Set
    End Property

    Public ReadOnly Property RohstoffListe As ArrayList
        Get
            Return _RohstoffListe
        End Get
    End Property


    Public WriteOnly Property Anzeige As AnzeigeFilter
        Set(value As AnzeigeFilter)
            Select Case value

                Case AnzeigeFilter.Alle        ' alle aktiven Rohstoffe Typ > 100
                    _Filter = "(KO_Type > 100) AND KA_aktiv = 1"

                Case AnzeigeFilter.HandAuto    ' alle aktiven Rohstoffe Typ 101 und 102
                    _Filter = "((KO_Type = 101) OR (KO_Type = 102)) AND KA_aktiv = 1"

                Case AnzeigeFilter.RezeptKomp   'alle Rohstoffe Rezeptauswahl
                    _Filter = "(KO_Type > 100) AND (KO_Type <> " & wb_Functions.KomponTypeToInt(wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE) &
                              ") AND (KO_Type <> " & wb_Functions.KomponTypeToInt(wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE) &
                              ") AND (KO_Type <> " & wb_Functions.KomponTypeToInt(wb_Global.KomponTypen.KO_TYPE_KESSEL) &
                              ") AND (KO_Type <> " & wb_Functions.KomponTypeToInt(wb_Global.KomponTypen.KO_TYPE_KNETER) & ") AND KA_aktiv = 1"

                Case AnzeigeFilter.OhneKneter   'alle Rohstoffe Rezeptauswahl ohne Kneter-Komponenten
                    _Filter = "(KO_Type > 100) AND (KO_Type <> " & wb_Functions.KomponTypeToInt(wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE) &
                              ") AND (KO_Type <> " & wb_Functions.KomponTypeToInt(wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE) &
                              ") AND (KO_Type <> " & wb_Functions.KomponTypeToInt(wb_Global.KomponTypen.KO_TYPE_KESSEL) &
                              ") AND (KO_Type <> " & wb_Functions.KomponTypeToInt(wb_Global.KomponTypen.KO_TYPE_KNETER) &
                              ") AND (KO_Type <> " & wb_Functions.KomponTypeToInt(wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT) & ") AND KA_aktiv = 1"

                Case AnzeigeFilter.NurKneter   'alle aktiven Rohstoffe Type Kneter-Schritt
                    _Filter = "(KO_Type = 118) AND KA_aktiv = 1"

                Case AnzeigeFilter.Sauerteig   ' alle aktiven Rohstoffe Sauerteig
                    _Filter = "(KO_Type < 100) AND KA_aktiv = 1"

                Case Else
                    _Filter = ""
            End Select
        End Set
    End Property

    Public Property RohstoffNr As Integer
        Get
            Return _RohstoffNr
        End Get
        Set(value As Integer)
            _RohstoffNr = value
        End Set
    End Property

    Public Property RohstoffNummer As String
        Get
            Return _RohstoffNummer
        End Get
        Set(value As String)
            _RohstoffNummer = value
        End Set
    End Property

    Public Property RohstoffName As String
        Get
            Return _RohstoffName
        End Get
        Set(value As String)
            _RohstoffName = value
        End Set
    End Property

    Public Property RohstoffType As wb_Global.KomponTypen
        Get
            Return _RohstoffType
        End Get
        Set(value As wb_Global.KomponTypen)
            _RohstoffType = value
        End Set
    End Property

    Public Property RohstoffEinheit As String
        Get
            Return _RohstoffEinheit
        End Get
        Set(value As String)
            _RohstoffEinheit = value
        End Set
    End Property

    Public Property RohstoffKommentar As String
        Get
            Return _RohstoffKommentar
        End Get
        Set(value As String)
            _RohstoffKommentar = value
        End Set
    End Property

    Public Property RohstoffFormat As EnhEdit_Global.wb_Format
        Get
            Return _RohstoffFormat
        End Get
        Set(value As EnhEdit_Global.wb_Format)
            _RohstoffFormat = value
        End Set
    End Property

    Public Property RohstoffUG As String
        Get
            Return _RohstoffUG
        End Get
        Set(value As String)
            _RohstoffUG = value
        End Set
    End Property

    Public Property RohstoffOG As String
        Get
            Return _RohstoffOG
        End Get
        Set(value As String)
            _RohstoffOG = value
        End Set
    End Property

    Private Sub wb_Rezept_AuswahlListe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"Nummer", "&Name", "", "Kommentar", "Gruppe", "Gruppe"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next
        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "KO_Bezeichnung"

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRohstoffLst, "RohstoffAuswahlListe")

        'Filter einschalten
        DataGridView.Filter = _Filter
        'Focus auf Sortier-Feld
        DataGridView.SortCol = 0
        'Sortier-Kriterium ist die zweite Spalte (Rohstoff-Name)
        DataGridView.SetSortColumn(1)
        'Multiselect (Áuswahl-Liste Statistik)
        DataGridView.MultiSelect = _MultiSelect
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridView_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView.DoubleClick
        GetResult()
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        GetResult()
    End Sub

    Private Sub GetResult()
        'Ergebnis-Liste leeren (immer)
        _RohstoffListe.Clear()

        'Aktuelle Auswahl (MultiSelect = False)
        RohstoffNr = DataGridView.iField("KO_Nr")
        RohstoffNummer = DataGridView.Field("KO_Nr_AlNum")
        RohstoffName = DataGridView.Field("KO_Bezeichnung")
        RohstoffKommentar = DataGridView.Field("KO_Kommentar")
        RohstoffType = wb_Functions.IntToKomponType(DataGridView.iField("KO_Type"))
        RohstoffEinheit = DataGridView.Field("E_Einheit")
        RohstoffFormat = wb_Functions.StrToFormat(DataGridView.Field("KT_Format"))
        RohstoffUG = DataGridView.Field("KT_UnterGW")
        RohstoffOG = DataGridView.Field("KT_OberGW")

        'MultiSelect
        For Each dl As System.Windows.Forms.DataGridViewRow In DataGridView.SelectedRows
            Dim RohListenElement As New wb_StatistikListenElement
            RohListenElement.Nr = dl.Cells("KO_Nr").Value
            RohListenElement.Nummer = dl.Cells("KO_Nr_AlNum").Value
            RohListenElement.Bezeichnung = dl.Cells("KO_Bezeichnung").Value

            _RohstoffListe.Add(RohListenElement)
        Next

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub wb_Rohstoffe_AuswahlListe_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Layout sichern
        DataGridView.SaveToDisk("RohstoffAuswahlListe")
    End Sub
End Class