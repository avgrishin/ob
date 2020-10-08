using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MO5.Helpers
{
  public interface IConfigurationProvider : IDisposable
  {
    T GetValue<T>(string key);
    T GetConnectionString<T>(string key);
  }
}
