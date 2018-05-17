use QLTiemNetDB;
go

INSERT INTO Role (Name) VALUES
('admin'),
('user');

INSERT INTO [User] (Name,UserName,Password,TimeRemaining,RoleId) VALUES
(N'Admin','admin','admin','86400','1'),
(N'Cá sấu','casau','123456','86400','2'),
(N'Chó con','chocon','123456','43200','2'),
(N'Vịt lùn','vitlun','123456','43200','2'),
(N'Gián biết bay','gianbietbay','123456','21600','2');

INSERT INTO Status (Name) VALUES 
('Run'),
('Empty'),
('Maintenance'),
('Complete'),
('Waiting'),
('Cancel');

INSERT INTO ComputerDetail(Name,Cpu,Ram,Graphic,HardDisk,Monitor) VALUES
('Combo_1_i7','Intel, Core i7, 2.80 GHz','16 GB, DDR4, 2400MHz','NVIDIA Geforce GTX 1050Ti 4 GB','15.6 inchWide-View','1TB (SATA) 7200rpm HDD'),
('Combo_2_i7','Intel, Core i7, 2.80 GHz','8 GB, DDR4, 2400MHz','NVIDIA Geforce GTX 1050 4 GB','15.6 inchWide-View','1TB (SATA) 7200rpm HDD'),
('Combo_1_i5','Intel Core i5-8300H','8 GB, DDR4, 2400MHz','NVIDIA Geforce GTX 1050Ti 4 GB','15.6 inchWide-View','1TB (SATA) 7200rpm HDD'),
('Combo_2_i5','Intel Core i5-8300H','8 GB, DDR4, 2400MHz','NVIDIA Geforce GTX 1050 4 GB','15.6 inchWide-View','1TB (SATA) 7200rpm HDD');

INSERT INTO Computer(Name,TimeStart,TimeEnd,StatusId,UserId,ComputerDetailId) VALUES
(N'Máy 1',null,null,'2',null,'1'),
(N'Máy 2',null,null,'2',null,'1'),
(N'Máy 3',null,null,'2',null,'1'),
(N'Máy 4',null,null,'2',null,'1'),
(N'Máy 5',null,null,'2',null,'1'),
(N'Máy 6',null,null,'2',null,'2'),
(N'Máy 7',null,null,'2',null,'2'),
(N'Máy 8',null,null,'2',null,'2'),
(N'Máy 9',null,null,'2',null,'2'),
(N'Máy 10',null,null,'2',null,'2');