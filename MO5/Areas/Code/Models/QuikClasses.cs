using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MO5.Areas.Code.Models
{
  public class QuikDeal
  {
    public string Brief { get; set; }
    public string ClassCode { get; set; }
    public string ClientAccount { get; set; }
    public string ClientCode { get; set; }
    public DateTime? CreateDate { get; set; }
    public double? DealPrice { get; set; }
    public byte? Direction { get; set; }
    public int? ID { get; set; }
    public string ISIN { get; set; }
    public int? Lot { get; set; }
    public string Name { get; set; }
    public decimal? Num { get; set; }
    public int? QuikID { get; set; }
    public string Path { get; set; }
    public string RegNumber { get; set; }
    public int? StatusID { get; set; }
    public int? SecurityID { get; set; }
    public int? Trans_ID { get; set; }
    public int? TreatyID { get; set; }
    public bool? Type { get; set; }
    public int? TypeO { get; set; }
    public byte Form { get; set; }
  }
}