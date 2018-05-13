namespace QLTiemNet.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;


    public class User
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public DateTime TimeRemaining { get; set; }

    }

    public class Status
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class Computer
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime TimeActive { get; set; }

        ///thuộc tính khoá ngoại
        public int UserId { get; set; }
        public int ComputerDetailId { get; set; }
        public int StatusId { get; set; }
    }

    public class ComputerDetail
    {
        public int Id { get; set; }
        public String Cpu { get; set; }
        public String Ram { get; set; }
        public String HardDisk { get; set; }
        public String Graphic { get; set; }
        public String Monitor { get; set; }

    }

    public class Scheduler
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }

        ///thuộc tính khoá ngoại
        public int UserId { get; set; }
        public int ComputerId { get; set; }
        public int StatusId { get; set; }
    }

    public class Bill
    {
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }

        ///thuộc tính khoá ngoại
        public int UserId { get; set; }
        public int ComputerId { get; set; }
        public int StatusId { get; set; }
    }

    public class QLTiemNetDB : DbContext
    {
        public QLTiemNetDB(): base("name=QLTiemNetString")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<ComputerDetail> ComputerDetails { get; set; }
        public DbSet<Scheduler> Schedulers { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
} 