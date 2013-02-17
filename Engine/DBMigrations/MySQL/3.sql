ALTER TABLE `locations` 
	ADD COLUMN iotaref VARCHAR(10) NOT NULL, 
	ADD COLUMN iotaname VARCHAR(30) NOT NULL;

UPDATE `setup` SET `val`=3 WHERE `key`='schemaVersion';