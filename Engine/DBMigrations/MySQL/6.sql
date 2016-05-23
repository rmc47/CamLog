DELETE FROM `modemap`;
INSERT INTO `modemap` (`sourceText`, `displayText`) VALUES
('PSK63', 'PSK'),
('PSK31', 'PSK'),
('PSK125', 'PSK'),
('JT65b', 'JT/FSK'),
('JT6m', 'JT/FSK'),
('FSK441', 'JT/FSK'),
('SSB', 'Phone'),
('AM', 'Phone'),
('FM', 'Phone'),
('JT65c', 'JT/FSK'),
('ISCAT', 'JT/FSK');

DELETE FROM `bandorder`;
INSERT INTO `bandorder` (`order`, `band`) VALUES
(100, '160m'),
(200, '80m'),
(300, '60m'),
(400, '40m'),
(500, '30m'),
(600, '20m'),
(700, '17m'),
(800, '15m'),
(900, '12m'),
(1000, '10m'),
(1100, '6m'),
(1200, '4m'),
(1300, '2m'),
(1400, '70cm'),
(1500, '23cm'),
(1600, '3cm');

DELETE FROM `modeorder`;
INSERT INTO `modeorder` (`order`, `mode`) VALUES
(100, 'Phone'),
(200, 'CW'),
(300, 'PSK'),
(400, 'JT6m'),
(500, 'FM'),
(600, 'FSK441'),
(700, 'RTTY'),
(800, 'Olivia'),
(900, 'SSTV'),
(450, 'JT65b'),
(310, 'PSK31'),
(410, 'JT/FSK');