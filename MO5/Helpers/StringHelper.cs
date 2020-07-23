using System;

namespace MO5.Helpers
{
  public static class StringHelper
  {
    public static string Left(this String str, int length)
    {
      return str.Length > length ? str.Substring(0, length) : str;
    }

    public static string Right(this String str, int length)
    {
      return str.Length > length ? str.Substring(str.Length - length, length) : str;
    }
  }
}