Imports System.Globalization
Imports WinBack.wb_Functions
Imports WinBack.wb_Global

Public Class wb_ktTyp301
    Inherits wb_ChangeLog

    Private Structure Typ301
        Public _Allergen As AllergenInfo
        Public _Naehrwert As Double
    End Structure
    Private NaehrwertInfo(maxTyp301) As Typ301
    Private _TimeStamp
    Public Shared ktTyp301Params As New Hashtable

    Public Property TimeStamp As DateTime
        Get
            Return _TimeStamp
        End Get
        Set(value As DateTime)
            _TimeStamp = value
        End Set
    End Property

    Public Function IsAllergen(index As Integer) As Boolean
        If index >= minTyp301Allergen And index <= maxTyp301Allergen Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Property Allergen(index As Integer) As AllergenInfo
        Get
            If IsAllergen(index) Then
                Return NaehrwertInfo(index)._Allergen
            Else
                Return AllergenInfo.ERR
            End If
        End Get
        Set(value As AllergenInfo)
            If IsAllergen(index) Then
                NaehrwertInfo(index)._Allergen = value
            End If
        End Set
    End Property

    Public Property Naehrwert(index As Integer) As Double
        Get
            If Not IsAllergen(index) Then
                Return NaehrwertInfo(index)._Naehrwert
            Else
                Return 0.0
            End If
        End Get
        Set(value As Double)
            If Not IsAllergen(index) Then
                NaehrwertInfo(index)._Naehrwert = value
            End If
        End Set
    End Property

    Public Property Wert(index As Integer) As String
        Get
            If IsAllergen(index) Then
                Return NaehrwertInfo(index)._Allergen
            Else
                Return NaehrwertInfo(index)._Naehrwert
            End If
        End Get
        Set(value As String)
            If IsAllergen(index) Then
                'Änderungen loggen
                NaehrwertInfo(index)._Allergen = ChangeLogAdd(LogType.Alg, index, NaehrwertInfo(index)._Allergen, StringtoAllergen(value))
            Else
                'Änderungen loggen
                NaehrwertInfo(index)._Naehrwert = ChangeLogAdd(LogType.Nrw, index, NaehrwertInfo(index)._Naehrwert, StrToDouble(value))
            End If
        End Set
    End Property

    Public WriteOnly Property dlNaehrWert(index As String) As String
        Set(value As String)
            Dim idx As Integer = DatenLinkToIndex(index)
            If idx > 0 Then
                Try
                    Naehrwert(idx) = CDbl(value)
                Catch ex As Exception
                    Trace.WriteLine("Fehler bei DatenLink - Index = " & index & " Wert = " & value)
                End Try
            Else
                Trace.WriteLine("Fehler bei DatenLink - Index " & index & " nicht definiert")
            End If
        End Set
    End Property

    Public WriteOnly Property dlAllergen(index As String) As String
        Set(value As String)
            Dim idx As Integer = DatenLinkToIndex(index)
            If idx > 0 Then
                Select Case value
                    Case "CONTAINED"
                        Allergen(idx) = AllergenInfo.C
                    Case "MAY_CONTAINED"
                        Allergen(idx) = AllergenInfo.T
                    Case Else
                        Allergen(idx) = AllergenInfo.ERR
                End Select
            Else
                Trace.WriteLine("Fehler bei DatenLink - Index " & index & " nicht definiert")
            End If
        End Set
    End Property

    Public Sub ClearReport()
        ChangeLogClear()
    End Sub

    Public Function GetReport(Optional ReportAll As Boolean = vbFalse) As String
        Return ChangeLogReport(ReportAll)
    End Function

    Public Shared Sub LoadKompon301Tabelle()
        Dim k As ktTyp301Param
        Dim winback As New wb_Sql(My.Settings.WinBackConString, My.Settings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlKompTyp301)
        ktTyp301Params.Clear()
        While winback.Read

            k.ParamNr = winback.iField("KT_ParamNr")
            k.Bezeichnung = TextFilter(winback.sField("KT_Bezeichnung"))
            k.KurzBezeichnung = winback.sField("KT_KurzBez")
            k.Gruppe = wb_Functions.StringTokt301Gruppe(winback.sField("KT_Wert"))
            k.Einheit = winback.sField("E_Einheit")
            k.Feld = winback.sField("KT_Kommentar")
            k.Used = (winback.sField("KT_Rezept") = "X")
            Try
                ktTyp301Params.Add(k.ParamNr, k)
            Catch
            End Try
        End While
        winback.Close()
    End Sub

End Class
