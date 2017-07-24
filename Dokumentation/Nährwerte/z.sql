USE winback;


DROP TABLE IF EXISTS ENummern;
CREATE TABLE ENummern (
  EN_Nr int(10) NOT NULL default '0',
  EN_Idx int(10) NOT NULL default '0',
  EN_Bezeichnung varchar(255) default NULL,
  EN_Name varchar(10) default NULL,
  EN_Beschreibung varchar(255) default NULL,
  EN_Bemerkung varchar(255) default NULL,
  EN_Key varchar(1) default NULL,
  EN_CleanLabel varchar(1) default NULL,
  EN_Timestamp timestamp(6) NOT NULL,
  PRIMARY KEY  (EN_Nr,EN_Idx)
) ;




