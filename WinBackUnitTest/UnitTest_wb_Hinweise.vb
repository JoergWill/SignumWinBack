Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_Hinweise

    <TestInitialize>
    Sub TestInitialize()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then
            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_GlobalSettings.WinBackDBType = wb_Sql.dbType.mySql
        End If
    End Sub
    <TestMethod()> Public Sub TestDatenTyp()

        'Verarbeitungs-Hinweise Rezept
        Dim h2 As New wb_Hinweise(wb_Global.Hinweise.RezeptHinweise)
        Assert.AreEqual(2, h2.Typ)
        Assert.AreEqual(0, h2.Typ2)

        'Verarbeitungs-Hinweise Artikel
        Dim h3 As New wb_Hinweise(wb_Global.Hinweise.ArtikelHinweise)
        Assert.AreEqual(3, h3.Typ)
        Assert.AreEqual(0, h3.Typ2)

        'Benutzer-Info
        Dim h4 As New wb_Hinweise(wb_Global.Hinweise.UserInfo)
        Assert.AreEqual(4, h4.Typ)
        Assert.AreEqual(0, h4.Typ2)

        'Zutaten-Liste
        Dim h9 As New wb_Hinweise(wb_Global.Hinweise.ZutatenListe)
        Assert.AreEqual(9, h9.Typ)
        Assert.AreEqual(1, h9.Typ2)

        'Mehl-Zusammensetzung
        h9 = New wb_Hinweise(wb_Global.Hinweise.MehlZusammensetzung)
        Assert.AreEqual(9, h9.Typ)
        Assert.AreEqual(2, h9.Typ2)

        'Gebäck-Charakteristik
        Dim h10 = New wb_Hinweise(wb_Global.Hinweise.GebCharakteristik)
        Assert.AreEqual(10, h10.Typ)
        Assert.AreEqual(1, h10.Typ2)

        'Verzehrtipps
        h10 = New wb_Hinweise(wb_Global.Hinweise.Verzehrtipps)
        Assert.AreEqual(10, h10.Typ)
        Assert.AreEqual(2, h10.Typ2)

        'Wissenswertes
        h10 = New wb_Hinweise(wb_Global.Hinweise.Wissenswertes)
        Assert.AreEqual(10, h10.Typ)
        Assert.AreEqual(3, h10.Typ2)

        'Deklarationsbezeichnung Rohstoff
        Dim h11 = New wb_Hinweise(wb_Global.Hinweise.DeklBezRohstoff)
        Assert.AreEqual(11, h11.Typ)
        Assert.AreEqual(0, h11.Typ2)

        'Deklarationsbezeichnung Rohstoff
        h11 = New wb_Hinweise(wb_Global.Hinweise.DeklBezRohstoffIntern)
        Assert.AreEqual(11, h11.Typ)
        Assert.AreEqual(1, h11.Typ2)

        'Messaging-System
        Dim h20 = New wb_Hinweise(wb_Global.Hinweise.MessageTextLinie)
        Assert.AreEqual(20, h20.Typ)
        Assert.AreEqual(0, h20.Typ2)

        'Messaging-System
        h20 = New wb_Hinweise(wb_Global.Hinweise.MessageTextUser)
        Assert.AreEqual(20, h20.Typ)
        Assert.AreEqual(1, h20.Typ2)

        'Update Nährwerte
        Dim h21 = New wb_Hinweise(wb_Global.Hinweise.NaehrwertUpdate)
        Assert.AreEqual(21, h21.Typ)
        Assert.AreEqual(0, h21.Typ2)

    End Sub

    <TestMethod()> Public Sub TestSchreibenLesen()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Objekt initialisieren (ohne Rohstoff-Nummer)
            Dim h As New wb_Hinweise(wb_Global.Hinweise.NaehrwertUpdate)
            'erstmaliges Schreiben in Hinweise2.H2_Memo
            h.ArtikelNr = 9999
            h.Memo = "TEST"
            Assert.IsTrue(h.Write())

            'Daten wieder lesen
            Assert.IsTrue(h.Read(9999))
            Assert.AreEqual("TEST", h.Memo)

            'Zweites Schreiben in Hinweise2.H2_Memo
            h.Memo = "TESTNEU"
            Assert.IsTrue(h.Write())
            'Daten wieder lesen
            Assert.IsTrue(h.Read(9999))
            Assert.AreEqual("TESTNEU", h.Memo)

            'wieder aufräumen
            Assert.IsTrue(h.Delete())
            'erneut lesen
            Assert.IsFalse(h.Read(9999))

            'Nicht existierenenden Datensatz löschen
            Assert.IsFalse(h.Delete())

        End If
    End Sub

    <TestMethod()> Public Sub TestSonderzeichen()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Objekt initialisieren (ohne Rohstoff-Nummer)
            Dim h As New wb_Hinweise(wb_Global.Hinweise.NaehrwertUpdate)

            'Sonderzeichen im Text
            h.ArtikelNr = 9999
            h.Memo = "TES'T"
            Assert.IsTrue(h.Write())

            'Daten wieder lesen
            Assert.IsTrue(h.Read(9999))
            Assert.AreEqual("TES'T", h.Memo)


        End If
    End Sub
End Class