-- -------------------------------------------------------------------------------
-- Basic DB schema creation script - creates a V1 schema, which then gets upgraded
-- By running a set of migrations on top of that. Therefore:

--     **** DO NOT ADD NEW SCHEMA TO THIS FILE ****
--     **** DO NOT ADD NEW SCHEMA TO THIS FILE ****
--     **** DO NOT ADD NEW SCHEMA TO THIS FILE ****

-- -------------------------------------------------------------------------------

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

-- --------------------------------------------------------

--
-- Table structure for table `bandmodestats`
--

CREATE TABLE IF NOT EXISTS `bandmodestats` (
  `band` varchar(15) NOT NULL,
  `mode` varchar(15) NOT NULL,
  `cnt` int(11) NOT NULL,
  PRIMARY KEY (`band`,`mode`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `bandorder`
--

CREATE TABLE IF NOT EXISTS `bandorder` (
  `order` int(11) NOT NULL,
  `band` varchar(15) NOT NULL,
  PRIMARY KEY (`order`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `frequencies`
--

CREATE TABLE IF NOT EXISTS `frequencies` (
  `station` varchar(15) NOT NULL,
  `frequency` int(11) NOT NULL,
  PRIMARY KEY (`station`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `knowncalls`
--

CREATE TABLE IF NOT EXISTS `knowncalls` (
  `callsign` varchar(15) NOT NULL,
  `locator` varchar(10) NOT NULL DEFAULT '',
  PRIMARY KEY (`callsign`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `sources`
--

CREATE TABLE IF NOT EXISTS `sources` (
  `id` int(11) NOT NULL,
  `callsign` varchar(20) NOT NULL,
  `default` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `log`
--

CREATE TABLE IF NOT EXISTS `log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sourceId` bigint(20) NOT NULL,
  `lastModified` datetime NOT NULL,
  `startTime` datetime NOT NULL,
  `endTime` datetime NOT NULL,
  `callsign` varchar(20) NOT NULL,
  `station` varchar(20) NOT NULL,
  `operator` varchar(20) NOT NULL,
  `band` varchar(10) DEFAULT NULL,
  `mode` varchar(10) DEFAULT NULL,
  `frequency` bigint(20) DEFAULT NULL,
  `reportTx` varchar(10) DEFAULT NULL,
  `reportRx` varchar(10) DEFAULT NULL,
  `locator` varchar(10) DEFAULT NULL,
  `powerRx` int(11) DEFAULT NULL,
  `antennaGainRx` int(11) DEFAULT NULL,
  `notes` varchar(255) DEFAULT NULL,
  `serialSent` varchar(10) NOT NULL,
  `serialReceived` varchar(10) NOT NULL,
  `qslRxDate` datetime DEFAULT NULL,
  `qslTxDate` datetime DEFAULT NULL,
  `qslMethod` varchar(20) DEFAULT NULL,
  `location` int(11) not null default 0,
  PRIMARY KEY (`id`,`sourceId`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1;

-- --------------------------------------------------------

--
-- Table structure for table `modemap`
--

CREATE TABLE IF NOT EXISTS `modemap` (
  `sourceText` varchar(15) NOT NULL,
  `displayText` varchar(15) NOT NULL,
  PRIMARY KEY (`sourceText`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `modeorder`
--

CREATE TABLE IF NOT EXISTS `modeorder` (
  `order` int(11) NOT NULL,
  `mode` varchar(15) NOT NULL,
  PRIMARY KEY (`order`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `serials`
--

CREATE TABLE IF NOT EXISTS `serials` (
  `band` varchar(10) NOT NULL,
  `nextSerial` int(11) NOT NULL,
  PRIMARY KEY (`band`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `setup`
--

CREATE TABLE IF NOT EXISTS `setup` (
  `key` varchar(255) NOT NULL,
  `val` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`key`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `locations`
--
CREATE TABLE IF NOT EXISTS `locations` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `locator` varchar(10) not null default '',
  `wab` varchar(6) not null default '',
  `club` varchar(30) not null default '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO setup (`key`, `val`) VALUES ('schemaVersion', '2');