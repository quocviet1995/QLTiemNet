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
    
    public partial class ComputerDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ComputerDetail()
        {
            this.Computers = new HashSet<Computer>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpu { get; set; }
        public string Ram { get; set; }
        public string HardDisk { get; set; }
        public string Graphic { get; set; }
        public string Monitor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Computer> Computers { get; set; }
    }
}