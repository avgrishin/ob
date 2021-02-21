using MO5.Areas.Code.Models;
using MO5.Helpers;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MO5.Areas.Code.Controllers
{
  public class Enreg3Controller : EnregBaseController
  {
    public Enreg3Controller(IEnregRepository _enregRepository, IConfigurationProvider configProvider) : base(_enregRepository, configProvider, 2, "ЖРПК ДЕПО")
    {
    }

    [Authorize(Roles = "jrpkd")]
    public new ActionResult Index()
    {
      return base.Index();
    }
    [Authorize(Roles = "jrpkd")]
    public new ActionResult getEnregList(DateTime? d1, DateTime? d2, Boolean? sd, string sort, string dir)
    {
      return base.getEnregList(d1, d2, sd, sort, dir);
    }
    [Authorize(Roles = "jrpkd")]
    public new async Task<ActionResult> addEnreg(List<Enregistrement> data)
    {
      return await base.addEnreg(data);
    }
    [Authorize(Roles = "jrpkd")]
    public new async Task<ActionResult> updEnreg(List<Enregistrement> data)
    {
      return await base.updEnreg(data);
    }
    [Authorize(Roles = "jrpkd")]
    public new ActionResult delEnreg(List<tEnregistrement> data)
    {
      return base.delEnreg(data);
    }
    
  }
}