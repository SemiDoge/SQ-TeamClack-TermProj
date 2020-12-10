-- MySqlBackup.NET 2.3.3
-- Dump Time: 2020-12-08 22:14:33
-- --------------------------------------
-- Server version 8.0.22 MySQL Community Server - GPL


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of carrier
-- 

DROP TABLE IF EXISTS `carrier`;
CREATE TABLE IF NOT EXISTS `carrier` (
  `CarrierID` int NOT NULL AUTO_INCREMENT,
  `CarrierName` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`CarrierID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table carrier
-- 

/*!40000 ALTER TABLE `carrier` DISABLE KEYS */;
INSERT INTO `carrier`(`CarrierID`,`CarrierName`) VALUES
(1,'Planet Express'),
(2,'Schooners'),
(3,'Tillman Transport'),
(4,'We Haul');
/*!40000 ALTER TABLE `carrier` ENABLE KEYS */;

-- 
-- Definition of carriercities
-- 

DROP TABLE IF EXISTS `carriercities`;
CREATE TABLE IF NOT EXISTS `carriercities` (
  `CarrierCitiesID` int NOT NULL AUTO_INCREMENT,
  `CityName` varchar(100) DEFAULT NULL,
  `CarrierID` int DEFAULT NULL,
  `FTLA` int DEFAULT NULL,
  `LTLA` int DEFAULT NULL,
  `FTLRate` double DEFAULT NULL,
  `LTLRate` double DEFAULT NULL,
  `reefCharge` double DEFAULT NULL,
  PRIMARY KEY (`CarrierCitiesID`),
  KEY `CarrierID` (`CarrierID`),
  CONSTRAINT `carriercities_ibfk_1` FOREIGN KEY (`CarrierID`) REFERENCES `carrier` (`CarrierID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table carriercities
-- 

/*!40000 ALTER TABLE `carriercities` DISABLE KEYS */;
INSERT INTO `carriercities`(`CarrierCitiesID`,`CityName`,`CarrierID`,`FTLA`,`LTLA`,`FTLRate`,`LTLRate`,`reefCharge`) VALUES
(1,'Windsor',1,50,640,5.21,0.3621,0.08),
(2,'Hamilton',1,50,640,5.21,0.3621,0.08),
(3,'Oshawa',1,50,640,5.21,0.3621,0.08),
(4,'Belleville',1,50,640,5.21,0.3621,0.08),
(5,'Ottawa',1,50,640,5.21,0.3621,0.08),
(6,'London',2,18,98,5.05,0.3434,0.07),
(7,'Toronto',2,18,98,5.05,0.3434,0.07),
(8,'Kingston',2,18,98,5.05,0.3434,0.07),
(9,'Windsor',3,24,35,5.11,0.3012,0.09),
(10,'London',3,24,35,5.11,0.3012,0.09),
(11,'Hamilton',3,24,35,5.11,0.3012,0.09),
(12,'Ottawa',4,11,0,5.2,0,0.065),
(13,'Toronto',4,11,0,5.2,0,0.065);
/*!40000 ALTER TABLE `carriercities` ENABLE KEYS */;

-- 
-- Definition of customers
-- 

DROP TABLE IF EXISTS `customers`;
CREATE TABLE IF NOT EXISTS `customers` (
  `CustomerID` int NOT NULL AUTO_INCREMENT,
  `CustomerName` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`CustomerID`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table customers
-- 

/*!40000 ALTER TABLE `customers` DISABLE KEYS */;

/*!40000 ALTER TABLE `customers` ENABLE KEYS */;

-- 
-- Definition of orders
-- 

DROP TABLE IF EXISTS `orders`;
CREATE TABLE IF NOT EXISTS `orders` (
  `OrderID` int NOT NULL,
  `OrderDate` varchar(100) DEFAULT NULL,
  `CustomerName` varchar(100) DEFAULT NULL,
  `JobType` int DEFAULT NULL,
  `Quantity` int DEFAULT NULL,
  `Origin` varchar(100) DEFAULT NULL,
  `Destination` varchar(100) DEFAULT NULL,
  `Van_Type` int DEFAULT NULL,
  `CarrierName` varchar(100) DEFAULT NULL,
  `MarkedForAction` tinyint(1) DEFAULT NULL,
  `NumberOfTrips` int DEFAULT NULL,
  `ETA` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`OrderID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table orders
-- 

/*!40000 ALTER TABLE `orders` DISABLE KEYS */;

/*!40000 ALTER TABLE `orders` ENABLE KEYS */;

-- 
-- Definition of rates
-- 

DROP TABLE IF EXISTS `rates`;
CREATE TABLE IF NOT EXISTS `rates` (
  `RatesID` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(50) DEFAULT NULL,
  `CarrierName` varchar(100) DEFAULT NULL,
  `UniqueCarrierID` int DEFAULT NULL,
  `Rate` double DEFAULT NULL,
  PRIMARY KEY (`RatesID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table rates
-- 

/*!40000 ALTER TABLE `rates` DISABLE KEYS */;
INSERT INTO `rates`(`RatesID`,`Type`,`CarrierName`,`UniqueCarrierID`,`Rate`) VALUES
(1,'Standard','DHL',1,9.99),
(2,'Express','DHL',1,19.99),
(3,'Standard','FedEx',2,9.99),
(4,'Express','FedEx',3,19.99),
(5,'Standard','Canada Post',4,9.99),
(6,'Express','Canada Post',4,19.99);
/*!40000 ALTER TABLE `rates` ENABLE KEYS */;

-- 
-- Definition of route
-- 

DROP TABLE IF EXISTS `route`;
CREATE TABLE IF NOT EXISTS `route` (
  `RouteID` int NOT NULL AUTO_INCREMENT,
  `Destination` varchar(100) DEFAULT NULL,
  `Distance` int DEFAULT NULL,
  `Time` double DEFAULT NULL,
  `West` varchar(100) DEFAULT NULL,
  `East` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`RouteID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table route
-- 

/*!40000 ALTER TABLE `route` DISABLE KEYS */;
INSERT INTO `route`(`RouteID`,`Destination`,`Distance`,`Time`,`West`,`East`) VALUES
(1,'Windsor',191,2.5,'END','London'),
(2,'London',128,1.75,'Windsor','Hamilton'),
(3,'Hamilton',68,1.25,'London','Toronto'),
(4,'Toronto',60,1.3,'Hamilton','Oshawa'),
(5,'Oshawa',134,1.65,'Toronto','Belleville'),
(6,'Belleville',82,1.2,'Oshawa','Kingston'),
(7,'Kingston',196,2.5,'Belleville','Ottawa'),
(8,'Ottawa',0,0,'Kingston','END');
/*!40000 ALTER TABLE `route` ENABLE KEYS */;

-- 
-- Definition of users
-- 

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) DEFAULT NULL,
  `Password` varchar(100) DEFAULT NULL,
  `ContactNumber` varchar(10) DEFAULT NULL,
  `Role` int DEFAULT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table users
-- 

/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users`(`UserID`,`UserName`,`Password`,`ContactNumber`,`Role`) VALUES
(1,'tngo','abc',NULL,0),
(2,'iandr','abc',NULL,0),
(3,'jzabar','123',NULL,1),
(4,'tsalam','123',NULL,2);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2020-12-08 22:14:33
-- Total time: 0:0:0:0:178 (d:h:m:s:ms)
