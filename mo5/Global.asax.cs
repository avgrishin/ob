using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MO5
{
  public class MvcApplication : NinjectHttpApplication
  {
    //public void Application_Start()
    //{
    //  AreaRegistration.RegisterAllAreas();
    //  FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    //  RouteConfig.RegisterRoutes(RouteTable.Routes);
    //  BundleConfig.RegisterBundles(BundleTable.Bundles);
    //}

    protected override IKernel CreateKernel()
    {
      return new StandardKernel(
        new Areas.Code.Models.EnvoiModule(), 
        new Models.HomeModule(), 
        new Areas.Code.Models.ObjClsModule(), 
        new Areas.Code.Models.InvestDeclModule(),
        new Areas.Code.Models.JurModule(),
        new Areas.Code.Models.RegDocModule(),
        new Areas.Code.Models.EnregModule(),
        new Areas.Code.Models.RiskModule(),
        new Hubs.HubConnectionModule(),
        new Areas.Code.Models.QuikModule(),
        new Helpers.AppConfigProviderModule(),
        new Areas.Code.Models.PaymentModule()
      );


      //var kernel = new StandardKernel();
      //kernel.Load(Assembly.GetExecutingAssembly());
      //return kernel;
    }

    protected override void OnApplicationStarted()
    {
      base.OnApplicationStarted();
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
  }
}
