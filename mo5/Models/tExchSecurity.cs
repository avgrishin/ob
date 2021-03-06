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
    
    public partial class tExchSecurity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tExchSecurity()
        {
            this.tExchPrice = new HashSet<tExchPrice>();
        }
    
        public int ID { get; set; }
        public Nullable<int> TypeID { get; set; }
        public Nullable<int> ListLevel { get; set; }
        public string BoardID { get; set; }
        public string SecID { get; set; }
        public string SecBrief { get; set; }
        public string CurrencyID { get; set; }
        public string ISIN { get; set; }
        public string RegNumber { get; set; }
        public string SecType { get; set; }
        public Nullable<int> SecurityID { get; set; }
        public Nullable<bool> IsDisabled { get; set; }
        public Nullable<System.DateTime> InDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tExchPrice> tExchPrice { get; set; }
    }
}
