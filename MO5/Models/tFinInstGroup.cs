//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MO5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tFinInstGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tFinInstGroup()
        {
            this.tFinInst = new HashSet<tFinInst>();
        }
    
        public int FinInstGroupID { get; set; }
        public string Name { get; set; }
        public int FinInstGroupTypeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tFinInst> tFinInst { get; set; }
    }
}
