Public Class wb_SiloBef

    Private _LieferMenge As Integer
    Private _Verteilt As Integer = 0
    Private _RestMenge As Integer = 0
    Private _ChargenNummer As String
    Private _Preis As Double
    Private _KompNummer As String

    Public Property LieferMenge As Integer
        Get
            Return _LieferMenge
        End Get
        Set(value As Integer)
            _LieferMenge = value
            tbLieferMenge.Text = _LieferMenge.ToString & " kg"
        End Set
    End Property

    Public Property Verteilt As Integer
        Get
            Return _Verteilt
        End Get
        Set(value As Integer)
            _Verteilt = value
            RestMenge = _LieferMenge - _Verteilt
            tbVerteilt.Text = _Verteilt.ToString & " kg"
        End Set
    End Property

    Public Property RestMenge As Integer
        Get
            Return _RestMenge
        End Get
        Set(value As Integer)
            _RestMenge = value
            tbRest.Text = _RestMenge.ToString & " kg"

            'Hintergrund-Farbe Feld Restmenge
            Select Case RestMenge
                Case < 0
                    tbRest.BackColor = System.Drawing.Color.Red
                    BtnLieferungVerbuchen.Enabled = False
                Case 0
                    tbRest.BackColor = System.Drawing.Color.Lime
                    BtnLieferungVerbuchen.Enabled = True
                Case Else
                    tbRest.BackColor = System.Drawing.SystemColors.Control
                    BtnLieferungVerbuchen.Enabled = False
            End Select

        End Set
    End Property

    Public Property ChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
        Set(value As String)
            _ChargenNummer = value
            lblRohCharge.Text = "Chrg." & value
        End Set
    End Property

    ''' <summary>
    ''' Rohstoff-Nummer(ASCII) zum Silo
    ''' </summary>
    ''' <returns></returns>
    Public Property KompNummer As String
        Get
            Return _KompNummer
        End Get
        Set(value As String)
            _KompNummer = value
            lblNummer.Text = "Nr.  " & _KompNummer
        End Set
    End Property

    Public Property Preis As Double
        Get
            Return _Preis
        End Get
        Set(value As Double)
            _Preis = value
        End Set
    End Property

    Private Sub BtnLieferungVerbuchen_Click(sender As Object, e As EventArgs) Handles BtnLieferungVerbuchen.Click
        Parent.FindForm.DialogResult = vbOK
        Parent.FindForm.Close()
    End Sub

    Private Sub Abbruch_Click(sender As Object, e As EventArgs) Handles Abbruch.Click
        If MsgBox("Soll das Verteilen der Lieferung auf die einzelnen Silos wirklich abgebrochen werden ?", MsgBoxStyle.YesNo, "Silo Befüllung") = MsgBoxResult.Yes Then
            Parent.FindForm.DialogResult = vbAbort
            Parent.FindForm.Close()
        End If
    End Sub
End Class
