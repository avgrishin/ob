using MO5.Areas.Code.Models;
using MO5.Helpers;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MO5.Areas.Code.Controllers
{
  public class EnregController : EnregBaseController
  {
    public EnregController(IEnregRepository _enregRepository, IConfigurationProvider configProvider) : base(_enregRepository, configProvider, 0, "ЖРПК")
    {
    }

    public EnregController(IEnregRepository _enregRepository, IConfigurationProvider configProvider, int _enregTypeID, string _title) : base(_enregRepository, configProvider, _enregTypeID, _title)
    {
    }

    [Authorize(Roles = "jrpk, jrpki, jrpkr")]
    public new ActionResult Index()
    {
      return base.Index();
    }
    [Authorize(Roles = "jrpk, jrpki, jrpkr")]
    public new ActionResult getEnregList(DateTime? d1, DateTime? d2, Boolean? sd, string sort, string dir)
    {
      return base.getEnregList(d1, d2, sd, sort, dir);
    }
    [Authorize(Roles = "jrpk, jrpki")]
    public new async Task<ActionResult> addEnreg(List<Enregistrement> data)
    {
      return await base.addEnreg(data);
    }
    [Authorize(Roles = "jrpk, jrpkm")]
    public new async Task<ActionResult> updEnreg(List<Enregistrement> data)
    {
      return await base.updEnreg(data);
    }
    [Authorize(Roles = "admin")]
    public new ActionResult delEnreg(List<tEnregistrement> data)
    {
      return base.delEnreg(data);
    }
    
  }
}