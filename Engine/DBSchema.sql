-- phpMyAdmin SQL Dump
-- version 3.2.0.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Oct 22, 2011 at 08:18 PM
-- Server version: 5.1.36
-- PHP Version: 5.3.0

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

--
-- Database: `arran2011`
--

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
  PRIMARY KEY (`callsign`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

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
  PRIMARY KEY (`id`,`sourceId`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=9054 ;

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
