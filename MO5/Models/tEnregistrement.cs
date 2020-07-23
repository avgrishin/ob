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
    
    public partial class tEnregistrement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tEnregistrement()
        {
            this.tEnregSteps1 = new HashSet<tEnregSteps>();
            this.tEnregistrementLog = new HashSet<tEnregistrementLog>();
        }
    
        public int ID { get; set; }
        public string Numero { get; set; }
        public Nullable<int> TreatyID { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public Nullable<System.DateTime> RecuDate { get; set; }
        public string Temps { get; set; }
        public Nullable<System.DateTime> Tm { get; set; }
        public Nullable<bool> Original { get; set; }
        public Nullable<bool> ScanCopy { get; set; }
        public Nullable<int> DocTypeID { get; set; }
        public Nullable<int> EmployeID { get; set; }
        public string Remarque { get; set; }
        public Nullable<int> DaysDog { get; set; }
        public Nullable<System.DateTime> DateDog { get; set; }
        public Nullable<int> DayDogTypeID { get; set; }
        public Nullable<int> DaysDoc { get; set; }
        public Nullable<System.DateTime> DateDoc { get; set; }
        public Nullable<int> DaysFact { get; set; }
        public Nullable<System.DateTime> DateFact { get; set; }
        public string FileName { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string DocNum { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> MethodID { get; set; }
        public Nullable<System.DateTime> InDateTime { get; set; }
        public string FileNameO { get; set; }
        public Nullable<System.Guid> StepID { get; set; }
        public string FileNameD { get; set; }
        public Nullable<bool> FullOut { get; set; }
        public string UserName { get; set; }
        public int EnregTypeID { get; set; }
    
        public virtual tObjClassifier tObjClassifier { get; set; }
        public virtual tObjClassifier tObjClassifier1 { get; set; }
        public virtual tEnregSteps tEnregSteps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tEnregSteps> tEnregSteps1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tEnregistrementLog> tEnregistrementLog { get; set; }
    }
}
