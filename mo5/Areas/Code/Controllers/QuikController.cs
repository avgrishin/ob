using MO5.Areas.Code.Models;
using MO5.Helpers;
using MO5.Models;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MO5.Areas.Code.Controllers
{
  [Authorize(Roles="quik")]
  public class QuikController : Controller
  {
    private readonly IQuikRepository quikRepository;
    public QuikController(IQuikRepository _quikRepository)
    {
      quikRepository = _quikRepository;
    }
    // GET: Code/Quik
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult getQuikDealList(DateTime d1, DateTime d2, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = quikRepository.getQuikDealList(d1, d2, (Guid)Membership.GetUser().ProviderUserKey, sort, dir) } };
    }
    public ActionResult getQuikDealCrDt(DateTime cd)
    {
      return new JsonnResult { Data = new { success = true, data = quikRepository.getQuikDealList(cd, (Guid)Membership.GetUser().ProviderUserKey) } };
    }
    public ActionResult addQuikDeal(List<QuikDeal> data)
    {
      return new JsonnResult { Data = new { success = true, data = quikRepository.addQuikDeal(data, (Guid)Membership.GetUser().ProviderUserKey) } };
    }
    public ActionResult updQuikDeal(List<QuikDeal> data)
    {
      return new JsonnResult { Data = new { success = true, data = quikRepository.updQuikDeal(data, (Guid)Membership.GetUser().ProviderUserKey) } };
    }
    public ActionResult delQuikDeal(List<QuikDeal> data)
    {
      return new JsonnResult { Data = new { success = true, data = quikRepository.delQuikDeal(data, (Guid)Membership.GetUser().ProviderUserKey) } };
    }
    public ActionResult setQuikUserPath(int QuikID, string Path)
    {
      return new JsonnResult { Data = new { success = true, data = quikRepository.setQuikUserPath(QuikID, Path, (Guid)Membership.GetUser().ProviderUserKey) } };
    }

    public ActionResult getQuikUserList()
    {
      return new JsonnResult { Data = new { success = true, data = quikRepository.getQuikUserList((Guid)Membership.GetUser().ProviderUserKey) } };
    }
    public ActionResult getQuikDealTypeList()
    {
      return new JsonnResult { Data = new { success = true, data = quikRepository.getQuikDealTypeList() } };
    }
    public ActionResult getQuikSec(string q)
    {
      return new JsonnResult { Data = new { data = quikRepository.getQuikSec(q) } };
    }
    public ActionResult getQuikCln(int QuikID)
    {
      return new JsonnResult { Data = new { data = quikRepository.getQuikCln(QuikID) } };
    }
    public ActionResult getQuikClnF(string q, int QuikID)
    {
      return new JsonnResult { Data = new { data = quikRepository.getQuikCln(q, QuikID) } };
    }
    public ActionResult getCreateDate()
    {
      return new JsonnResult { Data = new { success = true, Data = DateTime.Now } };
    }
    public ActionResult sendQuikOrder(DateTime cd)
    {
      return new JsonnResult { Data = new { success = quikRepository.sendQuikOrder(cd, User.Identity.Name) } };
    }
    public ActionResult addQuikModDeal(DateTime cd)
    {
      return new JsonnResult { Data = new { success = quikRepository.addQuikModDeal(cd, User.Identity.Name) } };
    }
    public ActionResult checkQuikDeal(DateTime cd)
    {
      var q = quikRepository.checkQuikDeal(cd, User.Identity.Name);
      return new JsonnResult { Data = new { success = q.Item1, message = q.Item2 } };
    }
    public ActionResult getMyTreatyCode(int QuikID) =>
      new JsonnResult { Data = new { data = quikRepository.getMyTreatyCode(QuikID, User.Identity.Name) } };
    public ActionResult addMyTreatyCode(List<tMyTreatyCode> data) =>
      new JsonnResult { Data = new { data = quikRepository.addMyTreatyCode(data, User.Identity.Name) } };
    public ActionResult delMyTreatyCode(List<tMyTreatyCode> data) =>
      new JsonnResult { Data = new { success = quikRepository.delMyTreatyCode(data, User.Identity.Name) } };

  }

}