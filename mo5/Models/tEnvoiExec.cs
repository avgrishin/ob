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
    
    public partial class tEnvoiExec
    {
        public int ID { get; set; }
        public int EnvoiID { get; set; }
        public Nullable<System.DateTime> Date1 { get; set; }
        public Nullable<System.DateTime> Date2 { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> Date3 { get; set; }
        public Nullable<System.DateTime> InDateTime { get; set; }
    
        public virtual tEnvoi tEnvoi { get; set; }
    }
}