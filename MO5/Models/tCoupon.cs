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
    
    public partial class tCoupon
    {
        public int ID { get; set; }
        public Nullable<int> SecurityID { get; set; }
        public Nullable<int> Num { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public Nullable<System.DateTime> DatePay { get; set; }
    
        public virtual tSecurity tSecurity { get; set; }
    }
}
