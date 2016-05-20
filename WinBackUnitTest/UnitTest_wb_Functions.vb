Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Signum.OrgaSoft.AddIn.wb_Functions

<TestClass()> Public Class UnitTest_wb_Functions

    <TestMethod()> Public Sub Test_wb_Functions()
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
End Class