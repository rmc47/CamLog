-- Migration from DB version 1 - 2


-- Create sources table

CREATE TABLE IF NOT EXISTS `sources` (
  `id` int(11) NOT NULL,
  `callsign` varchar(20) NOT NULL,
  `default` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
