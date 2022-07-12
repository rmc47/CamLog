INSERT INTO `modemap` (`sourceText`, `displayText`) VALUES ('FT8', 'JT/FSK');
INSERT INTO `modemap` (`sourceText`, `displayText`) VALUES ('FT4', 'JT/FSK');
INSERT INTO `modemap` (`sourceText`, `displayText`) VALUES ('JT9', 'JT/FSK');

alter table log add uploadedToClublog bit default false;
alter table log add uploadedToLotw bit default false;

UPDATE `setup` SET `val`=9 WHERE `key`='schemaVersion';