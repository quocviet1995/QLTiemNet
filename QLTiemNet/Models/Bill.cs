//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLTiemNet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        public int Id { get; set; }
        public System.DateTime TimeStart { get; set; }
        public System.DateTime TimeEnd { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> ComputerId { get; set; }
        public Nullable<int> StatusId { get; set; }
    
        public virtual Computer Computer { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}
