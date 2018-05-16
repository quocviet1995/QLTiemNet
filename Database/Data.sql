use QLTiemNetDB;
go

INSERT INTO Users (Name,UserName,Password,TimeRemaining,Role) VALUES
('Admin','admin','admin',null,'admin'),
('Cá sấu','casau','123456',null,'user'),
('Chó con','chocon','123456',null,'user'),
('Vịt lùn','vitlun','123456',null,'user'),
('Gián biết bay','gianbietbay','123456',null,'user');

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

INSERT INTO Computer(Name,TimeActive,TimeStart,TimeEnd,StatusId,UserId,ComputerDetailId) VALUES
('May 1',null,null,null,'2',null,'1'),
('May 2',null,null,null,'2',null,'1'),
('May 3',null,null,null,'2',null,'1'),
('May 4',null,null,null,'2',null,'1'),
('May 5',null,null,null,'2',null,'1'),
('May 6',null,null,null,'2',null,'2'),
('May 7',null,null,null,'2',null,'2'),
('May 8',null,null,null,'2',null,'2'),
('May 9',null,null,null,'2',null,'2'),
('May 10',null,null,null,'2',null,'2');