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
    
    public partial class tExchPrice
    {
        public int ID { get; set; }
        public int ExchSecurityID { get; set; }
        public System.DateTime PriceDate { get; set; }
        public Nullable<double> Nominal { get; set; }
        public Nullable<double> Open { get; set; }
        public Nullable<double> Low { get; set; }
        public Nullable<double> High { get; set; }
        public Nullable<double> Close { get; set; }
        public Nullable<double> LegalClosePrice { get; set; }
        public Nullable<double> HighBid { get; set; }
        public Nullable<double> Bid { get; set; }
        public Nullable<double> Offer { get; set; }
        public Nullable<double> LowOffer { get; set; }
        public Nullable<double> Avg { get; set; }
        public Nullable<double> Volume { get; set; }
        public Nullable<double> AdmittedQuote { get; set; }
        public Nullable<double> MarkerPrice2 { get; set; }
        public Nullable<double> MarketPrice3 { get; set; }
        public Nullable<double> Value { get; set; }
        public Nullable<double> NumTrades { get; set; }
        public Nullable<double> LastPrice { get; set; }
        public Nullable<double> AccInt { get; set; }
        public Nullable<double> Duration { get; set; }
        public Nullable<double> YieldAtWap { get; set; }
        public Nullable<double> YieldClose { get; set; }
        public Nullable<System.DateTime> OfferDate { get; set; }
    
        public virtual tExchSecurity tExchSecurity { get; set; }
    }
}