using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MO5.Areas.Code.Models
{
  public class PaymentDoc
  {
    public int ID { get; set; }
    public string BICO { get; set; }
    public string RAccO { get; set; }
    public string BankO { get; set; }
    public string KAccO { get; set; }
    public string INNO { get; set; }
    public string KPPO { get; set; }
    public string NameO { get; set; }
    public decimal? Amount { get; set; }
    public string Reference { get; set; }
    public string Client { get; set; }
    public string Treaty { get; set; }
    public DateTime? TreatyDate { get; set; }
  }
}