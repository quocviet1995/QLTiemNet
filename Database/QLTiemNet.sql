create database QLTiemNetDB;

go

use QLTiemNetDB;
go

create table [Role]
(
	Id int NOT NULL IDENTITY,
	Name nvarchar(256) NOT NULL,
	PRIMARY KEY (Id)
);

go

create table [User]
(
	Id int NOT NULL IDENTITY,
	Name nvarchar(256) NOT NULL,
	UserName varchar(256) NOT NULL UNIQUE,
	Password varchar(256) NOT NULL,
	TimeRemaining int  Default 0 NOT NULL,
	RoleId int NOT NULL,
	PRIMARY KEY (Id),
	FOREIGN KEY(RoleId) REFERENCES dbo.[Role](Id),
);

go

create table Status
(
	Id int NOT NULL IDENTITY,
	Name nvarchar(256) NOT NULL,
	PRIMARY KEY (Id)
);

go

create table ComputerDetail
(
	Id int NOT NULL IDENTITY,
	Name nvarchar (256) NOT NULL,
	Cpu nvarchar(256) NOT NULL,
    Ram nvarchar(256) NOT NULL,
    HardDisk nvarchar(256) NOT NULL,
    Graphic nvarchar(256) NOT NULL,
    Monitor nvarchar(256) NOT NULL,
	PRIMARY KEY (Id)
);

go

create table Computer
(
	Id int NOT NULL IDENTITY,
	Name nvarchar(256) NOT NULL,
	TimeStart datetime Default NULL,
	TimeEnd datetime Default NULL,
	TimeActive int Default 0 NOT NULL,
	UserId int Default NULL,
	ComputerDetailId int NOT NULL,
	StatusId int NOT NULL,
	PRIMARY KEY (Id),
	FOREIGN KEY(UserId) REFERENCES dbo.[User](Id),
	FOREIGN KEY(ComputerDetailId) REFERENCES dbo.ComputerDetail(Id),
	FOREIGN KEY(StatusId) REFERENCES dbo.Status(Id)
);

go

create table Scheduler
(
	Id int NOT NULL IDENTITY,
	Time datetime NOT NULL,
	UserId int  NOT NULL,
	ComputerId int NOT NULL,
	StatusId int NOT NULL,
	PRIMARY KEY (Id),
	FOREIGN KEY(UserId) REFERENCES dbo.[User](Id),
	FOREIGN KEY(ComputerId) REFERENCES dbo.Computer(Id),
	FOREIGN KEY(StatusId) REFERENCES dbo.Status(Id)
);

go

create table Bill
(
	Id int NOT NULL IDENTITY,
	TimeStart datetime NOT NULL,
	TimeEnd datetime NOT NULL,
	UserId int NOT NULL,
	ComputerId int NOT NULL,
	StatusId int NOT NULL,
	PRIMARY KEY (Id),
	FOREIGN KEY(UserId) REFERENCES dbo.[User](Id),
	FOREIGN KEY(ComputerId) REFERENCES dbo.Computer(Id),
	FOREIGN KEY(StatusId) REFERENCES dbo.Status(Id)
);