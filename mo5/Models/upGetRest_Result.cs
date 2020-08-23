using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MO5.Models
{
  public class upGetRest_Result
  {
    public int? TreatyID { get; set; }
    public string trName { get; set; }
    public int? FinInstID { get; set; }
    public string clName { get; set; }
    public int? SecurityID { get; set; }
    public string secName { get; set; }
    public string ISIN { get; set; }
    public string RegNumber { get; set; }
    public int? Class { get; set; }
    public string ClassName { get; set; }
    public DateTime? DateEnd { get; set; }
    public string Issuer { get; set; }
    public string IssuerINN { get; set; }
    public double? Num { get; set; }
    public double? Course { get; set; }
    public double? Coupon { get; set; }
    public int? AccType { get; set; }
    public string Account { get; set; }
    public decimal? Qty { get; set; }
  }
}