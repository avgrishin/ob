using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MO5.Areas.Code.Models
{
  public class MoneyLimits
  {
    public string Tag { get; set; }
    public string Curr { get; set; }
    public string Client { get; set; }
    public int Limit { get; set; }
    public decimal Balance { get; set; }
  }

  public class DepoTreaty
  {
    public string Number { get; set; }
    public DateTime? DateStart { get; set; }
    public string Client { get; set; }
  }
}