create database QLTiemNetDB;

go

use QLTiemNetDB;
go

create table Users
(
	Id int NOT NULL IDENTITY,
	Name nvarchar(256) NOT NULL,
	UserName varchar(256) NOT NULL,
	Password varchar(256) NOT NULL,
	TimeRemaining datetime NOT NULL,
	PRIMARY KEY (Id)
);

create table Status
(
	Id int NOT NULL IDENTITY,
	Name nvarchar(256) NOT NULL,
	PRIMARY KEY (Id)
)

create table ComputerDetail
(
	Id int NOT NULL IDENTITY,
	Name varchar (256),
	Cpu varchar(256) NOT NULL,
    Ram varchar(256) NOT NULL,
    HardDisk varchar(256) NOT NULL,
    Graphic varchar(256) NOT NULL,
    Monitor varchar(256) NOT NULL,
	PRIMARY KEY (Id)
)

create table Computer
(
	Id int NOT NULL IDENTITY,
	Name varchar(256) NOT NULL,
	TimeStart datetime default null,
	TimeEnd datetime default null,
	TimeActive datetime default null,
	UserId int,
	ComputerDetailId int,
	StatusId int,
	PRIMARY KEY (Id),
	FOREIGN KEY(UserId) REFERENCES dbo.Users(Id),
	FOREIGN KEY(ComputerDetailId) REFERENCES dbo.ComputerDetail(Id),
	FOREIGN KEY(StatusId) REFERENCES dbo.Status(Id)
)

create table Scheduler
(
	Id int NOT NULL IDENTITY,
	Time datetime NOT NULL,
	UserId int,
	ComputerId int,
	StatusId int,
	PRIMARY KEY (Id),
	FOREIGN KEY(UserId) REFERENCES dbo.Users(Id),
	FOREIGN KEY(ComputerId) REFERENCES dbo.Computer(Id),
	FOREIGN KEY(StatusId) REFERENCES dbo.Status(Id)
)

create table Bill
(
	Id int NOT NULL IDENTITY,
	TimeStart datetime NOT NULL,
	TimeEnd datetime NOT NULL,
	UserId int,
	ComputerId int,
	StatusId int,
	PRIMARY KEY (Id),
	FOREIGN KEY(UserId) REFERENCES dbo.Users(Id),
	FOREIGN KEY(ComputerId) REFERENCES dbo.Computer(Id),
	FOREIGN KEY(StatusId) REFERENCES dbo.Status(Id)
) 