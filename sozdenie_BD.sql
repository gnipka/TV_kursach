/*
Created: 08.08.2021
Modified: 13.08.2021
Model: MySQL 8.0
Database: MySQL 8.0
*/

-- Create tables section -------------------------------------------------

-- Table Ordering

CREATE DATABASE TV;
USE TV;

CREATE TABLE `Ordering`
(
  `Ord_ID` Int NOT NULL AUTO_INCREMENT,
  `Ord_Status` Char(120) NOT NULL,
  `Ord_Count` Int NOT NULL,
  `Ord_Date` Datetime NOT NULL,
  `Ord_DatePlan` Datetime NOT NULL,
  `Nom_ID` Int NOT NULL,
  PRIMARY KEY (`Ord_ID`)
)
;

CREATE INDEX `IX_Relationship2` ON `Ordering` (`Nom_ID`)
;

-- Table Nomenclature

CREATE TABLE `Nomenclature`
(
  `Nom_ID` Int NOT NULL,
  `Nom_Description` Char(120) NOT NULL,
  `Map_ID` Int,
  `Spec_ID` Int
)
;

CREATE INDEX `IX_Relationship7` ON `Nomenclature` (`Map_ID`)
;

CREATE INDEX `IX_Relationship13` ON `Nomenclature` (`Spec_ID`)
;

ALTER TABLE `Nomenclature` ADD PRIMARY KEY (`Nom_ID`)
;

-- Table Usage_Log

CREATE TABLE `Usage_Log`
(
  `Note_ID` Int NOT NULL,
  `Log_Count` Int NOT NULL,
  `Ord_ID` Int NOT NULL,
  `Nom_ID` Int NOT NULL
)
;

CREATE INDEX `IX_Relationship11` ON `Usage_Log` (`Ord_ID`)
;

CREATE INDEX `IX_Relationship12` ON `Usage_Log` (`Nom_ID`)
;

ALTER TABLE `Usage_Log` ADD PRIMARY KEY (`Note_ID`)
;

-- Table Technological_map

CREATE TABLE `Technological_map`
(
  `Map_ID` Int NOT NULL,
  `Map_Description` Char(120) NOT NULL
)
;

ALTER TABLE `Technological_map` ADD PRIMARY KEY (`Map_ID`)
;

-- Table Map_Composition

CREATE TABLE `Map_Composition`
(
  `Operation_ID` Int NOT NULL,
  `Processing_Time` Int NOT NULL,
  `Number_Next_Operation` Int,
  `Map_ID` Int NOT NULL,
  `WC_ID` Int
)
;

CREATE INDEX `IX_Relationship9` ON `Map_Composition` (`WC_ID`)
;

ALTER TABLE `Map_Composition` ADD PRIMARY KEY (`Operation_ID`, `Map_ID`)
;

-- Table Work_Center

CREATE TABLE `Work_Center`
(
  `WC_ID` Int NOT NULL,
  `WC_status` Char(120) NOT NULL,
  `WC_Work_Time` Char(120) NOT NULL
)
;

ALTER TABLE `Work_Center` ADD PRIMARY KEY (`WC_ID`)
;

-- Table Storage

CREATE TABLE `Storage`
(
  `Storage_ID` Int NOT NULL,
  `Storage_Description` Char(120) NOT NULL
)
;

ALTER TABLE `Storage` ADD PRIMARY KEY (`Storage_ID`)
;

-- Table Specification

CREATE TABLE `Specification`
(
  `Spec_ID` Int NOT NULL,
  `Spec_Description` Char(100) NOT NULL
)
;

ALTER TABLE `Specification` ADD PRIMARY KEY (`Spec_ID`)
;

-- Table Spec_Composition

CREATE TABLE `Spec_Composition`
(
  `Comp_ID` Int NOT NULL,
  `Comp_Description` Char(100) NOT NULL,
  `Comp_Count` Int NOT NULL,
  `Nom_ID` Int,
  `Spec_ID` Int NOT NULL
)
;

CREATE INDEX `IX_Relationship15` ON `Spec_Composition` (`Nom_ID`)
;

ALTER TABLE `Spec_Composition` ADD PRIMARY KEY (`Comp_ID`, `Spec_ID`)
;

-- Table Work_Log

CREATE TABLE `Work_Log`
(
  `Note_ID` Int NOT NULL,
  `Log_Work_Time` Int NOT NULL,
  `Output_quantity` Char(100) NOT NULL,
  `Ord_ID` Int NOT NULL,
  `Nom_ID` Int NOT NULL,
  `Operation_ID` Int NOT NULL,
  `Map_ID` Int NOT NULL
)
;

CREATE INDEX `IX_Relationship5` ON `Work_Log` (`Ord_ID`)
;

CREATE INDEX `IX_Relationship6` ON `Work_Log` (`Nom_ID`)
;

CREATE INDEX `IX_Relationship10` ON `Work_Log` (`Operation_ID`, `Map_ID`)
;

ALTER TABLE `Work_Log` ADD PRIMARY KEY (`Note_ID`)
;

-- Table Stocks

CREATE TABLE `Stocks`
(
  `Nom_ID` Int NOT NULL,
  `Storage_ID` Int NOT NULL,
  `St_Count` Int NOT NULL
)
;

ALTER TABLE `Stocks` ADD PRIMARY KEY (`Nom_ID`, `Storage_ID`)
;

-- Create foreign keys (relationships) section -------------------------------------------------

ALTER TABLE `Ordering` ADD CONSTRAINT `Consist of Nom` FOREIGN KEY (`Nom_ID`) REFERENCES `Nomenclature` (`Nom_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Stocks` ADD CONSTRAINT `Contains1` FOREIGN KEY (`Nom_ID`) REFERENCES `Nomenclature` (`Nom_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Stocks` ADD CONSTRAINT `Contains2` FOREIGN KEY (`Storage_ID`) REFERENCES `Storage` (`Storage_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Work_Log` ADD CONSTRAINT `Recorded in WL1` FOREIGN KEY (`Ord_ID`) REFERENCES `Ordering` (`Ord_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Work_Log` ADD CONSTRAINT `Recorded in WL2` FOREIGN KEY (`Nom_ID`) REFERENCES `Nomenclature` (`Nom_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Nomenclature` ADD CONSTRAINT `TM is used` FOREIGN KEY (`Map_ID`) REFERENCES `Technological_map` (`Map_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Map_Composition` ADD CONSTRAINT `Consists of TM` FOREIGN KEY (`Map_ID`) REFERENCES `Technological_map` (`Map_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Map_Composition` ADD CONSTRAINT `Uses in` FOREIGN KEY (`WC_ID`) REFERENCES `Work_Center` (`WC_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Work_Log` ADD CONSTRAINT `Recorded in WL3` FOREIGN KEY (`Operation_ID`, `Map_ID`) REFERENCES `Map_Composition` (`Operation_ID`, `Map_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Usage_Log` ADD CONSTRAINT `Recorded in UL1` FOREIGN KEY (`Ord_ID`) REFERENCES `Ordering` (`Ord_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Usage_Log` ADD CONSTRAINT `Recorded in UL2` FOREIGN KEY (`Nom_ID`) REFERENCES `Nomenclature` (`Nom_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Nomenclature` ADD CONSTRAINT `Uses in Nom` FOREIGN KEY (`Spec_ID`) REFERENCES `Specification` (`Spec_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Spec_Composition` ADD CONSTRAINT `Uses in Spec_Comp` FOREIGN KEY (`Nom_ID`) REFERENCES `Nomenclature` (`Nom_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `Spec_Composition` ADD CONSTRAINT `Consists of` FOREIGN KEY (`Spec_ID`) REFERENCES `Specification` (`Spec_ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE spec_composition ADD Operation_ID integer;
ALTER TABLE map_composition ADD Operation_desc CHAR(100);
