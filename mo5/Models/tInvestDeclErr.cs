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
    
    public partial class tInvestDeclErr
    {
        public int lD { get; set; }
        public int InvestDeclID { get; set; }
        public int InvestDeclWhereID { get; set; }
        public int FinInstID { get; set; }
        public Nullable<System.DateTime> Dt { get; set; }
        public Nullable<int> FLAG_Group { get; set; }
        public Nullable<int> FLAG_Calculation { get; set; }
        public Nullable<double> numerator { get; set; }
        public Nullable<double> denominator { get; set; }
        public Nullable<double> coef { get; set; }
        public Nullable<double> StartValue { get; set; }
        public Nullable<double> StopValue { get; set; }
        public Nullable<int> SecurityID { get; set; }
        public Nullable<double> Num { get; set; }
        public Nullable<double> Course { get; set; }
        public Nullable<double> Qty { get; set; }
        public Nullable<double> coefS { get; set; }
        public Nullable<int> TreatyID { get; set; }
    }
}
