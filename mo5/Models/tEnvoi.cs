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
    
    public partial class tEnvoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tEnvoi()
        {
            this.tEnvoiExec = new HashSet<tEnvoiExec>();
            this.tEnvoiHoraire = new HashSet<tEnvoiHoraire>();
        }
    
        public int ID { get; set; }
        public Nullable<int> Num { get; set; }
        public string TypeInf { get; set; }
        public string SrokRask { get; set; }
        public string Mesto { get; set; }
        public string Osnovan { get; set; }
        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
        public string PoryadPredst { get; set; }
        public string Periodich { get; set; }
        public Nullable<int> PeriodichID { get; set; }
        public string VidAktiv { get; set; }
        public string SrokRass { get; set; }
        public Nullable<bool> IsAuto { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public Nullable<int> TypeID { get; set; }
        public string Responsible { get; set; }
        public Nullable<int> InstOwnerID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tEnvoiExec> tEnvoiExec { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tEnvoiHoraire> tEnvoiHoraire { get; set; }
        public virtual tObjClassifier tObjClassifier { get; set; }
    }
}
