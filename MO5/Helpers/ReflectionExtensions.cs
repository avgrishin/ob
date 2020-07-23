using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MO5.Helpers
{
  public static class ReflectionExtensions
  {
    public static IEnumerable<PropertyInfo> GetProperties<T>(BindingFlags binding, PropertyReflectionOptions options = PropertyReflectionOptions.All)
    {
      var properties = typeof(T).GetProperties(binding);
      bool all = (options & PropertyReflectionOptions.All) != 0;
      bool ignoreIndexer = (options & PropertyReflectionOptions.IgnoreIndexer) != 0;
      bool ignoreEnumerable = (options & PropertyReflectionOptions.IgnoreEnumerable) != 0;
      foreach (var property in properties)
      {
        if (!all)
        {
          if (ignoreIndexer && IsIndexer(property))
          {
            continue;
          }
          if (ignoreIndexer && !property.PropertyType.Equals(typeof(string)) && IsEnumerable(property))
          {
            continue;
          }
        }
        yield return property;
      }
    }

    private static bool IsIndexer(PropertyInfo property)
    {
      var parameters = property.GetIndexParameters();
      if (parameters != null && parameters.Length > 0)
      {
        return true;
      }
      return false;
    }

    private static bool IsEnumerable(PropertyInfo property)
    {
      return property.PropertyType.GetInterfaces().Any(x => x.Equals(typeof(System.Collections.IEnumerable)));
    }
  }

  [Flags]
  public enum PropertyReflectionOptions : int
  {
    All = 0,
    IgnoreIndexer = 1,
    IgnoreEnumerable = 2
  }
}