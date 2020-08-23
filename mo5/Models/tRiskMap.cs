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
    
    public partial class tRiskMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tRiskMap()
        {
            this.tRiskMapHoraire = new HashSet<tRiskMapHoraire>();
        }
    
        public int ID { get; set; }
        public string JurPersonne { get; set; }
        public string Dep { get; set; }
        public string BisProc { get; set; }
        public Nullable<int> NumRisk { get; set; }
        public string RiskName { get; set; }
        public Nullable<int> Influence { get; set; }
        public Nullable<int> Probabilite { get; set; }
        public Nullable<int> ControlForce { get; set; }
        public Nullable<bool> EssentielRisk { get; set; }
        public string But { get; set; }
        public string PossesseurBut { get; set; }
        public string Control { get; set; }
        public string PossesseurControl { get; set; }
        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
        public Nullable<System.DateTime> InDateTime { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tRiskMapHoraire> tRiskMapHoraire { get; set; }
    }
}
