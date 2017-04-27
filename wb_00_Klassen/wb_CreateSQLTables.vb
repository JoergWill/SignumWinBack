''' <summary>
''' Routinen zum Erzeugen der WinBack-Tabellen in der
''' MSSQL-Datenbank
''' </summary>
Public Class wb_CreateSQLTables

    Public Shared Function DataBaseWinBack(ConString As String) As Boolean
        Try
            'Datenbank-Verbindung öffnen - SQL
            Dim WinBack As New wb_Sql(ConString, wb_Sql.dbType.msSql)

            'Datenbank WinBack erstellen
            WinBack.sqlCommand("IF EXISTS(SELECT * FROM sys.databases WHERE name='WinBack') " &
                                    "DROP DATABASE WinBack")
            WinBack.sqlCommand("CREATE DATABASE WinBack ON PRIMARY " &
                                    "(NAME = WinBack, " &
                                    "FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL12.SIGNUM\MSSQL\DATA\WinBack.mdf', " &
                                    "SIZE = 5MB, MAXSIZE = 100MB, FILEGROWTH = 10%) " &
                                    "LOG ON (NAME = WinBack_Log, " &
                                    "FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL12.SIGNUM\MSSQL\DATA\WinBackLog.ldf', " &
                                    "SIZE = 1MB, MAXSIZE = 5MB, FILEGROWTH = 10%)")
            Return True
            WinBack.Close()
            WinBack = Nothing
        Catch e As Exception
            Debug.Print(e.ToString)
            Return False
        End Try

    End Function
    Public Shared Function DataBaseWbDaten(ConString As String) As Boolean
        Try
            'Datenbank-Verbindung öffnen - SQL
            Dim WbDaten As New wb_Sql(ConString, wb_Sql.dbType.msSql)

            'Datenbank WbDaten erstellen
            WbDaten.sqlCommand("IF EXISTS(SELECT * FROM sys.databases WHERE name='WbDaten') " &
                                    "DROP DATABASE WbDaten")
            WbDaten.sqlCommand("CREATE DATABASE WbDaten ON PRIMARY " &
                                    "(NAME = WbDaten, " &
                                    "FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL12.SIGNUM\MSSQL\DATA\WbDaten.mdf', " &
                                    "SIZE = 5MB, MAXSIZE = 100MB, FILEGROWTH = 10%) " &
                                    "LOG ON (NAME = WbDaten_Log, " &
                                    "FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL12.SIGNUM\MSSQL\DATA\WbDatenLog.ldf', " &
                                    "SIZE = 1MB, MAXSIZE = 5MB, FILEGROWTH = 10%)")
            Return True
            WbDaten.Close()
            WbDaten = Nothing
        Catch e As Exception
            Debug.Print(e.ToString)
            Return False
        End Try

    End Function

    Public Shared Function Komponenten(ConString As String) As Boolean
        Try
            'Datenbank-Verbindung öffnen - SQL
            Dim WinBack As New wb_Sql(ConString, wb_Sql.dbType.msSql)

            'Tabelle Komponenten erstellen
            WinBack.sqlCommand("IF OBJECT_ID('Komponenten', 'U') IS NOT NULL DROP TABLE Komponenten;")
            WinBack.sqlCommand("CREATE TABLE Komponenten (" &
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
            WinBack.Close()
            WinBack = Nothing
        Catch e As Exception
            Debug.Print(e.ToString)
            Return False
        End Try
    End Function

    Public Shared Function Rezepte(ConString As String) As Boolean
        Try
            'Datenbank-Verbindung öffnen - SQL
            Dim WinBack As New wb_Sql(ConString, wb_Sql.dbType.msSql)

            'Tabelle Komponenten erstellen
            WinBack.sqlCommand("IF OBJECT_ID('Rezepe', 'U') IS NOT NULL DROP TABLE Rezepte;")
            WinBack.sqlCommand("CREATE TABLE Rezepte (" &
                               "RZ_Nr INT Not NULL Default 0," &
                               "RZ_Variante_Nr SMALLINT Not NULL Default 0," &
                               "RZ_Nr_AlNum VARCHAR(8) Default NULL," &
                               "RZ_Bezeichnung VARCHAR(60) Default NULL," &
                               "RZ_Gewicht VARCHAR(30) DEFAULT '0,0'," &
                               "RZ_Kommentar VARCHAR(30) Default NULL," &
                               "RZ_Kurzname VARCHAR(16) Default NULL," &
                               "RZ_Matchcode VARCHAR(10) Default NULL," &
                               "RZ_Type VARCHAR(1) Default NULL," &
                               "RZ_Charge_Opt VARCHAR(30) Default NULL," &
                               "RZ_Charge_Min VARCHAR(30) Default NULL," &
                               "RZ_Charge_Max VARCHAR(30) Default NULL," &
                               "RZ_Aenderung_Datum DATETIME Default NULL," &
                               "RZ_Aenderung_User INT Default -1," &
                               "RZ_Aenderung_Name VARCHAR(24) Default NULL," &
                               "RZ_Aenderung_Nr INT Default NULL," &
                               "RZ_Teigtemperatur VARCHAR(10) Default NULL," &
                               "RZ_Kneterkennlinie INT Default NULL," &
                               "RZ_Verarbeitungshinweise VARCHAR(100) Default NULL," &
                               "RZ_Liniengruppe TINYINT Default NULL," &
                               "RZ_Gruppe INT Default 0," &
                               "KA_Gruppe INT Default 0," &
                               "RZ_Timestamp TIMESTAMP," &
                               "PRIMARY KEY (RZ_Nr, RZ_Variante_Nr))")

            Return True
            WinBack.Close()
            WinBack = Nothing
        Catch e As Exception
            Debug.Print(e.ToString)
            Return False
        End Try
    End Function

    Public Shared Function His_Rezepte(ConString As String) As Boolean
        Try
            'Datenbank-Verbindung öffnen - SQL
            Dim WbDaten As New wb_Sql(ConString, wb_Sql.dbType.msSql)

            'Tabelle Komponenten erstellen
            WbDaten.sqlCommand("If OBJECT_ID('His_Rezepte', 'U') IS NOT NULL DROP TABLE His_Rezepte;")
            WbDaten.sqlCommand("CREATE TABLE His_Rezepte (" &
                               "H_RZ_Nr INT Not NULL Default 0," &
                               "H_RZ_Variante_Nr INT Not NULL Default 0," &
                               "H_RZ_Aenderung_Nr INT Not NULL Default 0," &
                               "H_RZ_Aenderung_Datum DATETIME Default NULL," &
                               "H_RZ_Aenderung_User INT Default -1," &
                               "H_RZ_Aenderung_Name VARCHAR(24) Default NULL," &
                               "H_RZ_Nr_AlNum VARCHAR(8) Default NULL," &
                               "H_RZ_Bezeichnung VARCHAR(30) Default NULL," &
                               "H_RZ_Gewicht VARCHAR(30) Default 0," &
                               "H_RZ_Kommentar VARCHAR(30) Default NULL," &
                               "H_RZ_Kurzname VARCHAR(16) Default NULL," &
                               "H_RZ_Matchcode VARCHAR(10) Default NULL," &
                               "H_RZ_Type VARCHAR(1) Default NULL," &
                               "H_RZ_Teigtemperatur VARCHAR(10) Default NULL," &
                               "H_RZ_Kneterkennlinie INT Default NULL," &
                               "H_RZ_Verarbeitungshinweise VARCHAR(100) Default NULL," &
                               "H_RZ_Liniengruppe INT Default NULL," &
                               "H_RZ_Gruppe INT Default NULL," &
                               "H_KA_Gruppe INT Default NULL," &
                               "H_RZ_Timestamp TIMESTAMP," &
                               "PRIMARY KEY (H_RZ_Nr, H_RZ_Variante_Nr, H_RZ_Aenderung_Nr))")

            Return True
            WbDaten.Close()
            WbDaten = Nothing
        Catch e As Exception
            Debug.Print(e.ToString)
            Return False
        End Try
    End Function

End Class
