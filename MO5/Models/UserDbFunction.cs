using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MO5.Models
{
  public static class UserDbFunction
  {
    [DbFunction("MiddleOfficeModel.Store", "ufEmailToStr")]
    public static string ufEmailToStr(string Email)
    {
      throw new NotSupportedException();
    }

    [DbFunction("MiddleOfficeModel.Store", "ufAddWorkDate")]
    public static DateTime? ufAddWorkDate(DateTime? dt, int? days)
    {
      throw new NotSupportedException();
    }
  }
}