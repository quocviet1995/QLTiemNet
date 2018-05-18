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

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Bills = new HashSet<Bill>();
            this.Computers = new HashSet<Computer>();
            this.Schedulers = new HashSet<Scheduler>();
        }

        [DisplayName("User")]
        public int Id { get; set; }
        [DisplayName("Full Name")]
        public string Name { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Time Remaining")]
        public int TimeRemaining { get; set; }
        [DisplayName("Role")]
        public int RoleId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Computer> Computers { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Scheduler> Schedulers { get; set; }
    }
}
