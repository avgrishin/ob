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
    
    public partial class tInvestDecl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tInvestDecl()
        {
            this.tInvestDeclWhere = new HashSet<tInvestDeclWhere>();
            this.tInvestDeclLink = new HashSet<tInvestDeclLink>();
        }
    
        public int InvestDeclID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> Enb { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public Nullable<System.DateTime> Modify_Date { get; set; }
        public Nullable<int> Create_UserID { get; set; }
        public Nullable<int> Modify_UserID { get; set; }
        public Nullable<int> InvestDeclTypeID { get; set; }
    
        public virtual tInvestDeclType tInvestDeclType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tInvestDeclWhere> tInvestDeclWhere { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tInvestDeclLink> tInvestDeclLink { get; set; }
    }
}
