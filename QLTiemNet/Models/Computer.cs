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
    using System.ComponentModel;

    public partial class Computer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Computer()
        {
            this.Bills = new HashSet<Bill>();
            this.Schedulers = new HashSet<Scheduler>();
        }

        [DisplayName("Computer")]
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Time Start")]
        public Nullable<System.DateTime> TimeStart { get; set; }
        [DisplayName("Time End")]
        public Nullable<System.DateTime> TimeEnd { get; set; }
        [DisplayName("Time Active")]
        public int TimeActive { get; set; }
        [DisplayName("User")]
        public Nullable<int> UserId { get; set; }
        [DisplayName("Computer Detail")]
        public int ComputerDetailId { get; set; }
        [DisplayName("Status")]
        public int StatusId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ComputerDetail ComputerDetail { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Scheduler> Schedulers { get; set; }
    }
}
