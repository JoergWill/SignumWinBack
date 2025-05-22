use winback;

INSERT INTO `Linien` (`L_Nr`, `L_Bezeichnung`, `L_Seg_Idx`, `L_Std_Var_Nr`, `L_LU2_L_Nr`, `L_aktiv`, `L_Kommentar`, `L_Timestamp`, `L_ProdFiliale`) VALUES (10, 'L10: VLinie', 2, 1, 0, 0, NULL, '20220411132536', NULL);

INSERT INTO `Linien` (`L_Nr`, `L_Bezeichnung`, `L_Seg_Idx`, `L_Std_Var_Nr`, `L_LU2_L_Nr`, `L_aktiv`, `L_Kommentar`, `L_Timestamp`, `L_ProdFiliale`) VALUES (10, 'L10: VLinie', 2, 1, 0, 0, NULL, '20220411132536', NULL);

INSERT INTO `AktionsTimer` (`AT_idx`, `AT_Quelle_Art`, `AT_Quelle_Nr`, `AT_Quelle_Typ`, `AT_Quelle_ID`, `AT_Ziel_Art`, `AT_Ziel_Nr`, `AT_Ziel_Aktion`, `AT_Ziel_Verzoegerung`, `AT_TimingArt`, `AT_Startzeit`, `AT_Endezeit`, `AT_Periode`, `AT_Str1`, `AT_Str2`, `AT_Kommentar`, `AT_Timestamp`) VALUES (70, 0, 0, 'virt_linie', NULL, 0, 0, 1, 0, 0, '2024-03-12 23:00:00', '2024-03-11 23:00:01', 86400, 'WB VLinie', '10', 'WinBack virtuelle Linie', '20240311230001');
