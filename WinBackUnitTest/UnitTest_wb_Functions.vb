Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports WinBack.wb_Functions
Imports WinBack.wb_Global

<TestClass()> Public Class UnitTest_wb_Functions

    <TestMethod()> Public Sub Test_KeyToString()
        Dim s As String = ""

        Assert.IsTrue(KeyToString("A", s))
        Assert.AreEqual("A", s)
        Assert.IsTrue(KeyToString("A", s))
        Assert.AreEqual("AA", s)

        Assert.IsTrue(KeyToString(" ", s))
        Assert.AreEqual("AA ", s)
        Assert.IsTrue(KeyToString("!", s))
        Assert.AreEqual("AA !", s)
        Assert.IsFalse(KeyToString(".", s))

        Assert.IsTrue(KeyToString("#", s))
        Assert.AreEqual("AA !#", s)
        Assert.IsTrue(KeyToString("$", s))
        Assert.AreEqual("AA !#$", s)
        Assert.IsTrue(KeyToString("%", s))
        Assert.AreEqual("AA !#$%", s)
        Assert.IsTrue(KeyToString("&", s))
        Assert.AreEqual("AA !#$%&", s)
        Assert.IsTrue(KeyToString("Ä", s))
        Assert.AreEqual("AA !#$%&Ä", s)

        Assert.IsTrue(KeyToString("a", s))
        Assert.AreEqual("AA !#$%&Äa", s)
        Assert.IsTrue(KeyToString("z", s))
        Assert.AreEqual("AA !#$%&Äaz", s)

        Assert.IsTrue(KeyToString("ä", s))
        Assert.AreEqual("AA !#$%&Äazä", s)
        Assert.IsTrue(KeyToString("ü", s))
        Assert.AreEqual("AA !#$%&Äazäü", s)
        Assert.IsTrue(KeyToString("ö", s))
        Assert.AreEqual("AA !#$%&Äazäüö", s)

    End Sub
    <TestMethod()> Public Sub Test_IntToKomponType()
        Assert.AreEqual(KomponTypen.KO_TYPE_ARTIKEL, IntToKomponType(0))
        Assert.AreEqual(KomponTypen.KO_TYPE_AUTOKOMPONENTE, IntToKomponType(101))
        Assert.AreEqual(KomponTypen.KO_TYPE_HANDKOMPONENTE, IntToKomponType(102))
        Assert.AreEqual(KomponTypen.KO_TYPE_WASSERKOMPONENTE, IntToKomponType(103))
        Assert.AreEqual(KomponTypen.KO_TYPE_EISKOMPONENTE, IntToKomponType(104))
        Assert.AreEqual(KomponTypen.KO_TYPE_STUECK, IntToKomponType(105))
        Assert.AreEqual(KomponTypen.KO_TYPE_METER, IntToKomponType(106))

        Assert.AreEqual(KomponTypen.KO_TYPE_TEMPERATURERFASSUNG, IntToKomponType(111))
        Assert.AreEqual(KomponTypen.KO_TYPE_KNETER, IntToKomponType(118))
        Assert.AreEqual(KomponTypen.KO_TYPE_TEIGZETTEL, IntToKomponType(119))
        Assert.AreEqual(KomponTypen.KO_TYPE_KNETERREZEPT, IntToKomponType(128))

        Assert.AreEqual(KomponTypen.KO_TYPE_TEXTKOMPONENTE, IntToKomponType(121))
        Assert.AreEqual(KomponTypen.KO_TYPE_PRODUKTIONSSTUFE, IntToKomponType(122))
        Assert.AreEqual(KomponTypen.KO_TYPE_KESSEL, IntToKomponType(123))

        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_MEHL, IntToKomponType(1))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_WASSER, IntToKomponType(3))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_TEMP, IntToKomponType(4))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_DIGITAL, IntToKomponType(10))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_ANALOG, IntToKomponType(11))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_WARTEN, IntToKomponType(16))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_RUEHREN, IntToKomponType(17))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_ZUGABE, IntToKomponType(19))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_STATUS, IntToKomponType(20))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_TEXT, IntToKomponType(21))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE, IntToKomponType(22))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_REZEPT_START, IntToKomponType(30))
        Assert.AreEqual(KomponTypen.KO_TYPE_SAUER_REPEAT, IntToKomponType(31))

        Assert.AreEqual(KomponTypen.KO_ZEILE_ARTIKEL, IntToKomponType(-1))
        Assert.AreEqual(KomponTypen.KO_ZEILE_CHARGE, IntToKomponType(-2))

        Assert.AreEqual(KomponTypen.KO_TYPE_UNDEFINED, IntToKomponType(999))
    End Sub

    <TestMethod()> Public Sub Test_AllergenToString()
        Assert.AreEqual("ERR", AllergenToString(AllergenInfo.ERR))
        Assert.AreEqual("ERR", AllergenToString(AllergenInfo.X))
        Assert.AreEqual("ERR", 10)

        Assert.AreEqual("C", AllergenToString(AllergenInfo.C))
        Assert.AreEqual("K", AllergenToString(AllergenInfo.K))
        Assert.AreEqual("N", AllergenToString(AllergenInfo.N))
        Assert.AreEqual("T", AllergenToString(AllergenInfo.T))
    End Sub

    <TestMethod()> Public Sub Test_StringToAllergen()
        Assert.AreEqual(StringtoAllergen("ERR"), AllergenInfo.ERR)
        Assert.AreEqual(StringtoAllergen("NotDefined"), AllergenInfo.ERR)
        Assert.AreEqual(StringtoAllergen("AnyString"), AllergenInfo.ERR)

        Assert.AreEqual(StringtoAllergen("X"), AllergenInfo.X)
        Assert.AreEqual(StringtoAllergen(""), AllergenInfo.X)

        Assert.AreEqual(StringtoAllergen("C"), AllergenInfo.C)
        Assert.AreEqual(StringtoAllergen("K"), AllergenInfo.K)
        Assert.AreEqual(StringtoAllergen("N"), AllergenInfo.N)
        Assert.AreEqual(StringtoAllergen("T"), AllergenInfo.T)
    End Sub

    <TestMethod()> Public Sub Test_FormatStr()
        Assert.AreEqual(" 22,000", FormatStr("22", 3, 3))
        Assert.AreEqual("-22,000", FormatStr("-22", 3, 3))
        Assert.AreEqual("1235", FormatStr("1234,5678", 4, 0))

        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Assert.AreEqual("1234.5678", FormatStr("1234.5678", 4, 4))
        Assert.AreEqual("12345678.0", FormatStr("1234,5678", 8, 1))
        Assert.AreEqual("1234.5678", FormatStr("1234,5678", 4, 4, "de-DE"))
        Assert.AreEqual("12345678", FormatStr("1234,5678", 8, 0))
        Assert.AreEqual("1235", FormatStr("1234,5678", 4, 0, "de-DE"))

        Assert.AreEqual("-", FormatStr("", 4, 0))
        Assert.AreEqual("-", FormatStr("", 4, 0, "de-DE"))
    End Sub

    <TestMethod()> Public Sub Test_bz2CompressFile()
        'TestFile erstellen
        Dim OutputFileName As String = My.Application.Info.DirectoryPath & "\Test_bz2Compress.txt"
        Dim bz2FileName As String = Path.GetDirectoryName(OutputFileName) & Path.GetFileNameWithoutExtension(OutputFileName)

        Dim lines() As String = {"First line", "Second line", "Third line"}
        Using outputFile As New StreamWriter(OutputFileName)
            For Each line As String In lines
                outputFile.WriteLine(line)
            Next
        End Using
        'TestFile komprimieren
        Assert.IsTrue(bz2CompressFile(OutputFileName, bz2FileName))

        'TestFile vorhanden prüfen
        Assert.IsTrue(My.Computer.FileSystem.FileExists(bz2FileName))

        'Ergebnisfile löschen
        My.Computer.FileSystem.DeleteFile(OutputFileName)
        'Komprimieren einer nicht vorhandenen Datei -> Return False
        Assert.IsFalse(bz2CompressFile(OutputFileName, bz2FileName))
    End Sub
    <TestMethod()> Public Sub Test_bz2DeompressFile()
        ' Testfile aus Text_bz2CompressFile
        Dim OutputFileName As String = My.Application.Info.DirectoryPath & "\Test_bz2Compress.txt"
        Dim bz2FileName As String = Path.GetDirectoryName(OutputFileName) & Path.GetFileNameWithoutExtension(OutputFileName)

        'TestFile dekomprimieren
        Assert.IsTrue(bz2DecompressFile(bz2FileName, OutputFileName))
        'TestFile vorhanden prüfen
        Assert.IsTrue(My.Computer.FileSystem.FileExists(OutputFileName))

        'Input-File löschen
        My.Computer.FileSystem.DeleteFile(bz2FileName)
        'DeKomprimieren einer nicht vorhandenen Datei -> Return False
        Assert.IsFalse(bz2DecompressFile(bz2FileName, OutputFileName))

        'Ergebnisfile löschen (Aufräumen)
        My.Computer.FileSystem.DeleteFile(OutputFileName)
    End Sub

    <TestMethod()> Public Sub Test_SshShell()
        Dim Output As String
        Output = DoShell("herbst", "herbst", "172.16.17.5", "pwd")
        Assert.AreEqual("/home/herbst", Output)
    End Sub

    <TestMethod()> Public Sub Test_StrToDouble()
        Assert.AreEqual(0.1, StrToDouble("0.1"))
        Assert.AreEqual(0.1, StrToDouble("0,1"))
        Assert.AreEqual(1000.1, StrToDouble("1000,1"))
        Assert.AreNotEqual(1000.1, StrToDouble("1.000,1"))
        Assert.AreEqual(1000.1, StrToDouble("1000.1"))

        Assert.AreEqual(-1000.1, StrToDouble("-1000,1"))
        Assert.AreEqual(-1000.1, StrToDouble("-1000.1"))

        Assert.AreEqual(0.0, StrToDouble(""))
        Assert.AreEqual(0.0, StrToDouble("xxx"))

    End Sub
End Class