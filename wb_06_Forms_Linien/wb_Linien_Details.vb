Imports WeifenLuo.WinFormsUI.Docking
Imports System.Globalization
Imports System.Threading

Public Class wb_Linien_Details
    Inherits DockContent

    Private Sub LinienDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler wb_Linien_Shared.eListe_Click, AddressOf DetailInfo
    End Sub

    Private Sub Panel_Leave(sender As Object, e As EventArgs) Handles Panel.Leave
        wb_Linien_Shared.aktBezeichnung = tBezeichnung.Text
        wb_Linien_Shared.aktAdresse = tAdresse.Text
        wb_Linien_Shared.Edit_Leave(sender)
    End Sub

    Public Sub DetailInfo(sender As Object)
        tBezeichnung.Text = wb_Linien_Shared.aktBezeichnung
        tAdresse.Text = wb_Linien_Shared.aktAdresse

        'Linie-Nummer aus der IP-Adresse
        Dim Linie As String = wb_Linien_Shared.WinBackLinieNummer
        If Linie <> "" Then
            'Zusatzinformationen Linie einblenden
            lbLinienSprache.Visible = True
            lbScanner.Visible = True
            cbScannerDefekt.Visible = True

            'aktuelle Daten zur Linie aus winback.Konfiguration auslesen - Sprache
            If wb_GlobalSettings.getWinBackKonfiguration("__cl" & Linie & ":sprache") Then
                'Länderflagge anzeigen
                Dim SpracheLinie As Integer = wb_GlobalSettings.KonfigurationWert
                lbLinienSprache.Image = LanguageFlags.Images(SpracheLinie)
                lbLinienSprache.Text = wb_Language.GetLanguageName(SpracheLinie)
            Else
                lbLinienSprache.Image = LanguageFlags.Images(wb_GlobalSettings.WinBackLanguage1)
                lbLinienSprache.Text = " "
            End If

            'aktuelle Daten zur Linie aus winback.Konfiguration auslesen - Scanner aktiv/defekt
            If wb_GlobalSettings.getWinBackKonfiguration("__cl" & Linie & ":scanner_defekt") Then
                'Zusatzinformationen Scanner einblenden
                lbScanner.Enabled = True
                cbScannerDefekt.Enabled = True
                'Scanner aktiv-deaktiviert/defekt
                Dim ScannerDefekt As Integer = wb_GlobalSettings.KonfigurationWert
                cbScannerDefekt.Checked = (ScannerDefekt = 1)
            Else
                'Zusatzinformationen Scanner einblenden
                lbScanner.Enabled = False
                cbScannerDefekt.Enabled = False
                cbScannerDefekt.Checked = False
            End If
        Else
            'Zusatzinformationen Linie ausblenden
            lbLinienSprache.Visible = False
            lbScanner.Visible = False
            cbScannerDefekt.Visible = False
        End If

    End Sub

    Public Sub DetailEdit()
        tBezeichnung.Focus()
    End Sub

    Private Sub cbScannerDefekt_Click(sender As Object, e As EventArgs) Handles cbScannerDefekt.Click
        'Linie
        Dim Linie As String = wb_Linien_Shared.WinBackLinieNummer
        If cbScannerDefekt.Checked Then
            wb_GlobalSettings.setWinBackKonfiguration("__cl" & Linie & ":scanner_defekt", "1")
        Else
            wb_GlobalSettings.setWinBackKonfiguration("__cl" & Linie & ":scanner_defekt", "0")
        End If
    End Sub

    Private Sub wb_Linien_Details_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        AddHandler wb_Linien_Shared.eListe_Click, AddressOf DetailInfo
    End Sub
End Class