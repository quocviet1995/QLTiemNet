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
        public String Phone { get; set; }
        public int Diamond { get; set; }

    }

    public class Computer
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Status { get; set; }
        public DateTime TimeRemaining { get; set; }

        ///thuộc tính khoá ngoại
        public int UserId { get; set; }
    }

    public class QLTiemNetDB : DbContext
    {
        public QLTiemNetDB(): base("name=QLTiemNetString")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Computer> Computers { get; set; }
    }
} 