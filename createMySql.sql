CREATE DATABASE  IF NOT EXISTS `library_app` /*!40100 DEFAULT CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `library_app`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: library_app
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `kiralanan`
--

DROP TABLE IF EXISTS `kiralanan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kiralanan` (
  `KiralananID` int NOT NULL AUTO_INCREMENT,
  `KitapID` int DEFAULT NULL,
  `KullaniciID` int DEFAULT NULL,
  `AlisTarihi` date DEFAULT NULL,
  `IadeTarihi` date DEFAULT NULL,
  `GeriGetirmeTarihi` date DEFAULT NULL,
  PRIMARY KEY (`KiralananID`),
  KEY `KitapID` (`KitapID`),
  KEY `KullaniciID` (`KullaniciID`),
  CONSTRAINT `kiralanan_ibfk_1` FOREIGN KEY (`KitapID`) REFERENCES `kitaplar` (`KitapID`),
  CONSTRAINT `kiralanan_ibfk_2` FOREIGN KEY (`KullaniciID`) REFERENCES `kullanicilar` (`KullaniciID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kiralanan`
--

LOCK TABLES `kiralanan` WRITE;
/*!40000 ALTER TABLE `kiralanan` DISABLE KEYS */;
/*!40000 ALTER TABLE `kiralanan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `kitaplar`
--

DROP TABLE IF EXISTS `kitaplar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kitaplar` (
  `KitapID` int NOT NULL AUTO_INCREMENT,
  `Baslik` varchar(255) COLLATE utf8mb3_unicode_ci NOT NULL,
  `Yazar` varchar(255) COLLATE utf8mb3_unicode_ci NOT NULL,
  `YayinYili` int NOT NULL,
  `Tur` varchar(50) COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `ISBN` varchar(20) COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `RafNumarasi` varchar(20) COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `Durum` varchar(50) COLLATE utf8mb3_unicode_ci NOT NULL DEFAULT 'Mevcut',
  PRIMARY KEY (`KitapID`),
  UNIQUE KEY `ISBN` (`ISBN`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kitaplar`
--

LOCK TABLES `kitaplar` WRITE;
/*!40000 ALTER TABLE `kitaplar` DISABLE KEYS */;
INSERT INTO `kitaplar` VALUES 
(1,'Sefiller','Victor Hugo',1862,'Roman','978-0140444308','A1','Mevcut'),
(2,'Yüzyıllık Yalnızlık','Gabriel Garcia Marquez',1967,'Roman','978-0060883287','B2','Mevcut'),
(3,'Suç ve Ceza','Fyodor Dostoyevski',1866,'Roman','978-0486415871','C3','Mevcut'),
(4,'Kürk Mantolu Madonna','Sabahattin Ali',1943,'Roman','978-9750729289','D4','Mevcut'),
(5,'Fahrenheit 451','Ray Bradbury',1953,'Bilim Kurgu','978-1451673319','E5','Mevcut'),
(6,'Harry Potter ve Felsefe Taşı','J.K. Rowling',1997,'Fantastik','978-0747532699','F6','Mevcut'),
(7,'Zamanın Kısa Tarihi','Stephen Hawking',1988,'Bilim','978-0553380163','G7','Mevcut'),
(8,'Martı Jonathan Livingston','Richard Bach',1970,'Felsefe','978-0684846842','H8','Mevcut'),
(9,'Bülbülü Öldürmek','Harper Lee',1960,'Roman','978-0061120084','I9','Mevcut'),
(10,'1984','George Orwell',1949,'Distopya','978-0451524935','J10','Mevcut'),
(11,'Hayvan Çiftliği','George Orwell',1945,'Distopya','978-0451526342','K11','Mevcut'),
(12,'Uçurtma Avcısı','Khaled Hosseini',2003,'Roman','978-1594631931','L12','Mevcut'),
(13,'Da Vinci Şifresi','Dan Brown',2003,'Gerilim','978-0307474278','M13','Mevcut'),
(14,'Alkimist','Paulo Coelho',1988,'Roman','978-0062315007','N14','Mevcut'),
(15,'Hobbit','J.R.R. Tolkien',1937,'Fantastik','978-0547928227','O15','Mevcut');
/*!40000 ALTER TABLE `kitaplar` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `kullanicilar`
--

DROP TABLE IF EXISTS `kullanicilar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kullanicilar` (
  `KullaniciID` int NOT NULL AUTO_INCREMENT,
  `Adi` varchar(255) COLLATE utf8mb3_unicode_ci NOT NULL,
  `Soyadi` varchar(255) COLLATE utf8mb3_unicode_ci NOT NULL,
  `Eposta` varchar(255) COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `KullaniciAdi` varchar(50) COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `Sifre` varchar(255) COLLATE utf8mb3_unicode_ci NOT NULL,
  `Rol` varchar(50) COLLATE utf8mb3_unicode_ci NOT NULL DEFAULT 'Kullanıcı',
  PRIMARY KEY (`KullaniciID`),
  UNIQUE KEY `Eposta` (`Eposta`),
  UNIQUE KEY `KullaniciAdi` (`KullaniciAdi`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kullanicilar`
--

LOCK TABLES `kullanicilar` WRITE;
/*!40000 ALTER TABLE `kullanicilar` DISABLE KEYS */;
INSERT INTO `kullanicilar` VALUES 
(1,'Ahmet','Yılmaz','ahmetyilmaz@example.com','ahmetyilmaz','$2a$11$Dq3urm2YGMhgkbnCNgYrLuU.CVXjhZYDo6eC/4t75QmkdOPFw1/n6','Kullanıcı'),
(2,'Mehmet','Özdemir','mehmetozdemir@example.com','mehmetozdemir','$2a$11$GuzYMB9rsNx2j5Fr/CqDEOhCHmruVPwjviYUSMbFR8nXGYsjGNmza','Test'),
(3,'Elif','Kaya','elifkaya@example.com','elifkaya','$2a$11$inuMCYaSjDs34i/ltThyAuYtdhu4HFo1dYM5yBz2daWGJj4SDLjZu','Kullanıcı'),
(4,'Zeynep','Demir','zeynepdemir@example.com','zeynepdemir','$2a$11$cVLkhKafSkS6ONCEZDUzpOWY5W8gCTwf.bpwOX3ggGFMvLJpan7.y','Kullanıcı'),
(5,'Emre','Can','emrecan@example.com','emrecan','$2a$11$CsFzro.2WoUV5MiZM4z4ZOUugch9F7r7d9YzE6vuoelPRHgRO2ms6','Kullanıcı'),
(6,'Yusuf','Kaplan','yusufkaplan@example.com','yusufkaplan','$2a$11$KgazvPgvWA2pDOuPzlMDjubPhIXMHjdBXxJPBWlvlwSnfieSLiWQq','Kullanıcı'),
(7,'Selin','Çetin','selincetin@example.com','selincetin','$2a$11$v7tLcrBoAwy5IAvre0yte.QXwsqd0rlk0V01uZ3z4E0Y8e9KDo.RK','Kullanıcı'),
(8,'Canan','Erbil','cananerbil@example.com','cananerbil','$2a$11$rtMQHtXQJustLW0srgQ8cONa.5MbyRshvKOt4iM0xd2LGPKVxnV/e','Kullanıcı'),
(9,'Burak','Yıldırım','burakyildirim@example.com','burakyildirim','$2a$11$VNvWaJqMCegkTW.W5i1K9.kzHgRthep5lqa9CM7ATcjlA3r4ZuJGG','Kullanıcı'),
(10,'Derya','Uluğ','deryaulug@example.com','deryaulug','$2a$11$dpKu1Em7XxOSEQ0ab3ilDuPnSaNXDH2MTphWccwzee0e84wKWnWlG','Kullanıcı'),
(11,'Oğuz','Kara','oguzkara@example.com','oguzkara','$2a$11$b5O6Bjuqr0i6AJgYjhGc7.u.SDjXlOj794m.IktoT6Co6UNHnfiD2','Kullanıcı'),
(12,'Sıla','Genc','silagenc@example.com','silagenc','$2a$11$Mm5JjZd03i4gwz0bysrqIO6v1sl6Q5xJuKT/6dVqpK60cn9ZOBUXC','Kullanıcı'),
(13,'Fatih','Sultan','fatihultan@example.com','fatihultan','$2a$11$1mBUXlaeuM4VDoaLS1lq7e8V.T8WLdKf51KHa4FFqn6eO460fgU1W','Kullanıcı'),
(14,'Ebru','Gündeş','ebrugundes@example.com','ebrugundes','$2a$11$f8jVtNZp2NuU71XXs4oQ7.ijg20Rn29CdtxKTDSQYcTWpwv7ZxpRK','Kullanıcı'),
(15,'Ali','Veli','aliveli@example.com','aliveli','$2a$11$ZZeSv97SRFxIPFJjVAIHgOP6SJftAGXdI9Qin9rHilwVshFWHMZPG','Kullanıcı'),
(20,'Mert','Denizci','mertdenizci@example.com','mertdenizci','$2a$11$zQaHObK2bg56ze0/tbX0pePf4Zs6APpoXxknbM8imdBw.MZcSdDsG','Yönetici');
/*!40000 ALTER TABLE `kullanicilar` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-03-31  6:18:35
