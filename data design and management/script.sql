CREATE TABLE `Taaskly`.`USER` (
  `User_Id` INT NOT NULL,
  `User_Email` CHAR(50) NOT NULL,
  `User_Nickname` CHAR(25) NOT NULL,
  `User_Firstname` CHAR(25) NOT NULL,
  `User_Lastname` CHAR(25) NOT NULL,
  `User_Bio` VARCHAR(1000) NULL,
  `User_Phone` CHAR(13) NOT NULL,
  `User_Balance` DOUBLE NOT NULL,
  `User_Referral_Code` CHAR(6) NOT NULL,
  `User_Profile_Picture` BLOB NULL,
  PRIMARY KEY (`User_Id`));

CREATE TABLE `Taaskly`.`SHOP` (
  `Shop_Id` INT NOT NULL,
  `Shop_Name` CHAR(50) NOT NULL,
  `Shop_Profile_Picture` BLOB NOT NULL,
  `Shop_Bio` VARCHAR(1000) NOT NULL,
  `Shop_Phone` CHAR(13) NOT NULL,
  `Shop_Street` CHAR(25) NOT NULL,
  `Shop_City` CHAR(25) NOT NULL,
  `Shop_State` CHAR(25) NOT NULL,
  PRIMARY KEY (`Shop_Id`));

CREATE TABLE `Taaskly`.`ITEM` (
  `Item_Id` INT NOT NULL,
  `Shop_Id` INT NOT NULL,
  `Item_Name` CHAR(50) NOT NULL,
  `Item_Description` VARCHAR(1000) NOT NULL,
  `Item_Price` DOUBLE NOT NULL,
  `Item_Image` BLOB NOT NULL,
  PRIMARY KEY (`Item_Id`),
  INDEX `fk_ITEM_1_idx` (`Shop_Id` ASC) VISIBLE,
  CONSTRAINT `fk_ITEM_1`
    FOREIGN KEY (`Shop_Id`)
    REFERENCES `Taaskly`.`SHOP` (`Shop_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE `Taaskly`.`SERVICE` (
  `Service_Id` INT NOT NULL,
  `Service_Name` CHAR(25) NOT NULL,
  `Service_Bio` VARCHAR(1000) NOT NULL,
  `Service_Phone` CHAR(13) NOT NULL,
  `Service_Profile_Picture` BLOB NOT NULL,
  `Service_Street` CHAR(25) NOT NULL,
  `Service_City` CHAR(25) NOT NULL,
  `Service_State` CHAR(25) NOT NULL,
  PRIMARY KEY (`Service_Id`));

CREATE TABLE `Taaskly`.`TASK` (
  `Task_Id` INT NOT NULL,
  `Task_Description` VARCHAR(1000) NOT NULL,
  `Task_Price` DOUBLE NOT NULL,
  `Task_Status` VARCHAR(45) NOT NULL,
  `Task_Expiration_Date` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Task_Id`));

CREATE TABLE `Taaskly`.`REVIEW` (
  `Review_Id` INT NOT NULL,
  `User_Id` INT NOT NULL,
  `Shop_Id` INT NULL,
  `Service_Id` INT NULL,
  `Review_Comment` VARCHAR(1000) NOT NULL,
  `Review_Rating` DOUBLE NOT NULL,
  `Review_Timestamp` DATETIME NOT NULL,
  PRIMARY KEY (`Review_Id`),
  INDEX `fk_REVIEW_1_idx` (`User_Id` ASC) VISIBLE,
  INDEX `fk_REVIEW_2_idx` (`Shop_Id` ASC) VISIBLE,
  INDEX `fk_REVIEW_3_idx` (`Service_Id` ASC) VISIBLE,
  CONSTRAINT `fk_REVIEW_1`
    FOREIGN KEY (`User_Id`)
    REFERENCES `Taaskly`.`USER` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_REVIEW_2`
    FOREIGN KEY (`Shop_Id`)
    REFERENCES `Taaskly`.`SHOP` (`Shop_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_REVIEW_3`
    FOREIGN KEY (`Service_Id`)
    REFERENCES `Taaskly`.`SERVICE` (`Service_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


CREATE TABLE `Taaskly`.`CATEGORY` (
  `Category_Type` CHAR(25) NOT NULL,
  `Task_Id` INT NULL,
  `Item_Id` INT NULL,
  `Service_Id` INT NULL,
  INDEX `fk_CATEGORY_1_idx` (`Task_Id` ASC) VISIBLE,
  INDEX `fk_CATEGORY_2_idx` (`Item_Id` ASC) VISIBLE,
  INDEX `fk_CATEGORY_3_idx` (`Service_Id` ASC) VISIBLE,
  CONSTRAINT `fk_CATEGORY_1`
    FOREIGN KEY (`Task_Id`)
    REFERENCES `Taaskly`.`TASK` (`Task_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CATEGORY_2`
    FOREIGN KEY (`Item_Id`)
    REFERENCES `Taaskly`.`ITEM` (`Item_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CATEGORY_3`
    FOREIGN KEY (`Service_Id`)
    REFERENCES `Taaskly`.`SERVICE` (`Service_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE `Taaskly`.`OFFER` (
  `Offer_Id` INT NOT NULL,
  `Task_Id` INT NOT NULL,
  `User_Id` INT NOT NULL,
  `Offer_Description` VARCHAR(1000) NOT NULL,
  `Offer_Price` DOUBLE NOT NULL,
  `Offer_Status` CHAR(10) NOT NULL,
  `Offer_Timestamp` DATETIME NOT NULL,
  PRIMARY KEY (`Offer_Id`),
  INDEX `fk_OFFER_1_idx` (`User_Id` ASC) VISIBLE,
  INDEX `fk_OFFER_2_idx` (`Task_Id` ASC) VISIBLE,
  CONSTRAINT `fk_OFFER_1`
    FOREIGN KEY (`User_Id`)
    REFERENCES `Taaskly`.`USER` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_OFFER_2`
    FOREIGN KEY (`Task_Id`)
    REFERENCES `Taaskly`.`TASK` (`Task_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
CREATE TABLE `Taaskly`.`BOOKING` (
  `Booking_Id` INT NOT NULL,
  `Service_Id` INT NOT NULL,
  `User_Id` INT NOT NULL,
  `Booking_Date` DATETIME NOT NULL,
  PRIMARY KEY (`Booking_Id`),
  INDEX `fk_BOOKING_1_idx` (`Service_Id` ASC) VISIBLE,
  INDEX `fk_BOOKING_2_idx` (`User_Id` ASC) VISIBLE,
  CONSTRAINT `fk_BOOKING_1`
    FOREIGN KEY (`Service_Id`)
    REFERENCES `Taaskly`.`SERVICE` (`Service_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_BOOKING_2`
    FOREIGN KEY (`User_Id`)
    REFERENCES `Taaskly`.`USER` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE `Taaskly`.`ORDER` (
  `Order_Id` INT NOT NULL,
  `Shop_Id` INT NOT NULL,
  `User_Id` INT NOT NULL,
  `Order_Cost` DOUBLE NOT NULL,
  PRIMARY KEY (`Order_Id`),
  INDEX `fk_ORDER_1_idx` (`Shop_Id` ASC) VISIBLE,
  INDEX `fk_ORDER_2_idx` (`User_Id` ASC) VISIBLE,
  CONSTRAINT `fk_ORDER_1`
    FOREIGN KEY (`Shop_Id`)
    REFERENCES `Taaskly`.`SHOP` (`Shop_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ORDER_2`
    FOREIGN KEY (`User_Id`)
    REFERENCES `Taaskly`.`USER` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE `Taaskly`.`ORDER_LINE` (
  `Order_Line_Id` INT NOT NULL,
  `Order_Id` INT NOT NULL,
  `Item_Id` INT NOT NULL,
  `Order_Line_Item_Quantity` INT NOT NULL,
  `Order_Line_Cost` DOUBLE NOT NULL,
  PRIMARY KEY (`Order_Line_Id`),
  INDEX `fk_ORDER_LINE_1_idx` (`Order_Id` ASC) VISIBLE,
  INDEX `fk_ORDER_LINE_2_idx` (`Item_Id` ASC) VISIBLE,
  CONSTRAINT `fk_ORDER_LINE_1`
    FOREIGN KEY (`Order_Id`)
    REFERENCES `Taaskly`.`ORDER` (`Order_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ORDER_LINE_2`
    FOREIGN KEY (`Item_Id`)
    REFERENCES `Taaskly`.`ITEM` (`Item_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE `Taaskly`.`PAYMENT` (
  `Payment_Id` INT NOT NULL,
  `User_Id` INT NOT NULL,
  `Order_Id` INT NULL,
  `Booking_Id` INT NULL,
  `Payment_Amount` DOUBLE NOT NULL,
  `Payment_Reference` CHAR(25) NOT NULL,
  `Payment_Type` CHAR(25) NOT NULL,
  `Payment_Timestamp` DATETIME NOT NULL,
  PRIMARY KEY (`Payment_Id`),
  INDEX `fk_PAYMENT_1_idx` (`User_Id` ASC) VISIBLE,
  INDEX `fk_PAYMENT_2_idx` (`Order_Id` ASC) VISIBLE,
  INDEX `fk_PAYMENT_3_idx` (`Booking_Id` ASC) VISIBLE,
  CONSTRAINT `fk_PAYMENT_1`
    FOREIGN KEY (`User_Id`)
    REFERENCES `Taaskly`.`USER` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PAYMENT_2`
    FOREIGN KEY (`Order_Id`)
    REFERENCES `Taaskly`.`ORDER` (`Order_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PAYMENT_3`
    FOREIGN KEY (`Booking_Id`)
    REFERENCES `Taaskly`.`BOOKING` (`Booking_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
