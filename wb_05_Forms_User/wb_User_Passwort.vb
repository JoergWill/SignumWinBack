Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_User_Shared

Public Class wb_User_Passwort
    Inherits DockContent
    Private Sub wb_User_Passwort_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DetailInfo()
    End Sub

    Public Sub DetailInfo()
        'User Name
        tUserName.Text = User.Name
        'User Personalnummer
        tPersonalNr.Text = User.PersonalNr

        'Mit Admin-Rechten wird das Passwort angezeigt
        If wb_AktUser.RechtOK(20, wb_AktUser.SuperUser) Then
            'Passwörter anzeigen
            tUserPassAlt.UseSystemPasswordChar = False
            tbUserPassNeu_A.UseSystemPasswordChar = False
            tbUserPassNeu_B.UseSystemPasswordChar = False
            'altes Passwort vorausfüllen
            tUserPassAlt.Text = User.Passwort
            tUserPassAlt.TabStop = False
            'Eingabefeld Passwort neu
            tbUserPassNeu_A.Focus()
        End If
    End Sub

    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high", Justification:="<Ausstehend>")>
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        'Prüfen ob das alte Passwort richtig ist
        If tUserPassAlt.Text = User.Passwort Then
            'Prüfen ob beide Eingabefelder für das neue Passwort identisch sind
            If tbUserPassNeu_A.Text = tbUserPassNeu_B.Text Then
                'Passwort ändern
                User.Passwort = tbUserPassNeu_A.Text
                'Prüfen ob die Änderung durchgführt wurde (Check auf doppelte Einträge)
                If User.Passwort <> tbUserPassNeu_A.Text Then
                    MsgBox("Das neue Passwort ist ungültig !", MsgBoxStyle.Critical)
                Else
                    'Alle Eingaben korrekt - Daten speichern
                    wb_User_Shared.Edit_Leave(sender)
                    'alle anderen Fenster aktualisieren
                    wb_User_Shared.Liste_Click(sender)
                    'Fenster Passwort-Eingabe schliessen
                    Close()
                End If
            Else
                'Prüfen ob ein Eingabe-Feld leer ist
                If tbUserPassNeu_A.Text = "" Or tbUserPassNeu_B.Text = "" Then
                    MsgBox("Bitte beide Eingabefelder für das neue Passwort ausfüllen", MsgBoxStyle.Exclamation)
                Else
                    MsgBox("Die Eingabewerte für das neue Passwort sind unterschiedlich", MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            'Prüfen ob das alte Passwort eingegeben wurde
            If tUserPassAlt.Text = "" Then
                MsgBox("Bitte zuerst das alte Passwort eingeben", MsgBoxStyle.Exclamation)
            Else
                MsgBox("Das alte Passwort wurde nicht korrekt eingeben !", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
End Class