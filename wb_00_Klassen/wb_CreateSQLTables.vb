'---------------------------------------------------------
'19.05.2016/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Routinen zum Erzeugen der WinBack-Tabellen in der
'MSSQL-Datenbank

Public Class wb_CreateSQLTables

    Public Shared Function DataBaseWinBack(ConString As String) As Boolean
        'Datenbank-Verbindung öffnen - SQL
        Dim OrgasoftMain As New wb_Sql(ConString, wb_Sql.dbType.msSql)

        'Datenbank WinBack erstellen
        OrgasoftMain.sqlCommand("IF EXISTS(SELECT * FROM sys.databases WHERE name='WinBack') " &
                                "DROP DATABASE WinBack")
        OrgasoftMain.sqlCommand("CREATE DATABASE WinBack ON PRIMARY " &
                                "(NAME = WinBack, " &
                                "FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL12.SIGNUM\MSSQL\DATA\WinBack.mdf', " &
                                "SIZE = 5MB, MAXSIZE = 100MB, FILEGROWTH = 10%) " &
                                "LOG ON (NAME = WinBack_Log, " &
                                "FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL12.SIGNUM\MSSQL\DATA\WinBackLog.ldf', " &
                                "SIZE = 1MB, MAXSIZE = 5MB, FILEGROWTH = 10%)")
        Return True
        OrgasoftMain.Close()
        OrgasoftMain = Nothing
        'Catch
        'Return False
        'End Try

    End Function

    Public Shared Function Komponenten(ConString As String) As Boolean
        Try
            'Datenbank-Verbindung öffnen - SQL
            Dim OrgasoftMain As New wb_Sql(ConString, wb_Sql.dbType.msSql)

            'Tabelle Komponenten erstellen
            OrgasoftMain.sqlCommand("IF OBJECT_ID('Komponenten', 'U') IS NOT NULL DROP TABLE Komponenten;")
            OrgasoftMain.sqlCommand("CREATE TABLE Komponenten (" &
                                "KO_Nr INT NOT NULL DEFAULT 0," &
                                "KO_Type TINYINT Not NULL DEFAULT 0," &
                                "KO_Bezeichnung VARCHAR(60) NULL Default NULL," &
                                "KO_Kommentar VARCHAR(50) NULL DEFAULT NULL," &
                                "KO_Nr_AlNum VARCHAR(16) NULL DEFAULT NULL," &
                                "KO_Temp_Korr SMALLINT NULL DEFAULT 0," &
                                "KA_Nr INT NULL DEFAULT 0," &
                                "KA_Kurzname VARCHAR(16) NULL DEFAULT NULL," &
                                "KA_Matchcode VARCHAR(10) NULL DEFAULT NULL," &
                                "KA_Art TINYINT NULL DEFAULT 0," &
                                "KA_Artikel_Typ SMALLINT NULL DEFAULT 0," &
                                "KA_RZ_Nr INT NULL DEFAULT 0," &
                                "KA_Lagerort VARCHAR(16) NULL DEFAULT NULL," &
                                "KA_Prod_Linie TINYINT NULL DEFAULT 0," &
                                "KA_Stueckgewicht VARCHAR(20) NULL DEFAULT NULL," &
                                "KA_Charge_Opt VARCHAR(30) NULL DEFAULT NULL," &
                                "KA_Charge_Min VARCHAR(30) NULL DEFAULT NULL," &
                                "KA_Charge_Max VARCHAR(30) NULL DEFAULT NULL," &
                                "KA_Charge_Opt_kg VARCHAR(30) NULL DEFAULT NULL," &
                                "KA_Charge_Min_kg VARCHAR(30) NULL DEFAULT NULL," &
                                "KA_Charge_Max_kg VARCHAR(30) NULL DEFAULT NULL," &
                                "KA_RS_veraenderbar CHAR(1) NULL DEFAULT NULL," &
                                "KA_RS_abh_von_RZ_Menge CHAR(1) NULL DEFAULT NULL," &
                                "KA_RS_aendert_WasMenge CHAR(1) NULL DEFAULT NULL," &
                                "KA_zaehlt_zu_RZ_Gesamtmenge CHAR(1) NULL DEFAULT NULL," &
                                "KA_spez_WKap VARCHAR(30) NULL DEFAULT NULL," &
                                "KA_alternativ_RS VARCHAR(16) NULL DEFAULT NULL," &
                                "KA_Verarbeitungshinweise VARCHAR(100) NULL DEFAULT NULL," &
                                "KA_aktiv SMALLINT NULL DEFAULT 1," &
                                "KA_Preis VARCHAR(20) NULL DEFAULT NULL," &
                                "KA_PreisEinheit SMALLINT NULL DEFAULT 0," &
                                "KA_Grp1 INT NULL DEFAULT 0," &
                                "KA_Grp2 INT NULL DEFAULT 0," &
                                "KA_Timestamp TIMESTAMP Not NULL," &
                                "PRIMARY KEY (KO_Nr));")

            Return True
            OrgasoftMain.Close()
            OrgasoftMain = Nothing
        Catch
            Return False
        End Try
    End Function

End Class
