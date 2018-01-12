CREATE DATABASE SoilCare;
GO

use SoilCare;

CREATE TABLE Soil(
	Soil_id nvarchar(200), 
	Soil_name nvarchar(200), 
	Min_nutrient float,
	Max_nutrient float,
	Min_humidity float,
	Max_humidity float,
	Min_acidity float,
	Max_acidity float,
	Min_porosity float,
	Max_porosity float,
	Min_water_retention float,
	Max_water_retention float,
	Min_salinity float,
	Max_salinity float,
	CONSTRAINT Soil_pk PRIMARY KEY(Soil_id),
	CONSTRAINT Soil_check_nutrient CHECK (Min_nutrient <= Max_nutrient),
	CONSTRAINT Soil_check_humidity CHECK (Min_humidity <= Max_humidity),
	CONSTRAINT Soil_check_acidity CHECK (Min_acidity <= Max_acidity),
	CONSTRAINT Soil_check_porosity CHECK (Min_porosity <= Max_porosity),
	CONSTRAINT Soil_check_water CHECK (Min_water_retention <= Max_water_retention),
	CONSTRAINT Soil_check_salinity CHECK (Min_salinity <= Max_salinity),
);
CREATE TABLE Plant(
	Plant_id nvarchar(200),
	Plant_name nvarchar(200),
	Plant_image nvarchar(200), 
	Plant_discription nvarchar(200),
	Soil_id nvarchar(200),
	[Status] nvarchar(200),
	CONSTRAINT Plant_pk PRIMARY KEY(Plant_id),
	CONSTRAINT Plant_fk FOREIGN KEY(Soil_id) REFERENCES Soil(Soil_id),
);




/*
CREATE TABLE Compatibility(
	Plant_id nvarchar(200), 
	Soil_id nvarchar(200), 
	CONSTRAINT Compatibility_pk PRIMARY KEY(Plant_id, Soil_id),
	CONSTRAINT Compatibility_fk1 FOREIGN KEY(Plant_id) REFERENCES Plant(Plant_id),
	CONSTRAINT Compatibility_fk2 FOREIGN KEY(Soil_id) REFERENCES Soil(Soil_id),
);
*/

CREATE TABLE [User](
	User_id nvarchar(200),
	User_name nvarchar(200),
	Telephone nvarchar(200),
	Region nvarchar(200),
	User_image nvarchar(200),
	Created_at datetime,
	CONSTRAINT User_pk PRIMARY KEY(User_id),
	CONSTRAINT User_unique UNIQUE(Telephone),
);

CREATE TABLE Land(
	Land_id nvarchar(200), 
	Land_name nvarchar(200),
	Land_address nvarchar(200),
	Land_image nvarchar(200),
	Land_area float,
	User_id nvarchar(200),
	Created_at datetime,
	[Status] nvarchar(200),
	CONSTRAINT Land_pk PRIMARY KEY(Land_id),
	CONSTRAINT Land_fk FOREIGN KEY(User_id) REFERENCES [User](User_id),
);

CREATE TABLE Measure(
	Measure_id nvarchar(200),
	Created_at datetime,
	Land_id nvarchar(200),
	Plant_id nvarchar(200),
	Nutrient float,
	Humidity float, 
	Acidity float,
	Porosity float,
	Water_retention float,
	Salinity float,
	Rate int,
	CONSTRAINT Measure_pk PRIMARY KEY(Measure_id),
	CONSTRAINT Measure_fk1 FOREIGN KEY(Land_id) REFERENCES Land(Land_id),
	CONSTRAINT Measure_fk2 FOREIGN KEY(Plant_id) REFERENCES Plant(Plant_id),
);

CREATE TABLE Solution(
	Solution_id nvarchar(200),
	Solution_name nvarchar(200),
	[Value] float,
	Unit_name nvarchar(200),
	Unit_symbol nvarchar(200),
	Quantity nvarchar(200),
	Solution_purpose nvarchar(200),
	Solution_discription nvarchar(200),
	Owner nvarchar(200),
	CONSTRAINT Solution_pk PRIMARY KEY(Solution_id)
);
CREATE TABLE SolutionOffer(
	Measure_id nvarchar(200),
	Solution_id nvarchar(200),
	Value_config float,
	Unit_symbol_config nvarchar(200),
	Unit_name_config nvarchar(200),
	[Status] nvarchar(200),
	CONSTRAINT SolutionOffer_pk PRIMARY KEY(Measure_id, Solution_id),
	CONSTRAINT SolutionOffer_fk1 FOREIGN KEY(Measure_id) REFERENCES Measure(Measure_id),
	CONSTRAINT SolutionOffer_fk2 FOREIGN KEY(Solution_id) REFERENCES Solution(Solution_id),
);

