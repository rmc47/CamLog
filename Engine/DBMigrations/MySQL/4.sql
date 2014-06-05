ALTER TABLE `log` 
	ADD COLUMN satellitename VARCHAR(10), 
	ADD COLUMN satellitemode VARCHAR(10);

UPDATE `setup` SET `val`=4 WHERE `key`='schemaVersion';