Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Functions
Imports WinBack.wb_Global
Imports System.Text

<TestClass()>
Public Class UnitTest_Encoding_Conversions

    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    Private Function AreByteArraysEqual(arr1 As Byte(), arr2 As Byte()) As Boolean
        If arr1 Is Nothing AndAlso arr2 Is Nothing Then
            Return True
        End If
        If arr1 Is Nothing OrElse arr2 Is Nothing Then
            Return False
        End If
        If arr1.Length <> arr2.Length Then
            Return False
        End If
        For i As Integer = 0 To arr1.Length - 1
            If arr1(i) <> arr2(i) Then
                Return False
            End If
        Next
        Return True
    End Function

    <TestMethod()>
    Public Sub Test_MySqlToUtf8_And_UTF8toMySql_Conversion()
        Dim originalString_Latin15 As String = "Test € String with Umlauts ÄÖÜäöüß"
        Dim originalString_Cyrillic As String = "Тест на кирилица" ' Test in Cyrillic

        ' Simulate different CodePage settings in wb_GlobalSettings for testing
        Dim originalCodePageSetting As wb_Global.MySqlCodepage = wb_GlobalSettings.ConvertMySQL_CodePage

        ' Test Case 1: iso8859_15 (should effectively do no conversion for MySqlToUtf8, or correctly encode for UTF8toMySql)
        wb_GlobalSettings.ConvertMySQL_CodePage = wb_Global.MySqlCodepage.iso8859_15
        
        Dim toUtf8_Latin15_as_Latin15 As String = wb_Functions.MySqlToUtf8(originalString_Latin15)
        Assert.AreEqual(originalString_Latin15, toUtf8_Latin15_as_Latin15, "Test_MySqlToUtf8: Latin15 to UTF8 (via Latin15) should be no change.")
        
        Dim fromUtf8_Latin15_to_Latin15 As String = wb_Functions.UTF8toMySql(originalString_Latin15)
        Assert.AreEqual(originalString_Latin15, fromUtf8_Latin15_to_Latin15, "Test_UTF8toMySql: UTF8 (Latin15) to Latin15 should be no change.")

        ' Test Case 2: iso8859_5 (Cyrillic)
        ' We need a string that is valid in iso-8859-15 but represents Cyrillic when its bytes are interpreted as iso-8859-5
        ' Let's take known Cyrillic bytes and convert them TO iso-8859-15 representation FIRST.
        Dim encoding_iso8859_5 As Encoding = Encoding.GetEncoding("iso-8859-5")
        Dim encoding_iso8859_15 As Encoding = Encoding.GetEncoding("iso-8859-15")

        Dim cyrillicBytes As Byte() = encoding_iso8859_5.GetBytes(originalString_Cyrillic)
        Dim stringRepresentingCyrillicInLatin15 As String = encoding_iso8859_15.GetString(cyrillicBytes)

        wb_GlobalSettings.ConvertMySQL_CodePage = wb_Global.MySqlCodepage.iso8859_5
        
        ' Test MySqlToUtf8: input is stringRepresentingCyrillicInLatin15, should be converted to originalString_Cyrillic
        Dim toUtf8_Cyrillic_as_Latin15 As String = wb_Functions.MySqlToUtf8(stringRepresentingCyrillicInLatin15)
        Assert.AreEqual(originalString_Cyrillic, toUtf8_Cyrillic_as_Latin15, "Test_MySqlToUtf8: Latin15 (representing Cyrillic) to UTF8 (Cyrillic).")

        ' Test UTF8toMySql: input is originalString_Cyrillic, should be converted to stringRepresentingCyrillicInLatin15
        Dim fromUtf8_Cyrillic_to_Latin15 As String = wb_Functions.UTF8toMySql(originalString_Cyrillic)
        Assert.AreEqual(stringRepresentingCyrillicInLatin15, fromUtf8_Cyrillic_to_Latin15, "Test_UTF8toMySql: UTF8 (Cyrillic) to Latin15 (representing Cyrillic).")
        
        ' Test with a common Western European string that might lose info if converted through Cyrillic
        wb_GlobalSettings.ConvertMySQL_CodePage = wb_Global.MySqlCodepage.iso8859_5
        Dim euroSymbol As String = "€" ' Euro symbol is in Latin-15, not easily in Latin-1 or Cyrillic
        Dim latin15BytesEuro As Byte() = encoding_iso8859_15.GetBytes(euroSymbol) ' Bytes of € in Latin-15
        
        ' Represent these Latin-15 bytes as if they were iso-8859-5 string (might be garbage or map to some Cyrillic chars)
        Dim stringRepresentingEuroInLatin5 As String = encoding_iso8859_5.GetString(latin15BytesEuro)

        ' Now, convert this string (which is stringRepresentingEuroInLatin5) using MySqlToUtf8 with codepage iso8859_5
        ' The function's internal logic: Dim o15 As Byte() = Encoding_iso8859_15.GetBytes(Value)
        '                               Return Encoding_iso8859_5.GetString(o15)
        ' So if Value = stringRepresentingEuroInLatin5, its UTF-16 bytes are taken, converted to Latin-15 bytes,
        ' then those Latin-15 bytes are interpreted as a Latin-5 string.
        
        Dim actualOutput_MySqlToUtf8_Euro As String = wb_Functions.MySqlToUtf8(stringRepresentingEuroInLatin5)
        Dim expected_Bytes_for_MySqlToUtf8_Euro_Test As Byte() = encoding_iso8859_15.GetBytes(stringRepresentingEuroInLatin5) ' Value -> Latin15 bytes
        Dim expected_String_for_MySqlToUtf8_Euro_Test As String = encoding_iso8859_5.GetString(expected_Bytes_for_MySqlToUtf8_Euro_Test) ' Latin15 bytes -> Latin5 string
        Assert.AreEqual(expected_String_for_MySqlToUtf8_Euro_Test, actualOutput_MySqlToUtf8_Euro, "Test_MySqlToUtf8: String representing Euro in Latin5 through chain.")

        ' Restore original setting
        wb_GlobalSettings.ConvertMySQL_CodePage = originalCodePageSetting
    End Sub

End Class
