using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTiemNet.DTO
{
    public class ComputerAllInformationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int TimeActive { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public int? ComputerDetailId { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public string ComputerDetailName { get; set; }
        public string Cpu { get; set; }
        public string Ram { get; set; }
        public string HardDisk { get; set; }
        public string Graphic { get; set; }
        public string Monitor { get; set; }

    }
}