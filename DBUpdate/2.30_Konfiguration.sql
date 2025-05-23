USE winback;
ALTER TABLE `Konfiguration`	ADD COLUMN `KF_Id2` INT(10) NULL DEFAULT NULL AFTER `KF_Kommentar`;
ALTER TABLE `Konfiguration`	ADD COLUMN `KF_Id1` INT(10) NULL DEFAULT NULL AFTER `KF_Kommentar`;
DELETE FROM Hinweise2 WHERE H2_Typ=5

UPDATE Konfiguration SET KF_Id2=0 ;

INSERT IGNORE INTO Hinweise2 VALUES (5,0,0,99,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Alte Eintr�ge','','',20171213151752);
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='BCTypenNeu';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='NowDifferenz';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='Perl';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='ServerMainThread';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='KO_Nr_Kneten_I';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='KO_Nr_Kneten_II';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='MultiMehlVariante';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='Verwenden_DELX';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='vts__local_anz_st_bcs';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='vts__max_st_beh_bc_offs';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='vts__max_st_mpx_beh_bc_offs';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='vts__st_mehl_bc_offs';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=99 WHERE KF_Tag='vts__st_wasser_bc_offs';
INSERT IGNORE INTO Hinweise2 VALUES (5,0,1,99,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Alte Eintr�ge','','<h1>Alte Daten</h1><p>Diese Konfiguration ist jetzt in den User-Rechten hinterlegt</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=99 WHERE KF_Tag='AendernEisMenge';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=99 WHERE KF_Tag='AendernWasserMenge';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=99 WHERE KF_Tag='FehlerArtikel_loeschen';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=99 WHERE KF_Tag='HandWaagen';

INSERT IGNORE INTO Hinweise2 VALUES (5,0,0,98,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Programm-Konstanten PPS','','<h1>Programm-Konstanten</h1><p>Festwerte WinBack-PPS</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='ShutdownCmd';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='TermKommando';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='Simulation';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='ClientTimeout';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='ServerTimeout';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='ServerTimeout';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='UpdateRateVH';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='LogGB';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='VWLog_Aktiv';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='writeln_log_client_nr';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='writeln_log_level_client';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='writeln_log_level_server';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='UpdateTimer';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=98 WHERE KF_Tag='UpdTicker';

INSERT IGNORE INTO Hinweise2 VALUES (5,0,1,98,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Programm-Konstanten PPS','','<h1>Programm-Konstanten</h1><p>Festwerte WinBack-PPS</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=98 WHERE KF_Tag='AutoLogout';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=98 WHERE KF_Tag='ClickIgnorierZeit';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=98 WHERE KF_Tag='DatenbankVersion';
INSERT IGNORE INTO Hinweise2 VALUES (5,0,2,98,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Programm-Konstanten PPS','','<h1>Programm-Konstanten</h1><p>Festwerte WinBack Import Produktionsliste</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=98 WHERE KF_Tag='BakeProdListe';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=98 WHERE KF_Tag='BakeSubDir';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=98 WHERE KF_Tag='ProdImportProg';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=98 WHERE KF_Tag='NeuChargenGroesse';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=98 WHERE KF_Tag='NeuChargenMengeVariante';
INSERT IGNORE INTO Hinweise2 VALUES (5,0,3,98,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Programm-Konstanten PPS','','<h1>Programm-Konstanten</h1><p>Festwerte WinBack Bilanzierung</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=3, KF_Id2=98 WHERE KF_Tag='BilanzLockFilename';
UPDATE Konfiguration SET KF_Id1=3, KF_Id2=98 WHERE KF_Tag='BilanzSkript';
INSERT IGNORE INTO Hinweise2 VALUES (5,0,4,98,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Programm-Konstanten PPS','','<h1>Programm-Konstanten</h1><p>Festwerte BC9000-Kopplung</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=4, KF_Id2=98 WHERE KF_Tag='bc9000_sleep_time';
UPDATE Konfiguration SET KF_Id1=4, KF_Id2=98 WHERE KF_Tag='FehlermeldungenBC';
UPDATE Konfiguration SET KF_Id1=4, KF_Id2=98 WHERE KF_Tag='BC9000Neustart';
UPDATE Konfiguration SET KF_Id1=4, KF_Id2=98 WHERE KF_Tag='Nicht_warten_auf_Status_5';
UPDATE Konfiguration SET KF_Id1=4, KF_Id2=98 WHERE KF_Tag='UliTaste';
INSERT IGNORE INTO Hinweise2 VALUES (5,0,5,98,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Programm-Konstanten PPS','','<h1>Programm-Konstanten</h1><p>Festwerte Teigruhe/Hebekipper</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=5, KF_Id2=98 WHERE KF_Tag='BCTeigruheAnzahlSaetze';
UPDATE Konfiguration SET KF_Id1=5, KF_Id2=98 WHERE KF_Tag='BCTeigruheDownloadAbstand';
UPDATE Konfiguration SET KF_Id1=5, KF_Id2=98 WHERE KF_Tag='teigruhe_abgelaufen_max_sekunden';
UPDATE Konfiguration SET KF_Id1=5, KF_Id2=98 WHERE KF_Tag='teigruhe_default_kessel_nr';
UPDATE Konfiguration SET KF_Id1=5, KF_Id2=98 WHERE KF_Tag='Teigruhe_HD';

INSERT IGNORE INTO Hinweise2 VALUES (5,0,0,97,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Programm-Konstanten VTS','','<h1>Programm-Konstanten</h1><p>Festwerte WinBack-VTS</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=97 WHERE KF_Tag='BehTextAlt';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=97 WHERE KF_Tag='VtsBehNrPlus10';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=97 WHERE KF_Tag='VtsVariante';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=97 WHERE KF_Tag='ClientNrVTS';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=97 WHERE KF_Tag='Bits_Wiederaufsetzen_Check';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=97 WHERE KF_Tag='Eing�ngeVerwenden';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=97 WHERE KF_Tag='WasserBeiMehlMitbeenden';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=97 WHERE KF_Tag='DefaultLGVts';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=97 WHERE KF_Tag='VtsAlarmSicherheitsZeit';
INSERT IGNORE INTO Hinweise2 VALUES (5,0,1,97,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Programm-Konstanten VTS','','<h1>Programm-Konstanten</h1><p>Festwerte WinBack-VTS Farben/Status</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus00';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus01';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus02';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus03';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus04';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus05';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus06';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus07';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus08';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus09';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=97 WHERE KF_Tag='Farbe_BehStatus10';
INSERT IGNORE INTO Hinweise2 VALUES (5,0,2,97,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Konfiguration VTS','','<h1>Konfiguration VTS</h1><p>Einstellungen Sauerteig-Anlage</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=97 WHERE KF_Tag='WinBackVTS';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=97 WHERE KF_Tag='VtsFolgewoche';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=97 WHERE KF_Tag='VtsMaxZeitStop';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=97 WHERE KF_Tag='VtsMaxZeitWarteWeg';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=97 WHERE KF_Tag='vts__bc9_nr_mehl';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=97 WHERE KF_Tag='vts__bc9_nr_wasser';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=97 WHERE KF_Tag='vts__local_anz_st_bcs';

INSERT IGNORE INTO Hinweise2 VALUES (5,0,0,96,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Kundenspezifische Daten','','<h1>Kundenspezifische-Konstanten</h1><p>Festwerte WinBack</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=96 WHERE KF_Tag='IpBasisAdresse';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=96 WHERE KF_Tag='KundenName';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=96 WHERE KF_Tag='KundenText';
INSERT IGNORE INTO Hinweise2 VALUES (5,0,1,96,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Kundenspezifische Daten','','<h1>Kundenspezifische-Konstanten</h1><p>Sprachumschaltung</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=96 WHERE KF_Tag='LaenderCode';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=96 WHERE KF_Tag='Sprache';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=96 WHERE KF_Tag='Sprache1';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=96 WHERE KF_Tag='Sprache2';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=96 WHERE KF_Tag='Sprache3';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=96 WHERE KF_Tag='SprachenVariante';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=96 WHERE KF_Tag='NeueTexteMarkieren';
UPDATE Konfiguration SET KF_Id1=1, KF_Id2=96 WHERE KF_Tag='NeueTexteSpeichern';
INSERT IGNORE INTO Hinweise2 VALUES (5,0,2,96,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Kundenspezifische Daten','','<h1>Kundenspezifische-Konstanten</h1><p>Design</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=96 WHERE KF_Tag='ButtonTextFett';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=96 WHERE KF_Tag='ProduktionsLayout';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=96 WHERE KF_Tag='MehlMaxMenge';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=96 WHERE KF_Tag='WaagenAnzeigeDefault';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=96 WHERE KF_Tag='OeffnenAlt';
INSERT IGNORE INTO Hinweise2 VALUES (5,0,2,95,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Tageswechsel','','<h1>Kundenspezifische-Konstanten</h1><p>Tageswechsel</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=95 WHERE KF_Tag='TW_Modus';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=95 WHERE KF_Tag='VorTWKommando';
UPDATE Konfiguration SET KF_Id1=2, KF_Id2=95 WHERE KF_Tag='Loeschen_aelter_als_Tage';

INSERT IGNORE INTO Hinweise2 VALUES (5,0,0,90,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Programm-Funktionen','','<h1>Programm-Funktionen</h1><p>WinBack Funktionen aktivieren</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='ArtRzNrAuswahl';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='NeuRezepte';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='Teigzettel';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='AutoEinAus';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='TextAutoQuitt';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='FoerderUeberStop';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='FrageArtikelLoeschen';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='IntStartSperre';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='KesselGroesse';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='KKA_Silo_Reihenfolge';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='Mehl_RS_Reihenfolge';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='KTKurzBezVTS';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='LinienUmschaltung';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='LU_2_Linien_Nr';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='MaterialAlleKo';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='Befuellung';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='Multistart';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=90 WHERE KF_Tag='RechteDefaultWert';

INSERT IGNORE INTO Hinweise2 VALUES (5,0,0,80,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Handwaagen','','<h1>Handwaagen</h1><p>Einstellungen Tisch/Hand-Bodenwaage</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=80 WHERE KF_Tag='Nullung_BW_unterhalb';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=80 WHERE KF_Tag='Nullung_TW_unterhalb';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=80 WHERE KF_Tag='TaraWerte';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=80 WHERE KF_Tag='TWBW_KeinDynTara';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=80 WHERE KF_Tag='TwNegativVerwiegung';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=80 WHERE KF_Tag='TwObenOffs';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=80 WHERE KF_Tag='TwRechtsOffs';


INSERT IGNORE INTO Hinweise2 VALUES (5,0,0,30,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Cloud-Verbindung','','<h1>WinBack-Cloud</h1><p>Silo-F�llstand �ber WinBack-Cloud</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=30 WHERE KF_Tag='CloudConnectionEnable';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=30 WHERE KF_Tag='CloudSleepTime';

INSERT IGNORE INTO Hinweise2 VALUES (5,0,0,20,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Chargen-Erfassung','','<h1>Chargen-Erfassung</h1><p>Rohstoff-Chargenerfassung</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=20 WHERE KF_Tag='ChargenerfassungEin';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=20 WHERE KF_Tag='ChargenerfassungEingabeInfo';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=20 WHERE KF_Tag='ChargenerfassungVariante';

INSERT IGNORE INTO Hinweise2 VALUES (5,0,0,12,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Teigtemperatur-Steuerung','','<h1>TeigTemperatur</h1><p>WinBack Einstellungen Teigtemperatur-Steuerung</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=12 WHERE KF_Tag='RMFvorhanden';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=12 WHERE KF_Tag='TempDauerhaftDefault';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=12 WHERE KF_Tag='temp_delta_aenderung_max';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=12 WHERE KF_Tag='temp_erfassung_max_minuten';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=12 WHERE KF_Tag='temp_rezept_abweichung_max';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=12 WHERE KF_Tag='TTSvorhanden';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=12 WHERE KF_Tag='T_Korr_Variante';

INSERT IGNORE INTO Hinweise2 VALUES (5,0,0,10,0,'2021-11-22 09:00:00',709760,'WinBack GmbH','Kneter-Steuerung','','<h1>Kneter-Steuerung</h1><p>WinBack Ansteuerung Kneter</p>',20171213151752);
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=10 WHERE KF_Tag='default_kneter_nr';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=10 WHERE KF_Tag='rs_nach_kneter';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=10 WHERE KF_Tag='Gruppen_Zuordnung';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=10 WHERE KF_Tag='KneterPlusTempErf';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=10 WHERE KF_Tag='KneterStartVariante';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=10 WHERE KF_Tag='KneterVariante';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=10 WHERE KF_Tag='kneter_sofortstart_wenn_nur_TR';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=10 WHERE KF_Tag='KNZvorhanden';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=10 WHERE KF_Tag='KneterPlusTempErf';
UPDATE Konfiguration SET KF_Id1=0, KF_Id2=10 WHERE KF_Tag='QuittTeigTempKneter';

