USE winback;

CREATE TABLE IF NOT EXISTS Messreihen(id int(11) NOT NULL default '0', bezeichnung varchar(254) default NULL, PRIMARY KEY(id)) 

CREATE TABLE IF NOT EXISTS Messungen(id int(11) NOT NULL default '0', timestamp int(11) NOT NULL default '0', wert decimal(4,1) NOT NULL default '0.0', PRIMARY KEY (id,timestamp)) 

ALTER TABLE `Messreihen` ADD COLUMN `behaelter` TINYINT NULL DEFAULT NULL, ADD COLUMN `color` VARCHAR(15) NULL DEFAULT NULL, ADD COLUMN `faktor` TINYINT(4) NULL DEFAULT 1, ADD COLUMN `offset_BC9000Liste` TINYINT(4) NULL DEFAULT 1,	ADD COLUMN `kanal_BC9000Plus9` TINYINT(4) NULL DEFAULT 11, ADD COLUMN `typ` VARCHAR(5) NULL DEFAULT 'X';