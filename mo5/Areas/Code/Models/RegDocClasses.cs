using System;
using System.Collections.Generic;
using System.Linq;
using MO5.Models;

namespace MO5.Areas.Code.Models
{
  public class RegDocEmail: tRegDoc
  {
    public string TypeName { get; set; }
    public string InstOwner { get; set; }
    public string EmailToName { get; set; }
    public string EmailCcName { get; set; }
    public string OrigUser { get; set; }
  }

  //public class RegDocEmailKey
  //{
  //  public string EmailTo { get; set; }
  //}
}