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
    
    public partial class tEnregistrementLog
    {
        public int ID { get; set; }
        public int EnregID { get; set; }
        public System.Guid EnregStepID { get; set; }
        public System.DateTime InDateTime { get; set; }
        public string Login { get; set; }
    
        public virtual tEnregistrement tEnregistrement { get; set; }
        public virtual tEnregSteps tEnregSteps { get; set; }
    }
}
