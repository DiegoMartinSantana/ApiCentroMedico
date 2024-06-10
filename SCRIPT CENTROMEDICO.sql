CREATE DATABASE CENTROMEDICO
GO
USE CENTROMEDICO
GO
SET DATEFORMAT dmy;
GO
CREATE TABLE OBRAS_SOCIALES(
  IDOBRASOCIAL INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  NOMBRE VARCHAR(50) NOT NULL,
  COBERTURA DECIMAL (10, 2) NOT NULL CHECK (COBERTURA BETWEEN 0.00 AND 1.00) -- 0 (SIN DESCUENTO), 0.5 (50% DTO), 1 (100% DESCUENTO), ETC.
)
GO
CREATE TABLE PACIENTES(
  IDPACIENTE BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
  APELLIDO VARCHAR(50) COLLATE Latin1_general_CI_AI NOT NULL,
  NOMBRE VARCHAR(50) COLLATE Latin1_general_CI_AI NOT NULL,
  IDOBRASOCIAL INT NULL FOREIGN KEY REFERENCES OBRAS_SOCIALES(IDOBRASOCIAL),
  FECHANAC DATE NOT NULL,
  SEXO CHAR NOT NULL CHECK (SEXO IN ('M', 'F')),
 
)
GO
CREATE TABLE PERMISOS (
IDPERMISO INT PRIMARY KEY IDENTITY(1,1),
NOMBRE VARCHAR(60) NOT NULL
)
GO
CREATE TABLE USUARIOS (
IdPaciente BigInt  ,
CONSTRAINT Fk_Paciente_Usuario FOREIGN KEY (IdPaciente) REFERENCES PACIENTES (IdPaciente),
Email varchar(200) not null PRIMARY KEY,
Pass Varchar(50) not null,
IdPermiso int not null ,
 CONSTRAINT FK_Permisos FOREIGN KEY (IdPermiso) REFERENCES PERMISOS (IdPermiso),
IdMedico int not null ,
CONSTRAINT FK_MEDICOS_USUARIO FOREIGN KEY (IdMedico) REFERENCES Medicos(IdMedico)
)



GO
CREATE TABLE ESPECIALIDADES(
  IDESPECIALIDAD INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  NOMBRE VARCHAR(50) COLLATE Latin1_general_CI_AI NOT NULL 
)
GO
CREATE TABLE MEDICOS(
  IDMEDICO BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
  IDESPECIALIDAD INT NOT NULL FOREIGN KEY REFERENCES ESPECIALIDADES (IDESPECIALIDAD),
  APELLIDO VARCHAR(50) NOT NULL,
  NOMBRE VARCHAR(50) NOT NULL,
  SEXO CHAR NOT NULL CHECK (SEXO IN ('M', 'F')),
  FECHANAC DATE NOT NULL,
  FECHAINGRESO DATE NOT NULL,
  COSTO_CONSULTA MONEY NOT NULL CHECK (COSTO_CONSULTA >= 0),
  DNI INT NOT NULL UNIQUE
 
)
GO
CREATE TABLE TURNOS(
  IDTURNO BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
  FECHAHORA DATETIME NOT NULL,
  IDMEDICO BIGINT NOT NULL FOREIGN KEY REFERENCES MEDICOS (IDMEDICO),
  IDPACIENTE BIGINT NOT NULL FOREIGN KEY REFERENCES PACIENTES (IDPACIENTE),
  DURACION INT NOT NULL CHECK (DURACION > 0) -- EN MINUTOS 
)


/* PERMISOS */
/* PERMISOS */

insert into permisos(NOMBRE) VALUES ('PACIENTE'), ('MEDICO') ,('ADMINISTRADOR')
/* USUARIOS */
/* USUARIOS */
insert into USUARIOS(IdPaciente,Email,pass,IdPermiso) values 
(1,'user@user.com','user',1),(null,'medico@medico.com','medico',2),(null,'admin@admin.com','admin',3) 
/* OBRAS SOCIALES */
/* OBRAS SOCIALES */
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('PAMI', 0.7)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('GALENO', 0.5)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('OSDE', 0.5)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('DASUTEN', 0.5)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('SWISS MEDICAL', 0.6)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('UTHGRA', 0.4)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('OSDEPYM', 0.5)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('IOMA', 0.4)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('OSCHOCA', 0.6)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('OSPACA', 0.4)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('CALABUIG HEALTH', 0.2)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('KLOSTER MEDICAL', 0.2)
INSERT INTO OBRAS_SOCIALES (NOMBRE, COBERTURA) VALUES ('SQL MEDICAL', 1)

/* ESPECIALIDADES */
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('MÉDICO CLÍNICO')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('CARDIOLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('GASTROENTEROLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('PEDIATRÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('ENDOCRINOLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('ODONTOLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('NEUROLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('NEUMOLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('NUTRICIÓN')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('OFTALMOLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('ONCOLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('PSIQUIATRÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('REUMATOLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('UROLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('PROCTOLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('DERMATOLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('CIRUGÍA GENERAL')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('ANÁLISIS CLÍNICOS')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('ANATOMÍA PATOLÓGICA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('MICROBIOLOGÍA Y PARASITOLOGÍA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('GENÉTICA MÉDICA')
INSERT INTO ESPECIALIDADES (NOMBRE) VALUES ('MEDICINA NUCLEAR')




/* PACIENTES */

INSERT INTO PACIENTES(APELLIDO,NOMBRE,IDOBRASOCIAL,FECHANAC,SEXO) 
VALUES 
('Conte','Federica',13,'13/03/1961','M'),
('Farina','Kristopher',10,'19/10/2004','F'),
('Paredes','Rebecca',7,'20/12/1985','M'),
('De Angelis','Riccardo',5,'20/06/1958','M'),
('Riquelme','Erika',13,'14/06/1953','M'),
('Lagos','Fabio',2,'14/02/2001','M'),
('Ferretti','Alejandro',3,'17/04/1949','M'),
('Greco','Sofia',11,'03/05/1959','M'),
('Valenzuela','Sara',3,'17/01/1950','F'),
('Maldonado','Alex',4,'24/03/1994','M'),
('Longo','Enrico',13,'09/08/1997','M'),
('Paredes','Helen',3,'29/03/1988','F'),
('González','Paolo',9,'06/10/2002','F'),
('Salazar','Angela',11,'05/01/1968','M'),
('Rizzo','Yarela',6,'17/10/1956','F'),
('Palma','Marco',13,'29/12/1971','F'),
('Cavallo','Joakin',4,'12/10/1967','F'),
('Costantini','Gianluca',4,'22/05/1966','F'),
('Ceccarelli','Angelo',13,'10/09/1961','F'),
('Henríquez','Alessandra',10,'15/07/1993','M'),
('Olivares','Giancarlo',12,'05/11/1956','F'),
('Proietti','Andy',8,'15/04/2009','M'),
('Costa','Lara',8,'17/07/1975','F'),
('Espinoza','Daniela',12,'03/07/1966','M'),
('Mele','Miriam',9,'25/07/1993','F'),
('Sánchez','Martinna',10,'08/10/1989','F'),
('Morales','Cristiano',13,'06/06/1994','F'),
('Pérez','Mattia',9,'01/04/1966','F'),
--
('Silva','Anna',4,'13/07/1982','M'),
('García','Genaro',7,'10/09/1958','M'),
('Montanari','Davor',3,'03/06/2006','F'),
('Alvarado','Felipe',13,'13/04/1960','F'),
('Lombardo','Melany',10,'07/12/2005','M'),
('Herrera','Dastin',3,'14/12/1948','F'),
('Bernardi','Alice',13,'21/02/1945','M'),
('Proietti','Beatrice',6,'10/03/2009','F'),
('Araya','Viola',3,'10/04/1948','M'),
('Morales','Gianni',6,'15/12/1946','F'),
('Sanna','Mario',10,'10/02/1981','F'),
('De Angelis','Sarah',6,'01/01/1947','M'),
('Giordano','Leonor',12,'01/03/1956','M'),
('Silvestri','Nicolò',6,'15/01/2006','F'),
('Martínez','Alessio',2,'26/01/1984','F'),
('Donoso','Beatrice',11,'12/12/2006','F'),
('Escobar','Francesco',13,'08/07/1982','M'),
('Silva','Arianna',8,'07/09/1976','M'),
('Ortega','Valerio',13,'10/05/2005','F'),
('Bustos','Armando',7,'30/01/2010','M'),
('Morales','Roger',1,'08/10/1963','M'),
('Sanhueza','Paola',4,'25/12/1976','M'),
('Peña','Alfredo',9,'04/03/1980','M'),
('Peña','Lidia',8,'25/05/1951','M'),
('García','Margherita',10,'31/08/1994','F'),
('Figueroa','Flor',13,'11/12/1994','M'),
('Sandoval','Ema',3,'16/09/1954','M'),
('Ricciardi','Matilde',8,'21/11/1965','F'),
('Serra','Giacomo',13,'27/06/1950','M'),
('Carrasco','Greta',8,'06/12/1977','M'),
('Basile','Ainara',12,'14/01/1976','M'),
('Lombardo','Ginevra',8,'21/12/1970','M'),
('Greco','Alessia',13,'14/05/1960','M'),
('Henríquez','Junior',7,'08/09/2003','M'),
('Bravo','Gaia',6,'19/12/1984','F'),
('Leone','Cindy',5,'03/10/1976','M'),
('Herrera','Jhonatan',3,'02/08/1992','F'),
('De Rosa','Camilla',4,'01/03/1955','M'),
('López','Roberto',4,'28/05/1996','M'),
('Pino','León',5,'24/05/1987','M'),
('Valente','Monica',13,'19/08/2010','M'),
('Castillo','Estrella',10,'25/09/1948','F'),
('Bustamante','Lara',8,'17/12/1978','M'),
('Ortega','Mijael',6,'24/09/1950','M'),
('Riz

zi','Alice',7,'18/03/1963','F'),
('San Martín','Domenico',8,'14/03/1969','F'),
('Espinoza','Aymara',4,'31/01/2005','M'),
('Sandoval','Simone',4,'13/07/1984','F'),
('Pérez','Vicente',10,'14/07/1997','M'),
('Messina','Ginevra',2,'28/03/1996','F'),
('García','Jasmín',12,'12/09/2007','M'),
('Conti','Nicolò',9,'10/01/1947','M'),
('Giorgi','Alessandro',5,'25/10/1997','M'),
('Valdés','Maite',13,'21/08/1951','M'),
('Longo','Ivanna',4,'06/01/1997','M'),
('Peña','Axcel',5,'23/09/1971','M'),
('Guzmán','Andrea',8,'08/05/1985','F'),
('Parra','Blanca',12,'27/03/1960','F'),
('Arena','Alain',4,'20/10/1979','F'),
('López','Josue',9,'08/04/1996','M'),
('Rodríguez','Francia',7,'13/11/1987','F'),
('Ruiz','Samuele',1,'18/07/1960','M'),
('Giuliani','Caterina',4,'19/11/1960','M'),
('Calabrese','Filippo',4,'08/11/1984','M'),
('Campos','Evans',2,'26/06/1993','M'),
('Soto','Gabriel',6,'17/08/1947','M'),
('Silvestri','Luca',6,'26/05/1996','F'),
('Lagos','Ismael',7,'10/11/1952','M'),
('Parra','Mayra',8,'06/04/1958','F'),
('Henríquez','Adiel',2,'09/09/1955','F'),
('Poblete','Thyare',4,'06/03/1954','M'),
('Jiménez','Anahy',7,'19/05/2008','F'),
('Riquelme','Noemi',6,'23/12/1950','M'),
('Rodríguez','Josepha',11,'26/10/1993','M'),
('Rizzo','Caroline',6,'22/05/2000','M'),
('Zúñiga','Mirko',5,'09/04/1973','F'),
('Ruggiero','Rodolfo',2,'14/03/1994','M'),
('Fumagalli','Christofer',2,'17/03/2002','M'),
('Ricciardi','Randy',2,'09/02/1973','F'),
('Colombo','Yesenia',12,'14/01/1983','M'),
('Montanari','Nicoletta',3,'05/01/1952','M'),
('Cortés','Ademir',10,'22/03/1980','F'),
('Cavallo','Linda',1,'02/04/1974','M'),
('Fumagalli','Dalia',10,'13/04/1968','F'),
('Carrasco','Teresa',12,'30/05/1950','F'),
('Catalano','Adán',5,'08/02/2005','M'),
('Gutiérrez','Cristian',1,'15/04/2005','F'),
('Henríquez','Sara',4,'25/07/1981','M'),
('Guerra','Vittoria',1,'27/11/1992','M'),
('Vera','Bryam',10,'15/06/1975','M'),
('Longo','Nicoletta',12,'25/03/1965','F'),
('Piccolo','Emma',3,'20/05/1967','F'),
('Alvarado','Sara',2,'09/08/1979','F'),
('Carrasco','Stefano',10,'11/09/1945','M'),
('Napolitano','Viola',13,'13/06/1952','M'),
('Riva','Mariano',9,'05/10/1988','F'),
('Conti','Abram',10,'17/01/2004','M'),
('Vásquez','Christian',7,'09/03/1988','M'),
('Sorrentino','José',12,'12/11/1986','M'),
('Galli','Lucio',3,'20/09/1988','M'),
('Guzmán','Erika',5,'16/09/1978','F'),
('Maldonado','Riccardo',7,'09/10/1948','F'),
('Morales','Bautista',12,'29/01/1958','M'),
('Reyes','Aldo',4,'08/08/1974','M'),
('Valentini','Jastin',6,'22/08/1945','F'),
('Sáez','Alex',9,'08/09/2000','M'),
('Cavallo','Pascalle',3,'26/07/1959','M'),
('Vergara','Randy',6,'09/05/1986','M'),
('Mariani','Bayron',3,'07/12/1998','F'),
('Milani','Pascuala',11,'10/02/1945','F'),
('Testa','Ilaria',13,'20/06/1956','M'),
('Sepúlveda','Salvatore',13,'22/11/1994','M'),
('Fiore','Yeison',12,'29/08/1961','F'),
('Poblete','Federica',1,'01/03/2007','F'),
('Rodríguez','Miriam',6,'27/05/1966','M'),
('Salinas','Dario',3,'21/10/1976','F'),
('Marino','Arianna',2,'24/05/1961','F'),
('Castelli','Gisselle',6,'10/01/1991','F'),
('Sánchez','Alberto',10,'20/11/1995','F'),
('Milani','Alessia',11,'04/06/1970','M'),
('Venegas','Lionel',4,'16/11/1989','M'),
('Cattaneo','Romina',8,'20/08/1971','F'),
('Serra','Fredy',7,'06/10/2006','M'),
('Guzmán','Manuela',11,'23/09/2005','F'),


('Sanhueza','Ociel',12,'06/04/2004','M'),
('Moreno','Giancarlo',11,'24/09/1996','M'),
('Reyes','Naiara',1,'12/11/1955','M'),
('Cáceres','Jasmín',1,'11/11/1970','M'),
('Moro','Silvia',13,'10/02/1970','F'),
('Ferretti','Eleazar',8,'02/11/1968','M'),
('González','Lucia',3,'16/11/1959','M'),
('Serra','Noemi',9,'14/09/1957','F'),
('Moreno','Gastón',1,'11/12/1981','M'),
('Moreno','Mathias',8,'10/03/1989','F'),
('Cattaneo','Anahís',10,'10/10/2002','M'),
('Ruggiero','Nayara',11,'27/07/2002','M'),
('Morales','Nahuel',4,'08/08/2006','M'),
('Riva','Armin',11,'08/10/1956','M'),
('Conti','Lucio',3,'14/10/1975','F'),
('Gutiérrez','Ginevra',6,'21/07/1955','M'),
('Salinas','Karim',2,'11/05/1993','M'),
('Ferri','Betzabeth',12,'20/10/1951','F'),
('Fabbri','Samuele',9,'06/10/1963','M'),
('Valentini','Beatrice',6,'07/01/1997','M'),
('Meloni','Adriana',3,'19/02/1957','M'),
('De Santis','Robin',4,'03/11/1961','M'),
('Mazza','Nicolò',10,'29/07/1960','F'),
('Navarro','Daniele',2,'19/12/1979','F'),
('Sanhueza','Beatrice',3,'19/09/1957','M'),
('Contreras','Konstanza',4,'04/03/1945','M'),
('Barone','Samuele',5,'26/12/1985','M'),
('Hernández','Giorgio',13,'12/06/1992','M'),
('Donoso','Mónica',3,'25/12/1954','F'),
('Bianco','Maylin',2,'03/11/1989','F'),
('Pérez','Sean',5,'11/09/1977','M'),
('Medina','Giuseppe',13,'14/11/1996','M'),
('Soto','Gerson',3,'16/12/2006','M'),
('Fumagalli','Axl',5,'03/07/2008','F'),
('Leone','Marta',8,'09/06/1983','M'),
('Cáceres','Marcello',10,'06/05/1962','M'),
('Piazza','Alessia',2,'08/11/1987','F'),
('Monti','Luigi',4,'23/11/1947','F'),
('Valentini','Kilian',4,'16/04/1990','F'),
('Ramírez','Soraya',5,'07/06/1948','M'),
('Conte','Federica',13,'13/03/1961','M'),
('Farina','Kristopher',10,'19/10/2004','F'),
('Paredes','Rebecca',7,'20/12/1985','M'),
('De Angelis','Riccardo',5,'20/06/1958','M'),
('Riquelme','Erika',13,'14/06/1953','M'),
('Lagos','Fabio',2,'14/02/2001','M'),
('Ferretti','Alejandro',3,'17/04/1949','M'),
('Greco','Sofia',11,'03/05/1959','M'),
('Valenzuela','Sara',3,'17/01/1950','F');


/* MEDICOS */

INSERT INTO MEDICOS([APELLIDO],[NOMBRE],[SEXO],[FECHANAC],[FECHAINGRESO],[COSTO_CONSULTA],[IDESPECIALIDAD],[DNI]) VALUES
('Owen','Elijah','F','13/12/1983','11/05/2011',1225,1),
('Medina','Uriel','F','29/11/1979','18/05/2001',1283,17),
('Flynn','Charity','F','08/12/1976','09/11/2009',466,20),
('Grimes','Kai','M','18/12/1985','11/04/2006',743,9),
('Salazar','Angela','F','02/02/1984','07/12/2016',697,20),
('Wood','Dacey','F','06/06/1951','25/11/2015',347,11),
('Vaughn','Slade','M','22/01/1977','01/09/2013',1570,11),
('Cash','Caleb','F','05/01/1976','18/03/2004',588,12),
('Jefferson','Zephr','M','29/11/1966','30/12/2002',1035,17),
('Vang','Price','F','07/01/1958','01/04/2013',512,2),
('Bruce','Ethan','F','22/03/1975','26/11/2007',1283,22),
('Castillo','Levi','M','16/12/1983','25/06/2002',1223,16),
('Jefferson','Jael','M','12/04/1953','05/07/2002',374,10),
('Pate','Martina','M','17/05/1946','03/06/1995',366,16),
('Palmer','Sage','M','25/01/1947','14/05/1997',325,16),
('Lucas','James','M','10/04/1988','12/09/2001',1043,15),
('Kirby','Quemby','F','01/08/1973','29/03/2007',289,19),
('Cannon','Melanie','M','28/11/1946','15/03/2004',960,8),
('Maddox','James','M','10/09/1961','19/12/1999',1410,22),
('Hale','Nomlanga','M','25/11/1959','23/01/1999',1431,4),
('Mejia','Ora','M','21/11/1961','08/12/1995',1505,19),
('Farrell','Porter','M','15/11/1978','15/07/2015',1135,7),
('Obrien','Zelda','M','31/12/1986','20/04/2013',1009,17),
('Avery','Idola','M','20/10/1977','01/11/2014',822,3),
('Miranda','Kamal','M','03/07/1953','08/12/2014',1087,4),
('Whitfield','Cara','F','07/12/1988','02/10/2015',1251,14),
('Delacruz','Signe','M','04/11/1960','12/02/2000',1391,12),
('Little','Jescie','M','28/06/1947','10/07/1995',1254,13),
('Rosales','Kamal','F','18/05/1961','29/11/2002',161,8),
('Ferrell','David','F','04/10/1966','16/02/2000',1536,17),
('Hood','Rhea','F','28/07/1963','31/08/2003',280,16),
('Hunter','Daryl','M','19/12/1965','22/03/1997',822,20),
('Bridges','Iris','M','14/12/1985','29/05/2003',1348,21),
('Bishop','Reece','F','06/02/1958','21/11/2005',855,13),
('Pennington','Bell','F','19/04/1973','11/01/1996',1348,15),
('Sykes','Barrett','M','21/06/1974','06/12/2014',1440,5),
('Kim','Kareem','M','24/04/1983','13/12/1994',1221,22),
('Jenkins','Lars','F','01/10/1963','08/02/2002',963,7),
('Barr','Ariana','F','01/04/1981','06/02/2004',437,10),
('Ashley','Chanda','M','03/10/1965','05/05/2007',1532,16),
('Rice','Flavia','F','04/09/1986','23/11/2007',1457,5),
('Kelley','Jessica','F','09/06/1983','10/06/1998',505,9),
('Martinez','Isadora','F','14/09/1958','28/02/2000',949,13),
('Lara','Alfreda','M','16/05/1981','06/03/2001',413,22),
('Cannon','Stacey','F','16/08/1970','22/06/1996',1402,10),
('Russell','Laith','F','06/03/1969','28/09/1996',993,1),
('Berry','Audra','F','14/02/1977','09/04/2014',270,2),
('Browning','Astra','F','02/06/1984','07/11/1998',1214,15),
('Odom','Kuame','M','08/08/1990','30/04/2005',1411,12),
('Craig','Rhona','F','02/01/1966','03/04/2014',1278,17),
('English','Jonah','M','16/05/1973','12/03/2014',1018,16),
('Hodge','Neve','F','21/10/1947','16/06/2016',938,13),
('Murray','Chantale','M','03/06/1956','22/11/2002',1441,2),
('Travis','September','M','09/03/1954','18/06/2003',1305,11),
('Torres','Angela','F','19/05/1953','25/10/2009',177,21),
('Herring','Janna','F','15/08/1982','23/10/2012',899,16),
('Levine','Nomlanga','F','24/09/1985','10/11/2005',1491,21),
('Hardy','Conan','M','27/08/1975','18/02/2015',360,13),
('Berger','Jayme','F','26/05/1952','17/09/2011',1386,18),
('Petersen','Miranda','M','31/01/1962','04/09/1999',292,7),
('Simon','Knox','F','26/05/1957','14/01/2002',841,14),
('Flores','Murphy','F','26/08/1950','02/11/2002',260,22),
('Gardner','April','F','03/11/1982','18/08/2013',238,19),
('Griffin','Kim','F','25/01/1974','13/10/2011',598,14),
('Pittman','Hilda','M','28/01/1973','28/06/2015',1496,4),
('Parrish','Yen','M','18/03/1979','02/11/2002',649,8),
('Dixon','Byron','F','13/10/1951','30/03/2004',1360,14),
('Yang','Barrett','F','23/10/1968','18/06/2016',1557,7),
('Orr','Aileen','M','11/11/1949','07/02/2013',1089,9),
('Middleton','Hyacinth','F','10/01/1963','23/05/2004',1384,1),
('Hayes','Maxwell','F','04/09/1957','24/10/1994',327,1),
('Hall','Chantale','F','28/07/1947','01/09/2009',634,17),
('Warner','Denton','M','30/11/1952','23/03/2014',1513,16),
('Santos','Jennifer','M','19/11/1961','16/05/2005',350,9),
('Herrera','Melanie','M','11/12/1972','11/09/2005',785,10),
('Booker','Kamal','M','15/05/1959','01/02/2011',1358,21),
('Bright','Melissa','M','19/01/1983','29/08/2000',1322,12),
('Lancaster','Tashya','F','19/05/1990','22/04/1997',1524,16),
('Briggs','Leilani','M','22/08/1989','19/12/2005',358,18),
('Randall','Avram','F','04/06/1963','14/11/1996',1598,16),
('Cohen','Justine','F','18/04/1954','31/05/1997',1545,3),
('Fischer','Evan','M','24/11/1973','27/07/2011',674,19),
('Rasmussen','Winter','M','12/03/1964','29/03/2012',1345,18),
('Gallagher','Dawn','F','13/07/1975','02/02/2016',375,5),
('Spencer','Elmo','F','21/03/1956','10/11/1997',262,15),
('Shepherd','Pamela','M','30/09/1971','12/07/1995',701,10),
('Doyle','Beau','M','20/09/1987','18/05/2016',420,2),
('Summers','Camille','M','27/01/1968','18/08/2013',483,22),
('Luna','Cara','M','22/02/1991','14/02/1997',1559,19),
('Gray','Timon','M','28/05/1970','20/11/1999',1476,12),
('Caldwell','Kasimir','F','10/11/1955','09/09/1999',1204,19),
('Fischer','Hakeem','M','12/02/1964','01/09/1996',589,15),
('Haney','Kathleen','M','14/04/1958','18/05/2011',1036,17),
('Bridges','Dakota','M','14/10/1950','12/04/1995',235,12),
('Mckay','Evelyn','F','12/10/1979','25/02/2000',834,13),
('Hudson','Tarik','F','06/09/1984','02/04/1998',1299,3),
('Fletcher','Rebecca','F','18/12/1954','09/04/1997',1113,11),
('Guerra','Robin','F','25/08/1964','28/12/2010',1227,6),
('Ortiz','Cailin','M','11/06/1978','09/10/1995',1550,18),
('Shannon','Lisandra','F','27/12/1945','06/01/1995',1521,11);

set dateformat 'ymd'
/* TURNOS */
INSERT INTO TURNOS (FECHAHORA,IDMEDICO,IDPACIENTE,DURACION) VALUES
	 ('2012-09-08 07:38:00.0',93,131,73),
	 ('2002-04-18 18:52:00.0',55,84,36),
	 ('2010-09-04 23:28:00.0',79,45,19),
	 ('2002-01-08 21:35:00.0',97,165,42),
	 ('2019-05-17 12:10:00.0',7,75,58),
	 ('2001-08-12 15:52:00.0',37,22,45),
	 ('1997-02-15 12:13:00.0',39,109,36),
	 ('2019-02-01 10:43:00.0',64,154,82),
	 ('2006-03-17 15:06:00.0',40,36,48),
	 ('2002-06-26 01:58:00.0',85,38,75),
	 ('1998-09-22 06:47:00.0',11,6,84),
	 ('2013-03-03 02:02:00.0',45,120,19),
	 ('2006-11-15 01:11:00.0',76,200,77),
	 ('2003-11-01 02:42:00.0',13,51,90),
	 ('2003-11-13 07:20:00.0',73,33,86),
	 ('2002-03-27 05:04:00.0',19,143,65),
	 ('2013-08-23 07:36:00.0',91,13,40),
	 ('2013-06-09 11:11:00.0',24,181,27),
	 ('2000-12-18 15:07:00.0',98,150,88),
	 ('2017-08-21 16:38:00.0',9,126,24),
	 ('2008-10-19 15:58:00.0',90,10,74),
	 ('2015-01-11 21:05:00.0',95,33,65),
	 ('2004-05-09 01:36:00.0',61,36,51),
	 ('2003-09-28 13:22:00.0',66,46,62),
	 ('2016-10-07 23:59:00.0',37,46,39),
	 ('1999-12-19 09:10:00.0',7,191,81),
	 ('2018-11-01 01:01:00.0',49,81,59),
	 ('2001-01-08 16:55:00.0',99,192,74),
	 ('2013-05-16 09:59:00.0',43,39,30),
	 ('2010-01-24 00:31:00.0',8,44,28),
	 ('2013-03-23 23:36:00.0',52,50,68),
	 ('2010-02-15 04:55:00.0',18,87,64),
	 ('2009-10-19 20:43:00.0',3,43,78),
	 ('2018-06-19 16:48:00.0',19,81,89),
	 ('2018-11-04 11:25:00.0',31,28,89),
	 ('2019-06-16 18:54:00.0',1,120,43),
	 ('2013-10-10 11:00:00.0',27,3,89),
	 ('2002-04-28 13:53:00.0',92,32,27),
	 ('2012-06-13 18:17:00.0',3,121,50),
	 ('2013-12-13 16:36:00.0',57,22,49),
	 ('2006-05-27 23:07:00.0',76,158,61),
	 ('2008-06-25 22:27:00.0',50,160,16),
	 ('2019-10-08 10:01:00.0',84,138,75),
	 ('1998-10-07 01:24:00.0',26,164,29),
	 ('2009-02-17 09:01:00.0',2,15,8),
	 ('1999-05-22 08:41:00.0',67,105,59),
	 ('2014-10-01 10:38:00.0',60,39,42),
	 ('2018-07-16 07:50:00.0',18,150,24),
	 ('1997-02-13 14:01:00.0',1,151,77),
	 ('2007-07-21 00:47:00.0',69,76,30),
	 ('2002-01-10 01:20:00.0',53,23,19),
	 ('2012-06-15 05:12:00.0',26,57,21),
	 ('2014-09-13 17:43:00.0',97,90,58),
	 ('2009-11-18 10:14:00.0',28,55,34),
	 ('2011-09-08 06:39:00.0',86,2,10),
	 ('1999-10-08 03:17:00.0',55,95,61),
	 ('1999-03-29 02:59:00.0',29,3,22),
	 ('2018-03-13 19:53:00.0',6,126,70),
	 ('2010-07-24 18:05:00.0',85,81,90),
	 ('1998-07-10 23:55:00.0',29,116,69),
	 ('2010-11-20 05:57:00.0',91,103,36),
	 ('2005-01-17 06:53:00.0',3,200,85),
	 ('2001-01-15 07:18:00.0',70,4,68),
	 ('2013-06-16 02:41:00.0',15,51,62),
	 ('2001-08-28 21:25:00.0',26,102,27),
	 ('2011-03-19 19:05:00.0',32,132,40),
	 ('2014-02-09 08:27:00.0',27,181,13),
	 ('1998-12-13 21:23:00.0',88,11,10),
	 ('2005-02-21 08:57:00.0',3,76,59),
	 ('2019-11-24 07:42:00.0',98,54,81),
	 ('2003-11-16 17:42:00.0',64,148,46),
	 ('1999-01-14 10:15:00.0',25,100,74),
	 ('2004-05-10 18:41:00.0',57,85,28),
	 ('1997-10-27 19:02:00.0',77,20,11),
	 ('2001-07-02 20:07:00.0',85,192,34),
	 ('1997-09-05 01:33:00.0',41,14,78),
	 ('2009-05-05 07:57:00.0',75,23,56),
	 ('2019-02-09 14:21:00.0',67,101,82),
	 ('2004-08-05 11:00:00.0',50,101,69),
	 ('2001-11-06 16:49:00.0',25,87,72),
	 ('2008-02-10 22:03:00.0',7,48,81),
	 ('2010-08-20 05:14:00.0',38,92,67),
	 ('2012-08-13 22:33:00.0',76,62,74),
	 ('2013-06-27 03:26:00.0',45,57,76),
	 ('2002-01-25 18:42:00.0',54,145,51),
	 ('2015-05-07 22:35:00.0',40,137,56),
	 ('2011-02-17 22:42:00.0',35,78,27),
	 ('2003-05-19 19:32:00.0',80,77,59),
	 ('1998-06-10 10:32:00.0',30,145,24),
	 ('2009-10-24 18:32:00.0',29,119,55),
	 ('2008-10-14 06:09:00.0',90,120,27),
	 ('2004-02-21 15:57:00.0',96,74,59),
	 ('2018-02-16 00:38:00.0',4,105,77),
	 ('2005-10-21 06:16:00.0',55,137,73),
	 ('2010-02-18 09:33:00.0',56,155,27),
	 ('1999-02-05 03:29:00.0',78,10,71),
	 ('2008-11-12 20:26:00.0',44,184,38),
	 ('2011-11-18 11:09:00.0',48,89,13),
	 ('2011-05-10 11:54:00.0',25,63,73),
	 ('2009-02-24 07:29:00.0',69,19,33),
	 ('2010-04-24 19:47:00.0',89,98,25),
	 ('1998-04-21 04:29:00.0',35,49,79),
	 ('2001-03-10 17:45:00.0',29,52,73),
	 ('2014-04-23 03:29:00.0',35,17,81),
	 ('2015-02-14 04:32:00.0',23,184,90),
	 ('2003-03-24 08:10:00.0',49,19,67),
	 ('2010-06-13 18:13:00.0',93,60,49),
	 ('2007-06-27 21:34:00.0',57,166,82),
	 ('2011-10-26 07:00:00.0',6,102,32),
	 ('2007-10-21 10:58:00.0',49,93,41),
	 ('1999-01-14 09:09:00.0',58,109,78),
	 ('2010-12-06 03:55:00.0',44,174,49),
	 ('1999-07-22 21:06:00.0',43,165,55),
	 ('2017-05-14 23:53:00.0',65,8,72),
	 ('2009-09-23 03:45:00.0',56,66,19),
	 ('1997-09-13 14:15:00.0',51,39,38),
	 ('2009-01-09 21:15:00.0',14,6,73),
	 ('2009-04-06 08:44:00.0',67,23,60),
	 ('1997-12-10 14:51:00.0',30,146,36),
	 ('1997-07-28 17:43:00.0',34,101,19),
	 ('2010-03-03 23:21:00.0',71,153,35),
	 ('2000-12-14 04:02:00.0',9,84,82),
	 ('2008-03-01 18:22:00.0',42,17,43),
	 ('2003-01-25 03:14:00.0',55,126,73),
	 ('2014-03-07 08:36:00.0',5,143,86),
	 ('2007-08-24 11:05:00.0',71,15,76),
	 ('2018-05-01 06:08:00.0',68,22,45),
	 ('2011-09-27 02:02:00.0',71,18,18),
	 ('2001-10-20 03:20:00.0',72,59,51),
	 ('2017-03-02 11:16:00.0',82,114,80),
	 ('2003-06-09 04:14:00.0',9,14,27),
	 ('2003-09-29 07:39:00.0',52,61,29),
	 ('2016-01-05 06:53:00.0',57,121,67),
	 ('2006-01-14 02:39:00.0',65,2,45),
	 ('1998-09-01 11:23:00.0',65,47,73),
	 ('2012-12-26 00:06:00.0',98,135,51),
	 ('2014-10-06 09:41:00.0',14,131,85),
	 ('2011-10-07 15:46:00.0',71,39,54),
	 ('2016-06-22 02:42:00.0',7,200,67),
	 ('2000-11-23 08:58:00.0',89,123,35),
	 ('2008-11-09 10:24:00.0',48,200,78),
	 ('2010-03-22 03:19:00.0',29,11,41),
	 ('1998-03-16 08:40:00.0',99,143,29),
	 ('2002-08-08 04:43:00.0',25,91,46),
	 ('1997-12-21 08:55:00.0',27,30,68),
	 ('2013-08-16 20:36:00.0',54,31,72),
	 ('2006-12-11 19:46:00.0',80,166,27),
	 ('2019-05-01 03:57:00.0',3,102,51),
	 ('2009-04-21 19:24:00.0',77,6,31),
	 ('1998-08-28 12:27:00.0',58,132,43),
	 ('2016-07-06 03:44:00.0',85,106,63),
	 ('2007-09-10 08:27:00.0',13,120,71),
	 ('2015-05-23 00:42:00.0',48,73,86),
	 ('2016-04-27 15:19:00.0',68,52,83),
	 ('1999-01-03 19:36:00.0',55,20,52),
	 ('2014-01-02 18:55:00.0',14,12,35),
	 ('2017-09-05 21:21:00.0',89,99,88),
	 ('2003-02-07 22:40:00.0',61,91,33),
	 ('2001-05-17 10:54:00.0',84,122,48),
	 ('2010-10-01 01:57:00.0',27,106,29),
	 ('2016-03-18 17:44:00.0',10,30,48),
	 ('2013-10-05 14:06:00.0',97,75,62),
	 ('2014-10-19 12:45:00.0',60,119,66),
	 ('2002-08-22 12:48:00.0',94,130,44),
	 ('2012-09-01 18:15:00.0',80,18,49),
	 ('2005-12-10 02:52:00.0',85,18,76),
	 ('1999-01-14 06:58:00.0',26,55,27),
	 ('2012-07-10 09:14:00.0',80,105,81),
	 ('2005-10-05 09:53:00.0',52,18,28),
	 ('2001-06-30 22:53:00.0',20,17,48),
	 ('2015-07-07 10:21:00.0',62,93,48),
	 ('2000-02-16 17:22:00.0',13,76,54),
	 ('2008-06-12 19:44:00.0',89,7,43),
	 ('2003-05-06 23:56:00.0',59,18,65),
	 ('1999-11-02 00:54:00.0',50,54,88),
	 ('2011-05-07 08:02:00.0',96,128,78),
	 ('2007-02-24 08:38:00.0',11,133,45),
	 ('2008-12-02 17:52:00.0',79,54,42),
	 ('2015-01-27 21:13:00.0',65,38,74),
	 ('2007-08-27 23:28:00.0',72,24,31),
	 ('2014-04-01 06:55:00.0',4,43,86),
	 ('1998-08-02 08:52:00.0',51,147,23),
	 ('2013-11-22 01:57:00.0',29,33,49),
	 ('2018-02-08 00:24:00.0',43,70,86),
	 ('2002-09-05 18:34:00.0',46,155,62),
	 ('2000-02-10 21:26:00.0',33,33,69),
	 ('2002-01-05 06:49:00.0',13,88,32),
	 ('2017-05-10 00:44:00.0',93,92,14),
	 ('2018-07-24 20:11:00.0',25,151,44),
	 ('2016-01-05 01:52:00.0',23,47,71),
	 ('2003-05-17 18:33:00.0',39,131,66),
	 ('2000-04-15 11:15:00.0',6,91,50),
	 ('2010-05-19 22:38:00.0',65,4,29),
	 ('2009-06-05 21:32:00.0',72,3,62),
	 ('2017-10-21 14:25:00.0',32,152,14),
	 ('2015-03-02 03:00:00.0',90,34,71),
	 ('2008-08-26 11:20:00.0',16,43,53),
	 ('1998-05-23 22:47:00.0',23,8,63),
	 ('2000-08-05 00:56:00.0',23,78,65),
	 ('2001-10-21 08:10:00.0',21,93,27),
	 ('1997-11-06 07:55:00.0',1,78,84),
	 ('2007-03-05 23:54:00.0',61,102,46),
	 ('2011-03-14 15:41:00.0',7,75,47),
	 ('2016-05-18 17:02:00.0',68,150,86),
	 ('2007-06-29 06:15:00.0',65,102,42),
	 ('2014-03-01 23:48:00.0',97,122,77),
	 ('2005-01-09 08:49:00.0',40,52,49),
	 ('1999-10-03 01:10:00.0',5,125,28),
	 ('2007-05-18 11:40:00.0',91,124,77),
	 ('2015-11-13 04:00:00.0',13,76,84),
	 ('2009-02-24 12:01:00.0',95,84,89),
	 ('2018-02-17 19:44:00.0',67,74,86),
	 ('2013-03-14 14:42:00.0',50,115,67),
	 ('2011-06-18 23:40:00.0',61,20,19),
	 ('2017-05-09 00:14:00.0',82,16,55),
	 ('1997-12-27 21:52:00.0',72,126,80),
	 ('2014-07-08 14:27:00.0',19,125,88),
	 ('1997-06-12 15:43:00.0',1,74,36),
	 ('2012-03-04 03:47:00.0',41,143,49),
	 ('2011-03-26 22:34:00.0',96,4,79),
	 ('2013-07-27 22:36:00.0',66,106,42),
	 ('2014-11-06 06:19:00.0',7,46,85),
	 ('2000-09-23 12:34:00.0',72,70,20),
	 ('1998-06-12 20:03:00.0',54,61,90),
	 ('2009-09-06 23:38:00.0',52,192,73),
	 ('1998-02-12 21:08:00.0',71,111,54),
	 ('2009-03-13 04:26:00.0',75,36,61),
	 ('2016-11-09 15:34:00.0',84,79,13),
	 ('2018-07-23 15:50:00.0',52,54,60),
	 ('2000-05-28 23:02:00.0',90,53,45),
	 ('2009-05-06 22:55:00.0',57,183,17),
	 ('2016-05-07 20:22:00.0',49,99,74),
	 ('2003-11-16 06:33:00.0',4,60,77),
	 ('2005-05-18 14:05:00.0',38,191,35),
	 ('2002-06-07 04:53:00.0',7,38,78),
	 ('1999-04-19 12:12:00.0',98,147,82),
	 ('2004-01-06 23:56:00.0',40,72,68),
	 ('2009-02-22 08:20:00.0',67,145,89),
	 ('2017-09-11 14:25:00.0',27,125,51),
	 ('2007-05-21 21:17:00.0',18,114,49),
	 ('2013-10-24 11:19:00.0',73,62,61),
	 ('1998-11-08 15:35:00.0',81,96,51),
	 ('2003-05-07 02:51:00.0',65,148,65),
	 ('2008-09-12 00:02:00.0',8,165,86),
	 ('2018-05-14 10:24:00.0',19,79,43),
	 ('2015-01-15 12:14:00.0',28,184,39),
	 ('2013-05-27 18:13:00.0',46,75,81),
	 ('2017-06-05 10:18:00.0',61,85,53),
	 ('2005-11-11 10:57:00.0',43,24,89),
	 ('2010-09-21 17:00:00.0',33,30,90),
	 ('2012-01-12 01:00:00.0',2,99,27),
	 ('2006-08-29 22:47:00.0',85,16,61),
	 ('1999-07-19 22:22:00.0',91,78,13),
	 ('2007-04-01 02:35:00.0',19,179,79),
	 ('2006-01-02 14:12:00.0',68,43,90),
	 ('2015-01-01 08:02:00.0',16,80,70),
	 ('2000-08-20 02:15:00.0',92,9,48),
	 ('2004-11-14 06:19:00.0',40,93,75),
	 ('1999-07-24 12:56:00.0',36,122,69),
	 ('2006-09-17 17:41:00.0',53,186,90),
	 ('2007-02-17 22:41:00.0',41,158,73),
	 ('2001-10-09 06:07:00.0',54,73,63),
	 ('2017-10-26 02:16:00.0',93,30,71),
	 ('2001-08-30 00:01:00.0',40,52,71),
	 ('2011-07-09 01:07:00.0',86,79,19),
	 ('2008-09-17 12:27:00.0',5,144,72),
	 ('2014-08-22 19:40:00.0',73,146,47),
	 ('2000-06-02 06:11:00.0',36,63,32),
	 ('2010-11-21 19:47:00.0',7,65,26),
	 ('2016-04-06 00:56:00.0',53,115,89),
	 ('2005-09-09 04:41:00.0',27,141,27),
	 ('2018-09-11 01:20:00.0',84,118,45),
	 ('2019-02-22 23:36:00.0',94,22,43),
	 ('2011-02-17 12:57:00.0',43,146,65),
	 ('2013-07-10 18:25:00.0',15,25,88),
	 ('2006-04-22 08:05:00.0',67,50,36),
	 ('2007-07-19 11:32:00.0',50,118,71),
	 ('1998-01-14 12:23:00.0',93,132,89),
	 ('2008-10-19 10:34:00.0',92,52,67),
	 ('2002-04-17 09:05:00.0',41,125,39),
	 ('2013-06-01 23:35:00.0',91,60,70),
	 ('2003-10-26 17:54:00.0',63,24,85),
	 ('2001-06-09 19:33:00.0',53,131,19),
	 ('2016-12-22 06:52:00.0',45,38,43),
	 ('2012-03-26 06:00:00.0',63,24,84),
	 ('2016-01-14 10:15:00.0',69,137,74),
	 ('2008-06-02 05:32:00.0',47,136,28),
	 ('2019-06-22 07:58:00.0',42,114,73),
	 ('2003-05-14 17:19:00.0',73,30,69),
	 ('2002-08-01 19:39:00.0',62,108,36),
	 ('2013-03-19 06:29:00.0',86,4,65),
	 ('1998-09-09 17:57:00.0',56,30,39),
	 ('2003-09-22 14:17:00.0',92,146,54),
	 ('2015-05-23 08:43:00.0',28,131,90),
	 ('2001-05-27 11:20:00.0',16,45,89),
	 ('2018-05-25 00:12:00.0',27,38,65),
	 ('2012-03-29 20:52:00.0',79,146,68),
	 ('2011-06-14 21:12:00.0',97,103,76),
	 ('2012-09-23 21:53:00.0',5,36,74),
	 ('2009-06-12 06:15:00.0',51,80,83),
	 ('1997-03-07 09:34:00.0',69,154,69),
	 ('2000-05-07 20:58:00.0',56,78,63),
	 ('2013-10-18 00:55:00.0',3,132,53),
	 ('2003-05-09 12:13:00.0',11,101,65),
	 ('2010-03-07 18:29:00.0',75,109,51),
	 ('2001-05-14 00:39:00.0',42,125,20),
	 ('1998-11-05 06:21:00.0',76,121,46),
	 ('2013-05-23 12:09:00.0',48,92,60),
	 ('2010-02-16 06:13:00.0',75,78,89),
	 ('2019-07-10 08:51:00.0',59,26,84),
	 ('1998-04-23 11:32:00.0',27,91,86),
	 ('2013-11-19 07:51:00.0',65,158,51),
	 ('2008-01-07 22:24:00.0',82,57,65),
	 ('1999-12-10 22:47:00.0',59,60,46),
	 ('2009-07-19 22:26:00.0',40,84,52),
	 ('2006-08-26 19:25:00.0',41,77,71),
	 ('2001-09-30 14:42:00.0',11,77,51),
	 ('2016-07-23 06:42:00.0',15,142,31),
	 ('2017-01-28 20:34:00.0',57,131,50),
	 ('2012-11-26 17:02:00.0',47,73,43),
	 ('1999-09-30 02:34:00.0',92,118,82),
	 ('2018-11-07 01:26:00.0',43,103,79),
	 ('2012-04-22 03:08:00.0',68,146,20),
	 ('2008-11-05 06:54:00.0',84,156,52),
	 ('2011-08-20 05:55:00.0',62,3,83),
	 ('2013-08-26 07:05:00.0',30,142,29),
	 ('2009-03-06 03:43:00.0',98,9,89),
	 ('2015-04-25 03:07:00.0',33,3,76),
	 ('1998-01-23 07:25:00.0',16,41,39),
	 ('2002-05-27 01:44:00.0',40,99,65),
	 ('2006-07-17 11:51:00.0',5,86,65),
	 ('2011-05-30 07:34:00.0',93,106,52),
	 ('1999-10-28 09:33:00.0',20,58,84),
	 ('2015-08-07 03:38:00.0',68,99,81),
	 ('2000-02-17 22:32:00.0',89,156,76),
	 ('2006-06-20 01:40:00.0',82,98,47),
	 ('2001-12-29 01:40:00.0',35,43,83),
	 ('2018-09-09 04:34:00.0',52,80,71),
	 ('2008-10-22 08:57:00.0',70,33,48),
	 ('2015-05-02 04:58:00.0',76,65,82),
	 ('2012-02-21 03:17:00.0',73,12,51),
	 ('1997-08-09 14:17:00.0',45,2,69),
	 ('2015-11-06 07:14:00.0',26,70,82),
	 ('1998-09-16 21:41:00.0',42,192,71),
	 ('2006-08-04 09:48:00.0',8,52,76),
	 ('2010-07-01 11:39:00.0',44,59,85),
	 ('2005-05-28 03:12:00.0',53,72,86),
	 ('2004-03-10 00:25:00.0',42,50,89),
	 ('1998-01-26 01:58:00.0',61,64,76),
	 ('2015-03-15 03:02:00.0',65,116,65),
	 ('2008-04-05 23:43:00.0',76,117,46),
	 ('1997-11-25 20:52:00.0',81,100,54),
	 ('2007-01-05 06:51:00.0',84,91,49),
	 ('2004-08-08 21:09:00.0',25,77,69),
	 ('2013-11-24 09:02:00.0',30,124,35),
	 ('2006-05-29 07:13:00.0',70,69,84),
	 ('2015-10-11 03:44:00.0',19,108,19),
	 ('1999-06-05 22:12:00.0',71,116,29),
	 ('2009-10-20 20:45:00.0',63,155,27),
	 ('2003-12-08 21:40:00.0',74,86,89),
	 ('1998-12-10 07:53:00.0',5,22,49),
	 ('2017-09-06 08:54:00.0',88,62,76),
	 ('2013-03-04 06:17:00.0',82,189,50),
	 ('2017-08-05 00:21:00.0',9,39,61),
	 ('1999-09-01 10:02:00.0',86,37,51),
	 ('2000-01-16 12:46:00.0',15,61,67),
	 ('2006-12-13 21:34:00.0',54,143,19),
	 ('1998-06-17 23:46:00.0',29,5,60),
	 ('2015-09-18 11:17:00.0',43,79,72),
	 ('2012-05-06 01:17:00.0',65,47,74),
	 ('1999-06-07 03:10:00.0',58,186,29),
	 ('2017-07-16 21:45:00.0',91,2,31),
	 ('2009-07-12 10:48:00.0',91,109,90),
	 ('2006-08-06 15:55:00.0',41,22,28),
	 ('2016-05-30 00:09:00.0',38,118,20),
	 ('2019-04-29 23:08:00.0',65,45,84),
	 ('2011-02-01 14:53:00.0',52,68,60),
	 ('2016-06-14 19:34:00.0',73,23,50),
	 ('2005-09-20 17:25:00.0',51,12,13),
	 ('2007-05-25 01:22:00.0',63,124,39),
	 ('2008-12-16 09:21:00.0',27,100,54),
	 ('2016-03-17 11:10:00.0',35,35,79),
	 ('2000-01-17 02:33:00.0',71,81,46),
	 ('1998-03-30 08:49:00.0',44,54,81),
	 ('2009-05-14 15:19:00.0',53,153,83),
	 ('2017-01-05 07:11:00.0',40,142,68),
	 ('2014-03-02 03:45:00.0',19,142,65),
	 ('2005-07-09 22:58:00.0',66,46,19),
	 ('2001-03-12 21:00:00.0',9,31,47),
	 ('2018-11-14 17:32:00.0',44,31,60),
	 ('2012-11-15 19:28:00.0',19,126,42),
	 ('2004-07-18 14:21:00.0',68,89,54),
	 ('2008-03-09 04:34:00.0',15,63,84),
	 ('2001-05-28 02:58:00.0',85,79,51),
	 ('2009-10-22 18:33:00.0',49,118,31),
	 ('2000-08-14 12:02:00.0',94,142,88),
	 ('2017-04-12 08:28:00.0',92,143,79),
	 ('2019-02-13 01:13:00.0',76,154,50),
	 ('2011-12-23 22:23:00.0',53,148,72),
	 ('1999-10-17 05:16:00.0',18,101,60),
	 ('2014-11-16 09:45:00.0',52,41,47),
	 ('2016-11-05 21:29:00.0',71,43,51),
	 ('2000-07-15 21:51:00.0',59,130,89),
	 ('2015-06-20 02:44:00.0',15,142,36),
	 ('2002-07-13 09:25:00.0',44,61,13),
	 ('1997-11-24 10:56:00.0',78,189,46),
	 ('2007-04-06 00:52:00.0',88,129,47),
	 ('2013-02-16 02:34:00.0',95,68,65),
	 ('2019-04-08 20:34:00.0',63,24,51),
	 ('2003-06-20 12:21:00.0',29,84,71),
	 ('2008-08-16 17:15:00.0',41,118,84),
	 ('2016-07-29 14:07:00.0',14,134,63),
	 ('1998-11-21 02:05:00.0',91,132,32),
	 ('2001-01-07 18:42:00.0',21,114,69),
	 ('2015-12-23 23:32:00.0',42,31,72),
	 ('2004-07-08 10:02:00.0',38,64,19),
	 ('2011-01-07 03:13:00.0',86,5,20),
	 ('2016-01-29 00:48:00.0',5,80,47),
	 ('2005-01-15 04:00:00.0',59,77,13),
	 ('2002-05-14 08:02:00.0',55,21,88),
	 ('2006-12-29 15:19:00.0',57,125,51),
	 ('2014-08-11 19:42:00.0',7,20,46),
	 ('2000-07-18 12:46:00.0',9,140,89),
	 ('2017-03-26 04:33:00.0',86,91,65),
	 ('1998-08-21 07:44:00.0',29,112,65),
	 ('2002-03-01 11:20:00.0',49,30,29),
	 ('2013-09-29 01:56:00.0',72,24,89),
	 ('1997-12-07 23:51:00.0',74,124,32),
	 ('2018-12-22 15:58:00.0',16,54,83),
	 ('2001-10-12 08:19:00.0',64,139,68),
	 ('2017-01-24 00:45:00.0',78,39,79),
	 ('2004-06-06 17:01:00.0',18,48,65),
	 ('2008-05-01 05:57:00.0',54,151,20),
	 ('1999-08-10 09:33:00.0',90,68,53),
	 ('2016-04-02 12:03:00.0',69,82,86),
	 ('2005-07-16 18:40:00.0',22,127,49),
	 ('2006-07-20 22:01:00.0',52,61,76),
	 ('2018-09-06 20:57:00.0',93,26,13),
	 ('2010-03-05 06:14:00.0',63,87,19),
	 ('2014-10-15 09:15:00.0',67,126,72),
	 ('1999-04-20 01:52:00.0',22,125,90),
	 ('2000-09-27 10:44:00.0',66,122,50),
	 ('2009-08-07 17:55:00.0',53,91,89),
	 ('2016-03-01 23:32:00.0',68,65,39);

--GENERO DNI ALEATORIOS MEDICOS Y PACIENTES
	 ALTER TABLE MEDICOS 
	 ADD DNI INT 

	 SELECT * FROM MEDICOS

	 UPDATE MEDICOS

	SET DNI = CAST(1 + (10000000 - 1) * RAND(CHECKSUM(NEWID())) AS INT);
	GO

	ALTER TABLE PACIENTES
    ADD DNI INT
	GO
	UPDATE PACIENTES
	SET DNI = CAST(10000001 + (10000000 - 1) * RAND(CHECKSUM(NEWID())) AS INT);		
	
	SELECT * FROM PACIENTES

	--AÑADO UNIQUE 
	alter table PACIENTES
	ADD CONSTRAINT UQ_DniPaciente UNIQUE (DNI)
	
	alter table MEDICOS 
	ADD CONSTRAINT UQ_DniMedicos UNIQUE (DNI)