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
    
    public partial class tEnregDTSteps
    {
        public int ID { get; set; }
        public int DocTypeID { get; set; }
        public int Step { get; set; }
        public string EmailTo { get; set; }
        public string Name { get; set; }
    
        public virtual tObjClassifier tObjClassifier { get; set; }
    }
}