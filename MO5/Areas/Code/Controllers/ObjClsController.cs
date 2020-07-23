using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MO5.Areas.Code.Models;
using MO5.Helpers;
using MO5.Models;

namespace MO5.Areas.Code.Controllers
{
  [Authorize(Roles = "admin")]
  public class ObjClsController : Controller
  {
    public IObjClsRepository objClsRepository;

    public ObjClsController(IObjClsRepository _objClsRepository)
    {
      objClsRepository = _objClsRepository;
    }

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult GetObjClsNodes(int id)
    {
      return new JsonnResult { Data = new { success = true, data = objClsRepository.GetObjClsNode(id) } };
    }

    public ActionResult addObjCls(List<tObjClassifier> data)
    {
      return new JsonnResult { Data = new { success = true, data = objClsRepository.addObjCls(data) } };
    }

    public ActionResult updObjCls(List<tObjClassifier> data)
    {
      return new JsonnResult { Data = new { success = true, data = objClsRepository.updObjCls(data) } };
    }

    public ActionResult delObjCls(List<tObjClassifier> data)
    {
      return new JsonnResult { Data = new { success = objClsRepository.delObjCls(data) } };
    }

    //public ActionResult ObjClsRel(int id, string sort, string dir, int? start, int? limit)
    //{
    //  var q = objClsRepository.GetObjClsRel(id, sort, dir);
    //  return Json(new { data = q.Skip(start ?? 0).Take(limit ?? 500), totalCount = q.Count() });
    //}

    //public ActionResult ObjClsRelDel(List<OCR> data)
    //{
    //  if (objClsRepository.DelObjClsRel(data))
    //    return Json(new { success = true });
    //  else
    //    return Json(new { success = false, msg = "Ошибка при удалении" });
    //}

  }
}
