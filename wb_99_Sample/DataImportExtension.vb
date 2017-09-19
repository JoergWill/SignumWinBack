Option Strict On
Option Explicit On

Imports Signum.OrgaSoft.Extensibility
Imports Signum.OrgaSoft.FrameWork
Imports Signum.OrgaSoft.Common
Imports System.Windows.Forms
Imports System.IO

<Export(GetType(IExtension))>
<ExportMetadata("Description", "Test Import")>
Public Class DataImportExtension
    Implements IExtension
    Implements IDataImportPreProcessor

    Private oFactory As IFactoryService
    Private Const sImportTablename As String = "DateiImport87_90030"

#Region "IExtension"
    Public Property InfoContainer As Common.IInfoContainer Implements Extensibility.IExtension.InfoContainer

    Public Property ServiceProvider As Common.IOrgasoftServiceProvider Implements Extensibility.IExtension.ServiceProvider

    Public Sub Initialize() Implements IExtension.Initialize
        ' Meldet sich selbst als Dienst an
        ServiceProvider.AddService(GetType(IDataImportPreProcessor), Me)

        oFactory = TryCast(ServiceProvider.GetService(GetType(IFactoryService)), IFactoryService)
    End Sub
#End Region

#Region "IDataImport"
    ''' <summary>
    ''' Erzeugt WriteTable eine UserTable(True) oder direkt die Zwischentabelle(False)
    ''' Bei False können keine Vorgaben, Ersetzungen oder Zuordnungen in Orgasoft definiert werden
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property UserTable As Boolean Implements IDataImportPreProcessor.UserTable
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Liest eine Datei ein und liefert den Tabellennamen zurück
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function WriteTable(FileName As String) As String Implements IDataImportPreProcessor.WriteTable
        If UserTable Then
            Return "UserTableImportDaten"
        Else
            Dim oData As IData = oFactory.GetData

            If Not (oData.TableExists(Database.Main, sImportTablename)) Then
                oData.DropTable(Database.Main, sImportTablename)
                oData.ExecuteSQL(Database.Main, "CREATE TABLE " & sImportTablename & "(" &
                                                "[lfd] [int] NOT NULL," &
                                                "[Wechsel] [text] NULL," &
                                                "[Original] [text] NULL," &
                                                "[Dateiname] [text] NULL," &
                                                "[Artikel#0_ArtikelNr#0] [varchar](255) NULL," &
                                                "[GridForeColor] [varchar](50) NULL," &
                                                "[GridBackColor] [varchar](50) NULL)")

                Using oTable As DataTable = oData.OpenDataTable(Database.Main, "SELECT * FROM UserTableImportDaten", LockType.ReadOnly)
                    Using oNewTable As DataTable = oData.OpenDataTable(Database.Main, "SELECT * FROM " & sImportTablename, LockType.ReadWrite)
                        Dim sWechsel As String = String.Empty
                        Dim iWechsel As Integer = 0

                        For Each oRow As DataRow In oTable.Rows
                            Dim oNewRow As DataRow = oNewTable.NewRow

                            ' Prüfung ob ein neuer Datensatz begonnen hat
                            If sWechsel <> CType(oRow!TextString, String) Then
                                sWechsel = CType(oRow!TextString, String)
                                iWechsel += 1
                            End If

                            oNewRow!lfd = CType(oRow!lfd, Integer)
                            oNewRow!Wechsel = iWechsel
                            oNewRow!Original = CType(oRow!lfd, String) & ";" & CType(oRow!TextString, String)
                            oNewRow!Dateiname = "UserTableImportDaten"
                            oNewRow(4) = CType(oRow!TextString, String)

                            ' Zeilen einfärben
                            Select Case CType(oNewRow!lfd, Integer)
                                Case 4
                                    oNewRow!GridForeColor = "White"
                                    oNewRow!GridBackColor = "Blue"
                                Case 11
                                    oNewRow!GridBackColor = "LightGreen"
                            End Select

                            If oNewRow.RowState = DataRowState.Detached Then oNewTable.Rows.Add(oNewRow)
                        Next
                        oData.UpdateTable(oNewTable)
                    End Using
                End Using
            End If

            Return sImportTablename
        End If
    End Function

    ''' <summary>
    ''' Der Import ist beendet
    ''' </summary>
    ''' <param name="Errors">Liste der aufgetretenen Fehler</param>
    ''' <remarks></remarks>
    Public Sub OnImported(Errors As IList) Implements IDataImportPreProcessor.OnImported
        If Errors.Count = 0 Then
            MessageBox.Show("Der Import ist erfoglreich beendet worden!", "Datenimport-AddIn", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Der Import ist leider mit Fehler beendet worden!", "Datenimport-AddIn", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region

#Region "IOrgasoftService"
    Public ReadOnly Property ServiceName As String Implements IDataImportPreProcessor.ServiceName
        Get
            Return "Test import"
        End Get
    End Property
#End Region
End Class
