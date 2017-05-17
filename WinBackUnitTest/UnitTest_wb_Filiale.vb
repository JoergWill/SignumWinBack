Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_Filiale

    <TestInitialize>
    Sub TestInitialize()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMsSQL Then
            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_Konfig.SqlSetting("MySQL")
        End If
    End Sub

    <TestMethod()> Public Sub Test_FilialeIstProduktion()

        'Test mit leerem Array
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("9999"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion(""))

        'Test wenn Ms-Datenbank aktiv
        If My.Settings.TestMsSQL Then
            Assert.IsTrue(wb_Filiale.FilialeIstProduktion("1"))
            Assert.IsTrue(wb_Filiale.FilialeIstProduktion("2"))
        End If

        'Testdaten in Array-List laden
        wb_Filiale.AddFiliale("9999")

        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9999"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("9"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("1111"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("-10"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("abcd"))

        wb_Filiale.AddFiliale("9")
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9999"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("4"))

        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("4,9"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion("4,5"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9,9999"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("9,4"))

        wb_Filiale.AddFiliale("4")
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("4,9"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("4,5"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("5,9"))

        Assert.IsTrue(wb_Filiale.FilialeIstProduktion(",9"))
        Assert.IsTrue(wb_Filiale.FilialeIstProduktion("4,9,"))
        Assert.IsFalse(wb_Filiale.FilialeIstProduktion(""))

    End Sub

    <TestMethod()> Public Sub Test_SortimentIstProduktion()

        'Test mit leerem Array
        Assert.IsFalse(wb_Filiale.SortimentIstProduktion("9"))
        Assert.IsFalse(wb_Filiale.SortimentIstProduktion(""))

        'Test wenn Ms-Datenbank aktiv
        If My.Settings.TestMsSQL Then
            Assert.IsTrue(wb_Filiale.SortimentIstProduktion("1"))
            Assert.IsTrue(wb_Filiale.SortimentIstProduktion("2"))
        End If
    End Sub

End Class