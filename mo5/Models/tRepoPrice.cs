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
    
    public partial class tRepoPrice
    {
        public int ID { get; set; }
        public int SecurityID { get; set; }
        public int TreatyID { get; set; }
        public System.DateTime RDate { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<int> Num { get; set; }
        public Nullable<double> Price { get; set; }
    }
}
