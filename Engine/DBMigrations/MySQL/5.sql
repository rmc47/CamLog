CREATE TABLE IF NOT EXISTS `operatorstats` (
  `operator` varchar(10) NOT NULL,
  `qsos` int(11) NOT NULL,
  PRIMARY KEY (`operator`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS `squaresstats` (
  `band` varchar(10) NOT NULL,
  `squarecount` int(11) NOT NULL,
  PRIMARY KEY (`band`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;