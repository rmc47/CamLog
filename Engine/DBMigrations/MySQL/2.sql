-- Migration from DB version 1 - 2

-- Table structure for table `locations`
CREATE TABLE IF NOT EXISTS `locations` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `locator` varchar(10) not null default '',
  `wab` varchar(6) not null default '',
  `club` varchar(30) not null default '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Add Location to log table
ALTER TABLE `log` ADD COLUMN `location` int(11) not null default 0;

UPDATE `setup` SET `val`=2 WHERE `key`='schemaVersion';