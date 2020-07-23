﻿using System.Collections.Generic;
using System.Dynamic;
using System.Web.Routing;

namespace MO5.Helpers
{
  public static class DynamicHelper
  {
    public static ExpandoObject ToExpando(this object anonymousObject)
    {
      IDictionary<string, object> anonymousDictionary = new RouteValueDictionary(anonymousObject);
      IDictionary<string, object> expando = new ExpandoObject();
      foreach (var item in anonymousDictionary)
        expando.Add(item);
      return (ExpandoObject)expando;
    } 

  }
}