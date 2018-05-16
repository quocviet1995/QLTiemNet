using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTiemNet.DTO
{
    public class ComputerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public DateTime? TimeActive { get; set; }
        public int? UserId { get; set; }
        public int? ComputerDetailId { get; set; }
        public int? StatusId { get; set; }

    }
}