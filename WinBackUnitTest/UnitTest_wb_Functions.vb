﻿Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports WinBack
Imports WinBack.wb_Functions
Imports WinBack.wb_Global

<TestClass()> Public Class UnitTest_wb_Functions

    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

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
        Assert.AreEqual(KomponTypen.KO_ZEILE_REZEPT, IntToKomponType(-2))
        Assert.AreEqual(KomponTypen.KO_ZEILE_KOMPONENTE, IntToKomponType(-3))

        Assert.AreEqual(KomponTypen.KO_TYPE_UNDEFINED, IntToKomponType(999))
    End Sub
    <TestMethod()> Public Sub Test_KomponTypeToInt()
        Assert.AreEqual(0, KomponTypeToInt(KomponTypen.KO_TYPE_ARTIKEL))
        Assert.AreEqual(102, KomponTypeToInt(KomponTypen.KO_TYPE_HANDKOMPONENTE))

    End Sub
    <TestMethod()> Public Sub Test_AllergenToString()
        Assert.AreEqual("ERR", AllergenToString(AllergenInfo.ERR))
        Assert.AreEqual("ERR", AllergenToString(10))

        Assert.AreEqual("N", AllergenToString(AllergenInfo.X))
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

    <TestMethod()> Public Sub Test_StringTokt301Gruppe()
        Assert.AreEqual(wb_Global.ktTyp301Gruppen.Big4, StringTokt301Gruppe("Big 4"))
        Assert.AreEqual(wb_Global.ktTyp301Gruppen.Big4, StringTokt301Gruppe("BIG 4"))
        Assert.AreEqual(wb_Global.ktTyp301Gruppen.Big4, StringTokt301Gruppe("big 4"))
    End Sub


    <TestMethod()> Public Sub Test_FormatStr()
        Assert.AreEqual(" 22,000", FormatStr("22", 3, 3))
        Assert.AreEqual("-22,000", FormatStr("-22", 3, 3))

        Assert.AreEqual("22,000", FormatStr("22", 3))
        Assert.AreEqual("-22,000", FormatStr("-22", 3))

        Assert.AreEqual("1235", FormatStr("1234,5678", 0, 4))
        Assert.AreEqual("1235", FormatStr("1234,5678", 0))

        'ACHTUNG UMSTELLUNG AUF US-ENGLISCH
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Assert.AreEqual("1234.5678", FormatStr("1234.5678", 4, 4))
        Assert.AreEqual("12345678.0", FormatStr("1234,5678", 1, 8))
        Assert.AreEqual("1234.5678", FormatStr("1234,5678", 4, 4, "de-DE"))
        Assert.AreEqual("12345678", FormatStr("1234,5678", 0, 8))
        Assert.AreEqual("1235", FormatStr("1234,5678", 0, 4, "de-DE"))

        Assert.AreEqual("-", FormatStr("", 0, 4))
        Assert.AreEqual("-", FormatStr("", 0, 4, "de-DE"))

        'ACHTUNG UMSTELLUNG AUF DE-DEUTSCH
        Thread.CurrentThread.CurrentCulture = New CultureInfo("de-DE")
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
        Output = ExecSSH("herbst", "herbst", "172.16.17.4", "pwd")
        Assert.AreEqual("/home/herbst" & vbLf, Output)
    End Sub

    <TestMethod()> Public Sub Test_StrToDouble()
        Assert.AreEqual(0.1, StrToDouble("0.1"))
        Assert.AreEqual(0.1, StrToDouble("0,1"))
        Assert.AreEqual(0.6, StrToDouble("0,6"))

        Assert.AreEqual(0.6, StrToDouble("0,6 kg"))
        Assert.AreEqual(0.6, StrToDouble("0,6kg"))

        Assert.AreEqual(0.6, StrToDouble("0,6%"))
        Assert.AreEqual(0.6, StrToDouble("0,6 %"))

        Assert.AreEqual(0.6, StrToDouble("0,6°C"))
        Assert.AreEqual(0.6, StrToDouble("0,6 °C"))

        Assert.AreEqual(1000.1, StrToDouble("1000,1"))
        Assert.AreNotEqual(1000.1, StrToDouble("1.000,1"))
        Assert.AreEqual(1000.1, StrToDouble("1000.1"))

        Assert.AreEqual(-1000.1, StrToDouble("-1000,1"))
        Assert.AreEqual(-1000.1, StrToDouble("-1000.1"))

        Assert.AreEqual(0.0, StrToDouble(""))
        Assert.AreEqual(0.0, StrToDouble(Nothing))
        Assert.AreEqual(0.0, StrToDouble("xxx"))

    End Sub

    <TestMethod()> Public Sub Test_StrToInt()
        Assert.AreEqual(0, StrToInt("x"))
        Assert.AreEqual(0, StrToInt(""))
        Assert.AreEqual(100, StrToInt("100"))
        Assert.AreEqual(100, StrToInt("100,00"))
    End Sub

    <TestMethod()> Public Sub Test_XRemoveSonder()
        Assert.AreEqual("Weizenmehl", wb_Functions.XRemoveSonderZeichen("{Weizenmehl}", True))
        Assert.AreEqual("{Weizenmehl}", wb_Functions.XRemoveSonderZeichen("{Weizenmehl}", False))
    End Sub

    <TestMethod()> Public Sub Test_TypeIstSollwert()
        Assert.IsTrue(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE, 1))
        Assert.IsTrue(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE, 1))
        Assert.IsTrue(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE, 1))
        Assert.IsTrue(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 1))

        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 2))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 3))

        Assert.IsTrue(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE, 1))
        Assert.IsTrue(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL, 1))

        Assert.IsTrue(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER, 2))

        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_ARTIKEL, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_ARTIKEL, 2))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_KESSEL, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_KNETER, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_METER, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_STUECK, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE, 1))

        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_ANALOG, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_DIGITAL, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_REPEAT, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_REZEPT_START, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_RUEHREN, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_STATUS, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_TEMP, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_TEXT, 1))
        Assert.IsFalse(TypeIstSollMenge(wb_Global.KomponTypen.KO_TYPE_SAUER_WARTEN, 1))

    End Sub

    <TestMethod()> Public Sub Test_DoubleToXString()
        Assert.AreEqual("10.1", wb_Functions.DoubleToXString(10.1))
        Assert.AreEqual("0", wb_Functions.DoubleToXString(0))
        Assert.AreEqual("10", wb_Functions.DoubleToXString(10))
        Assert.AreEqual("-10", wb_Functions.DoubleToXString(-10))
    End Sub

    <TestMethod()> Public Sub Test_UsDateToString()
        Assert.AreEqual(#11/08/2017#, wb_Functions.ConvertUSDateStringToDate("20171108"))
    End Sub

    <TestMethod()> Public Sub Test_UnixTimeToDate()
        Assert.AreEqual(#05/06/2022 15:52:44#, wb_Functions.UnixToTime("1651845164"))
        Assert.AreEqual("1651845164", wb_Functions.TimeToUnix(#05/06/2022 15:52:44#))
    End Sub

    ''' <summary>
    ''' Dieser Test funktioniert momentan NUR mit einer MySQL-3.2-Datenbank
    '''     Mit dem Windows-MySQL-Server(5.02) wird ein anderer String zurückgegeben !!!
    '''     Vermutlich gibt es hier auch Probleme mit Umlauten 
    ''' </summary>
    <TestMethod()> Public Sub Test_MySqlToUtf8()

        ''NUR Für WinBack Version 3.23
        'If wb_GlobalSettings.WinBackMySqlServerVersion = "3.23.48-Max" Then
        '    'Test utf-8 nach MySql
        '    Dim x As String = wb_Functions.UTF8toMySql("Статус")
        '    Assert.AreEqual("ÁâÐâãá", x)
        '    Dim s As String = wb_Functions.MySqlToUtf8(x)
        '    Assert.AreEqual("Статус", s)

        '    'Datensatz aus MySQL lesen - Texte 1/24/6
        '    wb_GlobalSettings.WinBackDBType = wb_Sql.dbType.mySql
        '    'Datenbank-Verbindung öffnen - MySQL
        '    Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        '    'Daten aus Tabelle Texte lesen (Text: Status - статус)
        '    If winback.sqlSelect("SELECT * FROM Texte WHERE T_Typ=1 AND T_TextIndex=24 AND T_Sprache=6") Then
        '        If winback.Read Then
        '            Dim sString As String = winback.sField("T_Text")
        '            Assert.AreEqual("ÁâÐâãá", sString)

        '            Dim xString As String = wb_Functions.MySqlToUtf8(sString)
        '            Assert.AreEqual("Статус", xString)
        '        End If
        '    End If

        '    winback.CloseRead()
        'End If
    End Sub

    <TestMethod()> Public Sub Test_CompareVersion()
        'Vompare Version liefert True zurück, wenn die alte Version (Parameter 1) älter ist, als die neue Version (Parameter 2)
        'also ein Update möglich ist...
        Assert.AreEqual(wb_Global.CompareVersionResult.VersionUpdate, wb_Functions.CompareVersion("1.0.0", "2.0.0"))
        Assert.AreEqual(wb_Global.CompareVersionResult.VersionUpdate, wb_Functions.CompareVersion("1.1.0", "2.0.0"))
        Assert.AreEqual(wb_Global.CompareVersionResult.VersionUpdate, wb_Functions.CompareVersion("1.0.1", "2.0.0"))
        Assert.AreEqual(wb_Global.CompareVersionResult.VersionUpdate, wb_Functions.CompareVersion("1.0.xxx", "2.0.0"))
        Assert.AreEqual(wb_Global.CompareVersionResult.VersionUpdate, wb_Functions.CompareVersion("1.xxx.0", "2.0.0"))
        Assert.AreEqual(wb_Global.CompareVersionResult.Err, wb_Functions.CompareVersion("102", "2.0.0"))
        Assert.AreEqual(wb_Global.CompareVersionResult.Err, wb_Functions.CompareVersion("xxx.1.02", "2.0.0"))

        Assert.AreEqual(wb_Global.CompareVersionResult.NoUpdate, wb_Functions.CompareVersion("1.0.0", "1.0.0"))
        Assert.AreEqual(wb_Global.CompareVersionResult.NoUpdate, wb_Functions.CompareVersion("1.0.1", "1.0.0"))
        Assert.AreEqual(wb_Global.CompareVersionResult.MinorUpdate, wb_Functions.CompareVersion("1.0.0", "1.0.1"))

        Assert.AreEqual(wb_Global.CompareVersionResult.MinorUpdate, wb_Functions.CompareVersion("1.2.0", "1.2.1"))
        Assert.AreEqual(wb_Global.CompareVersionResult.MinorUpdate, wb_Functions.CompareVersion("1.1.0", "1.2.0"))
    End Sub

    <TestMethod()> Public Sub Test_GetProcessList()
        Assert.AreEqual(False, wb_Functions.GetProcessRunning("XXX"))
    End Sub

    <TestMethod()> Public Sub Test_RegKeyNameExists()
        'Nicht ausführen wenn automatischer Test läuft 2022.06.02/JW
        'Assert.AreEqual(True, wb_Functions.RegKeyNameExists(Microsoft.Win32.RegistryHive.CurrentUser, "Software\Signum GmbH Darmstadt\OrgaSoft.NET", "CompaniesHeight"))
    End Sub

    <TestMethod()> Public Sub Test_FormatTimeStr()
        Assert.AreEqual("00:00:00", wb_Functions.FormatTimeStr("00:"))
        Assert.AreEqual("01:00:00", wb_Functions.FormatTimeStr("01:"))
        Assert.AreEqual("02:00:00", wb_Functions.FormatTimeStr("02"))

        Assert.AreEqual("00:00:00", wb_Functions.FormatTimeStr("00:00"))
        Assert.AreEqual("01:00:00", wb_Functions.FormatTimeStr("01:00"))
        Assert.AreEqual("10:00:00", wb_Functions.FormatTimeStr("10:00"))
        Assert.AreEqual("04:00:00", wb_Functions.FormatTimeStr("04:00"))

        Assert.AreEqual("04:00:00", wb_Functions.FormatTimeStr("04:00:00"))
        Assert.AreEqual("04:00:00", wb_Functions.FormatTimeStr("03:60:00"))
        Assert.AreEqual("01:00:00", wb_Functions.FormatTimeStr("xx:60:00"))

    End Sub

    <TestMethod()> Public Sub Test_TypeIstTeigTempSollwert()
        'in der Default-DB ist Komponente 111 die TTS-Erfassung und Komponente 1186 die Kneter-Teigtemp-Messung
        Assert.IsTrue(wb_Functions.TypeIstTeigTemperaturSollwert(111))
        Assert.IsTrue(wb_Functions.TypeIstTeigTemperaturSollwert(1186))
        Assert.IsFalse(wb_Functions.TypeIstTeigTemperaturSollwert(204))

    End Sub

    <TestMethod()> Public Sub Test_CheckOrgaBackArtikelGruppe()
        Assert.IsTrue(wb_Functions.CheckOrgaBackArtikelGruppe("10", "10"))
        Assert.IsTrue(wb_Functions.CheckOrgaBackArtikelGruppe("20", "20"))
        Assert.IsFalse(wb_Functions.CheckOrgaBackArtikelGruppe("10", "20"))
        Assert.IsFalse(wb_Functions.CheckOrgaBackArtikelGruppe("20", "10"))

        Assert.IsTrue(wb_Functions.CheckOrgaBackArtikelGruppe("20", "2[0-9]|3[0-5]"))
        Assert.IsFalse(wb_Functions.CheckOrgaBackArtikelGruppe("10", "2[0-9]|3[0-5]"))

        Assert.IsTrue(wb_Functions.CheckOrgaBackArtikelGruppe("21", "2[0-9]|3[0-5]"))
        Assert.IsTrue(wb_Functions.CheckOrgaBackArtikelGruppe("22", "2[0-9]|3[0-5]"))
        Assert.IsTrue(wb_Functions.CheckOrgaBackArtikelGruppe("23", "2[0-9]|3[0-5]"))
        Assert.IsTrue(wb_Functions.CheckOrgaBackArtikelGruppe("24", "2[0-9]|3[0-5]"))

        Assert.IsFalse(wb_Functions.CheckOrgaBackArtikelGruppe("-1", "2[0-9]|3[0-5]"))
        Assert.IsFalse(wb_Functions.CheckOrgaBackArtikelGruppe("A", "2[0-9]|3[0-5]"))
        Assert.IsFalse(wb_Functions.CheckOrgaBackArtikelGruppe("", "2[0-9]|3[0-5]"))

    End Sub

    <TestMethod()> Public Sub Test_LevenshteinDistance()
        Assert.IsTrue(wb_Functions.LevenshteinDistance("test", "TEST") = 0)
        Assert.IsTrue(wb_Functions.LevenshteinDistance("testa", "TEST") = 1)
        Assert.IsTrue(wb_Functions.LevenshteinDistance("testab", "TEST") = 2)
    End Sub

    <TestMethod()> Public Sub Test_FuzzyStringMatch()
        Dim fsm As Integer
        Dim s1 As String
        Dim s2 As String

        s1 = "Emulgator Sojalecithin"
        s2 = "Emulgator: (Sojalezitin)"
        fsm = wb_Functions.FuzzyStringMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)

        s1 = "Emulgator Sojalecithin"
        s2 = "Emulgator: (Weizenstärke)"
        fsm = wb_Functions.FuzzyStringMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)

        s1 = "Emulgator Sojalecithin"
        s2 = "Soja-Milch"
        fsm = wb_Functions.FuzzyStringMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)

        s1 = "Weizenmehl"
        s2 = "Weizen"
        fsm = wb_Functions.FuzzyStringMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)

        s1 = "Weizenmehl"
        s2 = "Roggenmehl"
        fsm = wb_Functions.FuzzyStringMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)
    End Sub

    <TestMethod()> Public Sub Test_SoundExString()
        Dim s1 As String
        Dim s2 As String
        Dim fsm As Boolean

        s1 = "Emulgator Sojalecithin"
        s2 = "Emulgator: (Sojalezitin)"
        fsm = wb_Functions.SoundExMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm.ToString)

        s1 = "Emulgator Sojalecithin"
        s2 = "Emulgator: (Weizenstärke)"
        fsm = wb_Functions.SoundExMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)

        s1 = "Emulgator Sojalecithin"
        s2 = "Soja-Milch"
        fsm = wb_Functions.SoundExMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)

        s1 = "Weizenmehl"
        s2 = "Weizen"
        fsm = wb_Functions.SoundExMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)

        s1 = "Weizenmehl"
        s2 = "Roggenmehl"
        fsm = wb_Functions.SoundExMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)

        s1 = "Weizenmehl"
        s2 = "Weicenmehl"
        fsm = wb_Functions.SoundExMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)

        s1 = "Weizenmehl"
        s2 = "Wejzenmehl"
        fsm = wb_Functions.SoundExMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)

        s1 = "Weizenmehl"
        s2 = "Wejzenmeehl"
        fsm = wb_Functions.SoundExMatch(s1, s2)
        Debug.Print("Fuzzy-String-Match Test-1 " & s1 & "/" & s2 & " = " & fsm)
    End Sub

End Class