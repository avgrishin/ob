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
    
    public partial class tQuikUser
    {
        public int QuikID { get; set; }
        public System.Guid UserID { get; set; }
        public string Path { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual tQuik tQuik { get; set; }
    }
}
