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
    
    public partial class tInvestDeclSec
    {
        public int InvestDeclSecID { get; set; }
        public Nullable<int> InvestDeclWhereID { get; set; }
        public Nullable<int> FLAG_Not { get; set; }
        public Nullable<int> FLAG_Div { get; set; }
        public Nullable<bool> Enb { get; set; }
        public Nullable<int> ObjID { get; set; }
        public Nullable<int> ObjType { get; set; }
    
        public virtual tInvestDeclWhere tInvestDeclWhere { get; set; }
    }
}
