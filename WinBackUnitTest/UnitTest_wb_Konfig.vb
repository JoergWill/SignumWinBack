Imports WinBack
Imports WinBack.wb_Konfig

<TestClass()> Public Class UnitTest_wb_Konfig
    Dim winback As wb_Sql
    Dim IniFile As New WinBack.wb_IniFile

    <TestMethod()>
    Public Sub Test_IniFile()
        ' Test Ini-File lesen/schreiben
        Dim IniFile As New WinBack.wb_IniFile

        Dim bTest As Boolean
        Dim iTest As Integer
        Dim sTest, sLoremIpsum As String

        ' Test vorbereiten
        IniFile.SilentMode = True
        IniFile.DeleteIniFile()

        ' Test Read Boolean - Ini-File nicht vorhanden
        bTest = IniFile.ReadBool("Test", "TestBoolean", False)
        Assert.IsFalse(bTest)
        Assert.IsFalse(IniFile.ReadResult)
        bTest = IniFile.ReadBool("Test", "TestBoolean", True)
        Assert.IsTrue(bTest)
        Assert.IsFalse(IniFile.ReadResult)

        ' Test Write/Read Boolean 
        IniFile.WriteBool("Test", "TestBoolean", True)
        bTest = IniFile.ReadBool("Test", "TestBoolean", False)
        Assert.IsTrue(bTest)
        Assert.IsTrue(IniFile.ReadResult)
        IniFile.WriteBool("Test", "TestBoolean", False)
        bTest = IniFile.ReadBool("Test", "TestBoolean", True)
        Assert.IsFalse(bTest)
        Assert.IsTrue(IniFile.ReadResult)

        ' Test Read Boolean - Schlüssel nicht vorhanden
        bTest = IniFile.ReadBool("TestX", "TestBoolean", False)
        Assert.IsFalse(bTest)
        bTest = IniFile.ReadBool("TestX", "TestBoolean", True)
        Assert.IsTrue(bTest)

        ' Test Read Integer - Schlüssel nicht vorhanden
        iTest = IniFile.ReadInt("Test", "TestInteger")
        Equals(iTest = 0)
        iTest = IniFile.ReadInt("Test", "TestInteger", 99)
        Assert.AreEqual(iTest, 99)

        ' Test Write/Read Integer
        IniFile.WriteInt("Test", "TestInteger", -1234567)
        iTest = IniFile.ReadInt("Test", "TestInteger")
        Assert.AreEqual(iTest, -1234567)

        ' Test Read String - Schlüssel nicht vorhanden
        sTest = IniFile.ReadString("Test", "TestString")
        Equals(sTest = "")
        sTest = IniFile.ReadString("Test", "TestString", "LEER")
        Assert.AreEqual(sTest, "LEER")

        ' Test Read/Write String
        sLoremIpsum = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt " &
                     "ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo " &
                     "dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit " &
                     "amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt " &
                     "ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores " &
                     "et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
        IniFile.WriteString("Test", "TestString", sLoremIpsum)
        sTest = IniFile.ReadString("Test", "TestString")
        Assert.AreEqual(sTest, sLoremIpsum)

    End Sub

    <TestMethod()>
    Public Sub Test_SetGetLanguage()
        Dim Xlangague As String
        'aktuelle Einstellung zwischenspeichern
        Xlangague = IniFile.ReadString("winback", "Language", "de-DE")

        'Sprache Deutsch
        SetLanguage("de-DE")
        Assert.AreEqual("de-DE", GetLanguage)
        Assert.AreEqual("0", GetLanguageNr)

        'Sprache Ungarisch
        SetLanguage("hu-HU")
        Assert.AreEqual("hu-HU", GetLanguage)
        Assert.AreEqual("1", GetLanguageNr)

        'Sprache Niederländisch
        SetLanguage("nl-NL")
        Assert.AreEqual("nl-NL", GetLanguage)
        Assert.AreEqual("2", GetLanguageNr)

        'Sprache Englisch
        SetLanguage("en-US")
        Assert.AreEqual("en-US", GetLanguage)
        Assert.AreEqual("3", GetLanguageNr)

        'Sprache Portugisisch
        SetLanguage("pt-PT")
        Assert.AreEqual("pt-PT", GetLanguage)
        Assert.AreEqual("4", GetLanguageNr)

        'Sprache Slovenisch
        SetLanguage("sl-SL")
        Assert.AreEqual("sl-SL", GetLanguage)
        Assert.AreEqual("5", GetLanguageNr)

        'Sprache Russisch
        SetLanguage("ru-RU")
        Assert.AreEqual("ru-RU", GetLanguage)
        Assert.AreEqual("6", GetLanguageNr)

        'Sprache Französisch
        SetLanguage("fr-FR")
        Assert.AreEqual("fr-FR", GetLanguage)
        Assert.AreEqual("7", GetLanguageNr)

        'Sprache Spanisch
        SetLanguage("es-ES")
        Assert.AreEqual("es-ES", GetLanguage)
        Assert.AreEqual("8", GetLanguageNr)

        'aktuelle Einstellung zurückschreiben
        IniFile.WriteString("winback", "Language", Xlangague)
    End Sub
    <TestInitialize>
    Sub TestInitialize()
        'Datenbank Verbindung Einstellungen setzen
        '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
        wb_Konfig.SqlSetting("MySQL")
        winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)

        'Test-Einträge in Tabelle Texte schreiben (TextIndex, TextTyp,Sprache,Text,Timestamp)
        winback.sqlCommand("INSERT INTO Texte VALUES(9998,9999,0,'Test-0',0,'20160704120000')")
        winback.sqlCommand("INSERT INTO Texte VALUES(9998,9999,1,'Test-1',0,'20160704120000')")
        winback.sqlCommand("INSERT INTO Texte VALUES(9998,9999,2,'Test-2',0,'20160704120000')")
        winback.sqlCommand("INSERT INTO Texte VALUES(9998,9999,3,'Test-3',0,'20160704120000')")
        winback.sqlCommand("INSERT INTO Texte VALUES(9998,9999,4,'Test-4',0,'20160704120000')")

    End Sub

    <TestMethod()>
    Public Sub Test_TextFilter()

        'Tabelle Hash-Table aktualisieren
        LoadTexteTabelle(0)
        ' Erste Zahl (Texttyp), zweite Zahl (Textindex)
        Assert.AreEqual("Test-0", wb_Functions.TextFilter("@[9999,9998]XXXX"))

        'Tabelle Hash-Table aktualisieren
        LoadTexteTabelle(1)
        Assert.AreEqual("Test-1", wb_Functions.TextFilter("@[9999,9998]XXXX"))

        'Tabelle Hash-Table aktualisieren
        LoadTexteTabelle(2)
        Assert.AreEqual("Test-2", wb_Functions.TextFilter("@[9999,9998]XXXX"))

        'Tabelle Hash-Table aktualisieren
        LoadTexteTabelle(3)
        Assert.AreEqual("Test-3", wb_Functions.TextFilter("@[9999,9998]XXXX"))

        'Tabelle Hash-Table aktualisieren
        LoadTexteTabelle(4)
        Assert.AreEqual("Test-4", wb_Functions.TextFilter("@[9999,9998]XXXX"))

        'Text wird nicht gefunden
        Assert.AreEqual("Test-5", wb_Functions.TextFilter("@[9999,9995]Test-5"))
        Assert.AreEqual("Test-6", wb_Functions.TextFilter("Test-6"))
        'Länge kleiner 6 Zeichen
        Assert.AreEqual("@[T-7", wb_Functions.TextFilter("@[T-7"))

    End Sub

    <TestCleanup>
    Sub TestCleanup()
        'Datenbank wieder bereinigen
        winback.sqlCommand("DELETE FROM Texte WHERE T_Textindex='9998'")
    End Sub

End Class