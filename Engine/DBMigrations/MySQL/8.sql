INSERT INTO `modemap` (`sourceText`, `displayText`) VALUES ('MSK144', 'JT/FSK');
INSERT INTO `modemap` (`sourceText`, `displayText`) VALUES ('FSK315', 'JT/FSK');

UPDATE `setup` SET `val`=8 WHERE `key`='schemaVersion';