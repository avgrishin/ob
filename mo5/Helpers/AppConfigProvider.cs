using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Configuration;

namespace MO5.Helpers
{
  public class AppConfigProvider : IConfigurationProvider
  {
    public void Dispose()
    {
    }

    public T GetConnectionString<T>(string key)
    {
      var value = ConfigurationManager.ConnectionStrings[key].ConnectionString;
      return (T)Convert.ChangeType(value, typeof(T));
    }

    public T GetValue<T>(string key)
    {
      var value = ConfigurationManager.AppSettings[key];
      var t = typeof(T);
      t = Nullable.GetUnderlyingType(t) ?? t;

      return value == null ? default : (T)Convert.ChangeType(value, t);
    }
  }

  public class AppConfigProviderModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IConfigurationProvider>().To<AppConfigProvider>().InRequestScope();
    }
  }

}