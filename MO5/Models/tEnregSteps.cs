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
    
    public partial class tEnregSteps
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tEnregSteps()
        {
            this.tEnregistrement = new HashSet<tEnregistrement>();
            this.tEnregistrementLog = new HashSet<tEnregistrementLog>();
        }
    
        public System.Guid ID { get; set; }
        public int EnregID { get; set; }
        public int Step { get; set; }
        public bool IsConfirmed { get; set; }
        public Nullable<System.DateTime> InDateTime { get; set; }
        public Nullable<System.DateTime> InDateTimeC { get; set; }
        public string UserName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tEnregistrement> tEnregistrement { get; set; }
        public virtual tEnregistrement tEnregistrement1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tEnregistrementLog> tEnregistrementLog { get; set; }
        public virtual tEnregSteps tEnregSteps1 { get; set; }
        public virtual tEnregSteps tEnregSteps2 { get; set; }
    }
}