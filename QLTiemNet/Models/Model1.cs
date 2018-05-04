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

    }
}