-- phpMyAdmin SQL Dump
-- version 3.2.0.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Jul 22, 2009 at 11:30 PM
-- Server version: 5.1.36
-- PHP Version: 5.3.0

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

--
-- Database: `vhfnfd`
--

-- --------------------------------------------------------

--
-- Table structure for table `log`
--

CREATE TABLE IF NOT EXISTS `log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `lastModified` datetime NOT NULL,
  `time` datetime NOT NULL,
  `callsign` varchar(20) NOT NULL,
  `reportSent` varchar(10) NOT NULL,
  `reportReceived` varchar(10) NOT NULL,
  `serialSent` int(11) NOT NULL,
  `serialReceived` int(11) NOT NULL,
  `locatorReceived` varchar(10) DEFAULT NULL,
  `notes` varchar(1024) NOT NULL,
  `operator` varchar(20) NOT NULL,
  `band` varchar(10) NOT NULL,
  `mode` varchar(10) NOT NULL,
  `points` int(11) NOT NULL,
  `iotaRef` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `callsign` (`callsign`),
  KEY `callsign_2` (`callsign`,`band`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `serials`
--

CREATE TABLE IF NOT EXISTS `serials` (
  `band` varchar(10) NOT NULL,
  `nextSerial` int(11) NOT NULL,
  PRIMARY KEY (`band`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
